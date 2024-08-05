Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.SetTiposSectores

    <XmlType(Namespace:="urn:SetTiposSectores")> _
    <XmlRoot(Namespace:="urn:SetTiposSectores")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _oidTipoSector As String
        Private _codTipoSector As String
        Private _desTipoSector As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As Date
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Date
        Private _desUsuarioModificacion As String
        Private _codCaractTipoSector As CaracteristicaColeccion
        Private _CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

#End Region

#Region "[PROPRIEDADE]"

        Public Property oidTipoSector() As String
            Get
                Return _oidTipoSector
            End Get
            Set(value As String)
                _oidTipoSector = value
            End Set
        End Property

        Public Property codTipoSector() As String
            Get
                Return _codTipoSector
            End Get
            Set(value As String)
                _codTipoSector = value
            End Set
        End Property

        Public Property desTipoSector() As String
            Get
                Return _desTipoSector
            End Get
            Set(value As String)
                _desTipoSector = value
            End Set
        End Property

        Public Property bolActivo() As Boolean
            Get
                Return _bolActivo
            End Get
            Set(value As Boolean)
                _bolActivo = value
            End Set
        End Property

        Public Property gmtCreacion() As Date
            Get
                Return _gmtCreacion
            End Get
            Set(value As Date)
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion() As Date
            Get
                Return _gmtModificacion
            End Get
            Set(value As Date)
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

        Public Property codCaractTipoSector() As CaracteristicaColeccion
            Get
                Return _codCaractTipoSector
            End Get
            Set(value As CaracteristicaColeccion)
                _codCaractTipoSector = value
            End Set
        End Property

        Public Property CodigosAjenos() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigoAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigoAjeno = value
            End Set
        End Property

#End Region

    End Class
End Namespace
