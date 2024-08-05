Imports Prosegur.Framework.Dicionario


Partial Public Class dsReciboMovimentacionFondo

    Private Const _TotalImporteDenominacion As String = " ({0})"

    ''' <summary>
    ''' Método para popular as tabelas 
    ''' </summary>
    ''' <param name="pObjMovimentacionFondo">Objeto para preencher o DataSet</param>
    ''' <history>[gfraga] 25/08/2010 Creado</history>
    ''' <remarks></remarks>
    Public Sub PopularTabelas(pObjMovimentacionFondo As NuevoSalidas.Recibo.EnvioPuesto.Parametros.MovimentacionFondo)

        If pObjMovimentacionFondo IsNot Nothing Then

            'Cadastrando duas vezes para gerar duas cópias no crystal.
            For I As Integer = 0 To 1 Step 1

                'Preenchendo o DataSet com as informações do Objeto parâmetro
                Dim rowMovimentacioFondo As MovimentacionFondoRow = Me.MovimentacionFondo.NewRow()
                With rowMovimentacioFondo
                    .OidMovimentacionFondo = pObjMovimentacionFondo.OidMovimentacionFondo
                    .CodTicket = pObjMovimentacionFondo.CodTicket
                    .FyhMovimentacion = pObjMovimentacionFondo.FyhMovimentacion
                    .CodDelegacion = pObjMovimentacionFondo.CodDelegacion
                    .CodOrigenDinero = pObjMovimentacionFondo.CodOrigenDinero
                    .CodDestinoDinero = pObjMovimentacionFondo.CodDestinoDinero
                    .NecTipoMovimentacion = pObjMovimentacionFondo.NecTipoMovimentacion
                    .DireccionDelegacion = pObjMovimentacionFondo.DireccionDelegacion
                    .LogoTipo = Util.RecuperarLogoRelatorio()
                    .FLOriginal = I

                    'Adicionando a linha na tabela do DataSet
                    MovimentacionFondo.Rows.Add(rowMovimentacioFondo)

                End With

                'Para cada Item de detalhes da Movimentação de Fundo
                For Each objMovimentacionFondoDet As NuevoSalidas.Recibo.EnvioPuesto.Parametros.MovimentacionFondoDet In pObjMovimentacionFondo.MovimentacionFondoDet

                    'Adicionando os valores de detalhes na tabela do DataSet
                    Dim rowMovimentacionFondoDet As MovimentacionFondoDetRow = Me.MovimentacionFondoDet.NewRow()

                    With rowMovimentacionFondoDet
                        .OidMovimentacionFondo = objMovimentacionFondoDet.OidMovimentacionFondo
                        .CodDivisa = objMovimentacionFondoDet.CodDivisa
                        .DesDivisa = objMovimentacionFondoDet.DesDivisa
                        .CodDenominacion = objMovimentacionFondoDet.CodDenominacion
                        .NelCantidad = objMovimentacionFondoDet.NelCantidad
                        .NumImporteDenominacion = objMovimentacionFondoDet.NumImporteDenominacion
                        .FlOriginal = I
                    End With

                    MovimentacionFondoDet.Rows.Add(rowMovimentacionFondoDet)

                Next

                Dim _valor As Integer = I
                'Agrupando os totais por divisas
                Dim valoresDivisas = (From mfd In MovimentacionFondoDet _
                                      Where mfd.FlOriginal = _valor _
                                      Group mfd By mfd.CodDivisa, mfd.DesDivisa Into grp = Group _
                                      Select New With {grp, .ImporteTotal = grp.Sum(Function(mfd) mfd.NumImporteDenominacion)})

                'Adicionando a descrição do valor total do Importe por Divisas
                For Each valor As Object In valoresDivisas

                    Dim rowTotalImporteDivisa As TotalImporteDivisaRow = Me.TotalImporteDivisa.NewRow()
                    rowTotalImporteDivisa.CodDivisa = valor.grp(0).CodDivisa
                    rowTotalImporteDivisa.DesDivisa = valor.grp(0).DesDivisa
                    rowTotalImporteDivisa.TotalImporte = valor.ImporteTotal
                    rowTotalImporteDivisa.DesTotalImporte = Me.RetornaDescricaoTotalDivisa(valor.grp(0).CodDivisa, Double.Parse(valor.ImporteTotal.ToString()))
                    rowTotalImporteDivisa.FlOriginal = I
                    Me.TotalImporteDivisa.Rows.Add(rowTotalImporteDivisa)

                Next

            Next

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
               Util.EscrevePorExtenso(Decimal.Parse(pImporteTotal), pCodDivisa) & _
               String.Format(_TotalImporteDenominacion, pImporteTotal.ToString("N"))

    End Function

End Class
