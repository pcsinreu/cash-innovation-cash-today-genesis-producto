Imports System.Xml.Serialization
Imports System.Xml

Namespace Pais.GetPaisDetail
    <Serializable()> _
    Public Class Pais

#Region "[VARIAVEIS]"

        Private _OidPais As String
        Private _CodPais As String
        Private _Description As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property OidPais As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property CodPais() As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property


#End Region

    End Class
End Namespace
