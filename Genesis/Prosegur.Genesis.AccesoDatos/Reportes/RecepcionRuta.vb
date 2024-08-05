Imports Prosegur.DbHelper

Namespace Reportes
    Public Class RecepcionRuta

        ''' <summary>
        ''' Grabar una RecepcionRuta.
        ''' </summary>
        ''' <param name="objRecepcionRuta">Objeto RecepcionRuta preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarRecepcionRuta(objRecepcionRuta As Prosegur.Genesis.ContractoServicio.Contractos.Reportes.GrabarRecepcionRuta.RecepcionRuta)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_REPORTES)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Reportes_RecepcionRutaInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "OID_REL_RECEPCION_RUTA", ProsegurDbType.Objeto_Id, objRecepcionRuta.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_ESTADO_SERVICIO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.EstadoServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "NEC_SECUENCIA", ProsegurDbType.Inteiro_Longo, objRecepcionRuta.Secuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_SERVICIO", ProsegurDbType.Observacao_Curta, objRecepcionRuta.Servicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_TIPO_SERVICIO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.TipoServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_CLIENTE_FATURADO", ProsegurDbType.Observacao_Curta, objRecepcionRuta.ClienteFaturado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_CLIENTE_ATEND", ProsegurDbType.Observacao_Curta, objRecepcionRuta.ClienteAtendido))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_SUBCLIENTE_ATEND", ProsegurDbType.Observacao_Curta, objRecepcionRuta.SubClienteAtendido))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_PTO_SERVICIO_ATEND", ProsegurDbType.Observacao_Curta, objRecepcionRuta.PuntoServicioAtendido))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_EXTERNO_DOCUMENTO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.CodigoExternoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Precinto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_DIVISA", ProsegurDbType.Observacao_Curta, objRecepcionRuta.Divisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "NUM_IMPORTE", ProsegurDbType.Objeto_Id, objRecepcionRuta.Importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_SIMBOLO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Simbolo))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_REPORTES, cmd)
        End Sub
    End Class

End Namespace
