Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class TipoMaquina

#Region "[CONSULTAS]"

    Public Shared Function GetTipos(codTipoMaquina As String, bolActivo As Boolean?) As List(Of Comon.Clases.TipoMaquina)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaTipoMaquina)
        cmd.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(codTipoMaquina) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND COD_TIPO_MAQUINA = :COD_TIPO_MAQUINA "
            Else
                cmd.CommandText &= " WHERE COD_TIPO_MAQUINA = :COD_TIPO_MAQUINA "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MAQUINA", ProsegurDbType.Descricao_Longa, codTipoMaquina.ToUpper()))
        End If

        'Se a validade for informada
        If bolActivo IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO"
            Else
                cmd.CommandText &= " WHERE BOL_ACTIVO = :BOL_ACTIVO"
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, bolActivo))
        End If

        'cria o objeto de Maquina
        Dim lstTipoMaquina As New List(Of Comon.Clases.TipoMaquina)

        Dim dtTipoMaquina As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' caso encontre algum registro
        If dtTipoMaquina IsNot Nothing _
            AndAlso dtTipoMaquina.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtTipoMaquina.Rows

                lstTipoMaquina.Add(PopularGetTipoMaquina(dr))

            Next
        End If

        Return lstTipoMaquina
    End Function


#End Region

#Region "[INSERIR]"

#End Region

#Region "[UPDATE]"

#End Region

#Region "[DELETE]"

#End Region

#Region "[DEMAIS]"

    Private Shared Function PopularGetTipoMaquina(dr As DataRow) As Comon.Clases.TipoMaquina


        Dim objTipoMaquina As New Comon.Clases.TipoMaquina

        Util.AtribuirValorObjeto(objTipoMaquina.Identificador, dr("OID_TIPO_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.Codigo, dr("COD_TIPO_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.Descripcion, dr("DES_TIPO_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.Observacion, dr("OBS_TIPO_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.BolActivo, dr("BOL_ACTIVO"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoMaquina.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTipoMaquina.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTipoMaquina.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        ' retornar objeto divisa preenchido
        Return objTipoMaquina

    End Function

#End Region

End Class
