﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.1433
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports Prosegur.Global.Seguridad.ContractoServicio
Imports Microsoft.Web.Services3.Security.Tokens

Namespace ServicioATM

    '
    'This source code was auto-generated by wsdl, Version=2.0.50727.42.
    '

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42"), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ATMSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.ATM")> _
    Partial Public Class ServicioATM
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol

        Public Sub New()

            MyBase.New()

            Me.Url = Parametros.Configuracion.UrlServicioATM

        End Sub

        <System.Web.Services.Protocols.SoapHeaderAttribute("SesionInfoValue"), _
     System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.ATM/VerificarMorfologia", RequestNamespace:="http://Prosegur.Global.GesEfectivo.ATM", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.ATM", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
     Public Function VerificarMorfologia(ByVal Peticion As ATM.ContractoServicio.VerificarMorfologia.Peticion) As ATM.ContractoServicio.VerificarMorfologia.Respuesta

            Dim results() As Object = Me.Invoke("VerificarMorfologia", New Object() {Peticion})
            Return CType(results(0), ATM.ContractoServicio.VerificarMorfologia.Respuesta)

        End Function

    End Class

End Namespace