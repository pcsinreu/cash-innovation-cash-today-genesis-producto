Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.SetDivisasDenominaciones

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetDivisasDenominaciones")> _
    <XmlRoot(Namespace:="urn:SetDivisasDenominaciones")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Divisas As DivisaColeccion
        Private _CodigoUsuario As String
        Public Property CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

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

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace