Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text

Namespace Genesis

    Public Class Parcial

        Shared Function cargarParciales(dtParciales As DataTable) As ObservableCollection(Of Clases.Parcial)
            Dim parciales As New ObservableCollection(Of Clases.Parcial)
            If dtParciales IsNot Nothing AndAlso dtParciales.Rows.Count > 0 Then

                For Each rowParciales In dtParciales.Rows

                    If parciales.Find(Function(p) p.Identificador = rowParciales("P_OID_PARCIAL")) Is Nothing Then
                        Dim parcial As New Clases.Parcial
                        With parcial

                            .Identificador = Util.AtribuirValorObj(rowParciales("P_OID_PARCIAL"), GetType(String))
                            .IdentificadorExterno = Util.AtribuirValorObj(rowParciales("P_OID_EXTERNO"), GetType(String))
                            .EsFicticio = False
                            .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoParcial)(Util.AtribuirValorObj(rowParciales("P_COD_ESTADO"), GetType(String)))
                            .FechaHoraCreacion = Util.AtribuirValorObj(rowParciales("P_GMT_CREACION"), GetType(DateTime))
                            .FechaHoraFinConteo = Util.AtribuirValorObj(rowParciales("P_FYH_CONTEO_FIN"), GetType(DateTime))
                            .FechaHoraInicioConteo = Util.AtribuirValorObj(rowParciales("P_FYH_CONTEO_INICIO"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(rowParciales("P_GMT_MODIFICACION"), GetType(DateTime))
                            .Precintos = New ObservableCollection(Of String)
                            .Precintos.Add(Util.AtribuirValorObj(rowParciales("P_COD_PRECINTO"), GetType(String)))
                            .PuestoResponsable = Util.AtribuirValorObj(rowParciales("P_COD_PUESTO_RESPONSABLE"), GetType(String))
                            .Secuencia = Util.AtribuirValorObj(rowParciales("P_NEC_SECUENCIA"), GetType(Integer))
                            .UsuarioCreacion = Util.AtribuirValorObj(rowParciales("P_DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(rowParciales("P_DES_USUARIO_MODIFICACION"), GetType(String))
                            .UsuarioResponsable = Util.AtribuirValorObj(rowParciales("P_COD_USUARIO_RESPONSABLE"), GetType(String))
                            .Divisas = New ObservableCollection(Of Clases.Divisa)
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowParciales("P_OID_IAC"), GetType(String))) Then
                                .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowParciales("P_OID_IAC"), GetType(String))}
                            End If

                        End With

                        parciales.Add(parcial)
                    End If
                Next

            End If

            Return parciales
        End Function

        Shared Function cargarParciales_SinProcedure(dtParciales As DataTable) As ObservableCollection(Of Clases.Parcial)
            Dim parciales As New ObservableCollection(Of Clases.Parcial)
            If dtParciales IsNot Nothing AndAlso dtParciales.Rows.Count > 0 Then

                For Each rowParciales In dtParciales.Rows

                    If parciales.Find(Function(p) p.Identificador = rowParciales("OID_PARCIAL")) Is Nothing Then
                        Dim parcial As New Clases.Parcial
                        With parcial

                            .Identificador = Util.AtribuirValorObj(rowParciales("OID_PARCIAL"), GetType(String))
                            .IdentificadorExterno = Util.AtribuirValorObj(rowParciales("P_OID_EXTERNO"), GetType(String))
                            .EsFicticio = False
                            .Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoParcial)(Util.AtribuirValorObj(rowParciales("P_COD_ESTADO"), GetType(String)))
                            .FechaHoraCreacion = Util.AtribuirValorObj(rowParciales("P_GMT_CREACION"), GetType(DateTime))
                            .FechaHoraFinConteo = Util.AtribuirValorObj(rowParciales("P_FYH_CONTEO_FIN"), GetType(DateTime))
                            .FechaHoraInicioConteo = Util.AtribuirValorObj(rowParciales("P_FYH_CONTEO_INICIO"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(rowParciales("P_GMT_MODIFICACION"), GetType(DateTime))
                            .Precintos = New ObservableCollection(Of String)
                            .Precintos.Add(Util.AtribuirValorObj(rowParciales("P_COD_PRECINTO"), GetType(String)))
                            .PuestoResponsable = Util.AtribuirValorObj(rowParciales("P_COD_PUESTO_RESPONSABLE"), GetType(String))
                            .Secuencia = Util.AtribuirValorObj(rowParciales("P_NEC_SECUENCIA"), GetType(Integer))
                            .UsuarioCreacion = Util.AtribuirValorObj(rowParciales("P_DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(rowParciales("P_DES_USUARIO_MODIFICACION"), GetType(String))
                            .UsuarioResponsable = Util.AtribuirValorObj(rowParciales("P_COD_USUARIO_RESPONSABLE"), GetType(String))
                            .Divisas = New ObservableCollection(Of Clases.Divisa)
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowParciales("P_OID_IAC"), GetType(String))) Then
                                .GrupoTerminosIAC = New Clases.GrupoTerminosIAC With {.Identificador = Util.AtribuirValorObj(rowParciales("P_OID_IAC"), GetType(String))}
                            End If

                        End With

                        parciales.Add(parcial)
                    End If
                Next

            End If

            Return parciales
        End Function
#Region "[CONSULTAS]"

        ''' <summary>
        ''' Obtener Parciales de acuerdo con el filtro
        ''' </summary>
        ''' <param name="objFiltro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerParciales(objFiltro As Clases.Transferencias.Filtro) As DataTable

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = My.Resources.ObtenerParciales
            cmd.CommandType = CommandType.Text

            Dim QueryWhere As New StringBuilder
            QueryWhere.Append(" WHERE ")

            If objFiltro IsNot Nothing Then

                ' Monta query Cuenta
                Util.PreencherQueryCuenta(objFiltro, QueryWhere, cmd)

                If objFiltro.Remesa IsNot Nothing Then
                    Dim RemesaIdentificador As New List(Of String)
                    For Each objRemesa In objFiltro.Remesa
                        If Not String.IsNullOrEmpty(objRemesa.Identificador) Then
                            RemesaIdentificador.Add(objRemesa.Identificador)
                        End If
                    Next

                    If RemesaIdentificador IsNot Nothing AndAlso RemesaIdentificador.Count > 0 Then
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, RemesaIdentificador, "OID_REMESA", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "B", "BUL"))
                    End If

                End If

                If objFiltro.Bulto IsNot Nothing Then
                    Dim BultoIdentificador As New List(Of String)
                    For Each objBulto In objFiltro.Bulto
                        If Not String.IsNullOrEmpty(objBulto.Identificador) Then
                            BultoIdentificador.Add(objBulto.Identificador)
                        End If
                    Next

                    If BultoIdentificador IsNot Nothing AndAlso BultoIdentificador.Count > 0 Then
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, BultoIdentificador, "OID_BULTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "B", "BUL"))
                    End If

                End If

                Dim CodigosValor As New List(Of String)
                If objFiltro.Parcial IsNot Nothing Then

                    Dim ParcialIdentificador As New List(Of String)
                    Dim ParcialPrecintos As New List(Of String)
                    Dim ParcialFechaAltaDesde As Nullable(Of DateTime)
                    Dim ParcialFechaAltaHasta As Nullable(Of DateTime)

                    For Each objParcial In objFiltro.Parcial
                        If Not String.IsNullOrEmpty(objParcial.Identificador) Then
                            ParcialIdentificador.Add(objParcial.Identificador)
                        End If

                        If objParcial.Precintos IsNot Nothing Then
                            For Each precinto In objParcial.Precintos
                                ParcialPrecintos.Add(precinto)
                            Next
                        End If

                        If objParcial.TipoFormato IsNot Nothing Then
                            CodigosValor.Add(objParcial.TipoFormato.Codigo)
                        End If

                        If objParcial.FechaAltaDesde Is Nothing Then
                            ParcialFechaAltaDesde = objParcial.FechaAltaDesde
                        End If
                        If objParcial.FechaAltaHasta IsNot Nothing Then
                            ParcialFechaAltaHasta = objParcial.FechaAltaHasta
                        End If
                    Next

                    If ParcialIdentificador IsNot Nothing AndAlso ParcialIdentificador.Count > 0 Then
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ParcialIdentificador, "OID_PARCIAL", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "P", "PAR"))
                    End If

                    If ParcialPrecintos IsNot Nothing AndAlso ParcialPrecintos.Count > 0 Then
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, ParcialPrecintos, "COD_PRECINTO", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "P", "PAR"))
                    End If

                    If ParcialFechaAltaDesde IsNot Nothing Then
                        QueryWhere.Append(If(QueryWhere.ToString <> " WHERE ", " AND P.GMT_CREACION >= []GMT_CREACION_PARCIAL_DESDE ", " P.GMT_CREACION >= []GMT_CREACION_PARCIAL_DESDE "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_PARCIAL_DESDE", ProsegurDbType.Data_Hora, ParcialFechaAltaDesde))
                    End If
                    If ParcialFechaAltaHasta IsNot Nothing Then
                        QueryWhere.Append(If(QueryWhere.ToString <> " WHERE ", " AND P.GMT_CREACION < []GMT_CREACION_PARCIAL_HASTA ", " P.GMT_CREACION < []GMT_CREACION_PARCIAL_HASTA "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_PARCIAL_HASTA", ProsegurDbType.Data_Hora, ParcialFechaAltaHasta))
                    End If

                End If

                If CodigosValor.Count > 0 Then
                    QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigosValor, "COD_VALOR", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "LV", "LVA"))
                    cmd.CommandText = String.Format(cmd.CommandText, "INNER JOIN SAPR_TLISTA_VALORXELEMENTO LVE ON LVE.OID_REMESA = B.OID_REMESA AND LVE.OID_BULTO = B.OID_BULTO AND LVE.OID_PARCIAL IS NULL" & Environment.NewLine & _
                                                                        "INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LVE.OID_LISTA_TIPO " & Environment.NewLine & _
                                                                        "INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = LVE.OID_LISTA_VALOR")

                End If

            End If

            If QueryWhere.ToString = " WHERE " Then
                QueryWhere = New StringBuilder
            End If
            If QueryWhere.ToString.Length > 0 Then
                cmd.CommandText = cmd.CommandText & QueryWhere.ToString()
            End If
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, " "))

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Grabar una nueva parcial.
        ''' </summary>
        ''' <param name="objParcial"></param>
        ''' <param name="identificadorBulto">Identificador del Bulto</param>
        ''' <remarks></remarks>
        Public Shared Sub GrabarParcial(objParcial As Clases.Parcial, identificadorBulto As String,
                             Optional ByRef _transacion As DbHelper.Transacao = Nothing)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParcialInserir)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, objParcial.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

            If objParcial.IdentificadorExterno IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, objParcial.IdentificadorExterno))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_EXTERNO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            'O precinto está preparado para uma lista de valores, porem nesta caso terá apanes um valor, por isso recupera o primeiro registro.
            If objParcial.Precintos IsNot Nothing AndAlso objParcial.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objParcial.Precintos.FirstOrDefault) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objParcial.Precintos.FirstOrDefault))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, DBNull.Value))
            End If

            If objParcial.GrupoTerminosIAC IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, objParcial.GrupoTerminosIAC.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objParcial.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_PADRE", ProsegurDbType.Objeto_Id, objParcial.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objParcial.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_SUSTITUTA", ProsegurDbType.Objeto_Id, objParcial.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_SUSTITUTA", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objParcial.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objParcial.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objParcial.PuestoResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEC_SECUENCIA", ProsegurDbType.Inteiro_Curto, objParcial.Secuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objParcial.UsuarioCreacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objParcial.UsuarioModificacion))

            If _transacion IsNot Nothing Then
                _transacion.AdicionarItemTransacao(cmd)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End If

        End Sub

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar Parcial.
        ''' </summary>
        ''' <param name="objParcial">Objeto parcial preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarParcial(objParcial As Clases.Parcial)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParcialAtualizar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, objParcial.Identificador))

            'O precinto está preparado para uma lista de valores, porem nesta caso terá apanes um valor, por isso recupera o primeiro registro.
            If objParcial.Precintos IsNot Nothing AndAlso objParcial.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(objParcial.Precintos.FirstOrDefault) Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, objParcial.Precintos.FirstOrDefault()))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Descricao_Longa, DBNull.Value))
            End If

            If objParcial.ElementoPadre IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_PADRE", ProsegurDbType.Objeto_Id, objParcial.ElementoPadre.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_PADRE", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objParcial.ElementoSustituto IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_SUSTITUTA", ProsegurDbType.Objeto_Id, objParcial.ElementoSustituto.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL_SUSTITUTA", ProsegurDbType.Objeto_Id, Nothing))
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objParcial.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_USUARIO_RESPONSABLE", ProsegurDbType.Descricao_Longa, objParcial.UsuarioResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO_RESPONSABLE", ProsegurDbType.Descricao_Curta, objParcial.PuestoResponsable))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEC_SECUENCIA", ProsegurDbType.Inteiro_Curto, objParcial.Secuencia))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objParcial.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Actualizar estado de la parcial.
        ''' </summary>
        ''' <param name="objParcial">Objeto parcial preenchido.</param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstadoParcial(objParcial As Clases.Parcial)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParcialAtualizarEstado)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, objParcial.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, objParcial.Estado.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objParcial.UsuarioModificacion))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        Public Shared Sub ActualizarEstadoParcial(identificador As String,
                                                  estado As Enumeradores.EstadoParcial,
                                                  usuario As String)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, identificador))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Descricao_Curta, estado.RecuperarValor))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParcialAtualizarEstado)
                    command.CommandType = CommandType.Text

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub
#End Region

#Region "[ELIMINAR]"

        ''' <summary>
        ''' Eliminar Parcial
        ''' </summary>
        ''' <param name="idendificadorParcial">Idendificador de la Parcial</param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarParcial(idendificadorParcial As String)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParcialExcluir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PARCIAL", ProsegurDbType.Objeto_Id, idendificadorParcial))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

#End Region

    End Class

End Namespace
