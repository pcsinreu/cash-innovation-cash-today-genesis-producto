Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetDivisasMedioPago

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDivisasMedioPago")> _
    <XmlRoot(Namespace:="urn:GetDivisasMedioPago")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Divisas As DivisaColeccion

#End Region

#Region "[Propriedades]"

        Public Property Divisas() As DivisaColeccion
            Get
                Return _Divisas
            End Get
            Set(value As DivisaColeccion)
                _Divisas = value
            End Set
        End Property

#End Region

    End Class

End Namespace