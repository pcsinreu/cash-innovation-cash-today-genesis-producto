Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class Pais

        <DataMember()> _
        Public Property Codigo() As String

        <DataMember()> _
        Public Property Nombre() As String

        <DataMember()> _
        Public Property CodigoISODivisa() As String

        <DataMember()> _
        Public Property Delegaciones() As DelegacionColeccion

    End Class

End Namespace

