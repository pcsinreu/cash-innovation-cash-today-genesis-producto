Imports System.Xml.Serialization

Namespace Login.EjecutarLoginAplicacion


    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 15/07/2009 Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLoginAplicacion")> _
    <XmlRoot(Namespace:="urn:EjecutarLoginAplicacion")> _
    Public Class Peticion

        Public Property IdentificadorUsuario As String
        Public Property Password As String
        Public Property Pais As String
        Public Property Delegacion As String
        Public Property Planta As String
        Public Property Aplicacion As String
        Public Property VersionAplicacion As String
    End Class

End Namespace
