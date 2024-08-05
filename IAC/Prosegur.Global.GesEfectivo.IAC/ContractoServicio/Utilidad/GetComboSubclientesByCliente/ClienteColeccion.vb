Namespace Utilidad.GetComboSubclientesByCliente

    ''' <summary>
    ''' Classe ClienteColeccion
    ''' Acrescentada devido à necessidade de consultas com mais de um cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 31/10/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class ClienteColeccion
        Inherits List(Of Cliente)

    End Class

End Namespace