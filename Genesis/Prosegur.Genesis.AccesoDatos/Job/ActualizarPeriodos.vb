Imports System.Globalization
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Namespace Genesis.Job

    Public Class ActualizarPeriodos

        Public Shared Sub Ejecutar(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Job.ActualizarPeriodos.Peticion,
                          ByRef respuesta As ContractoServicio.Contractos.Job.ActualizarPeriodos.Respuesta, ByRef periodos As List(Of ContractoServicio.Contractos.Job.ActualizarPeriodos.Periodo),
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
                PoblarRespuesta(ds, respuesta, periodos)

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

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As ContractoServicio.Contractos.Job.ActualizarPeriodos.Respuesta, periodos As List(Of ContractoServicio.Contractos.Job.ActualizarPeriodos.Periodo))
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Job.ActualizarPeriodos.Salida.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Job.ActualizarPeriodos.Salida.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next
                End If
                If ds.Tables.Contains("periodos") AndAlso ds.Tables("periodos").Rows.Count > 0 Then

                    If periodos Is Nothing Then periodos = New List(Of ContractoServicio.Contractos.Job.ActualizarPeriodos.Periodo)
                    For Each row As DataRow In ds.Tables("periodos").Rows
                        Dim periodo As ContractoServicio.Contractos.Job.ActualizarPeriodos.Periodo = New ContractoServicio.Contractos.Job.ActualizarPeriodos.Periodo
                        periodo.PeriodoIdentificador = Util.AtribuirValorObj(row(0), GetType(String))
                        periodo.DeviceId = Util.AtribuirValorObj(row(1), GetType(String))
                        periodo.MaquinaDesc = Util.AtribuirValorObj(row(2), GetType(String))
                        periodo.CodigoMensaje = Util.AtribuirValorObj(row(3), GetType(String))
                        periodo.DescripcionMensaje = Util.AtribuirValorObj(row(4), GetType(String))
                        periodos.Add(periodo)
                    Next
                End If

            End If
        End Sub

        Private Shared Function ColectarPeticion(peticion As ContractoServicio.Contractos.Job.ActualizarPeriodos.Peticion, identificadorLlamada As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PJOB_{0}.sactualizar_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_periodos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "periodos")


            Return spw

        End Function

    End Class
End Namespace

