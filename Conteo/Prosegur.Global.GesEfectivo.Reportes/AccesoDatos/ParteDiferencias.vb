Imports Prosegur.DbHelper
Imports System.Text

Public Class ParteDiferencias

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar las partes de diferencias
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    Public Shared Function ListarParteDiferencias(objPeticion As ContractoServ.ParteDiferencias.GetParteDiferencias.Peticion) As List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias)

        ' Declara variável de retorno
        Dim objRetornaParteDiferencias As New List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias)

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' executar consulta
        Dim drParteDiferencias As IDataReader = Nothing

        Try

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = My.Resources.ListadoParteDiferencias.ToString
                comando.CommandType = CommandType.Text

                Dim filtros As New StringBuilder

                If objPeticion.CodigoDelegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoDelegacion) Then
                    filtros.AppendLine(" AND B.COD_DELEGACION = []COD_DELEGACION ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
                End If
                If objPeticion.PrecintoRemesa IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.PrecintoRemesa) Then
                    filtros.AppendLine(" AND R.COD_PRECINTO = []COD_PRECINTO_REMESA ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PRECINTO_REMESA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.PrecintoRemesa))
                End If
                If objPeticion.PrecintoBulto IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.PrecintoBulto) Then
                    filtros.AppendLine(" AND R.COD_PRECINTO = []COD_PRECINTO_BULTO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.PrecintoBulto))
                End If
                If objPeticion.NumeroTransporte IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.NumeroTransporte) Then
                    filtros.AppendLine(" AND B.COD_TRANSPORTE = []COD_TRANSPORTE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TRANSPORTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.NumeroTransporte))
                End If
                If objPeticion.CodigoCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoCliente) Then
                    filtros.AppendLine(" AND B.COD_CLIENTE = []COD_CLIENTE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
                End If
                If objPeticion.CodigoSubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoSubCliente) Then
                    filtros.AppendLine(" AND B.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoSubCliente))
                End If
                If objPeticion.CodigoPuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio) Then
                    filtros.AppendLine(" AND B.COD_PUNTO_SERVICIO = []COD_PUNTO_SERVICIO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoPuntoServicio))
                End If
                If objPeticion.FechaConteoDesde > DateTime.MinValue AndAlso objPeticion.FechaConteoHasta > DateTime.MinValue Then
                    filtros.AppendLine(" AND DPD.FYH_CONTEO BETWEEN []FYH_CONTEO_INI AND []FYH_CONTEO_FIN ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_CONTEO_INI", ProsegurDbType.Data_Hora, objPeticion.FechaConteoDesde))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaConteoHasta))
                End If
                If objPeticion.FechaTransporteDesde > DateTime.MinValue AndAlso objPeticion.FechaTransporteHasta > DateTime.MinValue Then
                    filtros.AppendLine(" AND B.FEC_TRANSPORTE BETWEEN []FEC_TRANSPORTE_INI AND []FEC_TRANSPORTE_FIN ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_TRANSPORTE_INI", ProsegurDbType.Data_Hora, objPeticion.FechaTransporteDesde))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_TRANSPORTE_FIN", ProsegurDbType.Data_Hora, objPeticion.FechaTransporteHasta))
                End If
                If objPeticion.Contador IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.Contador) Then
                    filtros.AppendLine(" AND RM.COD_USUARIO = []COD_USUARIO ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Contador))
                End If
                If objPeticion.Supervisor IsNot Nothing AndAlso Not String.IsNullOrEmpty(objPeticion.Supervisor) Then
                    filtros.AppendLine(" AND RM.COD_SUPERVISOR = UPPER([]COD_SUPERVISOR) ")
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUPERVISOR", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Supervisor))
                End If

                comando.CommandText = Util.PrepararQuery(comando.CommandText.Replace("[FILTROS]", filtros.ToString()))

                ' executar consulta
                drParteDiferencias = comando.ExecuteReader()

                'Percorre o dr e retorna uma coleção de partes de diferencias
                objRetornaParteDiferencias = ConverteDadosParteDiferencias(drParteDiferencias)

                ' Fecha a conexão do Data Reader
                drParteDiferencias.Close()
                drParteDiferencias.Dispose()

            End Using

        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRetornaParteDiferencias

    End Function

    ''' <summary>
    ''' Converte os dados de retorno da consulta
    ''' </summary>
    ''' <param name="drParteDiferencias"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConverteDadosParteDiferencias(drParteDiferencias As IDataReader) As List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias)

        Dim objParteDiferencias = New List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias)

        While (drParteDiferencias.Read)

            Dim codPrecinto As String = AtribuirValorObjeto(drParteDiferencias("COD_PRECINTO"), GetType(String))
            Dim fechaConteo As DateTime = AtribuirValorObjeto(drParteDiferencias("FYH_CONTEO"), GetType(DateTime))

            If Not objParteDiferencias.Exists(Function(pd) pd.PrecintoRemesa = codPrecinto AndAlso pd.FechaConteo = fechaConteo) Then

                objParteDiferencias.Add(New ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias() With {
                                        .Cliente = AtribuirValorObjeto(drParteDiferencias("DES_CLIENTE"), GetType(String)),
                                        .CodigoTransporte = AtribuirValorObjeto(drParteDiferencias("COD_TRANSPORTE"), GetType(String)),
                                        .DatosDocumentos = New List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.DatosDocumentos),
                                        .FechaConteo = AtribuirValorObjeto(drParteDiferencias("FYH_CONTEO"), GetType(DateTime)),
                                        .FechaTransporte = AtribuirValorObjeto(drParteDiferencias("FEC_TRANSPORTE"), GetType(DateTime)),
                                        .PrecintoRemesa = AtribuirValorObjeto(drParteDiferencias("COD_PRECINTO"), GetType(String)),
                                        .PuntoServicio = AtribuirValorObjeto(drParteDiferencias("DES_PUNTO_SERVICIO"), GetType(String)),
                                        .SubCliente = AtribuirValorObjeto(drParteDiferencias("DES_SUBCLIENTE"), GetType(String))
                                    })

            End If

            If Not objParteDiferencias.Exists(Function(pd) pd.DatosDocumentos.Exists(Function(doc) doc.ID = AtribuirValorObjeto(drParteDiferencias("OID_DOC_PARTE_DIFERENCIAS"), GetType(String)))) Then

                objParteDiferencias.Find(Function(pd) pd.PrecintoRemesa = codPrecinto AndAlso pd.FechaConteo = fechaConteo).DatosDocumentos.Add(New ContractoServ.ParteDiferencias.GetParteDiferencias.DatosDocumentos() With {
                                                                                                                    .ID = AtribuirValorObjeto(drParteDiferencias("OID_DOC_PARTE_DIFERENCIAS"), GetType(String)),
                                                                                                                    .FechaCreacion = AtribuirValorObjeto(drParteDiferencias("FYH_ACTUALIZACION"), GetType(DateTime)),
                                                                                                                    .NumeroDocumento = AtribuirValorObjeto(drParteDiferencias("NEL_NUMERO_DOCUMENTO"), GetType(String)),
                                                                                                                    .HayDocumentoGeneral = AtribuirValorObjeto(drParteDiferencias("HAY_GENERAL"), GetType(Boolean)),
                                                                                                                    .HayDocumentoIncidencia = AtribuirValorObjeto(drParteDiferencias("HAY_INCIDENCIA"), GetType(Boolean)),
                                                                                                                    .HayDocumentoJustificacion = AtribuirValorObjeto(drParteDiferencias("HAY_JUSTIFICATIVA"), GetType(Boolean))})

            End If

        End While

        Return objParteDiferencias

    End Function

    ''' <summary>
    ''' Retorna o valor de acordo com o tipo
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function AtribuirValorObjeto(Valor As Object, _
                                                TipoCampo As System.Type) As Object

        Dim Campo As Object = Nothing

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor.ToString.Trim).ToString("dd/MM/yyyy HH:mm:ss")
            ElseIf TipoCampo Is GetType(Date) Then
                Campo = Convert.ToDateTime(Valor.ToString.Trim).ToString("dd/MM/yyyy")
            End If
        Else
            Campo = Nothing
        End If

        Return Campo

    End Function

    ''' <summary>
    ''' Método responsável por recuperar los documentos de las partes de fiferencias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RecuperarDocumentos(objPeticion As ContractoServ.ParteDiferencias.GetDocumentos.Peticion) As ContractoServ.ParteDiferencias.GetDocumentos.Documentos

        ' Declara variável de retorno
        Dim objDocumentos As ContractoServ.ParteDiferencias.GetDocumentos.Documentos = Nothing

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' executar consulta
        Dim drDocumentos As IDataReader = Nothing

        Try

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                ' obter procedure
                comando.CommandText = My.Resources.RecuperarDocumentos.ToString
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DOC_PARTE_DIFERENCIAS", ProsegurDbType.Identificador_Alfanumerico, objPeticion.ID))

                Dim documentos As New StringBuilder
                Dim filtros As New StringBuilder

                If objPeticion.General Then
                    documentos.AppendLine(", DPD.BIN_DOCUMENTO_GENERAL")
                    filtros.AppendLine(" AND DPD.BIN_DOCUMENTO_GENERAL IS NOT NULL ")
                End If

                If objPeticion.Comentario Then
                    documentos.AppendLine(", DPD.BIN_DOCUMENTO_COMENTARIO")
                    filtros.AppendLine(" AND DPD.BIN_DOCUMENTO_COMENTARIO IS NOT NULL ")
                End If

                If objPeticion.Justificativa Then
                    documentos.AppendLine(", DPD.BIN_DOCUMENTO_JUSTIFICATIVA")
                    filtros.AppendLine(" AND DPD.BIN_DOCUMENTO_JUSTIFICATIVA IS NOT NULL ")
                End If

                If objPeticion.DatosConteo Then
                    documentos.AppendLine(", DPD.BIN_DATOS_CONTEO")
                    filtros.AppendLine(" AND DPD.BIN_DATOS_CONTEO IS NOT NULL ")
                End If

                comando.CommandText = Util.PrepararQuery(comando.CommandText.Replace("[DOCUMENTOS]", documentos.ToString()).Replace("[FILTROS]", filtros.ToString()))

                ' executar consulta
                drDocumentos = comando.ExecuteReader()

                While (drDocumentos.Read)

                    objDocumentos = New ContractoServ.ParteDiferencias.GetDocumentos.Documentos()

                    With objDocumentos

                        .ID = objPeticion.ID

                        If objPeticion.General Then
                            If drDocumentos("BIN_DOCUMENTO_GENERAL") IsNot DBNull.Value Then
                                .General = CType(drDocumentos("BIN_DOCUMENTO_GENERAL"), Byte())
                            End If
                        End If

                        If objPeticion.Comentario Then
                            If drDocumentos("BIN_DOCUMENTO_COMENTARIO") IsNot DBNull.Value Then
                                .Comentario = CType(drDocumentos("BIN_DOCUMENTO_COMENTARIO"), Byte())
                            End If
                        End If

                        If objPeticion.Justificativa Then
                            If drDocumentos("BIN_DOCUMENTO_JUSTIFICATIVA") IsNot DBNull.Value Then
                                .Justificativa = CType(drDocumentos("BIN_DOCUMENTO_JUSTIFICATIVA"), Byte())
                            End If
                        End If

                        If objPeticion.DatosConteo Then
                            If drDocumentos("BIN_DATOS_CONTEO") IsNot DBNull.Value Then
                                .DatosConteo = CType(drDocumentos("BIN_DATOS_CONTEO"), Byte())
                            End If
                        End If

                    End With

                End While

            End Using

        Finally

            ' Fecha a conexão do Data Reader
            drDocumentos.Close()
            drDocumentos.Dispose()

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objDocumentos

    End Function

#End Region

End Class
