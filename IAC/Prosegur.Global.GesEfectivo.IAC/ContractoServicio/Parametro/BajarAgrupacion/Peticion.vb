Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.BajarAgrupacion

    <XmlType(Namespace:="urn:BajarAgrupacion")> _
    <XmlRoot(Namespace:="urn:BajarAgrupacion")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _DesAgrupacion As String
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
#End Region

    End Class
End Namespace
