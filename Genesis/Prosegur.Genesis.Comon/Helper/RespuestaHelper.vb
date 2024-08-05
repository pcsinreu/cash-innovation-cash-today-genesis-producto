Imports Prosegur.Genesis.Comon

''' <summary>
''' Classe contratual do resposta Controle Helper.
''' </summary>
''' <history>
''' [Thiago Dias] 22/08/2013 - Criado.
'''</history>
<Serializable()>
Public Class RespuestaHelper
    Inherits BaseRespuestaPaginacion

    ''' <summary>
    ''' Valida se ocorreram modificação nos dados de Resposta Helper.
    ''' </summary>
    Public Property ResultadoModificado As Boolean

    ''' <summary>
    ''' Obtém e Define Lista de dados do tipo Respuesta Helper.
    ''' </summary>
    Public Property DatosRespuesta As List(Of Helper.Respuesta)
End Class

Namespace Helper

    ''' <summary>
    ''' Classe responsável por gerenciar o Tipo de Resposta do Controle Helper.
    ''' </summary>
    <Serializable()>
    Public Class Respuesta

        Private _Identificador As String
        Private _Codigo As String
        Private _Descricao As String
        Private _IdentificadorPai As String

        ''' <summary>
        ''' Obtém e Define Código Identificador.
        ''' </summary>
        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define Código.
        ''' </summary>
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define Descrição.
        ''' </summary>
        Public Property Descricao As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property

        ''' <summary>
        ''' Obtém e Define valor do Identificador Pai.
        ''' </summary>
        Public Property IdentificadorPai As String
            Get
                Return _IdentificadorPai
            End Get
            Set(value As String)
                _IdentificadorPai = value
            End Set
        End Property

    End Class
End Namespace