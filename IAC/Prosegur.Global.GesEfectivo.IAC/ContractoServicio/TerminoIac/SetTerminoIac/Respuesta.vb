Imports System.Xml.Serialization
Imports System.Xml


Namespace TerminoIac.SetTerminoIac
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetTerminoIac")> _
    <XmlRoot(Namespace:="urn:SetTerminoIac")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _CodigoTermino As String
        Private _DescricaoTermino As String
#End Region

#Region "Propriedades"

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property DescricaoTermino() As String
            Get
                Return _DescricaoTermino
            End Get
            Set(value As String)
                _DescricaoTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace
