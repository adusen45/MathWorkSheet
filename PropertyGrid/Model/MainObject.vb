Imports System.Reflection

Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports HelperLibrary

Public Class MainObject
    Inherits RangeObservableCollection(Of [Property])

    Private _object As Object

    Public Sub New(obj As Object)
        MyBase.New()

        Me._object = obj

        If obj IsNot Nothing Then
            Dim propInfoList As List(Of PropertyInfo) = obj.GetType.GetProperties().ToList
            Dim propQuery = From propInfo As PropertyInfo In propInfoList
                            Where propInfo.CanWrite = True And propInfo.CanRead = True
                          Select New [Property](owner:=obj, info:=propInfo)

            Me.AddRange(propQuery.ToList)

        End If


    End Sub

    Public ReadOnly Property [Object] As Object
        Get
            Return Me._object

        End Get
    End Property

    Public Function DeepCopy() As Object
        Return ObjectCloner.DeepCopy(_object)

    End Function

End Class

Public Class ObjectCloner

    Public Shared Function DeepCopy(ByVal [Object] As Object) As Object
        Return DeserializeFromByteArray(SerializeToByteArray([Object]))

    End Function

    Shared Function SerializeToByteArray(ByVal [Object] As Object) As Byte()
        Dim retByte() As Byte
        Dim locMs As MemoryStream = New MemoryStream
        Dim locBinaryFormatter As New BinaryFormatter(Nothing, _
                        New StreamingContext(StreamingContextStates.Clone))

        locBinaryFormatter.Serialize(locMs, [Object])
        locMs.Flush()
        locMs.Close()
        retByte = locMs.ToArray()

        Return retByte

    End Function

    Shared Function DeserializeFromByteArray(ByVal by As Byte()) As Object
        Dim locObject As Object
        Dim locFs As MemoryStream = New MemoryStream(by)
        Dim locBinaryFormatter As New BinaryFormatter(Nothing, _
                         New StreamingContext(StreamingContextStates.File))

        locObject = locBinaryFormatter.Deserialize(locFs)
        locFs.Close()

        Return locObject

    End Function

End Class

Public Class CreateDeepCopy
    Public Shared Function Clone(Of T)(ByVal objectToClone As T) As T
        'If the source object is null, simply returns the current
        'object (as a default)
        If Object.ReferenceEquals(objectToClone, Nothing) Then
            Return objectToClone
        End If
        'Creates a new formatter whose behavior is for cloning purposes
        Dim formatter As New BinaryFormatter(Nothing,
        New StreamingContext(
        StreamingContextStates.Clone))
        'Serializes to a memory stream
        Dim ms As New MemoryStream
        Using ms
            formatter.Serialize(ms, objectToClone)
            'Gets back to the first stream byte
            ms.Seek(0, SeekOrigin.Begin)
            'Deserializes the object graph to a new T object
            Return CType(formatter.Deserialize(ms), T)
        End Using
    End Function
End Class

'Public Function DeepCopy() As Object
'    Dim returnType As Type = _object.GetType()
'    Dim tempMainObject As New MainObject(returnType.GetConstructors()(0).Invoke(parameters:={}))

'    For index = 0 To Me.Count - 1
'        ' this is where the copying is done
'        If Me(index).Accessor Is Nothing Then
'            ' the value of the property is nothing
'            tempMainObject(index).Accessor = Me(index).Accessor

'        ElseIf Me(index).Type.IsValueType Then
'            ' this object is value type
'            If (Me(index).Type.IsPrimitive Or Me(index).Type.IsEnum) Then
'                ' direct assignment copies to destination
'                tempMainObject(index).Accessor = Me(index).Accessor

'            Else
'                ' direct assignment copies only the object to destination
'                ' the properties could be reference types and only copy
'                ' the addresses
'                tempMainObject(index).Accessor = New MainObject(Me(index).Accessor).DeepCopy()

'            End If

'        Else
'            ' this object is reference type
'            If Me(index).Type Is GetType(String) Then
'                ' construct a new string
'                'Dim sdk = New String("hjgjh".ToArray)
'                tempMainObject(index).Accessor = New String(Me(index).Accessor)

'            ElseIf Me(index).Type.IsArray Then
'                ' clone this array

'            Else
'                ' this is a class or delegate
'                tempMainObject(index).Accessor = New MainObject(Me(index).Accessor).DeepCopy()
'                tempMainObject(index).Accessor = New String(Me(index).Accessor)

'            End If

'        End If

'    Next

'    Return tempMainObject._object

'End Function