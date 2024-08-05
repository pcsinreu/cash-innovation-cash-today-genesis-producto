Namespace RecuperarRemesasPorGrupo

    Public Class Documento

#Region "[VARIAVEIS]"

        Private _NombreDocumento As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property NombreDocumento() As String
            Get
                Return _NombreDocumento
            End Get
            Set(value As String)
                _NombreDocumento = value
            End Set
        End Property

#End Region

    End Class

End Namespace