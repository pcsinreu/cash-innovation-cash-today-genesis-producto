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
    Public Class TiposProcesado
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITipoProcesado

        <WebMethod()> _
        Public Function GetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.GetTiposProcesado
            Dim objTiposProcesado As New LogicaNegocio.AccionTiposProcesado
            Return objTiposProcesado.GetTiposProcesado(Peticion)
        End Function

        <WebMethod()> _
        Public Function SetTiposProcesado(Peticion As ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion) As ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta Implements ContractoServicio.ITipoProcesado.SetTiposProcesado
            Dim objTiposProcesado As New LogicaNegocio.AccionTiposProcesado
            Return objTiposProcesado.SetTiposProcesado(Peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarCodigoTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.VerificarCodigoTipoProcesado
            Dim objTiposProcesado As New LogicaNegocio.AccionTiposProcesado
            Return objTiposProcesado.VerificarCodigoTipoProcesado(peticion)
        End Function

        <WebMethod()> _
        Public Function VerificarDescripcionTipoProcesado(peticion As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion) As ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta Implements ContractoServicio.ITipoProcesado.VerificarDescripcionTipoProcesado
            Dim objTiposProcesado As New LogicaNegocio.AccionTiposProcesado
            Return objTiposProcesado.VerificarDescripcionTipoProcesado(peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITipoProcesado.Test
            Dim objTiposProcesado As New LogicaNegocio.AccionTiposProcesado
            Return objTiposProcesado.Test()
        End Function
    End Class

End Namespace