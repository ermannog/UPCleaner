Public NotInheritable Class UPCleanerUtil
    Private Sub New()
        MyBase.New()
    End Sub

    Private Shared selectedUserProfilesValue As New System.Collections.Generic.List(Of UserProfile)
    Public Shared ReadOnly Property SelectedUserProfiles() As System.Collections.Generic.List(Of UserProfile)
        Get
            Return UPCleanerUtil.selectedUserProfilesValue
        End Get
    End Property

    Private Shared userProfilesValue As New System.Collections.Generic.List(Of UserProfile)
    Public Shared ReadOnly Property UserProfiles() As System.Collections.Generic.List(Of UserProfile)
        Get
            Return UPCleanerUtil.userProfilesValue
        End Get
    End Property

#Region "Delete User Profile"
    <System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError:=True, charset:=Runtime.InteropServices.CharSet.Auto)> _
    Private Shared Function DeleteProfile(ByVal lpSidString As String, ByVal lpProfilePath As String, ByVal lpComputerName As String) As Boolean
    End Function

    Public Shared Sub DeleteUserProfile(ByVal userProfile As UserProfile)
        Util.AddLogEntry("Delete user profile " & userProfile.Name & " Sid=" & userProfile.SID & ".")
        Try
            If Not DeleteProfile(userProfile.SID, Nothing, Nothing) Then
                Dim errorDescription As String = _
                    "Error during delete profile " & userProfile.Name & " Sid=" & userProfile.SID
                Dim errorText As String = _
                    Util.GetLastWin32Error(String.Empty)

                If Not My.Application.IsBatchMode Then
                    Util.ShowError(errorDescription & ControlChars.NewLine & _
                                   errorText, False)
                End If

                selectedUserProfilesValue.Remove(userProfile)
                userProfilesValue.Remove(userProfile)

                Util.AddLogEntry(errorDescription & " " & errorText & ".")
            Else
                Util.AddLogEntry("Successfully delete user profile " & userProfile.Name & " Sid=" & userProfile.SID & ".")
            End If

        Catch ex As Exception
            If Not My.Application.IsBatchMode Then
                Util.ShowErrorException(ex, False)
            End If

            Util.AddLogEntry(Util.GetExceptionMessage( _
                "Error during delete user profile " & _
                userProfile.Name & " Sid=" & userProfile.SID & ".", _
                ex))
        End Try
    End Sub
#End Region

    Public Shared Sub ClearUserProfile(ByVal userProfile As UserProfile)
        Util.AddLogEntry("Clear user profile " & userProfile.Name & " Sid=" & userProfile.SID & ".")

        Try
            Dim path As String = String.Empty
            For Each subDirectory In My.Settings.UserProfileSubDirectoriesList
                path = System.IO.Path.Combine(userProfile.Path, subDirectory)

                If System.IO.Directory.Exists(path) Then
                    'Delete Files
                    Dim filesDeleted As Integer = 0
                    Dim filesErrorDeleted As Integer = 0

                    For Each file In System.IO.Directory.GetFiles(path)
                        Try
                            System.IO.File.Delete(file)
                            filesDeleted += 1
                        Catch ex As Exception
                            filesErrorDeleted += 1

                            If Not My.Application.IsBatchMode Then
                                Util.ShowErrorException(ex, False)
                            End If

                            Util.AddLogEntry(Util.GetExceptionMessage( _
                                "Error during delete file " & file & ".", ex))
                        End Try
                    Next

                    'Delete directories
                    Dim directoriesDeleted As Integer = 0
                    Dim directoriesErrorDeleted As Integer = 0

                    For Each directory In System.IO.Directory.GetDirectories(path)
                        Try
                            System.IO.Directory.Delete(directory, True)
                            directoriesDeleted += 1
                        Catch ex As Exception
                            directoriesErrorDeleted += 1

                            If Not My.Application.IsBatchMode Then
                                Util.ShowErrorException(ex, False)
                            End If

                            Util.AddLogEntry(Util.GetExceptionMessage( _
                                "Error during delete directory " & directory & ".", ex))
                        End Try
                    Next

                    If filesErrorDeleted = 0 AndAlso directoriesErrorDeleted = 0 Then
                        If filesDeleted = 0 AndAlso directoriesDeleted = 0 Then
                            Util.AddLogEntry("Directory " & path & " is void.") '& _

                        Else
                            Util.AddLogEntry("Successfully clear directory " & path & " " & _
                                filesDeleted & " files deleted, " & _
                                directoriesDeleted & " directories deleted.")
                        End If
                    ElseIf filesErrorDeleted = 0 AndAlso directoriesErrorDeleted = 0 Then
                        Util.AddLogEntry("Unsuccessfully clear directory " & path & " " & _
                            filesDeleted & " files deleted, " & _
                            directoriesDeleted & " directories deleted, " & _
                            filesErrorDeleted & " files error deleted, " & _
                            directoriesDeleted & " directories error deleted.")
                    End If
                Else
                    Dim errorText As String = _
                        String.Format("Error during clear Subdirectory {0} the directory not exists.", path)
                    If Not My.Application.IsBatchMode Then
                        Util.ShowError(errorText, False)
                    End If
                    Util.AddLogEntry(errorText)
                End If
            Next
        Catch ex As Exception
            If Not My.Application.IsBatchMode Then
                Util.ShowErrorException(ex, False)
            End If

            Util.AddLogEntry(Util.GetExceptionMessage( _
                "Error during clear user profile " & _
                userProfile.Name & " Sid=" & userProfile.SID, _
                ex))
        End Try
    End Sub

