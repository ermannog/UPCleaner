Public NotInheritable Class Util
    Private Sub New()
        MyBase.New()
    End Sub

    Public Shared Function SetWaitCursor(ByVal state As Boolean) As Boolean
        Dim waitState As Boolean

        waitState = System.Windows.Forms.Cursor.Current Is System.Windows.Forms.Cursors.WaitCursor

        If state Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        Return waitState
    End Function

    Public Shared Function GetExceptionMessage(ByVal message As String, ByVal exception As System.Exception) As String
        Dim text As String = message

        Dim ex As System.Exception = exception

        While ex IsNot Nothing
            'Aggiunta Message
            If Not String.IsNullOrEmpty(ex.Message) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine & ControlChars.NewLine
                text &= ex.Message
            End If

            'If showDetailsException Then
            'Aggiunta Source
            If Not String.IsNullOrEmpty(ex.Source) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine
                text &= ex.GetType().ToString()
            End If

            'Aggiunta Error code
            Dim lastWin32Error = GetLastWin32Error(String.Empty)
            If Not String.IsNullOrEmpty(lastWin32Error) Then
                text &= ControlChars.NewLine & ControlChars.NewLine
                text &= "Last Win32 Error: " & lastWin32Error
            End If

            'Aggiunta StackTrace
            If Not String.IsNullOrEmpty(ex.StackTrace) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine & ControlChars.NewLine
                text &= ex.StackTrace.Trim()
            End If

            ex = ex.InnerException
        End While

        Return text
    End Function

#Region "Method ShowErrorException"
    Public Overloads Shared Sub ShowErrorException(ByVal exception As System.Exception, ByVal abort As Boolean)
        ShowError(GetExceptionMessage(String.Empty, exception), abort)
    End Sub

    Public Overloads Shared Sub ShowErrorException(ByVal message As String, ByVal exception As System.Exception, ByVal abort As Boolean)
        ShowError(GetExceptionMessage(message, exception), abort)
    End Sub
#End Region

    Public Shared Function GetLastWin32Error(ByVal message As String) As String
        Dim text As String = message

        Dim ex As New System.ComponentModel.Win32Exception( _
            System.Runtime.InteropServices.Marshal.GetLastWin32Error())


        'Aggiunta Error code
        If ex.NativeErrorCode <> 0 Then
            If Not String.IsNullOrEmpty(text) Then _
                text &= ControlChars.NewLine & ControlChars.NewLine
            text &= "Error code: " & Hex(ex.NativeErrorCode) & " (" & ex.Message & ")"
        End If

        Return text
    End Function

#Region "Method ShowLastWin32Error"
    Public Overloads Shared Sub ShowLastWin32Error(ByVal abort As Boolean)
        ShowLastWin32Error(String.Empty, abort)
    End Sub

    Public Overloads Shared Sub ShowLastWin32Error(ByVal message As String, ByVal abort As Boolean)

        ShowMessage(GetLastWin32Error(message), _
            "Error", _
            System.Windows.Forms.MessageBoxIcon.Stop)

        If abort Then
            System.Environment.Exit(0)
        End If
    End Sub
#End Region

#Region "Method ShowMessage"
    Public Overloads Shared Function ShowMessage(ByVal text As String) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, ByVal title As String) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, title, System.Windows.Forms.MessageBoxIcon.None)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                ByVal icon As System.Windows.Forms.MessageBoxIcon) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty, icon)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                                  ByVal title As String, _
                                  ByVal icon As System.Windows.Forms.MessageBoxIcon) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, title, System.Windows.Forms.MessageBoxButtons.OK, icon, System.Windows.Forms.MessageBoxDefaultButton.Button1)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                                  ByVal title As String, _
                                  ByVal buttons As System.Windows.Forms.MessageBoxButtons, _
                                  ByVal icon As System.Windows.Forms.MessageBoxIcon, _
                                  ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        ShowMessage = System.Windows.Forms.MessageBox.Show(text, _
            My.Application.Info.Title & " " & title, _
            buttons, icon, defaultButton)
    End Function
#End Region

