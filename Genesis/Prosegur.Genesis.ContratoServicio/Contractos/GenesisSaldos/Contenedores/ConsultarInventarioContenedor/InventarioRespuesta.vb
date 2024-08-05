Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarInventarioContenedor

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:ConsultarInventarioContenedor")> _
    <Serializable()> _
    Public Class InventarioRespuesta
#Region "[PROPRIEDADES]"

        Public Property codInventario As String
        Public Property fechaHoraInventario As DateTime
        Public Property desUsuario As String
        Public Property cantidadeLogica As Integer
        Public Property cantidadeInventariada As Integer
        Public Property Sector As Sector

#End Region
    End Class

End Namespace

