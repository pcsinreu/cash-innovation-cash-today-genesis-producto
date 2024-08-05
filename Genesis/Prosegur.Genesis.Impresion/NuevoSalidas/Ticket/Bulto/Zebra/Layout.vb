Imports Prosegur.Impressao.Zebra
Imports Prosegur.Genesis.Impresion.NuevoSalidas.Ticket.Bulto.Parametros
Imports Prosegur.Framework.Dicionario

Namespace NuevoSalidas.Ticket.Bulto.Zebra

    ''' <summary>
    ''' Classe que determina o Layout da Etiqueta de Bulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gfraga] 23/08/2010 Criado
    ''' </history>
    Public Class Layout

        Private Const TamanhoForm As Integer = 800
        Private Const _Hifen As String = "-"
        Private Const _EspacoMin As String = " "
        Private Const _StrDoisPontos As String = "{0}: "
        Private Const _PosicaoBillete As Integer = 453
        Private Const _PosicaoBilleteUnid As Integer = 520
        Private Const _PosicaoMoneda As Integer = 585
        Private Const _PosicaoMonedaUnid As Integer = 650
        Private Const _PosicaoBorda As Integer = 50
        Private Const _TamanhoMaxCaracteresLinha As Integer = 34
        'Para quais itens serão exibidas linhas abaixo.
        Private Shared _IntItensLinhas() As Integer = {3, 4, 5, 6, 8}
        Private Const DIVISORIA_COLUNA_1 As Integer = 56
        Private Const DIVISORIA_COLUNA_2 As Integer = 580

        ''' <summary>
        ''' Tipo da denominação
        ''' </summary>
        ''' <remarks></remarks>
        Private Enum TipoDenominacion
            Billete
            Moneda
        End Enum


        ''' <summary>
        ''' Imprime etiquetas por bulto
        ''' </summary>
        ''' <param name="pObjBulto"></param>
        ''' <param name="Impressora"></param>
        ''' <remarks></remarks>
        Public Shared Sub Imprimir(pObjBulto As Parametros.Bulto, Impressora As String)

            'Cria uma instância do objeto de impressão do documento
            Dim tick As New Prosegur.Impressao.Zebra.TicketImpressao(Impressora, 90, 30, 3)

            'Escrevendo a primeira linha da etiqueta
            tick.X = _PosicaoBillete
            tick.EscreveTexto(Tradutor.Traduzir("imp_col_Billete"), FonteZebra.MINUSCULO)
            tick.X = _PosicaoBilleteUnid
            tick.EscreveTexto(Tradutor.Traduzir("imp_item_unid"), FonteZebra.MINUSCULO)

            tick.X = _PosicaoMoneda
            tick.EscreveTexto(Tradutor.Traduzir("imp_col_Moneda"), FonteZebra.MINUSCULO)
            tick.X = _PosicaoMonedaUnid
            tick.EscreveLinha(Tradutor.Traduzir("imp_item_unid"), FonteZebra.MINUSCULO)

            For I As Integer = 1 To 9 Step 1

                'Escrevendo a linha correspondente ao número passado em perâmetro
                EscreveLinhaIndice(tick, pObjBulto, I)

                'Escrevendo as Notas
                EscreveLinhaValoresDenominaciones(tick, pObjBulto.DenominacionBillete, _
                                                  I, TipoDenominacion.Billete)
                'Escrevendo as Moedas
                EscreveLinhaValoresDenominaciones(tick, pObjBulto.DenominacionMoneda, _
                                                  I, TipoDenominacion.Moneda)
            Next

#If Not Debug Then

            tick.Imprimir(1)

#End If

        End Sub

        Private Shared Sub EscreveLinhaIndice(tick As Prosegur.Impressao.Zebra.TicketImpressao, _
                                              pObjBulto As Parametros.Bulto, _
                                              pIndice As Integer)
            tick.X = _PosicaoBorda

            Select Case pIndice

                Case 1
                    'Escrevendo a primeira linha da etiqueta com os valores das denominações
                    Dim textoCliente = VerificaTamMaximo(pObjBulto.CodCliente & _Hifen & pObjBulto.DesCliente)
                    tick.EscreveTexto(TrataTexto(textoCliente), FonteZebra.PEQUENO)
                    tick.DesenhaLinha(New System.Drawing.Point(TicketImpressao.ConverteParaDpi(DIVISORIA_COLUNA_1), 0), 2, 220, CorLinha.Preto)
                Case 2
                    'Escrevendo a segunda linha da etiqueta com os valores das denominações
                    Dim textoSubCliente = VerificaTamMaximo(pObjBulto.CodSubCliente & _Hifen & pObjBulto.DesSubCliente)
                    tick.EscreveTexto(TrataTexto(textoSubCliente), FonteZebra.PEQUENO)
                Case 3
                    'Escrevendo a terceira linha da etiqueta com os valores das denominações
                    Dim textoPuntoServicio = VerificaTamMaximo(pObjBulto.CodPuntoServicio & _Hifen & pObjBulto.DesPuntoServicio)
                    tick.EscreveTexto(TrataTexto(textoPuntoServicio), FonteZebra.PEQUENO)
                Case 4
                    'Escrevendo a quarta linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(String.Format(_StrDoisPontos, Tradutor.Traduzir("imp_item_ruta")) & pObjBulto.CodRuta & "  ", FonteZebra.MINUSCULO)
                    tick.EscreveTexto(_EspacoMin & Tradutor.Traduzir("imp_item_bulto") & _EspacoMin & pObjBulto.NumBulto & "/" & pObjBulto.TotalBultosRemesa & "  ", FonteZebra.MINUSCULO)
                    tick.EscreveTexto(" Sec. " & pObjBulto.SecNumParada & _EspacoMin, FonteZebra.MINUSCULO)
                Case 5
                    'Escrevendo a quinta linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(String.Format(_StrDoisPontos, Tradutor.Traduzir("imp_item_fecha_entrega")) & _
                                      pObjBulto.FechaEntrega.ToString("dd/MM/yyyy"), FonteZebra.MINUSCULO)
                Case 6
                    'Escrevendo a sexta linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(String.Format(_StrDoisPontos, Tradutor.Traduzir("imp_item_Precinto")) & _
                                                    TrataTexto(pObjBulto.DesPrecinto), FonteZebra.MINUSCULO)
                Case 7
                    'Escrevendo a sétimo linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(Tradutor.Traduzir("imp_item_tot_bulto") & _EspacoMin & pObjBulto.TotalBulto, FonteZebra.MINUSCULO)
                Case 8
                    'Escrevendo a oitava linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(Tradutor.Traduzir("imp_item_tot_remesa") & _EspacoMin & pObjBulto.TotalRemesa, FonteZebra.MINUSCULO)
                Case 9
                    'Escrevendo a última linha da etiqueta com os valores das denominações
                    tick.EscreveTexto(String.Format(_StrDoisPontos, Tradutor.Traduzir("imp_item_cod_ref")) & _
                                                    TrataTexto(pObjBulto.CodRefCliente), FonteZebra.MINUSCULO)
            End Select

        End Sub

        Private Shared Function VerificaTamMaximo(texto As String) As String

            'Verificando se o campo é maior que o tamanho máximo da linha
            If texto.Length > _TamanhoMaxCaracteresLinha Then
                texto = texto.Substring(0, _TamanhoMaxCaracteresLinha)
            End If

            'Retorna o texto recortado.
            Return texto

        End Function

        Private Shared Sub EscreveLinhaValoresDenominaciones(tick As Prosegur.Impressao.Zebra.TicketImpressao, _
                                                      pDenominacionCollection As Impresion.NuevoSalidas.Ticket.Bulto.Parametros.DenominacionColeccion, _
                                                      pNumLinha As Integer, _
                                                      pTipoDenominacion As TipoDenominacion)

            'Posiciona a escrita de acordo com o tipo da denominação
            If pTipoDenominacion = TipoDenominacion.Billete Then
                tick.X = _PosicaoBillete
            Else
                tick.X = _PosicaoMoneda
            End If

            'Se existir item na coleção de denominação, imprimir os valores
            If pDenominacionCollection.Count >= pNumLinha Then

                If pTipoDenominacion = TipoDenominacion.Billete Then

                    tick.EscreveTexto(pDenominacionCollection(pNumLinha - 1).CodDenominacion, FonteZebra.MINUSCULO)

                    tick.X = _PosicaoBilleteUnid
                    tick.EscreveTexto(pDenominacionCollection(pNumLinha - 1).NelCantidad, FonteZebra.MINUSCULO)

                    If (pNumLinha = 1) Then
                        tick.DesenhaLinha(New System.Drawing.Point(DIVISORIA_COLUNA_2, 0), 2, 220, CorLinha.Preto)
                    End If

                Else

                    tick.EscreveTexto(pDenominacionCollection(pNumLinha - 1).CodDenominacion, FonteZebra.MINUSCULO)

                    tick.X = _PosicaoMonedaUnid
                    tick.EscreveTexto(pDenominacionCollection(pNumLinha - 1).NelCantidad, FonteZebra.MINUSCULO)

                    If _IntItensLinhas.Contains(pNumLinha) Then
                        tick.DesenhaLinha(New System.Drawing.Point(tick.PosicaoAtual.X, tick.PosicaoAtual.Y - 6), 472)
                    End If

                End If

            Else

                'Somente para quebrar a linha caso não existir denominaciones para imprimir
                If pTipoDenominacion = TipoDenominacion.Moneda Then
                    tick.EscreveLinha(String.Empty, FonteZebra.MINUSCULO)
                End If

                'Somente quando for moeda, imprimir uma linha abaixo
                If pTipoDenominacion = TipoDenominacion.Moneda Then

                    If _IntItensLinhas.Contains(pNumLinha) Then
                        tick.DesenhaLinha(New System.Drawing.Point(tick.PosicaoAtual.X, tick.PosicaoAtual.Y - 6), TicketImpressao.ConverteParaDpi(DIVISORIA_COLUNA_1))
                    End If

                End If

            End If

        End Sub

        ''' <summary>
        ''' Trata o texto para trocar os caracteres não aceitos pela impressora
        ''' </summary>
        ''' <param name="Texto">Texto a ser tratado</param>
        ''' <remarks></remarks>
        Private Shared Function TrataTexto(Texto As String) As String
            If Not String.IsNullOrEmpty(Texto) Then
                Texto = Texto.Replace("Ç", Chr(&H80))
                Texto = Texto.Replace("ü", Chr(&H81))
                Texto = Texto.Replace("é", Chr(&H82))
                Texto = Texto.Replace("â", Chr(&H83))
                Texto = Texto.Replace("ã", Chr(&H84))
                Texto = Texto.Replace("à", Chr(&H85))
                Texto = Texto.Replace("Á", Chr(&H86))
                Texto = Texto.Replace("ç", Chr(&H87))
                Texto = Texto.Replace("ê", Chr(&H88))
                Texto = Texto.Replace("Ê", Chr(&H89))
                Texto = Texto.Replace("è", Chr(&H8A))
                Texto = Texto.Replace("Í", Chr(&H8B))
                Texto = Texto.Replace("Ô", Chr(&H8C))
                Texto = Texto.Replace("Ì", Chr(&H8D))
                Texto = Texto.Replace("Ã", Chr(&H8E))
                Texto = Texto.Replace("Â", Chr(&H8F))
                Texto = Texto.Replace("É", Chr(&H90))
                Texto = Texto.Replace("À", Chr(&H91))
                Texto = Texto.Replace("È", Chr(&H92))
                Texto = Texto.Replace("ô", Chr(&H93))
                Texto = Texto.Replace("õ", Chr(&H94))
                Texto = Texto.Replace("ò", Chr(&H95))
                Texto = Texto.Replace("Ú", Chr(&H96))
                Texto = Texto.Replace("ù", Chr(&H97))
                Texto = Texto.Replace("Ì", Chr(&H98))
                Texto = Texto.Replace("Õ", Chr(&H99))
                Texto = Texto.Replace("Ü", Chr(&H9A))
                Texto = Texto.Replace("Ù", Chr(&H9D))
                Texto = Texto.Replace("Ó", Chr(&H9F))
                Texto = Texto.Replace("á", Chr(&HA0))
                Texto = Texto.Replace("í", Chr(&HA1))
                Texto = Texto.Replace("ó", Chr(&HA2))
                Texto = Texto.Replace("ú", Chr(&HA3))
                Texto = Texto.Replace("ñ", Chr(&HA4))
                Texto = Texto.Replace("Ñ", Chr(&HA5))
                Texto = Texto.Replace("ª", Chr(&HA6))
                Texto = Texto.Replace("º", Chr(&HA7))
                Texto = Texto.Replace("¿", Chr(&HA8))
                Texto = Texto.Replace("Ò", Chr(&HA9))
            End If
            Return Texto
        End Function

    End Class

End Namespace