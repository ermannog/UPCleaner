Public Class UserProfilesExclusionListForm

#Region "Property IsModified"
    Private isModifiedValue As Boolean = False

    Public ReadOnly Property IsModified() As Boolean
        Get
            Return Me.isModifiedValue
        End Get
    End Property
#End Region

    Private excludedUserProfiles As New SortableBindingList(Of UserProfile)

    Private Sub UserProfilesEsclusionListForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RefreshData()
    End Sub

    Private Sub grdMain_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.SelectionChanged
        Me.btnRemove.Enabled = Me.grdMain.Rows.Count > 0 AndAlso _
            Me.grdMain.SelectedRows.Count > 0
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        'Set Modify Flag
        Me.isModifiedValue = True

        'Remove SID User Profile from User Profile Exclusion List
        For Each row As System.Windows.Forms.DataGridViewRow In Me.grdMain.SelectedRows
            With DirectCast(row.DataBoundItem, UserProfile)
                My.Settings.UserProfileExclusionList.Remove(.SID)
            End With
        Next

        'Write setting
        Try
            Util.WriteApplicationSetting("UserProfileExclusionList", _
                My.Settings.UserProfileExclusionList)
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try

        'Refresh
        Me.RefreshData()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If Util.ShowQuestion("Confirm the clear of user profiles exclusion list?") = Windows.Forms.DialogResult.No Then Return

        'Set Modify Flag
        Me.isModifiedValue = True

        'Clear User Profile Exclusion List
        My.Settings.UserProfileExclusionList.Clear()

        'Write setting
        Try
            Util.WriteApplicationSetting("UserProfileExclusionList", _
                My.Settings.UserProfileExclusionList)
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try

        'Refresh
        Me.RefreshData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RefreshData()
        Me.BindingSourceExcludeUserProfiles.Sort = String.Empty
        Me.BindingSourceExcludeUserProfiles.DataSource = Nothing
        Me.excludedUserProfiles.Clear()

        Dim sid As String = String.Empty
        For Each sid In My.Settings.UserProfileExclusionList
            Me.excludedUserProfiles.Add( _
                UPCleanerUtil.UserProfiles.Find(Function(m) m.SID = sid))
        Next

        Me.BindingSourceExcludeUserProfiles.DataSource = Me.excludedUserProfiles
        Me.BindingSourceExcludeUserProfiles.Sort = "LastLoadTime"
        Me.grdMain.ClearSelection()
        Me.btnClear.Enabled = Me.grdMain.RowCount > 0
    End Sub
End Class