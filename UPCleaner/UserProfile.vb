Public Class UserProfile
    Public Sub New(ByVal sid As String, ByVal pathPath As String, ByVal lastLoadTime As DateTime)
        Me.sidValue = sid
        Me.pathValue = pathPath
        Me.lastLoadTimeValue = lastLoadTime
    End Sub

#Region "Proprietà SID"
    Private sidValue As String

    <System.ComponentModel.Browsable(False)> _
    Public ReadOnly Property SID() As String
        Get
            Return Me.sidValue
        End Get
    End Property
#End Region

#Region "Proprietà LastLoadTime"
    Private lastLoadTimeValue As DateTime

    Public ReadOnly Property LastLoadTime() As DateTime
        Get
            Return Me.lastLoadTimeValue
        End Get
    End Property
#End Region

#Region "Proprietà Path"
    Private pathValue As String

    Public ReadOnly Property Path() As String
        Get
            Return Me.pathValue
        End Get
    End Property
#End Region

    Public ReadOnly Property Name() As String
        Get
            Return System.IO.Path.GetFileName(Me.pathValue)
        End Get
    End Property

    Public Function GetFolderSize() As Long
        GetFolderSize = 0
        For Each f In My.Computer.FileSystem.GetDirectoryInfo(Path).GetFiles("*.*", IO.SearchOption.AllDirectories)
            GetFolderSize += f.Length
        Next
    End Function
End Class
