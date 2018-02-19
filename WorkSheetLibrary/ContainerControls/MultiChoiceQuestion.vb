Imports PropertyGrid

Public Class MultiChoiceQuestion
    Inherits QuestionControl

    Private _question As SimpleControl

    Private _numberOfOptions As UInteger = 2
    Private _correctOption As Char = "a"c

    Public Overrides Function GetProblemInstances() As List(Of ProblemBase)
        If _question Is Nothing Then
            Return New List(Of ProblemBase)

        Else
            Dim returnList As New List(Of ProblemBase)

            For i As UInteger = 1 To _instancesToGenerate
                ' the list should be populated with multichoice problem objects
                returnList.Add(New MultiChoiceProblem(_question, _numberOfOptions, _correctOption))

            Next

            Return returnList

        End If

    End Function

    <PropertyGrid.PropertyEditing(PropertyEditingAttribute.EditModes.AddNewDialog, GetType(AddStandardItemsDialog))>
    Public Property Question As SimpleControl
        Get
            Return _question

        End Get
        Set(value As SimpleControl)
            _question = value
            OnPropertyChanged("Question")

        End Set
    End Property

    Public Property NumberOfOptions As UInteger
        Get
            Return _numberOfOptions

        End Get
        Set(value As UInteger)
            _numberOfOptions = value
            OnPropertyChanged("NumberOfOptions")

        End Set
    End Property

    Public Property CorrectOption As Char
        Get
            Return _correctOption

        End Get
        Set(value As Char)
            _correctOption = value
            OnPropertyChanged("CorrectOption")

        End Set
    End Property

End Class
