Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Cliente.vb
    '  Descripción: Clase definición Cliente
    ' ***********************************************************************
    <Serializable()>
    Public Class Cliente
        Inherits BindableBase
        Implements IEntidadeHelper


#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EstaActivo As String
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _EstaEnviadoSaldos As Boolean
        Private _CodigoMigracion As String
        Private _EsTotalizadorSaldo As Boolean
        Private _TipoCliente As Tipo
        Private _SubClientes As ObservableCollection(Of SubCliente)
        Private _Maquinas As ObservableCollection(Of Maquina)
        Public _PorcComisionCliente As Nullable(Of Decimal)
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

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property EstaActivo As String
            Get
                Return _EstaActivo
            End Get
            Set(value As String)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
            End Set
        End Property

        Public Property FechaHoraActualizacion As DateTime
            Get
                Return _FechaHoraActualizacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraActualizacion, value, "FechaHoraActualizacion")
            End Set
        End Property

        Public Property EstaEnviadoSaldos As Boolean
            Get
                Return _EstaEnviadoSaldos
            End Get
            Set(value As Boolean)
                SetProperty(_EstaEnviadoSaldos, value, "EstaEnviadoSaldos")
            End Set
        End Property

        Public Property CodigoMigracion As String
            Get
                Return _CodigoMigracion
            End Get
            Set(value As String)
                SetProperty(_CodigoMigracion, value, "CodigoMigracion")
            End Set
        End Property

        Public Property EsTotalizadorSaldo As Boolean
            Get
                Return _EsTotalizadorSaldo
            End Get
            Set(value As Boolean)
                SetProperty(_EsTotalizadorSaldo, value, "EsTotalizadorSaldo")
            End Set
        End Property

        Public Property TipoCliente As Tipo
            Get
                Return _TipoCliente
            End Get
            Set(value As Tipo)
                SetProperty(_TipoCliente, value, "TipoCliente")
            End Set
        End Property

        Public Property SubClientes As ObservableCollection(Of SubCliente)
            Get
                Return _SubClientes
            End Get
            Set(value As ObservableCollection(Of SubCliente))
                SetProperty(_SubClientes, value, "SubClientes")
            End Set
        End Property
        Public Property PorcComisionCliente As Nullable(Of Decimal)
            Get
                Return _PorcComisionCliente
            End Get
            Set(value As Nullable(Of Decimal))
                SetProperty(_PorcComisionCliente, value, "PorcComisionCliente")
            End Set
        End Property
        Public Property Maquinas As ObservableCollection(Of Maquina)
            Get
                Return _Maquinas
            End Get
            Set(value As ObservableCollection(Of Maquina))
                SetProperty(_Maquinas, value, "Maquinas")
            End Set
        End Property

        Public Property BolGrabaSaldoHistorico As Boolean
        Public Property CodFechaSaldoHistorico As String
#End Region

    End Class

End Namespace
