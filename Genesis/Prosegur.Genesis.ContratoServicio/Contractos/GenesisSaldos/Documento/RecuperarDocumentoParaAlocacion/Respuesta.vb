Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.GenesisSaldos.Documento.RecuperarDocumentoParaAlocacion

    <XmlType(Namespace:="urn:RecuperarDocumentoParaAlocacion")> _
    <XmlRoot(Namespace:="urn:RecuperarDocumentoParaAlocacion")> _
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

        Public Property Documentos As ObservableCollection(Of Clases.Documento)

    End Class

End Namespace