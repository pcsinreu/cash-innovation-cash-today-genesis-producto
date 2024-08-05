Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class TipoPuntoServicio
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoPuntoServicio

        <WebMethod()> _
        Public Function getTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta Implements ContractoServicio.ITipoPuntoServicio.getTiposPuntosServicio
            Dim objAccionTipoPunto As New LogicaNegocio.AccionTipoPuntoServicio
            Return objAccionTipoPunto.getTiposPuntosServicio(Peticion)
        End Function

        <WebMethod()> _
        Public Function setTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion) As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Respuesta Implements ContractoServicio.ITipoPuntoServicio.setTiposPuntosServicio
            Dim objAccionTipoPunto As New LogicaNegocio.AccionTipoPuntoServicio
            Return objAccionTipoPunto.setTiposPuntosServicio(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoPuntoServicio.Test
            Dim objAccionTipoPunto As New LogicaNegocio.AccionTipoPuntoServicio
            Return objAccionTipoPunto.Test()
        End Function

    End Class
End Namespace