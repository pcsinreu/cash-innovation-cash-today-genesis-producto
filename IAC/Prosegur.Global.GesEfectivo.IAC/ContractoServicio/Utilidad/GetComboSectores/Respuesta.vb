Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboSectores

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
   
    <XmlType(Namespace:="urn:GetComboSectores")> _
    <XmlRoot(Namespace:="urn:GetComboSectores")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "[Variáveis]"

        Private _Sectores As SectorColeccion

#End Region

#Region "[Propriedades]"

        Public Property Sectores() As SectorColeccion
            Get
                Return _Sectores
            End Get
            Set(value As SectorColeccion)
                _Sectores = value
            End Set
        End Property

#End Region

    End Class

End Namespace