Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas

Namespace ProxyWS.GenesisMovil

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="GenesisMovilSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.GenesisMovil/")> _
    Public Class ProxyGenesisMovil
        Inherits ProxyWS.ServicioBase

        Public Sub New()
            MyBase.New()

            ' Set UrlServicio
            If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
                Me.Url = UrlServicio & "GenesisMovil/Servicio.asmx"
            End If

            ' Set TimeOut Service
            If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
                MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
            End If

        End Sub

        '<System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisMovil/Teste", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        'Public Function Teste(Peticion As String) As String Implements IGenesisMovil.Teste
        '    Dim results() As Object = Me.Invoke("Teste", New Object() {Peticion})
        '    Return ""
        'End Function

        '<System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisMovil/EjecutarLogin", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        'Public Function EjecutarLogin(Peticion As Genesis.ContractoServicio.Login.EjecutarLogin.Peticion) As Genesis.ContractoServicio.Login.EjecutarLogin.Respuesta Implements IGenesisMovil.EjecutarLogin
        '    Dim results() As Object = Me.Invoke("EjecutarLogin", New Object() {Peticion})
        '    Return CType(results(0), Genesis.ContractoServicio.Login.EjecutarLogin.Respuesta)
        'End Function

        '<System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.GenesisMovil/ObtenerSectoresTesoro", RequestNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.GenesisMovil/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        'Public Function ObtenerSectoresTesoro(Peticion As Genesis.ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresTesoro.Peticion) As Genesis.ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresTesoro.Respuesta Implements IGenesisMovil.ObtenerSectoresTesoro
        '    Dim results() As Object = Me.Invoke("ObtenerSectoresTesoro", New Object() {Peticion})
        '    Return CType(results(0), Genesis.ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresTesoro.Respuesta)
        'End Function

    End Class
End Namespace
