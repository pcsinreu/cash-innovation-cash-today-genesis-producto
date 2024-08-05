Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarDescripcionCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danienines] 13/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionCliente")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _DesCliente As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property DesCliente() As String
            Get
                Return _DesCliente
            End Get
            Set(value As String)
                _DesCliente = value
            End Set
        End Property

#End Region
    End Class
End Namespace