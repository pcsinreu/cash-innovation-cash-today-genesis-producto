Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreClientes

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ReenvioEntreClientes")> _
    <XmlRoot(Namespace:="urn:ReenvioEntreClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

        Public Property IdentificadorDocumento As String
        Public Property CodigoCombrobante As String

    End Class

End Namespace