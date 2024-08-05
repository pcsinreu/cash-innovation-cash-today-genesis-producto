Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.RecuperarBultosNoArqueados

    <XmlType(Namespace:="urn:RecuperarBultosNoArqueados")> _
    <XmlRoot(Namespace:="urn:RecuperarBultosNoArqueados")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoPuesto As String
        Public Property CodigoDelegacion As String
        Public Property TrabajaPorBulto As String

    End Class

End Namespace