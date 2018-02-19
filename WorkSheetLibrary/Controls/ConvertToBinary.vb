<Serializable()>
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

    Public Overrides ReadOnly Property MainContent As Object
        Get
            Return _numberToConvert

        End Get
    End Property

    Public Overrides ReadOnly Property Answer As Object
        Get
            Return ConvertToBinary(n:=_numberToConvert)

        End Get
    End Property

    Private Function ConvertToBinary(n As UInteger) As String
        ' convert the number to binary
        Dim returnString As String = ""
        Dim quot As UInteger = n, remdr As UInteger

        Do
            ' get the remainder
            remdr = CUInt(quot Mod 2)

            ' add to output
            returnString = Trim(remdr.ToString) + returnString

            ' update the quotient
            quot = CUInt(quot \ 2)

        Loop

        Return returnString

    End Function

    Public Overrides Function ToString() As String
        Return "Convert " + _numberToConvert.ToString + " to binary"

    End Function

    Public Overrides Function GetListItem() As ListItem
        Return New ListItem(New Paragraph(New Run(Me.ToString)))

    End Function

End Class
