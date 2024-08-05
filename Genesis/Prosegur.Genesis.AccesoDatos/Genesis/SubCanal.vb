Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis
    ''' <summary>
    ''' Classe de Acesso a dados SubCanal.
    ''' </summary>
    ''' <history>
    ''' [Henrique Ribeiro] 11/11/2013 - Criado.
    '''</history>
    Public Class SubCanal

        Public Shared Function Validar(_identificadorCanal As String, codigoSubCanal As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(_identificadorCanal) AndAlso Not String.IsNullOrEmpty(codigoSubCanal) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL' AND CA.OID_TABLA_GENESIS = SC.OID_SUBCANAL "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoSubCanal))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND SC.COD_SUBCANAL = []COD_SUBCANAL "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Descricao_Curta, codigoSubCanal))
                    End If

                    filtro &= " AND SC.OID_CANAL = []OID_CANAL AND SC.BOL_VIGENTE = 1 "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Descricao_Curta, _identificadorCanal))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.SubCanal_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function


        Public Shared Function ObtenerIdentificadorSubCanal(CodigoSubCanal As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorSubCanalPorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Descricao_Longa, CodigoSubCanal))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_SUBCANAL"), GetType(String))
            End If
            Return Nothing
        End Function



        ''' <summary>
        ''' Obtem os subcanais pelo identificador do canal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorIdentificadorCanal(ParamArray identificador As String()) As ObservableCollection(Of Clases.SubCanal)
            Dim subCanales As New ObservableCollection(Of Clases.SubCanal)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerSubCanalPorIdentificadorCanal
                Dim sqlWhere As New StringBuilder()

                If (identificador.Length > 0) Then
                    sqlWhere.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificador.ToList(), "OID_CANAL", cmd, "AND", , "C"))
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each row In dtDados.Rows
                    subCanales.Add(Cargar(row))
                Next

            End Using

            Return subCanales

        End Function

        Public Shared Function ObtenerCodigosSubCanalPorCodigoCanal(codigosCanales As List(Of String),
                                                                    codigoDelegacion As String) As List(Of String)

            Dim subCanales As New List(Of String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerCodigosSubCanalPorCodigoCanal
                Dim sqlWhere As New StringBuilder()

                If (codigosCanales.Count > 0) Then
                    sqlWhere.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosCanales.ToList(), "COD_CANAL", cmd, "AND", , "C"))
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each row In dtDados.Rows
                    subCanales.Add(Util.AtribuirValorObj(Of String)(row("COD_SUBCANAL")))
                Next

            End Using

            Return subCanales

        End Function

        Public Shared Function ObtenerSubCanalPorIDPS(IDPS As String) As Clases.SubCanal
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSubCanalPorIDPS
                comando.CommandType = CommandType.Text

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Longa, IDPS))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Dim subCanal As Clases.SubCanal = Cargar(dt.Rows(0))
                    Return subCanal
                End If

            End Using

            Return Nothing

        End Function

        Public Shared Function ObtenerSubCanalPorCodigo(CodigoSubCanal As String) As Clases.SubCanal
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSubCanalPorCodigo
                comando.CommandType = CommandType.Text

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Descricao_Curta, CodigoSubCanal))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Dim subCanal As Clases.SubCanal = Cargar(dt.Rows(0))
                    Return subCanal
                End If

            End Using

            Return Nothing

        End Function

        Public Shared Function ObtenerSubCanalYCanalPorCodigo(CodigoSubCanal As String) As DataTable
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSubCanalyCanalPorCodigo
                comando.CommandType = CommandType.Text

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Descricao_Curta, CodigoSubCanal))

                Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

            End Using

        End Function

        Private Shared Function Cargar(datos As DataRow) As Prosegur.Genesis.Comon.Clases.SubCanal
            Dim subCanal As New Prosegur.Genesis.Comon.Clases.SubCanal() With
                                 {
                                     .Identificador = Util.AtribuirValorObj(Of String)(datos("OID_SUBCANAL")),
                                     .Codigo = Util.AtribuirValorObj(Of String)(datos("COD_SUBCANAL")),
                                     .Descripcion = Util.AtribuirValorObj(Of String)(datos("DES_SUBCANAL")),
                                     .Observacion = Util.AtribuirValorObj(Of String)(datos("OBS_SUBCANAL")),
                                     .EstaActivo = Util.AtribuirValorObj(Of Boolean)(datos("BOL_VIGENTE")),
                                     .CodigoUsuario = Util.AtribuirValorObj(Of String)(datos("COD_USUARIO")),
                                     .FechaHoraActualizacion = Util.AtribuirValorObj(Of DateTime)(datos("FYH_ACTUALIZACION")),
                                     .EsPorDefecto = Util.AtribuirValorObj(Of Boolean)(datos("BOL_POR_DEFECTO")),
                                     .CodigoMigracion = Util.AtribuirValorObj(Of String)(datos("COD_MIGRACION"))
                                 }
            Return subCanal
        End Function

        Public Shared Function ObtenerSubCanalPorIdentificador(Identificador As String) As Clases.SubCanal
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerSubCanalPorIdentificador
                comando.CommandType = CommandType.Text

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, Identificador))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Dim subCanal As Clases.SubCanal = Cargar(dt.Rows(0))
                    Return subCanal
                End If

            End Using

            Return Nothing

        End Function


        Public Shared Function ObtenerSubCanales() As List(Of Clases.SubCanal)
            Dim subCanales As New List(Of Clases.SubCanal)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.SubCanalObtenerSubCanales)

                Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                For Each row In dt.Rows
                    subCanales.Add(Cargar(row))
                Next

            End Using

            Return subCanales

        End Function

        Shared Function ObtenerSubCanalJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_SUBCANAL) like '%' || []COD_SUBCANAL || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))


                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_SUBCANAL) like '%' || []DES_SUBCANAL || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_SUBCANAL", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(identificadorPadre) Then
                        filtro &= " AND OID_CANAL like '%' || []OID_CANAL || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorPadre))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerSubCanalPorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_SUBCANAL"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_SUBCANAL"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_SUBCANAL"), GetType(String))
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