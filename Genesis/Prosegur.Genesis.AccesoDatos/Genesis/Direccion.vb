Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    Public Class Direccion

#Region "Consulta"

        Public Shared Function ObtenerDireccionesPuntoServicio(codPuntoServicio As String, _
                                                     codSubCliente As String, _
                                                     codCliente As String) As List(Of Clases.Direccion)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim Consulta As New StringBuilder()
            Dim Join As New StringBuilder()

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Descricao_Longa, "GEPR_TPUNTO_SERVICIO"))

            Join.AppendLine("INNER JOIN GEPR_TPUNTO_SERVICIO P ON P.OID_PTO_SERVICIO = D.OID_TABLA_GENESIS")
            Consulta.AppendLine(" AND P.COD_PTO_SERVICIO = []COD_PTO_SERVICIO")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, codPuntoServicio))

            Join.AppendLine("INNER JOIN GEPR_TSUBCLIENTE SC ON SC.OID_SUBCLIENTE = P.OID_SUBCLIENTE")
            Consulta.AppendLine(" AND SC.COD_SUBCLIENTE = []COD_SUBCLIENTE")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, codSubCliente))

            Join.AppendLine("INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = SC.OID_CLIENTE")
            Consulta.AppendLine(" AND C.COD_CLIENTE = []COD_CLIENTE")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, codCliente))

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerDirecciones, Join.ToString(), Consulta.ToString()))
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Direcciones As New List(Of Clases.Direccion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Direcciones.Add(PopulaDireccion(row))

                Next row

            End If

            Return If(Direcciones.Count = 0, Nothing, Direcciones)
        End Function

        Public Shared Function ObtenerDireccionesSubCliente(codSubCliente As String, _
                                                     codCliente As String) As List(Of Clases.Direccion)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim Consulta As New StringBuilder()
            Dim Join As New StringBuilder()

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Descricao_Longa, "GEPR_TSUBCLIENTE"))

            Join.AppendLine("INNER JOIN GEPR_TSUBCLIENTE SC ON SC.OID_SUBCLIENTE = D.OID_TABLA_GENESIS")
            Consulta.AppendLine(" AND SC.COD_SUBCLIENTE = []COD_SUBCLIENTE")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, codSubCliente))

            Join.AppendLine("INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = SC.OID_CLIENTE")
            Consulta.AppendLine(" AND C.COD_CLIENTE = []COD_CLIENTE")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, codCliente))

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerDirecciones, Join.ToString(), Consulta.ToString()))
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Direcciones As New List(Of Clases.Direccion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Direcciones.Add(PopulaDireccion(row))
                Next row
            End If

            Return If(Direcciones.Count = 0, Nothing, Direcciones)
        End Function

        Public Shared Function ObtenerDireccionesCliente(codCliente As String) As List(Of Clases.Direccion)

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim Consulta As New StringBuilder()
            Dim Join As New StringBuilder()

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Descricao_Longa, "GEPR_TCLIENTE"))

            Join.AppendLine("INNER JOIN GEPR_TCLIENTE C ON C.OID_CLIENTE = D.OID_TABLA_GENESIS")
            Consulta.AppendLine(" AND C.COD_CLIENTE = []COD_CLIENTE")
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, codCliente))

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerDirecciones, Join.ToString(), Consulta.ToString()))
            Comando.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            Dim Direcciones As New List(Of Clases.Direccion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Direcciones.Add(PopulaDireccion(row))
                Next row
            End If

            Return If(Direcciones.Count = 0, Nothing, Direcciones)
        End Function
#End Region

#Region "Metodos"

        Private Shared Function PopulaDireccion(dr As DataRow) As Clases.Direccion
            'Cria um objeto da classe de Direccion
            Dim objDireccion As New Clases.Direccion
            'Atribui o resultado na linha do dataRown ao campo da classe
            Util.AtribuirValorObjeto(objDireccion.codFiscal, dr("COD_FISCAL"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.codPostal, dr("COD_POSTAL"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.codTipoTablaGenesis, dr("COD_TIPO_TABLA_GENESIS"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCampoAdicional1, dr("DES_CAMPO_ADICIONAL_1"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCampoAdicional2, dr("DES_CAMPO_ADICIONAL_2"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCampoAdicional3, dr("DES_CAMPO_ADICIONAL_3"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional1, dr("DES_CATEGORIA_ADICIONAL_1"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional2, dr("DES_CATEGORIA_ADICIONAL_2"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional3, dr("DES_CATEGORIA_ADICIONAL_3"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desCiudad, dr("DES_CIUDAD"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desDireccionLinea1, dr("DES_DIRECCION_LINEA_1"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desDireccionLinea2, dr("DES_DIRECCION_LINEA_2"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desEmail, dr("DES_EMAIL"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desNumeroTelefono, dr("DES_NUMERO_TELEFONO"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desPais, dr("DES_PAIS"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desProvincia, dr("DES_PROVINCIA"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.gmtCreacion, dr("GMT_CREACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(objDireccion.gmtModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
            Util.AtribuirValorObjeto(objDireccion.oidDireccion, dr("OID_DIRECCION"), GetType(String))
            Util.AtribuirValorObjeto(objDireccion.oidTablaGenesis, dr("OID_TABLA_GENESIS"), GetType(String))

            'Retorna a classe preenchida
            Return objDireccion
        End Function

#End Region





    End Class

End Namespace