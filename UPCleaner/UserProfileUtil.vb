Public NotInheritable Class UserProfileUtil
    'Public Const LastLoadDaysDefault As Integer = 60

    Private Sub New()
        MyBase.New()
    End Sub

#Region "Delete user profile"
    '<System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError:=True, charset:=Runtime.InteropServices.CharSet.Auto)> _
    'Private Shared Function DeleteProfile(ByVal lpSidString As String, ByVal lpProfilePath As String, ByVal lpComputerName As String) As Boolean
    'End Function

    'Public Shared Function DeleteUserProfile(ByVal sid As String) As Boolean
    '    Return DeleteProfile(sid, Nothing, Nothing)
    'End Function
#End Region

#Region "Search user profiles"
    Private Const ProfileListKey As String = "Software\Microsoft\Windows NT\CurrentVersion\ProfileList"
    Private Const ProfileImagePathName As String = "ProfileImagePath"
    Private Const ProfileLoadTimeLowName As String = "ProfileLoadTimeLow"
    Private Const ProfileLoadTimeHighName As String = "ProfileLoadTimeHigh"
    Private Const TempEnvironmentVariableName As String = "TEMP"
    Private Const UserProfileEnvironmentVariableName As String = "USERPROFILE"
    Private Const UserSIDPrefix As String = "S-1-5-21"
    Private Const RIDNonDefaultUserMinValue As Integer = 1000

    Public Overloads Shared Function GetUserProfiles() As System.Collections.Generic.List(Of UserProfile)
        Return GetUserProfiles(0)
    End Function

    Public Overloads Shared Function GetUserProfiles(ByVal daysLastLoad As Integer) As System.Collections.Generic.List(Of UserProfile)
        'daysLastLoad=0 All Profiles

        Dim profiles As New System.Collections.Generic.List(Of UserProfile)

        Dim path As String = String.Empty
        Dim loadTime As DateTime = Nothing
        For Each name In My.Computer.Registry.LocalMachine.OpenSubKey(ProfileListKey, False).GetSubKeyNames()
            If IncludeUser(name) Then
                path = My.Computer.Registry.LocalMachine.OpenSubKey(ProfileListKey, False).OpenSubKey(name).GetValue(ProfileImagePathName).ToString()
                loadTime = GetLastLoadProfileTime(name, path)

                If (Now - loadTime).TotalDays >= daysLastLoad Then
                    profiles.Add(New UserProfile(name, path, loadTime))
                End If
            End If
        Next

        Return profiles
    End Function

    Private Shared Function IncludeUser(ByVal sid As String) As Boolean
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
