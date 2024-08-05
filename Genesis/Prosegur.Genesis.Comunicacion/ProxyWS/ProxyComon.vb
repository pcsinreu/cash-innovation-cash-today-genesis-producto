Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Configuration
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces


<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433"), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Web.Services.WebServiceBindingAttribute(Name:="ComonSoap", [Namespace]:="http://Prosegur.Genesis.Servicio")> _
Public Class ProxyComon
    Inherits ProxyWS.ServicioBase
    Implements IComon

    Public Sub New()
        MyBase.New()

        ' Set UrlServicio
        If UrlServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(UrlServicio) Then
            Me.Url = UrlServicio & "Comon.asmx"
        End If

        ' Set TimeOut Service
        If Not String.IsNullOrEmpty(Ws_TimeOut) AndAlso IsNumeric(Ws_TimeOut) Then
            MyBase.Timeout = Integer.Parse(Ws_TimeOut) * 1000
        End If

    End Sub

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/Test", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function Test() As Test.Respuesta Implements IComon.Test
        Dim results() As Object = Me.Invoke("Test", New Object())
        Return CType(results(0), Test.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/GetNumeroDeSerieBillete", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta Implements IComon.GetNumeroDeSerieBillete
        Dim results() As Object = Me.Invoke("GetNumeroDeSerieBillete", New Object() {Peticion})
        Return CType(results(0), NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta)

    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/SetNumeroDeSerieBillete", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function SetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta Implements IComon.SetNumeroDeSerieBillete
        Dim results() As Object = Me.Invoke("SetNumeroDeSerieBillete", New Object() {Peticion})
        Return CType(results(0), NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerEmisoresDocumento", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerEmisoresDocumento(Peticion As EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoPeticion) As EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta Implements IComon.ObtenerEmisoresDocumento
        Dim results() As Object = Me.Invoke("ObtenerEmisoresDocumento", New Object() {Peticion})
        Return CType(results(0), EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerTipoServicios", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerTipoServicios() As TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta Implements IComon.ObtenerTipoServicios
        Dim results() As Object = Me.Invoke("ObtenerTipoServicios", New Object() {})
        Return CType(results(0), TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerTiposFormato", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerTiposFormato() As TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta Implements IComon.ObtenerTiposFormato
        Dim results() As Object = Me.Invoke("ObtenerTiposFormato", New Object() {})
        Return CType(results(0), TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarPorTipoComModulo", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarPorTipoComModulo(peticion As TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloPeticion) As TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta Implements IComon.RecuperarPorTipoComModulo
        Dim results() As Object = Me.Invoke("RecuperarPorTipoComModulo", New Object() {peticion})
        Return CType(results(0), TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerCalidades", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerCalidades() As Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta Implements IComon.ObtenerCalidades
        Dim results() As Object = Me.Invoke("ObtenerCalidades", New Object() {})
        Return CType(results(0), Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDivisas", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerDivisas() As Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta Implements ContractoServicio.Interfaces.IComon.ObtenerDivisas
        Dim results() As Object = Me.Invoke("ObtenerDivisas", New Object() {})
        Return CType(results(0), Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerSectoresPorCaracteristicas", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerSectoresPorCaracteristicas(Peticion As ContractoServicio.Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta Implements ContractoServicio.Interfaces.IComon.ObtenerSectoresPorCaracteristicas
        Dim results() As Object = Me.Invoke("ObtenerSectoresPorCaracteristicas", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerSectoresPorSectorPadre", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadrePeticion) As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta Implements IComon.ObtenerSectoresPorSectorPadre
        Dim results() As Object = Me.Invoke("ObtenerSectoresPorSectorPadre", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDenominaciones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerDenominaciones(Peticion As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesPeticion) As ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta Implements ContractoServicio.Interfaces.IComon.ObtenerDenominaciones
        Dim results() As Object = Me.Invoke("ObtenerDenominaciones", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/Busqueda", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Function Busqueda(Peticion As Helper.Busqueda.HelperBusquedaPeticion) As Helper.Busqueda.HelperBusquedaRespuesta Implements IComon.Busqueda
        Dim results() As Object = Me.Invoke("Busqueda", New Object() {Peticion})
        Return CType(results(0), Helper.Busqueda.HelperBusquedaRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/GuardarConfigGrid", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GuardarConfigGrid(Peticion As Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Peticion) As Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta Implements IComon.GuardarConfigGrid
        Dim results() As Object = Me.Invoke("GuardarConfigGrid", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarConfigGrid", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarConfigGrid(Peticion As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Peticion) As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta Implements IComon.RecuperarConfigGrid
        Dim results() As Object = Me.Invoke("RecuperarConfigGrid", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerPuestos", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerPuestos(Peticion As Contractos.Genesis.Puesto.ObtenerPuestos.Peticion) As Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta Implements IComon.ObtenerPuestos
        Dim results() As Object = Me.Invoke("ObtenerPuestos", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerTiposImpresora", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerTiposImpresora() As TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta Implements IComon.ObtenerTiposImpresoras
        Dim results() As Object = Me.Invoke("ObtenerTiposImpresora", New Object() {})
        Return CType(results(0), TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerPaisPorDelegacion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerPaisPorDelegacion(Peticion As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionPeticion) As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta Implements IComon.ObtenerPaisPorDelegacion
        Dim results() As Object = Me.Invoke("ObtenerPaisPorDelegacion", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerSectores", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta Implements IComon.ObtenerSectores
        Dim results() As Object = Me.Invoke("ObtenerSectores", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Comon.Sector.ObtenerSectoresRespuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarSectores", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta Implements IComon.RecuperarSectores
        Dim results() As Object = Me.Invoke("RecuperarSectores", New Object() {Peticion})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Comon.Sector.ObtenerSectoresRespuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerUnidadesMedida", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerUnidadesMedida() As Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta Implements IComon.ObtenerUnidadesMedida
        Dim results() As Object = Me.Invoke("ObtenerUnidadesMedida", New Object() {})
        Return Util.TratarRetornoServico(CType(results(0), Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta))
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDelegacionesDelUsuario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerDelegacionesDelUsuario(Peticion As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta Implements IComon.ObtenerDelegacionesDelUsuario
        Dim results() As Object = Me.Invoke("ObtenerDelegacionesDelUsuario", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarSectoresSalidas", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarSectoresSalidas(Peticion As Contractos.Comon.Sector.RecuperarSectoresSalidasPeticion) As Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta Implements IComon.RecuperarSectoresSalidas
        Dim results() As Object = Me.Invoke("RecuperarSectoresSalidas", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerSectoresPorCaracteristicasSimultaneas", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerSectoresPorCaracteristicasSimultaneas(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasRespuesta Implements IComon.ObtenerSectoresPorCaracteristicasSimultaneas
        Dim results() As Object = Me.Invoke("ObtenerSectoresPorCaracteristicasSimultaneas", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarTerminosPorCodigos", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTerminosPorCodigos(Peticion As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta Implements IComon.RecuperarTerminosPorCodigos
        Dim results() As Object = Me.Invoke("RecuperarTerminosPorCodigos", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/VerificarPuestoPorSectorPadre", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function VerificarPuestoPorSectorPadre(Peticion As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Peticion) As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Respuesta Implements IComon.VerificarPuestoPorSectorPadre
        Dim results() As Object = Me.Invoke("VerificarPuestoPorSectorPadre", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerCodigosSectoresPorSectorPadre", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerCodigosSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Peticion) As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Respuesta Implements IComon.ObtenerCodigosSectoresPorSectorPadre
        Dim results() As Object = Me.Invoke("ObtenerCodigosSectoresPorSectorPadre", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarTotalizadoresSaldos", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTotalizadoresSaldos(peticion As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Peticion) As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta Implements IComon.RecuperarTotalizadoresSaldos
        Dim results() As Object = Me.Invoke("RecuperarTotalizadoresSaldos", New Object() {peticion})
        Return CType(results(0), Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarTerminosIAC", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTerminosIAC(peticion As Contractos.Comon.Terminos.RecuperarTerminosIAC.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta Implements IComon.RecuperarTerminosIAC
        Dim results() As Object = Me.Invoke("RecuperarTerminosIAC", New Object() {peticion})
        Return CType(results(0), Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarClienteTotalizadorSaldo", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarClienteTotalizadorSaldo(Peticion As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Peticion) As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta Implements IComon.RecuperarClienteTotalizadorSaldo
        Dim results() As Object = Me.Invoke("RecuperarClienteTotalizadorSaldo", New Object() {Peticion})
        Return CType(results(0), Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/BusquedaTipoContenedor", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Function BusquedaTipoContenedor(Peticion As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorPeticion) As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta Implements IComon.BusquedaTipoContenedor
        Dim results() As Object = Me.Invoke("BusquedaTipoContenedor", New Object() {Peticion})
        Return CType(results(0), Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecuperarTotalizadoresSaldosPorCodigo", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecuperarTotalizadoresSaldosPorCodigo(peticion As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Peticion) As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta Implements IComon.RecuperarTotalizadoresSaldosPorCodigo
        Dim results() As Object = Me.Invoke("RecuperarTotalizadoresSaldosPorCodigo", New Object() {peticion})
        Return CType(results(0), Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/CargarNotificaciones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function CargarNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta Implements IComon.CargarNotificaciones
        Dim results() As Object = Me.Invoke("CargarNotificaciones", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/GrabarNotificacionLeido", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GrabarNotificacionLeido(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta Implements IComon.GrabarNotificacionLeido
        Dim results() As Object = Me.Invoke("GrabarNotificacionLeido", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/GrabarNotificacion", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function GrabarNotificacion(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta Implements IComon.GrabarNotificacion
        Dim results() As Object = Me.Invoke("GrabarNotificacion", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/RecibirMensajeExterno", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function RecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta Implements IComon.RecibirMensajeExterno
        Dim results() As Object = Me.Invoke("RecibirMensajeExterno", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerCantidadNotificaciones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerCantidadNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta Implements IComon.ObtenerCantidadNotificaciones
        Dim results() As Object = Me.Invoke("ObtenerCantidadNotificaciones", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDelegacionGMT", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerDelegacionGMT(Peticion As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Peticion) As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta Implements IComon.ObtenerDelegacionGMT
        Dim results() As Object = Me.Invoke("ObtenerDelegacionGMT", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerDelegaciones", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerDelegaciones(Peticion As Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta Implements IComon.ObtenerDelegaciones
        Dim results() As Object = Me.Invoke("ObtenerDelegaciones", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerValorDiccionario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerValorDiccionario(Peticion As Contractos.Comon.Diccionario.ObtenerValorDiccionario.Peticion) As Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta Implements IComon.ObtenerValorDiccionario
        Dim results() As Object = Me.Invoke("ObtenerValorDiccionario", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta)
    End Function

    <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://Prosegur.Genesis.Servicio/ObtenerValoresDiccionario", RequestNamespace:="http://Prosegur.Genesis.Servicio", ResponseNamespace:="http://Prosegur.Genesis.Servicio", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)> _
    Public Function ObtenerValoresDiccionario(Peticion As Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion) As Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta Implements IComon.ObtenerValoresDiccionario
        Dim results() As Object = Me.Invoke("ObtenerValoresDiccionario", New Object() {Peticion})
        Return CType(results(0), ContractoServicio.Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta)
    End Function
End Class