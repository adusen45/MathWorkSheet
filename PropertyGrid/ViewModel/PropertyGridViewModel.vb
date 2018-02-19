Imports HelperLibrary

Public Class PropertyGridViewModel
    Inherits NotifierBase

    Private _mainObject As MainObject = Nothing

    Public Sub New()

    End Sub

    Public Property SelectedObject As Object
        Get
            Return _mainObject.Object

        End Get
        Set(value As Object)
            Me._mainObject = New MainObject(value)

            OnPropertyChanged("SelectedObject")
            OnPropertyChanged("MainObject")

        End Set
    End Property

    Public ReadOnly Property MainObject As MainObject
        Get
            Return _mainObject

        End Get
    End Property

End Class
