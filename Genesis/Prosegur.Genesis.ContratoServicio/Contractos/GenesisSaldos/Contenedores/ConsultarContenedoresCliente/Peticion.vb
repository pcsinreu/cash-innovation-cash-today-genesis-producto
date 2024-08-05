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
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Clientes As List(Of Cliente)
        Public Property Sectores As List(Of Sector)
        Public Property Canais As List(Of Canal)
        Public Property Contenedor As Contenedor
        Public Property CodigoUsuario As String

#End Region

    End Class

End Namespace