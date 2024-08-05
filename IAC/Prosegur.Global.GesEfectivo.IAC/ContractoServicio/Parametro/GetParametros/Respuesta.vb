Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametros

    <XmlType(Namespace:="urn:GetParametros")> _
    <XmlRoot(Namespace:="urn:GetParametros")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Parametros As ParametroColeccion
#End Region

#Region "Propriedades"
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