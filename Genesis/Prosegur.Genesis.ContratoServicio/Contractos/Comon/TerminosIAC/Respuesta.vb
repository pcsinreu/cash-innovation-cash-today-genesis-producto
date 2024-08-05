Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Terminos.RecuperarTerminosIAC

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTerminosIAC")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosIAC")> _
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
        Public Property TerminosIAC As ObservableCollection(Of Clases.TerminoIAC)

    End Class

End Namespace