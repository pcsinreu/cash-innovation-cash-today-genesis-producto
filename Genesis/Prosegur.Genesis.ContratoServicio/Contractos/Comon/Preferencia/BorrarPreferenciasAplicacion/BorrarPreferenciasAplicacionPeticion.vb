Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
    <XmlType(Namespace:="urn:BorrarPreferenciasAplicacion")> _
    <XmlRoot(Namespace:="urn:BorrarPreferenciasAplicacion")> _
    <Serializable()>
    Public Class BorrarPreferenciasAplicacionPeticion
        Inherits BasePeticion

        Public Property CodigoAplicacion As Enumeradores.CodigoAplicacion
        Public Property CodigoUsuario As String
        Public Property CodigoFuncionalidad As String

    End Class
End Namespace