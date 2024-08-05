Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresCliente")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresCliente")> _
    <Serializable()> _
    Public Class ClienteRespuesta

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property codSubcliente As String
        Public Property codPuntoServicio As String
        Public Property Contenedores As List(Of ContenedorRespuesta)

#End Region

    End Class

End Namespace
