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
    Public Class ATM
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IATM

        ''' <summary>
        ''' Esta operación es responsable por obtener los detalles de un ATM de acuerdo con los parámetros de entrada. 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 07/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetATMDetail(Peticion As ContractoServicio.ATM.GetATMDetail.Peticion) As ContractoServicio.ATM.GetATMDetail.Respuesta Implements ContractoServicio.IATM.GetATMDetail

            Dim accion As New LogicaNegocio.AccionATM
            Return accion.GetATMDetail(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por mantener los datos del ATM, y de sus morfologías y procesos. 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 07/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetATM(Peticion As ContractoServicio.ATM.SetATM.Peticion) As ContractoServicio.ATM.SetATM.Respuesta Implements ContractoServicio.IATM.SetATM

            Dim accion As New LogicaNegocio.AccionATM
            Return accion.SetATM(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de los ATM’s de acuerdo con los parámetros de entrada. 
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 07/01/2011 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetATMs(Peticion As ContractoServicio.GetATMs.Peticion) As ContractoServicio.GetATMs.Respuesta Implements ContractoServicio.IATM.GetATMs

            Dim accion As New LogicaNegocio.AccionATM
            Return accion.GetATMs(Peticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IATM.Test
            Dim accion As New LogicaNegocio.AccionATM
            Return accion.Test()
        End Function

    End Class

End Namespace