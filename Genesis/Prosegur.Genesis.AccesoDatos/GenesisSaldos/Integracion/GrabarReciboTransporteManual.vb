Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Integracion

    Public Class GrabarReciboTransporteManual

        Public Shared Function Grabar(remesas As List(Of Clases.Remesa)) As List(Of String)

            Dim codigoRepetidos As List(Of String)

            Try

                Dim spw As SPWrapper = ColectarRemesas(remesas)
                Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False)

                codigoRepetidos = RecuperarRecibosRepetidos(ds)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return codigoRepetidos

        End Function

        Private Shared Function ColectarRemesas(remesas As List(Of Clases.Remesa)) As SPWrapper

            Dim SP As String = String.Format("sapr_pdocumento_{0}.sgrabar_recibo_transp_manual", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$arem_oid_remesa", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$arem_cod_recibo_transp", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$arem_cod_estado", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$arem_oid_ot", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$fyh_actualizacion", ProsegurDbType.Data_Hora, remesas.FirstOrDefault.FechaHoraModificacion, , False)
            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, remesas.FirstOrDefault.CodigoDelegacion, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, remesas.FirstOrDefault.UsuarioModificacion, , False)
            spw.AgregarParam("par$rem_rc_rec_duplicado", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "RecibosDuplicados")

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            For Each remesa In remesas
                spw.Param("par$arem_oid_remesa").AgregarValorArray(remesa.Identificador)
                spw.Param("par$arem_cod_recibo_transp").AgregarValorArray(remesa.CodigoReciboSalida)
                spw.Param("par$arem_cod_estado").AgregarValorArray(remesa.Estado.RecuperarValor())
                spw.Param("par$arem_oid_ot").AgregarValorArray(remesa.IdentificadorOT)
            Next

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Private Shared Function RecuperarRecibosRepetidos(ds As DataSet) As List(Of String)
            Dim recibosDuplicados As List(Of String) = Nothing

            'validar el dataset con el SP wrapper
            If ds IsNot Nothing Then

                If ds.Tables.Contains("RecibosDuplicados") AndAlso ds.Tables("RecibosDuplicados").Rows.Count > 0 Then
                    recibosDuplicados = New List(Of String)
                    For Each row As DataRow In ds.Tables("RecibosDuplicados").Rows
                        Dim codigoRecibo As String = Util.AtribuirValorObj(row("OID"), GetType(String))
                        If Not recibosDuplicados.Contains(codigoRecibo) Then
                            recibosDuplicados.Add(codigoRecibo)
                        End If
                    Next
                End If

                Return recibosDuplicados

            End If
            Return recibosDuplicados

        End Function

    End Class

End Namespace