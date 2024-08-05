Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboPais
Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboPais

    ''' <summary>
    ''' Classe Pais
    ''' </summary>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Pais

        Private _OidPais As String
        Private _Codigo As String
        Private _Description As String

        Public Property OidPais() As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

    End Class

End Namespace