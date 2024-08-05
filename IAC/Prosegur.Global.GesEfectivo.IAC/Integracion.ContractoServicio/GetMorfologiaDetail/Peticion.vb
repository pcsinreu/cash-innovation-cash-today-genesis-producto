Imports System.Xml.Serialization
Imports System.Xml

Namespace GetMorfologiaDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetMorfologiaDetail")> _
    <XmlRoot(Namespace:="urn:GetMorfologiaDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _oidMorfologia As String

#End Region

#Region "[Propriedades]"

        Public Property OidMorfologia() As String
            Get
                Return _oidMorfologia
            End Get
            Set(value As String)
                _oidMorfologia = value
            End Set
        End Property

#End Region

    End Class

End Namespace