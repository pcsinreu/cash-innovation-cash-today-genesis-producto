Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo

Namespace GenesisSaldos
    Public Class DocumentoElemento

#Region "[Consultas]"

        ''' <summary>
        ''' Recupera os bultos, documento e Estado do bultos na tabela de documento elemento, para todos os documentos ou somente para os documentos diferente do atual.
        ''' </summary>
        ''' <param name="bulto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DocumentoElementoRecuperarPorBulto(bulto As Clases.Bulto, diferenteDocumentoAtual As Boolean) As List(Of Clases.Bulto)
            Dim bultos As New List(Of Clases.Bulto)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.DocumentoElementoRecuperarPorIdentificadorBulto
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, bulto.Identificador))

            'Recupera os bultos somente para os documentos diferente do documento atual.
            If diferenteDocumentoAtual Then
                cmd.CommandText += " AND OID_DOCUMENTO <> []OID_DOCUMENTO"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, bulto.IdentificadorDocumento))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr In dt.Rows
                    Dim objBulto As New Clases.Bulto
                    objBulto.Identificador = dr("OID_BULTO")
                    objBulto.IdentificadorDocumento = dr("OID_DOCUMENTO")

                    objBulto.UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))

                    If dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.Historico.RecuperarValor Then
                        objBulto.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.Historico
                    ElseIf dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.EnTransito.RecuperarValor Then
                        objBulto.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.EnTransito
                    ElseIf dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor Then
                        objBulto.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.Concluido
                    End If

                    bultos.Add(objBulto)
                Next
            End If

            Return bultos
        End Function

        ''' <summary>
        ''' Recupera os bultos, documento e Estado do bultos na tabela de documento elemento, para todos os documentos ou somente para os documentos diferente do atual.
        ''' </summary>
        ''' <param name="remesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DocumentoElementoRecuperarPorRemesa(remesa As Clases.Remesa, diferenteDocumentoAtual As Boolean) As List(Of Clases.Remesa)
            Dim remesas As New List(Of Clases.Remesa)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.DocumentoElementoRecuperarPorIdentificadorRemesa
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, remesa.Identificador))

            'Recupera os bultos somente para os documentos diferente do documento atual.
            If diferenteDocumentoAtual Then
                cmd.CommandText += " AND OID_DOCUMENTO <> []OID_DOCUMENTO"
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, remesa.IdentificadorDocumento))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr In dt.Rows
                    Dim objRemesa As New Clases.Remesa
                    objRemesa.Identificador = dr("OID_REMESA")
                    objRemesa.IdentificadorDocumento = dr("OID_DOCUMENTO")

                    If dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.Historico.RecuperarValor Then
                        objRemesa.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.Historico
                    ElseIf dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.EnTransito.RecuperarValor Then
                        objRemesa.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.EnTransito
                    ElseIf dr("COD_ESTADO_DOCXELEMENTO") = Enumeradores.EstadoDocumentoElemento.Concluido.RecuperarValor Then
                        objRemesa.EstadoDocumentoElemento = Enumeradores.EstadoDocumentoElemento.Concluido
                    End If

                    remesas.Add(objRemesa)
                Next
            End If

            Return remesas
        End Function

        ''' <summary>
        ''' Recupera informações adicionais dos elementos, como: conta origen, estados documentoxelemento
        ''' </summary>
        ''' <param name="identificadoresElementos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function DocumentoElementoRecuperarInformacionesAdicionales(identificadoresElementos As List(Of String)) As ObservableCollection(Of RecuperarSaldoExpuestoxDetallado.InformacionesAdicionales)

            Dim informaciones As New ObservableCollection(Of RecuperarSaldoExpuestoxDetallado.InformacionesAdicionales)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim filtroInRemesa As String = String.Empty
            Dim filtroInBulto As String = String.Empty

            cmd.CommandText = My.Resources.DocumentoElementoRecuperarInformacionesAdicionales
            cmd.CommandType = CommandType.Text

            filtroInRemesa = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresElementos, "OID_REMESA", cmd, String.Empty, "DOCE", , False)
            filtroInBulto = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresElementos, "OID_BULTO", cmd, String.Empty, "DOCE", , False)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, filtroInRemesa, filtroInBulto))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim IdentificadorElemento As String = String.Empty
            Dim objInformacion As RecuperarSaldoExpuestoxDetallado.InformacionesAdicionales
            Dim objEstadosDocumento As Nullable(Of Enumeradores.EstadoDocumento)
            Dim objEstadoDocumentoElemento As Nullable(Of Enumeradores.EstadoDocumentoElemento)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr In dt.Rows

                    If Util.AtribuirValorObj(dr("R_BOL_GESTION_BULTO"), GetType(Integer)).Equals(0) Then
                        IdentificadorElemento = Util.AtribuirValorObj(dr("IDENTIFICADOR_REMESA"), GetType(String))
                    Else
                        IdentificadorElemento = Util.AtribuirValorObj(dr("IDENTIFICADOR_BULTO"), GetType(String))
                    End If

                    If Not String.IsNullOrEmpty(IdentificadorElemento) Then

                        'Recupera o objeto informacion atual
                        objInformacion = (From i In informaciones Where Not String.IsNullOrEmpty(i.IdentificadorElemento) AndAlso
                                                                           i.IdentificadorElemento = IdentificadorElemento).FirstOrDefault

                        If objInformacion Is Nothing Then

                            Dim sector As New Clases.Sector
                            Dim estadosDocumento As New ObservableCollection(Of Enumeradores.EstadoDocumento)
                            Dim estadosDocumentoElemento As New ObservableCollection(Of Enumeradores.EstadoDocumentoElemento)

                            sector.Identificador = Util.AtribuirValorObj(dr("IDENTIFICADOR_SECTOR"), GetType(String))
                            sector.Codigo = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String))
                            sector.Descripcion = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String))

                            estadosDocumento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("ESTADO_DOCUMENTO"), GetType(String))))
                            estadosDocumentoElemento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(dr("ESTADO_DOCXELEMENTO"), GetType(String))))

                            informaciones.Add(New RecuperarSaldoExpuestoxDetallado.InformacionesAdicionales With {.IdentificadorElemento = IdentificadorElemento, .SectorOrigenElemento = sector, _
                                                                                                                 .EstadosDocumento = estadosDocumento, .EstadosDocumentoElemento = estadosDocumentoElemento})

                            objInformacion = (From i In informaciones Where Not String.IsNullOrEmpty(i.IdentificadorElemento) AndAlso
                                                                          i.IdentificadorElemento = IdentificadorElemento).FirstOrDefault

                        End If

                        If objInformacion IsNot Nothing Then

                            If objInformacion.EstadosDocumento IsNot Nothing AndAlso objInformacion.EstadosDocumento.Count > 0 Then
                                objEstadosDocumento = Nothing
                                objInformacion.EstadosDocumento.Foreach(Sub(ed)
                                                                            If ed.RecuperarValor().Equals(Util.AtribuirValorObj(dr("ESTADO_DOCUMENTO"), GetType(String))) Then
                                                                                objEstadosDocumento = ed
                                                                            End If
                                                                        End Sub)
                                If objEstadosDocumento Is Nothing Then
                                    objInformacion.EstadosDocumento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumento)(Util.AtribuirValorObj(dr("ESTADO_DOCUMENTO"), GetType(String))))
                                End If
                            End If

                            If objInformacion.EstadosDocumentoElemento IsNot Nothing AndAlso objInformacion.EstadosDocumentoElemento.Count > 0 Then

                                objEstadoDocumentoElemento = Nothing
                                objInformacion.EstadosDocumentoElemento.Foreach(Sub(ede)
                                                                                    If ede.RecuperarValor().Equals(Util.AtribuirValorObj(dr("ESTADO_DOCXELEMENTO"), GetType(String))) Then
                                                                                        objEstadoDocumentoElemento = ede
                                                                                    End If
                                                                                End Sub)
                                If objEstadoDocumentoElemento Is Nothing Then
                                    objInformacion.EstadosDocumentoElemento.Add(RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(dr("ESTADO_DOCXELEMENTO"), GetType(String))))
                                End If

                            End If
                        End If
                    End If
                    IdentificadorElemento = String.Empty
                Next
            End If
            Return informaciones

        End Function

        ''' <summary>
        ''' buscar cronológicamente (desde el más nuevo hasta el más viejo) las informaciones de histórico relacionadas: Formulário, Sector Origen, Sector Destino, Fecha y Hora (creación)
        ''' </summary>
        Public Shared Function RecuperarHistorico(esGestionBulto As Boolean, idElemento As String) As DataTable
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.DocumentoElementoHistoricoElemento
            cmd.CommandType = CommandType.Text
            If esGestionBulto Then
                cmd.CommandText = String.Format(cmd.CommandText, " AND BUL.OID_BULTO = []OID_BULTO")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, idElemento))
            Else
                cmd.CommandText = String.Format(cmd.CommandText, " AND REM.OID_REMESA = []OID_REMESA")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, idElemento))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Return dt
        End Function

#End Region

#Region "[Inserir]"

        ''' <summary>
        ''' Inserir
        ''' </summary>
        ''' <param name="valores"></param>
        ''' <remarks></remarks>
        Public Shared Sub DocumentoElementoInserir(valores As ObservableCollection(Of Clases.Transferencias.DocumentoElementoInserir),
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Try
                For Each valor As Clases.Transferencias.DocumentoElementoInserir In valores

                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DocumentoElementoInserir)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTOXELEMENTO", ProsegurDbType.Objeto_Id, valor.identificadorDocumentoElemento))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, valor.identificadorDocumento))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, valor.identificadorRemesa))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, valor.identificadorBulto))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, valor.usuarioModificacion))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, valor.usuarioModificacion))

                        If _transacion IsNot Nothing Then
                            _transacion.AdicionarItemTransacao(command)
                        Else
                            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)
                        End If

                    End Using

                Next
            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try
        End Sub

