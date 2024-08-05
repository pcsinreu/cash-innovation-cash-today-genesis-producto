Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class PermisoColeccion
        Inherits List(Of Permiso)

    End Class

End Namespace

