Namespace GuardarDatosDocumento

    Public Class Campo

#Region "[VARIÁVEIS]"

        Private _Nombre As Enumeradores.eCampos
        Private _Valor As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Nombre() As Enumeradores.eCampos
            Get
                Return _Nombre
            End Get
            Set(Value As Enumeradores.eCampos)
                _Nombre = Value
            End Set
        End Property

        Public Property Valor() As String
            Get
                Return _Valor
            End Get
            Set(Value As String)
                _Valor = Value
            End Set
        End Property

#End Region

    End Class

End Namespace