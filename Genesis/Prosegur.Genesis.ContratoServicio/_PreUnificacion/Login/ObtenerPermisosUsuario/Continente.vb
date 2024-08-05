Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class Continente

        <DataMember()> _
        Public Property Nombre() As String

        <DataMember()> _
        Public Property Paises() As PaisColeccion

    End Class

End Namespace

