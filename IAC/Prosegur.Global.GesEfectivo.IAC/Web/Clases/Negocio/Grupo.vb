Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Grupo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  13/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class Grupo
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidGrupo As String
        Private _codGrupo As String
        Private _desGrupo As String
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidGrupo() As String
            Get
                Return _oidGrupo
            End Get
            Set(value As String)
                _oidGrupo = value
            End Set
        End Property

        Public Property CodGrupo() As String
            Get
                Return _codGrupo
            End Get
            Set(value As String)
                _codGrupo = value
            End Set
        End Property

        Public Property DesGrupo() As String
            Get
                Return _desGrupo
            End Get
            Set(value As String)
                _desGrupo = value
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

        Public ReadOnly Property CodigoDescripcion As String
            Get
                Return _codGrupo & "-" & _desGrupo
            End Get
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()


        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String)

            _oidGrupo = Oid
            _codGrupo = Codigo
            _desGrupo = Descripcion

        End Sub

#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Obtém grupos para preencher uma combo
        ''' Preenche apenas Oid, Codigo e Descrição dos objetos Red
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  13/01/2011  criado
        ''' </history>
        Public Function ObtenerCombo() As List(Of Grupo)

            Dim grupos As New List(Of Grupo)
            Dim grupo As Grupo
            Dim objProxy As New Comunicacion.ProxyGrupo
            Dim objPeticion As New IAC.ContractoServicio.Grupo.GetGrupos.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Grupo.GetGrupos.Respuesta

            objPeticion.BolObtenerTodosGrupos = True

            ' executa operação do serviço
            objRespuesta = objProxy.GetGrupos(objPeticion)

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                For Each item In objRespuesta.Grupos

                    ' preenche objeto red
                    grupo = New Grupo
                    With grupo
                        .OidGrupo = item.OidGrupo
                        .CodGrupo = item.CodigoGrupo
                        .DesGrupo = item.DescripcionGrupo
                    End With

                    ' adiciona a lista de retorno
                    grupos.Add(grupo)

                Next

            End If

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            Return grupos

        End Function

        ''' <summary>
        ''' Obtém grupos para preencher uma combo
        ''' Preenche apenas Oid, Codigo e Descrição dos objetos Red
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  13/01/2011  criado
        ''' </history>
        Public Function ObtenerCombo(BolObtenerTodosGrupos As Boolean) As List(Of Grupo)

            Dim grupos As New List(Of Grupo)
            Dim grupo As Grupo
            Dim objProxy As New Comunicacion.ProxyGrupo
            Dim objPeticion As New IAC.ContractoServicio.Grupo.GetGrupos.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Grupo.GetGrupos.Respuesta

            ' seta parámetros da petição
            objPeticion.BolObtenerTodosGrupos = BolObtenerTodosGrupos

            ' executa operação do serviço
            objRespuesta = objProxy.GetGrupos(objPeticion)

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                For Each item In objRespuesta.Grupos

                    ' preenche objeto red
                    grupo = New Grupo
                    With grupo
                        .OidGrupo = item.OidGrupo
                        .CodGrupo = item.CodigoGrupo
                        .DesGrupo = item.DescripcionGrupo
                    End With

                    ' adiciona a lista de retorno
                    grupos.Add(grupo)

                Next

            End If

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            Return grupos

        End Function

        ''' <summary>
        ''' Verifica se existe um grupo com os parámetros informados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  27/01/2011  criado
        ''' </history>
        Public Function VerificarGrupo(CodigoGrupo As String) As Boolean

            Dim objProxy As New Comunicacion.ProxyGrupo
            Dim objPeticion As New IAC.ContractoServicio.Grupo.VerificarGrupo.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Grupo.VerificarGrupo.Respuesta

            ' seta parámetros da petição
            objPeticion.CodigoGrupo = CodigoGrupo

            ' executa operação do serviço
            objRespuesta = objProxy.VerificarGrupo(objPeticion)

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                Return objRespuesta.Morfologia.BolExiste

            Else

                Return False

            End If

        End Function

        ''' <summary>
        ''' Obtém ATMs do grupo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  16/02/2011  criado
        ''' </history>
        Public Function GetATMsByGrupo(OidGrupo As String) As List(Of ATM)

            Dim objProxy As New Comunicacion.ProxyGrupo
            Dim objPeticion As New IAC.ContractoServicio.Grupo.GetATMsbyGrupo.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Grupo.GetATMsbyGrupo.Respuesta

            ' seta parámetros da petição
            objPeticion.OidGrupo = OidGrupo

            ' executa operação do serviço
            objRespuesta = objProxy.GetATMsbyGrupo(objPeticion)

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            ' se não houve erro, monta objeto de retorno
            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                Return CriarListaATMs(objRespuesta.Clientes)

            Else

                Return Nothing

            End If

        End Function

        ''' <summary>
        ''' Insere um grupo
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  27/01/2011  criado
        ''' </history>
        Public Sub Insertar()

            Dim objProxy As New Comunicacion.ProxyGrupo
            Dim objPeticion As New IAC.ContractoServicio.Grupo.SetGrupo.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Grupo.SetGrupo.Respuesta

            ' seta parámetros da petição
            With objPeticion
                .CodigoGrupo = _codGrupo
                .CodUsuario = _codUsuario
                .DescripcionGrupo = _desGrupo
            End With

            ' executa operação do serviço
            objRespuesta = objProxy.SetGrupo(objPeticion)

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

        End Sub

        Private Function CriarListaATMs(Clientes As List(Of ContractoServicio.Grupo.GetATMsbyGrupo.Cliente)) As List(Of ATM)

            Dim lista As New List(Of ATM)
            Dim objATM As ATM
            Dim objSubCliente As Subcliente
            Dim objPtoServicio As PuntoServicio

            For Each cli In Clientes

                For Each sc In cli.SubClientes

                    For Each pto In sc.PuntosServicio

                        objATM = New ATM

                        With objATM
                            .OidATM = pto.OidCajero
                            .CodigoATM = pto.CodigoCajero
                            .Cliente.CodigoCliente = cli.CodigoCliente
                            .Cliente.DesCliente = cli.DescripcionCliente
                            .FyhActualizacion = pto.FyhActualizacion
                        End With

                        objSubCliente = New Subcliente(sc.CodigoSubcliente, sc.DescripcionSubcliente)

                        objATM.Cliente.Subclientes.Add(objSubCliente)

                        objPtoServicio = New PuntoServicio(pto.OidPuntoServicio, pto.CodigoPuntoServicio, pto.DescripcionPuntoServicio)

                        objSubCliente.PtosServicio.Add(objPtoServicio)

                        lista.Add(objATM)

                    Next

                Next

            Next

            Return lista

        End Function

#End Region

    End Class

End Namespace
