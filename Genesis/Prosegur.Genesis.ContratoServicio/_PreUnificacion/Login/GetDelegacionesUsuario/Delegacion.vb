Imports System.Xml.Serialization

Namespace Login.GetDelegacionesUsuario

    ''' <summary>
    ''' Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra]  07/03/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class Delegacion

#Region " Variáveis "

        Private _Codigo As String
        Private _Descricao As String


#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descricao() As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property

#End Region

    End Class

End Namespace