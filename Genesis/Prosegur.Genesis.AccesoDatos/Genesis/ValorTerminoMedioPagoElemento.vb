Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class ValorTerminoMedioPagoElemento

        ''' <summary>
        ''' Insere o valor do termino para o medio pago contado.
        ''' </summary>
        ''' <param name="identificadorMedioPago">identificador do medio pago.</param>
        ''' <param name="termino">Termino com o valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoInserir(identificadorMedioPago As String, termino As Clases.Termino, tipoValor As Enumeradores.TipoValor, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_TERM_MP_ELEMENTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))

            If tipoValor = Enumeradores.TipoValor.Contado Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONTADO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DECLARADO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, Nothing))
            ElseIf tipoValor = Enumeradores.TipoValor.Declarado Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DECLARADO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONTADO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEC_INDICE_GRUPO", ProsegurDbType.Inteiro_Longo, termino.NecIndiceGrupo))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o medio pago.
        ''' </summary>
        ''' <param name="identificadorRemesa">Identificador da remesa.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoExcluirRemesa(identificadorRemesa As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoExcluirRemesa)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o medio pago.
        ''' </summary>
        ''' <param name="identificadorBulto">Identificador da bulto.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoExcluirBulto(identificadorBulto As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoExcluirBulto)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para o medio pago contado.
        ''' </summary>
        ''' <param name="identificadorParcial">Identificador da parcial.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorTerminoMedioPagoElementoExcluirParcial(identificadorParcial As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoExcluirParcial)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub

        ' ''' <summary>
        ' ''' Recupera os valores dos terminos de medio pago
        ' ''' </summary>
        ' ''' <param name="identificadorContadoDeclaradoMedioPago"></param>
        ' ''' <returns></returns>
        ' ''' <remarks></remarks>
        'Public Shared Function ValorTerminoMedioPagoElementoRecuperar(identificadorContadoDeclaradoMedioPago As String) As Dictionary(Of String, String)

        '    Dim objValorTerminos As Dictionary(Of String, String) = Nothing

        '    Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        '        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DEC_CONT_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorContadoDeclaradoMedioPago))
        '        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoRecuperar)

        '        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

        '            objValorTerminos = New Dictionary(Of String, String)

        '            For Each dr In dt.Rows
        '                objValorTerminos.Add(Util.AtribuirValorObj(dr("OID_TERMINO_MEDIO_PAGO"), GetType(String)), Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)))
        '            Next

        '        End If

        '    End Using

        '    Return objValorTerminos
        'End Function

        Public Shared Function ValorTerminosMedioPagoElementoRecuperar(identificadorContadoDeclaradoMedioPago As String) As ObservableCollection(Of Clases.Termino)

            Dim objValorTerminos As ObservableCollection(Of Clases.Termino) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DEC_CONT_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorContadoDeclaradoMedioPago))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorTerminoMedioPagoElementoRecuperar)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    objValorTerminos = New ObservableCollection(Of Clases.Termino)

                    For Each dr In dt.Rows
                        Dim termino As New Clases.Termino With {.Identificador = Util.AtribuirValorObj(dr("OID_TERMINO_MEDIO_PAGO"), GetType(String)), _
                                                                .Valor = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)), _
                                                                .NecIndiceGrupo = Util.AtribuirValorObj(dr("NEC_INDICE_GRUPO"), GetType(Integer))
                                                               }
                        objValorTerminos.Add(termino)
                        'objValorTerminos.Add(Util.AtribuirValorObj(dr("OID_TERMINO_MEDIO_PAGO"), GetType(String)), Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)))
                    Next

                End If

            End Using

            Return objValorTerminos
        End Function

    End Class
End Namespace

