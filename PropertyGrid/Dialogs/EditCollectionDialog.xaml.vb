Imports HelperLibrary

Public Class EditCollectionDialog
    Implements IDialogService

    Private _standardValues As New RangeObservableCollection(Of Object)
    Private _selectionFromStandardValues 'As New RangeObservableCollection(Of Object)

    Private _value As Object
    Private _prop As [Property]

    Private ReadOnly defaultButtonBrush As New SolidColorBrush(Color.FromArgb(255, 221, 221, 221))

    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog
        Me._prop = prop

        ' construct a clone of the object in the accessor
        ' the property's type should have a clone implementation/deep copy implementation
        'Dim tempList = New MainObject(prop.Accessor).DeepCopy
        Me._selectionFromStandardValues = New MainObject(prop.Accessor).DeepCopy

        ' set the cloned object to the list of selected values
        ' so that it can be displayed and edited
        'Me._selectionFromStandardValues.AddRange(tempList)

        Dim dlgResult = Me.ShowDialog()
        Return If(dlgResult, True, False)

    End Function

    Public ReadOnly Property [Property] As [Property] Implements IDialogService.Property
        Get
            Return _prop

        End Get
    End Property

    Public ReadOnly Property Value As Object Implements IDialogService.Value
        Get
            Return _value

        End Get
    End Property

    Public Property StandardValues As RangeObservableCollection(Of Object)
        Get
            Return _standardValues

        End Get
        Set(value As RangeObservableCollection(Of Object))
            _standardValues = value
            lstQuestionClasses.DataContext = Me._standardValues

        End Set
    End Property

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim dfg = CType(sender, Button).Background
        Dim fgh = New SolidColorBrush(Colors.AliceBlue)
        Dim defaultButtonBrush As SolidColorBrush = Me.FindResource("defaultButtonBrush")

        If CType(sender, Button).Background Is defaultButtonBrush Then
            ' set the new brush
            CType(sender, Button).Background = Brushes.Gray

            ' add the number base conversion question type to the template
            Me._selectionFromStandardValues.Add(CType(sender, Button).Tag)

        Else
            ' return the original brush
            CType(sender, Button).Background = defaultButtonBrush

            ' remove the number base conversion question type from the template
            Me._selectionFromStandardValues.Remove(CType(sender, Button).Tag)

        End If

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' get and load the standard values
        Me._standardValues.AddRange(Me._prop.GetStandardValues(Me._prop.CollectionItemType))

        lstQuestionClasses.DataContext = Me._standardValues
        lstSelectedQuestionClasses.DataContext = Me._selectionFromStandardValues

    End Sub

    Private Sub lstSelectedQuestionClasses_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstSelectedQuestionClasses.SelectionChanged
        ' get the first item that was added to the list selection
        ' the list is a single selection list
        If e.AddedItems.Count > 0 Then
            pGrid.DataContext = e.AddedItems(0)

        Else
            pGrid.DataContext = Nothing

        End If


    End Sub

    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = False
        Me.Close()

    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = True

        ' set the value here
        Me._value = Me._selectionFromStandardValues

        Me.Close()

    End Sub

End Class
