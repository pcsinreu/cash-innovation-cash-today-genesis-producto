Imports System.Text
Imports Prosegur.DbHelper

Public Class Formulario

    Public Shared Function getFormularios(objPeticion As ContractoServicio.Formulario.GetFormularios.Peticion) As ContractoServicio.Formulario.GetFormularios.FormularioColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetFormulario.ToString)



        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto divisa coleccion
        Dim objFormularios As New ContractoServicio.Formulario.GetFormularios.FormularioColeccion

        ' executar query
        Dim dtFormulario As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtFormulario IsNot Nothing _
            AndAlso dtFormulario.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtFormulario.Rows

                ' preencher a coleção com objetos divisa
                objFormularios.Add(PopularGetFormularios(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objFormularios

    End Function
    Private Shared Function PopularGetFormularios(dr As DataRow) As ContractoServicio.Formulario.GetFormularios.Formulario

        ' criar objeto divisa
        Dim objFormulario As New ContractoServicio.Formulario.GetFormularios.Formulario

        Util.AtribuirValorObjeto(objFormulario.Identificador, dr("OID_FORMULARIO"), GetType(String))
        Util.AtribuirValorObjeto(objFormulario.Descripcion, dr("DES_FORMULARIO"), GetType(String))
        Util.AtribuirValorObjeto(objFormulario.Codigo, dr("COD_FORMULARIO"), GetType(String))

        ' retornar objeto divisa preenchido
        Return objFormulario

    End Function
End Class
