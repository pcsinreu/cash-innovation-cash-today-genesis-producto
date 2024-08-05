Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon


Namespace Contractos.Integracion.ObtenerFormularioFondos

    <XmlType(Namespace:="urn:ObtenerFormularioFondos")> _
    <XmlRoot(Namespace:="urn:ObtenerFormularioFondos")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

        Sub New(mesaje As String)
            MyBase.New(mesaje)
        End Sub

        Sub New()

        End Sub
        
        Public Property CodFormulario As String

    End Class

End Namespace
