Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio.Dinamico

Namespace ListadosConteo

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
             System.Diagnostics.DebuggerStepThroughAttribute(), _
             System.ComponentModel.DesignerCategoryAttribute("code"), _
             System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Genesis.Servicio.Dinamico")> _
    Public Class ProxyDinamico
        Inherits ProxyWS.ServicioBase
        Implements IConsultar

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "Dinamico/Consultar.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio.Dinamico/Consultar", RequestNamespace:="http://Prosegur.Genesis.Servicio.Dinamico", ResponseNamespace:="http://Prosegur.Genesis.Servicio.Dinamico", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Consultar(Peticion As Peticion) As Respuesta Implements IConsultar.Consultar
            Dim results() As Object = Me.Invoke("Consultar", New Object() {Peticion})
            Return CType(results(0), Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio.Dinamico/Test", RequestNamespace:="http://Prosegur.Genesis.Servicio.Dinamico", ResponseNamespace:="http://Prosegur.Genesis.Servicio.Dinamico", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoServicio.Test.Respuesta Implements IConsultar.Test
            Dim results() As Object = Me.Invoke("Test", New Object() {-1})
            Return CType(results(0), ContractoServicio.Test.Respuesta)
        End Function

    End Class

End Namespace