Imports System.Data.OracleClient
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ModificarMovimientos
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Peticion = Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ModificarMovimientos.Peticion
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio

Namespace Integracion
    Public Class ModificarMovimientos
        Public Shared Sub ActualizarExtradata(identificadorLlamada As String, movimientos As List(Of Entrada.Movimiento), peticion As Peticion, ByRef respuesta As Respuesta)

            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ArmarPeticion(identificadorLlamada, movimientos, peticion)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                PoblarRespuesta(ds, peticion, respuesta)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub


        Private Shared Function ArmarPeticion(identificadorLlamada As String, movimientos As List(Of Entrada.Movimiento), peticion As Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.smodificar_movimiento", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_externos", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cods_extr_campo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cods_extr_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_tipo_resultado", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "tipo_resultado")

            'spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)


            For Each movimiento In movimientos
                For Each extradata In movimiento.CamposExtra
                    spw.Param("par$cod_externos").AgregarValorArray(movimiento.CodigoExterno)
                    spw.Param("par$cods_extr_campo").AgregarValorArray(extradata.Campo)
                    spw.Param("par$cods_extr_valor").AgregarValorArray(extradata.Valor)
                Next
            Next

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, peticion As Peticion,
                                           ByRef respuesta As Respuesta)

            For Each objMoviEntrada As Entrada.Movimiento In peticion.Movimientos

                If respuesta.Movimientos Is Nothing Then respuesta.Movimientos = New List(Of Salida.Movimiento)

                Dim movimiento As Salida.Movimiento = New Salida.Movimiento()
                movimiento.Codigo = objMoviEntrada.CodigoExterno
                movimiento.TipoResultado = "1" 'verificar

                If ds.Tables.Contains("tipo_resultado") AndAlso ds.Tables("tipo_resultado").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("tipo_resultado").Select("COD_EXTERNO = '" + objMoviEntrada.CodigoExterno + "'")
                        movimiento.TipoResultado = Util.AtribuirValorObj(row("TIPO_RESULTADO"), GetType(String))
                    Next
                End If

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("validaciones").Select("COD_EXTERNO = '" + objMoviEntrada.CodigoExterno + "'")

                        If movimiento.Detalles Is Nothing Then movimiento.Detalles = New List(Of Salida.Detalle)

                        movimiento.Detalles.Add(New Salida.Detalle With {.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String)),
                                                                                                                            .Descripcion = Util.AtribuirValorObj(row("DESCRICION"), GetType(String))})
                    Next

                End If


                respuesta.Movimientos.Add(movimiento)
            Next

            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ModificarMovimientos,
                               "0000", "",
                               True)
            End If

        End Sub
    End Class
End Namespace
