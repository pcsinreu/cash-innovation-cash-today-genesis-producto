Imports System.Xml.Serialization
Imports System.Xml

Namespace TerminoIac.VerificarDescripcionTermino

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionTermino")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionTermino")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Descripcion As String

#End Region

#Region "[Propriedades]"

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

#End Region

    End Class

End Namespace