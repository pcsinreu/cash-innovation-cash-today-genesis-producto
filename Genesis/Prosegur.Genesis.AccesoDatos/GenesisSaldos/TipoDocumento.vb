Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text

Namespace GenesisSaldos

    Public Class TipoDocumento

        ''' <summary>
        ''' Recupera o Tipo de documento
        ''' </summary>
        ''' <param name="TipoDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoDocumento(TipoDocumento As Clases.TipoDocumento) As Clases.TipoDocumento

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TipoDocumentoRecuperarPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_DOCUMENTO", ProsegurDbType.Objeto_Id, TipoDocumento.Codigo))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objTipoDocumento As Clases.TipoDocumento = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objTipoDocumento = New Clases.TipoDocumento() With {
                                   .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_DOCUMENTO"), GetType(String)),
                                   .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_TIPO_DOCUMENTO"), GetType(String)),
                                   .EsExhibidoCertificado = Util.AtribuirValorObj(dt.Rows(0)("BOL_CERTIFICACION"), GetType(Boolean)),
                                   .EstaActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean)),
                                   .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime)),
                                   .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime)),
                                   .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_TIPO_DOCUMENTO"), GetType(String)),
                                   .Orden = Util.AtribuirValorObj(dt.Rows(0)("NEC_ORDEN"), GetType(Integer)),
                                   .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String)),
                                   .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))}




            End If

            Return objTipoDocumento
        End Function

        ''' <summary>
        ''' Recupera todos Tipos de documentos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarTipoDocumento() As List(Of Clases.TipoDocumento)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.TipoDocumentoRecuperarTodos, ""))
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objTipoDocumento As Clases.TipoDocumento = Nothing
            Dim listaTiposDocumentos As New List(Of Clases.TipoDocumento)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    objTipoDocumento = New Clases.TipoDocumento() With {
                                       .Codigo = Util.AtribuirValorObj(row("COD_TIPO_DOCUMENTO"), GetType(String)),
                                       .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_DOCUMENTO"), GetType(String)),
                                       .EsExhibidoCertificado = Util.AtribuirValorObj(row("BOL_CERTIFICACION"), GetType(Boolean)),
                                       .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean)),
                                       .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)),
                                       .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)),
                                       .Identificador = Util.AtribuirValorObj(row("OID_TIPO_DOCUMENTO"), GetType(String)),
                                       .Orden = Util.AtribuirValorObj(row("NEC_ORDEN"), GetType(Integer)),
                                       .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)),
                                       .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))}

                    listaTiposDocumentos.Add(objTipoDocumento)
                Next

            End If

            Return listaTiposDocumentos
        End Function

        ''' <summary>
        ''' Recuperar tipo documento certificación
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function RecuperarTipoDocumentoCertificacion() As List(Of Clases.TipoDocumento)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim sbFiltro As New StringBuilder()

            cmd.CommandType = CommandType.Text

            sbFiltro.AppendLine(" AND BOL_CERTIFICACION = 1 ")
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.TipoDocumentoRecuperarTodos, sbFiltro.ToString()))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objTipoDocumento As Clases.TipoDocumento = Nothing
            Dim listaTiposDocumentos As New List(Of Clases.TipoDocumento)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    objTipoDocumento = New Clases.TipoDocumento() With {
                                       .Codigo = Util.AtribuirValorObj(row("COD_TIPO_DOCUMENTO"), GetType(String)),
                                       .Descripcion = Util.AtribuirValorObj(row("DES_TIPO_DOCUMENTO"), GetType(String)),
                                       .EsExhibidoCertificado = Util.AtribuirValorObj(row("BOL_CERTIFICACION"), GetType(Boolean)),
                                       .EstaActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean)),
                                       .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)),
                                       .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)),
                                       .Identificador = Util.AtribuirValorObj(row("OID_TIPO_DOCUMENTO"), GetType(String)),
                                       .Orden = Util.AtribuirValorObj(row("NEC_ORDEN"), GetType(Integer)),
                                       .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)),
                                       .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))}

                    listaTiposDocumentos.Add(objTipoDocumento)
                Next

            End If

            Return listaTiposDocumentos
        End Function

    End Class

End Namespace



