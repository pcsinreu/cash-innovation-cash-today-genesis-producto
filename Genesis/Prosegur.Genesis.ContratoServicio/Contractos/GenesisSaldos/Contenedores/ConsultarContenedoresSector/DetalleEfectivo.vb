Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()> _
    Public Class DetalleEfectivo

        Public Property CodigoIsoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property CodigoCalidad As String
        Public Property Disponible As Boolean
        Public Property Bloqueado As Boolean
        Public Property Cantidad As Integer
        Public Property Importe As Decimal

        Public Property NumValor As Integer

    End Class

End Namespace