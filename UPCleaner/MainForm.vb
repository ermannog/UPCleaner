Public Class MainForm
    Private isLoading As Boolean = True
    Private labelUserProfilesNotLoadByDaysTextDefault As String = String.Empty

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.labelUserProfilesNotLoadByDaysTextDefault = Me.lblUserProfilesNotLoadByDays.Text
        Me.nupLastLoadDays.Value = My.Settings.LastLoadDays
        Me.isLoading = False
        Me.btnRefresh.PerformClick()
    End Sub

    Private Sub nupLastLoadDays_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nupLastLoadDays.ValueChanged
        If Me.isLoading Then Return

        'Refresh
        Me.btnRefresh.PerformClick()

        'Write setting
        Try
            Util.WriteApplicationSetting("LastLoadDays", Me.nupLastLoadDays.Value)
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try
    End Sub

    Private Sub mniFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniFileExit.Click
        Me.btnClose.PerformClick()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub grdMain_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdMain.SelectionChanged
        Me.btnDeleteClear.Enabled = Me.grdMain.Rows.Count > 0 AndAlso _
            Me.grdMain.SelectedRows.Count > 0
        Me.btnExcludeUserProfile.Enabled = Me.grdMain.Rows.Count > 0 AndAlso _
            Me.grdMain.SelectedRows.Count > 0
    End Sub

    Private Sub btnDeleteClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteClear.Click
        Select Case My.Settings.UserProfileClearMode
            Case UPCleanerSettings.UserProfileClearModes.DeleteUserProfile
                If Util.ShowQuestion("Confirm the delete of the selected user profiles?") = Windows.Forms.DialogResult.No Then Return

                Util.SetWaitCursor(True)
                For Each row As System.Windows.Forms.DataGridViewRow In Me.grdMain.SelectedRows
                    UPCleanerUtil.DeleteUserProfile(DirectCast(row.DataBoundItem, UserProfile))
                Next
                Util.SetWaitCursor(False)

                'Refresh
                Me.btnRefresh.PerformClick()
            Case UPCleanerSettings.UserProfileClearModes.ClearSubDirectories
                If Util.ShowQuestion("Confirm the clear of the selected user profiles?") = Windows.Forms.DialogResult.No Then Return

                Util.SetWaitCursor(True)
                For Each row As System.Windows.Forms.DataGridViewRow In Me.grdMain.SelectedRows
                    UPCleanerUtil.ClearUserProfile(DirectCast(row.DataBoundItem, UserProfile))
                Next

                Util.SetWaitCursor(False)
        End Select
    End Sub

    Private Sub mniViewRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniViewRefresh.Click
        Me.btnRefresh.PerformClick()
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        If SettingsForm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            My.Settings.Reload()

            'Refresh
            Me.btnRefresh.PerformClick()
        End If
    End Sub

    Private Sub mniToolsSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniToolsSettings.Click
        Me.btnSettings.PerformClick()
    End Sub

    Private Sub mniToolsUserProfileExclusionList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniToolsUserProfileExclusionList.Click
        Me.btnUserProfilesExclusionList.PerformClick()
    End Sub

    Private Sub btnUserProfilesExclusionList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserProfilesExclusionList.Click
        Using frm = New UserProfilesExclusionListForm
            If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                If frm.IsModified Then _
                    Me.btnRefresh.PerformClick()
            End If
        End Using
    End Sub

    Private Sub mniToolsUserProfileSubDirectoriesList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniToolsUserProfileSubDirectoriesList.Click
        Me.btnToolsUserProfileSubDirectoriesList.PerformClick()
    End Sub

    Private Sub btnUserProfileSubDirectoriesList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToolsUserProfileSubDirectoriesList.Click
        Using frm = New UserProfileSubDirectoriesListForm
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If Me.isLoading Then Return

        Util.SetWaitCursor(True)

        Me.BindingSourceUserProfiles.Sort = String.Empty
        Me.BindingSourceUserProfiles.DataSource = Nothing
        Me.lblUserProfilesNotLoadByDays.Text = String.Format( _
            Me.labelUserProfilesNotLoadByDaysTextDefault, _
            Me.nupLastLoadDays.Value)
        Me.lblUserProfilesNotLoadByDaysCount.Text = "0"
        Me.lblUserProfilesExcludedCount.Text = "0"
        Me.lblUserProfilesCount.Text = "0"

        Select Case My.Settings.UserProfileClearMode
            Case UPCleanerSettings.UserProfileClearModes.DeleteUserProfile
                Me.btnDeleteClear.Text = "Delete"
            Case UPCleanerSettings.UserProfileClearModes.ClearSubDirectories
                Me.btnDeleteClear.Text = "Clear"
        End Select

        UPCleanerUtil.FillUserProfiles(CInt(Me.nupLastLoadDays.Value))

        Try
            Me.lblUserProfilesNotLoadByDays.Text = String.Format( _
                Me.labelUserProfilesNotLoadByDaysTextDefault, _
                Me.nupLastLoadDays.Value)
            Me.lblUserProfilesNotLoadByDaysCount.Text = _
                UPCleanerUtil.SelectedUserProfiles.Count.ToString("#,##0")

            Me.lblUserProfilesExcludedCount.Text = _
                    My.Settings.UserProfileExclusionList.Count.ToString("#,##0")

            Me.lblUserProfilesCount.Text = _
                UPCleanerUtil.UserProfiles.Count.ToString("#,##0")

            Me.BindingSourceUserProfiles.DataSource = _
                New SortableBindingList(Of UserProfile)(UPCleanerUtil.SelectedUserProfiles)
            Me.BindingSourceUserProfiles.Sort = "LastLoadTime"
            Me.grdMain.ClearSelection()
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try

        Util.SetWaitCursor(False)
    End Sub

    Private Sub btnExcludeUserProfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcludeUserProfile.Click
        'Update list
        For Each row As System.Windows.Forms.DataGridViewRow In Me.grdMain.SelectedRows
            With DirectCast(row.DataBoundItem, UserProfile)
                My.Settings.UserProfileExclusionList.Add(.SID)
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
        Me.btnRefresh.PerformClick()
    End Sub


End Class