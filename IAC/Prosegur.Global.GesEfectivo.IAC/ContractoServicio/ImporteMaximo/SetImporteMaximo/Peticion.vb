Imports System.Xml.Serialization
Imports System.Xml

Namespace ImporteMaximo.SetImporteMaximo

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetImporteMaximo")> _
    <XmlRoot(Namespace:="urn:SetImporteMaximo")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _ImportesMaximo As ImporteMaximoColeccion

#End Region

#Region "[Propriedades]"

        Public Property ImportesMaximo() As ImporteMaximoColeccion
            Get
                Return _ImportesMaximo
            End Get
            Set(value As ImporteMaximoColeccion)
                _ImportesMaximo = value
            End Set
        End Property


#End Region

    End Class

End Namespace