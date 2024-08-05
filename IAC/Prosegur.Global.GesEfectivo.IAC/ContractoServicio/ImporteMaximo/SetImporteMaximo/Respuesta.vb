Imports System.Xml.Serialization
Imports System.Xml

Namespace ImporteMaximo.SetImporteMaximo

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:SetImporteMaximo")> _
    <XmlRoot(Namespace:="urn:SetImporteMaximo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _ImportesMaximo As ImporteMaximoRespuestaColeccion

#End Region

#Region "[Propriedades]"

        Public Property ImportesMaximo() As ImporteMaximoRespuestaColeccion
            Get
                Return _ImportesMaximo
            End Get
            Set(value As ImporteMaximoRespuestaColeccion)
                _ImportesMaximo = value
            End Set
        End Property


#End Region


    End Class

End Namespace
