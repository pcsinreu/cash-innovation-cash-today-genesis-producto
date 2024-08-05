Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoPuntosServicio.getTiposPuntosServicio

    <XmlType(Namespace:="urn:getTiposPuntosServicio")> _
    <XmlRoot(Namespace:="urn:getTiposPuntosServicio")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoPuntoServicio As String

        Private _desTipoPuntoServicio As String

        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoPuntoServicio() As String
            Get
                Return _codTipoPuntoServicio
            End Get
            Set(value As String)
                _codTipoPuntoServicio = value
            End Set
        End Property

        Public Property desTipoPuntoServicio() As String
            Get
                Return _desTipoPuntoServicio
            End Get
            Set(value As String)
                _desTipoPuntoServicio = value
            End Set
        End Property

        Public Property bolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property

#End Region

    End Class

End Namespace

