Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <Serializable()> _
    Public Class DetalleMedioPago

        Public Property CodigoIsoDivisa As String
        Public Property CodigoTipoMedioPago As Prosegur.Genesis.Comon.Enumeradores.TipoMedioPago
        Public Property CodigoMedioPago As String
        Public Property Disponible As Boolean
        Public Property Bloqueado As Boolean
        Public Property Cantidad As Integer
        Public Property Importe As Decimal

    End Class

End Namespace