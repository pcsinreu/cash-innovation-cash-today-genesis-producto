Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.Conteo
Imports Prosegur.Global
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.ContractoServicio

Namespace ProxyWS.Dashboard

    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="Dashboard", [Namespace]:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard")> _
    Partial Public Class ProxyDashboard
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        Implements ContractoServicio.Interfaces.Dashboard.IDashboard

        Public Sub New(ByVal urlServicio As String)
            MyBase.New()
            Me.Url = urlServicio & "Dashboard.asmx"
        End Sub

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/ObtenerDivisas", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerDivisas() As Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta Implements Interfaces.Dashboard.IDashboard.ObtenerDivisas
            Dim results() As Object = Me.Invoke("ObtenerDivisas", New Object() {})
            Return CType(results(0), Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/RecuperarCodigoIsoDivisaDefecto", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function RecuperarCodigoIsoDivisaDefecto(Peticion As Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto.Peticion) As Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto.Respuesta Implements Interfaces.Dashboard.IDashboard.RecuperarCodigoIsoDivisaDefecto
            Dim results() As Object = Me.Invoke("RecuperarCodigoIsoDivisaDefecto", New Object() {Peticion})
            Return CType(results(0), Contractos.Dashboard.RecuperarCodigoIsoDivisaDefecto.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/ObtenerSectores", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta Implements Interfaces.Dashboard.IDashboard.ObtenerSectores
            Dim results() As Object = Me.Invoke("ObtenerSectores", New Object() {Peticion})
            Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresRespuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/ObtenerClientes", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerClientes(Peticion As Contractos.Dashboard.ObtenerClientes.Peticion) As Contractos.Dashboard.ObtenerClientes.Respuesta Implements Interfaces.Dashboard.IDashboard.ObtenerClientes
            Dim results() As Object = Me.Invoke("ObtenerClientes", New Object() {Peticion})
            Return CType(results(0), Contractos.Dashboard.ObtenerClientes.Respuesta)
        End Function

        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/ObtenerSectoresPorDelegacion", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        Public Function ObtenerSectoresPorDelegacion(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Peticion) As Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta Implements Interfaces.Dashboard.IDashboard.ObtenerSectoresPorDelegacion
            Dim results() As Object = Me.Invoke("ObtenerSectoresPorDelegacion", New Object() {Peticion})
            Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta)
        End Function

        '<System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Dashboard.Servicio.Dashboard/ObtenerSectoresMAEPorDelegacion", RequestNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", ResponseNamespace:="http://Prosegur.Genesis.Dashboard.Servicio.Dashboard", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
        'Public Function ObtenerSectoresMAEPorDelegacion(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Peticion) As Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta Implements Interfaces.Dashboard.IDashboard.ObtenerSectoresMAEPorDelegacion
        '    Dim results() As Object = Me.Invoke("ObtenerSectoresMAEPorDelegacion", New Object() {Peticion})
        '    Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresPorDelegacion.Respuesta)
        'End Function
    End Class
End Namespace