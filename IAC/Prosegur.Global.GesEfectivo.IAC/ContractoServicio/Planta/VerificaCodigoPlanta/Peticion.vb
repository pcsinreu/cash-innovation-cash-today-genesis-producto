Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.VerificaCodigoPlanta
    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificaCodigoPlanta")> _
    <XmlRoot(Namespace:="urn:VerificaCodigoPlanta")> _
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
