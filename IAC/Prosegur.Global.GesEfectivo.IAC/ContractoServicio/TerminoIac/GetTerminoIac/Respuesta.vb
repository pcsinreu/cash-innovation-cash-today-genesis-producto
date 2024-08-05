Imports System.Xml.Serialization
Imports System.Xml


Namespace TerminoIac.GetTerminoIac
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTerminoIac")> _
    <XmlRoot(Namespace:="urn:GetTerminoIac")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _terminoIacCol As TerminoIacColeccion
#End Region

#Region "Propriedades"

        Public Property TerminosIac() As TerminoIacColeccion
            Get
                Return _terminoIacCol
            End Get
            Set(value As TerminoIacColeccion)
                _terminoIacCol = value
            End Set
        End Property

#End Region

    End Class

End Namespace
