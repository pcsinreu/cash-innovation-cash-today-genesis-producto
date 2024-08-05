Imports Prosegur.Framework.Dicionario.Tradutor

Partial Class DetalleParciales

#Region "[ENUMARAÇÕES]"

    Private Enum eTipoValor
        Efectivo = 1
        MedioPago = 2
        Falsos = 3
        Total = 4
    End Enum

#End Region

#Region "[VARIÁVEIS]"

    Private rowDetalle As DetalleParciales.DetalleRow
    Private rowDeclarado As DetalleParciales.DeclaradoRow
    Private rowObs As DetalleParciales.ObservacionRow
    Private rowDiferencia As DetalleParciales.DiferenciaRow
    Private rowEfectivoContado As DetalleParciales.ContadoRow
    Private rowFalsosContado As DetalleParciales.ContadoRow
    Private rowTotalContado As DetalleParciales.ContadoRow
    Private rowMedioPagoContado As DetalleParciales.ContadoRow
    Private rowEfectivo As DetalleParciales.EfetivoRow
    Private rowIAC As DetalleParciales.IACsRow

#End Region

#Region "[MÉTODOS]"

    Public Sub Popular(Dados As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial))

        For Each parcial In Dados

            ' preenche informações da parcial
            PreencherDetalle(parcial)

            ' preenche declarados da parcial 
            PreencherDeclarado(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.Declarados)

            ' preenche valores contados
            PreencherEfetivos(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.Efectivos)

            ' preenche valores contados
            PreencherDiferencias(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.Efectivos, _
                                 parcial.MediosPago, parcial.Declarados)

            ' preenche valores contados
            PreencherContados(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.Efectivos, _
                              parcial.MediosPago)

            ' preenche observações
            PreencherObservaciones(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.Observaciones)

            ' preenche IACs
            PreencherIACS(parcial.NumeroParcial, parcial.NumeroRemesa, parcial.NumeroPrecinto, parcial.IACs)

        Next

    End Sub

    Private Sub PreencherDetalle(Parcial As ContractoServ.DetalleParciales.GetDetalleParciales.DetalleParcial)

        rowDetalle = Detalle.NewDetalleRow()

        With rowDetalle
            .CodCliente = Parcial.CodigoCliente
            .NombreCliente = Parcial.NomeCliente
            .CodSubCliente = IIf(Parcial.CodigoSubCliente.Equals("0"), String.Empty, Parcial.CodigoSubCliente.ToUpper)
            .NombreSubCliente = IIf(Parcial.NomeSubCliente.Equals(" "), String.Empty, Parcial.NomeSubCliente.ToUpper)
            .FechaProceso = Parcial.FechaProceso
            .FechaTransporte = Parcial.FechaTransporte
            .NumParcial = Parcial.NumeroParcial
            .NumPrecinto = Parcial.NumeroPrecinto
            .NumRemesa = Parcial.NumeroRemesa
            .PuntoServicio = IIf(Parcial.PuntoServicio.Equals("0 -  "), String.Empty, Parcial.PuntoServicio.ToUpper)
            .OidParcial = Parcial.OidParcial
        End With

        Detalle.Rows.Add(rowDetalle)

    End Sub

    Private Sub PreencherDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                   DeclaradoXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.Declarado))

        For Each dec In DeclaradoXParcial

            ' se não existe declarado, adiciona 
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, dec.Divisa, dec.ImporteTotal, dec.TipoDeclarado)

        Next

    End Sub

    Private Function VerificarExistenciaDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                                  Divisa As String, TipoDeclarado As String) As Boolean

        Dim existeDeclarado As Boolean

        existeDeclarado = (From row As DeclaradoRow In Declarado.Rows _
                                      Where row.DesDivisa = Divisa _
                                      AndAlso row.TipoDeclarado = TipoDeclarado _
                                      AndAlso row.NumRemesa = NumRemesa _
                                      AndAlso row.NumPrecinto = NumPrecinto _
                                      AndAlso row.NumParcial = NumParcial _
                                      Select True).ToList().Count > 0

        Return existeDeclarado

    End Function

    Private Sub AdicionarDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                   Divisa As String, ImporteTotal As Decimal, TipoDeclarado As String)

        rowDeclarado = Declarado.NewDeclaradoRow()

        With rowDeclarado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .DesDivisa = Divisa
            .NumImporteTotal = ImporteTotal
            .TipoDeclarado = TipoDeclarado
        End With

        Declarado.Rows.Add(rowDeclarado)

    End Sub

    Private Sub PreencherEfetivos(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                  EfetivosXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.Efectivo))

        For Each efe In EfetivosXParcial

            rowEfectivo = Efetivo.NewEfetivoRow()

            With rowEfectivo
                .NumParcial = NumParcial
                .NumPrecinto = NumPrecinto
                .NumRemesa = NumRemesa
                .Denominacao = efe.Denominacion
                .Divisa = efe.Divisa
                .Unidades = efe.Unidades
                .Tipo = efe.Tipo
                .Calidad = efe.Calidad

                If efe.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE Then
                    .Descripcion = efe.Divisa & " " & Traduzir("gen_csv_lbl_billete") & efe.Denominacion & " " & efe.Calidad
                Else
                    .Descripcion = efe.Divisa & " " & Traduzir("gen_csv_lbl_moneda") & efe.Denominacion & " " & efe.Calidad
                End If

            End With

            Efetivo.Rows.Add(rowEfectivo)

        Next

    End Sub

    Private Sub PreencherDiferencias(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                     EfetivosXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.Efectivo), _
                                     MediosPagoXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.MedioPago), _
                                     DeclaradosXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.Declarado))

        Dim valEfetivos As Decimal
        Dim valMediosPago As Decimal
        Dim valContado As Decimal
        Dim valDeclaradoParcial As Decimal
        Dim valDeclaradoRemesa As Decimal
        Dim valDeclaradoBulto As Decimal
        Dim descDivisa As String = String.Empty

        For Each div In (From e In EfetivosXParcial Select e.Divisa).Distinct

            descDivisa = div

            valEfetivos = (From e In EfetivosXParcial _
                        Where e.Divisa = descDivisa _
                       Select (e.Unidades * e.Denominacion)).Sum()

            valMediosPago = (From mp In MediosPagoXParcial _
                           Where mp.Divisa = descDivisa _
                           Select mp.Valor).Sum()

            valContado = (From mp In MediosPagoXParcial _
                           Where mp.Divisa = descDivisa _
                           Select mp.Valor).Sum()

            valContado = valEfetivos + valMediosPago

            valDeclaradoParcial = (From d In DeclaradosXParcial _
                            Where d.Divisa = descDivisa _
                            AndAlso d.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL _
                            Select d.ImporteTotal).Sum()

            valDeclaradoBulto = (From d In DeclaradosXParcial _
                                 Where d.Divisa = descDivisa _
                                 AndAlso d.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO _
                                 Select d.ImporteTotal).Sum()

            valDeclaradoRemesa = (From d In DeclaradosXParcial _
                                  Where d.Divisa = descDivisa _
                                  AndAlso d.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA _
                                  Select d.ImporteTotal).Sum()

            If valContado > 0 OrElse valDeclaradoParcial > 0 Then

                rowDiferencia = Diferencia.NewDiferenciaRow()

                With rowDiferencia
                    .Divisa = descDivisa
                    .NumParcial = NumParcial
                    .NumPrecinto = NumPrecinto
                    .NumRemesa = NumRemesa
                    .ValContado = valContado
                    .ValDeclarado = valDeclaradoParcial
                    .ValTotal = valContado - valDeclaradoParcial
                    .ValDeclaradoBulto = valDeclaradoBulto
                    .ValDeclaradoRemesa = valDeclaradoRemesa
                End With

                Diferencia.Rows.Add(rowDiferencia)

            End If

        Next

    End Sub

    Private Sub PreencherContados(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                  EfectivosXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.Efectivo), _
                                  MediosPagoXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.MedioPago))

        Dim valEfetivos As Decimal
        Dim valFalsos As Decimal
        Dim valMedioPago As Decimal
        Dim valTotal As Decimal
        Dim descTipoMP As String = String.Empty
        Dim descDivisa As String = String.Empty
        Dim verificaDeclaradoNulo As Boolean

        For Each div In (From e In EfectivosXParcial Select e.Divisa).Distinct

            ' inicializa variáveis
            descDivisa = div
            verificaDeclaradoNulo = False

            ' soma efetivos contados na divisa
            valEfetivos = (From e In EfectivosXParcial _
                                    Where e.Divisa = descDivisa _
                                    Select e.Unidades * e.Denominacion).Sum()

            valTotal = valEfetivos

            ' soma falsos contados da divisa
            'valFalsos = (From e In EfectivosXParcial _
            '              Where e.Falsos > 0 _
            '              AndAlso e.Divisa = descDivisa _
            '              Select e.Falsos * e.Denominacion).Sum()
            'a consulta linq acima retorna valores duplicados para mesmas denominações com qualidades diferentes, foi substituída pelo laço abaixo
            valFalsos = 0D
            Dim list As New List(Of String)
            For Each e In EfectivosXParcial
                If e.Divisa = descDivisa AndAlso Not list.Contains(e.Denominacion) Then
                    valFalsos += e.Falsos * e.Denominacion
                    list.Add(e.Denominacion)
                End If
            Next e

            ' percorre meios de pagamento da divisa
            For Each tipoMP In (From e In MediosPagoXParcial Where e.Divisa = descDivisa _
                                Select e.TipoMedioPago).Distinct()

                descTipoMP = tipoMP

                ' soma meios de pagamento contados da divisa
                valMedioPago = (From e In MediosPagoXParcial _
                                  Where e.Divisa = descDivisa _
                                  AndAlso e.TipoMedioPago = descTipoMP _
                                  Select e.Valor).Sum()

                valTotal += valMedioPago

                ' adiciona meios de pagamento, qdo houver
                If valMedioPago > 0 Then
                    AdicionarMedioPagoContado(NumParcial, NumRemesa, NumPrecinto, div, tipoMP, valMedioPago)
                    verificaDeclaradoNulo = True
                End If

            Next

            ' adiciona efetivos contados, qdo houver
            If valEfetivos > 0 Then
                AdicionarEfectivo(NumParcial, NumRemesa, NumPrecinto, div, valEfetivos)
                verificaDeclaradoNulo = True
            End If

            ' adiciona falsos, qdo houver
            If valFalsos > 0 Then
                AdicionarFalsoContado(NumParcial, NumRemesa, NumPrecinto, div, valFalsos)
                verificaDeclaradoNulo = True
            End If

            If valTotal > 0 Then
                AdicionarTotal(NumParcial, NumRemesa, NumPrecinto, div, valTotal)
            End If

            ' se existe algum valor contado e nao existe declarado na divisa, cria declarado 
            ' igual a zero
            If verificaDeclaradoNulo Then
                VerificarContadoXDeclarado(NumParcial, NumRemesa, NumPrecinto, div)
            End If

        Next

    End Sub

    Private Sub VerificarContadoXDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                           Divisa As String)

        If Not VerificarExistenciaDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL) Then
            ' se não existe, adiciona um declarado igual a zero para a parcial
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL)
        End If

        If Not VerificarExistenciaDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO) Then
            ' se não existe, adiciona um declarado igual a zero para o bulto
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO)
        End If

        If Not VerificarExistenciaDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA) Then
            ' se não existe, adiciona um declarado igual a zero para a remessa
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA)
        End If

    End Sub

    Private Sub AdicionarEfectivo(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                  Divisa As String, Valor As Decimal)

        rowEfectivoContado = Contado.NewContadoRow()

        With rowEfectivoContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa
            .Descricao = Divisa & " " & Traduzir("rpt_007_lbl_efetivo")
            .Tipo = eTipoValor.Efectivo
        End With

        Contado.Rows.Add(rowEfectivoContado)


    End Sub

    Private Sub AdicionarFalsoContado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                      Divisa As String, Valor As Decimal)

        rowFalsosContado = Contado.NewContadoRow()

        With rowFalsosContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa
            .Descricao = Divisa & " " & Traduzir("rpt_007_lbl_falsos")
            .Tipo = eTipoValor.Falsos
        End With

        Contado.Rows.Add(rowFalsosContado)

    End Sub

    Private Sub AdicionarTotal(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                               Divisa As String, Valor As Decimal)

        rowTotalContado = Contado.NewContadoRow()

        With rowTotalContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa
            .Descricao = Divisa & " " & Traduzir("rpt_007_lbl_diferenca_total")
            .Tipo = eTipoValor.Total
        End With

        Contado.Rows.Add(rowTotalContado)

    End Sub

    Private Sub AdicionarMedioPagoContado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                          Divisa As String, DescricaoMP As String, Valor As Decimal)

        rowMedioPagoContado = Contado.NewContadoRow()

        With rowMedioPagoContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa.ToUpper()
            .Descricao = Divisa & " " & DescricaoMP
            .Tipo = eTipoValor.MedioPago
        End With

        Contado.Rows.Add(rowMedioPagoContado)

    End Sub

    Private Sub PreencherObservaciones(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                       ObsXParcial As List(Of String))

        For Each obs In ObsXParcial

            rowObs = Observacion.NewObservacionRow()

            With rowObs
                .NumParcial = NumParcial
                .NumPrecinto = NumPrecinto
                .NumRemesa = NumRemesa
                .DesComentario = obs
            End With

            Observacion.Rows.Add(rowObs)

        Next

    End Sub

    Private Sub PreencherIACS(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                              IACsXParcial As List(Of ContractoServ.DetalleParciales.GetDetalleParciales.IAC))

        For Each info In IACsXParcial

            rowIAC = IACs.NewIACsRow()

            With rowIAC
                .NumParcial = NumParcial
                .NumPrecinto = NumPrecinto
                .NumRemesa = NumRemesa
                .CodTermino = info.CodigoTermino
                .DesTermino = info.Descricao
                .DesValor = info.Valor
            End With

            IACs.Rows.Add(rowIAC)

        Next

    End Sub

#End Region

End Class
