Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ConfiguracionGeneral
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.IConfiguracionGeneral

    ''' <summary>
    ''' Recupera as Configuracines generales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function GetConfiguracionGeneralReportes() As ContractoServicio.Configuracion.General.Respuesta Implements ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReportes
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.GetConfiguracionGeneralReportes()
    End Function

    ''' <summary>
    ''' Metódo de test.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 05/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IConfiguracionGeneral.Test
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.Test()
    End Function

    ''' <summary>
    ''' Metódo de inserir uma nova configuração.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 08/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function InserirConfiguracionGeneralReporte(peticion As ContractoServicio.Configuracion.General.Peticion) As IAC.ContractoServicio.Configuracion.General.Respuesta Implements IAC.ContractoServicio.IConfiguracionGeneral.InserirConfiguracionGeneralReporte
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.InserirConfiguracionGeneralReporte(peticion)
    End Function

    ''' <summary>
    ''' Metódo de configurações.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function ExcluirConfiguracionGeneralReporte(peticion As IAC.ContractoServicio.Configuracion.General.Peticion) As IAC.ContractoServicio.Configuracion.General.Respuesta Implements IAC.ContractoServicio.IConfiguracionGeneral.ExcluirConfiguracionGeneralReporte
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.ExcluirConfiguracionGeneralReporte(peticion)
    End Function

    ''' <summary>
    ''' Metódo de configurações.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function AtualizarConfiguracionGeneralReporte(peticion As IAC.ContractoServicio.Configuracion.General.Peticion) As IAC.ContractoServicio.Configuracion.General.Respuesta Implements IAC.ContractoServicio.IConfiguracionGeneral.AtualizarConfiguracionGeneralReporte
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.AtualizarConfiguracionGeneralReporte(peticion)
    End Function

    ''' <summary>
    ''' Metódo de configurações.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    <WebMethod()> _
    Public Function GetConfiguracionGeneralReporte(peticion As IAC.ContractoServicio.Configuracion.General.Peticion) As IAC.ContractoServicio.Configuracion.General.Respuesta Implements IAC.ContractoServicio.IConfiguracionGeneral.GetConfiguracionGeneralReporte
        Dim objConfiguracionGeneral As New LogicaNegocio.AccionConfiguracionGeneral
        Return objConfiguracionGeneral.GetConfiguracionGeneralReporte(peticion)
    End Function
End Class