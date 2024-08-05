Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data

''' <summary>
''' Classe que gera Codigo de Barras
''' Essa classe foi copiada do projeto Genesis - Conteo
''' </summary>
''' <history>[jorge.viana]	24/08/2010	Creado</history>
''' <remarks></remarks>
Public Class CodigoBarras

    Public Enum AlignType
        Left
        Center
        Right
    End Enum

    Public Enum BarCodeWeight
        Small = 1
        Medium
        Large
    End Enum

    Private align As AlignType = AlignType.Left
    Private code As String = ""
    Private _leftMargin As Integer = 10
    Private _topMargin As Integer = 10
    Private height As Integer = 50
    Private _width As Integer = 100
    Private _showHeader As Boolean
    Private _showFooter As Boolean
    Private _headerText As String = ""
    Private _weight As BarCodeWeight = BarCodeWeight.Small
    Private _headerFont As Font = New Font("Courier New", 12)
    Private _footerFont As Font = New Font("Courier New", 12)

    Public Property VertAlign() As AlignType
        Get
            Return align
        End Get
        Set(value As AlignType)
            align = value
        End Set
    End Property

    Public Property BarCode() As String
        Get
            Return code
        End Get
        Set(value As String)
            code = value.ToUpper()
        End Set
    End Property

    Public Property BarCodeHeight() As Integer
        Get
            Return height
        End Get
        Set(value As Integer)
            height = value
        End Set
    End Property

    Public Property LeftMargin() As Integer
        Get
            Return _leftMargin
        End Get
        Set(value As Integer)
            _leftMargin = value
        End Set
    End Property

    Public Property TopMargin() As Integer
        Get
            Return _topMargin
        End Get
        Set(value As Integer)
            _topMargin = value
        End Set
    End Property

    Public Property ShowHeader() As Boolean
        Get
            Return _showHeader
        End Get
        Set(value As Boolean)
            _showHeader = value
        End Set
    End Property

    Public Property ShowFooter() As Boolean
        Get
            Return _showFooter
        End Get
        Set(value As Boolean)
            _showFooter = value
        End Set
    End Property

    Public Property HeaderText() As String
        Get
            Return _headerText
        End Get
        Set(value As String)
            _headerText = value
        End Set
    End Property

    Public Property Weight() As BarCodeWeight
        Get
            Return _weight
        End Get
        Set(value As BarCodeWeight)
            _weight = value
        End Set
    End Property

    Public Property Width() As BarCodeWeight
        Get
            Return _width
        End Get
        Set(value As BarCodeWeight)
            _width = value
        End Set
    End Property

    Public Property HeaderFont() As Font
        Get
            Return _headerFont
        End Get
        Set(value As Font)
            _headerFont = value
        End Set
    End Property

    Public Property FooterFont() As Font
        Get
            Return _footerFont
        End Get
        Set(value As Font)
            _footerFont = value
        End Set
    End Property

    Dim alphabet39 As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*"

    Dim coded39Char As String() = _
    {"000110100", _
     "100100001", _
     "001100001", _
     "101100000", _
     "000110001", _
     "100110000", _
     "001110000", _
     "000100101", _
     "100100100", _
     "001100100", _
     "100001001", _
     "001001001", _
     "101001000", _
     "000011001", _
     "100011000", _
     "001011000", _
     "000001101", _
     "100001100", _
     "001001100", _
     "000011100", _
     "100000011", _
     "001000011", _
     "101000010", _
     "000010011", _
     "100010010", _
     "001010010", _
     "000000111", _
     "100000110", _
     "001000110", _
     "000010110", _
     "110000001", _
     "011000001", _
     "111000000", _
     "010010001", _
     "110010000", _
     "011010000", _
     "010000101", _
     "110000100", _
     "011000100", _
     "010101000", _
     "010100010", _
     "010001010", _
     "000101010", _
     "010010100"}

    '"000110100", = '0'
    '"100100001", = '1'
    '"001100001", = '2'
    '"101100000", = '3'
    '"000110001", = '4'
    '"100110000", = '5'
    '"001110000", = '6'
    '"000100101", = '7'
    '"100100100", = '8'
    '"001100100", = '9'
    '"100001001", = 'A'
    '"001001001", = 'B'
    '"101001000", = 'C'
    '"000011001", = 'D'
    '"100011000", = 'E'
    '"001011000", = 'F'
    '"000001101", = 'G'
    '"100001100", = 'H'
    '"001001100", = 'I'
    '"000011100", = 'J'
    '"100000011", = 'K'
    '"001000011", = 'L'
    '"101000010", = 'M'
    '"000010011", = 'N'
    '"100010010", = 'O'
    '"001010010", = 'P'
    '"000000111", = 'Q'
    '"100000110", = 'R'
    '"001000110", = 'S'
    '"000010110", = 'T'
    '"110000001", = 'U'
    '"011000001", = 'V'
    '"111000000", = 'W'
    '"010010001", = 'X'
    '"110010000", = 'Y'
    '"011010000", = 'Z'
    '"010000101", = '-'
    '"110000100", = '.'
    '"011000100", = ' '
    '"010101000", = '$'
    '"010100010", = '/'
    '"010001010", = '+'
    '"000101010", = '%'
    '"010010100"  = '*'

    Public Sub Desenhar(graph As Graphics)
        Dim intercharacterGap As String = "0"
        Dim str As String = "*" & code.ToUpper() & "*"
        Dim strLength As Integer = str.Length

        For i As Integer = 0 To code.Length - 1
            If alphabet39.IndexOf(code(i)) = -1 OrElse code(i) = "*" Then
                Throw New InvalidExpressionException("Código de barra inválido")
            End If
        Next

        Dim encodedString As String = ""
        For i As Integer = 0 To strLength - 1
            If i > 0 Then
                encodedString += intercharacterGap
            End If
            encodedString &= coded39Char(alphabet39.IndexOf(str(i)))
        Next

        Dim encodedStringLength As Integer = encodedString.Length
        Dim widthOfBarCodeString As Integer = 0
        Dim wideToNarrowRatio As Double = 3

        If align <> AlignType.Left Then
            For i As Integer = 0 To encodedStringLength - 1
                If encodedString(i) = "1" Then
                    widthOfBarCodeString += wideToNarrowRatio * _weight
                Else
                    widthOfBarCodeString += _weight
                End If
            Next
        End If

        Dim x As Integer = 0
        Dim wid As Integer = 0
        Dim yTop As Integer = 0
        Dim hSize As SizeF = graph.MeasureString(_headerText, _headerFont)
        Dim fSize As SizeF = graph.MeasureString(code, _footerFont)

        Dim headerX As Integer = 0
        Dim footerX As Integer = 0

        If align = AlignType.Left Then
            x = _leftMargin
            headerX = _leftMargin
            footerX = _leftMargin
        ElseIf align = AlignType.Center Then
            x = (_width - widthOfBarCodeString) / 2
            headerX = (_width - hSize.Width) / 2
            footerX = (_width - fSize.Width) / 2
        Else
            x = _width - widthOfBarCodeString - _leftMargin
            headerX = _width - hSize.Width - _leftMargin
            footerX = _width - fSize.Width - _leftMargin
        End If

        If _showHeader Then
            yTop = hSize.Height + _topMargin
            graph.DrawString(_headerText, _headerFont, Brushes.Black, headerX, _topMargin)
        Else
            yTop = _topMargin
        End If

        For i As Integer = 0 To encodedStringLength - 1
            If encodedString(i) = "1" Then
                wid = wideToNarrowRatio * _weight
            Else
                wid = _weight
            End If

            graph.FillRectangle(IIf(i Mod 2 = 0, Brushes.Black, Brushes.White), x, yTop, wid, height)

            x += wid
        Next

        yTop += height

        If _showFooter Then
            graph.DrawString(code, _footerFont, Brushes.Black, footerX, yTop)
        End If

    End Sub

End Class