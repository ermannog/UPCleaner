Public Class PropertyComparer(Of T)
    Implements System.Collections.Generic.IComparer(Of T)


    ' The following code contains code implemented by Rockford Lhotka:
    ' http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnadvnet/html/vbnet01272004.asp

    Private _property As System.ComponentModel.PropertyDescriptor
    Private _direction As System.ComponentModel.ListSortDirection

    Public Sub New(ByVal [property] As System.ComponentModel.PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection)
        _property = [property]
        _direction = direction
    End Sub

#Region "IComparer<T>"
    Public Function Compare(ByVal xWord As T, ByVal yWord As T) As Integer Implements System.Collections.Generic.IComparer(Of T).Compare
        ' Get property values
        Dim xValue As Object = GetPropertyValue(xWord, _property.Name)
        Dim yValue As Object = GetPropertyValue(yWord, _property.Name)

        ' Determine sort order
        If _direction = System.ComponentModel.ListSortDirection.Ascending Then
            Return CompareAscending(xValue, yValue)
        Else
            Return CompareDescending(xValue, yValue)
        End If
    End Function

    Public Shadows Function Equals(ByVal xWord As T, ByVal yWord As T) As Boolean
        Return xWord.Equals(yWord)
    End Function

    Public Shadows Function GetHashCode(ByVal obj As T) As Integer
        Return obj.GetHashCode()
    End Function
#End Region

    ' Compare two property values of any type
    Private Function CompareAscending(ByVal xValue As Object, ByVal yValue As Object) As Integer
        Dim result As Integer

        ' If values implement IComparer
        If TypeOf xValue Is IComparable Then
            result = (DirectCast(xValue, IComparable)).CompareTo(yValue)
            ' If values don't implement IComparer but are equivalent
        ElseIf xValue.Equals(yValue) Then
            result = 0
        Else
            ' Values don't implement IComparer and are not equivalent, so compare as string values
            result = xValue.ToString().CompareTo(yValue.ToString())
        End If

        ' Return result
        Return result
    End Function

    Private Function CompareDescending(ByVal xValue As Object, ByVal yValue As Object) As Integer
        ' Return result adjusted for ascending or descending sort order ie
        ' multiplied by 1 for ascending or -1 for descending
        Return CompareAscending(xValue, yValue) * -1
    End Function

    Private Function GetPropertyValue(ByVal value As T, ByVal [property] As String) As Object
        ' Get property
        Dim propertyInfo As System.Reflection.PropertyInfo = _
            value.[GetType]().GetProperty([property])

        ' Return value
        Return propertyInfo.GetValue(value, Nothing)
    End Function

End Class

