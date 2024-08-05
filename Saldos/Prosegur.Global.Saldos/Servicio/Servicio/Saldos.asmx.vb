Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

<System.Web.Services.WebService(Namespace:="http://Prosegur.Global.Saldos.Servicio/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Saldos
    Inherits System.Web.Services.WebService
    Implements ContractoServicio.ISaldos

    <WebMethod()> _
    Public Function RecuperarCaracteristicaDocumento(ByVal Peticion As ContractoServicio.RecuperarCaracteristicasDocumento.Peticion) As ContractoServicio.RecuperarCaracteristicasDocumento.Respuesta Implements ContractoServicio.ISaldos.RecuperarCaracteristicaDocumento
        Dim objNegocio As New LogicaNegocioServicio.AccionRecuperarCaracteristicaDocumento
        Return objNegocio.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarDatosDocumento(ByVal Peticion As ContractoServicio.RecuperarDatosDocumento.Peticion) As ContractoServicio.RecuperarDatosDocumento.Respuesta Implements ContractoServicio.ISaldos.RecuperarDatosDocumento
        Dim objNegocio As New LogicaNegocioServicio.AccionRecuperarDatosDocumento
        Return objNegocio.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Function RecuperarSaldosPorSector(ByVal Peticion As ContractoServicio.RecuperarSaldosPorSector.Peticion) As ContractoServicio.RecuperarSaldosPorSector.Respuesta Implements ContractoServicio.ISaldos.RecuperarSaldosPorSector
        Dim objNegocio As New LogicaNegocioServicio.AccionRecuperarSaldosPorSector
        Return objNegocio.Ejecutar(Peticion)
    End Function

    <WebMethod()> _
    Function AccionGuardarDatosDocumento(ByVal Peticion As ContractoServicio.GuardarDatosDocumento.Peticion) As ContractoServicio.GuardarDatosDocumento.Respuesta Implements ContractoServicio.ISaldos.AccionGuardarDatosDocmento
        Dim objNegocio As New LogicaNegocioServicio.AccionGuardarDatosDocumento
        Return objNegocio.Ejecutar(Peticion)
    End Function

End Class