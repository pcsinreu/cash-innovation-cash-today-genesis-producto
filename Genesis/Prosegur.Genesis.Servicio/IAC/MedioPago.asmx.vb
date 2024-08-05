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
    Public Class MedioPago
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IMedioPago

        ''' <summary>
        ''' Método responsável por obter os Medios de Pago
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 12/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetMedioPagos(Peticion As ContractoServicio.MedioPago.GetMedioPagos.Peticion) As ContractoServicio.MedioPago.GetMedioPagos.Respuesta Implements ContractoServicio.IMedioPago.GetMedioPagos
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.GetMedioPagos(Peticion)
        End Function
        ''' <summary>
        ''' Método responsável por obter os Terminos de Medios de Pago
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 12/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetMedioPagoDetail(Peticion As ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion) As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta Implements ContractoServicio.IMedioPago.GetMedioPagoDetail
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.GetMedioPagoDetail(Peticion)
        End Function

        ''' <summary>
        ''' Método responsável por verificar se o código Medio de Pagojá existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 25/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarCodigoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta Implements ContractoServicio.IMedioPago.VerificarCodigoMedioPago
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.VerificarCodigoMedioPago(Peticion)
        End Function

        ''' <summary>
        ''' Método responsável por verificar se o código do termino Medio de Pago já existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 25/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarCodigoTerminoMedioPago(Peticion As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion) As ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta Implements ContractoServicio.IMedioPago.VerificarCodigoTerminoMedioPago
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.VerificarCodigoTerminoMedioPago(Peticion)
        End Function

        ''' <summary>
        ''' Método responsável por inserir, atualizar e deletar Medios de Pago já existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 27/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetMedioPago(Peticion As ContractoServicio.MedioPago.SetMedioPago.Peticion) As ContractoServicio.MedioPago.SetMedioPago.Respuesta Implements ContractoServicio.IMedioPago.SetMedioPago
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.SetMedioPago(Peticion)
        End Function

        ''' <summary>
        ''' Método Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 05/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IMedioPago.Test
            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMedioPago
            Return objAccionMedioPago.Test()
        End Function
    End Class

End Namespace