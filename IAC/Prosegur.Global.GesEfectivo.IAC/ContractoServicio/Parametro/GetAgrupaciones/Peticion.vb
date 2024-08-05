Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetAgrupaciones

    <XmlType(Namespace:="urn:GetAgrupaciones")> _
    <XmlRoot(Namespace:="urn:GetAgrupaciones")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _DesAgrupacion As String
        Private _Permisos As List(Of String)
        Private _Aplicaciones As List(Of Aplicacion)
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

        Public Property DesAgrupacion() As String
            Get
                Return _DesAgrupacion
            End Get
            Set(value As String)
                _DesAgrupacion = value
            End Set
        End Property

        Public Property Permisos() As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property

        Public Property Aplicaciones() As List(Of Aplicacion)
            Get
                Return _Aplicaciones
            End Get
            Set(value As List(Of Aplicacion))
                _Aplicaciones = value
            End Set
        End Property
#End Region

    End Class
End Namespace
