Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class IngresoRemesa
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.IIngresoRemesa

    <WebMethod()> _
    Function AccionIngresoRemesas(ByVal Peticion As ContractoServicio.IngresoRemesas.Peticion) As ContractoServicio.IngresoRemesas.Respuesta Implements ContractoServicio.IIngresoRemesa.AccionIngresoRemesas
        Dim objNegocio As New LogicaNegocioServicio.AccionIngresoRemesas
        Return objNegocio.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Function AccionRecuperarCentroProceso(ByVal Peticion As ContractoServicio.RecuperarCentroProceso.Peticion) As ContractoServicio.RecuperarCentroProceso.Respuesta Implements ContractoServicio.IIngresoRemesa.AccionRecuperarCentroProceso
        Dim objNegocio As New LogicaNegocioServicio.AccionRecuperarCentroProceso
        Return objNegocio.Ejecutar(Peticion)
    End Function

End Class