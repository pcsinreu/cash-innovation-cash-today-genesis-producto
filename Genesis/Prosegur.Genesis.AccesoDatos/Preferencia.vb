Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Clases
Imports System.Text

Public Class Preferencia
    ''' <summary>
    ''' Realiza la búsqueda de preferencias.
    ''' </summary>
    ''' <param name="CodigoUsuario">Código del usuário</param>
    ''' <param name="codigoAplicacion">Código de la aplicación</param>
    ''' <param name="CodigoFuncionalidad">Código de la funcionalidad</param>
    ''' <returns>Colección de <see cref="PreferenciaUsuario"/></returns>
    Public Shared Function ObtenerPreferencias(CodigoUsuario As String, codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion, CodigoFuncionalidad As String) As ObservableCollection(Of PreferenciaUsuario)
        If String.IsNullOrEmpty(CodigoUsuario) Or
            String.IsNullOrEmpty(CodigoFuncionalidad) Then

            Return Nothing
        End If

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' Incluyendo los filtros en la instrucción SQL
        Dim sql As New StringBuilder()
        sql.Append(My.Resources.ObtenerPreferencias)
        sql.AppendLine(" WHERE PU.COD_USUARIO = []pCOD_USUARIO AND ")
        sql.AppendLine(" AP.COD_APLICACION = []pCOD_APLICACION AND ")
        sql.AppendLine(" PU.COD_FUNCIONALIDAD = []pCOD_FUNCIONALIDAD ")

        ' Incluyendo los parámetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_USUARIO", ProsegurDbType.Descricao_Longa, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_APLICACION", ProsegurDbType.Objeto_Id, codigoAplicacion.RecuperarValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, CodigoFuncionalidad))

        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql.ToString())

        ' Use un DataReader para un mejor desempeño
        Dim reader As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GENESIS, comando)
        Dim preferencias As New ObservableCollection(Of PreferenciaUsuario)

        While reader.Read()
            Dim preferencia As New PreferenciaUsuario()
            Util.AtribuirValorObjeto(preferencia.OidPreferenciaUsuario, reader("OID_PREFERENCIAS_USUARIO"), GetType(String))
            Util.AtribuirValorObjeto(preferencia.CodigoComponente, reader("COD_COMPONENTE"), GetType(String))
            Util.AtribuirValorObjeto(preferencia.CodigoFuncionalidad, reader("COD_FUNCIONALIDAD"), GetType(String))
            Util.AtribuirValorObjeto(preferencia.CodigoPropriedad, reader("COD_PROPRIEDAD"), GetType(String))
            Util.AtribuirValorObjeto(preferencia.CodigoUsuario, reader("COD_USUARIO"), GetType(String))
            preferencia.CodigoAplicacion = RecuperarEnum(Of Enumeradores.CodigoAplicacion)(reader("COD_APLICACION").ToString)
            Util.AtribuirValorObjeto(preferencia.TipoValorBinario, reader("VALOR_BINARIO_TIPO"), GetType(String))
            Util.AtribuirValorObjeto(preferencia.Valor, reader("VALOR"), GetType(String))
            preferencia.ValorBinario = IIf(reader("VALOR_BINARIO") Is DBNull.Value, Nothing, reader("VALOR_BINARIO"))
            preferencias.Add(preferencia)
        End While

        reader.Close()
        reader = Nothing

        AcessoDados.Desconectar(comando.Connection)

        Return preferencias

    End Function

    ''' <summary>
    ''' Realiza la alta de la preferencia
    ''' </summary>
    ''' <param name="preferencias">Preferencias del usuário</param>
    Public Shared Sub GuardarPreferenciaOld(preferencias As IEnumerable(Of PreferenciaUsuario))
        Try

            ' criar transacao
            Dim objTransacao As New Transacao(Constantes.CONEXAO_GENESIS)

            For Each preferencia As PreferenciaUsuario In preferencias
                ' criar comando
                Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                ' obter query
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.GuardarPreferencia)
                comando.CommandType = CommandType.Text

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_APLICACION", ProsegurDbType.Observacao_Curta, preferencia.CodigoAplicacion.RecuperarValor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_USUARIO", ProsegurDbType.Descricao_Longa, preferencia.CodigoUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoFuncionalidad))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_COMPONENTE", ProsegurDbType.Observacao_Curta, preferencia.CodigoComponente))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_PROPRIEDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoPropriedad))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR", ProsegurDbType.Observacao_Longa, preferencia.Valor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR_BINARIO", ProsegurDbType.Binario, preferencia.ValorBinario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR_BINARIO_TIPO", ProsegurDbType.Observacao_Curta, preferencia.TipoValorBinario))

                ' adicionar comando para transação
                objTransacao.AdicionarItemTransacao(comando)
            Next

            ' realizar a transação
            objTransacao.RealizarTransacao()

        Catch

            Throw

        End Try
    End Sub

    ''' <summary>
    ''' Realiza la alta de la preferencia
    ''' </summary>
    ''' <param name="preferencias">Preferencias del usuário</param>
    Public Shared Sub GuardarPreferencia(preferencias As IEnumerable(Of PreferenciaUsuario))
        Try

            ' criar transacao
            Dim objTransacao As New Transacao(Constantes.CONEXAO_GENESIS)

            For Each preferencia As PreferenciaUsuario In preferencias
                ' criar comando
                Dim comandoSelect As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                ' obter query
                comandoSelect.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.SelecionaPreferencia)
                comandoSelect.CommandType = CommandType.Text

                comandoSelect.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_APLICACION", ProsegurDbType.Observacao_Curta, preferencia.CodigoAplicacion.RecuperarValor))
                comandoSelect.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_USUARIO", ProsegurDbType.Descricao_Longa, preferencia.CodigoUsuario))
                comandoSelect.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoFuncionalidad))
                comandoSelect.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_COMPONENTE", ProsegurDbType.Observacao_Curta, preferencia.CodigoComponente))
                comandoSelect.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_PROPRIEDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoPropriedad))

                If (AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, comandoSelect) <> 0) Then

                    ' criar comando
                    Dim comandoDelete As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    ' obter query
                    comandoDelete.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ExcluiPreferencia)
                    comandoDelete.CommandType = CommandType.Text

                    comandoDelete.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_APLICACION", ProsegurDbType.Observacao_Curta, preferencia.CodigoAplicacion.RecuperarValor))
                    comandoDelete.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_USUARIO", ProsegurDbType.Descricao_Longa, preferencia.CodigoUsuario))
                    comandoDelete.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoFuncionalidad))
                    comandoDelete.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_COMPONENTE", ProsegurDbType.Observacao_Curta, preferencia.CodigoComponente))
                    comandoDelete.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_PROPRIEDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoPropriedad))

                    ' adicionar comando para transação
                    objTransacao.AdicionarItemTransacao(comandoDelete)

                End If

                ' criar comando
                Dim comandoInsert As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                ' obter query
                comandoInsert.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserePreferencia)
                comandoInsert.CommandType = CommandType.Text

                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_APLICACION", ProsegurDbType.Observacao_Curta, preferencia.CodigoAplicacion.RecuperarValor))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_USUARIO", ProsegurDbType.Descricao_Longa, preferencia.CodigoUsuario))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoFuncionalidad))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_COMPONENTE", ProsegurDbType.Observacao_Curta, preferencia.CodigoComponente))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pCOD_PROPRIEDAD", ProsegurDbType.Observacao_Curta, preferencia.CodigoPropriedad))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR", ProsegurDbType.Observacao_Longa, preferencia.Valor))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR_BINARIO", ProsegurDbType.Binario, preferencia.ValorBinario))
                comandoInsert.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "pVALOR_BINARIO_TIPO", ProsegurDbType.Observacao_Curta, preferencia.TipoValorBinario))

                ' adicionar comando para transação
                objTransacao.AdicionarItemTransacao(comandoInsert)
            Next

            ' realizar a transação
            objTransacao.RealizarTransacao()

        Catch

            Throw

        End Try
    End Sub

    Public Shared Function BorrarPreferenciasAplicacion(codigoAplicacion As Enumeradores.CodigoAplicacion, codigoUsuario As String, CodigoFuncionalidad As String) As Boolean
        Dim borrouPreferencias As Boolean = False
        Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim sql As New StringBuilder()
            sql.Append(My.Resources.BorraPreferenciasAplicacion)

            If String.IsNullOrWhiteSpace(CodigoFuncionalidad) Then
                sql.AppendLine(" AND COD_FUNCIONALIDAD = []COD_FUNCIONALIDAD ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Observacao_Curta, CodigoFuncionalidad))
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sql.ToString())
            comando.CommandType = CommandType.Text
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Observacao_Curta, codigoAplicacion.RecuperarValor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO", ProsegurDbType.Observacao_Curta, codigoUsuario))
            borrouPreferencias = (AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, comando) > 0)
        End Using
        Return borrouPreferencias
    End Function

End Class
