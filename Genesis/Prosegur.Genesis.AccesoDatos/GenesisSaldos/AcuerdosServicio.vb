Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports System.Linq
Namespace GenesisSaldos
    Public Class AcuerdosServicio

        Public Shared Function Configurar(identificadorLlamada As String, peticion As Peticion, log As StringBuilder, ByRef respuesta As Respuesta) As Salida.Resultado
            Dim TiempoParcial As DateTime

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                If peticion.Acuerdos IsNot Nothing AndAlso peticion.Acuerdos.Count > 0 Then

                    log.AppendLine("AccesoDatos - cantidad de acuerdos: " & peticion.Acuerdos.Count.ToString() & ";")

                    TiempoParcial = Now
                    spw = sconfigurar_acuerdos(identificadorLlamada, peticion)
                    log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                    log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    PoblarAcuerdos(ds, respuesta, peticion.Acuerdos)
                    log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "3") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorAplicacion
                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "2") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorNegocio

                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "0") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.Exito
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

            Return respuesta.Resultado
        End Function

        Private Shared Function sconfigurar_acuerdos(identificadorLlamada As String, peticion As Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PCLIENTE_{0}.sconfigurar_AcuerdosServicio", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$anel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
            spw.AgregarParam("par$acod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ades_ContractId", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$ades_serviceOrderID", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$ades_serviceOrderCode", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$ades_ProductCode", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$acod_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_sub_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$afyh_vigencia_inicio", ProsegurDbType.Data, Nothing, , True)
            spw.AgregarParam("par$afyh_vigencia_fin", ProsegurDbType.Data, Nothing, , True)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser())
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario)
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            Dim index = 0
            For Each acuerdo In peticion.Acuerdos
                If (String.IsNullOrEmpty(acuerdo.Accion)) Then
                    spw.Param("par$acod_accion").AgregarValorArray("ALTA")
                Else
                    spw.Param("par$acod_accion").AgregarValorArray(acuerdo.Accion.ToUpper())
                End If

                spw.Param("par$anel_index").AgregarValorArray(index)
                spw.Param("par$ades_ContractId").AgregarValorArray(acuerdo.ContractId)
                spw.Param("par$ades_serviceOrderID").AgregarValorArray(acuerdo.ServiceOrderId)
                spw.Param("par$ades_serviceOrderCode").AgregarValorArray(acuerdo.ServiceOrderCode)
                spw.Param("par$ades_ProductCode").AgregarValorArray(acuerdo.ProductCode)
                spw.Param("par$acod_cliente").AgregarValorArray(acuerdo.CodigoCliente)
                spw.Param("par$acod_sub_cliente").AgregarValorArray(acuerdo.CodigoSubCliente)
                spw.Param("par$acod_punto_servicio").AgregarValorArray(acuerdo.CodigoPuntoServicio)

                If (acuerdo.FechaVigenciaInicio = New DateTime(1, 1, 1)) Then
                    spw.Param("par$afyh_vigencia_inicio").AgregarValorArray(DBNull.Value)
                Else
                    spw.Param("par$afyh_vigencia_inicio").AgregarValorArray(acuerdo.FechaVigenciaInicio)
                End If

                If (acuerdo.FechaVigenciaFin = New DateTime(1, 1, 1)) Then
                    spw.Param("par$afyh_vigencia_fin").AgregarValorArray(DBNull.Value)
                Else
                    spw.Param("par$afyh_vigencia_fin").AgregarValorArray(acuerdo.FechaVigenciaFin)
                End If


                index += 1
            Next

            Return spw
        End Function

        Private Shared Sub PoblarAcuerdos(ds As DataSet, ByRef respuesta As Respuesta, acuerdosPeticion As List(Of Entrada.Acuerdo))

            If respuesta.Acuerdos Is Nothing Then respuesta.Acuerdos = New List(Of Salida.Acuerdo)()
            If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of Salida.Detalle)()

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("validaciones").Select(" NEL_INDEX IS NULL ")
                        Dim detalle As New Salida.Detalle With {
                            .Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        }
                        respuesta.Resultado.Detalles.Add(detalle)
                    Next

                    Dim index As Integer = 0
                    For Each acuerdoPeticion In acuerdosPeticion
                        Dim acuerdo = New Salida.Acuerdo With {
                            .CodigoCliente = acuerdoPeticion.CodigoCliente,
                            .CodigoPuntoServicio = acuerdoPeticion.CodigoPuntoServicio,
                            .CodigoSubCliente = acuerdoPeticion.CodigoSubCliente,
                            .ContractId = acuerdoPeticion.ContractId,
                            .ProductCode = acuerdoPeticion.ProductCode,
                            .ServiceOrderCode = acuerdoPeticion.ServiceOrderCode,
                            .ServiceOrderId = acuerdoPeticion.ServiceOrderId,
                            .FechaVigenciaInicio = acuerdoPeticion.FechaVigenciaInicio,
                            .FechaVigenciaFin = acuerdoPeticion.FechaVigenciaFin
                        }

                        For Each row As DataRow In ds.Tables("validaciones").Select(" NEL_INDEX =  " & index)
                            If acuerdo.Detalles Is Nothing Then acuerdo.Detalles = New List(Of Salida.Detalle)

                            Dim detalle As New Salida.Detalle
                            detalle.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                            detalle.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                            acuerdo.Detalles.Add(detalle)
                        Next

                        respuesta.Acuerdos.Add(acuerdo)
                        index += 1

                    Next
                End If
            End If

        End Sub
    End Class
End Namespace
