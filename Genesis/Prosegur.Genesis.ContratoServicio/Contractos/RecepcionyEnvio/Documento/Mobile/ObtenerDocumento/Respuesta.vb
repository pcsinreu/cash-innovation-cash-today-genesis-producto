Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Documento.Mobile.ObtenerDocumento

    <XmlType(Namespace:="urn:ObtenerDocumento")> _
    <XmlRoot(Namespace:="urn:ObtenerDocumento")> _
    <Serializable()>
    Public Class Respuesta

        Public Property exito As Boolean

        <XmlArray(ElementName:="documentos")>
        <XmlArrayItem(ElementName:="documento")>
        Public Property documento As List(Of Documento)
         
        <XmlArray(ElementName:="errores")>
        <XmlArrayItem(ElementName:="error")>
        Public Property errores As List(Of ErrorRespuesta)

        Private Sub CheckDocumentoCollection()
            If IsNothing(documento) Then
                documento = New List(Of Documento)()
            End If
        End Sub

        Public Sub AddDocumentos(docs As ObservableCollection(Of Documento))
            If Not IsNothing(docs) Then
                CheckDocumentoCollection()
                For Each doc In docs
                    documento.Add(doc)
                Next
            End If
        End Sub

        ''' <summary>
        ''' Añade el error y establece exito=1
        ''' </summary>
        ''' <param name="objetoError"></param>
        ''' <remarks></remarks>
        Public Sub AddError(objetoError As ErrorRespuesta)
            If IsNothing(errores) Then
                errores = New List(Of ErrorRespuesta)()
            End If
            errores.Add(objetoError)
            exito = False
        End Sub


    End Class

End Namespace