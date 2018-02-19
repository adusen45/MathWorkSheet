Public Class PropertyGridControl

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' the view model goes to the grid's datacontext
        grdObjectProperties.DataContext = New PropertyGridViewModel

    End Sub

    'Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
    '    ' the view model goes to the grid's datacontext
    '    If Me.IsLoaded Then
    '        grdObjectProperties.DataContext = New PropertyGridViewModel

    '    End If

    'End Sub

    Private Sub UserControl_DataContextChanged(sender As Object, e As DependencyPropertyChangedEventArgs)
        ' the user of the control set the selected object to the data context
        ' update the data context of the list view
        CType(grdObjectProperties.DataContext, PropertyGridViewModel).SelectedObject = Me.DataContext

        ' recreate the grid
        GeneratePropertyInterface()

    End Sub

    Private Sub GeneratePropertyInterface()
        ' create the grid structure first
        Dim mainObject As MainObject = CType(grdObjectProperties.DataContext, PropertyGridViewModel).MainObject
        RefreshGridStructure(mainObject.Count)

        ' construct the UI
        For rowIndex = 0 To grdObjectProperties.RowDefinitions.Count - 1
            ' create a row based on the type info provided by the property
            CreateRow(mainObject, rowIndex)

        Next

    End Sub

    Private Sub RefreshGridStructure(propertyCount As Integer)
        ' clear the grid holding the properties
        grdObjectProperties.Children.Clear()
        grdObjectProperties.RowDefinitions.Clear()

        ' extract all the public properties of the object
        Dim newRow As RowDefinition

        For i = 0 To propertyCount - 1
            ' construct a new row in the grid
            newRow = New RowDefinition()
            grdObjectProperties.RowDefinitions.Add(newRow)

        Next

    End Sub

    Private Sub CreateRow(mainObj As MainObject, rowIndex As Integer)
        ' check the property edit mode
        Select Case mainObj(rowIndex).EditMode
            Case PropertyEditingAttribute.EditModes.InlinePropertyGrid
                ' use the inbuilt sub property grid
                CreateInlineDialogRow([property]:=mainObj(rowIndex), row:=rowIndex)

            Case PropertyEditingAttribute.EditModes.AddNewDialog
                ' launch the default add new object dialog
                CreatePropertyHeader([property]:=mainObj(rowIndex), row:=rowIndex)
                CreateNewObjectDialogLauncherRow(mainObject:=mainObj, row:=rowIndex)

            Case PropertyEditingAttribute.EditModes.EditDialog
                ' launch the default edit object dialog
                CreatePropertyHeader([property]:=mainObj(rowIndex), row:=rowIndex)
                CreateEditObjectDialogLauncherRow(mainObject:=mainObj, row:=rowIndex)

            Case PropertyEditingAttribute.EditModes.EditCollectionDialog
                ' launch the default edit object dialog
                CreatePropertyHeader([property]:=mainObj(rowIndex), row:=rowIndex)
                CreateEditCollectionDialogLauncherRow(mainObject:=mainObj, row:=rowIndex)

            Case Else
                ' use the default row template
                CreatePropertyHeader([property]:=mainObj(rowIndex), row:=rowIndex)
                CreatePropertyCell(mainObject:=mainObj, row:=rowIndex)

                CreateErrorStatusCell(row:=rowIndex)

        End Select

    End Sub

