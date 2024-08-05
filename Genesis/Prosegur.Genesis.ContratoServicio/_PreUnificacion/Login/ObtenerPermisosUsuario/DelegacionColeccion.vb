Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class DelegacionColeccion
        Inherits List(Of Delegacion)


    End Class

End Namespace

