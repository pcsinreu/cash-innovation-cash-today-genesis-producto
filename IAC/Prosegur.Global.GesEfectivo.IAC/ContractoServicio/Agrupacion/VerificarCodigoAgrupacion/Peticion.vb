Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.VerificarCodigoAgrupacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoAgrupacion")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoAgrupacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoAgrupacion As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoAgrupacion() As String
            Get
                Return _CodigoAgrupacion
            End Get
            Set(value As String)
                _CodigoAgrupacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace