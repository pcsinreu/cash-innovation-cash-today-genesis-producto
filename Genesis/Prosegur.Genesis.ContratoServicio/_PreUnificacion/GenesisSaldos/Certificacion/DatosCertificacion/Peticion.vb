Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.DatosCertificacion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GenerarCertificacion")> _
    <XmlRoot(Namespace:="urn:GenerarCertificacion")> _
    <Serializable()> _
    Public Class Peticion
        Inherits CertificadoComon

#Region "[PROPRIEDADES]"

        Public Property DelegacionLogada As Clases.Delegacion
        Public Property CodigoPais As String
        Public Property CodigoCertificadoDefinitivo As String
        Public Property EsTodosSectores As Boolean
        Public Property EsTodosCanales As Boolean
        Public Property EsTodasDelegaciones As Boolean
        Public Property UsuarioCreacion As String
        Public Property GmtCreacion As DateTime

        Public Property IdentificadorCertificado As String


#End Region

    End Class

End Namespace