Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class Planificacion
        Inherits BindableBase
        Implements IEntidadeHelper

#Region "Variaveis"

        Private _CodigosAjeno As List(Of Comon.Clases.CodigoAjeno)
        Private _CodigoAjeno As Comon.Clases.CodigoAjeno
        Private _CodigoAjenoBancoComision As Comon.Clases.CodigoAjeno
        Private _Identificador As String
        Private _Codigo As String
        Private _Cliente As Cliente
        Private _Delegacion As Delegacion
        Private _TipoPlanificacion As TipoPlanificacion
        Private _Descripcion As String
        Private _FechaHoraVigenciaInicio As DateTime
        Private _FechaHoraVigenciaFin As DateTime?
        Private _Programacion As List(Of Clases.PlanXProgramacion)
        Private _ProgramacionOriginal As List(Of Clases.PlanXProgramacion)
        Private _Maquinas As List(Of Clases.Maquina)
        Private _Canales As List(Of Clases.Canal)
        Private _BolActivo As Boolean
        Private _DesUsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _DesUsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime
        Private _NecContigencia As Integer
        Private _FechaVigenciaFinGmt As String
        Private _FechaVigenciaInicioGmt As String
        Private _BolHayCambioHorario As Boolean
        Private _BolCambioHorarioProgramacion As Boolean
        Private _BolCambioTipoPlaniFVC As Boolean
        Private _NecQtdeProgramaciones As Integer
        Private _BolControlaFacturacion As Boolean
        Private _BancoComision As Cliente
        Private _PorcComisionPlanificacion As Nullable(Of Decimal)
        Private _DiaCierreFacturacion As Nullable(Of Integer)
        Private _DatosBancario As List(Of Prosegur.Genesis.Comon.Clases.DatoBancario)
        Private _BancoTesoreria As SubCliente
        Private _CuentaTesoreria As PuntoServicio
        Private _BolAgrupaPorSubCanal As Boolean
        Private _BolAgrupaPorPuntoServicio As Boolean
        Private _BolAgrupaPorFechaContable As Boolean
        Private _BolDividePorSubcanal As Boolean
        Private _BolDividePorDivisa As Boolean
        Private _BolDividePorPto As Boolean
        Private _CamposExtrasPatrones As Clases.CamposExtrasDeIAC
        Private _CamposExtrasDinamicos As Clases.CamposExtrasDeIAC
        Private _mensajes As List(Of Clases.MensajeDePlanificacion)
#End Region

