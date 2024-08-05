Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class IntegracionMiAgencia
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.IIntegracionMiAgencia

    <WebMethod()> _
    Public Function CrearMovimiento(Peticion As ContractoServicio.CrearMovimiento.Peticion) As ContractoServicio.CrearMovimiento.Respuesta Implements ContractoServicio.IIntegracionMiAgencia.CrearMovimiento

        Dim acao As New LogicaNegocioServicio.AccionCrearMovimiento
        Return acao.Ejecutar(Peticion)

    End Function

    <WebMethod()> _
    Function RecuperarSaldos(ByVal Peticion As ContractoServicio.RecuperarSaldos.Peticion) As ContractoServicio.RecuperarSaldos.Respuesta Implements ContractoServicio.IIntegracionMiAgencia.RecuperarSaldos

        Dim acao As New LogicaNegocioServicio.AccionRecuperarSaldos
        Return acao.Ejecutar(Peticion)

    End Function

    <WebMethod()> _
    Function RecuperarTransacciones(ByVal Peticion As ContractoServicio.RecuperarTransacciones.Peticion) As ContractoServicio.RecuperarTransacciones.Respuesta Implements ContractoServicio.IIntegracionMiAgencia.RecuperarTransacciones

        Dim acao As New LogicaNegocioServicio.AccionRecuperarTransacciones
        Return acao.Ejecutar(Peticion)

    End Function

    <WebMethod()> _
    Function RecuperarTransaccionDetallada(ByVal Peticion As ContractoServicio.RecuperarTransaccionDetallada.Peticion) As ContractoServicio.RecuperarTransaccionDetallada.Respuesta Implements ContractoServicio.IIntegracionMiAgencia.RecuperarTransaccionDetallada

        Dim acao As New LogicaNegocioServicio.AccionRecuperarTransaccionDetallada
        Return acao.Ejecutar(Peticion)

    End Function


End Class