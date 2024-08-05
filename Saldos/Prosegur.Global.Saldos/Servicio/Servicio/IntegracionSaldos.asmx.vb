Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class IntegracionSaldos
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.IIntegracionSalidas

    ''' <summary>
    ''' Es la operación que crea el documento F22 en el sistema de Saldos de acuerdo con los parámetros enviados en el mensaje de entrada.
    ''' </summary>
    ''' <param name="Peticion">ContractoServicio.GeneracionF22.Peticion</param>
    ''' <returns>ContractoServicio.GeneracionF22.Respuesta</returns>
    ''' <remarks></remarks>
    ''' <history>[abueno] 13/07/2010 Creado</history>
    <WebMethod()> _
    Public Function GeneracionF22(ByVal Peticion As ContractoServicio.GeneracionF22.Peticion) As ContractoServicio.GeneracionF22.Respuesta Implements ContractoServicio.IIntegracionSalidas.GeneracionF22
        Dim acao As New LogicaNegocioServicio.AccionGeneracionF22
        Return acao.GeneracionF22(Peticion)
    End Function

    ''' <summary>
    ''' Es la operación que crea el documento MIF Intersector en el sistema de Saldos de acuerdo con los 
    ''' parámetros enviados en el mensaje de entrada.
    ''' </summary>
    ''' <param name="Peticion">ContractoServicio.CreacionMifIntersector.Peticion</param>
    ''' <returns>ContractoServicio.CreacionMifIntersector.Respuesta</returns>
    ''' <remarks></remarks>
    ''' <history>[abueno] 23/07/2010 Creado</history>
    <WebMethod()> _
    Public Function CreacionMifIntersector(ByVal Peticion As ContractoServicio.CreacionMifIntersector.Peticion) As ContractoServicio.CreacionMifIntersector.Respuesta Implements ContractoServicio.IIntegracionSalidas.CreacionMifIntersector
        Dim acao As New LogicaNegocioServicio.AccionMIF
        Return acao.CrearMifIntersector(Peticion)
    End Function

    ''' <summary>
    ''' Responsavel por atualizar o estado do documento mif
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/04/2011 - Criado
    ''' </history>
    <WebMethod()> _
    Public Function CambiaEstadoDocumentoFondosSaldos(Peticion As ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion) As ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta Implements ContractoServicio.IIntegracionSalidas.CambiaEstadoDocumentoFondosSaldos
        Dim acao As New LogicaNegocioServicio.AccionCambiaEstadoDocumentoFondosSaldos
        Return acao.Ejecutar(Peticion)
    End Function

End Class