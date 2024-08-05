Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports System.Text
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace Genesis
    ''' <summary>
    ''' Clase AccionDelegacion
    ''' </summary>
    Public Class Delegacion

        Shared Function Validar(codigoDelegacion As String, IdentificadorAjeno As String) As String

            Dim identificador As String = ""

            If Not String.IsNullOrEmpty(codigoDelegacion) Then

                Dim dt As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtro As String = ""
                    Dim inner As String = ""

                    If Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                        inner = " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND CA.OID_TABLA_GENESIS = D.OID_DELEGACION "

                        filtro = " AND CA.COD_AJENO = []COD_AJENO AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR AND D.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    Else
                        filtro = " AND D.COD_DELEGACION = []COD_DELEGACION AND D.BOL_VIGENTE = 1 "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                    End If

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.Delegacion_ValidarCodigo, inner, filtro))
                    cmd.CommandType = CommandType.Text

                    dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    identificador = If(dt.Rows(0).Table.Columns.Contains("IDENTIFICADOR"), Util.AtribuirValorObj(dt.Rows(0)("IDENTIFICADOR"), GetType(String)), "")

                End If

            End If

            Return identificador
        End Function

        Shared Function ObtenerCodigoPorIdentificador(identificadorDelegacion As String) As String

            Dim codigo As String = ""

            If Not String.IsNullOrEmpty(identificadorDelegacion) Then
                Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                    Dim dtDados As DataTable
                    Dim sqlResource As String = My.Resources.Delegacion_ObtenerCodigoPorIdentificador

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, identificadorDelegacion))
                    cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                    dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                    If dtDados IsNot Nothing AndAlso dtDados.Rows.Count > 0 Then
                        codigo = dtDados.Rows(0)("COD_DELEGACION")
                    End If
                End Using
            End If

            Return codigo
        End Function


        Public Shared Function ObtenerPorIdentificadorSector(identificadorSector As String) As Prosegur.Genesis.Comon.Clases.Delegacion
            Dim delegacion As New Prosegur.Genesis.Comon.Clases.Delegacion

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerDelegacionPorIdentificadorSector

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Curta, identificadorSector))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If (dtDados.Rows.Count > 0) Then
                    delegacion = Cargar(dtDados.Rows(0))
                End If
            End Using

            Return delegacion
        End Function

        Public Shared Function ObtenerIdentificadorPorCodigo(CodigoDelegacion As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.DelegacionObtenerIdentificadorPorCodigo)
            cmd.CommandType = CommandType.Text


            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        ''' <summary>
        ''' Método que recupera a delegação pelo identificador da planta.
        ''' </summary>
        ''' <param name="identificadorPlanta">Identificador da planta</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorIdentificadorPlanta(identificadorPlanta As String) As Prosegur.Genesis.Comon.Clases.Delegacion
            Dim delegacion As New Prosegur.Genesis.Comon.Clases.Delegacion

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerDelegacionPorIdentificadorPlanta

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pOID_PLANTA", ProsegurDbType.Descricao_Curta, identificadorPlanta))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If (dtDados.Rows.Count > 0) Then
                    delegacion = Cargar(dtDados.Rows(0))
                End If
            End Using

            Return delegacion
        End Function

        ''' <summary>
        ''' Método que recupera a delegação pelo seu OID.
        ''' </summary>
        ''' <param name="oid">OID a ser pesquisado</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String) As Prosegur.Genesis.Comon.Clases.Delegacion
            Dim delegacion As New Prosegur.Genesis.Comon.Clases.Delegacion

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerDelegacionPorOid

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "pOID_DELEGACION", ProsegurDbType.Descricao_Curta, oid))
                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                If (dtDados.Rows.Count > 0) Then
                    delegacion = Cargar(dtDados.Rows(0))
                End If
            End Using

            Return delegacion
        End Function

        ''' <summary>
        ''' Método que recupera a delegação pelo seu OID.
        ''' </summary>
        ''' <param name="codigoDelegacion">codigo Delegacion</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDelegacionGMT(codigoDelegacion As String, Optional IdentificadorAjeno As String = "") As Prosegur.Genesis.Comon.Clases.Delegacion

            Dim delegaciones As New List(Of Prosegur.Genesis.Comon.Clases.Delegacion)

            Dim codDelegacion As New List(Of String)
            codDelegacion.Add(codigoDelegacion)
            delegaciones = ObtenerPorCodigos(codDelegacion, IdentificadorAjeno)

            If delegaciones IsNot Nothing AndAlso delegaciones.Count > 0 Then
                Return delegaciones(0)
            Else
                Return Nothing
            End If

        End Function

        ''' <summary>
        ''' Método que recupera as delegações pelos seus respectivos codigos.
        ''' </summary>
        ''' <param name="codigos">Codigos a serem pesquisados</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorCodigos(codigos As List(Of String), Optional IdentificadorAjeno As String = "") As List(Of Prosegur.Genesis.Comon.Clases.Delegacion)
            Dim delegaciones As New List(Of Prosegur.Genesis.Comon.Clases.Delegacion)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim dtDados As DataTable
                Dim sqlResource As String = My.Resources.ObtenerDelegacionesPorCodigos
                Dim sqlWhere As New StringBuilder()

                If String.IsNullOrEmpty(IdentificadorAjeno) Then

                    If (codigos.Count > 1) Then
                        sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigos.ToList(), "COD_DELEGACION", cmd, "AND", , "D"))
                    ElseIf codigos.Count = 1 Then
                        sqlWhere.AppendLine(" AND D.COD_DELEGACION = []COD_DELEGACION ")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigos.FirstOrDefault))
                    End If

                    sqlResource = String.Format(sqlResource, "")
                Else
                    sqlResource = String.Format(sqlResource, " INNER JOIN GEPR_TCODIGO_AJENO CA ON CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND CA.OID_TABLA_GENESIS = D.OID_DELEGACION ")

                    sqlWhere.AppendLine(" AND CA.COD_IDENTIFICADOR = []COD_IDENTIFICADOR ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    If (codigos.Count > 1) Then
                        sqlWhere.AppendLine(Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigos.ToList(), "COD_AJENO", cmd, "AND", , "CA"))
                    ElseIf codigos.Count = 1 Then
                        sqlWhere.AppendLine(" AND CA.COD_AJENO = []COD_AJENO ")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigos.FirstOrDefault))
                    End If

                End If

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource & sqlWhere.ToString())

                dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                For Each row As DataRow In dtDados.Rows
                    delegaciones.Add(Cargar(row))
                Next
            End Using

            Return delegaciones
        End Function

        Public Shared Function ObtenerDelegacionesPorCodigos(codigosDelegaciones As List(Of String), codigosTipoSectores As List(Of String)) As DataTable
            Dim dt As DataTable
            Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                Dim sqlResource As String = ""

                If (codigosDelegaciones IsNot Nothing AndAlso codigosDelegaciones.Count > 0) Then
                    sqlResource &= Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigosDelegaciones.ToList(), "COD_DELEGACION", cmd, "AND", "D", )
                End If

                If (codigosTipoSectores IsNot Nothing AndAlso codigosTipoSectores.Count > 0) Then
                    sqlResource &= Prosegur.Genesis.AccesoDatos.Util.MontarClausulaIn(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, codigosTipoSectores.ToList(), "COD_TIPO_SECTOR", cmd, "AND", "TS", )
                End If

                sqlResource = String.Format(My.Resources.ObtenerDelegacionesPorCodigos_V2, sqlResource)

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)
                dt = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)
            End Using
            Return dt
        End Function

        Private Shared Function Cargar(dataRow As DataRow) As Prosegur.Genesis.Comon.Clases.Delegacion
            Dim delegacionRetorno As New Prosegur.Genesis.Comon.Clases.Delegacion With
            {
                .Identificador = Util.AtribuirValorObj(Of String)(dataRow("OID_DELEGACION")),
                .Codigo = Util.AtribuirValorObj(Of String)(dataRow("COD_DELEGACION")),
                .Descripcion = Util.AtribuirValorObj(Of String)(dataRow("DES_DELEGACION")),
                .EsActivo = Util.AtribuirValorObj(Of Boolean)(dataRow("BOL_VIGENTE")),
                .HusoHorarioEnMinutos = Util.AtribuirValorObj(Of Integer)(dataRow("NEC_GMT_MINUTOS")),
                .FechaHoraVeranoInicio = Util.AtribuirValorObj(Of Date)(dataRow("FYH_VERANO_INICIO")),
                .FechaHoraVeranoFin = Util.AtribuirValorObj(Of Date)(dataRow("FYH_VERANO_FIN")),
                .AjusteHorarioVerano = Util.AtribuirValorObj(Of Integer)(dataRow("NEC_VERANO_AJUSTE")),
                .Zona = Util.AtribuirValorObj(Of String)(dataRow("DES_ZONA")),
                .FechaHoraCreacion = Util.AtribuirValorObj(Of Date)(dataRow("GMT_CREACION")),
                .UsuarioCreacion = Util.AtribuirValorObj(Of String)(dataRow("DES_USUARIO_CREACION")),
                .FechaHoraModificacion = Util.AtribuirValorObj(Of Date)(dataRow("GMT_MODIFICACION")),
                .UsuarioModificacion = Util.AtribuirValorObj(Of String)(dataRow("DES_USUARIO_MODIFICACION")),
                .CodigoPais = Util.AtribuirValorObj(Of String)(dataRow("COD_PAIS"))
            }

            Return delegacionRetorno
        End Function

        Shared Function ObtenerCodigoDelegacionPorCodigoAjeno(codigoDelegacion As String, IdentificadorAjeno As String) As String

            Dim codigo As String = ""

            If Not String.IsNullOrEmpty(codigoDelegacion) AndAlso Not String.IsNullOrEmpty(IdentificadorAjeno) Then

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerCodigoDelegacionPorCodigoAjeno)
                    cmd.CommandType = CommandType.Text
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Descricao_Curta, codigoDelegacion))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, IdentificadorAjeno))

                    codigo = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

                End Using

            End If

            Return codigo
        End Function

        Shared Function ObtenerDelegacionJSON(codigo As String, descripcion As String) As List(Of ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Throw New NotImplementedException
        End Function


    End Class
End Namespace