Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Cuenta.vb
    '  Descripción: Clase definición Cuenta
    ' ***********************************************************************
    <Serializable()>
    Public Class Cuenta
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Cliente As Cliente
        Private _SubCliente As SubCliente
        Private _PuntoServicio As PuntoServicio
        Private _Sector As Sector
        Private _Canal As Canal
        Private _SubCanal As SubCanal
        Private _TipoCuenta As Enumeradores.TipoCuenta
        Private _UsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime


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

        Public Property Cliente As Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Cliente)
                SetProperty(_Cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As SubCliente
            Get
                Return _SubCliente
            End Get
            Set(value As SubCliente)
                SetProperty(_SubCliente, value, "SubCliente")
            End Set
        End Property

        Public Property PuntoServicio As PuntoServicio
            Get
                Return _PuntoServicio
            End Get
            Set(value As PuntoServicio)
                SetProperty(_PuntoServicio, value, "PuntoServicio")
            End Set
        End Property

        Public Property Sector As Sector
            Get
                Return _Sector
            End Get
            Set(value As Sector)
                SetProperty(_Sector, value, "Sector")
            End Set
        End Property

        Public Property Canal As Canal
            Get
                Return _Canal
            End Get
            Set(value As Canal)
                SetProperty(_Canal, value, "Canal")
            End Set
        End Property

        Public Property SubCanal As SubCanal
            Get
                Return _SubCanal
            End Get
            Set(value As SubCanal)
                SetProperty(_SubCanal, value, "SubCanal")
            End Set
        End Property

        Public Property TipoCuenta As Enumeradores.TipoCuenta
            Get
                Return _TipoCuenta
            End Get
            Set(value As Enumeradores.TipoCuenta)
                SetProperty(_TipoCuenta, value, "TipoCuenta")
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

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
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

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

#End Region

    End Class

End Namespace
