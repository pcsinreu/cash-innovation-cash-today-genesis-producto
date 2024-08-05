Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Cliente

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property codSubCliente As String
        Public Property codPtoServicio As String
        Public Property IdentificadorCliente As String
        Public Property IdentificadorSubCliente As String
        Public Property IdentificadorPtoServicio As String
        Public Property desCliente As String
        Public Property desSubCliente As String
        Public Property desPtoServicio As String

#End Region

    End Class

End Namespace
