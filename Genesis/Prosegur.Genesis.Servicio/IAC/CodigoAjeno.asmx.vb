Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class CodigoAjeno
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ICodigoAjeno

        ''' <summary>
        ''' Obtem Códigos Ajenos
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 19/04/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetCodigosAjenos(objPeticion As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion) As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta Implements ContractoServicio.ICodigoAjeno.GetCodigosAjenos
            ' criar objeto
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno
            Return objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)
        End Function

        ''' <summary>
        ''' Obtem Códigos Ajenos By Cliente, SubCliente e Ponto Servico
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [danielnunes] 22/07/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function GetAjenoByClienteSubClientePuntoServicio(objPeticion As ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Peticion) As ContractoServicio.CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio.Respuesta Implements ContractoServicio.ICodigoAjeno.GetAjenoByClienteSubClientePuntoServicio
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno
            Return objAccionCodigoAjeno.getAjenoByClienteSubClientePuntoServicio(objPeticion)
        End Function

        ''' <summary>
        ''' Insere ou atualiza Códigos Ajenos
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 23/04/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function SetCodigosAjenos(objPeticion As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta Implements ContractoServicio.ICodigoAjeno.SetCodigosAjenos
            ' criar objeto
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno
            Return objAccionCodigoAjeno.SetCodigosAjenos(objPeticion)
        End Function

        ''' <summary>
        ''' Insere ou atualiza Códigos Ajenos
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [kasantos] 23/04/2013 Criado
        ''' </history>
        <WebMethod()> _
        Public Function VerificarIdentificadorXCodigoAjeno(objPeticion As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Peticion) As ContractoServicio.CodigoAjeno.VerificarIdentificadorXCodigoAjeno.Respuesta Implements ContractoServicio.ICodigoAjeno.VerificarIdentificadorXCodigoAjeno
            ' criar objeto
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno
            Return objAccionCodigoAjeno.VerificarIdentificadorXCodigoAjeno(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICodigoAjeno.Test
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno
            Return objAccionCodigoAjeno.Test()
        End Function

       
    End Class

End Namespace