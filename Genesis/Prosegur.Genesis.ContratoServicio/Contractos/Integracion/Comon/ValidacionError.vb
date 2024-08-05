Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class ValidacionError

        ''' <summary>
        ''' Código del error de validación (VALXXXX)
        ''' </summary>
        Public Property codigo As String

        ''' <summary>
        ''' Descripción del error enviado.
        ''' </summary>
        Public Property descripcion As String

    End Class

End Namespace