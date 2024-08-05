Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace Genesis
    ''' <summary>
    ''' Classe Mascara
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 06/09/2013
    ''' </history>

    Public Class Mascara

        ''' <summary>
        ''' Recupera os Mascara.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarMascara(identificador As String) As Clases.Mascara

            Dim objMascara As Clases.Mascara = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormatoRecuperarPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MASCARA", ProsegurDbType.Objeto_Id, identificador))

            Dim dr As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GENESIS, cmd)
            If dr.Read Then
                objMascara = New Clases.Mascara
                With objMascara
                    .Identificador = Util.AtribuirValorObj(dr("OID_MASCARA"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dr("COD_MASCARA"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dr("DES_MASCARA"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime))
                    .ExpresionRegular = Util.AtribuirValorObj(dr("DES_EXP_REGULAR"), GetType(String))
                End With
            End If

            dr.Close()
            dr.Dispose()

            AcessoDados.Desconectar(cmd.Connection)

            Return objMascara
        End Function

        ''' <summary>
        ''' Metodo ObtenerValoresPosiblesPorTermino
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerMascara(Identificador As String) As Clases.Mascara

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerMascara.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MASCARA", ProsegurDbType.Objeto_Id, Identificador))

            ' criar objeto denominacion coleccion
            Dim objMascara As New Clases.Mascara

            ' executar query
            Dim dtMascara As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtMascara IsNot Nothing AndAlso dtMascara.Rows.Count > 0 Then

                Util.AtribuirValorObjeto(objMascara.Identificador, dtMascara.Rows(0)("OID_MASCARA"), GetType(String))
                Util.AtribuirValorObjeto(objMascara.Codigo, dtMascara.Rows(0)("COD_MASCARA"), GetType(String))
                Util.AtribuirValorObjeto(objMascara.Descripcion, dtMascara.Rows(0)("DES_MASCARA"), GetType(String))
                Util.AtribuirValorObjeto(objMascara.CodigoUsuario, dtMascara.Rows(0)("COD_USUARIO"), GetType(String))
                Util.AtribuirValorObjeto(objMascara.FechaHoraActualizacion, dtMascara.Rows(0)("FYH_ACTUALIZACION"), GetType(String))
                Util.AtribuirValorObjeto(objMascara.ExpresionRegular, dtMascara.Rows(0)("DES_EXP_REGULAR"), GetType(String))

                ' retornar objFormato
                Return objMascara
            End If

            Return Nothing

        End Function
    End Class

End Namespace
