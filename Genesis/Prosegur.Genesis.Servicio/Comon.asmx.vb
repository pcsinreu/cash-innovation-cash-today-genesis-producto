Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Prosegur.Genesis.Servicio")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Comon
    Inherits ServicioBase
    Implements IComon

    <WebMethod()> _
    Public Function Test() As ContractoServicio.Test.Respuesta Implements IComon.Test
        Dim objAccion As New LogicaNegocio.Test.AccionTest
        Return objAccion.Ejecutar()
    End Function

    <WebMethod()> _
    Public Function GetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.GetNumeroDeSerieBillete.GetNumeroDeSerieBilleteRespuesta Implements Interfaces.IComon.GetNumeroDeSerieBillete
        Return LogicaNegocio.NumeroDeSerie.GetNumeroDeSerieBillete(Peticion)
    End Function

    <WebMethod()> _
    Public Function SetNumeroDeSerieBillete(Peticion As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilletePeticion) As NumeroSerieBillete.SetNumeroDeSerieBillete.SetNumeroDeSerieBilleteRespuesta Implements Interfaces.IComon.SetNumeroDeSerieBillete
        Return LogicaNegocio.NumeroDeSerie.SetNumeroDeSerieBillete(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerEmisoresDocumento(Peticion As ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoPeticion) As ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta Implements IComon.ObtenerEmisoresDocumento
        Return LogicaNegocio.EmisorDocumento.ObtenerEmisoresDocumento(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerDenominaciones(Peticion As Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesPeticion) As Contractos.Comon.Denominacion.ObtenerDenominaciones.ObtenerDenominacionesRespuesta Implements Interfaces.IComon.ObtenerDenominaciones
        Return LogicaNegocio.Denominacion.ObtenerDenominaciones(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerDivisas() As Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta Implements Interfaces.IComon.ObtenerDivisas
        Return LogicaNegocio.Divisa.ObtenerDivisas()
    End Function

    <WebMethod()> _
    Public Function ObtenerSectoresPorCaracteristicas(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicas.ObtenerSectoresPorCaracteristicasRespuesta Implements Interfaces.IComon.ObtenerSectoresPorCaracteristicas
        Return LogicaNegocio.Sector.ObtenerSectoresPorCaracteristicas(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadrePeticion) As Contractos.Comon.Sector.ObtenerSectoresPorSectorPadre.ObtenerSectoresPorSectorPadreRespuesta Implements IComon.ObtenerSectoresPorSectorPadre
        Return LogicaNegocio.Sector.ObtenerSectoresPorSectorPadre(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta Implements IComon.ObtenerSectores
        Return LogicaNegocio.Sector.ObtenerSectores(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarSectores(Peticion As Contractos.Comon.Sector.ObtenerSectoresPeticion) As Contractos.Comon.Sector.ObtenerSectoresRespuesta Implements IComon.RecuperarSectores
        Return LogicaNegocio.Sector.RecuperarSectores(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarSectoresSalidas(Peticion As Contractos.Comon.Sector.RecuperarSectoresSalidasPeticion) As Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta Implements IComon.RecuperarSectoresSalidas
        Return LogicaNegocio.Sector.RecuperarSectoresSalidas(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerTipoServicios() As ContractoServicio.TipoServicio.ObtenerTipoServicios.ObtenerTipoServiciosRespuesta Implements IComon.ObtenerTipoServicios
        Return LogicaNegocio.TipoServicio.ObtenerTipoServicios()
    End Function

    <WebMethod()> _
    Public Function ObtenerTiposFormato() As ContractoServicio.TipoFormato.ObtenerTiposFormato.ObtenerTiposFormatoRespuesta Implements IComon.ObtenerTiposFormato
        Return LogicaNegocio.TipoFormato.ObtenerTiposFormato()
    End Function

    <WebMethod()> _
    Public Function RecuperarPorTipoComModulo(peticion As ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloPeticion) As ContractoServicio.TipoFormato.RecuperarPorTipoComModulo.RecuperarPorTipoComModuloRespuesta Implements IComon.RecuperarPorTipoComModulo
        Return LogicaNegocio.TipoFormato.RecuperarPorTipoComModulo(peticion)
    End Function


    <WebMethod()> _
    Public Function ObtenerCalidades() As Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta Implements IComon.ObtenerCalidades
        Return LogicaNegocio.Calidad.ObtenerCalidades()
    End Function

    <WebMethod()> _
    Public Function ObtenerUnidadesMedida() As Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta Implements IComon.ObtenerUnidadesMedida
        Return LogicaNegocio.UnidadMedida.ObtenerUnidadesMedida()
    End Function

    <WebMethod()> _
    Function Busqueda(Peticion As ContractoServicio.Helper.Busqueda.HelperBusquedaPeticion) As ContractoServicio.Helper.Busqueda.HelperBusquedaRespuesta Implements IComon.Busqueda
        Return LogicaNegocio.Helper.Busqueda(Peticion)
    End Function

    <WebMethod()> _
    Function RecuperarConfigGrid(Peticion As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Peticion) As Contractos.Genesis.ConfigGrid.RecuperarConfigGrid.Respuesta Implements IComon.RecuperarConfigGrid
        Return LogicaNegocio.Genesis.ConfigGrid.RecuperarConfigGrid(Peticion)
    End Function

    <WebMethod()> _
    Function GuardarConfigGrid(Peticion As ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Peticion) As ContractoServicio.Contractos.Genesis.ConfigGrid.GuardarConfigGrid.Respuesta Implements IComon.GuardarConfigGrid
        Return LogicaNegocio.Genesis.ConfigGrid.GuardarConfigGrid(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerPuestos(Peticion As Contractos.Genesis.Puesto.ObtenerPuestos.Peticion) As Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta Implements IComon.ObtenerPuestos
        Return LogicaNegocio.Genesis.Puesto.ObtenerPuestos(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerTiposImpresora() As ContractoServicio.TipoImpresora.ObtenerTiposImpresora.ObtenerTiposImpresoraRespuesta Implements IComon.ObtenerTiposImpresoras
        Return LogicaNegocio.TipoImpresora.ObtenerTiposImpresora()
    End Function

    <WebMethod()> _
    Public Function ObtenerPaisPorDelegacion(Peticion As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionPeticion) As Contractos.Comon.Pais.ObtenerPaisPorDelegacion.ObtenerPaisPorDelegacionRespuesta Implements IComon.ObtenerPaisPorDelegacion
        Return LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Peticion)
    End Function

    <WebMethod()> _
    Function ObtenerDelegacionesDelUsuario(Peticion As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario.Respuesta Implements IComon.ObtenerDelegacionesDelUsuario
        Return LogicaNegocio.Genesis.Delegacion.ObtenerDelegacionesDelUsuario(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerSectoresPorCaracteristicasSimultaneas(Peticion As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasPeticion) As Contractos.Comon.Sector.ObtenerSectoresPorCaracteristicasSimultaneasRespuesta Implements IComon.ObtenerSectoresPorCaracteristicasSimultaneas
        Return LogicaNegocio.Sector.ObtenerSectoresPorCaracteristicasSimultaneas(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarTerminosPorCodigos(Peticion As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosPorCodigos.Respuesta Implements IComon.RecuperarTerminosPorCodigos
        Return LogicaNegocio.Genesis.TerminoIAC.ObtenerTerminosIACPorCodigo(Peticion)
    End Function

    <WebMethod()> _
    Public Function VerificarPuestoPorSectorPadre(Peticion As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Peticion) As Contractos.Comon.Sector.VerificarPuestoPorSectorPadre.Respuesta Implements IComon.VerificarPuestoPorSectorPadre
        Return LogicaNegocio.Sector.VerificarPuestoPorSectorPadre(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerCodigosSectoresPorSectorPadre(Peticion As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Peticion) As Contractos.Comon.Sector.ObtenerCodigosSectoresPorSectorPadre.Respuesta Implements IComon.ObtenerCodigosSectoresPorSectorPadre
        Return LogicaNegocio.Sector.ObtenerCodigosSectoresPorSectorPadre(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarTotalizadoresSaldos(peticion As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Peticion) As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Respuesta Implements IComon.RecuperarTotalizadoresSaldos
        Return LogicaNegocio.TotalizadorSaldo.RecuperarTotalizadoresSaldos(peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarTotalizadoresSaldosPorCodigo(peticion As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Peticion) As Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo.Respuesta Implements IComon.RecuperarTotalizadoresSaldosPorCodigo
        Return LogicaNegocio.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo(peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarTerminosIAC(peticion As Contractos.Comon.Terminos.RecuperarTerminosIAC.Peticion) As Contractos.Comon.Terminos.RecuperarTerminosIAC.Respuesta Implements IComon.RecuperarTerminosIAC
        Return LogicaNegocio.Genesis.TerminoIAC.RecuperarTerminosIAC(peticion)
    End Function

    <WebMethod()> _
    Public Function RecuperarClienteTotalizadorSaldo(Peticion As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Peticion) As Contractos.Comon.Cliente.RecuperarClienteTotalizadorSaldo.Respuesta Implements IComon.RecuperarClienteTotalizadorSaldo
        Return LogicaNegocio.Genesis.Cliente.RecuperarClienteTotalizadorSaldo(Peticion)
    End Function

    <WebMethod()> _
    Public Function BusquedaTipoContenedor(Peticion As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorPeticion) As Prosegur.Genesis.ContractoServicio.Helper.BusquedaTipoContenedor.HelperBusquedaTipoContenedorRespuesta Implements IComon.BusquedaTipoContenedor
        Return LogicaNegocio.Helper.BusquedaTipoContenedor(Peticion)
    End Function

    <WebMethod()> _
    Public Function CargarNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.CargarNotificacion.Respuesta Implements IComon.CargarNotificaciones
        Return LogicaNegocio.Genesis.Notificacion.CargarNotificaciones(Peticion)
    End Function

    <WebMethod()> _
    Public Function GrabarNotificacion(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacion.Respuesta Implements IComon.GrabarNotificacion
        Return LogicaNegocio.Genesis.Notificacion.GrabarNotification(Peticion)
    End Function

    <WebMethod()> _
    Public Function GrabarNotificacionLeido(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.GrabarNotificacionLeido.Respuesta Implements IComon.GrabarNotificacionLeido
        Return LogicaNegocio.Genesis.Notificacion.GrabarNotificacionLeido(Peticion)
    End Function

    <WebMethod()> _
    Public Function RecibirMensajeExterno(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.RecibirMensajeExterno.Respuesta Implements IComon.RecibirMensajeExterno
        Return LogicaNegocio.Genesis.Notificacion.RecibirMensajeExterno(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerCantidadNotificaciones(Peticion As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Peticion) As ContractoServicio.Contractos.Genesis.Notificacion.ObtenerCantidadNotificaciones.Respuesta Implements IComon.ObtenerCantidadNotificaciones
        Return LogicaNegocio.Genesis.Notificacion.ObtenerCantidadNotificaciones(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerDelegacionGMT(Peticion As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Peticion) As ContractoServicio.Contractos.Comon.Delegacion.ObtenerDelegacionGMT.Respuesta Implements IComon.ObtenerDelegacionGMT
        Return LogicaNegocio.Genesis.Delegacion.ObtenerDelegacionGMT(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerDelegaciones(Peticion As Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion) As Contractos.Comon.Delegacion.ObtenerDelegaciones.Respuesta Implements IComon.ObtenerDelegaciones
        Return LogicaNegocio.Genesis.Delegacion.ObtenerDelegaciones(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerValorDiccionario(Peticion As Contractos.Comon.Diccionario.ObtenerValorDiccionario.Peticion) As Contractos.Comon.Diccionario.ObtenerValorDiccionario.Respuesta Implements IComon.ObtenerValorDiccionario
        Return LogicaNegocio.Genesis.Diccionario.ObtenerValorDicionario(Peticion)
    End Function

    <WebMethod()> _
    Public Function ObtenerValoresDiccionario(Peticion As Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Peticion) As Contractos.Comon.Diccionario.ObtenerValoresDiccionario.Respuesta Implements IComon.ObtenerValoresDiccionario
        Return LogicaNegocio.Genesis.Diccionario.ObtenerValoresDicionario(Peticion)
    End Function
End Class