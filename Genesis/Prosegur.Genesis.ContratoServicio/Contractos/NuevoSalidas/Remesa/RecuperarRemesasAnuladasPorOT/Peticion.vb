Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Enumeradores.Salidas

Namespace Contractos.NuevoSalidas.Remesa.RecuperarRemesasRefPorOT

    <XmlType(Namespace:="urn:RecuperarRemesasRefPorOT")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasRefPorOT")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorOT() As String

    End Class

End Namespace