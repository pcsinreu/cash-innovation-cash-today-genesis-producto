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
    Public Class Termino
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ITermino

        ''' <summary>
        ''' Método responsável por obter os Terminos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 12/02/2009 Criado
        ''' </history>
        <WebMethod()> _
        Public Function getTerminos(Peticion As ContractoServicio.TerminoIac.GetTerminoIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoIac.Respuesta Implements ContractoServicio.ITermino.getTerminos
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.getTerminos(Peticion)

        End Function

        <WebMethod()> _
        Public Function getTerminoDetail(Peticion As ContractoServicio.TerminoIac.GetTerminoDetailIac.Peticion) As ContractoServicio.TerminoIac.GetTerminoDetailIac.Respuesta Implements ContractoServicio.ITermino.getTerminoDetail
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.getTerminoDetail(Peticion)

        End Function

        <WebMethod()> _
        Public Function setTermino(Peticion As ContractoServicio.TerminoIac.SetTerminoIac.Peticion) As ContractoServicio.TerminoIac.SetTerminoIac.Respuesta Implements ContractoServicio.ITermino.setTermino
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.setTermino(Peticion)

        End Function
        <WebMethod()> _
        Public Function VerificarCodigoTermino(Peticion As ContractoServicio.TerminoIac.VerificarCodigoTermino.Peticion) As ContractoServicio.TerminoIac.VerificarCodigoTermino.Respuesta Implements ContractoServicio.ITermino.VerificarCodigoTermino
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.VerificarCodigoTermino(Peticion)
        End Function
        <WebMethod()> _
        Public Function VerificarDescripcionTermino(Peticion As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Peticion) As ContractoServicio.TerminoIac.VerificarDescripcionTermino.Respuesta Implements ContractoServicio.ITermino.VerificarDescripcionTermino
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.VerificarDescripcionTermino(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ITermino.Test
            ' criar objeto
            Dim objAccionTermino As New LogicaNegocio.AccionTermino
            Return objAccionTermino.Test()
        End Function
    End Class

End Namespace