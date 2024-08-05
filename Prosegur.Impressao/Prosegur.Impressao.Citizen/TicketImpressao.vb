Imports System.Drawing
Imports System.Text

''' <summary>
''' TicketImpressao
''' </summary>
''' <remarks></remarks>
''' <history>
''' [cbomtempo] 07/11/2008 Criado
''' </history>
Public Class TicketImpressao

    Private Class Opcodes
        Public Const LF As Char = Chr(&HA)
        Public Const CR As Char = Chr(&HD)
        Public Const HT As Char = Chr(&H9)
        Public Const ESC As Char = Chr(&H1B)
        Public Const GS As Char = Chr(&H1D)
    End Class

    Private _CaminhoImpressoara As String
    Private Comandos As New StringBuilder()

    'ESC @
    '[Function] Initializing the printer
    '[Code] <1B>H<40>H
    '[Outline] Clears data stored in the print buffer and brings various settings to the initial state
    '(Default state).
    '[Caution] 
    '• The settings of DIP switches are not read again.
    '• Data inside the internal input buffer is not cleared.
    '• Macro definitions are not cleared.
    '• NV bit image definitions are not cleared.
    '• Data in the user NV memory is not cleared.
    '
    'ESC R n
    '[Function] Selecting the international character set
    '[Code] <1B>H<52>H<n>
    '[Range] 0≤n≤13
    '[Outline] Depending on the value of “n”, one of the following character sets is specified;
    '
    'n Character Set n Character Set
    '
    '0 U.S.A. 
    '1 France 
    '2 Germany 
    '3 U.K. 
    '4 Denmark I 
    '5 Sweden 
    '6 Italy 
    '7 Spain I
    '8 Japan
    '9 Norway
    '10 Denmark II
    '11 Spain II
    '12 Latin America
    '13 Korea
    '
    '[Default] n = 0 (Overseas), n = 8 (Domestic)
    '
    Private Const ComandosImpressoara As String = Opcodes.ESC & Chr(&H40) & Opcodes.ESC & Chr(&H74) & Chr(3)

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="CaminhoImpressora">Caminho/Nome da impressora</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal CaminhoImpressora As String)
        _CaminhoImpressoara = CaminhoImpressora
    End Sub

    ''' <summary>
    ''' Seta o tamanho da fonte utilizada para escrever as linhas.
    ''' </summary>
    ''' <param name="Largura">Largura da fonte a ser utilizada</param>
    ''' <param name="Altura">Altura da fonte a ser utilizada</param>
    ''' <remarks></remarks>
    Public Sub SetaTamanhoFonte(ByVal Largura As TamanhoFonteCitizen, ByVal Altura As TamanhoFonteCitizen)
        'GS ! n - Specifying the character size
        Comandos.Append(Opcodes.GS & Chr(&H21) & Chr((16 * Altura.Codigo) + Largura.Codigo))
    End Sub

    ''' <summary>
    ''' Seta o alinhamento utilizado para escrever as linhas.
    ''' </summary>
    ''' <param name="AlinhamentoTexto">Alinhamento a ser utilizado</param>
    ''' <remarks></remarks>
    Public Sub SetaAlinhamento(ByVal AlinhamentoTexto As Alinhamento)
        'ESC a n - Aligning the characters
        Comandos.Append(Opcodes.ESC & Chr(&H61) & Int(AlinhamentoTexto).ToString())
    End Sub

    ''' <summary>
    ''' Escreve um texto na impressora assumindo como fonte padrão a Média e alinhado a esquerda.
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <remarks></remarks>
    Public Sub EscreveTexto(ByVal Texto As String)
        'ESC a n - Aligning the characters
        Texto = TrataTexto(Texto)
        Comandos.Append(Texto)
    End Sub

    ''' <summary>
    ''' Define a posicao de cada coluna para ser utilizado com o metodo TabularTexto.
    ''' </summary>
    ''' <param name="Posicao">Array de posições</param>
    ''' <remarks></remarks>
    Public Sub DefinirTabulacao(ByVal ParamArray Posicao() As Integer)
        'ESC D [ n ] k NULL - Setting horizontal tab position
        Comandos.Append(Opcodes.ESC & Chr(&H44))
        For Each i As Integer In Posicao
            Comandos.Append(Chr(i))
        Next
        Comandos.Append(Chr(&H0))
    End Sub

    ''' <summary>
    ''' Executa a tabulação do texto.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub TabulaTexto()
        'HT - Horizontal tab
        Comandos.Append(Opcodes.HT)
    End Sub

    ''' <summary>
    ''' Escreve uma linha de texto na impressora alinhado a esquerda assumindo como padrao a fonte Média
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <remarks></remarks>
    Public Sub EscreveLinha(ByVal Texto As String)
        'ESC a n - Aligning the characters
        Texto = TrataTexto(Texto)
        Comandos.Append(Texto & Opcodes.LF)
    End Sub

    ''' <summary>
    ''' Imprime os comandos armazenados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Imprimir()
        Dim comando As String = ComandosImpressoara & Comandos.ToString()
        Prosegur.Impressao.Ticket.Zebra.ImprimirString(_CaminhoImpressoara, comando)
    End Sub

    ''' <summary>
    ''' Desenha um código de barras
    ''' </summary>
    ''' <param name="Codigo">Código a ser impresso</param>
    ''' <remarks></remarks>
    Public Sub DesenhaCodigoBarras(ByVal Codigo As String)
        Comandos.Append(Opcodes.GS & Chr(&H68) & Chr(100))
        Comandos.Append(Opcodes.GS & Chr(&H77) & Chr(3))
        If Codigo.Length Mod 2 Then
            Codigo = "0" & Codigo
        End If
        Comandos.Append(Opcodes.GS & Chr(&H6B) & Chr(73))
        Comandos.Append(Chr((Codigo.Length / 2) + 2) & Chr(&H7B) & Chr(&H43))
        For i As Integer = 0 To Codigo.Length - 1 Step 2
            Comandos.Append(Chr(Convert.ToInt32(Codigo.Substring(i, 2))))
        Next
    End Sub

    ''' <summary>
    ''' Trata o texto para trocar os caracteres não aceitos pela impressora
    ''' </summary>
    ''' <param name="Texto">Texto a ser tratado</param>
    ''' <remarks></remarks>
    Private Function TrataTexto(ByVal Texto As String) As String
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
        Return Texto
    End Function
End Class
