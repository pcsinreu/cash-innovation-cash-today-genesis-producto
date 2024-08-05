''' <summary>
''' TipoCodigoBarra
''' </summary>
''' <remarks></remarks>
''' <history>
''' [cbomtempo] 07/11/2008 Criado
''' </history>
Public Class TipoCodigoBarra

    Public Shared ReadOnly INTERLEAVED2OF5 As TipoCodigoBarra = New TipoCodigoBarra("2")
    Public ReadOnly Codigo As String

    ''' <summary>
    ''' Construtor da classe
    ''' </summary>
    ''' <param name="Codigo"></param>
    ''' <remarks></remarks>
    Private Sub New(ByVal Codigo As String)
        Me.Codigo = Codigo
    End Sub

    ''' <summary>
    ''' ToString() sobrescrito para retornar o código da fonte
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Codigo
    End Function
End Class
