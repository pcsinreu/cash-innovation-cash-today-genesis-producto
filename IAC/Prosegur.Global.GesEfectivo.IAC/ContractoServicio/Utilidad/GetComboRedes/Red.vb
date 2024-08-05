Namespace Utilidad.GetComboRedes

    ''' <summary>
    ''' Classe red
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Red

#Region "[Variáveis]"

        Private _oidRed As String
        Private _codigoRed As String
        Private _descripcionRed As String

#End Region

#Region "[Propriedades]"

        Public Property OidRed() As String
            Get
                Return _oidRed
            End Get
            Set(value As String)
                _oidRed = value
            End Set
        End Property

        Public Property CodigoRed() As String
            Get
                Return _codigoRed
            End Get
            Set(value As String)
                _codigoRed = value
            End Set
        End Property

        Public Property DescripcionRed() As String
            Get
                Return _descripcionRed
            End Get
            Set(value As String)
                _descripcionRed = value
            End Set
        End Property

#End Region

    End Class

End Namespace