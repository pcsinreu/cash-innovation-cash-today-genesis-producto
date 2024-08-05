Imports System.Xml.Serialization
Imports System.Xml

Namespace ParteDiferencias.GetDocumentos

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:Documentos")> _
    <XmlRoot(Namespace:="urn:Documentos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Documentos As Documentos

#End Region

#Region "Propriedades"

        Public Property Documentos() As Documentos
            Get
                Return _Documentos
            End Get
            Set(value As Documentos)
                _Documentos = value
            End Set
        End Property

#End Region

    End Class

End Namespace