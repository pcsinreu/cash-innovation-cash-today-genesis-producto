Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports Prosegur.Framework.Dicionario

Namespace Genesis
    Public Class ListaValor

        ''' <summary>
        ''' Retorna o valor do tipo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValor(Tipo As Enumeradores.Tipos, _
                                            IdentificadorRemesa As String, _
                                            IdentificadorBulto As String, _
                                            IdentificadorParcial As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.ListaValorBultoObtenerValor
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorRemesa) Then
                cmd.CommandText &= " AND LVB.OID_REMESA = []OID_REMESA "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
            End If

            If Not String.IsNullOrEmpty(IdentificadorBulto) Then
                cmd.CommandText &= " AND LVB.OID_BULTO = []OID_BULTO "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
            End If

            If Not String.IsNullOrEmpty(IdentificadorParcial) Then
                cmd.CommandText &= " AND LVB.OID_PARCIAL = []OID_PARCIAL "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, IdentificadorParcial))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(Tipo)))

            ' Garante que retornará apenas um valor
            cmd.CommandText &= " AND rownum = 1 "

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Recupera uma tabela com o valor para o elemento e tipo informado. Os campos retornados são: OID_LISTA_VALOR, COD_VALOR e DES_VALOR
        ''' </summary>
        ''' <param name="tipoValor"></param>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <param name="IdentificadorBulto"></param>
        ''' <param name="IdentificadorParcial"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerValorPorElemento(tipoValor As Enumeradores.TipoListaValor, _
                                                        IdentificadorRemesa As String, _
                                                        IdentificadorBulto As String, _
                                                        IdentificadorParcial As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim filtro As New List(Of String)

            If String.IsNullOrEmpty(IdentificadorParcial) Then
                filtro.Add("VE.OID_PARCIAL IS NULL")
                If String.IsNullOrEmpty(IdentificadorBulto) Then
                    Throw New ArgumentException()
                Else
                    'filtro.Add("VE.OID_REMESA = (SELECT B.OID_REMESA FROM SAPR_TBULTO B WHERE B.OID_BULTO = VE.OID_BULTO)")
                    filtro.Add("VE.OID_REMESA = []OID_REMESA")
                    filtro.Add("VE.OID_BULTO = []OID_BULTO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
                End If
            Else
                filtro.Add("VE.OID_REMESA = []OID_REMESA")
                filtro.Add("VE.OID_BULTO = []OID_BULTO")
                filtro.Add("VE.OID_PARCIAL = []OID_PARCIAL")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, IdentificadorBulto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, IdentificadorParcial))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, tipoValor.RecuperarValor()))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerListaValorPorElemento, String.Join(" AND ", filtro.ToArray())))
            cmd.CommandType = CommandType.Text

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        ''' <summary>
        ''' Recupera uma tabela com os possiveis valores para o tipo informado. Os campos retornados são: OID_LISTA_VALOR, COD_VALOR e DES_VALOR
        ''' </summary>
        ''' <param name="TipoLista"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPorTipo(TipoLista As Enumeradores.TipoListaValor) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ListaValorRecuperar)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, TipoLista.RecuperarValor()))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function RecuperarPorTipoComModulos(TipoLista As Enumeradores.TipoListaValor, codigos As List(Of String)) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.ListaValorRecuperarComModulos
            cmd.CommandType = CommandType.Text

            Dim filtro As String = ""

            If codigos Is Nothing OrElse codigos.Count = 0 Then

                filtro &= ""


            ElseIf codigos.Count = 1 Then

                filtro &= " AND LV.COD_VALOR = []COD_VALOR "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_VALOR", _
                                                                            ProsegurDbType.Objeto_Id, codigos(0)))

            Else

                filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigos, "COD_VALOR", cmd, "AND")

            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, TipoLista.RecuperarValor()))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, filtro))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        ''' <summary>
        ''' Recupera um valor utilizando o identificador. Os campos retornados são: OID_LISTA_VALOR, COD_VALOR e DES_VALOR
        ''' </summary>
        ''' <param name="identificadorListaValor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPorIdentificador(identificadorListaValor As String) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerListaValorPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_LISTA_VALOR", ProsegurDbType.Objeto_Id, identificadorListaValor))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        ''' <summary>
        ''' Recupera um valor utilizando o código e o tipo da lista valor. Os campos retornados são: OID_LISTA_VALOR, COD_VALOR e DES_VALOR
        ''' </summary>
        ''' <param name="CodigoValor">String</param>
        ''' <param name="tipoValor">Enumeradores.TipoListaValor</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPorCodigo(CodigoValor As String, tipoValor As Enumeradores.TipoListaValor) As DataTable

            Dim codigos As New List(Of String)
            codigos.Add(CodigoValor)
            Return RecuperarPorCodigos(codigos, tipoValor)

        End Function

        Public Shared Function RecuperarPorCodigos(codigos As List(Of String), tipo As Enumeradores.TipoListaValor) As DataTable

            Dim dt As DataTable = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""

                    If codigos.Count = 1 Then

                        filtro &= " AND LV.COD_VALOR = []COD_VALOR "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_VALOR", _
                                                                                    ProsegurDbType.Objeto_Id, codigos(0)))

                    Else

                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigos, "COD_VALOR", command, "AND")

                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, Extenciones.RecuperarValor(tipo)))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerListaValorPorCodigoTipo, filtro))
                    command.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return dt

        End Function

        ''' <summary>
        ''' Recupera uma tabela com o valor para o elemento e tipo informado. Os campos retornados são: OID_LISTA_VALOR, COD_VALOR e DES_VALOR
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="tipoValor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPorElemento(identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String, tipoValor As Enumeradores.TipoListaValor) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim filtro As New List(Of String)

            If String.IsNullOrEmpty(identificadorParcial) Then
                filtro.Add("VE.OID_PARCIAL IS NULL")
                If String.IsNullOrEmpty(identificadorBulto) Then
                    Throw New ArgumentException()
                Else
                    filtro.Add("VE.OID_REMESA = (SELECT B.OID_REMESA FROM SAPR_TBULTO B WHERE B.OID_BULTO = VE.OID_BULTO)")
                    filtro.Add("VE.OID_BULTO = []OID_BULTO")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
                End If
            Else
                filtro.Add("VE.OID_REMESA = (SELECT B.OID_REMESA FROM SAPR_TBULTO B WHERE B.OID_BULTO = VE.OID_BULTO)")
                filtro.Add("VE.OID_BULTO = (SELECT P.OID_BULTO FROM SAPR_TPARCIAL P WHERE P.OID_PARCIAL = VE.OID_PARCIAL)")
                filtro.Add("VE.OID_PARCIAL = []OID_PARCIAL")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, tipoValor.RecuperarValor()))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerListaValorPorElemento, String.Join(" AND ", filtro.ToArray())))
            cmd.CommandType = CommandType.Text

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function RecuperarIdentificadorValorNoDefinido(CodigoListaTipo As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarIdentificadorValorNoDefinido)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Objeto_Id, CodigoListaTipo))

            Dim IdentificadorValorNoDefinido = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            ' se não encontrou o valor padrão, retorna um erro
            If String.IsNullOrEmpty(IdentificadorValorNoDefinido) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("064_msg_lista_tipo_sin_valor_padron"), CodigoListaTipo))
            End If

            Return IdentificadorValorNoDefinido

        End Function

        Public Shared Sub InserirPorElemento(identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String, identificadorListaValor As String, usuario As String, codigoListaTipo As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirListaValorPorElemento)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_LISTA_VALORXELEMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, If(identificadorParcial, DBNull.Value)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_LISTA_VALOR", ProsegurDbType.Objeto_Id, If(String.IsNullOrEmpty(identificadorListaValor), RecuperarIdentificadorValorNoDefinido(codigoListaTipo), identificadorListaValor)))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))

            If _transacion IsNot Nothing Then
                _transacion.AdicionarItemTransacao(cmd)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If
        End Sub

        Public Shared Sub ExcluirPorElemento(identificadorRemesa As String, identificadorBulto As String, identificadorParcial As String, identificadorListaValor As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_LISTA_VALOR", ProsegurDbType.Objeto_Id, identificadorListaValor))

            Dim where As String = " AND "
            If Not String.IsNullOrEmpty(identificadorRemesa) Then
                where += " OID_REMESA =[]OID_REMESA"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            ElseIf Not String.IsNullOrEmpty(identificadorBulto) Then
                where += " OID_BULTO =[]OID_BULTO"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            ElseIf Not String.IsNullOrEmpty(identificadorParcial) Then
                where += " OID_PARCIAL =[]OID_PARCIAL"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificadorParcial))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ExcluirListaValorPorElemento + where)
            cmd.CommandType = CommandType.Text

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

    End Class

End Namespace

