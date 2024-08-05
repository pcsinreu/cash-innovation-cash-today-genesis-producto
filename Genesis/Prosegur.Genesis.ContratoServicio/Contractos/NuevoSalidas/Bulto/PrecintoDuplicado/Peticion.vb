Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.PrecintoDuplicado

    <XmlType(Namespace:="urn:PrecintoDuplicado")> _
    <XmlRoot(Namespace:="urn:PrecintoDuplicado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorBulto As String
        Public Property IdentificadorObjeto As String
        Public Property CodigoPrecinto As String
        Public Property ValidarPrecintoObjeto As Boolean

    End Class

End Namespace