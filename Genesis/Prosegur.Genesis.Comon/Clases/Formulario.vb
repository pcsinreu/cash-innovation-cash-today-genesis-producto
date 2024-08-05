Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class Formulario
        Inherits BindableBase

        Sub New()  'we need a parameter-less constructor to make it serializable
        End Sub

#Region "Variaveis"

        Private _Identificador As String
        Private _AccionContable As AccionContable
        Private _GrupoTerminosIACIndividual As GrupoTerminosIAC
        Private _GrupoTerminosIACGrupo As GrupoTerminosIAC
        Private _FiltroFormulario As Transferencias.FiltroFormulario
        Private _TipoDocumento As TipoDocumento
        Private _Codigo As String
        Private _Descripcion As String
        Private _Color As System.Drawing.Color
        Private _Icono As Byte()
        Private _EstaActivo As Boolean
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _DescripcionCodigoExterno As String
        Private _Caracteristicas As List(Of Enumeradores.CaracteristicaFormulario)
        Private _TieneImpresion As Boolean

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

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
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
        Public Property AccionContable As AccionContable
            Get
                Return _AccionContable
            End Get
            Set(value As AccionContable)
                SetProperty(_AccionContable, value, "AccionContable")
            End Set
        End Property

        Public Property GrupoTerminosIACIndividual As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIACIndividual
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIACIndividual, value, "GrupoTerminosIACIndividual")
            End Set
        End Property

        Public Property GrupoTerminosIACGrupo As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIACGrupo
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIACGrupo, value, "GrupoTerminosIACGrupo")
            End Set
        End Property

        Public Property FiltroFormulario As Transferencias.FiltroFormulario
            Get
                Return _FiltroFormulario
            End Get
            Set(value As Transferencias.FiltroFormulario)
                SetProperty(_FiltroFormulario, value, "FiltroFormulario")
            End Set
        End Property

        Public Property TipoDocumento As TipoDocumento
            Get
                Return _TipoDocumento
            End Get
            Set(value As TipoDocumento)
                SetProperty(_TipoDocumento, value, "TipoDocumento")
            End Set
        End Property

        Public Property Color As System.Drawing.Color
            Get
                Return _Color
            End Get
            Set(value As System.Drawing.Color)
                SetProperty(_Color, value, "Color")
            End Set
        End Property

        Public Property Icono As Byte()
            Get
                Return _Icono
            End Get
            Set(value As Byte())
                SetProperty(_Icono, value, "Icono")
            End Set
        End Property

        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
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
        Public Property DescripcionCodigoExterno As String
            Get
                Return _DescripcionCodigoExterno
            End Get
            Set(value As String)
                SetProperty(_DescripcionCodigoExterno, value, "DescripcionCodigoExterno")
            End Set
        End Property

        Public Property Caracteristicas As List(Of Enumeradores.CaracteristicaFormulario)
            Get
                Return _Caracteristicas
            End Get
            Set(value As List(Of Enumeradores.CaracteristicaFormulario))
                SetProperty(_Caracteristicas, value, "Caracteristicas")
            End Set
        End Property

        Public Property TieneImpresion As Boolean
            Get
                Return _TieneImpresion
            End Get
            Set(value As Boolean)
                SetProperty(_TieneImpresion, value, "TieneImpresion")
            End Set
        End Property

        Public ReadOnly Property CodigoDescripcion As String
            Get
                Return $"{Codigo} - {Descripcion}"
            End Get
        End Property
        Public Property Accion As String

#End Region

    End Class

End Namespace
