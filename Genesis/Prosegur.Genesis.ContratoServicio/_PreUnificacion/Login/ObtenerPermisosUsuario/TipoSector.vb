Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class TipoSector

        <DataMember()> _
        Public Property Codigo() As String

        <DataMember()> _
        Public Property Permisos() As PermisoColeccion

    End Class

End Namespace

