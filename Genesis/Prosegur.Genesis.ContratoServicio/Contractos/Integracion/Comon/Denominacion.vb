Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Denominacion

        ''' <summary>
        ''' Código de la denominación
        ''' </summary>
        Public Property codigoDenominacion As String

        ''' <summary>
        ''' Cantidad
        ''' </summary>
        Public Property cantidad As Integer

        ''' <summary>
        ''' Importe detallado
        ''' </summary>
        Public Property importe As Double

    End Class

End Namespace