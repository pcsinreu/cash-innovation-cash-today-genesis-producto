Namespace GenesisSaldos.Certificacion.ObtenerCertificado

    <Serializable()> _
    Public Class Certificado

#Region "[VARIAVEIS]"
        Private _codCertificado As String
        Private _codEstado As String
        Private _fyhCertificado As DateTime
        Private _identificadorCertificado As String
#End Region

#Region "[PROPRIEDADES]"
        Public Property codCertificado() As String
            Get
                Return _codCertificado
            End Get
            Set(value As String)
                _codCertificado = value
            End Set
        End Property

        Public Property codEstado() As String
            Get
                Return _codEstado
            End Get
            Set(value As String)
                _codEstado = value
            End Set
        End Property

        Public Property fyhCertificado() As DateTime
            Get
                Return _fyhCertificado
            End Get
            Set(value As DateTime)
                _fyhCertificado = value
            End Set
        End Property

        Public Property IdentificadorCertificado() As String
            Get
                Return _identificadorCertificado
            End Get
            Set(value As String)
                _identificadorCertificado = value
            End Set
        End Property
#End Region

    End Class

End Namespace

