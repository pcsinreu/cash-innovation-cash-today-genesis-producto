Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Integracion.vb
    '  Descripción: Clase definición Integracion
    ' ***********************************************************************

    <Serializable()>
    Public Class Integracion
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _IdentificadorTablaIntegracion As String
        Private _CodigoTablaIntegracionEnum As Enumeradores.TablaIntegracion
        Private _CodigoEstado As Enumeradores.EstadoIntegracion
        Private _CodigoModuloOrigen As Enumeradores.Aplicacion
        Private _CodigoModuloDestino As Enumeradores.Aplicacion
        Private _CodigoProceso As Enumeradores.CodigoProcesoIntegracion
        Private _Intentos As Integer
        Private _IntegracionErros As ObservableCollection(Of IntegracionError)
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _CodigoTablaIntegracion As String

#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property IdentificadorTablaIntegracion As String
            Get
                Return _IdentificadorTablaIntegracion
            End Get
            Set(value As String)
                SetProperty(_IdentificadorTablaIntegracion, value, "IdentificadorTablaIntegracion")
            End Set
        End Property

        Public Property CodigoTablaIntegracionEnum As Enumeradores.TablaIntegracion
            Get
                Return _CodigoTablaIntegracionEnum
            End Get
            Set(value As Enumeradores.TablaIntegracion)
                SetProperty(_CodigoTablaIntegracionEnum, value, "CodigoTablaIntegracionEnum")
            End Set
        End Property


        Public Property CodigoTablaIntegracion() As String
            Get
                Return _CodigoTablaIntegracion
            End Get
            Set(ByVal value As String)
                SetProperty(_CodigoTablaIntegracion, value, "CodigoTablaIntegracion")
            End Set
        End Property


        Public Property CodigoEstado As Enumeradores.EstadoIntegracion
            Get
                Return _CodigoEstado
            End Get
            Set(value As Enumeradores.EstadoIntegracion)
                SetProperty(_CodigoEstado, value, "CodigoEstado")
            End Set
        End Property

        Public Property CodigoModuloOrigen As Enumeradores.Aplicacion
            Get
                Return _CodigoModuloOrigen
            End Get
            Set(value As Enumeradores.Aplicacion)
                SetProperty(_CodigoModuloOrigen, value, "CodigoModuloOrigen")
            End Set
        End Property

        Public Property CodigoModuloDestino As Enumeradores.Aplicacion
            Get
                Return _CodigoModuloDestino
            End Get
            Set(value As Enumeradores.Aplicacion)
                SetProperty(_CodigoModuloDestino, value, "CodigoModuloDestino")
            End Set
        End Property

        Public Property CodigoProceso As Enumeradores.CodigoProcesoIntegracion
            Get
                Return _CodigoProceso
            End Get
            Set(value As Enumeradores.CodigoProcesoIntegracion)
                SetProperty(_CodigoProceso, value, "CodigoProceso")
            End Set
        End Property

        Public Property Intentos As Integer
            Get
                Return _Intentos
            End Get
            Set(value As Integer)
                SetProperty(_Intentos, value, "Intentos")
            End Set
        End Property

        Public Property IntegracionErros As ObservableCollection(Of IntegracionError)
            Get
                Return _IntegracionErros
            End Get
            Set(value As ObservableCollection(Of IntegracionError))
                SetProperty(_IntegracionErros, value, "IntegracionErros")
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

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
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

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

#End Region

    End Class

End Namespace