#Region "Search user profiles"
    Private Const ProfileListKey As String = "Software\Microsoft\Windows NT\CurrentVersion\ProfileList"
    Private Const ProfileImagePathName As String = "ProfileImagePath"
    Private Const ProfileLoadTimeLowName As String = "ProfileLoadTimeLow"
    Private Const ProfileLoadTimeHighName As String = "ProfileLoadTimeHigh"
    Private Const TempEnvironmentVariableName As String = "TEMP"
    Private Const UserProfileEnvironmentVariableName As String = "USERPROFILE"
    Private Const UserSIDPrefix As String = "S-1-5-21"
    Private Const RIDNonDefaultUserMinValue As Integer = 1000

    Public Shared Sub FillUserProfiles(ByVal daysLastLoad As Integer)
        Try
            Util.AddLogEntry(String.Format( _
                "Search user profiles not load by days {0}.", daysLastLoad))

            UPCleanerUtil.selectedUserProfilesValue.Clear()
            UPCleanerUtil.userProfilesValue.Clear()

            Dim path As String = String.Empty
            Dim loadTime As DateTime = Nothing
            Dim userProfile As UserProfile
            For Each sid In My.Computer.Registry.LocalMachine.OpenSubKey(ProfileListKey, False).GetSubKeyNames()
                If IsValidUser(sid) Then
                    path = My.Computer.Registry.LocalMachine.OpenSubKey(ProfileListKey, False).OpenSubKey(sid).GetValue(ProfileImagePathName).ToString()
                    loadTime = GetLastLoadProfileTime(sid, path)
                    userProfile = New UserProfile(sid, path, loadTime)

                    userProfilesValue.Add(userProfile)

                    If My.Settings.UserProfileExclusionList.IndexOf(sid) = -1 AndAlso _
                        (Now - loadTime).TotalDays >= daysLastLoad Then
                        UPCleanerUtil.selectedUserProfilesValue.Add(New UserProfile(sid, path, loadTime))
                    End If
                End If
            Next

            Util.AddLogEntry(String.Format( _
                "Found {0} user profiles not load by days {1}.", _
                UPCleanerUtil.selectedUserProfilesValue.Count, daysLastLoad))
        Catch ex As Exception
            If Not My.Application.IsBatchMode Then
                Util.ShowErrorException(ex, False)
            End If

            Util.AddLogEntry(Util.GetExceptionMessage(String.Format( _
                "Error during serch user profiles not load by days {0}.", daysLastLoad), _
                ex))
        End Try
    End Sub

    Private Shared Function IsValidUser(ByVal sid As String) As Boolean
        'Esclusion of system'users
        'Well-known security identifiers in Windows operating systems
        'http://support.microsoft.com/kb/243330/en-us

        'Esclusion SID of user that are not local users or domain users
        If Not sid.ToUpper().StartsWith(UserSIDPrefix) Then _
            Return False

        'If Not CInt(Microsoft.VisualBasic.Strings.Mid(sid, sid.LastIndexOf("-"c) + 2)) > RIDNonDefaultUserMinValue Then Return False

        'Exclusion of current user
        If sid.ToUpper() = System.Security.Principal.WindowsIdentity.GetCurrent().User.ToString().ToUpper() Then _
            Return False

        Return True
    End Function

    Private Shared Function GetLastLoadProfileTime(ByVal profile As String, ByVal path As String) As DateTime
        Dim profileLoadTimeLow As Integer = _
            CInt(My.Computer.Registry.LocalMachine.OpenSubKey( _
                ProfileListKey, False).OpenSubKey(profile).GetValue( _
                ProfileLoadTimeLowName, 0))

        Dim profileLoadTimeHigh As Integer = _
            CInt(My.Computer.Registry.LocalMachine.OpenSubKey( _
                ProfileListKey, False).OpenSubKey(profile).GetValue( _
                ProfileLoadTimeHighName, 0))

        Dim nanoSecs As Long = _
            CLng((profileLoadTimeHigh * 2 ^ 32) + profileLoadTimeLow)

        If nanoSecs <> 0 Then
            GetLastLoadProfileTime = #1/1/1601#.AddDays(((nanoSecs / 600000000) / 1440)).ToLocalTime()
        Else
            'Esame della data ultima modifica alla directory Temp dell'utente
            'nel caso il profilo non risulti mai acceduto
            'per esempio nel caso di utente RDS con avvio dell'applicazione
            'e non del desktop

            'Ritrovamento path profilo utente corrente e 
            'rimozione eventuali path contratti con ~
            Dim currentUserProfilePath = New System.IO.DirectoryInfo( _
                System.Environment.GetEnvironmentVariable(UserProfileEnvironmentVariableName)).FullName

            'Ritrovamento path temp utente corrente
            Dim currentTempRootPath = System.IO.Path.GetTempPath()
            'Su sistemi multi utente viene fornita una subdirectory per ogni sessione
            While Not currentTempRootPath.ToLower().EndsWith(TempEnvironmentVariableName.ToLower())
                currentTempRootPath = currentTempRootPath.Substring(0, currentTempRootPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar))
            End While

            'Costruzione SubDirectory path temp utente
            Dim tempSubDirectoryPath As String = _
                currentTempRootPath.Remove( _
                0, currentUserProfilePath.Length).Trim( _
                System.IO.Path.DirectorySeparatorChar)

            'Lettura data ultimo accesso al file UsrClass.dat
            GetLastLoadProfileTime = System.IO.Directory.GetLastWriteTime( _
                System.IO.Path.Combine(path, tempSubDirectoryPath))
        End If
    End Function
#End Region

End Class
