Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text

Namespace Genesis
    Public Class TiposBultos

        ''' <summary>
        ''' Recupera todos los tipo de bultos.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperaTiposBultos() As List(Of Clases.TipoBulto)

            Dim listaTipoBultos As New List(Of Clases.TipoBulto)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoBultoRecuperarTodos)
            cmd.CommandType = CommandType.Text

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim tipoBulto As New Clases.TipoBulto
                    With tipoBulto
                        .Identificador = Util.AtribuirValorObj(row("OID_TIPO_BULTO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_TIPO_BULTO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_BULTO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                    End With
                    listaTipoBultos.Add(tipoBulto)
                Next
            End If

            Return listaTipoBultos
        End Function
        ''' <summary>
        ''' Método que insere os tipos de bultos para um formulario
        ''' </summary>
        ''' <param name="identificadorFormulario">Oid Formulário</param>
        ''' <param name="tiposBultos">Tipos Bultos</param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarTiposBultosFormulario(identificadorFormulario As String, tiposBultos As List(Of Clases.TipoBulto))

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoBultoInserirPorFormulario)
            cmd.CommandType = CommandType.Text

            For Each tipoBulto In tiposBultos

                cmd.Parameters.Clear()

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIOXTIPO_BULTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_BULTO", ProsegurDbType.Objeto_Id, tipoBulto.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, tipoBulto.UsuarioCreacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, tipoBulto.UsuarioModificacion))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            Next

        End Sub
        ''' <summary>
        ''' Método que exclui os tipos de bultos de um formulario
        ''' </summary>
        ''' <param name="identificadorFormulario">Oid Formulário</param>
        ''' <remarks></remarks>
        Public Shared Sub BorrarTiposBultosFormulario(identificadorFormulario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoBultoExluirPorFormulario)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identificadorFormulario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

        End Sub
        ''' <summary>
        ''' Recupera todos os tipo de bultos de um formulário
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperaTiposBultos(identidicadorFormulario) As List(Of Clases.TipoBulto)

            Dim listaTipoBultos As New List(Of Clases.TipoBulto)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, identidicadorFormulario))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoBultoRecuperarTipoBultoFormulario)
            cmd.CommandType = CommandType.Text

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim tipoBulto As New Clases.TipoBulto
                    With tipoBulto
                        .Identificador = Util.AtribuirValorObj(row("OID_TIPO_BULTO"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_TIPO_BULTO"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_BULTO"), GetType(String))
                        .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                        .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime))
                        .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String))
                        .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime))
                        .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                    End With
                    listaTipoBultos.Add(tipoBulto)
                Next
            End If

            Return listaTipoBultos
        End Function

    End Class
End Namespace
