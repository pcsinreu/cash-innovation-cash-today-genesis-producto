Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.GetMedioPagoDetail
    ''' <summary>
    ''' Classe ValorTermino
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>

    <Serializable()> _
    Public Class ValorTermino

#Region "[Variáveis]"

        'Medio Pago
        Private _Codigo As String
        Private _Descripcion As String
        Private _Vigente As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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
        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property
#End Region

    End Class

End Namespace