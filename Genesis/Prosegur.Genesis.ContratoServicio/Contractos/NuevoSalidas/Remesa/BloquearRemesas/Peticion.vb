Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.BloquearRemesas

    <XmlType(Namespace:="urn:BloquearRemesas")> _
    <XmlRoot(Namespace:="urn:BloquearRemesas")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadoresRemesas As List(Of String)
        Public Property IdentificadoresBultos As List(Of String)
        Public Property CodigoUsuarioBloqueio As String
    End Class

End Namespace