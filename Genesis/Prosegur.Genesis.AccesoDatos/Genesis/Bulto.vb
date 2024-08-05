Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.DataBaseHelper

Namespace Genesis

    Public Class Bulto

        Public Shared Function cargarBultos(dtBultos As DataTable, cuentas As List(Of Clases.Cuenta)) As ObservableCollection(Of Clases.Bulto)

            Dim bultos As New ObservableCollection(Of Clases.Bulto)

            If dtBultos IsNot Nothing AndAlso dtBultos.Rows.Count > 0 Then

                For Each rowBultos In dtBultos.Rows

                    If bultos.FirstOrDefault(Function(r) r.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_BULTO"), GetType(String))) Is Nothing Then
                        Dim bulto As New Clases.Bulto

                        With bulto

                            .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(rowBultos("B_COD_ESTADO"), GetType(String)))
                            .Cuenta = cuentas.FirstOrDefault(Function(c) c.Identificador = rowBultos("B_OID_CUENTA"))
                            .CuentaSaldo = cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowBultos.Table.Columns.Contains("B_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_CUENTA_SALDO"), GetType(String))), rowBultos("B_OID_CUENTA_SALDO"), Nothing))
                            .Identificador = Util.AtribuirValorObj(rowBultos("B_OID_BULTO"), GetType(String))
                            .IdentificadorExterno = Util.AtribuirValorObj(rowBultos("B_OID_EXTERNO"), GetType(String))
                            .IdentificadorDocumento = Util.AtribuirValorObj(rowBultos("B_OID_DOCUMENTO_DE"), GetType(String))
                            .FechaHoraCreacion = Util.AtribuirValorObj(rowBultos("B_GMT_CREACION"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(rowBultos("B_GMT_MODIFICACION"), GetType(DateTime))
                            .FechaProcessoLegado = Util.AtribuirValorObj(rowBultos("B_FYH_PROCESO_LEGADO"), GetType(DateTime))
                            .FechaHoraTransporte = Util.AtribuirValorObj(rowBultos("R_FYH_TRANSPORTE"), GetType(DateTime))
                            .FechaHoraInicioConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_INICIO"), GetType(DateTime))
                            .FechaHoraFinConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_FIN"), GetType(DateTime))
                            .CodigoBolsa = Util.AtribuirValorObj(rowBultos("B_COD_BOLSA"), GetType(String))
                            .CantidadParciales = Util.AtribuirValorObj(rowBultos("B_NEL_CANTIDAD_PARCIALES"), GetType(Integer))
                            .Cuadrado = Util.AtribuirValorObj(rowBultos("B_BOL_CUADRADO"), GetType(Boolean))
                            .Precintos = New ObservableCollection(Of String)
                            .Precintos.Add(Util.AtribuirValorObj(rowBultos("B_COD_PRECINTO"), GetType(String)))
                            .PuestoResponsable = Util.AtribuirValorObj(rowBultos("B_COD_PUESTO_RESPONSABLE"), GetType(String))
                            .TipoUbicacion = If(String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))), Nothing,
                                              Extenciones.RecuperarEnum(Of Enumeradores.TipoUbicacion)(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))))
                            .UsuarioCreacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_MODIFICACION"), GetType(String))
                            .UsuarioResponsable = Util.AtribuirValorObj(rowBultos("B_COD_USUARIO_RESPONSABLE"), GetType(String))
                            .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowBultos("B_COD_ESTADO_DOCXELEMENTO"), GetType(String)))
                            .ConfiguracionNivelSaldos = If(rowBultos.Table.Columns.Contains("B_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowBultos("B_COD_NIVEL_DETALLE"), GetType(String))), Nothing)

                            If rowBultos("B_ROWVER") Is DBNull.Value Then
                                .Rowver = 0
                            Else
                                .Rowver = Util.AtribuirValorObj(rowBultos("B_ROWVER"), GetType(Int64))
                            End If

                            If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_BULTO_PADRE"), GetType(String)))) Then
                                .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_BULTO_PADRE"), GetType(String))}
                            End If

                            Dim result = From d In dtBultos.AsEnumerable()
                                         Where d.Field(Of String)("B_OID_BULTO") = .Identificador AndAlso Not String.IsNullOrEmpty(d.Field(Of String)("P_OID_PARCIAL"))
                                         Select d
                            If result IsNot Nothing AndAlso result.Count > 0 Then
                                Dim dtParciales = result.CopyToDataTable()
                                .Parciales = Genesis.Parcial.cargarParciales(dtParciales)
                            End If

                            .Divisas = New ObservableCollection(Of Clases.Divisa)

                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_IAC"), GetType(String))) Then
                                .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_IAC"), GetType(String))}
                            End If
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_IAC_PARCIALES"), GetType(String))) Then
                                .GrupoTerminosIACParciales = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_IAC_PARCIALES"), GetType(String))}
                            End If

                        End With
                        bultos.Add(bulto)
                    End If

                Next

            End If

            Return bultos
        End Function


        ''' <summary>
        ''' Obsoleto: deve morrer e passar tudo para procedure
        ''' </summary>
        ''' <param name="dtBultos"></param>
        ''' <param name="cuentas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function cargarBultos_SinProcedure(dtBultos As DataTable, cuentas As ObservableCollection(Of Clases.Cuenta)) As ObservableCollection(Of Clases.Bulto)

            Dim bultos As New ObservableCollection(Of Clases.Bulto)
            Dim dvBultos As DataTable = dtBultos.DefaultView.ToTable(True, "OID_BULTO")

            If dtBultos IsNot Nothing AndAlso dtBultos.Rows.Count > 0 Then

                For Each rowIdBultos In dvBultos.Rows
                    Dim rowBultos As DataRow = dtBultos.Select("OID_BULTO = '" + rowIdBultos("OID_BULTO").ToString() + "'")(0)

                    Dim bulto As New Clases.Bulto

                    With bulto
                        If rowBultos("B_ROWVER") Is DBNull.Value Then
                            .Rowver = 0
                        Else
                            .Rowver = Util.AtribuirValorObj(rowBultos("B_ROWVER"), GetType(Int64))
                        End If

                        .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoBulto)(Util.AtribuirValorObj(rowBultos("B_COD_ESTADO"), GetType(String)))
                        .Cuenta = cuentas.FirstOrDefault(Function(c) c.Identificador = rowBultos("B_OID_CUENTA"))
                        .CuentaSaldo = cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowBultos.Table.Columns.Contains("B_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_CUENTA_SALDO"), GetType(String))), rowBultos("B_OID_CUENTA_SALDO"), Nothing))
                        .Identificador = Util.AtribuirValorObj(rowBultos("OID_BULTO"), GetType(String))
                        .IdentificadorExterno = Util.AtribuirValorObj(rowBultos("B_OID_EXTERNO"), GetType(String))
                        .IdentificadorDocumento = Util.AtribuirValorObj(rowBultos("B_OID_DOCUMENTO"), GetType(String))
                        .FechaHoraCreacion = Util.AtribuirValorObj(rowBultos("B_GMT_CREACION"), GetType(DateTime))
                        .FechaHoraModificacion = Util.AtribuirValorObj(rowBultos("B_GMT_MODIFICACION"), GetType(DateTime))
                        .FechaProcessoLegado = Util.AtribuirValorObj(rowBultos("B_FYH_PROCESO_LEGADO"), GetType(DateTime))
                        .FechaHoraTransporte = Util.AtribuirValorObj(rowBultos("R_FYH_TRANSPORTE"), GetType(DateTime))
                        .FechaHoraInicioConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_INICIO"), GetType(DateTime))
                        .FechaHoraFinConteo = Util.AtribuirValorObj(rowBultos("B_FYH_CONTEO_FIN"), GetType(DateTime))
                        .CodigoBolsa = Util.AtribuirValorObj(rowBultos("B_COD_BOLSA"), GetType(String))
                        .CantidadParciales = Util.AtribuirValorObj(rowBultos("B_NEL_CANTIDAD_PARCIALES"), GetType(Integer))
                        .Cuadrado = Util.AtribuirValorObj(rowBultos("BOL_CUADRADO"), GetType(Boolean))
                        .Precintos = New ObservableCollection(Of String)
                        .Precintos.Add(Util.AtribuirValorObj(rowBultos("B_COD_PRECINTO"), GetType(String)))
                        .PuestoResponsable = Util.AtribuirValorObj(rowBultos("B_COD_PUESTO_RESPONSABLE"), GetType(String))
                        .TipoUbicacion = If(String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))), Nothing,
                                          Extenciones.RecuperarEnum(Of Enumeradores.TipoUbicacion)(Util.AtribuirValorObj(rowBultos("B_TipoUbicacion"), GetType(String))))
                        .UsuarioCreacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_CREACION"), GetType(String))
                        .UsuarioModificacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_MODIFICACION"), GetType(String))
                        .UsuarioResponsable = Util.AtribuirValorObj(rowBultos("B_COD_USUARIO_RESPONSABLE"), GetType(String))
                        .EstadoDocumentoElemento = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumentoElemento)(Util.AtribuirValorObj(rowBultos("COD_ESTADO_DOCXELEMENTO_B"), GetType(String)))
                        .ConfiguracionNivelSaldos = If(rowBultos.Table.Columns.Contains("B_COD_NIVEL_DETALLE"), Extenciones.RecuperarEnum(Of Enumeradores.ConfiguracionNivelSaldos)(Util.AtribuirValorObj(rowBultos("B_COD_NIVEL_DETALLE"), GetType(String))), Nothing)
                        If (Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("OID_BULTO_PADRE"), GetType(String)))) Then
                            .ElementoPadre = New Clases.Remesa With {.Identificador = Util.AtribuirValorObj(rowBultos("OID_BULTO_PADRE"), GetType(String))}
                        End If

                        Dim result = From d In dtBultos.AsEnumerable()
                                     Where d.Field(Of String)("OID_BULTO") = .Identificador AndAlso Not String.IsNullOrEmpty(d.Field(Of String)("OID_PARCIAL"))
                                     Select d
                        If result IsNot Nothing AndAlso result.Count > 0 Then
                            Dim dtParciales = result.CopyToDataTable()
                            .Parciales = Genesis.Parcial.cargarParciales_SinProcedure(dtParciales)
                        End If

                        .Divisas = New ObservableCollection(Of Clases.Divisa)

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_IAC"), GetType(String))) Then
                            .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_IAC"), GetType(String))}
                        End If
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_IAC_PARCIALES"), GetType(String))) Then
                            .GrupoTerminosIACParciales = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowBultos("B_OID_IAC_PARCIALES"), GetType(String))}
                        End If

                    End With
                    bultos.Add(bulto)
                Next

            End If

            Return bultos
        End Function

        Public Shared Function RellenarBultosEnviarEfectivoPrecintado(dtBultos As DataTable, cuentas As ObservableCollection(Of Clases.Cuenta)) As ObservableCollection(Of Clases.Bulto)

            Dim bultos As New ObservableCollection(Of Clases.Bulto)

            If dtBultos IsNot Nothing AndAlso dtBultos.Rows.Count > 0 Then

                For Each rowBultos In dtBultos.Rows

                    If bultos.Find(Function(b) b.Identificador = rowBultos("OID_BULTO")) Is Nothing Then
                        Dim bulto As New Clases.Bulto

                        With bulto

                            .Cuenta = cuentas.FirstOrDefault(Function(c) c.Identificador = rowBultos("B_OID_CUENTA"))
                            .CuentaSaldo = cuentas.FirstOrDefault(Function(c) c.Identificador = If(rowBultos.Table.Columns.Contains("B_OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowBultos("B_OID_CUENTA_SALDO"), GetType(String))), rowBultos("B_OID_CUENTA_SALDO"), Nothing))
                            .Identificador = Util.AtribuirValorObj(rowBultos("OID_BULTO"), GetType(String))
                            .IdentificadorDocumento = Util.AtribuirValorObj(rowBultos("B_OID_DOCUMENTO"), GetType(String))
                            .FechaHoraCreacion = Util.AtribuirValorObj(rowBultos("B_GMT_CREACION"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(rowBultos("B_GMT_MODIFICACION"), GetType(DateTime))
                            .UsuarioCreacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(rowBultos("B_DES_USUARIO_MODIFICACION"), GetType(String))
                            .Cuadrado = Util.AtribuirValorObj(rowBultos("BOL_CUADRADO"), GetType(Boolean))
                            .Precintos = New ObservableCollection(Of String)
                            .Precintos.Add(Util.AtribuirValorObj(rowBultos("B_COD_PRECINTO"), GetType(String)))

                            If rowBultos("B_ROWVER") Is DBNull.Value Then
                                .Rowver = 0
                            Else
                                .Rowver = Util.AtribuirValorObj(rowBultos("B_ROWVER"), GetType(Int64))
                            End If

                            .Divisas = New ObservableCollection(Of Clases.Divisa)

                        End With
                        bultos.Add(bulto)
                    End If
                Next

            End If

            Return bultos
        End Function

























#Region "[CONSULTAS]"

        ''' <summary>
        ''' Verifica si hay Bulto cerrado en la Remesa
        ''' </summary>
        ''' <param name="identificadorRemesa">identificadorRemesa</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function HayBultoCerradoPorRemesa(identificadorRemesa As String) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoCerradoPorIdentificadorRemesa)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Conteo de bultos en un determiado estado
        ''' </summary>
        ''' <param name="estado">Estado del bulto</param>
        ''' <param name="identificadorRemesa"></param>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="contarDiferencia">Si queire saber cuantos bultos distindos del estado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function HayBultoEnEstado(estado As Enumeradores.EstadoBulto, _
                                       Optional identificadorRemesa As List(Of String) = Nothing, _
                                       Optional identificadorBulto As List(Of String) = Nothing, _
                                       Optional contarDiferencia As Boolean = False) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.BultoVerificarEstado
            cmd.CommandType = CommandType.Text

            ' Buscar de acuerdo con los identificadores de la remesa
            If identificadorRemesa IsNot Nothing AndAlso identificadorRemesa.Count > 0 Then
                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorRemesa, "OID_REMESA", cmd, "AND", "B")
            End If

            ' Buscar de acuerdo con los identificadores del bulto
            If identificadorBulto IsNot Nothing AndAlso identificadorBulto.Count > 0 Then
                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorBulto, "OID_BULTO", cmd, "AND", "B")
            End If

            ' Verifica el estado
            If Not contarDiferencia Then
                cmd.CommandText &= " AND B.COD_ESTADO = []COD_ESTADO "
            Else
                cmd.CommandText &= " AND B.COD_ESTADO <> []COD_ESTADO "
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.ToString()))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function HayBultoSinCuadrar(identificadorBulto As List(Of String)) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.BultoSinCuadrar
            cmd.CommandType = CommandType.Text

            ' Buscar de acuerdo con los identificadores del bulto
            If identificadorBulto IsNot Nothing AndAlso identificadorBulto.Count > 0 Then
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorBulto, "OID_BULTO", cmd, "AND", "B"))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim resp As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

            If resp > 0 Then
                Return False
            End If

            Return True
        End Function

        Public Shared Function ObtenerIdentificadorDocumento(identificadores As List(Of String), Optional obtenerOrigen As Boolean = True) As List(Of String)

            Dim _identificadoresDocumento As New List(Of String)

            If obtenerOrigen Then
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = My.Resources.RecuperarIdentificadoresDocumentosBultos
                cmd.CommandType = CommandType.Text

                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadores, "OID_EXTERNO", cmd, "AND", "B")
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        _identificadoresDocumento.Add(Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)))
                        identificadores.RemoveAll(Function(x) x = dr("OID_EXTERNO"))
                    Next
                End If

            End If

            If identificadores IsNot Nothing AndAlso identificadores.Count > 0 Then
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = My.Resources.RecuperarIdentificadoresDocumentosBultos
                cmd.CommandType = CommandType.Text

                cmd.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadores, "OID_BULTO", cmd, "AND", "B")
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        _identificadoresDocumento.Add(Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)))
                    Next
                End If
            End If

            Return _identificadoresDocumento

        End Function

        Shared Function ObtenerUltimoDocumento(identificadorBulto As String, identificadorDocumento As String, estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(identificadorBulto) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoObtenerUltimoDocumentoIdBulto)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Identificador_Alfanumerico, identificadorBulto))
            End If

            If Not String.IsNullOrEmpty(identificadorDocumento) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoObtenerUltimoDocumentoIdDocumento)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCXELEMENTO", ProsegurDbType.Identificador_Alfanumerico, estadoDocumentoElemento.RecuperarValor()))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function VerificarCantidadBultosMismoSector(identificadoresRemesas As List(Of String),
                                                                  identificadorSector As String) As List(Of String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text

            cmd.CommandText = My.Resources.BultoVerificarCantidadBultosMismoSector

            cmd.CommandText = String.Format(cmd.CommandText,
                                            Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "WHERE", "BULT"),
                                            Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "BULT"))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, identificadorSector))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim codigosExternos As New List(Of String)

                For Each row In dt.Rows
                    codigosExternos.Add(Util.AtribuirValorObj(Of String)(row("COD_EXTERNO")))
                Next

                Return codigosExternos

            End If

            Return Nothing

        End Function

        Public Shared Function RecuperarBultosRemesas(IdentificadoresRemesas As List(Of String), IdentificadoresBultos As List(Of String)) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            cmd.CommandType = CommandType.Text

            cmd.CommandText = My.Resources.BultoRecuperarBultosRemesas

            Dim clausulaInRemesas As String = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "BULT")
            Dim clausulaInBultos As String = Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresBultos, "OID_BULTO", cmd, "AND", "BULT", EsNotIn:=True)
            Dim clausulaIn As String = clausulaInRemesas & " " & clausulaInBultos

            cmd.CommandText = String.Format(cmd.CommandText, clausulaIn)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

        End Function

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Grabar un nuevo bulto
        ''' </summary>
        ''' <param name="objBulto"></param>
        ''' <param name="identificadorRemessa"></param>
        ''' <remarks></remarks>
        Public Shared Sub grabarBulto(ByRef objBulto As Clases.Bulto, identificadorRemessa As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, objBulto.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemessa))

            If objBulto.IdentificadorExterno IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, objBulto.IdentificadorExterno))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objBulto.GrupoTerminosIAC IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, objBulto.GrupoTerminosIAC.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objBulto.GrupoTerminosIACParciales IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_PARCIALES", ProsegurDbType.Objeto_Id, objBulto.GrupoTerminosIACParciales.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_PARCIALES", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objBulto.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, objBulto.Cuenta.Identificador))

            'Precinto foi preparado para uma lista de valores, mas no momento será gravado apenas um precinto.
            If objBulto.Precintos IsNot Nothing AndAlso objBulto.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objBulto.Precintos.FirstOrDefault) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objBulto.Precintos.FirstOrDefault))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, DBNull.Value))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_BOLSA", ProsegurDbType.Descricao_Longa, objBulto.CodigoBolsa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO_INGRESO", ProsegurDbType.Objeto_Id, If(objBulto.BancoIngreso Is Nothing, Nothing, objBulto.BancoIngreso.Identificador)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_BANCO_INGRESO_ES_MADRE", ProsegurDbType.Logico, objBulto.BancoIngresoEsBancoMadre))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objBulto.Estado.RecuperarValor))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objBulto.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objBulto.PuestoResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_PARCIALES", ProsegurDbType.Inteiro_Longo, objBulto.CantidadParciales))

            If objBulto.FechaHoraInicioConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, objBulto.FechaHoraInicioConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.FechaHoraFinConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, objBulto.FechaHoraFinConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.FechaProcessoLegado <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROCESO_LEGADO", ProsegurDbType.Data_Hora, objBulto.FechaProcessoLegado))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROCESO_LEGADO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_PADRE", ProsegurDbType.Objeto_Id, objBulto.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objBulto.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_SUSTITUTO", ProsegurDbType.Objeto_Id, objBulto.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_SUSTITUTO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objBulto.ConfiguracionNivelSaldos.RecuperarValor))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CUADRADO", ProsegurDbType.Logico, objBulto.Cuadrado))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objBulto.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objBulto.UsuarioModificacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_ABONO", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoAbonoElemento.NoAbonado.RecuperarValor()))

            If objBulto.CuentaSaldo IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, objBulto.CuentaSaldo.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If _transacion IsNot Nothing Then
                _transacion.AdicionarItemTransacao(cmd)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If

        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar Bulto.
        ''' </summary>
        ''' <param name="objBulto"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarBulto(objBulto As Clases.Bulto)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoAtualizar)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, objBulto.Identificador))

            If objBulto.GrupoTerminosIAC IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, objBulto.GrupoTerminosIAC.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objBulto.GrupoTerminosIACParciales IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_PARCIALES", ProsegurDbType.Objeto_Id, objBulto.GrupoTerminosIACParciales.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC_PARCIALES", ProsegurDbType.Objeto_Id, Nothing))
            End If

            'TODO : valor não defindio
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO_INGRESO", ProsegurDbType.Descricao_Curta, Nothing))

            'TODO : valor não defindio
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_BANCO_INGRESO_ES_MADRE", ProsegurDbType.Logico, 0))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objBulto.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objBulto.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objBulto.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objBulto.PuestoResponsable))

            'Precinto foi preparado para uma lista de valores, mas no momento será gravado apenas um precinto.
            If objBulto.Precintos IsNot Nothing AndAlso objBulto.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objBulto.Precintos.FirstOrDefault) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objBulto.Precintos.FirstOrDefault))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, DBNull.Value))
            End If

            'TODO : valor não defindio
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_BOLSA", ProsegurDbType.Descricao_Curta, objBulto.CodigoBolsa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD_PARCIALES", ProsegurDbType.Inteiro_Longo, objBulto.CantidadParciales))

            If objBulto.FechaHoraInicioConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, objBulto.FechaHoraInicioConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_INICIO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.FechaHoraFinConteo <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, objBulto.FechaHoraFinConteo))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_CONTEO_FIN", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.FechaProcessoLegado <> DateTime.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROCESO_LEGADO", ProsegurDbType.Data_Hora, objBulto.FechaProcessoLegado))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PROCESO_LEGADO", ProsegurDbType.Data_Hora, Nothing))
            End If

            If objBulto.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_PADRE", ProsegurDbType.Objeto_Id, objBulto.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objBulto.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_SUSTITUTO", ProsegurDbType.Objeto_Id, objBulto.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO_SUSTITUTO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, objBulto.ConfiguracionNivelSaldos.RecuperarValor))

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, objBulto.Cuenta.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objBulto.UsuarioModificacion))

            If objBulto.CuentaSaldo IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, objBulto.CuentaSaldo.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar el estado del bulto.
        ''' </summary>
        ''' <param name="objBulto">Objeto Bulto.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoBulto(objBulto As Clases.Bulto)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoAtualizarEstado)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, objBulto.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objBulto.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objBulto.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Sub ActualizarEstadoAbonoBulto(identificadorRemesa As String, codigoEstadoAbono As String, usuario As String,
                                                     soloCuandoNoEsNoAbonados As Boolean,
                                                     ByRef transaccion As DataBaseHelper.Transaccion)

            Dim nonQuery = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoAtualizarEstadoAbono)

            If soloCuandoNoEsNoAbonados Then
                nonQuery += " AND COD_ESTADO_ABONO <> 'NA' "
            End If

            Dim wrapper As New DataBaseHelper.SPWrapper(nonQuery, True, CommandType.Text)

            wrapper.AgregarParam("OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa)
            wrapper.AgregarParam("COD_ESTADO_ABONO", ProsegurDbType.Descricao_Curta, codigoEstadoAbono)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Sub ActualizarEstadoBulto(identificador As String,
                                                 estado As Enumeradores.EstadoBulto,
                                                 usuario As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificador))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoAtualizarEstado)
                    command.CommandType = CommandType.Text

                    If _transacion IsNot Nothing Then
                        _transacion.AdicionarItemTransacao(command)
                    Else
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)
                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

        ''' <summary>
        ''' Actualizar cuenta del bulto
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="identificadorCuenta"></param>
        ''' <param name="usuario"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCuenta(identificadorBulto As String, identificadorCuenta As String, usuario As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoActualizarCuenta)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Descricao_Curta, identificadorCuenta))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar el campo BOL_CUADRADO.
        ''' </summary>
        ''' <param name="precintosBultos">Precintos dos Bultos</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCuadrado(precintosBultos As List(Of String), usuarioModificacion As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandType = CommandType.Text
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoActualizarCuadrado)

            cmd.CommandText += Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, precintosBultos, "COD_PRECINTO", cmd, "WHERE", "B")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuarioModificacion))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualiza IdentificadorDocumento del Bulto.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarIdentificadorDocumentoDelBulto(objBulto As Clases.Bulto)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoAtualizarIdentificadorDocumento)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, objBulto.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Descricao_Curta, objBulto.IdentificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objBulto.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar Bulto.
        ''' </summary>
        ''' <param name="identificadorBulto">identificadorBulto.</param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarBulto(identificadorBulto As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.BultoExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region


        Public Shared Function ActualizarPrecintoSaldos(bulto As Clases.Bulto) As Integer

            If bulto IsNot Nothing AndAlso bulto.Precintos IsNot Nothing AndAlso bulto.Precintos.Count > 0 Then
                'Cria o comando SQL 
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.BultoActualizaPrecintoCodigoBolsa

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, bulto.Identificador))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "PRECINTO", ProsegurDbType.Identificador_Alfanumerico, bulto.Precintos.FirstOrDefault))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_BOLSA", ProsegurDbType.Identificador_Alfanumerico, bulto.CodigoBolsa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, bulto.UsuarioModificacion))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If

        End Function
        ''' <summary>
        ''' VALIDA SE O BULTO TEM ALGUM DOCUMENTO DE SALIDAS A RECORRIDO COM O ESTADO DIFERENTE DE EM CURSO RELACIONADO A ELE
        ''' </summary>
        ''' <param name="identificadoresBultos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function HayDocumentoSalidasRecorrigoRelacionado(identificadoresBultos As ObservableCollection(Of String)) As ObservableCollection(Of Clases.Bulto)

            Dim bultos As New ObservableCollection(Of Clases.Bulto)

            If identificadoresBultos IsNot Nothing AndAlso identificadoresBultos.Count > 0 Then
                'Cria o comando SQL 
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.BultoHayDocumentoSalidasRecorrigoRelacionado

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresBultos, "OID_EXTERNO", cmd, "AND", "B")))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows

                        Dim bulto As New Clases.Bulto With
                        {.Precintos = New ObservableCollection(Of String) From {Util.AtribuirValorObj(dr("COD_PRECINTO"), GetType(String))},
                         .Documento = New Clases.Documento With {.NumeroExterno = Util.AtribuirValorObj(dr("COD_EXTERNO"), GetType(String)),
                                                                 .CodigoComprobante = Util.AtribuirValorObj(dr("COD_COMPROBANTE"), GetType(String))}
                        }
                        bultos.Add(bulto)
                    Next
                End If

            End If
            Return bultos
        End Function

        ''' <summary>
        ''' Valida se o posto que está rompendo o lacre é do mesmo setor pai de onde está o remesa/bulto
        ''' </summary>
        ''' <param name="identificadorBulto"></param>
        ''' <param name="codigoPuesto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function VerificaMismoSectorPadreDelBultoYPuesto(identificadorBulto As String, codigoDelegacion As String, codigoPlanta As String, codigoPuesto As String) As Boolean

            Dim mismoSectorPadre As Boolean = False

            If Not String.IsNullOrEmpty(identificadorBulto) AndAlso Not String.IsNullOrEmpty(codigoPuesto) Then

                'Cria o comando SQL 
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text

                cmd.CommandText = My.Resources.SectorEsHijo
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                'Valida se o setor é filho
                If AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd) Then

                    cmd = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    cmd.CommandType = CommandType.Text

                    cmd.CommandText = My.Resources.BultoMismoSectorPadreDelBultoYPuesto

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, identificadorBulto))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, codigoPlanta))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                    mismoSectorPadre = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

                Else
                    'Se for setor pai (quando trabalha com saldo por setor, o setor do parâmetro "codigoPuesto" é pai)
                    'ou seja, quando trabalha com saldo por setor, não deve fazer a validação acima
                    mismoSectorPadre = True

                End If

            End If
            Return mismoSectorPadre
        End Function

        Public Property ReenvioCambioPrecintoLegado As Boolean
        Public Property ReenvioCambioPrecintoSol As Boolean
        Public Property CodigoPlanta As String
        Public Property CodigoDelegacion As String
        Public Property CodigoUsuario As String

        Public Shared Sub RomperPrecintosSaldosSalidas(bultos As List(Of Clases.Bulto), ReenvioCambioPrecintoLegado As Boolean, ReenvioCambioPrecintoSol As Boolean, trabajaPorBulto As Boolean, CodigoPlanta As String, CodigoDelegacion As String, CodigoUsuario As String)

            Try

                Dim spw As SPWrapper = ColectarRomperPrecintosSaldosSalidas(bultos, ReenvioCambioPrecintoLegado, ReenvioCambioPrecintoSol, trabajaPorBulto, CodigoPlanta, CodigoDelegacion, CodigoUsuario)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If

            End Try

        End Sub

        Private Shared Function ColectarRomperPrecintosSaldosSalidas(bultos As List(Of Clases.Bulto), ReenvioCambioPrecintoLegado As Boolean, ReenvioCambioPrecintoSol As Boolean, trabajaPorBulto As Boolean, CodigoPlanta As String, CodigoDelegacion As String, CodigoUsuario As String)

            Dim SP As String = String.Format("sapr_pbulto_{0}.sromper_precinto_saldossalidas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$abulrem_oid_bulto", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$abulrem_cod_precint_nuevo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abulrem_cod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)


            spw.AgregarParam("par$reenvio_precintos_legado", ProsegurDbType.Logico, ReenvioCambioPrecintoLegado, ParameterDirection.Input, False)
            spw.AgregarParam("par$reenvio_precintos_sol", ProsegurDbType.Logico, ReenvioCambioPrecintoSol, ParameterDirection.Input, False)
            spw.AgregarParam("par$trabaja_por_bulto", ProsegurDbType.Logico, trabajaPorBulto, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Descricao_Longa, CodigoDelegacion, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_planta", ProsegurDbType.Descricao_Longa, CodigoPlanta, ParameterDirection.Input, False)
            spw.AgregarParam("par$usuario", ProsegurDbType.Descricao_Longa, CodigoUsuario, ParameterDirection.Input, False)

            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$hacer_commit", ProsegurDbType.Logico, True, , False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                                        "par$cod_ejecucion",
                                                        "par$cod_ejecucion",
                                                        "par$num_duracion_trans_ext_seg",
                                                        "par$cod_transaccion",
                                                        "par$cod_resultado")

            If bultos IsNot Nothing AndAlso bultos.Count > 0 Then
                For Each bulto In bultos
                    spw.Param("par$abulrem_oid_bulto").AgregarValorArray(bulto.Identificador)
                    spw.Param("par$abulrem_cod_precint_nuevo").AgregarValorArray(bulto.Precintos.FirstOrDefault)
                    spw.Param("par$abulrem_cod_sector").AgregarValorArray(bulto.Cuenta.Sector.Codigo)
                Next
            Else
                spw.Param("par$abulrem_oid_bulto").AgregarValorArray(DBNull.Value)
                spw.Param("par$abulrem_cod_precint_nuevo").AgregarValorArray(DBNull.Value)
                spw.Param("par$abulrem_cod_sector").AgregarValorArray(DBNull.Value)
            End If

            Return spw

        End Function

    End Class
End Namespace