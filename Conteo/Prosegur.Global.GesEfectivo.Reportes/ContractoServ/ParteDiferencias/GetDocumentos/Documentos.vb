Namespace ParteDiferencias.GetDocumentos

    Public Class Documentos

#Region " Variáveis "

        Private _ID As String = String.Empty
        Private _General As Byte()
        Private _Comentario As Byte()
        Private _Justificativa As Byte()
        Private _DatosConteo As Byte()

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
        Public Property General() As Byte()
            Get
                Return _General
            End Get
            Set(value As Byte())
                _General = value
            End Set
        End Property
        Public Property Comentario() As Byte()
            Get
                Return _Comentario
            End Get
            Set(value As Byte())
                _Comentario = value
            End Set
        End Property
        Public Property Justificativa() As Byte()
            Get
                Return _Justificativa
            End Get
            Set(value As Byte())
                _Justificativa = value
            End Set
        End Property
        Public Property DatosConteo() As Byte()
            Get
                Return _DatosConteo
            End Get
            Set(value As Byte())
                _DatosConteo = value
            End Set
        End Property

#End Region

    End Class

End Namespace
