Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario

Namespace Genesis
    ''' <summary>
    ''' Classe Codigo Ajeno
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CodigoAjeno

        Public Shared Function ObtenerCodigosAjenos(IdentificadorTablaGenesis As String, _
                                                    CodigoAjeno As String, _
                                                    CodigoIdentificador As String, _
                                                    CodigoTipoTablaGenesis As String) As ObservableCollection(Of Clases.CodigoAjeno)


            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            comando.CommandText = My.Resources.RecuperarCodigosAjenos
            comando.CommandType = CommandType.Text

            If Not String.IsNullOrEmpty(IdentificadorTablaGenesis) Then
                comando.CommandText &= " AND C.OID_TABLA_GENESIS = []OID_TABLA_GENESIS"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TABLA_GENESIS", ProsegurDbType.Objeto_Id, IdentificadorTablaGenesis))
            End If

            If Not String.IsNullOrEmpty(CodigoAjeno) Then
                comando.CommandText &= " AND C.COD_AJENO = []COD_AJENO"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Objeto_Id, CodigoAjeno))
            End If

            If Not String.IsNullOrEmpty(CodigoIdentificador) Then
                comando.CommandText &= " AND C.COD_IDENTIFICADOR = []COD_IDENTIFICADOR"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Objeto_Id, CodigoIdentificador))
            End If

            If Not String.IsNullOrEmpty(CodigoTipoTablaGenesis) Then
                comando.CommandText &= " AND C.COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS"
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Objeto_Id, CodigoTipoTablaGenesis))
            End If

            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Dim CodigosAjenos As New ObservableCollection(Of Clases.CodigoAjeno)

            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows

                    CodigosAjenos.Add(CargarCodigoAjeno(row))

                Next row

            End If

            Return CodigosAjenos

        End Function

        Private Shared Function CargarCodigoAjeno(row As DataRow) As Clases.CodigoAjeno

            Return New Clases.CodigoAjeno With {.Identificador = Util.AtribuirValorObj(row("OID_CODIGO_AJENO"), GetType(String)), _
                                                .IdentificadorTablaGenesis = Util.AtribuirValorObj(row("OID_TABLA_GENESIS"), GetType(String)), _
                                                .CodigoTipoTablaGenesis = Util.AtribuirValorObj(row("COD_TIPO_TABLA_GENESIS"), GetType(String)), _
                                                .CodigoIdentificador = Util.AtribuirValorObj(row("COD_IDENTIFICADOR"), GetType(String)), _
                                                .Codigo = Util.AtribuirValorObj(row("COD_AJENO"), GetType(String)), _
                                                .Descripcion = Util.AtribuirValorObj(row("DES_AJENO"), GetType(String)), _
                                                .EsDefecto = Util.AtribuirValorObj(row("BOL_DEFECTO"), GetType(Boolean)), _
                                                .EsActivo = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean)), _
                                                .EsMigrado = Util.AtribuirValorObj(row("BOL_MIGRADO"), GetType(Boolean)), _
                                                .FechaHoraCreacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(DateTime)), _
                                                .UsuarioCreacion = Util.AtribuirValorObj(row("DES_USUARIO_CREACION"), GetType(String)), _
                                                .FechaHoraModificacion = Util.AtribuirValorObj(row("GMT_MODIFICACION"), GetType(DateTime)), _
                                                .UsuarioModificacion = Util.AtribuirValorObj(row("DES_USUARIO_MODIFICACION"), GetType(String))
                                               }

        End Function

        Shared Sub ObtenerCodigosAjenos(identificadorAjeno As String,
                                        ByRef codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno))

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim _query As String = My.Resources.RecuperarCodigosAjenos

                    If Not String.IsNullOrEmpty(identificadorAjeno) Then
                        _query &= " AND C.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Objeto_Id, identificadorAjeno))
                    End If

                    If codigosAjenos IsNot Nothing AndAlso codigosAjenos.Count > 0 Then

                        Dim c As Integer = 0
                        For Each _codigo In codigosAjenos

                            If Not String.IsNullOrEmpty(command.CommandText) Then
                                command.CommandText &= vbNewLine & " UNION ALL " & vbNewLine
                            End If

                            If Not String.IsNullOrEmpty(_codigo.Codigo) Then
                                command.CommandText &= _query & " AND C.COD_AJENO = []COD_AJENO_" & c.ToString & " AND C.COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS_" & c.ToString & " "
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO_" & c.ToString, ProsegurDbType.Objeto_Id, _codigo.Codigo))

                            ElseIf Not String.IsNullOrEmpty(_codigo.IdentificadorTablaGenesis) Then
                                command.CommandText &= _query & " AND C.OID_TABLA_GENESIS = []OID_TABLA_GENESIS_" & c.ToString & " AND C.COD_TIPO_TABLA_GENESIS = []COD_TIPO_TABLA_GENESIS_" & c.ToString & " "
                                command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TABLA_GENESIS_" & c.ToString, ProsegurDbType.Objeto_Id, _codigo.IdentificadorTablaGenesis))

                            End If

                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS_" & c.ToString, ProsegurDbType.Objeto_Id, _codigo.CodigoTipoTablaGenesis))

                            c = c + 1
                        Next

                        codigosAjenos.Clear()

                    Else

                        command.CommandText = _query

                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, command.CommandText)
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        For Each _row In dt.Rows
                            codigosAjenos.Add(CargarCodigoAjeno(_row))
                        Next

                    End If
                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

        Public Shared Sub recuperarCodigoAjenoMae(ByRef codigoDelegacion As String,
                                                  ByRef codigoPlanta As String,
                                                  ByRef codigoSector As String,
                                                  identificadorAjeno As String,
                                                  usuario As String,
                                                  ByRef validaciones As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarCodigoAjenoMae(codigoDelegacion, codigoPlanta, codigoSector, identificadorAjeno, usuario)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("validaciones") Then
                    Dim rValidaciones() As DataRow = ds.Tables("validaciones").Select()

                    If rValidaciones IsNot Nothing AndAlso rValidaciones.Count > 0 Then

                        If validaciones Is Nothing Then validaciones = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                        For Each row As DataRow In rValidaciones
                            validaciones.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = Util.AtribuirValorObj(row(0), GetType(String)), .descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                        Next
                    End If

                End If

                If validaciones Is Nothing OrElse validaciones.Count = 0 Then
                    codigoDelegacion = If(spw.Param("par$cod_delegacion") Is DBNull.Value OrElse spw.Param("par$cod_delegacion").Valor Is Nothing OrElse spw.Param("par$cod_delegacion").Valor.ToString = "null", String.Empty, spw.Param("par$cod_delegacion").Valor.ToString)
                    codigoPlanta = If(spw.Param("par$cod_planta") Is DBNull.Value OrElse spw.Param("par$cod_planta").Valor Is Nothing OrElse spw.Param("par$cod_planta").Valor.ToString = "null", String.Empty, spw.Param("par$cod_planta").Valor.ToString)
                    codigoSector = If(spw.Param("par$cod_sector") Is DBNull.Value OrElse spw.Param("par$cod_sector").Valor Is Nothing OrElse spw.Param("par$cod_sector").Valor.ToString = "null", String.Empty, spw.Param("par$cod_sector").Valor.ToString)
                Else
                    codigoDelegacion = String.Empty
                    codigoPlanta = String.Empty
                    codigoSector = String.Empty
                End If
                

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

        Private Shared Function ColectarCodigoAjenoMae(codigoDelegacion As String,
                                                        codigoPlanta As String,
                                                        codigoSector As String,
                                                        identificadorAjeno As String,
                                                        usuario As String) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("gepr_pcodigo_ajeno_{0}.srecuperar_codigo_ajeno_mae", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, codigoPlanta, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, codigoSector, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_identificador", ProsegurDbType.Identificador_Alfanumerico, identificadorAjeno, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParamInfo("par$info_ejecucion")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

    End Class

End Namespace
