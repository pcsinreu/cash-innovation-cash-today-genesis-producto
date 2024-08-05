Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Xml.Serialization


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
<Microsoft.Web.Services3.Policy("ServerPolicy")> _
Public Class ServicioBase
    Inherits Microsoft.Web.Services3.WebServicesClientProtocol

    Public Sesion As SesionInfo

    Public Class SesionInfo
        Inherits System.Web.Services.Protocols.SoapHeader

        Public ID As String
        Public Usuario As String
        Public NombreAplicacion As String

    End Class

End Class
