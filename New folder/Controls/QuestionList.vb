Imports PropertyGrid

Public Class QuestionList
    Inherits QuestionControl

    Private _questionToRepeat As SimpleControl
    Private _listItemCount As UInteger
    Private _header As String = "Convert the following to binary"

    Public Sub New()
        _questionToRepeat = Nothing
        _listItemCount = 0

    End Sub

    <PropertyGrid.PropertyEditing(PropertyEditingAttribute.EditModes.AddNewDialog, GetType(AddStandardItemsDialog))>
    Public Property QuestionToRepeat As SimpleControl
        Get
            Return _questionToRepeat

        End Get
        Set(value As SimpleControl)
            _questionToRepeat = value
            OnPropertyChanged("QuestionToRepeat")
        End Set
    End Property

    Public Property ListItemCount As UInteger
        Get
            Return _listItemCount

        End Get
        Set(value As UInteger)
            _listItemCount = value
            _questionToRepeat.InstancesToGenerate = value

            OnPropertyChanged("ListItemCount")
        End Set
    End Property

    Public Property Header As String
        Get
            Return _header

        End Get
        Set(value As String)
            _header = value

        End Set
    End Property

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        If _questionToRepeat Is Nothing Then
            Return Nothing

        Else
            Dim returnList As New List(Of ProblemBase)

            For i As UInteger = 1 To _instancesToGenerate
                returnList.Add(New ProblemList(_questionToRepeat))

            Next

            Return returnList

        End If

    End Function

End Class

Public Class ProblemList
    Inherits ProblemBase

    Private _listOfProblems As New RangeObservableCollection(Of ProblemBase)

    Public Sub New(questionToRepeat As SimpleControl)
        _listOfProblems.AddRange(questionToRepeat.GetProblemInstances)

    End Sub

    Public Property ListOfProblems As RangeObservableCollection(Of ProblemBase)
        Get
            Return _listOfProblems

        End Get
        Set(value As RangeObservableCollection(Of ProblemBase))
            _listOfProblems = value

        End Set
    End Property

End Class
