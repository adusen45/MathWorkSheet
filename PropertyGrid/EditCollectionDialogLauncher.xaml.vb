Public Class EditCollectionDialogLauncher

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        ' launch the dialog given in the datacontext
        Dim prop As [Property] = Me.DataContext

        Dim dlgServer As IDialogService = prop.CreateDialogService

        Dim dlgResultIsOK As Boolean = dlgServer.LaunchDialog(prop)

        If dlgResultIsOK Then
            ' the textblock should update
            CType(Me.DataContext, [Property]).Accessor = dlgServer.Value

        End If

    End Sub
End Class
