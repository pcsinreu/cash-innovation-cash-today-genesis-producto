Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports System.Threading.Tasks
Imports DataBaseHelper = Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis

    ''' <summary>
    ''' Clase AccionSector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 16/09/2013 - Criado
    ''' </history>
    Public Class Sector

        Public Shared Function Validar(identificadorDelegacion As String, identificadorPlanta As String, codigoSector As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(codigoSector) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' AND CA.OID_TABLA_GENESIS = S.OID_SECTOR "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND S.BOL_ACTIVO = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoSector))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND S.COD_SECTOR = []COD_SECTOR AND S.BOL_ACTIVO = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, codigoSector))

                        If Not String.IsNullOrEmpty(identificadorPlanta) Then
                            filtro += " AND S.OID_PLANTA = []OID_PLANTA "
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Descricao_Curta, identificadorPlanta))

                        ElseIf Not String.IsNullOrEmpty(identificadorDelegacion) Then
                            inner = " INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = S.OID_PLANTA "
                            filtro += " AND P.OID_DELEGACION = []OID_DELEGACION "
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, identificadorDelegacion))
                        End If
                    End If

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Sector_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")
                End If

            End If

            Return identificador
        End Function

        Public Shared Function ObtenerIdentificadorSector(CodigoSector As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorSectorPorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Longa, CodigoSector))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_SECTOR"), GetType(String))
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerIdentificadorSector(CodigoSector As String, CodigoDelegacion As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorSectorPorCodigoyDelegacion)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Longa, CodigoSector))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, CodigoDelegacion))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_SECTOR"), GetType(String))
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerIdentificadorSector(CodigoSector As String, CodigoDelegacion As String, transaccion As DataBaseHelper.Transaccion) As String

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorSectorPorCodigoyDelegacion), False, CommandType.Text)

            wrapper.AgregarParam("COD_SECTOR", ProsegurDbType.Descricao_Longa, CodigoSector)
            wrapper.AgregarParam("COD_DELEGACION", ProsegurDbType.Descricao_Longa, CodigoDelegacion)

            Dim ds As DataSet = DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Dim dt As DataTable = IIf(ds IsNot Nothing AndAlso ds.Tables.Count > 0, ds.Tables(0), Nothing)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_SECTOR"), GetType(String))
            End If

            Return Nothing
        End Function

        ''' <summary>
        ''' Método que recupera las plantas y delegaciones asociadas a sector
        ''' </summary>
        ''' <param name="pSectores">Sectores</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPlantasDelegacionesPorSector(pSectores As List(Of String), pIdentificadorAjeno As String) As DataTable
            Dim retorno As DataTable
            Dim ds As DataSet = Nothing
            Dim spw As SPWrapper = Nothing
            Try
                spw = ArmarWrapperPlantasDelegacionesPorSector(pSectores, pIdentificadorAjeno)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                retorno = ds.Tables(0)
            Catch ex As Exception
                retorno = Nothing
            End Try
            Return retorno
        End Function

        Private Shared Function ArmarWrapperPlantasDelegacionesPorSector(pSectores As List(Of String), pIdentificadorAjeno As String) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSECTOR_{0}.srecuperar_delegplantBySector", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$acod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, pIdentificadorAjeno, , False)
            spw.AgregarParam("par$rc_reporte", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "reporte")

            spw.Param("par$acod_sector").AgregarValorArray("")
            For Each unSector As String In pSectores
                spw.Param("par$acod_sector").AgregarValorArray(unSector)
            Next

            Return spw
        End Function

        ''' <summary>
        ''' Método que recupera os tipos de setores pelas suas respectivas características.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da delegação</param>
        ''' <param name="codigoPlanta">Código da planta</param>
        ''' <param name="caracteristicasTipoSector">Características a serem pesquisadas</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCaracteristicas(codigoDelegacion As String,
                                                         codigoPlanta As String,
                                                         caracteristicasTipoSector As List(Of Enumeradores.CaracteristicaTipoSector),
                                                Optional CargarCodigosAjenos As Boolean = False) As ObservableCollection(Of Clases.Sector)

            Dim sectores As ObservableCollection(Of Clases.Sector) = Nothing

            If Not String.IsNullOrEmpty(codigoDelegacion) AndAlso Not String.IsNullOrEmpty(codigoPlanta) Then

                Try

                    Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                        Dim dtSectores As DataTable = Nothing
                        Dim filtro As String = ""

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, codigoPlanta))

                        If (caracteristicasTipoSector.Count > 0) Then
                            filtro = Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, caracteristicasTipoSector.Select(Function(s) s.RecuperarValor).ToList(), "COD_CARACT_TIPOSECTOR", cmd, "AND", "CTS")
                        End If

                        cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerSectoresPorCaracteristicas & filtro)
                        cmd.CommandType = CommandType.Text

                        dtSectores = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                        sectores = Cargar_v2(dtSectores, CargarCodigosAjenos, False)

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return sectores

        End Function

        Private Shared Function Cargar_v2(dtSectores As DataTable,
                                 Optional CargarCodigosAjenos As Boolean = False,
                                 Optional CargarSectorPadre As Boolean = True) As ObservableCollection(Of Clases.Sector)

            Dim sectores As ObservableCollection(Of Clases.Sector) = Nothing

            If dtSectores IsNot Nothing AndAlso dtSectores.Rows IsNot Nothing AndAlso dtSectores.Rows.Count > 0 Then

                Dim identificadoresCaracteristicasTipoSector As List(Of String) = dtSectores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_TIPO_SECTOR") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_TIPO_SECTOR"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_TIPO_SECTOR")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim dtCaracteristicasTipoSector As DataTable = Nothing
                Dim codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing
                dtCaracteristicasTipoSector = Genesis.CaracteristicaTipoSector.obtenerCaracteristicasPorTipoSector(identificadoresCaracteristicasTipoSector)
                If CargarCodigosAjenos Then
                    codigosAjenos = New ObservableCollection(Of Clases.CodigoAjeno)
                    For Each _sector In dtSectores.Rows
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(_sector("OID_SECTOR"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSECTOR", .IdentificadorTablaGenesis = Util.AtribuirValorObj(_sector("OID_SECTOR"), GetType(String))})
                        End If
                    Next
                    AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos("", codigosAjenos)
                End If

                For Each rowSector As DataRow In dtSectores.Rows

                    Dim caracteristicas As New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector)
                    If rowSector.Table.Columns.Contains("OID_TIPO_SECTOR") Then
                        Dim _CaracteristicasTipoSector = dtCaracteristicasTipoSector.Select(" OID_TIPO_SECTOR = '" & Util.AtribuirValorObj(rowSector("OID_TIPO_SECTOR"), GetType(String)) & "'")

                        If _CaracteristicasTipoSector IsNot Nothing Then
                            For Each _valor In _CaracteristicasTipoSector
                                If ExisteEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR")) Then
                                    caracteristicas.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(_valor("COD_CARACT_TIPOSECTOR").ToString))
                                End If
                            Next
                        End If
                    End If

                    Dim sectorPadre As Clases.Sector = Nothing
                    If rowSector.Table.Columns.Contains("OID_SECTOR_PADRE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowSector("OID_SECTOR_PADRE"), GetType(String))) Then
                        sectorPadre = New Clases.Sector
                        If CargarSectorPadre Then
                            sectorPadre = ObtenerPorOid(Util.AtribuirValorObj(Of String)(rowSector("OID_SECTOR_PADRE")))
                        Else
                            sectorPadre.Identificador = Util.AtribuirValorObj(rowSector("OID_SECTOR_PADRE"), GetType(String))
                        End If
                    End If

                    Dim sector As New Clases.Sector With {.Codigo = If(rowSector.Table.Columns.Contains("COD_SECTOR"), Util.AtribuirValorObj(rowSector("COD_SECTOR"), GetType(String)), Nothing), _
                                                          .Identificador = If(rowSector.Table.Columns.Contains("OID_SECTOR"), Util.AtribuirValorObj(rowSector("OID_SECTOR"), GetType(String)), Nothing), _
                                                          .Descripcion = If(rowSector.Table.Columns.Contains("DES_SECTOR"), Util.AtribuirValorObj(rowSector("DES_SECTOR"), GetType(String)), Nothing), _
                                                          .EsActivo = If(rowSector.Table.Columns.Contains("SE_BOL_ACTIVO"), Util.AtribuirValorObj(rowSector("SE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                          .EsCentroProceso = If(rowSector.Table.Columns.Contains("BOL_CENTRO_PROCESO"), Util.AtribuirValorObj(rowSector("BOL_CENTRO_PROCESO"), GetType(Boolean)), Nothing), _
                                                          .EsConteo = If(rowSector.Table.Columns.Contains("BOL_CONTEO"), Util.AtribuirValorObj(rowSector("BOL_CONTEO"), GetType(Boolean)), Nothing), _
                                                          .EsTesoro = If(rowSector.Table.Columns.Contains("BOL_TESORO"), Util.AtribuirValorObj(rowSector("BOL_TESORO"), GetType(Boolean)), Nothing), _
                                                          .PemitirDisponerValor = If(rowSector.Table.Columns.Contains("BOL_PERMITE_DISPONER_VALOR"), Util.AtribuirValorObj(rowSector("BOL_PERMITE_DISPONER_VALOR"), GetType(Boolean)), Nothing), _
                                                          .SectorPadre = sectorPadre, _
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = If(rowSector.Table.Columns.Contains("COD_DELEGACION"), Util.AtribuirValorObj(rowSector("COD_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowSector.Table.Columns.Contains("DES_DELEGACION"), Util.AtribuirValorObj(rowSector("DES_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowSector.Table.Columns.Contains("OID_DELEGACION"), Util.AtribuirValorObj(rowSector("OID_DELEGACION"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowSector.Table.Columns.Contains("D_BOL_VIGENTE"), Util.AtribuirValorObj(rowSector("D_BOL_VIGENTE"), GetType(Boolean)), Nothing), _
                                                                                            .HusoHorarioEnMinutos = If(rowSector.Table.Columns.Contains("NEC_GMT_MINUTOS"), Util.AtribuirValorObj(rowSector("NEC_GMT_MINUTOS"), GetType(Integer)), Nothing), _
                                                                                            .FechaHoraVeranoInicio = If(rowSector.Table.Columns.Contains("FYH_VERANO_INICIO"), Util.AtribuirValorObj(rowSector("FYH_VERANO_INICIO"), GetType(Date)), Nothing), _
                                                                                            .FechaHoraVeranoFin = If(rowSector.Table.Columns.Contains("FYH_VERANO_FIN"), Util.AtribuirValorObj(rowSector("FYH_VERANO_FIN"), GetType(Date)), Nothing), _
                                                                                            .AjusteHorarioVerano = If(rowSector.Table.Columns.Contains("NEC_VERANO_AJUSTE"), Util.AtribuirValorObj(rowSector("NEC_VERANO_AJUSTE"), GetType(Integer)), Nothing), _
                                                                                            .Zona = If(rowSector.Table.Columns.Contains("DES_ZONA"), Util.AtribuirValorObj(rowSector("DES_ZONA"), GetType(String)), Nothing), _
                                                                                            .CodigoPais = If(rowSector.Table.Columns.Contains("COD_PAIS"), Util.AtribuirValorObj(rowSector("COD_PAIS"), GetType(String)), Nothing)}, _
                                                          .TipoSector = New Clases.TipoSector With {.Codigo = If(rowSector.Table.Columns.Contains("COD_TIPO_SECTOR"), Util.AtribuirValorObj(rowSector("COD_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowSector.Table.Columns.Contains("DES_TIPO_SECTOR"), Util.AtribuirValorObj(rowSector("DES_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowSector.Table.Columns.Contains("OID_TIPO_SECTOR"), Util.AtribuirValorObj(rowSector("OID_TIPO_SECTOR"), GetType(String)), Nothing), _
                                                                                            .EstaActivo = If(rowSector.Table.Columns.Contains("TSE_BOL_ACTIVO"), Util.AtribuirValorObj(rowSector("TSE_BOL_ACTIVO"), GetType(Boolean)), Nothing), _
                                                                                            .CaracteristicasTipoSector = caracteristicas}, _
                                                          .Planta = New Clases.Planta With {.Codigo = If(rowSector.Table.Columns.Contains("COD_PLANTA"), Util.AtribuirValorObj(rowSector("COD_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Descripcion = If(rowSector.Table.Columns.Contains("DES_PLANTA"), Util.AtribuirValorObj(rowSector("DES_PLANTA"), GetType(String)), Nothing), _
                                                                                            .Identificador = If(rowSector.Table.Columns.Contains("OID_PLANTA"), Util.AtribuirValorObj(rowSector("OID_PLANTA"), GetType(String)), Nothing), _
                                                                                            .EsActivo = If(rowSector.Table.Columns.Contains("P_BOL_ACTIVO"), Util.AtribuirValorObj(rowSector("P_BOL_ACTIVO"), GetType(Boolean)), Nothing)}}


                    If codigosAjenos IsNot Nothing AndAlso codigosAjenos.Count > 0 Then
                        sector.CodigosAjenos = codigosAjenos.FindAll(Function(x) x.IdentificadorTablaGenesis = sector.Identificador)
                    End If

                    If sectores Is Nothing Then sectores = New ObservableCollection(Of Clases.Sector)
                    sectores.Add(sector)

                Next

            End If

            Return sectores
        End Function

        Public Shared Sub ActualizarDelegacionPlanta(pOidSector As String, pOidPlanta As String, pObjTransacao As Transacao)
            Try
                Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                    cmd.CommandText = My.Resources.ActualizarPlantaEnSector
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Objeto_Id, pOidSector))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Objeto_Id, pOidPlanta))
                    cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)
                    cmd.CommandType = CommandType.Text

                    If pObjTransacao IsNot Nothing Then
                        pObjTransacao.AdicionarItemTransacao(cmd)
                    Else
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
                    End If

                End Using
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Shared Function VerificarPuestoPorSectorPadre(CodigoDelegacion As String, CodigoPlanta As String, CodigoSectorPadre As String, CodigoPuesto As String) As Boolean

            Dim esPuestoValido As Boolean = False

            If Not String.IsNullOrEmpty(CodigoDelegacion) AndAlso Not String.IsNullOrEmpty(CodigoPlanta) AndAlso Not String.IsNullOrEmpty(CodigoSectorPadre) AndAlso Not String.IsNullOrEmpty(CodigoPuesto) Then

                Try

                    Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                        Dim dtSectores As DataTable = Nothing

                        cmd.CommandText = String.Format(My.Resources.ObtenerSectoresPorSectorPadre, " AND P.COD_PLANTA = []COD_PLANTA AND D.COD_DELEGACION = []COD_DELEGACION AND S.COD_SECTOR = []COD_PUESTO ")

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, CodigoDelegacion))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, CodigoPlanta))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_SECTOR_PADRE", ProsegurDbType.Descricao_Curta, CodigoSectorPadre))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PUESTO", ProsegurDbType.Descricao_Curta, CodigoPuesto))

                        cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)
                        cmd.CommandType = CommandType.Text

                        dtSectores = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                        If dtSectores IsNot Nothing AndAlso dtSectores.Rows.Count > 0 Then
                            esPuestoValido = True
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            Else
                Return Nothing
            End If

            Return esPuestoValido
        End Function


















        ''' <summary>
        ''' Método que recupera los sectores de acuerdo con el codigo de la delegación, de la planta y el tipo de sector
        ''' </summary>
        ''' <param name="codigoDelegacion"></param>
        ''' <param name="codigoPlanta"></param>
        ''' <param name="TipoSectores"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectores(CodigoDelegacion As String, CodigoPlanta As String, Optional TipoSectores As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.TipoSector) = Nothing, Optional CargarCodigosAjenos As Boolean = False) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Dim sectores As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtSecores As DataTable

                If (Not String.IsNullOrEmpty(CodigoDelegacion) AndAlso Not String.IsNullOrEmpty(CodigoPlanta)) Then

                    Dim sqlResource As String = My.Resources.ObtenerSectores

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, CodigoDelegacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, CodigoPlanta))
                    Dim sqlWhere As New StringBuilder

                    If (TipoSectores IsNot Nothing AndAlso TipoSectores.Count > 0) Then
                        sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, (From item In TipoSectores Select item.Codigo).ToList(), "COD_TIPO_SECTOR", cmd, "AND", , "D"))
                    End If

                    cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                    dtSecores = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                    For Each row As DataRow In dtSecores.Rows
                        Dim sector = Cargar(row, CargarCodigosAjenos)
                        sectores.Add(sector)
                    Next
                End If
            End Using

            Return sectores
        End Function
        Private Shared Function CargarSectoresTesoto(dt As DataTable) As List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector)

            Dim Sectores As New List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim codigoTipoSetor As String = String.Empty

                For Each dr As DataRow In dt.Rows

                    Dim objSector As New ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector

                    Util.AtribuirValorObjeto(objSector.Identificador, dr("OID_SECTOR"), GetType(String))
                    Util.AtribuirValorObjeto(objSector.Codigo, dr("COD_SECTOR"), GetType(String))
                    Util.AtribuirValorObjeto(objSector.Descripcion, dr("DES_SECTOR"), GetType(String))

                    Sectores.Add(objSector)
                Next

            End If

            Return Sectores
        End Function

        Private Shared Function Cargar(datos As DataRow, Optional CargarCodigosAjenos As Boolean = False, Optional CargarSectorPadre As Boolean = True) As Prosegur.Genesis.Comon.Clases.Sector
            Dim sector As New Prosegur.Genesis.Comon.Clases.Sector() With
                                 {
                                     .Identificador = Util.AtribuirValorObj(Of String)(datos("OID_SECTOR")),
                                     .Descripcion = Util.AtribuirValorObj(Of String)(datos("DES_SECTOR")),
                                     .Codigo = Util.AtribuirValorObj(Of String)(datos("COD_SECTOR")),
                                     .CodigoMigracion = Util.AtribuirValorObj(Of String)(datos("COD_MIGRACION")),
                                     .EsActivo = Util.AtribuirValorObj(Of Boolean)(datos("BOL_ACTIVO")),
                                     .EsCentroProceso = Util.AtribuirValorObj(Of Boolean)(datos("BOL_CENTRO_PROCESO")),
                                     .EsConteo = Util.AtribuirValorObj(Of Boolean)(datos("BOL_CONTEO")),
                                     .EsTesoro = Util.AtribuirValorObj(Of Boolean)(datos("BOL_TESORO")),
                                     .FechaHoraCreacion = Util.AtribuirValorObj(Of DateTime)(datos("GMT_CREACION")),
                                     .PemitirDisponerValor = Util.AtribuirValorObj(Of Boolean)(datos("BOL_PERMITE_DISPONER_VALOR")),
                                     .FechaHoraModificacion = Util.AtribuirValorObj(Of DateTime)(datos("GMT_MODIFICACION")),
                                     .UsuarioCreacion = Util.AtribuirValorObj(Of String)(datos("DES_USUARIO_CREACION")),
                                     .UsuarioModificacion = Util.AtribuirValorObj(Of String)(datos("DES_USUARIO_MODIFICACION")),
                                     .Planta = Planta.ObtenerPorOid(Util.AtribuirValorObj(Of String)(datos("OID_PLANTA"))),
                                     .Delegacion = Delegacion.ObtenerPorIdentificadorPlanta(.Planta.Identificador)
                                 }

            If CargarCodigosAjenos Then

                sector.CodigosAjenos = CodigoAjeno.ObtenerCodigosAjenos(sector.Identificador, String.Empty, String.Empty, Enumeradores.Tabela.Sector.RecuperarValor())

            End If

            If CargarSectorPadre AndAlso (datos("OID_SECTOR_PADRE") IsNot DBNull.Value) Then
                sector.SectorPadre = ObtenerPorOid(Util.AtribuirValorObj(Of String)(datos("OID_SECTOR_PADRE")))
            End If

            If (datos("OID_TIPO_SECTOR") IsNot DBNull.Value) Then
                sector.TipoSector = AccesoDatos.Genesis.TipoSector.RecuperarTipoSectorPorIdentificador(Util.AtribuirValorObj(Of String)(datos("OID_TIPO_SECTOR")))
            End If

            Return sector
        End Function

        ''' <summary>
        ''' Método que recupera os tipos de setores pelas suas respectivas características.
        ''' </summary>
        ''' <param name="codigoDelegacion">Código da delegação</param>
        ''' <param name="codigoPlanta">Código da planta</param>
        ''' <param name="caracteristicasTipoSector">Características a serem pesquisadas</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorCaracteristicasSimultaneas(codigoDelegacion As String, codigoPlanta As String, caracteristicasTipoSector As List(Of Enumeradores.CaracteristicaTipoSector)) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Dim sectores As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerSectoresPorCaracteristicasSimultaneas
                Dim sqlWhere As New StringBuilder()

                If (caracteristicasTipoSector.Count > 0) Then
                    sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, caracteristicasTipoSector.Select(Function(s) s.RecuperarValor).ToList(), "COD_CARACT_TIPOSECTOR", cmd, "WHERE", "CTS"))
                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pCOD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pCOD_PLANTA", ProsegurDbType.Descricao_Curta, codigoPlanta))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, String.Format(sqlResource, sqlWhere.ToString(), caracteristicasTipoSector.Count))

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                For Each row As DataRow In dtDados.Rows
                    sectores.Add(Cargar(row, False))
                Next

            End Using

            Return sectores

        End Function

        Public Shared Function ObtenerSectoresTesoro(codigoDelegacion As String, codigoPlanta As String, deslogin As String) As List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector)

            Dim sectores As New List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim dtDados As DataTable
                cmd.CommandText = My.Resources.ObtenerSectoresTesoro

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, codigoPlanta))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "DES_LOGIN", ProsegurDbType.Identificador_Alfanumerico, deslogin.ToUpper()))

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
                If dtDados IsNot Nothing AndAlso dtDados.Rows.Count > 0 Then
                    sectores = CargarSectoresTesoto(dtDados)
                End If

            End Using

            Return sectores

        End Function

        ''' <summary>
        ''' Obtener SubSectores de acordo com o setor pai
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSectorPadre">Codigo Sector Pai</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorSectorPadre(CodigoDelegacion As String, _
                                             CodigoPlanta As String, _
                                             CodigoSectorPadre As String) As List(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim sectores As New List(Of Prosegur.Genesis.Comon.Clases.Sector)

                comando.CommandText = My.Resources.ObtenerSectoresPorSectorPadre

                If Not String.IsNullOrEmpty(CodigoDelegacion) AndAlso Not String.IsNullOrEmpty(CodigoPlanta) Then

                    comando.CommandText = String.Format(comando.CommandText, " AND P.COD_PLANTA = []COD_PLANTA AND D.COD_DELEGACION = []COD_DELEGACION ")

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, CodigoDelegacion))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, CodigoPlanta))
                Else
                    comando.CommandText = String.Format(comando.CommandText, String.Empty)
                End If

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR_PADRE", ProsegurDbType.Descricao_Curta, CodigoSectorPadre))

                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count > 0 Then

                    For Each row As DataRow In dt.Rows
                        sectores.Add(Cargar(row))
                    Next

                    Return sectores
                Else
                    Return Nothing
                End If

            End Using

        End Function

        ''' <summary>
        ''' Obtener identificadores de los sectores hijos
        ''' </summary>
        ''' <param name="identificadorSectorPadre">Identificador Sector Pai</param>
        ''' <returns>Objeto List(of identificadoresSectores)</returns>
        ''' <remarks></remarks>
        Public Shared Function ObteneridentificadoresSectoresHijos(identificadorSectorPadre As List(Of String)) As List(Of String)

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim query As String = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorSectorPadre, "OID_SECTOR_PADRE", comando, "", "SECT")
                comando.CommandText = String.Format(My.Resources.ObteneridentificadoresSectoresHijos, query)
                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count > 0 Then
                    Dim listIdentificador As New List(Of String)
                    For Each i As DataRow In dt.Rows
                        If Not listIdentificador.Contains(i.Item(0)) Then
                            listIdentificador.Add(i.Item(0))
                        End If
                    Next
                    Return listIdentificador
                Else
                    Return Nothing
                End If

            End Using

        End Function

        ''' <summary>
        ''' Obtener Codigos SubSectores de acordo com o setor pai
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSectorPadre">Codigo Sector Pai</param>
        ''' <returns>List String</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCodigosSectoresPorSectorPadre(CodigoDelegacion As String, _
                                             CodigoPlanta As String, _
                                             CodigoSectorPadre As String) As List(Of String)

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim codigosSectores As New List(Of String)

                comando.CommandText = My.Resources.ObtenerCodigosSectoresPorSectorPadre

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, CodigoPlanta))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR_PADRE", ProsegurDbType.Descricao_Curta, CodigoSectorPadre))

                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count > 0 Then

                    For Each row As DataRow In dt.Rows
                        codigosSectores.Add(Util.AtribuirValorObj(Of String)(row("COD_SECTOR")))
                    Next

                    Return codigosSectores
                Else
                    Return Nothing
                End If

            End Using

        End Function

        ''' <summary>
        ''' Obtener Sector especifico
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSector">Codigo Sector</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSector(CodigoDelegacion As String, _
                                             CodigoPlanta As String, _
                                             CodigoSector As String) As Prosegur.Genesis.Comon.Clases.Sector

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)


                comando.CommandText = My.Resources.ObtenerSectoresConCodSector
                If Not String.IsNullOrEmpty(CodigoPlanta) Then
                    comando.CommandText += " AND P.COD_PLANTA = :COD_PLANTA "
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, CodigoPlanta))
                End If

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, CodigoDelegacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Descricao_Curta, CodigoSector))
                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count > 0 Then
                    Dim Sector As Prosegur.Genesis.Comon.Clases.Sector = Cargar(dt.Rows(0))
                    Return Sector
                Else
                    Return Nothing
                End If
            End Using

        End Function

        ''' <summary>
        ''' Método que recupera o setor pelo seu OID.
        ''' </summary>
        ''' <param name="oid">OID a ser pesquisado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String, Optional cargarCodigoAjenos As Boolean = False, Optional CargarSectorPadre As Boolean = True) As Prosegur.Genesis.Comon.Clases.Sector
            Dim sector As New Prosegur.Genesis.Comon.Clases.Sector

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerSectorPorOid

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pOID_SECTOR", ProsegurDbType.Descricao_Curta, oid))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If (dtDados.Rows.Count > 0) Then
                    sector = Cargar(dtDados.Rows(0), cargarCodigoAjenos, CargarSectorPadre)
                End If
            End Using

            Return sector
        End Function

        ''' <summary>
        ''' Método que recupera los sectores de acuerdo con el identificador de la delegación. recupera somente codigo e descrição
        ''' </summary>
        ''' <param name="identificadorDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorDelegacion(identificadorDelegacion As String) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Dim sectores As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Dim delegaciones As New List(Of String)
            delegaciones.AddRange(identificadorDelegacion.Split(";"))
            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                cmd.CommandText = My.Resources.ObtenerSectorPorDelegacion

                If delegaciones.Count = 1 Then
                    cmd.CommandText = String.Format(cmd.CommandText, " AND P.OID_DELEGACION IN ([]OID_DELEGACION) ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, identificadorDelegacion))
                Else
                    Dim delegacionesIn = Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, delegaciones, "OID_DELEGACION", cmd, "AND", "P")
                    cmd.CommandText = String.Format(cmd.CommandText, delegacionesIn)
                End If

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Dim dtSecores = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                For Each row As DataRow In dtSecores.Rows
                    Dim sector As New Clases.Sector() With {.Identificador = row("OID_SECTOR"), .Codigo = row("COD_SECTOR"), .Descripcion = row("DES_SECTOR"),
                                                            .Delegacion = New Clases.Delegacion With {.Identificador = row("OID_DELEGACION"), .Codigo = row("COD_DELEGACION")}}
                    If Util.AtribuirValorObj(Of String)(row("OID_SECTOR_PADRE")) IsNot Nothing Then
                        sector.SectorPadre = New Clases.Sector() With {.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_PADRE"))}
                    End If
                    sectores.Add(sector)
                Next

            End Using

            Return sectores
        End Function

        Public Shared Function ObtenerSectoresPorCodigoDelegacion(codigoDelegaciones As List(Of String), tiposSectores As List(Of String), solamentePadres As Boolean) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Dim sectores As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                Dim query As String = ""

                cmd.CommandText = My.Resources.ObtenerSectoresPorCodigoDelegacion

                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegaciones, "COD_DELEGACION", cmd, "AND", "DELE")

                If (tiposSectores IsNot Nothing AndAlso tiposSectores.Count > 0) Then
                    query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, tiposSectores, "COD_TIPO_SECTOR", cmd, "AND", "TSECT")
                End If

                If solamentePadres Then
                    query &= " AND SECT.OID_SECTOR_PADRE IS NULL"
                End If

                cmd.CommandText = String.Format(cmd.CommandText, query)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Dim dtSecores = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                For Each row As DataRow In dtSecores.Rows
                    Dim sector As New Clases.Sector()
                    sector.Codigo = row("COD_SECTOR")
                    sector.Descripcion = row("DES_SECTOR")
                    sector.Delegacion = New Clases.Delegacion()
                    sector.Delegacion.Codigo = row("COD_DELEGACION")
                    sector.Delegacion.Descripcion = row("DES_DELEGACION")
                    sectores.Add(sector)
                Next

            End Using

            Return sectores
        End Function

        ''' <summary>
        ''' Método que recupera los sectores de acuerdo con el identificador de la delegación. recupera somente codigo e descrição
        ''' </summary>
        ''' <param name="listaCodigoTiposSectores"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCodigosSectoresPorCodigoTiposSectores(listaCodigoTiposSectores As List(Of String)) As List(Of String)
            Dim listaCodigosSectores As New List(Of String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                cmd.CommandText = My.Resources.ObtenerCodigoSectoresPorCodTipoSector.Replace("[]COD_TIPO_SECTOR", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, listaCodigoTiposSectores, "COD_TIPO_SECTOR", cmd))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        listaCodigosSectores.Add(dr("COD_SECTOR"))
                    Next
                End If
            End Using

            Return listaCodigosSectores
        End Function

        Public Shared Function ObtenerSectoresPorCodigo(listaCodigoSectores As List(Of String)) As ObservableCollection(Of Clases.Sector)
            Dim listaCodigosSectores As New ObservableCollection(Of Clases.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                cmd.CommandText = My.Resources.ObtenerSectoresPorCodigos.Replace("[]CODIGOS_SECTORES", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, listaCodigoSectores, "CODIGOS_SECTORES", cmd))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        listaCodigosSectores.Add(New Clases.Sector With {.Identificador = dr("OID_SECTOR"), .Codigo = dr("COD_SECTOR"), .Descripcion = dr("DES_SECTOR")})
                    Next
                End If
            End Using

            Return listaCodigosSectores
        End Function

        Public Shared Function ObtenerSectorPorCodigo(CodigoDelegacion As String, CodigoSector As String) As Clases.Sector
            Dim objSector As Clases.Sector = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                cmd.CommandText = My.Resources.ObtenerSectoresPorDelegacion
                cmd.CommandText += " AND SECT.COD_SECTOR = []COD_SECTOR"
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd.CommandText)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Objeto_Id, CodigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Objeto_Id, CodigoSector))

                Dim dt = If(AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd), New DataTable)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        objSector = New Clases.Sector With {.Descripcion = dr("DES_SECTOR")}
                    Next
                End If
            End Using

            Return objSector
        End Function

        Public Shared Function ObtenerDatosResumidos(codDelegacion As String, codPlanta As String) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Dim sectores As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                If (Not String.IsNullOrEmpty(codDelegacion) AndAlso Not String.IsNullOrEmpty(codPlanta)) Then

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerDatosResumidosSectores)

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codDelegacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, codPlanta))

                    Dim rdSecores = AcessoDados.ExecutarDataReader(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
                    While rdSecores.Read()
                        Dim sector = New Clases.Sector
                        sector.Codigo = rdSecores("COD_SECTOR")
                        sector.Descripcion = rdSecores("DES_SECTOR")
                        sector.Planta = New Clases.Planta()
                        sector.Planta.Codigo = rdSecores("COD_PLANTA")
                        sector.Delegacion = New Clases.Delegacion()
                        sector.Delegacion.Codigo = rdSecores("COD_DELEGACION")
                        sectores.Add(sector)
                    End While

                    rdSecores.Close()
                    rdSecores.Dispose()

                End If

                If cmd IsNot Nothing AndAlso cmd.Connection IsNot Nothing Then
                    cmd.Connection.Close()
                End If

            End Using

            Return sectores
        End Function

        Shared Function ObtenerSectores(paises As List(Of Clases.Pais), Optional sinPuesto As Boolean = False) As List(Of DataTable)

            Dim dt As New List(Of DataTable)

            ' Paises 
            If paises IsNot Nothing AndAlso paises.Count > 0 Then
                For Each _pais In paises

                    ' Delegaciones
                    If _pais.Delegaciones IsNot Nothing AndAlso _pais.Delegaciones.Count > 0 Then
                        For Each _delegacion In _pais.Delegaciones

                            ' Plantas
                            If _delegacion.Plantas IsNot Nothing AndAlso _delegacion.Plantas.Count > 0 Then
                                For Each _planta In _delegacion.Plantas

                                    ' TiposSectores
                                    If _planta.TiposSector IsNot Nothing AndAlso _planta.TiposSector.Count > 0 Then

                                        Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                                            Dim sqlResource As String = Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, (From item In _planta.TiposSector Select item.Codigo).ToList(), "COD_TIPO_SECTOR", cmd, "AND", , "D") & vbNewLine

                                            If sinPuesto Then
                                                sqlResource &= " AND CTS.COD_CARACT_TIPOSECTOR <> '" & Prosegur.Genesis.Comon.Enumeradores.CaracteristicaTipoSector.Puesto.RecuperarValor & "' " & vbNewLine & sqlResource
                                            End If

                                            sqlResource = String.Format(My.Resources.ObtenerSectores_V2, sqlResource)

                                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, _delegacion.Codigo))
                                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, _planta.Codigo))

                                            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sqlResource)
                                            cmd.CommandType = CommandType.Text

                                            Dim _dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                                            If _dt IsNot Nothing AndAlso _dt.Rows IsNot Nothing AndAlso _dt.Rows.Count > 0 Then
                                                dt.Add(_dt)
                                            End If

                                        End Using

                                    End If

                                Next
                            End If

                        Next
                    End If

                Next
            End If

            Return dt

        End Function

        Public Shared Function ObtenerSectorPadrePrimerNivel(identificadorSector As String) As Prosegur.Genesis.Comon.Clases.Sector
            Dim sector As New Clases.Sector

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                comando.CommandText = My.Resources.ObtenerSectorPadrePrimerNivel

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Curta, identificadorSector))
                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count > 0 Then
                    sector.Identificador = dt.Rows(0)("OID_SECTOR")
                    sector.Codigo = dt.Rows(0)("COD_SECTOR")
                    sector.Descripcion = dt.Rows(0)("DES_SECTOR")
                End If
            End Using

            Return sector
        End Function

        Public Shared Function RecuperarPorDeviceID(deviceID As String) As DataTable
            Dim sector As New Clases.Sector

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                comando.CommandText = My.Resources.SectorRecuperarPorDeviceID

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICACION", ProsegurDbType.Descricao_Curta, deviceID))
                comando.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            End Using
        End Function

