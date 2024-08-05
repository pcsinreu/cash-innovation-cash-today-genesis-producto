Imports Prosegur.Framework.Dicionario
Imports System.Globalization


Partial Public Class dsReciboTransporteF22Peru

    Partial Class InformacionesGeneralesDataTable

    End Class

    Partial Class EfectivosDetalleDataTable

    End Class

    Partial Class DenominacionesDataTable

    End Class

    ''' <summary>
    ''' Método para popular as tabelas 
    ''' </summary>
    ''' <param name="objRemesa">Objeto para preencher o DataSet</param>
    ''' <history>[gfraga] 25/08/2010 Creado</history>
    ''' <remarks></remarks>
    Public Sub PopularTabelas(objRemesa As Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa, LogEnDisco As Boolean)

        ' mensagem de log
        If LogEnDisco Then
            Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Inicio PopularTabelas")
        End If

        'verifica se a remessa não está nula
        If objRemesa IsNot Nothing Then

            'dicionario do relatório
            Me.Dicionarios.Rows.Add("rpt_028_num", Tradutor.Traduzir("rpt_028_num"))
            Me.Dicionarios.Rows.Add("rpt_028_detalle_cambio", Tradutor.Traduzir("rpt_028_detalle_cambio"))
            Me.Dicionarios.Rows.Add("rpt_028_recibo_transporte", Tradutor.Traduzir("rpt_028_recibo_transporte"))
            Me.Dicionarios.Rows.Add("rpt_028_tipo_servicio", Tradutor.Traduzir("rpt_028_tipo_servicio"))
            Me.Dicionarios.Rows.Add("rpt_028_preparacion", Tradutor.Traduzir("rpt_028_preparacion"))
            Me.Dicionarios.Rows.Add("rpt_028_por_entregar", Tradutor.Traduzir("rpt_028_por_entregar"))
            Me.Dicionarios.Rows.Add("rpt_028_entrega", Tradutor.Traduzir("rpt_028_entrega"))
            Me.Dicionarios.Rows.Add("rpt_028_fecha", Tradutor.Traduzir("rpt_028_fecha"))
            Me.Dicionarios.Rows.Add("rpt_028_num_recorrido", Tradutor.Traduzir("rpt_028_num_recorrido"))
            Me.Dicionarios.Rows.Add("rpt_028_num_camion", Tradutor.Traduzir("rpt_028_num_camion"))
            Me.Dicionarios.Rows.Add("rpt_028_hora_legada", Tradutor.Traduzir("rpt_028_hora_legada"))
            Me.Dicionarios.Rows.Add("rpt_028_hora_salida", Tradutor.Traduzir("rpt_028_hora_salida"))
            Me.Dicionarios.Rows.Add("rpt_028_num_cuenta", Tradutor.Traduzir("rpt_028_num_cuenta"))
            Me.Dicionarios.Rows.Add("rpt_028_recto_pacas", Tradutor.Traduzir("rpt_028_recto_pacas"))
            Me.Dicionarios.Rows.Add("rpt_028_fajos", Tradutor.Traduzir("rpt_028_fajos"))
            Me.Dicionarios.Rows.Add("rpt_028_cliente", Tradutor.Traduzir("rpt_028_cliente"))
            Me.Dicionarios.Rows.Add("rpt_028_recebido_de", Tradutor.Traduzir("rpt_028_recebido_de"))
            Me.Dicionarios.Rows.Add("rpt_028_para", Tradutor.Traduzir("rpt_028_para"))
            Me.Dicionarios.Rows.Add("rpt_028_direcion", Tradutor.Traduzir("rpt_028_direcion"))
            Me.Dicionarios.Rows.Add("rpt_028_observaciones", Tradutor.Traduzir("rpt_028_observaciones"))
            Me.Dicionarios.Rows.Add("rpt_028_importe", Tradutor.Traduzir("rpt_028_importe"))
            Me.Dicionarios.Rows.Add("rpt_028_total_importe", Tradutor.Traduzir("rpt_028_total_importe"))
            Me.Dicionarios.Rows.Add("rpt_028_precintos", Tradutor.Traduzir("rpt_028_precintos"))
            Me.Dicionarios.Rows.Add("rpt_028_tipo_caja", Tradutor.Traduzir("rpt_028_tipo_caja"))
            Me.Dicionarios.Rows.Add("rpt_028_tipo_bolsa", Tradutor.Traduzir("rpt_028_tipo_bolsa"))
            Me.Dicionarios.Rows.Add("rpt_028_conforme_declarado", Tradutor.Traduzir("rpt_028_conforme_declarado"))
            Me.Dicionarios.Rows.Add("rpt_028_recebi_conforme", Tradutor.Traduzir("rpt_028_recebi_conforme"))
            Me.Dicionarios.Rows.Add("rpt_028_por_prosegur", Tradutor.Traduzir("rpt_028_por_prosegur"))
            Me.Dicionarios.Rows.Add("rpt_028_total", Tradutor.Traduzir("rpt_028_total"))
            Me.Dicionarios.Rows.Add("rpt_028_firma_remitente", Tradutor.Traduzir("rpt_028_firma_remitente"))
            Me.Dicionarios.Rows.Add("rpt_028_firma_receptor", Tradutor.Traduzir("rpt_028_firma_receptor"))
            Me.Dicionarios.Rows.Add("rpt_028_firma", Tradutor.Traduzir("rpt_028_firma"))
            Me.Dicionarios.Rows.Add("rpt_028_texto", Tradutor.Traduzir("rpt_028_texto"))

            'Verifica se há copias para gerar linhas de InformacionesGenerales
            If objRemesa.Copias IsNot Nothing Then
                Dim numCopia As Integer = 1
                'Percorre as copias da Remesa. As informações serão copiadas por Cópia
                For Each strCopia As String In objRemesa.Copias

                    ' mensagem de log
                    If LogEnDisco Then
                        Impresion.Util.LogMensagemEmDisco("         ReciboTransporteF22 - PopularTabelas - Percorre as copias da Remesa. As informações serão copiadas por Cópia")
                    End If

                    ' Cria a linha de InformacionesGenerales
                    Dim drInformacionesGenerales As InformacionesGeneralesRow = InformacionesGenerales.NewInformacionesGeneralesRow
                    ' Atualiza as informações gerais
                    With drInformacionesGenerales
                        .DireccionDelegacion = objRemesa.DesConfiguracionPlanta
                        .PlantaConfeccionRemitoF22 = objRemesa.PlantaConfeccionRemitoF22
                        .TipoServicio = objRemesa.TipoServicio

                        If objRemesa.FechaHoraPreparacion IsNot Nothing Then
                            .FechaPreparacion = objRemesa.FechaHoraPreparacion
                        End If

                        .FechaPorEntregar = objRemesa.FechaServicio
                        .DesCliente = objRemesa.DesClienteFacturacion
                        .CodigoCliente = objRemesa.CodClienteFacturacion
                        .DesRecebidoDe = objRemesa.DesClienteSaldo
                        .CodigoRecebidoDe = objRemesa.CodClienteSaldo
                        .DesPara = String.Format("{0} {1}", objRemesa.DesClienteFacturacion, objRemesa.DesPuntoServicio)
                        .CodigoPara = String.Format("{0} {1}", objRemesa.CodClienteFacturacion, objRemesa.CodPuntoServicio)
                        .Direccion = String.Format("{0} {1}", objRemesa.DesDireccionEntrega, objRemesa.DesLocalidadEntrega)
                        .Observaciones = objRemesa.DescripcionComentario

                        ' mensagem de log
                        If LogEnDisco Then
                            Impresion.Util.LogMensagemEmDisco("            ReciboTransporteF22 - PopularTabelas - Cópia (" & strCopia & ")")
                        End If

                        .Copia = strCopia
                        .NumCopia = numCopia
                        numCopia += 1

                        'chama o método para gerar o código de barra
                        .CodigoBarras = Util.GerarCodigoBarra(objRemesa.CodReciboRemesa)
                        .CodigoReciboRemesa = objRemesa.CodReciboRemesa
                        'chama o método para recuperar o logotipo da Prosegur
                        .LogoTipo = Util.RecuperarLogoRelatorio()

                    End With
                    ' Adiciona a linha na tabela de InformacionesGenerales
                    Me.InformacionesGenerales.Rows.Add(drInformacionesGenerales)

                Next
            End If

            'Verifica se há bultos na Remesa
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each codigoISO In (From bulto In objRemesa.Bultos
                                    Select bulto.CodIsoDivisa).Distinct

                    'Precintos
                    Dim sequencial As Int16 = 1
                    For Each objBulto In objRemesa.Bultos.Where(Function(d) d.CodIsoDivisa = codigoISO AndAlso Not String.IsNullOrWhiteSpace(d.CodPrecintoBulto))
                        Dim drPrecintos As PrecintosRow = Precintos.NewPrecintosRow
                        If Not String.IsNullOrWhiteSpace(objBulto.CodPrecintoBulto) Then
                            With drPrecintos
                                .Codigo = objBulto.CodPrecintoBulto
                                .Sequencial = sequencial
                                .CodigoISO = objBulto.CodIsoDivisa
                            End With

                            ' Adiciona a linha na tabela de Efectivos
                            Me.Precintos.Rows.Add(drPrecintos)
                            sequencial += 1
                        End If
                    Next

                    'Tipo BOLSA
                    sequencial = 1
                    Dim cantidadBolsa As Integer = objRemesa.Bultos.Where(Function(d) d.CodIsoDivisa = codigoISO AndAlso Not String.IsNullOrWhiteSpace(d.CodBolsa)).Count
                    For Each tipoBolsa In (From bulto In objRemesa.Bultos
                                            Where bulto.CodIsoDivisa = codigoISO AndAlso Not String.IsNullOrWhiteSpace(bulto.CodBolsa)
                                    Select bulto.CodBolsa).Distinct

                        Dim drBolsa As BolsasRow = Bolsas.NewBolsasRow
                        drBolsa.Cantidad = cantidadBolsa
                        Dim cantidad As Integer = objRemesa.Bultos.Where(Function(d) d.CodIsoDivisa = codigoISO AndAlso d.CodBolsa = tipoBolsa).Count()
                        drBolsa.Codigo = String.Format("{0}({1})", tipoBolsa, cantidad.ToString("000"))
                        drBolsa.CodigoISO = codigoISO
                        drBolsa.Sequencial = sequencial

                        ' Adiciona a linha na tabela de Efectivos
                        Me.Bolsas.Rows.Add(drBolsa)
                        sequencial += 1
                    Next

                    Dim cantidadCaja As Integer = 0

                    For Each objbulto In objRemesa.Bultos.Where(Function(d) d.CodIsoDivisa = codigoISO AndAlso Not String.IsNullOrWhiteSpace(d.TipoCaja))
                        cantidadCaja += objbulto.Cantidad
                    Next

                    'Tipo CAJA
                    sequencial = 1
                    For Each codCaja In (From bulto In objRemesa.Bultos
                                            Where bulto.CodIsoDivisa = codigoISO AndAlso Not String.IsNullOrWhiteSpace(bulto.TipoCaja)
                                    Select bulto.TipoCaja).Distinct

                        Dim dr As CajasRow = Cajas.NewCajasRow
                        Dim cantidad As Integer = 0
                        For Each objbulto In objRemesa.Bultos.Where(Function(d) d.CodIsoDivisa = codigoISO AndAlso d.TipoCaja = codCaja)
                            cantidad += objbulto.Cantidad
                        Next

                        dr.Cantidad = cantidadCaja
                        dr.Codigo = String.Format("{0}({1})", codCaja, cantidad.ToString("000"))
                        dr.CodigoISO = codigoISO
                        dr.Sequencial = sequencial

                        ' Adiciona a linha na tabela de Efectivos
                        Me.Cajas.Rows.Add(dr)
                        sequencial += 1
                    Next
                Next
            End If

            'Verifica se há efectivos na Remesa
            If objRemesa.Efectivos IsNot Nothing AndAlso objRemesa.Efectivos.Count > 0 Then
                'Percorre os efectivos da remessa
                Dim valorSemCasasDecimais As Decimal
                Dim centavos As String
                For i As Integer = 0 To objRemesa.Efectivos.Count - 1
                    'objeto que representa o efectivo referente ao índice do loop
                    Dim objEfectivo As NuevoSalidas.Recibo.TransporteF22.Parametros.Efectivo = objRemesa.Efectivos(i)
                    ' Cria a linha de Efectivos
                    Dim drEfectivos As EfectivosRow = Efectivos.NewEfectivosRow
                   
                    valorSemCasasDecimais = Math.Truncate(objEfectivo.ImporteTotal)
                    centavos = CType(Math.Round((objEfectivo.ImporteTotal Mod 1) * 100), Integer)

                    ' Atualiza os efectivos
                    With drEfectivos
                        .CodigoISO = objEfectivo.CodIsoDivisa
                        .NumSecuencia = i + 1
                        .DesDivisa = objEfectivo.DesDivisa
                        .ImporteTotal = objEfectivo.ImporteTotal
                        'chama o método para gerar o valor por extenso
                        If valorSemCasasDecimais > 1 Then
                            .ValorExtenso = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Util.EscrevePorExtenso(valorSemCasasDecimais, String.Empty) _
                                                                                            .Replace("[imp_moneda_plural_]", String.Empty) _
                                                                                            .Replace("[imp_centavo_plural_]", String.Empty)) & " y " & centavos & "/100 " & Tradutor.Traduzir("imp_moneda_plural_" & objEfectivo.CodIsoDivisa.ToLower)
                        Else
                            .ValorExtenso = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Util.EscrevePorExtenso(valorSemCasasDecimais, String.Empty) _
                                                                                            .Replace("[imp_moneda_sing_]", String.Empty) _
                                                                                            .Replace("[imp_centavo_sing_]", String.Empty)) & " y " & centavos & "/100 " & Tradutor.Traduzir("imp_moneda_sing_" & objEfectivo.CodIsoDivisa.ToLower)
                        End If

                        'Util.EscrevePorExtenso(.ImporteTotal, objEfectivo.CodIsoDivisa) & " y " & centavos & "/100 "
                    End With

                    ' Adiciona a linha na tabela de Efectivos
                    Me.Efectivos.Rows.Add(drEfectivos)
                Next

                'Recupera todas as denominações agrupadas e com o importe total somado
                Dim fDetalles = From e In objRemesa.Efectivos, det In e.EfectivoDetalles _
                                Where (e.EfectivoDetalles IsNot Nothing AndAlso e.EfectivoDetalles.Count > 0) _
                                Group By CodIsoDivisa = e.CodIsoDivisa, _
                                         DesDivisa = e.DesDivisa, _
                                         CodDenominacion = det.CodDenominacion, _
                                         DesDenominacion = det.DesDenominacion, _
                                         NumValorFacial = det.NumValorFacial, _
                                         DescripcionTipoMedioPago = det.DescripcionTipoMedioPago, _
                                         CantidadModulo = det.CantidadModulo, _
                                         DescripcionModulo = det.DescripcionModulo,
                                         Billete = det.EsBillete
                                Into Group Select CodIsoDivisa, DesDivisa, CodDenominacion, DesDenominacion, NumValorFacial, DescripcionTipoMedioPago, _
                                CantidadModulo, DescripcionModulo, Billete, ImporteTotal = Group.Sum(Function(o) o.det.ImporteTotal)
                                Order By Billete Descending, NumValorFacial Descending

                'Verifica se foi encontrado EfectivoDetalle
                If fDetalles IsNot Nothing AndAlso fDetalles.Count > 0 Then
                    'variável que irá controlar o número de denominações

                    For Each div In (From divisas In fDetalles
                                    Select divisas.CodIsoDivisa).Distinct

                        Dim iDetalle As Int16 = 0
                        'Percorre as denominações agrupadas dos efectivos da remesa
                        For Each objDetalle In fDetalles.Where(Function(d) d.CodIsoDivisa = div)
                            'verifica a quantidade de denominações para que seja criado no máximo 4 colunas no relatório
                            'Cada coluna do relatório terá no máximo 26 denominações

                            'incrementa o índice do detalle
                            iDetalle += 1
                            If iDetalle <= 16 Then
                                ' Cria a linha de EfectivosDetalle da primeira coluna
                                Dim drEfectivosDetalle As EfectivosDetalleRow = EfectivosDetalle.NewEfectivosDetalleRow
                                ' Atualiza os efectivos detalle
                                With drEfectivosDetalle
                                    .Sequencial = iDetalle
                                    .CodigoIso = objDetalle.CodIsoDivisa
                                    .DesDivisa = objDetalle.DesDivisa
                                    .NumValorFacial = objDetalle.NumValorFacial
                                    .DesDenominacion = objDetalle.DesDenominacion
                                    .ImporteTotal = objDetalle.ImporteTotal
                                    .Billete = objDetalle.Billete
                                End With
                                ' Adiciona a linha na tabela de EfectivosDetalle
                                Me.EfectivosDetalle.Rows.Add(drEfectivosDetalle)
                            End If
                        Next

                        'Se não tiver 16 registros, então insere linhas vazias
                        If iDetalle < 16 Then
                            For index = iDetalle To 15
                                Dim drEfectivosDetalle As EfectivosDetalleRow = EfectivosDetalle.NewEfectivosDetalleRow
                                ' Adiciona a linha na tabela de EfectivosDetalle
                                drEfectivosDetalle.CodigoIso = div
                                Me.EfectivosDetalle.Rows.Add(drEfectivosDetalle)
                            Next
                        End If
                    Next
                End If
            End If
        End If

        ' mensagem de log
        If LogEnDisco Then
            Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Fim PopularTabelas")
        End If

    End Sub

    ''' <summary>
    ''' Retorna a descrição do valor somando do importe por extenso
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RetornaDescricaoTotalDivisa(pCodDivisa As String, pImporteTotal As Double) As String

        Dim descricaoPluralDivisa As String = String.Empty
        'Gerando a descrição da divisa no plural
        Select Case pCodDivisa.ToUpper

            Case "DOL"
                descricaoPluralDivisa = Tradutor.Traduzir("rpt_030_dolares")

            Case "REA"
                descricaoPluralDivisa = Tradutor.Traduzir("rpt_030_reales")

            Case "PES"
                descricaoPluralDivisa = Tradutor.Traduzir("rpt_030_pesos")

            Case "EUR"
                descricaoPluralDivisa = Tradutor.Traduzir("rpt_030_euros")

        End Select

        'Gerando a descrição final para o valor total da divisa
        Return Tradutor.Traduzir("rpt_030_son") & " " & _
               descricaoPluralDivisa & ": " & _
               Util.EscrevePorExtenso(Decimal.Parse(pImporteTotal), pCodDivisa)

    End Function

End Class
