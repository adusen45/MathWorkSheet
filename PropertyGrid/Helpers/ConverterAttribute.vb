<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
Public Class ConverterAttribute
    Inherits Attribute

    Private _converterType As Type

    Public Sub New(converterType As Type)
        Me._converterType = converterType

    End Sub

    Public Function CreateConverter(bindingSource As [Property]) As IValueConverter
        Dim allConstructors = _converterType.GetConstructors()

        Dim allSingleArgConstructors = (From c In allConstructors
                                Where (From param In c.GetParameters
                                     Where param.ParameterType = GetType([Property])).Count = 1)

        Dim constructor = allSingleArgConstructors(0)

        Return constructor.Invoke(parameters:={bindingSource})

    End Function

End Class
