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
    Public Class Morfologia
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IMorfologia

        ''' <summary>
        ''' Esta operación es responsable por obtener los datos de las morfologías de acuerdo con los parámetros de entrada.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 20/12/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetMorfologia(Peticion As ContractoServicio.Morfologia.GetMorfologia.Peticion) As ContractoServicio.Morfologia.GetMorfologia.Respuesta Implements ContractoServicio.IMorfologia.GetMorfologia

            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMorfologia
            Return objAccionMedioPago.GetMorfologia(Peticion)

        End Function


        ''' <summary>
        ''' Esta operación es responsable por mantener las morfologías de acuerdo con los parámetros de entrada.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 20/12/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetMorfologia(Peticion As ContractoServicio.Morfologia.SetMorfologia.Peticion) As ContractoServicio.Morfologia.SetMorfologia.Respuesta Implements ContractoServicio.IMorfologia.SetMorfologia

            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMorfologia
            Return objAccionMedioPago.SetMorfologia(Peticion)

        End Function

        ''' <summary>
        ''' Esta operación es responsable por verificar la existencia de una morfología con el
        ''' codigo o descripción informados.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 29/12/2010 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarMorfologia(Peticion As ContractoServicio.Morfologia.VerificarMorfologia.Peticion) As ContractoServicio.Morfologia.VerificarMorfologia.Respuesta Implements ContractoServicio.IMorfologia.VerificarMorfologia

            ' criar objeto
            Dim objAccionMedioPago As New LogicaNegocio.AccionMorfologia
            Return objAccionMedioPago.VerificarMorfologia(Peticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IMorfologia.Test
            Dim objAccion As New LogicaNegocio.AccionMorfologia
            Return objAccion.Test()
        End Function

    End Class

End Namespace