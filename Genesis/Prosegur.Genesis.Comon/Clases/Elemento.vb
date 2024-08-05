Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Elemento.vb
    '  Descripción: Clase definición Elemento
    ' ***********************************************************************
    <XmlRoot("Elemento", Namespace:="Prosegur.Genesis.Comon.Clases")>
    <XmlInclude(GetType(Remesa))>
    <XmlInclude(GetType(Bulto))>
    <XmlInclude(GetType(Contenedor))>
    <Serializable()>
    Public Class Elemento
        Inherits BindableBase

#Region "[VARIAVEIS]"

        Private _Precintos As ObservableCollection(Of String)
        Private _IdentificadorExterno As String
        Private _Identificador As String
        Private _ElementoPadre As Elemento
        Private _ElementoSustituto As Elemento
        Private _EstadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento
        Private _GrupoTerminosIAC As GrupoTerminosIAC
        Private _Cuenta As Cuenta
        Private _CuentaSaldo As Cuenta
        Private _Divisas As ObservableCollection(Of Divisa)
        Private _UsuarioModificacion As String
        Private _UsuarioBloqueo As String
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _FechaHoraCreacion As DateTime
        Private _PuestoResponsable As String
        Private _UsuarioResponsable As String
        Private _Modulo As Modulo
        Private _identificadorDocumento As String
        Private _Rowver As Nullable(Of Int64)
#End Region

#Region "[PROPRIEDADES]"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Precintos As ObservableCollection(Of String)
            Get
                Return _Precintos
            End Get
            Set(value As ObservableCollection(Of String))
                SetProperty(_Precintos, value, "Precintos")
            End Set
        End Property

        Public Property UsuarioResponsable As String
            Get
                Return _UsuarioResponsable
            End Get
            Set(value As String)
                SetProperty(_UsuarioResponsable, value, "UsuarioResponsable")
            End Set
        End Property

        Public Property PuestoResponsable As String
            Get
                Return _PuestoResponsable
            End Get
            Set(value As String)
                SetProperty(_PuestoResponsable, value, "PuestoResponsable")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property UsuarioBloqueo As String
            Get
                Return _UsuarioBloqueo
            End Get
            Set(value As String)
                SetProperty(_UsuarioBloqueo, value, "UsuarioBloqueo")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property Divisas As ObservableCollection(Of Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

        Public Property Cuenta As Cuenta
            Get
                Return _Cuenta
            End Get
            Set(value As Cuenta)
                SetProperty(_Cuenta, value, "Cuenta")
            End Set
        End Property

        Public Property CuentaSaldo As Cuenta
            Get
                Return _CuentaSaldo
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaSaldo, value, "CuentaSaldo")
            End Set
        End Property

        Public Property GrupoTerminosIAC As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIAC
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIAC, value, "GrupoTerminosIAC")
            End Set
        End Property

        Public Property EstadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento
            Get
                Return _EstadoDocumentoElemento
            End Get
            Set(value As Enumeradores.EstadoDocumentoElemento)
                SetProperty(_EstadoDocumentoElemento, value, "EstadoDocumentoElemento")
            End Set
        End Property

        Public Property ElementoPadre As Elemento
            Get
                Return _ElementoPadre
            End Get
            Set(value As Elemento)
                SetProperty(_ElementoPadre, value, "ElementoPadre")
            End Set
        End Property

        Public Property ElementoSustituto As Elemento
            Get
                Return _ElementoSustituto
            End Get
            Set(value As Elemento)
                SetProperty(_ElementoSustituto, value, "ElementoSustituto")
            End Set
        End Property

        Public Property IdentificadorExterno As String
            Get
                Return _IdentificadorExterno
            End Get
            Set(value As String)
                SetProperty(_IdentificadorExterno, value, "IdentificadorExterno")
            End Set
        End Property

        Public Property Modulo As Modulo
            Get
                Return _Modulo
            End Get
            Set(value As Modulo)
                SetProperty(_Modulo, value, "Modulo")
            End Set
        End Property

        Public Property IdentificadorDocumento As String
            Get
                Return _identificadorDocumento
            End Get
            Set(value As String)
                SetProperty(_identificadorDocumento, value, "IdentificadorDocumento")
            End Set
        End Property

        Public Property Rowver As Nullable(Of Int64)
            Get
                Return _Rowver
            End Get
            Set(value As Nullable(Of Int64))
                SetProperty(_Rowver, value, "Rowver")
            End Set
        End Property
#End Region

    End Class

End Namespace

