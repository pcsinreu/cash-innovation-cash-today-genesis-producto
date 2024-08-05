Namespace Utilidad.GetComboModelosCajero

    ''' <summary>
    ''' Classe modelo de cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class ModeloCajero

#Region "[Variáveis]"

        Private _oidModeloCajero As String
        Private _codigoModeloCajero As String
        Private _descripcionModeloCajero As String

#End Region

#Region "[Propriedades]"

        Public Property OidModeloCajero() As String
            Get
                Return _oidModeloCajero
            End Get
            Set(value As String)
                _oidModeloCajero = value
            End Set
        End Property

        Public Property CodigoModeloCajero() As String
            Get
                Return _codigoModeloCajero
            End Get
            Set(value As String)
                _codigoModeloCajero = value
            End Set
        End Property

        Public Property DescripcionModeloCajero() As String
            Get
                Return _descripcionModeloCajero
            End Get
            Set(value As String)
                _descripcionModeloCajero = value
            End Set
        End Property

#End Region

    End Class

End Namespace