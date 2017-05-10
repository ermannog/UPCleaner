Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Public IsBatchMode As Boolean = False

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            'Check command line parameter /b
            For Each element In e.CommandLine
                If element.ToLower = "/b" Then
                    Me.IsBatchMode = True
                    Exit For
                End If
            Next

            'Setting log
            If My.Settings.LogEnabled Then
                'Set location to executable directory
                My.Application.Log.DefaultFileLogWriter.Location = _
                    Logging.LogFileLocation.ExecutableDirectory
                'Set AutoFlush
                My.Application.Log.DefaultFileLogWriter.AutoFlush = True
            End If

            'Clear old log files
            If My.Settings.LogEnabled AndAlso My.Settings.LogHistoryFiles > 0 Then
                'Delete last log file
                Dim lastLogFile = My.Application.Log.DefaultFileLogWriter.FullLogFileName & _
                    "." & My.Settings.LogHistoryFiles
                If My.Computer.FileSystem.FileExists(lastLogFile) Then
                    My.Computer.FileSystem.DeleteFile(lastLogFile)
                End If

                'Move log files
                For i As Integer = My.Settings.LogHistoryFiles - 1 To 1 Step -1
                    If My.Computer.FileSystem.FileExists( _
                        My.Application.Log.DefaultFileLogWriter.FullLogFileName & "." & i) Then
                        My.Computer.FileSystem.MoveFile( _
                            My.Application.Log.DefaultFileLogWriter.FullLogFileName & "." & i, _
                            My.Application.Log.DefaultFileLogWriter.FullLogFileName & "." & i + 1)
                    End If
                Next

                'Move current log files
                Dim currentLogFile = My.Application.Log.DefaultFileLogWriter.FullLogFileName
                If My.Computer.FileSystem.FileExists(currentLogFile) AndAlso _
                    My.Computer.FileSystem.GetFileInfo(currentLogFile).Length > 0 Then
                    My.Application.Log.DefaultFileLogWriter.Close()

                    My.Computer.FileSystem.MoveFile( _
                        currentLogFile, currentLogFile & ".1")
                End If
            End If

            'Batch Mode
            If Me.IsBatchMode Then
                'Log Start
                Util.AddLogEntry(String.Format("Start {0} version {1} in batch mode.", _
                    System.Windows.Forms.Application.ProductName, _
                    System.Windows.Forms.Application.ProductVersion))

                'Search user profiles not load by LastLoadDays days
                UPCleanerUtil.FillUserProfiles(My.Settings.LastLoadDays)

                'Clear User profiles
                For Each userProfile In UPCleanerUtil.SelectedUserProfiles
                    Select Case My.Settings.UserProfileClearMode
                        Case UPCleanerSettings.UserProfileClearModes.DeleteUserProfile
                            UPCleanerUtil.DeleteUserProfile(userProfile)
                        Case UPCleanerSettings.UserProfileClearModes.ClearSubDirectories
                            UPCleanerUtil.ClearUserProfile(userProfile)
                    End Select
                Next

                'Log Shutdown
                Util.AddLogEntry("End application.")

                e.Cancel = True
            Else
                'Log Start
                Util.AddLogEntry(String.Format("Start {0} version {1} in interactive mode.", _
                    System.Windows.Forms.Application.ProductName, _
                    System.Windows.Forms.Application.ProductVersion))
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            If Not My.Application.IsBatchMode Then
                Util.ShowErrorException(e.Exception, False)
            End If

            Util.AddLogEntry(Util.GetExceptionMessage( _
                String.Empty, e.Exception))
        End Sub

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            'Log Shutdown
            Util.AddLogEntry("End application.")
        End Sub

    End Class

End Namespace

