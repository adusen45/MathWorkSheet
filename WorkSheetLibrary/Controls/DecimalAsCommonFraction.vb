<Serializable()>
Public Class DecimalAsCommonFraction
    Inherits SimpleControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New DecimalAsCommonFractionProblem(RandomDecimal()))

        Next

        Return returnList

    End Function

    Private Function RandomDecimal() As Decimal
        Dim decimalToReturn As Decimal

        ' try to avoid a "convert 0.0 to common fraction" situation
        Do
            decimalToReturn = CDec(_randomizer.NextDouble())

        Loop Until decimalToReturn > 0.0

        Return decimalToReturn

    End Function

End Class

Public Class DecimalAsCommonFractionProblem
    Inherits ProblemBase

    Private _numberToConvert As Decimal

    Public Sub New(numberToConvert As Decimal)
        _numberToConvert = numberToConvert

    End Sub

    Public Overrides ReadOnly Property MainContent As Object
        Get
            Return _numberToConvert

        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "Convert " + _numberToConvert.ToString + " to common fraction"

    End Function

    Public Overrides Function GetListItem() As ListItem
        Return New ListItem(New Paragraph(New Run(Me.ToString)))

    End Function

End Class
