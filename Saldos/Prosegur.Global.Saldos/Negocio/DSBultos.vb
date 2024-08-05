
Partial Class DSBultos

    Partial Class InventarioBultoClienteImporteDataTable

        ''' <summary>
        ''' Popula a tabela de cliente por Moeda
        ''' </summary>
        ''' <param name="lstInventarioBulto">Lista com os dados do cliente e da moeda</param>
        ''' <remarks></remarks>
        Public Sub Popular(lstInventarioBulto)

            ' Declara o item
            Dim item

            ' Para cada item na lista de inventarios por bultos
            For Each item In lstInventarioBulto
                ' Adiciona uma nova linha na tabela
                Me.AddInventarioBultoClienteImporteRow(item.ClienteOrigen, _
                                                        item.Importe, _
                                                        item.Moneda, _
                                                        item.Simbolo)
            Next

        End Sub

    End Class

    Partial Class InventarioBultoDataTable

        ''' <summary>
        ''' Popula a tabela de clientes por Moeda
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PopularClienteImporte(ByRef dtClienteImporte As InventarioBultoClienteImporteDataTable)

            ' Recupera os clientes por moedas sem duplicação
            Dim lstInventarioBulto = (From ib In Me.Rows _
                                      Where ib.Simbolo IsNot Nothing _
                                      AndAlso ib.Moneda IsNot Nothing _
                                      AndAlso ib.Importe <> 0 _
                                      Select ib.IdDocumento, ib.ClienteOrigen, ib.Simbolo, ib.Moneda, ib.Importe _
                                    ).Distinct

            ' Popula a tabela de cliente por importe
            dtClienteImporte.Popular(lstInventarioBulto)

        End Sub

    End Class

    Partial Class BultoPorPlantaDataTable

    End Class

    Partial Class SeguimientoBultoDataTable


    End Class

    Partial Class BultoPorRecorridoDataTable

        ''' <summary>
        ''' Popula a tabela de Número externos por Moeda
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub PopularNumeroExternoImporte(ByRef dtNumeroExternoImporte As BultoPorRecorridoNumExternoImporteDataTable)

            ' Recupera os número externos por moedas sem duplicação
            Dim lstBultoPorRecorrido = (From br In Me.Rows _
                                     Select br.PlantaDescripcion, br.Recorrido, br.NumeroExterno, br.MonedaDescripcion, br.Importe _
                                    ).Distinct

            ' Popula a tabela de número externo por moeda
            dtNumeroExternoImporte.Popular(lstBultoPorRecorrido)

        End Sub

    End Class

    Partial Class BultoPorRecorridoNumExternoImporteDataTable

        ''' <summary>
        ''' Popula a tabela de cliente por Moeda
        ''' </summary>
        ''' <param name="lstBultoRecorrido">Lista com os dados do número externo e da moeda</param>
        ''' <remarks></remarks>
        Public Sub Popular(lstBultoRecorrido)

            ' Declara o item
            Dim item

            ' Para cada item na lista de bultos por recorrido 
            For Each item In lstBultoRecorrido
                ' Adiciona uma nova linha na tabela
                Me.AddBultoPorRecorridoNumExternoImporteRow(item.NumeroExterno, _
                                                            item.MonedaDescripcion, _
                                                            item.Importe, _
                                                            item.Recorrido, _
                                                            item.PlantaDescripcion)
            Next

        End Sub


    End Class

End Class
