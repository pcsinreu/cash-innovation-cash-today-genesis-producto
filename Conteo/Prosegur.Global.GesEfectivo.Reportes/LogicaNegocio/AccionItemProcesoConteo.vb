Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionItemProcesoConteo


    ''' <summary>
    ''' Grava os item de proceso no bd
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 11/07/2012 - Criado
    ''' </history>
    Public Function Ejecutar(Peticion As ContractoServ.bcp.GuardarItemProcesoConteo.Peticion) As ContractoServ.bcp.GuardarItemProcesoConteo.Respuesta

        Dim respuesta As New ContractoServ.bcp.GuardarItemProcesoConteo.Respuesta
        Dim transacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Try

            ' valida dados
            ValidarDatos(Peticion)

            Dim listParamToken = Peticion.Parametros.Split("|").ToList()

            Dim parametros As String = Nothing
            Dim especies As List(Of String) = listParamToken(0).Split(";").ToList
            Dim depositos As List(Of String) = listParamToken(1).Split(";").ToList

            For Each especie In especies
                For Each deposito In depositos

                    'Faz as combinações maximo 4
                    parametros = especie & "|" & deposito & "|" & listParamToken(2) & "|" & listParamToken(3)

                    Dim dt As DataTable = AccesoDatos.ItemProceso.BuscarItemProcesoPorParametro(Peticion.CodProceso,
                                                                          Peticion.CodDelegacion,
                                                                          parametros)

                    If dt.Rows Is Nothing OrElse dt.Rows.Count = 0 Then
                        AccesoDatos.ItemProceso.GravarItemProcesoConteo(Peticion.CodProceso, Peticion.FechaCreacion,
                                                                        Peticion.CodigoUsuario, Peticion.CodDelegacion,
                                                                        parametros, transacion)
                    End If

                Next
            Next

            transacion.RealizarTransacao()

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            respuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString

        Finally

            respuesta.MensajeErrorDescriptiva = Util.TratarError(respuesta.MensajeError)

        End Try

        Return respuesta
    End Function

    ''' <summary>
    ''' Faz a validação de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 11/07/2012 - Criado
    ''' </history>
    Private Sub ValidarDatos(Peticion As ContractoServ.bcp.GuardarItemProcesoConteo.Peticion)

        'Valida campos obrigatórios
        Util.ValidarCampoObrigatorio(Peticion.CodProceso, "002_CodProceso", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.Parametros, "002_Parametros", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.FechaCreacion, "002_FechaCreacion", GetType(DateTime), False, True)
        Util.ValidarCampoObrigatorio(Peticion.CodigoUsuario, "002_CodigoUsuario", GetType(String), False, True)
        Util.ValidarCampoObrigatorio(Peticion.CodDelegacion, "002_CodDelegacion", GetType(String), False, True)

        'Especies & "|" & Depositos & "|" & FechaConteoDesde & "|" & FechaConteoHasta
        '0|1;2|07/07/2012 00:00|09/07/2012 23:59

        'Valida toda string de parametros
        Dim listParamToken = Peticion.Parametros.Split("|").ToList()
        Dim campoVazio As Boolean = listParamToken.Exists(Function(f) String.IsNullOrEmpty(f))
        If listParamToken.Count <> 4 OrElse campoVazio Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("002_CampoInvalido"), Traduzir("002_Parametros")))
        End If

        'Valida Tipo depósito e Tipo espécie
        Dim valores As List(Of Short) = listParamToken(0).Split(";").OfType(Of Short).ToList
        Dim valoresincorreto As Boolean = valores.Exists(Function(f) (f <> 0 AndAlso f <> 1))
        valores = listParamToken(1).Split(";").OfType(Of Short).ToList
        Dim valoresincorreto2 As Boolean = valores.Exists(Function(f) (f <> 1 AndAlso f <> 2))

        'Valida campos de data
        Dim data As Date = Nothing
        Dim dataInvalida As Boolean = Not (Date.TryParse(listParamToken(2), data) AndAlso Date.TryParse(listParamToken(3), data))

        If valoresincorreto OrElse valoresincorreto2 OrElse dataInvalida Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("002_CampoInvalido"), Traduzir("002_Parametros")))
        End If

    End Sub

    ''' <summary>
    ''' lê último item de proceso no bd
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 11/07/2012 - Criado
    ''' </history>
    Public Function Ejecutar(Peticion As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Peticion) As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Respuesta
        Dim respuesta As New ContractoServ.bcp.RecuperarPedidosReportadosBCP.Respuesta
        Dim transacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Try

            ' valida dados
            ValidarDatos(Peticion)

            Dim dt As DataTable = AccesoDatos.ItemProceso.RecuperarUltimoItemProceso(Peticion.CodProceso,
                                                                                     Peticion.CodDelegacion,
                                                                                     Peticion.FechaHasta)
            Dim pedidos As New ContractoServ.bcp.RecuperarPedidosReportadosBCP.PedidoColeccion
            If dt IsNot Nothing AndAlso dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim pedido As New ContractoServ.bcp.RecuperarPedidosReportadosBCP.Pedido
                    pedido.CodDelegacion = Util.AtribuirValorObjeto(dr("COD_DELEGACION"), GetType(String))
                    pedido.CodEstado = Util.AtribuirValorObjeto(dr("COD_ESTADO"), GetType(String))
                    pedido.DesError = Util.AtribuirValorObjeto(dr("DES_ERROR"), GetType(String))
                    pedido.FechaCreacion = Util.AtribuirValorObjeto(dr("FYH_CREACION"), GetType(Date))

                    Dim parans As String = Util.AtribuirValorObjeto(dr("OBS_PARAMETROS"), GetType(String))
                    If Not String.IsNullOrEmpty(parans) Then
                        Dim aParans As String() = parans.Split("|")
                        pedido.TipoEspecie = aParans(0)
                        pedido.TipoDeposito = aParans(1)
                        Dim data1 As String = aParans(2)
                        Dim data2 As String = aParans(3)
                        pedido.FechaDesde = Date.Parse(data1)
                        pedido.FechaHasta = Date.Parse(data2)
                    End If
                    pedidos.Add(pedido)
                Next

            End If
            respuesta.Pedidos = pedidos

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            respuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString

        Finally

            respuesta.MensajeErrorDescriptiva = Util.TratarError(respuesta.MensajeError)

        End Try

        Return respuesta
    End Function

    ''' <summary>
    ''' Faz a validação de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 11/07/2012 - Criado
    ''' </history>
    Private Sub ValidarDatos(Peticion As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Peticion)

        'Valida campos obrigatórios
        Util.ValidarCampoObrigatorio(Peticion.CodDelegacion, "002_CodDelegacion", GetType(String), False, True)

    End Sub

End Class
