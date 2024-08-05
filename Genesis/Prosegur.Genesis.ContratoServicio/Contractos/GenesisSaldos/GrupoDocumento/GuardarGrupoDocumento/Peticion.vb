Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento

    <XmlType(Namespace:="urn:GuardarGrupoDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarGrupoDocumento")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property UsuarioLogado As String

        Public Property GrupoDocumento As Clases.GrupoDocumentos

    End Class

End Namespace