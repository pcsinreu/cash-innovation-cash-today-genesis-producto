Namespace Divisa.SetDivisasDenominaciones

    ''' <summary>
    ''' Classe RespuestaDivisa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class RespuestaDivisa
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _CodigoIso As String
        Private _Descripcion As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoIso() As String
            Get
                Return _CodigoIso
            End Get
            Set(value As String)
                _CodigoIso = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

#End Region

    End Class

End Namespace