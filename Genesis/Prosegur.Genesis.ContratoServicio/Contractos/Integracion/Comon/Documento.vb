Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Documento

        ''' <summary>
        ''' Código del comprobante generado en Génesis.
        ''' </summary>
        Public Property codigoComprobante As String

        ''' <summary>
        ''' Identificador informado por origen
        ''' </summary>
        Public Property codigoExterno As String

        Public Property codigoTipoPlanificacion As String

        Public Property codigoPlanificacion As String
    End Class

End Namespace