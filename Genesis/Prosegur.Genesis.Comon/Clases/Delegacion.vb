Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Delegacion.vb
    '  Descripción: Clase definición Delegacion
    ' ***********************************************************************
    <Serializable()>
    Public Class Delegacion
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _HusoHorarioEnMinutos As Integer
        Private _FechaHoraVeranoInicio As DateTime
        Private _FechaHoraVeranoFin As DateTime
        Private _AjusteHorarioVerano As Integer
        Private _Zona As String
        Private _EsActivo As Boolean
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _Plantas As ObservableCollection(Of Planta)
        'Private _TiposSector As ObservableCollection(Of TipoSector)
        Private _CodigoPais As String

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

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property HusoHorarioEnMinutos As Integer
            Get
                Return _HusoHorarioEnMinutos
            End Get
            Set(value As Integer)
                SetProperty(_HusoHorarioEnMinutos, value, "HusoHorarioEnMinutos")
            End Set
        End Property

        Public Property FechaHoraVeranoInicio As DateTime
            Get
                Return _FechaHoraVeranoInicio
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraVeranoInicio, value, "FechaHoraVeranoInicio")
            End Set
        End Property

        Public Property FechaHoraVeranoFin As DateTime
            Get
                Return _FechaHoraVeranoFin
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraVeranoFin, value, "FechaHoraVeranoFin")
            End Set
        End Property

        Public Property AjusteHorarioVerano As Integer
            Get
                Return _AjusteHorarioVerano
            End Get
            Set(value As Integer)
                SetProperty(_AjusteHorarioVerano, value, "AjusteHorarioVerano")
            End Set
        End Property

        Public Property Zona As String
            Get
                Return _Zona
            End Get
            Set(value As String)
                SetProperty(_Zona, value, "Zona")
            End Set
        End Property

        Public Property EsActivo As Boolean
            Get
                Return _EsActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EsActivo, value, "EsActivo")
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

        Public Property Plantas As ObservableCollection(Of Planta)
            Get
                Return _Plantas
            End Get
            Set(value As ObservableCollection(Of Planta))
                SetProperty(_Plantas, value, "Plantas")
            End Set
        End Property

        'Public Property TiposSector As ObservableCollection(Of TipoSector)
        '    Get
        '        Return _TiposSector
        '    End Get
        '    Set(value As ObservableCollection(Of TipoSector))
        '        SetProperty(_TiposSector, value, "TiposSector")
        '    End Set
        'End Property

        Public Shadows Function DataComGMT(data As DateTime?) As DateTime
            Dim dt As DateTime = Nothing
            If data Is Nothing Then
                dt = DateTime.UtcNow.AddMinutes(HusoHorarioEnMinutos)
            Else
                dt = data
            End If

            If AjusteHorarioVerano > 0 AndAlso
                (dt >= FechaHoraVeranoInicio AndAlso dt < FechaHoraVeranoFin.AddMinutes(HusoHorarioEnMinutos)) Then
                dt = dt.AddMinutes(HusoHorarioEnMinutos)
            End If

            Return dt

        End Function

        Public Property CodigoPais As String
            Get
                Return _CodigoPais
            End Get
            Set(value As String)
                SetProperty(_CodigoPais, value, "CodigoPais")
            End Set
        End Property

        Public ReadOnly Property GMT As String
            Get
                Try

                    Dim _gmt As String = String.Empty
                    Dim _HusoHorarioEnMinutos As Integer = Me.HusoHorarioEnMinutos
                    Dim _AjusteHorarioVerano As Integer = 0

                    ' Verifica se utilia Horario Verano
                    If Me.AjusteHorarioVerano > 0 AndAlso
                        (DateTime.Now >= Me.FechaHoraVeranoInicio AndAlso
                         DateTime.Now < Me.FechaHoraVeranoFin.AddMinutes(Me.HusoHorarioEnMinutos)) Then
                        _AjusteHorarioVerano = Me.AjusteHorarioVerano
                    End If

                    _HusoHorarioEnMinutos += AjusteHorarioVerano

                    Dim hora As Integer = _HusoHorarioEnMinutos / 60
                    Dim minutos = _HusoHorarioEnMinutos - (hora * 60)

                    _gmt = hora.ToString("00") & ":" & minutos.ToString("00")


                    If _HusoHorarioEnMinutos > 0 Then
                        _gmt = "+" + _gmt
                    End If

                    Return _gmt

                Catch ex As Exception
                    Return String.Empty
                End Try
            End Get
        End Property

#End Region

    End Class

End Namespace
