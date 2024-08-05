Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Cliente
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.ICliente

        <WebMethod()> _
        Public Function GetClientes(objPeticion As ContractoServicio.Cliente.GetClientes.Peticion) As ContractoServicio.Cliente.GetClientes.Respuesta Implements ContractoServicio.ICliente.GetClientes
            Return Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientes(objPeticion)
        End Function

        <WebMethod()> _
        Public Function GetClientesDetalle(objPeticion As ContractoServicio.Cliente.GetClientesDetalle.Peticion) As ContractoServicio.Cliente.GetClientesDetalle.Respuesta Implements ContractoServicio.ICliente.GetClientesDetalle
            Return Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientesDetalle(objPeticion)
        End Function

        <WebMethod()> _
        Public Function SetClientes(objPeticion As ContractoServicio.Cliente.SetClientes.Peticion) As ContractoServicio.Cliente.SetClientes.Respuesta Implements ContractoServicio.ICliente.SetClientes
            Return Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.grabarClientes(objPeticion)
        End Function

        <WebMethod()> _
        Public Function GetClienteByCodigoAjeno(objPeticion As ContractoServicio.Cliente.GetClienteByCodigoAjeno.Peticion) As ContractoServicio.Cliente.GetClienteByCodigoAjeno.Respuesta Implements ContractoServicio.ICliente.GetClienteByCodigoAjeno
            Return Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.GetClienteByCodigoAjeno(objPeticion)
        End Function

        <WebMethod()> _
        Public Function Test() As Prosegur.Genesis.ContractoServicio.Test.Respuesta Implements ContractoServicio.ICliente.Test
            Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar()
        End Function

    End Class

End Namespace