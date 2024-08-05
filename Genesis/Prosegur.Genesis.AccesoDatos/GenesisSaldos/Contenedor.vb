Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Transactions
Imports Prosegur.Genesis.DataBaseHelper

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Contenedor

#Region "[CONSULTAR]"
        Public Shared Function ConsultarContenedorxPosicion(Peticion As Contenedores.ConsultarContenedorxPosicion.Peticion) As DataSet

            'Define si deben ser regresados los contenedores del tipo pack modular, no pack modular o ambos:
            'Nulo: Serán regresados ambos contenedores: pack modular y no pack modular
            '0: Serán regresado solamente contenedores q no son pack modular
            '1: Serán regresado solamente contenedores q son pack modular

            Dim spw As SPWrapper = ColectarContenedoresxPosicion(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function
        Public Shared Function ConsultarContenedoresSector(Peticion As Contenedores.ConsultarContenedoresSector.Peticion) As DataSet

            Dim spw As SPWrapper = ColectarContenedoresSector(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        Public Shared Function ConsultarContenedoresCliente(Peticion As Contenedores.ConsultarContenedoresCliente.Peticion) As DataSet

            Dim spw As SPWrapper = ColectarContenedoresCliente(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        Private Shared Function ColectarConsultarInventario(peticion As Contenedores.ConsultarInventarioContenedor.Peticion) As SPWrapper

            ''stored procedure
            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.s_consultar_invent_contenedor", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            'Contenedor
            If peticion.Inventario IsNot Nothing Then
                spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                               Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                               Tradutor.CulturaSistema.Name,
                                                                               If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

                spw.AgregarParam("par$codInventario", ProsegurDbType.Descricao_Longa, peticion.Inventario.codInventario, , False)
                spw.AgregarParam("par$fechaHoraInventarioDesde", ProsegurDbType.Data_Hora, peticion.Inventario.fechaHoraInventarioDesde, , False)
                spw.AgregarParam("par$fechaHoraInventarioHasta", ProsegurDbType.Data_Hora, peticion.Inventario.fechaHoraInventarioHasta, , False)

                If peticion.Inventario.Sector IsNot Nothing Then
                    spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Sector.codDelegacion), Nothing, peticion.Inventario.Sector.codDelegacion), , False)
                    spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Sector.codPlanta), Nothing, peticion.Inventario.Sector.codPlanta), , False)
                    spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Sector.codSector), Nothing, peticion.Inventario.Sector.codSector), , False)
                Else
                    spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, Nothing, , False)
                    spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, Nothing, , False)
                    spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, Nothing, , False)
                End If

                If peticion.Inventario.Cliente IsNot Nothing Then
                    spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Cliente.codCliente), Nothing, peticion.Inventario.Cliente.codCliente), , False)
                    spw.AgregarParam("par$codSubcliente", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Cliente.codSubcliente), Nothing, peticion.Inventario.Cliente.codSubcliente), , False)
                    spw.AgregarParam("par$codPuntoServicio", ProsegurDbType.Descricao_Longa, IIf(String.IsNullOrEmpty(peticion.Inventario.Cliente.codPuntoServicio), Nothing, peticion.Inventario.Cliente.codPuntoServicio), , False)
                Else
                    spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                    spw.AgregarParam("par$codSubcliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                    spw.AgregarParam("par$codPuntoServicio", ProsegurDbType.Descricao_Longa, Nothing, , False)
                End If
                'Retorno
                spw.AgregarParam("par$rc_Inventario", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "INVENTARIO")
                spw.AgregarParam("par$rc_detalle_efec_mp", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMP")

                spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
                spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
                spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                      "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                      "par$cod_transaccion", "par$cod_resultado")


            End If

            Return spw

        End Function
        Private Shared Function ColectarContenedoresxPosicion(peticion As Contenedores.ConsultarContenedorxPosicion.Peticion) As SPWrapper

            ''stored procedure
            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.s_buscarcontenedorxposicion", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            'Contenedor
            If peticion.Contenedor IsNot Nothing Then

                spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, peticion.Contenedor.sector.codDelegacion, , False)
                spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, peticion.Contenedor.sector.codPlanta, , False)
                spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, peticion.Contenedor.sector.codSector, , False)

                If String.IsNullOrEmpty(peticion.Contenedor.sector.packModular) Then
                    spw.AgregarParam("par$packModular", ProsegurDbType.Descricao_Longa, Nothing, , False)
                Else
                    spw.AgregarParam("par$packModular", ProsegurDbType.Descricao_Longa, peticion.Contenedor.sector.packModular, , False)
                End If

                'Retorno
                spw.AgregarParam("par$rc_contenedores", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CONTENEDORES")
                spw.AgregarParam("par$rc_detalle_efec_mp", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMP")

                spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
                spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
                spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                           "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                           "par$cod_transaccion", "par$cod_resultado")

            End If

            Return spw
        End Function

        Private Shared Function ColectarContenedoresCliente(peticion As Contenedores.ConsultarContenedoresCliente.Peticion) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.sconsultar_contenedor_cli", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            'Contenedor
            If peticion.Contenedor IsNot Nothing Then
                spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Descricao_Longa, peticion.Contenedor.codTipoContenedor, , False)
                spw.AgregarParam("par$codEstadoConenedor", ProsegurDbType.Descricao_Longa, peticion.Contenedor.codEstadoContenedor, , False)
                If peticion.Contenedor.fechaHoraArmadoDesde = Date.MinValue Then
                    spw.AgregarParam("par$fyhArmadoDesde", ProsegurDbType.Data_Hora, Nothing, , False)
                Else
                    spw.AgregarParam("par$fyhArmadoDesde", ProsegurDbType.Data_Hora, peticion.Contenedor.fechaHoraArmadoDesde, , False)
                End If
                If peticion.Contenedor.fechaHoraArmadoHasta = Date.MinValue Then
                    spw.AgregarParam("par$fyhArmadoHasta", ProsegurDbType.Data_Hora, Nothing, , False)
                Else
                    spw.AgregarParam("par$fyhArmadoHasta", ProsegurDbType.Data_Hora, peticion.Contenedor.fechaHoraArmadoHasta, , False)
                End If
                If String.IsNullOrEmpty(peticion.Contenedor.packModular) Then
                    spw.AgregarParam("par$bolPackModular", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                Else
                    spw.AgregarParam("par$bolPackModular", ProsegurDbType.Inteiro_Curto, peticion.Contenedor.packModular, , False)
                End If
            Else
                spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codEstadoConenedor", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$fyhArmadoDesde", ProsegurDbType.Data_Hora, Nothing, , False)
                spw.AgregarParam("par$fyhArmadoHasta", ProsegurDbType.Data_Hora, Nothing, , False)
                spw.AgregarParam("par$bolPackModular", ProsegurDbType.Inteiro_Curto, Nothing, , False)
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            'Clientes
            spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codSubCliente", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codCanal", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codSubcanal", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'Retorno
            spw.AgregarParam("par$rcContenedores", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CONTENEDORES")
            spw.AgregarParam("par$rcClientes", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CLIENTES")
            spw.AgregarParam("par$rcPrecintos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "PRECINTOS")
            spw.AgregarParam("par$rcDetalleEfecMP", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMP")

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            ' VALORES ARRAY
            If peticion.Clientes IsNot Nothing AndAlso peticion.Clientes.Count > 0 Then
                For Each objCliente In peticion.Clientes
                    'Clientes
                    spw.Param("par$codCliente").AgregarValorArray(objCliente.codCliente)
                    spw.Param("par$codSubCliente").AgregarValorArray(objCliente.codSubcliente)
                    spw.Param("par$codPtoServicio").AgregarValorArray(objCliente.codPuntoServicio)
                Next
            End If

            'Sectores
            If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                For Each sector In peticion.Sectores
                    If Not String.IsNullOrEmpty(sector.codSector) Then
                        spw.Param("par$codSector").AgregarValorArray(sector.codSector)
                    End If
                    If Not String.IsNullOrEmpty(sector.codDelegacion) Then
                        spw.Param("par$codDelegacion").AgregarValorArray(sector.codDelegacion)
                    End If
                    If Not String.IsNullOrEmpty(sector.codPlanta) Then
                        spw.Param("par$codPlanta").AgregarValorArray(sector.codPlanta)
                    End If
                Next
            End If

            'Canais
            If peticion.Canais IsNot Nothing AndAlso peticion.Canais.Count > 0 Then
                For Each canal In peticion.Canais
                    spw.Param("par$codCanal").AgregarValorArray(canal.codCanal)
                    spw.Param("par$codSubcanal").AgregarValorArray(canal.codSubcanal)
                Next
            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                    "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                    "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Function ColectarContenedoresSector(peticion As Contenedores.ConsultarContenedoresSector.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.sconsultar_contenedor_sector", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$codIdioma", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)


            'Contenedor
            spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Contenedor IsNot Nothing,
                                                                                                   peticion.Contenedor.codTipoContenedor,
                                                                                                   Nothing), , False)
            spw.AgregarParam("par$codEstadoConenedor", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Contenedor IsNot Nothing,
                                                                                                     peticion.Contenedor.codEstadoContenedor,
                                                                                                     Nothing), , False)
            spw.AgregarParam("par$fyhArmadoDesde", ProsegurDbType.Data_Hora, If(peticion.Contenedor IsNot Nothing AndAlso peticion.Contenedor.fechaHoraArmadoDesde IsNot Nothing,
                                                                               peticion.Contenedor.fechaHoraArmadoDesde,
                                                                               Nothing), , False)
            spw.AgregarParam("par$fyhArmadoHasta", ProsegurDbType.Data_Hora, If(peticion.Contenedor IsNot Nothing AndAlso peticion.Contenedor.fechaHoraArmadoHasta IsNot Nothing,
                                                                                peticion.Contenedor.fechaHoraArmadoHasta,
                                                                                Nothing), , False)
            spw.AgregarParam("par$bolmayorNivel", ProsegurDbType.Inteiro_Curto, If(peticion.Contenedor IsNot Nothing,
                                                                                   peticion.Contenedor.bolMayorNivel,
                                                                                   False), , False)
            spw.AgregarParam("par$bolPackModular", ProsegurDbType.Inteiro_Curto, If(peticion.Contenedor IsNot Nothing,
                                                                                    peticion.Contenedor.packModular,
                                                                                    False), , False)
            spw.AgregarParam("par$bolretornarElementosHijos", ProsegurDbType.Inteiro_Curto, If(peticion.Contenedor IsNot Nothing,
                                                                                               peticion.Contenedor.bolRetornarElementosHijos,
                                                                                               False), , False)

            spw.AgregarParam("par$aPrecintos", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            spw.AgregarParam("par$codCliente", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Cliente IsNot Nothing,
                                                                                             peticion.Cliente.codCliente,
                                                                                             Nothing), , False)
            spw.AgregarParam("par$codSubCliente", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Cliente IsNot Nothing,
                                                                                             peticion.Cliente.codSubcliente,
                                                                                             Nothing), , False)
            spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Cliente IsNot Nothing,
                                                                                             peticion.Cliente.codPuntoServicio,
                                                                                             Nothing), , False)
            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion, , False)
            spw.AgregarParam("par$codPlanta", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta, , False)
            spw.AgregarParam("par$acodSector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$codCanal", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Canal IsNot Nothing,
                                                                                             peticion.Canal.codCanal,
                                                                                             Nothing), , False)
            spw.AgregarParam("par$codSubcanal", ProsegurDbType.Identificador_Alfanumerico, If(peticion.Canal IsNot Nothing,
                                                                                             peticion.Canal.codSubcanal,
                                                                                             Nothing), , False)


            spw.AgregarParam("par$bolRecSoloContSinPosicion", ProsegurDbType.Logico, peticion.RecuperarSoloContenedoresSinPosicion, , False)

            'Retorno
            spw.AgregarParam("par$rcContenedores", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CONTENEDORES")
            spw.AgregarParam("par$rcEfectivos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "EFECTIVOS")
            spw.AgregarParam("par$rcMediosPago", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "MEDIOS_PAGO")

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            ' VALORES ARRAY
            If peticion.Precintos IsNot Nothing AndAlso peticion.Precintos.Count > 0 Then
                For Each prec In peticion.Precintos
                    spw.Param("par$aPrecintos").AgregarValorArray(prec)
                Next
            End If

            'Canais
            If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                For Each sector In peticion.Sectores
                    spw.Param("par$acodSector").AgregarValorArray(sector.codSector)
                Next
            End If



            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                       "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                       "par$cod_transaccion", "par$cod_resultado")


            Return spw
        End Function

        Public Shared Function ConsultarContenedoresPackModular(Peticion As Contenedores.ConsultarContenedoresPackModular.Peticion) As DataSet

            Dim spw As SPWrapper = ColectarContenedoresPackModular(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        Private Shared Function ColectarContenedoresPackModular(peticion As Contenedores.ConsultarContenedoresPackModular.Peticion) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.SP_CONSULTARCONTPACKMODULAR", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            If String.IsNullOrEmpty(peticion.vencidos) Then
                spw.AgregarParam("par$vencidos", ProsegurDbType.Inteiro_Longo, Nothing, , False)
            Else
                spw.AgregarParam("par$vencidos", ProsegurDbType.Inteiro_Longo, peticion.vencidos, , False)
            End If
            If peticion.fechaVencimiento = DateTime.MinValue Then
                spw.AgregarParam("par$fechaVencimiento", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fechaVencimiento", ProsegurDbType.Data_Hora, peticion.fechaVencimiento, , False)
            End If
            spw.AgregarParam("par$codTipoSector", ProsegurDbType.Descricao_Longa, Nothing, , True)
            'Sectores
            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, Nothing, , True)
            'Cliente
            If peticion.cliente IsNot Nothing Then
                spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, peticion.cliente.codCliente, , False)
                spw.AgregarParam("par$codSubCliente", ProsegurDbType.Descricao_Longa, peticion.cliente.codSubcliente, , False)
                spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Descricao_Longa, peticion.cliente.codPuntoServicio, , False)
            Else
                spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codSubCliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Descricao_Longa, Nothing, , False)
            End If
            'Canal
            If peticion.canal IsNot Nothing Then
                spw.AgregarParam("par$codCanal", ProsegurDbType.Descricao_Longa, peticion.canal.codCanal, , False)
                spw.AgregarParam("par$codSubcanal", ProsegurDbType.Descricao_Longa, peticion.canal.codSubcanal, , False)
            Else
                spw.AgregarParam("par$codCanal", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codSubcanal", ProsegurDbType.Descricao_Longa, Nothing, , False)
            End If

            'Retorno
            spw.AgregarParam("par$rcContenedores", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CONTENEDORES")
            spw.AgregarParam("par$rcAlertas", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ALERTAS")
            spw.AgregarParam("par$rcClientes", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CLIENTES")
            spw.AgregarParam("par$rcPrecintos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "PRECINTOS")
            spw.AgregarParam("par$rcDetalleEfec", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFEC")

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            ' VALORES ARRAY
            'Sectores
            If peticion.sectores IsNot Nothing AndAlso peticion.sectores.Count > 0 Then
                For Each sector In peticion.sectores
                    If Not String.IsNullOrEmpty(sector.codSector) Then
                        spw.Param("par$codSector").AgregarValorArray(sector.codSector)
                    End If
                    If Not String.IsNullOrEmpty(sector.codDelegacion) Then
                        spw.Param("par$codDelegacion").AgregarValorArray(sector.codDelegacion)
                    End If
                    If Not String.IsNullOrEmpty(sector.codPlanta) Then
                        spw.Param("par$codPlanta").AgregarValorArray(sector.codPlanta)
                    End If
                Next
            End If

            'TipoSector
            If peticion.tiposSectores IsNot Nothing AndAlso peticion.tiposSectores.Count > 0 Then
                For Each tipoSector In peticion.tiposSectores
                    spw.Param("par$codTipoSector").AgregarValorArray(tipoSector)
                Next
            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Public Shared Function ConsultarDocumentosGestionContenedores(Peticion As Contenedores.ConsultarDocumentosGestionContenedores.Peticion) As DataSet

            Dim spw As SPWrapper = ColectarContenedoresDocumentosGestionContenedores(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        Private Shared Function ColectarContenedoresDocumentosGestionContenedores(peticion As Contenedores.ConsultarDocumentosGestionContenedores.Peticion) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.SP_CONSULTARDOCGESTIONCONT", Prosegur.Genesis.Comon.Util.Version)


            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$codIdioma", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            If peticion.Documento IsNot Nothing Then
                If peticion.Documento.FechaDocumentoDesde > DateTime.MinValue Then
                    spw.AgregarParam("par$fechaDocumentoDesde", ProsegurDbType.Data_Hora, peticion.Documento.FechaDocumentoDesde, , False)
                Else
                    spw.AgregarParam("par$fechaDocumentoDesde", ProsegurDbType.Data_Hora, Nothing, , False)
                End If
                If peticion.Documento.FechaDocumentoHasta > DateTime.MinValue Then
                    spw.AgregarParam("par$fechaDocumentoHasta", ProsegurDbType.Data_Hora, peticion.Documento.FechaDocumentoHasta, , False)
                Else
                    spw.AgregarParam("par$fechaDocumentoHasta", ProsegurDbType.Data_Hora, Nothing, , False)
                End If
                spw.AgregarParam("par$codEstadoDocumento", ProsegurDbType.Identificador_Alfanumerico, peticion.Documento.CodEstadoDocumento, , False)

                If peticion.Documento.Cuenta IsNot Nothing Then

                    With peticion.Documento.Cuenta

                        If .Sector IsNot Nothing AndAlso .Sector.Delegacion IsNot Nothing AndAlso .Sector.Planta IsNot Nothing Then
                            spw.AgregarParam("par$codSector", ProsegurDbType.Identificador_Alfanumerico, .Sector.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codSector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        If .Sector.Delegacion IsNot Nothing Then
                            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Identificador_Alfanumerico, .Sector.Delegacion.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        If .Sector.Planta IsNot Nothing Then
                            spw.AgregarParam("par$codPlanta", ProsegurDbType.Identificador_Alfanumerico, .Sector.Planta.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codPlanta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        'Cliente
                        If .Cliente IsNot Nothing Then
                            spw.AgregarParam("par$codCliente", ProsegurDbType.Identificador_Alfanumerico, .Cliente.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codCliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        If .SubCliente IsNot Nothing Then
                            spw.AgregarParam("par$codSubCliente", ProsegurDbType.Identificador_Alfanumerico, .SubCliente.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codSubCliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        If .PuntoServicio IsNot Nothing Then
                            spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Identificador_Alfanumerico, .PuntoServicio.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        'Canal
                        If .Canal IsNot Nothing Then
                            spw.AgregarParam("par$codCanal", ProsegurDbType.Identificador_Alfanumerico, .Canal.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codCanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                        If .SubCanal IsNot Nothing Then
                            spw.AgregarParam("par$codSubcanal", ProsegurDbType.Identificador_Alfanumerico, .SubCanal.Codigo, , False)
                        Else
                            spw.AgregarParam("par$codSubcanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                        End If

                    End With

                End If

                'Contenedor
                spw.AgregarParam("par$codPrecinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

                If peticion.Documento IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.Documento.Precinto) Then
                    spw.Param("par$codPrecinto").AgregarValorArray(peticion.Documento.Precinto)
                End If

                If peticion.Documento.Contenedor IsNot Nothing Then

                    spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Identificador_Alfanumerico, peticion.Documento.Contenedor.CodTipoContenedor, , False)

                    'Precintos
                    If peticion.Documento.Contenedor.Precintos IsNot Nothing AndAlso peticion.Documento.Contenedor.Precintos.Count > 0 Then

                        For Each precinto In peticion.Documento.Contenedor.Precintos
                            spw.Param("par$codPrecinto").AgregarValorArray(precinto)
                        Next

                    End If
                Else
                    spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                End If

                If Not String.IsNullOrEmpty(peticion.Documento.OrigenBusqueda) Then
                    spw.AgregarParam("par$codOrigenBusqueda", ProsegurDbType.Identificador_Alfanumerico, peticion.Documento.OrigenBusqueda, , False)
                Else
                    spw.AgregarParam("par$codOrigenBusqueda", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                End If


            Else
                spw.AgregarParam("par$fechaDocumentoDesde", ProsegurDbType.Data_Hora, Nothing, , False)
                spw.AgregarParam("par$fechaDocumentoHasta", ProsegurDbType.Data_Hora, Nothing, , False)
                spw.AgregarParam("par$codEstadoDocumento", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codSector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codDelegacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codPlanta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codCliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codSubCliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codPtoServicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codCanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codSubcanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codTipoContenedor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codOrigenBusqueda", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$codPrecinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            End If

            'Retorno
            spw.AgregarParam("par$rcDocumentos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DOCUMENTOS")
            spw.AgregarParam("par$rcPrecintos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "PRECINTOS")
            'spw.AgregarParam("par$rcDetalleEfecMP", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMP")
            spw.AgregarParam("par$rcDetalleEfecMPDocumento", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMPDOC")

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                     "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                     "par$cod_transaccion", "par$cod_resultado")


            Return spw
        End Function

        Public Shared Function ConsultarSeguimientoElemento(Peticion As Contenedores.ConsultarSeguimientoElemento.Peticion) As DataSet

            Try
                Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.s_ins_tconsultarelemento", Prosegur.Genesis.Comon.Util.Version)

                Dim spw As New SPWrapper(SP)

                spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                    Tradutor.CulturaSistema.Name,
                                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

                spw.AgregarParam("par$codTipoElemento", ProsegurDbType.Descricao_Longa, Peticion.Elemento.codTipoElemento, , False)
                spw.AgregarParam("par$oidElemento", ProsegurDbType.Descricao_Longa, Peticion.Elemento.oidElemento, , False)
                spw.AgregarParam("par$codPrecinto", ProsegurDbType.Descricao_Longa, Peticion.Elemento.codPrecinto, , False)

                'Retorno
                spw.AgregarParam("par$rcElementos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ELEMENTOS")
                spw.AgregarParam("par$rcDocumentos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DOCUMENTOS")
                spw.AgregarParam("par$rc_precintos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "PRECINTOS")
                spw.AgregarParam("par$rcCuentas", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "CUENTAS")
                spw.AgregarParam("par$rcAcciones", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ACIONES")

                Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

            Catch ex As System.Exception
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, ex.InnerException.InnerException.ToString)
            End Try


        End Function

        Public Shared Function ConsultarInventarioContenedor(Peticion As Contenedores.ConsultarInventarioContenedor.Peticion) As DataSet

            Dim spw As SPWrapper = ColectarConsultarInventario(Peticion)
            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        ''' <summary>
        ''' Obtem la fecha creacion del contenedor para impressión de la etiqueta
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerFechaCreacionContenedor(identificadorDocumento As String)
            Dim fechaCreacionContenedor As DateTime
            Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerFechaCreacionContenedor)
                command.CommandType = CommandType.Text
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

                fechaCreacionContenedor = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command)

            End Using
            Return fechaCreacionContenedor
        End Function

#Region "ConsultarContenedoresPorFIFO"

        Public Shared Function ConsultarContenedoresPorFIFO(codigoTipoContenedor As String, identificadorDelegacion As String,
                                                            identificadorPlanta As String, identificadorSector As String,
                                                            identificadorCliente As String, identificadorSubCliente As String,
                                                            identificadorPuntoServicio As String, identificadorCanal As String,
                                                            identificadorSubCanal As String, identificadorIsoDivisa As String,
                                                            identificadorDenominacion As String, importe As Decimal, CodCanal As String, codSubCanal As String, CodigoUsuario As String) As DataSet

            Dim spw As SPWrapper = ColectarContenedoresPorFIFO(codigoTipoContenedor, identificadorDelegacion, identificadorPlanta, identificadorSector,
                                                               identificadorCliente, identificadorSubCliente, identificadorPuntoServicio, identificadorCanal,
                                                               identificadorSubCanal, identificadorIsoDivisa, identificadorDenominacion, importe, CodCanal, codSubCanal, CodigoUsuario)

            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

        Private Shared Function ColectarContenedoresPorFIFO(codigoTipoContenedor As String, identificadorDelegacion As String,
                                                            identificadorPlanta As String, identificadorSector As String,
                                                            identificadorCliente As String, identificadorSubCliente As String,
                                                            identificadorPuntoServicio As String, identificadorCanal As String,
                                                            identificadorSubCanal As String, identificadorDivisa As String,
                                                            identificadorDenominacion As String, importe As Decimal,
                                                            codCanal As String, codSubCanal As String, CodigoUsuario As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.srecuperar_contenedores_fifo", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$cod_tipo_contenedor", ProsegurDbType.Identificador_Alfanumerico, codigoTipoContenedor, , False)
            spw.AgregarParam("par$oid_delegacion", ProsegurDbType.Objeto_Id, identificadorDelegacion, , False)
            spw.AgregarParam("par$oid_planta", ProsegurDbType.Objeto_Id, identificadorPlanta, , False)
            spw.AgregarParam("par$oid_sector", ProsegurDbType.Objeto_Id, identificadorSector, , False)
            spw.AgregarParam("par$oid_cliente", ProsegurDbType.Objeto_Id, identificadorCliente, , False)
            spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Objeto_Id, identificadorSubCliente, , False)
            spw.AgregarParam("par$oid_pto_servicio", ProsegurDbType.Objeto_Id, identificadorPuntoServicio, , False)
            spw.AgregarParam("par$oid_canal", ProsegurDbType.Objeto_Id, identificadorCanal, , False)
            spw.AgregarParam("par$oid_subcanal", ProsegurDbType.Objeto_Id, identificadorSubCanal, , False)
            spw.AgregarParam("par$oid_divisa", ProsegurDbType.Objeto_Id, identificadorDivisa, , False)
            spw.AgregarParam("par$oid_denominacion", ProsegurDbType.Objeto_Id, identificadorDenominacion, , False)
            spw.AgregarParam("par$importe", ProsegurDbType.Numero_Decimal, importe, , False)
            spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                    Tradutor.CulturaSistema.Name,
                                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$cod_canal", ProsegurDbType.Objeto_Id, codCanal, , False)
            spw.AgregarParam("par$cod_subcanal", ProsegurDbType.Objeto_Id, codSubCanal, , False)
            spw.AgregarParam("par$rc_contenedores", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, Prosegur.Genesis.Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_CONTENEDORES)
            spw.AgregarParam("par$rc_precintos", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, Prosegur.Genesis.Comon.Constantes.CONST_RECUPERAR_CONTENEDORES_FIFO_TABLA_RC_PRECINTOS)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario, , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                    "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                    "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region

#End Region

#Region "[INSERIR]/[ATUALIZAR]"

        Public Shared Sub DefinirCambiarExtraerPosicionContenedor(Peticion As Contenedores.DefinirCambiarExtraerPosicionContenedor.Peticion)
            Try
                Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.S_UPD_TPOSICION_SECTOR", Prosegur.Genesis.Comon.Util.Version)

                Dim spw As New SPWrapper(SP, True)

                spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                    Tradutor.CulturaSistema.Name,
                                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

                spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, Peticion.Sector.codDelegacion, , False)
                spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, Peticion.Sector.codPlanta, , False)
                spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, Peticion.Sector.codSector, , False)
                spw.AgregarParam("par$acont_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$acont_cod_posicion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$acont_cod_posicion_destino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$UsuarioCreacion", ProsegurDbType.Descricao_Longa, Peticion.UsuarioCreacion, , False)
                spw.AgregarParam("par$hacer_Commit", ProsegurDbType.Inteiro_Curto, 1, , False)

                spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
                spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
                spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
                spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

                If Peticion.Contenedores IsNot Nothing AndAlso Peticion.Contenedores.Count > 0 Then

                    For Each Contenedor In Peticion.Contenedores

                        spw.Param("par$acont_cod_precinto").AgregarValorArray(Contenedor.Posicion.codPrecinto)
                        spw.Param("par$acont_cod_posicion").AgregarValorArray(Contenedor.Posicion.codPosicion)
                        spw.Param("par$acont_cod_posicion_destino").AgregarValorArray(Contenedor.Posicion.codPosicionDestino)

                    Next

                End If

                spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                           "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                           "par$cod_transaccion", "par$cod_resultado")

                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
            Catch ex As System.Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Public Shared Sub GrabarAlertaVencimiento(Peticion As Contenedores.GrabarAlertaVencimiento.Peticion)

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.S_INS_TALERTA_VENC_CONTENEDOR", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, True)

            'spw.AgregarParam("par$codIdioma", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
            '                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
            '                                                                    Tradutor.CulturaSistema.Name,
            '                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$codPrecinto", ProsegurDbType.Descricao_Longa, Peticion.Contenedor.codPrecinto, , False)
            spw.AgregarParam("par$fechaAlerta", ProsegurDbType.Data_Hora, Peticion.Contenedor.AlertaVencimento.fechaAlerta, , False)
            spw.AgregarParam("par$codTipoAlerta", ProsegurDbType.Descricao_Longa, Peticion.Contenedor.AlertaVencimento.codTipoAlerta, , False)
            spw.AgregarParam("par$diasVencer", ProsegurDbType.Inteiro_Curto, Peticion.Contenedor.AlertaVencimento.diasVencer, , False)
            spw.AgregarParam("par$Emails", ProsegurDbType.Descricao_Longa, Peticion.Contenedor.AlertaVencimento.emails, , False)
            spw.AgregarParam("par$UsuarioCreacion", ProsegurDbType.Descricao_Longa, Peticion.Contenedor.UsuarioCreacion, , False)
            spw.AgregarParam("par$hacer_Commit", ProsegurDbType.Inteiro_Curto, 1, , False)


            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Sub

        Public Shared Function GrabarInventarioContenedor(Peticion As Contenedores.GrabarInventarioContenedor.Peticion) As DataSet

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.s_ins_tinventario_contenedor", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP)

            spw.AgregarParam("par$codIdioma", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$codInventario", ProsegurDbType.Descricao_Longa, Peticion.Inventario.codInventario, , False)
            spw.AgregarParam("par$regresarDetalles", ProsegurDbType.Inteiro_Curto, Peticion.Inventario.regresarDetalles, , False)
            spw.AgregarParam("par$codDelegacion", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Sector.codDelegacion, , False)
            spw.AgregarParam("par$codPlanta", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Sector.codPlanta, , False)
            spw.AgregarParam("par$codSector", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Sector.codSector, , False)
            If Peticion.Inventario.Cliente Is Nothing Then
                spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codSubcliente", ProsegurDbType.Descricao_Longa, Nothing, , False)
                spw.AgregarParam("par$codPuntoServicio", ProsegurDbType.Descricao_Longa, Nothing, , False)
            Else
                spw.AgregarParam("par$codCliente", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Cliente.codCliente, , False)
                spw.AgregarParam("par$codSubcliente", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Cliente.codSubcliente, , False)
                spw.AgregarParam("par$codPuntoServicio", ProsegurDbType.Descricao_Longa, Peticion.Inventario.Cliente.codPuntoServicio, , False)
            End If

            spw.AgregarParam("par$cods_precintos", ProsegurDbType.Objeto_Id, Nothing, , True)

            If Peticion.Inventario.codigosPrecintos IsNot Nothing AndAlso
                Peticion.Inventario.codigosPrecintos.Count > 0 Then

                For Each precinto In Peticion.Inventario.codigosPrecintos
                    spw.Param("par$cods_precintos").AgregarValorArray(precinto)
                Next
            End If

            spw.AgregarParam("par$UsuarioCreacion", ProsegurDbType.Descricao_Longa, Peticion.Inventario.UsuarioCreacion, , False)
            spw.AgregarParam("par$hacer_Commit", ProsegurDbType.Inteiro_Curto, 1, , False)

            'Retorno
            spw.AgregarParam("par$rc_Inventario", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "INVENTARIO")
            spw.AgregarParam("par$rc_detalle_efec_mp", ParamWrapper.ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "DETEFECMP")

            Return AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Function

#End Region

#Region "ReenvioEntreSectores"

        Public Shared Sub ReenvioEntreSectores(Documento As Contenedores.ReenvioEntreSectores.Documento,
                                               ByRef CodigoComprobante As String, ByRef IdentificadorDocumento As String)

            Try

                Dim spw As SPWrapper = ColectarDocumentosReenvioEntreSectores(Documento)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                CodigoComprobante = If(spw.Param("par$cod_comprobante").Valor IsNot Nothing, spw.Param("par$cod_comprobante").Valor.ToString, String.Empty)
                IdentificadorDocumento = If(spw.Param("par$oid_documento").Valor IsNot Nothing, spw.Param("par$oid_documento").Valor.ToString, String.Empty)

            Catch ex As Exception

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

        End Sub

        Private Shared Function ColectarDocumentosReenvioEntreSectores(documento As Contenedores.ReenvioEntreSectores.Documento) As SPWrapper

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.sreenvio_entre_sectores", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, True)


            spw.AgregarParam("par$cod_formulario", ProsegurDbType.Identificador_Alfanumerico, documento.CodigoFormulario, , False)

            If documento.FechaHoraPlanCertificado = Date.MinValue Then
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, documento.FechaHoraPlanCertificado, , False)
            End If

            If documento.FechaHoraGestion = Date.MinValue Then
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, documento.FechaHoraGestion, , False)
            End If

            spw.AgregarParam("par$oid_delegacion_ori", ProsegurDbType.Objeto_Id, documento.SectorOrigen.IdentificadorDelegacion, , False)
            spw.AgregarParam("par$oid_planta_ori", ProsegurDbType.Objeto_Id, documento.SectorOrigen.IdentificadorPlanta, , False)
            spw.AgregarParam("par$oid_sector_ori", ProsegurDbType.Objeto_Id, documento.SectorOrigen.IdentificadorSector, , False)

            spw.AgregarParam("par$oid_delegacion_des", ProsegurDbType.Objeto_Id, documento.SectorDestino.IdentificadorDelegacion, , False)
            spw.AgregarParam("par$oid_planta_des", ProsegurDbType.Objeto_Id, documento.SectorDestino.IdentificadorPlanta, , False)
            spw.AgregarParam("par$oid_sector_des", ProsegurDbType.Objeto_Id, documento.SectorDestino.IdentificadorSector, , False)

            spw.AgregarParam("par$acosoid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$acoscod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acosrowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, documento.CodigoUsuario.ToUpper, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$oid_documento", ProsegurDbType.Objeto_Id, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$hacer_commit", ProsegurDbType.Inteiro_Longo, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            If documento.Contenedores IsNot Nothing AndAlso documento.Contenedores.Count > 0 Then

                For Each contenedor In documento.Contenedores

                    If Not String.IsNullOrEmpty(contenedor.IdentificadorContenedor) Then
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(contenedor.IdentificadorContenedor)
                        spw.Param("par$acoscod_precinto").AgregarValorArray(Nothing)
                    ElseIf Not String.IsNullOrEmpty(contenedor.CodigoPrecinto) Then
                        spw.Param("par$acoscod_precinto").AgregarValorArray(contenedor.CodigoPrecinto)
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(Nothing)

                    End If

                Next

            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region

#Region "ReenvioEntreClientes"

        Public Shared Sub ReenvioEntreClientes(Documento As Contenedores.ReenvioEntreClientes.Documento,
                                               ByRef CodigoComprobante As String, ByRef IdentificadorDocumento As String)

            Try

                Dim spw As SPWrapper = ColectarDocumentosReenvioEntreClientes(Documento)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                CodigoComprobante = If(spw.Param("par$cod_comprobante").Valor IsNot Nothing, spw.Param("par$cod_comprobante").Valor.ToString, String.Empty)
                IdentificadorDocumento = If(spw.Param("par$oid_documento").Valor IsNot Nothing, spw.Param("par$oid_documento").Valor.ToString, String.Empty)

            Catch ex As Exception

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

        End Sub

        Private Shared Function ColectarDocumentosReenvioEntreClientes(documento As Contenedores.ReenvioEntreClientes.Documento) As SPWrapper

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.sreenvio_entre_clientes", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, True)


            spw.AgregarParam("par$cod_formulario", ProsegurDbType.Identificador_Alfanumerico, documento.CodigoFormulario, , False)

            If documento.FechaHoraPlanCertificado = Date.MinValue Then
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, documento.FechaHoraPlanCertificado, , False)
            End If

            If documento.FechaHoraGestion = Date.MinValue Then
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, documento.FechaHoraGestion, , False)
            End If

            spw.AgregarParam("par$oid_delegacion_ori", ProsegurDbType.Objeto_Id, documento.Sector.IdentificadorDelegacion, , False)
            spw.AgregarParam("par$oid_planta_ori", ProsegurDbType.Objeto_Id, documento.Sector.IdentificadorPlanta, , False)
            spw.AgregarParam("par$oid_sector_ori", ProsegurDbType.Objeto_Id, documento.Sector.IdentificadorSector, , False)

            spw.AgregarParam("par$oid_cliente_des", ProsegurDbType.Objeto_Id, documento.ClienteDestino.IdentificadorCliente, , False)
            spw.AgregarParam("par$oid_subcliente_des", ProsegurDbType.Objeto_Id, documento.ClienteDestino.IdentificadorSubCliente, , False)
            spw.AgregarParam("par$oid_pto_servicio_des", ProsegurDbType.Objeto_Id, documento.ClienteDestino.IdentificadorPuntoServicio, , False)

            spw.AgregarParam("par$acosoid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$acoscod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acosrowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, documento.CodigoUsuario.ToUpper, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$oid_documento", ProsegurDbType.Objeto_Id, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$hacer_commit", ProsegurDbType.Inteiro_Longo, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            If documento.Contenedores IsNot Nothing AndAlso documento.Contenedores.Count > 0 Then

                For Each contenedor In documento.Contenedores

                    If Not String.IsNullOrEmpty(contenedor.IdentificadorContenedor) Then
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(contenedor.IdentificadorContenedor)
                        spw.Param("par$acoscod_precinto").AgregarValorArray(Nothing)
                    ElseIf Not String.IsNullOrEmpty(contenedor.CodigoPrecinto) Then
                        spw.Param("par$acoscod_precinto").AgregarValorArray(contenedor.CodigoPrecinto)
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(Nothing)

                    End If

                Next

            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                           "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                           "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region

#Region "DesarmarContenedores"

        Public Shared Sub DesarmarContenedores(documento As ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento,
                                               ByRef CodigoComprobante As String, ByRef IdentificadorDocumento As String)

            Try

                Dim spw As SPWrapper = ColectarDocumentosDesarmarContenedores(documento)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                CodigoComprobante = If(spw.Param("par$cod_comprobante").Valor IsNot Nothing, spw.Param("par$cod_comprobante").Valor.ToString, String.Empty)
                IdentificadorDocumento = If(spw.Param("par$oid_documento").Valor IsNot Nothing, spw.Param("par$oid_documento").Valor.ToString, String.Empty)

            Catch ex As Exception

                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

        End Sub

        Private Shared Function ColectarDocumentosDesarmarContenedores(documento As ContractoServicio.GenesisSaldos.Contenedores.Comon.Documento) As SPWrapper

            Dim SP As String = String.Format("SAPR_PCONTENEDOR_{0}.sdesarmar_contenedor", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, True)


            spw.AgregarParam("par$cod_formulario", ProsegurDbType.Identificador_Alfanumerico, documento.CodFormulario, , False)

            If documento.FechaPlanCertificacion = Date.MinValue Then
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, documento.FechaPlanCertificacion, , False)
            End If

            If documento.FechaGestion = Date.MinValue Then
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, documento.FechaGestion, , False)
            End If

            spw.AgregarParam("par$oid_delegacion", ProsegurDbType.Objeto_Id, documento.SectorOrigen.Delegacion.Identificador, , False)
            spw.AgregarParam("par$oid_planta", ProsegurDbType.Objeto_Id, documento.SectorOrigen.Planta.Identificador, , False)
            spw.AgregarParam("par$oid_sector", ProsegurDbType.Objeto_Id, documento.SectorOrigen.Identificador, , False)
            spw.AgregarParam("par$acosoid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$acoscod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, documento.CodUsuario.ToUpper, , False)
            spw.AgregarParam("par$codcultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                Tradutor.CulturaSistema.Name,
                                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$oid_documento", ProsegurDbType.Objeto_Id, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$hacer_commit", ProsegurDbType.Inteiro_Longo, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)


            If documento.Contenedores IsNot Nothing AndAlso documento.Contenedores.Count > 0 Then

                For Each contenedor In documento.Contenedores

                    If Not String.IsNullOrEmpty(contenedor.IdentificadorContenedor) Then
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(contenedor.IdentificadorContenedor)
                        spw.Param("par$acoscod_precinto").AgregarValorArray(Nothing)
                    ElseIf contenedor.Precintos IsNot Nothing AndAlso contenedor.Precintos.Count > 0 AndAlso Not String.IsNullOrWhiteSpace(contenedor.Precintos.First) Then
                        spw.Param("par$acoscod_precinto").AgregarValorArray(contenedor.Precintos.First)
                        spw.Param("par$acosoid_contenedor").AgregarValorArray(Nothing)

                    End If

                Next

            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                            "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                            "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region

#Region "ObtenerPosicionesSector"

        Public Shared Function ObtenerPosicionesSector(identificadorSector As String) As ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerPosicionesSector)
                    command.CommandType = CommandType.Text
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, identificadorSector))

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)
                    objRespuesta = RellenarObtenerPosicionesSector(dt)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try
            Return objRespuesta
        End Function
        Private Shared Function RellenarObtenerPosicionesSector(dt As DataTable)
            Dim objRespuesta As New ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta
            Try
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objRespuesta.PosicionesSector = New List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.PosicionSector)
                    For Each row In dt.Rows
                        Dim posicion As New ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.PosicionSector
                        posicion.OidPosicionSector = Util.AtribuirValorObj(row("OID_POSICION_SECTOR"), GetType(String))
                        posicion.OidSector = Util.AtribuirValorObj(row("OID_SECTOR"), GetType(String))
                        posicion.CodigoPosicion = Util.AtribuirValorObj(row("COD_POSICION"), GetType(String))
                        posicion.OidContenedor = Util.AtribuirValorObj(row("OID_CONTENEDOR"), GetType(String))
                        posicion.Activo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        objRespuesta.PosicionesSector.Add(posicion)
                    Next
                End If
                Return objRespuesta
            Catch ex As Exception
                Throw
            End Try
        End Function
#End Region

    End Class

End Namespace