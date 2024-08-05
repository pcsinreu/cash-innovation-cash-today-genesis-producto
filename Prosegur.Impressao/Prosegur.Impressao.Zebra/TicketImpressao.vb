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
    Public Const ConversaoMilimetroPolegada As Double = 0.0393700787
    Public Const DPI As Integer = 203
    Private _AlturaForm As Integer = 800
    Private _LarguraForm As Integer = 50
    Private _EspacoEntreEtiquetas As Integer = 24
    Private _CaminhoImpressoara As String
    Private _PontoDeReferencia As Point = New Point(0, 0)
    Private Comandos As New StringBuilder()
    Private MaiorFonteLinha As FonteZebra = FonteZebra.MINUSCULO

    'N Command - Clear Image Buffer
    'O Command - Hardware Options - D = Enable Direct Thermal Mode, use this option when using direct thermal media in a thermal transfer printer.
    'S Command - Speed Select - 3 = 2.5 ips (63 mm/s)
    'D Command - Density - Aceita 0-15 - Padrao 10
    'Z Command - Print Direction - T = Printing from top of image buffer.
    'R Command - Set Reference Point
    'P Command - Print - Use this command to print the contents of the image buffer.
    Private _ComandosImpressora As String = "N{0}OD{0}q800{0}Q50,0{0}S3{0}D8{0}ZT{0}R{1},{2}{0}{3}P{4}{0}"

    Private _EspacamentoLinhaPadrao As Integer = 10
    Private _PosicaoAtual As Point = New Point(0, 0)

    Private _EncodingType As Encoding = Nothing

    Private _FonteZebraGeral As FonteZebra
    ''' <summary>
    ''' Configura uma fonte geral para ignorar a fonte informada e imprimir tudo usando a mesma FonteZebra
    ''' </summary>
    ''' <value>FonteZebra</value>
    Public Property FonteZebraGeral As FonteZebra
        Get
            Return _FonteZebraGeral
        End Get
        Set(value As FonteZebra)
            _FonteZebraGeral = value
        End Set
    End Property

    Public Property EspacamentoLinhaPadrao() As Integer
        Get
            Return _EspacamentoLinhaPadrao
        End Get
        Set(ByVal value As Integer)
            _EspacamentoLinhaPadrao = value
        End Set
    End Property

    Public Property PosicaoAtual() As Point
        Get
            Return _PosicaoAtual
        End Get
        Set(ByVal value As Point)
            _PosicaoAtual = value
        End Set
    End Property

    Public Property X() As Integer
        Get
            Return _PosicaoAtual.X
        End Get
        Set(ByVal value As Integer)
            _PosicaoAtual.X = value
        End Set
    End Property

    Public Property Y() As Integer
        Get
            Return _PosicaoAtual.Y
        End Get
        Set(ByVal value As Integer)
            _PosicaoAtual.Y = value
        End Set
    End Property

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="CaminhoImpressora">Caminho/Nome da impressora</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal CaminhoImpressora As String)
        _CaminhoImpressoara = CaminhoImpressora
    End Sub

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="CaminhoImpressora">Caminho/Nome da impressora</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal CaminhoImpressora As String, ByVal LarguraEtiqueta As Int32, ByVal AlturaEtiqueta As Int32, ByVal EspacoEntreEtiquetas As Int32)
        'Para evitar que versões anteriores parem de funcionar, só uso este formato quando forem especificadas as medidas da etiqueta
        _ComandosImpressora = "OD{0}N{0}q{5}{0}Q{6},{7}+0{0}S3{0}D8{0}ZT{0}{0}{3}P{4}{0}"

        _CaminhoImpressoara = CaminhoImpressora

        _AlturaForm = ConverteParaDpi(AlturaEtiqueta)
        _LarguraForm = ConverteParaDpi(LarguraEtiqueta)
        _EspacoEntreEtiquetas = ConverteParaDpi(EspacoEntreEtiquetas)
    End Sub

    ''' <summary>
    ''' Constructor indicando CharSet. Info de parametros: http://www.servopack.de/support/zebra/EPL2_Manual.pdf
    ''' </summary>
    ''' <param name="rutaImpresora"> nombre o ruta de impresora </param>
    ''' <param name="LargoEtiqueta"> largo de etiqueta </param>
    ''' <param name="AltoEtiqueta"> alto de etiqueta </param>
    ''' <param name="EspacioEntreEtiquetas"> espacio entre etiquetas </param>
    ''' <param name="p1DataBits"> Bits de datos; posibles valores: 7 o 8 </param>
    ''' <param name="p2CodePage"> CodePage: 0-13 o A-F </param>
    ''' <param name="p3Country"> Country Code: 001-032, etc</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal rutaImpresora As String, ByVal LargoEtiqueta As Int32, ByVal AltoEtiqueta As Int32, ByVal EspacioEntreEtiquetas As Int32, ByVal p1DataBits As String, ByVal p2CodePage As String, ByVal p3Country As String)
        'Para evitar que versões anteriores parem de funcionar, só uso este formato quando forem especificadas as medidas da etiqueta
        _ComandosImpressora = "OD{0}N{0}I" & p1DataBits & "," & p2CodePage & "," & p3Country & "{0}q{5}{0}Q{6},{7}+0{0}S3{0}D8{0}ZT{0}{0}{3}P{4}{0}"

        _CaminhoImpressoara = rutaImpresora

        _AlturaForm = ConverteParaDpi(AltoEtiqueta)
        _LarguraForm = ConverteParaDpi(LargoEtiqueta)
        _EspacoEntreEtiquetas = ConverteParaDpi(EspacioEntreEtiquetas)
    End Sub

    ''' <summary>
    ''' Escreve um texto na impressora assumindo como fonte padrão a Média e alinhado a esquerda.
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <remarks></remarks>
    Public Sub EscreveTexto(ByVal Texto As String)
        EscreveTexto(Texto, FonteZebra.MEDIO)
    End Sub

    ''' <summary>
    ''' Escreve um texto na impressora alinhado a esquerda
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <param name="Alinhamento">Alinhamento do texto</param>
    ''' <param name="TamanhoMaximo">Tamanho máximo de caracteres para o alinhamento a direita.</param>
    ''' <remarks></remarks>
    Public Sub EscreveTexto(ByVal Texto As String, ByVal Fonte As FonteZebra, ByVal Alinhamento As Alinhamento, ByVal TamanhoMaximo As Integer)
        Select Case Alinhamento
            Case Zebra.Alinhamento.Esquerda
                'Nao faz nada
            Case Zebra.Alinhamento.Centro
                _PosicaoAtual.X = (_LarguraForm / 2) - (Fonte.Largura * (Texto.Length / 2))
            Case Zebra.Alinhamento.Direita
                Texto = Texto.PadLeft(TamanhoMaximo)
        End Select
        EscreveTexto(Texto, Fonte)
    End Sub

    ''' <summary>
    ''' Escreve uma linha de texto na impressora
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <param name="Alinhamento">Alinhamento do texto</param>
    ''' <param name="TamanhoMaximo">Tamanho máximo de caracteres para o alinhamento a direita.</param>
    ''' <remarks></remarks>
    Public Sub EscreveLinha(ByVal Texto As String, ByVal Fonte As FonteZebra, ByVal Alinhamento As Alinhamento, ByVal TamanhoMaximo As Integer)
        EscreveTexto(Texto, Fonte, Alinhamento, TamanhoMaximo)
        _PosicaoAtual.X = 0
        _PosicaoAtual.Y += MaiorFonteLinha.Altura + EspacamentoLinhaPadrao
        MaiorFonteLinha = FonteZebra.MINUSCULO
    End Sub

    ''' <summary>
    ''' Escreve um texto na impressora alinhado a esquerda
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <remarks></remarks>
    Public Sub EscreveTexto(ByVal Texto As String, ByVal Fonte As FonteZebra, Optional ByVal RotacaoFonte As Integer = -1, Optional ByVal DirecaoFonte As String = "N")
        'A Command - ASCII Text
        '        Prints an ASCII text string.
        '        Syntax - Ap1, p2, p3, p4, p5, p6, p7, "DATA"
        'Parameters p1 = Horizontal start position (X) in dots.
        '           p2 = Vertical start position (Y) in dots.
        '           p3 = Rotation
        '               Value       Description
        '               0           No rotation
        '               1           90 degrees
        '               2           180 degrees
        '               3           270 degrees
        '           p4 = Font selection
        '               Fonts 1 - 5 are fixed pitch.
        '                     8 Simplified Chinese.
        '               A - Z Reserved for Soft Fonts
        '           p5 = Horizontal multiplier, expands the text horizontally. 
        '               Values: 1, 2, 3, 4, 5, 6, & 8.
        '           p6 = Vertical multiplier, expands the text vertically.
        '               Values: 1, 2, 3, 4, 5, 6, 7, 8, & 9.
        '           p7 = N for normal or R for reverse image
        '           "DATA" = Represents a fixed data field.
        Texto = TrataTexto(Texto)

        If FonteZebraGeral IsNot Nothing Then
            Fonte = FonteZebraGeral
        End If

        If Fonte.Codigo = FonteZebra.GIGANTE.Codigo Then
            Texto = Texto.ToUpper()
        End If
        If Fonte.Codigo = FonteZebra.CHINES.Codigo Or Fonte.Codigo = FonteZebra.CHINES_TRAD.Codigo Then
            'Codepage 54936 = Chinês Simplificado (GB18030)
            _EncodingType = System.Text.Encoding.GetEncoding(54936)
        End If

        If RotacaoFonte = -1 Then
            RotacaoFonte = 0
            ''Testamos e a forma da rotação da fonte é o mesmo que em Latin.
            ''Embora a documentação pede para caracteres asiáticos ser 4.
            '    RotacaoFonte = If(Fonte.Equals(FonteZebra.CHINES), 4, 0)
        End If

        Comandos.AppendLine(String.Format("A{0},{1},{2},{3},1,1,{4},""{5}""", _PosicaoAtual.X, _PosicaoAtual.Y, RotacaoFonte, Fonte, DirecaoFonte, Texto))
        '_PosicaoAtual.X += (Texto.Length + 1) * Fonte.Largura
        _PosicaoAtual.X += Texto.Length * Fonte.Largura
        If MaiorFonteLinha.Codigo < Fonte.Codigo Then
            MaiorFonteLinha = Fonte
        End If
    End Sub

    ''' <summary>
    ''' Escreve uma linha de texto na impressora alinhado a esquerda assumindo como padrao a fonte Média
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <remarks></remarks>
    Public Sub EscreveLinha(ByVal Texto As String)
        EscreveLinha(Texto, FonteZebra.MEDIO)
    End Sub

    ''' <summary>
    ''' Escreve uma linha de texto na impressora a esquerda
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <remarks></remarks>
    Public Sub EscreveLinha(ByVal Texto As String, ByVal Fonte As FonteZebra)
        EscreveTexto(Texto, Fonte)
        _PosicaoAtual.X = 0
        _PosicaoAtual.Y += MaiorFonteLinha.Altura + EspacamentoLinhaPadrao
        MaiorFonteLinha = FonteZebra.MINUSCULO
    End Sub

    ''' <summary>
    ''' Escreve uma linha de texto na impressora a esquerda
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <remarks></remarks>
    Public Sub EscreveLinha(ByVal Texto As String, ByVal Fonte As FonteZebra, ByVal RotacaoFonte As Integer, ByVal DirecaoFonte As String)
        EscreveTexto(Texto, Fonte, RotacaoFonte, DirecaoFonte)
        _PosicaoAtual.X = 0
        _PosicaoAtual.Y += MaiorFonteLinha.Altura + EspacamentoLinhaPadrao
        MaiorFonteLinha = FonteZebra.MINUSCULO
    End Sub

    ''' <summary>
    ''' Imprime os comandos armazenados
    ''' </summary>
    ''' <param name="Copias">Quantidade de cópias</param>
    ''' <remarks></remarks>
    Public Function Imprimir(ByVal Copias As Integer, Optional ByVal EncodingType As Encoding = Nothing) As String
        Dim comando As String = String.Format(_ComandosImpressora, vbCrLf, _PontoDeReferencia.X, _PontoDeReferencia.Y, Comandos.ToString(), Copias.ToString(), _LarguraForm, _AlturaForm, _EspacoEntreEtiquetas)

        If EncodingType Is Nothing Then
            If _EncodingType Is Nothing Then
                Prosegur.Impressao.Ticket.Zebra.ImprimirString(_CaminhoImpressoara, comando)
            Else
                Prosegur.Impressao.Ticket.Zebra.ImprimirString(_CaminhoImpressoara, comando, _EncodingType)
            End If
        Else
            Prosegur.Impressao.Ticket.Zebra.ImprimirString(_CaminhoImpressoara, comando, EncodingType)
        End If

        'Prosegur.Impressao.Ticket.Zebra.PrintDirect("\\127.0.0.1\" & _CaminhoImpressoara, comando, EncodingType)
        'Return comando

        'Dim fileCommand As String = Environment.CurrentDirectory & "\" & Guid.NewGuid().ToString("N") & ".txt"
        'System.IO.File.WriteAllText(fileCommand, comando, EncodingType)
        'System.IO.File.Copy(fileCommand, "\\127.0.0.1\" & _CaminhoImpressoara)
        'System.IO.File.Delete(fileCommand)

        Return comando
    End Function

    ''' <summary>
    ''' Desenha uma linha horizontal ou vertical na tela.
    ''' </summary>
    ''' <param name="PontoInicial">Ponto inicial da linha</param>
    ''' <param name="ComprimentoHorizontal">Comprimento Horizontal</param>
    ''' <param name="ComprimentoVertical">Comprimento Vertical</param>
    ''' <param name="Cor">Cor da linha</param>
    ''' <remarks></remarks>
    Public Sub DesenhaLinha(ByVal PontoInicial As Point, Optional ByVal ComprimentoHorizontal As Integer = 2, Optional ByVal ComprimentoVertical As Integer = 2, Optional ByVal Cor As CorLinha = CorLinha.Preto)
        'LO Command - Line Draw Black
        '       Use this command to draw black lines, overwriting previous information.
        '       Syntax - LOp1, p2, p3, p4
        'Parameters p1 = Horizontal start position (X) in dots.
        '           p2 = Vertical start position (Y) in dots.
        '           p3 = Horizontal length in dots.
        '           p4 = Vertical length in dots.

        'LW Command - Line Draw White
        '       Use this command to draw white lines, effectively erasing previous information.
        '       Syntax - LWp1,p2,p3,p4
        'Parameters p1 = Horizontal start position (X) in dots.
        '           p2 = Vertical start position (Y) in dots.
        '           p3 = Horizontal length in dots.
        '           p4 = Vertical length in dots.
        If Cor = CorLinha.Preto Then
            Comandos.AppendLine(String.Format("LO{0},{1},{2},{3}", PontoInicial.X, PontoInicial.Y, ComprimentoHorizontal, ComprimentoVertical))
        Else
            Comandos.AppendLine(String.Format("LW{0},{1},{2},{3}", PontoInicial.X, PontoInicial.Y, ComprimentoHorizontal, ComprimentoVertical))
        End If
    End Sub

    ''' <summary>
    ''' Desenha um código de barras
    ''' </summary>
    ''' <param name="Codigo">Código a ser impresso</param>
    ''' <param name="Posicao">Posição</param>
    ''' <param name="Tipo">Tipo de barras</param>
    ''' <param name="Altura">Altura das barras</param>
    ''' <param name="ExibirCodigo">Exibe ou não o codigo abaixo das barras</param>
    ''' <remarks></remarks>
    Public Sub DesenhaCodigoBarras(ByVal Codigo As String, ByVal Posicao As Point, ByVal Tipo As TipoCodigoBarra, Optional ByVal Altura As Integer = 100, Optional ByVal ExibirCodigo As Boolean = False, Optional ByVal ReduzirCodigo As Boolean = False)
        'B Command - Bar Code
        '       Use this command to print standard bar codes.
        '       Syntax - Bp1, p2, p3, p4, p5, p6, p7, p8, "DATA"
        'Parameters p1 = Horizontal start position (X) in dots
        '           p2 = Vertical start position (Y) in dots.
        '           p3 = Rotation
        '               Value       Description
        '               0           No rotation
        '               1           90 degrees
        '               2           180 degrees
        '               3           270 degrees
        '           p4 = Bar Code selection.
        '               Interleaved 2 of 5 - 2
        '           p5 = Narrow bar width in dots.
        '               Interleaved 2 of 5 - 1-10
        '           p6 = Wide bar width in dots.
        '               Acceptable values are 2-30.
        '           p7 = Bar code height in dots.
        '           p8 = Print human readable code.
        '               Values: B=yes or N=no.
        '           "DATA" = Represents a fixed data field. The
        '               data in this field must comply with the selected
        '               bar(code) 's specified format.

        Codigo = TrataTexto(Codigo)
        Comandos.AppendLine(String.Format("B{0},{1},0,{2},2,{3},{4},{5},""{6}""", Posicao.X, Posicao.Y, Tipo, IIf(ReduzirCodigo, "4", "8"), Altura, IIf(ExibirCodigo, "B", "N"), Codigo))
    End Sub

    ''' <summary>
    ''' Trata o texto para remover os caracteres não aceitos pela impressora
    ''' </summary>
    ''' <param name="Texto">Texto a ser tratado</param>
    ''' <remarks></remarks>
    Private Function TrataTexto(ByVal Texto As String) As String
        Return Texto.Replace("\", "\\").Replace("""", "\""")
    End Function

    ''' <summary>
    ''' Retorna o tamanho em pixels que o texto irá ocupar na impressora
    ''' </summary>
    ''' <param name="Texto">Texto a ser impresso</param>
    ''' <param name="Fonte">Fonte utilizada na impressao</param>
    ''' <remarks></remarks>
    Private Function TamanhoTexto(ByVal Texto As String, ByVal Fonte As FonteZebra) As String
        Return Texto.Length * Fonte.Largura
    End Function

    Public Shared Function ConverteParaDpi(ByVal TamanhoEmMilimetros As Double) As Double
        Return (TamanhoEmMilimetros * ConversaoMilimetroPolegada) * DPI
    End Function

End Class
