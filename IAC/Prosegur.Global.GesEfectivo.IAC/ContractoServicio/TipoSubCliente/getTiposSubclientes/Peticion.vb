Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSubCliente.getTiposSubclientes

    <XmlType(Namespace:="urn:getTiposSubclientes")> _
    <XmlRoot(Namespace:="urn:getTiposSubclientes")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoSubcliente As String
        Private _desTipoSubcliente As String
        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoSubcliente() As String
            Get
                Return _codTipoSubcliente
            End Get
            Set(value As String)
                _codTipoSubcliente = value
            End Set
        End Property

        Public Property desTipoSubcliente() As String
            Get
                Return _desTipoSubcliente
            End Get
            Set(value As String)
                _desTipoSubcliente = value
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
