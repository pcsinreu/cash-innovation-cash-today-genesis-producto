Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports ContractoSalidas = Prosegur.Global.GesEfectivo.Salidas.ContractoServicio
Imports ContractoIntIac = Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Microsoft.Web.Services3.Security.Tokens

Namespace ProxyWS.Salidas

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
         System.ComponentModel.DesignerCategoryAttribute("code"), _
         System.Web.Services.WebServiceBindingAttribute(Name:="LoginSoap", [Namespace]:="http://Prosegur.Global.GesEfectivo.Login")> _
    Public Class ProxyLoginSalidas
        Inherits Microsoft.Web.Services3.WebServicesClientProtocol
        Implements ContractoSalidas.ILogin

        Private useDefaultCredentialsSetExplicitly As Boolean

        '''<remarks/>
        Public Sub New(ByVal usuario As String, ByVal contrasenha As String)
            MyBase.New()
            Dim token As New UsernameToken(usuario, contrasenha, PasswordOption.SendPlainText)
            Me.SetClientCredential(token)
            Me.SetPolicy("ClientPolicy")
            Me.useDefaultCredentialsSetExplicitly = True
        End Sub

        ''' <summary>
        ''' Login inicial da aplicação
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [cbomtempo] 25/08/2008 Criado
        ''' </history>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Login/Login", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Login", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Login", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Login(ByVal Peticion As ContractoSalidas.Login.Peticion) As ContractoSalidas.Login.Respuesta Implements ContractoSalidas.ILogin.Login
            Dim results() As Object = Me.Invoke("Login", New Object() {Peticion})
            Return CType(results(0), ContractoSalidas.Login.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Global.GesEfectivo.Login/Test", RequestNamespace:="http://Prosegur.Global.GesEfectivo.Login", ResponseNamespace:="http://Prosegur.Global.GesEfectivo.Login", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function Test() As ContractoSalidas.Test.Respuesta Implements ContractoSalidas.ILogin.Test
            Dim results() As Object = Me.Invoke("Test", New Object(-1) {})
            Return CType(results(0), ContractoSalidas.Test.Respuesta)
        End Function

    End Class

End Namespace