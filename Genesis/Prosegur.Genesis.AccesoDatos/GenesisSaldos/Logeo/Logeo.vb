
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.LogeoEntidades.Log.Movimiento

Namespace GenesisSaldos.Logeo
    Public Class Logeo

#Region "GeneraIdentificadorLlamada"
        Public Shared Sub GeneraIdentificadorLlamada(ByRef codigoPais As String, ByRef recurso As String, ByRef identificador As String)
            Try
                Dim spw As SPWrapper = Nothing
                spw = armarWrapperGeneraLlamada(codigoPais, recurso, identificador)
                AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                identificador = PoblarRespuestaIdentificadorLlamada(spw)

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

        Private Shared Function PoblarRespuestaIdentificadorLlamada(spw As SPWrapper) As String
            Dim identificador As String

            If Not spw.Param("par$oid_llamada").Valor.IsNull Then
                identificador = spw.Param("par$oid_llamada").Valor.ToString()
            Else
                identificador = String.Empty
            End If

            Return identificador
        End Function

        Private Shared Function armarWrapperGeneraLlamada(codigoPais As String, desRecurso As String, identificador As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PLOG_API.SGENERA_OID_LLAMADA")
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, codigoPais, , False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificador, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$des_recurso", ProsegurDbType.Descricao_Curta, desRecurso, , False)

            Return spw
        End Function
#End Region

#Region "IniciaLlamada"
        Public Shared Sub IniciaLlamada(ByRef identificador As String, ByRef recurso As String, ByRef version As String, ByRef datosEntrada As Object, ByRef codigoPais As String, ByRef codigoHashEntrada As String)
            Try
                Dim sHostName As String = DnsHelper.GetHostName()
                Dim ipE As String = DnsHelper.GetHostNameIp4()

                If Not String.IsNullOrWhiteSpace(identificador) Then
                    Dim spw As SPWrapper = Nothing
                    spw = armarWrapperIniciaLlamada(identificador, recurso, version, datosEntrada, codigoPais, codigoHashEntrada, sHostName, ipE)
                    AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
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


        Private Shared Function armarWrapperIniciaLlamada(identificador As String, origen As String, version As String, datosEntrada As Object, codigoPais As String, codigoHashEntrada As String, sHostName As String, ipE As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PLOG_API.SINICIA_LLAMADA")
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificador, , False)
            spw.AgregarParam("par$des_recurso", ProsegurDbType.Descricao_Curta, origen, , False)
            spw.AgregarParam("par$des_version", ProsegurDbType.Descricao_Curta, version, , False)
            spw.AgregarParam("par$des_datos_entrada", ProsegurDbType.Observacao_Longa, datosEntrada.ToString(), , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, codigoPais, , False)
            spw.AgregarParam("par$cod_hash_entrada", ProsegurDbType.Descricao_Curta, codigoHashEntrada, , False)
            spw.AgregarParam("par$des_host", ProsegurDbType.Descricao_Curta, sHostName, , False)
            spw.AgregarParam("par$des_host_ip", ProsegurDbType.Descricao_Curta, ipE, , False)

            Return spw
        End Function

#End Region

#Region "FinalizaLlamada"
        Public Shared Sub FinalizaLlamada(ByRef identificador As String, ByRef datosSalida As Object, ByRef codigoResultado As String, ByRef descripcionResultado As String, ByRef codigoHashSalida As String)
            Try
                If Not String.IsNullOrWhiteSpace(identificador) Then
                    Dim spw As SPWrapper = Nothing
                    spw = armarWrapperFinalizaLlamada(identificador, datosSalida, codigoResultado, descripcionResultado, codigoHashSalida)
                    AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
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

        Private Shared Function armarWrapperFinalizaLlamada(identificador As String, datosSalida As Object, codigoResultado As String, descripcionResultado As String, codigoHashSalida As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PLOG_API.SFINALIZA_LLAMADA")
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificador, , False)
            spw.AgregarParam("par$des_datos_salida", ProsegurDbType.Observacao_Longa, datosSalida, , False)
            spw.AgregarParam("par$cod_resultado", ProsegurDbType.Descricao_Curta, codigoResultado, , False)
            spw.AgregarParam("par$des_resultado", ProsegurDbType.Descricao_Longa, descripcionResultado, , False)
            spw.AgregarParam("par$cod_hash_salida", ProsegurDbType.Descricao_Curta, codigoHashSalida, , False)

            Return spw
        End Function
#End Region

#Region "AgregaDetalle"
        Public Shared Sub AgregaDetalle(ByRef identificador As String, ByRef origen As String, ByRef version As String, ByRef mensaje As String, ByRef codigoIdentificador As String)
            Try
                If Not String.IsNullOrWhiteSpace(identificador) Then
                    Dim spw As SPWrapper = Nothing
                    spw = armarWrapperAgregaDetalle(identificador, origen, version, mensaje, codigoIdentificador)
                    AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
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

        Private Shared Function armarWrapperAgregaDetalle(identificador As String, origen As String, version As String, mensaje As String, codigoIdentificador As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PLOG_API.SAGREGA_DETALLE")
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificador, , False)
            spw.AgregarParam("par$des_origen", ProsegurDbType.Descricao_Longa, origen, , False)
            spw.AgregarParam("par$des_version", ProsegurDbType.Descricao_Longa, version, , False)
            spw.AgregarParam("par$des_detalle", ProsegurDbType.Descricao_Longa, mensaje, , False)
            spw.AgregarParam("par$cod_identificador", ProsegurDbType.Descricao_Longa, codigoIdentificador, , False)

            Return spw
        End Function
#End Region
#Region "RecuperarDatos"
        Public Shared Function RecuperarDatos(codigoPais As String, actualID As String, identificadorLlamada As String, recurso As String, desDatosEntrada As String, desDatosSalida As String, codHashEntrada As String, codHashSalida As String, fechaHoraInicio As DateTime?, fechaHoraFin As DateTime?, sHostName As String, ipE As String) As List(Of LogeoEntidades.Log.Movimiento.Llamada)
            Dim retorno As New List(Of LogeoEntidades.Log.Movimiento.Llamada)()
            Dim ds As DataSet
            Try
                Dim spw As SPWrapper = Nothing
                ds = Nothing

                spw = armarWrapperRecuperarDatos(codigoPais, actualID, identificadorLlamada, recurso, desDatosEntrada, desDatosSalida, codHashEntrada, codHashSalida, fechaHoraInicio, fechaHoraFin, sHostName, ipE)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                retorno = poblarRespuestaDeRecuperarDatos(ds)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                     MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return retorno
        End Function
        Private Shared Function armarWrapperRecuperarDatos(codigoPais As String, codigoIdentificador As String, identificadorLlamada As String, recurso As String, desDatosEntrada As String, desDatosSalida As String, codHashEntrada As String, codHashSalida As String, fechaHoraInicio As DateTime?, fechaHoraFin As DateTime?, sHostName As String, ipE As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PLOG_API.SRECUPERA_DATOS")
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, codigoPais, , False)
            spw.AgregarParam("par$cod_identificador", ProsegurDbType.Descricao_Curta, codigoIdentificador, , False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$recurso", ProsegurDbType.Descricao_Longa, recurso, , False)
            spw.AgregarParam("par$des_datos_entrada", ProsegurDbType.Descricao_Longa, desDatosEntrada, , False)
            spw.AgregarParam("par$des_datos_salida", ProsegurDbType.Descricao_Longa, desDatosSalida, , False)
            spw.AgregarParam("par$cod_hash_entrada", ProsegurDbType.Descricao_Longa, codHashEntrada, , False)
            spw.AgregarParam("par$cod_hash_salida", ProsegurDbType.Descricao_Longa, codHashSalida, , False)
            spw.AgregarParam("par$des_host", ProsegurDbType.Descricao_Curta, sHostName, , False)
            spw.AgregarParam("par$des_host_ip", ProsegurDbType.Descricao_Curta, ipE, , False)

            If fechaHoraInicio Is Nothing Then
                spw.AgregarParam("par$fyh_llamada_inicio", ProsegurDbType.Data_Hora, DBNull.Value, , False)
            Else
                spw.AgregarParam("par$fyh_llamada_inicio", ProsegurDbType.Data_Hora, fechaHoraInicio, , False)
            End If
            If fechaHoraFin Is Nothing Then
                spw.AgregarParam("par$fyh_llamada_fin", ProsegurDbType.Data_Hora, DBNull.Value, , False)
            Else
                spw.AgregarParam("par$fyh_llamada_fin", ProsegurDbType.Data_Hora, fechaHoraFin, , False)
            End If


            spw.AgregarParam("par$cur_llamadas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "llamadas")
            spw.AgregarParam("par$cur_detalles", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "detalles")


            Return spw
        End Function
        Private Shared Function poblarRespuestaDeRecuperarDatos(ds As DataSet) As List(Of LogeoEntidades.Log.Movimiento.Llamada)
            Dim lista As New List(Of LogeoEntidades.Log.Movimiento.Llamada)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim dtLlamadas As DataTable = Nothing
                Dim dtDetLlamadas As DataTable = Nothing

                Try
                    dtLlamadas = ds.Tables("llamadas")
                    dtDetLlamadas = ds.Tables("detalles")
                Catch ex As Exception
                    Throw ex
                End Try

                If dtLlamadas IsNot Nothing AndAlso dtLlamadas.Rows.Count > 0 Then
                    For Each rowLlamada As DataRow In dtLlamadas.Rows

                        Dim unaLlamada As Llamada
                        unaLlamada = New LogeoEntidades.Log.Movimiento.Llamada()

                        unaLlamada.Identificador = Util.AtribuirValorObj(rowLlamada("OID_LOG_API_LLAMADA"), GetType(String))
                        unaLlamada.Recurso = Util.AtribuirValorObj(rowLlamada("DES_RECURSO"), GetType(String))
                        unaLlamada.Version = Util.AtribuirValorObj(rowLlamada("DES_VERSION"), GetType(String))
                        unaLlamada.DatosEntrada = Util.AtribuirValorObj(rowLlamada("DES_DATOS_ENTRADA"), GetType(String))
                        unaLlamada.DatosSalida = Util.AtribuirValorObj(rowLlamada("DES_DATOS_SALIDA"), GetType(String))
                        unaLlamada.CodigoResultado = Util.AtribuirValorObj(rowLlamada("COD_RESULTADO"), GetType(String))
                        unaLlamada.DescripcionResultado = Util.AtribuirValorObj(rowLlamada("DES_RESULTADO"), GetType(String))
                        unaLlamada.FechaHoraInicio = Util.AtribuirValorObj(rowLlamada("FYH_LLAMADA_INICIO"), GetType(DateTime))
                        unaLlamada.FechaHoraFin = Util.AtribuirValorObj(rowLlamada("FYH_LLAMADA_FIN"), GetType(DateTime))
                        unaLlamada.HashEntrada = Util.AtribuirValorObj(rowLlamada("COD_HASH_ENTRADA"), GetType(String))
                        unaLlamada.HashSalida = Util.AtribuirValorObj(rowLlamada("COD_HASH_SALIDA"), GetType(String))
                        unaLlamada.HostName = Util.AtribuirValorObj(rowLlamada("DES_HOST"), GetType(String))
                        unaLlamada.IpAddress = Util.AtribuirValorObj(rowLlamada("DES_HOST_IP"), GetType(String))
                        unaLlamada.Detalles = PoblarDetallesLlamadas(unaLlamada.Identificador, dtDetLlamadas)


                        lista.Add(unaLlamada)
                    Next
                End If
            End If

            Return lista
        End Function



        Private Shared Function PoblarDetallesLlamadas(identificadorLlamada As String, dtDetLlamadas As DataTable) As List(Of DetalleLlamada)
            Dim lista As New List(Of DetalleLlamada)
            Dim detalle_llamada As DetalleLlamada = Nothing
            If dtDetLlamadas IsNot Nothing AndAlso dtDetLlamadas.Rows.Count > 0 Then
                For Each rowDetalle As DataRow In dtDetLlamadas.Select("OID_LOG_API_LLAMADA = '" & identificadorLlamada & "'")
                    detalle_llamada = New DetalleLlamada()

                    detalle_llamada.CodigoIdentificador = Util.AtribuirValorObj(rowDetalle("COD_IDENTIFICADOR"), GetType(String))
                    detalle_llamada.FechaHora = Util.AtribuirValorObj(rowDetalle("FYH_DETALLE"), GetType(DateTime))
                    detalle_llamada.Mensaje = Util.AtribuirValorObj(rowDetalle("DES_DETALLE"), GetType(String))
                    detalle_llamada.Origen = Util.AtribuirValorObj(rowDetalle("DES_ORIGEN"), GetType(String))
                    detalle_llamada.Version = Util.AtribuirValorObj(rowDetalle("DES_VERSION"), GetType(String))

                    lista.Add(detalle_llamada)
                Next
            End If

            Return lista
        End Function
#End Region
    End Class
End Namespace

