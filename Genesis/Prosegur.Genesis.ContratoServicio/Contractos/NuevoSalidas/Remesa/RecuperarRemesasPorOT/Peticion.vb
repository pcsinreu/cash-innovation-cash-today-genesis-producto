Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Enumeradores.Salidas

Namespace Contractos.NuevoSalidas.Remesa.RecuperarRemesasPorOT

    <XmlType(Namespace:="urn:RecuperarRemesasPorOT")> _
    <XmlRoot(Namespace:="urn:RecuperarRemesasPorOT")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorOT() As String

    End Class

End Namespace