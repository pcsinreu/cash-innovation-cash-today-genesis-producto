Public Class CampoObrigatorioException
    Inherits NegocioExcepcion

    ''' <summary>
    ''' Validação de campo obrigatório.
    ''' </summary>
    ''' <param name="descricao">Mensagem de erro.</param>
    ''' <remarks></remarks>
    Public Sub New(descricao As String)
        MyBase.New(descricao)
    End Sub
End Class
