Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametroOpciones

    <XmlType(Namespace:="urn:GetParametroOpciones")> _
    <XmlRoot(Namespace:="urn:GetParametroOpciones")> _
    <Serializable()> _
    Public Class Peticion
#Region "Variáveis"
        Private _CodigoAplicacion As String
        Private _CodigoParametro As String
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

        Public Property CodigoParametro() As String
            Get
                Return _CodigoParametro
            End Get
            Set(value As String)
                _CodigoParametro = value
            End Set
        End Property
#End Region

    End Class
End Namespace
