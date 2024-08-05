Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis.Comon

Namespace MAE.SetMAE

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:SetMAE")> _
    <XmlRoot(Namespace:="urn:SetMAE")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"
        Private _OidMaquina As String
        Private _AsociaPlan As Boolean
#End Region

#Region "Propriedades"

        Public Property OidMaquina() As String
            Get
                Return _OidMaquina
            End Get
            Set(value As String)
                _OidMaquina = value
            End Set
        End Property

        Public Property AsociaPlan() As Boolean
            Get
                Return _AsociaPlan
            End Get
            Set(value As Boolean)
                _AsociaPlan = value
            End Set
        End Property

#End Region

    End Class
End Namespace

