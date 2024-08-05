Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametros

    <XmlType(Namespace:="urn:GetParametros")> _
    <XmlRoot(Namespace:="urn:GetParametros")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"


        Private _CodigoAplicacion As String
        Private _CodigoNivel As String
        Private _DesCortaAgrupacion As String
        Private _Permisos As List(Of String)
        Private _CodParametro As String
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

        Public Property DesCortaAgrupacion As String
            Get
                Return _DesCortaAgrupacion
            End Get
            Set(value As String)
                _DesCortaAgrupacion = value
            End Set
        End Property

        Public Property Permisos As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property

        Public Property CodParametro() As String
            Get
                Return _CodParametro
            End Get
            Set(value As String)
                _CodParametro = value
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
