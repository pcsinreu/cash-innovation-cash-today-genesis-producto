Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis

    Public Class Canal

        Shared Function Validar(codigoCanal As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(codigoCanal) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL' AND CA.OID_TABLA_GENESIS = C.OID_CANAL "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND C.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoCanal))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND C.COD_CANAL = []COD_CANAL AND C.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", ProsegurDbType.Descricao_Curta, codigoCanal))
                    End If

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Canal_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function

        Shared Function ObtenerCanalPatronJSON(codigo As String) As ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_CANAL) = []COD_CANAL"
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerCanalPorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim rowCliente As DataRow = dt.Rows(0)
                        Return New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_CANAL"), GetType(String)), _
                                    .Descripcion = Util.AtribuirValorObj(rowCliente("DES_CANAL"), GetType(String)), _
                                    .Codigo = Util.AtribuirValorObj(rowCliente("COD_CANAL"), GetType(String))}

                    End If


                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return Nothing
        End Function

        Shared Function ObtenerCanalJSON(codigo As String, descripcion As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_CANAL) like '%' || []COD_CANAL || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CANAL", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))


                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_CANAL) like '%' || []DES_CANAL || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CANAL", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerCanalPorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_CANAL"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_CANAL"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_CANAL"), GetType(String))
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

        Public Shared Function ObtenerCanalPorIdentificador(Identificador As String) As String
            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                comando.CommandText = My.Resources.ObtenerCanalPorIdentificador
                comando.CommandType = CommandType.Text

                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Identificador_Alfanumerico, Identificador))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)

                If dt.Rows.Count > 0 Then
                    Return Util.AtribuirValorObj(dt.Rows(0)("COD_CANAL"), GetType(String))
                End If

            End Using

            Return Nothing

        End Function

    End Class

End Namespace

