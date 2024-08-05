Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.Formulario.ObtenerFormularioPorCaractetisticas

    ''' <summary>
    ''' Clase ObtenerFormularioPorCaractetisticasRespuesta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerFormularioPorCaractetisticas")> _
    <XmlRoot(Namespace:="urn:ObtenerFormularioPorCaractetisticas")> _
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
        Public Property CodigoFormulario As String

    End Class

End Namespace