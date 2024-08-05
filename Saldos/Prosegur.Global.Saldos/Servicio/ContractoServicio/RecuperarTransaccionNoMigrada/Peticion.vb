Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarTransaccionNoMigrada

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [marcel.espiritosanto] - 21/05/2013 - Creado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionNoMigrada")> _
    <Serializable()> _
    Public Class Peticion

#Region "Propriedades"
        Public Property Fecha As DateTime
        Public Property CodigoClienteSaldo As String
        Public Property Plantas As List(Of String)
#End Region

    End Class

End Namespace