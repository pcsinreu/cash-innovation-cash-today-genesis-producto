Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.SetParametrosValues

    <XmlType(Namespace:="urn:SetParametrosValues")> _
    <XmlRoot(Namespace:="urn:SetParametrosValues")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"

        Private _CodigoDelegacion As String
        Private _CodigoPuesto As String
        Private _CodigoAplicacion As String
        Private _CodigoUsuario As String
        Private _Parametros As ParametroColeccion

#End Region

#Region "Propriedades"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoPuesto() As String
            Get
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
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

        Public Property Parametros() As ParametroColeccion
            Get
                Return _Parametros
            End Get
            Set(value As ParametroColeccion)
                _Parametros = value
            End Set
        End Property

#End Region

    End Class
End Namespace
