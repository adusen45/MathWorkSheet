Imports HelperLibrary

<Serializable()>
Public MustInherit Class QuestionControl
    Inherits NotifierBase

    Protected Shared _randomizer As New Random(Date.Now.Millisecond)

    Protected _instancesToGenerate As UInteger    ' the qty of problems to appear at parent level

    Public Sub New()
        Me._instancesToGenerate = 1

    End Sub

    Public Property InstancesToGenerate As UInteger
        Get
            Return _instancesToGenerate

        End Get
        Set(value As UInteger)
            _instancesToGenerate = value
            OnPropertyChanged("InstancesToGenerate")

        End Set
    End Property

    Public MustOverride Function GetProblemInstances() As List(Of ProblemBase)

    Public ReadOnly Property Name As String
        Get
            Return Me.GetType().Name

        End Get
    End Property

End Class

Public MustInherit Class ProblemBase
    Public Overrides Function ToString() As String
        Return ""

    End Function

    Public Overridable ReadOnly Property MainContent As Object
        Get
            Throw New NotImplementedException("This property is not implemented")

        End Get
    End Property

    Public Overridable ReadOnly Property Answer As Object
        Get
            Throw New NotImplementedException("This property is not implemented")

        End Get
    End Property

    Public MustOverride Function GetListItem() As ListItem

End Class
