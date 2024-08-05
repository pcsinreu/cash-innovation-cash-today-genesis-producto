Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContratoIac = Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Configuration

<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="IACSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.IAC")> _
Public Class ProxySector
    Inherits ProxyWS.ServicioBase
    Implements Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ISetor

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "IAC/Sector.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getSectores(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Setor.GetSectores.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Setor.GetSectores.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.getSectores
        Dim results() As Object = Me.Invoke("getSectores", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Setor.GetSectores.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSectoresIAC", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSectoresIAC(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresIAC.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresIAC.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.GetSectoresIAC
        Dim results() As Object = Me.Invoke("GetSectoresIAC", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresIAC.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/setSectores", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function setSectores(ObjPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Setor.SetSectores.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Setor.SetSectores.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.setSectores
        Dim results() As Object = Me.Invoke("setSectores", New Object() {ObjPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Setor.SetSectores.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/getSetorDetail", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function getSetorDetail(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Setor.GetSectoresDetail.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Setor.GetSectoresDetail.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.getSetorDetail
        Dim results() As Object = Me.Invoke("getSetorDetail", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Setor.GetSectoresDetail.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/GetSectoresTesoro", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetSectoresTesoro(objPeticion As [Global].GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresTesoro.Peticion) As [Global].GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresTesoro.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.GetSectoresTesoro
        Dim results() As Object = Me.Invoke("GetSectoresTesoro", New Object() {objPeticion})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Sector.GetSectoresTesoro.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.IAC/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.IAC", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.IAC", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As [Global].GesEfectivo.IAC.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.IAC.ContractoServicio.ISetor.Test
        Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
        Return CType(results(0), Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Test.Respuesta)
    End Function

End Class
