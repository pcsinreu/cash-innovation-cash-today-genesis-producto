Imports System.Xml.Serialization
Imports System.Xml

Namespace Divisa.GetDivisas

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDivisas")> _
    <XmlRoot(Namespace:="urn:GetDivisas")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoIso As List(Of String)
        Private _Descripcion As List(Of String)
        Private _Vigente As Boolean

#End Region

#Region "[Propriedades]"

        Public Property CodigoIso() As List(Of String)
            Get
                Return _CodigoIso
            End Get
            Set(value As List(Of String))
                _CodigoIso = value
            End Set
        End Property

        Public Property Descripcion() As List(Of String)
            Get
                Return _Descripcion
            End Get
            Set(value As List(Of String))
                _Descripcion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace