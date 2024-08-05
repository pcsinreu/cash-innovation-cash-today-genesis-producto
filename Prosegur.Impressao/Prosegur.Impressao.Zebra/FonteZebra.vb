''' <summary>
''' FonteZebra
''' </summary>
''' <remarks></remarks>
''' <history>
''' [cbomtempo] 07/11/2008 Criado
''' </history>
Public Class FonteZebra

    'Public Shared ReadOnly MINUSCULO As FonteZebra = New FonteZebra(1, 8, 12)
    'Public Shared ReadOnly PEQUENO As FonteZebra = New FonteZebra(2, 10, 16)
    'Public Shared ReadOnly MEDIO As FonteZebra = New FonteZebra(3, 12, 20)
    'Public Shared ReadOnly GRANDE As FonteZebra = New FonteZebra(4, 14, 24)
    'Public Shared ReadOnly GIGANTE As FonteZebra = New FonteZebra(5, 32, 48)

    Public Shared ReadOnly MINUSCULO As FonteZebra = New FonteZebra(1, 9, 12)
    Public Shared ReadOnly PEQUENO As FonteZebra = New FonteZebra(2, 12, 16)
    Public Shared ReadOnly MEDIO As FonteZebra = New FonteZebra(3, 14, 20)
    Public Shared ReadOnly GRANDE As FonteZebra = New FonteZebra(4, 16, 24)
    Public Shared ReadOnly GIGANTE As FonteZebra = New FonteZebra(5, 35, 48)
    Public Shared ReadOnly CHINES As FonteZebra = New FonteZebra(8, 36, 36)
    Public Shared ReadOnly CHINES_TRAD As FonteZebra = New FonteZebra(9, 36, 36)

    Friend ReadOnly Codigo As Integer
    Friend ReadOnly Largura As Integer
    Friend ReadOnly Altura As Integer

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="Codigo">Codigo da fonte</param>
    ''' <param name="Largura">Largura em pixels</param>
    ''' <param name="Altura">Altura em pixels</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal Codigo As Integer, ByVal Largura As Integer, ByVal Altura As Integer)
        Me.Codigo = Codigo
        Me.Largura = Largura
        Me.Altura = Altura
    End Sub

    ''' <summary>
    ''' ToString() sobrescrito para retornar o código da fonte
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Codigo.ToString()
    End Function

End Class