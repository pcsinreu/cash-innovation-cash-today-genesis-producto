Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetConfigNivel

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 20/05/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetConfigNivel")> _
    <XmlRoot(Namespace:="urn:GetConfigNivel")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

        Public Property CodCliente As String
        Public Property CodSubCliente As String
        Public Property CodPtoServicio As String
        Public Property CodSubCanal As List(Of String)
        Public Property BolActivo As Nullable(Of Boolean)

    End Class
End Namespace
