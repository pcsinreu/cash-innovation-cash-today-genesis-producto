Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Terminos.RecuperarTerminosPorCodigos

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTerminosPorCodigos")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosPorCodigos")> _
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
        Public Property Terminos As ObservableCollection(Of Clases.TerminoIAC)

    End Class

End Namespace