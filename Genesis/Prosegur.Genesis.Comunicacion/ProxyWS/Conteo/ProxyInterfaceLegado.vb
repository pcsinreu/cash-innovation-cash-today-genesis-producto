Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContractoConteo = Prosegur.Global.GesEfectivo.Conteo.ContractoServicio

Namespace ProxyWS.Conteo

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.Diagnostics.DebuggerStepThroughAttribute(), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="ConteoSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Conteo")> _
    Public Class ProxyInterfaceLegado
        Inherits ProxyWS.ServicioBase
        Implements ContractoConteo.IInterfaceLegado

        Private sesionInfoValueField As New ContractoConteo.ServicoBase.SesionInfo()

        '''<remarks/>
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(urlBase As String)
            MyBase.New()

            If String.IsNullOrEmpty(urlBase) Then
                Me.Url = UrlServicio() & "Conteo/InterfaceLegado.asmx"
            Else
                Me.Url = urlBase & "Conteo/InterfaceLegado.asmx"
            End If

        End Sub

        Public Property SesionInfoValue() As ContractoConteo.ServicoBase.SesionInfo
            Get
                Return Me.sesionInfoValueField
            End Get
            Set(ByVal value As ContractoConteo.ServicoBase.SesionInfo)
                Me.sesionInfoValueField = value
            End Set
        End Property

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/ActualizarDatosRemesaBCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ActualizarDatosRemesaBCP(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.ActualizarDatosRemesaBCP.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.ActualizarDatosRemesaBCP.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.ActualizarDatosRemesaBCP
            Dim results() As Object = Me.Invoke("ActualizarDatosRemesaBCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.ActualizarDatosRemesaBCP.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GrabarTransaccionesBCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GrabarTransaccionesBCP(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GrabarTransaccionesBCP.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GrabarTransaccionesBCP.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.GrabarTransaccionesBCP
            Dim results() As Object = Me.Invoke("GrabarTransaccionesBCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GrabarTransaccionesBCP.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/GuardarParcialCP", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function GuardarParcialCP(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarParcialCP.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.GuardarParcialCP.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.GuardarParcialCP
            Dim results() As Object = Me.Invoke("GuardarParcialCP", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.GuardarParcialCP.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/LeeEscribeEstadoRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function LeeEscribeEstadoRemesas(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.LeeEscribeEstadoRemesa.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.LeeEscribeEstadoRemesa.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.LeeEscribeEstadoRemesas
            Dim results() As Object = Me.Invoke("LeeEscribeEstadoRemesas", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.LeeEscribeEstadoRemesa.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecibirDatosAdicionalesDeclarados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirDatosAdicionalesDeclarados(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirDatosAdicionalesDeclarados.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirDatosAdicionalesDeclarados.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.RecibirDatosAdicionalesDeclarados
            Dim results() As Object = Me.Invoke("RecibirDatosAdicionalesDeclarados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecibirDatosAdicionalesDeclarados.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecibirRemesas", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesas(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesas.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesas.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.RecibirRemesas
            Dim results() As Object = Me.Invoke("RecibirRemesas", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecibirRemesas.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecibirRemesasPendientes", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecibirRemesasPendientes(peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesasPendientes.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecibirRemesasPendientes.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.RecibirRemesasPendientes
            Dim results() As Object = Me.Invoke("RecibirRemesasPendientes", New Object() {peticion})
            Return CType(results(0), ContractoConteo.RecibirRemesasPendientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/RecuperarValoresContados", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarValoresContados(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.RecuperarValoresContados.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.RecuperarValoresContados.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.RecuperarValoresContados
            Dim results() As Object = Me.Invoke("RecuperarValoresContados", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.RecuperarValoresContados.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/SincronizarDivisasConteo", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function SincronizarDivisasConteo(Peticion As [Global].GesEfectivo.Conteo.ContractoServicio.SincronizarDivisasConteo.Peticion) As [Global].GesEfectivo.Conteo.ContractoServicio.SincronizarDivisasConteo.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.SincronizarDivisasConteo
            Dim results() As Object = Me.Invoke("SincronizarDivisasConteo", New Object() {Peticion})
            Return CType(results(0), ContractoConteo.SincronizarDivisasConteo.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
             System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Conteo/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Conteo", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As [Global].GesEfectivo.Conteo.ContractoServicio.Test.Respuesta Implements [Global].GesEfectivo.Conteo.ContractoServicio.IInterfaceLegado.Test
            Dim results() As Object = Me.Invoke("Test", New Object())
            Return CType(results(0), ContractoConteo.Test.Respuesta)
        End Function
    End Class

End Namespace