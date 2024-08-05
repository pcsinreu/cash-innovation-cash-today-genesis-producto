Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
        Public Class MedioDePago

        ''' <summary>
        ''' Código del medio de pago informado.
        ''' </summary>
        Public Property codigoMedioDePago As String

        ''' <summary>
        ''' Código ISO de la Divisa
        ''' </summary>
        Public Property codigoDivisa As String

        ''' <summary>
        ''' Importe
        ''' </summary>
        Public Property importe As Double

        ''' <summary>
        ''' Cantidad de medios de pago que componen ese importe. En caso de no venir informado, asumir 1
        ''' </summary>
        Public Property cantidad As Integer

    End Class

End Namespace