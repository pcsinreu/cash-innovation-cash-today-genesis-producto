Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Formulario.ObtenerIdentificadoresFormulariosPorCaracteristicas

    ''' <summary>
    ''' Clase ObtenerFormularioPorCaractetisticasRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerIdentificadoresFormulariosPorCaracteristicas")> _
    <XmlRoot(Namespace:="urn:ObtenerIdentificadoresFormulariosPorCaracteristicas")> _
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

        Sub New(mesaje As String)
            MyBase.New(mesaje)
        End Sub

        Sub New()

        End Sub
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IdendificadoresFormularios As New List(Of String)

    End Class

End Namespace