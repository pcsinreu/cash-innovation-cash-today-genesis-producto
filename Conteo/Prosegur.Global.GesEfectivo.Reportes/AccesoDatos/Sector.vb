Imports Prosegur.DbHelper

Public Class Sector

#Region "[CONSULTAR]"
    Public Shared Function RecuperarSectorPorDelegacion(peticion As Prosegur.Genesis.ContractoServicio.RecuperarSectorPorDelegacionPeticion) As Prosegur.Genesis.ContractoServicio.RecuperarSectorPorDelegacionRespuesta

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_REPORTE)
        Dim objRespuesta As New Prosegur.Genesis.ContractoServicio.RecuperarSectorPorDelegacionRespuesta
        Dim cmd As IDbCommand = conexao.CreateCommand

        Dim Sectores As New List(Of Prosegur.Genesis.Comon.Clases.Sector)
        Try

            cmd.CommandText = Util.PrepararQuery(My.Resources.RecuperarSectorPorDelegacion.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTE, "COD_DELEGACION", ProsegurDbType.Observacao_Curta, peticion.CodigoDelegacion))

            ' executar consulta            
            Dim dr As IDataReader = cmd.ExecuteReader()
            While (dr.Read)
                Dim objSector As New Prosegur.Genesis.Comon.Clases.Sector
                Util.AtribuirValorObjeto(objSector.Codigo, dr("COD_SECTOR"), GetType(String))
                Util.AtribuirValorObjeto(objSector.Descripcion, dr("DES_SECTOR"), GetType(String))
                Util.AtribuirValorObjeto(objSector.Identificador, dr("OID_SECTOR"), GetType(String))
                Sectores.Add(objSector)
            End While

            objRespuesta.ListaSectores = Sectores

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
