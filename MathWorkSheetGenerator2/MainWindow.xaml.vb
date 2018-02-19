Imports System.ComponentModel
Imports PropertyGrid

Class MainWindow

    Private _obj1 As New Class1
    Private _obj2 As New Class2
    Private _obj3 As New Class3

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        propGrd.DataContext = _obj1

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)

    End Sub
End Class


<Serializable()> Public Class Class1
    Private _prop1 As Boolean
    Private _prop2 As Base
    Private _prop3 As Date
    Private _prop4 As Boolean
    Private _prop5 As New Class2

    <Description("Styles give you a practical way to reuse groups of property settings.")>
    Public Property Property1Property As Boolean
        Get
            Return _prop1

        End Get
        Set(value As Boolean)
            _prop1 = value

        End Set
    End Property

    '<Description("They’re a great first step that can help you build consistent, well-organized interfaces—but they’re also broadly limited")>
    'Public Property Prop2 As Base
    '    Get
    '        Return _prop2

    '    End Get
    '    Set(value As Base)
    '        _prop2 = value

    '    End Set
    'End Property

    <PropertyEditing(PropertyEditingAttribute.EditModes.EditDialog, GetType(EditObjectDialog)), _
    Description("WPF defines a Label control, which is also able to display text.")>
    Public Property Prop5pROPERpROPERTY As Class2
        Get
            Return _prop5

        End Get
        Set(value As Class2)
            _prop5 = value

        End Set
    End Property

    <Description("The idea is simple: you (or another developer) create a behavior that encapsulates a common bit of user-interface functionality.")>
    Public Property Prop3 As Date
        Get
            Return _prop3

        End Get
        Set(value As Date)
            _prop3 = value

        End Set
    End Property

    <Description("This functionality can be basic (such as starting a storyboard or navigating to a hyperlink).")>
    Public Property Prop4 As Boolean
        Get
            Return _prop4

        End Get
        Set(value As Boolean)
            _prop4 = value

        End Set
    End Property

End Class

<Serializable()> Public Class Class2
    Private _field1 As Boolean
    Private _field2 As Integer
    Private _field3 As Boolean

    Public Property Field1 As Boolean
        Get
            Return _field1

        End Get
        Set(value As Boolean)
            _field1 = value

        End Set
    End Property

    Public Property Field2 As Integer
        Get
            Return Me._field2

        End Get
        Set(value As Integer)
            _field2 = value

        End Set
    End Property

    Public Property Field3 As Boolean
        Get
            Return _field3

        End Get
        Set(value As Boolean)
            _field3 = value

        End Set
    End Property

    Public Shared Function Parse(s As String) As Class2
        Try
            Dim cvb = Strings.Split(s, ",")
            Dim cas As New Class2() With {._field1 = Boolean.Parse(cvb(0)), _
                                          ._field2 = Integer.Parse(Strings.Trim(cvb(1))), _
                                          ._field3 = Boolean.Parse(cvb(2))}

            Return cas

        Catch ex As Exception

            Throw New Exception("String is not a valid Class2 object")

        End Try

    End Function

    Public Overrides Function ToString() As String
        Return _field1.ToString + "," + _field2.ToString + "," + _field3.ToString

    End Function

End Class

Public Class Class3
    Public Property Attr1 As Short
        Get

        End Get
        Set(value As Short)

        End Set
    End Property

    Public Property Attr2 As Integer
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    Public Property Attr3 As Boolean
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

    Public Property Attr4 As String
        Get

        End Get
        Set(value As String)

        End Set
    End Property

    Public Property Attr5 As Boolean
        Get

        End Get
        Set(value As Boolean)

        End Set
    End Property

End Class

Public Class Base
    Private _field1 As Boolean
    Private _field2 As Integer
    Private _field3 As Boolean

    Public Sub New()
        With Me
            ._field1 = True
            ._field2 = 0
            ._field3 = True

        End With
    End Sub

    Public Property Field1 As Boolean
        Get
            Return _field1

        End Get
        Set(value As Boolean)
            _field1 = value

        End Set
    End Property

    Public Property Field2 As Integer
        Get
            Return Me._field2

        End Get
        Set(value As Integer)
            _field2 = value

        End Set
    End Property

    Public Property Field3 As Boolean
        Get
            Return _field3

        End Get
        Set(value As Boolean)
            _field3 = value

        End Set
    End Property

    Public Shared Function Parse(s As String) As Base
        Try
            Dim cvb = Strings.Split(s, ",")
            Dim cas As New Base() With {._field1 = Boolean.Parse(cvb(0)), _
                                          ._field2 = Integer.Parse(Strings.Trim(cvb(1))), _
                                          ._field3 = Boolean.Parse(cvb(2))}

            Return cas

        Catch ex As Exception

            Throw New Exception("String is not a valid base object")

        End Try

    End Function

    Public Overrides Function ToString() As String
        Return _field1.ToString + "," + _field2.ToString + "," + _field3.ToString

    End Function

End Class

Public Class Derived1
    Inherits Base
    Private _prop1 As Boolean
    Private _prop2 As Integer
    Private _prop3 As Date

    Public Sub New()
        MyBase.New()

        With Me
            ._prop1 = False
            ._prop2 = 9
            ._prop3 = DateTime.Now

        End With
    End Sub

    <Description("Styles give you a practical way to reuse groups of property settings.")>
    Public Property Property1PropertyProperty As Boolean
        Get
            Return _prop1

        End Get
        Set(value As Boolean)
            _prop1 = value

        End Set
    End Property

    <Description("They’re a great first step that can help you build consistent, well-organized interfaces—but they’re also broadly limited")>
    Public Property Prop2 As Integer
        Get
            Return _prop2

        End Get
        Set(value As Integer)
            _prop2 = value

        End Set
    End Property

    <Description("The idea is simple: you (or another developer) create a behavior that encapsulates a common bit of user-interface functionality.")>
    Public Property Prop3 As Date
        Get
            Return _prop3

        End Get
        Set(value As Date)
            _prop3 = value

        End Set
    End Property

End Class

Public Class Derived2
    Inherits Base

    Private _prop4 As Boolean
    Private _prop5 As Class2

    Public Sub New()
        MyBase.New()

        With Me
            ._prop4 = True
            ._prop5 = New Class2

        End With
    End Sub

    <Description("WPF defines a Label control, which is also able to display text.")>
    Public Property Prop5pROPERpROPERTY As Class2
        Get
            Return _prop5

        End Get
        Set(value As Class2)
            _prop5 = value

        End Set
    End Property

    <Description("This functionality can be basic (such as starting a storyboard or navigating to a hyperlink).")>
    Public Property Prop4 As Boolean
        Get
            Return _prop4

        End Get
        Set(value As Boolean)
            _prop4 = value

        End Set
    End Property

End Class