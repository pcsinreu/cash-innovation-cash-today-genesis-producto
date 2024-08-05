Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="DashboardProductividad", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad")> _
    Partial Public Class ProxyDashboardProductividad
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboardProductividad


        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "DashboardProductividad.asmx"
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad/RetornaValoresProcesados", _
            RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad", _
            ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaValoresProcesados(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta Implements Interfaces.Dashboard.IDashboardProductividad.RetornaValoresProcesados
            Dim results() As Object = Me.Invoke("RetornaValoresProcesados", New Object() {Peticion})
            Return CType(results(0), Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad/RetornaValoresProcesadosPorHora", _
            RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad", _
            ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.DashboardProductividad", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RetornaValoresProcesadosPorHora(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta Implements Interfaces.Dashboard.IDashboardProductividad.RetornaValoresProcesadosPorHora
            Dim results() As Object = Me.Invoke("RetornaValoresProcesadosPorHora", New Object() {Peticion})
            Return CType(results(0), Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta)
        End Function
    End Class

End Namespace