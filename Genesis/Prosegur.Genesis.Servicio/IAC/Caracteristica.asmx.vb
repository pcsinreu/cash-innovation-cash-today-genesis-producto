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
    Public Class Caracteristica
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ICaracteristica

        <WebMethod()> _
        Public Function GetCaracteristica(objPeticion As ContractoServicio.Caracteristica.GetCaracteristica.Peticion) As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta Implements ContractoServicio.ICaracteristica.GetCaracteristica
            Dim objAccionCaracteristica As New LogicaNegocio.AccionCaracteristica()
            Return objAccionCaracteristica.GetCaracteristica(objPeticion)
        End Function

        <WebMethod()> _
        Public Function SetCaracteristica(objPeticion As ContractoServicio.Caracteristica.SetCaracteristica.Peticion) As ContractoServicio.Caracteristica.SetCaracteristica.Respuesta Implements ContractoServicio.ICaracteristica.SetCaracteristica
            Dim objAccionCaracteristica As New LogicaNegocio.AccionCaracteristica()
            Return objAccionCaracteristica.SetCaracteristica(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICaracteristica.Test
            Dim objAccionCaracteristica As New LogicaNegocio.AccionCaracteristica()
            Return objAccionCaracteristica.Test()
        End Function
    End Class

End Namespace