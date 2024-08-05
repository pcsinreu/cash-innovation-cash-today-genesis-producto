Imports System.ComponentModel
Imports System.ServiceModel
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Notification.Nilo

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://xmlns.services.prosegur.com/corp/notifications/online/event")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Notificacion
    Inherits System.Web.Services.WebService
    <WebMethod()>
    <SoapDocumentMethod(ParameterStyle:=SoapParameterStyle.Bare, Action:="/notificationAPI")>
    Public Function onlineNotificationEventOp(<XmlElement(ElementName:="request")> request As Request) As <XmlElement(ElementName:="response")> Response

        Return Genesis.LogicaNegocio.Notification.AccionRegistrarEventoNilo.Ejecutar(request)
    End Function

End Class