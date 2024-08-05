Imports System.Xml.Serialization
Imports System.Xml

Namespace Agrupacion.VerificarDescripcionAgrupacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionAgrupacion")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionAgrupacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _DescripcionAgrupacion As String

#End Region

#Region "[Propriedades]"

        Public Property DescripcionAgrupacion() As String
            Get
                Return _DescripcionAgrupacion
            End Get
            Set(value As String)
                _DescripcionAgrupacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace