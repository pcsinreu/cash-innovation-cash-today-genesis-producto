Imports System.Windows
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Excepcion
Imports Prosegur.Genesis.ContractoServicio

Public Class EntidadeBase

#Region "[Variáveis]"

    Private _erroServico As NegocioExcepcion
    Private _respostaMessageBox As Windows.MessageBoxResult
    Private _exibirMsgGravar As Boolean = False

#End Region

#Region "[Propriedades]"

    ''' <summary>
    ''' Retorno do messagebox de erros ou validações
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Public Property RespostaMessageBox As Windows.MessageBoxResult
        Get
            Return _respostaMessageBox
        End Get
        Set(value As Windows.MessageBoxResult)
            _respostaMessageBox = value
        End Set
    End Property

    <XmlIgnoreAttribute()> _
    Public Property ErroServico() As NegocioExcepcion
        Get
            Return _erroServico
        End Get
        Set(value As NegocioExcepcion)
            _erroServico = value
        End Set
    End Property

    Public Property ExibirMsgGravar() As Boolean
        Get
            Return _exibirMsgGravar
        End Get
        Set(value As Boolean)
            _exibirMsgGravar = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' trata erros retornados por uma operação do serviço que altera alguma informação na base de dados
    ''' </summary>
    ''' <returns>True = não ocorreu erros</returns>
    ''' <param name="Respuesta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  15/12/2010  criado
    ''' </history>
    Public Function TratarRespuestaGrabar(Respuesta As RespuestaGenerico) As Boolean

        If TratarRespuestaServicio(Respuesta) Then

            _exibirMsgGravar = True

            Return True

        End If

        Return False

    End Function

    ' ''' <summary>
    ' ''' trata erros retornados por uma operação do serviço
    ' ''' </summary>
    ' ''' <returns>True = não ocorreu erros</returns>
    ' ''' <param name="Respuesta"></param>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' [bruno.costa]  15/12/2010  criado
    ' ''' </history>
    'Public Function TratarRespuestaServicio(Respuesta As IAC.Integracion.ContractoServicio.RespuestaGenerico) As Boolean

    '    If Respuesta IsNot Nothing AndAlso Respuesta.CodigoError <> Prosegur.Global.GesEfectivo.ATM.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

    '        ' se ocorreu erro, retorna exceção e finaliza
    '        _erroServico = New Excepcion.NegocioExcepcion(Respuesta.CodigoError, Respuesta.MensajeError)

    '        Return False

    '    End If

    '    Return True

    'End Function

    ''' <summary>
    ''' trata erros retornados por uma operação do serviço
    ''' </summary>
    ''' <returns>True = não ocorreu erros</returns>
    ''' <param name="Respuesta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  15/12/2010  criado
    ''' </history>
    Public Function TratarRespuestaServicio(Respuesta As RespuestaGenerico) As Boolean

        If Respuesta IsNot Nothing AndAlso Respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

            ' se ocorreu erro, retorna exceção e finaliza
            _erroServico = New Excepcion.NegocioExcepcion(Respuesta.CodigoError, Respuesta.MensajeError)

            Return False

        End If

        Return True

    End Function

#End Region

#Region "[CONSTRUTOR]"

    Public Sub New()

        _erroServico = New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT, String.Empty)

    End Sub

#End Region

End Class
