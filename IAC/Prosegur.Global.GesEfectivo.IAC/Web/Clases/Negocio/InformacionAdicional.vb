Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio IAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/02/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class InformacionAdicional
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidIAC As String
        Private _codIAC As String
        Private _desIAC As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidIAC As String
            Get
                Return _oidIAC
            End Get
            Set(value As String)
                _oidIAC = value
            End Set
        End Property

        Public Property CodIAC As String
            Get
                Return _codIAC
            End Get
            Set(value As String)
                _codIAC = value
            End Set
        End Property

        Public Property DesIAC As String
            Get
                Return _desIAC
            End Get
            Set(value As String)
                _desIAC = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()

        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)

            _oidIAC = Oid
            _codIAC = Codigo
            _desIAC = Descripcion

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém valores para preencher uma combo 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 07/02/2011 - Criado 
        ''' </history>
        Public Function ObtenerCombo() As List(Of InformacionAdicional)

            Dim objProxy As New Comunicacion.ProxyUtilidad
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicional.Respuesta

            ' obtém morfologias
            objRespuesta = objProxy.GetComboInformacionAdicional()

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToList(objRespuesta.Iacs)

            Else

                ' se houve erros, retorna lista vazia
                Return New List(Of InformacionAdicional)

            End If

        End Function

        Public Function ConvertToList(Infos As ContractoServicio.Utilidad.GetComboInformacionAdicional.IacColeccion) As List(Of InformacionAdicional)

            Dim lista As New List(Of InformacionAdicional)
            Dim item As InformacionAdicional

            If Infos Is Nothing OrElse Infos.Count = 0 Then
                Return lista
            End If

            For Each infAdicional In Infos

                item = New InformacionAdicional

                With item
                    .CodIAC = infAdicional.Codigo
                    .DesIAC = infAdicional.Descripcion
                End With

                lista.Add(item)

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
