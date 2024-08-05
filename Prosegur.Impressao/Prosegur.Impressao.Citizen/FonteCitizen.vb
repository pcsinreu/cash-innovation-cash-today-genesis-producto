''' <summary>
''' FonteCitizen
''' </summary>
''' <remarks></remarks>
''' <history>
''' [cbomtempo] 07/11/2008 Criado
''' </history>
Public Class TamanhoFonteCitizen

    Public Shared ReadOnly X1 As TamanhoFonteCitizen = New TamanhoFonteCitizen(0)
    Public Shared ReadOnly X2 As TamanhoFonteCitizen = New TamanhoFonteCitizen(1)
    Public Shared ReadOnly X3 As TamanhoFonteCitizen = New TamanhoFonteCitizen(2)
    Public Shared ReadOnly X4 As TamanhoFonteCitizen = New TamanhoFonteCitizen(3)
    Public Shared ReadOnly X5 As TamanhoFonteCitizen = New TamanhoFonteCitizen(4)
    Public Shared ReadOnly X6 As TamanhoFonteCitizen = New TamanhoFonteCitizen(5)
    Public Shared ReadOnly X7 As TamanhoFonteCitizen = New TamanhoFonteCitizen(6)
    Public Shared ReadOnly X8 As TamanhoFonteCitizen = New TamanhoFonteCitizen(7)

    Friend ReadOnly Codigo As Integer

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="Codigo">Codigo da fonte</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal Codigo As Integer)
        Me.Codigo = Codigo
    End Sub

    ''' <summary>
    ''' ToString() sobrescrito para retornar o código da fonte
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Codigo.ToString()
    End Function

End Class