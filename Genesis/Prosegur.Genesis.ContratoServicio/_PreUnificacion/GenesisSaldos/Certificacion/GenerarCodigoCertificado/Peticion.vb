Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.GenerarCodigoCertificado

    <XmlType(Namespace:="urn:GenerarCodigoCertificado")> _
    <XmlRoot(Namespace:="urn:GenerarCodigoCertificado")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property CodCliente As String
        Public Property CodEstado As String
        Public Property FyhCertificado As DateTime
        Public Property CodDelegacion As String
        Public Property CodSector As String
        Public Property CodSubcanal As String
        Public Property BolTodosSectores As Boolean
        Public Property BolTodosCanales As Boolean
        Public Property BolTodasDelegaciones As Boolean
        Public Property BolVariosSectores As Boolean
        Public Property BolVariosCanales As Boolean
        Public Property BolVariosDelegaciones As Boolean

#End Region

    End Class

End Namespace