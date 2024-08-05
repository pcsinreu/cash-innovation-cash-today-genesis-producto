Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Direccion
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IDireccion

        'Serviço que faz a busca no Direcciones existentes
        <WebMethod()> _
        Public Function GetDirecciones(Peticion As ContractoServicio.Direccion.GetDirecciones.Peticion) As ContractoServicio.Direccion.GetDirecciones.Respuesta Implements ContractoServicio.IDireccion.GetDirecciones
            'Cria objeto da Accion
            Dim objAccion As New LogicaNegocio.AccionDireccion
            Return objAccion.GetDirecciones(Peticion)
        End Function

        'Serviço que insere uma Direccion no banco de dados
        <WebMethod()> _
        Public Function SetDirecciones(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion) As ContractoServicio.Direccion.SetDirecciones.Respuesta Implements ContractoServicio.IDireccion.SetDirecciones
            'Cria objeto da Accion
            Dim objAccion As New LogicaNegocio.AccionDireccion
            Return objAccion.SetDirecciones(Peticion)
        End Function

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDireccion.Test
            Dim objAccion As New LogicaNegocio.AccionDireccion
            Return objAccion.Test()
        End Function

    End Class

End Namespace
