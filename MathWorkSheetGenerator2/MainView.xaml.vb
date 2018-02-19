Imports System.Collections.ObjectModel
Imports HelperLibrary
Imports WorkSheetLibrary

Public Class MainView
    Private _questionTemplates As New RangeObservableCollection(Of QuestionControl)

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' set up the window
        Me.DataContext = _questionTemplates

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        ' add a number base conversion question type to the template
        _questionTemplates.Add(New ConvertToBinary)

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        ' add a number base conversion question type to the template
        _questionTemplates.Add(New ConvertFromBinary)

    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        ' add a single topic question list to the template
        _questionTemplates.Add(New SingleTopicQuestionList)

    End Sub

    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        ' generate the questions
        Dim genQWindw As New GeneratedQuestionsWindow With {.QuestionControls = _questionTemplates}
        genQWindw.ShowDialog()

    End Sub

    Private Sub Button_Click_4(sender As Object, e As RoutedEventArgs)
        ' add a multi  topic question list to the template
        _questionTemplates.Add(New MultiTopicQuestionList)

    End Sub
End Class
