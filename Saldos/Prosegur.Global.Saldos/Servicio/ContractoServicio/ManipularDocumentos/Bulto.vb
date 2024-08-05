Namespace ManipularDocumentos

    ''' <summary>
    ''' Bulto
    ''' </summary>
    <Serializable()> _
    Public Class Bulto

#Region "[VARIÁVEIS]"

        Private _CodPrecinto As String
        Private _IdBultoOrigen As String
        Private _CodUbicacion As Integer

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodPrecinto() As String
            Get
                Return _CodPrecinto
            End Get
            Set(value As String)
                _CodPrecinto = value
            End Set
        End Property

        Public Property IdBultoOrigen() As String
            Get
                Return _IdBultoOrigen
            End Get
            Set(value As String)
                _IdBultoOrigen = value
            End Set
        End Property

        Public Property CodUbicacion() As Integer
            Get
                Return _CodUbicacion
            End Get
            Set(value As Integer)
                _CodUbicacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace