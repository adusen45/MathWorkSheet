<Serializable()>
Public Class PercentageAsDecimal
    Inherits SimpleControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New PercentageAsCommonFractionProblem(_randomizer.Next(minValue:=2, maxValue:=100)))

        Next

        Return returnList

    End Function

End Class

Public Class PercentageAsDecimalProblem
    Inherits ProblemBase

    Private _numberToConvert As UInteger

    Public Sub New(numberToConvert As UInteger)
        _numberToConvert = numberToConvert

    End Sub

    Public Overrides ReadOnly Property MainContent As Object
        Get
            Return _numberToConvert

        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "Convert " + _numberToConvert.ToString + "% to decimal"

    End Function

    Public Overrides Function GetListItem() As ListItem
        Return New ListItem(New Paragraph(New Run(Me.ToString)))

    End Function

End Class
