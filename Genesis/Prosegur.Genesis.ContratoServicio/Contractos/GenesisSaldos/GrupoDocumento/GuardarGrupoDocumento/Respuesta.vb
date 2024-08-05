Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.GrupoDocumento.GuardarGrupoDocumento

    <XmlType(Namespace:="urn:GuardarGrupoDocumento")> _
    <XmlRoot(Namespace:="urn:GuardarGrupoDocumento")> _
    <Serializable()>
    Public Class Respuesta
        Inherits BaseRespuesta

        Sub New()
            MyBase.New()
        End Sub

        Sub New(mensaje As String)
            MyBase.New(mensaje)
        End Sub

        Sub New(exception As Exception)
            MyBase.New(exception)
        End Sub

        Public Property GrupoDocumento As Clases.GrupoDocumentos

    End Class

End Namespace