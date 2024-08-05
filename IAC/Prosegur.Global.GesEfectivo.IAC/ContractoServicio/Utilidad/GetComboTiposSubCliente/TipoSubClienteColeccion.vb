Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposSubCliente

    ''' <summary>
    ''' Coleção de TipoSubCliente
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TipoSubClienteColeccion
        Inherits List(Of TipoSubCliente)

    End Class

End Namespace