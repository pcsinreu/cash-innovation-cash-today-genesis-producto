Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace GenesisSaldos

    ''' <summary>
    ''' almacena el historico de movimentacion de un DOCUMENTO en el flujo.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HistoricoMovimentacionDocumento

        ''' <summary>
        ''' Grabar la movimientación del documento.
        ''' </summary>
        ''' <param name="identificador">Identificador del Documento</param>
        ''' <param name="estado">Estado del Documento</param>
        ''' <param name="usuario">Usuario que está haciendo la actualización</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarHistoricoMovimentacion(identificador As String,
                                                       estado As Enumeradores.EstadoDocumento,
                                                       Usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentacionDocumentoInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_HIST_MOV_DOCUMENTO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor()))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, Usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, Usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub


        Shared Function ObtenerHistoricoMovimentacion(ByRef identificadoresDocumento As List(Of String)) As DataTable

            Dim dtHistoricos As New DataTable

            If identificadoresDocumento IsNot Nothing AndAlso identificadoresDocumento.Count > 0 Then
                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim query As String = My.Resources.HistoricoMovimentacionDocumentosObtener

                        If identificadoresDocumento.Count = 1 Then

                            query &= " AND OID_DOCUMENTO = []OID_DOCUMENTO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", _
                                                                                        ProsegurDbType.Objeto_Id, identificadoresDocumento(0)))

                        Else

                            query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresDocumento, "OID_DOCUMENTO", _
                                                                                   command, "AND")

                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, query)
                        command.CommandType = CommandType.Text

                        dtHistoricos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return dtHistoricos
        End Function






















        ''' <summary>
        ''' Recupera o historico dos documentos.
        ''' </summary>
        ''' <param name="IdentificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarHistoricoMovimentacion(IdentificadorDocumento As String) As ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentacionDocumentosRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim objHistorico As ObservableCollection(Of Clases.HistoricoMovimientoDocumento) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objHistorico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

                For Each dr In dt.Rows

                    objHistorico.Add(New Clases.HistoricoMovimientoDocumento With { _
                                     .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))), _
                                     .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                                     .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                                     .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                                     .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))})

                Next

            End If

            Return objHistorico
        End Function


        ''' <summary>
        ''' Recupera o historico dos documentos.
        ''' </summary>
        ''' <param name="IdentificadorDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarHistoricoMovimentacion(IdentificadorDocumento As String, delegacion As Clases.Delegacion) As ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentacionDocumentosRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim objHistorico As ObservableCollection(Of Clases.HistoricoMovimientoDocumento) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objHistorico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)

                For Each dr In dt.Rows

                    objHistorico.Add(New Clases.HistoricoMovimientoDocumento With { _
                                     .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String))), _
                                     .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)).QuieroExibirEstaFechaEnLaPatalla(delegacion), _
                                     .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)).QuieroExibirEstaFechaEnLaPatalla(delegacion), _
                                     .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                                     .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))})

                Next

            End If

            Return objHistorico
        End Function

        ''' <summary>
        ''' Exlui a mivimentação do documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Es una referencia a la entidad de DOCUMENTO</param>
        ''' <remarks></remarks>
        Public Shared Sub HistoricoMovimentacionDocumentoExcluir(identificadorDocumento As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.HistoricoMovimentacionDocumentoExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        
    End Class
End Namespace
