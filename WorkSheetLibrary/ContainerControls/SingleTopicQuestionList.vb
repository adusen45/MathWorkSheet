Imports PropertyGrid
Imports HelperLibrary

Public Class SingleTopicQuestionList
    Inherits QuestionControl

    Private _questionToRepeat As SimpleControl = Nothing
    Private _header As String = ""

    <PropertyGrid.PropertyEditing(PropertyEditingAttribute.EditModes.AddNewDialog, GetType(AddStandardItemsDialog))>
       Public Property QuestionToRepeat As SimpleControl
        Get
            Return _questionToRepeat

        End Get
        Set(value As SimpleControl)
            _questionToRepeat = value
            OnPropertyChanged("QuestionToRepeat")
        End Set
    End Property

    Public Property Header As String
        Get
            Return _header

        End Get
        Set(value As String)
            _header = value
            OnPropertyChanged("Header")

        End Set
    End Property

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        If _questionToRepeat Is Nothing Then
            Return New List(Of ProblemBase)

        Else
            Dim returnList As New List(Of ProblemBase)

            For i As UInteger = 1 To _instancesToGenerate
                ' the question to repeat also stores the list count
                ' which is the number of times to repeat in the problem list
                returnList.Add(New HomogenousProblemList(_questionToRepeat, _header))

            Next

            Return returnList

        End If

    End Function

End Class

