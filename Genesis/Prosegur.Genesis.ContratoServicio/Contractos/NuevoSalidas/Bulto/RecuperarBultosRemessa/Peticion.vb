Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.RecuperarBultosRemesa
    <XmlType(Namespace:="urn:RecuperarBultosRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarBultosRemesa")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorRemesa As String
        Public Property IdentificadoresBultos As List(Of String)
    End Class
End Namespace

