Public Class AddStandardItemsDialog
    Implements IDialogService

    Private _value As Object
    Private _prop As [Property]

    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog
        Me._prop = prop

        ' set the items source of the list with the standard items
        lstSimpleControls.ItemsSource = prop.GetStandardValues

        '' construct a fresh object
        'Dim tempObject = prop.Type.GetConstructors()(0).Invoke(parameters:={})

        '' set the datacontext of the property grid so that the object can be displayed
        'Me.pGrid.DataContext = tempObject

        If lstSimpleControls.ItemsSource IsNot Nothing Then
            ' set the datacontext of the property grid so that the object can be displayed
            Me.pGrid.DataContext = lstSimpleControls.SelectedValue

        End If

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

    Private Sub lstSimpleControls_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstSimpleControls.SelectionChanged
         ' set the datacontext of the property grid so that the object can be displayed
        Me.pGrid.DataContext = lstSimpleControls.SelectedValue

    End Sub
End Class
