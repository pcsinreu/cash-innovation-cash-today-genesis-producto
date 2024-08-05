Imports System.Xml.Serialization
Imports System.Xml

Namespace Parametro.GetParametrosValues

    <XmlType(Namespace:="urn:GetParametrosValues")> _
    <XmlRoot(Namespace:="urn:GetParametrosValues")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _Niveles As NivelColeccion
#End Region

#Region "Propriedades"

        Public Property Niveles() As NivelColeccion
            Get
                Return _Niveles
            End Get
            Set(value As NivelColeccion)
                _Niveles = value
            End Set
        End Property

#End Region

    End Class
End Namespace