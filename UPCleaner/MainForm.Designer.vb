<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.grdMain = New System.Windows.Forms.DataGridView
        Me.colName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colLastLoad = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPath = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BindingSourceUserProfiles = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnDeleteClear = New System.Windows.Forms.Button
        Me.btnExcludeUserProfile = New System.Windows.Forms.Button
        Me.stsMain = New System.Windows.Forms.StatusStrip
        Me.lblUserProfilesNotLoadByDays = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUserProfilesNotLoadByDaysCount = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUserProfilesExcluded = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUserProfilesExcludedCount = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUserProfiles = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblUserProfilesCount = New System.Windows.Forms.ToolStripStatusLabel
        Me.tltMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblLastLoadDays = New System.Windows.Forms.Label
        Me.tlsMain = New System.Windows.Forms.ToolStrip
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.btnSettings = New System.Windows.Forms.ToolStripButton
        Me.btnUserProfilesExclusionList = New System.Windows.Forms.ToolStripButton
        Me.btnToolsUserProfileSubDirectoriesList = New System.Windows.Forms.ToolStripButton
        Me.mnsMain = New System.Windows.Forms.MenuStrip
        Me.mniFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mniFileExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mniView = New System.Windows.Forms.ToolStripMenuItem
        Me.mniViewRefresh = New System.Windows.Forms.ToolStripMenuItem
        Me.mniTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mniToolsSettings = New System.Windows.Forms.ToolStripMenuItem
        Me.mniToolsUserProfileExclusionList = New System.Windows.Forms.ToolStripMenuItem
        Me.mniToolsUserProfileSubDirectoriesList = New System.Windows.Forms.ToolStripMenuItem
        Me.nupLastLoadDays = New System.Windows.Forms.NumericUpDown
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceUserProfiles, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.stsMain.SuspendLayout()
        Me.tlsMain.SuspendLayout()
        Me.mnsMain.SuspendLayout()
        CType(Me.nupLastLoadDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMain
        '
        Me.grdMain.AllowUserToAddRows = False
        Me.grdMain.AllowUserToDeleteRows = False
        Me.grdMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMain.AutoGenerateColumns = False
        Me.grdMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colName, Me.colLastLoad, Me.colPath})
        Me.grdMain.DataSource = Me.BindingSourceUserProfiles
        Me.grdMain.Location = New System.Drawing.Point(0, 49)
        Me.grdMain.Name = "grdMain"
        Me.grdMain.ReadOnly = True
        Me.grdMain.RowHeadersVisible = False
        Me.grdMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdMain.Size = New System.Drawing.Size(632, 343)
        Me.grdMain.TabIndex = 1
        '
        'colName
        '
        Me.colName.DataPropertyName = "Name"
        Me.colName.HeaderText = "Name"
        Me.colName.Name = "colName"
        Me.colName.ReadOnly = True
        '
        'colLastLoad
        '
        Me.colLastLoad.DataPropertyName = "LastLoadTime"
        Me.colLastLoad.HeaderText = "Last load time"
        Me.colLastLoad.Name = "colLastLoad"
        Me.colLastLoad.ReadOnly = True
        '
        'colPath
        '
        Me.colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colPath.DataPropertyName = "Path"
        Me.colPath.HeaderText = "Path"
        Me.colPath.Name = "colPath"
        Me.colPath.ReadOnly = True
        '
        'BindingSourceUserProfiles
        '
        Me.BindingSourceUserProfiles.Filter = ""
        Me.BindingSourceUserProfiles.Sort = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(545, 398)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnDeleteClear
        '
        Me.btnDeleteClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDeleteClear.Enabled = False
        Me.btnDeleteClear.Location = New System.Drawing.Point(464, 398)
        Me.btnDeleteClear.Name = "btnDeleteClear"
        Me.btnDeleteClear.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteClear.TabIndex = 3
        Me.btnDeleteClear.Text = "Delete/Clear"
        Me.tltMain.SetToolTip(Me.btnDeleteClear, "Delete/Clear selected user profiles")
        Me.btnDeleteClear.UseVisualStyleBackColor = True
        '
        'btnExcludeUserProfile
        '
        Me.btnExcludeUserProfile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExcludeUserProfile.Enabled = False
        Me.btnExcludeUserProfile.Location = New System.Drawing.Point(252, 398)
        Me.btnExcludeUserProfile.Name = "btnExcludeUserProfile"
        Me.btnExcludeUserProfile.Size = New System.Drawing.Size(75, 23)
        Me.btnExcludeUserProfile.TabIndex = 4
        Me.btnExcludeUserProfile.Text = "Exclude"
        Me.tltMain.SetToolTip(Me.btnExcludeUserProfile, "Exclude user profile")
        Me.btnExcludeUserProfile.UseVisualStyleBackColor = True
        '
        'stsMain
        '
        Me.stsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblUserProfilesNotLoadByDays, Me.lblUserProfilesNotLoadByDaysCount, Me.lblUserProfilesExcluded, Me.lblUserProfilesExcludedCount, Me.lblUserProfiles, Me.lblUserProfilesCount})
        Me.stsMain.Location = New System.Drawing.Point(0, 424)
        Me.stsMain.Name = "stsMain"
        Me.stsMain.Size = New System.Drawing.Size(632, 22)
        Me.stsMain.SizingGrip = False
        Me.stsMain.TabIndex = 8
        '
        'lblUserProfilesNotLoadByDays
        '
        Me.lblUserProfilesNotLoadByDays.Name = "lblUserProfilesNotLoadByDays"
        Me.lblUserProfilesNotLoadByDays.Size = New System.Drawing.Size(173, 17)
        Me.lblUserProfilesNotLoadByDays.Text = "User Profiles not load by days {0}:"
        '
        'lblUserProfilesNotLoadByDaysCount
        '
        Me.lblUserProfilesNotLoadByDaysCount.Name = "lblUserProfilesNotLoadByDaysCount"
        Me.lblUserProfilesNotLoadByDaysCount.Size = New System.Drawing.Size(13, 17)
        Me.lblUserProfilesNotLoadByDaysCount.Text = "0"
        Me.lblUserProfilesNotLoadByDaysCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserProfilesExcluded
        '
        Me.lblUserProfilesExcluded.Name = "lblUserProfilesExcluded"
        Me.lblUserProfilesExcluded.Size = New System.Drawing.Size(117, 17)
        Me.lblUserProfilesExcluded.Text = "User profiles excluded:"
        '
        'lblUserProfilesExcludedCount
        '
        Me.lblUserProfilesExcludedCount.Name = "lblUserProfilesExcludedCount"
        Me.lblUserProfilesExcludedCount.Size = New System.Drawing.Size(13, 17)
        Me.lblUserProfilesExcludedCount.Text = "0"
        Me.lblUserProfilesExcludedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserProfiles
        '
        Me.lblUserProfiles.Name = "lblUserProfiles"
        Me.lblUserProfiles.Size = New System.Drawing.Size(71, 17)
        Me.lblUserProfiles.Text = "User profiles:"
        '
        'lblUserProfilesCount
        '
        Me.lblUserProfilesCount.Name = "lblUserProfilesCount"
        Me.lblUserProfilesCount.Size = New System.Drawing.Size(13, 17)
        Me.lblUserProfilesCount.Text = "0"
        Me.lblUserProfilesCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLastLoadDays
        '
        Me.lblLastLoadDays.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLastLoadDays.AutoSize = True
        Me.lblLastLoadDays.Location = New System.Drawing.Point(12, 403)
        Me.lblLastLoadDays.Name = "lblLastLoadDays"
        Me.lblLastLoadDays.Size = New System.Drawing.Size(184, 13)
        Me.lblLastLoadDays.TabIndex = 9
        Me.lblLastLoadDays.Text = "Show profiles not load by days (0=all):"
        '
        'tlsMain
        '
        Me.tlsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnSettings, Me.btnUserProfilesExclusionList, Me.btnToolsUserProfileSubDirectoriesList})
        Me.tlsMain.Location = New System.Drawing.Point(0, 24)
        Me.tlsMain.Name = "tlsMain"
        Me.tlsMain.Size = New System.Drawing.Size(632, 25)
        Me.tlsMain.TabIndex = 10
        Me.tlsMain.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(23, 22)
        Me.btnRefresh.ToolTipText = "Refresh"
        '
        'btnSettings
        '
        Me.btnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSettings.Image = Global.UPCleaner.My.Resources.Resources.PropertiesHS
        Me.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(23, 22)
        Me.btnSettings.Text = "Settings"
        '
        'btnUserProfilesExclusionList
        '
        Me.btnUserProfilesExclusionList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnUserProfilesExclusionList.Image = Global.UPCleaner.My.Resources.Resources.List
        Me.btnUserProfilesExclusionList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUserProfilesExclusionList.Name = "btnUserProfilesExclusionList"
        Me.btnUserProfilesExclusionList.Size = New System.Drawing.Size(23, 22)
        Me.btnUserProfilesExclusionList.Text = "User profiles exclusion list"
        '
        'btnToolsUserProfileSubDirectoriesList
        '
        Me.btnToolsUserProfileSubDirectoriesList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnToolsUserProfileSubDirectoriesList.Image = Global.UPCleaner.My.Resources.Resources.CopyFolderHS
        Me.btnToolsUserProfileSubDirectoriesList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnToolsUserProfileSubDirectoriesList.Name = "btnToolsUserProfileSubDirectoriesList"
        Me.btnToolsUserProfileSubDirectoriesList.Size = New System.Drawing.Size(23, 22)
        Me.btnToolsUserProfileSubDirectoriesList.Text = "User profile subdirectories list"
        '
        'mnsMain
        '
        Me.mnsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFile, Me.mniView, Me.mniTools})
        Me.mnsMain.Location = New System.Drawing.Point(0, 0)
        Me.mnsMain.Name = "mnsMain"
        Me.mnsMain.Size = New System.Drawing.Size(632, 24)
        Me.mnsMain.TabIndex = 11
        Me.mnsMain.Text = "MenuStrip1"
        '
        'mniFile
        '
        Me.mniFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniFileExit})
        Me.mniFile.Name = "mniFile"
        Me.mniFile.Size = New System.Drawing.Size(35, 20)
        Me.mniFile.Text = "&File"
        '
        'mniFileExit
        '
        Me.mniFileExit.Name = "mniFileExit"
        Me.mniFileExit.Size = New System.Drawing.Size(103, 22)
        Me.mniFileExit.Text = "E&xit"
        '
        'mniView
        '
        Me.mniView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniViewRefresh})
        Me.mniView.Name = "mniView"
        Me.mniView.Size = New System.Drawing.Size(41, 20)
        Me.mniView.Text = "&View"
        '
        'mniViewRefresh
        '
        Me.mniViewRefresh.Image = CType(resources.GetObject("mniViewRefresh.Image"), System.Drawing.Image)
        Me.mniViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mniViewRefresh.Name = "mniViewRefresh"
        Me.mniViewRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.mniViewRefresh.Size = New System.Drawing.Size(142, 22)
        Me.mniViewRefresh.Text = "Refresh"
        Me.mniViewRefresh.ToolTipText = "Refresh"
        '
        'mniTools
        '
        Me.mniTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mniToolsSettings, Me.mniToolsUserProfileExclusionList, Me.mniToolsUserProfileSubDirectoriesList})
        Me.mniTools.Name = "mniTools"
        Me.mniTools.Size = New System.Drawing.Size(44, 20)
        Me.mniTools.Text = "&Tools"
        '
        'mniToolsSettings
        '
        Me.mniToolsSettings.Image = Global.UPCleaner.My.Resources.Resources.PropertiesHS
        Me.mniToolsSettings.Name = "mniToolsSettings"
        Me.mniToolsSettings.Size = New System.Drawing.Size(226, 22)
        Me.mniToolsSettings.Text = "&Settings"
        '
        'mniToolsUserProfileExclusionList
        '
        Me.mniToolsUserProfileExclusionList.Image = Global.UPCleaner.My.Resources.Resources.List
        Me.mniToolsUserProfileExclusionList.Name = "mniToolsUserProfileExclusionList"
        Me.mniToolsUserProfileExclusionList.Size = New System.Drawing.Size(226, 22)
        Me.mniToolsUserProfileExclusionList.Text = "User profiles &exclusion list"
        '
        'mniToolsUserProfileSubDirectoriesList
        '
        Me.mniToolsUserProfileSubDirectoriesList.Image = Global.UPCleaner.My.Resources.Resources.CopyFolderHS
        Me.mniToolsUserProfileSubDirectoriesList.Name = "mniToolsUserProfileSubDirectoriesList"
        Me.mniToolsUserProfileSubDirectoriesList.Size = New System.Drawing.Size(226, 22)
        Me.mniToolsUserProfileSubDirectoriesList.Text = "User profile sub&directories list"
        '
        'nupLastLoadDays
        '
        Me.nupLastLoadDays.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.nupLastLoadDays.Location = New System.Drawing.Point(202, 401)
        Me.nupLastLoadDays.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.nupLastLoadDays.Name = "nupLastLoadDays"
        Me.nupLastLoadDays.Size = New System.Drawing.Size(44, 20)
        Me.nupLastLoadDays.TabIndex = 6
        Me.nupLastLoadDays.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 446)
        Me.Controls.Add(Me.tlsMain)
        Me.Controls.Add(Me.lblLastLoadDays)
        Me.Controls.Add(Me.stsMain)
        Me.Controls.Add(Me.mnsMain)
        Me.Controls.Add(Me.nupLastLoadDays)
        Me.Controls.Add(Me.btnExcludeUserProfile)
        Me.Controls.Add(Me.btnDeleteClear)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grdMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnsMain
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User profiles"
        CType(Me.grdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceUserProfiles, System.ComponentModel.ISupportInitialize).EndInit()
        Me.stsMain.ResumeLayout(False)
        Me.stsMain.PerformLayout()
        Me.tlsMain.ResumeLayout(False)
        Me.tlsMain.PerformLayout()
        Me.mnsMain.ResumeLayout(False)
        Me.mnsMain.PerformLayout()
        CType(Me.nupLastLoadDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdMain As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnDeleteClear As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colLastLoad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPath As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BindingSourceUserProfiles As System.Windows.Forms.BindingSource
    Friend WithEvents btnExcludeUserProfile As System.Windows.Forms.Button
    Friend WithEvents nupLastLoadDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents stsMain As System.Windows.Forms.StatusStrip
    Friend WithEvents lblUserProfilesNotLoadByDays As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserProfilesNotLoadByDaysCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tltMain As System.Windows.Forms.ToolTip
    Friend WithEvents lblLastLoadDays As System.Windows.Forms.Label
    Friend WithEvents tlsMain As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnsMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mniView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniViewRefresh As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnUserProfilesExclusionList As System.Windows.Forms.ToolStripButton
    Friend WithEvents mniFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mniToolsUserProfileExclusionList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblUserProfilesExcluded As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserProfilesExcludedCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserProfiles As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblUserProfilesCount As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents mniToolsSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnToolsUserProfileSubDirectoriesList As System.Windows.Forms.ToolStripButton
    Friend WithEvents mniToolsUserProfileSubDirectoriesList As System.Windows.Forms.ToolStripMenuItem
End Class
