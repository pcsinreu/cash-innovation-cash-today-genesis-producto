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
    Public Class Divisa
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IDivisa

        ''' <summary>
        ''' Método responsável por obter denominaciones pela divisa
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function getDenominacionesByDivisa(Peticion As ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion) As ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta Implements ContractoServicio.IDivisa.getDenominacionesByDivisa

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.getDenominacionesByDivisa(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por obter as divisas
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function getDivisas(Peticion As ContractoServicio.Divisa.GetDivisas.Peticion) As ContractoServicio.Divisa.GetDivisas.Respuesta Implements ContractoServicio.IDivisa.getDivisas

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.getDivisas(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por obter as divisas realizando a paginação na base de dados.
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [carlos.bomtempo] 07/02/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetDivisasPaginacion(Peticion As ContractoServicio.Divisa.GetDivisasPaginacion.Peticion) As ContractoServicio.Divisa.GetDivisasPaginacion.Respuesta Implements ContractoServicio.IDivisa.GetDivisasPaginacion

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.GetDivisasPaginacion(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por inserir as divisas e suas denominaciones
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function setDivisaDenominaciones(Peticion As ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion) As ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta Implements ContractoServicio.IDivisa.setDivisaDenominaciones

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.setDivisaDenominaciones(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por verificar se o codigo denominacion existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarCodigoDenominacion(Peticion As ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion) As ContractoServicio.Divisa.VerificarCodigoDenominacion.Respuesta Implements ContractoServicio.IDivisa.VerificarCodigoDenominacion

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.VerificarCodigoDenominacion(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por verificar se o codigo divisa existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarCodigoDivisa(Peticion As ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion) As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta Implements ContractoServicio.IDivisa.VerificarCodigoDivisa

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.VerificarCodigoDivisa(Peticion)

        End Function

        ''' <summary>
        ''' Método responsável por verificar se a descripcion divisa existe
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarDescripcionDivisa(Peticion As ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion) As ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta Implements ContractoServicio.IDivisa.VerificarDescripcionDivisa

            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.VerificarDescripcionDivisa(Peticion)

        End Function


        ''' <summary>
        ''' Método de teste.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/02/2010 - Criado
        ''' </history>
        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDivisa.Test
            ' criar objeto
            Dim objAccionDivisa As New LogicaNegocio.AccionDivisa
            Return objAccionDivisa.Test()

        End Function
    End Class

End Namespace