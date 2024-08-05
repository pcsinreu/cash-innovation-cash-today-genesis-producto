Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Canal
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Canal
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidCanal As String
        Private _codCanal As String
        Private _desCanal As String
        Private _obsCanal As String
        Private _bolVigente As Boolean
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

        Private _subCanais As New List(Of SubCanal)

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidCanal As String
            Get
                Return _oidCanal
            End Get
            Set(value As String)
                _oidCanal = value
            End Set
        End Property

        Public Property CodCanal As String
            Get
                Return _codCanal
            End Get
            Set(value As String)
                _codCanal = value
            End Set
        End Property

        Public Property DesCanal As String
            Get
                Return _desCanal
            End Get
            Set(value As String)
                _desCanal = value
            End Set
        End Property

        Public Property ObsCanal As String
            Get
                Return _obsCanal
            End Get
            Set(value As String)
                _obsCanal = value
            End Set
        End Property

        Public Property BolVigente As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property CodUsuario As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property FyhActualizacion As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

        Public Property SubCanais As List(Of SubCanal)
            Get
                Return _subCanais
            End Get
            Set(value As List(Of SubCanal))
                _subCanais = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(OidCanal As String, CodCanal As String, DesCanal As String)

            _oidCanal = OidCanal
            _codCanal = CodCanal
            _desCanal = DesCanal

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém valores para preencher uma combo 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 04/02/2011 - Criado 
        ''' </history>
        Public Function ObtenerCombo() As List(Of Canal)

            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta

            ' obtém morfologias
            objRespuesta = objProxy.GetComboCanales()

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToList(objRespuesta.Canales)

            Else

                ' se houve erros, retorna lista vazia
                Return New List(Of Canal)

            End If

        End Function

        Public Function ConvertToList(Canais As ContractoServicio.Utilidad.GetComboCanales.CanalColeccion) As List(Of Canal)

            Dim lista As New List(Of Canal)
            Dim item As Canal

            If Canais Is Nothing Then
                Return lista
            End If

            For Each canal In Canais

                item = New Canal

                With item
                    .CodCanal = canal.Codigo
                    .DesCanal = canal.Descripcion
                End With

                lista.Add(item)

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
