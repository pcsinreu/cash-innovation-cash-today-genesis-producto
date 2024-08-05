Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario

Public Class BilletajeSucursal

#Region "[LISTAR]"

    ''' <summary>
    ''' Método responsável por listar las billetajes por sucursal para o arquivo CSV
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' [magnum.oliveira] 26/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarBilletajeSucursalCSV(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion

        ' Limpa o objeto da memória quando termina de usá-lo
        Dim objRetornaBilletajesSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Declara a variável e executa a consulta
        Using comando

            ' obter query
            comando.CommandText = My.Resources.ListadoBilletajeSucursalCSV.ToString
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MOVIMENTO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoMovimiento))

            If Not objPeticion.FechaTransporteDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", "AND TBULT.FEC_TRANSPORTE >= []FECHA_TRANSPORTE_DESDE AND TBULT.FEC_TRANSPORTE < []FECHA_TRANSPORTE_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_DESDE", ProsegurDbType.Data, objPeticion.FechaTransporteDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_HASTA", ProsegurDbType.Data, objPeticion.FechaTransporteHasta.AddDays(1)))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", String.Empty)

            End If

            If Not objPeticion.FechaDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", "AND TBULT.FEC_PROCESO >= []FECHA_DESDE AND TBULT.FEC_PROCESO <= []FECHA_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", String.Empty)

            End If

            If Not objPeticion.FechaDesteFinConteo.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", "AND TBULT.FYH_FIN_CONTEO >= []FECHA_DESDE_FINCONTEO AND TBULT.FYH_FIN_CONTEO <= []FECHA_HASTA_FINCONTEO")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaDesteFinConteo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaHastaFinConteo))

            Else

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", String.Empty)

            End If

            If objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0 Then

                Dim filtroProcesos As String = "AND TBULT.COD_CANAL IN (" & Util.RetornaStringListaValores(objPeticion.Canales) & ")"
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", filtroProcesos)

            Else

                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", String.Empty)

            End If

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            ' Declara a variável e executa a consulta
            Dim drBiletajesSucursal As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drBiletajesSucursal

                Try

                    'Percorre o dr e retorna uma coleção Billetajes por Sucursal.
                    objRetornaBilletajesSucursal = RetornaColecaoBilletajeSucursalCSV(drBiletajesSucursal)

                Finally

                    ' Fecha a conexão do DataReader com o banco
                    If drBiletajesSucursal IsNot Nothing Then
                        drBiletajesSucursal.Close()
                        drBiletajesSucursal.Dispose()
                    End If

                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)

                End Try

            End Using

        End Using

        Return objRetornaBilletajesSucursal

    End Function

    ''' <summary>
    ''' Método responsável por listar las billetajes por sucursal para o arquivo PDF
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' [magnum.oliveira] 26/10/2009 Alterado
    ''' </history>
    Public Shared Function ListarBilletajeSucursalPDF(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion

        ' Declara variável de retorno
        Dim objRetornaBilletajesSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter query
            comando.CommandText = My.Resources.ListadoBilletajeSucursalPDF.ToString
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MOVIMENTO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoMovimiento))

            If Not objPeticion.FechaTransporteDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", "AND TBULT.FEC_TRANSPORTE >= []FECHA_TRANSPORTE_DESDE AND TBULT.FEC_TRANSPORTE < []FECHA_TRANSPORTE_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_DESDE", ProsegurDbType.Data, objPeticion.FechaTransporteDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_HASTA", ProsegurDbType.Data, objPeticion.FechaTransporteHasta.AddDays(1)))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", String.Empty)

            End If

            If Not objPeticion.FechaDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", "AND TBULT.FEC_PROCESO >= []FECHA_DESDE AND TBULT.FEC_PROCESO <= []FECHA_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", String.Empty)

            End If

            If Not objPeticion.FechaDesteFinConteo.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", "AND TBULT.FYH_FIN_CONTEO >= []FECHA_DESDE_FINCONTEO AND TBULT.FYH_FIN_CONTEO <= []FECHA_HASTA_FINCONTEO")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaDesteFinConteo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaHastaFinConteo))

            Else

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", String.Empty)

            End If

            If objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0 Then

                Dim filtroProcesos As String = "AND TBULT.COD_CANAL IN (" & Util.RetornaStringListaValores(objPeticion.Canales) & ")"
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", filtroProcesos)

            Else

                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", "")

            End If

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            ' executar consulta
            Dim drBiletajesSucursal As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drBiletajesSucursal

                Try

                    'Percorre o dr e retorna uma coleção Billetajes por Sucursal.
                    objRetornaBilletajesSucursal = RetornaColecaoBilletajeSucursalPDF(drBiletajesSucursal)

                    ' Recupera os dados dos totais das divisas
                    ListarDivisa(objPeticion, objRetornaBilletajesSucursal)

                    ' Recupera os dados dos totalizadores dos respaldos
                    ListarRespaldo(objPeticion, objRetornaBilletajesSucursal)

                Finally

                    ' Fecha a conexão do Data Reader
                    If drBiletajesSucursal IsNot Nothing Then
                        drBiletajesSucursal.Close()
                        drBiletajesSucursal.Dispose()
                    End If

                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)

                End Try

            End Using

        End Using

        Return objRetornaBilletajesSucursal

    End Function

    ''' <summary>
    ''' Método responsável por listar las billetajes por sucursal e divisa para o arquivo PDF
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' [magnum.oliveira] 26/10/2009 Alterado
    ''' </history>
    Public Shared Sub ListarDivisa(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion, ByRef objBilletajesSucursais As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter query
            comando.CommandText = My.Resources.ListadoBilletajeSucursalDivisaPDF.ToString
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))            

            If Not objPeticion.FechaTransporteDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", "AND TBULT.FEC_TRANSPORTE >= []FECHA_TRANSPORTE_DESDE AND TBULT.FEC_TRANSPORTE < []FECHA_TRANSPORTE_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_DESDE", ProsegurDbType.Data, objPeticion.FechaTransporteDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_HASTA", ProsegurDbType.Data, objPeticion.FechaTransporteHasta.AddDays(1)))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", String.Empty)

            End If

            If Not objPeticion.FechaDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", "AND TBULT.FEC_PROCESO >= []FECHA_DESDE AND TBULT.FEC_PROCESO <= []FECHA_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", String.Empty)

            End If

            If Not objPeticion.FechaDesteFinConteo.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", "AND TBULT.FYH_FIN_CONTEO >= []FECHA_DESDE_FINCONTEO AND TBULT.FYH_FIN_CONTEO <= []FECHA_HASTA_FINCONTEO")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaDesteFinConteo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaHastaFinConteo))

            Else

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", String.Empty)

            End If

            If objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0 Then
                Dim filtroProcesos As String = "AND TBULT.COD_CANAL IN (" & Util.RetornaStringListaValores(objPeticion.Canales) & ")"
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", filtroProcesos)
            Else
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", String.Empty)
            End If

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            ' executar consulta
            Dim drDivisas As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drDivisas
                Try
                    'Percorre o dr e retorna uma coleção Billetajes por Sucursal.
                    RetornaColecaoDivisa(drDivisas, objBilletajesSucursais)
                Finally
                    ' Fecha a conexão do Data Reader
                    drDivisas.Close()
                    drDivisas.Dispose()
                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)
                End Try
            End Using
        End Using

    End Sub

    ''' <summary>
    ''' Método responsável por listar las billetajes por sucursal respaldo para o arquivo PDF
    ''' </summary>
    ''' <param name="objPeticion">Objeto com os filtros da pesquisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' [magnum.oliveira] 26/10/2009 Alterado
    ''' </history>
    Public Shared Sub ListarRespaldo(objPeticion As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Peticion, ByRef BilletajesSucursais As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando

            ' obter query
            comando.CommandText = My.Resources.ListadoBilletajeSucursalRespaldoPDF.ToString
            comando.CommandType = CommandType.Text

            ' setar parametros
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoCliente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoDelegacion))            

            If Not objPeticion.FechaTransporteDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", "AND TBULT.FEC_TRANSPORTE >= []FECHA_TRANSPORTE_DESDE AND TBULT.FEC_TRANSPORTE < []FECHA_TRANSPORTE_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_DESDE", ProsegurDbType.Data, objPeticion.FechaTransporteDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_TRANSPORTE_HASTA", ProsegurDbType.Data, objPeticion.FechaTransporteHasta.AddDays(1)))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_TRANSPORTE]", String.Empty)

            End If

            If Not objPeticion.FechaDesde.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", "AND TBULT.FEC_PROCESO >= []FECHA_DESDE AND TBULT.FEC_PROCESO <= []FECHA_HASTA")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE", ProsegurDbType.Data_Hora, objPeticion.FechaDesde))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA", ProsegurDbType.Data_Hora, objPeticion.FechaHasta))

            Else

                comando.CommandText = comando.CommandText.Replace("[FEC_PROCESO]", String.Empty)

            End If

            If Not objPeticion.FechaDesteFinConteo.Equals(Date.MinValue) Then

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", "AND TBULT.FYH_FIN_CONTEO >= []FECHA_DESDE_FINCONTEO AND TBULT.FYH_FIN_CONTEO <= []FECHA_HASTA_FINCONTEO")

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_DESDE_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaDesteFinConteo))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FECHA_HASTA_FINCONTEO", ProsegurDbType.Data_Hora, objPeticion.FechaHastaFinConteo))

            Else

                comando.CommandText = comando.CommandText.Replace("[FYH_FIN_CONTEO]", String.Empty)

            End If

            If objPeticion.Canales IsNot Nothing AndAlso objPeticion.Canales.Count > 0 Then
                Dim filtroProcesos As String = "AND TBULT.COD_CANAL IN (" & Util.RetornaStringListaValores(objPeticion.Canales) & ")"
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", filtroProcesos)
            Else
                comando.CommandText = comando.CommandText.Replace("[FILTRO_PROCESO]", String.Empty)
            End If

            comando.CommandText = Util.PrepararQuery(comando.CommandText)

            ' executar consulta
            Dim drRespaldos As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)

            ' Declara variável de retorno
            Dim objRetornaRespaldo As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion

            ' Limpa o objeto da memória quando termina de usá-lo
            Using drRespaldos
                Try
                    'Percorre o dr e retorna uma coleção Billetajes por Sucursal.
                    RetornaColecaoRespaldo(drRespaldos, BilletajesSucursais)
                Finally
                    ' Fecha a conexão do Data Reader
                    drRespaldos.Close()
                    drRespaldos.Dispose()
                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(comando.Connection)
                End Try
            End Using
        End Using

    End Sub

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de billetajes por sucursal para gerar o arquivo CSV
    ''' </summary>
    ''' <param name="drBilletajesSucursal">Objeto com os dados da Billetajes</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoBilletajeSucursalCSV(drBilletajesSucursal As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion

        Dim objRetornaBilletajeSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSVColeccion

        While (drBilletajesSucursal.Read)

            ' adicionar para objeto
            objRetornaBilletajeSucursal.Add(PopularBilletajeSucursalCSV(drBilletajesSucursal))

        End While

        Return objRetornaBilletajeSucursal

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de billetajes por sucursal para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drBilletajesSucursal">Objeto com os dados da Billetajes</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoBilletajeSucursalPDF(drBilletajesSucursal As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion

        ' Grava os dados recuperados da sucursal
        Dim objRetornaBilletajeSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion
        ' Guarda o código da ultima estação
        Dim ultimaEstacion As String = String.Empty
        Dim EstacionActual As String = String.Empty
        ' Guarda o código da ultima divisa
        Dim ultimaDivisa As String = String.Empty

        ' Para cada sucursal recuperada
        While (drBilletajesSucursal.Read)

            If IsDBNull(drBilletajesSucursal("ESTACION")) Then

                EstacionActual = "NULL"

            Else

                EstacionActual = drBilletajesSucursal("ESTACION") & drBilletajesSucursal("DES_ESTACION")

            End If

            ' Verifica se a estacion foi alterada
            If ultimaEstacion <> EstacionActual Then

                ' adiciona para objeto
                objRetornaBilletajeSucursal.Add(PopularBilletajeSucursalPDF(drBilletajesSucursal))

                ' Cria uma nova coleção de divisias
                objRetornaBilletajeSucursal.Last().Divisas = New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaColeccion

                ' guarda o código da última estação
                ultimaEstacion = EstacionActual

                ' Limpa a ultima divisa
                ultimaDivisa = String.Empty

            End If

            ' Verifica se a divisa foi alterada
            If (ultimaDivisa <> drBilletajesSucursal("DIVISA")) Then

                ' Cria uma nova instancia de divisa
                Dim objDivisa As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Divisa = Nothing

                ' Recupera os dados da divisa
                PopularDivisa(drBilletajesSucursal, objDivisa)

                ' Adiciona a divisa a sucursal associada
                objRetornaBilletajeSucursal.Last().Divisas.Add(objDivisa)

                ' Cria uma nova coleção de detalhes da divisa
                objRetornaBilletajeSucursal.Last().Divisas.Last().DividasDetalles = New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalheColeccion

                ' guarda o código da última divisa
                ultimaDivisa = drBilletajesSucursal("DIVISA")

            End If

            'Popula os dados do detalhe da divisa
            objRetornaBilletajeSucursal.Last.Divisas.Last.DividasDetalles.Add(PopularDivisaDetalhe(drBilletajesSucursal))

        End While

        Return objRetornaBilletajeSucursal

    End Function

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de billetajes por sucursal divisa para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drDivisa">Objeto com os dados da Billetajes</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' </history>
    Private Shared Sub RetornaColecaoDivisa(drDivisa As IDataReader, ByRef BilletajesSucursais As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion)

        Dim objBilletajesSucursal As IEnumerable(Of ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDF) = Nothing

        ' Para cada divisa retornada
        While (drDivisa.Read)

            If Not IsDBNull(drDivisa("ESTACION")) Then

                ' Recupera a estacion associada a divida
                objBilletajesSucursal = From bs In BilletajesSucursais Where
                                        bs.Estacion = drDivisa("ESTACION") ' AndAlso bs.DescricionEstacion = drDivisa("DES_ESTACION")

            Else

                ' Recupera a estacion associada a divida
                objBilletajesSucursal = From bs In BilletajesSucursais Where bs.Estacion Is Nothing

            End If

            ' Verifica se a estacion existe
            If (objBilletajesSucursal IsNot Nothing AndAlso objBilletajesSucursal.Count) Then

                ' Recupera a divisa
                Dim objDivisas = From d In objBilletajesSucursal(0).Divisas _
                                 Where d.CodigoDivisa = drDivisa("COD_ISO_DIVISA")

                ' Verifica se a divisa existe
                If (objDivisas IsNot Nothing AndAlso objDivisas.Count > 0) Then

                    ' Atualiza os dados da divisa
                    PopularDivisa(drDivisa, objDivisas.First())

                End If

            End If

        End While

    End Sub

    ''' <summary>
    ''' Percorre o dr e retorna uma coleção de billetajes por sucursal e respaldos para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drRespaldo">Objeto com os dados da Billetajes</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' </history>
    Private Shared Sub RetornaColecaoRespaldo(drRespaldo As IDataReader, ByRef BilletajesSucursais As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion)

        Dim objBilletajesSucursal As IEnumerable(Of ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDF) = Nothing

        ' Para cada respaldo retornado
        While (drRespaldo.Read)

            If Not IsDBNull(drRespaldo("ESTACION")) Then

                ' Recupera a estacion associada a divida
                objBilletajesSucursal = From bs In BilletajesSucursais Where
                                        bs.Estacion = drRespaldo("ESTACION") 'And bs.DescricionEstacion = drRespaldo("DES_ESTACION")

            Else

                ' Recupera a estacion associada a divida
                objBilletajesSucursal = From bs In BilletajesSucursais Where bs.Estacion Is Nothing

            End If

            ' Verifica se a estacion existe
            If (objBilletajesSucursal IsNot Nothing AndAlso objBilletajesSucursal.Count) Then

                ' Se a lista de respaldos já foi criada
                If (objBilletajesSucursal.First().Respaldos Is Nothing) Then
                    ' Cria uma nova coleção de respaldos
                    objBilletajesSucursal.First().Respaldos = New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion
                End If

                ' adiciona os dados do respaldo objeto
                objBilletajesSucursal.First().Respaldos.Add(PopularRespaldo(drRespaldo))

            End If

        End While

    End Sub

    ''' <summary>
    ''' Popula o objeto para gerar o arquivo CSV
    ''' </summary>
    ''' <param name="drBilletajesSucursal">Registro Atual com os dados da Billetaje</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function PopularBilletajeSucursalCSV(drBilletajesSucursal As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSV

        Dim objBilletajeSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalCSV

        Util.AtribuirValorObjeto(objBilletajeSucursal.Recuento, drBilletajesSucursal("RECUENTO"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Fecha, drBilletajesSucursal("FECHA"), GetType(Date))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Letra, drBilletajesSucursal("LETRA"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.F22, drBilletajesSucursal("F22"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.OidRemesaOri, drBilletajesSucursal("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.CodSubCliente, drBilletajesSucursal("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Estacion, drBilletajesSucursal("ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.DescricionEstacion, drBilletajesSucursal("DES_ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.MedioPago, drBilletajesSucursal("MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.CodigoDivisa, drBilletajesSucursal("DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.DescricionDivisa, drBilletajesSucursal("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Unidad, drBilletajesSucursal("UNIDAD"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Multiplicador, drBilletajesSucursal("MUTIPLICADOR"), GetType(Decimal))
        Util.AtribuirValorObjeto(objBilletajeSucursal.EsBillete, drBilletajesSucursal("ES_BILLETE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Cantidad, drBilletajesSucursal("CANTIDAD"), GetType(Decimal))
        Util.AtribuirValorObjeto(objBilletajeSucursal.Valor, drBilletajesSucursal("VALOR"), GetType(Decimal))
        Util.AtribuirValorObjeto(objBilletajeSucursal.CodCalidad, drBilletajesSucursal("COD_CALIDAD"), GetType(String))

        Return objBilletajeSucursal
    End Function

    ''' <summary>
    ''' Popula o obejto para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drBilletajesSucursal">Registro Atual com os dados da Billetaje</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    Private Shared Function PopularBilletajeSucursalPDF(drBilletajesSucursal As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDF

        Dim objBilletajeSucursal As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDF

        Util.AtribuirValorObjeto(objBilletajeSucursal.Estacion, drBilletajesSucursal("ESTACION"), GetType(String))
        Util.AtribuirValorObjeto(objBilletajeSucursal.DescricionEstacion, drBilletajesSucursal("DES_ESTACION"), GetType(String))

        Return objBilletajeSucursal

    End Function

    ''' <summary>
    ''' Popula o obejto para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drDadosDivisa">Registro atual com os dados da billetaje da sucursal divisa</param>
    ''' <param name="objDivisa">Obejto com os dados da divisa</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' </history>
    Private Shared Sub PopularDivisa(drDadosDivisa As IDataReader, Optional ByRef objDivisa As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Divisa = Nothing)

        ' Verifica se a divisa está vazia (vem dos dados da sucursal)
        If (objDivisa Is Nothing) Then
            ' Cria uma nova instancia da divisa
            objDivisa = New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Divisa
            ' Atualiza somente o código da divisa
            Util.AtribuirValorObjeto(objDivisa.CodigoDivisa, drDadosDivisa("DIVISA"), GetType(String))
        Else
            ' Atualiza os declarados da divisa
            Util.AtribuirValorObjeto(objDivisa.DeclaradoEfectivo, drDadosDivisa("IMPORTE_DECLARADO_EFETIVO"), GetType(Decimal))
            Util.AtribuirValorObjeto(objDivisa.DeclaradoCheque, drDadosDivisa("IMPORTE_DECLARADO_CHEQUE"), GetType(Decimal))
            Util.AtribuirValorObjeto(objDivisa.DeclaradoTicket, drDadosDivisa("IMPORTE_DECLARADO_TICKET"), GetType(Decimal))
            Util.AtribuirValorObjeto(objDivisa.DeclaradoOtroValor, drDadosDivisa("IMPORTE_DECLARADO_OTRO_VALOR"), GetType(Decimal))
        End If

    End Sub

    ''' <summary>
    ''' Popula o objeto de detalhes da divisa para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drDadosDivisaDetalhe">Registro Atual com os dados do detalhe da divisa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 27/07/2009 Criado
    ''' </history>
    Private Shared Function PopularDivisaDetalhe(drDadosDivisaDetalhe As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalhe

        Dim objDivisaDetalhes As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalhe

        Util.AtribuirValorObjeto(objDivisaDetalhes.DescricionDivisa, drDadosDivisaDetalhe("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.CodigoTipo, drDadosDivisaDetalhe("COD_TIPO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.DescricionTipo, drDadosDivisaDetalhe("DES_TIPO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.EsBillete, drDadosDivisaDetalhe("ES_BILLETE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDivisaDetalhes.UnidadMoeda, drDadosDivisaDetalhe("UNIDAD_MOEDA"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDivisaDetalhes.Unidad, drDadosDivisaDetalhe("UNIDADES"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.ValorRecontado, drDadosDivisaDetalhe("VALOR_RECONTADO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDivisaDetalhes.DescricionMedioPago, drDadosDivisaDetalhe("DES_MEDIO_PAGO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.CodigoTransporte, drDadosDivisaDetalhe("CODIGO_TRANSPORTE"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.OidRemesaOri, drDadosDivisaDetalhe("OID_REMESA_ORI"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.CodSubCliente, drDadosDivisaDetalhe("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.CodDenominacion, drDadosDivisaDetalhe("COD_DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objDivisaDetalhes.CodCalidad, drDadosDivisaDetalhe("COD_CALIDAD"), GetType(String))

        Return objDivisaDetalhes

    End Function

    ''' <summary>
    ''' Popula o objeto para gerar o arquivo PDF
    ''' </summary>
    ''' <param name="drRespaldo">Registro Atual com os dados da Billetaje de Respaldo e Número Parcial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 24/07/2009 Criado
    ''' </history>
    Private Shared Function PopularRespaldo(drRespaldo As IDataReader) As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Respaldo

        Dim objRespaldo As New ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Respaldo

        Util.AtribuirValorObjeto(objRespaldo.NumParcialRecontado, drRespaldo("NUM_PARCIALES_CON"), GetType(Decimal))
        Util.AtribuirValorObjeto(objRespaldo.NumParcialDeclarado, drRespaldo("NUM_PARCIALES_DECL"), GetType(Decimal))

        Return objRespaldo

    End Function

#End Region

End Class