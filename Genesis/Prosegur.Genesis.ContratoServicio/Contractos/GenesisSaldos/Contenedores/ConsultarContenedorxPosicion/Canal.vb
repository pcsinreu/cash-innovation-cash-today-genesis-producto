Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedorxPosicion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedorxPosicion")> _
    <Serializable()> _
    Public Class Canal

#Region "[PROPRIEDADES]"

        Public Property oidCanal As String
        Public Property codCanal As String
        Public Property oidSubCanal As String
        Public Property codSubcanal As String
        Public Property desCanal As String
        Public Property desSubCanal As String

#End Region

    End Class

End Namespace
