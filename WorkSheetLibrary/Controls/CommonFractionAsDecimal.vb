<Serializable()>
Public Class CommonFractionAsDecimal
    Inherits SimpleControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New CommonFractionAsDecimalProblem(numerator:=_randomizer.Next(minValue:=1, maxValue:=100), _
                                                              denominator:=_randomizer.Next(minValue:=2, maxValue:=100)))
            Dim k = 0
        Next

        Return returnList

    End Function

End Class

Public Class CommonFractionAsDecimalProblem
    Inherits ProblemBase

    Private _fractionToConvert As CommonFraction

    Public Sub New(numerator As UInteger, denominator As UInteger)
        _fractionToConvert = New CommonFraction(numerator, denominator)

    End Sub

    Public Overrides ReadOnly Property MainContent As Object
        Get
            Return _fractionToConvert.ToString

        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "Convert " + _fractionToConvert.ToString + " to decimal"

    End Function

    Public Overrides Function GetListItem() As ListItem
        Return New ListItem(New Paragraph(New Run(Me.ToString)))

    End Function

End Class
