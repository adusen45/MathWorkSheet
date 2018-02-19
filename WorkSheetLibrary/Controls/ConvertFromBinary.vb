<Serializable()>
Public Class ConvertFromBinary
    Inherits SimpleControl

    Private _minStringLength As UInteger = 1, _maxStringLength As UInteger = 1

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Dim returnList As New List(Of ProblemBase)

        For i As UInteger = 1 To _instancesToGenerate
            returnList.Add(New ConvertFromBinaryProblem(RandomBinaryString()))

        Next

        Return returnList

    End Function

    Private Function RandomBinaryString() As String
        Dim binaryAlphabet As String = "01"
        Dim binaryString As String = "1"        ' prevents leading zeros. also works since min string length is one
        Dim bitChar As Char

        ' get the length of string to generate
        ' the length should be one less because a leading 1 has already been produced
        Dim extraLengthToGenerate As UInteger = _randomizer.Next(minValue:=_minStringLength, maxValue:=_maxStringLength + 1) - 1

        For index = 0 To extraLengthToGenerate - 1
            ' randomly select a char from the binary alphabet
            bitChar = binaryAlphabet(_randomizer.Next(minValue:=0, maxValue:=2))

            ' append it to the growing binary string
            binaryString += bitChar

        Next

        Return binaryString

    End Function

    Public Property MinStringLength As UInteger
        Get
            Return _minStringLength

        End Get
        Set(value As UInteger)
            If value <= _maxStringLength And value > 0 Then _minStringLength = value

            OnPropertyChanged("MinStringLength")

        End Set
    End Property

    Public Property MaxStringLength As UInteger
        Get
            Return _maxStringLength

        End Get
        Set(value As UInteger)
            If value >= _minStringLength Then _maxStringLength = value

            OnPropertyChanged("MaxStringLength")

        End Set
    End Property

End Class

Public Class ConvertFromBinaryProblem
    Inherits ProblemBase

    Private _binaryString As String

    Public Sub New(binaryString As String)
        _binaryString = binaryString

    End Sub

    Public Overrides ReadOnly Property MainContent As Object
        Get
            Return _binaryString

        End Get
    End Property

    Public Overrides Function ToString() As String
        Return "Convert " + _binaryString + " to decimal"

    End Function

    Public Overrides Function GetListItem() As ListItem
        Return New ListItem(New Paragraph(New Run(Me.ToString)))

    End Function

End Class
