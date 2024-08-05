Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboDelegaciones

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboDelegaciones")> _
    <XmlRoot(Namespace:="urn:GetComboDelegaciones")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Delegaciones As DelegacionColeccion

#End Region

#Region "[Propriedades]"

        Public Property Delegaciones() As DelegacionColeccion
            Get
                Return _Delegaciones
            End Get
            Set(value As DelegacionColeccion)
                _Delegaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace