Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarSeguimientoElemento

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <XmlRoot(Namespace:="urn:ConsultarSeguimientoElemento")> _
    <Serializable()> _
    Public Class Canal

#Region "[PROPRIEDADES]"

        Public Property codCanal As String
        Public Property codSubcanal As String
        Public Property IdentificadorCanal As String
        Public Property IdentificadorSubcanal As String
        Public Property desCanal As String
        Public Property desSubcanal As String

#End Region

    End Class

End Namespace
