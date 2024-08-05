Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarAlertaVencimiento

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarAlertaVencimiento")> _
    <XmlRoot(Namespace:="urn:GrabarAlertaVencimiento")> _
    <Serializable()> _
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property codTipoContenedor As String
        Public Property codEstadoContenedor As String
        Public Property fechaHoraArmado As DateTime
        Public Property Precintos As System.Collections.ObjectModel.ObservableCollection(Of String)

#End Region

    End Class
End Namespace