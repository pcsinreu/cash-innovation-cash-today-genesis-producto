Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarInventarioContenedor

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:ConsultarInventarioContenedor")> _
    <Serializable()> _
    Public Class DetalleMedioPago

#Region "[PROPRIEDADES]"

        Public Property codDivisa As String
        Public Property codTipoMedioPago As String
        Public Property codMedioPago As String
        Public Property disponible As Boolean
        Public Property bloqueado As Boolean
        Public Property cantidad As Integer
        Public Property importe As Decimal

#End Region

    End Class

End Namespace
