Public Class NewObjectDialog
    Implements IDialogService

    Private _value As Object
    Private _prop As [Property]

    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog
        Me._prop = prop

        ' construct a fresh object
        Dim tempObject = prop.Type.GetConstructors()(0).Invoke(parameters:={})

        ' set the datacontext of the property grid so that the object can be displayed
        Me.pGrid.DataContext = tempObject

        Dim dlgResult = Me.ShowDialog()
        Return If(dlgResult, True, False)

    End Function

    Public ReadOnly Property Value As Object Implements IDialogService.Value
        Get
            Return Me._value

        End Get
    End Property

    Public ReadOnly Property [Property] As [Property] Implements IDialogService.Property
        Get
            Return Me._prop

        End Get
    End Property

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = True

        ' set the value here
        Me._value = Me.pGrid.DataContext

        Me.Close()

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = False
        Me.Close()

    End Sub

End Class



'Imports HelperLibrary

'Public Class EditCollectionDialog
'    Implements IDialogService

'    Private _standardValues As New RangeObservableCollection(Of Object)
'    Private _collectionValues As New RangeObservableCollection(Of Object)

'    Public Sub New()

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.

'    End Sub

'    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog

'    End Function

'    Public ReadOnly Property [Property] As [Property] Implements IDialogService.Property
'        Get

'        End Get
'    End Property

'    Public ReadOnly Property Value As Object Implements IDialogService.Value
'        Get

'        End Get
'    End Property

'    Public Property StandardValues As RangeObservableCollection(Of Object)
'        Get
'            Return _standardValues

'        End Get
'        Set(value As RangeObservableCollection(Of Object))
'            _standardValues = value

'        End Set
'    End Property

'    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
'        Static isSelected As Boolean = False
'        Static backBrush As Brush

'        If Not isSelected Then
'            isSelected = True

'            ' save the original brush
'            backBrush = CType(sender, Button).Background
'            ' set the new brush
'            CType(sender, Button).Background = New SolidColorBrush(SystemColors.ControlLightColor)

'            ' add the number base conversion question type to the template
'            'lstQuestionClasses.Add(_ConvertToBinary)

'        Else
'            isSelected = False

'            ' return the original brush
'            CType(sender, Button).Background = backBrush

'            ' remove the number base conversion question type from the template
'            ' _simpleQuestionControls.Remove(_ConvertToBinary)

'        End If

'    End Sub

'End Class


'Public Class EditCollectionDialog
'    Implements IDialogService

'    Private _simpleQuestionControls As New RangeObservableCollection(Of SimpleControl)

'    Private _value As Object
'    Private _prop As [Property]

'    Private _ConvertToBinary As New ConvertToBinary

'    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog
'        Me._prop = prop

'        ' construct a clone of the object in the accessor
'        ' the property's type should have a clone implementation/deep copy implementation
'        Dim tempObject = New MainObject(prop.Accessor).DeepCopy

'        ' set the datacontext of the property grid so that the object can be displayed
'        Me.pGrid.DataContext = tempObject

'        Dim dlgResult = Me.ShowDialog()
'        Return If(dlgResult, True, False)

'    End Function

'    Public ReadOnly Property Value As Object Implements IDialogService.Value
'        Get
'            Return Me._value

'        End Get
'    End Property

'    Public ReadOnly Property [Property] As [Property] Implements IDialogService.Property
'        Get
'            Return Me._prop

'        End Get
'    End Property

'    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
'        '' add the number base conversion question type to the template
'        '_simpleQuestionControls.Add(New ConvertToBinary)
'        Static isSelected As Boolean = False
'        Static backBrush As Brush

'        If Not isSelected Then
'            isSelected = True

'            ' save the original brush
'            backBrush = CType(sender, Button).Background
'            ' set the new brush
'            CType(sender, Button).Background = New SolidColorBrush(SystemColors.ControlLightColor)

'            ' add the number base conversion question type to the template
'            _simpleQuestionControls.Add(_ConvertToBinary)

'        Else
'            isSelected = False

'            ' return the original brush
'            CType(sender, Button).Background = backBrush

'            ' remove the number base conversion question type from the template
'            _simpleQuestionControls.Remove(_ConvertToBinary)

'        End If

'    End Sub

'    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
'        ' add a number base conversion question type to the template
'        _simpleQuestionControls.Add(New ConvertFromBinary)

'    End Sub

'    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
'        Me.DialogResult = True

'        ' set the value here
'        ' actually set the clone, the clone is stored in this
'        ' datacontext. so the code below is ok
'        Me._value = Me.pGrid.DataContext

'        Me.Close()

'    End Sub

'    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
'        Me.DialogResult = False
'        Me.Close()

'    End Sub

'    Private Function pGridy() As Object
'        Throw New NotImplementedException
'    End Function

'    Private Sub m_Click(sender As Object, e As RoutedEventArgs) Handles m.Click
'        lstQuestionClasses()

'    End Sub
'End Class
