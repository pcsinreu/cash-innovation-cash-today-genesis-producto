Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio SubCanal
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  14/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class SubCanal
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidSubcanal As String
        Private _codSubcanal As String
        Private _canal As Canal
        Private _desSubcanal As String
        Private _obsSubcanal As String
        Private _bolVigente As Boolean
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime
        Private _bolPorDefecto As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidSubcanal As String
            Get
                Return _oidSubcanal
            End Get
            Set(value As String)
                _oidSubcanal = value
            End Set
        End Property

        Public Property CodSubcanal() As String
            Get
                Return _codSubcanal
            End Get
            Set(value As String)
                _codSubcanal = value
            End Set
        End Property

        Public Property Canal As Canal
            Get
                Return _canal
            End Get
            Set(value As Canal)
                _canal = value
            End Set
        End Property

        Public Property DesSubcanal As String
            Get
                Return _desSubcanal
            End Get
            Set(value As String)
                _desSubcanal = value
            End Set
        End Property

        Public Property ObsSubcanal As String
            Get
                Return _obsSubcanal
            End Get
            Set(value As String)
                _obsSubcanal = value
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

        Public Property BolPorDefecto As Boolean
            Get
                Return _bolPorDefecto
            End Get
            Set(value As Boolean)
                _bolPorDefecto = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()

        End Sub

        Public Sub New(OidSubcanal As String, CodSubcanal As String, DesSubcanal As String)

            _oidSubcanal = OidSubcanal
            _codSubcanal = CodSubcanal
            _desSubcanal = DesSubcanal

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
        Public Function ObtenerCombo(CodigoCanal As String) As List(Of SubCanal)

            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta

            ' preenche petição
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(CodigoCanal)

            ' obtém morfologias
            objRespuesta = objProxy.GetComboSubcanalesByCanal(objPeticion)

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToList(objRespuesta.Canales)

            Else

                ' se houve erros, retorna lista vazia
                Return New List(Of SubCanal)

            End If

        End Function

        Public Function ConvertToList(Canais As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion) As List(Of SubCanal)

            Dim lista As New List(Of SubCanal)
            Dim item As SubCanal

            If Canais Is Nothing OrElse Canais.Count = 0 Then
                Return lista
            End If

            For Each sc In Canais(0).SubCanales

                item = New SubCanal

                With item
                    .CodSubcanal = sc.Codigo
                    .DesSubcanal = sc.Descripcion
                End With

                lista.Add(item)

            Next

            Return lista

        End Function
     
#End Region

    End Class

End Namespace
