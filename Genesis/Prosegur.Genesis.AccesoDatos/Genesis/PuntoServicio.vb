Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class PuntoServicio


        Shared Function Validar(_identificadorSubCliente As String, codigoPuntoServicio As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(_identificadorSubCliente) AndAlso Not String.IsNullOrEmpty(codigoPuntoServicio) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND CA.OID_TABLA_GENESIS = PS.oid_pto_servicio "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoPuntoServicio))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND PS.COD_pto_servicio = []COD_pto_servicio "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_pto_servicio", ProsegurDbType.Descricao_Curta, codigoPuntoServicio))
                    End If

                    filtro &= " AND PS.OID_SUBCLIENTE = []OID_SUBCLIENTE AND PS.BOL_VIGENTE = 1 "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Descricao_Curta, _identificadorSubCliente))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.PuntoServicio_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function

        Public Shared Function ObtenerIdentificadorPuntoServicio_v2(CodigoCliente As String, CodigoSubCliente As String, CodigoPuntoServicio As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorPuntoServicioPorCodigo_v2)
            Comando.CommandType = CommandType.Text

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Objeto_Id, CodigoCliente))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Objeto_Id, CodigoSubCliente))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, CodigoPuntoServicio))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_PTO_SERVICIO"), GetType(String))
            End If
            Return Nothing
        End Function

        Public Shared Function ObtenerIdentificadorPuntoServicio(IdentificadorCliente As String, IdentificadorSubCliente As String, CodigoPuntoServicio As String) As String

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerIdentificadorPuntoServicioPorCodigo)
            Comando.CommandType = CommandType.Text

            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IdentificadorCliente))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IdentificadorSubCliente))
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, CodigoPuntoServicio))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return Util.AtribuirValorObj(dt.Rows(0)("OID_PTO_SERVICIO"), GetType(String))
            End If
            Return Nothing
        End Function


        Public Shared Function ObtenerPuntoServicio(CodigoPuntoServicio As String) As Clases.PuntoServicio

            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerPuntoServicioPorCodigo)
            Comando.CommandType = CommandType.Text
            Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, CodigoPuntoServicio))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim PuntoServicio As Clases.PuntoServicio = CargarPuntoServicio(dt.Rows(0))
                Return PuntoServicio

            End If
            Return Nothing
        End Function

        Private Shared Function CargarPuntoServicio(dr As DataRow) As Clases.PuntoServicio
            Return New Clases.PuntoServicio With {.Identificador = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String)), _
                                                  .Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String)), _
                                                  .Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String)), _
                                                  .EstaActivo = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Int16)), _
                                                  .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                                  .FechaHoraActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)), _
                                                  .EstaEnviadoSaldos = Util.AtribuirValorObj(dr("BOL_ENVIADO_SALDOS"), GetType(Int16)), _
                                                  .CodigoMigracion = Util.AtribuirValorObj(dr("COD_MIGRACION"), GetType(String)), _
                                                  .EsTotalizadorSaldo = Util.AtribuirValorObj(dr("BOL_TOTALIZADOR_SALDO"), GetType(Int16)), _
                                                  .TipoPuntoServicio = If(Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("TPPS_OID_TIPO_PUNTO_SERVICIO"), GetType(String))), (New Clases.Tipo() With {
                                                                                                                                                                                                         .Codigo = Util.AtribuirValorObj(dr("TPPS_COD_TIPO_PUNTO_SERVICIO"), GetType(String)), _
                                                                                                                                                                                                         .Descripcion = Util.AtribuirValorObj(dr("TPPS_DES_TIPO_PUNTO_SERVICIO"), GetType(String)), _
                                                                                                                                                                                                         .EstaActivo = Util.AtribuirValorObj(dr("TPPS_BOL_ACTIVO"), GetType(Int16)), _
                                                                                                                                                                                                         .FechaHoraCreacion = Util.AtribuirValorObj(dr("TPPS_GMT_CREACION"), GetType(DateTime)), _
                                                                                                                                                                                                         .FechaHoraModificacion = Util.AtribuirValorObj(dr("TPPS_GMT_MODIFICACION"), GetType(DateTime)), _
                                                                                                                                                                                                         .Identificador = Util.AtribuirValorObj(dr("TPPS_OID_TIPO_PUNTO_SERVICIO"), GetType(String)), _
                                                                                                                                                                                                         .TipoFiliacion = Enumeradores.TipoFiliacion.PuntoServicio, _
                                                                                                                                                                                                         .UsuarioCreacion = Util.AtribuirValorObj(dr("TPPS_DES_USUARIO_CREACION"), GetType(String)), _
                                                                                                                                                                                                         .UsuarioModificacion = Util.AtribuirValorObj(dr("TPPS_DES_USUARIO_MODIFICACION"), GetType(String))
                                                                                                                                                                                                         }), Nothing)
                                              }
        End Function

        ''' <summary>
        ''' Recupera uma lista de identificadores de Punto de Servicio que são totalizadores de saldos
        ''' </summary>
        ''' <param name="identificadoresPuntoServicio">Lista de identificadores a ser verificado se são totalizadores</param>
        ''' <returns>Retorna os identificadores de Punto de Servicio que são totalizadores de saldos</returns>
        ''' <remarks></remarks>
        Public Shared Function IdentificadoresPuntoServicioTotalizadorSaldo(identificadoresPuntoServicio As List(Of String)) As List(Of String)
            Dim listaRetorno As List(Of String) = Nothing
            Dim Comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Comando.CommandType = CommandType.Text
            Comando.CommandText = My.Resources.PuntoServicioIdentificadoresTotalizadorSaldo.Replace("[]OID_PTO_SERVICIO", Util.MontarParametroIn(Constantes.CONEXAO_GENESIS, identificadoresPuntoServicio, "OID_PTO_SERVICIO", Comando))
            Comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, Comando.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, Comando)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                listaRetorno = New List(Of String)
                For Each dr In dt.Rows
                    listaRetorno.Add(dr("OID_PTO_SERVICIO"))
                Next
            End If

            Return listaRetorno
        End Function

        Shared Function ObtenerPuntoServicioJSON(codigo As String, descripcion As String, identificadorPadre As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Dim lista As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    filtro &= " AND UPPER(COD_PTO_SERVICIO) like '%' || []COD_PTO_SERVICIO || '%' "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", _
                                                                                ProsegurDbType.Objeto_Id, codigo.ToUpper()))


                    If Not String.IsNullOrEmpty(descripcion) Then
                        filtro &= " AND UPPER(DES_PTO_SERVICIO) like '%' || []DES_PTO_SERVICIO || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_PTO_SERVICIO", _
                                                                                    ProsegurDbType.Objeto_Id, descripcion.ToUpper()))
                    End If

                    If Not String.IsNullOrEmpty(identificadorPadre) Then
                        filtro &= " AND OID_SUBCLIENTE like '%' || []OID_SUBCLIENTE || '%' "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", _
                                                                                    ProsegurDbType.Objeto_Id, identificadorPadre))
                    End If

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerPuntoServicioPorCodigo_v2, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                        lista = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)

                        For Each rowCliente In dt.Rows

                            lista.Add(New Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor With {.Identificador = Util.AtribuirValorObj(rowCliente("OID_PTO_SERVICIO"), GetType(String)), _
                                        .Descripcion = Util.AtribuirValorObj(rowCliente("DES_PTO_SERVICIO"), GetType(String)), _
                                        .Codigo = Util.AtribuirValorObj(rowCliente("COD_PTO_SERVICIO"), GetType(String))
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