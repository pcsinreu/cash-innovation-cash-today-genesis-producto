Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones

Namespace Genesis
    ''' <summary>
    ''' Classe AlgoritmoValidacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 06/09/2013
    ''' </history>

    Public Class AlgoritmoValidacion

        ''' <summary>
        ''' Recupera os AlgoritmoValidacion.
        ''' </summary>
        ''' <param name="identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarAlgoritmoValidacion(identificador As String) As Clases.AlgoritmoValidacion

            Dim objAlgoritimo As Clases.AlgoritmoValidacion = Nothing
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.FormatoRecuperarPorIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ALGORITMO_VALIDACION", ProsegurDbType.Objeto_Id, identificador))

            Dim dr As IDataReader = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GENESIS, cmd)

            If dr.Read Then
                objAlgoritimo = New Clases.AlgoritmoValidacion
                With objAlgoritimo
                    .Identificador = Util.AtribuirValorObj(dr("OID_ALGORITMO_VALIDACION"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dr("COD_ALGORITMO_VALIDACION"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dr("DES_ALGORITMO_VALIDACION"), GetType(String))
                    .Observacion = Util.AtribuirValorObj(dr("OBS_ALGORITMO_VALIDACION"), GetType(String))
                    .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))
                    .FechaHoraAplicacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime))
                End With
            End If

            dr.Close()
            dr.Dispose()

            AcessoDados.Desconectar(cmd.Connection)

            Return objAlgoritimo
        End Function


        ''' <summary>
        ''' Metodo ObtenerValoresPosiblesPorTermino
        ''' </summary>
        ''' <param name="Identificador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerAlgoritmoValidacion(Identificador As String) As Clases.AlgoritmoValidacion

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            ' obter comando sql
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerAlgoritmoValidacion.ToString)
            comando.CommandType = CommandType.Text

            ' criar parameter
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ALGORITMO_VALIDACION", ProsegurDbType.Objeto_Id, Identificador))

            ' criar objeto denominacion coleccion
            Dim objAlgoritmoValidacion As New Clases.AlgoritmoValidacion

            ' executar query
            Dim dtAlgoritmoValidacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            ' se encontrou algum registro
            If dtAlgoritmoValidacion IsNot Nothing AndAlso dtAlgoritmoValidacion.Rows.Count > 0 Then

                Util.AtribuirValorObjeto(objAlgoritmoValidacion.Identificador, dtAlgoritmoValidacion.Rows(0)("OID_ALGORITMO_VALIDACION"), GetType(String))
                Util.AtribuirValorObjeto(objAlgoritmoValidacion.Codigo, dtAlgoritmoValidacion.Rows(0)("COD_ALGORITMO_VALIDACION"), GetType(String))
                Util.AtribuirValorObjeto(objAlgoritmoValidacion.Descripcion, dtAlgoritmoValidacion.Rows(0)("DES_ALGORITMO_VALIDACION"), GetType(String))
                Util.AtribuirValorObjeto(objAlgoritmoValidacion.Observacion, dtAlgoritmoValidacion.Rows(0)("OBS_ALGORITMO_VALIDACION"), GetType(String))

                Util.AtribuirValorObjeto(objAlgoritmoValidacion.CodigoUsuario, dtAlgoritmoValidacion.Rows(0)("COD_USUARIO"), GetType(String))
                Util.AtribuirValorObjeto(objAlgoritmoValidacion.FechaHoraAplicacion, dtAlgoritmoValidacion.Rows(0)("FYH_ACTUALIZACION"), GetType(String))

                ' retornar objFormato
                Return objAlgoritmoValidacion
            End If

            Return Nothing

        End Function

    End Class

End Namespace
