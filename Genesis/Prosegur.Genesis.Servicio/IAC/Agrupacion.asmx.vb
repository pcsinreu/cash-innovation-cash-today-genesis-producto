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
    Public Class Agrupacion
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IAgrupacion

        ''' <summary>
        ''' Verifica se um código agrupacion existe
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 05/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarCodigoAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta Implements ContractoServicio.IAgrupacion.VerificarCodigoAgrupacion
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionAgrupacion
            Return objAccionDivisa.VerificarCodigoAgrupacion(objPeticion)
        End Function

        ''' <summary>
        ''' Verifica se uma descrição agrupacion existe
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 05/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarDescripcionAgrupacion(objPeticion As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion) As ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta Implements ContractoServicio.IAgrupacion.VerificarDescripcionAgrupacion
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionAgrupacion
            Return objAccionDivisa.VerificarDescripcionAgrupacion(objPeticion)
        End Function

        ''' <summary>
        ''' Obtém as agrupaciones através de várias possibilidades de busca
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 05/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.GetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta Implements ContractoServicio.IAgrupacion.GetAgrupaciones
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionAgrupacion
            Return objAccionDivisa.GetAgrupaciones(objPeticion)
        End Function

        ''' <summary>
        ''' Obtém os detalhes da agrupacion atraves dos codigos
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 05/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetAgrupacionesDetail(objPeticion As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta Implements ContractoServicio.IAgrupacion.GetAgrupacionesDetail
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionAgrupacion
            Return objAccionDivisa.GetAgrupacionesDetail(objPeticion)
        End Function

        ''' <summary>
        ''' Insere, altera e baixa as agrupaciones
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 05/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetAgrupaciones(objPeticion As ContractoServicio.Agrupacion.SetAgrupaciones.Peticion) As ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta Implements ContractoServicio.IAgrupacion.SetAgrupaciones
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionAgrupacion
            Return objAccionDivisa.SetAgrupaciones(objPeticion)
        End Function

        ''' <summary>
        ''' Metodo Test
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IAgrupacion.Test
            ' criar objeto
            Dim objAccion As New LogicaNegocio.AccionAgrupacion
            Return objAccion.Test()
        End Function
    End Class

End Namespace