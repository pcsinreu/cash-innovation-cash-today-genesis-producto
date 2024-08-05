Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class Cliente

        Public Shared Function Validar(codigoCliente As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(codigoCliente) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND CA.OID_TABLA_GENESIS = C.OID_CLIENTE "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND C.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoCliente))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND C.COD_CLIENTE = []COD_CLIENTE AND C.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Curta, codigoCliente))
                    End If

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Cliente_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")
                End If

            End If

            Return identificador
        End Function

        Public Shared Function ObtenerIdentificadorCliente(CodigoCliente As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorClientePorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, CodigoCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_CLIENTE"), GetType(String))
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerCodigoCliente(IdentificadorCliente As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCodigoClientePorIdentificador)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Descricao_Longa, IdentificadorCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("COD_CLIENTE"), GetType(String))
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerClientesJSON(codigo As String, descripcion As String, esbanco As Boolean) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim clientes As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_CLIENTE) like '%' || []COD_CLIENTE || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))

                    If esbanco Then
                        inner = " inner join GEPR_TTIPO_CLIENTE TC on TC.OID_TIPO_CLIENTE = C.OID_TIPO_CLIENTE AND TC.COD_TIPO_CLIENTE = '1' "
                    End If

                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_CLIENTE) like '%' || []DES_CLIENTE || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_CLIENTE", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerClientePorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        clientes = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            clientes.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_CLIENTE"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_CLIENTE"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_CLIENTE"), GetType(String))
                                        })

                        Next

                    End If


                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return clientes
        End Function

        ''' <summary>
        ''' Obtener el Cliente por el Codigo
        ''' </summary>
        ''' <param name="CodigoCliente">Codigo del cliente</param>
        ''' <returns>Objeto Cliente</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCliente(CodigoCliente As String) As Clases.Cliente

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerClientePorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, CodigoCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Cliente As Clases.Cliente = CargarCliente(dt.Rows(0))
                Return Cliente

            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerClientePorSubCliente(IdentificadorSubCliente As String) As Clases.Cliente

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerClientePorSubCliente)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Descricao_Longa, IdentificadorSubCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim Cliente As Clases.Cliente = CargarCliente(dt.Rows(0))
                Return Cliente

            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerCliente(codigosCliente As List(Of String)) As ObservableCollection(Of Clases.Cliente)

            Dim clientes As ObservableCollection(Of Clases.Cliente) = Nothing

            If codigosCliente IsNot Nothing AndAlso codigosCliente.Count > 0 Then

                Try

                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        Dim filtro As String = ""
                        Dim inner As String = ""

                        If codigosCliente.Count = 1 Then

                            filtro &= " AND COD_CLIENTE = []COD_CLIENTE "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", _
                                                                                        ProsegurDbType.Objeto_Id, codigosCliente(0)))

                        Else

                            filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosCliente, "COD_CLIENTE", _
                                                                                   command, "AND")

                        End If

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerClientePorCodigo_v2, inner, filtro))
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            clientes = New ObservableCollection(Of Clases.Cliente)

                            For Each rowCliente In dt.Rows

                                clientes.Add(New Clases.Cliente With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_CLIENTE"), GetType(String)), _
                                            .Descripcion = Util.AtribuirValorObj(rowCliente("DES_CLIENTE"), GetType(String)), _
                                            .Codigo = Util.AtribuirValorObj(rowCliente("COD_CLIENTE"), GetType(String)), _
                                            .EstaActivo = Util.AtribuirValorObj(rowCliente("BOL_VIGENTE"), GetType(Int16)), _
                                            .CodigoUsuario = Util.AtribuirValorObj(rowCliente("COD_USUARIO"), GetType(String)), _
                                            .FechaHoraActualizacion = Util.AtribuirValorObj(rowCliente("FYH_ACTUALIZACION"), GetType(DateTime)), _
                                            .EstaEnviadoSaldos = Util.AtribuirValorObj(rowCliente("BOL_ENVIADO_SALDOS"), GetType(Int16)), _
                                            .TipoCliente = Nothing, _
                                            .CodigoMigracion = Util.AtribuirValorObj(rowCliente("COD_MIGRACION"), GetType(String)), _
                                            .EsTotalizadorSaldo = Util.AtribuirValorObj(rowCliente("BOL_TOTALIZADOR_SALDO"), GetType(String)), _
                                            .SubClientes = Nothing
                                             })

                            Next
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

            Return clientes

        End Function

        Private Shared Function CargarCliente(dr As DataRow) As Clases.Cliente
            Return New Clases.Cliente With {.Identificador = Util.AtribuirValorObj(dr("OID_CLIENTE"), GetType(String)), 
                                            .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String)),
                                            .Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String)), 
                                            .EstaActivo = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Int16)), 
                                            .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)),
                                            .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)), 
                                            .EstaEnviadoSaldos = Util.AtribuirValorObj(dr("BOL_ENVIADO_SALDOS"), GetType(Int16)), 
                                            .TipoCliente = Nothing, 
                                            .CodigoMigracion = Util.AtribuirValorObj(dr("COD_MIGRACION"), GetType(String)), 
                                            .EsTotalizadorSaldo = Util.AtribuirValorObj(dr("BOL_TOTALIZADOR_SALDO"), GetType(String)), 
                                            .BolGrabaSaldoHistorico = Util.AtribuirValorObj(dr("BOL_SALDO_HISTORICO"), GetType(Boolean)),
                                            .CodFechaSaldoHistorico = Util.AtribuirValorObj(dr("COD_FECHA_SALDO_HISTORICO"), GetType(String)),
                                            .SubClientes = Nothing
                                           }
        End Function

        ''' <summary>
        ''' Recupera uma lista de identificadores de clientes que são totalizadores de saldos
        ''' </summary>
        ''' <param name="identificadoresCliente">Lista de identificadores a ser verificado se são totalizadores</param>
        ''' <returns>Retorna os identificadores de clientes que são totalizadores de saldos</returns>
        ''' <remarks></remarks>
        Public Shared Function IdentificadoresClienteTotalizadorSaldo(identificadoresCliente As List(Of String)) As List(Of String)
            Dim listaRetorno As List(Of String) = Nothing
            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandType = CommandType.Text
            Comando.CommandText = My.Resources.ClienteIdentificadoresTotalizadorSaldo.Replace("[]OID_CLIENTE", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, identificadoresCliente, "OID_CLIENTE", Comando))
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaRetorno = New List(Of String)
                For Each dr In dt.Rows
                    listaRetorno.Add(dr("OID_CLIENTE"))
                Next
            End If

            Return listaRetorno
        End Function

        ''' <summary>
        ''' Recupera uma lista de identificadores de clientes ou subcliente ou punto de servicio são totalizadores de saldos
        ''' </summary>
        ''' <param name="identificadoresCliente">Lista de identificadores a ser verificado se são totalizadores</param>
        ''' <returns>Retorna os identificadores de clientes são totalizadores de saldos ou subcliente ou punto de servicio são totalizadores de saldos</returns>
        ''' <remarks></remarks>
        Public Shared Function IdentificadoresClienteOuSubClienteouPuntoServicioTotalizadorSaldo(identificadoresCliente As List(Of String)) As List(Of String)
            Dim listaRetorno As List(Of String) = Nothing
            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandType = CommandType.Text

            Comando.CommandText = My.Resources.ClienteIdentificadoresTotalizadorSaldoSubCliPtoServicio.Replace("[]OID_CLIENTE", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, identificadoresCliente, "OID_CLIENTE", Comando))
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaRetorno = New List(Of String)
                For Each dr In dt.Rows
                    listaRetorno.Add(dr("OID_CLIENTE"))
                Next
            End If

            Return listaRetorno
        End Function

        ''' <summary>
        ''' Recupera o cliente totalizador de saldo
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function RecuperarClienteTotalizadorSaldo(CodigoCliente As String, CodigoSubCliente As String, CodigoPuntoServicio As String, CodigoSubCanal As String) As Clases.Cliente

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandType = CommandType.Text

            Dim cliente As Clases.Cliente = Nothing

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarClienteTotalizadorSaldo)
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoCliente))

            If Not String.IsNullOrEmpty(CodigoSubCliente) AndAlso Not String.IsNullOrEmpty(CodigoPuntoServicio) Then

                Comando.CommandText += " AND SUBC_M.COD_SUBCLIENTE = []COD_SUBCLIENTE AND PUSE_M.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCliente))
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuntoServicio))

            ElseIf Not String.IsNullOrEmpty(CodigoSubCliente) Then

                Comando.CommandText += " AND SUBC_M.COD_SUBCLIENTE = []COD_SUBCLIENTE AND PUSE_M.COD_PTO_SERVICIO IS NULL "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCliente))

            ElseIf Not String.IsNullOrEmpty(CodigoPuntoServicio) Then

                Comando.CommandText += " AND SUBC_M.COD_SUBCLIENTE IS NULL AND PUSE_M.COD_PTO_SERVICIO = []COD_PTO_SERVICIO "
                Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuntoServicio))

            Else

                Comando.CommandText += " AND SUBC_M.COD_SUBCLIENTE IS NULL AND PUSE_M.COD_PTO_SERVICIO IS NULL "

            End If

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim linha As DataRow = dt.Rows(0)

                If Not String.IsNullOrEmpty(CodigoSubCanal) Then

                    linha = (From row As DataRow In dt.Rows
                             Where Util.AtribuirValorObj(row("COD_SUBCANAL"), GetType(String)) = CodigoSubCanal).FirstOrDefault

                    If linha Is Nothing Then
                        linha = (From row As DataRow In dt.Rows
                                 Where Util.AtribuirValorObj(row("COD_SUBCANAL"), GetType(String)) <> CodigoSubCanal).FirstOrDefault
                    End If

                End If

                cliente = New Clases.Cliente With {.Identificador = Util.AtribuirValorObj(linha("OID_CLIENTE"), GetType(String)),
                                                   .Codigo = Util.AtribuirValorObj(linha("COD_CLIENTE"), GetType(String)),
                                                   .Descripcion = Util.AtribuirValorObj(linha("DES_CLIENTE"), GetType(String))}

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(linha("OID_SUBCLIENTE"), GetType(String))) Then
                    cliente.SubClientes = New ObjectModel.ObservableCollection(Of Clases.SubCliente)
                    cliente.SubClientes.Add(New Clases.SubCliente With {.Identificador = Util.AtribuirValorObj(linha("OID_SUBCLIENTE"), GetType(String)),
                                                                        .Codigo = Util.AtribuirValorObj(linha("COD_SUBCLIENTE"), GetType(String)),
                                                                        .Descripcion = Util.AtribuirValorObj(linha("DES_SUBCLIENTE"), GetType(String))})
                End If

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dt.Rows(0)("OID_PTO_SERVICIO"), GetType(String))) Then
                    cliente.SubClientes.First.PuntosServicio = New ObjectModel.ObservableCollection(Of Clases.PuntoServicio)
                    cliente.SubClientes.First.PuntosServicio.Add(New Clases.PuntoServicio With {.Identificador = Util.AtribuirValorObj(linha("OID_PTO_SERVICIO"), GetType(String)),
                                                                                                .Codigo = Util.AtribuirValorObj(linha("COD_PTO_SERVICIO"), GetType(String)),
                                                                                                .Descripcion = Util.AtribuirValorObj(linha("DES_PTO_SERVICIO"), GetType(String))})
                End If

            End If

            Return cliente

        End Function

        Public Shared Function ObtenerClienteTotalizadorSaldo(cuenta As Clases.Cuenta) As Clases.Cliente

            If cuenta IsNot Nothing Then

                If cuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuenta.Cliente.Identificador) AndAlso
                   cuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(cuenta.SubCanal.Identificador) Then

                    Dim identificadorSubCliente = If(cuenta.SubCliente Is Nothing, String.Empty, cuenta.SubCliente.Identificador)
                    Dim identificadorPuntoServicio = If(cuenta.PuntoServicio Is Nothing, String.Empty, cuenta.PuntoServicio.Identificador)

                    Dim totalizadoresSaldos As List(Of Clases.TotalizadorSaldo) = AccesoDatos.Comon.RecuperarTotalizadoresSaldos(String.Empty,
                                                                                                                                 String.Empty,
                                                                                                                                 String.Empty,
                                                                                                                                 cuenta.Cliente.Identificador,
                                                                                                                                 identificadorSubCliente,
                                                                                                                                 identificadorPuntoServicio,
                                                                                                                                 cuenta.SubCanal.Identificador)

                    If totalizadoresSaldos IsNot Nothing AndAlso totalizadoresSaldos IsNot Nothing AndAlso totalizadoresSaldos.Count > 0 Then

                        Dim totalizador As Clases.TotalizadorSaldo = Nothing

                        If totalizadoresSaldos.Count = 1 Then
                            totalizador = totalizadoresSaldos.First

                        Else

                            totalizador = totalizadoresSaldos.Find(Function(t) t.bolDefecto = True)

                        End If

                        If totalizador Is Nothing Then Return Nothing

                        Dim cliente As Clases.Cliente = totalizador.Cliente

                        If totalizador.SubCliente IsNot Nothing Then
                            cliente.SubClientes = New ObservableCollection(Of Clases.SubCliente) From {totalizador.SubCliente}

                            If totalizador.PuntoServicio IsNot Nothing Then
                                cliente.SubClientes.FirstOrDefault.PuntosServicio = New ObservableCollection(Of Clases.PuntoServicio) From {totalizador.PuntoServicio}
                            End If

                        End If

                        Return cliente

                    End If

                End If

            End If

            Return Nothing

        End Function

    End Class

End Namespace