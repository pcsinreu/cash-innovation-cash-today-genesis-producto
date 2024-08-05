Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas
Imports Prosegur.Genesis.ContractoServicio.Interfaces

Namespace NuevoSalidasIntegracion

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.NuevoSalidasIntegracion/")> _
    <WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Integracion
        Inherits System.Web.Services.WebService
        Implements INuevoSalidasIntegracion

        <WebMethod()> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements INuevoSalidasIntegracion.Test
            Dim objAccion As New LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar()
        End Function


        <WebMethod()> _
        Public Function RecuperarRemesasPorIdentificadorCodigoExternos(Peticion As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Peticion) As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta Implements INuevoSalidasIntegracion.RecuperarRemesasPorIdentificadorCodigoExternos
            Return Genesis.LogicaNegocio.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos(Peticion)
        End Function

    End Class

End Namespace