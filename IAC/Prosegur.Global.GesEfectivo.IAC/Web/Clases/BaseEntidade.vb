
Namespace Negocio

    ''' <summary>
    ''' Classe de negócio base
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public MustInherit Class BaseEntidade

#Region "[ENUMERAÇÕES]"

        Public Enum eAcao
            Alta
            Modificacion
            Baja
        End Enum

#End Region

#Region "[VARIÁVEIS]"

        Private _acao As eAcao
        Private _respuesta As ContractoServicio.RespuestaGenerico

#End Region

#Region "[PROPRIEDADES]"

        Public Property Acao() As eAcao
            Get
                Return _acao
            End Get
            Set(value As eAcao)
                _acao = value
            End Set
        End Property

        ''' <summary>
        ''' Armazena a mensagem de resposta do último serviço executado
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Respuesta() As ContractoServicio.RespuestaGenerico
            Get
                Return _respuesta
            End Get
            Set(value As ContractoServicio.RespuestaGenerico)
                _respuesta = value
            End Set
        End Property

#End Region

#Region "[MÉTODOS]"


#End Region
        
    End Class

End Namespace