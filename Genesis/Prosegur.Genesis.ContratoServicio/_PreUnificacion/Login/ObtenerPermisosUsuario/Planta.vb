Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class Planta

        <DataMember()> _
        Public Property Codigo() As String

        <DataMember()> _
        Public Property Descricao() As String

        <DataMember()> _
        Public Property TiposSectores() As TipoSectorColeccion

    End Class
End Namespace
