Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametrosValues

    <XmlType(Namespace:="urn:GetParametrosValues")> _
    <XmlRoot(Namespace:="urn:GetParametrosValues")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoDelegacion As String
        Private _CodigoAplicacion As String
        Private _CodigoPuesto As String
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

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
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

#End Region

    End Class
End Namespace
