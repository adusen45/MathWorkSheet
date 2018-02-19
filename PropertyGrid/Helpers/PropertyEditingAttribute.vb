<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
Public Class PropertyEditingAttribute
    Inherits Attribute

    Public Enum EditModes As Byte
        ''' <summary>
        ''' Use the default control for the property
        ''' </summary>
        ''' <remarks></remarks>
        [Default] = 0

        ''' <summary>
        ''' Use a specified user control for editing the property value
        ''' </summary>
        ''' <remarks></remarks>
        Control = 1

        ''' <summary>
        ''' Edit the property with an inline property grid
        ''' </summary>
        ''' <remarks></remarks>
        InlinePropertyGrid = 2

        ''' <summary>
        ''' Edit the property wth a custom inline editor
        ''' </summary>
        ''' <remarks></remarks>
        CustomInlineEditor = 3

        ''' <summary>
        ''' Launch a dialog to construct a new instance of the property type
        ''' </summary>
        ''' <remarks></remarks>
        AddNewDialog = 4

        ''' <summary>
        ''' Launch a dialog to edit the property
        ''' </summary>
        ''' <remarks></remarks>
        EditDialog = 5

        ''' <summary>
        ''' Launch a collection dialog to edit the collection
        ''' </summary>
        ''' <remarks></remarks>
        EditCollectionDialog = 6

    End Enum

    Private _editMode As EditModes = EditModes.Default
    Private _dialog As IDialogService
    Private _collectionItemType As Type

    Public Sub New(editMode As EditModes, Optional dlg As Type = Nothing)
        With Me
            ._editMode = editMode

            If dlg Is Nothing Then
                ' check for cases dialogmode to set default
                ' dialog in case no dialog is entered
                Select Case editMode
                    Case EditModes.AddNewDialog
                        ._dialog = New NewObjectDialog

                    Case EditModes.EditDialog
                        ._dialog = New EditObjectDialog

                    Case Else
                        ._dialog = Nothing

                End Select

            Else
                ' check if window implements idialogservice
                Dim anImplementedInterface = dlg.GetInterface("IDialogService")

                If anImplementedInterface Is GetType(IDialogService) Then
                    ' invoke the default constructor
                    ._dialog = dlg.GetConstructors()(0).Invoke(parameters:={})

                Else
                    Throw New Exception("Type does not implement IDialogService")

                End If

            End If

        End With

    End Sub

    Public Sub New(collectionItemType As Type, dlg As Type)
        ' assumes the edit mode is edit collection
        _editMode = EditModes.EditCollectionDialog

        If dlg Is Nothing Then
            Throw New Exception("Dialog cannot be nothing for edit mode = EditCollectionDialog")

        Else
            ' check if window implements idialogservice
            Dim anImplementedInterface = dlg.GetInterface("IDialogService")

            If anImplementedInterface Is GetType(IDialogService) Then
                ' invoke the default constructor
                Me._dialog = dlg.GetConstructors()(0).Invoke(parameters:={})
                Me._collectionItemType = collectionItemType

            Else
                Throw New Exception("dialog Type does not implement IDialogService")

            End If

        End If

    End Sub

    Public ReadOnly Property EditMode As EditModes
        Get
            Return _editMode

        End Get
    End Property

    Public ReadOnly Property Dialog As IDialogService
        Get
            Return _dialog

        End Get
    End Property

    Public ReadOnly Property CollectionItemType As Type
        Get
            Return _collectionItemType

        End Get
    End Property

End Class
