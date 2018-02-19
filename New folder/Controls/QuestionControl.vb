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

End Class

Public MustInherit Class ProblemBase
    Public Overrides Function Tostring() As String
        Return ""

    End Function

End Class
