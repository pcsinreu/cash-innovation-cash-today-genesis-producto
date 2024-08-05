Namespace ParteDiferencias.GetParteDiferencias

    Public Class DatosDocumentos

#Region " Variáveis "

        Private _ID As String = String.Empty
        Private _NumeroDocumento As String = String.Empty
        Private _FechaCreacion As DateTime = DateTime.MinValue
        Private _HayDocumentoGeneral As Boolean
        Private _HayDocumentoIncidencia As Boolean
        Private _HayDocumentoJustificacion As Boolean

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
        Public Property NumeroDocumento() As String
            Get
                Return _NumeroDocumento
            End Get
            Set(value As String)
                _NumeroDocumento = value
            End Set
        End Property
        Public Property FechaCreacion() As DateTime
            Get
                Return _FechaCreacion
            End Get
            Set(value As DateTime)
                _FechaCreacion = value
            End Set
        End Property
        Public Property HayDocumentoGeneral() As Boolean
            Get
                Return _HayDocumentoGeneral
            End Get
            Set(value As Boolean)
                _HayDocumentoGeneral = value
            End Set
        End Property
        Public Property HayDocumentoIncidencia() As Boolean
            Get
                Return _HayDocumentoIncidencia
            End Get
            Set(value As Boolean)
                _HayDocumentoIncidencia = value
            End Set
        End Property
        Public Property HayDocumentoJustificacion() As Boolean
            Get
                Return _HayDocumentoJustificacion
            End Get
            Set(value As Boolean)
                _HayDocumentoJustificacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
