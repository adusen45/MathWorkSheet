Imports HelperLibrary

Public Class HomogenousProblemList
    Inherits ProblemBase

    Private _listOfProblems As New RangeObservableCollection(Of ProblemBase)
    Private _header As String

    Public Sub New(questionToRepeat As SimpleControl, header As String)
        _listOfProblems.AddRange(questionToRepeat.GetProblemInstances)
        Me._header = header

    End Sub

    Public Sub New(questionsToRepeat As RangeObservableCollection(Of SimpleControl), header As String)
        For Each questionToRepeat In questionsToRepeat
            _listOfProblems.AddRange(questionToRepeat.GetProblemInstances)

        Next

        Me._header = header

    End Sub

    Public Property ListOfProblems As RangeObservableCollection(Of ProblemBase)
        Get
            Return _listOfProblems

        End Get
        Set(value As RangeObservableCollection(Of ProblemBase))
            _listOfProblems = value

        End Set
    End Property

    Public Overrides Function GetListItem() As ListItem
        Dim qParagraph As Paragraph, qList As List, qListItem As ListItem

        qList = New List With {.MarkerStyle = TextMarkerStyle.LowerLatin, .MarkerOffset = 15}

        For Each p In _listOfProblems
            qParagraph = New Paragraph(New Run(p.MainContent))
            qList.ListItems.Add(New ListItem(qParagraph))

        Next

        qListItem = New ListItem
        qListItem.Blocks.Add(New Paragraph(New Run(_header)))
        qListItem.Blocks.Add(qList)

        Return qListItem

    End Function

End Class
