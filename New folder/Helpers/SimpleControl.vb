Imports System.Reflection

Public Class SimpleControl
    Inherits QuestionControl

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        Return Nothing

    End Function

    Public Shared Function GetStandardValues() As RangeObservableCollection(Of Object)
        ' get all the subclasses of this type
        Dim allAssemblies As List(Of System.Reflection.Assembly) = AppDomain.CurrentDomain.GetAssemblies.ToList
        Dim assemblyQuery = From assembly In allAssemblies
                          Where assembly.FullName.Contains("WorkSheetLibrary")

        Dim myAssembly As System.Reflection.Assembly = assemblyQuery(0)
        Dim allTypes As List(Of Type) = myAssembly.GetTypes.ToList

        Dim typeQuery = From t In allTypes
                        Where t.BaseType Is GetType(SimpleControl)
                        Select t.GetConstructors()(0).Invoke(parameters:={})


        'Dim typ As Type

        'Dim listOfDerivedClasses = From domainAssembly In AppDomain.CurrentDomain.GetAssemblies()
        '                          From assemblyType In domainAssembly.GetTypes()
        '                          Where GetType(QuestionFormatBase).IsSubclassOf(assemblyType)
        '                          Select assemblyType

        Dim returnList As New RangeObservableCollection(Of Object)
        returnList.AddRange(typeQuery.ToList)
        Return returnList

    End Function

End Class
