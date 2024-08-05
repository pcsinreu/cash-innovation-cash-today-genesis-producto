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
    Public Class Producto
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IProducto

        <WebMethod()> _
        Public Function GetProductos(peticion As ContractoServicio.Producto.GetProductos.Peticion) As ContractoServicio.Producto.GetProductos.Respuesta Implements ContractoServicio.IProducto.GetProductos
            Dim objProducto As New LogicaNegocio.AccionProducto
            Return objProducto.GetProductos(peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IProducto.Test
            Dim objProducto As New LogicaNegocio.AccionProducto
            Return objProducto.Test()
        End Function
    End Class

End Namespace