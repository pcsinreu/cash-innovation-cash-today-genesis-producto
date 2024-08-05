Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class BaseRespuesta

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        Public Property codigoResultado As String

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        Public Property descripcionResultado As String

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        Public Property ValidacionesError As List(Of ValidacionError)

        ''' <summary>
        ''' Estructura donde vendrá informado el movimiento a crear
        ''' </summary>
        Public Property TiempoDeEjecucion As String

    End Class

End Namespace