#Region "Metodo Question"
    Public Overloads Shared Function ShowQuestion(ByVal text As String) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, System.Windows.Forms.MessageBoxButtons.YesNo)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, System.Windows.Forms.MessageBoxButtons.YesNo, defaultButton)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, _
                                    ByVal buttons As System.Windows.Forms.MessageBoxButtons) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, buttons, System.Windows.Forms.MessageBoxDefaultButton.Button2)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, _
                                              ByVal buttons As System.Windows.Forms.MessageBoxButtons, _
                                              ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty, buttons, System.Windows.Forms.MessageBoxIcon.Question, defaultButton)
    End Function
#End Region

    Public Shared Sub ShowError(ByVal message As String, ByVal abort As Boolean)
        ShowMessage(message, "Error", System.Windows.Forms.MessageBoxIcon.Stop)

        If abort Then
            System.Environment.Exit(0)
        End If
    End Sub

    Public Overloads Shared Sub WriteApplicationSetting(ByVal settingName As String, ByVal value As String)
        Const sectionGroupName As String = "applicationSettings"
        Dim sectionName As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name & _
            ".My.MySettings"

        'Lettura configurazione
        Dim config = System.Configuration.ConfigurationManager.OpenExeConfiguration( _
            Configuration.ConfigurationUserLevel.None)

        'Ricerca sezione applicationSettings
        Dim applicationSettingsSection As System.Configuration.ClientSettingsSection = _
            DirectCast(config.SectionGroups(sectionGroupName).Sections(sectionName), System.Configuration.ClientSettingsSection)

        'Impostazione Valore
        If applicationSettingsSection.Settings.Get(settingName) IsNot Nothing Then
            applicationSettingsSection.Settings.Get(settingName).Value.ValueXml.InnerXml = value
        End If
        applicationSettingsSection.SectionInformation.ForceSave = True

        'Salvataggio valore
        config.Save()

        'Reload configurazione
        System.Configuration.ConfigurationManager.RefreshSection(applicationSettingsSection.SectionInformation.SectionName)

        'Rilascio risorse
        applicationSettingsSection = Nothing
        config = Nothing
    End Sub

    Public Overloads Shared Sub WriteApplicationSetting(ByVal settingName As String, ByVal value As Object)
        If value.GetType().IsValueType Then
            WriteApplicationSetting(settingName, System.Convert.ToString(value))
        Else
            Dim xs As New System.Xml.Serialization.XmlSerializer(value.GetType())
            Dim sb As New System.Text.StringBuilder

            Using sw As New System.IO.StringWriter(sb)
                xs.Serialize(sw, value)
            End Using

            Dim s = sb.ToString
            s = s.Substring(s.IndexOf(ControlChars.NewLine), s.Length - s.IndexOf(ControlChars.NewLine)).Trim()

            WriteApplicationSetting(settingName, s)

            s = Nothing
            xs = Nothing
            sb = Nothing
        End If

    End Sub

    Public Shared Sub WriteConnectionStringsSetting(ByVal name As String, ByVal value As String)
        Const sectionName As String = "connectionStrings"
        Dim connectionStringName As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name & _
            ".My.MySettings." & name

        'Lettura configurazione
        Dim config = System.Configuration.ConfigurationManager.OpenExeConfiguration( _
            Configuration.ConfigurationUserLevel.None)

        'Ricerca sezione applicationSettings
        Dim connectionStringsSection As System.Configuration.ConnectionStringsSection = _
            DirectCast(config.Sections(sectionName), System.Configuration.ConnectionStringsSection)

        'Impostazione Valore
        connectionStringsSection.ConnectionStrings(connectionStringName).ConnectionString = value

        'Salvataggio valore
        config.Save()

        'Refresh configurazione per forzare lettura da file
        System.Configuration.ConfigurationManager.RefreshSection(connectionStringsSection.SectionInformation.SectionName)

        'Rilascio risorse
        connectionStringsSection = Nothing
        config = Nothing
    End Sub

    Public Shared Sub AddLogEntry(ByVal message As String)
        If My.Settings.LogEnabled Then
            My.Application.Log.DefaultFileLogWriter.WriteLine( _
                "[" & Now.ToShortDateString & " " & Now.ToLongTimeString & "] " & message)
        End If
    End Sub

End Class
