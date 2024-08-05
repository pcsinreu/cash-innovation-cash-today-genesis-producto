Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ObtenerPermisosUsuario

    <Serializable()> _
    <DataContract()> _
    Public Class PlantaColeccion
        Inherits List(Of Planta)

    End Class
End Namespace
