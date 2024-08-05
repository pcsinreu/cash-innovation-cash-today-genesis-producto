Imports Prosegur.Impressao.Zebra
Imports Prosegur.Genesis.Impresion.REYD.Ticket.Contenedor.Parametros
Imports Prosegur.Framework.Dicionario

Namespace REYD.Ticket.Contenedor.Zebra

    ''' <summary>
    ''' Classe que determina o Layout da Etiqueta de Bulto
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Layout

        Private Const _TamanhoMaxCaracteresLinha As Integer = 34

        ''' <summary>
        ''' Imprime etiquetas por contenedor
        ''' </summary>
        ''' <param name="pObjContenedor"></param>
        ''' <param name="Impressora"></param>
        ''' <remarks></remarks>
        Public Shared Sub Imprimir(pObjContenedor As Parametros.Contenedor, Impressora As String)

            'Cria uma instância do objeto de impressão do documento
            Const LARGURA As Integer = 68
            Const ALTURA As Integer = 34
            Const DISTANCIA As Integer = 34
            Dim tick As New Prosegur.Impressao.Zebra.TicketImpressao(Impressora, LARGURA, ALTURA, DISTANCIA)

            'If (Not pObjContenedor.AceptaPico) Then
            tick.X = 10
            tick.Y = 0
            tick.EscreveTexto(pObjContenedor.SimboloDivisa, FonteZebra.GRANDE)
            'End If
            tick.X = 10
            tick.Y = 25
            tick.EscreveTexto(pObjContenedor.DenominacionContenedor, FonteZebra.GRANDE)
            tick.X = 10
            tick.Y = 60
            Dim textoCliente = VerificaTamMaximo(pObjContenedor.CodCliente)
            tick.EscreveLinha(textoCliente, FonteZebra.PEQUENO)

            tick.X = 120
            tick.Y = 2
            tick.EscreveTexto(pObjContenedor.TotalContenedor.ToString("N2"), FonteZebra.GIGANTE)

            Dim codigoBarras As String = pObjContenedor.DesPrecinto
            tick.DesenhaCodigoBarras(codigoBarras,
                                     New System.Drawing.Point(TicketImpressao.ConverteParaDpi(1), TicketImpressao.ConverteParaDpi(11)),
                                     TipoCodigoBarra.Codigo128,
                                     TicketImpressao.ConverteParaDpi(13), False, True)

            Dim textoPrecinto = VerificaTamMaximo(pObjContenedor.DesPrecinto)
            tick.X = 10
            tick.Y = 200
            tick.EscreveTexto(textoPrecinto, FonteZebra.PEQUENO, Alinhamento.Centro, textoPrecinto.Length)

            tick.X = 10
            tick.Y = 235
            tick.EscreveTexto(pObjContenedor.FechaArmado.ToString("dd/MM/yyyy"), FonteZebra.PEQUENO)

            tick.X = 160
            tick.Y = 235
            tick.EscreveTexto(pObjContenedor.FechaArmado.ToString("HH:mm:ss"), FonteZebra.PEQUENO)

            tick.X = 280
            tick.Y = 235
            tick.EscreveTexto(pObjContenedor.UsuarioResponsavelArmarContenedor, FonteZebra.PEQUENO)

            tick.X = 400
            tick.Y = 235
            tick.EscreveTexto(pObjContenedor.CodPuestoContenedor, FonteZebra.PEQUENO)

            '#If Not Debug Then

            tick.Imprimir(1)


            '#End If

        End Sub


        Private Shared Function VerificaTamMaximo(texto As String) As String

            'Verificando se o campo é maior que o tamanho máximo da linha
            If texto.Length > _TamanhoMaxCaracteresLinha Then
                texto = texto.Substring(0, _TamanhoMaxCaracteresLinha)
            End If

            'Retorna o texto recortado.
            Return texto

        End Function

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