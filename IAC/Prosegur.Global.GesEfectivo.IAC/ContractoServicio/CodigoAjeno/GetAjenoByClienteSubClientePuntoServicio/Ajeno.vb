
Namespace CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 19/07/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class Ajeno

#Region "[VARIAVEIS]"

        Private _CodAjeno As String
        Private _DesAjeno As String
        Private _BolDefecto As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodAjeno() As String
            Get
                Return _CodAjeno
            End Get
            Set(value As String)
                _CodAjeno = value
            End Set
        End Property

        Public Property DesAjeno() As String
            Get
                Return _DesAjeno
            End Get
            Set(value As String)
                _DesAjeno = value
            End Set
        End Property

        Public Property BolDefecto() As Boolean
            Get
                Return _BolDefecto
            End Get
            Set(value As Boolean)
                _BolDefecto = value
            End Set
        End Property
#End Region

    End Class
End Namespace