#Region "Create Row Helpers"
    Private Sub CreatePropertyHeader([property] As [Property], row As Integer)
        Dim txtPropertyHeader As TextBlock = New TextBlock With {.Text = [property].Name, _
                                                        .Background = Brushes.White}
        Dim txtPropertyHeaderBorder As New Border With {.Child = txtPropertyHeader, .BorderBrush = Brushes.Black, _
                                                        .BorderThickness = New Thickness(0.5), .Margin = New Thickness(5)}

        ' set the tool tip for the textblock
        txtPropertyHeader.ToolTip = CreateDescriptionTooltip([property])

        grdObjectProperties.Children.Add(txtPropertyHeaderBorder)
        Grid.SetColumn(txtPropertyHeaderBorder, value:=0) : Grid.SetRow(txtPropertyHeaderBorder, row)

    End Sub

    Private Function CreateDescriptionTooltip([property] As [Property]) As ToolTip
        ' the property header should also hold the description in a tooltip
        Dim typeName As String = [property].Type.FullName       ' get the fully qualified type name for the property
        Dim propName As String = [property].Name                ' get the name of the property

        ' get the description of the property
        Dim propDescription As String = [property].Description

        ' construct the tooltip body
        Dim tt As New ToolTip, toolTipBody As New Grid
        With toolTipBody
            .RowDefinitions.Add(New RowDefinition) ' for the description
            .RowDefinitions.Add(New RowDefinition) ' for the type name and property name

            .ColumnDefinitions.Add(New ColumnDefinition)

            ' the tooltip contents
            Dim descTextBlock As New TextBlock With {.Text = propDescription, .Width = 300, .HorizontalAlignment = Windows.HorizontalAlignment.Left, _
                                                     .TextWrapping = TextWrapping.Wrap}
            Dim typeTextBlock As New TextBlock With {.TextWrapping = TextWrapping.NoWrap}
            typeTextBlock.Inlines.Add(New Bold(New Run(typeName)))
            Dim nameTextBlock As New TextBlock With {.Text = propName, .TextWrapping = TextWrapping.NoWrap, .Margin = New Thickness(5, 0, 0, 0)}

            ' finish up
            toolTipBody.Children.Add(descTextBlock)
            Grid.SetColumn(descTextBlock, value:=0) : Grid.SetRow(descTextBlock, value:=0)

            Dim typeAndPropNameStack As New StackPanel With {.Orientation = Orientation.Horizontal}
            typeAndPropNameStack.Children.Add(typeTextBlock) : typeAndPropNameStack.Children.Add(nameTextBlock)
            toolTipBody.Children.Add(typeAndPropNameStack)
            Grid.SetColumn(typeAndPropNameStack, value:=0) : Grid.SetRow(typeAndPropNameStack, value:=1)

        End With

        ' set the tooltips body
        tt.Content = toolTipBody

        Return tt

    End Function

    Private Sub CreatePropertyCell(mainObject As MainObject, row As Integer)
        Dim propManipControl As New UIElement

        '' create the binding with the property
        '' not working
        'Dim propBinding As New Binding() With {.Source = mainObject.Object, .Path = New PropertyPath(mainObject(row).Name), _
        '                                       .NotifyOnValidationError = True}

        ' this one throws conversion exception
        ' will need direct access to the to string/parse methods within the property object
        Dim propBinding As New Binding() With {.Source = mainObject(row), .Path = New PropertyPath("Accessor"), _
                                               .NotifyOnValidationError = True, .ValidatesOnDataErrors = True, .Mode = BindingMode.TwoWay}

        'Dim f As Type
        'f.IsPrimitive

        Select Case mainObject(row).DefaultControlType
            Case Is = GetType(CheckBox)
                propManipControl = New CheckBox With {.Margin = New Thickness(5)}
                BindingOperations.SetBinding(propManipControl, CheckBox.IsCheckedProperty, propBinding)

            Case Is = GetType(DatePicker)
                propManipControl = New DatePicker With {.Margin = New Thickness(5)}
                BindingOperations.SetBinding(propManipControl, DatePicker.SelectedDateProperty, propBinding)

            Case Else
                propManipControl = New TextBox With {.Margin = New Thickness(5)}

                ' set the string cnverter to use for the textbox
                Dim typConv = mainObject(row).TypeConverter
                If typConv IsNot Nothing Then
                    ' type converter specified on property
                    ' will override default toString/Parse conversion
                    propBinding.Converter = typConv

                ElseIf mainObject(row).Type IsNot GetType(String) Then
                    ' type convertr not specified on property
                    ' use the default toString/Parse methods

                    ' NB: string does not need a type converter
                    propBinding.Converter = New DefaultStringConverter(bindingSource:=mainObject(row))

                End If

                BindingOperations.SetBinding(propManipControl, TextBox.TextProperty, propBinding)

        End Select


        ' do not need this since on error vaalue converter resets
        ' the input to the last value
        ' so remove this validation rule
        '' add validation rule to indicate error on invalid user input for the property
        'propBinding.ValidationRules.Add(New ExceptionValidationRule())

        '' add the event handler for the error
        'Validation.AddErrorHandler(propManipControl, AddressOf HandleValidationError)


        grdObjectProperties.Children.Add(propManipControl)
        Grid.SetColumn(propManipControl, value:=1) : Grid.SetRow(propManipControl, row)

    End Sub

    Private Sub CreateErrorStatusCell(row As Integer)
        Dim txtPropertyErrorStatus As New TextBlock With {.Foreground = Brushes.Red}
        grdObjectProperties.Children.Add(txtPropertyErrorStatus)
        Grid.SetColumn(txtPropertyErrorStatus, value:=2) : Grid.SetRow(txtPropertyErrorStatus, row)

    End Sub

    Private Sub HandleValidationError(sender As Object, e As ValidationErrorEventArgs)
        Dim uiElementQuery = (From uiElem As UIElement In grdObjectProperties.Children
                                           Where Grid.GetRow(uiElem) = Grid.GetRow(sender) And Grid.GetColumn(uiElem) = 2)

        Dim errorStatusBlock As TextBlock = uiElementQuery(0)

        If e.Action = ValidationErrorEventAction.Added Then
            errorStatusBlock.Text = "Error!"

            ' set the error tooltip
            Dim tt As New ToolTip() With {.Content = e.Error.ErrorContent}
            errorStatusBlock.ToolTip = tt

            'CType(sender, TextBox).ToolTip = tt
            'tt.IsOpen = True : tt.StaysOpen = True

        Else
            ' clear the error tooltip
            errorStatusBlock.Text = ""
            errorStatusBlock.ToolTip = Nothing

            'CType(sender, TextBox).ToolTip = Nothing

        End If

    End Sub

    Private Sub CreateInlineDialogRow([property] As [Property], row As Integer)
        ' create the inline dialog for this property

        ' no need to create binding externally
        ' the binding is created based on the datacontext set on construction
        ' in the following...
        Dim inlineDlg As New SubPropertyGrid With {.DataContext = [property], .Margin = New Thickness(0, 5, 5, 5)}

        grdObjectProperties.Children.Add(inlineDlg)
        Grid.SetColumn(inlineDlg, value:=0) : Grid.SetRow(inlineDlg, row) : Grid.SetColumnSpan(inlineDlg, value:=3)

    End Sub

    Private Sub CreateNewObjectDialogLauncherRow(mainObject As MainObject, row As Integer)
        ' create the launcher for the newDialogBox for this property
        Dim launcher As New NewObjectDialogLauncher With {.DataContext = mainObject(row), .Margin = New Thickness(5)}

        grdObjectProperties.Children.Add(launcher)
        Grid.SetColumn(launcher, value:=1) : Grid.SetRow(launcher, row)

    End Sub

    Private Sub CreateEditObjectDialogLauncherRow(mainObject As MainObject, row As Integer)
        ' create the launcher for the editDialogBox for this property
        Dim launcher As New EditObjectDialogLauncher With {.DataContext = mainObject(row), .Margin = New Thickness(5)}

        grdObjectProperties.Children.Add(launcher)
        Grid.SetColumn(launcher, value:=1) : Grid.SetRow(launcher, row)

    End Sub

    Private Sub CreateEditCollectionDialogLauncherRow(mainObject As MainObject, row As Integer)
        ' create the launcher for the editDialogBox for this property
        Dim launcher As New EditCollectionDialogLauncher With {.DataContext = mainObject(row), .Margin = New Thickness(5)}

        grdObjectProperties.Children.Add(launcher)
        Grid.SetColumn(launcher, value:=1) : Grid.SetRow(launcher, row)

    End Sub

#End Region

End Class