#Region "Integracion Salidas -> NuevoSaldos"

        Public Shared Function ObtenerSector(Idps As String) As Prosegur.Genesis.Comon.Clases.Sector

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSectorPorIDPS
                comando.CommandType = CommandType.Text
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Objeto_Id, Idps))
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
                If dt.Rows.Count = 1 Then
                    Dim Sector As Prosegur.Genesis.Comon.Clases.Sector = Cargar(dt.Rows(0))
                    Return Sector
                End If

            End Using
            Return Nothing
        End Function

        Public Shared Function RecuperarSectoresSalidas(CodigoDelegacion As String, CodigoPlanta As String) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.RecuperarSectoresSalidas
                comando.CommandType = CommandType.Text
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Objeto_Id, CodigoPlanta))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Objeto_Id, CodigoDelegacion))
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                Return CargarSectores(dt)

            End Using

            Return Nothing

        End Function

        Private Shared Function CargarSectores(dt As DataTable) As ObservableCollection(Of Clases.Sector)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim sectores As New ObservableCollection(Of Clases.Sector)

                For Each row In dt.Rows

                    Dim sector As Clases.Sector = Nothing

                    ' verifica se já existe o setor
                    If sectores.Exists(Function(s) s.Identificador.Equals(row("OID_SECTOR"))) Then

                        sector = sectores.Find(Function(s) s.Identificador.Equals(row("OID_SECTOR")))

                    Else

                        sector = New Clases.Sector
                        sector.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR"))
                        sector.Descripcion = Util.AtribuirValorObj(Of String)(row("DES_SECTOR"))
                        sector.Codigo = Util.AtribuirValorObj(Of String)(row("COD_SECTOR"))
                        sector.EsActivo = Util.AtribuirValorObj(Of Int16)(row("BOL_ACTIVO"))
                        sector.Delegacion = New Clases.Delegacion() With
                                    {
                                        .Identificador = Util.AtribuirValorObj(Of String)(row("OID_DELEGACION")),
                                        .Codigo = Util.AtribuirValorObj(Of String)(row("COD_DELEGACION")),
                                        .Descripcion = Util.AtribuirValorObj(Of String)(row("DES_DELEGACION"))
                                    }
                        sector.Planta = New Clases.Planta() With
                                    {
                                        .Identificador = Util.AtribuirValorObj(Of String)(row("OID_PLANTA")),
                                        .Codigo = Util.AtribuirValorObj(Of String)(row("COD_PLANTA")),
                                        .Descripcion = Util.AtribuirValorObj(Of String)(row("DES_PLANTA"))
                                    }

                        sectores.Add(sector)

                    End If

                    If Not IsDBNull(Util.AtribuirValorObj(Of String)(row("OID_SECTOR_PADRE"))) Then

                        If sector.SectorPadre Is Nothing AndAlso sectores.Exists(Function(s) s.Identificador.Equals(row("OID_SECTOR_PADRE"))) Then
                            sector.SectorPadre = sectores.Find(Function(s) s.Identificador.Equals(row("OID_SECTOR_PADRE")))
                        ElseIf sector.SectorPadre Is Nothing AndAlso Not sectores.Exists(Function(s) s.Identificador.Equals(row("OID_SECTOR_PADRE"))) Then
                            sector.SectorPadre = New Clases.Sector
                            sector.SectorPadre.Identificador = Util.AtribuirValorObj(Of String)(row("OID_SECTOR_PADRE"))
                        End If

                    End If

                    If sector.TipoSector Is Nothing Then
                        sector.TipoSector = New Clases.TipoSector() With
                                    {
                                        .Identificador = Util.AtribuirValorObj(Of String)(row("OID_TIPO_SECTOR")),
                                        .Codigo = Util.AtribuirValorObj(Of String)(row("COD_TIPO_SECTOR")),
                                        .Descripcion = Util.AtribuirValorObj(Of String)(row("DES_TIPO_SECTOR")),
                                        .CaracteristicasTipoSector = New ObservableCollection(Of Enumeradores.CaracteristicaTipoSector) From {RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR"))}
                                    }
                    Else
                        sector.TipoSector.CaracteristicasTipoSector.Add(RecuperarEnum(Of Enumeradores.CaracteristicaTipoSector)(row("COD_CARACT_TIPOSECTOR")))
                    End If

                Next row

                For Each sector As Clases.Sector In sectores
                    If sector.SectorPadre IsNot Nothing AndAlso Not String.IsNullOrEmpty(sector.SectorPadre.Identificador) Then
                        RellenarSectorPadre(sectores, sector.SectorPadre)
                    End If
                Next

                Return sectores

            End If

            Return Nothing

        End Function

        Public Shared Sub RellenarSectorPadre(ByRef sectores As ObservableCollection(Of Clases.Sector), ByRef sector As Clases.Sector)

            If sectores IsNot Nothing AndAlso sectores.Count > 0 Then

                If sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(sector.Identificador) AndAlso String.IsNullOrEmpty(sector.Descripcion) Then

                    Dim identificador As String = sector.Identificador

                    Dim sectorTemp As Clases.Sector = sectores.Find(Function(s) s.Identificador.Equals(identificador))

                    If sectorTemp IsNot Nothing AndAlso sectorTemp.SectorPadre IsNot Nothing AndAlso Not String.IsNullOrEmpty(sectorTemp.SectorPadre.Identificador) AndAlso String.IsNullOrEmpty(sectorTemp.SectorPadre.Descripcion) Then

                        RellenarSectorPadre(sectores, sectorTemp.SectorPadre)

                    End If

                    sector = sectorTemp

                End If

            End If

        End Sub

        Public Shared Function ObtenerPermisionSectorPorIDPS(CodigoAjeno As String, _
                                                             IdentificadorFormulario As String, _
                                                             OrigenDestino As Char) As List(Of Prosegur.Genesis.Comon.Clases.Sector)

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerPermisoSectorOrigenPorIDPS
                comando.CommandType = CommandType.Text
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Longa, CodigoAjeno))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_RELACION_CON_FORMULARIO", ProsegurDbType.Descricao_Curta, OrigenDestino))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_FORMULARIO", ProsegurDbType.Objeto_Id, IdentificadorFormulario))
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Dim Sectores As New List(Of Prosegur.Genesis.Comon.Clases.Sector)
                    For Each row In dt.Rows
                        Dim Sector As New Prosegur.Genesis.Comon.Clases.Sector With {.Identificador = row("OID_SECTOR"), _
                                                                                     .Planta = Planta.ObtenerPorOid(Util.AtribuirValorObj(Of String)(row("OID_PLANTA"))),
                                                                                     .Delegacion = Delegacion.ObtenerPorIdentificadorPlanta(.Planta.Identificador)
                                                                                    }


                        Sectores.Add(Sector)
                    Next row

                    Return Sectores
                End If

            End Using
            Return Nothing
        End Function

        Public Shared Function ObtenerSectorPorIDPS(CodigoAjeno As String) As Prosegur.Genesis.Comon.Clases.Sector

            Using comando As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSectorPorIDPS
                comando.CommandType = CommandType.Text
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Longa, CodigoAjeno))
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Dim Sectores As New List(Of Prosegur.Genesis.Comon.Clases.Sector)
                    Return Cargar(dt.Rows(0))

                End If
                Return Nothing
            End Using
        End Function

#End Region

        Shared Function ObtenerSectorJSON(codigo As String, descripcion As String, identificadorPadre As String, considerarTodosNiveis As Boolean) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""



                    If Not String.IsNullOrEmpty(codigo) Then
                        filtro &= " AND UPPER(COD_SECTOR) like '%' || []COD_SECTOR || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", _
                                                                                    ProsegurDbType.Objeto_Id, codigo.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_SECTOR) like '%' || []DES_SECTOR || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_SECTOR", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(identificadorPadre) Then
                        filtro &= " AND OID_PLANTA = []OID_PLANTA "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorPadre))
                    End If

                    If Not considerarTodosNiveis Then
                        filtro &= " AND OID_SECTOR_PADRE IS NULL "
                        'My.Resources.ObtenerSectorPorCodigoConsiderandoTodosNiveis
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerSectorPorCodigo_v2, filtro))

                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_SECTOR"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_SECTOR"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_SECTOR"), GetType(String))
                                        })

                        Next

                    End If


                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return lista
        End Function



    End Class

End Namespace