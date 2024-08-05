Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarCentroProceso

    ''' <summary>
    ''' Recuperar centro de proceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 29/04/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:RecuperarCentroProceso")> _
    <XmlRoot(Namespace:="urn:RecuperarCentroProceso")> _
    <Serializable()> _
    Public Class Peticion

#Region "Variáveis"

        Private _CodigoDelegacion As String

#End Region

#Region "Propriedades"

        Public Property CodigoDelegacion() As String

            Get
                Return _CodigoDelegacion
            End Get

            Set(value As String)
                _CodigoDelegacion = value
            End Set

        End Property

#End Region

    End Class

End Namespace