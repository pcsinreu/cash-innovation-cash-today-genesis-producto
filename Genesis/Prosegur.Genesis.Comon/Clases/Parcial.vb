Imports System.Reflection
Imports System.ComponentModel

Namespace Clases
    ' ***********************************************************************
    '  Modulo:  Parcial.vb
    '  Descripción: Clase definición Parcial
    ' ***********************************************************************
    <Serializable()>
    Public Class Parcial
        Inherits Elemento

#Region "[VARIAVEIS]"

        Private _Secuencia As Integer
        Private _EsFicticio As Boolean
        Private _FechaHoraInicioConteo As DateTime
        Private _FechaHoraFinConteo As DateTime
        Private _Estado As Enumeradores.EstadoParcial
        Private _TipoFormato As TipoFormato

#End Region

#Region "[PROPRIEDADES]"

        Public Property EsFicticio As Boolean
            Get
                Return _EsFicticio
            End Get
            Set(value As Boolean)
                SetProperty(_EsFicticio, value, "EsFicticio")
            End Set
        End Property
        Public Property Estado As Enumeradores.EstadoParcial
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoParcial)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property
        Public Property FechaHoraFinConteo As DateTime
            Get
                Return _FechaHoraFinConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraFinConteo, value, "FechaHoraFinConteo")
            End Set
        End Property
        Public Property FechaHoraInicioConteo As DateTime
            Get
                Return _FechaHoraInicioConteo
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraInicioConteo, value, "FechaHoraInicioConteo")
            End Set
        End Property

        Public Property Secuencia As Integer
            Get
                Return _Secuencia
            End Get
            Set(value As Integer)
                SetProperty(_Secuencia, value, "Secuencia")
            End Set
        End Property
        Public Property TipoFormato As TipoFormato
            Get
                Return _TipoFormato
            End Get
            Set(value As TipoFormato)
                SetProperty(_TipoFormato, value, "TipoFormato")
            End Set
        End Property

#End Region

        Sub New()
            Identificador = System.Guid.NewGuid().ToString()
        End Sub

    End Class

End Namespace
