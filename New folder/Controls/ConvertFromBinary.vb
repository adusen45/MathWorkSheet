Public Class ConvertFromBinary
    Inherits SimpleControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New ConvertFromBinaryProblem(_randomizer.Next(minValue:=7, maxValue:=63)))

        Next

        Return returnList

    End Function

End Class

Public Class ConvertFromBinaryProblem
    Inherits ProblemBase

    Private _binaryString As String

    Public Sub New(binaryString As UInteger)
        _binaryString = binaryString

    End Sub

    Public ReadOnly Property BinaryString As String
        Get
            Return _binaryString

        End Get
    End Property

    Public Overrides Function Tostring() As String
        Return "Convert " + _binaryString + " to decimal"

    End Function


End Class
