Imports System.Xml.Serialization
Imports System.Xml

Namespace ImporteMaximo.GetImporteMaximo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetImporteMaximo")> _
    <XmlRoot(Namespace:="urn:GetImporteMaximo")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[Variáveis]"

        Private _ImporteMaximo As ImporteMaximo

#End Region

#Region "[Propriedades]"

        Public Property ImporteMaximo() As ImporteMaximo
            Get
                Return _ImporteMaximo
            End Get
            Set(value As ImporteMaximo)
                _ImporteMaximo = value
            End Set
        End Property


#End Region

    End Class

End Namespace