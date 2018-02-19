Public Class TestStringConverter
    Implements IValueConverter

    Private _bindingSource As [Property]

    Public Sub New(bindingSource As [Property])
        ' source type is assumed to be a string
        Me._bindingSource = bindingSource

    End Sub

    Public Function Convert(value As Object, targetType As Type, parameter As Object, _
                            culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert

        ' since this will be bound to a text box, or text block
        ' it should convert from the date data type to string

        ' need to have access to the to string method
        If targetType IsNot GetType(String) Then
            Return Nothing

        Else
            ' this is for situation when toString is defined on the type
            Return CType(value, Boolean).ToString()(0)

        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, _
                                culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack


        ' since this will be bound to a text box, or text block
        ' it should convert from string to the date data type 
        Dim daysOfWeek() As String = [Enum].GetNames(GetType(DayOfWeek))

        Dim strValue As String = value
        Dim input As String = Strings.UCase(strValue(0)) + Strings.LCase(Strings.Right(strValue, strValue.Count - 1))

        If daysOfWeek.Contains(input) Then
            Dim dateobj As Date = _bindingSource.Accessor
            DayOfWeek.Parse(GetType(DayOfWeek), input)
            [Enum].TryParse(Of DayOfWeek)(input, CType(_bindingSource.Accessor, Date).DayOfWeek)

        End If

        Return _bindingSource.Accessor

    End Function

End Class
