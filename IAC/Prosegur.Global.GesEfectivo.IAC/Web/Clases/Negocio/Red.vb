Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Red
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class Red
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidRed As String
        Private _codRed As String
        Private _desRed As String
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidRed() As String
            Get
                Return _oidRed
            End Get
            Set(value As String)
                _oidRed = value
            End Set
        End Property

        Public Property CodRed() As String
            Get
                Return _codRed
            End Get
            Set(value As String)
                _codRed = value
            End Set
        End Property

        Public Property DesRed() As String
            Get
                Return _desRed
            End Get
            Set(value As String)
                _desRed = value
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

            _oidRed = Oid
            _codRed = Codigo
            _desRed = Descripcion

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém redes através do serviço Utilidad.
        ''' Preenche apenas OidRed, CodRed e DesRed dos objetos Red
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  12/01/2011  criado
        ''' </history>
        Public Function ObtenerCombo() As List(Of Red)

            Dim redes As New List(Of Red)
            Dim red As Red
            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboRedes.Respuesta

            ' executa operação do serviço
            objRespuesta = objProxy.GetComboRedes()

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                For Each item In objRespuesta.Redes

                    ' preenche objeto red
                    red = New Red
                    With red
                        .OidRed = item.OidRed
                        .CodRed = item.CodigoRed
                        .DesRed = item.DescripcionRed
                    End With

                    ' adiciona a lista de retorno
                    redes.Add(red)

                Next

            End If

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            Return redes

        End Function

#End Region

    End Class

End Namespace
