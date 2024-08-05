Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace GenesisSaldos
    Public Class CaracteristicaFormulario

        ''' <summary>
        ''' Recupera a CaracteristicaFormulario pelo formulario
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 06/09/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarCaracteristicasFormulario(identificadorFormulario As String) As List(Of Enumeradores.CaracteristicaFormulario)

            Dim listaCaracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CaracteristicaFormularioRecuperarPorFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            Using dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each itemDT In dt.Rows
                    'Verifica se o Enum existe antes de add
                    'Quando voltava versão e havia alguma característica salva no BD que não existia no Enum dava erro, pois na versão que voltou não existia o Enum
                    If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")) Then
                        listaCaracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")))
                    End If
                Next

            End Using


            Return listaCaracteristicas
        End Function

        ''' <summary>
        ''' Recupera o id da caracteristica pelo código
        ''' </summary>
        ''' <param name="codigoCaracteristica"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [victor.ramos] 16/01/2014 - Criado
        ''' </history>
        Public Shared Function RecuperarIdentificadorCaracteristica(codigoCaracteristica As String) As String

            Dim identificador As String = String.Empty
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CaracteristicaFormularioRecuperarIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CARACT_FORMULARIO", ProsegurDbType.Descricao_Longa, codigoCaracteristica))

            Dim dt As DataTable
            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_CARACT_FORMULARIO"), GetType(String))
            Else
                Return String.Empty
            End If

        End Function

        ''' <summary>
        ''' Insere a característica do formulário
        ''' </summary>
        ''' <param name="caracteristicaFormulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarCaracteristicaFormulario(caracteristicaFormulario As Clases.CaracteristicaFormulario)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CaracteristicaFormularioInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CARACTFORMXFORMULARIO", ProsegurDbType.Objeto_Id, caracteristicaFormulario.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, caracteristicaFormulario.IdentificadorFormulario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CARACT_FORMULARIO", ProsegurDbType.Objeto_Id, caracteristicaFormulario.IdentificadorCaracteristica))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, caracteristicaFormulario.CodigoMigracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, caracteristicaFormulario.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, caracteristicaFormulario.UsuarioModificacion))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
        ''' <summary>
        ''' Exclui a características do formulário
        ''' </summary>
        ''' <param name="identificadorFormulario"></param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarCaracteristicaFormularioPorFormulario(identificadorFormulario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CaracteristicaFormularioBorrarPorFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub

    End Class
End Namespace

