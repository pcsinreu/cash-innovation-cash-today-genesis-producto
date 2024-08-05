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
    Public Class DatoBancario
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IDatoBancario

        <WebMethod()> _
        Public Function GetDatosBancarios(Peticion As ContractoServicio.DatoBancario.GetDatosBancarios.Peticion) As ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta Implements ContractoServicio.IDatoBancario.GetDatosBancarios

            ' criar objeto
            Dim objAccionDatoBancario As New LogicaNegocio.AccionDatoBancario
            Return objAccionDatoBancario.GetDatosBancarios(Peticion)

        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDatoBancario.Test
            ' criar objeto
            Dim objAccionDatoBancario As New LogicaNegocio.AccionDatoBancario
            Return objAccionDatoBancario.Test()

        End Function
    End Class

End Namespace