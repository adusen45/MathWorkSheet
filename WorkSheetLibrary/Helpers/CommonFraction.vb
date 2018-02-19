Public Class CommonFraction
    Private _numerator As UInteger = 0
    Private _denominator As UInteger = 1

    Public Sub New(numerator As UInteger, denominator As UInteger)
        With Me
            ._numerator = numerator

            If denominator > 0 Then
                ._denominator = denominator

            Else
                Throw New Exception("Denominator cannot be zero")

            End If

        End With
    End Sub

    Public Property Numerator As UInteger
        Get
            Return _numerator

        End Get
        Set(value As UInteger)
            _numerator = value

        End Set
    End Property

    Public Property Denominator As UInteger
        Get
            Return _denominator

        End Get
        Set(value As UInteger)
            'If value > 0 Then _denominator = value

            If value > 0 Then
                _denominator = value

            Else
                Throw New Exception("Denominator cannot be zero")

            End If

        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _numerator.ToString + If(_denominator = 1, "", "/" + _denominator.ToString)

    End Function

End Class