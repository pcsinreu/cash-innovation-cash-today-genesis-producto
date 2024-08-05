Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreSectores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ReenvioEntreSectores")> _
    <XmlRoot(Namespace:="urn:ReenvioEntreSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property IdentificadorDocumento As String
        Public Property CodigoCombrobante As String

    End Class

End Namespace