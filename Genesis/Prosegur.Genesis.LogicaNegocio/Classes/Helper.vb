Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Global

Namespace Classes

    ''' <summary>
    ''' Classe Helper.
    ''' </summary>
    ''' <history>
    ''' [Thiago Dias] 22/08/2013 - Criado.
    '''</history>
    Public Class Helper

        ''' <summary>
        ''' Método Principal de Busca do Controle Helper.
        ''' </summary>    
        ''' <returns>Resultado da consulta.</returns>    
        Public Shared Function Busqueda(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaHelper As New RespuestaHelper


            Select Case peticion.Tabela.Tabela
                Case TabelaHelper.Cliente
                    respuestaHelper = BusquedaCliente(peticion)
                Case TabelaHelper.ClientePorTipoPlanificacion
                    respuestaHelper = BusquedaClientePorTipoPlanificacion(peticion)
                Case TabelaHelper.SubCliente
                    respuestaHelper = BusquedaSubCliente(peticion)
                Case TabelaHelper.PuntoServicio
                    respuestaHelper = BusquedaPuntoServicio(peticion)
                Case TabelaHelper.PuntoServicioMaquina
                    respuestaHelper = BusquedaPuntoServicioMaquina(peticion)
                Case TabelaHelper.Sector
                    respuestaHelper = BusquedaSector(peticion)
                Case TabelaHelper.TipoSector
                    respuestaHelper = BusquedaTipoSector(peticion)
                Case TabelaHelper.Planta
                    respuestaHelper = BusquedaPlanta(peticion)
                Case TabelaHelper.Delegacion
                    respuestaHelper = BusquedaDelegacion(peticion)
                Case TabelaHelper.DelegacionCuadre
                    respuestaHelper = BusquedaDelegacionCuadre(peticion)
                Case TabelaHelper.Canal
                    respuestaHelper = BusquedaCanal(peticion)
                Case TabelaHelper.SubCanal
                    respuestaHelper = BusquedaSubCanal(peticion)
                Case TabelaHelper.Puesto
                    respuestaHelper = BusquedaPuesto(peticion)
                Case TabelaHelper.TipoContenedor
                    respuestaHelper = BusquedaTipoContenedorRespuestaGenerica(peticion)
                Case TabelaHelper.Denominacion
                    respuestaHelper = BusquedaDenominacion(peticion)
                Case TabelaHelper.Usuario
                    respuestaHelper = BusquedaUsuario(peticion)
                Case TabelaHelper.Planificacion
                    respuestaHelper = BusquedaPlanificacion(peticion)
                Case TabelaHelper.MaquinaSector
                    respuestaHelper = BusquedaMaquinaSector(peticion)
                Case TabelaHelper.MaquinaPunto
                    respuestaHelper = BusquedaMaquina(peticion)
                Case TabelaHelper.CanalPorCodigo
                    respuestaHelper = BusquedaCanalPorCodigo(peticion)
                Case TabelaHelper.SubCanalPorCodigo
                    respuestaHelper = BusquedaSubCanalPorCodigo(peticion)
                Case TabelaHelper.Termino
                    respuestaHelper = BusquedaTermino(peticion)

                Case TabelaHelper.PuntoServicioPorCodigo
                    respuestaHelper = BusquedaPuntoServicio2(peticion)

            End Select

            Return respuestaHelper

        End Function
        Public Shared Function Busqueda_Puesto(peticion As PeticionHelperPuesto) As RespuestaHelperPuesto

            Dim respuestaHelper As New RespuestaHelperPuesto

            Select Case peticion.Tabela.Tabela
                Case TabelaHelper.Puesto
                    respuestaHelper = BusquedaPuesto(peticion)
            End Select

            Return respuestaHelper

        End Function

        ''' <summary>
        ''' Busca por informações de Clientes.
        ''' </summary>        
        Protected Shared Function BusquedaCliente(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaCliente As New RespuestaHelper

            respuestaCliente.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaCliente.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaCliente.DatosRespuesta = AccesoDatos.Cliente.BuscarClientes(peticion, respuestaCliente.ParametrosPaginacion)

            Return respuestaCliente

        End Function

        ''' <summary>
        ''' Buscar Clientes por tipo planificacion.
        ''' </summary>        
        Protected Shared Function BusquedaClientePorTipoPlanificacion(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaCliente As New RespuestaHelper

            respuestaCliente.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaCliente.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaCliente.DatosRespuesta = AccesoDatos.Cliente.BuscarClientesPorTipoPlanificacion(peticion, respuestaCliente.ParametrosPaginacion)

            Return respuestaCliente

        End Function

        ''' <summary>
        ''' Busca por informações de SubCliente.
        ''' </summary>
        Private Shared Function BusquedaSubCliente(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaSubCliente As New RespuestaHelper

            respuestaSubCliente.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaSubCliente.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaSubCliente.DatosRespuesta = AccesoDatos.SubCliente.BuscarSubCliente(peticion, respuestaSubCliente.ParametrosPaginacion)

            Return respuestaSubCliente

        End Function

        ''' <summary>
        ''' Busca por informações de Punto de Servicio.
        ''' </summary>
        Private Shared Function BusquedaPuntoServicio(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPtoServicio As New RespuestaHelper

            respuestaPtoServicio.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPtoServicio.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPtoServicio.DatosRespuesta = AccesoDatos.PuntoServicio.PesquisarPuntoServicio(peticion, respuestaPtoServicio.ParametrosPaginacion)

            Return respuestaPtoServicio

        End Function



        ''' <summary>
        ''' Busca por informações de Punto de Servicio.
        ''' </summary>
        Private Shared Function BusquedaPuntoServicio2(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPtoServicio As New RespuestaHelper

            respuestaPtoServicio.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPtoServicio.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPtoServicio.DatosRespuesta = AccesoDatos.PuntoServicio.PesquisarPuntoServicio2(peticion, respuestaPtoServicio.ParametrosPaginacion)

            Return respuestaPtoServicio

        End Function


        ''' <summary>
        ''' Busca por informações de Punto de Servicio.
        ''' </summary>
        Private Shared Function BusquedaPuntoServicioMaquina(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPtoServicio As New RespuestaHelper

            respuestaPtoServicio.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPtoServicio.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPtoServicio.DatosRespuesta = AccesoDatos.PuntoServicio.PesquisarPuntoServicioMaquina(peticion, respuestaPtoServicio.ParametrosPaginacion)

            Return respuestaPtoServicio

        End Function



        ''' <summary>
        ''' Busca por informações de Sector.
        ''' </summary>
        Protected Shared Function BusquedaSector(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaSector As New RespuestaHelper

            respuestaSector.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaSector.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaSector.DatosRespuesta = AccesoDatos.Sector.PesquisarSector(peticion, respuestaSector.ParametrosPaginacion)

            Return respuestaSector

        End Function

        Protected Shared Function BusquedaTipoSector(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaSector As New RespuestaHelper

            respuestaSector.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaSector.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaSector.DatosRespuesta = AccesoDatos.TipoSector.PesquisarTipoSector(peticion, respuestaSector.ParametrosPaginacion)

            Return respuestaSector

        End Function

        Protected Shared Function BusquedaMaquinaSector(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaMaquina As New RespuestaHelper

            respuestaMaquina.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaMaquina.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaMaquina.DatosRespuesta = AccesoDatos.Maquina.PesquisarMaquinaSector(peticion, respuestaMaquina.ParametrosPaginacion)

            Return respuestaMaquina

        End Function
        Protected Shared Function BusquedaMaquina(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaMaquina As New RespuestaHelper

            respuestaMaquina.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaMaquina.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaMaquina.DatosRespuesta = AccesoDatos.Maquina.PesquisarMaquina(peticion, respuestaMaquina.ParametrosPaginacion)

            Return respuestaMaquina

        End Function

        Protected Shared Function BusquedaUsuario(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaUsuario As New RespuestaHelper

            respuestaUsuario.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaUsuario.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaUsuario.DatosRespuesta = AccesoDatos.Usuario.PesquisarUsuario(peticion, respuestaUsuario.ParametrosPaginacion)

            Return respuestaUsuario

        End Function

        Protected Shared Function BusquedaPlanificacion(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPlanificacion As New RespuestaHelper

            respuestaPlanificacion.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPlanificacion.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPlanificacion.DatosRespuesta = AccesoDatos.Genesis.Planificacion.PesquisarPlanificacion(peticion, respuestaPlanificacion.ParametrosPaginacion)

            Return respuestaPlanificacion

        End Function

        ''' <summary>
        ''' Busca por informações de Planta.
        ''' </summary>
        Protected Shared Function BusquedaPlanta(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPlanta As New RespuestaHelper

            respuestaPlanta.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPlanta.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPlanta.DatosRespuesta = AccesoDatos.Planta.PesquisarPlanta(peticion, respuestaPlanta.ParametrosPaginacion)

            Return respuestaPlanta

        End Function

        ''' <summary>
        ''' Busca por informações de Delegacion.
        ''' </summary>
        Protected Shared Function BusquedaDelegacion(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaDelegacion As New RespuestaHelper

            respuestaDelegacion.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaDelegacion.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaDelegacion.DatosRespuesta = AccesoDatos.Delegacion.PesquisarDelegacion(peticion, respuestaDelegacion.ParametrosPaginacion)

            Return respuestaDelegacion

        End Function


        Protected Shared Function BusquedaTermino(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaTermino As New RespuestaHelper

            respuestaTermino.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaTermino.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaTermino.DatosRespuesta = AccesoDatos.Termino.PesquisarTermino(peticion, respuestaTermino.ParametrosPaginacion)


            Return respuestaTermino

        End Function

        Protected Shared Function BusquedaDelegacionCuadre(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaDelegacion As New RespuestaHelper

            respuestaDelegacion.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaDelegacion.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaDelegacion.DatosRespuesta = AccesoDatos.Delegacion.PesquisarDelegacionCuadre(peticion, respuestaDelegacion.ParametrosPaginacion)

            Return respuestaDelegacion

        End Function

        ''' <summary>
        ''' Busca por informações de Canal.
        ''' </summary>
        Private Shared Function BusquedaCanal(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaCanal As New RespuestaHelper

            respuestaCanal.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaCanal.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaCanal.DatosRespuesta = AccesoDatos.Canal.PesquisarCanal(peticion, respuestaCanal.ParametrosPaginacion)

            Return respuestaCanal

        End Function


        Private Shared Function BusquedaCanalPorCodigo(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaCanal As New RespuestaHelper

            respuestaCanal.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaCanal.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaCanal.DatosRespuesta = AccesoDatos.Canal.PesquisarCanalCodigo(peticion, respuestaCanal.ParametrosPaginacion)

            Return respuestaCanal

        End Function

        ''' <summary>
        ''' Busca por informações de SubCanal.
        ''' </summary>
        Private Shared Function BusquedaSubCanal(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaSubCanal As New RespuestaHelper

            respuestaSubCanal.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaSubCanal.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaSubCanal.DatosRespuesta = AccesoDatos.SubCanal.PesquisarSubCanal(peticion, respuestaSubCanal.ParametrosPaginacion)

            Return respuestaSubCanal

        End Function
        ''' <summary>
        ''' Busca por informações de SubCanal.
        ''' </summary>
        Private Shared Function BusquedaSubCanalPorCodigo(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaSubCanal As New RespuestaHelper

            respuestaSubCanal.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaSubCanal.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaSubCanal.DatosRespuesta = AccesoDatos.SubCanal.PesquisarSubCanalPorCodigo(peticion, respuestaSubCanal.ParametrosPaginacion)

            Return respuestaSubCanal

        End Function

        ''' <summary>
        ''' Buscar puesto
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function BusquedaPuesto(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaPuesto As New RespuestaHelper

            respuestaPuesto.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPuesto.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPuesto.DatosRespuesta = AccesoDatos.Puesto.PesquisarPuesto(peticion, respuestaPuesto.ParametrosPaginacion)

            Return respuestaPuesto

        End Function

        Public Shared Function BusquedaTipoContenedor(peticion As PeticionHelperTipoContenedor) As RespuestaHelperTipoContenedor
            Dim respuestaTipoContenedor As New RespuestaHelperTipoContenedor

            respuestaTipoContenedor.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaTipoContenedor.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaTipoContenedor.DatosRespuesta = AccesoDatos.TipoContenedor.PesquisarTipoContenedor(peticion, respuestaTipoContenedor.ParametrosPaginacion)

            Return respuestaTipoContenedor
        End Function

        Public Shared Function BusquedaTipoContenedorRespuestaGenerica(peticion As PeticionHelper) As RespuestaHelper
            Dim respuestaTipoContenedor As New RespuestaHelperTipoContenedor

            respuestaTipoContenedor.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaTipoContenedor.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaTipoContenedor.DatosRespuesta = AccesoDatos.TipoContenedor.PesquisarTipoContenedor(ObtenerPeticionHelperTipoContenedor(peticion), respuestaTipoContenedor.ParametrosPaginacion)

            Return ObtenerRespuestaHelperPorTipoContenedor(respuestaTipoContenedor)
        End Function

        Private Shared Function ObtenerPeticionHelperTipoContenedor(peticion As PeticionHelper) As PeticionHelperTipoContenedor
            Dim HelperPeticionTipoContenedor = New PeticionHelperTipoContenedor

            HelperPeticionTipoContenedor.Codigo = peticion.Codigo
            HelperPeticionTipoContenedor.Descripcion = peticion.Descripcion
            HelperPeticionTipoContenedor.FiltroSQL = peticion.FiltroSQL
            HelperPeticionTipoContenedor.JuncaoSQL = peticion.JuncaoSQL
            HelperPeticionTipoContenedor.OrdenacaoSQL = peticion.OrdenacaoSQL
            HelperPeticionTipoContenedor.Query = peticion.Query
            HelperPeticionTipoContenedor.Tabela = peticion.Tabela
            HelperPeticionTipoContenedor.UsarLike = peticion.UsarLike

            If (peticion.DadosPeticao IsNot Nothing) Then

                HelperPeticionTipoContenedor.DadosPeticao = New List(Of PeticionTipoContenedor)

                For Each objDadoPeticion As Peticion In peticion.DadosPeticao
                    Dim peticionTipoContenedor = New PeticionTipoContenedor

                    With peticionTipoContenedor
                        .Codigo = objDadoPeticion.Codigo
                        .Descripcion = objDadoPeticion.Descricao
                        .Identificador = objDadoPeticion.Identificador
                        .IdentificadorPai = objDadoPeticion.IdentificadorPai
                        .TabelaIdentificador = objDadoPeticion.TabelaIdentificador
                        .TabelaIdentificadorPai = objDadoPeticion.TabelaIdentificadorPai
                    End With

                    HelperPeticionTipoContenedor.DadosPeticao.Add(peticionTipoContenedor)
                Next
            End If

            HelperPeticionTipoContenedor.ParametrosPaginacion = peticion.ParametrosPaginacion

            Return HelperPeticionTipoContenedor
        End Function

        Private Shared Function ObtenerRespuestaHelperPorTipoContenedor(respuestaHelperTipoContenedor As RespuestaHelperTipoContenedor) As RespuestaHelper
            Dim HelperRespuesta = New RespuestaHelper

            HelperRespuesta.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            If respuestaHelperTipoContenedor.DatosRespuesta IsNot Nothing Then
                For Each objDataRespuestaTipoContenedor As Comon.Helper.RespuestaHelperTipoContenedorDatos In respuestaHelperTipoContenedor.DatosRespuesta
                    Dim respuesta = New Comon.Helper.Respuesta
                    respuesta.Codigo = objDataRespuestaTipoContenedor.Codigo
                    respuesta.Descricao = objDataRespuestaTipoContenedor.Descricao
                    respuesta.Identificador = objDataRespuestaTipoContenedor.Identificador
                    respuesta.IdentificadorPai = objDataRespuestaTipoContenedor.IdentificadorPai
                    HelperRespuesta.DatosRespuesta.Add(respuesta)
                Next
            End If
            HelperRespuesta.ParametrosPaginacion = respuestaHelperTipoContenedor.ParametrosPaginacion

            Return HelperRespuesta
        End Function

        Private Shared Function BusquedaPuesto(peticion As PeticionHelperPuesto) As RespuestaHelperPuesto

            Dim respuestaPuesto As New RespuestaHelperPuesto

            respuestaPuesto.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaPuesto.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaPuesto.DatosRespuesta = AccesoDatos.Puesto.PesquisarPuesto2(peticion, respuestaPuesto.ParametrosPaginacion)

            Return respuestaPuesto

        End Function

        Private Shared Function BusquedaDenominacion(peticion As PeticionHelper) As RespuestaHelper

            Dim respuestaDenominacion As New RespuestaHelper

            respuestaDenominacion.ParametrosPaginacion = New ParametrosRespuestaPaginacion()
            respuestaDenominacion.ParametrosPaginacion.TotalRegistrosPaginados = peticion.ParametrosPaginacion.RegistrosPorPagina
            respuestaDenominacion.DatosRespuesta = AccesoDatos.Denominacion.PesquisarDenominacion(peticion, respuestaDenominacion.ParametrosPaginacion)

            Return respuestaDenominacion

        End Function
    End Class

End Namespace