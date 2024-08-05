Imports System.Xml.Serialization
Imports System.Xml


Namespace Delegacion.VerificaCodigoDelegacion

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 15/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaCodigoDelegacion")> _
    <XmlRoot(Namespace:="urn:VerificaCodigoDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String
#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region

    End Class
End Namespace
