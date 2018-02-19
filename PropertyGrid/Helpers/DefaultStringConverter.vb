Public Class DefaultStringConverter
    Implements IValueConverter

    Private _bindingSource As [Property]

    Public Sub New(bindingSource As [Property])
        ' source type is assumed to be a string
        Me._bindingSource = bindingSource

    End Sub

    Public Function Convert(value As Object, targetType As Type, parameter As Object, _
                            culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert

        ' since this will be bound to a text box, or text block
        ' it should convert from the data type to string

        ' need to have access to the to string method
        If targetType IsNot GetType(String) Then
            Return Nothing

        Else
            ' this is for situation when toString is defined on the type
            Return If(IsNothing(value), Nothing, value.ToString())





            'Dim allMethods = info.PropertyType.GetMethods()
            'Dim allToStringMethods = (From mthd In allMethods
            '                    Where mthd.Name = "ToString" And mthd.ReturnType = GetType(String) And mthd.GetParameters.Count = 0)

            '._toStringMethod = allToStringMethods(0)

        End If

    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, _
                                culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack


        ' since this will be bound to a text box, or text block
        ' it should convert from string to the data type 

        ' need to have access to the parse method
        ' single argument, and defined in the class
        Dim allMethods = _bindingSource.Type.GetMethods(Reflection.BindingFlags.Static _
                              Xor Reflection.BindingFlags.Public Xor _
                              Reflection.BindingFlags.DeclaredOnly)

        'Dim allMethods = targetType.GetMethods()
        'Dim allParseMethods = (From mthd In allMethods
        '                        Where mthd.Name = "Parse" And mthd.ReturnType = targetType _
        '                        And mthd.GetParameters.Count = 1)

        Dim allParseMethods = (From mthd In allMethods
                                Where mthd.Name = "Parse" And mthd.ReturnType = _bindingSource.Type _
                                And (From param In mthd.GetParameters
                                     Where param.ParameterType = GetType(String)).Count = 1)
        Dim parseMethod = allParseMethods(0)

        Try
            If parseMethod IsNot Nothing Then
                ' there is a parse method available
                Return parseMethod.Invoke(Nothing, {value})

            Else
                ' no parse method available
                ' notify the binding engine somehow
                ' so that error will be notified
                Return DependencyProperty.UnsetValue

            End If


        Catch ex As Exception
            ' do not throw exceptions in ivalue converters because the wpf data binding engine
            ' does not handle them

            '' do one of the following
            'Return DependencyProperty.UnsetValue
            ' OR return the previous value
            Return _bindingSource.Accessor

        End Try

    End Function

End Class
