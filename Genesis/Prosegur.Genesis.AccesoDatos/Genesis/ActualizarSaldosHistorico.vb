﻿Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis

    Public Class ActualizarSaldosHistorico

        Public Shared Sub Ejecutar(identificadorLlamada As String,
                                  peticion As ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Peticion,
                          ByRef respuesta As ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Respuesta,
                 Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ColectarPeticion(peticion, identificadorLlamada)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                PoblarRespuesta(ds, respuesta)

                spw = Nothing
                ds.Dispose()

                log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")

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

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Salida.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Salida.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next
                End If

            End If
        End Sub

        Private Shared Function ColectarPeticion(peticion As ContractoServicio.Contractos.Job.ActualizarSaldosHistorico.Peticion, identificadorLlamada As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PJOB_{0}.sactualizar_saldos_historico", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            Return spw

        End Function

    End Class
End Namespace

