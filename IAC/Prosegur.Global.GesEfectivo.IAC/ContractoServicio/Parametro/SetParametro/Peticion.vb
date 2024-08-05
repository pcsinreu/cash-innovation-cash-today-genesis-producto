Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.SetParametro

    <XmlType(Namespace:="urn:SetParametro")> _
    <XmlRoot(Namespace:="urn:SetParametro")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _CodigoParametro As String
        Private _DescripcionCortaParametro As String
        Private _DescripcionLargaParametro As String
        Private _NecOrden As Nullable(Of Integer)
        Private _DescripcionAgrupacion As String
        Private _CodigoUsuario As String
        Private _ParametroOpciones As Parametro.GetParametroOpciones.OpcionColeccion
#End Region

#Region "Propriedades"
        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property CodigoNivel() As String
            Get
                Return _CodigoNivel
            End Get
            Set(value As String)
                _CodigoNivel = value
            End Set
        End Property

        Public Property CodigoParametro() As String
            Get
                Return _CodigoParametro
            End Get
            Set(value As String)
                _CodigoParametro = value
            End Set
        End Property

        Public Property DescripcionCortaParametro() As String
            Get
                Return _DescripcionCortaParametro
            End Get
            Set(value As String)
                _DescripcionCortaParametro = value
            End Set
        End Property

        Public Property DescripcionLargaParametro() As String
            Get
                Return _DescripcionLargaParametro
            End Get
            Set(value As String)
                _DescripcionLargaParametro = value
            End Set
        End Property

        Public Property NecOrden() As Nullable(Of Integer)
            Get
                Return _NecOrden
            End Get
            Set(value As Nullable(Of Integer))
                _NecOrden = value
            End Set
        End Property

        Public Property DescripcionAgrupacion() As String
            Get
                Return _DescripcionAgrupacion
            End Get
            Set(value As String)
                _DescripcionAgrupacion = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        Public Property ParametroOpciones() As Parametro.GetParametroOpciones.OpcionColeccion
            Get
                Return _ParametroOpciones
            End Get
            Set(value As Parametro.GetParametroOpciones.OpcionColeccion)
                _ParametroOpciones = value
            End Set
        End Property
#End Region

    End Class
End Namespace
