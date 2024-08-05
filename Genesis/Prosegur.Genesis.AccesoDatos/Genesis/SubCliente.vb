Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class SubCliente

        Shared Function Validar(_identificadorCliente As String, codigoSubCliente As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(_identificadorCliente) AndAlso Not String.IsNullOrEmpty(codigoSubCliente) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' AND CA.OID_TABLA_GENESIS = SC.OID_SUBCLIENTE "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoSubCliente))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND SC.COD_SUBCLIENTE = []COD_SUBCLIENTE "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Curta, codigoSubCliente))
                    End If

                    filtro &= " AND SC.OID_CLIENTE = []OID_CLIENTE AND SC.BOL_VIGENTE = 1 "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Descricao_Curta, _identificadorCliente))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.SuCliente_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function

        Public Shared Function ObtenerIdentificadorSubCliente(IdentificadorCliente As String, CodigoSubCliente As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorSubClientePorCodigo)
            Comando.CommandType = CommandType.Text

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, CodigoSubCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_SUBCLIENTE"), GetType(String))
            End If
            Return Nothing
        End Function


        Public Shared Function ObtenerSubCliente(CodigoSubCliente As String) As Clases.SubCliente

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerSubClientePorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, CodigoSubCliente))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim SubCliente As Clases.SubCliente = CargarSubCliente(dt.Rows(0))
                Return SubCliente

            End If
            Return Nothing
        End Function

        Private Shared Function CargarSubCliente(dr As DataRow) As Clases.SubCliente
            Return New Clases.SubCliente With {.Identificador = Util.AtribuirValorObj(dr("OID_SUBCLIENTE"), GetType(String)), _
                                               .Descripcion = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String)), _
                                               .Codigo = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String)), _
                                               .EstaActivo = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Int16)), _
                                               .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                               .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)), _
                                               .EstaEnviadoSaldos = Util.AtribuirValorObj(dr("BOL_ENVIADO_SALDOS"), GetType(Int16)), _
                                               .EsTotalizadorSaldo = Util.AtribuirValorObj(dr("BOL_TOTALIZADOR_SALDO"), GetType(Int16)), _
                                               .CodigoMigracion = Util.AtribuirValorObj(dr("COD_MIGRACION"), GetType(String)), _
                                               .TipoSubCliente = If(Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("TPSU_OID_TIPO_SUBCLIENTE"), GetType(String))), (New Clases.Tipo() With {
                                                                                                                                                                                              .Codigo = Util.AtribuirValorObj(dr("TPSU_COD_TIPO_SUBCLIENTE"), GetType(String)), _
                                                                                                                                                                                              .Descripcion = Util.AtribuirValorObj(dr("TPSU_DES_TIPO_SUBCLIENTE"), GetType(String)), _
                                                                                                                                                                                              .EstaActivo = Util.AtribuirValorObj(dr("TPSU_BOL_ACTIVO"), GetType(Int16)), _
                                                                                                                                                                                              .FechaHoraCreacion = Util.AtribuirValorObj(dr("TPSU_GMT_CREACION"), GetType(DateTime)), _
                                                                                                                                                                                              .FechaHoraModificacion = Util.AtribuirValorObj(dr("TPSU_GMT_MODIFICACION"), GetType(DateTime)), _
                                                                                                                                                                                              .Identificador = Util.AtribuirValorObj(dr("TPSU_OID_TIPO_SUBCLIENTE"), GetType(String)), _
                                                                                                                                                                                              .TipoFiliacion = Enumeradores.TipoFiliacion.SubCliente, _
                                                                                                                                                                                              .UsuarioCreacion = Util.AtribuirValorObj(dr("TPSU_DES_USUARIO_CREACION"), GetType(String)), _
                                                                                                                                                                                              .UsuarioModificacion = Util.AtribuirValorObj(dr("TPSU_DES_USUARIO_MODIFICACION"), GetType(String))
                                                                                                                                                                                              }), Nothing)
                                              }
        End Function

        ''' <summary>
        ''' Recupera uma lista de identificadores de Subclientes que são totalizadores de saldos
        ''' </summary>
        ''' <param name="identificadoresSubCliente">Lista de identificadores a ser verificado se são totalizadores</param>
        ''' <returns>Retorna os identificadores de subclientes que são totalizadores de saldos</returns>
        ''' <remarks></remarks>
        Public Shared Function IdentificadoresSubClienteTotalizadorSaldo(identificadoresSubCliente As List(Of String)) As List(Of String)
            Dim listaRetorno As List(Of String) = Nothing
            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandType = CommandType.Text
            Comando.CommandText = My.Resources.SubClienteIdentificadoresTotalizadorSaldo.Replace("[]OID_SUBCLIENTE", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, identificadoresSubCliente, "OID_SUBCLIENTE", Comando))
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaRetorno = New List(Of String)
                For Each dr In dt.Rows
                    listaRetorno.Add(dr("OID_SUBCLIENTE"))
                Next
            End If

            Return listaRetorno
        End Function

        ''' <summary>
        ''' Recupera uma lista de identificadores de SUBclientes ou punto de servicio são totalizadores de saldos
        ''' </summary>
        ''' <param name="identificadoresSubCliente">Lista de identificadores a ser verificado se são totalizadores</param>
        ''' <returns>Retorna os identificadores de subclientes são totalizadores de saldos ou punto de servicio são totalizadores de saldos</returns>
        ''' <remarks></remarks>
        Public Shared Function IdentificadoresSubClientePuntoServicioTotalizadorSaldo(identificadoresSubCliente As List(Of String)) As List(Of String)
            Dim listaRetorno As List(Of String) = Nothing
            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandType = CommandType.Text

            Comando.CommandText = My.Resources.SubClienteIdentificadoresTotalizadorSaldoPtoServicio.Replace("[]OID_SUBCLIENTE", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, identificadoresSubCliente, "OID_SUBCLIENTE", Comando))
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaRetorno = New List(Of String)
                For Each dr In dt.Rows
                    listaRetorno.Add(dr("OID_SUBCLIENTE"))
                Next
            End If

            Return listaRetorno
        End Function

        Public Shared Function ObtenerSubClientePorPuntoServicio(IdentificadorPuntoServicio As String) As Clases.SubCliente

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerSubClientePorPuntoServicio)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, IdentificadorPuntoServicio))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim SubCliente As Clases.SubCliente = CargarSubCliente(dt.Rows(0))
                Return SubCliente

            End If
            Return Nothing
        End Function

        Shared Function ObtenerSubClienteJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_SUBCLIENTE) like '%' || []COD_SUBCLIENTE || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))


                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_SUBCLIENTE) like '%' || []DES_SUBCLIENTE || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_SUBCLIENTE", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(identificadorPadre) Then
                        filtro &= " AND OID_CLIENTE like '%' || []OID_CLIENTE || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorPadre))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerSubClientePorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_SUBCLIENTE"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_SUBCLIENTE"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_SUBCLIENTE"), GetType(String))
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