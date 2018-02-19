Imports PropertyGrid
Imports WorkSheetLibrary
Imports HelperLibrary

Public Class GeneratedQuestionsWindow
    Private _questionControls As New RangeObservableCollection(Of QuestionControl)

    Public Property QuestionControls As RangeObservableCollection(Of QuestionControl)
        Get
            Return _questionControls

        End Get
        Set(value As RangeObservableCollection(Of QuestionControl))
            _questionControls = value

        End Set
    End Property

    Private _tempQuestionsList As New List(Of ProblemBase)
    Private _finalQuestionsList As New List(Of ProblemBase)        ' after shuffling, if shuffling is set to true

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' load the document
        ' assume the document is going to be a list of paragraphs
        Dim docGen As New DocumentGenerator
        myDocument.Blocks.Add(docGen.GenerateDocument(_questionControls))

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        docContainer.Print()

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()

    End Sub

End Class
