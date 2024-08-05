Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Data

Namespace Genesis
    Public Class TipoServicio

        Public Shared Function ObtenerTipoServicioDeElementos(identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ObtenerListaValorPorElemento_v2
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND VE.OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "VE", , False)
                    End If
                End If
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.TipoListaValor.TipoServicio.RecuperarValor()))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function




















        ''' <summary>
        ''' Recupera todos los tipo de servicio.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTiposServicio() As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoServicio)
            Dim dt As DataTable = ListaValor.RecuperarPorTipo(Enumeradores.TipoListaValor.TipoServicio)
            Dim listaTiposServicio As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoServicio)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    listaTiposServicio.Add(PreencherTipoServicioLinha(dr))
                Next
            End If
            Return listaTiposServicio
        End Function

        Public Shared Function RecuperarTipoServicioPorIdentificador(identificador As String) As Clases.TipoServicio
            Dim dt As DataTable = ListaValor.RecuperarPorIdentificador(identificador)
            Return PreencherTipoServicio(dt)
        End Function

        Public Shared Function RecuperarTipoServicioPorCodigo(codigo As String) As Clases.TipoServicio
            Dim dt As DataTable = ListaValor.RecuperarPorCodigo(codigo, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoServicio)
            Return PreencherTipoServicio(dt)
        End Function

        Public Shared Function ObtenerTipoServicioPorCodigos(codigos As List(Of String)) As ObservableCollection(Of Clases.TipoServicio)
            Dim tipoServicio As ObservableCollection(Of Clases.TipoServicio) = Nothing
            Dim dt As DataTable = ListaValor.RecuperarPorCodigos(codigos, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoServicio)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                tipoServicio = New ObservableCollection(Of Clases.TipoServicio)
                For Each _row In dt.Rows
                    If tipoServicio.FirstOrDefault(Function(x) x.Codigo = Util.AtribuirValorObj(_row("COD_VALOR"), GetType(String))) Is Nothing Then
                        tipoServicio.Add(PreencherTipoServicioLinha(_row))
                    End If
                Next
            End If
            Return tipoServicio
        End Function


        Public Shared Function RecuperarPorElemento(identificadorRemessa As String, identificadorBulto As String) As Clases.TipoServicio
            Dim dt As DataTable = ListaValor.RecuperarPorElemento(identificadorRemessa, identificadorBulto, Nothing, Prosegur.Genesis.Comon.Enumeradores.TipoListaValor.TipoServicio)
            Return PreencherTipoServicio(dt)
        End Function

        ''' <summary>
        ''' Recupera los valores de un DataTable e retorna un tipoServicio. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="datos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoServicio(datos As DataTable) As Prosegur.Genesis.Comon.Clases.TipoServicio
            If datos IsNot Nothing AndAlso datos.Rows.Count = 1 Then
                Return PreencherTipoServicioLinha(datos.Rows(0))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Recupera los valores de un DataRow e retorna un tipoServicio. Campos esperados del DataRow: OID_LISTA_VALOR, COD_VALOR y DES_VALOR
        ''' </summary>
        ''' <param name="linha"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherTipoServicioLinha(linha As DataRow) As Prosegur.Genesis.Comon.Clases.TipoServicio
            Dim tipoSevicio As New Clases.TipoServicio

            tipoSevicio = New Clases.TipoServicio()
            tipoSevicio.Identificador = Util.AtribuirValorObj(linha("OID_LISTA_VALOR"), GetType(String))
            tipoSevicio.Codigo = Util.AtribuirValorObj(linha("COD_VALOR"), GetType(String))
            tipoSevicio.Descripcion = Util.AtribuirValorObj(linha("DES_VALOR"), GetType(String))
            tipoSevicio.EsDefecto = Util.AtribuirValorObj(linha("BOL_DEFECTO"), GetType(String))

            Return tipoSevicio
        End Function

        ''' <summary>
        ''' Inseri lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub InserirTipoServicioPorBulto(identificadorRemesa As String, identificadorBulto As String, identificadorListaValor As String, usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)
            ListaValor.InserirPorElemento(identificadorRemesa, identificadorBulto, Nothing, identificadorListaValor, usuario, Enumeradores.TipoListaValor.TipoServicio.RecuperarValor(), _transacion)
        End Sub

        ''' <summary>
        ''' Exclui lo formato del bulto
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ExcluirTipoServicioPorBulto(identificadorBulto As String, identificadorListaValor As String)
            ListaValor.ExcluirPorElemento(Nothing, identificadorBulto, Nothing, identificadorListaValor)
        End Sub

    End Class

End Namespace

