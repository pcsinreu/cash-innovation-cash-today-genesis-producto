Imports System.Xml.Serialization
Imports System.Xml

Namespace TiposProcesado.SetTiposProcesado

    <XmlType(Namespace:="urn:SetTiposProcesado")> _
    <XmlRoot(Namespace:="urn:SetTiposProcesado")> _
    <Serializable()> _
   Public Class Peticion

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Nullable(Of Boolean)
        Private _codUsuario As String
        Private _Caracteristicas As CaracteristicaColeccion

#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property Caracteristicas() As CaracteristicaColeccion
            Get
                Return _Caracteristicas
            End Get
            Set(value As CaracteristicaColeccion)
                _Caracteristicas = value
            End Set
        End Property

#End Region

    End Class
End Namespace