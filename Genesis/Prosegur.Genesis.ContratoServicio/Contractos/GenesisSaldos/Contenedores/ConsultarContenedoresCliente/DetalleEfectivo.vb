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
    Public Class DetalleEfectivo

#Region "[PROPRIEDADES]"

        Public Property codDivisa As String
        Public Property codDenominacion As String
        Public Property codCalidad As String
        Public Property disponible As Boolean
        Public Property bloqueado As Boolean
        Public Property cantidad As Integer
        Public Property importe As Decimal

#End Region

    End Class

End Namespace
