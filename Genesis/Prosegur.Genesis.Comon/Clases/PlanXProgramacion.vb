Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class PlanXProgramacion
        Inherits BindableBase

#Region "Variaveis"
        Private _Identificador As String
        Private _NecDiaInicio As Integer
        Private _NecDiaFin As Integer
        Private _FechaHoraInicio As DateTime
        Private _FechaHoraFin As DateTime
        Private _DesUsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _DesUsuarioModificacion As String
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

        Public Property NecDiaInicio As Integer
            Get
                Return _NecDiaInicio
            End Get
            Set(value As Integer)
                SetProperty(_NecDiaInicio, value, "NecDiaInicio")
            

            End Set
        End Property

        Public Property NecDiaFin As Integer
            Get
                Return _NecDiaFin
            End Get
            Set(value As Integer)
                SetProperty(_NecDiaFin, value, "NecDiaFin")
            End Set
        End Property

        Public Property FechaHoraInicio As DateTime
            Get
                Return _FechaHoraInicio
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraInicio, value, "FechaHoraInicio")
            End Set
        End Property

        Public Property FechaHoraFin As DateTime
            Get
                Return _FechaHoraFin
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraFin, value, "FechaHoraFin")
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

        'Propriedas apenas para exibição no grid
        Public Property FyhLunes As String
        Public Property FyhMartes As String
        Public Property FyhMiercoles As String
        Public Property FyhJueves As String
        Public Property FyhViernes As String
        Public Property FyhSabado As String
        Public Property FyhDomingo As String
        Public Property LunesHabilitado As Boolean
        Public Property MartesHabilitado As Boolean
        Public Property MiercolesHabilitado As Boolean
        Public Property JuevesHabilitado As Boolean
        Public Property ViernesHabilitado As Boolean
        Public Property SabadoHabilitado As Boolean
        Public Property DomingoHabilitado As Boolean

#End Region

    End Class

End Namespace
