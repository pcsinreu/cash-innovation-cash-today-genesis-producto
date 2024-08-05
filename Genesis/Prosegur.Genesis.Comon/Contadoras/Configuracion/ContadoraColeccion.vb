Imports System.Xml.Serialization

Namespace Contadoras.Configuracion

    <Serializable()> _
    <XmlRoot(elementName:="ContadoraColeccion")> _
    Public Class ContadoraColeccion
        Inherits List(Of Contadora)

    End Class

End Namespace