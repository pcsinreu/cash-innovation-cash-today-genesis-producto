Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionDetail
    ''' <summary>
    ''' Classe Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>

    <Serializable()> _
    Public Class Delegacion
#Region "[VARIAVEIS]"

        Private _OidDelegacion As String
        Private _CodPais As String
        Private _codDelegacion As String
        Private _Description As String
        Private _OidPais As String
        Private _NecGmTMinutes As String
        Private _Fhy_Verao_Inicio As Date
        Private _Fhy_Verao_Fim As Date
        Private _Nec_Verao_Ajuste As String
        Private _Zona As String
        Private _Vigente As Boolean
        Private _Gmt_Creation As Date
        Private _Des_Usuario_Creation As String
        Private _Gmt_Modificacion As Date
        Private _Des_usuario_Modificacion As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodigoDelegacion As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
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

        Public Property CodDelegacion() As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
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

        Public Property FhyVeraoInicio() As String
            Get
                Return _Fhy_Verao_Inicio
            End Get
            Set(value As String)
                _Fhy_Verao_Inicio = value
            End Set
        End Property

        Public Property FhyVeraoFim() As String
            Get
                Return _Fhy_Verao_Fim
            End Get
            Set(value As String)
                _Fhy_Verao_Fim = value
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

        Public Property GmtCreation() As String
            Get
                Return _Gmt_Creation
            End Get
            Set(value As String)
                _Gmt_Creation = value
            End Set
        End Property

        Public Property Des_Usuario_Create() As String
            Get
                Return _Des_Usuario_Creation
            End Get
            Set(value As String)
                _Des_Usuario_Creation = value
            End Set
        End Property

        Public Property Gmt_Modificacion() As String
            Get
                Return _Gmt_Modificacion
            End Get
            Set(value As String)
                _Gmt_Modificacion = value
            End Set
        End Property

        Public Property Des_Usuario_Modificacion As String
            Get
                Return _Des_usuario_Modificacion
            End Get
            Set(value As String)
                _Des_usuario_Modificacion = value
            End Set
        End Property

#End Region
    End Class
End Namespace
