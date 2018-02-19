Imports System.Reflection
Imports System.ComponentModel
Imports HelperLibrary

Public Class [Property]
    Inherits NotifierBase

    Private Shared _defaultControlDictionary As New SortedDictionary(Of TypeCode, Type)

    Private _propertyInfo As PropertyInfo
    Private _propertyOwner As Object
    Private _toStringMethod As MethodInfo
    Private _parseMethod As MethodInfo

    Shared Sub New()
        With _defaultControlDictionary
            .Add(TypeCode.Boolean, GetType(CheckBox))
            .Add(TypeCode.DateTime, GetType(DatePicker))

        End With
    End Sub

    Public Sub New(owner As Object, info As PropertyInfo)
        With Me
            ._propertyOwner = owner
            ._propertyInfo = info

            'Dim allMethods = info.PropertyType.GetMethods()
            'Dim allToStringMethods = (From mthd In allMethods
            '                    Where mthd.Name = "ToString" And mthd.ReturnType = GetType(String) And mthd.GetParameters.Count = 0)

            '._toStringMethod = allToStringMethods(0)

            ''Dim allParseMethods = (From mthd In allMethods
            ''                    Where mthd.Name = "Parse" And mthd.ReturnType = info.PropertyType _
            ''                    And mthd.GetParameters.Count = 1)

            'Dim sdf = (From mthd In allMethods
            '                    Where mthd.Name = "Parse" And mthd.ReturnType = info.PropertyType)(0)

            'Dim ght = sdf.GetParameters

            'Dim allParseMethods = (From mthd In allMethods
            '                    Where mthd.Name = "Parse" And mthd.ReturnType = info.PropertyType _
            '                    And (From param In mthd.GetParameters
            '                         Where param.ParameterType = GetType(String)).Count = 1)


            'Dim allMethods2 = info.PropertyType.GetMethods(BindingFlags.DeclaredOnly Xor BindingFlags.Public Xor BindingFlags.Static)


        End With

    End Sub

    Public ReadOnly Property Owner As Object
        Get
            Return _propertyOwner

        End Get
    End Property

    Public ReadOnly Property Category As String
        Get
            Dim categoryAttr As CategoryAttribute = _propertyInfo.GetCustomAttributes(GetType(CategoryAttribute))(0)
            Return If(categoryAttr Is Nothing, "", categoryAttr.Category)

        End Get
    End Property

    Public ReadOnly Property Description As String
        Get
            Dim descriptionAttr As DescriptionAttribute = _propertyInfo.GetCustomAttributes(GetType(DescriptionAttribute))(0)
            Return If(descriptionAttr Is Nothing, "", descriptionAttr.Description)

        End Get
    End Property

    Public ReadOnly Property TypeConverter As IValueConverter
        Get
            Dim convertAttr As ConverterAttribute = _propertyInfo.GetCustomAttributes(GetType(ConverterAttribute))(0)
            Return If(convertAttr Is Nothing, Nothing, convertAttr.CreateConverter(bindingSource:=Me))

        End Get
    End Property

    Public ReadOnly Property EditMode As PropertyEditingAttribute.EditModes
        Get
            Dim editAttr As PropertyEditingAttribute = _propertyInfo.GetCustomAttributes(GetType(PropertyEditingAttribute))(0)
            Return If(editAttr Is Nothing, PropertyEditingAttribute.EditModes.Default, editAttr.EditMode)

        End Get
    End Property

    Public ReadOnly Property CollectionItemType As Type
        Get
            Dim editAttr As PropertyEditingAttribute = _propertyInfo.GetCustomAttributes(GetType(PropertyEditingAttribute))(0)
            Return If(editAttr Is Nothing, Nothing, editAttr.CollectionItemType)

        End Get
    End Property

    Public Function CreateDialogService() As IDialogService
        Dim editAttr As PropertyEditingAttribute = _propertyInfo.GetCustomAttributes(GetType(PropertyEditingAttribute))(0)

        If editAttr Is Nothing Then
            Return Nothing

        Else
            Return editAttr.Dialog

        End If

    End Function

    Private Function ControlForType(t As TypeCode) As Type
        Try
            Return _defaultControlDictionary(t)

        Catch ex As Exception
            ' primitive type not found in dictionary
            ' return textbox
            Return GetType(TextBox)

        End Try
    End Function

    Public ReadOnly Property DefaultControlType As Type
        Get
            Return If(Not Type.IsPrimitive, Nothing, Me.ControlForType(System.Type.GetTypeCode(Type)))

        End Get
    End Property

    Public ReadOnly Property Type As Type
        Get
            Return _propertyInfo.PropertyType

        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return _propertyInfo.Name

        End Get
    End Property

    Public Property Accessor
        Get
            Return _propertyInfo.GetValue(_propertyOwner)

        End Get
        Set(value)
            _propertyInfo.SetValue(_propertyOwner, value)

            OnPropertyChanged("Accessor")

        End Set
    End Property

    Public Function GetStandardValues() As RangeObservableCollection(Of Object)
        Dim standardValuesMethods = (From mthd In Me._propertyInfo.PropertyType.GetMethods
                            Where mthd.Name = "GetStandardValues").ToList

        If standardValuesMethods.Count = 0 Then
            ' no such method is defined, throw exception
            Throw New Exception("GetStandardValues method not defined for " + Me._propertyInfo.PropertyType.ToString)

        Else
            ' retieve the standardValues
            Dim standardValues = standardValuesMethods(0).Invoke(obj:=Nothing, parameters:={})
            Dim retList = New RangeObservableCollection(Of Object)
            retList.AddRange(standardValues)
            Return retList

        End If

    End Function

    Public Function GetStandardValues(T As Type) As RangeObservableCollection(Of Object)
        Dim standardValuesMethods = (From mthd In T.GetMethods
                            Where mthd.Name = "GetStandardValues").ToList

        If standardValuesMethods.Count = 0 Then
            ' no such method is defined, throw exception
            Throw New Exception("GetStandardValues method not defined for " + Me._propertyInfo.PropertyType.ToString)

        Else
            ' retieve the standardValues
            Dim standardValues = standardValuesMethods(0).Invoke(obj:=Nothing, parameters:={})
            Dim retList = New RangeObservableCollection(Of Object)
            retList.AddRange(standardValues)
            Return retList

        End If

    End Function

End Class
