Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.VerificaCodigoPaisDelegacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaCodigoPaisDelegacion")> _
    <XmlRoot(Namespace:="urn:VerificaCodigoPaisDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String
        Private _CodPais As String
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

        Public Property Pais() As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

#End Region
    End Class
End Namespace
