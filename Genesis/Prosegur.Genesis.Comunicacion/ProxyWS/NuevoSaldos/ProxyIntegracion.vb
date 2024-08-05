Imports Prosegur.Global
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarMAEs

Namespace ProxyWS.Integracion

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"),
     System.ComponentModel.DesignerCategoryAttribute("code"),
     System.Web.Services.WebServiceBindingAttribute(Name:="IngresoContadoSoap", [Namespace]:="Integracion")>
    Partial Public Class ProxyIntegracion
        Inherits ProxyWS.ServicioBase
        Implements ContractoServicio.Interfaces.IIntegracionSaldos

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "GenesisSaldos/Integracion.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub



        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("Integracion/crearDocumentoReenvio", RequestNamespace:="Integracion", ResponseNamespace:="Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function crearDocumentoReenvio(peticion As Contractos.Integracion.crearDocumentoReenvio.Peticion) As Contractos.Integracion.crearDocumentoReenvio.Respuesta Implements Interfaces.IIntegracionSaldos.crearDocumentoReenvio
            Dim results() As Object = Me.Invoke("crearDocumentoReenvio", New Object() {peticion})
            Return CType(results(0), Contractos.Integracion.crearDocumentoReenvio.Respuesta)
        End Function


        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("Integracion/generarCertificado", RequestNamespace:="Integracion", ResponseNamespace:="Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function generarCertificado(peticion As Contractos.Integracion.generarCertificado.Peticion) As Contractos.Integracion.generarCertificado.Respuesta Implements Interfaces.IIntegracionSaldos.generarCertificado
            Dim results() As Object = Me.Invoke("generarCertificado", New Object() {peticion})
            Return CType(results(0), Contractos.Integracion.generarCertificado.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("Integracion/Test", RequestNamespace:="Integracion", ResponseNamespace:="Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function Test() As Test.Respuesta Implements Interfaces.IIntegracionSaldos.Test
            Dim results = Me.Invoke("Test", New Object() {})
            Return CType(results(0), Test.Respuesta)
        End Function

        Function GetMorfologiaDetail(objPeticion As GesEfectivo.IAC.Integracion.ContractoServicio.GetMorfologiaDetail.Peticion) As GesEfectivo.IAC.Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta
            Throw New NotImplementedException
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("Integracion/GrabarReciboTransporteManual", RequestNamespace:="Integracion", ResponseNamespace:="Integracion", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>
        Public Function GrabarReciboTransporteManual(peticion As Contractos.Integracion.GrabarReciboTransporteManual.Peticion) As Contractos.Integracion.GrabarReciboTransporteManual.Respuesta Implements IIntegracionSaldos.GrabarReciboTransporteManual
            Dim results() As Object = Me.Invoke("GrabarReciboTransporteManual", New Object() {peticion})
            Return Util.TratarRetornoServico(CType(results(0), Contractos.Integracion.GrabarReciboTransporteManual.Respuesta))
        End Function

    End Class

End Namespace