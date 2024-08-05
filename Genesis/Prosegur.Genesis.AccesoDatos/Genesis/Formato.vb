Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace Genesis
    ''' <summary>
    ''' Classe Formato
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 06/09/2013
    ''' </history>
    Public Class Formato

        ''' <summary>
        ''' Recupera os Formato.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarFormato(identificador As String) As Clases.Formato

            Dim objFormato As Clases.Formato = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormatoRecuperarPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMATO", ProsegurDbType.Objeto_Id, identificador))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objFormato = New Clases.Formato
                With objFormato
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_FORMATO"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_FORMATO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_FORMATO"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(DateTime))
                End With

            End If

            Return objFormato
        End Function


        ''' <summary>
        ''' Metodo ObtenerValoresPosiblesPorTermino
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerFormato(Identificador As String) As Clases.Formato

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerFormato.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMATO", ProsegurDbType.Objeto_Id, Identificador))

            ' criar objeto denominacion coleccion
            Dim objFormato As New Clases.Formato

            ' executar query
            Dim dtFormato As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtFormato IsNot Nothing AndAlso dtFormato.Rows.Count > 0 Then

                Util.AtribuirValorObjeto(objFormato.Identificador, dtFormato.Rows(0)("OID_FORMATO"), GetType(String))
                Util.AtribuirValorObjeto(objFormato.Codigo, dtFormato.Rows(0)("COD_FORMATO"), GetType(String))
                Util.AtribuirValorObjeto(objFormato.Descripcion, dtFormato.Rows(0)("DES_FORMATO"), GetType(String))
                Util.AtribuirValorObjeto(objFormato.CodigoUsuario, dtFormato.Rows(0)("COD_USUARIO"), GetType(String))
                Util.AtribuirValorObjeto(objFormato.FechaHoraActualizacion, dtFormato.Rows(0)("FYH_ACTUALIZACION"), GetType(String))

                ' retornar objFormato
                Return objFormato
            End If

            Return Nothing

        End Function

    End Class
End Namespace



