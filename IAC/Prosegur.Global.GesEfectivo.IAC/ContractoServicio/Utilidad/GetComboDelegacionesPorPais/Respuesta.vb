Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboDelegacionesPorPais

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/04/12 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboDelegacionesPorPais")> _
    <XmlRoot(Namespace:="urn:GetComboDelegacionesPorPais")> _
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