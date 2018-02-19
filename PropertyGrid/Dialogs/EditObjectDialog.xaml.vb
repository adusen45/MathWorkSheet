Public Class EditObjectDialog
    Implements IDialogService

    Private _value As Object
    Private _prop As [Property]

    Public Function LaunchDialog(prop As [Property]) As Boolean Implements IDialogService.LaunchDialog
        Me._prop = prop

        ' construct a clone of the object in the accessor
        ' the property's type should have a clone implementation/deep copy implementation
        Dim tempObject = New MainObject(prop.Accessor).DeepCopy

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
        ' actually set the clone, the clone is stored in this
        ' datacontext. so the code below is ok
        Me._value = Me.pGrid.DataContext

        Me.Close()

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Me.DialogResult = False
        Me.Close()

    End Sub

End Class
