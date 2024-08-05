Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.ContractoServicio

Namespace Delegacion.SetDelegacion
    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pagoncalves] 07/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetDelegacion")> _
    <XmlRoot(Namespace:="urn:SetDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"
        Private _OidDelegacion As String
        Private _CodDelegacion As String
        Private _DesDelegacion As String
        Private _OidPais As String
        Private _CodPais As String
        Private _NecGmTMinutes As String
        Private _Fyh_Verano_Inicio As Nullable(Of Date)
        Private _Fyh_Verano_Fin As Nullable(Of Date)
        Private _Nec_Verao_Ajuste As String
        Private _Zona As String
        Private _Vigente As Boolean
        Private _BolTotasDelegacionesConfigRegionales As Boolean
        Private _Gmt_Creation As Nullable(Of Date)
        Private _Des_Usuario_Creation As String
        Private _Gmt_Modificacion As Nullable(Of Date)
        Private _Des_usuario_Modificacion As String
        Private _LstClienteFacturacion As List(Of ClienteFacturacion)
        Private _PeticionDatosBancarios As Contractos.Integracion.ConfigurarDatosBancarios.Peticion

        Public Property CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
#End Region

#Region "[PROPRIEDADE]"

        Public Property OidDelegacion As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property DesDelegacion() As String
            Get
                Return _DesDelegacion
            End Get
            Set(value As String)
                _DesDelegacion = value
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
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property
        Public Property NecGmtMinutos() As String
            Get
                Return _NecGmTMinutes
            End Get
            Set(value As String)
                _NecGmTMinutes = value
            End Set
        End Property

        Public Property FyhVeranoInicio() As Nullable(Of Date)
            Get
                Return _Fyh_Verano_Inicio
            End Get
            Set(value As Nullable(Of Date))
                _Fyh_Verano_Inicio = value
            End Set
        End Property

        Public Property FyhVeranoFin() As Nullable(Of Date)
            Get
                Return _Fyh_Verano_Fin
            End Get
            Set(value As Nullable(Of Date))
                _Fyh_Verano_Fin = value
            End Set
        End Property

        Public Property NecVeranoAjuste() As String
            Get
                Return _Nec_Verao_Ajuste
            End Get
            Set(value As String)
                _Nec_Verao_Ajuste = value
            End Set
        End Property

        Public Property DesZona() As String
            Get
                Return _Zona
            End Get
            Set(value As String)
                _Zona = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property GmtCreacion() As Nullable(Of Date)
            Get
                Return _Gmt_Creation
            End Get
            Set(value As Nullable(Of Date))
                _Gmt_Creation = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _Des_Usuario_Creation
            End Get
            Set(value As String)
                _Des_Usuario_Creation = value
            End Set
        End Property

        Public Property GmtModificacion() As Nullable(Of Date)
            Get
                Return _Gmt_Modificacion
            End Get
            Set(value As Nullable(Of Date))
                _Gmt_Modificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion As String
            Get
                Return _Des_usuario_Modificacion
            End Get
            Set(value As String)
                _Des_usuario_Modificacion = value
            End Set
        End Property

        Public Property BolTotasDelegacionesConfigRegionales() As Boolean
            Get
                Return _BolTotasDelegacionesConfigRegionales
            End Get
            Set(value As Boolean)
                _BolTotasDelegacionesConfigRegionales = value
            End Set
        End Property

        Public Property LstClienteFacturacion As List(Of ClienteFacturacion)
            Get
                Return _LstClienteFacturacion
            End Get
            Set(value As List(Of ClienteFacturacion))
                _LstClienteFacturacion = value
            End Set
        End Property

        Public Property PeticionDatosBancarios() As Contractos.Integracion.ConfigurarDatosBancarios.Peticion
            Get
                Return _PeticionDatosBancarios
            End Get
            Set(value As Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
                _PeticionDatosBancarios = value
            End Set
        End Property
#End Region
    End Class

End Namespace

