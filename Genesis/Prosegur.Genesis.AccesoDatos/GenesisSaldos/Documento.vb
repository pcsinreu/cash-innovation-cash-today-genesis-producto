Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.AccesoDatos.Genesis
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports System.Threading.Tasks
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe Documento
    ''' </summary>
    Public Class Documento

#Region " Procedure - Cargar"

        Public Shared Sub GrabarDocumento(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                          bol_gestion_bulto As Boolean,
                                          hacer_commit As Boolean,
                                          confirmar_doc As Boolean,
                                          caracteristica_integracion As Enumeradores.CaracteristicaFormulario?,
                                          ByRef TransaccionActual As DataBaseHelper.Transaccion)

            Try
                If documentos Is Nothing OrElse documentos.Count = 0 Then
                    Exit Sub
                End If

                Dim Caracteristicas As List(Of Enumeradores.CaracteristicaFormulario) = documentos.FirstOrDefault.Formulario.Caracteristicas

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                If Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                    spw = ColectarAltas(documentos, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion)

                ElseIf Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Actas) Then
                    spw = ColectarActas(documentos, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion)

                ElseIf Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) OrElse
                   Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas) Then
                    spw = ColectarReenvioBaja(documentos, hacer_commit, confirmar_doc)

                ElseIf Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                    spw = ColectarValores(documentos, hacer_commit, confirmar_doc)

                ElseIf Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    spw = ColectarSustitucion(documentos, bol_gestion_bulto, hacer_commit, confirmar_doc)

                End If

                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                If confirmar_doc AndAlso ds IsNot Nothing Then
                    PoblarDocumentos_Confirmar(documentos, ds)
                End If

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

        Private Shared Function ColectarValores(documentos As ObservableCollection(Of Clases.Documento),
                                              hacer_commit As Boolean,
                                              confirmar_doc As Boolean) As SPWrapper

            Dim identificadorFormulario As String = documentos(0).Formulario.Identificador
            Dim identificadorGrupoDocumento As String = documentos(0).IdentificadorGrupo
            Dim usuario As String = documentos(0).UsuarioModificacion

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_valores", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, identificadorFormulario, , False)
            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            ' arrays asociativos
            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' cuenta origen
            spw.AgregarParam("par$adocs_mov_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' cuenta destino
            spw.AgregarParam("par$adocs_mov_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' arrays de efectivo por documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago por documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago por documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de efectivo ANTERIORES por documento
            spw.AgregarParam("par$aefadoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefadoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefadoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefadoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefadoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefadoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefadoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefadoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefadoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago ANTERIORES por documento
            spw.AgregarParam("par$ampadoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampadoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampadoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampadoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampadoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampadoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampadoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago ANTERIORES por documento
            spw.AgregarParam("par$avtmpadoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpadoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpadoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpadoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos por documento
            spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Colectar_Documentos(documentos, spw)

            Return spw

        End Function

        Private Shared Function ColectarAltas(documentos As ObservableCollection(Of Clases.Documento),
                                              bol_gestion_bulto As Boolean,
                                              hacer_commit As Boolean,
                                              confirmar_doc As Boolean,
                                     Optional caracteristica_integracion As Enumeradores.CaracteristicaFormulario? = Nothing) As SPWrapper

            Dim identificadorGrupoDocumento As String = documentos(0).IdentificadorGrupo
            Dim usuario As String = documentos(0).UsuarioModificacion

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_elem_altas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            If documentos(0).Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(documentos(0).Formulario.Identificador) Then
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, documentos(0).Formulario.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            spw.AgregarParam("par$bol_gestion_bulto", ProsegurDbType.Objeto_Id, If(bol_gestion_bulto, 1, 0), , False)
            If caracteristica_integracion IsNot Nothing Then
                Dim _integracion As Enumeradores.CaracteristicaFormulario = caracteristica_integracion
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, _integracion.RecuperarValor(), , False)
            Else
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            ' arrays asociativos
            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' cuenta origen
            spw.AgregarParam("par$adocs_mov_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' cuenta destino
            spw.AgregarParam("par$adocs_mov_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' arrays de efectivo por documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago por documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago por documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos por documento
            spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' Remesa
            spw.AgregarParam("par$aremdoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_recibo_salida", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_ruta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_nel_parada", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_transporte", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_nel_cant_bultos", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_cajero", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_estado_abono", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' Remesa - arrays de terminos por remesa
            spw.AgregarParam("par$avtrem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac_parciales", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_bolsa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_banco_ingreso", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_banco_ing_mad", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_nel_cant_parciales", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_proceso_leg", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_cuadrado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_servicio", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Bulto - arrays de terminos por bulto
            spw.AgregarParam("par$avtbul_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Parcial - arrays
            spw.AgregarParam("par$apardoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_nec_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Parcial - arrays de terminos por parcial
            spw.AgregarParam("par$avtpar_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Elemento - Declarado Efectivo
            spw.AgregarParam("par$aelemvalefe_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_esbillete", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_tipo_efec", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Elemento - Declarado Medio Pago
            spw.AgregarParam("par$aelemval_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_tipo_mp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' Elemento - Terminos medio pago
            spw.AgregarParam("par$aelemter_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_nel_ind_grupo", ProsegurDbType.Numero_Decimal, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Colectar_Documentos(documentos, spw)

            Return spw

        End Function

        Private Shared Function ColectarReenvioBaja(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                                    hacer_commit As Boolean,
                                                    confirmar_doc As Boolean) As SPWrapper


            Dim identificadorFormulario As String = documentos(0).Formulario.Identificador
            Dim identificadorGrupoDocumento As String = documentos(0).IdentificadorGrupo
            Dim usuario As String = documentos(0).UsuarioModificacion

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_elem_reenviobaja", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, identificadorFormulario, , False)
            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            ' arrays asociativos
            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' cuenta origen
            spw.AgregarParam("par$adocs_mov_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' cuenta destino
            spw.AgregarParam("par$adocs_mov_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' arrays de efectivo por documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago por documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago por documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' Remesa
            spw.AgregarParam("par$aremdoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Colectar_Documentos(documentos, spw)

            Return spw
        End Function

        Private Shared Function ColectarActas(documentos As ObservableCollection(Of Clases.Documento),
                                              bol_gestion_bulto As Boolean,
                                              hacer_commit As Boolean,
                                              confirmar_doc As Boolean,
                                     Optional caracteristica_integracion As Enumeradores.CaracteristicaFormulario? = Nothing) As SPWrapper

            Dim identificadorFormulario As String = documentos(0).Formulario.Identificador
            Dim identificadorGrupoDocumento As String = documentos(0).IdentificadorGrupo
            Dim usuario As String = documentos(0).UsuarioModificacion

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_elem_actas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$bol_gestion_bulto", ProsegurDbType.Objeto_Id, If(bol_gestion_bulto, 1, 0), , False)
            If String.IsNullOrEmpty(identificadorFormulario) Then
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, identificadorFormulario, , False)
            Else
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            If caracteristica_integracion IsNot Nothing Then
                Dim _integracion As Enumeradores.CaracteristicaFormulario = caracteristica_integracion
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, _integracion.RecuperarValor(), , False)
            Else
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            ' arrays asociativos
            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de efectivo por documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago por documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago por documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos por documento
            spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' Remesa
            spw.AgregarParam("par$aremdoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Remesa - arrays de terminos por remesa
            spw.AgregarParam("par$avtrem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac_parciales", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_bolsa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_banco_ingreso", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_banco_ing_mad", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_nel_cant_parciales", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_proceso_leg", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_cuadrado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_servicio", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Bulto - arrays de terminos por bulto
            spw.AgregarParam("par$avtbul_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Parcial - arrays
            spw.AgregarParam("par$apardoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_nec_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Parcial - arrays de terminos por parcial
            spw.AgregarParam("par$avtpar_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Elemento - Declarado Efectivo
            spw.AgregarParam("par$aelemvalefe_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_esbillete", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_tipo_efec", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Elemento - Declarado Medio Pago
            spw.AgregarParam("par$aelemval_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_tipo_mp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' Elemento - Terminos medio pago
            spw.AgregarParam("par$aelemter_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_nel_ind_grupo", ProsegurDbType.Numero_Decimal, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Colectar_Documentos(documentos, spw)

            Return spw

        End Function

        Private Shared Function ColectarSustitucion(documentos As ObservableCollection(Of Clases.Documento),
                                                    bol_gestion_bulto As Boolean,
                                                    hacer_commit As Boolean,
                                                    confirmar_doc As Boolean) As SPWrapper

            Dim identificadorGrupoDocumento As String = documentos(0).IdentificadorGrupo
            Dim usuario As String = documentos(0).UsuarioModificacion

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_elem_sustitucion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            If documentos(0).Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(documentos(0).Formulario.Identificador) Then
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, documentos(0).Formulario.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            ' arrays asociativos
            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)

            spw.AgregarParam("par$adocs_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' cuenta origen
            spw.AgregarParam("par$adocs_mov_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' cuenta destino
            spw.AgregarParam("par$adocs_mov_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_subcan", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_delega", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_planta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_oid_sector", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_mov_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_client", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_subcli", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_oid_ptserv", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_sal_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' arrays de efectivo por documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de medio de pago por documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos de medio de pago por documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' arrays de terminos por documento
            spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' Remesa
            spw.AgregarParam("par$aremdoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_recibo_salida", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_ruta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_nel_parada", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_transporte", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_nel_cant_bultos", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_oid_remesa_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_cajero", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_estado_abono", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            ' Remesa - arrays de terminos por remesa
            spw.AgregarParam("par$avtrem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtrem_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtrem_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_iac_parciales", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_bolsa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_banco_ingreso", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_banco_ing_mad", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_nel_cant_parciales", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_fyh_proceso_leg", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_bulto_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_bol_cuadrado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_tipo_servicio", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Bulto - arrays de terminos por bulto
            spw.AgregarParam("par$avtbul_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtbul_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtbul_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Parcial - arrays
            spw.AgregarParam("par$apardoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_cod_precinto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_usuario_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_puesto_resp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apardoc_nec_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_parcial_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$apardoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Parcial - arrays de terminos por parcial
            spw.AgregarParam("par$avtpar_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtpar_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtpar_obligatorio", ProsegurDbType.Logico, Nothing, , True)

            ' Elemento - Declarado Efectivo
            spw.AgregarParam("par$aelemvalefe_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_esbillete", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_tipo_efec", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemvalefe_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)

            ' Elemento - Declarado Medio Pago
            spw.AgregarParam("par$aelemval_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_tipo_mp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_cod_nvdetalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aelemval_mp_nel_secuencia", ProsegurDbType.Numero_Decimal, Nothing, , True)

            ' Elemento - Terminos medio pago
            spw.AgregarParam("par$aelemter_mp_tipo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelemter_mp_nel_ind_grupo", ProsegurDbType.Numero_Decimal, Nothing, , True)

            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, 0, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Colectar_Documentos(documentos, spw)

            Return spw

        End Function

        Public Shared Sub Colectar_Documentos(documentos As ObservableCollection(Of Clases.Documento),
                                              ByRef spw As SPWrapper)

            ' === Documento ===
            For Each documento In documentos

                ' arrays de documentos
                spw.Param("par$adocs_oid").AgregarValorArray(documento.Identificador)

                If documento.DocumentoPadre IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.DocumentoPadre.Identificador) Then
                    spw.Param("par$adocs_oid_doc_padre").AgregarValorArray(documento.DocumentoPadre.Identificador)
                Else
                    spw.Param("par$adocs_oid_doc_padre").AgregarValorArray(DBNull.Value)
                End If
                If Not String.IsNullOrEmpty(documento.IdentificadorSustituto) Then
                    spw.Param("par$adocs_oid_sustituto").AgregarValorArray(documento.DocumentoPadre.Identificador)
                Else
                    spw.Param("par$adocs_oid_sustituto").AgregarValorArray(DBNull.Value)
                End If
                If Not String.IsNullOrEmpty(documento.IdentificadorMovimentacionFondo) Then
                    spw.Param("par$adocs_oid_mov_fondos").AgregarValorArray(documento.DocumentoPadre.Identificador)
                Else
                    spw.Param("par$adocs_oid_mov_fondos").AgregarValorArray(DBNull.Value)
                End If

                If documento.FechaHoraPlanificacionCertificacion = Date.MinValue Then
                    spw.Param("par$adocs_fyh_plncertif").AgregarValorArray(DBNull.Value)
                Else
                    spw.Param("par$adocs_fyh_plncertif").AgregarValorArray(documento.FechaHoraPlanificacionCertificacion.QuieroGrabarGMTZeroEnLaBBDD(documento.CuentaDestino.Sector.Delegacion))
                End If

                If documento.FechaHoraGestion Is Nothing OrElse documento.FechaHoraGestion = Date.MinValue Then
                    spw.Param("par$adocs_fyh_gestion").AgregarValorArray(DBNull.Value)
                Else
                    Dim d As DateTime = documento.FechaHoraGestion
                    spw.Param("par$adocs_fyh_gestion").AgregarValorArray(d.QuieroGrabarGMTZeroEnLaBBDD(documento.CuentaDestino.Sector.Delegacion))
                End If

                spw.Param("par$adocs_fyh_contable").AgregarValorArray(DBNull.Value)
                spw.Param("par$adocs_cod_actual_id").AgregarValorArray(DBNull.Value)
                spw.Param("par$adocs_collection_id").AgregarValorArray(DBNull.Value)

                spw.Param("par$adocs_cod_externo").AgregarValorArray(documento.NumeroExterno)
                spw.Param("par$adocs_rowver").AgregarValorArray(documento.Rowver)

                ' === Cuentas ===
                Colectar_Cuentas(documento, spw)

                ' === Valores do Documento ===
                If Not (documento.Formulario.Codigo = "MAESOC" Or documento.Formulario.Codigo = "MAESOD") Then
                    Prosegur.Genesis.Comon.Util.BorrarItemsDivisasSinValoresCantidades(documento.Divisas)
                End If
                ColectarAltas_ValoresDoc(documento.Divisas, documento.Identificador, False, spw)
                If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Classificacion) AndAlso
                    documento.DivisasSaldoAnterior IsNot Nothing AndAlso documento.DivisasSaldoAnterior.Count > 0 Then
                    ColectarAltas_ValoresDoc(documento.DivisasSaldoAnterior, documento.Identificador, True, spw)
                End If

                ' arrays de terminos por documento
                If documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC.Count > 0 Then
                    If Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso
                        Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas) Then

                        For Each trm As Clases.TerminoIAC In documento.GrupoTerminosIAC.TerminosIAC
                            If Not String.IsNullOrEmpty(trm.Valor) Then
                                spw.Param("par$avtdoc_oid_documento").AgregarValorArray(documento.Identificador)

                                If Not String.IsNullOrEmpty(trm.Identificador) Then
                                    spw.Param("par$avtdoc_oid_termino").AgregarValorArray(trm.Identificador)
                                Else
                                    spw.Param("par$avtdoc_oid_termino").AgregarValorArray(DBNull.Value)
                                End If
                                If spw.Param("par$avtdoc_cod_termino") IsNot Nothing Then
                                    If Not String.IsNullOrEmpty(trm.Codigo) Then
                                        spw.Param("par$avtdoc_cod_termino").AgregarValorArray(trm.Codigo)
                                    Else
                                        spw.Param("par$avtdoc_cod_termino").AgregarValorArray(DBNull.Value)
                                    End If
                                End If
                                If Not String.IsNullOrEmpty(trm.Valor) Then
                                    spw.Param("par$avtdoc_des_valor").AgregarValorArray(trm.Valor)
                                Else
                                    spw.Param("par$avtdoc_des_valor").AgregarValorArray(DBNull.Value)
                                End If
                            End If
                        Next

                    End If
                End If

                If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) OrElse
                    documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Sustitucion) Then
                    ' === Remesa ===
                    DirectCast(documento.Elemento, Clases.Remesa).IdentificadorDocumento = documento.Identificador
                    Colectar_Remesas(DirectCast(documento.Elemento, Clases.Remesa), Enumeradores.CaracteristicaFormulario.Altas, spw)

                ElseIf documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Actas) Then
                    ' === Remesa ===
                    DirectCast(documento.Elemento, Clases.Remesa).IdentificadorDocumento = documento.Identificador
                    Colectar_Remesas(DirectCast(documento.Elemento, Clases.Remesa), Enumeradores.CaracteristicaFormulario.Actas, spw)

                ElseIf documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) OrElse
                    documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas) Then

                    Dim remesa As Clases.Remesa = DirectCast(documento.Elemento, Clases.Remesa)

                    spw.Param("par$aremdoc_oid_remesa").AgregarValorArray(remesa.Identificador)
                    spw.Param("par$aremdoc_oid_documento").AgregarValorArray(documento.Identificador)
                    spw.Param("par$aremdoc_rowver").AgregarValorArray(remesa.Rowver)

                    If documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then

                        For Each bulto In remesa.Bultos

                            spw.Param("par$abuldoc_oid_bulto").AgregarValorArray(bulto.Identificador)
                            spw.Param("par$abuldoc_oid_remesa").AgregarValorArray(remesa.Identificador)
                            spw.Param("par$abuldoc_oid_documento").AgregarValorArray(documento.Identificador)
                            spw.Param("par$abuldoc_rowver").AgregarValorArray(bulto.Rowver)

                        Next

                    End If

                End If

            Next

        End Sub

        Public Shared Sub Colectar_Cuentas(documento As Clases.Documento,
                                           ByRef spw As SPWrapper)

            ' cuenta origen
            Dim mov_ori_oid_cuenta As String = String.Empty
            Dim mov_ori_oid_client As String = String.Empty
            Dim mov_ori_cod_client As String = String.Empty
            Dim mov_ori_oid_subcli As String = String.Empty
            Dim mov_ori_cod_subcli As String = String.Empty
            Dim mov_ori_oid_ptserv As String = String.Empty
            Dim mov_ori_cod_ptserv As String = String.Empty
            Dim mov_ori_oid_canal As String = String.Empty
            Dim mov_ori_cod_canal As String = String.Empty
            Dim mov_ori_oid_subcan As String = String.Empty
            Dim mov_ori_cod_subcan As String = String.Empty
            Dim mov_ori_oid_delega As String = String.Empty
            Dim mov_ori_cod_delega As String = String.Empty
            Dim mov_ori_oid_planta As String = String.Empty
            Dim mov_ori_cod_planta As String = String.Empty
            Dim mov_ori_oid_sector As String = String.Empty
            Dim mov_ori_cod_sector As String = String.Empty
            Dim sal_ori_oid_cuenta As String = String.Empty
            Dim sal_ori_oid_client As String = String.Empty
            Dim sal_ori_cod_client As String = String.Empty
            Dim sal_ori_oid_subcli As String = String.Empty
            Dim sal_ori_cod_subcli As String = String.Empty
            Dim sal_ori_oid_ptserv As String = String.Empty
            Dim sal_ori_cod_ptserv As String = String.Empty
            Dim sal_ori_oid_canal As String = String.Empty
            Dim sal_ori_cod_canal As String = String.Empty
            Dim sal_ori_oid_subcan As String = String.Empty
            Dim sal_ori_cod_subcan As String = String.Empty
            Dim sal_ori_oid_delega As String = String.Empty
            Dim sal_ori_cod_delega As String = String.Empty
            Dim sal_ori_oid_planta As String = String.Empty
            Dim sal_ori_cod_planta As String = String.Empty
            Dim sal_ori_oid_sector As String = String.Empty
            Dim sal_ori_cod_sector As String = String.Empty

            ' cuenta destino
            Dim mov_des_oid_cuenta As String = String.Empty
            Dim mov_des_oid_client As String = String.Empty
            Dim mov_des_cod_client As String = String.Empty
            Dim mov_des_oid_subcli As String = String.Empty
            Dim mov_des_cod_subcli As String = String.Empty
            Dim mov_des_oid_ptserv As String = String.Empty
            Dim mov_des_cod_ptserv As String = String.Empty
            Dim mov_des_oid_canal As String = String.Empty
            Dim mov_des_cod_canal As String = String.Empty
            Dim mov_des_oid_subcan As String = String.Empty
            Dim mov_des_cod_subcan As String = String.Empty
            Dim mov_des_oid_delega As String = String.Empty
            Dim mov_des_cod_delega As String = String.Empty
            Dim mov_des_oid_planta As String = String.Empty
            Dim mov_des_cod_planta As String = String.Empty
            Dim mov_des_oid_sector As String = String.Empty
            Dim mov_des_cod_sector As String = String.Empty
            Dim sal_des_oid_cuenta As String = String.Empty
            Dim sal_des_oid_client As String = String.Empty
            Dim sal_des_cod_client As String = String.Empty
            Dim sal_des_oid_subcli As String = String.Empty
            Dim sal_des_cod_subcli As String = String.Empty
            Dim sal_des_oid_ptserv As String = String.Empty
            Dim sal_des_cod_ptserv As String = String.Empty
            Dim sal_des_oid_canal As String = String.Empty
            Dim sal_des_cod_canal As String = String.Empty
            Dim sal_des_oid_subcan As String = String.Empty
            Dim sal_des_cod_subcan As String = String.Empty
            Dim sal_des_oid_delega As String = String.Empty
            Dim sal_des_cod_delega As String = String.Empty
            Dim sal_des_oid_planta As String = String.Empty
            Dim sal_des_cod_planta As String = String.Empty
            Dim sal_des_oid_sector As String = String.Empty
            Dim sal_des_cod_sector As String = String.Empty

            ' Movimento origen
            If documento.CuentaOrigen IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(documento.CuentaOrigen.Identificador) Then
                    mov_ori_oid_cuenta = documento.CuentaOrigen.Identificador
                End If
                If documento.CuentaOrigen.Cliente IsNot Nothing Then
                    mov_ori_oid_client = documento.CuentaOrigen.Cliente.Identificador
                    mov_ori_cod_client = documento.CuentaOrigen.Cliente.Codigo
                End If
                If documento.CuentaOrigen.SubCliente IsNot Nothing Then
                    mov_ori_oid_subcli = documento.CuentaOrigen.SubCliente.Identificador
                    mov_ori_cod_subcli = documento.CuentaOrigen.SubCliente.Codigo
                End If
                If documento.CuentaOrigen.PuntoServicio IsNot Nothing Then
                    mov_ori_oid_ptserv = documento.CuentaOrigen.PuntoServicio.Identificador
                    mov_ori_cod_ptserv = documento.CuentaOrigen.PuntoServicio.Codigo
                End If
                If documento.CuentaOrigen.Canal IsNot Nothing Then
                    mov_ori_oid_canal = documento.CuentaOrigen.Canal.Identificador
                    mov_ori_cod_canal = documento.CuentaOrigen.Canal.Codigo
                End If
                If documento.CuentaOrigen.Cliente IsNot Nothing Then
                    mov_ori_oid_subcan = documento.CuentaOrigen.SubCanal.Identificador
                    mov_ori_cod_subcan = documento.CuentaOrigen.SubCanal.Codigo
                End If

                If documento.CuentaOrigen.Sector IsNot Nothing Then
                    mov_ori_oid_sector = documento.CuentaOrigen.Sector.Identificador
                    mov_ori_cod_sector = documento.CuentaOrigen.Sector.Codigo
                    If documento.CuentaOrigen.Sector.Delegacion IsNot Nothing Then
                        mov_ori_oid_delega = documento.CuentaOrigen.Sector.Delegacion.Identificador
                        mov_ori_cod_delega = documento.CuentaOrigen.Sector.Delegacion.Codigo
                    End If
                    If documento.CuentaOrigen.Sector.Planta IsNot Nothing Then
                        mov_ori_oid_planta = documento.CuentaOrigen.Sector.Planta.Identificador
                        mov_ori_cod_planta = documento.CuentaOrigen.Sector.Planta.Codigo
                    End If
                End If
            End If

            ' Saldos Origen
            If documento.CuentaSaldoOrigen IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(documento.CuentaSaldoOrigen.Identificador) Then
                    sal_ori_oid_cuenta = documento.CuentaSaldoOrigen.Identificador
                End If
                If documento.CuentaSaldoOrigen.Cliente IsNot Nothing Then
                    sal_ori_oid_client = documento.CuentaSaldoOrigen.Cliente.Identificador
                    sal_ori_cod_client = documento.CuentaSaldoOrigen.Cliente.Codigo
                End If
                If documento.CuentaSaldoOrigen.SubCliente IsNot Nothing Then
                    sal_ori_oid_subcli = documento.CuentaSaldoOrigen.SubCliente.Identificador
                    sal_ori_cod_subcli = documento.CuentaSaldoOrigen.SubCliente.Codigo
                End If
                If documento.CuentaSaldoOrigen.PuntoServicio IsNot Nothing Then
                    sal_ori_oid_ptserv = documento.CuentaSaldoOrigen.PuntoServicio.Identificador
                    sal_ori_cod_ptserv = documento.CuentaSaldoOrigen.PuntoServicio.Codigo
                End If
                If documento.CuentaSaldoOrigen.Canal IsNot Nothing Then
                    sal_ori_oid_canal = documento.CuentaSaldoOrigen.Canal.Identificador
                    sal_ori_cod_canal = documento.CuentaSaldoOrigen.Canal.Codigo
                End If
                If documento.CuentaSaldoOrigen.Cliente IsNot Nothing Then
                    sal_ori_oid_subcan = documento.CuentaSaldoOrigen.SubCanal.Identificador
                    sal_ori_cod_subcan = documento.CuentaSaldoOrigen.SubCanal.Codigo
                End If
                If documento.CuentaSaldoOrigen.Sector IsNot Nothing Then
                    sal_ori_oid_sector = documento.CuentaSaldoOrigen.Sector.Identificador
                    sal_ori_cod_sector = documento.CuentaSaldoOrigen.Sector.Codigo
                    If documento.CuentaSaldoOrigen.Sector.Delegacion IsNot Nothing Then
                        sal_ori_oid_delega = documento.CuentaSaldoOrigen.Sector.Delegacion.Identificador
                        sal_ori_cod_delega = documento.CuentaSaldoOrigen.Sector.Delegacion.Codigo
                    End If
                    If documento.CuentaSaldoOrigen.Sector.Planta IsNot Nothing Then
                        sal_ori_oid_planta = documento.CuentaSaldoOrigen.Sector.Planta.Identificador
                        sal_ori_cod_planta = documento.CuentaSaldoOrigen.Sector.Planta.Codigo
                    End If
                End If
            End If

            ' Movimento Destino
            If documento.CuentaDestino IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(documento.CuentaDestino.Identificador) Then
                    mov_des_oid_cuenta = documento.CuentaDestino.Identificador
                End If
                If documento.CuentaDestino.Cliente IsNot Nothing Then
                    mov_des_oid_client = documento.CuentaDestino.Cliente.Identificador
                    mov_des_cod_client = documento.CuentaDestino.Cliente.Codigo
                End If
                If documento.CuentaDestino.SubCliente IsNot Nothing Then
                    mov_des_oid_subcli = documento.CuentaDestino.SubCliente.Identificador
                    mov_des_cod_subcli = documento.CuentaDestino.SubCliente.Codigo
                End If
                If documento.CuentaDestino.PuntoServicio IsNot Nothing Then
                    mov_des_oid_ptserv = documento.CuentaDestino.PuntoServicio.Identificador
                    mov_des_cod_ptserv = documento.CuentaDestino.PuntoServicio.Codigo
                End If
                If documento.CuentaDestino.Canal IsNot Nothing Then
                    mov_des_oid_canal = documento.CuentaDestino.Canal.Identificador
                    mov_des_cod_canal = documento.CuentaDestino.Canal.Codigo
                End If
                If documento.CuentaDestino.Cliente IsNot Nothing Then
                    mov_des_oid_subcan = documento.CuentaDestino.SubCanal.Identificador
                    mov_des_cod_subcan = documento.CuentaDestino.SubCanal.Codigo
                End If
                If documento.CuentaDestino.Sector IsNot Nothing Then
                    mov_des_oid_sector = documento.CuentaDestino.Sector.Identificador
                    mov_des_cod_sector = documento.CuentaDestino.Sector.Codigo
                    If documento.CuentaDestino.Sector.Delegacion IsNot Nothing Then
                        mov_des_oid_delega = documento.CuentaDestino.Sector.Delegacion.Identificador
                        mov_des_cod_delega = documento.CuentaDestino.Sector.Delegacion.Codigo
                    End If
                    If documento.CuentaDestino.Sector.Planta IsNot Nothing Then
                        mov_des_oid_planta = documento.CuentaDestino.Sector.Planta.Identificador
                        mov_des_cod_planta = documento.CuentaDestino.Sector.Planta.Codigo
                    End If
                End If
            End If

            ' Saldos Destino
            If documento.CuentaSaldoDestino IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(documento.CuentaSaldoDestino.Identificador) Then
                    sal_des_oid_cuenta = documento.CuentaSaldoDestino.Identificador
                End If
                If documento.CuentaSaldoDestino.Cliente IsNot Nothing Then
                    sal_des_oid_client = documento.CuentaSaldoDestino.Cliente.Identificador
                    sal_des_cod_client = documento.CuentaSaldoDestino.Cliente.Codigo
                End If
                If documento.CuentaSaldoDestino.SubCliente IsNot Nothing Then
                    sal_des_oid_subcli = documento.CuentaSaldoDestino.SubCliente.Identificador
                    sal_des_cod_subcli = documento.CuentaSaldoDestino.SubCliente.Codigo
                End If
                If documento.CuentaSaldoDestino.PuntoServicio IsNot Nothing Then
                    sal_des_oid_ptserv = documento.CuentaSaldoDestino.PuntoServicio.Identificador
                    sal_des_cod_ptserv = documento.CuentaSaldoDestino.PuntoServicio.Codigo
                End If
                If documento.CuentaSaldoDestino.Canal IsNot Nothing Then
                    sal_des_oid_canal = documento.CuentaSaldoDestino.Canal.Identificador
                    sal_des_cod_canal = documento.CuentaSaldoDestino.Canal.Codigo
                End If
                If documento.CuentaSaldoDestino.Cliente IsNot Nothing Then
                    sal_des_oid_subcan = documento.CuentaSaldoDestino.SubCanal.Identificador
                    sal_des_cod_subcan = documento.CuentaSaldoDestino.SubCanal.Codigo
                End If
                If documento.CuentaSaldoDestino.Sector IsNot Nothing Then
                    sal_des_oid_sector = documento.CuentaSaldoDestino.Sector.Identificador
                    sal_des_cod_sector = documento.CuentaSaldoDestino.Sector.Codigo
                    If documento.CuentaSaldoDestino.Sector.Delegacion IsNot Nothing Then
                        sal_des_oid_delega = documento.CuentaSaldoDestino.Sector.Delegacion.Identificador
                        sal_des_cod_delega = documento.CuentaSaldoDestino.Sector.Delegacion.Codigo
                    End If
                    If documento.CuentaSaldoDestino.Sector.Planta IsNot Nothing Then
                        sal_des_oid_planta = documento.CuentaSaldoDestino.Sector.Planta.Identificador
                        sal_des_cod_planta = documento.CuentaSaldoDestino.Sector.Planta.Codigo
                    End If
                End If
            End If

            If Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Actas) Then

                ' cuenta origen
                spw.Param("par$adocs_mov_ori_oid_cuenta").AgregarValorArray(mov_ori_oid_cuenta)
                spw.Param("par$adocs_mov_ori_oid_client").AgregarValorArray(mov_ori_oid_client)
                spw.Param("par$adocs_mov_ori_cod_client").AgregarValorArray(mov_ori_cod_client)
                spw.Param("par$adocs_mov_ori_oid_subcli").AgregarValorArray(mov_ori_oid_subcli)
                spw.Param("par$adocs_mov_ori_cod_subcli").AgregarValorArray(mov_ori_cod_subcli)
                spw.Param("par$adocs_mov_ori_oid_ptserv").AgregarValorArray(mov_ori_oid_ptserv)
                spw.Param("par$adocs_mov_ori_cod_ptserv").AgregarValorArray(mov_ori_cod_ptserv)
                spw.Param("par$adocs_mov_ori_oid_canal").AgregarValorArray(mov_ori_oid_canal)
                spw.Param("par$adocs_mov_ori_cod_canal").AgregarValorArray(mov_ori_cod_canal)
                spw.Param("par$adocs_mov_ori_oid_subcan").AgregarValorArray(mov_ori_oid_subcan)
                spw.Param("par$adocs_mov_ori_cod_subcan").AgregarValorArray(mov_ori_cod_subcan)
                spw.Param("par$adocs_mov_ori_oid_delega").AgregarValorArray(mov_ori_oid_delega)
                spw.Param("par$adocs_mov_ori_cod_delega").AgregarValorArray(mov_ori_cod_delega)
                spw.Param("par$adocs_mov_ori_oid_planta").AgregarValorArray(mov_ori_oid_planta)
                spw.Param("par$adocs_mov_ori_cod_planta").AgregarValorArray(mov_ori_cod_planta)
                spw.Param("par$adocs_mov_ori_oid_sector").AgregarValorArray(mov_ori_oid_sector)
                spw.Param("par$adocs_mov_ori_cod_sector").AgregarValorArray(mov_ori_cod_sector)
                If Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso
                   Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas) Then
                    spw.Param("par$adocs_sal_ori_oid_cuenta").AgregarValorArray(sal_ori_oid_cuenta)
                    spw.Param("par$adocs_sal_ori_oid_client").AgregarValorArray(sal_ori_oid_client)
                    spw.Param("par$adocs_sal_ori_cod_client").AgregarValorArray(sal_ori_cod_client)
                    spw.Param("par$adocs_sal_ori_oid_subcli").AgregarValorArray(sal_ori_oid_subcli)
                    spw.Param("par$adocs_sal_ori_cod_subcli").AgregarValorArray(sal_ori_cod_subcli)
                    spw.Param("par$adocs_sal_ori_oid_ptserv").AgregarValorArray(sal_ori_oid_ptserv)
                    spw.Param("par$adocs_sal_ori_cod_ptserv").AgregarValorArray(sal_ori_cod_ptserv)
                End If

                ' cuenta destino
                spw.Param("par$adocs_mov_des_oid_cuenta").AgregarValorArray(mov_des_oid_cuenta)
                spw.Param("par$adocs_mov_des_oid_client").AgregarValorArray(mov_des_oid_client)
                spw.Param("par$adocs_mov_des_cod_client").AgregarValorArray(mov_des_cod_client)
                spw.Param("par$adocs_mov_des_oid_subcli").AgregarValorArray(mov_des_oid_subcli)
                spw.Param("par$adocs_mov_des_cod_subcli").AgregarValorArray(mov_des_cod_subcli)
                spw.Param("par$adocs_mov_des_oid_ptserv").AgregarValorArray(mov_des_oid_ptserv)
                spw.Param("par$adocs_mov_des_cod_ptserv").AgregarValorArray(mov_des_cod_ptserv)
                spw.Param("par$adocs_mov_des_oid_canal").AgregarValorArray(mov_des_oid_canal)
                spw.Param("par$adocs_mov_des_cod_canal").AgregarValorArray(mov_des_cod_canal)
                spw.Param("par$adocs_mov_des_oid_subcan").AgregarValorArray(mov_des_oid_subcan)
                spw.Param("par$adocs_mov_des_cod_subcan").AgregarValorArray(mov_des_cod_subcan)
                spw.Param("par$adocs_mov_des_oid_delega").AgregarValorArray(mov_des_oid_delega)
                spw.Param("par$adocs_mov_des_cod_delega").AgregarValorArray(mov_des_cod_delega)
                spw.Param("par$adocs_mov_des_oid_planta").AgregarValorArray(mov_des_oid_planta)
                spw.Param("par$adocs_mov_des_cod_planta").AgregarValorArray(mov_des_cod_planta)
                spw.Param("par$adocs_mov_des_oid_sector").AgregarValorArray(mov_des_oid_sector)
                spw.Param("par$adocs_mov_des_cod_sector").AgregarValorArray(mov_des_cod_sector)
                If Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) AndAlso
                   Not documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas) Then
                    spw.Param("par$adocs_sal_des_oid_cuenta").AgregarValorArray(sal_des_oid_cuenta)
                    spw.Param("par$adocs_sal_des_oid_client").AgregarValorArray(sal_des_oid_client)
                    spw.Param("par$adocs_sal_des_cod_client").AgregarValorArray(sal_des_cod_client)
                    spw.Param("par$adocs_sal_des_oid_subcli").AgregarValorArray(sal_des_oid_subcli)
                    spw.Param("par$adocs_sal_des_cod_subcli").AgregarValorArray(sal_des_cod_subcli)
                    spw.Param("par$adocs_sal_des_oid_ptserv").AgregarValorArray(sal_des_oid_ptserv)
                    spw.Param("par$adocs_sal_des_cod_ptserv").AgregarValorArray(sal_des_cod_ptserv)
                End If

            End If

        End Sub

        Private Shared Sub ColectarAltas_ValoresDoc(divisas As ObservableCollection(Of Clases.Divisa),
                                                    identificadorDocumento As String,
                                                    valoresAnteriors As Boolean,
                                                    ByRef spw As SPWrapper)

            Dim NivelDetalle As String
            Dim flag As String = If(valoresAnteriors, "a", "")

            If divisas IsNot Nothing Then
                For Each div As Clases.Divisa In divisas

                    'efectivo
                    If div.Denominaciones IsNot Nothing Then
                        NivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
                        For Each den As Clases.Denominacion In div.Denominaciones
                            For Each vld As Clases.ValorDenominacion In den.ValorDenominacion
                                If vld.TipoValor = Enumeradores.TipoValor.NoDefinido Then
                                    spw.Param("par$aef" & flag & "doc_oid_documento").AgregarValorArray(identificadorDocumento)
                                    spw.Param("par$aef" & flag & "doc_oid_divisa").AgregarValorArray(div.Identificador)
                                    spw.Param("par$aef" & flag & "doc_oid_denominacion").AgregarValorArray(den.Identificador)
                                    If vld.UnidadMedida IsNot Nothing Then
                                        spw.Param("par$aef" & flag & "doc_oid_unid_medida").AgregarValorArray(vld.UnidadMedida.Identificador)
                                    Else
                                        spw.Param("par$aef" & flag & "doc_oid_unid_medida").AgregarValorArray(DBNull.Value) 'aca hay que ver en el SP como obtener el default del padron segun billete o moneda
                                    End If
                                    spw.Param("par$aef" & flag & "doc_cod_niv_detalle").AgregarValorArray(NivelDetalle)
                                    spw.Param("par$aef" & flag & "doc_cod_tp_efec_tot").AgregarValorArray(DBNull.Value)
                                    If vld.Calidad IsNot Nothing Then
                                        spw.Param("par$aef" & flag & "doc_oid_calidad").AgregarValorArray(vld.Calidad.Identificador)
                                    Else
                                        spw.Param("par$aef" & flag & "doc_oid_calidad").AgregarValorArray(DBNull.Value)
                                    End If
                                    spw.Param("par$aef" & flag & "doc_num_importe").AgregarValorArray(vld.Importe)
                                    spw.Param("par$aef" & flag & "doc_nel_cantidad").AgregarValorArray(vld.Cantidad)

                                End If
                            Next    'valor de denominacion
                        Next    'denominaciones
                    End If

                    'totales efectivo
                    If div.ValoresTotalesEfectivo IsNot Nothing Then
                        NivelDetalle = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                        For Each vtd As Clases.ValorEfectivo In div.ValoresTotalesEfectivo
                            If vtd.TipoValor = Enumeradores.TipoValor.NoDefinido Then
                                spw.Param("par$aef" & flag & "doc_oid_documento").AgregarValorArray(identificadorDocumento)
                                spw.Param("par$aef" & flag & "doc_oid_divisa").AgregarValorArray(div.Identificador)
                                spw.Param("par$aef" & flag & "doc_oid_denominacion").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aef" & flag & "doc_oid_unid_medida").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aef" & flag & "doc_cod_niv_detalle").AgregarValorArray(NivelDetalle)
                                spw.Param("par$aef" & flag & "doc_cod_tp_efec_tot").AgregarValorArray(vtd.TipoDetalleEfectivo.RecuperarValor())
                                spw.Param("par$aef" & flag & "doc_oid_calidad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aef" & flag & "doc_num_importe").AgregarValorArray(vtd.Importe)
                                spw.Param("par$aef" & flag & "doc_nel_cantidad").AgregarValorArray(0)
                            End If
                        Next
                    End If

                    'medios de pago
                    If div.MediosPago IsNot Nothing Then
                        NivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
                        For Each mp As Clases.MedioPago In div.MediosPago
                            For Each vl As Clases.ValorMedioPago In mp.Valores
                                If vl.TipoValor = Enumeradores.TipoValor.NoDefinido Then
                                    Dim oidMP As String = Util.NewGUID()
                                    spw.Param("par$amp" & flag & "doc_oid_documento").AgregarValorArray(identificadorDocumento)
                                    spw.Param("par$amp" & flag & "doc_oid_divisa").AgregarValorArray(div.Identificador)
                                    spw.Param("par$amp" & flag & "doc_oid_medio_pago").AgregarValorArray(mp.Identificador)
                                    spw.Param("par$amp" & flag & "doc_cod_tipo_med_pago").AgregarValorArray(mp.Tipo.RecuperarValor())
                                    spw.Param("par$amp" & flag & "doc_cod_nivel_detalle").AgregarValorArray(NivelDetalle)
                                    spw.Param("par$amp" & flag & "doc_num_importe").AgregarValorArray(vl.Importe)
                                    spw.Param("par$amp" & flag & "doc_nel_cantidad").AgregarValorArray(vl.Cantidad)

                                    'terminos del medio de pago
                                    If mp.Terminos IsNot Nothing Then
                                        For Each t As Clases.Termino In mp.Terminos
                                            If Not String.IsNullOrEmpty(t.Valor) Then
                                                spw.Param("par$avtmp" & flag & "doc_oid_documento").AgregarValorArray(oidMP)
                                                spw.Param("par$avtmp" & flag & "doc_oid_t_mediopago").AgregarValorArray(t.Identificador)
                                                spw.Param("par$avtmp" & flag & "doc_des_valor").AgregarValorArray(t.Valor)
                                                spw.Param("par$avtmp" & flag & "doc_nec_indice_grp").AgregarValorArray(t.NecIndiceGrupo)
                                            End If
                                        Next
                                    End If
                                End If
                            Next    'valor de medio de pago

                        Next    'medio de pago

                    End If

                    'totales medios de pago
                    If div.ValoresTotalesTipoMedioPago IsNot Nothing Then
                        NivelDetalle = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
                        For Each vtmp As Clases.ValorTipoMedioPago In div.ValoresTotalesTipoMedioPago
                            spw.Param("par$amp" & flag & "doc_oid_documento").AgregarValorArray(identificadorDocumento)
                            spw.Param("par$amp" & flag & "doc_oid_divisa").AgregarValorArray(div.Identificador)
                            spw.Param("par$amp" & flag & "doc_oid_medio_pago").AgregarValorArray(DBNull.Value)
                            spw.Param("par$amp" & flag & "doc_cod_tipo_med_pago").AgregarValorArray(vtmp.TipoMedioPago.RecuperarValor())
                            spw.Param("par$amp" & flag & "doc_cod_nivel_detalle").AgregarValorArray(NivelDetalle)
                            spw.Param("par$amp" & flag & "doc_num_importe").AgregarValorArray(vtmp.Importe)
                            spw.Param("par$amp" & flag & "doc_nel_cantidad").AgregarValorArray(vtmp.Cantidad)
                        Next
                    End If

                Next 'divisa

            End If

        End Sub

        Private Shared Sub Colectar_Remesas(remesa As Clases.Remesa,
                                            caracteristica_formulario As Enumeradores.CaracteristicaFormulario,
                                            ByRef spw As SPWrapper)

            spw.Param("par$aremdoc_oid_remesa").AgregarValorArray(remesa.Identificador)
            spw.Param("par$aremdoc_oid_documento").AgregarValorArray(remesa.IdentificadorDocumento)

            If caracteristica_formulario = Enumeradores.CaracteristicaFormulario.Altas Then

                If remesa.RemesaOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(remesa.RemesaOrigen.Identificador) Then
                    spw.Param("par$aremdoc_oid_remesa_origen").AgregarValorArray(remesa.RemesaOrigen.Identificador)
                Else
                    spw.Param("par$aremdoc_oid_remesa_origen").AgregarValorArray(DBNull.Value)
                End If
                If Not String.IsNullOrEmpty(remesa.IdentificadorExterno) Then
                    spw.Param("par$aremdoc_oid_externo").AgregarValorArray(remesa.IdentificadorExterno)
                Else
                    spw.Param("par$aremdoc_oid_externo").AgregarValorArray(DBNull.Value)
                End If
                If remesa.GrupoTerminosIAC IsNot Nothing AndAlso Not String.IsNullOrEmpty(remesa.GrupoTerminosIAC.Identificador) Then
                    spw.Param("par$aremdoc_oid_iac").AgregarValorArray(remesa.GrupoTerminosIAC.Identificador)
                Else
                    spw.Param("par$aremdoc_oid_iac").AgregarValorArray(DBNull.Value)
                End If

                spw.Param("par$aremdoc_cod_recibo_salida").AgregarValorArray(remesa.CodigoReciboSalida)
                spw.Param("par$aremdoc_usuario_resp").AgregarValorArray(remesa.UsuarioResponsable)
                spw.Param("par$aremdoc_puesto_resp").AgregarValorArray(remesa.PuestoResponsable)
                spw.Param("par$aremdoc_cod_ruta").AgregarValorArray(remesa.Ruta)
                spw.Param("par$aremdoc_nel_parada").AgregarValorArray(remesa.Parada)

                If remesa.FechaHoraTransporte <> DateTime.MinValue Then
                    spw.Param("par$aremdoc_fyh_transporte").AgregarValorArray(remesa.FechaHoraTransporte)
                Else
                    spw.Param("par$aremdoc_fyh_transporte").AgregarValorArray(DBNull.Value)
                End If
                If remesa.Bultos IsNot Nothing Then
                    spw.Param("par$aremdoc_nel_cant_bultos").AgregarValorArray(remesa.Bultos.Count)
                Else
                    spw.Param("par$aremdoc_nel_cant_bultos").AgregarValorArray(DBNull.Value)
                End If
                If remesa.FechaHoraInicioConteo <> DateTime.MinValue Then
                    spw.Param("par$aremdoc_fyh_conteo_inicio").AgregarValorArray(remesa.FechaHoraInicioConteo)
                Else
                    spw.Param("par$aremdoc_fyh_conteo_inicio").AgregarValorArray(DBNull.Value)
                End If
                If remesa.FechaHoraFinConteo <> DateTime.MinValue Then
                    spw.Param("par$aremdoc_fyh_conteo_fin").AgregarValorArray(remesa.FechaHoraFinConteo)
                Else
                    spw.Param("par$aremdoc_fyh_conteo_fin").AgregarValorArray(DBNull.Value)
                End If
                If remesa.ElementoPadre IsNot Nothing Then
                    spw.Param("par$aremdoc_oid_remesa_padre").AgregarValorArray(remesa.ElementoPadre.Identificador)
                Else
                    spw.Param("par$aremdoc_oid_remesa_padre").AgregarValorArray(DBNull.Value)
                End If
                If remesa.ElementoSustituto IsNot Nothing Then
                    spw.Param("par$aremdoc_oid_remesa_sub").AgregarValorArray(remesa.ElementoSustituto.Identificador)
                Else
                    spw.Param("par$aremdoc_oid_remesa_sub").AgregarValorArray(DBNull.Value)
                End If
                If remesa.DatosATM IsNot Nothing Then
                    spw.Param("par$aremdoc_cod_cajero").AgregarValorArray(remesa.DatosATM.CodigoCajero)
                Else
                    spw.Param("par$aremdoc_cod_cajero").AgregarValorArray(DBNull.Value)
                End If

                spw.Param("par$aremdoc_cod_nivel_detalle").AgregarValorArray(remesa.ConfiguracionNivelSaldos.RecuperarValor)
                spw.Param("par$aremdoc_cod_externo").AgregarValorArray(remesa.CodigoExterno)
                spw.Param("par$aremdoc_cod_estado_abono").AgregarValorArray(Enumeradores.EstadoAbonoElemento.NoAbonado.RecuperarValor())

            End If

            ' Remesa - arrays de terminos por remesa
            If remesa.GrupoTerminosIAC IsNot Nothing AndAlso remesa.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                For Each termino In remesa.GrupoTerminosIAC.TerminosIAC

                    spw.Param("par$avtrem_oid_remesa").AgregarValorArray(remesa.Identificador)
                    spw.Param("par$avtrem_oid_termino").AgregarValorArray(termino.Identificador)
                    spw.Param("par$avtrem_cod_termino").AgregarValorArray(termino.Codigo)
                    spw.Param("par$avtrem_des_valor").AgregarValorArray(termino.Valor)
                    spw.Param("par$avtrem_obligatorio").AgregarValorArray(termino.EsObligatorio)

                Next
            End If

            '=== Valores ===
            ColectarAltas_Valores(remesa.Divisas, remesa.Identificador, String.Empty, String.Empty, spw)

            '=== Bultos ===
            For Each bulto In remesa.Bultos
                bulto.IdentificadorDocumento = remesa.IdentificadorDocumento
                ColectarAltas_Bultos(bulto, remesa.Identificador, spw)
            Next

        End Sub

        Private Shared Sub ColectarAltas_Bultos(bulto As Clases.Bulto,
                                                identificadorRemesa As String,
                                                ByRef spw As SPWrapper)

            spw.Param("par$abuldoc_oid_bulto").AgregarValorArray(bulto.Identificador)
            spw.Param("par$abuldoc_oid_remesa").AgregarValorArray(identificadorRemesa)
            spw.Param("par$abuldoc_oid_documento").AgregarValorArray(bulto.IdentificadorDocumento)
            spw.Param("par$abuldoc_oid_externo").AgregarValorArray(bulto.IdentificadorExterno)
            spw.Param("par$abuldoc_cod_bolsa").AgregarValorArray(bulto.CodigoBolsa)
            spw.Param("par$abuldoc_oid_banco_ingreso").AgregarValorArray(If(bulto.BancoIngreso Is Nothing, Nothing, bulto.BancoIngreso.Identificador))
            spw.Param("par$abuldoc_bol_banco_ing_mad").AgregarValorArray(bulto.BancoIngresoEsBancoMadre)
            spw.Param("par$abuldoc_usuario_resp").AgregarValorArray(bulto.UsuarioResponsable)
            spw.Param("par$abuldoc_puesto_resp").AgregarValorArray(bulto.PuestoResponsable)
            spw.Param("par$abuldoc_nel_cant_parciales").AgregarValorArray(bulto.CantidadParciales)
            spw.Param("par$abuldoc_cod_nivel_detalle").AgregarValorArray(bulto.ConfiguracionNivelSaldos.RecuperarValor)
            spw.Param("par$abuldoc_bol_cuadrado").AgregarValorArray(bulto.Cuadrado)

            'Precinto foi preparado para uma lista de valores, mas no momento será gravado apenas um precinto.
            If bulto.Precintos IsNot Nothing AndAlso bulto.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(bulto.Precintos.FirstOrDefault) Then
                spw.Param("par$abuldoc_cod_precinto").AgregarValorArray(bulto.Precintos.FirstOrDefault)
            Else
                spw.Param("par$abuldoc_cod_precinto").AgregarValorArray(DBNull.Value)
            End If
            If bulto.GrupoTerminosIAC IsNot Nothing Then
                spw.Param("par$abuldoc_oid_iac").AgregarValorArray(bulto.GrupoTerminosIAC.Identificador)
            Else
                spw.Param("par$abuldoc_oid_iac").AgregarValorArray(DBNull.Value)
            End If
            If bulto.GrupoTerminosIACParciales IsNot Nothing Then
                spw.Param("par$abuldoc_oid_iac_parciales").AgregarValorArray(bulto.GrupoTerminosIACParciales.Identificador)
            Else
                spw.Param("par$abuldoc_oid_iac_parciales").AgregarValorArray(DBNull.Value)
            End If
            If bulto.FechaHoraInicioConteo <> DateTime.MinValue Then
                spw.Param("par$abuldoc_fyh_conteo_inicio").AgregarValorArray(bulto.FechaHoraInicioConteo)
            Else
                spw.Param("par$abuldoc_fyh_conteo_inicio").AgregarValorArray(DBNull.Value)
            End If
            If bulto.FechaHoraFinConteo <> DateTime.MinValue Then
                spw.Param("par$abuldoc_fyh_conteo_fin").AgregarValorArray(bulto.FechaHoraFinConteo)
            Else
                spw.Param("par$abuldoc_fyh_conteo_fin").AgregarValorArray(DBNull.Value)
            End If
            If bulto.FechaProcessoLegado <> DateTime.MinValue Then
                spw.Param("par$abuldoc_fyh_proceso_leg").AgregarValorArray(bulto.FechaProcessoLegado)
            Else
                bulto.FechaProcessoLegado = DateTime.UtcNow
                spw.Param("par$abuldoc_fyh_proceso_leg").AgregarValorArray(bulto.FechaProcessoLegado)
            End If
            If bulto.ElementoPadre IsNot Nothing Then
                spw.Param("par$abuldoc_oid_bulto_padre").AgregarValorArray(bulto.ElementoPadre.Identificador)
            Else
                spw.Param("par$abuldoc_oid_bulto_padre").AgregarValorArray(DBNull.Value)
            End If
            If bulto.ElementoSustituto IsNot Nothing Then
                spw.Param("par$abuldoc_oid_bulto_sub").AgregarValorArray(bulto.ElementoSustituto.Identificador)
            Else
                spw.Param("par$abuldoc_oid_bulto_sub").AgregarValorArray(DBNull.Value)
            End If
            If bulto.TipoFormato IsNot Nothing Then
                spw.Param("par$abuldoc_oid_tipo_formato").AgregarValorArray(bulto.TipoFormato.Identificador)
            Else
                spw.Param("par$abuldoc_oid_tipo_formato").AgregarValorArray(DBNull.Value)
            End If
            If bulto.TipoServicio IsNot Nothing Then
                spw.Param("par$abuldoc_oid_tipo_servicio").AgregarValorArray(bulto.TipoServicio.Identificador)
            Else
                spw.Param("par$abuldoc_oid_tipo_servicio").AgregarValorArray(DBNull.Value)
            End If

            ' Bulto - arrays de terminos por bulto
            If bulto.GrupoTerminosIAC IsNot Nothing AndAlso bulto.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                For Each termino In bulto.GrupoTerminosIAC.TerminosIAC

                    spw.Param("par$avtbul_oid_bulto").AgregarValorArray(bulto.Identificador)
                    spw.Param("par$avtbul_oid_termino").AgregarValorArray(termino.Identificador)
                    spw.Param("par$avtbul_cod_termino").AgregarValorArray(termino.Codigo)
                    spw.Param("par$avtbul_des_valor").AgregarValorArray(termino.Valor)
                    spw.Param("par$avtbul_obligatorio").AgregarValorArray(termino.EsObligatorio)

                Next
            End If

            '=== Valores ===
            ColectarAltas_Valores(bulto.Divisas, identificadorRemesa, bulto.Identificador, String.Empty, spw)

            If bulto.Parciales IsNot Nothing AndAlso bulto.Parciales.Count > 0 Then
                '=== Parcial ===
                For Each parcial In bulto.Parciales
                    ColectarAltas_Parcial(parcial, bulto.Identificador, identificadorRemesa, spw)
                Next
            End If

        End Sub

        Private Shared Sub ColectarAltas_Parcial(parcial As Clases.Parcial,
                                                 identificadorBulto As String,
                                                 identificadorRemesa As String,
                                                 ByRef spw As SPWrapper)

            ' Parcial - arrays
            spw.Param("par$apardoc_oid_remesa").AgregarValorArray(identificadorRemesa)
            spw.Param("par$apardoc_oid_bulto").AgregarValorArray(identificadorBulto)
            spw.Param("par$apardoc_oid_parcial").AgregarValorArray(parcial.Identificador)
            spw.Param("par$apardoc_oid_externo").AgregarValorArray(parcial.IdentificadorExterno)
            spw.Param("par$apardoc_usuario_resp").AgregarValorArray(parcial.UsuarioResponsable)
            spw.Param("par$apardoc_puesto_resp").AgregarValorArray(parcial.PuestoResponsable)
            spw.Param("par$apardoc_nec_secuencia").AgregarValorArray(parcial.Secuencia)

            'O precinto está preparado para uma lista de valores, porem nesta caso terá apanes um valor, por isso recupera o primeiro registro.
            If parcial.Precintos IsNot Nothing AndAlso parcial.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(parcial.Precintos.FirstOrDefault) Then
                spw.Param("par$apardoc_cod_precinto").AgregarValorArray(parcial.Precintos.FirstOrDefault)
            Else
                spw.Param("par$apardoc_cod_precinto").AgregarValorArray(DBNull.Value)
            End If
            If parcial.GrupoTerminosIAC IsNot Nothing Then
                spw.Param("par$apardoc_oid_iac").AgregarValorArray(parcial.GrupoTerminosIAC.Identificador)
            Else
                spw.Param("par$apardoc_oid_iac").AgregarValorArray(DBNull.Value)
            End If
            If parcial.ElementoPadre IsNot Nothing Then
                spw.Param("par$apardoc_oid_parcial_padre").AgregarValorArray(parcial.ElementoPadre.Identificador)
            Else
                spw.Param("par$apardoc_oid_parcial_padre").AgregarValorArray(DBNull.Value)
            End If
            If parcial.ElementoSustituto IsNot Nothing Then
                spw.Param("par$apardoc_oid_parcial_sub").AgregarValorArray(parcial.ElementoSustituto.Identificador)
            Else
                spw.Param("par$apardoc_oid_parcial_sub").AgregarValorArray(DBNull.Value)
            End If
            If parcial.TipoFormato IsNot Nothing Then
                spw.Param("par$apardoc_oid_tipo_formato").AgregarValorArray(parcial.TipoFormato.Identificador)
            Else
                spw.Param("par$apardoc_oid_tipo_formato").AgregarValorArray(DBNull.Value)
            End If

            ' Parcial - arrays de terminos por parcial
            If parcial.GrupoTerminosIAC IsNot Nothing AndAlso parcial.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                For Each termino In parcial.GrupoTerminosIAC.TerminosIAC

                    spw.Param("par$avtpar_oid_parcial").AgregarValorArray(parcial.Identificador)
                    spw.Param("par$avtpar_oid_termino").AgregarValorArray(termino.Identificador)
                    spw.Param("par$avtpar_cod_termino").AgregarValorArray(termino.Codigo)
                    spw.Param("par$avtpar_des_valor").AgregarValorArray(termino.Valor)
                    spw.Param("par$avtpar_obligatorio").AgregarValorArray(termino.EsObligatorio)

                Next
            End If

            '=== Valores ===
            ColectarAltas_Valores(parcial.Divisas, identificadorRemesa, identificadorBulto, parcial.Identificador, spw)

        End Sub

        Private Shared Sub ColectarAltas_Valores(divisas As ObservableCollection(Of Clases.Divisa),
                                                 identificadorRemesa As String,
                                                 identificadorBulto As String,
                                                 identificadorParcial As String,
                                                 ByRef spw As SPWrapper)

            Dim nivelDetalhe As String
            Dim oid_valor As String
            Dim secuencia As Integer
            Dim ingresado As Boolean

            If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                ' === Divisas ===
                For Each divisa In divisas

                    ' === Medio Pago - Detalle ===
                    If divisa.MediosPago IsNot Nothing Then

                        For Each medioPago In divisa.MediosPago.Where(Function(m) m.Valores IsNot Nothing)
                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

                            For Each valor As Clases.ValorMedioPago In medioPago.Valores.Where(Function(v) v.Importe <> 0)

                                oid_valor = Guid.NewGuid().ToString()

                                spw.Param("par$aelemval_mp_tipo").AgregarValorArray(valor.TipoValor.ToString())
                                spw.Param("par$aelemval_mp_oid_valor").AgregarValorArray(oid_valor)
                                spw.Param("par$aelemval_mp_oid_remesa").AgregarValorArray(identificadorRemesa)
                                spw.Param("par$aelemval_mp_oid_bulto").AgregarValorArray(identificadorBulto)
                                spw.Param("par$aelemval_mp_oid_parcial").AgregarValorArray(identificadorParcial)
                                spw.Param("par$aelemval_mp_oid_divisa").AgregarValorArray(divisa.Identificador)
                                spw.Param("par$aelemval_mp_oid_mediopago").AgregarValorArray(medioPago.Identificador)
                                spw.Param("par$aelemval_mp_cod_tipo_mp").AgregarValorArray(medioPago.Tipo.RecuperarValor())
                                spw.Param("par$aelemval_mp_num_importe").AgregarValorArray(valor.Importe)
                                spw.Param("par$aelemval_mp_nel_cantidad").AgregarValorArray(valor.Cantidad)
                                spw.Param("par$aelemval_mp_cod_nvdetalle").AgregarValorArray(nivelDetalhe)
                                spw.Param("par$aelemval_mp_bol_ingresado").AgregarValorArray(ingresado)
                                spw.Param("par$aelemval_mp_nel_secuencia").AgregarValorArray(secuencia)

                                If valor.Terminos IsNot Nothing Then
                                    ' === Medio Pago - Terminos ===
                                    For Each termino In valor.Terminos.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
                                        spw.Param("par$aelemter_mp_tipo").AgregarValorArray(valor.TipoValor.ToString())
                                        spw.Param("par$aelemter_mp_oid_valor").AgregarValorArray(oid_valor)
                                        spw.Param("par$aelemter_mp_oid_termino").AgregarValorArray(termino.Identificador)
                                        spw.Param("par$aelemter_mp_des_valor").AgregarValorArray(termino.Valor)
                                        spw.Param("par$aelemter_mp_nec_ind_grupo").AgregarValorArray(termino.NecIndiceGrupo)
                                    Next
                                End If
                            Next
                        Next
                    End If

                    ' === Medio Pago - Totales ===
                    If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then

                        nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()

                        For Each valor As Clases.ValorTipoMedioPago In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.Importe <> 0)

                            oid_valor = Guid.NewGuid().ToString()

                            spw.Param("par$aelemval_mp_tipo").AgregarValorArray(valor.TipoValor.ToString())
                            spw.Param("par$aelemval_mp_oid_valor").AgregarValorArray(oid_valor)
                            spw.Param("par$aelemval_mp_oid_remesa").AgregarValorArray(identificadorRemesa)
                            spw.Param("par$aelemval_mp_oid_bulto").AgregarValorArray(identificadorBulto)
                            spw.Param("par$aelemval_mp_oid_parcial").AgregarValorArray(identificadorParcial)
                            spw.Param("par$aelemval_mp_oid_divisa").AgregarValorArray(divisa.Identificador)
                            spw.Param("par$aelemval_mp_oid_mediopago").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemval_mp_cod_tipo_mp").AgregarValorArray(valor.TipoMedioPago.RecuperarValor())
                            spw.Param("par$aelemval_mp_num_importe").AgregarValorArray(valor.Importe)
                            spw.Param("par$aelemval_mp_nel_cantidad").AgregarValorArray(valor.Cantidad)
                            spw.Param("par$aelemval_mp_cod_nvdetalle").AgregarValorArray(nivelDetalhe)
                            spw.Param("par$aelemval_mp_bol_ingresado").AgregarValorArray(ingresado)
                            spw.Param("par$aelemval_mp_nel_secuencia").AgregarValorArray(secuencia)

                        Next
                    End If

                    ' === Efectivo - Detalle ===
                    If divisa.Denominaciones IsNot Nothing Then

                        For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing)

                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
                            For Each valor As Clases.ValorDenominacion In Denominacion.ValorDenominacion.Where(Function(v) v.Importe <> 0)

                                spw.Param("par$aelemvalefe_tipo").AgregarValorArray(valor.TipoValor.ToString())
                                spw.Param("par$aelemvalefe_oid_remesa").AgregarValorArray(identificadorRemesa)
                                spw.Param("par$aelemvalefe_oid_bulto").AgregarValorArray(identificadorBulto)
                                spw.Param("par$aelemvalefe_oid_parcial").AgregarValorArray(identificadorParcial)
                                spw.Param("par$aelemvalefe_oid_divisa").AgregarValorArray(divisa.Identificador)
                                spw.Param("par$aelemvalefe_oid_denom").AgregarValorArray(Denominacion.Identificador)
                                spw.Param("par$aelemvalefe_bol_esbillete").AgregarValorArray(Denominacion.EsBillete)
                                spw.Param("par$aelemvalefe_oid_unid_med").AgregarValorArray(If(valor.UnidadMedida IsNot Nothing, valor.UnidadMedida.Identificador, DBNull.Value))
                                spw.Param("par$aelemvalefe_cod_tipo_efec").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aelemvalefe_num_importe").AgregarValorArray(valor.Importe)
                                spw.Param("par$aelemvalefe_nel_cantidad").AgregarValorArray(valor.Cantidad)
                                spw.Param("par$aelemvalefe_cod_nvdetalle").AgregarValorArray(nivelDetalhe)
                                spw.Param("par$aelemvalefe_bol_ingresado").AgregarValorArray(ingresado)
                                spw.Param("par$aelemvalefe_oid_calidad").AgregarValorArray(If(valor.Calidad IsNot Nothing, valor.Calidad.Identificador, DBNull.Value))

                            Next
                        Next
                    End If

                    ' === Efectivo - Totales ===
                    If divisa.ValoresTotalesEfectivo IsNot Nothing Then

                        For Each valor As Clases.ValorEfectivo In divisa.ValoresTotalesEfectivo.Where(Function(v) v.Importe <> 0)

                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()

                            spw.Param("par$aelemvalefe_tipo").AgregarValorArray(valor.TipoValor.ToString())
                            spw.Param("par$aelemvalefe_oid_remesa").AgregarValorArray(identificadorRemesa)
                            spw.Param("par$aelemvalefe_oid_bulto").AgregarValorArray(identificadorBulto)
                            spw.Param("par$aelemvalefe_oid_parcial").AgregarValorArray(identificadorParcial)
                            spw.Param("par$aelemvalefe_oid_divisa").AgregarValorArray(divisa.Identificador)
                            spw.Param("par$aelemvalefe_oid_denom").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_bol_esbillete").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_oid_unid_med").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_cod_tipo_efec").AgregarValorArray(valor.TipoDetalleEfectivo.RecuperarValor())
                            spw.Param("par$aelemvalefe_num_importe").AgregarValorArray(valor.Importe)
                            spw.Param("par$aelemvalefe_nel_cantidad").AgregarValorArray(0)
                            spw.Param("par$aelemvalefe_cod_nvdetalle").AgregarValorArray(nivelDetalhe)
                            spw.Param("par$aelemvalefe_bol_ingresado").AgregarValorArray(ingresado)
                            spw.Param("par$aelemvalefe_oid_calidad").AgregarValorArray(DBNull.Value)

                        Next
                    End If

                    ' === Efectivo - Totales ===
                    If divisa.ValoresTotalesDivisa IsNot Nothing Then

                        For Each valor As Clases.ValorDivisa In divisa.ValoresTotalesDivisa.Where(Function(v) v.Importe <> 0)

                            nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor()

                            spw.Param("par$aelemvalefe_tipo").AgregarValorArray(valor.TipoValor.ToString())
                            spw.Param("par$aelemvalefe_oid_remesa").AgregarValorArray(identificadorRemesa)
                            spw.Param("par$aelemvalefe_oid_bulto").AgregarValorArray(identificadorBulto)
                            spw.Param("par$aelemvalefe_oid_parcial").AgregarValorArray(identificadorParcial)
                            spw.Param("par$aelemvalefe_oid_divisa").AgregarValorArray(divisa.Identificador)
                            spw.Param("par$aelemvalefe_oid_denom").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_bol_esbillete").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_oid_unid_med").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_cod_tipo_efec").AgregarValorArray(DBNull.Value)
                            spw.Param("par$aelemvalefe_num_importe").AgregarValorArray(valor.Importe)
                            spw.Param("par$aelemvalefe_nel_cantidad").AgregarValorArray(0)
                            spw.Param("par$aelemvalefe_cod_nvdetalle").AgregarValorArray(nivelDetalhe)
                            spw.Param("par$aelemvalefe_bol_ingresado").AgregarValorArray(ingresado)
                            spw.Param("par$aelemvalefe_oid_calidad").AgregarValorArray(DBNull.Value)

                        Next
                    End If

                Next

            End If

        End Sub

        Public Shared Sub PoblarDocumentos_Confirmar(ByRef documentos As ObservableCollection(Of Clases.Documento), ds As DataSet)
            For Each doc In documentos

                Dim rDoc() As DataRow = ds.Tables("doc_rc_documentos").Select("OID_DOCUMENTO ='" & doc.Identificador & "'")
                If rDoc Is Nothing OrElse rDoc.Count = 0 Then
                    rDoc = ds.Tables("doc_rc_documentos").Select("COD_EXTERNO ='" & doc.NumeroExterno & "'")
                End If

                If rDoc IsNot Nothing AndAlso rDoc.Count > 0 Then
                    Dim rowDocumento As DataRow = rDoc(0)
                    With doc
                        .Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String))
                        .IdentificadorGrupo = Util.AtribuirValorObj(rowDocumento("OID_GRUPO_DOCUMENTO"), GetType(String))
                        .IdentificadorMovimentacionFondo = Util.AtribuirValorObj(rowDocumento("OID_MOVIMENTACION_FONDO"), GetType(String))
                        .IdentificadorSustituto = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_SUSTITUTO"), GetType(String))
                        .EstaCertificado = Util.AtribuirValorObj(rowDocumento("BOL_CERTIFICADO"), GetType(Boolean))
                        .NumeroExterno = Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String))
                        .CodigoComprobante = Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String))
                        .FechaHoraGestion = Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime))
                        If rowDocumento("FYH_PLAN_CERTIFICACION_SINGMT") Is DBNull.Value Then
                            .FechaHoraPlanificacionCertificacion = Date.MinValue
                        Else
                            .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(rowDocumento("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                        End If
                        .Estado = If(Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                        .ExportadoSol = If(rowDocumento.Table.Columns.Contains("EXPORTADO_SOL") AndAlso Not rowDocumento("EXPORTADO_SOL").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("EXPORTADO_SOL"), GetType(Boolean)), False)
                        .IdentificadorIntegracion = If(rowDocumento.Table.Columns.Contains("OID_INTEGRACION"), Util.AtribuirValorObj(rowDocumento("OID_INTEGRACION"), GetType(String)), Nothing)
                        .NelIntentoEnvio = If(rowDocumento.Table.Columns.Contains("NEL_INTENTO_ENVIO") AndAlso Not rowDocumento("NEL_INTENTO_ENVIO").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("NEL_INTENTO_ENVIO"), GetType(Boolean)), False)
                        .CodigoCertificacionCuentas = Util.AtribuirValorObj(rowDocumento("COD_CERTIFICACION_CUENTAS"), GetType(String))
                        .EstadosPosibles = ObtenerEstadosPossibles(.Estado)
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                            .TipoDocumento = New Clases.TipoDocumento()
                            .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                        End If
                        .Rowver = Util.AtribuirValorObj(Of String)(rowDocumento("ROWVER"))
                        .MensajeExterno = If(rowDocumento.Table.Columns.Contains("DES_MENSAJE_EXTERNO"), Util.AtribuirValorObj(rowDocumento("DES_MENSAJE_EXTERNO"), GetType(String)), Nothing)

                        If ds.Tables.Contains("doc_rc_historico_mov_doc") AndAlso ds.Tables("doc_rc_historico_mov_doc").Rows.Count > 0 Then
                            Dim rHst() As DataRow = ds.Tables("doc_rc_historico_mov_doc").Select("OID_DOCUMENTO='" & doc.Identificador & "'")
                            If rHst IsNot Nothing AndAlso rHst.Count > 0 Then
                                .Historico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
                                For Each row As DataRow In rHst
                                    Dim historico As New Clases.HistoricoMovimientoDocumento
                                    With historico
                                        .Estado = If(Not row("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO").ToString), Nothing)
                                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                                    End With
                                    .Historico.Add(historico)
                                Next
                            End If
                        End If

                        Genesis.Remesa.poblarRemesas_Confirmar(DirectCast(.Elemento, Clases.Remesa), ds)

                    End With
                End If
            Next
        End Sub

        Public Shared Sub GrabarDocumentoFondos(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                validarCodigoExterno As Boolean,
                                                hacer_commit As Boolean,
                                                ByRef validaciones As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarDocumentoFondos(identificadorLlamada, peticion, validarCodigoExterno, hacer_commit)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("validaciones") Then
                    Dim rValidaciones() As DataRow = ds.Tables("validaciones").Select()

                    If rValidaciones IsNot Nothing AndAlso rValidaciones.Count > 0 Then

                        If validaciones Is Nothing Then validaciones = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                        For Each row As DataRow In rValidaciones
                            validaciones.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = Util.AtribuirValorObj(row(0), GetType(String)), .descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                        Next
                    End If

                End If

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

        Private Shared Function ColectarDocumentoFondos(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                        validarCodigoExterno As Boolean,
                                                        hacer_commit As Boolean) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_fondos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_accion", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.accion.ToString, , False)
            spw.AgregarParam("par$cod_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$adoc_cod_formulario", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.codigoFormulario, , False)
            spw.AgregarParam("par$adoc_cod_externo", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.codigoExterno, , False)
            spw.AgregarParam("par$adoc_fyh_gestion", ProsegurDbType.Data_Hora, peticion.movimiento.fechaHoraGestionFondos, , False)
            spw.AgregarParam("par$adoc_fyh_contable", ProsegurDbType.Data_Hora, peticion.movimiento.FechaContable, , False)
            spw.AgregarParam("par$adoc_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.ActualId, , False)
            spw.AgregarParam("par$adoc_cod_collection_id", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.CollectionId, , False)
            spw.AgregarParam("par$adoc_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoCliente, , False)
            spw.AgregarParam("par$adoc_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSubCliente, , False)
            spw.AgregarParam("par$adoc_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPuntoServicio, , False)
            spw.AgregarParam("par$adoc_ori_cod_canal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoCanal, , False)
            spw.AgregarParam("par$adoc_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSubCanal, , False)
            spw.AgregarParam("par$adoc_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoDelegacion, , False)
            spw.AgregarParam("par$adoc_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPlanta, , False)
            spw.AgregarParam("par$adoc_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSector, , False)

            If peticion.movimiento.destino IsNot Nothing AndAlso Not (peticion.movimiento.destino.codigoCliente = peticion.movimiento.origen.codigoCliente AndAlso
                    peticion.movimiento.destino.codigoSubCliente = peticion.movimiento.origen.codigoSubCliente AndAlso
                    peticion.movimiento.destino.codigoPuntoServicio = peticion.movimiento.origen.codigoPuntoServicio AndAlso
                    peticion.movimiento.destino.codigoDelegacion = peticion.movimiento.origen.codigoDelegacion AndAlso
                    peticion.movimiento.destino.codigoPlanta = peticion.movimiento.origen.codigoPlanta AndAlso
                    peticion.movimiento.destino.codigoSector = peticion.movimiento.origen.codigoSector AndAlso
                    peticion.movimiento.destino.codigoCanal = peticion.movimiento.origen.codigoCanal AndAlso
                    peticion.movimiento.destino.codigoSubCanal = peticion.movimiento.origen.codigoSubCanal) Then

                spw.AgregarParam("par$adoc_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoCliente, , False)
                spw.AgregarParam("par$adoc_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSubCliente, , False)
                spw.AgregarParam("par$adoc_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoPuntoServicio, , False)
                spw.AgregarParam("par$adoc_des_cod_canal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoCanal, , False)
                spw.AgregarParam("par$adoc_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSubCanal, , False)
                spw.AgregarParam("par$adoc_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoDelegacion, , False)
                spw.AgregarParam("par$adoc_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoPlanta, , False)
                spw.AgregarParam("par$adoc_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSector, , False)

            Else

                spw.AgregarParam("par$adoc_des_cod_client", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoCliente, , False)
                spw.AgregarParam("par$adoc_des_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSubCliente, , False)
                spw.AgregarParam("par$adoc_des_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPuntoServicio, , False)
                spw.AgregarParam("par$adoc_des_cod_canal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoCanal, , False)
                spw.AgregarParam("par$adoc_des_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSubCanal, , False)
                spw.AgregarParam("par$adoc_des_cod_delega", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoDelegacion, , False)
                spw.AgregarParam("par$adoc_des_cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPlanta, , False)
                spw.AgregarParam("par$adoc_des_cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSector, , False)

            End If

            spw.AgregarParam("par$aefdoc_cod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe_total", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_denominacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_div_den", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_medio_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$avtdoc_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$bol_validar_cod_externo", ProsegurDbType.Objeto_Id, If(validarCodigoExterno, 1, 0), , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParamInfo("par$info_ejecucion")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            If peticion.movimiento.valores IsNot Nothing Then

                ' Valores Medio Pago
                If peticion.movimiento.valores.divisas IsNot Nothing AndAlso peticion.movimiento.valores.divisas.Count > 0 Then
                    For Each efectivo In peticion.movimiento.valores.divisas

                        spw.Param("par$aefdoc_cod_divisa").AgregarValorArray(efectivo.codigoDivisa)
                        spw.Param("par$aefdoc_num_importe_total").AgregarValorArray(efectivo.importe)

                        If efectivo.denominaciones IsNot Nothing AndAlso efectivo.denominaciones.Count > 0 Then
                            For Each denominacion In efectivo.denominaciones

                                spw.Param("par$aefdoc_cod_denominacion").AgregarValorArray(denominacion.codigoDenominacion)
                                spw.Param("par$aefdoc_cod_div_den").AgregarValorArray(efectivo.codigoDivisa)
                                spw.Param("par$aefdoc_num_importe").AgregarValorArray(denominacion.importe)
                                spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(denominacion.cantidad)

                            Next
                        End If
                    Next
                End If

                ' Valores Medio Pago
                If peticion.movimiento.valores.mediosDePago IsNot Nothing AndAlso peticion.movimiento.valores.mediosDePago.Count > 0 Then
                    For Each mediopago In peticion.movimiento.valores.mediosDePago
                        spw.Param("par$ampdoc_cod_divisa").AgregarValorArray(mediopago.codigoDivisa)
                        spw.Param("par$ampdoc_cod_medio_pago").AgregarValorArray(mediopago.codigoMedioDePago)
                        spw.Param("par$ampdoc_num_importe").AgregarValorArray(mediopago.importe)
                        spw.Param("par$ampdoc_nel_cantidad").AgregarValorArray(mediopago.cantidad)
                    Next
                End If

            End If

            ' Valores Terminos
            If peticion.movimiento.camposAdicionales IsNot Nothing AndAlso peticion.movimiento.camposAdicionales.Count > 0 Then
                For Each termino In peticion.movimiento.camposAdicionales
                    If Not String.IsNullOrEmpty(termino.nombre) AndAlso Not String.IsNullOrEmpty(termino.valor) Then
                        spw.Param("par$avtdoc_cod_termino").AgregarValorArray(termino.nombre)
                        spw.Param("par$avtdoc_des_valor").AgregarValorArray(termino.valor)
                    End If
                Next
            End If

            Return spw

        End Function

#End Region

#Region " Procedure - Recuperar"
        Public Shared Function RecuperarDocumentosMAE(codigoDelegacion As String,
                                                    ByRef codigoMAE As String,
                                                    fechaTransaciones As Date) As List(Of Clases.DocumentoMae)

            Dim documentos As List(Of Clases.DocumentoMae) = Nothing

            Try
                Dim ds As DataSet = Nothing
                Dim SP As String = String.Format("sapr_pdocumento_{0}.srecuperar_documentos_mae", Prosegur.Genesis.Comon.Util.Version)
                Dim spw As New SPWrapper(SP, False)
                spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion, , False)
                spw.AgregarParam("par$cod_identificacion", ProsegurDbType.Descricao_Longa, codigoMAE, , False)
                spw.AgregarParam("par$fec_transacciones", ProsegurDbType.Data_Hora, fechaTransaciones, , False)
                spw.AgregarParam("par$rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "rc_documentos")

                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                If ds.Tables.Contains("rc_documentos") AndAlso ds.Tables("rc_documentos").Rows.Count > 0 Then
                    documentos = New List(Of Clases.DocumentoMae)

                    For Each row As DataRow In ds.Tables("rc_documentos").Rows
                        Dim documento As New Clases.DocumentoMae
                        documento.CodigoPuntoServicio = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
                        documento.DescripcionPuntoServicio = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String))
                        documento.CodigoTransaccion = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))
                        documento.CodigoIsoDivisa = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                        documento.FechaGestion = Util.AtribuirValorObj(row("FYH_GESTION"), GetType(DateTime))
                        documento.DescripcionFormulario = Util.AtribuirValorObj(row("DES_FORMULARIO"), GetType(String))
                        documento.CodigoFormulario = Util.AtribuirValorObj(row("COD_FORMULARIO"), GetType(String))
                        documento.IdentificadorDocumento = Util.AtribuirValorObj(row("OID_DOCUMENTO"), GetType(String))
                        documento.Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                        documento.MAE = Util.AtribuirValorObj(row("COD_IDENTIFICACION"), GetType(String))

                        documentos.Add(documento)
                    Next
                End If

                ds.Dispose()
                ds = Nothing
                spw = Nothing

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return documentos
        End Function

        Public Shared Function verificarDocumentoExiste(peticion As Clases.Transferencias.FiltroDocumentosAltas) As ObservableCollection(Of Clases.Documento)

            Dim _Documentos As ObservableCollection(Of Clases.Documento) = Nothing

            Try

                If peticion IsNot Nothing Then
                    Dim ds As DataSet = Nothing

                    Dim spw As SPWrapper = ColectarVerificarDocumentoExiste(peticion)
                    ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                    _Documentos = poblarDocumentos(ds)
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return _Documentos
        End Function

        Public Shared Function ColectarVerificarDocumentoExiste(peticion As Clases.Transferencias.FiltroDocumentosAltas) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumento_{0}.sverificar_documento_existe", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$acod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_client", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_subcli", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_ptserv", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_subcan", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_delega", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$amov_ori_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_documentos_padre", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos_padre")
            spw.AgregarParam("par$doc_rc_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_formulario")
            spw.AgregarParam("par$doc_rc_accion_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_accion_contable")
            spw.AgregarParam("par$doc_rc_estado_acc_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_estado_acc_contable")
            spw.AgregarParam("par$doc_rc_caract_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_formulario")
            spw.AgregarParam("par$doc_rc_grp_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_grp_terminos_indiv")
            spw.AgregarParam("par$doc_rc_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_indiv")
            spw.AgregarParam("par$doc_rc_valor_terminos_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valor_terminos_doc")
            spw.AgregarParam("par$doc_rc_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_sectores")
            spw.AgregarParam("par$doc_rc_tipos_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sectores")
            spw.AgregarParam("par$doc_rc_caract_tp_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_tp_sectores")
            spw.AgregarParam("par$doc_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$doc_rc_valores_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valores_documentos")
            spw.AgregarParam("par$doc_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_divisas")
            spw.AgregarParam("par$doc_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_denominaciones")
            spw.AgregarParam("par$doc_rc_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_mediospago")
            spw.AgregarParam("par$doc_rc_unidades_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_unidades_medida")
            spw.AgregarParam("par$doc_rc_calidades", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_calidades")
            spw.AgregarParam("par$doc_rc_terminos_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_mediospago")
            spw.AgregarParam("par$doc_rc_plantas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_plantas")
            spw.AgregarParam("par$doc_rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_delegaciones")
            spw.AgregarParam("par$doc_rc_tipos_sector_planta", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sector_planta")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$ele_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cuentas")
            spw.AgregarParam("par$ele_rc_carac_tipo_sector", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_carac_tipo_sector")
            spw.AgregarParam("par$ele_rc_val_det_efectivo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_efectivo")
            spw.AgregarParam("par$ele_rc_val_det_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_medio_pago")
            spw.AgregarParam("par$ele_rc_val_totales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_totales")
            spw.AgregarParam("par$ele_rc_lista_valor", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_lista_valor")
            spw.AgregarParam("par$ele_rc_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_iac")
            spw.AgregarParam("par$ele_rc_terminos_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_iac")
            spw.AgregarParam("par$ele_rc_valor_termino_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino_iac")
            spw.AgregarParam("par$ele_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_divisas")
            spw.AgregarParam("par$ele_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_denominaciones")
            spw.AgregarParam("par$ele_rc_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_medio_pago")
            spw.AgregarParam("par$ele_rc_unidad_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_unidad_medida")
            spw.AgregarParam("par$ele_rc_calidad", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_calidad")
            spw.AgregarParam("par$ele_rc_valor_termino", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino")
            spw.AgregarParam("par$ele_rc_cont_precintos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cont_precintos")
            spw.AgregarParam("par$usuario", ParamTypes.String, peticion.CodigoUsuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            For Each remesa In peticion.remesas
                spw.Param("par$acod_externo").AgregarValorArray(remesa.CodigoExterno)
                spw.Param("par$amov_ori_cod_client").AgregarValorArray(remesa.CodigoCliente)
                spw.Param("par$amov_ori_cod_subcli").AgregarValorArray(remesa.CodigoSubCliente)
                spw.Param("par$amov_ori_cod_ptserv").AgregarValorArray(remesa.CodigoPuntoServicio)
                spw.Param("par$amov_ori_cod_canal").AgregarValorArray(remesa.CodigoCanal)
                spw.Param("par$amov_ori_cod_subcan").AgregarValorArray(remesa.CodigoSubCanal)
                spw.Param("par$amov_ori_cod_delega").AgregarValorArray(remesa.CodigoDelegacion)
                spw.Param("par$amov_ori_cod_planta").AgregarValorArray(remesa.CodigoPlanta)
                spw.Param("par$amov_ori_cod_sector").AgregarValorArray(remesa.CodigoSector)
            Next

            Return spw
        End Function

        Public Shared Function recuperarUltimoDocumentosPorIdentificadores(identificadoresDocumentos As List(Of String),
                                                                           TrabajaPorBulto As Boolean,
                                                                           usuario As String) As ObservableCollection(Of Clases.Documento)

            Dim _Documentos As ObservableCollection(Of Clases.Documento) = Nothing

            Try

                If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then
                    Dim ds As DataSet = Nothing

                    Dim spw As SPWrapper = ColectarUltimoDocumentosRecuperar(identificadoresDocumentos, TrabajaPorBulto, usuario)
                    ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                    _Documentos = poblarDocumentos(ds)
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return _Documentos
        End Function

        Public Shared Function ColectarUltimoDocumentosRecuperar(identificadoresDocumentos As List(Of String),
                                                                 TrabajaPorBulto As Boolean,
                                                                 usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumento_{0}.srecuperar_ultimodocumentos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oids_documentos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$bol_gestion_bultos", ParamTypes.Integer, If(TrabajaPorBulto, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_documentos_padre", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos_padre")
            spw.AgregarParam("par$doc_rc_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_formulario")
            spw.AgregarParam("par$doc_rc_accion_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_accion_contable")
            spw.AgregarParam("par$doc_rc_estado_acc_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_estado_acc_contable")
            spw.AgregarParam("par$doc_rc_caract_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_formulario")
            spw.AgregarParam("par$doc_rc_grp_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_grp_terminos_indiv")
            spw.AgregarParam("par$doc_rc_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_indiv")
            spw.AgregarParam("par$doc_rc_valor_terminos_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valor_terminos_doc")
            spw.AgregarParam("par$doc_rc_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_sectores")
            spw.AgregarParam("par$doc_rc_tipos_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sectores")
            spw.AgregarParam("par$doc_rc_caract_tp_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_tp_sectores")
            spw.AgregarParam("par$doc_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$doc_rc_valores_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valores_documentos")
            spw.AgregarParam("par$doc_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_divisas")
            spw.AgregarParam("par$doc_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_denominaciones")
            spw.AgregarParam("par$doc_rc_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_mediospago")
            spw.AgregarParam("par$doc_rc_unidades_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_unidades_medida")
            spw.AgregarParam("par$doc_rc_calidades", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_calidades")
            spw.AgregarParam("par$doc_rc_terminos_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_mediospago")
            spw.AgregarParam("par$doc_rc_plantas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_plantas")
            spw.AgregarParam("par$doc_rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_delegaciones")
            spw.AgregarParam("par$doc_rc_tipos_sector_planta", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sector_planta")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$ele_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cuentas")
            spw.AgregarParam("par$ele_rc_carac_tipo_sector", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_carac_tipo_sector")
            spw.AgregarParam("par$ele_rc_val_det_efectivo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_efectivo")
            spw.AgregarParam("par$ele_rc_val_det_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_medio_pago")
            spw.AgregarParam("par$ele_rc_val_totales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_totales")
            spw.AgregarParam("par$ele_rc_lista_valor", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_lista_valor")
            spw.AgregarParam("par$ele_rc_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_iac")
            spw.AgregarParam("par$ele_rc_terminos_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_iac")
            spw.AgregarParam("par$ele_rc_valor_termino_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino_iac")
            spw.AgregarParam("par$ele_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_divisas")
            spw.AgregarParam("par$ele_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_denominaciones")
            spw.AgregarParam("par$ele_rc_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_medio_pago")
            spw.AgregarParam("par$ele_rc_unidad_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_unidad_medida")
            spw.AgregarParam("par$ele_rc_calidad", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_calidad")
            spw.AgregarParam("par$ele_rc_valor_termino", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino")
            spw.AgregarParam("par$ele_rc_cont_precintos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cont_precintos")
            spw.AgregarParam("par$ejecucion_interna", ParamTypes.Integer, 0, ParameterDirection.Output, False)
            spw.AgregarParam("par$usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$inserts", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Dim _identificadoresDocumentos = If(identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0, identificadoresDocumentos.Distinct, Nothing)
            If _identificadoresDocumentos IsNot Nothing AndAlso _identificadoresDocumentos.Count > 0 Then
                For Each identifacadores In _identificadoresDocumentos
                    spw.Param("par$oids_documentos").AgregarValorArray(identifacadores)
                Next
            Else
                spw.Param("par$oids_documentos").AgregarValorArray(DBNull.Value)
            End If

            Return spw
        End Function

        Public Shared Function recuperarDocumentosPorIdentificadores(identificadoresDocumentos As List(Of String),
                                                                     usuario As String,
                                                                     ByRef TransaccionActual As Transaccion) As ObservableCollection(Of Clases.Documento)

            Dim _Documentos As ObservableCollection(Of Clases.Documento) = Nothing

            Try
                If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then
                    Dim ds As DataSet = Nothing

                    Dim spw As SPWrapper = ColectarDocumentosRecuperar(identificadoresDocumentos, usuario)
                    ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                    _Documentos = poblarDocumentos(ds)
                End If
            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return _Documentos
        End Function

        Public Shared Function ColectarDocumentosRecuperar(identificadoresDocumentos As List(Of String),
                                                        usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumento_{0}.srecuperar_documentos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oids_documentos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_documentos_padre", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos_padre")
            spw.AgregarParam("par$doc_rc_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_formulario")
            spw.AgregarParam("par$doc_rc_accion_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_accion_contable")
            spw.AgregarParam("par$doc_rc_estado_acc_contable", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_estado_acc_contable")
            spw.AgregarParam("par$doc_rc_caract_formulario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_formulario")
            spw.AgregarParam("par$doc_rc_grp_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_grp_terminos_indiv")
            spw.AgregarParam("par$doc_rc_terminos_indiv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_indiv")
            spw.AgregarParam("par$doc_rc_valor_terminos_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valor_terminos_doc")
            spw.AgregarParam("par$doc_rc_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_sectores")
            spw.AgregarParam("par$doc_rc_tipos_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sectores")
            spw.AgregarParam("par$doc_rc_caract_tp_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_tp_sectores")
            spw.AgregarParam("par$doc_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$doc_rc_valores_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valores_documentos")
            spw.AgregarParam("par$doc_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_divisas")
            spw.AgregarParam("par$doc_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_denominaciones")
            spw.AgregarParam("par$doc_rc_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_mediospago")
            spw.AgregarParam("par$doc_rc_unidades_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_unidades_medida")
            spw.AgregarParam("par$doc_rc_calidades", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_calidades")
            spw.AgregarParam("par$doc_rc_terminos_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_mediospago")
            spw.AgregarParam("par$doc_rc_plantas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_plantas")
            spw.AgregarParam("par$doc_rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_delegaciones")
            spw.AgregarParam("par$doc_rc_tipos_sector_planta", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sector_planta")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$ele_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cuentas")
            spw.AgregarParam("par$ele_rc_carac_tipo_sector", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_carac_tipo_sector")
            spw.AgregarParam("par$ele_rc_val_det_efectivo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_efectivo")
            spw.AgregarParam("par$ele_rc_val_det_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_det_medio_pago")
            spw.AgregarParam("par$ele_rc_val_totales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valores_totales")
            spw.AgregarParam("par$ele_rc_lista_valor", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_lista_valor")
            spw.AgregarParam("par$ele_rc_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_iac")
            spw.AgregarParam("par$ele_rc_terminos_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_iac")
            spw.AgregarParam("par$ele_rc_valor_termino_iac", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino_iac")
            spw.AgregarParam("par$ele_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_divisas")
            spw.AgregarParam("par$ele_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_denominaciones")
            spw.AgregarParam("par$ele_rc_medio_pago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_medio_pago")
            spw.AgregarParam("par$ele_rc_unidad_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_unidad_medida")
            spw.AgregarParam("par$ele_rc_calidad", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_calidad")
            spw.AgregarParam("par$ele_rc_valor_termino", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_valor_termino")
            spw.AgregarParam("par$ele_rc_cont_precintos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_cont_precintos")
            spw.AgregarParam("par$ejecucion_interna", ParamTypes.Integer, 0, ParameterDirection.Output, False)
            spw.AgregarParam("par$usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$inserts", ParamTypes.Long, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Dim _identificadoresDocumentos = If(identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0, identificadoresDocumentos.Distinct, Nothing)
            If _identificadoresDocumentos IsNot Nothing AndAlso _identificadoresDocumentos.Count > 0 Then
                For Each identifacadores In _identificadoresDocumentos
                    spw.Param("par$oids_documentos").AgregarValorArray(identifacadores)
                Next
            Else
                spw.Param("par$oids_documentos").AgregarValorArray(DBNull.Value)
            End If

            Return spw
        End Function

        Public Shared Function poblarDocumentos(ds As DataSet,
                                                Optional _formularios As List(Of Clases.Formulario) = Nothing,
                                                Optional _cuentas As List(Of Clases.Cuenta) = Nothing) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)
            If ds.Tables.Contains("doc_rc_documentos") AndAlso ds.Tables("doc_rc_documentos").Rows.Count > 0 Then

                If _cuentas Is Nothing Then

                    Dim _identificadorDelegacion As New Dictionary(Of String, String)
                    Dim _delegaciones As List(Of Clases.Delegacion) = CargarDelegacion(ds)
                    Dim _tipoSectores As List(Of Clases.TipoSector) = CargarTipoSector(ds)
                    Dim _plantas As List(Of Clases.Planta) = CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
                    Dim _sectores As List(Of Clases.Sector) = CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)

                    _cuentas = New List(Of Clases.Cuenta)
                    _cuentas = CargarCuenta(ds)

                End If

                If _formularios Is Nothing Then

                    Dim _gruposIAC As List(Of Clases.GrupoTerminosIAC) = CargarGrupoTerminosIAC(ds)
                    Dim _acciones As List(Of Clases.AccionContable) = CargarAccionContable(ds)

                    _formularios = New List(Of Clases.Formulario)
                    _formularios = CargarFormulario(ds, _acciones, _gruposIAC)

                End If


                Dim _divisas As List(Of Clases.Divisa) = CargarDivisa(ds)
                CargarDenominacion(ds, _divisas)

                Dim elementos As ObservableCollection(Of Clases.Elemento) = Genesis.Elemento.poblarElementos(ds, , _cuentas)

                For Each rowDocumento As DataRow In ds.Tables("doc_rc_documentos").Rows
                    'separados en carga individual para poder reutilizarla para los documentos padre

                    Dim documento As Clases.Documento = CargarDocumento(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), _formularios, _cuentas, _divisas)
                    If elementos IsNot Nothing AndAlso elementos.Count > 0 Then

                        documento.Elemento = elementos.FirstOrDefault(Function(x) x.IdentificadorDocumento = documento.Identificador)
                    End If

                    documentos.Add(documento)
                Next
            End If

            Return documentos
        End Function

        Private Shared Function CargarDocumento(ds As DataSet, IdentificadorDocumento As String,
                                                Optional _formularios As List(Of Clases.Formulario) = Nothing,
                                                Optional _cuentas As List(Of Clases.Cuenta) = Nothing,
                                                Optional _divisas As List(Of Clases.Divisa) = Nothing,
                                                Optional _esPadre As Boolean = False) As Clases.Documento

            Dim doc As Clases.Documento = Nothing

            If Not String.IsNullOrEmpty(IdentificadorDocumento) AndAlso ds.Tables.Contains("doc_rc_documentos") AndAlso ds.Tables("doc_rc_documentos").Rows.Count > 0 Then
                Dim rDoc() As DataRow = ds.Tables("doc_rc_documentos").Select("OID_DOCUMENTO ='" & IdentificadorDocumento & "'")
                If rDoc IsNot Nothing AndAlso rDoc.Count > 0 Then
                    Dim rowDocumento As DataRow = rDoc(0)
                    doc = New Clases.Documento
                    With doc
                        .Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String))
                        .IdentificadorGrupo = Util.AtribuirValorObj(rowDocumento("OID_GRUPO_DOCUMENTO"), GetType(String))
                        .IdentificadorMovimentacionFondo = Util.AtribuirValorObj(rowDocumento("OID_MOVIMENTACION_FONDO"), GetType(String))
                        .IdentificadorSustituto = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_SUSTITUTO"), GetType(String))
                        .EstaCertificado = Util.AtribuirValorObj(rowDocumento("BOL_CERTIFICADO"), GetType(Boolean))
                        .NumeroExterno = Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String))
                        .CodigoComprobante = Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String))
                        .FechaHoraGestion = Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime))
                        If rowDocumento("FYH_PLAN_CERTIFICACION_SINGMT") Is DBNull.Value Then
                            .FechaHoraPlanificacionCertificacion = Date.MinValue
                        Else
                            .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(rowDocumento("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                        End If
                        If rowDocumento("BOL_SALDO_SUPRIMIDO") Is Nothing OrElse rowDocumento("BOL_SALDO_SUPRIMIDO") Is DBNull.Value Then
                            .SaldoSuprimido = False
                        Else
                            .SaldoSuprimido = Util.AtribuirValorObj(rowDocumento("BOL_SALDO_SUPRIMIDO"), GetType(Boolean))
                        End If

                        If rowDocumento("BOL_NOTIFICADO") Is Nothing OrElse rowDocumento("BOL_NOTIFICADO") Is DBNull.Value Then
                            .Notificado = False
                        Else
                            .Notificado = Util.AtribuirValorObj(rowDocumento("BOL_NOTIFICADO"), GetType(Boolean))
                        End If

                        .Estado = If(Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                        .ExportadoSol = If(rowDocumento.Table.Columns.Contains("EXPORTADO_SOL") AndAlso Not rowDocumento("EXPORTADO_SOL").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("EXPORTADO_SOL"), GetType(Boolean)), False)
                        .IdentificadorIntegracion = If(rowDocumento.Table.Columns.Contains("OID_INTEGRACION"), Util.AtribuirValorObj(rowDocumento("OID_INTEGRACION"), GetType(String)), Nothing)
                        .NelIntentoEnvio = If(rowDocumento.Table.Columns.Contains("NEL_INTENTO_ENVIO") AndAlso Not rowDocumento("NEL_INTENTO_ENVIO").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("NEL_INTENTO_ENVIO"), GetType(Boolean)), False)
                        .CodigoCertificacionCuentas = Util.AtribuirValorObj(rowDocumento("COD_CERTIFICACION_CUENTAS"), GetType(String))
                        .EstadosPosibles = ObtenerEstadosPossibles(.Estado)
                        .MensajeExterno = If(rowDocumento.Table.Columns.Contains("DES_MENSAJE_EXTERNO"), Util.AtribuirValorObj(rowDocumento("DES_MENSAJE_EXTERNO"), GetType(String)), Nothing)

                        .Historico = CargarHistoricosDocumento(ds, .Identificador)
                        .Formulario = _formularios.FirstOrDefault(Function(f) f.Identificador = Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String)))

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                            .TipoDocumento = New Clases.TipoDocumento()
                            .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                        End If
                        .DocumentoPadre = CargarDocumento(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String)), _formularios, _cuentas, , True)
                        .CuentaOrigen = _cuentas.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String)))
                        .CuentaDestino = _cuentas.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String)))
                        .CuentaSaldoOrigen = _cuentas.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String)))
                        .CuentaSaldoDestino = _cuentas.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String)))

                        If .Formulario IsNot Nothing AndAlso .Formulario.GrupoTerminosIACIndividual IsNot Nothing Then
                            .GrupoTerminosIAC = .Formulario.GrupoTerminosIACIndividual.Clonar
                            If .GrupoTerminosIAC IsNot Nothing AndAlso .GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                                CargarValoresTerminosDocumento(ds, .Identificador, .GrupoTerminosIAC.TerminosIAC)
                            End If
                        End If
                        .Rowver = Util.AtribuirValorObj(Of String)(rowDocumento("ROWVER"))
                        .Divisas = CargarValoresDocumento(ds, IdentificadorDocumento, True, False, _divisas)

                    End With

                ElseIf _esPadre Then
                    doc = New Clases.Documento With {.Identificador = IdentificadorDocumento}
                End If
            End If

            Return doc
        End Function

        Private Shared Function ObtenerEstadosPossibles(ByRef estadoDocumento As Enumeradores.EstadoDocumento) As ObservableCollection(Of Enumeradores.EstadoDocumento)
            Dim estados As New ObservableCollection(Of Enumeradores.EstadoDocumento)
            Select Case estadoDocumento
                Case Enumeradores.EstadoDocumento.Nuevo
                    estados.Add(Enumeradores.EstadoDocumento.EnCurso)
                Case Enumeradores.EstadoDocumento.EnCurso
                    estados.Add(Enumeradores.EstadoDocumento.EnCurso)
                    estados.Add(Enumeradores.EstadoDocumento.Anulado)
                    estados.Add(Enumeradores.EstadoDocumento.Confirmado)
                Case Enumeradores.EstadoDocumento.Confirmado
                    estados.Add(Enumeradores.EstadoDocumento.Aceptado)
                    estados.Add(Enumeradores.EstadoDocumento.Rechazado)
                Case Enumeradores.EstadoDocumento.Aceptado
                    estados.Add(Enumeradores.EstadoDocumento.Sustituido)
                Case Else
                    Return Nothing
            End Select
            Return estados
        End Function

        Private Shared Function CargarHistoricosDocumento(ds As DataSet, IdentificadorDocumento As String) As ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
            Dim Historicos As ObservableCollection(Of Clases.HistoricoMovimientoDocumento) = Nothing
            If ds.Tables.Contains("doc_rc_historico_mov_doc") AndAlso ds.Tables("doc_rc_historico_mov_doc").Rows.Count > 0 Then
                Dim rHst() As DataRow = ds.Tables("doc_rc_historico_mov_doc").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
                If rHst IsNot Nothing AndAlso rHst.Count > 0 Then
                    Historicos = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
                    For Each row As DataRow In rHst
                        Dim historico As New Clases.HistoricoMovimientoDocumento
                        With historico
                            .Estado = If(Not row("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO").ToString), Nothing)
                            .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                            Historicos.Add(historico)
                        End With
                    Next
                End If
            End If
            Return Historicos
        End Function

        Public Shared Function CargarCuenta(ds As DataSet,
                                            Optional _identificadorDelegacion As Dictionary(Of String, String) = Nothing,
                                            Optional _delegaciones As List(Of Clases.Delegacion) = Nothing,
                                            Optional _tipoSectores As List(Of Clases.TipoSector) = Nothing,
                                            Optional _plantas As List(Of Clases.Planta) = Nothing,
                                            Optional _sectores As List(Of Clases.Sector) = Nothing) As List(Of Clases.Cuenta)

            If _identificadorDelegacion Is Nothing Then
                _identificadorDelegacion = New Dictionary(Of String, String)
            End If

            If _delegaciones Is Nothing Then
                _delegaciones = CargarDelegacion(ds)
            End If

            If _tipoSectores Is Nothing Then
                _tipoSectores = CargarTipoSector(ds)
            End If

            If _plantas Is Nothing Then
                _plantas = CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
            End If

            If _sectores Is Nothing Then
                _sectores = CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)
            End If

            Dim _cuentas As New List(Of Clases.Cuenta)

            If ds.Tables.Contains("doc_rc_cuentas") AndAlso ds.Tables("doc_rc_cuentas").Rows.Count > 0 Then

                For Each rowCuenta As DataRow In ds.Tables("doc_rc_cuentas").Rows

                    If _cuentas.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowCuenta("OID_CUENTA"), GetType(String))) Is Nothing Then

                        Dim _cuenta As New Clases.Cuenta

                        With _cuenta
                            .Identificador = Util.AtribuirValorObj(rowCuenta("OID_CUENTA"), GetType(String))
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))) Then
                                Select Case Util.AtribuirValorObj(rowCuenta("COD_TIPO_CUENTA"), GetType(String))
                                    Case "A"
                                        .TipoCuenta = Enumeradores.TipoCuenta.Ambos
                                    Case "M"
                                        .TipoCuenta = Enumeradores.TipoCuenta.Movimiento
                                    Case "S"
                                        .TipoCuenta = Enumeradores.TipoCuenta.Saldo
                                End Select
                            End If

                            ' CL - Clienta
                            .Cliente = New Clases.Cliente With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)),
                                            .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_CLIENTE"), GetType(String)),
                                            .EstaActivo = Util.AtribuirValorObj(rowCuenta("CL_BOL_VIGENTE"), GetType(Boolean)),
                                            .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("CL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)),
                                            .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("CL_BOL_ENVIADO_SALDOS"), GetType(Boolean)),
                                            .Identificador = Util.AtribuirValorObj(rowCuenta("OID_CLIENTE"), GetType(String))}

                            ' PTO - Punto Servicio
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))) Then
                                .PuntoServicio = New Clases.PuntoServicio With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_PTO_SERVICIO"), GetType(String)),
                                                                                .EstaActivo = Util.AtribuirValorObj(rowCuenta("PTO_BOL_VIGENTE"), GetType(Boolean)),
                                                                                .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("PTO_BOL_ENVIADO_SALDOS"), GetType(Boolean)),
                                                                                .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("PTO_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)),
                                                                                .Identificador = Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))}
                            End If

                            ' SCL - SubCliente
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))) Then
                                .SubCliente = New Clases.SubCliente With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)),
                                                                          .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_SUBCLIENTE"), GetType(String)),
                                                                          .EstaActivo = Util.AtribuirValorObj(rowCuenta("SCL_BOL_VIGENTE"), GetType(Boolean)),
                                                                          .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("SCL_BOL_ENVIADO_SALDOS"), GetType(Boolean)),
                                                                          .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("SCL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)),
                                                                          .Identificador = Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))}
                            End If

                            ' CAN - Canal
                            .Canal = New Clases.Canal With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)),
                                                            .Identificador = Util.AtribuirValorObj(rowCuenta("OID_CANAL"), GetType(String)),
                                                            .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_CANAL"), GetType(String)),
                                                            .EstaActivo = Util.AtribuirValorObj(rowCuenta("CAN_BOL_VIGENTE"), GetType(Boolean))}

                            ' SBC - SubCanal
                            .SubCanal = New Clases.SubCanal With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)),
                                                                  .Identificador = Util.AtribuirValorObj(rowCuenta("OID_SUBCANAL"), GetType(String)),
                                                                  .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_SUBCANAL"), GetType(String)),
                                                                  .EstaActivo = Util.AtribuirValorObj(rowCuenta("SBC_BOL_VIGENTE"), GetType(Boolean))}

                            .Sector = _sectores.FirstOrDefault(Function(s) s.Identificador = Util.AtribuirValorObj(rowCuenta("OID_SECTOR"), GetType(String)))
                            If .Sector Is Nothing Then
                                .Sector = New Clases.Sector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_SECTOR"), GetType(String)), Nothing),
                                                          .Identificador = If(rowCuenta.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_SECTOR"), GetType(String)), Nothing),
                                                          .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_SECTOR"), GetType(String)), Nothing),
                                                          .EsActivo = If(rowCuenta.Table.Columns.Contains("SE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("SE_BOL_ACTIVO"), GetType(Boolean)), Nothing),
                                                          .EsCentroProceso = If(rowCuenta.Table.Columns.Contains("BOL_CENTRO_PROCESO"), Util.AtribuirValorObj(rowCuenta("BOL_CENTRO_PROCESO"), GetType(Boolean)), Nothing),
                                                          .EsConteo = If(rowCuenta.Table.Columns.Contains("BOL_CONTEO"), Util.AtribuirValorObj(rowCuenta("BOL_CONTEO"), GetType(Boolean)), Nothing),
                                                          .EsTesoro = If(rowCuenta.Table.Columns.Contains("BOL_TESORO"), Util.AtribuirValorObj(rowCuenta("BOL_TESORO"), GetType(Boolean)), Nothing),
                                                          .PemitirDisponerValor = If(rowCuenta.Table.Columns.Contains("BOL_PERMITE_DISPONER_VALOR"), Util.AtribuirValorObj(rowCuenta("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)), Nothing),
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_DELEGACION"), Util.AtribuirValorObj(rowCuenta("COD_DELEGACION"), GetType(String)), Nothing),
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_DELEGACION"), Util.AtribuirValorObj(rowCuenta("DES_DELEGACION"), GetType(String)), Nothing),
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_DELEGACION"), Util.AtribuirValorObj(rowCuenta("OID_DELEGACION"), GetType(String)), Nothing),
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("D_BOL_VIGENTE"), Util.AtribuirValorObj(rowCuenta("D_BOL_VIGENTE"), GetType(Boolean)), Nothing),
                                                                                            .HusoHorarioEnMinutos = If(rowCuenta.Table.Columns.Contains("NEC_GMT_MINUTOS"), Util.AtribuirValorObj(rowCuenta("NEC_GMT_MINUTOS"), GetType(Integer)), Nothing),
                                                                                            .FechaHoraVeranoInicio = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_INICIO"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_INICIO"), GetType(Date)), Nothing),
                                                                                            .FechaHoraVeranoFin = If(rowCuenta.Table.Columns.Contains("FYH_VERANO_FIN"), Util.AtribuirValorObj(rowCuenta("FYH_VERANO_FIN"), GetType(Date)), Nothing),
                                                                                            .AjusteHorarioVerano = If(rowCuenta.Table.Columns.Contains("NEC_VERANO_AJUSTE"), Util.AtribuirValorObj(rowCuenta("NEC_VERANO_AJUSTE"), GetType(Integer)), Nothing),
                                                                                            .Zona = If(rowCuenta.Table.Columns.Contains("DES_ZONA"), Util.AtribuirValorObj(rowCuenta("DES_ZONA"), GetType(String)), Nothing),
                                                                                            .CodigoPais = If(rowCuenta.Table.Columns.Contains("COD_PAIS"), Util.AtribuirValorObj(rowCuenta("COD_PAIS"), GetType(String)), Nothing)},
                                                          .TipoSector = New Clases.TipoSector With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("COD_TIPO_SECTOR"), GetType(String)), Nothing),
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("DES_TIPO_SECTOR"), GetType(String)), Nothing),
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_TIPO_SECTOR"), Util.AtribuirValorObj(rowCuenta("OID_TIPO_SECTOR"), GetType(String)), Nothing),
                                                                                            .EstaActivo = If(rowCuenta.Table.Columns.Contains("TSE_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("TSE_BOL_ACTIVO"), GetType(Boolean)), Nothing)},
                                                          .Planta = New Clases.Planta With {.Codigo = If(rowCuenta.Table.Columns.Contains("COD_PLANTA"), Util.AtribuirValorObj(rowCuenta("COD_PLANTA"), GetType(String)), Nothing),
                                                                                            .Descripcion = If(rowCuenta.Table.Columns.Contains("DES_PLANTA"), Util.AtribuirValorObj(rowCuenta("DES_PLANTA"), GetType(String)), Nothing),
                                                                                            .Identificador = If(rowCuenta.Table.Columns.Contains("OID_PLANTA"), Util.AtribuirValorObj(rowCuenta("OID_PLANTA"), GetType(String)), Nothing),
                                                                                            .EsActivo = If(rowCuenta.Table.Columns.Contains("P_BOL_ACTIVO"), Util.AtribuirValorObj(rowCuenta("P_BOL_ACTIVO"), GetType(Boolean)), Nothing)}}

                            End If

                        End With

                        _cuentas.Add(_cuenta)

                    End If

                Next

            End If

            Return _cuentas
        End Function

        Private Shared Sub CargarValoresTerminosDocumento(ds As DataSet, IdentificadorDocumento As String, Terminos As ObservableCollection(Of Clases.TerminoIAC))
            If ds.Tables.Contains("doc_rc_valor_terminos_doc") AndAlso ds.Tables("doc_rc_valor_terminos_doc").Rows.Count > 0 Then
                If Terminos IsNot Nothing AndAlso Terminos.Count > 0 Then
                    For Each t As Clases.TerminoIAC In Terminos
                        Dim rTer() As DataRow = ds.Tables("doc_rc_valor_terminos_doc").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "' and OID_TERMINO='" & t.Identificador & "'")
                        If rTer IsNot Nothing AndAlso rTer.Count > 0 Then
                            t.Valor = Util.AtribuirValorObj(rTer(0)("DES_VALOR"), GetType(String))
                        End If
                    Next
                End If
            End If
        End Sub

        Private Shared Function CargarValoresDocumento(ds As DataSet, IdentificadorDocumento As String,
                                               Optional rellenarTipoValorNoDefinido As Boolean = False,
                                               Optional esDisponibleNoDefinido As Boolean = False,
                                               Optional _divisas As List(Of Clases.Divisa) = Nothing) As ObservableCollection(Of Clases.Divisa)

            Dim Divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            If ds.Tables.Contains("doc_rc_valores_documentos") AndAlso ds.Tables("doc_rc_valores_documentos").Rows.Count > 0 Then
                Dim rVal() As DataRow = ds.Tables("doc_rc_valores_documentos").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
                If rVal IsNot Nothing AndAlso rVal.Count > 0 Then
                    Divisas = New ObservableCollection(Of Clases.Divisa)
                    For Each valor As DataRow In rVal
                        Dim OID_DIVISA As String = Util.AtribuirValorObj(valor("OID_DIVISA"), GetType(String))
                        Dim OID_DENOMINACION As String = Util.AtribuirValorObj(valor("OID_DENOMINACION"), GetType(String))
                        Dim OID_MEDIO_PAGO As String = Util.AtribuirValorObj(valor("OID_MEDIO_PAGO"), GetType(String))
                        Dim COD_TIPO_MEDIO_PAGO As String = Util.AtribuirValorObj(valor("COD_TIPO_MEDIO_PAGO"), GetType(String))
                        Dim COD_NIVEL_DETALLE As String = Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String))
                        Dim IMPORTE As Decimal = Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal))
                        Dim BOL_DISPONIBLE As String = Util.AtribuirValorObj(valor("BOL_DISPONIBLE"), GetType(String))
                        Dim CANTIDAD As Int64 = Util.AtribuirValorObj(valor("CANTIDAD"), GetType(Int64))
                        Dim OID_CALIDAD As String = Util.AtribuirValorObj(valor("OID_CALIDAD"), GetType(String))
                        Dim OID_UNIDAD_MEDIDA As String = Util.AtribuirValorObj(valor("OID_UNIDAD_MEDIDA"), GetType(String))

                        Dim Disponible As Boolean = False
                        If BOL_DISPONIBLE IsNot Nothing Then
                            Disponible = CBool(BOL_DISPONIBLE)
                        End If

                        Dim TipoValor As Enumeradores.TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido,
                                                                                                  If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido,
                                                                                                                                            Enumeradores.TipoValor.NoDisponible),
                                                                                                                             If(Disponible, Enumeradores.TipoValor.Disponible,
                                                                                                                                            Enumeradores.TipoValor.NoDisponible)))

                        'divisa
                        Dim div As Clases.Divisa = Divisas.FirstOrDefault(Function(d) d.Identificador = OID_DIVISA)
                        If div Is Nothing Then
                            div = _divisas.First(Function(d) d.Identificador = OID_DIVISA).Clonar
                            Divisas.Add(div)
                        End If
                        If div IsNot Nothing Then
                            '??????
                            If String.IsNullOrEmpty(OID_DENOMINACION) AndAlso String.IsNullOrEmpty(OID_MEDIO_PAGO) Then
                                If String.IsNullOrEmpty(COD_TIPO_MEDIO_PAGO) Then
                                    If Not String.IsNullOrEmpty(COD_NIVEL_DETALLE) AndAlso COD_NIVEL_DETALLE = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor Then
                                        If div.ValoresTotalesEfectivo Is Nothing Then
                                            div.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                                        End If
                                        div.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {
                                                                                                        .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla,
                                                                                                        .TipoValor = TipoValor,
                                                                                                        .Importe = IMPORTE})

                                    ElseIf Not String.IsNullOrEmpty(COD_NIVEL_DETALLE) AndAlso COD_NIVEL_DETALLE = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor Then
                                        If div.ValoresTotalesDivisa Is Nothing Then
                                            div.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                        End If
                                        div.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With {
                                                                             .TipoValor = TipoValor,
                                                                             .Importe = IMPORTE})
                                    End If
                                Else
                                    If div.ValoresTotalesTipoMedioPago Is Nothing Then
                                        div.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                    End If
                                    div.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With {
                                                                              .TipoValor = TipoValor,
                                                                              .Importe = IMPORTE,
                                                                              .Cantidad = CANTIDAD,
                                                                              .TipoMedioPago = If(Not String.IsNullOrEmpty(COD_TIPO_MEDIO_PAGO),
                                                                                                  Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(COD_TIPO_MEDIO_PAGO),
                                                                                                  Enumeradores.TipoMedioPago.OtroValor)})
                                End If
                            End If

                            'efectivo
                            If Not String.IsNullOrEmpty(OID_DENOMINACION) AndAlso Not String.IsNullOrEmpty(IMPORTE) Then
                                Dim den As Clases.Denominacion = Nothing
                                If div.Denominaciones IsNot Nothing Then
                                    den = div.Denominaciones.FirstOrDefault(Function(d) d.Identificador = OID_DENOMINACION)
                                Else
                                    div.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                                End If
                                'If den Is Nothing Then
                                '    den = CargarDenominacion(ds, OID_DIVISA, OID_DENOMINACION)
                                '    div.Denominaciones.Add(den)
                                'End If
                                If den IsNot Nothing Then
                                    If den.ValorDenominacion Is Nothing Then
                                        den.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                                    End If
                                    Dim objValor As New Clases.ValorDenominacion
                                    objValor.Cantidad = CANTIDAD
                                    objValor.Importe = IMPORTE
                                    objValor.TipoValor = TipoValor

                                    If Not String.IsNullOrEmpty(OID_CALIDAD) AndAlso ds.Tables.Contains("doc_rc_calidades") AndAlso ds.Tables("doc_rc_calidades").Rows.Count > 0 Then
                                        Dim calidad() As DataRow = ds.Tables("doc_rc_calidades").Select("OID_CALIDAD = '" & OID_CALIDAD & "'")
                                        If calidad IsNot Nothing AndAlso calidad.Count > 0 Then
                                            objValor.Calidad = New Clases.Calidad With {
                                                            .Identificador = Util.AtribuirValorObj(calidad(0)("OID_CALIDAD"), GetType(String)),
                                                            .Codigo = Util.AtribuirValorObj(calidad(0)("COD_CALIDAD"), GetType(String)),
                                                            .Descripcion = Util.AtribuirValorObj(calidad(0)("DES_CALIDAD"), GetType(String))}
                                        End If
                                    End If

                                    If Not String.IsNullOrEmpty(OID_UNIDAD_MEDIDA) AndAlso ds.Tables.Contains("doc_rc_unidades_medida") AndAlso ds.Tables("doc_rc_unidades_medida").Rows.Count > 0 Then
                                        Dim unidadMedida() = ds.Tables("doc_rc_unidades_medida").Select("OID_UNIDAD_MEDIDA = '" & OID_UNIDAD_MEDIDA & "'")
                                        If unidadMedida IsNot Nothing AndAlso unidadMedida.Count > 0 Then
                                            objValor.UnidadMedida = New Clases.UnidadMedida With {
                                                            .Identificador = Util.AtribuirValorObj(unidadMedida(0)("OID_UNIDAD_MEDIDA"), GetType(String)),
                                                            .Codigo = Util.AtribuirValorObj(unidadMedida(0)("COD_UNIDAD_MEDIDA"), GetType(String)),
                                                            .Descripcion = Util.AtribuirValorObj(unidadMedida(0)("DES_UNIDAD_MEDIDA"), GetType(String)),
                                                            .EsPadron = Util.AtribuirValorObj(unidadMedida(0)("BOL_DEFECTO"), GetType(Boolean)),
                                                            .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(unidadMedida(0)("COD_TIPO_UNIDAD_MEDIDA"), GetType(String))),
                                                            .ValorUnidad = Util.AtribuirValorObj(unidadMedida(0)("NUM_VALOR_UNIDAD"), GetType(Decimal))}
                                        End If
                                    End If

                                    den.ValorDenominacion.Add(objValor)

                                End If

                            End If


                            'medio de pago
                            If Not String.IsNullOrEmpty(OID_MEDIO_PAGO) AndAlso Not String.IsNullOrEmpty(IMPORTE) Then
                                Dim mp As Clases.MedioPago = Nothing
                                If div.MediosPago IsNot Nothing Then
                                    mp = div.MediosPago.FirstOrDefault(Function(d) d.Identificador = OID_MEDIO_PAGO)
                                Else
                                    div.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                                End If
                                If mp Is Nothing Then
                                    mp = CargarMedioPago(ds, OID_DIVISA, OID_MEDIO_PAGO)
                                    div.MediosPago.Add(mp)
                                End If

                                If mp IsNot Nothing Then

                                    Dim valoresTerminos As New ObservableCollection(Of Clases.Termino)
                                    If mp.Terminos IsNot Nothing AndAlso ds.Tables.Contains("doc_rc_terminos_mediospago") AndAlso ds.Tables("doc_rc_terminos_mediospago").Rows.Count > 0 Then
                                        Dim valorTermino() As DataRow = ds.Tables("doc_rc_terminos_mediospago").Select("OID_DOCUMENTO = '" & IdentificadorDocumento & "' ")
                                        If valorTermino IsNot Nothing AndAlso valorTermino.Count > 0 Then
                                            For Each v As DataRow In valorTermino
                                                Dim termino As Clases.Termino = mp.Terminos.Select(Function(t) t.Identificador = Util.AtribuirValorObj(v("OID_MEDIO_PAGO"), GetType(String)))
                                                If termino IsNot Nothing Then
                                                    termino.Valor = Util.AtribuirValorObj(v("DES_VALOR"), GetType(String))
                                                    termino.NecIndiceGrupo = Util.AtribuirValorObj(v("NEC_INDICE_GRUPO"), GetType(String))
                                                    valoresTerminos.Add(termino)
                                                End If
                                            Next
                                        End If
                                    End If

                                    If mp.Valores Is Nothing Then
                                        mp.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                    End If
                                    mp.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = CANTIDAD,
                                                                                             .Importe = IMPORTE,
                                                                                             .Terminos = valoresTerminos,
                                                                                             .TipoValor = TipoValor})
                                End If

                            End If

                            If Not String.IsNullOrEmpty(IMPORTE) Then
                                If div.ValoresTotales Is Nothing Then div.ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)
                                If div.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor) IsNot Nothing Then
                                    div.ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor).Importe += IMPORTE
                                Else
                                    Dim Total As New Clases.ImporteTotal
                                    Total.Importe = IMPORTE
                                    Total.TipoValor = TipoValor
                                    div.ValoresTotales.Add(Total)
                                End If
                            End If

                        End If
                    Next

                End If
            End If

            Return Divisas
        End Function

        Public Shared Function CargarDivisa(ds As DataSet) As List(Of Clases.Divisa)

            Dim _divisas As New List(Of Clases.Divisa)

            If ds.Tables.Contains("doc_rc_divisas") AndAlso ds.Tables("doc_rc_divisas").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_divisas").Rows

                    If _divisas.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String))) Is Nothing Then
                        Dim _divisa As New Clases.Divisa

                        With _divisa
                            .ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)

                            .Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String))
                            .CodigoISO = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
                            .CodigoSimbolo = Util.AtribuirValorObj(row("COD_SIMBOLO"), GetType(String))
                            .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                            .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                            .FechaHoraTransporte = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                            .CodigoAcceso = Util.AtribuirValorObj(row("COD_ACCESO"), GetType(String))
                            .Color = Util.AtribuirValorObj(row("COD_COLOR"), GetType(Drawing.Color))
                            .Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                        End With

                        _divisas.Add(_divisa)
                    End If

                Next

            End If

            Return _divisas
        End Function

        Public Shared Sub CargarDenominacion(ds As DataSet, ByRef _divisas As List(Of Clases.Divisa))

            Dim _denominaciones As New List(Of Clases.Denominacion)

            If ds.Tables.Contains("doc_rc_denominaciones") AndAlso ds.Tables("doc_rc_denominaciones").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_denominaciones").Rows

                    If _denominaciones.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String))) Is Nothing Then

                        Dim _denominacion As New Clases.Denominacion
                        With _denominacion
                            .Identificador = Util.AtribuirValorObj(row("OID_DENOMINACION"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))
                            .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
                            .EsBillete = Util.AtribuirValorObj(row("BOL_BILLETE"), GetType(Boolean))
                            .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                            .Valor = Util.AtribuirValorObj(row("NUM_VALOR"), GetType(Decimal))
                        End With

                        _denominaciones.Add(_denominacion)

                        Dim _divisa As Clases.Divisa = _divisas.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DIVISA"), GetType(String)))
                        _divisa.Denominaciones.Add(_denominacion)

                    End If

                Next

            End If

        End Sub

        Private Shared Function CargarMedioPago(ds As DataSet, IdentificadorDivisa As String, IdentificadorMedioPago As String) As Clases.MedioPago
            Dim mp As Clases.MedioPago = Nothing
            If ds.Tables.Contains("doc_rc_mediospago") AndAlso ds.Tables("doc_rc_mediospago").Rows.Count > 0 Then
                Dim rMp() As DataRow = ds.Tables("doc_rc_mediospago").Select("OID_DIVISA='" & IdentificadorDivisa & "' and OID_MEDIO_PAGO = '" & IdentificadorMedioPago & "'")
                If rMp IsNot Nothing AndAlso rMp.Count > 0 Then
                    Dim rowMedioPago As DataRow = rMp(0)
                    mp = New Clases.MedioPago
                    With mp
                        .Identificador = Util.AtribuirValorObj(rowMedioPago("OID_MEDIO_PAGO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(rowMedioPago("COD_MEDIO_PAGO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(rowMedioPago("DES_MEDIO_PAGO"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(rowMedioPago("OBS_MEDIO_PAGO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(rowMedioPago("BOL_VIGENTE"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(rowMedioPago("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(rowMedioPago("FYH_ACTUALIZACION"), GetType(DateTime))
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowMedioPago("COD_TIPO_MEDIO_PAGO"), GetType(String))) Then
                            .Tipo = EnumExtension.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(rowMedioPago("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                        End If
                        .Terminos = New ObservableCollection(Of Clases.Termino)

                        Dim termino As Clases.Termino = CargarTerminoMedioPago(rowMedioPago)
                        If termino IsNot Nothing Then
                            .Terminos.Add(termino)
                        End If
                    End With
                End If
            End If

            Return mp
        End Function

        Private Shared Function CargarTerminoMedioPago(rowMedioPago As DataRow) As Clases.Termino
            Dim trm As Clases.Termino = Nothing
            If rowMedioPago IsNot Nothing AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowMedioPago("T_OID_TERMINO"), GetType(String))) Then
                trm = New Clases.Termino
                With trm
                    .Identificador = Util.AtribuirValorObj(rowMedioPago("T_OID_TERMINO"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(rowMedioPago("T_COD_TERMINO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(rowMedioPago("T_DES_TERMINO"), GetType(String))
                    .Observacion = Util.AtribuirValorObj(rowMedioPago("T_OBS_TERMINO"), GetType(String))
                    .ValorInicial = Util.AtribuirValorObj(rowMedioPago("T_DES_VALOR_INICIAL"), GetType(String))
                    .Longitud = Util.AtribuirValorObj(rowMedioPago("T_NEC_LONGITUD"), GetType(Integer))
                    .MostrarDescripcionConCodigo = Util.AtribuirValorObj(rowMedioPago("T_BOL_MOSTRAR_CODIGO"), GetType(Int16))
                    .Orden = Util.AtribuirValorObj(rowMedioPago("T_NEC_ORDEN"), GetType(Integer))
                    .EstaActivo = Util.AtribuirValorObj(rowMedioPago("T_BOL_VIGENTE"), GetType(Int16))
                    .CodigoUsuario = Util.AtribuirValorObj(rowMedioPago("T_COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(rowMedioPago("T_FYH_ACTUALIZACION"), GetType(DateTime))

                    .Formato = New Clases.Formato
                    .Formato.Identificador = Util.AtribuirValorObj(rowMedioPago("OID_FORMATO"), GetType(String))
                    .Formato.Codigo = Util.AtribuirValorObj(rowMedioPago("COD_FORMATO"), GetType(String))
                    .Formato.Descripcion = Util.AtribuirValorObj(rowMedioPago("DES_FORMATO"), GetType(String))

                    .Mascara = New Clases.Mascara
                    .Mascara.Identificador = Util.AtribuirValorObj(rowMedioPago("OID_MASCARA"), GetType(String))
                    .Mascara.Codigo = Util.AtribuirValorObj(rowMedioPago("COD_MASCARA"), GetType(String))
                    .Mascara.Descripcion = Util.AtribuirValorObj(rowMedioPago("DES_MASCARA"), GetType(String))
                    .Mascara.ExpresionRegular = Util.AtribuirValorObj(rowMedioPago("DES_EXP_REGULAR"), GetType(String))

                    .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                    .AlgoritmoValidacion.Identificador = Util.AtribuirValorObj(rowMedioPago("OID_ALGORITMO_VALIDACION"), GetType(String))
                    .AlgoritmoValidacion.Codigo = Util.AtribuirValorObj(rowMedioPago("COD_ALGORITMO_VALIDACION"), GetType(String))
                    .AlgoritmoValidacion.Descripcion = Util.AtribuirValorObj(rowMedioPago("DES_ALGORITMO_VALIDACION"), GetType(String))
                    .AlgoritmoValidacion.Observacion = Util.AtribuirValorObj(rowMedioPago("OBS_ALGORITMO_VALIDACION"), GetType(String))

                    .ValoresPosibles = New ObservableCollection(Of Clases.TerminoValorPosible)
                    Dim valor As New Clases.TerminoValorPosible
                    valor.Identificador = Util.AtribuirValorObj(rowMedioPago("OID_VALOR"), GetType(String))
                    valor.Codigo = Util.AtribuirValorObj(rowMedioPago("COD_VALOR"), GetType(String))
                    valor.Descripcion = Util.AtribuirValorObj(rowMedioPago("DES_VALOR"), GetType(String))
                    valor.EstaActivo = Util.AtribuirValorObj(rowMedioPago("VT_BOL_VIGENTE"), GetType(String))
                    .ValoresPosibles.Add(valor)

                End With
            End If

            Return trm
        End Function

        Public Shared Function CargarFormulario(ds As DataSet,
                                                Optional _acciones As List(Of Clases.AccionContable) = Nothing,
                                                Optional _gruposIAC As List(Of Clases.GrupoTerminosIAC) = Nothing) As List(Of Clases.Formulario)


            Dim _formularios As New List(Of Clases.Formulario)

            If ds.Tables.Contains("doc_rc_formulario") AndAlso ds.Tables("doc_rc_formulario").Rows.Count > 0 Then

                For Each objRow As DataRow In ds.Tables("doc_rc_formulario").Rows

                    If _formularios.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(objRow("OID_FORMULARIO"), GetType(String))) Is Nothing Then

                        Dim _formulario As New Clases.Formulario

                        With _formulario

                            .Identificador = Util.AtribuirValorObj(objRow("OID_FORMULARIO"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(objRow("COD_FORMULARIO"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(objRow("DES_FORMULARIO"), GetType(String))
                            .DescripcionCodigoExterno = Util.AtribuirValorObj(objRow("DES_COD_EXTERNO"), GetType(String))
                            .TieneImpresion = Util.AtribuirValorObj(objRow("BOL_IMPRIMIR"), GetType(String))

                            If objRow("cod_color") IsNot Nothing Then
                                .Color = System.Drawing.Color.FromName(objRow("cod_color").ToString)
                            End If

                            .Icono = Util.AtribuirValorObj(objRow("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                            .EstaActivo = Util.AtribuirValorObj(objRow("BOL_ACTIVO"), GetType(String))
                            .FechaHoraCreacion = Util.AtribuirValorObj(objRow("GMT_CREACION"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(objRow("GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(objRow("DES_USUARIO_MODIFICACION"), GetType(String))

                            .AccionContable = _acciones.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(objRow("OID_ACCION_CONTABLE"), GetType(String)))

                            .Caracteristicas = CargarCaracteristicasFormulario(ds, .Identificador)

                            'GrupoTerminosIAC individual
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_INDIVIDUAL"), GetType(String))) Then
                                .GrupoTerminosIACIndividual = _gruposIAC.FirstOrDefault(Function(t) t.Identificador = objRow("OID_IAC_INDIVIDUAL"))
                            End If

                            'GrupoTerminosIAC Grupo
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_GRUPO"), GetType(String))) Then
                                .GrupoTerminosIACGrupo = _gruposIAC.FirstOrDefault(Function(t) t.Identificador = objRow("OID_IAC_GRUPO"))
                            End If

                            ' TipoDocumento
                            .TipoDocumento = New Clases.TipoDocumento
                            With .TipoDocumento
                                .Identificador = Util.AtribuirValorObj(objRow("OID_TIPO_DOCUMENTO"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(objRow("COD_TIPO_DOCUMENTO"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(objRow("DES_TIPO_DOCUMENTO"), GetType(String))
                                .EstaActivo = Util.AtribuirValorObj(objRow("TD_BOL_ACTIVO"), GetType(Boolean))
                                .FechaHoraCreacion = Util.AtribuirValorObj(objRow("TD_GMT_CREACION"), GetType(DateTime))
                                .FechaHoraModificacion = Util.AtribuirValorObj(objRow("TD_GMT_MODIFICACION"), GetType(DateTime))
                                .UsuarioCreacion = Util.AtribuirValorObj(objRow("TD_DES_USUARIO_CREACION"), GetType(String))
                                .UsuarioModificacion = Util.AtribuirValorObj(objRow("TD_DES_USUARIO_MODIFICACION"), GetType(String))
                            End With

                            'OID_FILTRO_FORMULARIO
                            'Verifica se não é nulo
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_FILTRO_FORMULARIO"), GetType(String))) Then
                                .FiltroFormulario = New Clases.Transferencias.FiltroFormulario
                                With .FiltroFormulario
                                    .Identificador = Util.AtribuirValorObj(objRow("OID_FILTRO_FORMULARIO"), GetType(String))
                                    .Descripcion = Util.AtribuirValorObj(objRow("DES_FILTRO_FORMULARIO"), GetType(String))
                                    .SoloMovimientoDisponible = Util.AtribuirValorObj(objRow("BOL_SOLO_DISPONIBLE"), GetType(Boolean))
                                    .UtilizaDocumentosConValor = Util.AtribuirValorObj(objRow("BOL_CON_VALOR"), GetType(Boolean))
                                    .UtilizaDocumentosConBulto = Util.AtribuirValorObj(objRow("FF_BOL_CON_BULTO"), GetType(Boolean))
                                    .SoloMovimientoDeReenvio = Util.AtribuirValorObj(objRow("BOL_SOLO_REENVIO"), GetType(Boolean))
                                    .SoloMovimientoDeSustitucion = Util.AtribuirValorObj(objRow("BOL_SOLO_SUSTITUCION"), GetType(Boolean))
                                    .MovimientoConFechaEspecifica = Util.AtribuirValorObj(objRow("BOL_CON_FECHA_ESPECIFICA"), GetType(Boolean))
                                    .CantidadDiasBusquedaInicio = Util.AtribuirValorObj(objRow("NEC_DIAS_BUSQUEDA_INICIO"), GetType(Boolean))
                                    .CantidadDiasBusquedaFin = Util.AtribuirValorObj(objRow("NEC_DIAS_BUSQUEDA_FIN"), GetType(Boolean))
                                    .EstaActivo = Util.AtribuirValorObj(objRow("FF_BOL_ACTIVO"), GetType(Boolean))
                                    .FechaHoraCreacion = Util.AtribuirValorObj(objRow("FF_GMT_CREACION"), GetType(DateTime))
                                    .UsuarioCreacion = Util.AtribuirValorObj(objRow("FF_DES_USUARIO_CREACION"), GetType(String))
                                    .FechaHoraModificacion = Util.AtribuirValorObj(objRow("FF_GMT_MODIFICACION"), GetType(DateTime))
                                    .UsuarioModificacion = Util.AtribuirValorObj(objRow("FF_DES_USUARIO_MODIFICACION"), GetType(String))
                                End With
                            End If
                        End With

                        _formularios.Add(_formulario)

                    End If

                Next
            End If

            Return _formularios
        End Function

        Public Shared Function CargarAccionContable(ds As DataSet) As List(Of Clases.AccionContable)

            Dim _acciones As New List(Of Clases.AccionContable)

            If ds.Tables.Contains("doc_rc_accion_contable") AndAlso ds.Tables("doc_rc_accion_contable").Rows.Count > 0 Then

                For Each objRow As DataRow In ds.Tables("doc_rc_accion_contable").Rows

                    If _acciones.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(objRow("OID_ACCION_CONTABLE"), GetType(String))) Is Nothing Then
                        Dim _accion As New Clases.AccionContable
                        With _accion
                            .Identificador = Util.AtribuirValorObj(objRow("OID_ACCION_CONTABLE"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(objRow("COD_ACCION_CONTABLE"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(objRow("DES_ACCION_CONTABLE"), GetType(String))
                            .EstaActivo = Util.AtribuirValorObj(objRow("BOL_ACTIVO"), GetType(String))
                            .FechaHoraCreacion = Util.AtribuirValorObj(objRow("GMT_CREACION"), GetType(String))
                            .FechaHoraModificacion = Util.AtribuirValorObj(objRow("GMT_MODIFICACION"), GetType(String))
                            .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(objRow("DES_USUARIO_MODIFICACION"), GetType(String))
                            .Acciones = New ObservableCollection(Of Clases.AccionTransaccion)

                            'estados accion contable
                            If ds.Tables.Contains("doc_rc_estado_acc_contable") AndAlso ds.Tables("doc_rc_estado_acc_contable").Rows.Count > 0 Then
                                Dim rEAcC() As DataRow = ds.Tables("doc_rc_estado_acc_contable").Select("OID_ACCION_CONTABLE = '" & .Identificador & "'")
                                If rEAcC IsNot Nothing AndAlso rEAcC.Count > 0 Then
                                    For Each row As DataRow In rEAcC
                                        Dim objEstadoAccionContable As New Clases.EstadoAccionContable
                                        With objEstadoAccionContable
                                            .Identificador = Util.AtribuirValorObj(row("OID_ESTADOXACCION_CONTABLE"), GetType(String))
                                            .IdentificadorAccionContable = Util.AtribuirValorObj(row("OID_ACCION_CONTABLE"), GetType(String))
                                            .Codigo = Util.AtribuirValorObj(row("COD_ESTADO"), GetType(String))
                                            .OrigemDisponible = Util.AtribuirValorObj(row("COD_ACCION_ORIGEN_DISPONIBLE"), GetType(String))
                                            .OrigemNoDisponible = Util.AtribuirValorObj(row("COD_ACCION_ORIGEN_NODISP"), GetType(String))
                                            .DestinoDisponible = Util.AtribuirValorObj(row("COD_ACCION_DESTINO_DISPONIBLE"), GetType(String))
                                            .DestinoNoDisponible = Util.AtribuirValorObj(row("COD_ACCION_DESTINO_NODISP"), GetType(String))
                                            .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                                            .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                                            .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                                            .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                                        End With

                                        ObtenerAccionesTransacciones(objEstadoAccionContable, .Acciones)
                                    Next
                                End If
                            End If
                        End With

                        _acciones.Add(_accion)
                    End If

                Next
            End If

            Return _acciones

        End Function

        Private Shared Sub ObtenerAccionesTransacciones(estadoAccion As Clases.EstadoAccionContable, Acciones As ObservableCollection(Of Clases.AccionTransaccion))
            If estadoAccion IsNot Nothing AndAlso Acciones IsNot Nothing Then
                Dim objAccionesContables As New ObservableCollection(Of Clases.AccionTransaccion)

                'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                If estadoAccion.OrigemDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                    'Quando for OrgemDisponible
                    Dim objAccionTransaccion As Clases.AccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.OrigemDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.Disponible)
                    Acciones.Add(objAccionTransaccion)
                End If

                'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                If estadoAccion.OrigemNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                    'Quando for OrgemNoDisponible
                    Dim objAccionTransaccion As Clases.AccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.OrigemNoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Origen, Enumeradores.TipoSaldo.NoDisponible)
                    Acciones.Add(objAccionTransaccion)
                End If

                'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                If estadoAccion.DestinoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                    'Quando for DestinoDisponible
                    Dim objAccionTransaccion As Clases.AccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.DestinoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.Disponible)
                    Acciones.Add(objAccionTransaccion)
                End If

                'Quando o valor for zero, será ignorado, não será recuperada a accionTransacion
                If estadoAccion.DestinoNoDisponible <> Prosegur.Genesis.Comon.Constantes.TipoMovimientoSinMovimiento Then
                    'Quando for DestinoNoDisponible
                    Dim objAccionTransaccion As Clases.AccionTransaccion = Prosegur.Genesis.Comon.Util.AccionTransaccion(estadoAccion.DestinoNoDisponible, estadoAccion.Codigo, Enumeradores.TipoSitio.Destino, Enumeradores.TipoSaldo.NoDisponible)
                    Acciones.Add(objAccionTransaccion)
                End If

            End If

        End Sub

        Private Shared Function CargarCaracteristicasFormulario(ds As DataSet, IdentificadorFormulario As String) As List(Of Enumeradores.CaracteristicaFormulario)
            Dim listaCaracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)
            If ds.Tables.Contains("doc_rc_caract_formulario") AndAlso ds.Tables("doc_rc_caract_formulario").Rows.Count > 0 Then
                Dim rCar() As DataRow = ds.Tables("doc_rc_caract_formulario").Select("OID_FORMULARIO='" & IdentificadorFormulario & "'")
                If rCar IsNot Nothing AndAlso rCar.Count > 0 Then
                    For Each row As DataRow In rCar
                        If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(row("COD_CARACT_FORMULARIO")) Then
                            listaCaracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(row("COD_CARACT_FORMULARIO")))
                        End If
                    Next
                End If
            End If
            Return listaCaracteristicas
        End Function

        Public Shared Function CargarGrupoTerminosIAC(ds As DataSet) As List(Of Clases.GrupoTerminosIAC)

            Dim _gruposIAC As List(Of Clases.GrupoTerminosIAC) = New List(Of Clases.GrupoTerminosIAC)

            If ds.Tables.Contains("doc_rc_grp_terminos_indiv") AndAlso ds.Tables("doc_rc_grp_terminos_indiv").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_grp_terminos_indiv").Rows

                    If _gruposIAC.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(row("OID_IAC"), GetType(String))) Is Nothing Then

                        Dim _IAC As New Clases.GrupoTerminosIAC

                        With _IAC
                            .Identificador = Util.AtribuirValorObj(row("OID_IAC"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(row("COD_IAC"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(row("DES_IAC"), GetType(String))
                            .Observacion = Util.AtribuirValorObj(row("OBS_IAC"), GetType(String))
                            .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                            .EsInvisible = Util.AtribuirValorObj(row("BOL_INVISIBLE"), GetType(Boolean))
                            .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                            .CopiarDeclarados = Util.AtribuirValorObj(row("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                            .TerminosIAC = CargarTerminosIAC(ds, .Identificador)
                        End With

                        _gruposIAC.Add(_IAC)

                    End If

                Next
            End If

            Return _gruposIAC
        End Function

        Private Shared Function CargarTerminosIAC(ds As DataSet, OID_IAC As String) As ObservableCollection(Of Clases.TerminoIAC)
            Dim listaTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            If ds.Tables.Contains("doc_rc_terminos_indiv") AndAlso ds.Tables("doc_rc_terminos_indiv").Rows.Count > 0 Then
                Dim rTrm() As DataRow = ds.Tables("doc_rc_terminos_indiv").Select("OID_IAC='" & OID_IAC & "'")
                If rTrm IsNot Nothing AndAlso rTrm.Count > 0 Then
                    For Each row As DataRow In rTrm
                        Dim objTerminoIAC As New Clases.TerminoIAC
                        With objTerminoIAC
                            .Identificador = Util.AtribuirValorObj(row("OID_TERMINO"), GetType(String))
                            .BuscarParcial = Util.AtribuirValorObj(row("BOL_BUSQUEDA_PARCIAL"), GetType(Boolean))
                            .EsCampoClave = Util.AtribuirValorObj(row("BOL_CAMPO_CLAVE"), GetType(Boolean))
                            .Orden = Util.AtribuirValorObj(row("NEC_ORDEN"), GetType(Integer))
                            .EsObligatorio = Util.AtribuirValorObj(row("BOL_ES_OBLIGATORIO"), GetType(Boolean))
                            .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                            .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                            .EsTerminoCopia = Util.AtribuirValorObj(row("BOL_TERMINO_COPIA"), GetType(Boolean))
                            .EsProtegido = Util.AtribuirValorObj(row("BOL_ES_PROTEGIDO"), GetType(Boolean))
                            .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                            .Observacion = Util.AtribuirValorObj(row("OBS_TERMINO"), GetType(String))
                            .Longitud = Util.AtribuirValorObj(row("NEC_LONGITUD"), GetType(Integer))
                            .MostrarDescripcionConCodigo = Util.AtribuirValorObj(row("BOL_MOSTRAR_CODIGO"), GetType(Boolean))
                            .TieneValoresPosibles = Util.AtribuirValorObj(row("BOL_VALORES_POSIBLES"), GetType(Boolean))
                            .AceptarDigitacion = Util.AtribuirValorObj(row("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))
                            .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                            .EsEspecificoDeSaldos = Util.AtribuirValorObj(row("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                            .Descripcion = Util.AtribuirValorObj(row("DES_TERMINO"), GetType(String))

                            .Formato = New Clases.Formato
                            With .Formato
                                .Identificador = Util.AtribuirValorObj(row("OID_FORMATO"), GetType(String))
                                .Codigo = Util.AtribuirValorObj(row("COD_FORMATO"), GetType(String))
                                .CodigoUsuario = Util.AtribuirValorObj(row("F_COD_USUARIO"), GetType(String))
                                .Descripcion = Util.AtribuirValorObj(row("DES_FORMATO"), GetType(String))
                                .FechaHoraActualizacion = Util.AtribuirValorObj(row("F_FYH_ACTUALIZACION"), GetType(DateTime))
                            End With

                            'Verifica se possui algoritimo de validação.
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))) Then
                                .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                With .AlgoritmoValidacion
                                    .Identificador = Util.AtribuirValorObj(row("OID_ALGORITMO_VALIDACION"), GetType(String))
                                    .Codigo = Util.AtribuirValorObj(row("COD_ALGORITMO_VALIDACION"), GetType(String))
                                    .Descripcion = Util.AtribuirValorObj(row("DES_ALGORITMO_VALIDACION"), GetType(String))
                                    .Observacion = Util.AtribuirValorObj(row("OBS_ALGORITMO_VALIDACION"), GetType(String))
                                    .CodigoUsuario = Util.AtribuirValorObj(row("AV_COD_USUARIO"), GetType(String))
                                    .FechaHoraAplicacion = Util.AtribuirValorObj(row("AV_FYH_ACTUALIZACION"), GetType(DateTime))
                                End With
                            End If

                            'Verifica se possui mascara.
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))) Then
                                .Mascara = New Clases.Mascara
                                With .Mascara
                                    .Identificador = Util.AtribuirValorObj(row("OID_MASCARA"), GetType(String))
                                    .Codigo = Util.AtribuirValorObj(row("COD_MASCARA"), GetType(String))
                                    .Descripcion = Util.AtribuirValorObj(row("DES_MASCARA"), GetType(String))
                                    .ExpresionRegular = Util.AtribuirValorObj(row("DES_EXP_REGULAR"), GetType(String))
                                    .CodigoUsuario = Util.AtribuirValorObj(row("M_COD_USUARIO"), GetType(String))
                                    .FechaHoraActualizacion = Util.AtribuirValorObj(row("M_FYH_ACTUALIZACION"), GetType(DateTime))
                                End With
                            End If
                        End With

                        listaTerminoIAC.Add(objTerminoIAC)
                    Next
                End If
            End If
            Return listaTerminoIAC
        End Function

        Public Shared Function CargarSector(ds As DataSet,
                                            Optional _identificadorDelegacion As Dictionary(Of String, String) = Nothing,
                                            Optional _delegaciones As List(Of Clases.Delegacion) = Nothing,
                                            Optional _tipoSectores As List(Of Clases.TipoSector) = Nothing,
                                            Optional _plantas As List(Of Clases.Planta) = Nothing) As List(Of Clases.Sector)

            If _identificadorDelegacion Is Nothing Then
                _identificadorDelegacion = New Dictionary(Of String, String)
            End If

            If _delegaciones Is Nothing Then
                _delegaciones = GenesisSaldos.Documento.CargarDelegacion(ds)
            End If

            If _tipoSectores Is Nothing Then
                _tipoSectores = GenesisSaldos.Documento.CargarTipoSector(ds)
            End If

            If _plantas Is Nothing Then
                _plantas = GenesisSaldos.Documento.CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
            End If

            Dim _sectores As New List(Of Clases.Sector)

            If ds.Tables.Contains("doc_rc_sectores") AndAlso ds.Tables("doc_rc_sectores").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_sectores").Rows

                    If _sectores.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(row("OID_SECTOR"), GetType(String))) Is Nothing Then

                        Dim _sector As New Clases.Sector
                        Dim IdentificadorDelegacion As String = ""
                        With _sector
                            .Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR"))
                            .Descripcion = Util.AtribuirValorObj(Of String)(row("DES_SECTOR"))
                            .Codigo = Util.AtribuirValorObj(Of String)(row("COD_SECTOR"))
                            .CodigoMigracion = Util.AtribuirValorObj(Of String)(row("COD_MIGRACION"))
                            .EsActivo = Util.AtribuirValorObj(Of Boolean)(row("BOL_ACTIVO"))
                            .EsCentroProceso = Util.AtribuirValorObj(Of Boolean)(row("BOL_CENTRO_PROCESO"))
                            .EsConteo = Util.AtribuirValorObj(Of Boolean)(row("BOL_CONTEO"))
                            .EsTesoro = Util.AtribuirValorObj(Of Boolean)(row("BOL_TESORO"))
                            .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_CREACION"))
                            .PemitirDisponerValor = Util.AtribuirValorObj(Of Boolean)(row("BOL_PERMITE_DISPONER_VALOR"))
                            .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_MODIFICACION"))
                            .UsuarioCreacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_CREACION"))
                            .UsuarioModificacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_MODIFICACION"))
                            .Planta = _plantas.FirstOrDefault(Function(p) p.Identificador = Util.AtribuirValorObj(Of String)(row("OID_PLANTA")))
                            .Delegacion = _delegaciones.FirstOrDefault(Function(d) d.Identificador = _identificadorDelegacion(Util.AtribuirValorObj(Of String)(row("OID_PLANTA"))))
                        End With

                        'If CargarCodigosAjenos Then
                        '    Sector.CodigosAjenos = CodigoAjeno.ObtenerCodigosAjenos(Sector.Identificador, String.Empty, String.Empty, Enumeradores.Tabela.Sector.RecuperarValor())
                        'End If

                        'If CargarSectorPadre AndAlso (rSec(0)("OID_SECTOR_PADRE") IsNot DBNull.Value) Then
                        '    Sector.SectorPadre = ObtenerPorOid(AtribuirValorObj(Of String)(rSec(0)("OID_SECTOR_PADRE")))
                        'End If

                        If (row("OID_TIPO_SECTOR") IsNot DBNull.Value) Then
                            _sector.TipoSector = _tipoSectores.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(Of String)(row("OID_TIPO_SECTOR")))
                        End If

                        _sectores.Add(_sector)
                    End If

                Next

            End If
            Return _sectores
        End Function

        Public Shared Function CargarPlanta(ds As DataSet, ByRef IdentificadorDelegacion As Dictionary(Of String, String), _tipoSectores As List(Of Clases.TipoSector)) As List(Of Clases.Planta)

            Dim _plantas As New List(Of Clases.Planta)

            If IdentificadorDelegacion Is Nothing Then
                IdentificadorDelegacion = New Dictionary(Of String, String)
            End If

            If ds.Tables.Contains("doc_rc_plantas") AndAlso ds.Tables("doc_rc_plantas").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_plantas").Rows
                    Dim _planta As Clases.Planta = Nothing
                    _planta = New Clases.Planta With
                                    {
                                        .Codigo = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String)),
                                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String)),
                                        .Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String)),
                                        .EsActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean)),
                                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)),
                                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)),
                                        .Identificador = Util.AtribuirValorObj(row("OID_PLANTA"), GetType(String)),
                                        .TiposSector = CargarTiposSectorPorPlanta(ds, .Identificador, _tipoSectores),
                                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)),
                                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                                    }

                    If Not IdentificadorDelegacion.ContainsKey(_planta.Identificador) Then
                        IdentificadorDelegacion.Add(_planta.Identificador, Util.AtribuirValorObj(row("OID_DELEGACION"), GetType(String)))
                    End If
                    _plantas.Add(_planta)
                Next
            End If

            Return _plantas
        End Function

        Public Shared Function CargarTiposSectorPorPlanta(ds As DataSet, IdentificadorPlanta As String, _tipoSectores As List(Of Clases.TipoSector)) As ObservableCollection(Of Clases.TipoSector)
            Dim TiposSectores As ObservableCollection(Of Clases.TipoSector) = Nothing

            If _tipoSectores IsNot Nothing AndAlso _tipoSectores.Count > 0 Then
                If ds.Tables.Contains("doc_rc_tipos_sector_planta") AndAlso ds.Tables("doc_rc_tipos_sector_planta").Rows.Count > 0 Then
                    Dim rPlns() As DataRow = ds.Tables("doc_rc_tipos_sector_planta").Select("OID_PLANTA='" & IdentificadorPlanta & "'")
                    If rPlns IsNot Nothing AndAlso rPlns.Count > 0 Then
                        TiposSectores = New ObservableCollection(Of Clases.TipoSector)
                        For Each row As DataRow In rPlns
                            TiposSectores.Add(_tipoSectores.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(row("OID_TIPO_SECTOR"), GetType(String))))
                        Next
                    End If
                End If

            End If
            Return TiposSectores
        End Function

        Public Shared Function CargarTipoSector(ds As DataSet) As List(Of Clases.TipoSector)

            Dim _tipoSectores As New List(Of Clases.TipoSector)

            If ds.Tables.Contains("doc_rc_tipos_sectores") AndAlso ds.Tables("doc_rc_tipos_sectores").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_tipos_sectores").Rows
                    Dim Tsect As New Clases.TipoSector
                    With Tsect
                        .Codigo = Util.AtribuirValorObj(row("COD_TIPO_SECTOR"), GetType(String))
                        .CodigoMigracion = Util.AtribuirValorObj(row("COD_MIGRACION"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_SECTOR"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .Identificador = Util.AtribuirValorObj(row("OID_TIPO_SECTOR"), GetType(String))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                        .CaracteristicasTipoSector = CargarCaracteristicasTipoSector(ds, .Identificador)
                    End With

                    _tipoSectores.Add(Tsect)
                Next

            End If

            Return _tipoSectores
        End Function

        Public Shared Function CargarCaracteristicasTipoSector(ds As DataSet, IdentificadorTipoSector As String) As ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
            Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
            If ds.Tables.Contains("doc_rc_caract_tp_sectores") AndAlso ds.Tables("doc_rc_caract_tp_sectores").Rows.Count > 0 Then
                Dim rCar() As DataRow = ds.Tables("doc_rc_caract_tp_sectores").Select("OID_TIPO_SECTOR='" & IdentificadorTipoSector & "'")
                If rCar IsNot Nothing AndAlso rCar.Count > 0 Then
                    For Each row As DataRow In rCar
                        If ExisteEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR")) Then
                            caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR").ToString))
                        End If
                    Next
                End If
            End If
            Return caracteristicas
        End Function

        Public Shared Function CargarDelegacion(ds As DataSet) As List(Of Clases.Delegacion)

            Dim _delegaciones As New List(Of Clases.Delegacion)

            If ds.Tables.Contains("doc_rc_delegaciones") AndAlso ds.Tables("doc_rc_delegaciones").Rows.Count > 0 Then

                For Each row As DataRow In ds.Tables("doc_rc_delegaciones").Rows

                    If _delegaciones.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(row("OID_DELEGACION"), GetType(String))) Is Nothing Then

                        Dim _delegacion As Clases.Delegacion = Nothing
                        _delegacion = New Clases.Delegacion With
                                        {
                                            .Identificador = Util.AtribuirValorObj(Of String)(row("OID_DELEGACION")),
                                            .Codigo = Util.AtribuirValorObj(Of String)(row("COD_DELEGACION")),
                                            .Descripcion = Util.AtribuirValorObj(Of String)(row("DES_DELEGACION")),
                                            .EsActivo = Util.AtribuirValorObj(Of Boolean)(row("BOL_VIGENTE")),
                                            .HusoHorarioEnMinutos = Util.AtribuirValorObj(Of Integer)(row("NEC_GMT_MINUTOS")),
                                            .FechaHoraVeranoInicio = Util.AtribuirValorObj(Of Date)(row("FYH_VERANO_INICIO")),
                                            .FechaHoraVeranoFin = Util.AtribuirValorObj(Of Date)(row("FYH_VERANO_FIN")),
                                            .AjusteHorarioVerano = Util.AtribuirValorObj(Of Integer)(row("NEC_VERANO_AJUSTE")),
                                            .Zona = Util.AtribuirValorObj(Of String)(row("DES_ZONA")),
                                            .FechaHoraCreacion = Util.AtribuirValorObj(Of Date)(row("GMT_CREACION")),
                                            .UsuarioCreacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_CREACION")),
                                            .FechaHoraModificacion = Util.AtribuirValorObj(Of Date)(row("GMT_MODIFICACION")),
                                            .UsuarioModificacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_MODIFICACION")),
                                            .CodigoPais = Util.AtribuirValorObj(Of String)(row("COD_PAIS"))
                                        }

                        _delegaciones.Add(_delegacion)

                    End If

                Next

            End If

            Return _delegaciones
        End Function

#End Region

#Region " Procedure - Confirmar/Aceptar/Anular/Rechazar"

        Shared Sub TransacionesDocumentos(ByRef documento As Clases.Documento,
                                          hacer_commit As Boolean,
                                          ByRef TransaccionActual As DataBaseHelper.Transaccion)
            Try

                Dim spw As SPWrapper = ColectarDocumentosTransaciones(documento, hacer_commit)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                documento.CodigoComprobante = spw.Param("par$cod_comprobante").Valor.ToString
                documento.Estado = RecuperarEnum(Of Enumeradores.EstadoDocumento)(spw.Param("par$cod_estado_documento").Valor.ToString)
                documento.Rowver = spw.Param("par$rowver").Valor.ToString

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

        Private Shared Function ColectarDocumentosTransaciones(documento As Clases.Documento,
                                                               hacer_commit As Boolean) As SPWrapper

            Dim SP As String = String.Format("sapr_ptransiciones_{0}.sejecutar_doc", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Objeto_Id, Nothing, , False) 'el dia de mañana se pasara el oid_llamada de log
            spw.AgregarParam("par$oid_documento", ProsegurDbType.Objeto_Id, documento.Identificador, , False)
            spw.AgregarParam("par$cod_estado_documento", ProsegurDbType.Identificador_Alfanumerico, documento.Estado.RecuperarValor, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, documento.Rowver, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, documento.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region

#Region " Procedure - Recuperar Certificado"

        Public Shared Function recuperarDocumentosCertificados(codigoCertificado As String,
                                                               identificadorAjeno As String,
                                                               detallarDesgloses As Boolean,
                                                               camposAdicionales As Boolean,
                                                               usuario As String,
                                                               delegacion As Clases.Delegacion,
                                                      Optional log As StringBuilder = Nothing) As List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Doc)

            Dim Tiempo As DateTime = Now
            If log Is Nothing Then
                log = New StringBuilder
            End If

            Dim _Documentos As List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Doc) = Nothing

            Try
                Dim ds As DataSet = Nothing

                Tiempo = Now
                Dim spw As SPWrapper = ColectarDocumentosCertificados(codigoCertificado, identificadorAjeno, detallarDesgloses, camposAdicionales, usuario)
                log.AppendLine("__Tiempo de Coletar: " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
                log.AppendLine("__Tiempo de EjecutarSP: " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                _Documentos = poblarDocumentosCertificados(ds, detallarDesgloses, camposAdicionales, delegacion)
                log.AppendLine("__Tiempo de Poblar: " & Now.Subtract(Tiempo).ToString() & "; ")

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return _Documentos
        End Function

        Public Shared Function ColectarDocumentosCertificados(codigoCertificado As String,
                                                              identificadorAjeno As String,
                                                              detallarDesgloses As Boolean,
                                                              camposAdicionales As Boolean,
                                                              usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumento_{0}.srecuperar_docs_certificados", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_certificado", ParamTypes.String, codigoCertificado, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_ajeno", ParamTypes.String, If(String.IsNullOrEmpty(identificadorAjeno), DBNull.Value, identificadorAjeno), , False)
            spw.AgregarParam("par$bol_detallar_desgloses", ParamTypes.String, If(detallarDesgloses, 1, 0), , False)
            spw.AgregarParam("par$bol_campos_adicionales", ParamTypes.String, If(camposAdicionales, 1, 0), , False)
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$doc_rc_valores_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valores_documentos")
            spw.AgregarParam("par$doc_rc_valor_terminos_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_valor_terminos_doc")
            spw.AgregarParam("par$usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")


            Return spw
        End Function

        Public Shared Function poblarDocumentosCertificados(ds As DataSet,
                                                            detallarDesgloses As Boolean,
                                                            camposAdicionales As Boolean,
                                                            delegacion As Clases.Delegacion) As List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Doc)

            Dim documentos As New List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Doc)
            If ds.Tables.Contains("doc_rc_documentos") AndAlso ds.Tables("doc_rc_documentos").Rows.Count > 0 Then

                For Each rowDocumento As DataRow In ds.Tables("doc_rc_documentos").Rows
                    Dim documento As New ContractoServicio.Contractos.Integracion.generarCertificado.Doc

                    With documento
                        .CodCom = Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String))
                        .CodExt = Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String))
                        .FecGes = Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime))
                        .FecRea = Util.AtribuirValorObj(rowDocumento("GMT_CREACION"), GetType(DateTime))

                        'converte para hora de la delegacion
                        .FecGes = .FecGes.QuieroExibirEstaFechaEnLaPatalla(delegacion)
                        .FecRea = .FecRea.QuieroExibirEstaFechaEnLaPatalla(delegacion)

                        .CodFor = Util.AtribuirValorObj(rowDocumento("COD_FORMULARIO"), GetType(String))
                        .NomFor = Util.AtribuirValorObj(rowDocumento("DES_FORMULARIO"), GetType(String))

                        poblarCuentaCertificados(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String)), .CueOri)
                        poblarCuentaCertificados(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String)), .CueDes)

                        If camposAdicionales Then
                            poblarTerminosCertificados(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), Util.AtribuirValorObj(rowDocumento("OID_IAC_INDIVIDUAL"), GetType(String)), .CamAdis)
                        End If

                        poblarDivisasCertificados(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), .Divs, detallarDesgloses)

                    End With

                    documentos.Add(documento)
                Next
            End If

            Return documentos
        End Function

        Public Shared Sub poblarCuentaCertificados(ds As DataSet, identificador As String, ByRef cuenta As ContractoServicio.Contractos.Integracion.generarCertificado.Cue)

            If Not String.IsNullOrEmpty(identificador) AndAlso ds.Tables.Contains("doc_rc_cuentas") AndAlso ds.Tables("doc_rc_cuentas").Rows.Count > 0 Then
                Dim rCtas() As DataRow = ds.Tables("doc_rc_cuentas").Select("OID_CUENTA='" & identificador & "'")
                If rCtas IsNot Nothing AndAlso rCtas.Count > 0 Then
                    cuenta = New ContractoServicio.Contractos.Integracion.generarCertificado.Cue

                    cuenta.CodCli = Util.AtribuirValorObj(rCtas(0)("COD_CLIENTE"), GetType(String))
                    cuenta.DesCli = Util.AtribuirValorObj(rCtas(0)("DES_CLIENTE"), GetType(String))

                    cuenta.CodSub = Util.AtribuirValorObj(rCtas(0)("COD_SUBCLIENTE"), GetType(String))
                    cuenta.DesSub = Util.AtribuirValorObj(rCtas(0)("DES_SUBCLIENTE"), GetType(String))

                    cuenta.CodPun = Util.AtribuirValorObj(rCtas(0)("COD_PTO_SERVICIO"), GetType(String))
                    cuenta.DesPun = Util.AtribuirValorObj(rCtas(0)("DES_PTO_SERVICIO"), GetType(String))

                    cuenta.CodCan = Util.AtribuirValorObj(rCtas(0)("COD_CANAL"), GetType(String))
                    cuenta.DesCan = Util.AtribuirValorObj(rCtas(0)("DES_CANAL"), GetType(String))

                    cuenta.CodSCa = Util.AtribuirValorObj(rCtas(0)("COD_SUBCANAL"), GetType(String))
                    cuenta.DesSCa = Util.AtribuirValorObj(rCtas(0)("DES_SUBCANAL"), GetType(String))

                    cuenta.CodSec = Util.AtribuirValorObj(rCtas(0)("COD_SECTOR"), GetType(String))
                    cuenta.DesSec = Util.AtribuirValorObj(rCtas(0)("DES_SECTOR"), GetType(String))

                    cuenta.CodDel = Util.AtribuirValorObj(rCtas(0)("COD_DELEGACION"), GetType(String))
                    cuenta.DesDel = Util.AtribuirValorObj(rCtas(0)("DES_DELEGACION"), GetType(String))

                    cuenta.CodPla = Util.AtribuirValorObj(rCtas(0)("COD_PLANTA"), GetType(String))
                    cuenta.DesPla = Util.AtribuirValorObj(rCtas(0)("DES_PLANTA"), GetType(String))

                End If
            End If

        End Sub

        Public Shared Sub poblarDivisasCertificados(ds As DataSet, IdentificadorDocumento As String, ByRef divs As List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Div), detallarDesgloses As Boolean)

            divs = New List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Div)
            Dim divisas As New ObservableCollection(Of Clases.Divisa)

            If ds.Tables.Contains("doc_rc_valores_documentos") AndAlso ds.Tables("doc_rc_valores_documentos").Rows.Count > 0 Then
                Dim rVal() As DataRow = ds.Tables("doc_rc_valores_documentos").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
                If rVal IsNot Nothing AndAlso rVal.Count > 0 Then
                    For Each valor As DataRow In rVal

                        Dim COD_DIVISA As String = Util.AtribuirValorObj(valor("COD_ISO_DIVISA"), GetType(String))
                        Dim COD_NIVEL_DETALLE As String = Util.AtribuirValorObj(valor("COD_NIVEL_DETALLE"), GetType(String))
                        Dim IMPORTE As Decimal = Util.AtribuirValorObj(valor("IMPORTE"), GetType(Decimal))

                        If detallarDesgloses Then

                            Dim COD_DENOMINACION As String = Util.AtribuirValorObj(valor("COD_DENOMINACION"), GetType(String))
                            Dim COD_MEDIO_PAGO As String = Util.AtribuirValorObj(valor("COD_MEDIO_PAGO"), GetType(String))
                            Dim COD_TIPO_MEDIO_PAGO As String = Util.AtribuirValorObj(valor("COD_TIPO_MEDIO_PAGO"), GetType(String))
                            Dim DES_MEDIO_PAGO As String = Util.AtribuirValorObj(valor("DES_MEDIO_PAGO"), GetType(String))
                            Dim CANTIDAD As Int64 = Util.AtribuirValorObj(valor("CANTIDAD"), GetType(Int64))
                            Dim OID_CALIDAD As String = Util.AtribuirValorObj(valor("OID_CALIDAD"), GetType(String))
                            Dim OID_UNIDAD_MEDIDA As String = Util.AtribuirValorObj(valor("OID_UNIDAD_MEDIDA"), GetType(String))

                            Dim div As ContractoServicio.Contractos.Integracion.generarCertificado.Div = divs.FirstOrDefault(Function(d) d.CodDiv = COD_DIVISA)
                            If div Is Nothing Then
                                div = New ContractoServicio.Contractos.Integracion.generarCertificado.Div
                                div.CodDiv = COD_DIVISA
                                divs.Add(div)
                            End If

                            If COD_NIVEL_DETALLE = "T" Then
                                div.Imp = IMPORTE.ToString
                            End If

                            ' Denominaciones
                            If Not String.IsNullOrEmpty(COD_DENOMINACION) AndAlso Not String.IsNullOrEmpty(IMPORTE) Then

                                If div.Dens Is Nothing Then
                                    div.Dens = New List(Of ContractoServicio.Contractos.Integracion.generarCertificado.Den)
                                End If

                                Dim den As ContractoServicio.Contractos.Integracion.generarCertificado.Den = Nothing
                                den = New ContractoServicio.Contractos.Integracion.generarCertificado.Den
                                den.CodDen = COD_DENOMINACION
                                den.Can = CANTIDAD
                                den.Imp = IMPORTE
                                div.Dens.Add(den)

                            End If

                            'Medio de pago
                            If Not String.IsNullOrEmpty(COD_MEDIO_PAGO) AndAlso Not String.IsNullOrEmpty(IMPORTE) Then

                                If div.MedPags Is Nothing Then
                                    div.MedPags = New List(Of ContractoServicio.Contractos.Integracion.generarCertificado.MedPag)
                                End If

                                Dim MedPag As ContractoServicio.Contractos.Integracion.generarCertificado.MedPag = Nothing
                                MedPag = New ContractoServicio.Contractos.Integracion.generarCertificado.MedPag
                                MedPag.CodMPa = COD_MEDIO_PAGO
                                MedPag.Can = CANTIDAD
                                MedPag.Imp = IMPORTE
                                MedPag.DesMPa = DES_MEDIO_PAGO
                                MedPag.CodTMP = COD_TIPO_MEDIO_PAGO
                                div.MedPags.Add(MedPag)

                            End If

                        Else

                            Dim div As ContractoServicio.Contractos.Integracion.generarCertificado.Div = divs.FirstOrDefault(Function(d) d.CodDiv = COD_DIVISA)
                            If div Is Nothing Then
                                div = New ContractoServicio.Contractos.Integracion.generarCertificado.Div
                                div.CodDiv = COD_DIVISA
                                divs.Add(div)
                            End If

                            ' Add importe nivel detalle = D ou, se só tiver T
                            If div.Tot = 0 AndAlso COD_NIVEL_DETALLE = "T" Then
                                div.Tot = IMPORTE.ToString
                            Else
                                div.Tot = IMPORTE.ToString
                            End If

                        End If

                    Next

                    If detallarDesgloses Then

                        'Soma importe denominações
                        If divs IsNot Nothing AndAlso divs.Count > 0 Then
                            For Each div In divs
                                If div.Dens IsNot Nothing AndAlso div.Dens.Count > 0 Then
                                    div.Tot = div.Dens.Sum(Function(a) a.Imp)
                                Else
                                    div.Tot = div.Imp
                                End If
                            Next
                        End If

                    End If

                End If
            End If

        End Sub

        Public Shared Sub poblarTerminosCertificados(ds As DataSet, IdentificadorDocumento As String, IdentificadorIAC As String, ByRef CamAdis As List(Of ContractoServicio.Contractos.Integracion.generarCertificado.CamAdi))
            If ds.Tables.Contains("doc_rc_valor_terminos_doc") AndAlso ds.Tables("doc_rc_valor_terminos_doc").Rows.Count > 0 Then
                For Each rTer As DataRow In ds.Tables("doc_rc_valor_terminos_doc").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
                    If CamAdis Is Nothing Then
                        CamAdis = New List(Of ContractoServicio.Contractos.Integracion.generarCertificado.CamAdi)
                    End If

                    Dim CamAdi As New ContractoServicio.Contractos.Integracion.generarCertificado.CamAdi
                    CamAdi.Nom = Util.AtribuirValorObj(rTer("DES_TERMINO"), GetType(String))
                    CamAdi.Val = Util.AtribuirValorObj(rTer("DES_VALOR"), GetType(String))
                    CamAdis.Add(CamAdi)
                Next
            End If
        End Sub

#End Region

#Region " Procedure - Recuperar Transacciones Pantalla"

        Public Shared Sub RecuperarTransacciones(ByVal peticion As RecuperarTransacciones.Peticion,
                                                 ByVal usuario As String,
                                                 ByRef transacciones As RecuperarTransacciones.Respuesta)
            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarPeticion(peticion, usuario)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    If ds.Tables.Contains("movimientos") Then
                        PoblarRespuesta(ds, transacciones)
                    End If
                End If
                spw = Nothing
                ds.Dispose()

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

        Private Shared Function ColectarPeticion(peticion As RecuperarTransacciones.Peticion, usuario As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_transaciones", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, False)

            If peticion IsNot Nothing Then


                spw.AgregarParam("par$oid_delegacion_gmt", ProsegurDbType.Identificador_Alfanumerico, peticion.Oid_delegacionGMT, , False)


                If String.IsNullOrWhiteSpace(peticion.Modalidad) Then
                    spw.AgregarParam("par$bol_planificada", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Modalidad = "1" OrElse peticion.Modalidad.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_planificada", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_planificada", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrWhiteSpace(peticion.Notificacion) Then
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Notificacion = "1" OrElse peticion.Notificacion.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrWhiteSpace(peticion.Acreditacion) Then
                    spw.AgregarParam("par$bol_acreditacion", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Acreditacion = "1" OrElse peticion.Acreditacion.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_acreditacion", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_acreditacion", ProsegurDbType.Logico, 0, , False)
                End If

                Dim format = ""
                If peticion.Fecha = Enumeradores.TipoFechas.Fecha_Creación Then
                    format = " -00:00"
                End If


                If peticion.FechaDesde Is Nothing OrElse peticion.FechaDesde = DateTime.MinValue Then
                    spw.AgregarParam("par$fyh_desde", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                Else

                    spw.AgregarParam("par$fyh_desde", ProsegurDbType.Identificador_Alfanumerico, peticion.FechaDesde.Value.ToString("yyyy-MM-dd HH:mm:ss") + format, , False)
                End If

                If peticion.FechaHasta Is Nothing OrElse peticion.FechaHasta = DateTime.MinValue Then
                    spw.AgregarParam("par$fyh_hasta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                Else
                    spw.AgregarParam("par$fyh_hasta", ProsegurDbType.Identificador_Alfanumerico, peticion.FechaHasta.Value.ToString("yyyy-MM-dd HH:mm:ss") + format, , False)
                End If

                If peticion.FechaGestion Is Nothing OrElse peticion.FechaGestion = DateTime.MinValue Then
                    spw.AgregarParam("par$fyh_referencia", ProsegurDbType.Data_Hora, Nothing, , False)
                Else
                    spw.AgregarParam("par$fyh_referencia", ProsegurDbType.Data_Hora, peticion.FechaGestion, , False)
                End If

                spw.AgregarParam("par$cod_fecha", ProsegurDbType.Identificador_Alfanumerico, peticion.Fecha.RecuperarValor, , False)
                spw.AgregarParam("par$bol_importeinformativo", ProsegurDbType.Logico, If(peticion.ImporteInformativo, 1, 0), , False)

                spw.AgregarParam("par$cod_transaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_delegaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_canales", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_clientes", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_subclientes", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_maquinas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_sectores", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_banco_capital", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_banco_facturacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_banco_tesoreria", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_cuenta_tesoreria", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_tipo_planificaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$oid_planificaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
                spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)


                If peticion.TipoTransacciones IsNot Nothing AndAlso peticion.TipoTransacciones.Count > 0 Then
                    spw.Param("par$cod_transaciones").AgregarValorArray("")
                    For Each obj In peticion.TipoTransacciones
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$cod_transaciones").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.Delegaciones IsNot Nothing AndAlso peticion.Delegaciones.Count > 0 Then
                    spw.Param("par$oid_delegaciones").AgregarValorArray("")
                    For Each obj In peticion.Delegaciones
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_delegaciones").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                    spw.Param("par$oid_canales").AgregarValorArray("")
                    For Each obj In peticion.Canales
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_canales").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.Clientes IsNot Nothing AndAlso peticion.Clientes.Count > 0 Then
                    spw.Param("par$oid_clientes").AgregarValorArray("")
                    For Each obj In peticion.Clientes
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_clientes").AgregarValorArray(obj)
                        End If
                    Next
                End If


                If peticion.Subclientes IsNot Nothing AndAlso peticion.Subclientes.Count > 0 Then
                    spw.Param("par$oid_subclientes").AgregarValorArray("")
                    For Each obj In peticion.Subclientes
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_subclientes").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.Maquinas IsNot Nothing AndAlso peticion.Maquinas.Count > 0 Then
                    spw.Param("par$oid_maquinas").AgregarValorArray("")
                    For Each obj In peticion.Maquinas
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_maquinas").AgregarValorArray(obj)
                        End If
                    Next
                End If


                If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then
                    spw.Param("par$oid_sectores").AgregarValorArray("")
                    For Each obj In peticion.Sectores
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_sectores").AgregarValorArray(obj)
                        End If
                    Next
                End If





                If peticion.PuntosServicios IsNot Nothing AndAlso peticion.PuntosServicios.Count > 0 Then
                    spw.Param("par$oid_punto_servicio").AgregarValorArray("")
                    For Each obj In peticion.PuntosServicios
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_punto_servicio").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.BancosCapital IsNot Nothing AndAlso peticion.BancosCapital.Count > 0 Then
                    spw.Param("par$oid_banco_capital").AgregarValorArray("")
                    For Each obj In peticion.BancosCapital
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_banco_capital").AgregarValorArray(obj)
                        End If
                    Next
                End If



                If peticion.BancosComision IsNot Nothing AndAlso peticion.BancosComision.Count > 0 Then
                    spw.Param("par$oid_banco_facturacion").AgregarValorArray("")
                    For Each obj In peticion.BancosComision
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_banco_facturacion").AgregarValorArray(obj)
                        End If
                    Next
                End If



                If peticion.BancosTesoreria IsNot Nothing AndAlso peticion.BancosTesoreria.Count > 0 Then
                    spw.Param("par$oid_banco_tesoreria").AgregarValorArray("")
                    For Each obj In peticion.BancosTesoreria
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_banco_tesoreria").AgregarValorArray(obj)
                        End If
                    Next
                End If



                If peticion.CuentaTesoreria IsNot Nothing AndAlso peticion.CuentaTesoreria.Count > 0 Then
                    spw.Param("par$oid_cuenta_tesoreria").AgregarValorArray("")
                    For Each obj In peticion.CuentaTesoreria
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_cuenta_tesoreria").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.TipoPlanificaciones IsNot Nothing AndAlso peticion.TipoPlanificaciones.Count > 0 Then
                    spw.Param("par$oid_tipo_planificaciones").AgregarValorArray("")
                    For Each obj In peticion.TipoPlanificaciones
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_tipo_planificaciones").AgregarValorArray(obj)
                        End If
                    Next
                End If

                If peticion.Planificaciones IsNot Nothing AndAlso peticion.Planificaciones.Count > 0 Then
                    spw.Param("par$oid_planificaciones").AgregarValorArray("")
                    For Each obj In peticion.Planificaciones
                        If Not String.IsNullOrEmpty(obj) Then
                            spw.Param("par$oid_planificaciones").AgregarValorArray(obj)
                        End If
                    Next
                End If

            End If

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, usuario, , False)
            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            If peticion.CampoExtra IsNot Nothing Or peticion.CampoExtraValor IsNot Nothing Then
                spw.AgregarParam("par$cod_termino", ProsegurDbType.Identificador_Alfanumerico, peticion.CampoExtra, , False)
                spw.AgregarParam("par$val_termino", ProsegurDbType.Descricao_Longa, peticion.CampoExtraValor, , False)
            Else
                spw.AgregarParam("par$cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$val_termino", ProsegurDbType.Descricao_Longa, Nothing, , False)
            End If

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           ByRef respuesta As RecuperarTransacciones.Respuesta)

            If respuesta Is Nothing Then respuesta = New RecuperarTransacciones.Respuesta

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                '' PrimaryKey
                'Dim primaryKeyMovimientos(ds.Tables("movimientos").PrimaryKey.Length + 1) As DataColumn
                'For Each _key In ds.Tables("movimientos").PrimaryKey
                '    primaryKeyMovimientos(primaryKeyMovimientos.Length - 1) = _key
                'Next
                'primaryKeyMovimientos(primaryKeyMovimientos.Length - 1) = ds.Tables("movimientos").Columns("COD_MOVIMIENTO")
                'ds.Tables("movimientos").PrimaryKey = primaryKeyMovimientos

                If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 Then

                    If respuesta.Transacciones Is Nothing Then respuesta.Transacciones = New List(Of ContractoServicio.Contractos.Integracion.RecuperarTransacciones.Transaccion)

                    For Each row As DataRow In ds.Tables("movimientos").Rows
                        Dim maquina = PoblarTransaccion(row)
                        respuesta.Transacciones.Add(maquina)
                    Next
                End If

            End If

        End Sub

        Private Shared Function PoblarTransaccion(row As DataRow) As RecuperarTransacciones.Transaccion
            Dim obj = New RecuperarTransacciones.Transaccion

            With obj
                .Oid_documento = Util.AtribuirValorObj(row("OID_DOCUMENTO"), GetType(String))
                Dim auxFechaGestion = Util.AtribuirValorObj(row("FYH_GESTION"), GetType(DateTime))
                Dim auxFechaHoraGestion = Util.AtribuirValorObj(row("HOR_GESTION"), GetType(DateTime))
                Dim auxFechaCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                Dim auxFechaAcreditacion = Util.AtribuirValorObj(row("FYH_ACREDITACION"), GetType(DateTime))
                Dim auxFechaNotificacion = Util.AtribuirValorObj(row("FYH_NOTIFICACION"), GetType(DateTime))

                .ColumnPivot = "Importe"
                .OidMovimiento = Util.AtribuirValorObj(row("OID_MOVIMIENTO"), GetType(String))
                .FechaGestion = BuscarDataFormatada(auxFechaHoraGestion)
                .FechaCreacion = BuscarDataFormatada(auxFechaCreacion)
                .FechaAcreditacion = BuscarDataFormatada(auxFechaAcreditacion)
                .FechaNotificacion = BuscarDataFormatada(auxFechaNotificacion)


                .HoraGestion = BuscarHoraFormatada(auxFechaHoraGestion)
                .HoraCreacion = BuscarHoraFormatada(auxFechaCreacion)
                .HoraAcreditacion = BuscarHoraFormatada(auxFechaAcreditacion)
                .HoraNotificacion = BuscarHoraFormatada(auxFechaNotificacion)



                .CampoExtra = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                .CampoExtraValor = Util.AtribuirValorObj(row("VAL_TERMINO"), GetType(String))


                .CodExterno = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))
                .CodExternoBase = Util.AtribuirValorObj(row("COD_EXTERNO_BASE"), GetType(String))
                .Maquina = Util.AtribuirValorObj(row("COD_MAQUINA"), GetType(String))
                .PuntoServicio = Util.AtribuirValorObj(row("PTO_SERVICIO"), GetType(String))
                .Cliente = Util.AtribuirValorObj(row("CLIENTE"), GetType(String))
                .SubCliente = Util.AtribuirValorObj(row("SUBCLIENTE"), GetType(String))
                .Delegacion = Util.AtribuirValorObj(row("DELEGACION"), GetType(String))
                .Canal = Util.AtribuirValorObj(row("CANAL"), GetType(String))
                .SubCanal = Util.AtribuirValorObj(row("SUBCANAL"), GetType(String))
                .TipoTransaccion = Util.AtribuirValorObj(row("TIPO_TRANSACCION"), GetType(String))
                .Formulario = Util.AtribuirValorObj(row("FORMULARIO"), GetType(String))
                .Divisa = Util.AtribuirValorObj(row("DIVISA"), GetType(String))
                .ImporteContado = Util.AtribuirValorObj(row("IMPORTE"), GetType(Double))
                '.ImporteDeclarado = Util.AtribuirValorObj(row("DECLARADO"), GetType(Double))
                .Simbolo = Util.AtribuirValorObj(row("SIMBOLO"), GetType(String))
                .CodResponsable = Util.AtribuirValorObj(row("TELLER"), GetType(String))
                .NombreResponsable = Util.AtribuirValorObj(row("TELLERNAME"), GetType(String))
                .ReceiptNumber = Util.AtribuirValorObj(row("RECEIPT_NUMBER"), GetType(String))
                .Barcode = Util.AtribuirValorObj(row("BAR_CODE"), GetType(String))
                .Modalidad = Util.AtribuirValorObj(row("MODALIDAD"), GetType(String))
                .Notificacion = Util.AtribuirValorObj(row("NOTIFICACION"), GetType(String))
                .ImporteInformativo = Util.AtribuirValorObj(row("IMPORTE_INFORMATIVO"), GetType(String))
                .Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Double))
                .Cantidad = Util.AtribuirValorObj(row("NEL_CANTIDAD"), GetType(Integer))
                .BaseDeviceId = Util.AtribuirValorObj(row("COD_DEVICEID_BASE"), GetType(String))
                '.ContadoDeclarado = Util.AtribuirValorObj(row("CONTADO_DECLARADO"), GetType(String))

            End With

            Return obj
        End Function

        Public Shared Function BuscarDataFormatada(fecha As DateTime) As String

            If fecha = Nothing OrElse fecha = DateTime.MinValue Then
                Return ""
            End If

            Return fecha.ToString("dd/MM/yyyy")
        End Function

        Public Shared Function BuscarHoraFormatada(fecha As DateTime) As String

            If fecha = Nothing OrElse fecha = DateTime.MinValue Then
                Return ""
            End If

            Return fecha.ToString("HH:mm:ss")
        End Function

        Public Shared Sub RecuperarDetalle(ByVal peticion As RecuperarTransacciones.PeticionDetalle,
                                           ByVal usuario As String,
                                           ByRef Respuesta As RecuperarTransacciones.RespuestaDetalle)
            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarPeticionDetalle(peticion, usuario)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                If Respuesta Is Nothing Then Respuesta = New RecuperarTransacciones.RespuestaDetalle
                PoblarRespuestaDetalle(ds, Respuesta)

                spw = Nothing
                ds.Dispose()

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

        Private Shared Function ColectarPeticionDetalle(peticion As RecuperarTransacciones.PeticionDetalle, usuario As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_detalle", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            If (String.IsNullOrWhiteSpace(peticion.OidMovimiento)) Then
                spw.AgregarParam("par$oid_transaccion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            Else
                spw.AgregarParam("par$oid_transaccion", ProsegurDbType.Identificador_Alfanumerico, peticion.OidMovimiento, , False)
            End If

            If (String.IsNullOrWhiteSpace(peticion.CodExternoBase)) Then
                spw.AgregarParam("par$cod_externo_base", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            Else
                spw.AgregarParam("par$cod_externo_base", ProsegurDbType.Identificador_Alfanumerico, peticion.CodExternoBase, , False)
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, usuario, , False)
            spw.AgregarParam("par$rc_detalle", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "detalle")
            spw.AgregarParam("par$rc_detalle_valores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "detalle_valores")
            spw.AgregarParam("par$rc_detalle_totales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "detalle_totales")
            spw.AgregarParam("par$rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "documentos")
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            Return spw

        End Function

        Private Shared Sub PoblarRespuestaDetalle(ds As DataSet, ByRef respuesta As RecuperarTransacciones.RespuestaDetalle)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("detalle") AndAlso ds.Tables("detalle").Rows.Count > 0 Then

                    If respuesta Is Nothing Then respuesta = New RecuperarTransacciones.RespuestaDetalle
                    respuesta.Cuentas = New List(Of RecuperarTransacciones.RespuestaDetalleCuenta)
                    For Each rowDetalle As DataRow In ds.Tables("detalle").Rows

                        Dim cuenta = New RecuperarTransacciones.RespuestaDetalleCuenta
                        cuenta.OidCuenta = Util.AtribuirValorObj(rowDetalle("OID_MOVIMIENTO"), GetType(String))


                        If respuesta.Cuentas.FirstOrDefault(Function(x) x.OidCuenta = cuenta.OidCuenta) IsNot Nothing Then
                            Continue For
                        End If

                        cuenta.Cliente = Util.AtribuirValorObj(rowDetalle("CLIENTE"), GetType(String))
                        cuenta.Subcliente = Util.AtribuirValorObj(rowDetalle("SUBCLIENTE"), GetType(String))
                        cuenta.PuntoServicio = Util.AtribuirValorObj(rowDetalle("PUNTO_SERVICIO"), GetType(String))
                        cuenta.Maquina = Util.AtribuirValorObj(rowDetalle("MAQUINA"), GetType(String))
                        cuenta.CodExterno = Util.AtribuirValorObj(rowDetalle("COD_EXTERNO"), GetType(String))
                        cuenta.Fechagestion = Util.AtribuirValorObj(rowDetalle("FYH_GESTION"), GetType(String))
                        cuenta.FechaCriacion = Util.AtribuirValorObj(rowDetalle("GMT_CREACION"), GetType(String))
                        cuenta.Teller = Util.AtribuirValorObj(rowDetalle("TELLER"), GetType(String))
                        cuenta.TipoMovimiento = Util.AtribuirValorObj(rowDetalle("TIPO_MOVIMIENTO"), GetType(String))
                        cuenta.Formulario = Util.AtribuirValorObj(rowDetalle("FORMULARIO"), GetType(String))
                        cuenta.Descricao = Util.AtribuirValorObj(rowDetalle("DESCRICAO"), GetType(String))
                        cuenta.Notificado = Util.AtribuirValorObj(rowDetalle("BOL_NOTIFICADO"), GetType(Boolean))
                        cuenta.Acreditado = Util.AtribuirValorObj(rowDetalle("BOL_ACREDITADO"), GetType(Boolean))
                        cuenta.Valores = New List(Of RecuperarTransacciones.RespuestaDetalleValores)

                        If ds.Tables.Contains("detalle_valores") AndAlso ds.Tables("detalle_valores").Rows.Count > 0 AndAlso
                            ds.Tables("detalle_valores").Select("OID_MOVIMIENTO = '" & rowDetalle("OID_MOVIMIENTO") & "' ") IsNot Nothing Then

                            cuenta.Valores = New List(Of RecuperarTransacciones.RespuestaDetalleValores)
                            For Each rowValores As DataRow In ds.Tables("detalle_valores").Select("OID_MOVIMIENTO = '" & rowDetalle("OID_MOVIMIENTO") & "' ")
                                Dim Valor = New RecuperarTransacciones.RespuestaDetalleValores
                                Valor = PoblarValores(rowValores)

                                cuenta.Valores.Add(Valor)
                            Next
                        End If


                        If ds.Tables.Contains("detalle_totales") AndAlso ds.Tables("detalle_totales").Rows.Count > 0 AndAlso
                            ds.Tables("detalle_totales").Select("OID_MOVIMIENTO = '" & rowDetalle("OID_MOVIMIENTO") & "' ") IsNot Nothing Then

                            cuenta.Totales = New List(Of RecuperarTransacciones.RespuestaDetalleTotales)
                            For Each rowValores As DataRow In ds.Tables("detalle_totales").Select("OID_MOVIMIENTO = '" & rowDetalle("OID_MOVIMIENTO") & "' ")
                                Dim Valor = New RecuperarTransacciones.RespuestaDetalleTotales
                                Valor = PoblarTotales(rowValores)

                                cuenta.Totales.Add(Valor)
                            Next
                        End If

                        respuesta.Cuentas.Add(cuenta)
                    Next


                    If ds.Tables.Contains("documentos") AndAlso ds.Tables("documentos").Rows.Count > 0 Then
                        respuesta.Documentos = New List(Of RecuperarTransacciones.RespuestaDetalleDocumento)
                        For Each row As DataRow In ds.Tables("documentos").Rows
                            Dim documento = New RecuperarTransacciones.RespuestaDetalleDocumento

                            documento.OidDocumento = Util.AtribuirValorObj(row("OID_MOVIMIENTO"), GetType(String))
                            documento.Documento = Util.AtribuirValorObj(row("COD_MOVIMIENTO"), GetType(String))

                            respuesta.Documentos.Add(documento)
                        Next
                    End If
                End If
            End If

        End Sub

        Private Shared Function PoblarTotales(row As DataRow) As RecuperarTransacciones.RespuestaDetalleTotales

            Dim obj = New RecuperarTransacciones.RespuestaDetalleTotales

            With obj
                .Canal = Util.AtribuirValorObj(row("CANAL"), GetType(String))
                .Subcanal = Util.AtribuirValorObj(row("SUBCANAL"), GetType(String))
                .Divisa = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
                .Importe = Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(Double))
                .Canal_Subcanal = .Canal + " / " + .Subcanal
                .OidCuenta = Util.AtribuirValorObj(row("OID_MOVIMIENTO"), GetType(String))

            End With

            Return obj

        End Function

        Private Shared Function PoblarValores(row As DataRow) As RecuperarTransacciones.RespuestaDetalleValores

            Dim obj = New RecuperarTransacciones.RespuestaDetalleValores

            With obj
                .Canal = Util.AtribuirValorObj(row("CANAL"), GetType(String))
                .Subcanal = Util.AtribuirValorObj(row("SUBCANAL"), GetType(String))
                .Divisa = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
                .Denominacion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
                .Importe = Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(Double))
                .Cantidad = Util.AtribuirValorObj(row("NEL_CANTIDAD"), GetType(Integer))
                .Canal_Subcanal = .Canal + " / " + .Subcanal
                .OidCuenta = Util.AtribuirValorObj(row("OID_MOVIMIENTO"), GetType(String))

            End With

            Return obj

        End Function

#End Region

#Region "[Integracion]"

#Region "[Metodos Base Para Consultas]"

        Public Shared Sub RecuperarDatosDeLosDocumentos(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                                        ByRef dtDocumentos As DataTable,
                                                        valorParamTermino As String,
                                            Optional obtenerPadre As Boolean = False,
                                            Optional ByRef log As StringBuilder = Nothing,
                                            Optional validarTermino As Boolean = False)

            If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then

                Dim logCuentas As New StringBuilder
                Dim logHistorico As New StringBuilder
                Dim logFormulario As New StringBuilder
                Dim logDocumentosPadres As New StringBuilder
                Dim logValoresTermino As New StringBuilder
                Dim logElementos As New StringBuilder

                If documentos Is Nothing Then
                    documentos = New ObservableCollection(Of Clases.Documento)
                End If

                Dim identificadoresDocumento As New List(Of String)
                Dim identificadoresCuenta As New List(Of String)
                Dim identificadoresFormulario As New List(Of String)
                Dim identificadoresDocumentoPadre As New List(Of String)
                Dim identificadoresTipoDocumento As New List(Of String)

                For Each rowDocumento In dtDocumentos.Rows

                    If documentos.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String))) Is Nothing Then

                        Dim documento As New Clases.Documento

                        With documento

                            Dim estadoRemesa As String = If(rowDocumento.Table.Columns.Contains("COD_ESTADO_REMESA"), Util.AtribuirValorObj(rowDocumento("COD_ESTADO_REMESA"), GetType(String)), Nothing)

                            If estadoRemesa = Enumeradores.EstadoRemesa.EnTransito.RecuperarValor OrElse estadoRemesa = Enumeradores.EstadoRemesa.Anulado.RecuperarValor Then
                                Continue For
                            End If

                            If validarTermino Then
                                Dim valorCodigoTermino As String = If(rowDocumento.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(rowDocumento("DES_VALOR"), GetType(String)), Nothing)

                                If Not valorParamTermino.Equals(valorCodigoTermino) Then
                                    Continue For
                                End If

                            End If

                            .Identificador = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), Nothing)
                            .IdentificadorGrupo = If(rowDocumento.Table.Columns.Contains("OID_GRUPO_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_GRUPO_DOCUMENTO"), GetType(String)), Nothing)
                            .IdentificadorMovimentacionFondo = If(rowDocumento.Table.Columns.Contains("OID_MOVIMENTACION_FONDO"), Util.AtribuirValorObj(rowDocumento("OID_MOVIMENTACION_FONDO"), GetType(String)), Nothing)
                            .IdentificadorSustituto = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_SUSTITUTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_SUSTITUTO"), GetType(String)), Nothing)
                            .EstaCertificado = If(rowDocumento.Table.Columns.Contains("BOL_CERTIFICADO"), Util.AtribuirValorObj(rowDocumento("BOL_CERTIFICADO"), GetType(Boolean)), Nothing)
                            .NumeroExterno = If(rowDocumento.Table.Columns.Contains("COD_EXTERNO"), Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String)), Nothing)
                            .CodigoComprobante = If(rowDocumento.Table.Columns.Contains("COD_COMPROBANTE"), Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String)), Nothing)
                            .FechaHoraGestion = If(rowDocumento.Table.Columns.Contains("FYH_GESTION"), Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime)), Nothing)
                            If rowDocumento.Table.Columns.Contains("FYH_PLAN_CERTIFICACION_SINGMT") AndAlso rowDocumento("FYH_PLAN_CERTIFICACION_SINGMT") Is DBNull.Value Then
                                .FechaHoraPlanificacionCertificacion = Date.MinValue
                            Else
                                .FechaHoraPlanificacionCertificacion = If(rowDocumento.Table.Columns.Contains("FYH_PLAN_CERTIFICACION"), Util.AtribuirValorObj(rowDocumento("FYH_PLAN_CERTIFICACION"), GetType(DateTime)), Nothing)
                            End If
                            .Estado = If(rowDocumento.Table.Columns.Contains("COD_ESTADO") AndAlso Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                            .ExportadoSol = If(rowDocumento.Table.Columns.Contains("EXPORTADO_SOL") AndAlso Not rowDocumento("EXPORTADO_SOL").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("EXPORTADO_SOL"), GetType(Boolean)), False)
                            .IdentificadorIntegracion = If(rowDocumento.Table.Columns.Contains("OID_INTEGRACION"), Util.AtribuirValorObj(rowDocumento("OID_INTEGRACION"), GetType(String)), Nothing)
                            .NelIntentoEnvio = If(rowDocumento.Table.Columns.Contains("NEL_INTENTO_ENVIO") AndAlso Not rowDocumento("NEL_INTENTO_ENVIO").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("NEL_INTENTO_ENVIO"), GetType(Boolean)), False)
                            .CodigoCertificacionCuentas = If(rowDocumento.Table.Columns.Contains("COD_CERTIFICACION_CUENTAS"), Util.AtribuirValorObj(rowDocumento("COD_CERTIFICACION_CUENTAS"), GetType(String)), Nothing)
                            .EstadosPosibles = ObtenerEstadosPossibles(.Estado)
                            .MensajeExterno = If(rowDocumento.Table.Columns.Contains("DES_MENSAJE_EXTERNO"), Util.AtribuirValorObj(rowDocumento("DES_MENSAJE_EXTERNO"), GetType(String)), Nothing)

                            If rowDocumento.Table.Columns.Contains("OID_FORMULARIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))) Then
                                .Formulario = New Clases.Formulario()
                                .Formulario.Identificador = Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))
                                If Not identificadoresFormulario.Contains(.Formulario.Identificador) Then
                                    identificadoresFormulario.Add(.Formulario.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_PADRE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))) Then
                                .DocumentoPadre = New Clases.Documento()
                                .DocumentoPadre.Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))
                                If Not identificadoresDocumentoPadre.Contains(.DocumentoPadre.Identificador) Then
                                    identificadoresDocumentoPadre.Add(.DocumentoPadre.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_TIPO_DOCUMENTO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                                .TipoDocumento = New Clases.TipoDocumento()
                                .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                                If Not identificadoresTipoDocumento.Contains(.TipoDocumento.Identificador) Then
                                    identificadoresTipoDocumento.Add(.TipoDocumento.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))) Then
                                .CuentaOrigen = New Clases.Cuenta()
                                .CuentaOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaOrigen.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaOrigen.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))) Then
                                .CuentaDestino = New Clases.Cuenta()
                                .CuentaDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaDestino.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaDestino.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))) Then
                                .CuentaSaldoOrigen = New Clases.Cuenta()
                                .CuentaSaldoOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaSaldoOrigen.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaSaldoOrigen.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))) Then
                                .CuentaSaldoDestino = New Clases.Cuenta()
                                .CuentaSaldoDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaSaldoDestino.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaSaldoDestino.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_REMESA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))) Then
                                .Elemento = New Clases.Remesa()
                                .Elemento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))
                            End If

                            identificadoresDocumento.Add(.Identificador)

                        End With

                        documentos.Add(documento)

                    End If

                Next


                Dim cuentas As ObservableCollection(Of Clases.Cuenta) = Nothing
                Dim dtHistoricos As DataTable = Nothing
                Dim dtValoresTermino As DataTable = Nothing
                Dim elementos As ObservableCollection(Of Clases.Remesa) = Nothing
                Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing
                Dim documentosPadres As ObservableCollection(Of Clases.Documento) = Nothing

                ' Cuentas (Origen, Destino, Movimiento y Saldos) - (OID_CUENTA_ORIGEN, OID_CUENTA_DESTINO, OID_CUENTA_SALDO_ORIGEN, OID_CUENTA_SALDO_DESTINO)
                Dim TCuentas As New Task(Sub()
                                             Dim TiempoCuentas As DateTime = Now
                                             cuentas = Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Ambos, "RecuperarDatosDeLosDocumentos")
                                             logCuentas.AppendLine("____Tiempo 'ObtenerCuentasPorIdentificadores_v2': " & Now.Subtract(TiempoCuentas).ToString() & "; ")
                                         End Sub)
                TCuentas.Start()

                ' Historico - (OID_DOCUMENTO)
                Dim THistorico As New Task(Sub()

                                               Dim TiempoHistoricos As DateTime = Now
                                               dtHistoricos = HistoricoMovimentacionDocumento.ObtenerHistoricoMovimentacion(identificadoresDocumento)
                                               logHistorico.AppendLine("____Tiempo 'ObtenerHistoricoMovimentacion': " & Now.Subtract(TiempoHistoricos).ToString() & "; ")
                                           End Sub)
                THistorico.Start()

                ' Formulario - (OID_FORMULARIO)
                Dim TFormulario As New Task(Sub()
                                                Dim TiempoFormularios As DateTime = Now
                                                formularios = Formulario.ObtenerFormulariosPorIdentificadores_v2(identificadoresFormulario)
                                                logFormulario.AppendLine("____Tiempo 'ObtenerFormulariosPorIdentificadores_v2': " & Now.Subtract(TiempoFormularios).ToString() & "; ")
                                            End Sub)
                TFormulario.Start()

                ' DocumentoPadre - (OID_DOCUMENTO_PADRE)
                Dim TDocumentosPadres As New Task(Sub()
                                                      Dim TiempoDocumentosPadres As DateTime = Now

                                                      If obtenerPadre Then
                                                          documentosPadres = recuperarDocumentosPorIdentificadores(identificadoresDocumentoPadre, "obtenerDocumentos", Nothing)
                                                          logDocumentosPadres.AppendLine("____Tiempo 'ObtenerDocumentosPorIdentificadores': " & Now.Subtract(TiempoDocumentosPadres).ToString() & "; ")
                                                      Else
                                                          logDocumentosPadres.AppendLine("____No recupera Documentos Padres; ")
                                                      End If
                                                  End Sub)
                TDocumentosPadres.Start()

                ' ValorTerminoDocumento - (OID_DOCUMENTO)
                Dim TValoresTermino As New Task(Sub()
                                                    Dim TiempoValoresTermino As DateTime = Now
                                                    dtValoresTermino = ValorTerminoDocumento.ObtenerValorTerminoDocumento_v2(identificadoresDocumento)
                                                    logValoresTermino.AppendLine("____Tiempo 'ObtenerValorTerminoDocumento_v2': " & Now.Subtract(TiempoValoresTermino).ToString() & "; ")
                                                End Sub)
                TValoresTermino.Start()

                ' Elemento - (OID_DOCUMENTO)
                Dim TElementos As New Task(Sub()
                                               Dim TiempoElementos As DateTime = Now
                                               elementos = Remesa.ObtenerRemesasPorDocumentos_SinProcedure(identificadoresDocumento, logElementos)
                                               logElementos.AppendLine("____Tiempo 'ObtenerRemesasPorDocumentos_v2': " & Now.Subtract(TiempoElementos).ToString() & "; ")
                                           End Sub)
                TElementos.Start()

                ' Valores del Documento - (OID_DOCUMENTO)
                Divisas.ObtenerValoresDeLosDocumentos_v2(documentos)

                Task.WaitAll(New Task() {TCuentas, THistorico, TFormulario, TDocumentosPadres, TValoresTermino, TElementos})

                If log IsNot Nothing Then
                    log.Append(logCuentas)
                    log.Append(logHistorico)
                    log.Append(logFormulario)
                    log.Append(logDocumentosPadres)
                    log.Append(logValoresTermino)
                    log.Append(logElementos)
                End If

                Dim Tiempo As DateTime = Now

                cargarDocumentos(documentos, dtHistoricos, formularios, cuentas, documentosPadres, dtValoresTermino, elementos)

                If log IsNot Nothing Then
                    log.AppendLine("____Tiempo 'cargarDocumentos': " & Now.Subtract(Tiempo).ToString() & "; ")
                End If

            End If

        End Sub

        Public Shared Sub ObtenerDatosDeLosDocumentos(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                                      ByRef dtDocumentos As DataTable,
                                             Optional obtenerPadre As Boolean = False,
                                             Optional ByRef log As StringBuilder = Nothing,
                                             Optional simplificado As Boolean = False)

            If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then

                Dim logCuentas As New StringBuilder
                Dim logHistorico As New StringBuilder
                Dim logFormulario As New StringBuilder
                Dim logDocumentosPadres As New StringBuilder
                Dim logValoresTermino As New StringBuilder
                Dim logElementos As New StringBuilder

                If documentos Is Nothing Then
                    documentos = New ObservableCollection(Of Clases.Documento)
                End If

                Dim identificadoresDocumento As New List(Of String)
                Dim identificadoresCuenta As New List(Of String)
                Dim identificadoresFormulario As New List(Of String)
                Dim identificadoresDocumentoPadre As New List(Of String)
                Dim identificadoresTipoDocumento As New List(Of String)

                For Each rowDocumento In dtDocumentos.Rows

                    If documentos.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String))) Is Nothing Then

                        Dim documento As New Clases.Documento

                        With documento

                            .Identificador = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), Nothing)
                            .IdentificadorGrupo = If(rowDocumento.Table.Columns.Contains("OID_GRUPO_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_GRUPO_DOCUMENTO"), GetType(String)), Nothing)
                            .IdentificadorMovimentacionFondo = If(rowDocumento.Table.Columns.Contains("OID_MOVIMENTACION_FONDO"), Util.AtribuirValorObj(rowDocumento("OID_MOVIMENTACION_FONDO"), GetType(String)), Nothing)
                            .IdentificadorSustituto = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_SUSTITUTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_SUSTITUTO"), GetType(String)), Nothing)
                            .EstaCertificado = If(rowDocumento.Table.Columns.Contains("BOL_CERTIFICADO"), Util.AtribuirValorObj(rowDocumento("BOL_CERTIFICADO"), GetType(Boolean)), Nothing)
                            .NumeroExterno = If(rowDocumento.Table.Columns.Contains("COD_EXTERNO"), Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String)), Nothing)
                            .CodigoComprobante = If(rowDocumento.Table.Columns.Contains("COD_COMPROBANTE"), Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String)), Nothing)
                            .FechaHoraGestion = If(rowDocumento.Table.Columns.Contains("FYH_GESTION"), Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime)), Nothing)

                            .UsuarioCreacion = If(rowDocumento.Table.Columns.Contains("DES_USUARIO_CREACION"), Util.AtribuirValorObj(rowDocumento("DES_USUARIO_CREACION"), GetType(String)), Nothing)
                            .UsuarioModificacion = If(rowDocumento.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(rowDocumento("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)

                            If rowDocumento.Table.Columns.Contains("FYH_PLAN_CERTIFICACION_SINGMT") AndAlso rowDocumento("FYH_PLAN_CERTIFICACION_SINGMT") Is DBNull.Value Then
                                .FechaHoraPlanificacionCertificacion = Date.MinValue
                            Else
                                .FechaHoraPlanificacionCertificacion = If(rowDocumento.Table.Columns.Contains("FYH_PLAN_CERTIFICACION"), Util.AtribuirValorObj(rowDocumento("FYH_PLAN_CERTIFICACION"), GetType(DateTime)), Nothing)
                            End If
                            .Estado = If(rowDocumento.Table.Columns.Contains("COD_ESTADO") AndAlso Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                            .ExportadoSol = If(rowDocumento.Table.Columns.Contains("EXPORTADO_SOL") AndAlso Not rowDocumento("EXPORTADO_SOL").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("EXPORTADO_SOL"), GetType(Boolean)), False)
                            .IdentificadorIntegracion = If(rowDocumento.Table.Columns.Contains("OID_INTEGRACION"), Util.AtribuirValorObj(rowDocumento("OID_INTEGRACION"), GetType(String)), Nothing)
                            .NelIntentoEnvio = If(rowDocumento.Table.Columns.Contains("NEL_INTENTO_ENVIO") AndAlso Not rowDocumento("NEL_INTENTO_ENVIO").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("NEL_INTENTO_ENVIO"), GetType(Integer)), False)
                            .CodigoCertificacionCuentas = If(rowDocumento.Table.Columns.Contains("COD_CERTIFICACION_CUENTAS"), Util.AtribuirValorObj(rowDocumento("COD_CERTIFICACION_CUENTAS"), GetType(String)), Nothing)
                            .EstadosPosibles = ObtenerEstadosPossibles(.Estado)
                            .MensajeExterno = If(rowDocumento.Table.Columns.Contains("DES_MENSAJE_EXTERNO"), Util.AtribuirValorObj(rowDocumento("DES_MENSAJE_EXTERNO"), GetType(String)), Nothing)

                            If rowDocumento.Table.Columns.Contains("OID_FORMULARIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))) Then
                                .Formulario = New Clases.Formulario()
                                .Formulario.Identificador = Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))
                                If Not identificadoresFormulario.Contains(.Formulario.Identificador) Then
                                    identificadoresFormulario.Add(.Formulario.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_PADRE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))) Then
                                .DocumentoPadre = New Clases.Documento()
                                .DocumentoPadre.Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))
                                If Not identificadoresDocumentoPadre.Contains(.DocumentoPadre.Identificador) Then
                                    identificadoresDocumentoPadre.Add(.DocumentoPadre.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_TIPO_DOCUMENTO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                                .TipoDocumento = New Clases.TipoDocumento()
                                .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                                If Not identificadoresTipoDocumento.Contains(.TipoDocumento.Identificador) Then
                                    identificadoresTipoDocumento.Add(.TipoDocumento.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))) Then
                                .CuentaOrigen = New Clases.Cuenta()
                                .CuentaOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaOrigen.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaOrigen.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))) Then
                                .CuentaDestino = New Clases.Cuenta()
                                .CuentaDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaDestino.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaDestino.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))) Then
                                .CuentaSaldoOrigen = New Clases.Cuenta()
                                .CuentaSaldoOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaSaldoOrigen.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaSaldoOrigen.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))) Then
                                .CuentaSaldoDestino = New Clases.Cuenta()
                                .CuentaSaldoDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))
                                If Not identificadoresCuenta.Contains(.CuentaSaldoDestino.Identificador) Then
                                    identificadoresCuenta.Add(.CuentaSaldoDestino.Identificador)
                                End If
                            End If
                            If rowDocumento.Table.Columns.Contains("OID_REMESA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))) Then
                                .Elemento = New Clases.Remesa()
                                .Elemento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))
                            End If
                            .Rowver = If(rowDocumento.Table.Columns.Contains("ROWVER"), Util.AtribuirValorObj(rowDocumento("ROWVER"), GetType(Long?)), Nothing)

                            identificadoresDocumento.Add(.Identificador)

                        End With

                        documentos.Add(documento)

                    End If

                Next

                If Not simplificado Then

                    Dim cuentas As ObservableCollection(Of Clases.Cuenta) = Nothing
                    Dim dtHistoricos As DataTable = Nothing
                    Dim dtValoresTermino As DataTable = Nothing
                    Dim elementos As ObservableCollection(Of Clases.Remesa) = Nothing
                    Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing
                    Dim documentosPadres As ObservableCollection(Of Clases.Documento) = Nothing

                    ' Cuentas (Origen, Destino, Movimiento y Saldos) - (OID_CUENTA_ORIGEN, OID_CUENTA_DESTINO, OID_CUENTA_SALDO_ORIGEN, OID_CUENTA_SALDO_DESTINO)
                    Dim TCuentas As New Task(Sub()
                                                 Dim TiempoCuentas As DateTime = Now
                                                 cuentas = Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Ambos, "ObtenerDatosDeLosDocumentos")
                                                 logCuentas.AppendLine("____Tiempo 'ObtenerCuentasPorIdentificadores_v2': " & Now.Subtract(TiempoCuentas).ToString() & "; ")
                                             End Sub)
                    TCuentas.Start()

                    ' Historico - (OID_DOCUMENTO)
                    Dim THistorico As New Task(Sub()

                                                   Dim TiempoHistoricos As DateTime = Now
                                                   dtHistoricos = HistoricoMovimentacionDocumento.ObtenerHistoricoMovimentacion(identificadoresDocumento)
                                                   logHistorico.AppendLine("____Tiempo 'ObtenerHistoricoMovimentacion': " & Now.Subtract(TiempoHistoricos).ToString() & "; ")
                                               End Sub)
                    THistorico.Start()

                    ' Formulario - (OID_FORMULARIO)
                    Dim TFormulario As New Task(Sub()
                                                    Dim TiempoFormularios As DateTime = Now
                                                    formularios = Formulario.ObtenerFormulariosPorIdentificadores_v2(identificadoresFormulario)
                                                    logFormulario.AppendLine("____Tiempo 'ObtenerFormulariosPorIdentificadores_v2': " & Now.Subtract(TiempoFormularios).ToString() & "; ")
                                                End Sub)
                    TFormulario.Start()

                    ' DocumentoPadre - (OID_DOCUMENTO_PADRE)
                    Dim TDocumentosPadres As New Task(Sub()
                                                          Dim TiempoDocumentosPadres As DateTime = Now

                                                          If obtenerPadre Then
                                                              documentosPadres = recuperarDocumentosPorIdentificadores(identificadoresDocumentoPadre, "obtenerDocumentos", Nothing)
                                                              logDocumentosPadres.AppendLine("____Tiempo 'ObtenerDocumentosPorIdentificadores': " & Now.Subtract(TiempoDocumentosPadres).ToString() & "; ")
                                                          Else
                                                              logDocumentosPadres.AppendLine("____No recupera Documentos Padres; ")
                                                          End If
                                                      End Sub)
                    TDocumentosPadres.Start()

                    ' ValorTerminoDocumento - (OID_DOCUMENTO)
                    Dim TValoresTermino As New Task(Sub()
                                                        Dim TiempoValoresTermino As DateTime = Now
                                                        dtValoresTermino = ValorTerminoDocumento.ObtenerValorTerminoDocumento_v2(identificadoresDocumento)
                                                        logValoresTermino.AppendLine("____Tiempo 'ObtenerValorTerminoDocumento_v2': " & Now.Subtract(TiempoValoresTermino).ToString() & "; ")
                                                    End Sub)
                    TValoresTermino.Start()

                    ' Elemento - (OID_DOCUMENTO)
                    Dim TElementos As New Task(Sub()
                                                   Dim TiempoElementos As DateTime = Now
                                                   elementos = Remesa.ObtenerRemesasPorDocumentos_SinProcedure(identificadoresDocumento, logElementos)
                                                   logElementos.AppendLine("____Tiempo 'ObtenerRemesasPorDocumentos_v2': " & Now.Subtract(TiempoElementos).ToString() & "; ")
                                               End Sub)
                    TElementos.Start()

                    ' Valores del Documento - (OID_DOCUMENTO)
                    Divisas.ObtenerValoresDeLosDocumentos_v2(documentos)

                    Task.WaitAll(New Task() {TCuentas, THistorico, TFormulario, TDocumentosPadres, TValoresTermino, TElementos})

                    If log IsNot Nothing Then
                        log.Append(logCuentas)
                        log.Append(logHistorico)
                        log.Append(logFormulario)
                        log.Append(logDocumentosPadres)
                        log.Append(logValoresTermino)
                        log.Append(logElementos)
                    End If

                    Dim Tiempo As DateTime = Now

                    cargarDocumentos(documentos, dtHistoricos, formularios, cuentas, documentosPadres, dtValoresTermino, elementos)

                    If log IsNot Nothing Then
                        log.AppendLine("____Tiempo 'cargarDocumentos': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                End If
            End If
        End Sub

        Public Shared Sub cargarDocumentos(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                           ByRef dtHistoricos As DataTable,
                                           ByRef formularios As ObservableCollection(Of Clases.Formulario),
                                           ByRef cuentas As ObservableCollection(Of Clases.Cuenta),
                                           ByRef documentosPadres As ObservableCollection(Of Clases.Documento),
                                           ByRef dtValoresTermino As DataTable,
                                           ByRef elementos As ObservableCollection(Of Clases.Remesa))

            If documentos IsNot Nothing AndAlso documentos.Count > 0 Then

                For Each documento In documentos

                    ' Cargar DocumentosPadres
                    If documentosPadres IsNot Nothing AndAlso documentosPadres.Count > 0 AndAlso
                        documento.DocumentoPadre IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.DocumentoPadre.Identificador) Then
                        Dim padre = documentosPadres.FirstOrDefault(Function(x) x.Identificador = documento.DocumentoPadre.Identificador)
                        If padre IsNot Nothing AndAlso Not String.IsNullOrEmpty(padre.Identificador) Then
                            documento.DocumentoPadre = padre
                        End If
                    End If

                    ' Cargar Formulario
                    If formularios IsNot Nothing AndAlso formularios.Count > 0 AndAlso
                        documento.Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.Formulario.Identificador) Then
                        Dim formulario = formularios.FirstOrDefault(Function(x) x.Identificador = documento.Formulario.Identificador)
                        If formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(formulario.Identificador) Then
                            documento.Formulario = formulario

                            ' Cargar Valores Terminos Documento
                            documento.GrupoTerminosIAC = formulario.GrupoTerminosIACIndividual.Clonar
                            If documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso
                                documento.GrupoTerminosIAC.TerminosIAC.Count > 0 AndAlso dtValoresTermino IsNot Nothing AndAlso dtValoresTermino.Rows.Count > 0 Then

                                Dim filtroValoresTermino = dtValoresTermino.Select("OID_DOCUMENTO = '" & documento.Identificador & "'")
                                If filtroValoresTermino IsNot Nothing Then
                                    For Each rowValor In filtroValoresTermino
                                        Dim termino = documento.GrupoTerminosIAC.TerminosIAC.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(rowValor("OID_TERMINO"), GetType(String)))
                                        If termino IsNot Nothing Then
                                            termino.Valor = If(rowValor.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(rowValor("DES_VALOR"), GetType(String)), Nothing)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If

                    ' Cargar Cuentas
                    If cuentas IsNot Nothing AndAlso cuentas.Count > 0 Then
                        If documento.CuentaOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaOrigen.Identificador) Then
                            Dim cuentaOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaOrigen.Identificador)
                            If cuentaOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaOrigen.Identificador) Then
                                documento.CuentaOrigen = cuentaOrigen
                            End If
                        End If
                        If documento.CuentaDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaDestino.Identificador) Then
                            Dim cuentaDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaDestino.Identificador)
                            If cuentaDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaDestino.Identificador) Then
                                documento.CuentaDestino = cuentaDestino
                            End If
                        End If
                        If documento.CuentaSaldoOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaSaldoOrigen.Identificador) Then
                            Dim cuentaSaldoOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaSaldoOrigen.Identificador)
                            If cuentaSaldoOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaSaldoOrigen.Identificador) Then
                                documento.CuentaSaldoOrigen = cuentaSaldoOrigen
                            End If
                        End If
                        If documento.CuentaSaldoDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaSaldoDestino.Identificador) Then
                            Dim cuentaSaldoDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaSaldoDestino.Identificador)
                            If cuentaSaldoDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaSaldoDestino.Identificador) Then
                                documento.CuentaSaldoDestino = cuentaSaldoDestino
                            End If
                        End If
                    End If

                    ' Cargar Historico Documento
                    If dtHistoricos IsNot Nothing AndAlso dtHistoricos.Rows.Count > 0 Then
                        Dim filtroHistorico = dtHistoricos.Select("OID_DOCUMENTO = '" & documento.Identificador & "'")
                        If filtroHistorico IsNot Nothing Then
                            If documento.Historico Is Nothing Then documento.Historico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
                            For Each rowHistorico In filtroHistorico
                                Dim historico As New Clases.HistoricoMovimientoDocumento
                                With historico
                                    .Estado = If(rowHistorico.Table.Columns.Contains("COD_ESTADO") AndAlso Not rowHistorico("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowHistorico("COD_ESTADO").ToString), Nothing)
                                    .FechaHoraModificacion = If(rowHistorico.Table.Columns.Contains("GMT_MODIFICACION"), Util.AtribuirValorObj(rowHistorico("GMT_MODIFICACION"), GetType(DateTime)), Nothing)
                                    .UsuarioModificacion = If(rowHistorico.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(rowHistorico("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)
                                End With
                                documento.Historico.Add(historico)
                            Next
                        End If
                    End If

                    ' Cargar Elementos
                    If elementos IsNot Nothing AndAlso elementos.Count > 0 Then

                        If elementos.First.TrabajaPorBulto Then

                            Dim Remesas As ObservableCollection(Of Clases.Remesa) = TryCast(elementos, ObservableCollection(Of Clases.Remesa))

                            If Remesas IsNot Nothing AndAlso Remesas.Count > 0 Then

                                For Each remesa In Remesas

                                    If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 AndAlso remesa.Bultos.Exists(Function(b) b.IdentificadorDocumento = documento.Identificador) Then
                                        documento.Elemento = remesa.Clonar
                                        Exit For
                                    End If

                                Next

                            End If

                        Else

                            Dim remesa = elementos.FirstOrDefault(Function(x) x.IdentificadorDocumento = documento.Identificador)
                            If remesa IsNot Nothing Then
                                documento.Elemento = remesa
                            ElseIf documento.Elemento IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.Elemento.Identificador) Then
                                remesa = elementos.FirstOrDefault(Function(x) x.Identificador = documento.Elemento.Identificador)
                                If remesa IsNot Nothing Then
                                    documento.Elemento = remesa
                                End If
                            End If

                        End If

                    End If

                    ' remove todos os valores vazios e com valores iguais a zero (e que tenham quantidades iguais a zero também)
                    Prosegur.Genesis.Comon.Util.BorrarItemsDivisaSinValoresCantidades(documento)

                Next

            End If

        End Sub

#End Region

#Region "[Consultas]"

        ''' <summary>
        ''' Recupera o codigo do comprobante
        ''' </summary>
        ''' <param name="IdentificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarCodigoComprobante(IdentificadorDocumento As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoRecuperarCodigoComprobante)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function ObtenerDocumentosPorIdentificadorGrupo(ByRef identificadoresGrupoDocumento As String,
                                                             Optional obtenerPadre As Boolean = False) As ObservableCollection(Of Clases.Documento)
            Dim documentos As New ObservableCollection(Of Clases.Documento)

            If Not String.IsNullOrEmpty(identificadoresGrupoDocumento) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim inner As String = ""
                        Dim filtro As String = " AND OID_GRUPO_DOCUMENTO = []OID_GRUPO_DOCUMENTO AND COD_ESTADO <> []COD_ESTADO "

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO",
                                                                                    ProsegurDbType.Objeto_Id, identificadoresGrupoDocumento))

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoDocumento.Anulado.RecuperarValor))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Documento_ObtenerDocumento_Base, inner, filtro))
                        command.CommandType = CommandType.Text

                        ObtenerDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), obtenerPadre)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return documentos
        End Function

        Public Shared Function obtenerDocumentosPorFiltro(ByRef parametrosFiltro As Clases.Transferencias.FiltroDocumentos_v2,
                                                 Optional obtenerPadre As Boolean = False,
                                                 Optional simplificado As Boolean = False) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            If parametrosFiltro IsNot Nothing Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim inner As String = ""
                        Dim filtro As String = ""

                        ' Documento
                        If Not String.IsNullOrEmpty(parametrosFiltro.IdentificadorSector) Then
                            filtro &= " AND S.OID_SECTOR = []OID_SECTOR"
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Curta, parametrosFiltro.IdentificadorSector))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoComprobante) Then
                            filtro &= " AND D.COD_COMPROBANTE = []COD_COMPROBANTE"
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoComprobante))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoDocumento) Then
                            filtro &= String.Format(" AND D.COD_ESTADO {0} []COD_ESTADO_DOCUMENTO ", If(parametrosFiltro.CodigoEstadoDocumentoIgual, " = ", " <> "))
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCUMENTO", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoEstadoDocumento))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoDocumentoElemento) Then
                            inner &= "INNER JOIN SAPR_TDOCUMENTOXELEMENTO DE ON DE.OID_DOCUMENTO = D.OID_DOCUMENTO " & vbNewLine
                            filtro &= " AND DE.COD_ESTADO_DOCXELEMENTO = []COD_ESTADO_DOCXELEMENTO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoEstadoDocumentoElemento))
                        End If

                        ' Elementos
                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoExterno) OrElse Not String.IsNullOrEmpty(parametrosFiltro.CodigoPrecintoBulto) OrElse
                            Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoBulto) OrElse parametrosFiltro.FechaTransporteDesde <> Date.MinValue OrElse
                            Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoRemesa) OrElse
                            Not String.IsNullOrEmpty(parametrosFiltro.IdentificadorExternoRemesa) OrElse Not String.IsNullOrEmpty(parametrosFiltro.CodigoRuta) Then

                            inner &= "INNER JOIN SAPR_TBULTO B ON B.OID_DOCUMENTO = D.OID_DOCUMENTO " & vbNewLine
                            inner &= "INNER JOIN SAPR_TREMESA R ON R.OID_REMESA = B.OID_REMESA " & vbNewLine

                        End If

                        ' Remesa
                        If Not String.IsNullOrEmpty(parametrosFiltro.IdentificadorExternoRemesa) Then
                            filtro &= " AND R.OID_EXTERNO = []OID_EXTERNO_REMESA "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO_REMESA", ProsegurDbType.Descricao_Curta, parametrosFiltro.IdentificadorExternoRemesa))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoExterno) Then
                            filtro &= " AND R.COD_EXTERNO = []CODIGO_EXTERNO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CODIGO_EXTERNO", ProsegurDbType.Descricao_Longa, parametrosFiltro.CodigoExterno))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoRuta) Then
                            filtro &= " AND R.COD_RUTA = []COD_RUTA "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoRuta))
                        End If

                        If parametrosFiltro.FechaCreacion <> Date.MinValue Then
                            filtro &= " AND D.GMT_CREACION >= []GMT_CREACION AND D.GMT_CREACION < ([]GMT_CREACION + 1) "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION", ProsegurDbType.Data, parametrosFiltro.FechaCreacion))
                        End If

                        If parametrosFiltro.FechaTransporteDesde <> Date.MinValue Then
                            filtro &= " AND R.FYH_TRANSPORTE >= []FYH_TRANSPORTE AND R.FYH_TRANSPORTE < ([]FYH_TRANSPORTE + 1) "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data, parametrosFiltro.FechaTransporteDesde))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoRemesa) Then
                            filtro &= " AND R.COD_ESTADO = []COD_ESTADO_REMESA "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_REMESA", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoEstadoRemesa))
                        End If

                        'Bulto
                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoPrecintoBulto) Then
                            filtro &= " AND B.COD_PRECINTO = []COD_PRECINTO_BULTO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO_BULTO", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoPrecintoBulto))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEstadoBulto) Then
                            filtro &= " AND B.COD_ESTADO = []COD_ESTADO_BULTO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_BULTO", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoEstadoBulto))
                        End If

                        'Ruta
                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEmisor) Then
                            inner &= " LEFT JOIN SAPR_TVALOR_TERMINOXDOCUMENTO TD ON TD.OID_DOCUMENTO = D.OID_DOCUMENTO "
                            filtro &= " AND TD.DES_VALOR = []COD_EMISOR "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EMISOR", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoEmisor))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoSector) AndAlso Not String.IsNullOrEmpty(parametrosFiltro.CodigoPlanta) AndAlso Not String.IsNullOrEmpty(parametrosFiltro.CodigoDelegacion) Then

                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Documento_ObtenerDocumento_Base, inner, filtro))
                        command.CommandType = CommandType.Text

                        ObtenerDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), obtenerPadre, , simplificado)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return documentos

        End Function

        Public Shared Function RecuperarDocumentosSinSalidaRecorrido(codigosExternos As List(Of String), ByRef codigosExternosConSalidaRecorrido As List(Of String)) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            'Dim retornoDocumentos As New ObservableCollection(Of Clases.Documento)
            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = My.Resources.RecuperarDocumentosSinSalidaRecorrido
                    command.CommandType = CommandType.Text
                    command.CommandText = String.Format(command.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosExternos, "COD_EXTERNO", command, "AND", "DOCU"))
                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)


                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        For Each row In dt.Rows

                            Dim documento As New Clases.Documento

                            documento.NumeroExterno = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))

                            If Not documentos.Exists(Function(x) x.NumeroExterno = documento.NumeroExterno) Then
                                documento.Identificador = Util.AtribuirValorObj(row("OID_DOCUMENTO"), GetType(String))
                                documentos.Add(documento)
                            End If

                        Next

                        codigosExternosConSalidaRecorrido = (From codigo In codigosExternos Where Not documentos.Exists(Function(x) x.NumeroExterno = codigo) Select codigo).ToList
                    Else
                        ' se não encontrou nenhum documento, é porque todos que foram passados no filtro tem seu ultimo formulario como SalidaRecorrido
                        codigosExternosConSalidaRecorrido = codigosExternos

                    End If

                    'ObtenerDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command))

                    '' elimina os documentos duplicados
                    'For Each doc In documentos

                    '    If Not retornoDocumentos.Exists(Function(e) e.NumeroExterno = doc.NumeroExterno) Then
                    '        retornoDocumentos.Add(doc)
                    '        Continue For
                    '    End If

                    '    If doc.Elemento IsNot Nothing Then
                    '        Dim docRecuperado = retornoDocumentos.FirstOrDefault(Function(e) e.NumeroExterno = doc.NumeroExterno)

                    '        If docRecuperado.Elemento Is Nothing Then
                    '            docRecuperado.Elemento = doc.Elemento
                    '        End If

                    '    End If

                    'Next

                End Using

            Catch ex As Exception
                Throw

            Finally
                GC.Collect()

            End Try

            Return documentos

        End Function

        Public Shared Function RecuperarDocumentoParaAlocacion(identificadorSector As String,
                                                               codigoExterno As String,
                                                               codigoPrecinto As String) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)
            Dim retornoDocumentos As New ObservableCollection(Of Clases.Documento)
            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = My.Resources.RecuperarDocumentoParaAlocacion
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, identificadorSector))

                    If Not String.IsNullOrEmpty(codigoExterno) Then
                        command.CommandText = String.Format(command.CommandText, " AND REME.COD_EXTERNO = []COD_EXTERNO")
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Curta, codigoExterno))

                    ElseIf Not String.IsNullOrEmpty(codigoPrecinto) Then
                        command.CommandText = String.Format(command.CommandText, " AND BULT.COD_PRECINTO = []COD_PRECINTO")
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Curta, codigoPrecinto))

                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                    ObtenerDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), True)

                    ' elimina os documentos duplicados
                    For Each doc In documentos

                        If Not retornoDocumentos.Exists(Function(e) e.NumeroExterno = doc.NumeroExterno) Then
                            retornoDocumentos.Add(doc)
                            Continue For
                        End If

                        If doc.Elemento IsNot Nothing Then
                            Dim docRecuperado = retornoDocumentos.FirstOrDefault(Function(e) e.NumeroExterno = doc.NumeroExterno)

                            If docRecuperado.Elemento Is Nothing Then
                                docRecuperado.Elemento = doc.Elemento
                            End If

                        End If

                    Next

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return retornoDocumentos

        End Function

        Public Shared Function RecuperarDocumentosElementosConcluidos(ByRef parametrosFiltro As Clases.Transferencias.FiltroDocumentos_v2) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            If parametrosFiltro IsNot Nothing Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim seleccion As String = String.Empty
                        Dim inner As String = String.Empty
                        Dim filtro As String = String.Empty

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoDelegacion))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Curta, parametrosFiltro.IdentificadorSector))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor))

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoRuta) Then
                            filtro &= " AND REME.COD_RUTA = []COD_RUTA "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, parametrosFiltro.CodigoRuta))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoExterno) Then
                            filtro &= " AND REME.COD_EXTERNO LIKE []COD_EXTERNO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Curta, "%" & parametrosFiltro.CodigoExterno & "%"))
                        End If

                        If parametrosFiltro.FechaTransporteDesde <> Date.MinValue AndAlso parametrosFiltro.FechaTransporteHasta <> Date.MinValue Then
                            filtro &= " AND (REME.FYH_TRANSPORTE >= []FYH_TRANSPORTE_DESDE AND REME.FYH_TRANSPORTE <= ([]FYH_TRANSPORTE_HASTA)) "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE_DESDE", ProsegurDbType.Data, parametrosFiltro.FechaTransporteDesde))
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE_HASTA", ProsegurDbType.Data, parametrosFiltro.FechaTransporteHasta))
                        End If

                        If parametrosFiltro.FechaCreacion <> Date.MinValue Then
                            filtro &= " AND DOCU.GMT_CREACION >= []GMT_CREACION AND DOCU.GMT_CREACION < ([]GMT_CREACION + 1) "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION", ProsegurDbType.Data, parametrosFiltro.FechaCreacion))
                        End If

                        If Not String.IsNullOrEmpty(parametrosFiltro.CodigoEmisor) Then
                            seleccion = " ,TEDO.DES_VALOR "
                            inner &= " INNER JOIN SAPR_TVALOR_TERMINOXDOCUMENTO TEDO ON TEDO.OID_DOCUMENTO = DOCU.OID_DOCUMENTO "
                            inner &= " INNER JOIN GEPR_TTERMINO TERM ON TERM.OID_TERMINO = TEDO.OID_TERMINO AND TERM.COD_TERMINO = " & "'" & Enumeradores.TerminosDatosRuta.CodigoOrigenRemesa.RecuperarValor & "' "
                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Documento_RecuperarDocumentosElementosConcluidos, seleccion, inner, filtro))
                        command.CommandType = CommandType.Text

                        RecuperarDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), parametrosFiltro.CodigoEmisor, validarTermino:=seleccion.Length > 0)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return documentos

        End Function

        Public Shared Function obtenerDocumentosNoEnviadoSol(identificadoresRemesas As List(Of String), numMaxIntentos As Integer,
                                                    Optional obtenerPadre As Boolean = False) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            If identificadoresRemesas IsNot Nothing AndAlso identificadoresRemesas.Count > 0 Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_INTENTO_ENVIO", ProsegurDbType.Inteiro_Longo, numMaxIntentos))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor))

                        command.CommandText = String.Format(My.Resources.Documento_ObtenerDocumento_NoEnviadasSol, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_EXTERNO", command, "AND", "R"))
                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                        command.CommandType = CommandType.Text

                        ObtenerDatosDeLosDocumentos(documentos, AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command), obtenerPadre)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return documentos

        End Function
        Public Shared Function ExportadoSOL(codigoExterno As String) As Boolean

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, codigoExterno))

                    command.CommandText = My.Resources.Documento_ExportadoSOL

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                    command.CommandType = CommandType.Text

                    Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Function

        Public Shared Function obtenerDocumentosPorRuta(codigoDelegacion As String,
                                                        codigoRuta As String,
                                                        fechaRuta As DateTime,
                                               Optional obtenerPadre As Boolean = False,
                                               Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)


            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim Tiempo As DateTime = Now

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentosRecuperarPorRuta)
                    command.CommandType = CommandType.Text
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumento.Sustituido.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Identificador_Alfanumerico, codigoRuta))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_RUTA", ProsegurDbType.Descricao_Curta, fechaRuta.ToString("dd/MM/yyyy")))

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If log IsNot Nothing Then
                        log.AppendLine("__Tiempo consulta 'DocumentosRecuperarPorRuta': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                    Tiempo = Now
                    ObtenerDatosDeLosDocumentos(documentos, dt, obtenerPadre, log)

                    If log IsNot Nothing Then
                        log.AppendLine("__Tiempo cargar 'ObtenerDatosDeLosDocumentos': " & Now.Subtract(Tiempo).ToString() & "; ")
                    End If

                End Using

                If documentos IsNot Nothing AndAlso documentos.Count = 0 Then
                    documentos = Nothing
                Else
                    RecuperarHistoricoDocumentosElementos(documentos)
                End If

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return documentos

        End Function

        Private Shared Sub RecuperarHistoricoDocumentosElementos(ByRef documentos As ObservableCollection(Of Clases.Documento))

            ' loop, se possuir formulario com caracteristicas e elemento (remesa)
            For Each doc In documentos.Where(Function(x) x.Formulario IsNot Nothing AndAlso
                                                         x.Formulario.Caracteristicas IsNot Nothing AndAlso
                                                         x.Formulario.Caracteristicas.Count > 0 AndAlso
                                                         x.Elemento IsNot Nothing).ToList


                Dim remesa As Clases.Remesa = DirectCast(doc.Elemento, Clases.Remesa)

                'se formulario for de Bajas (SalidaRecorida), seta propriedade da remesa
                If doc.Formulario.Caracteristicas.Exists(Function(x) x = Enumeradores.CaracteristicaFormulario.Bajas) Then
                    remesa.UltimoDocRelaccionadoSalidaRecorido = True
                    remesa.TuveSalidaRecorido = False

                Else
                    remesa.UltimoDocRelaccionadoSalidaRecorido = False

                    If documentos.Exists((Function(x) x.Identificador <> doc.Identificador AndAlso
                                                      DirectCast(x.Elemento, Clases.Remesa).CodigoExterno = remesa.CodigoExterno AndAlso
                                                      DirectCast(x.Elemento, Clases.Remesa).Ruta = remesa.Ruta AndAlso
                                                      DirectCast(x.Elemento, Clases.Remesa).FechaHoraTransporte.Date = remesa.FechaHoraTransporte.Date)) Then

                        remesa.TuveSalidaRecorido = True

                    End If

                End If

            Next doc

        End Sub

        Private Shared Function ValidarElementoTuveSalidaRecorida(codigoExterno As String,
                                                                  codigoRuta As String,
                                                                  fyh_transporte As DateTime) As Boolean

            Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim Tiempo As DateTime = Now

                command.CommandText = "WITH Q1 AS ( " _
                                    & "Select REME.OID_DOCUMENTO " _
                                    & "FROM SAPR_TREMESA REME " _
                                    & "WHERE REME.COD_EXTERNO = []COD_EXTERNO AND " _
                                    & "REME.COD_RUTA = []COD_RUTA AND " _
                                    & "REME.FYH_TRANSPORTE = []FYH_TRANSPORTE " _
                                    & ") " _
                                    & "SELECT COUNT(1) FROM Q1 INNER JOIN SAPR_TDOCUMENTO DOC ON DOC.OID_DOCUMENTO = Q1.OID_DOCUMENTO " _
                                                     & "INNER JOIN SAPR_TFORMULARIO F ON F.OID_FORMULARIO = DOC.OID_FORMULARIO " _
                                                     & "INNER JOIN SAPR_TCARACTFORMXFORMULARIO CFF ON CFF.OID_FORMULARIO = F.OID_FORMULARIO " _
                                                     & "INNER JOIN SAPR_TCARACT_FORMULARIO CF ON CF.OID_CARACT_FORMULARIO = CFF.OID_CARACT_FORMULARIO " _
                                   & "WHERE CF.COD_CARACT_FORMULARIO = 'ACCION_BAJAS' " _
                                   & "ORDER BY DOC.GMT_CREACION DESC "

                command.CommandType = CommandType.Text

                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Curta, codigoExterno))
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, codigoRuta))
                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, fyh_transporte))

                command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)

                Dim resultado As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command)

                Return resultado > 0

            End Using

        End Function

        Public Shared Function obtenerDocumentosNoAlocados(codigoCliente As String,
                                                           codigoSubCliente As String,
                                                           codigoPuntoServicio As String,
                                                           codigoServicio As String,
                                                           codigoDelegacion As String,
                                                           codigoFormulario As String,
                                                  Optional obtenerPadre As Boolean = False,
                                                  Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.Documento)

            Dim documentos As New ObservableCollection(Of Clases.Documento)

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim Tiempo As DateTime = Now

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentosRecuperarNaoAlocados)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumento.Sustituido.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TERMINO", ProsegurDbType.Observacao_Longa, Enumeradores.TerminosDatosRuta.CodigoServicio.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SERVICIO", ProsegurDbType.Observacao_Longa, codigoServicio))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Observacao_Longa, codigoCliente))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Observacao_Longa, codigoPuntoServicio))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Observacao_Longa, codigoSubCliente))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FORMULARIO", ProsegurDbType.Observacao_Longa, codigoFormulario))

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    ObtenerDatosDeLosDocumentos(documentos, dt, obtenerPadre, log)

                End Using

                If documentos IsNot Nothing AndAlso documentos.Count = 0 Then
                    documentos = Nothing
                End If

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return documentos

        End Function

        Shared Function obtenerDocumentosPorRuta_Mobile(Peticion As ContractoServicio.Documento.Mobile.ObtenerDocumento.Peticion,
                                                        codigoSectorRecepcionPorDefecto As String) As DataTable

            Dim dtDocumentos As DataTable = Nothing
            Dim queryDefecto As String = My.Resources.ObtenerDocumentoMobile

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim codigos As List(Of String) = Peticion.datosRuta.Select(Function(r) r.codigoRuta).ToList
                    Dim fechaRutaInicio As DateTime = Peticion.datosRuta(0).fechaRuta
                    Dim fechaRutaFin As DateTime = fechaRutaInicio.AddHours(11).AddMinutes(59).AddSeconds(59)
                    If Peticion.bolRutaDiaSiguiente Then
                        fechaRutaFin = fechaRutaFin.AddDays(1)
                    End If

                    If Not Peticion.bolTodosRutas AndAlso codigos IsNot Nothing AndAlso codigos.Count > 0 Then

                        If codigos.Count = 1 Then

                            command.CommandText = String.Format(queryDefecto, " AND REM.COD_RUTA = []COD_RUTA ")
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, codigos(0)))

                        Else

                            command.CommandText = String.Format(queryDefecto, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigos, "COD_RUTA", command, "AND", "REM"))

                        End If

                    Else
                        command.CommandText = String.Format(queryDefecto, String.Empty)
                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.codigoDelegacion))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_RUTA_INICIO", ProsegurDbType.Data_Hora, fechaRutaInicio))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_RUTA_FIN", ProsegurDbType.Data_Hora, fechaRutaFin))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSectorRecepcionPorDefecto))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                    command.CommandType = CommandType.Text

                    dtDocumentos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return dtDocumentos
        End Function

        Shared Function obtenerRemesasPorRuta_Mobile(Peticion As ContractoServicio.Documento.Mobile.ObtenerDocumento.Peticion) As DataTable

            Dim dtDocumentos As DataTable = Nothing
            Dim queryDefecto As String = My.Resources.ObtenerRemesaMobile

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim codigos As List(Of String) = Peticion.datosRuta.Select(Function(r) r.codigoRuta).ToList
                    Dim fechaRutaInicio As DateTime = Peticion.datosRuta(0).fechaRuta
                    Dim fechaRutaFin As DateTime = fechaRutaInicio.AddHours(11).AddMinutes(59).AddSeconds(59)
                    If Peticion.bolRutaDiaSiguiente Then
                        fechaRutaFin = fechaRutaFin.AddDays(1)
                    End If

                    If Not Peticion.bolTodosRutas AndAlso codigos IsNot Nothing AndAlso codigos.Count > 0 Then

                        If codigos.Count = 1 Then
                            command.CommandText = String.Format(queryDefecto, " AND REM.COD_RUTA = []COD_RUTA ", String.Empty)
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, codigos(0)))
                        Else
                            command.CommandText = String.Format(queryDefecto, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigos, "COD_RUTA", command, "AND", "REM"), String.Empty)
                        End If
                    ElseIf Peticion.somenteSinRutas Then
                        command.CommandText = String.Format(queryDefecto, "AND REM.COD_RUTA is null ", String.Empty)
                    Else
                        command.CommandText = String.Format(queryDefecto, String.Empty, String.Empty)
                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.codigoDelegacion))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_RUTA_INICIO", ProsegurDbType.Data_Hora, fechaRutaInicio))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_RUTA_FIN", ProsegurDbType.Data_Hora, fechaRutaFin))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, Peticion.codigoSector))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                    command.CommandType = CommandType.Text

                    dtDocumentos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return dtDocumentos
        End Function

        Shared Function ValidarSiExisteCodigoExterno(codigoExterno As String) As Boolean

            Dim respuesta As Boolean = False

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim Tiempo As DateTime = Now

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_ValidarSiExisteCodigoExterno)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, codigoExterno))

                    If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command) > 0 Then
                        respuesta = True
                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return respuesta
        End Function

        Shared Function ValidarSiExisteCodigoExternoEnTransito(codigosExternos As List(Of String)) As List(Of String)

            Dim respuesta As List(Of String) = Nothing

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""

                    If codigosExternos.Count = 1 Then

                        filtro &= " AND R.COD_EXTERNO = []COD_EXTERNO "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO",
                                                                                    ProsegurDbType.Objeto_Id, codigosExternos(0)))

                    Else

                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosExternos, "COD_EXTERNO",
                                                                               command, "AND", "R")

                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Documento_ValidarSiExisteCodigoExternoEnTransito, filtro))
                    command.CommandType = CommandType.Text

                    Dim dtDocumentos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then

                        For Each rowDocumento In dtDocumentos.Rows

                            If rowDocumento("COD_ESTADO") = "PE" OrElse
                               rowDocumento("COD_ESTADO") = "ET" OrElse
                               rowDocumento("COD_ESTADO_DOCXELEMENTO") = "T" Then

                                If respuesta Is Nothing Then respuesta = New List(Of String)

                                If Not respuesta.Contains(rowDocumento("COD_EXTERNO")) Then
                                    respuesta.Add(rowDocumento("COD_EXTERNO"))
                                End If

                            End If
                        Next

                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return respuesta
        End Function

        Shared Function HayCierreCajaEnCurso(identificadorSectorOrigen As String) As Boolean

            Dim respuesta As Boolean = False

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_HayCierreCajaEnCurso)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoDocumento.EnCurso.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CARACT_FORMULARIO", ProsegurDbType.Descricao_Longa, Enumeradores.CaracteristicaFormulario.CierreDeCaja.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_ORIGEN", ProsegurDbType.Identificador_Alfanumerico, identificadorSectorOrigen))

                    If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command) > 0 Then
                        respuesta = True
                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return respuesta

        End Function

#End Region

#End Region

#Region "[CONSULTAS]"

        ''' <summary>
        ''' Recupera o rowverdocumento
        ''' </summary>
        ''' <param name="IdentificadorDocumento"></param>
        ''' <param name="CodigoComprobante"></param>
        ''' <returns></returns>
        Public Shared Function RecuperarRowVerDocumento(IdentificadorDocumento As String, CodigoComprobante As String) As Integer

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DocumentoRecuperarRowVer
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorDocumento) Then

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, " DOC.OID_DOCUMENTO = []OID_DOCUMENTO "))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, IdentificadorDocumento))

            ElseIf Not String.IsNullOrEmpty(CodigoComprobante) Then

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, " DOC.COD_COMPROBANTE = []COD_COMPROBANTE "))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, CodigoComprobante))

            End If

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        ''' <summary>
        ''' Obtener Estado del Documento
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerEstadoDocumento(identificadorDocumento As String) As Enumeradores.EstadoDocumento?
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarEstadoDocumento)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                Dim objEstado As String = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

                If Not String.IsNullOrEmpty(objEstado) Then
                    Return RecuperarEnum(Of Enumeradores.EstadoDocumento)(objEstado)
                End If

            End Using
            Return Nothing
        End Function

        ''' <summary>
        '''Mudança paleativa solicitada pelo Ricardo para ser utilizada apenas por consultas com filtro de disponibilidade
        '''Essa consulta é de uma versão anterior
        '''Quando a consulta correspondente for passada para procedure este método deverá ser excluido
        '''para que o metodo 'ObtenerListaDocumentos' comporte todos os filtros da tela
        '''Feito por: Moane Prates Data: 13/09/16
        ''' </summary>
        ''' <param name="objPeticion"></param>
        ''' <param name="objRespuesta"></param>
        ''' <param name="objDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerListaDocumentosFiltro(objPeticion As Peticion(Of Clases.Transferencias.FiltroDocumentos),
                                         ByRef objRespuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento)),
                                         objDelegacion As Clases.Delegacion) As DataTable

            Dim dtDocumentos As New DataTable
            objRespuesta.ParametrosPaginacion = New Paginacion.ParametrosRespuestaPaginacion()

            If objPeticion IsNot Nothing AndAlso objPeticion.Parametro IsNot Nothing Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text

                ' Parametros Obrigatorios
                Dim DestinoOrigen As String = "D"
                If objPeticion.Parametro.TipoSitioDocumento = Enumeradores.TipoSitio.Origen Then DestinoOrigen = "O"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OIDSECTOR", ProsegurDbType.Objeto_Id, objPeticion.Parametro.IdentificadorSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESTINORIGEN", ProsegurDbType.Descricao_Curta, DestinoOrigen))

                Dim InnerJoin As String = ""
                Dim SqlConcat As New Text.StringBuilder()
                Dim sqlConcatIndividual As New Text.StringBuilder()
                Dim sqlConcatGrupo As New Text.StringBuilder()
                Dim sqlInnerSectorOrigenIndividual As New Text.StringBuilder()
                Dim sqlInnerSectorOrigenGrupo As New Text.StringBuilder()

                ' Estado
                If objPeticion.Parametro.EstadoDocumento IsNot Nothing Then
                    If objPeticion.Parametro.EstadoDocumento.Count > 1 Then
                        Dim listEstados As New List(Of String)
                        For Each objEstado In objPeticion.Parametro.EstadoDocumento
                            listEstados.Add("'" & objEstado.RecuperarValor() & "'")
                        Next
                        SqlConcat.AppendLine(" AND DOC.COD_ESTADO IN (" & String.Join(", ", listEstados.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.EstadoDocumento.Count = 1 Then
                        SqlConcat.AppendLine(" AND DOC.COD_ESTADO = '" & objPeticion.Parametro.EstadoDocumento(0).RecuperarValor() & "' ")
                    End If
                End If

                ' Numero Comprobante
                If objPeticion.Parametro.NumerosComprobantes IsNot Nothing Then
                    If objPeticion.Parametro.NumerosComprobantes.Count > 1 Then
                        sqlConcatIndividual.AppendLine(" AND DOC.COD_COMPROBANTE IN (" & String.Join(", ", objPeticion.Parametro.NumerosComprobantes.ToArray()) & ") ")
                        sqlConcatGrupo.AppendLine(" AND GRU.COD_COMPROBANTE IN (" & String.Join(", ", objPeticion.Parametro.NumerosComprobantes.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.NumerosComprobantes.Count = 1 Then
                        sqlConcatIndividual.AppendLine(" AND DOC.COD_COMPROBANTE = " & objPeticion.Parametro.NumerosComprobantes(0) & " ")
                        sqlConcatGrupo.AppendLine(" AND GRU.COD_COMPROBANTE = " & objPeticion.Parametro.NumerosComprobantes(0) & " ")
                    End If
                End If

                ' Codigo Externo
                If objPeticion.Parametro.NumerosExternos IsNot Nothing Then
                    If objPeticion.Parametro.NumerosExternos.Count > 1 Then
                        SqlConcat.AppendLine(" AND DOC.COD_EXTERNO IN (" & String.Join(", ", objPeticion.Parametro.NumerosExternos.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.NumerosExternos.Count = 1 Then
                        SqlConcat.AppendLine(" AND DOC.COD_EXTERNO = " & objPeticion.Parametro.NumerosExternos(0) & " ")
                    End If
                End If

                ' GMT_CREACION
                If objPeticion.Parametro.FechaCreacionDesde <> New DateTime() AndAlso objPeticion.Parametro.FechaCreacionHasta <> New DateTime() Then
                    SqlConcat.AppendLine(" AND (DOC.GMT_CREACION BETWEEN []GMT_CREACION_DESDE AND []GMT_CREACION_HASTA) ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_DESDE", ProsegurDbType.Data_Hora, AccesoDatos.Util.DataHoraGMT_GrabarEnLaBase(objPeticion.Parametro.FechaCreacionDesde, objDelegacion)))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_HASTA", ProsegurDbType.Data_Hora, AccesoDatos.Util.DataHoraGMT_GrabarEnLaBase(objPeticion.Parametro.FechaCreacionHasta, objDelegacion)))
                Else
                    If objPeticion.Parametro.FechaCreacionDesde <> New DateTime() Then
                        SqlConcat.AppendLine(" AND DOC.GMT_CREACION >= []GMT_CREACION_DESDE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_DESDE", ProsegurDbType.Data_Hora, AccesoDatos.Util.DataHoraGMT_GrabarEnLaBase(objPeticion.Parametro.FechaCreacionDesde, objDelegacion)))
                    End If
                    If objPeticion.Parametro.FechaCreacionHasta <> New DateTime() Then
                        SqlConcat.AppendLine(" AND DOC.GMT_CREACION <= []GMT_CREACION_HASTA")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_HASTA", ProsegurDbType.Data_Hora, AccesoDatos.Util.DataHoraGMT_GrabarEnLaBase(objPeticion.Parametro.FechaCreacionHasta, objDelegacion)))
                    End If
                End If

                ' Codigo Comprobante Desde Hasta
                If objPeticion.Parametro.NumeroComprovanteDesde <> Nothing AndAlso objPeticion.Parametro.NumeroComprovanteHasta <> Nothing Then
                    sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE <= '{1}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde, objPeticion.Parametro.NumeroComprovanteHasta))
                    sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE <= '{1}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde, objPeticion.Parametro.NumeroComprovanteHasta))
                Else
                    If objPeticion.Parametro.NumeroComprovanteDesde <> Nothing Then
                        sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde))
                        sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE >= '{0}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde))
                    End If
                    If objPeticion.Parametro.NumeroComprovanteHasta <> Nothing Then
                        sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE <= '{0}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteHasta))
                        sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE <= '{0}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteHasta))
                    End If
                End If

                'If objPeticion.Parametro.ConsiderarSectoresHijos Then
                '    sqlInnerSectorOrigenIndividual.AppendLine(" INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.DES_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR ")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                FROM GEPR_TSECTOR SECT")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                START WITH 1 = 1 AND SECT.OID_SECTOR = []OIDSECTOR")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE")
                '    sqlInnerSectorOrigenIndividual.AppendLine("              ) SECO ON SECO.OID_SECTOR = CAO.OID_SECTOR")

                '    sqlInnerSectorOrigenGrupo.AppendLine(" INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.DES_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR ")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                FROM GEPR_TSECTOR SECT")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                START WITH 1 = 1 AND SECT.OID_SECTOR = []OIDSECTOR")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE")
                '    sqlInnerSectorOrigenGrupo.AppendLine("              ) SECO ON CASE WHEN []DESTINORIGEN = 'O' THEN GRU.OID_SECTOR_ORIGEN  ELSE GRU.OID_SECTOR_DESTINO END = SECO.OID_SECTOR ")
                'Else
                sqlInnerSectorOrigenIndividual.AppendLine(" INNER JOIN GEPR_TSECTOR SECO ON CAO.OID_SECTOR = SECO.OID_SECTOR ")
                sqlInnerSectorOrigenGrupo.AppendLine(" INNER JOIN GEPR_TSECTOR SECO ON CASE WHEN []DESTINORIGEN = 'O' THEN GRU.OID_SECTOR_ORIGEN  ELSE GRU.OID_SECTOR_DESTINO END = SECO.OID_SECTOR ")
                SqlConcat.AppendLine(" AND SECO.OID_SECTOR = []OIDSECTOR ")
                'End If

                'Disponibible
                If objPeticion.Parametro.PorDisponibilidad IsNot Nothing Then
                    If objPeticion.Parametro.PorDisponibilidad Then
                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO MEDPAG ON DOC.Oid_Documento = MEDPAG.OID_DOCUMENTO "
                        cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentosFiltro),
                                                    SqlConcat.ToString() & " AND MEDPAG.BOL_DISPONIBLE = 1 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)

                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_EFECTIVO EFECT ON DOC.Oid_Documento = EFECT.OID_DOCUMENTO "
                        cmd.CommandText &= vbNewLine & " UNION " & vbNewLine & String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentosFiltro),
                                                    SqlConcat.ToString() & " AND EFECT.BOL_DISPONIBLE = 1 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                    Else
                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO MEDPAG ON DOC.Oid_Documento = MEDPAG.OID_DOCUMENTO "
                        cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentosFiltro),
                                                    SqlConcat.ToString() & " AND MEDPAG.BOL_DISPONIBLE = 0 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)

                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_EFECTIVO EFECT ON DOC.Oid_Documento = EFECT.OID_DOCUMENTO "
                        cmd.CommandText &= vbNewLine & " UNION " & vbNewLine & String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentosFiltro),
                                                    SqlConcat.ToString() & " AND EFECT.BOL_DISPONIBLE = 0 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                    End If
                Else
                    cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentosFiltro),
                                                    SqlConcat, InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                End If

                cmd.CommandText = "SELECT * FROM (" & vbNewLine & cmd.CommandText & vbNewLine & ") ORDER BY GMT_CREACION DESC"

                dtDocumentos = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, objRespuesta.ParametrosPaginacion)

            End If

            Return dtDocumentos
        End Function
        Public Shared Function ObtenerListaDocumentos(objPeticion As Peticion(Of Clases.Transferencias.FiltroDocumentos),
                                         ByRef objRespuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento)),
                                         objDelegacion As Clases.Delegacion) As DataTable

            Dim dtDocumentos As New DataTable
            objRespuesta.ParametrosPaginacion = New Paginacion.ParametrosRespuestaPaginacion()

            If objPeticion IsNot Nothing AndAlso objPeticion.Parametro IsNot Nothing Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text

                ' Parametros Obrigatorios
                Dim DestinoOrigen As String = "D"
                If objPeticion.Parametro.TipoSitioDocumento = Enumeradores.TipoSitio.Origen Then DestinoOrigen = "O"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OIDSECTOR", ProsegurDbType.Objeto_Id, objPeticion.Parametro.IdentificadorSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DESTINORIGEN", ProsegurDbType.Descricao_Curta, DestinoOrigen))

                Dim InnerJoin As String = ""
                Dim SqlConcat As New Text.StringBuilder()
                Dim sqlConcatIndividual As New Text.StringBuilder()
                Dim sqlConcatGrupo As New Text.StringBuilder()
                Dim sqlInnerSectorOrigenIndividual As New Text.StringBuilder()
                Dim sqlInnerSectorOrigenGrupo As New Text.StringBuilder()
                Dim whereDocumento As New Text.StringBuilder()

                ' Estado
                If objPeticion.Parametro.EstadoDocumento IsNot Nothing Then
                    If objPeticion.Parametro.EstadoDocumento.Count > 1 Then
                        Dim listEstados As New List(Of String)
                        For Each objEstado In objPeticion.Parametro.EstadoDocumento
                            listEstados.Add("'" & objEstado.RecuperarValor() & "'")
                        Next
                        whereDocumento.AppendLine(" AND DOC.COD_ESTADO IN (" & String.Join(", ", listEstados.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.EstadoDocumento.Count = 1 Then
                        whereDocumento.AppendLine(" AND DOC.COD_ESTADO = '" & objPeticion.Parametro.EstadoDocumento(0).RecuperarValor() & "' ")
                    End If
                End If

                ' Numero Comprobante
                If objPeticion.Parametro.NumerosComprobantes IsNot Nothing Then
                    If objPeticion.Parametro.NumerosComprobantes.Count > 1 Then
                        sqlConcatIndividual.AppendLine(" AND DOC.COD_COMPROBANTE IN (" & String.Join(", ", objPeticion.Parametro.NumerosComprobantes.ToArray()) & ") ")
                        sqlConcatGrupo.AppendLine(" AND GRU.COD_COMPROBANTE IN (" & String.Join(", ", objPeticion.Parametro.NumerosComprobantes.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.NumerosComprobantes.Count = 1 Then
                        sqlConcatIndividual.AppendLine(" AND DOC.COD_COMPROBANTE = " & objPeticion.Parametro.NumerosComprobantes(0) & " ")
                        sqlConcatGrupo.AppendLine(" AND GRU.COD_COMPROBANTE = " & objPeticion.Parametro.NumerosComprobantes(0) & " ")
                    End If
                End If

                ' Codigo Externo
                If objPeticion.Parametro.NumerosExternos IsNot Nothing Then
                    If objPeticion.Parametro.NumerosExternos.Count > 1 Then
                        whereDocumento.AppendLine(" AND DOC.COD_EXTERNO IN (" & String.Join(", ", objPeticion.Parametro.NumerosExternos.ToArray()) & ") ")
                    ElseIf objPeticion.Parametro.NumerosExternos.Count = 1 Then
                        whereDocumento.AppendLine(" AND DOC.COD_EXTERNO = " & objPeticion.Parametro.NumerosExternos(0) & " ")
                    End If
                End If

                ' GMT_CREACION
                If objPeticion.Parametro.FechaCreacionDesde <> New DateTime() AndAlso objPeticion.Parametro.FechaCreacionHasta <> New DateTime() Then

                    whereDocumento.AppendLine(" AND ( DOC.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') AND DOC.GMT_CREACION <= TO_TIMESTAMP_TZ([]GMT_CREACION_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM')) ")

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_DESDE", ProsegurDbType.Descricao_Curta,
                                                                                objPeticion.Parametro.FechaCreacionDesde.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_HASTA", ProsegurDbType.Descricao_Curta,
                                                                                objPeticion.Parametro.FechaCreacionHasta.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion).ToString("dd/MM/yyyy HH:mm:ss")))

                Else
                    If objPeticion.Parametro.FechaCreacionDesde <> New DateTime() Then
                        whereDocumento.AppendLine(" AND DOC.GMT_CREACION >= TO_TIMESTAMP_TZ([]GMT_CREACION_DESDE || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_DESDE", ProsegurDbType.Descricao_Curta,
                                                                                    objPeticion.Parametro.FechaCreacionDesde.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion).ToString("dd/MM/yyyy HH:mm:ss")))
                    End If
                    If objPeticion.Parametro.FechaCreacionHasta <> New DateTime() Then
                        whereDocumento.AppendLine(" AND DOC.GMT_CREACION <= TO_TIMESTAMP_TZ([]GMT_CREACION_HASTA || ' +00:00','DD/MM/YYYY HH24:MI:SS TZH:TZM') ")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_HASTA", ProsegurDbType.Descricao_Curta,
                                                                                    objPeticion.Parametro.FechaCreacionHasta.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion).ToString("dd/MM/yyyy HH:mm:ss")))
                    End If
                End If

                ' Codigo Comprobante Desde Hasta
                If objPeticion.Parametro.NumeroComprovanteDesde <> Nothing AndAlso objPeticion.Parametro.NumeroComprovanteHasta <> Nothing Then
                    sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE <= '{1}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde, objPeticion.Parametro.NumeroComprovanteHasta))
                    sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE <= '{1}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde, objPeticion.Parametro.NumeroComprovanteHasta))
                Else
                    If objPeticion.Parametro.NumeroComprovanteDesde <> Nothing Then
                        sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE >= '{0}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde))
                        sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE >= '{0}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteDesde))
                    End If
                    If objPeticion.Parametro.NumeroComprovanteHasta <> Nothing Then
                        sqlConcatIndividual.AppendLine(String.Format(" AND DOC.COD_COMPROBANTE <= '{0}' AND DOC.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteHasta))
                        sqlConcatGrupo.AppendLine(String.Format(" AND GRU.COD_COMPROBANTE <= '{0}' AND GRU.COD_COMPROBANTE IS NOT NULL", objPeticion.Parametro.NumeroComprovanteHasta))
                    End If
                End If

                'If objPeticion.Parametro.ConsiderarSectoresHijos Then
                '    sqlInnerSectorOrigenIndividual.AppendLine(" INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.DES_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR ")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                FROM GEPR_TSECTOR SECT")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                START WITH 1 = 1 AND SECT.OID_SECTOR = []OIDSECTOR")
                '    sqlInnerSectorOrigenIndividual.AppendLine("                CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE")
                '    sqlInnerSectorOrigenIndividual.AppendLine("              ) SECO ON SECO.OID_SECTOR = CAO.OID_SECTOR")

                '    sqlInnerSectorOrigenGrupo.AppendLine(" INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.DES_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR ")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                FROM GEPR_TSECTOR SECT")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                START WITH 1 = 1 AND SECT.OID_SECTOR = []OIDSECTOR")
                '    sqlInnerSectorOrigenGrupo.AppendLine("                CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE")
                '    sqlInnerSectorOrigenGrupo.AppendLine("              ) SECO ON CASE WHEN []DESTINORIGEN = 'O' THEN GRU.OID_SECTOR_ORIGEN  ELSE GRU.OID_SECTOR_DESTINO END = SECO.OID_SECTOR ")
                'Else
                sqlInnerSectorOrigenIndividual.AppendLine(" INNER JOIN GEPR_TSECTOR SECO ON CAO.OID_SECTOR = SECO.OID_SECTOR ")
                sqlInnerSectorOrigenGrupo.AppendLine(" INNER JOIN GEPR_TSECTOR SECO ON CASE WHEN []DESTINORIGEN = 'O' THEN GRU.OID_SECTOR_ORIGEN  ELSE GRU.OID_SECTOR_DESTINO END = SECO.OID_SECTOR ")
                SqlConcat.AppendLine(" AND SECO.OID_SECTOR = []OIDSECTOR ")
                'End If

                Dim sql As String = My.Resources.ObtenerDocumentos.Replace("[WHERE_DOCUMENTO]", whereDocumento.ToString)

                'Disponibible
                If objPeticion.Parametro.PorDisponibilidad IsNot Nothing Then
                    If objPeticion.Parametro.PorDisponibilidad Then
                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO MEDPAG ON DOC.Oid_Documento = MEDPAG.OID_DOCUMENTO "
                        cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql),
                                                    SqlConcat.ToString() & " AND MEDPAG.BOL_DISPONIBLE = 1 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)

                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_EFECTIVO EFECT ON DOC.Oid_Documento = EFECT.OID_DOCUMENTO "
                        cmd.CommandText &= vbNewLine & " UNION " & vbNewLine & String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentos),
                                                    SqlConcat.ToString() & " AND EFECT.BOL_DISPONIBLE = 1 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                    Else
                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_MEDIO_PAGO MEDPAG ON DOC.Oid_Documento = MEDPAG.OID_DOCUMENTO "
                        cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql),
                                                    SqlConcat.ToString() & " AND MEDPAG.BOL_DISPONIBLE = 0 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)

                        InnerJoin = " INNER JOIN SAPR_TTRANSACCION_EFECTIVO EFECT ON DOC.Oid_Documento = EFECT.OID_DOCUMENTO "
                        cmd.CommandText &= vbNewLine & " UNION " & vbNewLine & String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDocumentos),
                                                    SqlConcat.ToString() & " AND EFECT.BOL_DISPONIBLE = 0 ", InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                    End If
                Else
                    cmd.CommandText = String.Format(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql),
                                                    SqlConcat, InnerJoin, sqlConcatIndividual, sqlConcatGrupo, sqlInnerSectorOrigenIndividual, sqlInnerSectorOrigenGrupo)
                End If

                'cmd.CommandText = "SELECT * FROM (" & vbNewLine & cmd.CommandText & vbNewLine & ") ORDER BY GMT_CREACION DESC"

                dtDocumentos = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, cmd, objPeticion.ParametrosPaginacion, objRespuesta.ParametrosPaginacion)

            End If

            Return dtDocumentos
        End Function
        Public Shared Function EsGeneracionF22(identificadorDocumento As String, identificadorFormulario As String) As Boolean

            Dim respuesta As Boolean = False

            Try

                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoEsGeneracionF22)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

                    Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Function
        Public Shared Function ObtenerDocumentosPendientes(objPeticion As Peticion(Of Clases.Transferencias.FiltroDocumentos),
                                         ByRef objRespuesta As Respuesta(Of List(Of Clases.Transferencias.DocumentoGrupoDocumento)), objDelegacion As Clases.Delegacion) As DataTable

            Dim dtDocumentos As New DataTable
            objRespuesta.ParametrosPaginacion = New Paginacion.ParametrosRespuestaPaginacion()

            If objPeticion IsNot Nothing AndAlso objPeticion.Parametro IsNot Nothing Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text

                Dim InnerJoin As String = ""
                Dim SqlConcat As New Text.StringBuilder()

                ' Cliente
                If objPeticion.Parametro.Clientes IsNot Nothing AndAlso objPeticion.Parametro.Clientes.Count > 0 Then

                    SqlConcat.AppendLine(" AND CUOR.OID_CLIENTE = []OID_CLIENTE ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.Clientes(0).Identificador))

                End If

                ' Sector
                If objPeticion.Parametro.Sectores IsNot Nothing AndAlso objPeticion.Parametro.Sectores.Count > 0 Then

                    SqlConcat.AppendLine(" AND (SECO.OID_SECTOR IN ('" + String.Join("','", objPeticion.Parametro.Sectores.Select(Function(a) a.Identificador)) + "') ")
                    SqlConcat.AppendLine(" OR SECD.OID_SECTOR IN ('" + String.Join("','", objPeticion.Parametro.Sectores.Select(Function(a) a.Identificador)) + "')) ")

                End If

                ' FYH_PLAN_CERTIFICACION
                If objPeticion.Parametro.FechaPlanCertificacionDesde <> New DateTime() AndAlso objPeticion.Parametro.FechaPlanCertificacionHasta <> New DateTime() Then
                    SqlConcat.AppendLine(" AND (")
                    SqlConcat.AppendLine(" (DOCU.FYH_PLAN_CERTIFICACION BETWEEN []FYH_PLAN_CERTIFICACION_DESDE AND []FYH_PLAN_CERTIFICACION_HASTA) ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION_DESDE", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaPlanCertificacionDesde.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion)))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION_HASTA", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaPlanCertificacionHasta.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion)))
                    If objPeticion.Parametro.BolIncluirDocSinFechaPlan Then
                        SqlConcat.AppendLine(" OR DOCU.FYH_PLAN_CERTIFICACION IS NULL ")
                    End If
                    SqlConcat.AppendLine(") ")
                ElseIf objPeticion.Parametro.FechaPlanCertificacionDesde <> New DateTime() OrElse objPeticion.Parametro.FechaPlanCertificacionHasta <> New DateTime() Then
                    SqlConcat.AppendLine(" AND (")
                    If objPeticion.Parametro.FechaPlanCertificacionDesde <> New DateTime() Then
                        SqlConcat.AppendLine(" DOCU.FYH_PLAN_CERTIFICACION >= []FYH_PLAN_CERTIFICACION_DESDE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION_DESDE", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaPlanCertificacionDesde.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion)))
                    End If
                    If objPeticion.Parametro.FechaPlanCertificacionHasta <> New DateTime() Then
                        SqlConcat.AppendLine(" DOCU.FYH_PLAN_CERTIFICACION <= []FYH_PLAN_CERTIFICACION_HASTA")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION_HASTA", ProsegurDbType.Data_Hora, objPeticion.Parametro.FechaPlanCertificacionHasta.QuieroGrabarGMTZeroEnLaBBDD(objDelegacion)))
                    End If
                    If objPeticion.Parametro.BolIncluirDocSinFechaPlan Then
                        SqlConcat.AppendLine(" OR DOCU.FYH_PLAN_CERTIFICACION IS NULL ")
                    End If
                    SqlConcat.AppendLine(") ")
                End If

                If Not objPeticion.Parametro.BolIncluirDocSinFechaPlan Then
                    SqlConcat.AppendLine(" AND DOCU.FYH_PLAN_CERTIFICACION IS NOT NULL ")
                End If

                If Not objPeticion.Parametro.BolIncluirDocNoCertificar Then
                    SqlConcat.AppendLine(" AND DOCU.BOL_NO_CERTIFICAR = 0 ")
                End If

                ' Tipo Documento
                If Not String.IsNullOrEmpty(objPeticion.Parametro.IdentificadorTipoDocumento) Then

                    SqlConcat.AppendLine(" AND DOCU.OID_TIPO_DOCUMENTO = []OID_TIPO_DOCUMENTO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Parametro.IdentificadorTipoDocumento))

                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerDocumentosPendientes, SqlConcat.ToString()))

                dtDocumentos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End If

            Return dtDocumentos
        End Function

        ''' <summary>
        ''' Recupera o identificador do documento Generacion F22 da remesa informada.
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarIdentificadorF22PorRemesa(identificadorRemesa As String) As String

            Dim identificadorDocumento As String = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoRecuperarIdentificadorF22PorRemesa)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                identificadorDocumento = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return identificadorDocumento

        End Function


#End Region

#Region "[INSERIR]"

        Public Shared Sub GuardarDocumentoContenedor(Documento As Clases.Documento,
                                                     ByRef CodigoComprobante As String, ByRef IdentificadorDocumento As String)

            Try

                Dim spw As SPWrapper = RellenarParametrosArmarContenedores(Documento)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                CodigoComprobante = If(spw.Param("par$cod_comprobante").Valor IsNot Nothing, spw.Param("par$cod_comprobante").Valor.ToString, String.Empty)
                IdentificadorDocumento = Documento.Identificador

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

        Private Shared Function RellenarParametrosArmarContenedores(ByRef Documento As Clases.Documento) As SPWrapper

            Dim SP As String = Constantes.SP_GUARDAR_DOCUMENTO_CONTENEDOR_ALTA
            Dim spw As New SPWrapper(SP, True)

            spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, Documento.Formulario.Identificador, , False)
            spw.AgregarParam("par$cod_formulario", ProsegurDbType.Identificador_Alfanumerico, Documento.Formulario.Codigo, , False)
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, String.Empty, , False)
            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, String.Empty, ParameterDirection.Output, False)

            spw.AgregarParam("par$adoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_documento_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_documento_sust", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_moviment_fondo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_fyh_plan_certif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adoc_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adoc_fyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adoc_collection_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoc_cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoc_cod_externo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoc_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'Conta Origem
            spw.AgregarParam("par$adoc_oid_cuenta_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_cliente_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_subcliente_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_puntoservico_ori", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_canal_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_subcanal_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_delegacion_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_planta_origen", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_sector_origen", ProsegurDbType.Objeto_Id, Nothing, , True)

            'Conta Destino
            spw.AgregarParam("par$adoc_oid_cuenta_destino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_cliente_destino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_subcliente_des", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_puntoservico_des", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_canal_destino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_subcanal_destino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_delegacion_des", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_planta_destino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_sector_destino", ProsegurDbType.Objeto_Id, Nothing, , True)

            'Valores Efetivo do documento
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'Valores medio pago do documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_unidad_medida", ProsegurDbType.Objeto_Id, Nothing, , True)

            'Terminos do meio de pagamento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Inteiro_Curto, Nothing, , True)

            Dim objContenedor As Clases.Contenedor = Nothing

            If Documento.Elemento IsNot Nothing Then
                objContenedor = TryCast(Documento.Elemento, Clases.Contenedor)
            End If

            'Contenedor
            spw.AgregarParam("par$adoccont_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoccont_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoccont_cod_tipo_cont", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoccont_cod_tipo_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoccont_cod_tipo_embalaje", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoccont_cod_puesto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoccont_cod_contenedor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adoccont_rowver_contenedor", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'Precintos do contenedor
            spw.AgregarParam("par$aprco_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aprco_precintos_contenedor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aprco_bol_precinto_autom", ProsegurDbType.Inteiro_Curto, Nothing, , True)

            'Elementos
            spw.AgregarParam("par$aelco_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aelco_tipo_elemento", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelco_precintos_ele", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aelco_ids_elemento", ProsegurDbType.Objeto_Id, Nothing, , True)


            'Valores Efetivo do contenedor
            spw.AgregarParam("par$aefco_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefco_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefco_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefco_oid_uni_med", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefco_cod_nivel_det", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefco_cod_tipo_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefco_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefco_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefco_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'Valores medio pago do contenedor
            spw.AgregarParam("par$ampco_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampco_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampco_oid_mp", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampco_cod_tipo_mp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampco_cod_nivel_det", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampco_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampco_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
            spw.AgregarParam("par$ampco_oid_unidad_medida", ProsegurDbType.Objeto_Id, Nothing, , True)

            'Terminos do meio de contenedor
            spw.AgregarParam("par$avtmpco_oid_contenedor", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpco_oid_mp", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpco_oid_termediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpco_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$avtmpco_nec_indicegrupo", ProsegurDbType.Inteiro_Curto, Nothing, , True)


            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, Documento.UsuarioCreacion.ToUpper, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                                             Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                                             Tradutor.CulturaSistema.Name,
                                                                                             If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Descricao_Longa, 1, , False)
            spw.AgregarParam("par$hacer_commit", ProsegurDbType.Inteiro_Longo, 1, , False)
            spw.AgregarParam("par$confirmar_doc", ProsegurDbType.Inteiro_Longo, 1, , False)

            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, String.Empty, ParameterDirection.Output, False)

            Dim IdentificadorDocumento As String = Guid.NewGuid.ToString

            Documento.Identificador = IdentificadorDocumento

            spw.Param("par$adoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
            spw.Param("par$adoc_oid_documento_padre").AgregarValorArray(Nothing)
            spw.Param("par$adoc_oid_documento_sust").AgregarValorArray(Nothing)
            spw.Param("par$adoc_oid_moviment_fondo").AgregarValorArray(Nothing)

            If Documento.FechaHoraPlanificacionCertificacion = Date.MinValue Then
                spw.Param("par$adoc_fyh_plan_certif").AgregarValorArray(DBNull.Value)
            Else
                spw.Param("par$adoc_fyh_plan_certif").AgregarValorArray(Documento.FechaHoraPlanificacionCertificacion)
            End If

            If Documento.FechaHoraGestion = Date.MinValue Then
                spw.Param("par$adoc_fyh_gestion").AgregarValorArray(DBNull.Value)
            Else
                spw.Param("par$adoc_fyh_gestion").AgregarValorArray(Documento.FechaHoraGestion)
            End If
            spw.Param("par$adoc_fyh_contable").AgregarValorArray(DBNull.Value)


            spw.Param("par$adoc_collection_id").AgregarValorArray(DBNull.Value)
            spw.Param("par$adoc_cod_actual_id").AgregarValorArray(DBNull.Value)


            spw.Param("par$adoc_cod_externo").AgregarValorArray(Documento.NumeroExterno)
            spw.Param("par$adoc_rowver").AgregarValorArray(Nothing)

            'Conta Origem
            spw.Param("par$adoc_oid_cuenta_origen").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing, Documento.CuentaOrigen.Identificador, String.Empty))
            spw.Param("par$adoc_oid_cliente_origen").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.Cliente IsNot Nothing,
                                                                    Documento.CuentaOrigen.Cliente.Identificador, String.Empty))
            spw.Param("par$adoc_oid_subcliente_origen").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.SubCliente IsNot Nothing,
                                                                       Documento.CuentaOrigen.SubCliente.Identificador, String.Empty))
            spw.Param("par$adoc_oid_puntoservico_ori").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.PuntoServicio IsNot Nothing,
                                                                         Documento.CuentaOrigen.PuntoServicio.Identificador, String.Empty))
            spw.Param("par$adoc_oid_canal_origen").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.Canal IsNot Nothing,
                                                                  Documento.CuentaOrigen.Canal.Identificador, String.Empty))
            spw.Param("par$adoc_oid_subcanal_origen").AgregarValorArray(If(Documento.CuentaOrigen IsNot Nothing AndAlso Documento.CuentaOrigen.SubCanal IsNot Nothing,
                                                                     Documento.CuentaOrigen.SubCanal.Identificador, String.Empty))
            spw.Param("par$adoc_oid_delegacion_origen").AgregarValorArray(If(Documento.SectorOrigen IsNot Nothing AndAlso Documento.SectorOrigen.Delegacion IsNot Nothing,
                                                                       Documento.SectorOrigen.Delegacion.Identificador, String.Empty))
            spw.Param("par$adoc_oid_planta_origen").AgregarValorArray(If(Documento.SectorOrigen IsNot Nothing AndAlso Documento.SectorOrigen.Planta IsNot Nothing,
                                                                   Documento.SectorOrigen.Planta.Identificador, String.Empty))
            spw.Param("par$adoc_oid_sector_origen").AgregarValorArray(If(Documento.SectorOrigen IsNot Nothing, Documento.SectorOrigen.Identificador, String.Empty))


            'Conta Destino
            spw.Param("par$adoc_oid_cuenta_destino").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing, Documento.CuentaDestino.Identificador, String.Empty))
            spw.Param("par$adoc_oid_cliente_destino").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.Cliente IsNot Nothing,
                                                                           Documento.CuentaDestino.Cliente.Identificador, String.Empty))
            spw.Param("par$adoc_oid_subcliente_des").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.SubCliente IsNot Nothing,
                                                                          Documento.CuentaDestino.SubCliente.Identificador, String.Empty))
            spw.Param("par$adoc_oid_puntoservico_des").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.PuntoServicio IsNot Nothing,
                                                                            Documento.CuentaDestino.PuntoServicio.Identificador, String.Empty))
            spw.Param("par$adoc_oid_canal_destino").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.Canal IsNot Nothing,
                                                                         Documento.CuentaDestino.Canal.Identificador, String.Empty))
            spw.Param("par$adoc_oid_subcanal_destino").AgregarValorArray(If(Documento.CuentaDestino IsNot Nothing AndAlso Documento.CuentaDestino.SubCanal IsNot Nothing,
                                                                            Documento.CuentaDestino.SubCanal.Identificador, String.Empty))
            spw.Param("par$adoc_oid_delegacion_des").AgregarValorArray(If(Documento.SectorDestino IsNot Nothing AndAlso Documento.SectorDestino.Delegacion IsNot Nothing,
                                                                          Documento.SectorDestino.Delegacion.Identificador, String.Empty))
            spw.Param("par$adoc_oid_planta_destino").AgregarValorArray(If(Documento.SectorDestino IsNot Nothing AndAlso Documento.SectorDestino.Planta IsNot Nothing,
                                                                          Documento.SectorDestino.Planta.Identificador, String.Empty))
            spw.Param("par$adoc_oid_sector_destino").AgregarValorArray(If(Documento.SectorDestino IsNot Nothing, Documento.SectorDestino.Identificador, String.Empty))

            If Documento.Divisas IsNot Nothing AndAlso Documento.Divisas.Count > 0 Then

                For Each Div In Documento.Divisas

                    If (Div.Denominaciones IsNot Nothing AndAlso Div.Denominaciones.Count > 0) OrElse
                     (Div.ValoresTotalesEfectivo IsNot Nothing AndAlso Div.ValoresTotalesEfectivo.Count > 0) Then


                        If Div.Denominaciones IsNot Nothing AndAlso Div.Denominaciones.Count > 0 Then

                            For Each den In Div.Denominaciones.FindAll(Function(deno) deno.ValorDenominacion IsNot Nothing AndAlso deno.ValorDenominacion.Count > 0)

                                'Valores Efetivo do documento
                                spw.Param("par$aefdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                                spw.Param("par$aefdoc_oid_divisa").AgregarValorArray(Div.Identificador)
                                spw.Param("par$aefdoc_oid_denominacion").AgregarValorArray(den.Identificador)
                                spw.Param("par$aefdoc_oid_unid_medida").AgregarValorArray(If(den.ValorDenominacion.First.UnidadMedida IsNot Nothing,
                                                                                             den.ValorDenominacion.First.UnidadMedida.Identificador, String.Empty))
                                spw.Param("par$aefdoc_cod_niv_detalle").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor)
                                spw.Param("par$aefdoc_cod_tp_efec_tot").AgregarValorArray(Nothing)
                                spw.Param("par$aefdoc_oid_calidad").AgregarValorArray(If(den.ValorDenominacion.First.Calidad IsNot Nothing,
                                                                                             den.ValorDenominacion.First.Calidad.Identificador, String.Empty))
                                spw.Param("par$aefdoc_num_importe").AgregarValorArray(den.ValorDenominacion.First.Importe)
                                spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(den.ValorDenominacion.First.Cantidad)

                            Next

                        End If

                        If Div.ValoresTotalesEfectivo IsNot Nothing AndAlso Div.ValoresTotalesEfectivo.Count > 0 Then

                            For Each vte In Div.ValoresTotalesEfectivo

                                'Valores Efetivo do documento
                                spw.Param("par$aefdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                                spw.Param("par$aefdoc_oid_divisa").AgregarValorArray(Div.Identificador)
                                spw.Param("par$aefdoc_oid_denominacion").AgregarValorArray(Nothing)
                                spw.Param("par$aefdoc_oid_unid_medida").AgregarValorArray(Nothing)
                                spw.Param("par$aefdoc_cod_niv_detalle").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Total.RecuperarValor)
                                spw.Param("par$aefdoc_cod_tp_efec_tot").AgregarValorArray(Nothing)
                                spw.Param("par$aefdoc_oid_calidad").AgregarValorArray(Nothing)
                                spw.Param("par$aefdoc_num_importe").AgregarValorArray(vte.Importe)
                                spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(Nothing)

                            Next

                        End If

                    End If

                    If (Div.MediosPago IsNot Nothing AndAlso Div.MediosPago.Count > 0) OrElse
                        (Div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso Div.ValoresTotalesTipoMedioPago.Count > 0) Then

                        If Div.MediosPago IsNot Nothing AndAlso Div.MediosPago.Count > 0 Then

                            For Each mp In Div.MediosPago.FindAll(Function(mp1) mp1.Valores IsNot Nothing AndAlso mp1.Valores.Count > 0)

                                'Valores medio pago do documento
                                spw.Param("par$ampdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                                spw.Param("par$ampdoc_oid_divisa").AgregarValorArray(Div.Identificador)
                                spw.Param("par$ampdoc_oid_medio_pago").AgregarValorArray(mp.Identificador)
                                spw.Param("par$ampdoc_cod_tipo_med_pago").AgregarValorArray(mp.Tipo.RecuperarValor)
                                spw.Param("par$ampdoc_cod_nivel_detalle").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor)
                                spw.Param("par$ampdoc_num_importe").AgregarValorArray(mp.Valores.First.Importe)
                                spw.Param("par$ampdoc_nel_cantidad").AgregarValorArray(mp.Valores.First.Cantidad)
                                spw.Param("par$ampdoc_oid_unidad_medida").AgregarValorArray(If(mp.Valores.First.UnidadMedida IsNot Nothing,
                                                                                               mp.Valores.First.UnidadMedida.Identificador,
                                                                                               Nothing))

                                If mp.Terminos IsNot Nothing AndAlso mp.Terminos.Count > 0 Then

                                    For Each ter In mp.Terminos

                                        'Terminos do meio de pagamento
                                        spw.Param("par$avtmpdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                                        spw.Param("par$avtmpdoc_oid_mediopago").AgregarValorArray(mp.Identificador)
                                        spw.Param("par$avtmpdoc_oid_t_mediopago").AgregarValorArray(ter.Identificador)
                                        spw.Param("par$avtmpdoc_des_valor").AgregarValorArray(ter.Valor)
                                        spw.Param("par$avtmpdoc_nec_indice_grp").AgregarValorArray(Nothing)

                                    Next

                                End If

                            Next

                        End If

                        If Div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso Div.ValoresTotalesTipoMedioPago.Count > 0 Then

                            For Each vtmp In Div.ValoresTotalesTipoMedioPago

                                'Valores medio pago do documento
                                spw.Param("par$ampdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                                spw.Param("par$ampdoc_oid_divisa").AgregarValorArray(Div.Identificador)
                                spw.Param("par$ampdoc_oid_medio_pago").AgregarValorArray(Nothing)
                                spw.Param("par$ampdoc_cod_tipo_med_pago").AgregarValorArray(vtmp.TipoMedioPago.RecuperarValor)
                                spw.Param("par$ampdoc_cod_nivel_detalle").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Total.RecuperarValor)
                                spw.Param("par$ampdoc_num_importe").AgregarValorArray(vtmp.Importe)
                                spw.Param("par$ampdoc_nel_cantidad").AgregarValorArray(vtmp.Cantidad)
                                spw.Param("par$ampdoc_oid_unidad_medida").AgregarValorArray(Nothing)

                            Next

                            'Terminos do meio de pagamento
                            spw.Param("par$avtmpdoc_oid_documento").AgregarValorArray(IdentificadorDocumento)
                            spw.Param("par$avtmpdoc_oid_mediopago").AgregarValorArray(Nothing)
                            spw.Param("par$avtmpdoc_oid_t_mediopago").AgregarValorArray(Nothing)
                            spw.Param("par$avtmpdoc_des_valor").AgregarValorArray(Nothing)
                            spw.Param("par$avtmpdoc_nec_indice_grp").AgregarValorArray(Nothing)

                        End If

                    End If

                Next

            End If

            If objContenedor IsNot Nothing Then

                Dim IdentificadorContenedor As String = Guid.NewGuid.ToString

                'Contenedor
                spw.Param("par$adoccont_oid_documento").AgregarValorArray(IdentificadorDocumento)
                spw.Param("par$adoccont_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                spw.Param("par$adoccont_cod_tipo_cont").AgregarValorArray(If(objContenedor.TipoContenedor IsNot Nothing, objContenedor.TipoContenedor.Codigo, String.Empty))
                spw.Param("par$adoccont_cod_tipo_servicio").AgregarValorArray(If(objContenedor.TipoServicio IsNot Nothing, objContenedor.TipoServicio.Codigo, String.Empty))
                spw.Param("par$adoccont_cod_tipo_embalaje").AgregarValorArray(If(objContenedor.TipoFormato IsNot Nothing, objContenedor.TipoFormato.Codigo, String.Empty))
                spw.Param("par$adoccont_cod_puesto").AgregarValorArray(objContenedor.PuestoResponsable)
                spw.Param("par$adoccont_cod_contenedor").AgregarValorArray(objContenedor.Codigo)
                spw.Param("par$adoccont_rowver_contenedor").AgregarValorArray(Nothing)


                If objContenedor.Precintos IsNot Nothing AndAlso objContenedor.Precintos.Count > 0 Then

                    For Each Precintos In objContenedor.Precintos
                        spw.Param("par$aprco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                        spw.Param("par$aprco_precintos_contenedor").AgregarValorArray(Precintos)
                        spw.Param("par$aprco_bol_precinto_autom").AgregarValorArray(If(Precintos = objContenedor.PrecintoAutomatico, 1, 0))
                    Next

                End If

                If objContenedor.Elementos IsNot Nothing AndAlso objContenedor.Elementos.Count > 0 Then

                    For Each Elemento In objContenedor.Elementos

                        spw.Param("par$aelco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)

                        If TypeOf Elemento Is Clases.Remesa Then
                            spw.Param("par$aelco_tipo_elemento").AgregarValorArray(Enumeradores.TipoElemento.Remesa.RecuperarValor)
                        ElseIf TypeOf Elemento Is Clases.Bulto Then
                            spw.Param("par$aelco_tipo_elemento").AgregarValorArray(Enumeradores.TipoElemento.Bulto.RecuperarValor)
                        ElseIf TypeOf Elemento Is Clases.Parcial Then
                            spw.Param("par$aelco_tipo_elemento").AgregarValorArray(Enumeradores.TipoElemento.Parcial.RecuperarValor)
                        ElseIf TypeOf Elemento Is Clases.Contenedor Then
                            spw.Param("par$aelco_tipo_elemento").AgregarValorArray(Enumeradores.TipoElemento.Contenedor.RecuperarValor)
                        End If

                        spw.Param("par$aelco_ids_elemento").AgregarValorArray(Elemento.Identificador)

                        If Elemento.Precintos IsNot Nothing AndAlso Elemento.Precintos.Count > 0 Then
                            spw.Param("par$aelco_precintos_ele").AgregarValorArray(Elemento.Precintos.First)
                        Else
                            spw.Param("par$aelco_precintos_ele").AgregarValorArray(Nothing)
                        End If

                    Next

                End If

                If objContenedor.Divisas IsNot Nothing AndAlso objContenedor.Divisas.Count > 0 Then

                    For Each Div In Documento.Divisas

                        If (Div.Denominaciones IsNot Nothing AndAlso Div.Denominaciones.Count > 0) OrElse
                           (Div.ValoresTotalesEfectivo IsNot Nothing AndAlso Div.ValoresTotalesEfectivo.Count > 0) Then


                            If Div.Denominaciones IsNot Nothing AndAlso Div.Denominaciones.Count > 0 Then

                                For Each den In Div.Denominaciones.FindAll(Function(deno) deno.ValorDenominacion IsNot Nothing AndAlso deno.ValorDenominacion.Count > 0)

                                    'Valores Efetivo do documento
                                    spw.Param("par$aefco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                                    spw.Param("par$aefco_oid_divisa").AgregarValorArray(Div.Identificador)
                                    spw.Param("par$aefco_oid_denom").AgregarValorArray(den.Identificador)
                                    spw.Param("par$aefco_oid_uni_med").AgregarValorArray(If(den.ValorDenominacion.First.UnidadMedida IsNot Nothing,
                                                                                                 den.ValorDenominacion.First.UnidadMedida.Identificador, String.Empty))
                                    spw.Param("par$aefco_cod_nivel_det").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor)
                                    spw.Param("par$aefco_cod_tipo_efec_tot").AgregarValorArray(Nothing)
                                    spw.Param("par$aefco_oid_calidad").AgregarValorArray(If(den.ValorDenominacion.First.Calidad IsNot Nothing,
                                                                                                 den.ValorDenominacion.First.Calidad.Identificador, String.Empty))
                                    spw.Param("par$aefco_num_importe").AgregarValorArray(den.ValorDenominacion.First.Importe)
                                    spw.Param("par$aefco_nel_cantidad").AgregarValorArray(den.ValorDenominacion.First.Cantidad)

                                Next

                            End If

                            If Div.ValoresTotalesEfectivo IsNot Nothing AndAlso Div.ValoresTotalesEfectivo.Count > 0 Then

                                For Each vte In Div.ValoresTotalesEfectivo

                                    'Valores Efetivo do documento
                                    spw.Param("par$aefco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                                    spw.Param("par$aefco_oid_divisa").AgregarValorArray(Div.Identificador)
                                    spw.Param("par$aefco_oid_denom").AgregarValorArray(Nothing)
                                    spw.Param("par$aefco_oid_uni_med").AgregarValorArray(Nothing)
                                    spw.Param("par$aefco_cod_nivel_det").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Total.RecuperarValor)
                                    spw.Param("par$aefco_cod_tipo_efec_tot").AgregarValorArray(Nothing)
                                    spw.Param("par$aefco_oid_calidad").AgregarValorArray(Nothing)
                                    spw.Param("par$aefco_num_importe").AgregarValorArray(vte.Importe)
                                    spw.Param("par$aefco_nel_cantidad").AgregarValorArray(Nothing)

                                Next

                            End If

                        End If

                        If (Div.MediosPago IsNot Nothing AndAlso Div.MediosPago.Count > 0) OrElse
                            (Div.ValoresTotalesTipoMedioPago IsNot Nothing OrElse Div.ValoresTotalesTipoMedioPago.Count > 0) Then

                            If Div.MediosPago IsNot Nothing AndAlso Div.MediosPago.Count > 0 Then

                                For Each mp In Div.MediosPago.FindAll(Function(mp1) mp1.Valores IsNot Nothing AndAlso mp1.Valores.Count > 0)

                                    'Valores medio pago do documento
                                    spw.Param("par$ampco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                                    spw.Param("par$ampco_oid_divisa").AgregarValorArray(Div.Identificador)
                                    spw.Param("par$ampco_oid_mp").AgregarValorArray(mp.Identificador)
                                    spw.Param("par$ampco_cod_tipo_mp").AgregarValorArray(mp.Tipo.RecuperarValor)
                                    spw.Param("par$ampco_cod_nivel_det").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor)
                                    spw.Param("par$ampco_num_importe").AgregarValorArray(mp.Valores.First.Importe)
                                    spw.Param("par$ampco_nel_cantidad").AgregarValorArray(mp.Valores.First.Cantidad)
                                    spw.Param("par$ampco_oid_unidad_medida").AgregarValorArray(If(mp.Valores.First.UnidadMedida IsNot Nothing,
                                                                                                   mp.Valores.First.UnidadMedida.Identificador,
                                                                                                   Nothing))
                                    If mp.Terminos IsNot Nothing AndAlso mp.Terminos.Count > 0 Then

                                        For Each ter In mp.Terminos

                                            'Terminos do meio de pagamento
                                            spw.Param("par$avtmpco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                                            spw.Param("par$avtmpco_oid_mp").AgregarValorArray(mp.Identificador)
                                            spw.Param("par$avtmpco_oid_termediopago").AgregarValorArray(ter.Identificador)
                                            spw.Param("par$avtmpco_des_valor").AgregarValorArray(ter.Valor)
                                            spw.Param("par$avtmpco_nec_indicegrupo").AgregarValorArray(Nothing)

                                        Next

                                    End If

                                Next

                            End If

                            If Div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso Div.ValoresTotalesTipoMedioPago.Count > 0 Then

                                For Each vtmp In Div.ValoresTotalesTipoMedioPago

                                    'Valores medio pago do documento
                                    spw.Param("par$ampco_oid_contenedor").AgregarValorArray(IdentificadorContenedor)
                                    spw.Param("par$ampco_oid_divisa").AgregarValorArray(Div.Identificador)
                                    spw.Param("par$ampco_oid_mp").AgregarValorArray(Nothing)
                                    spw.Param("par$ampco_cod_tipo_mp").AgregarValorArray(vtmp.TipoMedioPago.RecuperarValor)
                                    spw.Param("par$ampco_cod_nivel_det").AgregarValorArray(Enumeradores.TipoNivelDetalhe.Total.RecuperarValor)
                                    spw.Param("par$ampco_num_importe").AgregarValorArray(vtmp.Importe)
                                    spw.Param("par$ampco_nel_cantidad").AgregarValorArray(vtmp.Cantidad)
                                    spw.Param("par$ampco_oid_unidad_medida").AgregarValorArray(Nothing)

                                Next

                            End If

                        End If

                    Next

                End If

            End If


            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function













        'Public Shared Sub GrabarDocumentoProc(objDocumento As Clases.Documento)
        '    Try
        '        Dim spw As SPWrapper = ColectarGrabarDocumentoProc(objDocumento)
        '        AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
        '    Catch ex As Exception
        '        If ex.Message.StartsWith("036_HayCierreCajaEnCurso") Then
        '            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_HAYCIERRECAJA, _
        '                             Tradutor.Traduzir("036_HayCierreCajaEnCurso"))
        '        Else
        '            Throw ex
        '        End If

        '    End Try
        'End Sub

        'Private Shared Function ColectarGrabarDocumentoProc(objDocumento As Clases.Documento) As SPWrapper
        '    Dim NivelDetalle As String

        '    'stored procedure
        '    Dim SP As String = String.Format("sapr_pdocumento_{0}.sguardar_doc_elemento_valores", Util.Versao) '"sapr_pdocumentos_grp.sguardar_grp_docs_valores" '
        '    Dim spw As New SPWrapper(SP, True)

        '    'grupo de documentos
        '    spw.AgregarParam("par$oid_documento", ProsegurDbType.Objeto_Id, objDocumento.Identificador, , False)
        '    spw.AgregarParam("par$oid_formulario", ProsegurDbType.Objeto_Id, objDocumento.Formulario.Identificador, , False)
        '    If objDocumento.DocumentoPadre IsNot Nothing Then
        '        spw.AgregarParam("par$oid_documento_padre", ProsegurDbType.Objeto_Id, objDocumento.DocumentoPadre.Identificador, , False)
        '    Else
        '        spw.AgregarParam("par$oid_documento_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '    End If

        '    spw.AgregarParam("par$oid_documento_sustituto", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorSustituto, , False)
        '    spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorGrupo, , False)
        '    spw.AgregarParam("par$oid_movimentacion_fondo", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorMovimentacionFondo, , False)
        '    spw.AgregarParam("par$fyh_plan_certificacion", ProsegurDbType.Data_Hora, objDocumento.FechaHoraPlanificacionCertificacion, , False)
        '    spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, objDocumento.FechaHoraGestion, , False)
        '    spw.AgregarParam("par$cod_externo", ProsegurDbType.Descricao_Curta, objDocumento.NumeroExterno, , False)
        '    spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, objDocumento.UsuarioModificacion, , False)
        '    spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, objDocumento.Rowver, , False)

        '    'cuenta origen
        '    spw.AgregarParam("par$oid_cuenta_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Identificador, , False)
        '    spw.AgregarParam("par$oid_cliente_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Cliente.Identificador, , False)
        '    If objDocumento.CuentaOrigen.SubCliente IsNot Nothing Then
        '        spw.AgregarParam("par$oid_subcliente_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.SubCliente.Identificador, , False)
        '    Else
        '        spw.AgregarParam("par$oid_subcliente_origen", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '    End If
        '    If objDocumento.CuentaOrigen.PuntoServicio IsNot Nothing Then
        '        spw.AgregarParam("par$oid_puntoservico_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.PuntoServicio.Identificador, , False)
        '    Else
        '        spw.AgregarParam("par$oid_puntoservico_origen", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '    End If
        '    spw.AgregarParam("par$oid_canal_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Canal.Identificador, , False)
        '    spw.AgregarParam("par$oid_subcanal_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.SubCanal.Identificador, , False)
        '    spw.AgregarParam("par$oid_delegacion_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Sector.Delegacion.Identificador, , False)
        '    spw.AgregarParam("par$oid_planta_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Sector.Planta.Identificador, , False)
        '    spw.AgregarParam("par$oid_sector_origen", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Sector.Identificador, , False)

        '    'cuenta destino
        '    spw.AgregarParam("par$oid_cuenta_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Identificador, , False)
        '    spw.AgregarParam("par$oid_cliente_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Cliente.Identificador, , False)
        '    If objDocumento.CuentaDestino.SubCliente IsNot Nothing Then
        '        spw.AgregarParam("par$oid_subcliente_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.SubCliente.Identificador, , False)
        '    Else
        '        spw.AgregarParam("par$oid_subcliente_destino", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '    End If
        '    If objDocumento.CuentaDestino.PuntoServicio IsNot Nothing Then
        '        spw.AgregarParam("par$oid_puntoservico_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.PuntoServicio.Identificador, , False)
        '    Else
        '        spw.AgregarParam("par$oid_puntoservico_destino", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '    End If
        '    spw.AgregarParam("par$oid_canal_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Canal.Identificador, , False)
        '    spw.AgregarParam("par$oid_subcanal_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.SubCanal.Identificador, , False)
        '    spw.AgregarParam("par$oid_delegacion_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Sector.Delegacion.Identificador, , False)
        '    spw.AgregarParam("par$oid_planta_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Sector.Planta.Identificador, , False)
        '    spw.AgregarParam("par$oid_sector_destino", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Sector.Identificador, , False)

        '    'efectivos x documentos
        '    spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

        '    'medios de pago x documento
        '    spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

        '    'terminos x medio de pago x documento
        '    spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Inteiro_Longo, Nothing, , True)

        '    'terminos x documentos
        '    spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

        '    'Remesa
        '    Dim objRemesa As Clases.Remesa = DirectCast(objDocumento.Elemento, Clases.Remesa)
        '    If objRemesa IsNot Nothing Then
        '        spw.AgregarParam("par$remdoc_oid_remesa", ProsegurDbType.Objeto_Id, objRemesa.Identificador, , False)
        '        If objRemesa.RemesaOrigen IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_remesa_origen", ProsegurDbType.Objeto_Id, objRemesa.RemesaOrigen.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_remesa_origen", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If
        '        spw.AgregarParam("par$remdoc_oid_externo", ProsegurDbType.Objeto_Id, objRemesa.CodigoExterno, , False)

        '        If objRemesa.GrupoTerminosIAC IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_iac", ProsegurDbType.Objeto_Id, objRemesa.GrupoTerminosIAC.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_iac", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        If objRemesa.Cuenta IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_cuenta", ProsegurDbType.Objeto_Id, objRemesa.Cuenta.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_cuenta", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        spw.AgregarParam("par$remdoc_cod_recibo_salida", ProsegurDbType.Descricao_Curta, objRemesa.CodigoReciboSalida, , False)
        '        spw.AgregarParam("par$remdoc_usuario_resp", ProsegurDbType.Descricao_Curta, objRemesa.UsuarioResponsable, , False)
        '        spw.AgregarParam("par$remdoc_puesto_resp", ProsegurDbType.Descricao_Curta, objRemesa.PuestoResponsable, , False)
        '        spw.AgregarParam("par$remdoc_cod_ruta", ProsegurDbType.Descricao_Curta, objRemesa.Ruta, , False)
        '        spw.AgregarParam("par$remdoc_nel_parada", ProsegurDbType.Inteiro_Longo, objRemesa.Parada, , False)
        '        If objRemesa.FechaHoraTransporte <> DateTime.MinValue Then
        '            spw.AgregarParam("par$remdoc_fyh_transporte", ProsegurDbType.Data_Hora, objRemesa.FechaHoraTransporte, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_fyh_transporte", ProsegurDbType.Data_Hora, DBNull.Value, , False)
        '        End If
        '        If objRemesa.Bultos IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_nel_cant_bultos", ProsegurDbType.Inteiro_Longo, objRemesa.Bultos.Count, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_nel_cant_bultos", ProsegurDbType.Inteiro_Longo, DBNull.Value, , False)
        '        End If
        '        If objRemesa.FechaHoraInicioConteo <> DateTime.MinValue Then
        '            spw.AgregarParam("par$remdoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, objRemesa.FechaHoraInicioConteo, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, DBNull.Value, , False)
        '        End If
        '        If objRemesa.FechaHoraFinConteo <> DateTime.MinValue Then
        '            spw.AgregarParam("par$remdoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, objRemesa.FechaHoraFinConteo, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, DBNull.Value, , False)
        '        End If

        '        If objRemesa.ElementoPadre IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_remesa_padre", ProsegurDbType.Objeto_Id, objRemesa.ElementoPadre.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_remesa_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        If objRemesa.ElementoSustituto IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_remesa_sub", ProsegurDbType.Objeto_Id, objRemesa.ElementoSustituto.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_remesa_sub", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        If objRemesa.DatosATM IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_cod_cajero", ProsegurDbType.Descricao_Curta, objRemesa.DatosATM.CodigoCajero, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_cod_cajero", ProsegurDbType.Descricao_Curta, DBNull.Value, , False)
        '        End If

        '        spw.AgregarParam("par$remdoc_cod_nivel_detalle", ProsegurDbType.Descricao_Curta, objRemesa.ConfiguracionNivelSaldos.RecuperarValor, , False)
        '        spw.AgregarParam("par$remdoc_cod_externo", ProsegurDbType.Descricao_Curta, objRemesa.CodigoExterno, , False)
        '        spw.AgregarParam("par$remdoc_cod_estado_abono", ProsegurDbType.Descricao_Curta, Enumeradores.EstadoAbonoElemento.NoAbonado.RecuperarValor(), , False)

        '        If objRemesa.CuentaSaldo IsNot Nothing Then
        '            spw.AgregarParam("par$remdoc_oid_cuenta_saldo", ProsegurDbType.Objeto_Id, objRemesa.CuentaSaldo.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_cuenta_saldo", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        If objRemesa.Cuenta IsNot Nothing AndAlso objRemesa.Cuenta.PuntoServicio IsNot Nothing _
        '            AndAlso Not String.IsNullOrEmpty(objRemesa.Cuenta.PuntoServicio.Identificador) Then
        '            spw.AgregarParam("par$remdoc_oid_pto_servicio", ProsegurDbType.Objeto_Id, objRemesa.Cuenta.PuntoServicio.Identificador, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_oid_pto_servicio", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
        '        End If

        '        If objRemesa.Cuenta IsNot Nothing AndAlso objRemesa.Cuenta.Sector IsNot Nothing AndAlso _
        '            objRemesa.Cuenta.Sector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(objRemesa.Cuenta.Sector.Delegacion.Codigo) Then
        '            spw.AgregarParam("par$remdoc_cod_delegacion", ProsegurDbType.Descricao_Curta, objRemesa.Cuenta.Sector.Delegacion.Codigo, , False)
        '        Else
        '            spw.AgregarParam("par$remdoc_cod_delegacion", ProsegurDbType.Descricao_Curta, DBNull.Value, , False)
        '        End If
        '        spw.AgregarParam("par$remdoc_estado", ProsegurDbType.Descricao_Curta, objRemesa.Estado.RecuperarValor(), , False)

        '    End If

        '    ' Terminos IAC Remesa
        '    spw.AgregarParam("par$avtrem_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtrem_cod_termino", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$avtrem_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$avtrem_obligatorio", ProsegurDbType.Logico, Nothing, , True)

        '    'Bulto
        '    spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_iac_parciales", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_cuenta", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_precinto", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_bolsa", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_banco_ingreso", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_bol_banco_ing_mad", ProsegurDbType.Logico, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_usuario_resp", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_puesto_resp", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_nel_cant_parciales", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_fyh_conteo_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_fyh_conteo_fin", ProsegurDbType.Data_Hora, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_fyh_proceso_leg", ProsegurDbType.Data_Hora, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_bulto_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_bulto_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_nivel_detalle", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_bol_cuadrado", ProsegurDbType.Logico, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_cuenta_saldo", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_oid_tipo_servicio", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_tipo_servicio", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_cliente", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_subcliente", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_pto_servicio", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_subcanal", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_cod_delegacion", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$abuldoc_estado", ProsegurDbType.Descricao_Curta, Nothing, , True)

        '    ' Terminos IAC Bulto
        '    spw.AgregarParam("par$avtbul_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtbul_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtbul_cod_termino", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$avtbul_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$avtbul_obligatorio", ProsegurDbType.Logico, Nothing, , True)

        '    'Parcial
        '    spw.AgregarParam("par$apardoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_externo", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_cod_precinto", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_iac", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_usuario_resp", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_puesto_resp", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_nec_secuencia", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_parcial_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_parcial_sub", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_oid_tipo_formato", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$apardoc_estado", ProsegurDbType.Descricao_Curta, Nothing, , True)

        '    ' Terminos IAC Parcial
        '    spw.AgregarParam("par$avtpar_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtpar_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtpar_cod_termino", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$avtpar_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$avtpar_obligatorio", ProsegurDbType.Logico, Nothing, , True)

        '    'Declarado Efectivo
        '    spw.AgregarParam("par$aefelem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_bol_es_billete", ProsegurDbType.Logico, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_cod_tipo_efec", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_cod_nv_detalle", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$aefelem_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)

        '    'Declarado Medio Pago
        '    spw.AgregarParam("par$adecmp_elem_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_cod_tipo_mp", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_cod_nv_detalle", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$adecmp_elem_bol_ingresado", ProsegurDbType.Logico, Nothing, , True)

        '    'Termino Medio Pago
        '    spw.AgregarParam("par$avtmpelem_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtmpelem_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$avtmpelem_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
        '    spw.AgregarParam("par$avtmpelem_nec_ind_grupo", ProsegurDbType.Inteiro_Longo, Nothing, , True)

        '    'Contado Efectivo
        '    spw.AgregarParam("par$adecef_elem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_cod_tipocont", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$adecef_elem_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)

        '    'Contado Medio Pago
        '    spw.AgregarParam("par$acontmp_elem_oid_valor", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_cod_tipocont", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_cod_nv_det", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$acontmp_elem_nel_secuencia", ProsegurDbType.Inteiro_Longo, Nothing, , True)

        '    ' Diferencia efectivo
        '    spw.AgregarParam("par$adifef_elem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_oid_denom", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_oid_unid_med", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_cod_tipo_efec", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$adifef_elem_cod_nv_detalle", ProsegurDbType.Descricao_Curta, Nothing, , True)

        '    ' Diferencia medio pago
        '    spw.AgregarParam("par$adifmp_elem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_oid_parcial", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_oid_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_cod_tipo_mp", ProsegurDbType.Descricao_Curta, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_nel_cantidad", ProsegurDbType.Inteiro_Longo, Nothing, , True)
        '    spw.AgregarParam("par$adifmp_elem_cod_nv_det", ProsegurDbType.Descricao_Curta, Nothing, , True)

        '    'Cultura para dicionario 
        '    spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
        '                                                            Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
        '                                                            Tradutor.CulturaSistema.Name,
        '                                                            If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

        '    'informacion de ejecución
        '    spw.AgregarParamInfo("par$info_ejecucion")
        '    spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

        '    ' Documento
        '    'valores del documento
        '    If objDocumento.Divisas IsNot Nothing Then
        '        For Each div As Clases.Divisa In objDocumento.Divisas

        '            'efectivo
        '            If div.Denominaciones IsNot Nothing Then
        '                NivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor
        '                For Each den As Clases.Denominacion In div.Denominaciones
        '                    For Each vld As Clases.ValorDenominacion In den.ValorDenominacion
        '                        If vld.TipoValor = Enumeradores.TipoValor.NoDefinido Then
        '                            spw.Param("par$aefdoc_oid_documento").AgregarValorArray(objDocumento.Identificador)
        '                            spw.Param("par$aefdoc_oid_divisa").AgregarValorArray(div.Identificador)
        '                            spw.Param("par$aefdoc_oid_denominacion").AgregarValorArray(den.Identificador)
        '                            If vld.UnidadMedida IsNot Nothing Then
        '                                spw.Param("par$aefdoc_oid_unid_medida").AgregarValorArray(vld.UnidadMedida.Identificador)
        '                            Else
        '                                spw.Param("par$aefdoc_oid_unid_medida").AgregarValorArray(String.Empty) 'aca hay que ver en el SP como obtener el default del padron segun billete o moneda
        '                            End If
        '                            spw.Param("par$aefdoc_cod_niv_detalle").AgregarValorArray(NivelDetalle)
        '                            spw.Param("par$aefdoc_cod_tp_efec_tot").AgregarValorArray(String.Empty)
        '                            If vld.Calidad IsNot Nothing Then
        '                                spw.Param("par$aefdoc_oid_calidad").AgregarValorArray(vld.Calidad.Identificador)
        '                            Else
        '                                spw.Param("par$aefdoc_oid_calidad").AgregarValorArray(String.Empty)
        '                            End If
        '                            spw.Param("par$aefdoc_num_importe").AgregarValorArray(vld.Importe)
        '                            spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(vld.Cantidad)

        '                        End If
        '                    Next    'valor de denominacion
        '                Next    'denominaciones
        '            End If

        '            'totales efectivo
        '            If div.ValoresTotalesEfectivo IsNot Nothing Then
        '                NivelDetalle = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                For Each vtd As Clases.ValorEfectivo In div.ValoresTotalesEfectivo
        '                    If vtd.TipoValor = Enumeradores.TipoValor.NoDefinido Then
        '                        spw.Param("par$aefdoc_oid_documento").AgregarValorArray(objDocumento.Identificador)
        '                        spw.Param("par$aefdoc_oid_divisa").AgregarValorArray(div.Identificador)
        '                        spw.Param("par$aefdoc_oid_denominacion").AgregarValorArray(String.Empty)
        '                        spw.Param("par$aefdoc_oid_unid_medida").AgregarValorArray(String.Empty)
        '                        spw.Param("par$aefdoc_cod_niv_detalle").AgregarValorArray(NivelDetalle)
        '                        spw.Param("par$aefdoc_cod_tp_efec_tot").AgregarValorArray(vtd.TipoDetalleEfectivo.RecuperarValor())
        '                        spw.Param("par$aefdoc_oid_calidad").AgregarValorArray(String.Empty)
        '                        spw.Param("par$aefdoc_num_importe").AgregarValorArray(vtd.Importe)
        '                        spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(0)
        '                    End If
        '                Next
        '            End If

        '            'medios de pago
        '            If div.MediosPago IsNot Nothing Then
        '                NivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
        '                For Each mp As Clases.MedioPago In div.MediosPago
        '                    For Each vl As Clases.ValorMedioPago In mp.Valores
        '                        If vl.TipoValor = Enumeradores.TipoValor.NoDefinido Then
        '                            Dim oidMP As String = Util.NewGUID()
        '                            spw.Param("par$ampdoc_oid_documento").AgregarValorArray(objDocumento.Identificador)
        '                            spw.Param("par$ampdoc_oid_divisa").AgregarValorArray(div.Identificador)
        '                            spw.Param("par$ampdoc_oid_medio_pago").AgregarValorArray(mp.Identificador)
        '                            spw.Param("par$ampdoc_cod_tipo_med_pago").AgregarValorArray(mp.Tipo.RecuperarValor())
        '                            spw.Param("par$ampdoc_cod_nivel_detalle").AgregarValorArray(NivelDetalle)
        '                            spw.Param("par$ampdoc_num_importe").AgregarValorArray(vl.Importe)
        '                            spw.Param("par$ampdoc_nel_cantidad").AgregarValorArray(vl.Cantidad)

        '                            'terminos del medio de pago
        '                            If mp.Terminos IsNot Nothing Then
        '                                For Each t As Clases.Termino In mp.Terminos
        '                                    If Not String.IsNullOrEmpty(t.Valor) Then
        '                                        spw.Param("par$avtmpdoc_oid_documento").AgregarValorArray(oidMP)
        '                                        spw.Param("par$avtmpdoc_oid_t_mediopago").AgregarValorArray(t.Identificador)
        '                                        spw.Param("par$avtmpdoc_des_valor").AgregarValorArray(t.Valor)
        '                                        spw.Param("par$avtmpdoc_nec_indice_grp").AgregarValorArray(t.NecIndiceGrupo)
        '                                    End If
        '                                Next
        '                            End If
        '                        End If
        '                    Next    'valor de medio de pago

        '                Next    'medio de pago

        '            End If

        '            'totales medios de pago
        '            If div.ValoresTotalesTipoMedioPago IsNot Nothing Then
        '                NivelDetalle = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                For Each vtmp As Clases.ValorTipoMedioPago In div.ValoresTotalesTipoMedioPago
        '                    spw.Param("par$ampdoc_oid_documento").AgregarValorArray(objDocumento.Identificador)
        '                    spw.Param("par$ampdoc_oid_divisa").AgregarValorArray(div.Identificador)
        '                    spw.Param("par$ampdoc_oid_medio_pago").AgregarValorArray(String.Empty)
        '                    spw.Param("par$ampdoc_cod_tipo_med_pago").AgregarValorArray(vtmp.TipoMedioPago.RecuperarValor())
        '                    spw.Param("par$ampdoc_cod_nivel_detalle").AgregarValorArray(NivelDetalle)
        '                    spw.Param("par$ampdoc_num_importe").AgregarValorArray(vtmp.Importe)
        '                    spw.Param("par$ampdoc_nel_cantidad").AgregarValorArray(vtmp.Cantidad)
        '                Next
        '            End If

        '        Next 'divisa

        '    End If

        '    'terminos del documento
        '    If objDocumento.GrupoTerminosIAC IsNot Nothing Then
        '        For Each trm As Clases.TerminoIAC In objDocumento.GrupoTerminosIAC.TerminosIAC
        '            If Not String.IsNullOrEmpty(trm.Valor) Then
        '                spw.Param("par$avtdoc_oid_documento").AgregarValorArray(objDocumento.Identificador)
        '                spw.Param("par$avtdoc_oid_termino").AgregarValorArray(trm.Identificador)
        '                spw.Param("par$avtdoc_des_valor").AgregarValorArray(trm.Valor)
        '            End If
        '        Next
        '    End If

        '    If objRemesa.GrupoTerminosIAC IsNot Nothing AndAlso objRemesa.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
        '        For Each termino In objRemesa.GrupoTerminosIAC.TerminosIAC

        '            spw.Param("par$avtrem_oid_termino").AgregarValorArray(termino.Identificador)
        '            spw.Param("par$avtrem_cod_termino").AgregarValorArray(termino.Codigo)
        '            spw.Param("par$avtrem_des_valor").AgregarValorArray(termino.Valor)
        '            spw.Param("par$avtrem_obligatorio").AgregarValorArray(termino.EsObligatorio)

        '        Next
        '    End If

        '    If objRemesa.Bultos IsNot Nothing Then
        '        For Each objBulto In objRemesa.Bultos
        '            spw.Param("par$abuldoc_oid_bulto").AgregarValorArray(objBulto.Identificador)
        '            spw.Param("par$abuldoc_oid_externo").AgregarValorArray(objBulto.IdentificadorExterno)

        '            If objBulto.GrupoTerminosIAC IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_iac").AgregarValorArray(objBulto.GrupoTerminosIAC.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_iac").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.GrupoTerminosIACParciales IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_iac_parciales").AgregarValorArray(objBulto.GrupoTerminosIACParciales.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_iac_parciales").AgregarValorArray(DBNull.Value)
        '            End If

        '            spw.Param("par$abuldoc_oid_cuenta").AgregarValorArray(objBulto.Cuenta.Identificador)

        '            'Precinto foi preparado para uma lista de valores, mas no momento será gravado apenas um precinto.
        '            If objBulto.Precintos IsNot Nothing AndAlso objBulto.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objBulto.Precintos.FirstOrDefault) Then
        '                spw.Param("par$abuldoc_cod_precinto").AgregarValorArray(objBulto.Precintos.FirstOrDefault)
        '            Else
        '                spw.Param("par$abuldoc_cod_precinto").AgregarValorArray(DBNull.Value)
        '            End If

        '            spw.Param("par$abuldoc_cod_bolsa").AgregarValorArray(objBulto.CodigoBolsa)
        '            spw.Param("par$abuldoc_oid_banco_ingreso").AgregarValorArray(If(objBulto.BancoIngreso Is Nothing, Nothing, objBulto.BancoIngreso.Identificador))
        '            spw.Param("par$abuldoc_bol_banco_ing_mad").AgregarValorArray(objBulto.BancoIngresoEsBancoMadre)
        '            spw.Param("par$abuldoc_usuario_resp").AgregarValorArray(objBulto.UsuarioResponsable)
        '            spw.Param("par$abuldoc_puesto_resp").AgregarValorArray(objBulto.PuestoResponsable)
        '            spw.Param("par$abuldoc_nel_cant_parciales").AgregarValorArray(objBulto.CantidadParciales)

        '            If objBulto.FechaHoraInicioConteo <> DateTime.MinValue Then
        '                spw.Param("par$abuldoc_fyh_conteo_inicio").AgregarValorArray(objBulto.FechaHoraInicioConteo)
        '            Else
        '                spw.Param("par$abuldoc_fyh_conteo_inicio").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.FechaHoraFinConteo <> DateTime.MinValue Then
        '                spw.Param("par$abuldoc_fyh_conteo_fin").AgregarValorArray(objBulto.FechaHoraFinConteo)
        '            Else
        '                spw.Param("par$abuldoc_fyh_conteo_fin").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.FechaProcessoLegado <> DateTime.MinValue Then
        '                spw.Param("par$abuldoc_fyh_proceso_leg").AgregarValorArray(objBulto.FechaProcessoLegado)
        '            Else
        '                spw.Param("par$abuldoc_fyh_proceso_leg").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.ElementoPadre IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_bulto_padre").AgregarValorArray(objBulto.ElementoPadre.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_bulto_padre").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.ElementoSustituto IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_bulto_sub").AgregarValorArray(objBulto.ElementoSustituto.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_bulto_sub").AgregarValorArray(DBNull.Value)
        '            End If

        '            spw.Param("par$abuldoc_cod_nivel_detalle").AgregarValorArray(objBulto.ConfiguracionNivelSaldos.RecuperarValor)
        '            spw.Param("par$abuldoc_bol_cuadrado").AgregarValorArray(objBulto.Cuadrado)

        '            If objBulto.CuentaSaldo IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_cuenta_saldo").AgregarValorArray(objBulto.CuentaSaldo.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_cuenta_saldo").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.TipoFormato IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_tipo_formato").AgregarValorArray(objBulto.TipoFormato.Identificador)
        '            Else
        '                spw.Param("par$abuldoc_oid_tipo_formato").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.TipoServicio IsNot Nothing Then
        '                spw.Param("par$abuldoc_oid_tipo_servicio").AgregarValorArray(objBulto.TipoServicio.Identificador)
        '                spw.Param("par$abuldoc_cod_tipo_servicio").AgregarValorArray(objBulto.TipoServicio.Codigo)
        '            Else
        '                spw.Param("par$abuldoc_oid_tipo_servicio").AgregarValorArray(DBNull.Value)
        '                spw.Param("par$abuldoc_cod_tipo_servicio").AgregarValorArray(DBNull.Value)
        '            End If

        '            If objBulto.Cuenta IsNot Nothing Then
        '                If objBulto.Cuenta.Cliente IsNot Nothing Then
        '                    spw.Param("par$abuldoc_cod_cliente").AgregarValorArray(objBulto.Cuenta.Cliente.Codigo)
        '                Else
        '                    spw.Param("par$abuldoc_cod_cliente").AgregarValorArray(DBNull.Value)
        '                End If
        '                If objBulto.Cuenta.SubCliente IsNot Nothing Then
        '                    spw.Param("par$abuldoc_cod_subcliente").AgregarValorArray(objBulto.Cuenta.SubCliente.Codigo)
        '                Else
        '                    spw.Param("par$abuldoc_cod_subcliente").AgregarValorArray(DBNull.Value)
        '                End If
        '                If objBulto.Cuenta.PuntoServicio IsNot Nothing Then
        '                    spw.Param("par$abuldoc_cod_pto_servicio").AgregarValorArray(objBulto.Cuenta.PuntoServicio.Codigo)
        '                Else
        '                    spw.Param("par$abuldoc_cod_pto_servicio").AgregarValorArray(DBNull.Value)
        '                End If
        '                If objBulto.Cuenta.SubCanal IsNot Nothing Then
        '                    spw.Param("par$abuldoc_cod_subcanal").AgregarValorArray(objBulto.Cuenta.SubCanal.Codigo)
        '                Else
        '                    spw.Param("par$abuldoc_cod_subcanal").AgregarValorArray(DBNull.Value)
        '                End If
        '                If objBulto.Cuenta.Sector IsNot Nothing AndAlso objBulto.Cuenta.Sector.Delegacion IsNot Nothing Then
        '                    spw.Param("par$abuldoc_cod_delegacion").AgregarValorArray(objBulto.Cuenta.Sector.Delegacion.Codigo)
        '                Else
        '                    spw.Param("par$abuldoc_cod_delegacion").AgregarValorArray(DBNull.Value)
        '                End If
        '            Else
        '                spw.Param("par$abuldoc_cod_cliente").AgregarValorArray(DBNull.Value)
        '                spw.Param("par$abuldoc_cod_subcliente").AgregarValorArray(DBNull.Value)
        '                spw.Param("par$abuldoc_cod_pto_servicio").AgregarValorArray(DBNull.Value)
        '                spw.Param("par$abuldoc_cod_subcanal").AgregarValorArray(DBNull.Value)
        '                spw.Param("par$abuldoc_cod_delegacion").AgregarValorArray(DBNull.Value)
        '            End If

        '            spw.Param("par$abuldoc_estado").AgregarValorArray(objBulto.Estado.RecuperarValor())

        '            ' Terminos IAC Bulto
        '            If objBulto.GrupoTerminosIAC IsNot Nothing AndAlso objBulto.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
        '                For Each termino In objBulto.GrupoTerminosIAC.TerminosIAC

        '                    spw.Param("par$avtbul_oid_bulto").AgregarValorArray(objBulto.Identificador)
        '                    spw.Param("par$avtbul_oid_termino").AgregarValorArray(termino.Identificador)
        '                    spw.Param("par$avtbul_cod_termino").AgregarValorArray(termino.Codigo)
        '                    spw.Param("par$avtbul_des_valor").AgregarValorArray(termino.Valor)
        '                    spw.Param("par$avtbul_obligatorio").AgregarValorArray(termino.EsObligatorio)

        '                Next
        '            End If

        '            If objBulto.Parciales IsNot Nothing Then
        '                For Each objParcial In objBulto.Parciales
        '                    spw.Param("par$apardoc_oid_remesa").AgregarValorArray(objRemesa.Identificador)
        '                    spw.Param("par$apardoc_oid_bulto").AgregarValorArray(objBulto.Identificador)
        '                    spw.Param("par$apardoc_oid_parcial").AgregarValorArray(objParcial.Identificador)
        '                    spw.Param("par$apardoc_oid_externo").AgregarValorArray(objParcial.IdentificadorExterno)

        '                    'O precinto está preparado para uma lista de valores, porem nesta caso terá apanes um valor, por isso recupera o primeiro registro.
        '                    If objParcial.Precintos IsNot Nothing AndAlso objParcial.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objParcial.Precintos.FirstOrDefault) Then
        '                        spw.Param("par$apardoc_cod_precinto").AgregarValorArray(objParcial.Precintos.FirstOrDefault)
        '                    Else
        '                        spw.Param("par$apardoc_cod_precinto").AgregarValorArray(DBNull.Value)
        '                    End If

        '                    If objParcial.GrupoTerminosIAC IsNot Nothing Then
        '                        spw.Param("par$apardoc_oid_iac").AgregarValorArray(objParcial.GrupoTerminosIAC.Identificador)
        '                    Else
        '                        spw.Param("par$apardoc_oid_iac").AgregarValorArray(DBNull.Value)
        '                    End If

        '                    If objParcial.ElementoPadre IsNot Nothing Then
        '                        spw.Param("par$apardoc_oid_parcial_padre").AgregarValorArray(objParcial.ElementoPadre.Identificador)
        '                    Else
        '                        spw.Param("par$apardoc_oid_parcial_padre").AgregarValorArray(DBNull.Value)
        '                    End If

        '                    If objParcial.ElementoSustituto IsNot Nothing Then
        '                        spw.Param("par$apardoc_oid_parcial_sub").AgregarValorArray(objParcial.ElementoSustituto.Identificador)
        '                    Else
        '                        spw.Param("par$apardoc_oid_parcial_sub").AgregarValorArray(DBNull.Value)
        '                    End If

        '                    spw.Param("par$apardoc_usuario_resp").AgregarValorArray(objParcial.UsuarioResponsable)
        '                    spw.Param("par$apardoc_puesto_resp").AgregarValorArray(objParcial.PuestoResponsable)
        '                    spw.Param("par$apardoc_nec_secuencia").AgregarValorArray(objParcial.Secuencia)

        '                    If objParcial.TipoFormato IsNot Nothing Then
        '                        spw.Param("par$apardoc_oid_tipo_formato").AgregarValorArray(objParcial.TipoFormato.Identificador)
        '                    Else
        '                        spw.Param("par$apardoc_oid_tipo_formato").AgregarValorArray(DBNull.Value)
        '                    End If

        '                    spw.Param("par$apardoc_estado").AgregarValorArray(objParcial.Estado.RecuperarValor())

        '                    ' Terminos IAC Parcial
        '                    If objParcial.GrupoTerminosIAC IsNot Nothing AndAlso objParcial.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
        '                        For Each termino In objParcial.GrupoTerminosIAC.TerminosIAC

        '                            spw.Param("par$avtpar_oid_parcial").AgregarValorArray(objParcial.Identificador)
        '                            spw.Param("par$avtpar_oid_termino").AgregarValorArray(termino.Identificador)
        '                            spw.Param("par$avtpar_cod_termino").AgregarValorArray(termino.Codigo)
        '                            spw.Param("par$avtpar_des_valor").AgregarValorArray(termino.Valor)
        '                            spw.Param("par$avtpar_obligatorio").AgregarValorArray(termino.EsObligatorio)

        '                        Next
        '                    End If

        '                Next
        '            End If

        '        Next
        '    End If

        '    ' Preenche declarado efectivo
        '    ColectarDeclaradoEfectivo(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)
        '    ' Preenche declarado medio pago
        '    ColectarDeclaradoMedioPago(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)
        '    ' Preenche contado efectivo
        '    ColectarContadoEfectivo(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)
        '    ' Preenche contado medio pago
        '    ColectarContadoMedioPago(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)
        '    ' Preenche diferencia efectivo
        '    ColectarDiferenciaEfectivo(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)
        '    ' Preenche diferencia medio pago
        '    ColectarDiferenciaMedioPago(spw, objDocumento.Estado, objDocumento.Identificador, objRemesa)

        '    spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Util.Versao), _
        '    "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg", _
        '    "par$cod_transaccion", "par$cod_resultado")

        '    Return spw
        'End Function

        'Public Shared Sub ColectarDiferenciaMedioPagoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarDiferenciaMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing Then
        '        For Each objParcial In objBulto.Parciales
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarDiferenciaMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDiferenciaMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarDiferenciaMedioPago(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing Then
        '        For Each objBulto In objRemesa.Bultos
        '            'Insere os efectivo do bulto.
        '            ColectarDiferenciaMedioPagoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Private Shared Sub ColectarDiferenciaMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorDivisa As String = String.Empty,
        '        identificadorMedioPago As String = String.Empty,
        '        tipoMedioPago As String = String.Empty,
        '        importe As Decimal,
        '        cantidad As Decimal,
        '        nivelDetalhe As String = String.Empty

        '    If listaDivisas IsNot Nothing Then

        '        For Each divisa In listaDivisas
        '            identificadorMedioPago = String.Empty
        '            identificadorDivisa = divisa.Identificador

        '            'se existe valores então 
        '            If divisa.MediosPago IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each medioPago In divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia))
        '                    identificadorMedioPago = medioPago.Identificador
        '                    tipoMedioPago = medioPago.Tipo.RecuperarValor()

        '                    For Each valor As Clases.ValorMedioPago In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad
        '                        nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

        '                        'Insere a diferencia do medio pago
        '                        spw.Param("par$adifmp_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$adifmp_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$adifmp_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$adifmp_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                        spw.Param("par$adifmp_elem_oid_mediopago").AgregarValorArray(identificadorMedioPago)
        '                        spw.Param("par$adifmp_elem_cod_tipo_mp").AgregarValorArray(tipoMedioPago)
        '                        spw.Param("par$adifmp_elem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$adifmp_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$adifmp_elem_cod_nv_det").AgregarValorArray(nivelDetalhe)

        '                    Next
        '                Next
        '            End If

        '            'se existe valores então 
        '            If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each valor As Clases.ValorTipoMedioPago In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
        '                    identificadorMedioPago = String.Empty
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                    importe = valor.Importe
        '                    cantidad = valor.Cantidad
        '                    tipoMedioPago = valor.TipoMedioPago.RecuperarValor()

        '                    'Insere a diferencia do medio pago
        '                    spw.Param("par$adifmp_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$adifmp_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$adifmp_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$adifmp_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$adifmp_elem_oid_mediopago").AgregarValorArray(identificadorMedioPago)
        '                    spw.Param("par$adifmp_elem_cod_tipo_mp").AgregarValorArray(tipoMedioPago)
        '                    spw.Param("par$adifmp_elem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$adifmp_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$adifmp_elem_cod_nv_det").AgregarValorArray(nivelDetalhe)

        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDiferenciaEfectivoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarDiferenciaEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
        '        For Each objParcial In objBulto.Parciales
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarDiferenciaEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDiferenciaEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarDiferenciaEfectivo(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
        '        For Each objBulto In objRemesa.Bultos
        '            'Insere os efectivo do bulto.
        '            ColectarDiferenciaEfectivoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Private Shared Sub ColectarDiferenciaEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorDivisa As String = String.Empty,
        '        identificadorDenominacion As String = String.Empty,
        '        identificadorUnidadMedida As String = String.Empty,
        '        tipoEfectivoTotal As String = String.Empty,
        '        importe As Decimal,
        '        cantidad As Decimal,
        '        nivelDetalhe As String

        '    If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

        '        For Each divisa In listaDivisas
        '            identificadorDenominacion = String.Empty
        '            identificadorDivisa = divisa.Identificador

        '            'se existe valores então 
        '            If divisa.Denominaciones IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia))
        '                    identificadorDenominacion = Denominacion.Identificador

        '                    For Each valor As Clases.ValorDenominacion In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad
        '                        tipoEfectivoTotal = String.Empty
        '                        nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

        '                        If valor.UnidadMedida IsNot Nothing Then
        '                            identificadorUnidadMedida = valor.UnidadMedida.Identificador
        '                        End If

        '                        'Insere o declarado Efectivo
        '                        spw.Param("par$adifef_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$adifef_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$adifef_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$adifef_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                        spw.Param("par$adifef_elem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                        spw.Param("par$adifef_elem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                        spw.Param("par$adifef_elem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                        spw.Param("par$adifef_elem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$adifef_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$adifef_elem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                    Next
        '                Next
        '            End If

        '            'se existe valores total de efectivo somente valores declarados
        '            If divisa.ValoresTotalesEfectivo IsNot Nothing Then
        '                cantidad = 0
        '                identificadorUnidadMedida = String.Empty
        '                For Each valor As Clases.ValorEfectivo In divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                    tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()
        '                    identificadorDenominacion = String.Empty
        '                    importe = valor.Importe

        '                    'Insere o declarado Efectivo
        '                    spw.Param("par$adifef_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$adifef_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$adifef_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$adifef_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$adifef_elem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                    spw.Param("par$adifef_elem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                    spw.Param("par$adifef_elem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                    spw.Param("par$adifef_elem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$adifef_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$adifef_elem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)

        '                Next
        '            End If

        '            'se existe valores total de divisas
        '            If divisa.ValoresTotalesDivisa IsNot Nothing Then
        '                cantidad = 0
        '                identificadorUnidadMedida = String.Empty
        '                For Each valor As Clases.ValorDivisa In divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Diferencia)
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor()
        '                    tipoEfectivoTotal = String.Empty
        '                    identificadorDenominacion = String.Empty
        '                    importe = valor.Importe

        '                    'Insere o declarado Efectivo
        '                    spw.Param("par$adifef_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$adifef_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$adifef_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$adifef_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$adifef_elem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                    spw.Param("par$adifef_elem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                    spw.Param("par$adifef_elem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                    spw.Param("par$adifef_elem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$adifef_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$adifef_elem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)

        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarContadoMedioPagoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarContadoMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing Then
        '        For Each objParcial In objBulto.Parciales
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarContadoMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarContadoMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarContadoMedioPago(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing Then
        '        For Each objBulto In objRemesa.Bultos
        '            'Insere os efectivo do bulto.
        '            ColectarContadoMedioPagoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Private Shared Sub ColectarContadoMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorMedioPago As String, _
        '            tipoContado As String, _
        '            importe As Decimal, _
        '            cantidad As Long, _
        '            nivelDetalhe As String, _
        '            secuencia As Integer

        '    If listaDivisas IsNot Nothing Then

        '        For Each divisa In listaDivisas
        '            identificadorMedioPago = String.Empty

        '            'se existe valores então 
        '            If divisa.MediosPago IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each medioPago In divisa.MediosPago.Where(Function(d) d.Valores IsNot Nothing AndAlso d.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado))
        '                    identificadorMedioPago = medioPago.Identificador

        '                    For Each valor As Clases.ValorMedioPago In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad
        '                        tipoContado = valor.InformadoPor.RecuperarValor()
        '                        nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()

        '                        Dim oid_valor As String = Guid.NewGuid().ToString()
        '                        spw.Param("par$acontmp_elem_oid_valor").AgregarValorArray(oid_valor)
        '                        spw.Param("par$acontmp_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$acontmp_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$acontmp_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$acontmp_elem_oid_mediopago").AgregarValorArray(identificadorMedioPago)
        '                        spw.Param("par$acontmp_elem_cod_tipocont").AgregarValorArray(tipoContado)
        '                        spw.Param("par$acontmp_elem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$acontmp_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$acontmp_elem_cod_nv_det").AgregarValorArray(nivelDetalhe)
        '                        spw.Param("par$acontmp_elem_nel_secuencia").AgregarValorArray(secuencia)

        '                        If valor.Terminos IsNot Nothing Then
        '                            For Each termino In valor.Terminos.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
        '                                spw.Param("par$avtmpelem_oid_valor").AgregarValorArray(oid_valor)
        '                                spw.Param("par$avtmpelem_oid_termino").AgregarValorArray(termino.Identificador)
        '                                spw.Param("par$avtmpelem_des_valor").AgregarValorArray(termino.Valor)
        '                                spw.Param("par$avtmpelem_nec_ind_grupo").AgregarValorArray(termino.NecIndiceGrupo)
        '                            Next
        '                        End If
        '                    Next
        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarContadoEfectivoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarContadoEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
        '        For Each objParcial In objBulto.Parciales
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarContadoEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarContadoEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarContadoEfectivo(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
        '        For Each objBulto In objRemesa.Bultos
        '            'Insere os efectivo do bulto.
        '            ColectarContadoEfectivoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Private Shared Sub ColectarContadoEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorDenominacion As String, _
        '        identificadorUnidadMedida As String, _
        '        tipoContado As String = String.Empty, _
        '        importe As Decimal, _
        '        cantidad As Decimal = 0, _
        '        identificadorCalidad As String = String.Empty

        '    If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

        '        For Each divisa In listaDivisas
        '            identificadorDenominacion = String.Empty

        '            'se existe valores então 
        '            If divisa.Denominaciones IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado))
        '                    identificadorDenominacion = Denominacion.Identificador

        '                    For Each valor As Clases.ValorDenominacion In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Contado)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad

        '                        If valor.Calidad IsNot Nothing Then
        '                            identificadorCalidad = valor.Calidad.Identificador
        '                        Else
        '                            identificadorCalidad = String.Empty
        '                        End If

        '                        tipoContado = valor.InformadoPor.RecuperarValor()

        '                        If valor.UnidadMedida IsNot Nothing Then
        '                            identificadorUnidadMedida = valor.UnidadMedida.Identificador
        '                        End If

        '                        'Insere o contado Efectivo
        '                        spw.Param("par$adecef_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$adecef_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$adecef_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$adecef_elem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                        spw.Param("par$adecef_elem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                        spw.Param("par$adecef_elem_cod_tipocont").AgregarValorArray(tipoContado)
        '                        spw.Param("par$adecef_elem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$adecef_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$adecef_elem_oid_calidad").AgregarValorArray(identificadorCalidad)

        '                    Next
        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDeclaradoMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarDeclaradoMedioPago(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing Then
        '        For Each objBulto In objRemesa.Bultos
        '            'Insere os efectivo do bulto.
        '            ColectarDeclaradoMedioPagoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDeclaradoMedioPagoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarDeclaradoMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing Then
        '        For Each objParcial In objBulto.Parciales
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarDeclaradoMedioPago(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objParcial.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub

        'Private Shared Sub ColectarDeclaradoMedioPago(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorDivisa As String = String.Empty,
        '        identificadorMedioPago As String = String.Empty,
        '        tipoMedioPago As String = String.Empty,
        '        importe As Decimal,
        '        cantidad As Long,
        '        nivelDetalhe As String = String.Empty,
        '        ingresado As Boolean

        '    If listaDivisas IsNot Nothing Then
        '        For Each divisa In listaDivisas
        '            identificadorDivisa = divisa.Identificador

        '            'se existe valores então 
        '            If divisa.MediosPago IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each medioPago In divisa.MediosPago.Where(Function(m) m.Valores IsNot Nothing AndAlso m.Valores.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado))
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
        '                    identificadorMedioPago = medioPago.Identificador
        '                    tipoMedioPago = medioPago.Tipo.RecuperarValor()

        '                    For Each valor As Clases.ValorMedioPago In medioPago.Valores.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad

        '                        'Insere o declarado medio pago
        '                        Dim oid_valor As String = Guid.NewGuid().ToString()
        '                        spw.Param("par$adecmp_elem_oid_valor").AgregarValorArray(oid_valor)
        '                        spw.Param("par$adecmp_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$adecmp_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$adecmp_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$adecmp_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                        spw.Param("par$adecmp_elem_oid_mediopago").AgregarValorArray(identificadorMedioPago)
        '                        spw.Param("par$adecmp_elem_cod_tipo_mp").AgregarValorArray(tipoMedioPago)
        '                        spw.Param("par$adecmp_elem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$adecmp_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$adecmp_elem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                        spw.Param("par$adecmp_elem_bol_ingresado").AgregarValorArray(ingresado)

        '                        If valor.Terminos IsNot Nothing Then
        '                            For Each termino In valor.Terminos.Where(Function(t) Not String.IsNullOrEmpty(t.Valor))
        '                                spw.Param("par$avtmpelem_oid_valor").AgregarValorArray(oid_valor)
        '                                spw.Param("par$avtmpelem_oid_termino").AgregarValorArray(termino.Identificador)
        '                                spw.Param("par$avtmpelem_des_valor").AgregarValorArray(termino.Valor)
        '                                spw.Param("par$avtmpelem_nec_ind_grupo").AgregarValorArray(termino.NecIndiceGrupo)
        '                            Next
        '                        End If
        '                    Next
        '                Next
        '            End If

        '            'se existe valores então 
        '            If divisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each valor As Clases.ValorTipoMedioPago In divisa.ValoresTotalesTipoMedioPago.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
        '                    identificadorMedioPago = String.Empty
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                    importe = valor.Importe
        '                    cantidad = valor.Cantidad
        '                    tipoMedioPago = valor.TipoMedioPago.RecuperarValor()

        '                    'Insere o declarado medio pago
        '                    spw.Param("par$adecmp_elem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$adecmp_elem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$adecmp_elem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$adecmp_elem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$adecmp_elem_oid_mediopago").AgregarValorArray(identificadorMedioPago)
        '                    spw.Param("par$adecmp_elem_cod_tipo_mp").AgregarValorArray(tipoMedioPago)
        '                    spw.Param("par$adecmp_elem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$adecmp_elem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$adecmp_elem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                    spw.Param("par$adecmp_elem_bol_ingresado").AgregarValorArray(ingresado)

        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDeclaradoEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, objRemesa As Clases.Remesa)
        '    ColectarDeclaradoEfectivo(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objRemesa.Divisas, objRemesa.UsuarioModificacion)

        '    'Se a remesa possui bultos, então insere o declarado efetivo para os bultos.
        '    If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
        '        For Each objBulto In objRemesa.Bultos.Where(Function(a) (a.Divisas IsNot Nothing AndAlso a.Divisas.Count > 0) OrElse (a.Parciales IsNot Nothing AndAlso a.Parciales.Count > 0))
        '            'Insere os efectivo do bulto.
        '            ColectarDeclaradoEfectivoBulto(spw, estadoDocumento, identificadorDocumento, objRemesa.Identificador, objBulto)
        '        Next
        '    End If
        'End Sub

        'Public Shared Sub ColectarDeclaradoEfectivoBulto(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, objBulto As Clases.Bulto)
        '    ColectarDeclaradoEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objBulto.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador)

        '    If objBulto.Parciales IsNot Nothing AndAlso objBulto.Parciales.Count > 0 Then
        '        For Each objParcial In objBulto.Parciales.Where(Function(a) (a.Divisas IsNot Nothing AndAlso a.Divisas.Count > 0))
        '            'se o bulto tiver parcial então insere declarado da parcial.
        '            ColectarDeclaradoEfectivo(spw, estadoDocumento, identificadorDocumento, identificadorRemesa, objParcial.Divisas, objBulto.UsuarioModificacion, objBulto.Identificador, objParcial.Identificador)
        '        Next
        '    End If
        'End Sub
        'Public Shared Sub ColectarDeclaradoEfectivo(ByRef spw As SPWrapper, estadoDocumento As Enumeradores.EstadoDocumento, identificadorDocumento As String, identificadorRemesa As String, listaDivisas As ObservableCollection(Of Clases.Divisa), usuario As String, Optional identificadorBulto As String = Nothing, Optional identificadorParcial As String = Nothing)
        '    Dim identificadorDivisa As String = String.Empty,
        '        identificadorDenominacion As String = String.Empty,
        '        identificadorUnidadMedida As String = String.Empty,
        '        tipoEfectivoTotal As String = String.Empty,
        '        importe As Decimal = 0,
        '        cantidad As Long = 0,
        '        nivelDetalhe As String = String.Empty,
        '        ingresado As Boolean,
        '        esBillete As Boolean

        '    If listaDivisas IsNot Nothing AndAlso listaDivisas.Count > 0 Then

        '        For Each divisa In listaDivisas
        '            identificadorDenominacion = String.Empty
        '            identificadorDivisa = divisa.Identificador

        '            'se existe valores então 
        '            If divisa.Denominaciones IsNot Nothing Then
        '                'recupera somente registros com valores declarados
        '                For Each Denominacion In divisa.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado))
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor()
        '                    tipoEfectivoTotal = String.Empty
        '                    identificadorDenominacion = Denominacion.Identificador
        '                    esBillete = Denominacion.EsBillete

        '                    For Each valor As Clases.ValorDenominacion In Denominacion.ValorDenominacion.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
        '                        importe = valor.Importe
        '                        cantidad = valor.Cantidad

        '                        If valor.UnidadMedida IsNot Nothing Then
        '                            identificadorUnidadMedida = valor.UnidadMedida.Identificador
        '                        End If

        '                        'Insere o declarado Efectivo
        '                        spw.Param("par$aefelem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                        spw.Param("par$aefelem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                        spw.Param("par$aefelem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                        spw.Param("par$aefelem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                        spw.Param("par$aefelem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                        spw.Param("par$aefelem_bol_es_billete").AgregarValorArray(esBillete)
        '                        spw.Param("par$aefelem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                        spw.Param("par$aefelem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                        spw.Param("par$aefelem_num_importe").AgregarValorArray(importe)
        '                        spw.Param("par$aefelem_nel_cantidad").AgregarValorArray(cantidad)
        '                        spw.Param("par$aefelem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                        spw.Param("par$aefelem_bol_ingresado").AgregarValorArray(ingresado)

        '                    Next
        '                Next
        '            End If

        '            'se existe valores total de efectivo somente valores declarados
        '            If divisa.ValoresTotalesEfectivo IsNot Nothing Then
        '                cantidad = 0
        '                identificadorUnidadMedida = String.Empty
        '                For Each valor As Clases.ValorEfectivo In divisa.ValoresTotalesEfectivo.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.Total.RecuperarValor()
        '                    tipoEfectivoTotal = valor.TipoDetalleEfectivo.RecuperarValor()
        '                    identificadorDenominacion = String.Empty
        '                    importe = valor.Importe

        '                    'Insere o declarado Efectivo
        '                    spw.Param("par$aefelem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$aefelem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$aefelem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$aefelem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$aefelem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                    spw.Param("par$aefelem_bol_es_billete").AgregarValorArray(Nothing)
        '                    spw.Param("par$aefelem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                    spw.Param("par$aefelem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                    spw.Param("par$aefelem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$aefelem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$aefelem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                    spw.Param("par$aefelem_bol_ingresado").AgregarValorArray(ingresado)

        '                Next
        '            End If

        '            'se existe valores total de divisas
        '            If divisa.ValoresTotalesDivisa IsNot Nothing Then
        '                cantidad = 0
        '                identificadorUnidadMedida = String.Empty
        '                For Each valor As Clases.ValorDivisa In divisa.ValoresTotalesDivisa.Where(Function(v) v.TipoValor = Enumeradores.TipoValor.Declarado AndAlso v.Importe <> 0)
        '                    nivelDetalhe = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor()
        '                    tipoEfectivoTotal = String.Empty
        '                    identificadorDenominacion = String.Empty

        '                    importe = valor.Importe

        '                    'Insere o declarado Efectivo
        '                    spw.Param("par$aefelem_oid_remesa").AgregarValorArray(identificadorRemesa)
        '                    spw.Param("par$aefelem_oid_bulto").AgregarValorArray(identificadorBulto)
        '                    spw.Param("par$aefelem_oid_parcial").AgregarValorArray(identificadorParcial)
        '                    spw.Param("par$aefelem_oid_divisa").AgregarValorArray(identificadorDivisa)
        '                    spw.Param("par$aefelem_oid_denom").AgregarValorArray(identificadorDenominacion)
        '                    spw.Param("par$aefelem_bol_es_billete").AgregarValorArray(Nothing)
        '                    spw.Param("par$aefelem_oid_unid_med").AgregarValorArray(identificadorUnidadMedida)
        '                    spw.Param("par$aefelem_cod_tipo_efec").AgregarValorArray(tipoEfectivoTotal)
        '                    spw.Param("par$aefelem_num_importe").AgregarValorArray(importe)
        '                    spw.Param("par$aefelem_nel_cantidad").AgregarValorArray(cantidad)
        '                    spw.Param("par$aefelem_cod_nv_detalle").AgregarValorArray(nivelDetalhe)
        '                    spw.Param("par$aefelem_bol_ingresado").AgregarValorArray(ingresado)

        '                Next
        '            End If
        '        Next
        '    End If
        'End Sub

























        ''' <summary>
        ''' Grabar un nuevo documento.
        ''' </summary>
        ''' <param name="objDocumento">Objeto documento preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarDocumento(objDocumento As Clases.Documento)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objDocumento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, objDocumento.Formulario.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_ORIGEN", ProsegurDbType.Objeto_Id, objDocumento.CuentaOrigen.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_DESTINO", ProsegurDbType.Objeto_Id, objDocumento.CuentaDestino.Identificador))

            If objDocumento.CuentaSaldoOrigen IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_ORIGEN", ProsegurDbType.Objeto_Id, objDocumento.CuentaSaldoOrigen.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_ORIGEN", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objDocumento.CuentaSaldoDestino IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_DESTINO", ProsegurDbType.Objeto_Id, objDocumento.CuentaSaldoDestino.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_DESTINO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objDocumento.DocumentoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, objDocumento.DocumentoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If Not String.IsNullOrEmpty(objDocumento.IdentificadorSustituto) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_SUSTITUTO", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorSustituto))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_SUSTITUTO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If Not String.IsNullOrEmpty(objDocumento.IdentificadorGrupo) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorGrupo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MOVIMENTACION_FONDO", ProsegurDbType.Objeto_Id, objDocumento.IdentificadorMovimentacionFondo))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_DOCUMENTO", ProsegurDbType.Objeto_Id, objDocumento.TipoDocumento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_ORIGEN", ProsegurDbType.Objeto_Id, objDocumento.SectorOrigen.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_DESTINO", ProsegurDbType.Objeto_Id, objDocumento.SectorDestino.Identificador))

            'Se a data não foi informada, então grava nulo
            If objDocumento.FechaHoraPlanificacionCertificacion = Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, objDocumento.FechaHoraPlanificacionCertificacion.QuieroGrabarGMTZeroEnLaBBDD(objDocumento.SectorDestino.Delegacion)))
            End If

            If objDocumento.FechaHoraGestion Is Nothing OrElse objDocumento.FechaHoraGestion = Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION", ProsegurDbType.Data_Hora, Nothing))
            Else
                'atualiza data gestion para hora zero de acordo com a delegacion
                Dim _FechaHoraGestion As DateTime = objDocumento.FechaHoraGestion
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION", ProsegurDbType.Data_Hora, _FechaHoraGestion.QuieroGrabarGMTZeroEnLaBBDD(objDocumento.SectorDestino.Delegacion)))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, objDocumento.EstaCertificado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, objDocumento.NumeroExterno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objDocumento.Estado.RecuperarValor()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ATM", ProsegurDbType.Descricao_Curta, objDocumento.CodigoATM))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, objDocumento.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objDocumento.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar documento.
        ''' </summary>
        ''' <param name="documento">Objeto documento preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarDocumento(documento As Clases.Documento, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoAtualizar)
            cmd.CommandType = CommandType.Text

            'Se a data não foi informada, então grava nulo
            If documento.FechaHoraPlanificacionCertificacion = Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, documento.FechaHoraPlanificacionCertificacion.QuieroGrabarGMTZeroEnLaBBDD(documento.SectorDestino.Delegacion)))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, documento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_ORIGEN", ProsegurDbType.Objeto_Id, documento.CuentaOrigen.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_DESTINO", ProsegurDbType.Objeto_Id, documento.CuentaDestino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_ORIGEN", ProsegurDbType.Objeto_Id, documento.CuentaSaldoOrigen.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO_DESTINO", ProsegurDbType.Objeto_Id, documento.CuentaSaldoDestino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_DESTINO", ProsegurDbType.Objeto_Id, documento.SectorDestino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, documento.EstaCertificado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Descricao_Longa, documento.NumeroExterno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, documento.Estado.RecuperarValor()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))

            If documento.DocumentoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, documento.DocumentoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If Not String.IsNullOrEmpty(documento.IdentificadorSustituto) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_SUSTITUTO", ProsegurDbType.Objeto_Id, documento.IdentificadorSustituto))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO_SUSTITUTO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If documento.FechaHoraGestion Is Nothing OrElse documento.FechaHoraGestion = Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION", ProsegurDbType.Data_Hora, Nothing))
            Else
                Dim _FechaHoraGestion As DateTime = documento.FechaHoraGestion
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION", ProsegurDbType.Data_Hora, _FechaHoraGestion.QuieroGrabarGMTZeroEnLaBBDD(documento.SectorDestino.Delegacion)))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar estado del documento.
        ''' </summary>
        ''' <param name="identificador">Identificador del Documento</param>
        ''' <param name="estado">Estado del Documento</param>
        ''' <param name="usuario">Usuario que está haciendo la actualización</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstado(identificador As String, estado As Enumeradores.EstadoDocumento, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoAtualizarEstado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor()))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar estado del documento.
        ''' </summary>
        ''' <param name="identificador">Identificador del Documento</param>
        ''' <param name="estado">Estado del Documento</param>
        ''' <param name="usuario">Usuario que está haciendo la actualización</param>
        ''' <param name="codigoComprobante">Codigo Comprobante del Documento</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoYCodidoComprobante(identificador As String, estado As Enumeradores.EstadoDocumento, usuario As String, codigoComprobante As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoActualizaEstadoCodComprobante)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Curta, codigoComprobante))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor()))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Sub DocumentoActualizarFechaHoraPlanCertificacion(documento As Clases.Documento,
                                                                        usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoActualizarFechaHoraPlanCertificacion)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, documento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))
            'Se a data não foi informada, então grava nulo
            If documento.FechaHoraPlanificacionCertificacion = Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, documento.FechaHoraPlanificacionCertificacion))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        Public Shared Sub ActualizarFechaHoraPlanCertificacion(identificadorDocumento As String,
                                                               FechaHoraPlanificacionCertificacion As DateTime,
                                                               usuario As String)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoActualizarFechaHoraPlanCertificacion)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))

                    If FechaHoraPlanificacionCertificacion = Date.MinValue Then
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
                    Else
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, FechaHoraPlanificacionCertificacion))
                    End If

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            TransaccionEfectivo.ActualizarFechaHoraPlanCertificacion(identificadorDocumento, FechaHoraPlanificacionCertificacion, usuario)
            TransaccionMedioPago.ActualizarFechaHoraPlanCertificacion(identificadorDocumento, FechaHoraPlanificacionCertificacion, usuario)

        End Sub

        ''' <summary>
        ''' Actualiza el campo Bol_Impreso
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento</param>
        ''' <param name="codigoComprobante">Código codigoComprobante</param>
        ''' <param name="impreso">Impreso</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarBolImpreso(identificadorDocumento As String, codigoComprobante As String, impreso As Boolean)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.DocumentoActualizaBolImpreso
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(identificadorDocumento) Then

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, " DOC.OID_DOCUMENTO = []OID_DOCUMENTO "))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            ElseIf Not String.IsNullOrEmpty(codigoComprobante) Then

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, " DOC.COD_COMPROBANTE = []COD_COMPROBANTE "))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, codigoComprobante))

            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_IMPRESO", ProsegurDbType.Inteiro_Curto, impreso))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        ''' <summary>
        ''' Actualizar el código comprovante do documento
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCodigoComprobante(identificadorDocumento As String, codigoComprobante As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoAtualizarCodigoComprobante)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Curta, codigoComprobante))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Sub ActualizarDatosRemesa(remesa As Clases.Remesa, codigoUsuario As String, fechaActualizacion As DateTime, ByRef transaccion As DataBaseHelper.Transaccion)
            Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AlocarDesalocarRemesa)
            Dim sp As New SPWrapper(sql, True, CommandType.Text)

            sp.AgregarParam("OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador)
            sp.AgregarParam("COD_RUTA", ProsegurDbType.Descricao_Curta, remesa.Ruta)
            sp.AgregarParam("NEL_PARADA", ProsegurDbType.Inteiro_Longo, remesa.Parada)
            sp.AgregarParam("FYH_TRANSPORTE", ProsegurDbType.Data_Hora, remesa.FechaHoraTransporte)
            sp.AgregarParam("GMT_MODIFICACION", ProsegurDbType.Data_Hora, fechaActualizacion)
            sp.AgregarParam("USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codigoUsuario)
            sp.AgregarParam("OID_EXTERNO", ProsegurDbType.Descricao_Longa, remesa.IdentificadorExterno)

            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Sub ActualizarDatosRemesa(documentos As ObservableCollection(Of Clases.Documento))

            For Each doc In documentos

                Dim remesa As Clases.Remesa = DirectCast(doc.Elemento, Clases.Remesa)

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AlocarPedidosExternos)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_RUTA", ProsegurDbType.Descricao_Curta, remesa.Ruta))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_TRANSPORTE", ProsegurDbType.Data_Hora, remesa.FechaHoraTransporte))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, remesa.FechaHoraModificacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, remesa.UsuarioModificacion))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Next

        End Sub

        Public Shared Sub ActualizarDatosDocumento(identificadorDocumento As String, codigoUsuario As String, fechaActualizacion As DateTime, ByRef transaccion As DataBaseHelper.Transaccion)
            Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarUsuarioFechaAlocarDesalocarDocumento)
            Dim sp As New SPWrapper(sql, True, CommandType.Text)

            sp.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento)
            sp.AgregarParam("GMT_MODIFICACION", ProsegurDbType.Data_Hora, fechaActualizacion)
            sp.AgregarParam("USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codigoUsuario)

            AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Sub ActualizarDatosDocumentos(documentos As ObservableCollection(Of Clases.Documento))

            For Each doc In documentos

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = My.Resources.ActualizarUsuarioFechaAlocarDesalocarDocumento
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, doc.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, doc.FechaHoraModificacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, doc.UsuarioModificacion))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            Next

        End Sub

        ''' <summary>
        ''' Actualiza el campo BOL_NO_CERTIFICAR
        ''' </summary>
        ''' <param name="OidDocumento">The oid documento.</param>
        ''' <param name="BolNoCertificar">if set to <c>true</c> [bol no certificar].</param>
        ''' <param name="GmtModificacion">The GMT modificacion.</param>
        ''' <param name="UsuarioModificacion">The usuario modificacion.</param>
        ''' <param name="objTransacion">The object transacion.</param>
        Public Shared Sub ActualizarBolNoCertificar(OidDocumento As String, BolNoCertificar As Boolean, GmtModificacion As DateTime,
                                                   UsuarioModificacion As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoActualizarBolNoCertificar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, OidDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_NO_CERTIFICAR", ProsegurDbType.Logico, BolNoCertificar))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, UsuarioModificacion))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ActualizarBolEnviadoSOL(identificadorRemesa As String)

            If Not String.IsNullOrEmpty(identificadorRemesa) Then

                Dim cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarBolEnviadoSOL)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))

                AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_SALIDAS, cmd)

            End If

        End Sub

        Public Shared Function ActualizarEstado(OidDocumento As String, CodEstado As String, NelIntentos As Integer, Optional DescricaoError As String = "", Optional OidIntegracion As String = "") As Boolean

            Dim dbComando As IDbCommand = Nothing

            Try
                If CodEstado = "OK" Then
                    If Not String.IsNullOrEmpty(OidIntegracion) AndAlso Not String.IsNullOrEmpty(DescricaoError) Then

                        Dim integracionError As Clases.IntegracionError = Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(New Clases.IntegracionError With {
                                                                                            .DescricaoError = DescricaoError,
                                                                                            .FechaHoraCreacion = DateTime.Now,
                                                                                            .FechaHoraModificacion = DateTime.Now,
                                                                                            .UsuarioCreacion = "DUO_ENVIARDOCUMENTOSOL",
                                                                                            .UsuarioModificacion = "DUO_ENVIARDOCUMENTOSOL"
                                                                                        }, OidIntegracion)

                    End If
                End If

                dbComando = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim Sql As String = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AtualizaEstadoDocumento)

                dbComando.CommandType = CommandType.Text
                dbComando.CommandText = Sql

                dbComando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "NEL_INTENTO_ENVIO", DbHelper.ProsegurDbType.Inteiro_Longo, NelIntentos))
                dbComando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_ESTADO", DbHelper.ProsegurDbType.Observacao_Longa, CodEstado))
                dbComando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, OidDocumento))

                Return (AcessoDados.ExecutarNonQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, dbComando) > 0)

            Catch ex As Exception
                Throw
            Finally
                If dbComando IsNot Nothing Then dbComando.Dispose()
            End Try
        End Function

        Public Shared Sub LimparValorMensajeExterna(identificadorDocumento As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_LimparMensajeExterna)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar el documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">identificadorDocumento.</param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarDocumento(identificadorDocumento As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoExcluir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "{PARA ORGANIZAR}"

        ''' <summary>
        ''' Recupera o documento
        ''' </summary>
        ''' <param name="IdDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 04/06/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarDocumento(IdDocumento As String) As CSCertificacion.Documento

            Dim objDocumento As CSCertificacion.Documento = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdDocumento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDocumento = New CSCertificacion.Documento

                Dim identificadorSectorDestino As String = Util.AtribuirValorObj(dt.Rows(0)("OID_SECTOR_DESTINO"), GetType(String))

                Dim delegacionDestino As Clases.Delegacion = Genesis.Delegacion.ObtenerPorIdentificadorSector(identificadorSectorDestino)

                With objDocumento
                    .CodEstado = Util.AtribuirValorObj(dt.Rows(0)("COD_ESTADO"), GetType(String))
                    .CodExterno = Util.AtribuirValorObj(dt.Rows(0)("COD_EXTERNO"), GetType(String))
                    .CodTipoDocumento = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_DOCUMENTO"), GetType(String))
                    .DesTipoDocumento = Util.AtribuirValorObj(dt.Rows(0)("DES_TIPO_DOCUMENTO"), GetType(String))
                    .FyhPlanCertificacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                    .FyhPlanCertificacion = .FyhPlanCertificacion.QuieroExibirEstaFechaEnLaPatalla(delegacionDestino)
                    .OidDocumento = Util.AtribuirValorObj(dt.Rows(0)("OID_DOCUMENTO"), GetType(String))
                    .OidGrupoDocumento = Util.AtribuirValorObj(dt.Rows(0)("OID_GRUPO_DOCUMENTO"), GetType(String))
                End With

            End If

            Return objDocumento
        End Function

        Public Shared Function ConsultaDocumentos(Peticion As ContractoServicio.Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion) As ObservableCollection(Of Clases.Documento)

            Dim objDocumento As Clases.Documento = Nothing
            Dim documentos As ObservableCollection(Of Clases.Documento) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoConsultaDocumentos)
                cmd.CommandType = CommandType.Text

                PreparaQueryConsultaDocumentos(Peticion, cmd)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    documentos = New ObservableCollection(Of Clases.Documento)

                    For Each dr In dt.Rows

                        objDocumento = New Clases.Documento
                        With objDocumento

                            'Campos obrigatórios..
                            .TipoDocumento = New Clases.TipoDocumento()

                            .Identificador = Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String))
                            .IdentificadorGrupo = Util.AtribuirValorObj(dr("OID_GRUPO_DOCUMENTO"), GetType(String))
                            .IdentificadorMovimentacionFondo = Util.AtribuirValorObj(dr("OID_MOVIMENTACION_FONDO"), GetType(String))
                            '.Historico = HistoricoMovimentacionDocumento.RecuperarHistoricoMovimentacion(.Identificador)

                            'O formulário será recuperado na classe de negocio.
                            .Formulario = Formulario.ObtenerFormulario(Util.AtribuirValorObj(dr("OID_FORMULARIO"), GetType(String)))

                            .CuentaOrigen = Cuenta.ObtenerCuentaPorIdentificador(Util.AtribuirValorObj(dr("OID_CUENTA_ORIGEN"), GetType(String)), Enumeradores.TipoCuenta.Movimiento, "ConsultaDocumentos")
                            .CuentaDestino = Cuenta.ObtenerCuentaPorIdentificador(Util.AtribuirValorObj(dr("OID_CUENTA_DESTINO"), GetType(String)), Enumeradores.TipoCuenta.Movimiento, "ConsultaDocumentos")

                            .TipoDocumento.Identificador = Util.AtribuirValorObj(dr("OID_TIPO_DOCUMENTO"), GetType(String))
                            .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(dr("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                            .FechaHoraPlanificacionCertificacion = .FechaHoraPlanificacionCertificacion.QuieroExibirEstaFechaEnLaPatalla(.CuentaDestino.Sector.Delegacion)
                            .EstaCertificado = Util.AtribuirValorObj(dr("BOL_CERTIFICADO"), GetType(Boolean))
                            .NumeroExterno = Util.AtribuirValorObj(dr("COD_EXTERNO"), GetType(String))
                            If Not dr("COD_ESTADO").Equals(DBNull.Value) Then
                                .Estado = RecuperarEnum(Of Enumeradores.EstadoDocumento)(dr("COD_ESTADO").ToString)
                            End If

                            .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                            .FechaHoraCreacion = .FechaHoraCreacion.QuieroExibirEstaFechaEnLaPatalla(.CuentaDestino.Sector.Delegacion)
                            .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                            .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                            .FechaHoraModificacion = .FechaHoraModificacion.QuieroExibirEstaFechaEnLaPatalla(.CuentaDestino.Sector.Delegacion)
                            .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
                            .CodigoComprobante = Util.AtribuirValorObj(dr("COD_COMPROBANTE"), GetType(String))

                            'Recupera o elemento do documento
                            Dim _FechaGestion As DateTime = Util.AtribuirValorObj(dr("FYH_GESTION"), GetType(DateTime))
                            .FechaHoraGestion = _FechaGestion.QuieroExibirEstaFechaEnLaPatalla(.CuentaDestino.Sector.Delegacion)
                        End With

                        documentos.Add(objDocumento)

                    Next

                End If

            End Using

            Return documentos

        End Function

        Public Shared Sub PreparaQueryConsultaDocumentos(Peticion As ContractoServicio.Contractos.GenesisSaldos.Documento.ConsultaDocumentos.Peticion,
                                                   ByRef comando As IDbCommand)

            Dim filtroCuenta As String = String.Empty
            Dim filtro As String = String.Empty

            If Peticion IsNot Nothing Then

                'Filtro de data desde, data de atualização do documento..
                If (Peticion.FechaHoraDesde IsNot Nothing AndAlso Peticion.FechaHoraDesde <> DateTime.MinValue) AndAlso (Peticion.FechaHoraHasta IsNot Nothing AndAlso Peticion.FechaHoraHasta <> DateTime.MinValue) Then
                    filtro = " AND (DOCU.GMT_MODIFICACION BETWEEN []FECHA_DESDE AND []FECHA_HASTA )"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_DESDE", ProsegurDbType.Data_Hora, Peticion.FechaHoraDesde))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FECHA_HASTA", ProsegurDbType.Data_Hora, Peticion.FechaHoraHasta))
                End If

                'Monta o filtro de estados dos documentos
                filtro += PreparaFiltroEstadosDocumento(Peticion.EstadosDocumento, comando)

                'Monta filtro dos formularios de acordo com os conjuntos de caracteristicas
                filtro += Formulario.PreparaFiltroFormulario(Peticion.ConjuntosCaracteristicas, comando)

                If Peticion.OrigenBusqueda = Enumeradores.OrigenBusquedaDocumento.Ambos OrElse
                   Peticion.OrigenBusqueda = Enumeradores.OrigenBusquedaDocumento.Enviados Then

                    filtroCuenta = "( 0=0 "

                    'CUENTA ORIGEN
                    If Not String.IsNullOrEmpty(Peticion.SubCanal) Then
                        filtroCuenta = " AND SUBCANALOR.COD_SUBCANAL = []COD_SUBCANAL "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCanal))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Canal) Then
                        filtroCuenta = filtroCuenta & " AND CANALOR.COD_CANAL = []COD_CANAL "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.Canal))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.PuntoServicio) Then
                        filtroCuenta = filtroCuenta & " AND PTOSERVOR.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.PuntoServicio))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.SubCliente) Then
                        filtroCuenta = filtroCuenta & " AND SUBCLIOR.COD_SUBCLIENTE = []COD_SUBCLIENTE "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCliente))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Cliente) Then
                        filtroCuenta = filtroCuenta & " AND CLIOR.COD_CLIENTE = []COD_CLIENTE "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Cliente))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Sector) Then
                        filtroCuenta = filtroCuenta & " AND SEOR.COD_SECTOR = []COD_SECTOR "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Peticion.Sector))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Planta) Then
                        filtroCuenta = filtroCuenta & " AND PLOR.COD_PLANTA = []COD_PLANTA "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, Peticion.Planta))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Delegacion) Then
                        filtroCuenta = filtroCuenta & " AND DELOR.COD_DELEGACION = []COD_DELEGACION "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Delegacion))
                    End If

                    filtroCuenta &= ")"

                End If

                If Peticion.OrigenBusqueda = Enumeradores.OrigenBusquedaDocumento.Ambos OrElse
                 Peticion.OrigenBusqueda = Enumeradores.OrigenBusquedaDocumento.Recebidos Then

                    If Peticion.OrigenBusqueda = Enumeradores.OrigenBusquedaDocumento.Ambos Then
                        filtroCuenta &= " OR ( 0=0 "
                    Else
                        filtroCuenta &= " ( 0=0 "
                    End If

                    'CUENTA DESTINO
                    If Not String.IsNullOrEmpty(Peticion.SubCanal) Then
                        filtroCuenta = filtroCuenta & " AND SUBCANALDE.COD_SUBCANAL = []COD_SUBCANAL "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCanal))
                    End If

                    If Not String.IsNullOrEmpty(Peticion.Canal) Then
                        filtroCuenta = filtroCuenta & " AND CANALDE.COD_CANAL = []COD_CANAL "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.Canal))
                    End If

                    If Not String.IsNullOrEmpty(Peticion.PuntoServicio) Then
                        filtroCuenta = filtroCuenta & " AND PTOSERVDE.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.PuntoServicio))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.SubCliente) Then
                        filtroCuenta = filtroCuenta & " AND SUBCLIDE.COD_SUBCLIENTE = []COD_SUBCLIENTE "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCliente))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Cliente) Then
                        filtroCuenta = filtroCuenta & " AND CLIDE.COD_CLIENTE = []COD_CLIENTE "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Cliente))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Sector) Then
                        filtroCuenta = filtroCuenta & " AND SEDE.COD_SECTOR = []COD_SECTOR "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Peticion.Sector))
                    End If
                    If Not String.IsNullOrEmpty(Peticion.Planta) Then
                        filtroCuenta = filtroCuenta & " AND PLDE.COD_PLANTA = []COD_PLANTA "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, Peticion.Planta))
                    End If

                    If Not String.IsNullOrEmpty(Peticion.Delegacion) Then
                        filtroCuenta = filtroCuenta & " AND DELDE.COD_DELEGACION = []COD_DELEGACION "
                        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Delegacion))
                    End If

                    filtroCuenta &= " )"
                End If

            End If

            comando.CommandText = String.Format(comando.CommandText, filtro, filtroCuenta)

        End Sub

        Public Shared Function PreparaFiltroEstadosDocumento(estadosDocumento As ObservableCollection(Of Enumeradores.EstadoDocumento),
                                                 ByRef comando As IDbCommand) As String

            Dim filtroIn As String = String.Empty

            Dim estados As New List(Of String)
            If estadosDocumento IsNot Nothing AndAlso estadosDocumento.Count > 0 Then
                For Each estado In estadosDocumento
                    estados.Add(estado.RecuperarValor())
                Next
            End If

            If estados.Count > 0 Then
                filtroIn = " AND " & Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, estados, "COD_ESTADO", comando, String.Empty, "DOCU", , False)
            End If

            Return filtroIn

        End Function

        ''' <summary>
        ''' RecuperarDetalheElemento
        ''' </summary>
        ''' <param name="IdDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 11/04/2014 - Criado
        ''' </history>
        Public Shared Function RecuperarDetalheElemento(IdDocumento As String) As Clases.Documento
            'Esse método foi criado por questão de performance para exibir essas informações no componente de elemento
            Dim objDocumento As Clases.Documento = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoDetalheElemento)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdDocumento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objDocumento = New Clases.Documento

                With objDocumento
                    .CodigoComprobante = Util.AtribuirValorObj(dt.Rows(0)("COD_COMPROBANTE"), GetType(String))
                    .Formulario = New Clases.Formulario With {.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FORMULARIO"), GetType(String))}
                    .CuentaOrigen = New Clases.Cuenta With {
                        .Sector = New Clases.Sector With {.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("SETOR_ORIGEM"), GetType(String))}}
                    .CuentaDestino = New Clases.Cuenta With {
                        .Sector = New Clases.Sector With {.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("SETOR_DESTINO"), GetType(String))}}

                End With
            End If

            Return objDocumento
        End Function

        ''' <summary>
        ''' Verifica se o documento possui alguma transação certicada com certificado DE=Definitivo.
        ''' </summary>
        ''' <param name="documento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DocumentoETransacionCertificado(documento As Clases.Documento) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoETransaccionCertificado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, documento.Identificador))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function ValidaCertificadoProvisional(identificadorDocumento As String, delegacionLogada As Clases.Delegacion) As Clases.Certificado

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim sbQuery As New StringBuilder

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValidaCertificadoProvisional)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

            Dim retorno As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If retorno IsNot Nothing AndAlso retorno.Rows.Count > 0 Then
                Dim objCertificado As New Clases.Certificado
                With objCertificado
                    .Identificador = Util.AtribuirValorObj(retorno.Rows(0)("OID_CERTIFICADO"), GetType(String))
                    .Estado = EnumExtension.RecuperarEnum(Of Enumeradores.EstadoCertificado)(Util.AtribuirValorObj(retorno.Rows(0)("COD_ESTADO"), GetType(String)))
                    .FechaHoraCertificado = Util.AtribuirValorObj(retorno.Rows(0)("FYH_CERTIFICADO"), GetType(DateTime))
                    .FechaHoraCertificado = .FechaHoraCertificado.QuieroExibirEstaFechaEnLaPatalla(delegacionLogada)
                End With
                Return objCertificado
            Else
                Return Nothing
            End If

        End Function


#End Region

    End Class

End Namespace