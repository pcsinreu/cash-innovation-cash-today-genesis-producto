Namespace Delegacion.SetDelegacion
    ''' <summary>
    ''' Classe RespuestaDivisa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class RespuestaDelegacion
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _CodigoOidDelegacion As String
        Private _CodPais As String
        Private _Description As String
        Private _OidPais As String
        Private _NecGmTMinutes As String
        Private _Fyh_Verano_Inicio As String
        Private _Fyh_Verano_Fin As String
        Private _Nec_Verao_Ajuste As String
        Private _Zona As String
        Private _Vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoOidDelegacion As String
            Get
                Return _CodigoOidDelegacion
            End Get
            Set(value As String)
                _CodigoOidDelegacion = value
            End Set
        End Property

        Public Property CodPais() As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Public Property OidPais() As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property NecGmTminutes() As String
            Get
                Return _NecGmTMinutes
            End Get
            Set(value As String)
                _NecGmTMinutes = value
            End Set
        End Property

        Public Property FyhVeranoInicio() As String
            Get
                Return _Fyh_Verano_Inicio
            End Get
            Set(value As String)
                _Fyh_Verano_Inicio = value
            End Set
        End Property

        Public Property FyhVeranoFin() As String
            Get
                Return _Fyh_Verano_Fin
            End Get
            Set(value As String)
                _Fyh_Verano_Fin = value
            End Set
        End Property

        Public Property NecVeraoAjuste() As String
            Get
                Return _Nec_Verao_Ajuste
            End Get
            Set(value As String)
                _Nec_Verao_Ajuste = value
            End Set
        End Property

        Public Property Zona() As String
            Get
                Return _Zona
            End Get
            Set(value As String)
                _Zona = value
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
