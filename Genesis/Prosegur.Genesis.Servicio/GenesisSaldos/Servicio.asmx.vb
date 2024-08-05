Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis.ContractoServicio

Namespace GenesisSaldos

    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://Prosegur.Global.GesEfectivo.GenesisSaldos/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class Servicio
        Inherits System.Web.Services.WebService
        Implements INuevoSaldos

        <WebMethod()> _
        Function CambiaEstadoDocumentoFondosSaldos(Peticion As CambiaEstadoDocumentoFondosSaldos.Peticion) As CambiaEstadoDocumentoFondosSaldos.Respuesta Implements INuevoSaldos.CambiaEstadoDocumentoFondosSaldos
            Dim objAccion As New Genesis.LogicaNegocio.GenesisSaldos.Integracion.CambiaEstadoDocumentoFondosSaldos()
            Return objAccion.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function IngresoRemesas(Peticion As IngresoRemesas.Peticion) As IngresoRemesas.Respuesta Implements INuevoSaldos.IngresoRemesas
            Return Genesis.LogicaNegocio.Integracion.AccionIngresoRemesas.Ejecutar(Peticion)
        End Function


        Public Function Test() As Genesis.ContractoServicio.Test.Respuesta Implements Genesis.ContractoServicio.Interfaces.INuevoSaldos.Test
            Dim objAccion As New Genesis.LogicaNegocio.Test.AccionTest
            Return objAccion.Ejecutar
        End Function

        <WebMethod()> _
        Public Function RecuperarSaldoExpuestoxDetallado(Peticion As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Peticion) As Contractos.GenesisSaldos.Saldo.RecuperarSaldoExpuestoxDetallado.Respuesta Implements INuevoSaldos.RecuperarSaldoExpuestoxDetallado
            Dim objAccion As New Genesis.LogicaNegocio.GenesisSaldos.Saldo
            Return objAccion.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function ConsultaDocumentos(Peticion As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Respuesta Implements INuevoSaldos.ConsultaDocumentos
            Dim objAccion As New Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos
            Return objAccion.EjecutarConsultaDocumentos(Peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperarDocumentoPorIdentificador(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoPorIdentificador.Respuesta Implements INuevoSaldos.RecuperarDocumentoPorIdentificador
            Return Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.RecuperarDocumentoPorIdentificador(Peticion)
        End Function

        <WebMethod()> _
        Public Function GuardarDocumento(Peticion As Contractos.GenesisSaldos.Documento.GuardarDocumento.Peticion) As Contractos.GenesisSaldos.Documento.GuardarDocumento.Respuesta Implements INuevoSaldos.GuardarDocumento
            Return Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.GuardarDocumento(Peticion)
        End Function

        <WebMethod()> _
        Public Function GuardarGrupoDocumento(Peticion As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento.Respuesta Implements INuevoSaldos.GuardarGrupoDocumento
            Return Prosegur.Genesis.LogicaNegocio.Integracion.AccionCrearGrupoDocumentos.GuardarGrupoDocumentos(Peticion)
        End Function

        <WebMethod()> _
        Function SalirRecorrido(peticion As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoPeticion) As Contractos.Comon.Elemento.SalirRecorrido.SalirRecorridoRespuesta Implements INuevoSaldos.SalirRecorrido
            Return Genesis.LogicaNegocio.Integracion.AccionSalidasRecorrido.Ejecutar(peticion)
        End Function

        <WebMethod()> _
        Function Reenvio(peticion As Contractos.Comon.Elemento.Reenvio.ReenvioPeticion) As Contractos.Comon.Elemento.Reenvio.ReenvioRespuesta Implements INuevoSaldos.Reenvio
            Return Genesis.LogicaNegocio.Integracion.AccionCrearDocumentoReenvio.ejecutarReenvioAntiguo(peticion)
        End Function

        <WebMethod()> _
        Public Function IngresoRemesasNuevo(Peticion As IngresoRemesasNuevo.Peticion) As IngresoRemesasNuevo.Respuesta Implements INuevoSaldos.IngresoRemesasNuevo
            Return Genesis.LogicaNegocio.Integracion.AccionIngresoRemesasNuevo.Ejecutar(Peticion)
        End Function


        <WebMethod()> _
        Public Function CrearDocumento(Peticion As Contractos.GenesisSaldos.Documento.CrearDocumento.Peticion) As Contractos.GenesisSaldos.Documento.CrearDocumento.Respuesta Implements INuevoSaldos.CrearDocumento
            Return Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.CrearDocumento(Peticion)
        End Function

        <WebMethod()> _
        Public Function ObtenerCuentaPorCodigos(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta Implements INuevoSaldos.ObtenerCuentaPorCodigos
            Return Genesis.LogicaNegocio.Genesis.MaestroCuenta.AccionObtenerCuentaPorCodigos(Peticion)
        End Function

        <WebMethod()> _
        Public Function ObtenerCuentas(Peticion As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Peticion) As Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta Implements INuevoSaldos.ObtenerCuentas
            Return Genesis.LogicaNegocio.Genesis.MaestroCuenta.AccionObtenerCuentas(Peticion)
        End Function

        <WebMethod()> _
        Function ActualizarSaldoPuesto(Peticion As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Peticion) As Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto.Respuesta Implements INuevoSaldos.ActualizarSaldoPuesto
            Return Genesis.LogicaNegocio.Integracion.ActualizarSaldoPuesto.Ejecutar(Peticion)
        End Function

        <WebMethod()> _
        Public Function AperturarElemento(Peticion As Contractos.GenesisSaldos.Documento.AperturarElemento.Peticion) As Contractos.GenesisSaldos.Documento.AperturarElemento.Respuesta Implements INuevoSaldos.AperturarElemento
            Return Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.AperturarElemento(Peticion)
        End Function

        <WebMethod()> _
        Public Function obtenerDocumentos(Peticion As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Peticion) As Contractos.GenesisSaldos.Documento.obtenerDocumentos.Respuesta Implements INuevoSaldos.obtenerDocumentos
            Return Genesis.LogicaNegocio.GenesisSaldos.Documento.ejecutarObtenerDocumentos(Peticion)
        End Function

        <WebMethod()>
        Public Function RecuperarDocumentoParaAlocacion(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion.Respuesta Implements INuevoSaldos.RecuperarDocumentoParaAlocacion
            Return Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion(Peticion)
        End Function

        <WebMethod()> _
        Public Function RecuperarDocumentosElementosConcluidos(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos.Respuesta Implements INuevoSaldos.RecuperarDocumentosElementosConcluidos
            Return Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarDocumentosElementosConcluidos(Peticion)
        End Function

        <WebMethod()>
        Public Function GenerarInforme(Peticion As Contractos.GenesisSaldos.Reporte.GenerarInforme.Peticion) As Contractos.GenesisSaldos.Reporte.GenerarInforme.Respuesta Implements INuevoSaldos.GenerarInforme
            Return Genesis.LogicaNegocio.GenesisSaldos.GenerarInforme.Ejecutar(Peticion)
        End Function

        <WebMethod()>
        Public Function ActualizaBolImpreso(Peticion As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Peticion) As Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Respuesta Implements INuevoSaldos.ActualizaBolImpreso

            Dim RowVerDocumento As Integer
            If Peticion IsNot Nothing Then
                If Peticion.EsGrupo Then
                    RowVerDocumento = Genesis.LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.ActualizaBolImpreso(Peticion.IdentificadorDocumento, Peticion.CodigoComprobante, Peticion.Impreso)
                Else
                    RowVerDocumento = Genesis.LogicaNegocio.GenesisSaldos.MaestroDocumentos.ActualizaBolImpreso(Peticion.IdentificadorDocumento, Peticion.CodigoComprobante, Peticion.Impreso)
                End If
            End If

            Dim objResposta As New Contractos.GenesisSaldos.Documento.ActualizaBolImpreso.Respuesta

            objResposta.RowVerDocumento = RowVerDocumento

            Return objResposta
        End Function

        <WebMethod()>
        Public Function RecuperarCaracteristicasPorCodigoComprobante(Peticion As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta Implements INuevoSaldos.RecuperarCaracteristicasPorCodigoComprobante
            Return Genesis.LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.RecuperarCaracteristicasPorCodigoComprobante(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarContenedoresCliente(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresCliente.Respuesta Implements INuevoSaldos.ConsultarContenedoresCliente
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedoresCliente(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarContenedoresPackModular(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresPackModular.Respuesta Implements INuevoSaldos.ConsultarContenedoresPackModular
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedoresPackModular(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarDocumentosGestionContenedores(Peticion As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Peticion) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarDocumentosGestionContenedores.Respuesta Implements INuevoSaldos.ConsultarDocumentosGestionContenedores
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarDocumentosGestionContenedores(Peticion)
        End Function

        <WebMethod()>
        Public Function ArmarContenedores(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ArmarContenedores.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ArmarContenedores.Respuesta Implements INuevoSaldos.ArmarContenedores
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ArmarContenedores(Peticion)
        End Function

        <WebMethod()>
        Public Function DesarmarContenedores(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.DesarmarContenedores.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.DesarmarContenedores.Respuesta Implements INuevoSaldos.DesarmarContenedores
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.DesarmarContenedores(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarContenedoresSector(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresSector.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedoresSector.Respuesta Implements INuevoSaldos.ConsultarContenedoresSector
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedoresSector(Peticion)
        End Function

        <WebMethod()>
        Public Function DefinirCambiarExtraerPosicionContenedor(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Respuesta Implements INuevoSaldos.DefinirCambiarExtraerPosicionContenedor
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.DefinirCambiarExtraerPosicionContenedor(Peticion)
        End Function

        'Almacenar Alertas de Vencimiento
        <WebMethod()>
        Public Function GrabarAlertaVencimiento(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarAlertaVencimiento.Respuesta Implements INuevoSaldos.GrabarAlertaVencimiento
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.GrabarAlertaVencimiento(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarContenedorxPosicion(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Respuesta Implements INuevoSaldos.ConsultarContenedorxPosicion
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedorxPosicion(Peticion)
        End Function

        'Almacenar Inventario
        <WebMethod()>
        Public Function GrabarInventarioContenedor(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta Implements INuevoSaldos.GrabarInventarioContenedor
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.GrabarInventarioContenedor(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarInventarioContenedor(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarInventarioContenedor.Respuesta Implements INuevoSaldos.ConsultarInventarioContenedor
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarInventarioContenedor(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarSeguimientoElemento(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarSeguimientoElemento.Respuesta Implements INuevoSaldos.ConsultarSeguimientoElemento
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarSeguimientoElemento(Peticion)
        End Function

        <WebMethod()>
        Public Function ConsultarContenedoresPorFIFO(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta Implements INuevoSaldos.ConsultarContenedoresPorFIFO
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedoresPorFIFO(Peticion)
        End Function

        <WebMethod()>
        Public Function ReenvioEntreSectores(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta Implements INuevoSaldos.ReenvioEntreSectores
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ReenvioEntreSectores(Peticion)
        End Function

        <WebMethod()>
        Public Function ReenvioEntreClientes(Peticion As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreClientes.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreClientes.Respuesta Implements INuevoSaldos.ReenvioEntreClientes
            Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ReenvioEntreClientes(Peticion)
        End Function

        <WebMethod()>
        Public Function RecuperarDocumentosSinSalidaRecorrido(Peticion As Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Peticion) As Contractos.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido.Respuesta Implements INuevoSaldos.RecuperarDocumentosSinSalidaRecorrido
            Return Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarDocumentosSinSalidaRecorrido(Peticion)
        End Function

        <WebMethod()>
        Public Function RomperPrecintosSaldosSalidas(Peticion As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Peticion) As Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas.Respuesta Implements INuevoSaldos.RomperPrecintosSaldosSalidas
            Return Genesis.LogicaNegocio.Genesis.Bulto.RomperPrecintosSaldosSalidas(Peticion)
        End Function

    End Class

End Namespace