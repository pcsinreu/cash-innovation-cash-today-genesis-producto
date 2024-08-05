Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Documento
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.AccesoDatos.Genesis
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports System.Threading.Tasks
Imports Prosegur.Genesis.DataBaseHelper
Imports System.Text

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe Documento Padre
    ''' </summary>
    Public Class DocumentoPadre

#Region "[Integracion]"

#Region "[Metodos Base Para Consultas]"

        Public Shared Sub ObtenerDatosDeLosDocumentos(ByRef documentos As ObservableCollection(Of Clases.Documento), ByRef dtDocumentos As DataTable)

            If dtDocumentos IsNot Nothing AndAlso dtDocumentos.Rows.Count > 0 Then

                If documentos Is Nothing Then
                    documentos = New ObservableCollection(Of Clases.Documento)
                End If

                Dim identificadoresDocumento As New List(Of String)
                Dim identificadoresCuenta As New List(Of String)
                Dim identificadoresFormulario As New List(Of String)
                Dim identificadoresDocumentoPadre As New List(Of String)
                Dim identificadoresTipoDocumento As New List(Of String)

                For Each rowDocumento In dtDocumentos.Rows

                    Dim documento As New Clases.Documento

                    With documento

                        .Identificador = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO"), GetType(String)), Nothing)
                        .IdentificadorGrupo = If(rowDocumento.Table.Columns.Contains("OID_GRUPO_DOCUMENTO"), Util.AtribuirValorObj(rowDocumento("OID_GRUPO_DOCUMENTO"), GetType(String)), Nothing)
                        .IdentificadorMovimentacionFondo = If(rowDocumento.Table.Columns.Contains("OID_MOVIMENTACION_FONDO"), Util.AtribuirValorObj(rowDocumento("OID_MOVIMENTACION_FONDO"), GetType(String)), Nothing)
                        .IdentificadorSustituto = If(rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_SUSTITUTO"), Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_SUSTITUTO"), GetType(String)), Nothing)
                        .EstaCertificado = If(rowDocumento.Table.Columns.Contains("BOL_CERTIFICADO"), Util.AtribuirValorObj(rowDocumento("BOL_CERTIFICADO"), GetType(Boolean)), Nothing)
                        .NumeroExterno = If(rowDocumento.Table.Columns.Contains("COD_EXTERNO"), Util.AtribuirValorObj(rowDocumento("COD_EXTERNO"), GetType(String)), Nothing)
                        .CodigoComprobante = If(rowDocumento.Table.Columns.Contains("COD_COMPROBANTE"), Util.AtribuirValorObj(rowDocumento("COD_COMPROBANTE"), GetType(String)), Nothing)
                        .FechaHoraGestion = If(rowDocumento.Table.Columns.Contains("FYH_GESTION"), Util.AtribuirValorObj(rowDocumento("FYH_GESTION"), GetType(DateTime)), Nothing)
                        .FechaHoraPlanificacionCertificacion = If(rowDocumento.Table.Columns.Contains("FYH_PLAN_CERTIFICACION"), Util.AtribuirValorObj(rowDocumento("FYH_PLAN_CERTIFICACION"), GetType(DateTime)), Nothing)
                        .Estado = If(rowDocumento.Table.Columns.Contains("COD_ESTADO") AndAlso Not rowDocumento("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowDocumento("COD_ESTADO").ToString), Nothing)
                        .EstadosPosibles = ObtenerEstadosPossibles(.Estado)

                        If rowDocumento.Table.Columns.Contains("OID_FORMULARIO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))) Then
                            .Formulario = New Clases.Formulario()
                            .Formulario.Identificador = Util.AtribuirValorObj(rowDocumento("OID_FORMULARIO"), GetType(String))
                            identificadoresFormulario.Add(.Formulario.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_DOCUMENTO_PADRE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))) Then
                            .DocumentoPadre = New Clases.Documento()
                            .DocumentoPadre.Identificador = Util.AtribuirValorObj(rowDocumento("OID_DOCUMENTO_PADRE"), GetType(String))
                            identificadoresDocumentoPadre.Add(.DocumentoPadre.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_TIPO_DOCUMENTO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))) Then
                            .TipoDocumento = New Clases.TipoDocumento()
                            .TipoDocumento.Identificador = Util.AtribuirValorObj(rowDocumento("OID_TIPO_DOCUMENTO"), GetType(String))
                            identificadoresTipoDocumento.Add(.TipoDocumento.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_CUENTA_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))) Then
                            .CuentaOrigen = New Clases.Cuenta()
                            .CuentaOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_ORIGEN"), GetType(String))
                            identificadoresCuenta.Add(.CuentaOrigen.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_CUENTA_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))) Then
                            .CuentaDestino = New Clases.Cuenta()
                            .CuentaDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_DESTINO"), GetType(String))
                            identificadoresCuenta.Add(.CuentaDestino.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_ORIGEN") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))) Then
                            .CuentaSaldoOrigen = New Clases.Cuenta()
                            .CuentaSaldoOrigen.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_ORIGEN"), GetType(String))
                            identificadoresCuenta.Add(.CuentaSaldoOrigen.Identificador)
                        End If
                        If rowDocumento.Table.Columns.Contains("OID_CUENTA_SALDO_DESTINO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))) Then
                            .CuentaSaldoDestino = New Clases.Cuenta()
                            .CuentaSaldoDestino.Identificador = Util.AtribuirValorObj(rowDocumento("OID_CUENTA_SALDO_DESTINO"), GetType(String))
                            identificadoresCuenta.Add(.CuentaSaldoDestino.Identificador)
                        End If
                        identificadoresDocumento.Add(.Identificador)

                    End With

                    documentos.Add(documento)

                Next


                Dim cuentas As ObservableCollection(Of Clases.Cuenta) = Nothing
                Dim dtHistoricos As DataTable = Nothing
                Dim dtValoresTermino As DataTable = Nothing
                Dim elementos As ObservableCollection(Of Clases.Remesa) = Nothing
                Dim formularios As ObservableCollection(Of Clases.Formulario) = Nothing
                Dim documentosPadres As ObservableCollection(Of Clases.Documento) = Nothing

                ' Cuentas (Origen, Destino, Movimiento y Saldos) - (OID_CUENTA_ORIGEN, OID_CUENTA_DESTINO, OID_CUENTA_SALDO_ORIGEN, OID_CUENTA_SALDO_DESTINO)
                Dim TCuentas As New Task(Sub()
                                             cuentas = Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Ambos, "ObtenerDatosDeLosDocumentos")
                                         End Sub)
                TCuentas.Start()

                ' Historico - (OID_DOCUMENTO)
                Dim THistorico As New Task(Sub()
                                               dtHistoricos = HistoricoMovimentacionDocumento.ObtenerHistoricoMovimentacion(identificadoresDocumento)
                                           End Sub)
                THistorico.Start()

                ' Formulario - (OID_FORMULARIO)
                Dim TFormulario As New Task(Sub()
                                                formularios = Formulario.ObtenerFormulariosPorIdentificadores_v2(identificadoresFormulario)
                                            End Sub)
                TFormulario.Start()

                ' DocumentoPadre - (OID_DOCUMENTO_PADRE)
                'Dim TDocumentosPadres As New Task(Sub()
                '                                      documentosPadres = DocumentoPadre.ObtenerDocumentosPorIdentificadores(identificadoresDocumentoPadre)
                '                                  End Sub)
                'TDocumentosPadres.Start()

                ' ValorTerminoDocumento - (OID_DOCUMENTO)
                Dim TValoresTermino As New Task(Sub()
                                                    dtValoresTermino = ValorTerminoDocumento.ObtenerValorTerminoDocumento_v2(identificadoresDocumento)
                                                End Sub)
                TValoresTermino.Start()

                ' Elemento - (OID_DOCUMENTO)
                Dim TElementos As New Task(Sub()
                                               elementos = Remesa.ObtenerRemesasPorDocumentos_SinProcedure(identificadoresDocumento)
                                           End Sub)
                TElementos.Start()

                ' Valores del Documento - (OID_DOCUMENTO)
                Divisas.ObtenerValoresDeLosDocumentos_v2(documentos)

                Task.WaitAll(New Task() {TCuentas, THistorico, TFormulario, TValoresTermino, TElementos})

                cargarDocumentos(documentos, dtHistoricos, formularios, cuentas, documentosPadres, dtValoresTermino, elementos)

            End If

        End Sub

        Public Shared Sub cargarDocumentos(ByRef documentos As ObservableCollection(Of Clases.Documento),
                                           ByRef dtHistoricos As DataTable,
                                           ByRef formularios As ObservableCollection(Of Clases.Formulario),
                                           ByRef cuentas As ObservableCollection(Of Clases.Cuenta),
                                           ByRef documentosPadres As ObservableCollection(Of Clases.Documento),
                                           ByRef dtValoresTermino As DataTable,
                                           ByRef elementos As ObservableCollection(Of Clases.Remesa))

            If documentos IsNot Nothing AndAlso documentos.Count > 0 Then

                For Each documento In documentos

                    ' Cargar DocumentosPadres
                    If documentosPadres IsNot Nothing AndAlso documentosPadres.Count > 0 AndAlso
                        documento.DocumentoPadre IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.DocumentoPadre.Identificador) Then
                        Dim padre = documentosPadres.FirstOrDefault(Function(x) x.Identificador = documento.DocumentoPadre.Identificador)
                        If padre IsNot Nothing AndAlso Not String.IsNullOrEmpty(padre.Identificador) Then
                            documento.DocumentoPadre = padre
                        End If
                    End If

                    ' Cargar Formulario
                    If formularios IsNot Nothing AndAlso formularios.Count > 0 AndAlso
                        documento.Formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.Formulario.Identificador) Then
                        Dim formulario = formularios.FirstOrDefault(Function(x) x.Identificador = documento.Formulario.Identificador)
                        If formulario IsNot Nothing AndAlso Not String.IsNullOrEmpty(formulario.Identificador) Then
                            documento.Formulario = formulario

                            ' Cargar Valores Terminos Documento
                            documento.GrupoTerminosIAC = formulario.GrupoTerminosIACIndividual
                            If documento.GrupoTerminosIAC IsNot Nothing AndAlso documento.GrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso _
                                documento.GrupoTerminosIAC.TerminosIAC.Count > 0 AndAlso dtValoresTermino IsNot Nothing AndAlso dtValoresTermino.Rows.Count > 0 Then

                                Dim filtroValoresTermino = dtValoresTermino.Select("OID_DOCUMENTO = '" & documento.Identificador & "'")
                                If filtroValoresTermino IsNot Nothing Then
                                    For Each rowValor In filtroValoresTermino
                                        Dim termino = documento.GrupoTerminosIAC.TerminosIAC.FirstOrDefault(Function(t) t.Identificador = Util.AtribuirValorObj(rowValor("OID_TERMINO"), GetType(String)))
                                        If termino IsNot Nothing Then
                                            termino.Valor = If(rowValor.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(rowValor("DES_VALOR"), GetType(String)), Nothing)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    End If

                    ' Cargar Cuentas
                    If cuentas IsNot Nothing AndAlso cuentas.Count > 0 Then
                        If documento.CuentaOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaOrigen.Identificador) Then
                            Dim cuentaOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaOrigen.Identificador)
                            If cuentaOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaOrigen.Identificador) Then
                                documento.CuentaOrigen = cuentaOrigen
                            End If
                        End If
                        If documento.CuentaDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaDestino.Identificador) Then
                            Dim cuentaDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaDestino.Identificador)
                            If cuentaDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaDestino.Identificador) Then
                                documento.CuentaDestino = cuentaDestino
                            End If
                        End If
                        If documento.CuentaSaldoOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaSaldoOrigen.Identificador) Then
                            Dim cuentaSaldoOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaSaldoOrigen.Identificador)
                            If cuentaSaldoOrigen IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaSaldoOrigen.Identificador) Then
                                documento.CuentaSaldoOrigen = cuentaSaldoOrigen
                            End If
                        End If
                        If documento.CuentaSaldoDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(documento.CuentaSaldoDestino.Identificador) Then
                            Dim cuentaSaldoDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = documento.CuentaSaldoDestino.Identificador)
                            If cuentaSaldoDestino IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuentaSaldoDestino.Identificador) Then
                                documento.CuentaSaldoDestino = cuentaSaldoDestino
                            End If
                        End If
                    End If

                    ' Cargar Historico Documento
                    If dtHistoricos IsNot Nothing AndAlso dtHistoricos.Rows.Count > 0 Then
                        Dim filtroHistorico = dtHistoricos.Select("OID_DOCUMENTO = '" & documento.Identificador & "'")
                        If filtroHistorico IsNot Nothing Then
                            If documento.Historico Is Nothing Then documento.Historico = New ObservableCollection(Of Clases.HistoricoMovimientoDocumento)
                            For Each rowHistorico In filtroHistorico
                                Dim historico As New Clases.HistoricoMovimientoDocumento
                                With historico
                                    .Estado = If(rowHistorico.Table.Columns.Contains("COD_ESTADO") AndAlso Not rowHistorico("COD_ESTADO").Equals(DBNull.Value), RecuperarEnum(Of Enumeradores.EstadoDocumento)(rowHistorico("COD_ESTADO").ToString), Nothing)
                                    .FechaHoraModificacion = If(rowHistorico.Table.Columns.Contains("GMT_MODIFICACION"), Util.AtribuirValorObj(rowHistorico("GMT_MODIFICACION"), GetType(DateTime)), Nothing)
                                    .UsuarioModificacion = If(rowHistorico.Table.Columns.Contains("DES_USUARIO_MODIFICACION"), Util.AtribuirValorObj(rowHistorico("DES_USUARIO_MODIFICACION"), GetType(String)), Nothing)
                                End With
                                documento.Historico.Add(historico)
                            Next
                        End If
                    End If

                    ' Cargar Elementos
                    If elementos IsNot Nothing AndAlso elementos.Count > 0 Then
                        Dim remesa = elementos.FirstOrDefault(Function(x) x.IdentificadorDocumento = documento.Identificador)
                        If remesa IsNot Nothing Then
                            documento.Elemento = remesa
                        End If
                    End If

                    ' remove todos os valores vazios e com valores iguais a zero (e que tenham quantidades iguais a zero também)
                    Prosegur.Genesis.Comon.Util.BorrarItemsDivisaSinValoresCantidades(documento)

                Next

            End If

        End Sub

        Private Shared Function ObtenerEstadosPossibles(ByRef estadoDocumento As Enumeradores.EstadoDocumento) As ObservableCollection(Of Enumeradores.EstadoDocumento)
            Dim estados As New ObservableCollection(Of Enumeradores.EstadoDocumento)
            Select Case estadoDocumento
                Case Enumeradores.EstadoDocumento.Nuevo
                    estados.Add(Enumeradores.EstadoDocumento.EnCurso)
                Case Enumeradores.EstadoDocumento.EnCurso
                    estados.Add(Enumeradores.EstadoDocumento.EnCurso)
                    estados.Add(Enumeradores.EstadoDocumento.Anulado)
                    estados.Add(Enumeradores.EstadoDocumento.Confirmado)
                Case Enumeradores.EstadoDocumento.Confirmado
                    estados.Add(Enumeradores.EstadoDocumento.Aceptado)
                    estados.Add(Enumeradores.EstadoDocumento.Rechazado)
                Case Enumeradores.EstadoDocumento.Aceptado
                    estados.Add(Enumeradores.EstadoDocumento.Sustituido)
                Case Else
                    Return Nothing
            End Select
            Return estados
        End Function

#End Region

#Region "[Obtener]"

        Public Shared Function ObtenerDocumentoPadreNoSustituto(identificadorDocumento As String,
                                                                usuario As String) As Clases.Documento

            Dim documento As Clases.Documento = Nothing

            If Not String.IsNullOrEmpty(identificadorDocumento) Then

                Dim identificadoresDocumentos As New List(Of String)

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorDocumento))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_ObtenerDocumentoPadreNoSustituto)
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            Dim filtro = dt.Select(" ESSUSTITUTO = 0 ")
                            If filtro IsNot Nothing Then
                                For Each rowValor In filtro
                                    identificadoresDocumentos.Add(Util.AtribuirValorObj(rowValor("OID_DOCUMENTO"), GetType(String)))
                                Next
                            End If

                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

                If identificadoresDocumentos IsNot Nothing AndAlso identificadoresDocumentos.Count > 0 Then
                    Dim documentos As ObservableCollection(Of Clases.Documento) = GenesisSaldos.Documento.recuperarDocumentosPorIdentificadores(identificadoresDocumentos, usuario, Nothing)
                    If documentos IsNot Nothing AndAlso documentos.Count > 0 Then
                        documento = documentos.FirstOrDefault
                    End If
                End If

            Else
                Return Nothing
            End If

            Return documento

        End Function

        Public Shared Function ObtenerCaracteristicasDocPadreNoSustituto(IdentificadorDocumento As String) As List(Of Enumeradores.CaracteristicaFormulario)

            Dim caracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)

            Try

                If Not String.IsNullOrEmpty(IdentificadorDocumento) Then

                    Dim identificadorDocumentoNoSustituto As New List(Of String)

                    Try
                        Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", _
                                                                                        ProsegurDbType.Objeto_Id, IdentificadorDocumento))

                            command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_ObtenerDocumentoPadreNoSustituto)
                            command.CommandType = CommandType.Text

                            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                Dim filtro = dt.Select(" ESSUSTITUTO = 0 ")
                                If filtro IsNot Nothing Then
                                    For Each rowValor In filtro
                                        identificadorDocumentoNoSustituto.Add(Util.AtribuirValorObj(rowValor("OID_DOCUMENTO"), GetType(String)))
                                    Next
                                End If

                            End If

                        End Using

                    Catch ex As Exception
                        Throw
                    Finally
                        GC.Collect()
                    End Try

                    If identificadorDocumentoNoSustituto IsNot Nothing AndAlso identificadorDocumentoNoSustituto.Count > 0 Then


                        Try
                            Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumentoNoSustituto(0)))

                                command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCaracteristicasFormularioPorIdentificadorDocumento)
                                command.CommandType = CommandType.Text

                                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                    For Each itemDT In dt.Rows
                                        If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")) Then
                                            caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")))
                                        End If
                                    Next

                                End If

                            End Using

                        Catch ex As Exception
                            Throw
                        Finally
                            GC.Collect()
                        End Try

                    End If

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally

            End Try

            Return caracteristicas

        End Function

        Public Shared Function ObtenerCaracteristicasDocPadreNoSustituto(IdentificadorDocumento As String, ByRef TransaccionActual As Transaccion) As List(Of Enumeradores.CaracteristicaFormulario)

            Dim caracteristicas As New List(Of Enumeradores.CaracteristicaFormulario)

            Try

                If Not String.IsNullOrEmpty(IdentificadorDocumento) Then

                    Dim identificadorDocumentoNoSustituto As New List(Of String)

                    Try
                        'Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.Documento_ObtenerDocumentoPadreNoSustituto), False, CommandType.Text)

                        wrapper.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, IdentificadorDocumento)

                        Dim ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, TransaccionActual)

                        Dim dt As DataTable = If(ds IsNot Nothing AndAlso ds.Tables.Count > 0, ds.Tables(0), Nothing)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            Dim filtro = dt.Select(" ESSUSTITUTO = 0 ")
                            If filtro IsNot Nothing Then
                                For Each rowValor In filtro
                                    identificadorDocumentoNoSustituto.Add(Util.AtribuirValorObj(rowValor("OID_DOCUMENTO"), GetType(String)))
                                Next
                            End If

                        End If

                        'End Using

                    Catch ex As Exception
                        Throw
                    Finally
                        GC.Collect()
                    End Try

                    If identificadorDocumentoNoSustituto IsNot Nothing AndAlso identificadorDocumentoNoSustituto.Count > 0 Then


                        Try
                            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCaracteristicasFormularioPorIdentificadorDocumento), False, CommandType.Text)
                            wrapper.AgregarParam("OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumentoNoSustituto(0))

                            Dim ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, TransaccionActual)
                            Dim dt As DataTable = If(ds IsNot Nothing AndAlso ds.Tables.Count > 0, ds.Tables(0), Nothing)

                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                                For Each itemDT In dt.Rows
                                    If ExisteEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")) Then
                                        caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaFormulario)(itemDT("COD_CARACT_FORMULARIO")))
                                    End If
                                Next

                            End If

                        Catch ex As Exception
                            Throw
                        Finally
                            GC.Collect()
                        End Try

                    End If

                End If

            Catch ex As Excepciones.ExcepcionLogica
                Throw New Exception(ex.Message)

            Catch ex As Exception
                Throw

            Finally

            End Try

            Return caracteristicas

        End Function

#End Region


#End Region

    End Class

End Namespace
