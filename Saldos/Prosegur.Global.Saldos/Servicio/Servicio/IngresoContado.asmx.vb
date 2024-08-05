Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.Conteo.Legado.ContractoServicio

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class IngresoContado
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Function AccionIngresoContado(ByVal Peticion As TransferirDatos.Peticion) As TransferirDatos.Respuesta
        Dim objNegocio As New LogicaNegocioServicio.AccionIngresoContado
        Return objNegocio.Ejecutar(Peticion)
    End Function

End Class