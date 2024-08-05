Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class SubCliente
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ISubCliente

        <WebMethod()> _
        Public Function GetSubClientes(objPeticion As ContractoServicio.SubCliente.GetSubClientes.Peticion) As ContractoServicio.SubCliente.GetSubClientes.Respuesta Implements ContractoServicio.ISubCliente.GetSubClientes
            Return Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.ObtenerSubClientes(objPeticion)
        End Function

        <WebMethod()> _
        Public Function GetSubClientesDetalle(objPeticion As ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion) As ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta Implements ContractoServicio.ISubCliente.GetSubClientesDetalle
            Return Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.ObtenerSubClientesDetalle(objPeticion)
        End Function

        <WebMethod()> _
        Public Function SetSubClientes(objPeticion As ContractoServicio.SubCliente.SetSubClientes.Peticion) As ContractoServicio.SubCliente.SetSubClientes.Respuesta Implements ContractoServicio.ISubCliente.SetSubClientes
            Return Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.grabarSubClientes(objPeticion)
        End Function

        <WebMethod()> _
        Public Function GetSubclienteByCodigoAjeno(objPeticion As ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Peticion) As ContractoServicio.SubCliente.GetSubclienteByCodigoAjeno.Respuesta Implements ContractoServicio.ISubCliente.GetSubclienteByCodigoAjeno
            Return Prosegur.Genesis.LogicaNegocio.Genesis.SubCliente.GetSubclienteByCodigoAjeno(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As Prosegur.Genesis.ContractoServicio.Test.Respuesta Implements ContractoServicio.ISubCliente.Test
            ' criar objeto
            Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar()
        End Function

    End Class

End Namespace