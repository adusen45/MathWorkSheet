Public Class ConvertToBinary
    Inherits SimpleControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New ConvertToBinaryProblem(_randomizer.Next(minValue:=7, maxValue:=63)))

        Next

        Return returnList

    End Function

End Class

Public Class ConvertToBinaryProblem
    Inherits ProblemBase

    Private _numberToConvert As UInteger

    Public Sub New(numberToConvert As UInteger)
        _numberToConvert = numberToConvert

    End Sub

    Public ReadOnly Property NumberToConvert As UInteger
        Get
            Return _numberToConvert

        End Get
    End Property

    Public Overrides Function Tostring() As String
        Return "Convert " + _numberToConvert.ToString + " to binary"

    End Function

End Class
