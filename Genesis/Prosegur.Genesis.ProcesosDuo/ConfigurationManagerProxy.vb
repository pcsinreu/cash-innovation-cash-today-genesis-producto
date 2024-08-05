Imports System.Configuration.Internal
Imports System.Configuration
Imports System.Reflection
Imports System.Collections.Specialized

Friend NotInheritable Class ConfigurationManagerProxy
    Implements IInternalConfigSystem

    ReadOnly baseConfig As IInternalConfigSystem
    ReadOnly currentNamespace As String

    Private Shared initialized As Boolean = False

    Friend Shared Sub SetupProxy(classNamespace As String)
        If Not initialized Then
            initialized = True
            Dim o As Object = ConfigurationManager.AppSettings
            Dim s_configSystem As FieldInfo = GetType(ConfigurationManager).GetField("s_configSystem", BindingFlags.Static OrElse BindingFlags.NonPublic)
            s_configSystem.SetValue(Nothing, New ConfigurationManagerProxy(DirectCast(s_configSystem.GetValue(Nothing), IInternalConfigSystem), classNamespace))
        End If
    End Sub

    Private Sub New(baseConfiguration As IInternalConfigSystem, classNamespace As String)
        Me.baseConfig = baseConfiguration
        Me.currentNamespace = classNamespace
    End Sub

    Private appsettings As Object
    Public Function GetSection(configKey As String) As Object Implements System.Configuration.Internal.IInternalConfigSystem.GetSection
        If configKey = "appSettings" AndAlso Me.appsettings IsNot Nothing Then
            Return Me.appsettings
        End If
        Dim originalSection As Object = baseConfig.GetSection(configKey)
        If configKey = "appSettings" AndAlso TypeOf originalSection Is NameValueCollection Then
            Dim customSection As New NameValueCollection(DirectCast(originalSection, NameValueCollection))
            Dim currentNamespaceSection As Object = baseConfig.GetSection(Me.currentNamespace)
            If currentNamespaceSection IsNot Nothing AndAlso TypeOf currentNamespaceSection Is NameValueCollection Then
                customSection.Add(DirectCast(currentNamespaceSection, NameValueCollection))
            End If
            Me.appsettings = customSection
            Return customSection
        End If
        Return originalSection
    End Function

    Public Sub RefreshConfig(sectionName As String) Implements System.Configuration.Internal.IInternalConfigSystem.RefreshConfig
        If sectionName = "appSettings" Then
            appsettings = Nothing
        End If
        baseConfig.RefreshConfig(sectionName)
    End Sub

    Public ReadOnly Property SupportsUserConfig As Boolean Implements System.Configuration.Internal.IInternalConfigSystem.SupportsUserConfig
        Get
            Return baseConfig.SupportsUserConfig
        End Get
    End Property

End Class
