Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboTiposCuenta

Namespace IAC.Servicio

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <WebService(Namespace:="http://Prosegur.Global.GesEfectivo.IAC")>
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
    <ToolboxItem(False)>
    Public Class Utilidad
        Inherits System.Web.Services.WebService
        Implements ContractoServicio.IUtilidad

        <WebMethod()>
        Public Function GetComboMaquinas() As ContractoServicio.Utilidad.GetComboMaquinas.Respuesta Implements ContractoServicio.IUtilidad.GetComboMaquinas
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboMaquinas()
        End Function

        <WebMethod()>
        Public Function GetComboAlgoritmos(objPeticion As ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion) As ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta Implements ContractoServicio.IUtilidad.GetComboAlgoritmos
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboAlgoritmos(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboFormatos() As ContractoServicio.Utilidad.GetComboFormatos.Respuesta Implements ContractoServicio.IUtilidad.GetComboFormatos
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboFormatos()
        End Function

        <WebMethod()>
        Public Function GetComboMascaras(objPeticion As ContractoServicio.Utilidad.GetComboMascaras.Peticion) As ContractoServicio.Utilidad.GetComboMascaras.Respuesta Implements ContractoServicio.IUtilidad.GetComboMascaras
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboMascaras(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboTerminosIAC(objPeticion As ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion) As ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta Implements ContractoServicio.IUtilidad.GetComboTerminosIAC
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTerminosIAC(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboMediosPagoByTipoAndDivisa(objPeticion As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion) As ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta Implements ContractoServicio.IUtilidad.GetComboMediosPagoByTipoAndDivisa
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboMediosPagoByTipoAndDivisa(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboTiposMedioPagoByDivisa(objPeticion As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Peticion) As ContractoServicio.Utilidad.GetComboTiposMedioPagoByDivisa.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposMedioPagoByDivisa
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposMedioPagoByDivisa(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboDivisas() As ContractoServicio.Utilidad.GetComboDivisas.Respuesta Implements ContractoServicio.IUtilidad.GetComboDivisas
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboDivisas()
        End Function

        <WebMethod()>
        Public Function GetComboDivisasByTipoMedioPago(objPeticion As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion) As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetComboDivisasByTipoMedioPago
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboDivisasByTipoMedioPago(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetDivisasMedioPago() As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetDivisasMedioPago
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetDivisasMedioPago()
        End Function

        <WebMethod()>
        Public Function GetComboClientes(objPeticion As ContractoServicio.Utilidad.GetComboClientes.Peticion) As ContractoServicio.Utilidad.GetComboClientes.Respuesta Implements ContractoServicio.IUtilidad.GetComboClientes
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboClientes(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboTiposMedioPago() As ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposMedioPago
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposMedioPago()
        End Function

        <WebMethod()>
        Public Function GetComboSubclientesByCliente(objPeticion As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboSubclientesByCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboSubclientesByCliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboPuntosServiciosByClienteSubcliente(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboPuntosServiciosByClienteSubcliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboPuntosServiciosBySubcliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboPuntosServiciosByClientesSubclientes(objPeticion As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Peticion) As ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.Respuesta Implements ContractoServicio.IUtilidad.GetComboPuntosServiciosByClientesSubclientes
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboPuntosServiciosByClientesSubClientes(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboCanales() As ContractoServicio.Utilidad.GetComboCanales.Respuesta Implements ContractoServicio.IUtilidad.GetComboCanales
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboCanales()
        End Function

        <WebMethod()>
        Public Function GetComboSubcanalesByCanal(objPeticion As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta Implements ContractoServicio.IUtilidad.GetComboSubcanalesByCanal
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboSubcanalesByCanal(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboProductos() As ContractoServicio.Utilidad.GetComboProductos.Respuesta Implements ContractoServicio.IUtilidad.GetComboProductos
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboProductos()
        End Function

        <WebMethod()>
        Public Function GetComboDelegaciones() As ContractoServicio.Utilidad.GetComboDelegaciones.Respuesta Implements ContractoServicio.IUtilidad.GetComboDelegaciones
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboDelegaciones
        End Function

        <WebMethod()>
        Public Function GetComboPais() As ContractoServicio.Utilidad.GetComboPais.Respuesta Implements ContractoServicio.IUtilidad.GetComboPais
            Dim ObjAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return ObjAccionUtilidad.GetComboPais
        End Function

        <WebMethod()>
        Public Function GetComboDelegacionesPorPais(objPeticion As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Peticion) As ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta Implements ContractoServicio.IUtilidad.GetComboDelegacionesPorPais
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboDelegacionesPorPais(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboModalidadesRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta Implements ContractoServicio.IUtilidad.GetComboModalidadesRecuento
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboModalidadesRecuento
        End Function

        <WebMethod()>
        Public Function GetComboInformacionAdicional() As ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta Implements ContractoServicio.IUtilidad.GetComboInformacionAdicional
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboInformacionAdicional
        End Function
        <WebMethod()>
        Public Function GetComboInformacionAdicionalConFiltros(objPeticion As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion) As ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta Implements ContractoServicio.IUtilidad.GetComboInformacionAdicionalConFiltros
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboInformacionAdicional(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetListaAgrupaciones() As ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta Implements ContractoServicio.IUtilidad.GetListaAgrupaciones
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetListaAgrupaciones
        End Function

        <WebMethod()>
        Public Function VerificarCodigoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoCaracteristica
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoCaracteristica(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoConteoCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoConteoCaracteristica
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoConteoCaracteristica(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarDescripcionCaracteristica(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionCaracteristica
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarDescripcionCaracteristica(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboCaracteristicas() As ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta Implements ContractoServicio.IUtilidad.GetComboCaracteristicas
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboCaracteristicas()
        End Function

        <WebMethod()>
        Public Function GetComboModelosCajero() As ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta Implements ContractoServicio.IUtilidad.GetComboModelosCajero
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboModelosCajero()
        End Function

        <WebMethod()>
        Public Function GetComboRedes() As ContractoServicio.Utilidad.GetComboRedes.Respuesta Implements ContractoServicio.IUtilidad.GetComboRedes
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboRedes()
        End Function

        <WebMethod()>
        Public Function GetComboNivelesParametros(objPeticion As ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion) As ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta Implements ContractoServicio.IUtilidad.GetComboNivelesParametros
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.getComboNivelesParametros(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboAplicaciones() As ContractoServicio.Utilidad.getComboAplicaciones.Respuesta Implements ContractoServicio.IUtilidad.GetComboAplicaciones
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.getComboAplicaciones()
        End Function

        <WebMethod()>
        Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IUtilidad.Test
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.Test()
        End Function

        <WebMethod()>
        Public Function GetComboCaractTipoSector() As ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta Implements ContractoServicio.IUtilidad.GetComboCaractTipoSector
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboCaractTipoSector()
        End Function

        <WebMethod()>
        Public Function GetConfigNivelSaldo(Peticion As ContractoServicio.Utilidad.GetConfigNivel.Peticion) As ContractoServicio.Utilidad.GetConfigNivel.Respuesta Implements ContractoServicio.IUtilidad.GetConfigNivelSaldo
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetConfigNivelSaldo(Peticion)
        End Function

        <WebMethod()>
        Public Function GetComboTiposSubCliente() As ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposSubCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposSubCliente()
        End Function

        <WebMethod()>
        Public Function GetComboTiposPuntoServicio() As ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposPuntoServicio
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposPuntoServicio()
        End Function

        <WebMethod()>
        Public Function GetComboTiposProcedencia() As ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposProcedencia
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposProcedencia()
        End Function

        <WebMethod()>
        Public Function VerificarCodigoCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoCliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoPtoServicio
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoPtoServicio(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoSubCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoSubCliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarDescripcionCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarDescripcionCliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarDescripcionPtoServicio(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionPtoServicio
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarDescripcionPtoServicio(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarDescripcionSubCliente(objPeticion As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion) As ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta Implements ContractoServicio.IUtilidad.VerificarDescripcionSubCliente
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarDescripcionSubCliente(objPeticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoAccesoDenominacion(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDenominacion.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoDenominacion
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoAccesoDenominacion(Peticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoAccesoDivisa(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoDivisa
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoAccesoDivisa(Peticion)
        End Function

        <WebMethod()>
        Public Function VerificarCodigoAccesoMedioPago(Peticion As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion) As ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta Implements ContractoServicio.IUtilidad.VerificarCodigoAccesoMedioPago
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.VerificarCodigoAccesoMedioPago(Peticion)
        End Function

        <WebMethod()>
        Public Function GetComboSectores(objPeticion As ContractoServicio.Utilidad.GetComboSectores.Peticion) As ContractoServicio.Utilidad.GetComboSectores.Respuesta Implements ContractoServicio.IUtilidad.GetComboSectores
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboSectores(objPeticion)
        End Function

        <WebMethod()>
        Public Function GetComboTiposCuenta() As Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposCuenta
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposCuentas
        End Function

        <WebMethod()>
        Public Function GetComboTiposPeriodo() As ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta Implements ContractoServicio.IUtilidad.GetComboTiposPeriodo
            Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad
            Return objAccionUtilidad.GetComboTiposPeriodos
        End Function

    End Class

End Namespace