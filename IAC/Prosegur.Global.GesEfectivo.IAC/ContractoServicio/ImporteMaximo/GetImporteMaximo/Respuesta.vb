Imports System.Xml.Serialization
Imports System.Xml

Namespace ImporteMaximo.GetImporteMaximo

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:GetImporteMaximo")> _
    <XmlRoot(Namespace:="urn:GetImporteMaximo")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[Variáveis]"

        Private _EntidadImporteMaximo As EntidadImporteMaximoColeccion

#End Region

#Region "[Propriedades]"

        Public Property EntidadImporteMaximo() As EntidadImporteMaximoColeccion
            Get
                Return _EntidadImporteMaximo
            End Get
            Set(value As EntidadImporteMaximoColeccion)
                _EntidadImporteMaximo = value
            End Set
        End Property


#End Region


    End Class

End Namespace
