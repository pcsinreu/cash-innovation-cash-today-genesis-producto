Imports Prosegur.Framework.Dicionario.Tradutor

Partial Public Class ContadoPuesto

#Region "[ENUMARAÇÕES]"

    Private Enum eTipoValor
        Efectivo = 1
        MedioPago = 2
        Falsos = 3
        Total = 4
    End Enum

#End Region

#Region "[VARIÁVEIS]"

    Private rowDetalle As ContadoPuesto.DetalleRow
    Private rowDeclarado As ContadoPuesto.DeclaradoRow
    Private rowObs As ContadoPuesto.ObservacionRow
    Dim rowEfectivo As EfetivoRow
    Dim rowDiferencia As DiferenciaRow
    Dim rowEfectivoContado As ContadoPuesto.ContadoRow
    Dim rowFalsosContado As ContadoPuesto.ContadoRow
    Dim rowMedioPagoContado As ContadoPuesto.ContadoRow

#End Region

#Region "[MÉTODOS]"

    Public Sub Popular(Dados As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

        For Each parcial In Dados

            ' preenche informações da parcial
            PreencherDetalle(parcial)

            ' preenche declarados da parcial 
            PreencherDeclarado(parcial.NumParcial, parcial.NumRemesa, parcial.NumPrecinto, parcial.Declarados)

            ' preenche valores contados
            PreencherEfetivos(parcial.NumParcial, parcial.NumRemesa, parcial.NumPrecinto, parcial.Efectivos)

            ' preenche valores contados
            PreencherDiferencias(parcial.NumParcial, parcial.NumRemesa, parcial.NumPrecinto, parcial.Efectivos, _
                                 parcial.MediosPago, parcial.Declarados)

            ' preenche valores contados
            PreencherContados(parcial.NumParcial, parcial.NumRemesa, parcial.NumPrecinto, parcial.Efectivos, _
                               parcial.MediosPago)

            ' preenche observações
            PreencherObservaciones(parcial.NumParcial, parcial.NumRemesa, parcial.NumPrecinto, parcial.Observaciones)

        Next

    End Sub

    Private Sub PreencherDetalle(Parcial As ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)

        rowDetalle = Detalle.NewDetalleRow()

        With rowDetalle
            .CodCliente = Parcial.CodCliente
            .NombreCliente = Parcial.NombreCliente
            .CodPuesto = Parcial.CodPuesto
            .CodSubCliente = Parcial.CodSubcliente
            .NombreSubCliente = Parcial.NombreSubcliente
            .Codusuario = Parcial.CodUsuario
            .FechaProceso = Parcial.FechaProceso
            .FechaTransporte = Parcial.FechaTransporte
            .NumParcial = Parcial.NumParcial
            .NumPrecinto = Parcial.NumPrecinto
            .NumRemesa = Parcial.NumRemesa
            .PuntoServicio = Parcial.PuntoServicio
        End With

        Detalle.Rows.Add(rowDetalle)

    End Sub

    Private Sub PreencherDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                   DeclaradoXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.Declarado))

        For Each dec In DeclaradoXParcial
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, dec.DesDivisa, dec.NumImporteTotal, dec.TipoDeclarados)
        Next

    End Sub

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

    Private Sub PreencherDiferencias(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                     EfetivosXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.Efectivo), _
                                     MediosPagoXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.MedioPago), _
                                     DeclaradosXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.Declarado))

        Dim valEfetivos As Decimal
        Dim valMediosPago As Decimal
        Dim valContado As Decimal
        Dim valDeclarado As Decimal
        Dim valDeclaradoRemesa As Decimal
        Dim valDeclaradoBulto As Decimal
        Dim descDivisa As String = String.Empty

        Dim decParcial = (From d In DeclaradosXParcial _
                          Where d.TipoDeclarados = ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL).ToList()

        For Each div In (From e In EfetivosXParcial Select e.Divisa).Distinct

            descDivisa = div

            valEfetivos = (From e In EfetivosXParcial _
                        Where e.Divisa = descDivisa _
                       Select (e.Unidades * e.Denominacion)).Sum()

            valMediosPago = (From mp In MediosPagoXParcial _
                           Where mp.Divisa = descDivisa _
                           Select mp.Valor).Sum()

            valContado = valEfetivos + valMediosPago

            valDeclarado = (From d In decParcial _
                            Where d.DesDivisa = descDivisa _
                            Select d.NumImporteTotal).Sum()

            valDeclaradoBulto = (From d In DeclaradosXParcial _
                                 Where d.DesDivisa = descDivisa _
                                 AndAlso d.TipoDeclarados = ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO _
                                 Select d.NumImporteTotal).Sum()

            valDeclaradoRemesa = (From d In DeclaradosXParcial _
                                  Where d.DesDivisa = descDivisa _
                                  AndAlso d.TipoDeclarados = ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA _
                                  Select d.NumImporteTotal).Sum()

            If valContado > 0 OrElse valDeclarado > 0 Then

                rowDiferencia = Diferencia.NewDiferenciaRow()

                With rowDiferencia
                    .Divisa = descDivisa
                    .DescContado = descDivisa & " " & Traduzir("rpt_008_lbl_diferenca_contado")
                    .DescDeclarado = descDivisa & " " & Traduzir("rpt_008_lbl_diferenca_dec")
                    .DescTotal = descDivisa & " " & Traduzir("rpt_008_lbl_diferenca_total")
                    .NumParcial = NumParcial
                    .NumPrecinto = NumPrecinto
                    .NumRemesa = NumRemesa
                    .ValContado = valContado
                    .ValDeclarado = valDeclarado
                    .ValTotal = valContado - valDeclarado
                    .ValDeclaradoBulto = valDeclaradoBulto
                    .ValDeclaradoRemesa = valDeclaradoRemesa
                End With

                Diferencia.Rows.Add(rowDiferencia)

            End If

        Next

    End Sub

    Private Sub PreencherEfetivos(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                  EfetivosXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.Efectivo))


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

                If efe.Tipo = ContractoServ.Constantes.C_TIPO_EFECTIVO_BILLETE Then
                    .Descricao = efe.Divisa & " " & Traduzir("gen_csv_lbl_billete") & efe.Denominacion
                Else
                    .Descricao = efe.Divisa & " " & Traduzir("gen_csv_lbl_moneda") & efe.Denominacion
                End If

            End With

            Efetivo.Rows.Add(rowEfectivo)

        Next

    End Sub

    Private Sub PreencherContados(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                   EfectivosXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.Efectivo), _
                                   MediosPagoXParcial As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.MedioPago))

        Dim valEfetivos As Decimal
        Dim valFalsos As Decimal
        Dim valMedioPago As Decimal
        Dim valTotal As Decimal
        Dim descTipoMP As String = String.Empty
        Dim descDivisa As String = String.Empty
        Dim verificaDeclaradoNulo As Boolean

        For Each div In (From e In EfectivosXParcial Select e.Divisa).Distinct

            descDivisa = div

            valEfetivos = (From e In EfectivosXParcial _
                        Where e.Divisa = descDivisa _
                       Select e.Unidades * e.Denominacion).Sum()

            valTotal = valEfetivos

            valFalsos = (From e In EfectivosXParcial _
                          Where e.Falsos > 0 _
                          AndAlso e.Divisa = descDivisa _
                          Select e.Falsos * e.Denominacion).Sum()

            For Each tipoMP In (From e In MediosPagoXParcial Where e.Divisa = descDivisa _
                                Select e.TipoMedioPago).Distinct()

                descTipoMP = tipoMP

                valMedioPago = (From e In MediosPagoXParcial _
                                  Where e.Divisa = descDivisa _
                                  AndAlso e.TipoMedioPago = descTipoMP _
                                  Select e.Valor).Sum()

                valTotal += valMedioPago

                If valMedioPago > 0 Then
                    AdicionarMedioPagoContado(NumParcial, NumRemesa, NumPrecinto, div, tipoMP, valMedioPago)
                    verificaDeclaradoNulo = True
                End If

            Next

            If valEfetivos > 0 Then
                AdicionarEfectivo(NumParcial, NumRemesa, NumPrecinto, div, valEfetivos)
                verificaDeclaradoNulo = True
            End If

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

    Private Sub AdicionarEfectivo(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                 Divisa As String, Valor As Decimal)

        rowEfectivoContado = Contado.NewContadoRow()

        With rowEfectivoContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa
            .Descricao = Divisa & " " & Traduzir("rpt_008_lbl_efetivo")
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
            .Descricao = Divisa & " " & Traduzir("rpt_008_lbl_falsos")
            .Tipo = eTipoValor.Falsos
        End With

        Contado.Rows.Add(rowFalsosContado)

    End Sub

    Private Sub AdicionarTotal(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                               Divisa As String, Valor As Decimal)

        rowFalsosContado = Contado.NewContadoRow()

        With rowFalsosContado
            .NumParcial = NumParcial
            .NumPrecinto = NumPrecinto
            .NumRemesa = NumRemesa
            .Valor = Valor
            .Divisa = Divisa
            .Descricao = Divisa & " " & Traduzir("rpt_008_lbl_diferenca_total")
            .Tipo = eTipoValor.Total
        End With

        Contado.Rows.Add(rowFalsosContado)

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

    Private Sub VerificarContadoXDeclarado(NumParcial As String, NumRemesa As String, NumPrecinto As String, _
                                           Divisa As String)

        Dim decParcial As Boolean
        Dim decBulto As Boolean
        Dim decRemesa As Boolean

        decParcial = (From row As DeclaradoRow In Declarado.Rows _
                      Where row.DesDivisa = Divisa _
                      AndAlso row.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL _
                      AndAlso row.NumRemesa = NumRemesa _
                      AndAlso row.NumPrecinto = NumPrecinto _
                      AndAlso row.NumParcial = NumParcial _
                      Select True).ToList().Count > 0

        decBulto = (From row As DeclaradoRow In Declarado.Rows _
                    Where row.DesDivisa = Divisa _
                    AndAlso row.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO _
                    AndAlso row.NumRemesa = NumRemesa _
                    AndAlso row.NumPrecinto = NumPrecinto _
                    AndAlso row.NumParcial = NumParcial _
                    Select True).ToList().Count > 0

        decRemesa = (From row As DeclaradoRow In Declarado.Rows _
                      Where row.DesDivisa = Divisa _
                      AndAlso row.TipoDeclarado = ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA _
                      AndAlso row.NumRemesa = NumRemesa _
                      AndAlso row.NumPrecinto = NumPrecinto _
                      AndAlso row.NumParcial = NumParcial _
                      Select True).ToList().Count > 0

        If Not decParcial Then
            ' se não existe, adiciona um declarado igual a zero para a parcial
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_PARCIAL)
        End If

        If Not decBulto Then
            ' se não existe, adiciona um declarado igual a zero para o bulto
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_BULTO)
        End If

        If Not decRemesa Then
            ' se não existe, adiciona um declarado igual a zero para a remessa
            AdicionarDeclarado(NumParcial, NumRemesa, NumPrecinto, Divisa, 0, ContractoServ.Constantes.C_TIPO_DECLARADO_REMESA)
        End If

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

#End Region

End Class
