Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresSector")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresSector")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Prosegur.Genesis.Comon.BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property Contenedores As List(Of ContenedorRespuesta)

#End Region

    End Class
End Namespace