Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresPackModular

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresPackModular")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property vencidos As String
        Public Property fechaVencimiento As DateTime
        Public Property tiposSectores As List(Of String)
        Public Property sectores As List(Of Sector)
        Public Property cliente As Cliente
        Public Property canal As Canal
        Public Property CodigoUsuario As String

#End Region

    End Class

End Namespace