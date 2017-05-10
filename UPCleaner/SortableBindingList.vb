Public Class SortableBindingList(Of T)
    Inherits System.ComponentModel.BindingList(Of T)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal list As System.Collections.Generic.IList(Of T))
        MyBase.New(list)
    End Sub

    Private isSortedValue As Boolean = False
    Private sortDirectionCoreValue As System.ComponentModel.ListSortDirection = Nothing
    Private sortPropertyCoreValue As System.ComponentModel.PropertyDescriptor = Nothing

    Protected Overloads Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overloads Overrides Sub ApplySortCore(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
        ' Get list to sort
        Dim items As List(Of T) = TryCast(Me.Items, List(Of T))

        ' Apply and set the sort, if items to sort
        If items IsNot Nothing Then
            Dim pc As New PropertyComparer(Of T)([property], direction)
            items.Sort(pc)
            Me.isSortedValue = True
            Me.sortDirectionCoreValue = direction
            Me.sortPropertyCoreValue = [property]
        Else
            Me.isSortedValue = False
            Me.sortDirectionCoreValue = Nothing
            Me.sortPropertyCoreValue = Nothing
        End If

        ' Let bound controls know they should refresh their views
        Me.OnListChanged(New System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.Reset, -1))
    End Sub

    Protected Overloads Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return Me.isSortedValue
        End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As System.ComponentModel.ListSortDirection
        Get
            Return Me.sortDirectionCoreValue
        End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As System.ComponentModel.PropertyDescriptor
        Get
            Return Me.SortPropertyCoreValue
        End Get
    End Property

    Protected Overloads Overrides Sub RemoveSortCore()
        Me.isSortedValue = False
        Me.sortDirectionCoreValue = Nothing
        Me.sortPropertyCoreValue = Nothing
    End Sub

End Class
