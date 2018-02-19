Imports HelperLibrary

Public Class DocumentGenerator
    Private _tempProblemsList As New List(Of ProblemBase)
    Private _finalProblemsList As New List(Of ProblemBase)        ' after shuffling, if shuffling is set to true

    Private _isShuffled As Boolean = False

    Public Property IsShuffled As Boolean
        Get
            Return _isShuffled

        End Get
        Set(value As Boolean)
            _isShuffled = value

        End Set
    End Property

    Public Function GenerateDocument(questionControls As RangeObservableCollection(Of QuestionControl)) As List
        _tempProblemsList.Clear()

        ' generate the problems
        For Each qControls In questionControls
            _tempProblemsList.AddRange(qControls.GetProblemInstances())

        Next

        ' TODO: shuffle here if shuffling is set to true
        _finalProblemsList.AddRange(_tempProblemsList)

        ' assume the document is going to be a list of paragraphs
        ' Create the body
        Dim lstOfQuestions As New List With {.MarkerStyle = TextMarkerStyle.Decimal, .MarkerOffset = 15}

        For Each prob As ProblemBase In _finalProblemsList
            lstOfQuestions.ListItems.Add(prob.GetListItem)

        Next

        Return lstOfQuestions

    End Function
End Class
