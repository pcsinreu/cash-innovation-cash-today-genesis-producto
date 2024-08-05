Imports Prosegur.DbHelper
Public Class Inventario
#Region "[CONSULTAR]"

    Public Shared Function RecuperarInventarios(peticion As Prosegur.Genesis.ContractoServicio.RecuperarInventariosPeticion) As Prosegur.Genesis.ContractoServicio.RecuperarInventariosRespuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_REPORTE)
        Dim objRespuesta As New Prosegur.Genesis.ContractoServicio.RecuperarInventariosRespuesta
        Dim cmd As IDbCommand = conexao.CreateCommand
        Dim inventarios As New List(Of Prosegur.Genesis.Comon.Clases.Inventario)
        Try

            cmd.CommandText = Util.PrepararQuery(My.Resources.RecuperarInventarios.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTE, "OID_SECTOR", ProsegurDbType.Objeto_Id, peticion.OIDSector))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTE, "DATA_INICIAL", ProsegurDbType.Data_Hora, peticion.DataInicial))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTE, "DATA_FINAL", ProsegurDbType.Data_Hora, peticion.DataFinal))

            ' executar consulta            
            Dim dr As IDataReader = cmd.ExecuteReader()
            While (dr.Read)
                Dim objInventario As New Prosegur.Genesis.Comon.Clases.Inventario
                Util.AtribuirValorObjeto(objInventario.Identificador, dr("OID_INVENTARIO"), GetType(String))
                Util.AtribuirValorObjeto(objInventario.Codigo, dr("COD_INVENTARIO"), GetType(String))
                Util.AtribuirValorObjeto(objInventario.FechaCreacion, dr("GMT_CREACION"), GetType(DateTime))
                Util.AtribuirValorObjeto(objInventario.UsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
                inventarios.Add(objInventario)
            End While

            objRespuesta.ListaInventarios = inventarios

            ' Fecha a conexão do Data Reader
            dr.Close()
            dr.Dispose()

        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRespuesta

    End Function

#End Region
End Class
