Namespace ManipularDocumentos

    ''' <summary>
    ''' CamposExtra
    ''' </summary>
    <Serializable()> _
    Public Class CamposExtra

#Region "[VARIÁVEIS]"

        Private _Nombre As String
        Private _Valor As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Valor() As String
            Get
                Return _Valor
            End Get
            Set(value As String)
                _Valor = value
            End Set
        End Property

#End Region

    End Class

End Namespace