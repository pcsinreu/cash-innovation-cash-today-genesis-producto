Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Enumeradores.Salidas

Namespace Contractos.NuevoSalidas.Remesa.EnviarRemesaSOL

    <XmlType(Namespace:="urn:EnviarRemesaSOL")> _
    <XmlRoot(Namespace:="urn:EnviarRemesaSOL")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorRemesa As List(Of String)

        Public Property UrlSOL As String

        Public Property ZerarIntentos As Boolean

    End Class

End Namespace