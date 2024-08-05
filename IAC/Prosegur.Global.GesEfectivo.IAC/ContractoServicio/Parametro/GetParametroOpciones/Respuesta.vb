Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametroOpciones

    <XmlType(Namespace:="urn:GetParametroOpciones")> _
    <XmlRoot(Namespace:="urn:GetParametroOpciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Opciones As OpcionColeccion
#End Region

#Region "Propriedades"

        Public Property Opciones As OpcionColeccion
            Get
                Return _Opciones
            End Get
            Set(value As OpcionColeccion)
                _Opciones = value
            End Set
        End Property
#End Region

    End Class
End Namespace