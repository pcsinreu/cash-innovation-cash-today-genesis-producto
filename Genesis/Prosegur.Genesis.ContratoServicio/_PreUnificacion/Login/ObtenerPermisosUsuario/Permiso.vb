Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class Permiso

        <DataMember()> _
        Public Property CodigoPermiso() As String

        <DataMember()> _
        Public Property CodigoAplicacion As String

        <DataMember()> _
        Public Property DescripcionAplicacion As String

    End Class

End Namespace

