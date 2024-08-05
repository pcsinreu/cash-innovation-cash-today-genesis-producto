Imports Prosegur.Genesis.ContractoServicio
Imports System.Data
Imports System.Configuration
Imports System.IO

Namespace Dashboard

    Public Class ValoresProcesados

#Region "[CONSULTAR]"

        Shared Function RetornaValoresProcesados(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta
            Dim objRespuesta As New Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta()
            Try
                With objRespuesta
                    Dim dtInicio As DateTime = DateTime.MinValue
                    Dim dtFin As DateTime = DateTime.MinValue
                    Dim hoje As DateTime = Date.Today

                    Select Case Peticion.RangoFechaHora
                        Case RangoFecha.Dia
                            dtInicio = hoje
                            dtFin = dtInicio.AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Semana
                            Dim primeiroDiaSemana = DayOfWeek.Monday
                            Dim ultimoDiaSemana = DayOfWeek.Sunday

                            Dim priDiaIndex As Integer = hoje.DayOfWeek
                            If priDiaIndex < primeiroDiaSemana Then
                                priDiaIndex += 7
                            End If
                            Dim priDiaDiff As Integer = priDiaIndex - primeiroDiaSemana

                            dtInicio = hoje.AddDays(-priDiaDiff)

                            Dim ultDiaIndex As Integer = hoje.DayOfWeek
                            If ultDiaIndex > ultimoDiaSemana Then
                                ultDiaIndex -= 7
                            End If
                            Dim ultDiaDiff As Integer = ultimoDiaSemana - ultDiaIndex

                            dtFin = hoje.AddDays(ultDiaDiff).AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Mes
                            dtInicio = New DateTime(hoje.Year, hoje.Month, 1)
                            dtFin = dtInicio.AddMonths(1).AddSeconds(-1)
                        Case RangoFecha.Ano
                            dtInicio = New DateTime(hoje.Year, 1, 1)
                            dtFin = dtInicio.AddYears(1).AddSeconds(-1)
                    End Select

                    Dim dtResult As DataTable = AccesoDatos.Dashboard.ValoresProcesados.RetornaValoresProcesados(dtInicio,
                                                                                                                 dtFin,
                                                                                                                 Peticion.CodigosDelegacion,
                                                                                                                 Peticion.CodigosSector,
                                                                                                                 Peticion.CodigosIsoDivisa,
                                                                                                                 Peticion.Top)
                    .Dados = PreencheRetornaValoresProcesados(dtResult)
                End With
                Return objRespuesta
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta = New Contractos.Dashboard.Productividad.RetornaValoresProcesados.Respuesta(ex)
                Return objRespuesta
            End Try
        End Function

        Shared Function RetornaSomaValoresProcesadosCliente(Peticion As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Peticion) As Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta
            Dim objRespuesta As New Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta()
            Try
                With objRespuesta
                    Dim dtInicio As DateTime = DateTime.MinValue
                    Dim dtFin As DateTime = DateTime.MinValue
                    Dim hoje As DateTime = Date.Today

                    Select Case Peticion.RangoFechaHora
                        Case RangoFecha.Dia
                            dtInicio = hoje
                            dtFin = dtInicio.AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Semana
                            Dim primeiroDiaSemana = DayOfWeek.Monday
                            Dim ultimoDiaSemana = DayOfWeek.Sunday

                            Dim priDiaIndex As Integer = hoje.DayOfWeek
                            If priDiaIndex < primeiroDiaSemana Then
                                priDiaIndex += 7
                            End If
                            Dim priDiaDiff As Integer = priDiaIndex - primeiroDiaSemana

                            dtInicio = hoje.AddDays(-priDiaDiff)

                            Dim ultDiaIndex As Integer = hoje.DayOfWeek
                            If ultDiaIndex > ultimoDiaSemana Then
                                ultDiaIndex -= 7
                            End If
                            Dim ultDiaDiff As Integer = ultimoDiaSemana - ultDiaIndex

                            dtFin = hoje.AddDays(ultDiaDiff).AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Mes
                            dtInicio = New DateTime(hoje.Year, hoje.Month, 1)
                            dtFin = dtInicio.AddMonths(1).AddSeconds(-1)
                        Case RangoFecha.Ano
                            dtInicio = New DateTime(hoje.Year, 1, 1)
                            dtFin = dtInicio.AddYears(1).AddSeconds(-1)
                    End Select

                    Dim dtResult As DataTable = AccesoDatos.Dashboard.ValoresProcesados.RetornaSomaValoresProcesadosCliente(dtInicio,
                                                                                                                            dtFin,
                                                                                                                            Peticion.CodigosDelegacion,
                                                                                                                            Peticion.CodigosSector,
                                                                                                                            Peticion.CodigosIsoDivisa,
                                                                                                                            Peticion.Top)
                    .Dados = PreencheRetornaSomaValoresProcesadosCliente(dtResult)
                End With
                Return objRespuesta
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta = New Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Respuesta(ex)
                Return objRespuesta
            End Try
        End Function

        Shared Function RetornaValoresProcesadosPorHora(Peticion As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Peticion) As Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta
            Dim objRespuesta As New Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta()
            Try
                With objRespuesta
                    Dim dtInicio As DateTime = DateTime.MinValue
                    Dim dtFin As DateTime = DateTime.MinValue
                    Dim hoje As DateTime = Date.Today

                    Select Case Peticion.RangoFechaHora
                        Case RangoFecha.Dia
                            dtInicio = hoje
                            dtFin = dtInicio.AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Semana
                            Dim primeiroDiaSemana = DayOfWeek.Monday
                            Dim ultimoDiaSemana = DayOfWeek.Sunday

                            Dim priDiaIndex As Integer = hoje.DayOfWeek
                            If priDiaIndex < primeiroDiaSemana Then
                                priDiaIndex += 7
                            End If
                            Dim priDiaDiff As Integer = priDiaIndex - primeiroDiaSemana

                            dtInicio = hoje.AddDays(-priDiaDiff)

                            Dim ultDiaIndex As Integer = hoje.DayOfWeek
                            If ultDiaIndex > ultimoDiaSemana Then
                                ultDiaIndex -= 7
                            End If
                            Dim ultDiaDiff As Integer = ultimoDiaSemana - ultDiaIndex

                            dtFin = hoje.AddDays(ultDiaDiff).AddDays(1).AddSeconds(-1)
                        Case RangoFecha.Mes
                            dtInicio = New DateTime(hoje.Year, hoje.Month, 1)
                            dtFin = dtInicio.AddMonths(1).AddSeconds(-1)
                        Case RangoFecha.Ano
                            dtInicio = New DateTime(hoje.Year, 1, 1)
                            dtFin = dtInicio.AddYears(1).AddSeconds(-1)
                    End Select

                    Dim dtResult As DataTable = AccesoDatos.Dashboard.ValoresProcesados.RetornaValoresProcesadosPorHora(dtInicio,
                                                                                                                 dtFin,
                                                                                                                 Peticion.CodigosDelegacion,
                                                                                                                 Peticion.CodigosSector,
                                                                                                                 Peticion.Top)
                    .Dados = PreencheRetornaValoresProcesadosPorHora(dtResult)
                End With
                Return objRespuesta
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta = New Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Respuesta(ex)
                Return objRespuesta
            End Try
        End Function
#End Region

#Region "[INSERIR/ATUALIZAR]"

        Shared Sub InsertarActualizar(IdentificadorRemesa As String, CodigoDelegacion As String, FechaHoraFinConteo As DateTime, CodigoUsuario As String, NombreUsuario As String, ApellidoUsuario As String)
            Try

                AccesoDatos.Dashboard.ValoresProcesados.GrabarDatos(IdentificadorRemesa)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Dim direccionLog As String = ConfigurationManager.AppSettings("DASHBOARD_DIRECCION_LOG_INTEGRACION")
                If Not String.IsNullOrEmpty(direccionLog) Then
                    Dim stw As StreamWriter
                    Dim arqLog As New FileInfo(Path.Combine(direccionLog, "DASHBOARD_LOG_INTEGRACION.txt"))

                    If Not arqLog.Exists Then
                        stw = File.AppendText(Path.Combine(direccionLog, "DASHBOARD_LOG_INTEGRACION.txt"))
                    Else
                        'Se maior que 5 MB deleta
                        If arqLog.Length >= 5120000 Then arqLog.Delete()

                        stw = New StreamWriter(Path.Combine(direccionLog, "DASHBOARD_LOG_INTEGRACION.txt"), True)
                    End If

                    Using stw
                        stw.WriteLine("***************")
                        stw.WriteLine(DateTime.Now.ToString())
                        stw.WriteLine("ID Remesa: " + IdentificadorRemesa)
                        stw.WriteLine("")
                        stw.WriteLine(ex.ToString())
                        stw.WriteLine("***************")
                        stw.WriteLine("")
                    End Using

                    arqLog = Nothing

                End If
                Throw ex
            End Try
        End Sub

#End Region

        Private Shared Function PreencheRetornaValoresProcesados(dtRespuesta As DataTable) As List(Of Contractos.Dashboard.Productividad.RetornaValoresProcesados.Dados)
            Dim lstDados As New List(Of Contractos.Dashboard.Productividad.RetornaValoresProcesados.Dados)

            If dtRespuesta IsNot Nothing AndAlso dtRespuesta.Rows.Count > 0 Then

                For Each dr In dtRespuesta.Rows
                    Dim dado As New Contractos.Dashboard.Productividad.RetornaValoresProcesados.Dados

                    If dtRespuesta.Columns.Contains("DES_DELEGACION") AndAlso dr("DES_DELEGACION") IsNot DBNull.Value Then
                        dado.DescricaoDelegacion = Util.AtribuirValorObj(dr("DES_DELEGACION"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_SECTOR") AndAlso dr("DES_SECTOR") IsNot DBNull.Value Then
                        dado.DescricaoSetor = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_CLIENTE") AndAlso dr("DES_CLIENTE") IsNot DBNull.Value Then
                        dado.DescricaoCliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_PRODUCTO") AndAlso dr("DES_PRODUCTO") IsNot DBNull.Value Then
                        dado.DescricaoProducto = Util.AtribuirValorObj(dr("DES_PRODUCTO"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_USUARIO") AndAlso dr("DES_USUARIO") IsNot DBNull.Value Then
                        dado.DescricaoUsuario = Util.AtribuirValorObj(dr("DES_USUARIO"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_DIVISA") AndAlso dr("DES_DIVISA") IsNot DBNull.Value Then
                        dado.DescricaoDivisa = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_DENOMINACION") AndAlso dr("DES_DENOMINACION") IsNot DBNull.Value Then
                        dado.DescricaoDenominacion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("CANTIDAD_MECANIZADO") AndAlso dr("CANTIDAD_MECANIZADO") IsNot DBNull.Value Then
                        dado.NelCantidadMecanizado = Util.AtribuirValorObj(dr("CANTIDAD_MECANIZADO"), GetType(Long))
                    End If

                    If dtRespuesta.Columns.Contains("CANTIDAD_MANUAL") AndAlso dr("CANTIDAD_MANUAL") IsNot DBNull.Value Then
                        dado.NelCantidadManual = Util.AtribuirValorObj(dr("CANTIDAD_MANUAL"), GetType(Long))
                    End If

                    'If dtRespuesta.Columns.Contains("CANTIDAD_MINUTOS_MECANIZADO") AndAlso dr("CANTIDAD_MINUTOS_MECANIZADO") IsNot DBNull.Value Then
                    '    dado.NelCantidadMinutosMecanizado = Util.AtribuirValorObj(dr("CANTIDAD_MINUTOS_MECANIZADO"), GetType(Long))
                    'End If

                    'If dtRespuesta.Columns.Contains("CANTIDAD_MINUTOS_MANUAL") AndAlso dr("CANTIDAD_MINUTOS_MANUAL") IsNot DBNull.Value Then
                    '    dado.NelCantidadMinutosManual = Util.AtribuirValorObj(dr("CANTIDAD_MINUTOS_MANUAL"), GetType(Long))
                    'End If

                    'If dtRespuesta.Columns.Contains("TIPO") AndAlso dr("TIPO") IsNot DBNull.Value Then
                    '    dado.Tipo = Util.AtribuirValorObj(dr("TIPO"), GetType(String))
                    'End If

                    lstDados.Add(dado)
                Next

            End If

            Return lstDados
        End Function

        Private Shared Function PreencheRetornaSomaValoresProcesadosCliente(dtRespuesta As DataTable) As List(Of Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Dados)
            Dim lstDados As New List(Of Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Dados)

            If dtRespuesta IsNot Nothing AndAlso dtRespuesta.Rows.Count > 0 Then

                For Each dr In dtRespuesta.Rows
                    Dim dado As New Contractos.Dashboard.Conteo.RetornaSomaValoresProcesadosCliente.Dados

                    If dtRespuesta.Columns.Contains("COD_CLIENTE") AndAlso dr("COD_CLIENTE") IsNot DBNull.Value Then
                        dado.CodigoCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                    End If
                    If dtRespuesta.Columns.Contains("DES_CLIENTE") AndAlso dr("DES_CLIENTE") IsNot DBNull.Value Then
                        dado.DescricaoCliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("TOTAL") AndAlso dr("TOTAL") IsNot DBNull.Value Then
                        dado.Total = Util.AtribuirValorObj(dr("TOTAL"), GetType(Integer))
                    End If

                    lstDados.Add(dado)
                Next

            End If

            Return lstDados
        End Function

        Private Shared Function PreencheRetornaValoresProcesadosPorHora(dtRespuesta As DataTable) As List(Of Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Dados)
            Dim lstDados As New List(Of Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Dados)

            If dtRespuesta IsNot Nothing AndAlso dtRespuesta.Rows.Count > 0 Then

                For Each dr In dtRespuesta.Rows
                    Dim dado As New Contractos.Dashboard.Productividad.RetornaValoresProcesadosPorHora.Dados

                    If dtRespuesta.Columns.Contains("DES_USUARIO") AndAlso dr("DES_USUARIO") IsNot DBNull.Value Then
                        dado.DescricaoUsuario = Util.AtribuirValorObj(dr("DES_USUARIO"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_DELEGACION") AndAlso dr("DES_DELEGACION") IsNot DBNull.Value Then
                        dado.DescricaoDelegacion = Util.AtribuirValorObj(dr("DES_DELEGACION"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_SECTOR") AndAlso dr("DES_SECTOR") IsNot DBNull.Value Then
                        dado.DescricaoSetor = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_PRODUCTO") AndAlso dr("DES_PRODUCTO") IsNot DBNull.Value Then
                        dado.DescricaoProducto = Util.AtribuirValorObj(dr("DES_PRODUCTO"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("DES_CLIENTE") AndAlso dr("DES_CLIENTE") IsNot DBNull.Value Then
                        dado.DescricaoCliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))
                    End If

                    If dtRespuesta.Columns.Contains("HORAS_TRABAJADAS") AndAlso dr("HORAS_TRABAJADAS") IsNot DBNull.Value Then
                        dado.HorasTrabajadas = Util.AtribuirValorObj(dr("HORAS_TRABAJADAS"), GetType(Double))
                    End If

                    If dtRespuesta.Columns.Contains("BILLETES_MONEDAS_PROCESADAS") AndAlso dr("BILLETES_MONEDAS_PROCESADAS") IsNot DBNull.Value Then
                        dado.BilletesYMonedasProcesadas = Util.AtribuirValorObj(dr("BILLETES_MONEDAS_PROCESADAS"), GetType(Double))
                    End If

                    dado.ProductividadHora = (dado.BilletesYMonedasProcesadas / dado.HorasTrabajadas)

                    lstDados.Add(dado)
                Next

            End If

            Return lstDados
        End Function
    End Class

End Namespace