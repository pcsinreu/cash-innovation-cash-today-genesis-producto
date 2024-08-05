Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Valores

        ''' <summary>
        ''' Colección de Divisas
        ''' </summary>
        Public Property divisas As List(Of Divisa)

        ''' <summary>
        ''' Colección de Medios de Pago
        ''' </summary>
        Public Property mediosDePago As List(Of MedioDePago)

    End Class

End Namespace