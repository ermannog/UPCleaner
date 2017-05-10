Public Class UserProfileSubDirectoriesListForm

    Private Sub UserProfilesEsclusionListForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RefreshData()
    End Sub

    Private Sub lsvMain_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lsvMain.SelectedIndexChanged
        Me.btnRemove.Enabled = Me.lsvMain.SelectedItems.Count > 0
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        'Remove SubDirectory from User Profile SubDirectories List
        For Each item As System.Windows.Forms.ListViewItem In Me.lsvMain.SelectedItems
            My.Settings.UserProfileSubDirectoriesList.Remove( _
                item.SubItems(Me.colPath.Index).Text)
        Next

        'Write setting
        Try
            Util.WriteApplicationSetting("UserProfileSubDirectoriesList", _
                My.Settings.UserProfileSubDirectoriesList)
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try

        'Refresh
        Me.RefreshData()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        If Util.ShowQuestion("Confirm the clear of user profiles subdirectories list?") = Windows.Forms.DialogResult.No Then Return

        'Clear User Profile SubDirectories List
        My.Settings.UserProfileSubDirectoriesList.Clear()

        'Write setting
        Try
            Util.WriteApplicationSetting("UserProfileSubDirectoriesList", _
                My.Settings.UserProfileSubDirectoriesList)
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
        Me.lsvMain.Items.Clear()

        Dim subDirectory As String = String.Empty
        For Each subDirectory In My.Settings.UserProfileSubDirectoriesList
            Me.lsvMain.Items.Add(subDirectory)
        Next

        Me.lsvMain.SelectedItems.Clear()
        'Me.btnRemove.Enabled = Me.lsvMain.Items.Count > 0
        Me.btnClear.Enabled = Me.lsvMain.Items.Count > 0
    End Sub

    Private Sub txtSubDirectory_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSubDirectory.TextChanged
        Me.btnAdd.Enabled = Not String.IsNullOrEmpty(Me.txtSubDirectory.Text)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        'Add SubDirectory to User Profile SubDirectories List
        My.Settings.UserProfileSubDirectoriesList.Add(Me.txtSubDirectory.Text)

        'Write setting
        Try
            Util.WriteApplicationSetting("UserProfileSubDirectoriesList", _
                My.Settings.UserProfileSubDirectoriesList)
        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try

        'Refresh
        Me.RefreshData()

        Me.txtSubDirectory.ResetText()
        Me.txtSubDirectory.Focus()
    End Sub

    Private Sub txtSubDirectory_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtSubDirectory.Validating
        If Me.txtSubDirectory.Text.IndexOfAny(System.IO.Path.GetInvalidPathChars()) <> -1 Then
            e.Cancel = True
            Util.ShowError("Path contains invalid chars.", False)
        End If

        If Not e.Cancel AndAlso System.IO.Path.IsPathRooted(Me.txtSubDirectory.Text) Then
            e.Cancel = True
            Util.ShowError("The path is not a subdirectory.", False)
        End If

        If e.Cancel Then Me.txtSubDirectory.SelectAll()
    End Sub
End Class