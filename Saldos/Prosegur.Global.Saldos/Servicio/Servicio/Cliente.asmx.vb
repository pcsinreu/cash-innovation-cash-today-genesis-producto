Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Cliente
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.ICliente

    <WebMethod()> _
    Public Function AccionGuardarCliente(ByVal Peticion As ContractoServicio.GuardarCliente.Peticion) As ContractoServicio.GuardarCliente.Respuesta Implements ContractoServicio.ICliente.AccionGuardarCliente
        Dim objNegocio As New LogicaNegocioServicio.AccionGuardarCliente
        Return objNegocio.Ejecutar(Peticion)
    End Function

End Class