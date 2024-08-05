Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Genesis.ConfigGrid.RecuperarConfigGrid

    <XmlType(Namespace:="urn:RecuperarConfigGrid")> _
    <XmlRoot(Namespace:="urn:RecuperarConfigGrid")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String
        Public Property CodigoFuncionalidade As String

    End Class

End Namespace
