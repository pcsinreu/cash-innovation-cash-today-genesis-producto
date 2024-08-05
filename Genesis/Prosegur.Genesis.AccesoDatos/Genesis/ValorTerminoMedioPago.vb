Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Valor Termino Medio Pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ValorTerminoMedioPago

        Shared Function ObtenerValorTerminoMedioPago_v2(identificadoresDocumentos As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValorTerminoMedioPagoObtener_v2
                Dim filtro As String = ""

                If identificadoresDocumentos IsNot Nothing Then
                    If identificadoresDocumentos.Count = 1 Then
                        filtro &= " AND PD.OID_DOCUMENTO = []OID_DOCUMENTO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, identificadoresDocumentos(0)))
                    ElseIf identificadoresDocumentos.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDocumentos, "OID_DOCUMENTO", cmd, "AND", "PD", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function













        ''' <summary>
        ''' Recupera o valor dos terminos de médio pago para o documento.
        ''' </summary>
        ''' <param name="identificadorTermino">Identificador do término.</param>
        ''' <param name="identificadorDocumento">Identificador do documento.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarValorTerminoPorDocumento(identificadorTermino As String, identificadorDocumento As String) As String
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pOID_TERMINO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorTermino))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pOID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoRecuperarValorTerminoPorDocumento)
                Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Function

        Public Shared Function RecuperarListaValorTerminoPorDocumento(identificadorTermino As String, identificadorDocumento As String) As ObservableCollection(Of Clases.Termino)

            Dim objValorTerminos As ObservableCollection(Of Clases.Termino) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pOID_TERMINO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorTermino))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pOID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoRecuperarValorTerminoPorDocumento)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    objValorTerminos = New ObservableCollection(Of Clases.Termino)

                    For Each dr In dt.Rows
                        Dim termino As New Clases.Termino With {.Identificador = identificadorTermino, _
                                                                .Valor = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)), _
                                                                .NecIndiceGrupo = Util.AtribuirValorObj(dr("NEC_INDICE_GRUPO"), GetType(Integer))
                                                               }
                        objValorTerminos.Add(termino)
                    Next

                End If

            End Using

            Return objValorTerminos
        End Function

        ''' <summary>
        ''' Insere o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorMedioPagoDocumento">identificador do medio pago de documento.</param>
        ''' <param name="termino">Termino com o valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoInserir(identificadorMedioPagoDocumento As String, termino As Clases.Termino, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_TERMINO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGOXDOCUMENTO", ProsegurDbType.Objeto_Id, identificadorMedioPagoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEC_INDICE_GRUPO", ProsegurDbType.Inteiro_Longo, termino.NecIndiceGrupo))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirDocumento(identificadorDocumento As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoExcluirDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirRemesa(identificadorDocumento As String, identificadorRemesa As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoExcluirRemesa)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirBulto(identificadorDocumento As String, identificadorBulto As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoExcluirRemesa)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o documento de medio pago.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoExcluirParcial(identificadorDocumento As String, identificadorParcial As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoExcluirRemesa)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Metodo ObtenerValoresPosiblesPorTermino
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValoresPosiblesPorTermino(Identificador As String) As ObservableCollection(Of Clases.TerminoValorPosible)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerValoresPosiblesPorTermino.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Objeto_Id, Identificador))

            ' criar objeto denominacion coleccion
            Dim ListaValoresPosible As New ObservableCollection(Of Clases.TerminoValorPosible)

            ' executar query
            Dim dtValorPosible As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtValorPosible IsNot Nothing AndAlso dtValorPosible.Rows.Count > 0 Then

                ' percorrer os registros encontrados
                For Each row As DataRow In dtValorPosible.Rows

                    ' adicionar divisa para coleção
                    ListaValoresPosible.Add(PopularValoresPosibles(row))

                Next

                ' retornar coleção de terminos
                Return ListaValoresPosible
            End If

            Return Nothing

        End Function

        ''' <summary>
        ''' Popula o objeto termino valor posible através de datarows
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <history>
        ''' [marcel.espiritosanto] 22/08/2013 Criado
        ''' </history>
        Private Shared Function PopularValoresPosibles(row As DataRow) As Clases.TerminoValorPosible

            ' criar objeto TerminoValorPosible
            Dim objValorePosible As New Clases.TerminoValorPosible

            Util.AtribuirValorObjeto(objValorePosible.Identificador, row("OID_VALOR"), GetType(String))
            Util.AtribuirValorObjeto(objValorePosible.Codigo, row("COD_VALOR"), GetType(String))
            Util.AtribuirValorObjeto(objValorePosible.Descripcion, row("DES_VALOR"), GetType(String))
            Util.AtribuirValorObjeto(objValorePosible.EstaActivo, row("BOL_VIGENTE"), GetType(Int16))
            Util.AtribuirValorObjeto(objValorePosible.CodigoUsuario, row("COD_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(objValorePosible.FechaHoraActualizacion, row("FYH_ACTUALIZACION"), GetType(DateTime))

            Return objValorePosible

        End Function

        Shared Function ObtenerValorTerminoMedioPagoElemento_v2(identificadorContadoDeclaradoMedioPago As List(Of String)) As DataTable
            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                '(OID_CONTADO_MEDIO_PAGO = []OID_DEC_CONT_MEDIO_PAGO OR OID_DECLARADO_MEDIO_PAGO = []OID_DEC_CONT_MEDIO_PAGO)
                Dim query As String = My.Resources.ValorTerminoMedioPagoElementoRecuperar_v2
                Dim filtroContado As String = ""
                Dim filtroDeclarado As String = ""

                If identificadorContadoDeclaradoMedioPago IsNot Nothing Then
                    If identificadorContadoDeclaradoMedioPago.Count = 1 Then
                        filtroContado &= " AND C.OID_CONTADO_MEDIO_PAGO = []OID_CONTADO_MEDIO_PAGO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONTADO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, identificadorContadoDeclaradoMedioPago(0)))

                        filtroDeclarado &= " AND D.OID_DECLARADO_MEDIO_PAGO = []OID_DECLARADO_MEDIO_PAGO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DECLARADO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, identificadorContadoDeclaradoMedioPago(0)))

                    ElseIf identificadorContadoDeclaradoMedioPago.Count > 0 Then
                        filtroContado &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorContadoDeclaradoMedioPago, "OID_CONTADO_MEDIO_PAGO", cmd, "AND", "C", , False)

                        filtroDeclarado &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorContadoDeclaradoMedioPago, "OID_DECLARADO_MEDIO_PAGO", cmd, "AND", "D", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtroContado, filtroDeclarado))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt
        End Function

    End Class

End Namespace