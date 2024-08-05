Imports System.Xml.Serialization

Namespace Login.ValidarPermisosUsuario

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:ValidarPermisosUsuario")> _
    <XmlRoot(Namespace:="urn:ValidarPermisosUsuario")> _
    Public Class Peticion

        Public Property Login As String
        Public Property ClaveSupervisor As String
        Public Property CodigoAplicacion As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoTipoSector As String
        Public Property Permisos As List(Of String)

    End Class

End Namespace