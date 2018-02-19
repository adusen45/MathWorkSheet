Imports PropertyGrid
Imports HelperLibrary

Public Class MultiTopicQuestionList
    Inherits QuestionControl

    Private _questionsToRepeat As New RangeObservableCollection(Of SimpleControl)
    Private _header As String = ""

    <PropertyGrid.PropertyEditing(GetType(SimpleControl), GetType(EditCollectionDialog))>
    Public Property QuestionsToRepeat As RangeObservableCollection(Of SimpleControl)
        Get
            Return _questionsToRepeat

        End Get
        Set(value As RangeObservableCollection(Of SimpleControl))
            _questionsToRepeat = value
            OnPropertyChanged("QuestionsToRepeat")

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
        If _questionsToRepeat.Count = 0 Then
            Return New List(Of ProblemBase)

        Else
            Dim returnList As New List(Of ProblemBase)

            For i As UInteger = 1 To _instancesToGenerate
                ' each item in questions to repeat also stores the list count
                ' which is the number of times to repeat in the problem list

                ' the questions to repeat is used to generate multiple topics
                ' in the problem list, rather than a single topic
                returnList.Add(New HeterogenousProblemList(_questionsToRepeat, _header))

            Next

            Return returnList

        End If

    End Function

End Class

