Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe GrupoDocumentos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GrupoDocumentos

#Region " Procedure - Cargar"

        Public Shared Sub GrabarGrupoDocumento(ByRef GrupoDocumentos As Clases.GrupoDocumentos,
                                               bol_gestion_bulto As Boolean,
                                               hacer_commit As Boolean,
                                               confirmar_doc As Boolean,
                                               caracteristica_integracion As Enumeradores.CaracteristicaFormulario?,
                                               ByRef TransaccionActual As DataBaseHelper.Transaccion)

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = Nothing

                If GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Cierres) OrElse _
                       GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                    spw = ColectarGrupoDocumentosValores(GrupoDocumentos, hacer_commit, confirmar_doc)

                ElseIf GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) Then
                    spw = ColectarGrupoDocumentosAltas(GrupoDocumentos, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion)

                ElseIf GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Actas) Then
                    spw = ColectarGrupoDocumentosActas(GrupoDocumentos, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion)

                ElseIf (GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Reenvios) OrElse _
                       GrupoDocumentos.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Bajas)) Then
                    spw = ColectarGrupoDocumentosReenvioBaja(GrupoDocumentos, hacer_commit, confirmar_doc)

                End If

                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                If confirmar_doc AndAlso ds IsNot Nothing Then
                    PoblarGrupoDocumentos_Confirmar(GrupoDocumentos, ds)
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

        Private Shared Function ColectarGrupoDocumentosValores(grupoDocumentos As Clases.GrupoDocumentos,
                                                               hacer_commit As Boolean,
                                                               confirmar_doc As Boolean) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_valores", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            'grupo de documentos
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, grupoDocumentos.Identificador, , False)
            If grupoDocumentos.GrupoDocumentoPadre IsNot Nothing Then
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, grupoDocumentos.GrupoDocumentoPadre.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, grupoDocumentos.Formulario.Identificador, , False)
            spw.AgregarParam("par$oid_sector_origen", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorOrigen.Identificador, , False)
            spw.AgregarParam("par$oid_sector_destino", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorDestino.Identificador, , False)
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, grupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, grupoDocumentos.Rowver, , False)

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

            'documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_cod_externo", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adocs_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'efectivos x documentos
            spw.AgregarParam("par$aefdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_denominacion", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_unid_medida", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_niv_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_tp_efec_tot", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_oid_calidad", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            'medios de pago x documento
            spw.AgregarParam("par$ampdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_divisa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_oid_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_tipo_med_pago", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_cod_nivel_detalle", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ampdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$ampdoc_nel_cantidad", ProsegurDbType.Numero_Decimal, Nothing, , True)

            'terminos x medio de pago x documento
            spw.AgregarParam("par$avtmpdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_oid_t_mediopago", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$avtmpdoc_nec_indice_grp", ProsegurDbType.Inteiro_Longo, Nothing, , True)

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

            'terminos x documentos
            spw.AgregarParam("par$avtdoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'terminos del grupo de documentos
            spw.AgregarParam("par$avtgdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'Cultura para dicionario 
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            'informacion de ejecución
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            ' Cargar valores de los documentos
            GenesisSaldos.Documento.Colectar_Documentos(grupoDocumentos.Documentos, spw)

            'terminos del grupo documentos
            If grupoDocumentos.GrupoTerminosIAC IsNot Nothing Then
                For Each trm As Clases.TerminoIAC In grupoDocumentos.GrupoTerminosIAC.TerminosIAC
                    If Not String.IsNullOrWhiteSpace(trm.Valor) Then
                        'pGDocsTrmOid.AgregarValorArray(NewGUID)
                        spw.Param("par$avtgdoc_oid_termino").AgregarValorArray(trm.Identificador)
                        spw.Param("par$avtgdoc_des_valor").AgregarValorArray(trm.Valor)
                    End If
                Next
            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Function ColectarGrupoDocumentosActas(grupoDocumentos As Clases.GrupoDocumentos,
                                                             bol_gestion_bulto As Boolean,
                                                             hacer_commit As Boolean,
                                                             confirmar_doc As Boolean,
                                                    Optional caracteristica_integracion As Enumeradores.CaracteristicaFormulario? = Nothing) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_elem_actas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            'grupo de documentos
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, grupoDocumentos.Identificador, , False)
            If grupoDocumentos.GrupoDocumentoPadre IsNot Nothing Then
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, grupoDocumentos.GrupoDocumentoPadre.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$bol_gestion_bulto", ProsegurDbType.Objeto_Id, If(bol_gestion_bulto, 1, 0), , False)
            If grupoDocumentos.Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(grupoDocumentos.Formulario.Identificador) Then
                spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, grupoDocumentos.Formulario.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            If caracteristica_integracion IsNot Nothing Then
                Dim _integracion As Enumeradores.CaracteristicaFormulario = caracteristica_integracion
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, _integracion.RecuperarValor(), , False)
            Else
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$oid_sector", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorDestino.Identificador, , False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, grupoDocumentos.Rowver, , False)

            ' arrays asociativos
            'terminos del grupo de documentos
            spw.AgregarParam("par$avtgdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
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

            'informacion de ejecución
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, grupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            ' Cargar valores de los documentos
            GenesisSaldos.Documento.Colectar_Documentos(grupoDocumentos.Documentos, spw)

            ' Cargar terminos del grupo documentos
            If grupoDocumentos.GrupoTerminosIAC IsNot Nothing Then
                For Each trm As Clases.TerminoIAC In grupoDocumentos.GrupoTerminosIAC.TerminosIAC
                    If Not String.IsNullOrWhiteSpace(trm.Valor) Then
                        spw.Param("par$avtgdoc_oid_termino").AgregarValorArray(trm.Identificador)
                        spw.Param("par$avtgdoc_cod_termino").AgregarValorArray(trm.Codigo)
                        spw.Param("par$avtgdoc_des_valor").AgregarValorArray(trm.Valor)
                    End If
                Next
            End If

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Function ColectarGrupoDocumentosAltas(grupoDocumentos As Clases.GrupoDocumentos,
                                                             bol_gestion_bulto As Boolean,
                                                             hacer_commit As Boolean,
                                                             confirmar_doc As Boolean,
                                                    Optional caracteristica_integracion As Enumeradores.CaracteristicaFormulario? = Nothing) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_elem_altas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            'grupo de documentos
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, grupoDocumentos.Identificador, , False)
            If grupoDocumentos.GrupoDocumentoPadre IsNot Nothing Then
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, grupoDocumentos.GrupoDocumentoPadre.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$bol_gestion_bulto", ProsegurDbType.Objeto_Id, If(bol_gestion_bulto, 1, 0), , False)
            If grupoDocumentos.Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(grupoDocumentos.Formulario.Identificador) Then
                spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, grupoDocumentos.Formulario.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            If caracteristica_integracion IsNot Nothing Then
                Dim _integracion As Enumeradores.CaracteristicaFormulario = caracteristica_integracion
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, _integracion.RecuperarValor(), , False)
            Else
                spw.AgregarParam("par$cod_integracion", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$oid_sector_origen", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorOrigen.Identificador, , False)
            spw.AgregarParam("par$oid_sector_destino", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorDestino.Identificador, , False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, grupoDocumentos.Rowver, , False)

            ' arrays asociativos
            'terminos del grupo de documentos
            spw.AgregarParam("par$avtgdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_cod_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
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
            spw.AgregarParam("par$avtdoc_cod_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
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

            'informacion de ejecución
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, grupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            ' Cargar valores de los documentos
            GenesisSaldos.Documento.Colectar_Documentos(grupoDocumentos.Documentos, spw)

            ' Cargar terminos del grupo documentos
            If grupoDocumentos.GrupoTerminosIAC IsNot Nothing Then
                For Each trm As Clases.TerminoIAC In grupoDocumentos.GrupoTerminosIAC.TerminosIAC
                    If Not String.IsNullOrWhiteSpace(trm.Valor) Then


                        If Not String.IsNullOrEmpty(trm.Identificador) Then
                            spw.Param("par$avtgdoc_oid_termino").AgregarValorArray(trm.Identificador)
                        Else
                            spw.Param("par$avtgdoc_oid_termino").AgregarValorArray(DBNull.Value)
                        End If
                        If Not String.IsNullOrEmpty(trm.Codigo) Then
                            spw.Param("par$avtgdoc_cod_termino").AgregarValorArray(trm.Codigo)
                        Else
                            spw.Param("par$avtgdoc_cod_termino").AgregarValorArray(DBNull.Value)
                        End If
                        If Not String.IsNullOrEmpty(trm.Valor) Then
                            spw.Param("par$avtgdoc_des_valor").AgregarValorArray(trm.Valor)
                        Else
                            spw.Param("par$avtgdoc_des_valor").AgregarValorArray(DBNull.Value)
                        End If
                    End If
                Next
            End If


            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Function ColectarGrupoDocumentosReenvioBaja(grupoDocumentos As Clases.GrupoDocumentos,
                                                                   hacer_commit As Boolean,
                                                                   confirmar_doc As Boolean) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_elem_re_baj", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, Not confirmar_doc)

            'grupo de documentos
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, grupoDocumentos.Identificador, , False)
            If grupoDocumentos.GrupoDocumentoPadre IsNot Nothing Then
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, grupoDocumentos.GrupoDocumentoPadre.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Objeto_Id, DBNull.Value, , False)
            End If
            spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, grupoDocumentos.Formulario.Identificador, , False)
            spw.AgregarParam("par$oid_sector_origen", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorOrigen.Identificador, , False)
            spw.AgregarParam("par$oid_sector_destino", ProsegurDbType.Objeto_Id, grupoDocumentos.SectorDestino.Identificador, , False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, grupoDocumentos.Rowver, , False)

            ' arrays asociativos
            'terminos del grupo de documentos
            spw.AgregarParam("par$avtgdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            ' arrays de documentos
            spw.AgregarParam("par$adocs_oid", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_doc_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_sustituto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_oid_mov_fondos", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_plncertif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adocs_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
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
            spw.AgregarParam("par$aremdoc_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            'informacion de ejecución
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, grupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            ' Cargar valores de los documentos
            GenesisSaldos.Documento.Colectar_Documentos(grupoDocumentos.Documentos, spw)

            ' Cargar terminos del grupo documentos
            If grupoDocumentos.GrupoTerminosIAC IsNot Nothing Then
                For Each trm As Clases.TerminoIAC In grupoDocumentos.GrupoTerminosIAC.TerminosIAC
                    If Not String.IsNullOrWhiteSpace(trm.Valor) Then
                        spw.Param("par$avtgdoc_oid_termino").AgregarValorArray(trm.Identificador)
                        spw.Param("par$avtgdoc_des_valor").AgregarValorArray(trm.Valor)
                    End If
                Next
            End If


            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Public Shared Sub PoblarGrupoDocumentos_Confirmar(ByRef GrupoDocumentos As Clases.GrupoDocumentos, ds As DataSet)

            If ds.Tables.Contains("GrupoDocumentos") AndAlso ds.Tables("GrupoDocumentos").Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables("GrupoDocumentos").Rows(0)

                With GrupoDocumentos
                    .Identificador = Util.AtribuirValorObj(Of String)(row("OID_GRUPO_DOCUMENTO"))
                    .Estado = Extenciones.EnumExtension.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO"))
                    .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_CREACION"))
                    .UsuarioCreacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_CREACION"))
                    .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_MODIFICACION"))
                    .CodigoComprobante = Util.AtribuirValorObj(Of String)(row("COD_COMPROBANTE"))
                    .UsuarioModificacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_MODIFICACION"))
                    .Rowver = Util.AtribuirValorObj(Of String)(row("ROWVER"))
                    .Historico = Nothing
                End With

                'historico
                If ds.Tables.Contains("HistoricoMovimientosGrupo") AndAlso ds.Tables("HistoricoMovimientosGrupo").Rows.Count > 0 Then
                    GrupoDocumentos.Historico = CargarHistoricoGrupo(ds.Tables("HistoricoMovimientosGrupo"))
                End If

                GenesisSaldos.Documento.PoblarDocumentos_Confirmar(GrupoDocumentos.Documentos, ds)

            End If

        End Sub

        Public Shared Sub GrabarGrupoDocumentoSalidasRecorrido(peticion As SalirRecorrido.SalirRecorridoPeticion,
                                                                caracteristica_integracion As Enumeradores.CaracteristicaFormulario)

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColectarGrupoDocumentosSalidasRecorrido(peticion, caracteristica_integracion)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

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

        Private Shared Function ColectarGrupoDocumentosSalidasRecorrido(peticion As SalirRecorrido.SalirRecorridoPeticion,
                                                                caracteristica_integracion As Enumeradores.CaracteristicaFormulario) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_elem_salida", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            If Not String.IsNullOrEmpty(peticion.CodigoRuta) Then
                spw.AgregarParam("par$cod_ruta", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoRuta, , False)
            Else
                spw.AgregarParam("par$cod_ruta", ProsegurDbType.Identificador_Alfanumerico, String.Empty, , False)
            End If

            spw.AgregarParam("par$arem_cod_remesa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            If peticion.Origen IsNot Nothing AndAlso peticion.Origen.Elementos IsNot Nothing AndAlso peticion.Origen.Elementos.Count > 0 Then
                For Each remesa In peticion.Origen.Elementos
                    spw.Param("par$arem_cod_remesa").AgregarValorArray(remesa.ReciboTransporte)
                Next
            End If

            spw.AgregarParam("par$fyh_plncertif", ProsegurDbType.Data_Hora, peticion.FechaHoraSolicitud, , False)


            If peticion.Origen IsNot Nothing Then
                spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.Origen.CodigoDelegacion, , False)
                spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.Origen.CodigoPlanta, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.Origen.CodigoSector, , False)
            Else
                spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            End If

            spw.AgregarParam("par$cod_integracion", ProsegurDbType.Descricao_Longa, caracteristica_integracion.RecuperarValor, , False)

            'informacion de ejecución
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoUsuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, 1, , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, 1, , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Public Shared Sub GrabarGrupoDocumentoReenvio(ByRef GrupoDocumentos As Clases.GrupoDocumentos,
                                                      bol_gestion_bulto As Boolean,
                                                      caracteristica_integracion As Enumeradores.CaracteristicaFormulario,
                                                      confirmar_doc As Boolean,
                                                      aceptar_doc As Boolean,
                                             Optional log As StringBuilder = Nothing)

            Dim Tiempo As DateTime = Now
            If log Is Nothing Then
                log = New StringBuilder
            End If

            Try
                Dim ds As DataSet = Nothing

                Tiempo = Now
                Dim spw As SPWrapper = ColectarGrupoDocumentosReenvioAuto(GrupoDocumentos, bol_gestion_bulto, caracteristica_integracion, confirmar_doc, aceptar_doc)
                log.AppendLine("____Tiempo 'AccesoDatos\GrabarGrupoDocumentos\ColectarGrupoDocumentos': " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
                log.AppendLine("____Tiempo 'AccesoDatos\GrabarGrupoDocumentos\EjecutarSP': " & Now.Subtract(Tiempo).ToString() & "; ")

                PoblarGrupoDocumentos_Confirmar(GrupoDocumentos, ds)

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

        Private Shared Function ColectarGrupoDocumentosReenvioAuto(grupoDocumentos As Clases.GrupoDocumentos,
                                                                   bol_gestion_bulto As Boolean,
                                                                   caracteristica_integracion As Enumeradores.CaracteristicaFormulario,
                                                                   confirmar_doc As Boolean,
                                                                   aceptar_doc As Boolean) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.sguardar_grp_docs_elem_reenvi", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            ' Remesa
            spw.AgregarParam("par$aremdoc_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aremdoc_cod_remesa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aremdoc_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

            ' Bulto - arrays
            spw.AgregarParam("par$abuldoc_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abuldoc_cod_bulto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abuldoc_rowver", ProsegurDbType.Inteiro_Longo, Nothing, , True)

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
            spw.AgregarParam("par$bol_gestion_bulto", ParamTypes.Integer, If(bol_gestion_bulto, 1, 0), , False)
            spw.AgregarParam("par$cod_integracion", ProsegurDbType.Descricao_Longa, caracteristica_integracion.RecuperarValor, , False)

            'informacion de ejecución
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, grupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, 1, , False)
            spw.AgregarParam("par$confirmar_doc", ParamTypes.Integer, If(confirmar_doc, 1, 0), , False)
            spw.AgregarParam("par$aceptar_doc", ParamTypes.Integer, If(aceptar_doc, 1, 0), , False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
            spw.AgregarParam("par$doc_rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_documentos")
            spw.AgregarParam("par$doc_rc_historico_mov_doc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_historico_mov_doc")
            spw.AgregarParam("par$ele_rc_elementos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_elementos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            ' Cargar valores de los documentos
            For Each _documento In grupoDocumentos.Documentos

                Dim _remesa As Clases.Remesa = DirectCast(_documento.Elemento, Clases.Remesa)

                ' Remesa
                spw.Param("par$aremdoc_oid_remesa").AgregarValorArray(_remesa.Identificador)
                spw.Param("par$aremdoc_cod_remesa").AgregarValorArray(_remesa.CodigoExterno)
                spw.Param("par$aremdoc_rowver").AgregarValorArray(_remesa.Rowver)

                ' Bulto - arrays
                For Each _bulto In _remesa.Bultos
                    spw.Param("par$abuldoc_oid_bulto").AgregarValorArray(_bulto.Identificador)
                    spw.Param("par$abuldoc_rowver").AgregarValorArray(_bulto.Rowver)
                    If _bulto.Precintos IsNot Nothing AndAlso _bulto.Precintos.Count > 0 Then
                        spw.Param("par$abuldoc_cod_bulto").AgregarValorArray(_bulto.Precintos.FirstOrDefault)
                    Else
                        spw.Param("par$abuldoc_cod_bulto").AgregarValorArray(String.Empty)
                    End If
                Next

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

                ' Movimento origen
                If Not String.IsNullOrWhiteSpace(_documento.CuentaOrigen.Identificador) Then
                    mov_ori_oid_cuenta = _documento.CuentaOrigen.Identificador
                End If
                If _documento.CuentaOrigen.Cliente IsNot Nothing Then
                    mov_ori_oid_client = _documento.CuentaOrigen.Cliente.Identificador
                    mov_ori_cod_client = _documento.CuentaOrigen.Cliente.Codigo
                End If
                If _documento.CuentaOrigen.SubCliente IsNot Nothing Then
                    mov_ori_oid_subcli = _documento.CuentaOrigen.SubCliente.Identificador
                    mov_ori_cod_subcli = _documento.CuentaOrigen.SubCliente.Codigo
                End If
                If _documento.CuentaOrigen.PuntoServicio IsNot Nothing Then
                    mov_ori_oid_ptserv = _documento.CuentaOrigen.PuntoServicio.Identificador
                    mov_ori_cod_ptserv = _documento.CuentaOrigen.PuntoServicio.Codigo
                End If
                If _documento.CuentaOrigen.Canal IsNot Nothing Then
                    mov_ori_oid_canal = _documento.CuentaOrigen.Canal.Identificador
                    mov_ori_cod_canal = _documento.CuentaOrigen.Canal.Codigo
                End If
                If _documento.CuentaOrigen.Cliente IsNot Nothing Then
                    mov_ori_oid_subcan = _documento.CuentaOrigen.SubCanal.Identificador
                    mov_ori_cod_subcan = _documento.CuentaOrigen.SubCanal.Codigo
                End If
                If _documento.CuentaOrigen.Sector IsNot Nothing Then
                    mov_ori_oid_sector = _documento.CuentaOrigen.Sector.Identificador
                    mov_ori_cod_sector = _documento.CuentaOrigen.Sector.Codigo
                    If _documento.CuentaOrigen.Sector.Delegacion IsNot Nothing Then
                        mov_ori_oid_delega = _documento.CuentaOrigen.Sector.Delegacion.Identificador
                        mov_ori_cod_delega = _documento.CuentaOrigen.Sector.Delegacion.Codigo
                    End If
                    If _documento.CuentaOrigen.Sector.Planta IsNot Nothing Then
                        mov_ori_oid_planta = _documento.CuentaOrigen.Sector.Planta.Identificador
                        mov_ori_cod_planta = _documento.CuentaOrigen.Sector.Planta.Codigo
                    End If
                End If

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

                ' Movimento Destino
                If Not String.IsNullOrWhiteSpace(_documento.CuentaDestino.Identificador) Then
                    mov_des_oid_cuenta = _documento.CuentaDestino.Identificador
                End If
                If _documento.CuentaDestino.Cliente IsNot Nothing Then
                    mov_des_oid_client = _documento.CuentaDestino.Cliente.Identificador
                    mov_des_cod_client = _documento.CuentaDestino.Cliente.Codigo
                End If
                If _documento.CuentaDestino.SubCliente IsNot Nothing Then
                    mov_des_oid_subcli = _documento.CuentaDestino.SubCliente.Identificador
                    mov_des_cod_subcli = _documento.CuentaDestino.SubCliente.Codigo
                End If
                If _documento.CuentaDestino.PuntoServicio IsNot Nothing Then
                    mov_des_oid_ptserv = _documento.CuentaDestino.PuntoServicio.Identificador
                    mov_des_cod_ptserv = _documento.CuentaDestino.PuntoServicio.Codigo
                End If
                If _documento.CuentaDestino.Canal IsNot Nothing Then
                    mov_des_oid_canal = _documento.CuentaDestino.Canal.Identificador
                    mov_des_cod_canal = _documento.CuentaDestino.Canal.Codigo
                End If
                If _documento.CuentaDestino.Cliente IsNot Nothing Then
                    mov_des_oid_subcan = _documento.CuentaDestino.SubCanal.Identificador
                    mov_des_cod_subcan = _documento.CuentaDestino.SubCanal.Codigo
                End If
                If _documento.CuentaDestino.Sector IsNot Nothing Then
                    mov_des_oid_sector = _documento.CuentaDestino.Sector.Identificador
                    mov_des_cod_sector = _documento.CuentaDestino.Sector.Codigo
                    If _documento.CuentaDestino.Sector.Delegacion IsNot Nothing Then
                        mov_des_oid_delega = _documento.CuentaDestino.Sector.Delegacion.Identificador
                        mov_des_cod_delega = _documento.CuentaDestino.Sector.Delegacion.Codigo
                    End If
                    If _documento.CuentaDestino.Sector.Planta IsNot Nothing Then
                        mov_des_oid_planta = _documento.CuentaDestino.Sector.Planta.Identificador
                        mov_des_cod_planta = _documento.CuentaDestino.Sector.Planta.Codigo
                    End If
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

            Next


            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

#End Region

#Region " Procedure - Recuperar"

        Public Shared Function recuperarGrupoDocumentos(identificador As String,
                                                        usuario As String,
                                                        ByRef TransaccionActual As DataBaseHelper.Transaccion) As Clases.GrupoDocumentos

            Dim _grupoDocumento As Clases.GrupoDocumentos = Nothing

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColectarGrupoDocumentosRecuperar(identificador, usuario)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                _grupoDocumento = PoblarGrupoDocumentos(ds, spw)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return _grupoDocumento
        End Function

        Public Shared Function ColectarGrupoDocumentosRecuperar(IdentificadorGrupoDocumentos As String, usuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumentos_grp_{0}.srecuperar_grp_documentos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_grupo_documento", ParamTypes.String, IdentificadorGrupoDocumentos, ParameterDirection.Input, False)
            spw.AgregarParam("par$rc_grupo_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoDocumentos")
            spw.AgregarParam("par$rc_grupo_terminos_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "GrupoTerminosGrupo")
            spw.AgregarParam("par$rc_terminos_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "TerminosGrupo")
            spw.AgregarParam("par$rc_valor_terminos_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ValorTerminosGrupo")
            spw.AgregarParam("par$rc_historico_mov_grupo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "HistoricoMovimientosGrupo")
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

        Public Shared Function PoblarGrupoDocumentos(ds As DataSet, spw As SPWrapper) As Clases.GrupoDocumentos
            Dim grp As Clases.GrupoDocumentos = Nothing

            'validar el dataset con el SP wrapper
            If ds IsNot Nothing Then
                grp = CargarGrupoDocumentos(ds)
            End If

            Return grp
        End Function

        Private Shared Function CargarGrupoDocumentos(ds As DataSet) As Clases.GrupoDocumentos
            Dim grp As Clases.GrupoDocumentos = Nothing

            Dim _identificadorDelegacion As New Dictionary(Of String, String)
            Dim _delegaciones As List(Of Clases.Delegacion) = GenesisSaldos.Documento.CargarDelegacion(ds)
            Dim _tipoSectores As List(Of Clases.TipoSector) = GenesisSaldos.Documento.CargarTipoSector(ds)
            Dim _plantas As List(Of Clases.Planta) = GenesisSaldos.Documento.CargarPlanta(ds, _identificadorDelegacion, _tipoSectores)
            Dim _sectores As List(Of Clases.Sector) = GenesisSaldos.Documento.CargarSector(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas)
            Dim _cuentas As List(Of Clases.Cuenta) = GenesisSaldos.Documento.CargarCuenta(ds, _identificadorDelegacion, _delegaciones, _tipoSectores, _plantas, _sectores)

            Dim _gruposIAC As List(Of Clases.GrupoTerminosIAC) = GenesisSaldos.Documento.CargarGrupoTerminosIAC(ds)
            Dim _acciones As List(Of Clases.AccionContable) = GenesisSaldos.Documento.CargarAccionContable(ds)
            Dim _formularios As List(Of Clases.Formulario) = GenesisSaldos.Documento.CargarFormulario(ds, _acciones, _gruposIAC)

            If ds.Tables.Contains("GrupoDocumentos") AndAlso ds.Tables("GrupoDocumentos").Rows.Count > 0 Then
                Dim row As DataRow = ds.Tables("GrupoDocumentos").Rows(0)
                grp = New Clases.GrupoDocumentos With
                {
                    .Identificador = Util.AtribuirValorObj(Of String)(row("OID_GRUPO_DOCUMENTO")),
                    .Estado = Extenciones.EnumExtension.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO")),
                    .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_CREACION")),
                    .UsuarioCreacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_CREACION")),
                    .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_MODIFICACION")),
                    .CodigoComprobante = Util.AtribuirValorObj(Of String)(row("COD_COMPROBANTE")),
                    .UsuarioModificacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_MODIFICACION")),
                    .Rowver = Util.AtribuirValorObj(Of String)(row("ROWVER")),
                    .Historico = Nothing
                }

                'historico
                If ds.Tables.Contains("HistoricoMovimientosGrupo") AndAlso ds.Tables("HistoricoMovimientosGrupo").Rows.Count > 0 Then
                    grp.Historico = CargarHistoricoGrupo(ds.Tables("HistoricoMovimientosGrupo"))
                End If

                If (row("OID_GRUPO_DOCUMENTO_PADRE") IsNot DBNull.Value) Then
                    grp.GrupoDocumentoPadre = New Clases.GrupoDocumentos()
                    grp.GrupoDocumentoPadre.Identificador = Util.AtribuirValorObj(Of String)(row("OID_GRUPO_DOCUMENTO_PADRE"))
                End If

                If (row("OID_FORMULARIO") IsNot DBNull.Value) Then
                    grp.Formulario = _formularios.FirstOrDefault(Function(f) f.Identificador = Util.AtribuirValorObj(Of String)(row("OID_FORMULARIO")))
                    If grp.Formulario IsNot Nothing Then
                        If grp.Formulario.GrupoTerminosIACGrupo Is Nothing Then
                            grp.GrupoTerminosIAC = Nothing
                        Else
                            grp.GrupoTerminosIAC = _gruposIAC.FirstOrDefault(Function(t) t.Identificador = grp.Formulario.GrupoTerminosIACGrupo.Identificador)
                            CargarValoresTerminosIAC(ds, grp.GrupoTerminosIAC.TerminosIAC)
                        End If
                    End If
                End If

                If (row("OID_SECTOR_ORIGEN") IsNot DBNull.Value) Then
                    grp.CuentaOrigen = New Clases.Cuenta() With {.Sector = New Clases.Sector() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_ORIGEN"))}}
                    grp.CuentaOrigen.Sector = _sectores.FirstOrDefault(Function(s) s.Identificador = grp.CuentaOrigen.Sector.Identificador)
                End If

                If (row("OID_SECTOR_DESTINO") IsNot DBNull.Value) Then
                    grp.CuentaDestino = New Clases.Cuenta() With {.Sector = New Clases.Sector() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_DESTINO"))}}
                    grp.CuentaDestino.Sector = _sectores.FirstOrDefault(Function(s) s.Identificador = grp.CuentaDestino.Sector.Identificador)
                End If

                'documentos
                grp.Documentos = GenesisSaldos.Documento.poblarDocumentos(ds, _formularios, _cuentas)

            End If

            Return grp
        End Function

        Private Shared Function CargarHistoricoGrupo(dt As DataTable) As ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
            Dim objHistorico As New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr In dt.Rows
                    objHistorico.Add(New Clases.HistoricoMovimientoDocumento With { _
                                     .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))), _
                                     .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                                     .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                                     .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                                     .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
                                 })
                Next

            End If
            Return objHistorico
        End Function

        Private Shared Function CargarFormulario(ds As DataSet, IdentificadorFormulario As String) As Clases.Formulario
            Dim objFormulario As Clases.Formulario = Nothing

            If ds.Tables.Contains("Formularios") AndAlso ds.Tables("Formularios").Rows.Count > 0 Then
                Dim rFrm() As DataRow = ds.Tables("Formularios").Select("OID_FORMULARIO='" & IdentificadorFormulario & "'")
                If rFrm IsNot Nothing AndAlso rFrm.Count > 0 Then
                    Dim objRow As DataRow = rFrm(0)

                    objFormulario = New Clases.Formulario
                    objFormulario.TipoDocumento = New Clases.TipoDocumento

                    With objFormulario

                        .Identificador = Util.AtribuirValorObj(objRow("OID_FORMULARIO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(objRow("COD_FORMULARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(objRow("DES_FORMULARIO"), GetType(String))

                        If objRow("cod_color") IsNot Nothing Then
                            .Color = System.Drawing.Color.FromName(objRow("cod_color").ToString)
                        End If

                        .Icono = Util.AtribuirValorObj(objRow("BIN_ICONO_FORMULARIO"), GetType(Byte()))
                        .EstaActivo = Util.AtribuirValorObj(objRow("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(objRow("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(objRow("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(objRow("DES_USUARIO_MODIFICACION"), GetType(String))

                        .AccionContable = CargarAccionContable(ds, Util.AtribuirValorObj(objRow("OID_ACCION_CONTABLE"), GetType(String)))

                        .Caracteristicas = CargarCaracteristicasFormulario(ds, .Identificador)

                        'GrupoTerminosIAC individual
                        'Verifica se não é nulo
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_INDIVIDUAL"), GetType(String))) Then
                            .GrupoTerminosIACIndividual = CargarGrupoTerminosIAC(ds, objRow("OID_IAC_INDIVIDUAL"))
                        End If

                        'GrupoTerminosIAC Grupo
                        'Verifica se não é nulo
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_IAC_GRUPO"), GetType(String))) Then
                            .GrupoTerminosIACGrupo = CargarGrupoTerminosIAC(ds, objRow("OID_IAC_GRUPO"))
                        End If

                        ' TipoDocumento
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

                End If
            End If


            Return objFormulario
        End Function

        Private Shared Function CargarAccionContable(ds As DataSet, IdentificadorAccionContable As String) As Clases.AccionContable
            Dim objAccionContable As Clases.AccionContable = Nothing

            If ds.Tables.Contains("AccionesContables") AndAlso ds.Tables("AccionesContables").Rows.Count > 0 Then
                Dim rAcC() As DataRow = ds.Tables("AccionesContables").Select("OID_ACCION_CONTABLE = '" & IdentificadorAccionContable & "'")
                If rAcC IsNot Nothing AndAlso rAcC.Count > 0 Then
                    objAccionContable = New Clases.AccionContable
                    With objAccionContable
                        .Identificador = Util.AtribuirValorObj(rAcC(0)("OID_ACCION_CONTABLE"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(rAcC(0)("COD_ACCION_CONTABLE"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(rAcC(0)("DES_ACCION_CONTABLE"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(rAcC(0)("BOL_ACTIVO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(rAcC(0)("GMT_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(rAcC(0)("GMT_MODIFICACION"), GetType(String))
                        .UsuarioCreacion = Util.AtribuirValorObj(rAcC(0)("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(rAcC(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                        .Acciones = New ObservableCollection(Of Clases.AccionTransaccion)

                        'estados accion contable
                        If ds.Tables.Contains("EstadoAccionesContables") AndAlso ds.Tables("EstadoAccionesContables").Rows.Count > 0 Then
                            Dim rEAcC() As DataRow = ds.Tables("EstadoAccionesContables").Select("OID_ACCION_CONTABLE = '" & IdentificadorAccionContable & "'")
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
                End If
            End If

            Return objAccionContable

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
            If ds.Tables.Contains("CaracteristicasFormularios") AndAlso ds.Tables("CaracteristicasFormularios").Rows.Count > 0 Then
                Dim rCar() As DataRow = ds.Tables("CaracteristicasFormularios").Select("OID_FORMULARIO='" & IdentificadorFormulario & "'")
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

        Private Shared Function CargarGrupoTerminosIAC(ds As DataSet, OID_IAC As String) As Clases.GrupoTerminosIAC
            Dim objGrupoTerminosIAC As Clases.GrupoTerminosIAC = Nothing
            If ds.Tables.Contains("GruposTerminosIndiv") AndAlso ds.Tables("GruposTerminosIndiv").Rows.Count > 0 Then
                Dim rGrpT() As DataRow = ds.Tables("GruposTerminosIndiv").Select("OID_IAC='" & OID_IAC & "'")
                If rGrpT IsNot Nothing AndAlso rGrpT.Count > 0 Then
                    objGrupoTerminosIAC = New Clases.GrupoTerminosIAC
                    Dim row As DataRow = rGrpT(0)
                    With objGrupoTerminosIAC
                        .Identificador = OID_IAC
                        .Codigo = Util.AtribuirValorObj(row("COD_IAC"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_IAC"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(row("OBS_IAC"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                        .EsInvisible = Util.AtribuirValorObj(row("BOL_INVISIBLE"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                        .CopiarDeclarados = Util.AtribuirValorObj(row("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                        .TerminosIAC = CargarTerminosIAC(ds, OID_IAC)
                    End With
                End If
            End If
            Return objGrupoTerminosIAC
        End Function

        Private Shared Function CargarTerminosIAC(ds As DataSet, OID_IAC As String) As ObservableCollection(Of Clases.TerminoIAC)
            Dim listaTerminoIAC As New ObservableCollection(Of Clases.TerminoIAC)
            If ds.Tables.Contains("TerminosIndiv") AndAlso ds.Tables("TerminosIndiv").Rows.Count > 0 Then
                Dim rTrm() As DataRow = ds.Tables("TerminosIndiv").Select("OID_IAC='" & OID_IAC & "'")
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

        Private Shared Sub CargarValoresTerminosIAC(ds As DataSet, Terminos As ObservableCollection(Of Clases.TerminoIAC))
            If ds.Tables.Contains("ValorTerminosGrupo") AndAlso ds.Tables("ValorTerminosGrupo").Rows.Count > 0 Then
                If Terminos IsNot Nothing AndAlso Terminos.Count > 0 Then
                    For Each t As Clases.TerminoIAC In Terminos
                        Dim rTer() As DataRow = ds.Tables("ValorTerminosGrupo").Select("OID_TERMINO='" & t.Identificador & "'")
                        If rTer IsNot Nothing AndAlso rTer.Count > 0 Then
                            t.Valor = Util.AtribuirValorObj(rTer(0)("DES_VALOR"), GetType(String))
                        End If
                    Next
                End If
            End If
        End Sub

        Private Shared Function CargarSector(ds As DataSet, IdentificadorSector As String, Optional IncluirTiposSectoresDePlanta As Boolean = True) As Clases.Sector
            Dim sect As Clases.Sector = Nothing
            If ds.Tables.Contains("Sectores") AndAlso ds.Tables("Sectores").Rows.Count > 0 Then
                Dim rSec() As DataRow = ds.Tables("Sectores").Select("OID_SECTOR='" & IdentificadorSector & "'")
                If rSec IsNot Nothing AndAlso rSec.Count > 0 Then
                    sect = New Clases.Sector
                    Dim IdentificadorDelegacion As String = ""
                    With sect
                        .Identificador = Util.AtribuirValorObj(Of String)(rSec(0)("OID_SECTOR"))
                        .Descripcion = Util.AtribuirValorObj(Of String)(rSec(0)("DES_SECTOR"))
                        .Codigo = Util.AtribuirValorObj(Of String)(rSec(0)("COD_SECTOR"))
                        .CodigoMigracion = Util.AtribuirValorObj(Of String)(rSec(0)("COD_MIGRACION"))
                        .EsActivo = Util.AtribuirValorObj(Of Boolean)(rSec(0)("BOL_ACTIVO"))
                        .EsCentroProceso = Util.AtribuirValorObj(Of Boolean)(rSec(0)("BOL_CENTRO_PROCESO"))
                        .EsConteo = Util.AtribuirValorObj(Of Boolean)(rSec(0)("BOL_CONTEO"))
                        .EsTesoro = Util.AtribuirValorObj(Of Boolean)(rSec(0)("BOL_TESORO"))
                        .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(rSec(0)("GMT_CREACION"))
                        .PemitirDisponerValor = Util.AtribuirValorObj(Of Boolean)(rSec(0)("BOL_PERMITE_DISPONER_VALOR"))
                        .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(rSec(0)("GMT_MODIFICACION"))
                        .UsuarioCreacion = Util.AtribuirValorObj(Of String)(rSec(0)("DES_USUARIO_CREACION"))
                        .UsuarioModificacion = Util.AtribuirValorObj(Of String)(rSec(0)("DES_USUARIO_MODIFICACION"))
                        .Planta = CargarPlanta(ds, Util.AtribuirValorObj(Of String)(rSec(0)("OID_PLANTA")), IdentificadorDelegacion, IncluirTiposSectoresDePlanta)
                        .Delegacion = CargarDelegacion(ds, IdentificadorDelegacion)
                    End With

                    'If CargarCodigosAjenos Then
                    '    Sector.CodigosAjenos = CodigoAjeno.ObtenerCodigosAjenos(Sector.Identificador, String.Empty, String.Empty, Enumeradores.Tabela.Sector.RecuperarValor())
                    'End If

                    'If CargarSectorPadre AndAlso (rSec(0)("OID_SECTOR_PADRE") IsNot DBNull.Value) Then
                    '    Sector.SectorPadre = ObtenerPorOid(AtribuirValorObj(Of String)(rSec(0)("OID_SECTOR_PADRE")))
                    'End If

                    If (rSec(0)("OID_TIPO_SECTOR") IsNot DBNull.Value) Then
                        sect.TipoSector = CargarTipoSector(ds, Util.AtribuirValorObj(Of String)(rSec(0)("OID_TIPO_SECTOR")))
                    End If

                End If

            End If

            Return sect
        End Function

        Private Shared Function CargarTipoSector(ds As DataSet, IdentificadorTipoSector As String) As Clases.TipoSector
            Dim Tsect As Clases.TipoSector = Nothing
            If ds.Tables.Contains("TiposSectores") AndAlso ds.Tables("TiposSectores").Rows.Count > 0 Then
                Dim rTsects() As DataRow = ds.Tables("TiposSectores").Select("OID_TIPO_SECTOR='" & IdentificadorTipoSector & "'")
                If rTsects IsNot Nothing AndAlso rTsects.Count > 0 Then
                    Tsect = New Clases.TipoSector
                    With Tsect
                        .Codigo = Util.AtribuirValorObj(rTsects(0)("COD_TIPO_SECTOR"), GetType(String))
                        .CodigoMigracion = Util.AtribuirValorObj(rTsects(0)("COD_MIGRACION"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(rTsects(0)("DES_TIPO_SECTOR"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(rTsects(0)("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(rTsects(0)("GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(rTsects(0)("GMT_MODIFICACION"), GetType(DateTime))
                        .Identificador = Util.AtribuirValorObj(rTsects(0)("OID_TIPO_SECTOR"), GetType(String))
                        .UsuarioCreacion = Util.AtribuirValorObj(rTsects(0)("DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(rTsects(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                        .CaracteristicasTipoSector = CargarCaracteristicasTipoSector(ds, IdentificadorTipoSector)
                    End With
                End If
            End If

            Return Tsect
        End Function

        Private Shared Function CargarCaracteristicasTipoSector(ds As DataSet, IdentificadorTipoSector As String) As ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
            Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
            If ds.Tables.Contains("CaractTiposSectores") AndAlso ds.Tables("CaractTiposSectores").Rows.Count > 0 Then
                Dim rCar() As DataRow = ds.Tables("CaractTiposSectores").Select("OID_TIPO_SECTOR='" & IdentificadorTipoSector & "'")
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

        Private Shared Function CargarPlanta(ds As DataSet, IdentificadorPlanta As String, Optional ByRef IdentificadorDelegacion As String = Nothing, Optional IncluirTiposSectores As Boolean = True) As Clases.Planta
            Dim Plnt As Clases.Planta = Nothing
            If ds.Tables.Contains("Plantas") AndAlso ds.Tables("Plantas").Rows.Count > 0 Then
                Dim rPlns() As DataRow = ds.Tables("Plantas").Select("OID_PLANTA='" & IdentificadorPlanta & "'")
                If rPlns IsNot Nothing AndAlso rPlns.Count > 0 Then
                    Plnt = New Clases.Planta With
                                    {
                                        .Codigo = Util.AtribuirValorObj(rPlns(0)("COD_PLANTA"), GetType(String)), _
                                        .CodigoMigracion = Util.AtribuirValorObj(rPlns(0)("COD_MIGRACION"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rPlns(0)("DES_PLANTA"), GetType(String)), _
                                        .EsActivo = Util.AtribuirValorObj(rPlns(0)("BOL_ACTIVO"), GetType(Boolean)), _
                                        .FechaHoraCreacion = Util.AtribuirValorObj(rPlns(0)("GMT_CREACION"), GetType(DateTime)), _
                                        .FechaHoraModificacion = Util.AtribuirValorObj(rPlns(0)("GMT_MODIFICACION"), GetType(DateTime)), _
                                        .Identificador = Util.AtribuirValorObj(rPlns(0)("OID_PLANTA"), GetType(String)), _
                                        .TiposSector = IIf(IncluirTiposSectores, CargarTiposSectorPorPlanta(ds, .Identificador), Nothing), _
                                        .UsuarioCreacion = Util.AtribuirValorObj(rPlns(0)("DES_USUARIO_CREACION"), GetType(String)), _
                                        .UsuarioModificacion = Util.AtribuirValorObj(rPlns(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                                    }
                    If IdentificadorDelegacion IsNot Nothing Then
                        IdentificadorDelegacion = Util.AtribuirValorObj(rPlns(0)("OID_DELEGACION"), GetType(String))
                    End If
                End If
            End If

            Return Plnt
        End Function

        Private Shared Function CargarTiposSectorPorPlanta(ds As DataSet, IdentificadorPlanta As String) As ObservableCollection(Of Clases.TipoSector)
            Dim TiposSectores As ObservableCollection(Of Clases.TipoSector) = Nothing
            If ds.Tables.Contains("TiposSectorPorPlanta") AndAlso ds.Tables("TiposSectorPorPlanta").Rows.Count > 0 Then
                Dim rPlns() As DataRow = ds.Tables("TiposSectorPorPlanta").Select("OID_PLANTA='" & IdentificadorPlanta & "'")
                If rPlns IsNot Nothing AndAlso rPlns.Count > 0 Then
                    TiposSectores = New ObservableCollection(Of Clases.TipoSector)
                    For Each row As DataRow In rPlns
                        TiposSectores.Add(CargarTipoSector(ds, Util.AtribuirValorObj(row("OID_TIPO_SECTOR"), GetType(String))))
                    Next
                End If
            End If
            Return TiposSectores
        End Function

        Private Shared Function CargarDelegacion(ds As DataSet, IdentificadorDelegacion As String) As Clases.Delegacion
            Dim Dleg As Clases.Delegacion = Nothing
            If ds.Tables.Contains("Delegaciones") AndAlso ds.Tables("Delegaciones").Rows.Count > 0 Then
                Dim rDlgs() As DataRow = ds.Tables("Delegaciones").Select("OID_DELEGACION='" & IdentificadorDelegacion & "'")
                If rDlgs IsNot Nothing AndAlso rDlgs.Count > 0 Then
                    Dleg = New Clases.Delegacion With
                                    {
                                        .Identificador = Util.AtribuirValorObj(Of String)(rDlgs(0)("OID_DELEGACION")),
                                        .Codigo = Util.AtribuirValorObj(Of String)(rDlgs(0)("COD_DELEGACION")),
                                        .Descripcion = Util.AtribuirValorObj(Of String)(rDlgs(0)("DES_DELEGACION")),
                                        .EsActivo = Util.AtribuirValorObj(Of Boolean)(rDlgs(0)("BOL_VIGENTE")),
                                        .HusoHorarioEnMinutos = Util.AtribuirValorObj(Of Integer)(rDlgs(0)("NEC_GMT_MINUTOS")),
                                        .FechaHoraVeranoInicio = Util.AtribuirValorObj(Of Date)(rDlgs(0)("FYH_VERANO_INICIO")),
                                        .FechaHoraVeranoFin = Util.AtribuirValorObj(Of Date)(rDlgs(0)("FYH_VERANO_FIN")),
                                        .AjusteHorarioVerano = Util.AtribuirValorObj(Of Integer)(rDlgs(0)("NEC_VERANO_AJUSTE")),
                                        .Zona = Util.AtribuirValorObj(Of String)(rDlgs(0)("DES_ZONA")),
                                        .FechaHoraCreacion = Util.AtribuirValorObj(Of Date)(rDlgs(0)("GMT_CREACION")),
                                        .UsuarioCreacion = Util.AtribuirValorObj(Of String)(rDlgs(0)("DES_USUARIO_CREACION")),
                                        .FechaHoraModificacion = Util.AtribuirValorObj(Of Date)(rDlgs(0)("GMT_MODIFICACION")),
                                        .UsuarioModificacion = Util.AtribuirValorObj(Of String)(rDlgs(0)("DES_USUARIO_MODIFICACION")),
                                        .CodigoPais = Util.AtribuirValorObj(Of String)(rDlgs(0)("COD_PAIS"))
                                    }
                End If
            End If

            Return Dleg
        End Function

        Private Shared Function CargarDocumentosGrupo(ds As DataSet) As ObservableCollection(Of Clases.Documento)
            Dim documentos As New ObservableCollection(Of Clases.Documento)
            If ds.Tables.Contains("Documentos") AndAlso ds.Tables("Documentos").Rows.Count > 0 Then
                For Each rowDocumento As DataRow In ds.Tables("Documentos").Rows
                    'separados en carga individual para poder reutilizarla para los documentos padre
                    Dim documento As Clases.Documento = CargarDocumento(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)))
                    documentos.Add(documento)
                Next
            End If

            Return documentos
        End Function

        Private Shared Function CargarDocumento(ds As DataSet, IdentificadorDocumento As String) As Clases.Documento
            Dim doc As Clases.Documento = Nothing
            If Not String.IsNullOrEmpty(IdentificadorDocumento) AndAlso ds.Tables.Contains("Documentos") AndAlso ds.Tables("Documentos").Rows.Count > 0 Then
                Dim rDoc() As DataRow = ds.Tables("Documentos").Select("OID_DOCUMENTO ='" & IdentificadorDocumento & "'")
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
                        .Estado = If(Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                        .ExportadoSol = If(rowDocumento.Table.Columns.Contains("EXPORTADO_SOL") AndAlso Not rowDocumento("EXPORTADO_SOL").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("EXPORTADO_SOL"), GetType(Boolean)), False)
                        .IdentificadorIntegracion = If(rowDocumento.Table.Columns.Contains("OID_INTEGRACION"), Util.AtribuirValorObj(rowDocumento("OID_INTEGRACION"), GetType(String)), Nothing)
                        .NelIntentoEnvio = If(rowDocumento.Table.Columns.Contains("NEL_INTENTO_ENVIO") AndAlso Not rowDocumento("NEL_INTENTO_ENVIO").Equals(DBNull.Value), Util.AtribuirValorObj(rowDocumento("NEL_INTENTO_ENVIO"), GetType(Boolean)), False)
                        .CodigoCertificacionCuentas = Util.AtribuirValorObj(rowDocumento("COD_CERTIFICACION_CUENTAS"), GetType(String))
                        .EstadosPosibles = ObtenerEstadosPossibles(.Estado)

                        .Historico = CargarHistoricosDocumento(ds, .Identificador)
                        .Formulario = CargarFormulario(ds, Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String)))
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                            .TipoDocumento = New Clases.TipoDocumento()
                            .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                        End If
                        .DocumentoPadre = CargarDocumento(ds, Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String)))
                        .CuentaOrigen = CargarCuenta(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String)))
                        .CuentaDestino = CargarCuenta(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String)))
                        .CuentaSaldoOrigen = CargarCuenta(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String)))
                        .CuentaSaldoDestino = CargarCuenta(ds, Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String)))

                        If .Formulario IsNot Nothing AndAlso .Formulario.GrupoTerminosIACIndividual IsNot Nothing Then
                            .GrupoTerminosIAC = .Formulario.GrupoTerminosIACIndividual.Clonar
                            If .GrupoTerminosIAC IsNot Nothing AndAlso .GrupoTerminosIAC.TerminosIAC IsNot Nothing Then
                                CargarValoresTerminosDocumento(ds, .Identificador, .GrupoTerminosIAC.TerminosIAC)
                            End If
                        End If
                        .Rowver = Util.AtribuirValorObj(Of String)(rowDocumento("ROWVER"))
                        'por ahora es para documentos de cierre de caja, no tienen remesa
                        'If rowDocumento.Table.Columns.Contains("OID_REMESA") AndAlso Not String.IsNullOrEmpty(AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))) Then
                        '    .Elemento = New Clases.Remesa()
                        '    .Elemento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_REMESA"), GetType(String))
                        'End If

                        .Divisas = CargarValoresDocumento(ds, IdentificadorDocumento, True)

                    End With

                End If
            End If

            Return doc
        End Function

        Private Shared Function ObtenerEstadosPossibles(ByRef estadoDocumento As Enumeradores.EstadoDocumento) As ObservableCollection(Of Enumeradores.EstadoDocumento)

            'copiada de \Genesis\Prosegur.Genesis.AccesoDatos\GenesisSaldos\Documento.vb (Prosegur.Genesis.Comon.Clases.Documento)
            'pero podría considerarse modificar su visibilidad de Private a Public para no duplicarla

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
            If ds.Tables.Contains("HistoricoMovimientosDocumentos") AndAlso ds.Tables("HistoricoMovimientosDocumentos").Rows.Count > 0 Then
                Dim rHst() As DataRow = ds.Tables("HistoricoMovimientosDocumentos").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
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

        Private Shared Function CargarCuenta(ds As DataSet, IdentificadorCuenta As String) As Clases.Cuenta
            Dim cta As Clases.Cuenta = Nothing

            If Not String.IsNullOrEmpty(IdentificadorCuenta) AndAlso ds.Tables.Contains("Cuentas") AndAlso ds.Tables("Cuentas").Rows.Count > 0 Then
                Dim rCtas() As DataRow = ds.Tables("Cuentas").Select("OID_CUENTA='" & IdentificadorCuenta & "'")
                If rCtas IsNot Nothing AndAlso rCtas.Count > 0 Then
                    Dim rowCuenta As DataRow = rCtas(0)
                    cta = New Clases.Cuenta
                    With cta
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
                        .Cliente = New Clases.Cliente With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_CLIENTE"), GetType(String)), _
                                        .EstaActivo = Util.AtribuirValorObj(rowCuenta("CL_BOL_VIGENTE"), GetType(Boolean)), _
                                        .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("CL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), _
                                        .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("CL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), _
                                        .Identificador = Util.AtribuirValorObj(rowCuenta("OID_CLIENTE"), GetType(String))}

                        ' PTO - Punto Servicio
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))) Then
                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)), _
                                                                            .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_PTO_SERVICIO"), GetType(String)), _
                                                                            .EstaActivo = Util.AtribuirValorObj(rowCuenta("PTO_BOL_VIGENTE"), GetType(Boolean)), _
                                                                            .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("PTO_BOL_ENVIADO_SALDOS"), GetType(Boolean)), _
                                                                            .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("PTO_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), _
                                                                            .Identificador = Util.AtribuirValorObj(rowCuenta("OID_PTO_SERVICIO"), GetType(String))}
                        End If

                        ' SCL - SubCliente
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))) Then
                            .SubCliente = New Clases.SubCliente With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)), _
                                                                      .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_SUBCLIENTE"), GetType(String)), _
                                                                      .EstaActivo = Util.AtribuirValorObj(rowCuenta("SCL_BOL_VIGENTE"), GetType(Boolean)), _
                                                                      .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCuenta("SCL_BOL_ENVIADO_SALDOS"), GetType(Boolean)), _
                                                                      .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCuenta("SCL_BOL_TOTALIZADOR_SALDO"), GetType(Boolean)), _
                                                                      .Identificador = Util.AtribuirValorObj(rowCuenta("OID_SUBCLIENTE"), GetType(String))}
                        End If

                        ' CAN - Canal
                        .Canal = New Clases.Canal With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)), _
                                                        .Identificador = Util.AtribuirValorObj(rowCuenta("OID_CANAL"), GetType(String)), _
                                                        .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_CANAL"), GetType(String)), _
                                                        .EstaActivo = Util.AtribuirValorObj(rowCuenta("CAN_BOL_VIGENTE"), GetType(Boolean))}

                        ' SBC - SubCanal
                        .SubCanal = New Clases.SubCanal With {.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)), _
                                                              .Identificador = Util.AtribuirValorObj(rowCuenta("OID_SUBCANAL"), GetType(String)), _
                                                              .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_SUBCANAL"), GetType(String)), _
                                                              .EstaActivo = Util.AtribuirValorObj(rowCuenta("SBC_BOL_VIGENTE"), GetType(Boolean))}

                        .Sector = CargarSector(ds, Util.AtribuirValorObj(rowCuenta("OID_SECTOR"), GetType(String)), False)

                    End With
                End If
            End If

            Return cta
        End Function

        Private Shared Sub CargarValoresTerminosDocumento(ds As DataSet, IdentificadorDocumento As String, Terminos As ObservableCollection(Of Clases.TerminoIAC))
            If ds.Tables.Contains("ValorTerminosDocumentos") AndAlso ds.Tables("ValorTerminosDocumentos").Rows.Count > 0 Then
                If Terminos IsNot Nothing AndAlso Terminos.Count > 0 Then
                    For Each t As Clases.TerminoIAC In Terminos
                        Dim rTer() As DataRow = ds.Tables("ValorTerminosDocumentos").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "' and OID_TERMINO='" & t.Identificador & "'")
                        If rTer IsNot Nothing AndAlso rTer.Count > 0 Then
                            t.Valor = Util.AtribuirValorObj(rTer(0)("DES_VALOR"), GetType(String))
                        End If
                    Next
                End If
            End If
        End Sub

        Private Shared Function CargarValoresDocumento(ds As DataSet, IdentificadorDocumento As String, _
                                               Optional rellenarTipoValorNoDefinido As Boolean = False, _
                                               Optional esDisponibleNoDefinido As Boolean = False) As ObservableCollection(Of Clases.Divisa)

            Dim Divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            If ds.Tables.Contains("ValoresDocumentos") AndAlso ds.Tables("ValoresDocumentos").Rows.Count > 0 Then
                Dim rVal() As DataRow = ds.Tables("ValoresDocumentos").Select("OID_DOCUMENTO='" & IdentificadorDocumento & "'")
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

                        Dim TipoValor As Enumeradores.TipoValor = If(rellenarTipoValorNoDefinido, Enumeradores.TipoValor.NoDefinido, _
                                                                                                  If(esDisponibleNoDefinido, If(Disponible, Enumeradores.TipoValor.NoDefinido, _
                                                                                                                                            Enumeradores.TipoValor.NoDisponible), _
                                                                                                                             If(Disponible, Enumeradores.TipoValor.Disponible, _
                                                                                                                                            Enumeradores.TipoValor.NoDisponible)))

                        'divisa
                        Dim div As Clases.Divisa = Divisas.FirstOrDefault(Function(d) d.Identificador = OID_DIVISA)
                        If div Is Nothing Then
                            div = CargarDivisa(ds, OID_DIVISA)
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
                                        div.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With { _
                                                                                                        .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla, _
                                                                                                        .TipoValor = TipoValor, _
                                                                                                        .Importe = IMPORTE})

                                    ElseIf Not String.IsNullOrEmpty(COD_NIVEL_DETALLE) AndAlso COD_NIVEL_DETALLE = Enumeradores.TipoNivelDetalhe.TotalGeral.RecuperarValor Then
                                        If div.ValoresTotalesDivisa Is Nothing Then
                                            div.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                        End If
                                        div.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With { _
                                                                             .TipoValor = TipoValor, _
                                                                             .Importe = IMPORTE})
                                    End If
                                Else
                                    If div.ValoresTotalesTipoMedioPago Is Nothing Then
                                        div.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                    End If
                                    div.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With { _
                                                                              .TipoValor = TipoValor, _
                                                                              .Importe = IMPORTE, _
                                                                              .Cantidad = CANTIDAD, _
                                                                              .TipoMedioPago = If(Not String.IsNullOrEmpty(COD_TIPO_MEDIO_PAGO), _
                                                                                                  Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(COD_TIPO_MEDIO_PAGO), _
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
                                If den Is Nothing Then
                                    den = CargarDenominacion(ds, OID_DIVISA, OID_DENOMINACION)
                                    div.Denominaciones.Add(den)
                                End If
                                If den IsNot Nothing Then
                                    If den.ValorDenominacion Is Nothing Then
                                        den.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                                    End If
                                    Dim objValor As New Clases.ValorDenominacion
                                    objValor.Cantidad = CANTIDAD
                                    objValor.Importe = IMPORTE
                                    objValor.TipoValor = TipoValor

                                    If Not String.IsNullOrEmpty(OID_CALIDAD) AndAlso ds.Tables.Contains("Calidad") AndAlso ds.Tables("Calidad").Rows.Count > 0 Then
                                        Dim calidad() As DataRow = ds.Tables("Calidad").Select("OID_CALIDAD = '" & OID_CALIDAD & "'")
                                        If calidad IsNot Nothing AndAlso calidad.Count > 0 Then
                                            objValor.Calidad = New Clases.Calidad With {
                                                            .Identificador = Util.AtribuirValorObj(calidad(0)("OID_CALIDAD"), GetType(String)), _
                                                            .Codigo = Util.AtribuirValorObj(calidad(0)("COD_CALIDAD"), GetType(String)), _
                                                            .Descripcion = Util.AtribuirValorObj(calidad(0)("DES_CALIDAD"), GetType(String))}
                                        End If
                                    End If

                                    If Not String.IsNullOrEmpty(OID_UNIDAD_MEDIDA) AndAlso ds.Tables.Contains("UnidadesMedida") AndAlso ds.Tables("UnidadesMedida").Rows.Count > 0 Then
                                        Dim unidadMedida() = ds.Tables("UnidadesMedida").Select("OID_UNIDAD_MEDIDA = '" & OID_UNIDAD_MEDIDA & "'")
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
                                    If mp.Terminos IsNot Nothing AndAlso ds.Tables.Contains("TerminosMediosPago") AndAlso ds.Tables("TerminosMediosPago").Rows.Count > 0 Then
                                        Dim valorTermino() As DataRow = ds.Tables("TerminosMediosPago").Select("OID_DOCUMENTO = '" & IdentificadorDocumento & "' ")
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
                                    mp.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = CANTIDAD, _
                                                                                             .Importe = IMPORTE, _
                                                                                             .Terminos = valoresTerminos, _
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

        Private Shared Function CargarDivisa(ds As DataSet, IdentificadorDivisa As String) As Clases.Divisa
            Dim div As Clases.Divisa = Nothing
            If ds.Tables.Contains("Divisas") AndAlso ds.Tables("Divisas").Rows.Count > 0 Then
                Dim rDiv() As DataRow = ds.Tables("Divisas").Select("OID_DIVISA='" & IdentificadorDivisa & "'")
                If rDiv IsNot Nothing AndAlso rDiv.Count > 0 Then
                    Dim rowDivisa As DataRow = rDiv(0)
                    div = New Clases.Divisa
                    With div
                        .ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)

                        .Identificador = Util.AtribuirValorObj(rowDivisa("OID_DIVISA"), GetType(String))
                        .CodigoISO = Util.AtribuirValorObj(rowDivisa("COD_ISO_DIVISA"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(rowDivisa("DES_DIVISA"), GetType(String))
                        .CodigoSimbolo = Util.AtribuirValorObj(rowDivisa("COD_SIMBOLO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(rowDivisa("BOL_VIGENTE"), GetType(Boolean))
                        .CodigoUsuario = Util.AtribuirValorObj(rowDivisa("COD_USUARIO"), GetType(String))
                        .FechaHoraTransporte = Util.AtribuirValorObj(rowDivisa("FYH_ACTUALIZACION"), GetType(DateTime))
                        .CodigoAcceso = Util.AtribuirValorObj(rowDivisa("COD_ACCESO"), GetType(String))
                        .Color = Util.AtribuirValorObj(rowDivisa("COD_COLOR"), GetType(Drawing.Color))
                    End With
                End If
            End If

            Return div
        End Function

        Private Shared Function CargarDenominacion(ds As DataSet, IdentificadorDivisa As String, IdentificadorDenominacion As String) As Clases.Denominacion
            Dim den As Clases.Denominacion = Nothing
            If ds.Tables.Contains("Denominaciones") AndAlso ds.Tables("Denominaciones").Rows.Count > 0 Then
                Dim rDen() As DataRow = ds.Tables("Denominaciones").Select("OID_DIVISA='" & IdentificadorDivisa & "' and OID_DENOMINACION = '" & IdentificadorDenominacion & "'")
                If rDen IsNot Nothing AndAlso rDen.Count > 0 Then
                    Dim rowDenominacion As DataRow = rDen(0)
                    den = New Clases.Denominacion
                    With den
                        .Identificador = Util.AtribuirValorObj(rowDenominacion("OID_DENOMINACION"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(rowDenominacion("COD_DENOMINACION"), GetType(String))
                        .CodigoUsuario = Util.AtribuirValorObj(rowDenominacion("COD_USUARIO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(rowDenominacion("DES_DENOMINACION"), GetType(String))
                        .EsBillete = Util.AtribuirValorObj(rowDenominacion("BOL_BILLETE"), GetType(Boolean))
                        .EstaActivo = Util.AtribuirValorObj(rowDenominacion("BOL_VIGENTE"), GetType(Boolean))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(rowDenominacion("FYH_ACTUALIZACION"), GetType(DateTime))
                        .Valor = Util.AtribuirValorObj(rowDenominacion("NUM_VALOR"), GetType(Decimal))
                    End With
                End If
            End If

            Return den
        End Function

        Private Shared Function CargarMedioPago(ds As DataSet, IdentificadorDivisa As String, IdentificadorMedioPago As String) As Clases.MedioPago
            Dim mp As Clases.MedioPago = Nothing
            If ds.Tables.Contains("MediosPago") AndAlso ds.Tables("MediosPago").Rows.Count > 0 Then
                Dim rMp() As DataRow = ds.Tables("MediosPago").Select("OID_DIVISA='" & IdentificadorDivisa & "' and OID_MEDIO_PAGO = '" & IdentificadorMedioPago & "'")
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

        Private Shared Function CargarTerminoMedioPago_ValoresPosibles(ds As DataSet, IdentificadorDivisa As String, IdentificadorMedioPago As String) As ObservableCollection(Of Clases.TerminoValorPosible)
            Dim ValoresPosibles As ObservableCollection(Of Clases.TerminoValorPosible) = Nothing
            If ds.Tables.Contains("TerminosMediosPago") AndAlso ds.Tables("TerminosMediosPago").Rows.Count > 0 Then
                '?????????????????????????????????????????????


            End If

            Return ValoresPosibles
        End Function

#End Region

#Region " Procedure - Confirmar/Aceptar/Anular/Rechazar"

        Shared Sub TransacionesGrupoDocumentos(GrupoDocumentos As Clases.GrupoDocumentos,
                                               hacer_commit As Boolean,
                                               ByRef TransaccionActual As DataBaseHelper.Transaccion)

            Try
                Dim spw As SPWrapper = ColectarGrupoDocumentosTransaciones(GrupoDocumentos, hacer_commit)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                GrupoDocumentos.CodigoComprobante = spw.Param("par$cod_comprobante").Valor.ToString
                GrupoDocumentos.Estado = RecuperarEnum(Of Enumeradores.EstadoDocumento)(spw.Param("par$cod_estado_documento").Valor.ToString)
                GrupoDocumentos.Rowver = spw.Param("par$rowver").Valor.ToString

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

        Private Shared Function ColectarGrupoDocumentosTransaciones(GrupoDocumentos As Clases.GrupoDocumentos, hacer_commit As Boolean) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_ptransiciones_{0}.sejecutar_grp_doc", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            'grupo de documentos
            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, GrupoDocumentos.Identificador, , False)
            spw.AgregarParam("par$cod_estado_documento", ProsegurDbType.Identificador_Alfanumerico, GrupoDocumentos.Estado.RecuperarValor, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Inteiro_Longo, GrupoDocumentos.Rowver, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, GrupoDocumentos.UsuarioModificacion, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$hacer_commit", ParamTypes.Integer, If(hacer_commit, 1, 0), , False)
            spw.AgregarParam("par$inserts", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$deletes", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$selects", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

#End Region


#Region "[INSERIR]"


        Public Shared Sub GuardarGrupoDocumentoContenedor(objGrupoDocumentos As Clases.GrupoDocumentos,
                                                          ByRef CodigoComprobante As String)

            Try

                Dim spw As SPWrapper = RellenarParametrosArmarContenedores(objGrupoDocumentos)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                CodigoComprobante = If(spw.Param("par$cod_comprobante").Valor IsNot Nothing, spw.Param("par$cod_comprobante").Valor.ToString, String.Empty)

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

        Private Shared Function RellenarParametrosArmarContenedores(objGrupoDocumentos As Clases.GrupoDocumentos) As SPWrapper

            Dim SP As String = Constantes.SP_GUARDAR_GRUPO_DOCUMENTO_CONTENEDOR_ALTA
            Dim spw As New SPWrapper(SP, True)

            spw.AgregarParam("par$oid_grupo_documento", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString, , False)
            spw.AgregarParam("par$oid_grupo_documento_padre", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            spw.AgregarParam("par$oid_formulario_grupo", ProsegurDbType.Objeto_Id, objGrupoDocumentos.Formulario.Identificador, , False)
            spw.AgregarParam("par$cod_formulario_grupo", ProsegurDbType.Identificador_Alfanumerico, objGrupoDocumentos.Formulario.Codigo, , False)
            spw.AgregarParam("par$oid_sector_origen", ProsegurDbType.Objeto_Id, objGrupoDocumentos.SectorOrigen.Identificador, , False)
            spw.AgregarParam("par$oid_sector_destino", ProsegurDbType.Objeto_Id, objGrupoDocumentos.SectorDestino.Identificador, , False)
            spw.AgregarParam("par$cod_comprobante", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rowver", ProsegurDbType.Objeto_Id, String.Empty, , False)

            spw.AgregarParam("par$avtgdoc_oid_termino", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avtgdoc_des_valor", ProsegurDbType.Descricao_Longa, Nothing, , True)

            spw.AgregarParam("par$adoc_oid_documento", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_documento_padre", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_documento_sust", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_oid_moviment_fondo", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$adoc_fyh_plan_certif", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$adoc_fyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
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


            spw.AgregarParam("par$usuario", ProsegurDbType.Identificador_Alfanumerico, objGrupoDocumentos.UsuarioCreacion.ToUpper, , False)
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


            Dim objContenedor As Clases.Contenedor = Nothing

            For Each Documento In objGrupoDocumentos.Documentos

                objContenedor = Nothing

                Dim IdentificadorDocumento As String = Guid.NewGuid.ToString

                If Documento.Elemento IsNot Nothing Then
                    objContenedor = TryCast(Documento.Elemento, Clases.Contenedor)
                End If

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

            Next

            Return spw
        End Function

        ''' <summary>
        ''' Insere o grupo de documentos
        ''' </summary>
        ''' <param name="GrupoDocumentos"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirGrupoDocumentos(GrupoDocumentos As Clases.GrupoDocumentos, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentosInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, GrupoDocumentos.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, GrupoDocumentos.Formulario.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_ORIGEN", ProsegurDbType.Objeto_Id, GrupoDocumentos.SectorOrigen.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_DESTINO", ProsegurDbType.Objeto_Id, GrupoDocumentos.SectorDestino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(Enumeradores.EstadoDocumento.EnCurso)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, usuario))

            'Verifica se grupo documento possui um documento padre.
            If GrupoDocumentos.GrupoDocumentoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, GrupoDocumentos.GrupoDocumentoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Atualiza o grupo de documentos
        ''' </summary>
        ''' <param name="GrupoDocumentos"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarGrupoDocumento(GrupoDocumentos As Clases.GrupoDocumentos, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As New System.Text.StringBuilder

            cmd.CommandText = My.Resources.GrupoDocumentosActualizar
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, GrupoDocumentos.Identificador))

            With GrupoDocumentos

                If Not String.IsNullOrEmpty(.Formulario.Identificador) Then
                    query.AppendLine(" ,OID_FORMULARIO = []OID_FORMULARIO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, .Formulario.Identificador))
                End If

                If Not String.IsNullOrEmpty(.SectorOrigen.Identificador) Then
                    query.AppendLine(" ,OID_SECTOR_ORIGEN = []OID_SECTOR_ORIGEN")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_ORIGEN", ProsegurDbType.Objeto_Id, .SectorOrigen.Identificador))
                End If

                If Not String.IsNullOrEmpty(.SectorDestino.Identificador) Then
                    query.AppendLine(" ,OID_SECTOR_DESTINO = []OID_SECTOR_DESTINO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_DESTINO", ProsegurDbType.Objeto_Id, .SectorDestino.Identificador))
                End If

                If Not String.IsNullOrEmpty(Extenciones.RecuperarValor(.Estado)) Then
                    query.AppendLine(" ,COD_ESTADO = []COD_ESTADO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(.Estado)))
                End If

                If Not String.IsNullOrEmpty(.CodigoComprobante) Then
                    query.AppendLine(" ,COD_COMPROBANTE = []COD_COMPROBANTE")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, .CodigoComprobante))
                End If

            End With

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, If(query.Length > 0, query.ToString, String.Empty)))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

        ''' <summary>
        ''' Atualiza o código comprovante do grupo documento
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">Identificador do documento</param>
        ''' <param name="codigoComprobante">Código Comprovante</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCodigoComprobanteGrupoDocumento(identificadorGrupoDocumento As String, codigoComprobante As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentosActualizaCodigoComprobante)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorGrupoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Curta, codigoComprobante))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Atualiza o Bol_Impreso do grupo documento
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">Identificador do documento</param>
        ''' <param name="codigoComprobante">Código codigoComprobante</param>
        ''' <param name="impreso">Impreso</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarBolImpreso(identificadorGrupoDocumento As String, codigoComprobante As String,
                                               impreso As Boolean)

            Dim cmdGrupo As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim cmdIndividuales As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmdGrupo.CommandText = My.Resources.GrupoDocumentosActualizaBolImpreso
            cmdGrupo.CommandType = CommandType.Text

            'A consulta está dessa forma para utilizar o identificador ou código comprovante como filtro
            cmdIndividuales.CommandText = My.Resources.GrupoDocumentosActualizaBolImpresoIndividuales
            cmdIndividuales.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then

                cmdGrupo.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdGrupo.CommandText, " GRU.OID_GRUPO_DOCUMENTO = []OID_GRUPO_DOCUMENTO "))
                cmdGrupo.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorGrupoDocumento))

                cmdIndividuales.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdIndividuales.CommandText, " GRU.OID_GRUPO_DOCUMENTO = []OID_GRUPO_DOCUMENTO "))
                cmdIndividuales.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorGrupoDocumento))

            ElseIf Not String.IsNullOrEmpty(codigoComprobante) Then
                cmdGrupo.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdGrupo.CommandText, " GRU.COD_COMPROBANTE = []COD_COMPROBANTE "))
                cmdGrupo.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, codigoComprobante))

                cmdIndividuales.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdIndividuales.CommandText, " GRU.COD_COMPROBANTE = []COD_COMPROBANTE "))
                cmdIndividuales.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, codigoComprobante))

            End If

            cmdGrupo.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_IMPRESO", ProsegurDbType.Logico, impreso))
            cmdIndividuales.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_IMPRESO", ProsegurDbType.Logico, impreso))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmdGrupo)
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmdIndividuales)

        End Sub

#End Region

#Region "[OBTENER]"


        ''' <summary>
        ''' Atualiza o Bol_Impreso do grupo documento
        ''' </summary>
        ''' <param name="identificadorGrupoDocumento">Identificador do documento</param>
        ''' <param name="codigoComprobante">Código codigoComprobante</param>
        ''' <remarks></remarks>
        Public Shared Function RecuperarRowVerGrupoDocumento(identificadorGrupoDocumento As String, codigoComprobante As String) As Integer

            Dim cmdGrupo As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim cmdIndividuales As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmdGrupo.CommandText = My.Resources.GrupoDocumentoRecuperarRowVer
            cmdGrupo.CommandType = CommandType.Text


            If Not String.IsNullOrEmpty(identificadorGrupoDocumento) Then

                cmdGrupo.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdGrupo.CommandText, " GRU.OID_GRUPO_DOCUMENTO = []OID_GRUPO_DOCUMENTO "))
                cmdGrupo.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorGrupoDocumento))

            ElseIf Not String.IsNullOrEmpty(codigoComprobante) Then
                cmdGrupo.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmdGrupo.CommandText, " GRU.COD_COMPROBANTE = []COD_COMPROBANTE "))
                cmdGrupo.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Identificador_Alfanumerico, codigoComprobante))

            End If

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmdGrupo)
        End Function

        ''' <summary>
        ''' Método que obtém o GrupoDocumentos pelo seu oid.
        ''' </summary>
        ''' <param name="oid">OID utilizado na consulta.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String) As Clases.GrupoDocumentos
            Dim grupoDocumentos As Clases.GrupoDocumentos = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pOID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, oid))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerGrupoDocumentosPorOid)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                If (dt.Rows.Count > 0) Then
                    grupoDocumentos = CargarGrupoDocumentos(dt.Rows(0))

                End If
            End Using

            Return grupoDocumentos
        End Function

        ''' <summary>
        ''' Método que obtém o GrupoDocumentos pelo seu oid padre.
        ''' </summary>
        ''' <param name="oid">OID utilizado na consulta.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOidPadre(oid As String) As Clases.GrupoDocumentos
            Dim grupoDocumentos As Clases.GrupoDocumentos = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPO_DOCUMENTO_PADRE", ProsegurDbType.Objeto_Id, oid))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentosPorIDPadre)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                If (dt.Rows.Count > 0) Then
                    grupoDocumentos = CargarGrupoDocumentos(dt.Rows(0))
                End If
            End Using

            Return grupoDocumentos
        End Function

        ''' <summary>
        ''' Recupera o identificador do grupo documento pelo codigo comprobante.
        ''' </summary>
        ''' <param name="CodigoComprobante"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarIdentificadorPorCodigoComprobante(CodigoComprobante As String) As String

            Dim identificadorDocumento As String = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentosRecuperarIdentificadorPorCodigoComprobante)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Longa, CodigoComprobante))

                identificadorDocumento = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return identificadorDocumento

        End Function

        Public Shared Function ConsultaGrupoDocumentos(Peticion As ContractoServicio.Contractos.GenesisSaldos.GrupoDocumento.ConsultaGrupoDocumentos.Peticion) As ObservableCollection(Of Clases.GrupoDocumentos)

            Dim objGrupoDocumento As Clases.GrupoDocumentos = Nothing
            Dim grupoDocumentos As ObservableCollection(Of Clases.GrupoDocumentos) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentoConsultaDocumentos)
                cmd.CommandType = CommandType.Text

                PreparaQueryConsultaDocumentos(Peticion, cmd)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    grupoDocumentos = New ObservableCollection(Of Clases.GrupoDocumentos)

                    For Each dr In dt.Rows
                        objGrupoDocumento = New Clases.GrupoDocumentos
                        With objGrupoDocumento

                            'Campos obrigatórios..
                            .Identificador = Util.AtribuirValorObj(dr("OID_GRUPO_DOCUMENTO"), GetType(String))


                            'O formulário será recuperado na classe de negocio.
                            .Formulario = Formulario.ObtenerFormulario(Util.AtribuirValorObj(dr("OID_FORMULARIO"), GetType(String)))

                            If (dr("OID_SECTOR_ORIGEN") IsNot DBNull.Value) Then
                                .CuentaOrigen = New Clases.Cuenta() With {.Sector = AccesoDatos.Genesis.Sector.ObtenerPorOid(Util.AtribuirValorObj(Of String)(dr("OID_SECTOR_ORIGEN")))}
                            End If

                            If (dr("OID_SECTOR_DESTINO") IsNot DBNull.Value) Then
                                .CuentaDestino = New Clases.Cuenta() With {.Sector = AccesoDatos.Genesis.Sector.ObtenerPorOid(Util.AtribuirValorObj(Of String)(dr("OID_SECTOR_DESTINO")))}
                            End If

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

                        End With

                        grupoDocumentos.Add(objGrupoDocumento)
                    Next
                End If

            End Using

            Return grupoDocumentos

        End Function

        Public Shared Sub PreparaQueryConsultaDocumentos(Peticion As ContractoServicio.Contractos.GenesisSaldos.GrupoDocumento.ConsultaGrupoDocumentos.Peticion, _
                                                   ByRef comando As IDbCommand)

            Dim filtroCuentaOrigen As String = String.Empty
            Dim filtroCuentaDestino As String = String.Empty
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


                'CUENTA ORIGEN
                If Not String.IsNullOrEmpty(Peticion.SubCanal) Then
                    filtroCuentaOrigen = " AND SUBCANALOR.COD_SUBCANAL = []COD_SUBCANAL "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCanal))
                End If
                If Not String.IsNullOrEmpty(Peticion.Canal) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND CANALOR.COD_CANAL = []COD_CANAL "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.Canal))
                End If
                If Not String.IsNullOrEmpty(Peticion.PuntoServicio) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND PTOSERVOR.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.PuntoServicio))
                End If
                If Not String.IsNullOrEmpty(Peticion.SubCliente) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND SUBCLIOR.COD_SUBCLIENTE = []COD_SUBCLIENTE "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCliente))
                End If
                If Not String.IsNullOrEmpty(Peticion.Cliente) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND CLIOR.COD_CLIENTE = []COD_CLIENTE "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Cliente))
                End If
                If Not String.IsNullOrEmpty(Peticion.Sector) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND SEOR.COD_SECTOR = []COD_SECTOR "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Peticion.Sector))
                End If
                If Not String.IsNullOrEmpty(Peticion.Planta) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND PLOR.COD_PLANTA = []COD_PLANTA "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, Peticion.Planta))
                End If
                If Not String.IsNullOrEmpty(Peticion.Delegacion) Then
                    filtroCuentaOrigen = filtroCuentaOrigen & " AND DELOR.COD_DELEGACION = []COD_DELEGACION "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Delegacion))
                End If

                'CUENTA DESTINO
                If Not String.IsNullOrEmpty(Peticion.SubCanal) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND SUBCANALDE.COD_SUBCANAL = []COD_SUBCANAL "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCanal))
                End If

                If Not String.IsNullOrEmpty(Peticion.Canal) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND CANALDE.COD_CANAL = []COD_CANAL "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, Peticion.Canal))
                End If

                If Not String.IsNullOrEmpty(Peticion.PuntoServicio) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND PTOSERVDE.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.PuntoServicio))
                End If
                If Not String.IsNullOrEmpty(Peticion.SubCliente) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND SUBCLIDE.COD_SUBCLIENTE = []COD_SUBCLIENTE "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.SubCliente))
                End If
                If Not String.IsNullOrEmpty(Peticion.Cliente) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND CLIDE.COD_CLIENTE = []COD_CLIENTE "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.Cliente))
                End If
                If Not String.IsNullOrEmpty(Peticion.Sector) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND SEDE.COD_SECTOR = []COD_SECTOR "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Peticion.Sector))
                End If
                If Not String.IsNullOrEmpty(Peticion.Planta) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND PLDE.COD_PLANTA = []COD_PLANTA "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, Peticion.Planta))
                End If

                If Not String.IsNullOrEmpty(Peticion.Delegacion) Then
                    filtroCuentaDestino = filtroCuentaDestino & " AND DELDE.COD_DELEGACION = []COD_DELEGACION "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.Delegacion))
                End If

            End If

            comando.CommandText = String.Format(comando.CommandText, filtro, filtroCuentaOrigen, filtroCuentaDestino)

        End Sub

        Public Shared Function PreparaFiltroEstadosDocumento(estadosDocumento As ObservableCollection(Of Enumeradores.EstadoDocumento), _
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
        ''' Recupera as caracteriticas do formulario do grupo de documento
        ''' </summary>
        ''' <param name="codigoComprobante"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarCaracteristicasPorCodigoComprobante(codigoComprobante As String) As Clases.GrupoDocumentos
            Dim grupoDocumentos As Clases.GrupoDocumentos = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_COMPROBANTE", ProsegurDbType.Descricao_Curta, codigoComprobante))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GrupoDocumentosRecuperarCaracteristicasPorCodigoComprobante)

                Using dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                    If (dt.Rows.Count > 0) Then
                        Dim listaCaracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)
                        grupoDocumentos = New Clases.GrupoDocumentos
                        grupoDocumentos.Formulario = New Clases.Formulario
                        grupoDocumentos.Formulario.Identificador = Util.AtribuirValorObj(Of String)(dt.Rows(0)("OID_FORMULARIO"))

                        For Each row In dt.Rows
                            'Verifica se o Enum existe antes de add
                            'Quando voltava versão e havia alguma característica salva no BD que não existia no Enum dava erro, pois na versão que voltou não existia o Enum
                            If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(row("COD_CARACT_FORMULARIO")) Then
                                listaCaracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(row("COD_CARACT_FORMULARIO")))
                            End If
                        Next

                        grupoDocumentos.Formulario.Caracteristicas = listaCaracteristicas
                    End If
                End Using
            End Using

            Return grupoDocumentos
        End Function

        Private Shared Function CargarGrupoDocumentos(row As DataRow) As Clases.GrupoDocumentos
            Dim grupoDocumentos As New Clases.GrupoDocumentos With
            {
                .Identificador = Util.AtribuirValorObj(Of String)(row("OID_GRUPO_DOCUMENTO")),
                .Estado = Extenciones.EnumExtension.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO")),
                .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_CREACION")),
                .UsuarioCreacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_CREACION")),
                .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(row("GMT_MODIFICACION")),
                .CodigoComprobante = Util.AtribuirValorObj(Of String)(row("COD_COMPROBANTE")),
                .UsuarioModificacion = Util.AtribuirValorObj(Of String)(row("DES_USUARIO_MODIFICACION")),
                .Historico = AccesoDatos.GenesisSaldos.HistoricoMovimentoGrupoDocumentos.RecuperarHistorico(.Identificador)
            }

            If (row("OID_GRUPO_DOCUMENTO_PADRE") IsNot DBNull.Value) Then
                grupoDocumentos.GrupoDocumentoPadre = New Clases.GrupoDocumentos()
                grupoDocumentos.GrupoDocumentoPadre.Identificador = Util.AtribuirValorObj(Of String)(row("OID_GRUPO_DOCUMENTO_PADRE"))
            End If

            If (row("OID_FORMULARIO") IsNot DBNull.Value) Then
                grupoDocumentos.Formulario = New Clases.Formulario() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_FORMULARIO"))}
            End If

            If (row("OID_SECTOR_ORIGEN") IsNot DBNull.Value) Then
                grupoDocumentos.CuentaOrigen = New Clases.Cuenta() With {.Sector = New Clases.Sector() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_ORIGEN"))}}
            End If

            If (row("OID_SECTOR_DESTINO") IsNot DBNull.Value) Then
                grupoDocumentos.CuentaDestino = New Clases.Cuenta() With {.Sector = New Clases.Sector() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_DESTINO"))}}
            End If

            Return grupoDocumentos
        End Function

        Shared Function ObtenerEstadoDelGrupoDocumentos(IdentificadorGrupoDocumento As String) As Enumeradores.EstadoDocumento
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarEstadoGrupoDocumento)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_GRUPODOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorGrupoDocumento))

                Dim objEstado As String = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

                If Not String.IsNullOrEmpty(objEstado) Then
                    Return RecuperarEnum(Of Enumeradores.EstadoDocumento)(objEstado)
                End If

            End Using
            Return Nothing
        End Function

#End Region


    End Class

End Namespace
