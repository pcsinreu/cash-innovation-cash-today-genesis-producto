Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorxPosicion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <Serializable()> _
    Public Class Cliente

#Region "[PROPRIEDADES]"

        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property codSubcliente As String
        Public Property codPuntoServicio As String
        Public Property oidPuntoServicio As String

        Public Property desCliente As String



#End Region

    End Class

End Namespace
