Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoCaracteristica

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoCaracteristica")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoCaracteristica")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

#End Region
    End Class
End Namespace