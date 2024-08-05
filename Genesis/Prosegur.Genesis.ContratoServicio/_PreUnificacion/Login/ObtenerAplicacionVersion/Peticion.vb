Imports System.Xml.Serialization

Namespace Login.ObtenerAplicacionVersion

    ''' <summary>
    ''' Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 09/05/2012 Criado
    ''' </history>

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerAplicacionVersion")> _
    <XmlRoot(Namespace:="urn:ObtenerAplicacionVersion")> _
    Public Class Peticion

#Region " Variáveis "
        Private _CodigoAplicacion As String
        Private _CodigoVersion As String
#End Region

#Region "Propriedades"

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property CodigoVersion() As String
            Get
                Return _CodigoVersion
            End Get
            Set(value As String)
                _CodigoVersion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
