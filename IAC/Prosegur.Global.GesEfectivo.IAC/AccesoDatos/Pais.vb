Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Public Class Pais

    ''' <summary>
    ''' Faz a pesquisa e preenche do datatable, retornando uma coleção de paises
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2013 Criado
    ''' </history>
    Public Shared Function GetComboPais() As ContractoServicio.Utilidad.GetComboPais.PaisColeccion

        Dim objCombo As New ContractoServicio.Utilidad.GetComboPais.PaisColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaPais.ToString())
        comando.CommandType = CommandType.Text

        Dim dtPais As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtPais IsNot Nothing _
            AndAlso dtPais.Rows.Count > 0 Then
            For Each dr As DataRow In dtPais.Rows
                objCombo.Add(PopularComboPais(dr))
            Next
        End If

        Return objCombo

    End Function

    ''' <summary>
    ''' Popula um objeto Paises com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2012 Criado
    ''' </history>
    Private Shared Function PopularComboPais(dr As DataRow) As ContractoServicio.Utilidad.GetComboPais.Pais

        ' criar objeto Pais
        Dim objPais As New ContractoServicio.Utilidad.GetComboPais.Pais

        Util.AtribuirValorObjeto(objPais.OidPais, dr("OID_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPais.Description, dr("DES_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPais.Codigo, dr("COD_PAIS"), GetType(String))
        ' retorna objeto preenchido
        Return objPais

    End Function

    ''' <summary>
    ''' Retorna os Paises
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2013 - Criado
    ''' </history>
    Public Shared Function GetPais() As ContractoServicio.Pais.GetPais.PaisColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaPais)
        cmd.CommandType = CommandType.Text

        cmd.CommandText &= " WHERE BOL_ACTIVO = 1"

        'cria o objeto de Pais
        Dim objPais As New ContractoServicio.Pais.GetPais.PaisColeccion

        Dim dtPais As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dtPais IsNot Nothing _
            AndAlso dtPais.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtPais.Rows

                ' preencher a coleção com objetos pais
                objPais.Add(PopularGetPais(dr))

            Next

        End If

        Return objPais
    End Function

    ''' <summary>
    ''' Popula um objeto Paises com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2012 Criado
    ''' </history>
    Private Shared Function PopularGetPais(dr As DataRow) As ContractoServicio.Pais.GetPais.Pais
        ' criar objeto divisa
        Dim objPais As New ContractoServicio.Pais.GetPais.Pais

        Util.AtribuirValorObjeto(objPais.OidPais, dr("OID_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPais.CodPais, dr("COD_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPais.DesPais, dr("DES_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPais.BolVigente, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objPais.GmtCreacion, dr("GMT_CREACION"), GetType(Date))
        Util.AtribuirValorObjeto(objPais.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objPais.GmtModificacion, dr("GMT_MODIFICACION"), GetType(Date))
        Util.AtribuirValorObjeto(objPais.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

        ' retornar objeto Pais preenchido
        Return objPais
    End Function

    Public Shared Function GetPaisDetail(objPeticion As String) As ContractoServicio.Pais.GetPaisDetail.PaisColeccion
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaPais.ToString)
        cmd.CommandType = CommandType.Text

        cmd.CommandText &= " WHERE OID_PAIS = :OID_PAIS"
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PAIS", ProsegurDbType.Identificador_Alfanumerico, objPeticion))

        ' criar objeto delegaciones
        Dim objPais As New ContractoServicio.Pais.GetPaisDetail.PaisColeccion

        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then
            For Each dr As DataRow In dtQuery.Rows
                objPais.Add(PopularGetPaisDetail(dr))
            Next
        End If

        ' retornar objeto preenchido
        Return objPais
    End Function

    ''' <summary>
    ''' Popula um objeto Delegacion através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    Private Shared Function PopularGetPaisDetail(dr As DataRow) As ContractoServicio.Pais.GetPaisDetail.Pais

        ' criar objeto termino Iac
        Dim objPaisDetail As New ContractoServicio.Pais.GetPaisDetail.Pais

        'Termino
        Util.AtribuirValorObjeto(objPaisDetail.OidPais, dr("OID_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPaisDetail.CodPais, dr("COD_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objPaisDetail.Description, dr("DES_PAIS"), GetType(String))

        Return objPaisDetail

    End Function
End Class
