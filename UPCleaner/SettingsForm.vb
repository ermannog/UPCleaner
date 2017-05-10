Public Class SettingsForm

    Private Sub SettingsForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.prgMain.SelectedObject = New UPCleanerSettings
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'Write settings
        Try
            With DirectCast(Me.prgMain.SelectedObject, UPCleanerSettings)
                Util.WriteApplicationSetting("LogEnabled", .LogEnabled.ToString())
                Util.WriteApplicationSetting("LogHistoryFiles", .LogHistoryFiles.ToString())
                Util.WriteApplicationSetting("UserProfileClearMode", CInt(.UserProfileClearMode).ToString())
            End With

        Catch ex As Exception
            Util.ShowErrorException(ex, False)
        End Try
    End Sub
End Class