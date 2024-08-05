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
    Public Class ValorPosible
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IValorPosible

        <WebMethod()> _
        Public Function GetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.GetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.GetValoresPosibles.Respuesta Implements ContractoServicio.IValorPosible.GetValoresPosibles
            Dim objAccionValorPosible As New LogicaNegocio.AccionValorPosible
            Return objAccionValorPosible.GetValoresPosibles(objPeticion)
        End Function

        <WebMethod()> _
        Public Function SetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.SetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.SetValoresPosibles.Respuesta Implements ContractoServicio.IValorPosible.SetValoresPosibles
            Dim objAccionValorPosible As New LogicaNegocio.AccionValorPosible
            Return objAccionValorPosible.SetValoresPosibles(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IValorPosible.Test
            Dim objAccionValorPosible As New LogicaNegocio.AccionValorPosible
            Return objAccionValorPosible.Test()
        End Function
    End Class

End Namespace