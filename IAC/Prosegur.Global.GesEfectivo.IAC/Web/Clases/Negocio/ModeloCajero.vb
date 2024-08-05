Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio modelo de cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class ModeloCajero
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidModeloCajero As String
        Private _codModeloCajero As String
        Private _desModeloCajero As String
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidModeloCajero() As String
            Get
                Return _oidModeloCajero
            End Get
            Set(value As String)
                _oidModeloCajero = value
            End Set
        End Property

        Public Property CodModeloCajero() As String
            Get
                Return _codModeloCajero
            End Get
            Set(value As String)
                _codModeloCajero = value
            End Set
        End Property

        Public Property DesModeloCajero() As String
            Get
                Return _desModeloCajero
            End Get
            Set(value As String)
                _desModeloCajero = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)

            _oidModeloCajero = Oid
            _codModeloCajero = Codigo
            _desModeloCajero = Descripcion

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém modelos de cajero através do serviço Utilidad.
        ''' Preenche apenas OidModeloCajero, CodModeloCajero e DesModeloCajero dos objetos ModeloCajero da lista.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  12/01/2011  criado
        ''' </history>
        Public Function ObtenerCombo() As List(Of ModeloCajero)

            Dim modelos As New List(Of ModeloCajero)
            Dim modelo As ModeloCajero
            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboModelosCajero.Respuesta

            ' executa operação do serviço
            objRespuesta = objProxy.GetComboModelosCajero()

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                For Each item In objRespuesta.ModelosCajero

                    ' preenche objeto modelo
                    modelo = New ModeloCajero
                    With modelo
                        .OidModeloCajero = item.OidModeloCajero
                        .CodModeloCajero = item.CodigoModeloCajero
                        .DesModeloCajero = item.DescripcionModeloCajero
                    End With

                    ' adiciona a lista de retorno
                    modelos.Add(modelo)

                Next

            End If

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            Return modelos

        End Function

#End Region

    End Class

End Namespace
