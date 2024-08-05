Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Classe Planta
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Planta

        Shared Function Validar(_identificadorDelegacion As String, codigoPlanta As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(_identificadorDelegacion) AndAlso Not String.IsNullOrEmpty(codigoPlanta) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' AND CA.OID_TABLA_GENESIS = P.OID_PLANTA "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoPlanta))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND P.COD_PLANTA = []COD_PLANTA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Curta, codigoPlanta))
                    End If

                    filtro &= " AND P.OID_DELEGACION = []OID_DELEGACION AND P.BOL_ACTIVO = 1 "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, _identificadorDelegacion))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Planta_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function

#Region "Consultas"

        Public Shared Function ObtenerPorOid(oid As String) As Clases.Planta
            Dim planta As New Clases.Planta

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerPlantaPorOid

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pOID_PLANTA", ProsegurDbType.Descricao_Curta, oid))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If (dtDados.Rows.Count > 0) Then
                    planta = Cargar(dtDados.Rows(0))
                End If
            End Using

            Return planta
        End Function

        Public Shared Function ObtenerIdentificadorPorCodigo(CodigoPlanta As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PlantaObtenerIdentificadorPorCodigo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodigoPlanta))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Método que recupera as plantas pelos seus respectivos codigos.
        ''' </summary>
        ''' <param name="codigosPlantas">Codigos a serem pesquisados</param>
        ''' <param name="codigoDelegacion">Codigo da delegação da planta</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCodigos(codigoDelegacion As String, ParamArray codigosPlantas As String()) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Planta)
            Dim plantas As New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Planta)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerPlantasPorCodigos
                Dim sqlWhere As New StringBuilder()

                If (codigosPlantas.Length > 0) Then
                    sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigosPlantas.ToList(), "COD_PLANTA", cmd, "AND", , "D"))
                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pCOD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                For Each row As DataRow In dtDados.Rows
                    plantas.Add(Cargar(row))
                Next
            End Using

            Return plantas
        End Function

        ''' <summary>
        ''' Recupera as plantas
        ''' </summary>
        ''' <param name="IdDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarPlantas(IdDelegacion As String) As ObservableCollection(Of Clases.Planta)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PlantaRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, IdDelegacion))

            Dim Plantas As ObservableCollection(Of Clases.Planta) = Nothing
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Plantas = New ObservableCollection(Of Clases.Planta)

                For Each dr In dt.Rows

                    Plantas.Add(Cargar(dr))

                Next

            End If

            Return Plantas
        End Function

        ''' <summary>
        ''' Obtener los codigos de migracion de las plantas por delegaciones
        ''' </summary>
        ''' <param name="CodigosDelegaciones">Lista de codigos de delegaciones</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [marcel.espiritosanto] 18/12/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerCodigosMigracionPlantas(CodigosDelegaciones As List(Of String)) As List(Of String)
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCodigosMigracionPlantas)
            comando.CommandType = CommandType.Text
            Dim strParametros As New StringBuilder(If(CodigosDelegaciones IsNot Nothing AndAlso CodigosDelegaciones.Count > 0, Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosDelegaciones, "COD_DELEGACION", comando, "AND", "DEL"), String.Empty))
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(comando.CommandText, If(strParametros.Length > 0, strParametros.ToString, String.Empty)))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            If dt.Rows.Count > 0 Then
                Dim Plantas As New List(Of String)
                For Each row In dt.Rows
                    Plantas.Add(row(0))
                Next
                Return Plantas
            Else
                Return Nothing
            End If
        End Function

#End Region

        Private Shared Function Cargar(dr As DataRow) As Clases.Planta
            Return New Clases.Planta With
            {
                .Codigo = Util.AtribuirValorObj(dr("COD_PLANTA"), GetType(String)), _
                .CodigoMigracion = Util.AtribuirValorObj(dr("COD_MIGRACION"), GetType(String)), _
                .Descripcion = Util.AtribuirValorObj(dr("DES_PLANTA"), GetType(String)), _
                .EsActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean)), _
                .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime)), _
                .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime)), _
                .Identificador = Util.AtribuirValorObj(dr("OID_PLANTA"), GetType(String)), _
                .TiposSector = TipoSector.RecuperarTiposSectores(.Identificador), _
                .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String)), _
                .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
            }
        End Function

        Shared Function ObtenerPlantaJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_PLANTA) like '%' || []COD_PLANTA || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))


                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_PLANTA) like '%' || []DES_PLANTA || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_PLANTA", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(identificadorPadre) Then
                        filtro &= " AND OID_DELEGACION like '%' || []OID_DELEGACION || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorPadre))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerPlantaPorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_PLANTA"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_PLANTA"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_PLANTA"), GetType(String))
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

        Shared Function ObtenerCodigoPlanta(codigoDelegacion As String, codigoSector As String) As String
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = "    SELECT P.COD_PLANTA " & _
                              "      FROM GEPR_TPLANTA P " & _
                              "INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION " & _
                              "INNER JOIN GEPR_TSECTOR S ON S.OID_PLANTA = P.OID_PLANTA " & _
                              "     WHERE S.COD_SECTOR = []COD_SECTOR AND D.COD_DELEGACION = []COD_DELEGACION "

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SECTOR", ProsegurDbType.Identificador_Alfanumerico, codigoSector))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        

    End Class
End Namespace