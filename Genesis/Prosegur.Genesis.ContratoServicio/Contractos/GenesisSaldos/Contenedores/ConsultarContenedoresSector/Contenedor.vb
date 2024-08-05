Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresSector

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresSector")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresSector")> _
    <Serializable()> _
    Public Class Contenedor

#Region "[PROPRIEDADES]"

        Public Property codTipoContenedor As String
        Public Property codEstadoContenedor As String
        Public Property fechaHoraArmadoDesde As Nullable(Of DateTime)
        Public Property fechaHoraArmadoHasta As Nullable(Of DateTime)
        Public Property bolMayorNivel As Boolean
        Public Property packModular As Boolean
        Public Property bolRetornarElementosHijos As Boolean
        Public Property Precintos As List(Of String)

#End Region

    End Class
End Namespace