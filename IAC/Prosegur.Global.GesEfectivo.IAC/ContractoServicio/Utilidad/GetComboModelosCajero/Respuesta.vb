Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboModelosCajero

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/01/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboModelosCajero")> _
    <XmlRoot(Namespace:="urn:GetComboModelosCajero")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _modelosCajero As List(Of ModeloCajero)

#End Region

#Region "[Propriedades]"

        Public Property ModelosCajero() As List(Of ModeloCajero)
            Get
                Return _modelosCajero
            End Get
            Set(value As List(Of ModeloCajero))
                _modelosCajero = value
            End Set
        End Property

#End Region

    End Class

End Namespace