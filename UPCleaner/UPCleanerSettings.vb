Public Class UPCleanerSettings
    Implements System.ComponentModel.ICustomTypeDescriptor

    Private Const SettingsCategory As String = "Settings"
    Private Const LoggingCategory As String = "Logging"

#Region "Property UserProfileClearMode"
    Public Enum UserProfileClearModes
        DeleteUserProfile = 0
        ClearSubDirectories = 1
    End Enum

    Private userProfileClearModesValue As UserProfileClearModes = _
        DirectCast(My.Settings.UserProfileClearMode, UserProfileClearModes)

    <System.ComponentModel.Category(SettingsCategory)> _
    <System.ComponentModel.DisplayName("User profile clear mode")> _
    <System.ComponentModel.Description("User profile clear mode")> _
    Public Property UserProfileClearMode() As UserProfileClearModes
        Get
            Return Me.userProfileClearModesValue
        End Get
        Set(ByVal value As UserProfileClearModes)
            Me.userProfileClearModesValue = value
        End Set
    End Property

    Private Function ShouldSerializeUserProfileClearMode() As Boolean
        Return Me.userProfileClearModesValue <> My.Settings.UserProfileClearMode
    End Function
#End Region

#Region "Property LogEnabled"
    Private logEnabledValue As Boolean = My.Settings.LogEnabled

    <System.ComponentModel.Category(LoggingCategory)> _
    <System.ComponentModel.DisplayName("Log enabled")> _
    <System.ComponentModel.Description("Enable logging")> _
    <System.ComponentModel.RefreshProperties(System.ComponentModel.RefreshProperties.All)> _
    Public Property LogEnabled() As Boolean
        Get
            Return Me.logEnabledValue
        End Get
        Set(ByVal value As Boolean)
            Me.logEnabledValue = value
        End Set
    End Property

    Private Function ShouldSerializeLogEnabled() As Boolean
        Return Me.logEnabledValue <> My.Settings.LogEnabled
    End Function
#End Region

#Region "Property LogHistoryFiles"
    Private logHistoryFilesValue As Integer = My.Settings.LogHistoryFiles

    <System.ComponentModel.Category(LoggingCategory)> _
    <System.ComponentModel.DisplayName("Log history files")> _
    <System.ComponentModel.Description("Number of log file maintained" & ControlChars.NewLine & _
                                       "(0=Always write on same log file)")> _
    Public Property LogHistoryFiles() As Integer
        Get
            Return Me.logHistoryFilesValue
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then Throw New System.ArgumentOutOfRangeException
            Me.logHistoryFilesValue = value
        End Set
    End Property

    Private Function ShouldSerializeLogHistoryFiles() As Boolean
        Return Me.logHistoryFilesValue <> My.Settings.LogHistoryFiles
    End Function
#End Region

#Region "Implementazione System.ComponentModel.ICustomTypeDescriptor"
    Public Function GetAttributes() As System.ComponentModel.AttributeCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetAttributes
        Return System.ComponentModel.TypeDescriptor.GetAttributes(Me, True)
    End Function

    Public Function GetClassName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetClassName
        Return System.ComponentModel.TypeDescriptor.GetClassName(Me, True)
    End Function

    Public Function GetComponentName() As String Implements System.ComponentModel.ICustomTypeDescriptor.GetComponentName
        Return System.ComponentModel.TypeDescriptor.GetComponentName(Me, True)
    End Function

    Public Function GetConverter() As System.ComponentModel.TypeConverter Implements System.ComponentModel.ICustomTypeDescriptor.GetConverter
        Return System.ComponentModel.TypeDescriptor.GetConverter(Me, True)
    End Function

    Public Function GetDefaultEvent() As System.ComponentModel.EventDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent
        Return System.ComponentModel.TypeDescriptor.GetDefaultEvent(Me, True)
    End Function

    Public Function GetDefaultProperty() As System.ComponentModel.PropertyDescriptor Implements System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty
        Return System.ComponentModel.TypeDescriptor.GetDefaultProperty(Me, True)
    End Function

    Public Function GetEditor(ByVal editorBaseType As System.Type) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetEditor
        Return System.ComponentModel.TypeDescriptor.GetEditor(Me, editorBaseType, True)
    End Function

    Public Function GetEvents() As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
        Return System.ComponentModel.TypeDescriptor.GetEvents(Me, True)
    End Function

    Public Function GetEvents(ByVal attributes() As System.Attribute) As System.ComponentModel.EventDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetEvents
        Return System.ComponentModel.TypeDescriptor.GetEvents(Me, attributes, True)
    End Function

    Public Function GetProperties() As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
        Return System.ComponentModel.TypeDescriptor.GetProperties(Me, True)
    End Function

    Public Function GetProperties(ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection Implements System.ComponentModel.ICustomTypeDescriptor.GetProperties
        Dim baseProperties As System.ComponentModel.PropertyDescriptorCollection = System.ComponentModel.TypeDescriptor.GetProperties(Me, attributes, True)

        Dim newProperties(baseProperties.Count - 1) As System.ComponentModel.PropertyDescriptor

        For i As Integer = 0 To baseProperties.Count - 1
            If baseProperties(i).Name = "LogHistoryFiles" Then
                newProperties(i) = New CustomPropertyDescriptor(baseProperties(i), Not Me.logEnabledValue)
            Else
                newProperties(i) = baseProperties(i)
            End If
        Next

        Return New System.ComponentModel.PropertyDescriptorCollection(newProperties)
    End Function

    Public Function GetPropertyOwner(ByVal pd As System.ComponentModel.PropertyDescriptor) As Object Implements System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function

#End Region

End Class

Public Class CustomPropertyDescriptor
    Inherits System.ComponentModel.PropertyDescriptor

    Private descriptor As System.ComponentModel.PropertyDescriptor
    Private readOnlyValue As Boolean

    Public Sub New(ByVal baseDescriptor As System.ComponentModel.PropertyDescriptor, ByVal readOlnly As Boolean)
        MyBase.New(baseDescriptor)
        Me.descriptor = baseDescriptor
        Me.readOnlyValue = readOlnly
    End Sub

    Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
        Me.descriptor.CanResetValue(component)
    End Function

    Public Overrides ReadOnly Property ComponentType() As System.Type
        Get
            Return Me.descriptor.ComponentType
        End Get
    End Property

    Public Overrides Function GetValue(ByVal component As Object) As Object
        Return Me.descriptor.GetValue(component)
    End Function

    Public Overrides ReadOnly Property IsReadOnly() As Boolean
        Get
            Return Me.readOnlyValue
            'Return Me.descriptor.IsReadOnly()
        End Get
    End Property

    Public Overrides ReadOnly Property PropertyType() As System.Type
        Get
            Return Me.descriptor.PropertyType
        End Get
    End Property

    Public Overrides Sub ResetValue(ByVal component As Object)
        Me.descriptor.ResetValue(component)
    End Sub

    Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
        Me.descriptor.SetValue(component, value)
    End Sub

    Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
        Return Me.descriptor.ShouldSerializeValue(component)
    End Function
End Class
