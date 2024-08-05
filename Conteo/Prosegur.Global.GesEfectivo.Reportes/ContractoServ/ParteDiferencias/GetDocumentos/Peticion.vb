Imports System.Xml.Serialization
Imports System.Xml

Namespace ParteDiferencias.GetDocumentos

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:Documentos")> _
    <XmlRoot(Namespace:="urn:Documentos")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _ID As String
        Private _General As Boolean
        Private _Comentario As Boolean
        Private _Justificativa As Boolean
        Private _DatosConteo As Boolean

#End Region

#Region " Propriedades "

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(value As String)
                _ID = value
            End Set
        End Property
        Public Property General() As Boolean
            Get
                Return _General
            End Get
            Set(value As Boolean)
                _General = value
            End Set
        End Property
        Public Property Comentario() As Boolean
            Get
                Return _Comentario
            End Get
            Set(value As Boolean)
                _Comentario = value
            End Set
        End Property
        Public Property Justificativa() As Boolean
            Get
                Return _Justificativa
            End Get
            Set(value As Boolean)
                _Justificativa = value
            End Set
        End Property
        Public Property DatosConteo() As Boolean
            Get
                Return _DatosConteo
            End Get
            Set(value As Boolean)
                _DatosConteo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
