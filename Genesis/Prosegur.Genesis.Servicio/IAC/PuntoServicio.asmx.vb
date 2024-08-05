Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PuntoServicio
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.IPuntoServicio

    <WebMethod()> _
    Public Function GetPuntoServicio(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicio.Respuesta Implements ContractoServicio.IPuntoServicio.GetPuntoServicio
        Return Prosegur.Genesis.LogicaNegocio.Genesis.PuntoServicio.ObtenerPuntoServicio(objPeticion)

    End Function

    <WebMethod()> _
    Public Function GetPuntoServicioDetalle(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicioDetalle.Respuesta Implements ContractoServicio.IPuntoServicio.GetPuntoServicioDetalle
        Return Genesis.LogicaNegocio.Genesis.PuntoServicio.ObternerPuntoServicioDetalle(objPeticion)
    End Function

    <WebMethod()> _
    Public Function SetPuntoServicio(objPeticion As ContractoServicio.PuntoServicio.SetPuntoServicio.Peticion) As ContractoServicio.PuntoServicio.SetPuntoServicio.Respuesta Implements ContractoServicio.IPuntoServicio.SetPuntoServicio
        Return Genesis.LogicaNegocio.Genesis.PuntoServicio.grabarPuntoServicio(objPeticion)
    End Function

    <WebMethod()> _
    Public Function GetPuntoServicioByCodigoAjeno(objPeticion As ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Peticion) As ContractoServicio.PuntoServicio.GetPuntoServicioByCodigoAjeno.Respuesta Implements ContractoServicio.IPuntoServicio.GetPuntoServicioByCodigoAjeno
        Return Prosegur.Genesis.LogicaNegocio.Genesis.PuntoServicio.GetPuntoServicioByCodigoAjeno(objPeticion)
    End Function

    <WebMethod()> _
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IPuntoServicio.Test
        ' criar objeto
        Dim objAccionPuntoServicio As New LogicaNegocio.AccionSetor
        Return objAccionPuntoServicio.Test()
    End Function

End Class