#Region "Propriedades"
        Public Property CodigosAjeno As List(Of Comon.Clases.CodigoAjeno)
            Get
                Return _CodigosAjeno
            End Get
            Set(value As List(Of Comon.Clases.CodigoAjeno))
                _CodigosAjeno = value
            End Set
        End Property
        Public Property CodigoAjenoCliente As Comon.Clases.CodigoAjeno
            Get
                Return _CodigoAjeno
            End Get
            Set(value As Comon.Clases.CodigoAjeno)
                _CodigoAjeno = value
            End Set
        End Property

        Public Property CodigoAjenoBancoComision As Comon.Clases.CodigoAjeno
            Get
                Return _CodigoAjenoBancoComision
            End Get
            Set(value As Comon.Clases.CodigoAjeno)
                _CodigoAjenoBancoComision = value
            End Set
        End Property

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

        Public Property Delegacion As Delegacion
            Get
                Return _Delegacion
            End Get
            Set(value As Delegacion)
                SetProperty(_Delegacion, value, "Delegacion")
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

        Public Property TipoPlanificacion As TipoPlanificacion
            Get
                Return _TipoPlanificacion
            End Get
            Set(value As TipoPlanificacion)
                SetProperty(_TipoPlanificacion, value, "TipoPlanificacion")
            End Set
        End Property


        Public Property FechaHoraVigenciaInicio As DateTime
            Get
                Return _FechaHoraVigenciaInicio
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraVigenciaInicio, value, "FechaHoraVigenciaInicio")
            End Set
        End Property

        Public Property FechaHoraVigenciaFin As DateTime?
            Get
                Return _FechaHoraVigenciaFin
            End Get
            Set(value As DateTime?)
                SetProperty(_FechaHoraVigenciaFin, value, "FechaHoraVigenciaFin")
            End Set
        End Property

        Public Property Programacion As List(Of PlanXProgramacion)
            Get
                Return _Programacion
            End Get
            Set(value As List(Of PlanXProgramacion))
                SetProperty(_Programacion, value, "_Programacion")
            End Set
        End Property
        Public Property ProgramacionOriginal As List(Of PlanXProgramacion)
            Get
                If _ProgramacionOriginal Is Nothing Then
                    _ProgramacionOriginal = New List(Of PlanXProgramacion)
                End If
                Return _ProgramacionOriginal
            End Get
            Set(value As List(Of PlanXProgramacion))
                SetProperty(_ProgramacionOriginal, value, "_ProgramacionOriginal")
            End Set
        End Property

        Public Property Maquinas As List(Of Clases.Maquina)
            Get
                Return _Maquinas
            End Get
            Set(value As List(Of Clases.Maquina))
                SetProperty(_Maquinas, value, "_Maquinas")
            End Set
        End Property

        Public Property Canales As List(Of Clases.Canal)
            Get
                Return _Canales
            End Get
            Set(value As List(Of Clases.Canal))
                SetProperty(_Canales, value, "Canales")
            End Set
        End Property

        Public Property BolActivo As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                SetProperty(_BolActivo, value, "BolActivo")
            End Set
        End Property

        Public Property BolCambioHorario As Boolean
            Get
                Return _BolHayCambioHorario
            End Get
            Set(value As Boolean)
                SetProperty(_BolHayCambioHorario, value, "BolCambioHorario")
            End Set
        End Property

        Public Property BolCambioHorarioProgramacion As Boolean
            Get
                Return _BolCambioHorarioProgramacion
            End Get
            Set(value As Boolean)
                SetProperty(_BolCambioHorarioProgramacion, value, "BolCambioHorarioProgramacion")
            End Set
        End Property

        Public Property BolCambioTipoPlaniFVC As Boolean
            Get
                Return _BolCambioTipoPlaniFVC
            End Get
            Set(value As Boolean)
                SetProperty(_BolCambioTipoPlaniFVC, value, "BolCambioTipoPlaniFVC")
            End Set
        End Property

        Public Property DesUsuarioCreacion As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_DesUsuarioCreacion, value, "DesUsuarioCreacion")
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

        Public Property DesUsuarioModificacion As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_DesUsuarioModificacion, value, "DesUsuarioModificacion")
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

        Public Property NecContigencia As Integer
            Get
                Return _NecContigencia
            End Get
            Set(value As Integer)
                SetProperty(_NecContigencia, value, "NecContigencia")
            End Set
        End Property

        Public Property NecQtdeProgramaciones As Integer
            Get
                Return _NecQtdeProgramaciones
            End Get
            Set(value As Integer)
                SetProperty(_NecQtdeProgramaciones, value, "NecQtdeProgramaciones")
            End Set
        End Property

        Public Property FechaVigenciaFinGmt As String
            Get
                Return _FechaVigenciaFinGmt
            End Get
            Set(value As String)
                SetProperty(_FechaVigenciaFinGmt, value, "FechaVigenciaFinGmt")
            End Set
        End Property

        Public Property FechaVigenciaInicioGmt As String
            Get
                Return _FechaVigenciaInicioGmt
            End Get
            Set(value As String)
                SetProperty(_FechaVigenciaInicioGmt, value, "FechaVigenciaInicioGmt")
            End Set
        End Property


        Public Property BolControlaFacturacion As Boolean
            Get
                Return _BolControlaFacturacion
            End Get
            Set(value As Boolean)
                SetProperty(_BolControlaFacturacion, value, "BolControlaFacturacion")
            End Set
        End Property

        Public Property BancoComision As Cliente
            Get
                Return _BancoComision
            End Get
            Set(value As Cliente)
                SetProperty(_BancoComision, value, "BancoComision")
            End Set
        End Property

        Public Property PorcComisionPlanificacion As Nullable(Of Decimal)
            Get
                Return _PorcComisionPlanificacion
            End Get
            Set(value As Nullable(Of Decimal))
                SetProperty(_PorcComisionPlanificacion, value, "PorcComisionPlanificacion")
            End Set
        End Property

        Public Property DiaCierreFacturacion As Nullable(Of Integer)
            Get
                Return _DiaCierreFacturacion
            End Get
            Set(value As Nullable(Of Integer))
                SetProperty(_DiaCierreFacturacion, value, "DiaCierreFacturacion")
            End Set
        End Property


        Public Property DatosBancario() As List(Of Genesis.Comon.Clases.DatoBancario)
            Get
                Return _DatosBancario
            End Get
            Set(value As List(Of Genesis.Comon.Clases.DatoBancario))

                SetProperty(_DatosBancario, value, "_DatosBancario")
            End Set
        End Property


        Public Property BancoTesoreria As SubCliente
            Get
                Return _BancoTesoreria
            End Get
            Set(value As SubCliente)
                SetProperty(_BancoTesoreria, value, "BancoTesoreria")
            End Set
        End Property


        Public Property CuentaTesoreria As PuntoServicio
            Get
                Return _CuentaTesoreria
            End Get
            Set(value As PuntoServicio)
                SetProperty(_CuentaTesoreria, value, "CuentaTesoreria")
            End Set
        End Property

        Public Property BolAgrupaPorSubCanal As Boolean
            Get
                Return _BolAgrupaPorSubCanal

            End Get
            Set(value As Boolean)
                SetProperty(_BolAgrupaPorSubCanal, value, "BolAgrupaPorSubCanal")
            End Set
        End Property

        Public Property BolAgrupaPorPuntoServicio As Boolean
            Get
                Return _BolAgrupaPorPuntoServicio

            End Get
            Set(value As Boolean)
                SetProperty(_BolAgrupaPorPuntoServicio, value, "BolAgrupaPorPuntoServicio")
            End Set
        End Property

        Public Property BolAgrupaPorFechaContable As Boolean
            Get
                Return _BolAgrupaPorFechaContable

            End Get
            Set(value As Boolean)
                SetProperty(_BolAgrupaPorFechaContable, value, "BolAgrupaPorFechaContable")
            End Set
        End Property

        Public Property BolDividePorSubcanal As Boolean
            Get
                Return _BolDividePorSubcanal

            End Get
            Set(value As Boolean)
                SetProperty(_BolDividePorSubcanal, value, "BolDividePorSubcanal")
            End Set
        End Property

        Public Property BolDividePorDivisa As Boolean
            Get
                Return _BolDividePorDivisa

            End Get
            Set(value As Boolean)
                SetProperty(_BolDividePorDivisa, value, "BolDividePorDivisa")
            End Set
        End Property

        Public Property BolDividePorPto As Boolean
            Get
                Return _BolDividePorPto

            End Get
            Set(value As Boolean)
                SetProperty(_BolDividePorPto, value, "BolDividePorPto")
            End Set
        End Property


        Public Property CamposExtrasPatrones() As Clases.CamposExtrasDeIAC
            Get
                Return _CamposExtrasPatrones
            End Get
            Set(ByVal value As Clases.CamposExtrasDeIAC)
                _CamposExtrasPatrones = value
                '  SetProperty(_CamposExtrasPadrones, value, "CamposExtrasPadrones")
            End Set
        End Property


        Public Property CamposExtrasDinamicos() As Clases.CamposExtrasDeIAC
            Get
                Return _CamposExtrasDinamicos
            End Get
            Set(ByVal value As Clases.CamposExtrasDeIAC)
                _CamposExtrasDinamicos = value
                'SetProperty(_CamposExtrasDinamicos, value, "CamposExtrasDinamicos")
            End Set
        End Property

        Public Property Limites As List(Of Limite)
        Public Property Divisas As List(Of Divisa)
        Public Property Movimientos As List(Of Formulario)
        Public Property Procesos As List(Of Proceso)
        Public Property PatronConfirmacion As String

        Public Property Mensajes() As List(Of Clases.MensajeDePlanificacion)
            Get
                Return _mensajes
            End Get
            Set(ByVal value As List(Of Clases.MensajeDePlanificacion))
                _mensajes = value
            End Set
        End Property

#End Region

    End Class

End Namespace
