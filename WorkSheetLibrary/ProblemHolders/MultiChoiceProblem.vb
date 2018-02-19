Public Class MultiChoiceProblem
    Inherits ProblemBase

    Private _options As New SortedList(Of Char, Object)

    Private _problem As ProblemBase
    Private _numberOfOptions As UInteger
    Private _correctOption As Char

    Public Sub New(question As SimpleControl, numberOfOptions As UInteger, correctOption As Char)
        With Me
            ._problem = question.GetProblemInstances(0)
            ._numberOfOptions = numberOfOptions
            ._correctOption = correctOption

            ' set the correct answer
            _options(correctOption) = ._problem.Answer

            ' set the wrong answers


        End With
    End Sub

    Public Overrides Function GetListItem() As ListItem
        ' generate display structure of the MCQ

        ' choose OptionsStyle = HorizontalBeneathQuestion

        ' first, each option
        Dim options As New SortedList(Of Char, Paragraph)
        For Each e In _options
            options.Add(e.Key, New Paragraph(New Run(e.Value.ToString)))
        Next

        ' the question, should go into a table cell
        Dim ques As New TableCell(New Paragraph(New Run(_problem.ToString))) With {.ColumnSpan = options.Count}
        Dim mcqTbl As New Table()
        Dim mcqTrg As New TableRowGroup()
        Dim mcqTr As New TableRow()
        mcqTr.Cells.Add(ques)
        mcqTrg.Rows.Add(mcqTr)
        mcqTbl.RowGroups.Add(mcqTrg)

        Dim optTr As New TableRow

        ' the options
        For Each op In options
            Dim tbl As New Table
            Dim trg As New TableRowGroup
            Dim tr As New TableRow
            tr.Cells.Add(New TableCell(New Paragraph(New Run(op.Key)))) : tr.Cells.Add(New TableCell(op.Value))
            trg.Rows.Add(tr)
            tbl.RowGroups.Add(trg)

            optTr.Cells.Add(New TableCell(tbl))

        Next

        ' next, the table holding the whole structure
        mcqTrg.Rows.Add(optTr)

        Dim qListItem As New ListItem
        qListItem.Blocks.Add(mcqTbl)
       
        Return qListItem

    End Function

End Class
