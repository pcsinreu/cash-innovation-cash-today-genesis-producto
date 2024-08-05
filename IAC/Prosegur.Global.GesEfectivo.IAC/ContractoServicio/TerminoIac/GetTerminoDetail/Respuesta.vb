Imports System.Xml.Serialization
Imports System.Xml


Namespace TerminoIac.GetTerminoDetailIac
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 13/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTerminoDetailIac")> _
    <XmlRoot(Namespace:="urn:GetTerminoDetailIac")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _terminoDetailIacCol As TerminoDetailIacColeccion
#End Region

#Region "Propriedades"

        Public Property TerminosDetailIac() As TerminoDetailIacColeccion
            Get
                Return _terminoDetailIacCol
            End Get
            Set(value As TerminoDetailIacColeccion)
                _terminoDetailIacCol = value
            End Set
        End Property

#End Region

    End Class

End Namespace
