Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.GetAgrupacionesDetail

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetAgrupacionesDetail")> _
    <XmlRoot(Namespace:="urn:GetAgrupacionesDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Agrupaciones As AgrupacionColeccion

#End Region

#Region "[Propriedades]"

        Public Property Agrupaciones() As AgrupacionColeccion
            Get
                Return _Agrupaciones
            End Get
            Set(value As AgrupacionColeccion)
                _Agrupaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace