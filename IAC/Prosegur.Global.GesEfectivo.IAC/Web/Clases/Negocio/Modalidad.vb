Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Modalidad
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Modalidad
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidModalidad As String
        Private _codModalidad As String
        Private _desModalidad As String
        Private _admiteIAC As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidModalidad As String
            Get
                Return _oidModalidad
            End Get
            Set(value As String)
                _oidModalidad = value
            End Set
        End Property

        Public Property CodModalidad As String
            Get
                Return _codModalidad
            End Get
            Set(value As String)
                _codModalidad = value
            End Set
        End Property

        Public Property DesModalidad As String
            Get
                Return _desModalidad
            End Get
            Set(value As String)
                _desModalidad = value
            End Set
        End Property

        Public Property AdmiteIAC As Boolean
            Get
                Return _admiteIAC
            End Get
            Set(value As Boolean)
                _admiteIAC = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)
            _oidModalidad = Oid
            _codModalidad = Codigo
            _desModalidad = Descripcion

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
        Public Function ObtenerCombo() As List(Of Modalidad)

            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta

            ' obtém morfologias
            objRespuesta = objProxy.GetComboModalidadesRecuento()

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToList(objRespuesta.ModalidadesRecuento)

            Else

                ' se houve erros, retorna lista vazia
                Return New List(Of Modalidad)

            End If

        End Function

        Public Function ConvertToList(Modalidades As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion) As List(Of Modalidad)

            Dim lista As New List(Of Modalidad)
            Dim item As Modalidad
            Dim codModalidad As String

            If Modalidades Is Nothing OrElse Modalidades.Count = 0 Then
                Return lista
            End If

            For Each modalidad In Modalidades

                codModalidad = modalidad.Codigo

                If (From m In lista Where m.CodModalidad = codModalidad).FirstOrDefault() Is Nothing Then

                    ' se ainda não adicionou a lista, adiciona
                    item = New Modalidad

                    With item
                        .CodModalidad = modalidad.Codigo
                        .DesModalidad = modalidad.Descripcion
                        .AdmiteIAC = modalidad.AdmiteIac
                    End With

                    lista.Add(item)

                End If

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
