
Namespace Excepciones

    Public NotInheritable Class ExcepcionLogica
        Inherits Exception

        ''' <summary>
        ''' Mensagem de erro.
        ''' </summary>
        ''' <param name="message">Dados da mensagem</param>
        ''' <remarks></remarks>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class
End Namespace