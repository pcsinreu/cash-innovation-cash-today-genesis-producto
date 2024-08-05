Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Public Class ListaTipoValores

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Consulta de todos los valores posibles existentes
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 18/03/2015 Criado
    ''' </history>
    Public Shared Function GetValores(codTipo As String, codValor As String, desValor As String, bolActivo As Nullable(Of Boolean)) As List(Of Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.Valor)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetValores.ToString)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(codTipo) Then

            comando.CommandText += " AND LT.COD_TIPO = []COD_TIPO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, codTipo))

        End If

        If Not String.IsNullOrEmpty(codValor) Then

            comando.CommandText += " AND LV.COD_VALOR = []COD_VALOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, codValor))

        End If

        If Not String.IsNullOrEmpty(desValor) Then

            comando.CommandText += " AND LV.DES_VALOR = []DES_VALOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR", ProsegurDbType.Identificador_Alfanumerico, desValor))

        End If

        If bolActivo IsNot Nothing Then

            comando.CommandText += " AND LV.BOL_ACTIVO = []BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, bolActivo))

        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim respuesta As New List(Of TiposYValores.Valor)

            For Each row In dt.Rows

                respuesta.Add(New TiposYValores.Valor With {.CodTipo = Util.AtribuirValorObj(row("COD_TIPO"), GetType(String)), _
                                                            .OidValor = Util.AtribuirValorObj(row("OID_LISTA_VALOR"), GetType(String)), _
                                                            .CodValor = Util.AtribuirValorObj(row("COD_VALOR"), GetType(String)), _
                                                            .DesValor = Util.AtribuirValorObj(row("DES_VALOR"), GetType(String)), _
                                                            .BolActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean)), _
                                                            .BolDefecto = Util.AtribuirValorObj(row("BOL_DEFECTO"), GetType(Boolean)), _
                                                            .GmtCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)), _
                                                            .DesUsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)), _
                                                            .GmtModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)), _
                                                            .DesUsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))})

            Next
            Return respuesta
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Permite realizar el alta, modificación y baja lógica  
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 18/03/2015 Criado
    ''' </history>
    Public Shared Function SetValor(peticion As TiposYValores.SetValor.Peticion) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetValores.ToString)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(peticion.Valor.CodTipo) Then

            comando.CommandText += " AND LT.COD_TIPO = []COD_TIPO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, peticion.Valor.CodTipo))

        End If

        If Not String.IsNullOrEmpty(peticion.Valor.CodValor) Then

            comando.CommandText += " AND LV.COD_VALOR = []COD_VALOR "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, peticion.Valor.CodValor))

        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandType = CommandType.Text


        Dim tipoValor As TiposYValores.Tipo = GetTipoValorPorCodigo(peticion.Valor.CodTipo)
        If tipoValor IsNot Nothing Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_LISTA_TIPO", ProsegurDbType.Identificador_Alfanumerico, tipoValor.IdentificadorTipo))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, peticion.Valor.CodValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR", ProsegurDbType.Descricao_Longa, peticion.Valor.DesValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, peticion.Valor.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, peticion.Valor.DesUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_DEFECTO", ProsegurDbType.Logico, peticion.Valor.BolDefecto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, peticion.Valor.BolActivo))

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'update
            comando.CommandText = Util.PrepararQuery(My.Resources.AtualizarValor.ToString)
            objtransacion.AdicionarItemTransacao(comando)
            objtransacion.RealizarTransacao()

        Else
            'insert
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_LISTA_VALOR", ProsegurDbType.Identificador_Alfanumerico, Guid.NewGuid().ToString()))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, peticion.Valor.GmtModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, peticion.Valor.DesUsuarioModificacion))

            comando.CommandText = Util.PrepararQuery(My.Resources.InserirValor.ToString)
            objtransacion.AdicionarItemTransacao(comando)
            objtransacion.RealizarTransacao()

        End If

        Return peticion.Valor.CodValor

    End Function

    ''' <summary>
    ''' Consulta de todos los valores posibles existentes
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 18/03/2015 Criado
    ''' </history>
    Public Shared Function GetTipoValorPorCodigo(codTipo As String) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.TiposYValores.Tipo

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetTipoValorPorCodigo.ToString)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(codTipo) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, codTipo))
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim respuesta As New TiposYValores.Tipo

            For Each row In dt.Rows

                respuesta = New TiposYValores.Tipo With {.CodTipo = Util.AtribuirValorObj(row("COD_TIPO"), GetType(String)),
                                                        .IdentificadorTipo = Util.AtribuirValorObj(row("OID_LISTA_TIPO"), GetType(String)),
                                                        .DesTipo = Util.AtribuirValorObj(row("DES_TIPO"), GetType(String)),
                                                        .GmtCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)),
                                                        .DesUsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)),
                                                        .GmtModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)),
                                                        .DesUsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String)),
                                                        .BolActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))}

            Next
            Return respuesta
        Else
            Return Nothing
        End If

    End Function

#End Region

End Class
