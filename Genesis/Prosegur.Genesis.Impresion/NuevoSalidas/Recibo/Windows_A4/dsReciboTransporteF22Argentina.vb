Imports Prosegur.Framework.Dicionario.Tradutor

Partial Class dsReciboTransporteF22Argentina

    Partial Class InformacionesGeneralesDataTable

        Private Sub InformacionesGeneralesDataTable_ColumnChanging(sender As System.Object, e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.LogotipoColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

    ''' <summary>
    ''' Método para popular as tabelas InformacionesGenerales, Efectivos, EfectivosDetalles e Bultos
    ''' com informações do objeto Remesa
    ''' </summary>
    ''' <param name="objRemesa">Recibo.TransporteF22.Parametros.Remesa</param>
    ''' <history>[jorge.viana]	24/08/2010	Creado</history>
    ''' <remarks></remarks>
    Public Sub PopularTabelas(objRemesa As NuevoSalidas.Recibo.TransporteF22.Parametros.Remesa, LogEnDisco As Boolean, ByRef contemBolsa As Boolean)

        ' mensagem de log
        If LogEnDisco Then
            Impresion.Util.LogMensagemEmDisco("      ReciboTransporteF22 - Inicio PopularTabelas")
        End If

        'verifica se a remessa não está nula
        If objRemesa IsNot Nothing Then
            'Verifica se há copias para gerar linhas de InformacionesGenerales
            If objRemesa.Copias IsNot Nothing Then
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

                        ' mensagem de log
                        If LogEnDisco Then
                            Impresion.Util.LogMensagemEmDisco("            ReciboTransporteF22 - PopularTabelas - Cópia (" & strCopia & ")")
                        End If

                        .CodUsuario = objRemesa.CodUsuario
                        .CodPlanta = objRemesa.CodDelegacion
                        .DesPlanta = objRemesa.DesDelegacion
                        .CodSector = objRemesa.CodSector
                        .DesSector = objRemesa.DesSector
                        .FechaServicio = objRemesa.FechaServicio.ToString(Util.FormatoData)
                        .NelPedidoLegado = objRemesa.NelPedidoLegado
                        .NelParada = objRemesa.NelParada
                        .NelControlLegado = objRemesa.NelControlLegado
                        .CodReciboRemesa = objRemesa.CodReciboRemesa
                        .CodClienteFacturacion = objRemesa.CodClienteFacturacion
                        .DesClienteFacturacion = objRemesa.DesClienteFacturacion
                        .CodSubcliente = objRemesa.CodSubcliente
                        .DesSubcliente = objRemesa.DesSubcliente
                        .DesDireccionEntrega = objRemesa.DesDireccionEntrega
                        .DesLocalidadEntrega = objRemesa.DesLocalidadEntrega
                        .DesConfiguracionPlanta = If(Not String.IsNullOrEmpty(objRemesa.DesConfiguracionPlanta), objRemesa.DesConfiguracionPlanta.Replace("|", vbCrLf), String.Empty)
                        .DesCondicionesTransporte = If(Not String.IsNullOrEmpty(objRemesa.DesCondicionesTransporte), objRemesa.DesCondicionesTransporte.Replace("|", vbCrLf), String.Empty)

                        Dim desContacto As String = String.Empty

                        If Not String.IsNullOrEmpty(objRemesa.DesContacto1) Then
                            desContacto = objRemesa.DesContacto1
                        End If

                        If Not String.IsNullOrEmpty(objRemesa.DesContacto2) Then

                            If desContacto.Length = 0 Then
                                desContacto = objRemesa.DesContacto2
                            Else
                                desContacto &= Environment.NewLine & objRemesa.DesContacto2
                            End If

                        End If

                        If Not String.IsNullOrEmpty(objRemesa.DesContacto3) Then

                            If desContacto.Length = 0 Then
                                desContacto = objRemesa.DesContacto3
                            Else
                                desContacto &= Environment.NewLine & objRemesa.DesContacto3
                            End If

                        End If

                        If Not String.IsNullOrEmpty(objRemesa.DesContacto4) Then

                            If desContacto.Length = 0 Then
                                desContacto = objRemesa.DesContacto4
                            Else
                                desContacto &= Environment.NewLine & objRemesa.DesContacto4
                            End If

                        End If

                        .DesContacto = desContacto

                        .Retirado = String.Format("{0} {1} {2} {3}", Traduzir("rpt_027_saldo_atessorado"), objRemesa.DesClienteSaldo, Traduzir("rpt_027_en"), objRemesa.RazonSocialEmpresaF22)

                        If Not String.IsNullOrEmpty(objRemesa.CodATM) Then
                            .DesClienteDestino = objRemesa.DesClienteDestino & " (" & Traduzir("rpt_027_codigoATM") & " " & objRemesa.CodATM & ")"
                        Else
                            .DesClienteDestino = objRemesa.DesClienteDestino
                        End If

                        .LocalidadRetirado = objRemesa.LocalidadRemitoF22
                        .DirecionRetirado = objRemesa.PlantaConfeccionRemitoF22
                        .Copia = strCopia
                        'chama o método para gerar o código de barra
                        .CodBarraReciboRemesa = Util.GerarCodigoBarra(.CodReciboRemesa)
                        'chama o método para recuperar o logotipo da Prosegur
                        .Logotipo = Util.RecuperarLogoRelatorio()

                    End With
                    ' Adiciona a linha na tabela de InformacionesGenerales
                    Me.InformacionesGenerales.Rows.Add(drInformacionesGenerales)
                Next
            End If

            'Verifica se há efectivos na Remesa
            If objRemesa.Efectivos IsNot Nothing AndAlso objRemesa.Efectivos.Count > 0 Then
                'Percorre os efectivos da remessa
                For i As Integer = 0 To objRemesa.Efectivos.Count - 1
                    'objeto que representa o efectivo referente ao índice do loop
                    Dim objEfectivo As NuevoSalidas.Recibo.TransporteF22.Parametros.Efectivo = objRemesa.Efectivos(i)
                    ' Cria a linha de Efectivos
                    Dim drEfectivos As EfectivosRow = Efectivos.NewEfectivosRow
                    ' Atualiza os efectivos
                    With drEfectivos
                        .NumSecuencia = i + 1
                        .DesDivisa = objEfectivo.DesDivisa
                        .ImporteTotal = objEfectivo.ImporteTotal
                        'chama o método para gerar o valor por extenso
                        .ValorExtenso = Util.EscrevePorExtenso(.ImporteTotal, objEfectivo.CodIsoDivisa)
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
                                         NumValorFacial = det.NumValorFacial, _
                                         DescripcionTipoMedioPago = det.DescripcionTipoMedioPago, _
                                         CantidadModulo = det.CantidadModulo, _
                                         DescripcionModulo = det.DescripcionModulo
                                Into Group Select CodIsoDivisa, DesDivisa, CodDenominacion, NumValorFacial, DescripcionTipoMedioPago, _
                                CantidadModulo, DescripcionModulo, ImporteTotal = Group.Sum(Function(o) o.det.ImporteTotal)
                                Order By NumValorFacial Descending, DescripcionTipoMedioPago Ascending

                'Verifica se foi encontrado EfectivoDetalle
                If fDetalles IsNot Nothing AndAlso fDetalles.Count > 0 Then
                    'variável que irá controlar o número de denominações
                    Dim iDetalle As Int16 = 0
                    'Percorre as denominações agrupadas dos efectivos da remesa
                    For Each objDetalle In fDetalles
                        'verifica a quantidade de denominações para que seja criado no máximo 4 colunas no relatório
                        'Cada coluna do relatório terá no máximo 26 denominações
                        If iDetalle <= 23 Then
                            ' Cria a linha de EfectivosDetalle da primeira coluna
                            Dim drEfectivosDetalle As EfectivosDetalleRow = EfectivosDetalle.NewEfectivosDetalleRow
                            ' Atualiza os efectivos detalle
                            With drEfectivosDetalle
                                .DesDivisa = objDetalle.DesDivisa
                                .NumValorFacial = objDetalle.NumValorFacial
                                .ImporteTotal = objDetalle.ImporteTotal
                                .DescTipoMP = objDetalle.DescripcionTipoMedioPago
                                .CantidadModulo = objDetalle.CantidadModulo
                                .DescModulo = objDetalle.DescripcionModulo
                            End With
                            ' Adiciona a linha na tabela de EfectivosDetalle
                            Me.EfectivosDetalle.Rows.Add(drEfectivosDetalle)
                        ElseIf iDetalle > 23 AndAlso iDetalle <= 48 Then
                            ' Cria a linha de EfectivosDetalle da segunda coluna
                            Dim drEfectivosDetalle As EfectivosDetalle1Row = EfectivosDetalle1.NewEfectivosDetalle1Row
                            ' Atualiza os efectivos detalle
                            With drEfectivosDetalle
                                .DesDivisa = objDetalle.DesDivisa
                                .NumValorFacial = objDetalle.NumValorFacial
                                .ImporteTotal = objDetalle.ImporteTotal
                                .DescTipoMP = objDetalle.DescripcionTipoMedioPago
                                .CantidadModulo = objDetalle.CantidadModulo
                                .DescModulo = objDetalle.DescripcionModulo
                            End With
                            ' Adiciona a linha na tabela de EfectivosDetalle
                            Me.EfectivosDetalle1.Rows.Add(drEfectivosDetalle)
                        End If
                        'incrementa o índice do detalle
                        iDetalle += 1
                    Next
                End If
            End If

            'Verifica se há bultos na Remesa
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                Dim qtdLinhas As Integer = 8
                Dim qtdColunas As Integer = 4
                contemBolsa = objRemesa.Bultos.Where(Function(a) Not String.IsNullOrEmpty(a.CodBolsa)).Count > 0
                objRemesa.Bultos.Sort(New Bulto_Sort())
                'percorre os bultos da remessa
                For i As Integer = 0 To objRemesa.Bultos.Count - 1
                    'objetos que irão armazenar 4 bultos que irão representar uma linha da tabela de Bultos
                    Dim objBulto1 As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto = Nothing
                    Dim objBulto2 As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto = Nothing
                    Dim objBulto3 As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto = Nothing
                    Dim objBulto4 As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto = Nothing
                    'Recupera o primeiro bulto caso exista
                    If i <= objRemesa.Bultos.Count - 1 Then
                        objBulto1 = objRemesa.Bultos(i)
                        i += 1
                    End If
                    'Recupera o segundo bulto caso exista
                    If i <= objRemesa.Bultos.Count - 1 Then
                        objBulto2 = objRemesa.Bultos(i)
                        i += 1
                    End If
                    'Recupera o terceiro bulto caso exista
                    If i <= objRemesa.Bultos.Count - 1 Then
                        objBulto3 = objRemesa.Bultos(i)
                        i += 1
                    End If
                    'Recupera o quarto bulto caso exista
                    If i <= objRemesa.Bultos.Count - 1 Then
                        objBulto4 = objRemesa.Bultos(i)
                    End If
                    ' Cria a linha de Bultos
                    Dim drBultos As BultosRow = Bultos.NewBultosRow
                    ' Atualiza os bultos
                    With drBultos
                        If objBulto1 IsNot Nothing Then .CodPrecintoBulto1 = objBulto1.CodPrecintoBulto
                        If objBulto2 IsNot Nothing Then .CodPrecintoBulto2 = objBulto2.CodPrecintoBulto
                        If objBulto3 IsNot Nothing Then .CodPrecintoBulto3 = objBulto3.CodPrecintoBulto
                        If objBulto4 IsNot Nothing Then .CodPrecintoBulto4 = objBulto4.CodPrecintoBulto

                        .CantidadBultos = objRemesa.CantidadBultosTotal
                        .Observacion = String.Format(Traduzir("rpt_027_lbl_descantidadbolsacajas"), If(objRemesa.RazonSocialEmpresaF22 Is Nothing, String.Empty, objRemesa.RazonSocialEmpresaF22).ToUpper())
                    End With

                    ' Adiciona a linha na tabela de Bultos
                    Me.Bultos.Rows.Add(drBultos)

                Next

                'Adiciona linhas restantes em branco
                For i As Integer = 1 To (qtdLinhas - Me.Bultos.Rows.Count)
                    Me.Bultos.Rows.Add(Bultos.NewBultosRow)
                Next

                'Preenche bolsas
                Dim lstBolsas = objRemesa.Bultos.Where(Function(a) a IsNot Nothing AndAlso Not String.IsNullOrEmpty(a.CodBolsa))
                If lstBolsas IsNot Nothing AndAlso lstBolsas.Count > 0 Then
                    Dim linha As Integer = 0
                    Dim coluna As Integer = 1
                    For i As Integer = 0 To lstBolsas.Count - 1

                        'Preenche a bolxa
                        Me.Bultos.Rows(linha)("CodBolsa" + coluna.ToString()) = lstBolsas(i).CodBolsa

                        'Próxima coluna
                        coluna += 1

                        'Se a proxima coluna for maior que o max. de colunas
                        If coluna >= (qtdColunas + 1) Then
                            'Proxima linha
                            linha += 1
                            'Retorna para a primeira coluna
                            coluna = 1
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

End Class


#Region "[Implementando IComparer para ordenar Bultos]"


Class Bulto_Sort
    Implements IComparer(Of Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto)

    Public Function Compare(x As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto, y As NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto) As Integer Implements System.Collections.Generic.IComparer(Of Impresion.NuevoSalidas.Recibo.TransporteF22.Parametros.Bulto).Compare

        Dim value As String = x.CodPrecintoBulto.CompareTo(y.CodPrecintoBulto)

        If (value = 0) Then
            Return (x.CodPrecintoBulto.CompareTo(y.CodPrecintoBulto))
        Else
            Return (value)
        End If

    End Function
End Class

#End Region
