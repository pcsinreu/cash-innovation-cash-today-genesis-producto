Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio.Dinamico
Imports Prosegur.Genesis.LogicaNegocio.Genesis

Namespace Dinamico

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Genesis.Servicio.Dinamico")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Consultar
        Inherits System.Web.Services.WebService
        Implements IConsultar

        <WebMethod()> _
        Public Function Consultar(Peticion As Peticion) As Respuesta Implements IConsultar.Consultar
            Dim objAccion As New DataSetReportes()
            Return objAccion.Consultar(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.Dinamico.IConsultar.Test
            Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar()
        End Function
    End Class

End Namespace
