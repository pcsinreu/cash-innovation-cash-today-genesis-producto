Imports Prosegur.DbHelper

Namespace Reportes

    Public Class TraspaseResponsabilidad

        Public Shared Sub GrabarTraspaseResponsabilidad(objRecepcionRuta As Prosegur.Genesis.ContractoServicio.Contractos.Reportes.GrabarTraspaseResponsabilidad.RecepcionRuta)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_REPORTES)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Reportes_TraspaseResponsabilidad)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "OID_REL_TRASPASE_RESP", ProsegurDbType.Objeto_Id, objRecepcionRuta.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_ESTADO_SERVICIO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.EstadoServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "NEC_SECUENCIA", ProsegurDbType.Inteiro_Longo, objRecepcionRuta.Secuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_SERVICIO", ProsegurDbType.Observacao_Curta, objRecepcionRuta.Servicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_TIPO_SERVICIO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.TipoServicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_CLIENTE_ATEND", ProsegurDbType.Observacao_Curta, objRecepcionRuta.ClienteAtendido))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_PTO_SERVICIO_ATEND", ProsegurDbType.Observacao_Curta, objRecepcionRuta.PuntoServicioAtendido))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_EXTERNO_DOCUMENTO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.CodigoExternoDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Precinto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_DIVISA", ProsegurDbType.Observacao_Curta, objRecepcionRuta.Divisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "NUM_IMPORTE", ProsegurDbType.Objeto_Id, objRecepcionRuta.Importe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "COD_SIMBOLO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Simbolo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_FORMATO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Formato))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_PORTA_VALOR", ProsegurDbType.Descricao_Longa, objRecepcionRuta.PortaValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_REPORTES, "DES_USUARIO", ProsegurDbType.Descricao_Longa, objRecepcionRuta.Usuario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_REPORTES, cmd)
        End Sub

    End Class

End Namespace