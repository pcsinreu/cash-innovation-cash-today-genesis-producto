Imports Prosegur.Framework.Dicionario.Tradutor

Partial Class BilletajesSucursais

    ''' <summary>
    ''' Popula os detalhes da divisa recuperados da lista na tabela de detalhes da divisa
    ''' </summary>
    ''' <param name="codigoEstacion">Código da Estacion</param>
    ''' <param name="descricionEstacion">Descrição da Estacion</param>
    ''' <param name="codigoDivisa">Código da Divisa</param>
    ''' <param name="objDivisasDetalhes">Lista de detalhes da divisa</param>
    ''' <remarks></remarks>
    Private Sub PopularDivisaDetalle(codigoEstacion As String, descricionEstacion As String, codigoDivisa As String, objDivisasDetalhes As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalheColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drDivisaDetalhe As DivisaDetalleRow

        ' Para cada detalhe de divisa existente na lista de detalhes da divisa
        For Each divisaDetalhe As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaDetalhe In objDivisasDetalhes

            ' Cria uma nova linha do detalhe da divisa
            drDivisaDetalhe = Me.DivisaDetalle.NewRow

            ' Adiciona os dados na linha
            drDivisaDetalhe.CodigoEstacion = codigoEstacion
            drDivisaDetalhe.CodigoDivisa = codigoDivisa
            drDivisaDetalhe.DescricionDivisa = divisaDetalhe.DescricionDivisa.ToUpper
            drDivisaDetalhe.CodigoTipo = If(divisaDetalhe.CodigoTipo.ToLower.Equals(Constantes.DESCRICAO_EFETIVO.ToLower), _
                                            If(divisaDetalhe.EsBillete, _
                                                Traduzir("lbl_billete").ToUpper, _
                                                Traduzir("lbl_moneda").ToUpper), _
                                               divisaDetalhe.CodigoTipo.ToUpper)
            drDivisaDetalhe.DescricionTipo = If(divisaDetalhe.CodigoTipo.ToLower.Equals(Constantes.DESCRICAO_EFETIVO.ToLower), _
                                                Traduzir("rpt_003_txt_lbl_efectivo").ToUpper, _
                                                divisaDetalhe.DescricionTipo.ToUpper)
            drDivisaDetalhe.UnidadMoeda = divisaDetalhe.UnidadMoeda
            drDivisaDetalhe.Unidad = divisaDetalhe.Unidad
            drDivisaDetalhe.ValorRecontado = divisaDetalhe.ValorRecontado
            drDivisaDetalhe.DescricionMedioPago = divisaDetalhe.DescricionMedioPago
            drDivisaDetalhe.CodigoTransporte = divisaDetalhe.CodigoTransporte
            drDivisaDetalhe.DescricionEstacion = descricionEstacion
            drDivisaDetalhe.OidRemesaOri = divisaDetalhe.OidRemesaOri
            drDivisaDetalhe.CodSubCliente = divisaDetalhe.CodSubCliente
            drDivisaDetalhe.CodDenominacion = divisaDetalhe.CodDenominacion
            drDivisaDetalhe.CodCalidad = If(Not String.IsNullOrEmpty(divisaDetalhe.CodCalidad), Traduzir("rpt_003_txt_lbl_deteriorado"), String.Empty)

            ' Adiciona a linha na tabela de detalhes da divisa
            Me.DivisaDetalle.Rows.Add(drDivisaDetalhe)

        Next

    End Sub

    Private Sub PopularDivisaTotal()

        Dim drDT As DivisaTotalRow

        ' cria uma cópia dos dados de detalhe e altera os códigos de Billete e Moneda para Efectivo
        Dim dtTemp As DivisaDetalleDataTable = DivisaDetalle.Copy()
        For Each drTemp As DivisaDetalleRow In dtTemp
            If drTemp.CodigoTipo.ToLower() <> "codtipo" AndAlso drTemp.CodigoTipo.ToLower() <> "codtipoa" AndAlso drTemp.CodigoTipo.ToLower() <> "codtipob" Then
                drTemp.CodigoTipo = drTemp.DescricionTipo
            End If
            drTemp.AcceptChanges()
        Next

        ' agrupa os dados de médio pago recontados
        Dim dadosMedioPago = (From drDD As DivisaDetalleRow In dtTemp.Rows _
                              Group By drDD.CodigoDivisa, drDD.DescricionDivisa, drDD.CodigoTipo, drDD.DescricionTipo _
                              Into TotalContado = Sum(drDD.ValorRecontado) _
                              Select CodigoDivisa, DescricionDivisa, CodigoTipo, DescricionTipo, TotalContado)

        ' agrupa os dados de divisas declarados
        Dim dadosDeclarados = (From drD As DivisaRow In Divisa.Rows _
                               Group By drD.CodigoDivisa _
                               Into TotalDeclaradoEfectivo = Sum(drD.DeclaradoEfectivo), TotalDeclaradoCheque = Sum(drD.DeclaradoCheque), TotalDeclaradoOtroValor = Sum(drD.DeclaradoOtroValor), TotalDeclaradoTicket = Sum(drD.DeclaradoTicket) _
                               Select CodigoDivisa, TotalDeclaradoEfectivo, TotalDeclaradoCheque, TotalDeclaradoOtroValor, TotalDeclaradoTicket)

        ' de acordo com os médios pagos recontados, insere as informações
        For Each dMedioPago In dadosMedioPago
            Dim dMedioPagoLocal = dMedioPago

            drDT = Me.DivisaTotal.NewRow()

            drDT.DescricionDivisa = dMedioPago.DescricionDivisa
            drDT.DescricionTipo = dMedioPago.DescricionTipo
            drDT.RecontadoTotal = dMedioPago.TotalContado

            ' seleciona o valor (se houver) do declarado para o médio pago
            Dim dDeclarado = (From dTemp In dadosDeclarados _
                              Where dTemp.CodigoDivisa = dMedioPagoLocal.CodigoDivisa).FirstOrDefault()

            If dDeclarado IsNot Nothing Then

                ' configura os valores de acordo com os tipos e configura os declarados
                Select Case dMedioPago.CodigoTipo.ToLower()
                    Case "codtipo"
                        drDT.DeclaradoTotal = dDeclarado.TotalDeclaradoOtroValor
                    Case "codtipoa"
                        drDT.DeclaradoTotal = dDeclarado.TotalDeclaradoTicket
                    Case "codtipob"
                        drDT.DeclaradoTotal = dDeclarado.TotalDeclaradoCheque
                    Case Else
                        drDT.DeclaradoTotal = dDeclarado.TotalDeclaradoEfectivo
                End Select

            End If

            Me.DivisaTotal.Rows.Add(drDT)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela de divisa com os dados recuperados da lista de divisas
    ''' </summary>
    ''' <param name="codigoEstacion">Código da Divisa</param>
    ''' <param name="descricionEstacion">Descrição da Divisa</param>
    ''' <param name="objDivisas">Lista de Divisas</param>
    ''' <remarks></remarks>
    Private Sub PopularDivisa(codigoEstacion As String, descricionEstacion As String, objDivisas As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.DivisaColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drDivisa As DivisaRow

        ' Para cada divisa existente na lista de divisa
        For Each divisa As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Divisa In objDivisas

            ' Cria uma nova linha da divisa
            drDivisa = Me.Divisa.NewRow

            ' Adiciona os dados na linha
            drDivisa.CodigoEstacion = codigoEstacion
            drDivisa.CodigoDivisa = divisa.CodigoDivisa
            drDivisa.DeclaradoEfectivo = divisa.DeclaradoEfectivo
            drDivisa.DeclaradoCheque = divisa.DeclaradoCheque
            drDivisa.DeclaradoTicket = divisa.DeclaradoTicket
            drDivisa.DeclaradoOtroValor = divisa.DeclaradoOtroValor
            drDivisa.DescricionEstacion = descricionEstacion

            ' Popula a tabela de detalhes da divisa
            Me.PopularDivisaDetalle(codigoEstacion, descricionEstacion, divisa.CodigoDivisa, divisa.DividasDetalles)

            ' Adiciona a linha na tabela de divisa
            Me.Divisa.Rows.Add(drDivisa)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela de respaldo com os recuperados da lista de respaldos
    ''' </summary>
    ''' <param name="codigoEstacion">Código da Estacion</param>
    ''' <param name="descricionEstacion">Descrição da Estacion</param>
    ''' <param name="objRespaldos">Lista de Respaldos</param>
    ''' <remarks></remarks>
    Private Sub PopularRespaldo(codigoEstacion As String, descricionEstacion As String, objRespaldos As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.RespaldoColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drRespaldo As RespaldoRow

        ' Para cada divisa existente na lista de respaldos
        For Each respaldo As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.Respaldo In objRespaldos

            ' Cria uma nova linha de respaldo
            drRespaldo = Me.Respaldo.NewRow

            ' Adiciona os dados na linha
            drRespaldo.CodigoEstacion = codigoEstacion
            drRespaldo.NumParcialDeclarado = respaldo.NumParcialDeclarado
            drRespaldo.NumParcialRecontado = respaldo.NumParcialRecontado
            drRespaldo.DescricionEstacion = descricionEstacion

            ' Adiciona a linha na tabela de respaldos
            Me.Respaldo.Rows.Add(drRespaldo)

        Next

    End Sub

    ''' <summary>
    ''' Popula a tabela de bilhetajes por sucursal com os dados recuperados da lista de bilhetajes por sucursal
    ''' </summary>
    ''' <param name="objBilletajesSucursais">Lista de Bilhetages Por sucursal</param>
    ''' <remarks></remarks>
    Public Sub PopularBilletajeSucursal(objBilletajesSucursais As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDFColeccion)

        ' Define a linha que receberá os dados da lista
        Dim drBilletajeSucursal As BilletajeSucursalRow

        ' Para cadas bilhetagem existente na lista de bilhetages por sucursais
        For Each billetajeSucursal As ContractoServ.BilletajeSucursal.GetBilletajesSucursais.BilletajeSucursalPDF In objBilletajesSucursais

            ' Cria uma nova linha de bilhetagem por sucursal
            drBilletajeSucursal = Me.BilletajeSucursal.NewRow

            ' Adiciona os dados na linha
            drBilletajeSucursal.CodigoEstacion = billetajeSucursal.Estacion
            drBilletajeSucursal.DescricionEstacion = billetajeSucursal.DescricionEstacion.ToUpper

            ' Popula a tabela de divisas
            Me.PopularDivisa(billetajeSucursal.Estacion, billetajeSucursal.DescricionEstacion, billetajeSucursal.Divisas)

            ' Popula a tabela de respaldo
            Me.PopularRespaldo(billetajeSucursal.Estacion, billetajeSucursal.DescricionEstacion, billetajeSucursal.Respaldos)

            ' Adiciona a linha na tabela de billetaje sucursal
            Me.BilletajeSucursal.Rows.Add(drBilletajeSucursal)

        Next

        Me.PopularDivisaTotal()

    End Sub

End Class
