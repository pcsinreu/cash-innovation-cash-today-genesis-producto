Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class CampoAdicional

        ''' <summary>
        ''' Nombre del campo de información Adicional.(Campo GEPR_TTERMINO.COD_TERMINO)
        ''' </summary>
        Public Property nombre As String

        ''' <summary>
        ''' Valor del término, transformar el tipo de dato del término enviado.
        ''' </summary>
        Public Property valor As String

    End Class

End Namespace