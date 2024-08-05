Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports System.Data
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis.Comunicacion

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe responsável pelo gerenciamento de MAE
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MAE

        Private Const PARAMETRO_CANALES_VALORES_VALIDADOS_MAE As String = "CanalesValoresValidadosMAE"
        Private Const PARAMETRO_CANALES_VALORES_DEPOSITADOS_MAE As String = "CanalesValoresDepositadosMAE"
        Private Const PARAMETRO_CODIGOS_FORMULARIOS_SHIPOUT_MAE As String = "CodigosFormulariosShipOutMAE"
        Private Const PARAMETRO_CODIGOS_TIPOS_SECTORES_MAE As String = "TiposSectoresMAE"
        Private Const PARAMETRO_APLICACION As String = "Genesis"

#Region "Dashboard"

        Public Shared Function RetornaSaldoTodasMAEPorDelegacion(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Respuesta
            Dim objRespuesta = New Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Respuesta

            Try
                Dim canalesValoresValidadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_VALIDADOS_MAE)
                Dim canalesValoresDepositadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_DEPOSITADOS_MAE)

                Dim dt = AccesoDatos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion(Peticion.CodigosDelegacao, canalesValoresValidadosMAE, canalesValoresDepositadosMAE, Peticion.IdentificadoresDivisa, Peticion.CodigosSector)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New Contractos.GenesisSaldos.MAE.RetornaSaldoTodasMAEPorDelegacion.Dados()
                        dados.TipoSaldo = dr("TIPO_SALDO")
                        dados.CodigoIsoDivisa = dr("COD_ISO_DIVISA")
                        dados.DescricaoDivisa = dr("DES_DIVISA")
                        dados.NumImporte = dr("NUM_IMPORTE")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        Public Shared Function RetornaCantidadMAESPorClientes(Peticion As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Peticion) As Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Respuesta
            Dim objRespuesta = New Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Respuesta

            Try
                Dim tiposSectoresMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CODIGOS_TIPOS_SECTORES_MAE)

                Dim dt = AccesoDatos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes(Peticion.CodigosDelegacao, tiposSectoresMAE)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New Contractos.GenesisSaldos.MAE.RetornaCantidadMAESPorClientes.Dados()
                        dados.Cliente = dr("DES_CLIENTE")
                        dados.Cantidad = dr("TOTAL_MAES")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        Public Shared Function RetornaSaldoMAEPorCliente(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Respuesta
            Dim objRespuesta = New Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Respuesta

            Try
                Dim canalesValoresValidadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_VALIDADOS_MAE)
                Dim canalesValoresDepositadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_DEPOSITADOS_MAE)

                Dim dt = AccesoDatos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente(Peticion.CodigosDelegacao, canalesValoresValidadosMAE, canalesValoresDepositadosMAE, Peticion.IdentificadoresDivisa, Peticion.CodigosSector)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Dados)

                    For Each dr As DataRow In dt.Rows
                        Dim dados = New Contractos.GenesisSaldos.MAE.RetornaSaldoMAEPorCliente.Dados()
                        dados.TipoSaldo = dr("TIPO_SALDO")
                        dados.CodigoCliente = dr("CODIGO")
                        dados.NomeCliente = dr("CLIENTE")
                        dados.CodigoIsoDivisa = dr("COD_ISO_DIVISA")
                        dados.NumImporte = dr("NUM_IMPORTE")
                        objRespuesta.Dados.Add(dados)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        Public Shared Function RetornaSaldoClienteMAEDetallado(Peticion As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Peticion) As Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Respuesta
            Dim objRespuesta = New Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.Respuesta

            Try
                Dim canalesValoresValidadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_VALIDADOS_MAE)
                Dim canalesValoresDepositadosMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CANALES_VALORES_DEPOSITADOS_MAE)
                Dim codigosFormulariosShipOutMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CODIGOS_FORMULARIOS_SHIPOUT_MAE)
                Dim tiposSectoresMAE = RecuperarParametroCanalesValoresMAE(Peticion.CodigosDelegacao(0), PARAMETRO_CODIGOS_TIPOS_SECTORES_MAE)

                Dim dt = AccesoDatos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado(Peticion.CodigosDelegacao, tiposSectoresMAE, _
                           canalesValoresValidadosMAE, canalesValoresDepositadosMAE, Peticion.IdentificadoresDivisa, codigosFormulariosShipOutMAE, Peticion.CodigosSectores)

                If (Not IsNothing(dt) AndAlso dt.Rows.Count > 0) Then
                    objRespuesta.Dados = New List(Of Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.DadosCliente)

                    For Each dr As DataRow In dt.Rows
                        Dim dadosCliente = objRespuesta.Dados.Where(Function(c) c.IdentificadorCliente = dr("OID_CLIENTE")).FirstOrDefault()

                        If (dadosCliente Is Nothing) Then
                            dadosCliente = New Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.DadosCliente
                            dadosCliente.IdentificadorCliente = dr("OID_CLIENTE")
                            dadosCliente.CodigoCliente = dr("COD_CLIENTE")
                            dadosCliente.DescricaoCliente = dr("DES_CLIENTE")
                            dadosCliente.ImporteCertificado = 0
                            dadosCliente.ImporteValidado = 0
                            dadosCliente.ImporteDepositado = 0
                            dadosCliente.CantidadMAE = 0
                            dadosCliente.DadosMAE = New List(Of Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.DadosMAE)()
                            objRespuesta.Dados.Add(dadosCliente)
                        End If

                        Dim dadosMAE = New Contractos.GenesisSaldos.MAE.RetornaSaldoClienteMAEDetallado.DadosMAE
                        dadosMAE.IdentificadorCliente = dadosCliente.IdentificadorCliente
                        dadosMAE.IdentificadorSetor = dr("OID_SECTOR")
                        dadosMAE.CodigoSetor = dr("COD_SECTOR")
                        dadosMAE.DescricaoSetor = dr("DES_SECTOR")
                        dadosMAE.ImporteCertificado = dr("IMPORTE_CERTIFICADO")
                        dadosMAE.ImporteValidado = dr("IMPORTE_VALIDADO")
                        dadosMAE.ImporteDepositado = dr("IMPORTE_DEPOSITADO")

                        If (dr("FECHA_ULTIMO_SHIPOUT") IsNot DBNull.Value) Then
                            dadosMAE.FechaUltimoShipout = dr("FECHA_ULTIMO_SHIPOUT")
                        Else
                            dadosMAE.FechaUltimoShipout = "-"
                        End If

                        If (dr("IMPORTE_ULTIMO_SHIPOUT") IsNot DBNull.Value) Then
                            dadosMAE.ImporteUltimoShipout = dr("IMPORTE_ULTIMO_SHIPOUT")
                        End If

                        dadosCliente.ImporteCertificado = dadosCliente.ImporteCertificado + dadosMAE.ImporteCertificado
                        dadosCliente.ImporteValidado = dadosCliente.ImporteValidado + dadosMAE.ImporteValidado
                        dadosCliente.ImporteDepositado = dadosCliente.ImporteDepositado + dadosMAE.ImporteDepositado
                        dadosCliente.CantidadMAE = dadosCliente.CantidadMAE + 1

                        dadosCliente.DadosMAE.Add(dadosMAE)
                    Next
                End If
            Catch ex As ArgumentException
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

        Private Shared Function RecuperarParametroCanalesValoresMAE(codDelegacion As String, codParametro As String) As Object
            Dim Peticion As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
            Peticion.CodigoAplicacion = PARAMETRO_APLICACION
            Peticion.ValidarParametros = False
            Peticion.CodigoDelegacion = codDelegacion
            Peticion.Parametros = New [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion
            Peticion.Parametros.Add(New [Global].GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro())
            Peticion.Parametros(0).CodigoParametro = codParametro

            Dim objNegocio As New [Global].GesEfectivo.IAC.LogicaNegocio.AccionIntegracion
            Dim respuesta = objNegocio.GetParametrosDelegacionPais(Peticion)
            If respuesta IsNot Nothing AndAlso respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, respuesta.MensajeError)
            End If

            Dim valorParametro = respuesta.Parametros _
                .Where(Function(p) Not String.IsNullOrEmpty(p.ValorParametro)) _
                .Select(Function(p) p.ValorParametro) _
                .FirstOrDefault()

            If (valorParametro Is Nothing) Then Return Nothing

            Dim canales = valorParametro.Split(",")
            Return canales.ToList()
        End Function

#End Region

    End Class

End Namespace