#End Region

#Region "[Actualizar]"

        ''' <summary>
        ''' Actualizar
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub Actualizar(estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento,
                                     usuario As String,
                                     identificadorDocumento As String,
                            Optional identificadorRemesa As String = "",
                            Optional cuantidadBultos As Integer = 0,
                            Optional identificadorBulto As String = "")

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""

                    If Not String.IsNullOrEmpty(identificadorRemesa) Then

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                        filtro &= " AND OID_REMESA = []OID_REMESA AND OID_BULTO IS NULL "

                        If cuantidadBultos > 0 Then
                            filtro &= " AND (SELECT COUNT(1) FROM SAPR_TBULTO B WHERE B.OID_REMESA = []OID_REMESA) = []CUANTIDADBULTOS "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "CUANTIDADBULTOS", ProsegurDbType.Inteiro_Curto, cuantidadBultos))
                        Else
                            Exit Try
                        End If

                    ElseIf Not String.IsNullOrEmpty(identificadorBulto) Then

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
                        filtro &= " AND OID_BULTO = []OID_BULTO "

                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Descricao_Curta, estadoDocumentoElemento.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DocumentoElementoActualizar, filtro))
                    command.CommandType = CommandType.Text

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

#End Region

#Region "[Eliminar]"

        ''' <summary>
        ''' Eliminar
        ''' </summary>
        ''' <param name="identificadorDocumento"></param>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub Eliminar(identificadorDocumento As String,
                          Optional identificadorRemesa As String = "",
                          Optional identificadorBulto As String = "")

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""

                    If Not String.IsNullOrEmpty(identificadorRemesa) Then

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                        filtro &= " AND OID_REMESA = []OID_REMESA "

                    End If

                    If Not String.IsNullOrEmpty(identificadorBulto) Then

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
                        filtro &= " AND OID_BULTO = []OID_BULTO "

                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.DocumentoElementoExcluir, filtro))
                    command.CommandType = CommandType.Text

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

#End Region

    End Class

End Namespace

