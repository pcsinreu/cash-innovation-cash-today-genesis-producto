Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio ATM
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011 criado
    ''' </history>
    <Serializable()> _
Public Class ATM
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidCajero As String
        Private _codCajero As String
        Private _codDelegacion As String
        Private _bolRegistroTira As Boolean
        Private _bolVigente As Boolean
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime
        Private _red As New Red
        Private _modelo As New ModeloCajero
        Private _grupo As New Grupo
        Private _cliente As New Cliente

        Private _morfologias As New List(Of Morfologia)

        ' associações da morfologia 
        Private _cajeroXMorfologia As New List(Of CajeroXMorfologia)

        Private _procesos As New List(Of Proceso)

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidATM() As String
            Get
                Return _oidCajero
            End Get
            Set(value As String)
                _oidCajero = value
            End Set
        End Property

        Public Property CodigoATM() As String
            Get
                Return _codCajero
            End Get
            Set(value As String)
                _codCajero = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _codDelegacion
            End Get
            Set(value As String)
                _codDelegacion = value
            End Set
        End Property

        Public Property BolRegistroTira() As Boolean
            Get
                Return _bolRegistroTira
            End Get
            Set(value As Boolean)
                _bolRegistroTira = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
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

        Public Property Red() As Red
            Get
                Return _red
            End Get
            Set(value As Red)
                _red = value
            End Set
        End Property

        Public Property Modelo() As ModeloCajero
            Get
                Return _modelo
            End Get
            Set(value As ModeloCajero)
                _modelo = value
            End Set
        End Property

        Public Property Grupo() As Grupo
            Get
                Return _grupo
            End Get
            Set(value As Grupo)
                _grupo = value
            End Set
        End Property

        Public Property Cliente() As Cliente
            Get
                Return _cliente
            End Get
            Set(value As Cliente)
                _cliente = value
            End Set
        End Property

        Public Property Morfologias() As List(Of Morfologia)
            Get
                Return _morfologias
            End Get
            Set(value As List(Of Morfologia))
                _morfologias = value
            End Set
        End Property

        Public Property Procesos() As List(Of Proceso)
            Get
                If _procesos Is Nothing Then
                    _procesos = New List(Of Proceso)
                End If
                Return _procesos
            End Get
            Set(value As List(Of Proceso))
                _procesos = value
            End Set
        End Property

        ''' <summary>
        ''' Retorna a descrição do Cliente, subcliente e ponto de serviço separados por "/"
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  06/01/2011  criado
        ''' </history>
        Public ReadOnly Property DesClienteSubcliPtoServ() As String
            Get
                Return String.Format("{0} / {1} / {2}", _cliente.DesCliente, _cliente.Subclientes(0).DesSubcliente, _cliente.Subclientes(0).PtosServicio(0).DesPuntoServicio)
            End Get
        End Property

        Public ReadOnly Property DesRed() As String
            Get
                Return _red.DesRed
            End Get
        End Property

        Public ReadOnly Property DesModelo() As String
            Get
                Return _modelo.DesModeloCajero
            End Get
        End Property

        Public ReadOnly Property DesMorfologia() As String
            Get
                If _morfologias IsNot Nothing AndAlso _morfologias.Count > 0 Then
                    Return _morfologias(0).DesMorfologia
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property DesGrupo As String
            Get
                If _grupo Is Nothing Then
                    Return String.Empty
                Else
                    Return _grupo.DesGrupo
                End If
            End Get
        End Property

        Public ReadOnly Property OidGrupo As String
            Get
                If _grupo Is Nothing Then
                    Return String.Empty
                Else
                    Return _grupo.OidGrupo
                End If
            End Get
        End Property

        Public Property CajeroXMorfologias() As List(Of CajeroXMorfologia)
            Get
                If _cajeroXMorfologia Is Nothing Then
                    _cajeroXMorfologia = New List(Of CajeroXMorfologia)
                End If
                Return _cajeroXMorfologia
            End Get
            Set(value As List(Of CajeroXMorfologia))
                _cajeroXMorfologia = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()

        End Sub

        ''' <summary>
        ''' cria um objeto ATM a partir de um GetATMs.ATM
        ''' </summary>
        ''' <param name="ATM"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  14/01/2011  criado
        ''' </history>
        Public Sub New(ATM As ContractoServicio.GetATMs.ATM)

            If ATM Is Nothing Then
                Exit Sub
            End If

            ' preenche dados atm
            _oidCajero = ATM.OidCajero
            _codCajero = ATM.CodigoCajero
            _bolRegistroTira = ATM.BolRegistroTira
            _fyhActualizacion = ATM.FyhActualizacion
            _bolVigente = ATM.BolVigente

            ' preenche rede
            _red.DesRed = ATM.DescripcionRed
            _red.CodRed = ATM.CodigoRed

            ' preenche modelo
            _modelo.DesModeloCajero = ATM.DescripcionModeloCajero
            _modelo.CodModeloCajero = ATM.CodigoModeloCajero

            ' preenche grupo
            _grupo.OidGrupo = ATM.OidGrupo
            _grupo.DesGrupo = ATM.DescripcionGrupo
            _grupo.CodGrupo = ATM.CodigoGrupo

            ' preenche cliente
            _cliente.CodigoCliente = ATM.CodigoCliente
            _cliente.DesCliente = ATM.DescripcionCliente

            ' preenche subcliente
            _cliente.Subclientes.Add(New Subcliente(ATM.CodigoSubcliente, ATM.DescripcionSubcliente))

            ' preenche ptos servicio
            _cliente.Subclientes(0).PtosServicio.Add(New PuntoServicio(Nothing, ATM.CodigoPuntoServicio, ATM.DescripcionPuntoServicio))

            ' preenche morfologia do ATM

            If ATM.Morfologia IsNot Nothing Then
                _morfologias.Add(New Morfologia(ATM.Morfologia.OidMorfologia, Nothing, ATM.Morfologia.DescripcionMorfologia, Nothing, Nothing, Nothing))
            End If

        End Sub

        ''' <summary>
        ''' cria um objeto ATM a partir de um ATM.GetATMDetail.ATM
        ''' </summary>
        ''' <param name="ATM"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  14/01/2011  criado
        ''' </history>
        Public Sub New(ATM As ContractoServicio.ATM.GetATMDetail.ATM, Optional OidATM As String = Nothing)

            If ATM Is Nothing Then
                Exit Sub
            End If

            _oidCajero = OidATM
            _red.DesRed = ATM.DescripcionRed
            _red.CodRed = ATM.CodigoRed
            _modelo.DesModeloCajero = ATM.DescripcionModeloCajero
            _modelo.CodModeloCajero = ATM.CodigoModeloCajero
            _grupo.DesGrupo = ATM.DescripcionGrupo
            _grupo.CodGrupo = ATM.CodigoGrupo
            _bolRegistroTira = ATM.BolRegistroTira
            _fyhActualizacion = ATM.FyhActualizacion

            ' preenche morfologias
            For Each morf In ATM.Morfologias
                _morfologias.Add(New Morfologia(morf))
            Next

            If ATM.Procesos Is Nothing OrElse ATM.Procesos Is Nothing Then
                Exit Sub
            End If

            ' preenche processos
            For Each proc In ATM.Procesos
                _procesos.Add(New Proceso(proc))
            Next

        End Sub

#End Region

#Region "[MÉTODOS]"

        Private Sub Preencher(objATM As ContractoServicio.ATM.GetATMDetail.ATM, OidCajero As String)

            _oidCajero = OidCajero
            _codCajero = objATM.CodCajero
            _red.OidRed = objATM.OidRede
            _red.DesRed = objATM.DescripcionRed
            _red.CodRed = objATM.CodigoRed
            _modelo.OidModeloCajero = objATM.OidModeloCajero
            _modelo.DesModeloCajero = objATM.DescripcionModeloCajero
            _modelo.CodModeloCajero = objATM.CodigoModeloCajero
            _grupo.OidGrupo = objATM.OidGrupo
            _grupo.DesGrupo = objATM.DescripcionGrupo
            _grupo.CodGrupo = objATM.CodigoGrupo
            _bolRegistroTira = objATM.BolRegistroTira
            _fyhActualizacion = objATM.FyhActualizacion

            ' preenche morfologias
            '_morfologias = New List(Of Morfologia)
            'For Each morf In objATM.Morfologias
            '    _morfologias.Add(New Morfologia(morf))
            'Next

            ' preenche morfologias
            _cajeroXMorfologia = New List(Of CajeroXMorfologia)
            For Each morf In objATM.Morfologias
                _cajeroXMorfologia.Add(New CajeroXMorfologia(morf, OidCajero, Nothing))
            Next

            _procesos = New List(Of Proceso)

            If objATM.Procesos Is Nothing OrElse objATM.Procesos Is Nothing OrElse objATM.Procesos.Count = 0 Then
                Exit Sub
            End If

            ' preenche cliente, subcliente e pto servicio
            Dim codSubcliente As String
            Dim codPtoServicio As String
            Dim subcliente As Negocio.Subcliente

            _cliente.CodigoCliente = objATM.Procesos(0).RespuestaProcesoDetail.Procesos(0).CodigoCliente
            _cliente.DesCliente = objATM.Procesos(0).RespuestaProcesoDetail.Procesos(0).DescripcionCliente

            ' preenche processos
            For Each proc In objATM.Procesos

                If proc.RespuestaProcesoDetail.Procesos Is Nothing OrElse proc.RespuestaProcesoDetail.Procesos.Count = 0 Then
                    Continue For
                End If

                codSubcliente = proc.RespuestaProcesoDetail.Procesos(0).CodigoSubcliente
                codPtoServicio = proc.RespuestaProcesoDetail.Procesos(0).CodigoPuntoServicio

                subcliente = (From s In _cliente.Subclientes Where s.CodigoSubcliente = codSubcliente).FirstOrDefault()

                If subcliente Is Nothing Then

                    ' adiciona sub cliente (caso já não tenha adicionado)
                    subcliente = New Subcliente(proc.RespuestaProcesoDetail.Procesos(0).CodigoSubcliente, proc.RespuestaProcesoDetail.Procesos(0).DescripcionSubcliente)

                    _cliente.Subclientes.Add(subcliente)

                End If

                If (From pto In subcliente.PtosServicio Where pto.CodigoPuntoServicio = codPtoServicio).FirstOrDefault() Is Nothing Then

                    ' adiciona pto servicio (caso já não tenha adicionado)
                    subcliente.PtosServicio.Add(New PuntoServicio(String.Empty, proc.RespuestaProcesoDetail.Procesos(0).CodigoPuntoServicio, _
                                                                proc.RespuestaProcesoDetail.Procesos(0).DescripcionPuntoServicio))

                End If

                ' adiciona processo
                _procesos.Add(New Proceso(proc))

            Next

        End Sub

        Public Function ConvertToSetATM() As ContractoServicio.ATM.SetATM.Cajero

            Dim obj As New ContractoServicio.ATM.SetATM.Cajero

            With obj
                .CodigoCajero = _codCajero
                .OidCajero = _oidCajero
                .FyhActualizacion = _fyhActualizacion

                If _cliente IsNot Nothing Then

                    .CodigoCliente = _cliente.CodigoCliente

                    If _cliente.Subclientes IsNot Nothing AndAlso _cliente.Subclientes.Count > 0 Then

                        .CodigoSubcliente = _cliente.Subclientes(0).CodigoSubcliente

                        If _cliente.Subclientes(0).PtosServicio IsNot Nothing AndAlso _cliente.Subclientes(0).PtosServicio.Count > 0 Then

                            .CodigoPuntoServicio = _cliente.Subclientes(0).PtosServicio(0).CodigoPuntoServicio

                        End If

                    End If

                End If
                
            End With

            Return obj

        End Function

        ''' <summary>
        ''' retorna uma lista de atm's de acordo com os filtros informados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  06/01/2011  criado
        ''' </history>
        Public Function GetAtms(CodDelegacion As String, CodCajero As String, CodRed As String, _
                                CodModeloCajero As String, CodGrupo As String, Vigente As Boolean, _
                                CodCliente As String, CodigosSubclientes As List(Of String), _
                                CodigosPtosServ As List(Of String)) As List(Of ATM)

            Dim listaAtms As New List(Of ATM)

            Dim objProxy As New Comunicacion.ProxyATM
            Dim objPeticion As New ContractoServicio.GetATMs.Peticion
            Dim objRespuesta As ContractoServicio.GetATMs.Respuesta

            'Recebe os valores do filtro
            With objPeticion
                .BolVigente = Vigente
                .CodigoCajero = CodCajero
                .CodigoDelegacion = CodDelegacion
                .CodigoGrupo = CodGrupo
                .CodigoModeloCajero = CodModeloCajero
                .CodigoRed = CodRed
                .Cliente = Cliente.ConvertToGetATMs(CodCliente)
            End With

            ' verifica se existe filtro subcliente
            If CodigosSubclientes IsNot Nothing AndAlso CodigosSubclientes.Count > 0 Then

                objPeticion.SubClientes = New List(Of ContractoServicio.GetATMs.Subcliente)

                For Each cod In CodigosSubclientes

                    Dim objSubcliente As New ContractoServicio.GetATMs.Subcliente
                    objSubcliente.CodigoSubcliente = cod
                    objPeticion.SubClientes.Add(objSubcliente)

                Next

                ' verifica se existe filtro pts de serviço
                If CodigosPtosServ IsNot Nothing AndAlso CodigosPtosServ.Count > 0 Then

                    objPeticion.PuntoServicio = New List(Of ContractoServicio.GetATMs.PuntoServicio)

                    For Each cod In CodigosPtosServ

                        Dim objPuntoServicio As New ContractoServicio.GetATMs.PuntoServicio
                        objPuntoServicio.CodigoPuntoServicio = cod
                        objPeticion.PuntoServicio.Add(objPuntoServicio)

                    Next

                End If

            End If

            objRespuesta = objProxy.GetATMs(objPeticion)

            MyBase.Respuesta = objRespuesta


            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erro, converte para uma lista de objetos e negócio
                For Each item In objRespuesta.ATMs
                    listaAtms.Add(New ATM(item))
                Next

            End If

            Return listaAtms

        End Function

        ''' <summary>
        ''' obtém uns detalhes de um ATM
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  14/01/2011  criado
        ''' </history>
        Public Sub GetAtmDetail(OidATM As String, Optional OidGrupo As String = Nothing)

            Dim objProxy As New Comunicacion.ProxyATM
            Dim objPeticion As New IAC.ContractoServicio.ATM.GetATMDetail.Peticion
            Dim objRespuesta As IAC.ContractoServicio.ATM.GetATMDetail.Respuesta

            'Recebe os valores do filtro
            objPeticion.OidCajero = OidATM
            objPeticion.OidGrupo = OidGrupo

            objRespuesta = objProxy.GetATMDetail(objPeticion)

            ' se ocorreu algum erro na chamada do serviço getProcesoDetail, trata erro
            If objRespuesta.ATM.Procesos IsNot Nothing Then

                For Each p In objRespuesta.ATM.Procesos
                    objRespuesta.CodigoError = p.RespuestaProcesoDetail.CodigoError
                    objRespuesta.MensajeError = p.RespuestaProcesoDetail.MensajeError
                    Exit For
                Next

            End If

            ' seta resposta do serviço GetATMDetail
            MyBase.Respuesta = objRespuesta

            ' inicializa classe
            Inicializar()

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                If objRespuesta.ATM IsNot Nothing Then

                    ' se não houve erros e retornou ATM: preenche objeto de negócio
                    Preencher(objRespuesta.ATM, OidATM)

                End If

            End If

        End Sub

        ''' <summary>
        ''' Exclui ATM
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [blcosta]  14/01/2011  criado
        ''' </history>
        Public Shared Function Borrar(OidATM As String, CodDelegacion As String, CodUsuario As String) As ContractoServicio.ATM.SetATM.Respuesta

            Dim proxy As New Comunicacion.ProxyATM
            Dim peticion As New IAC.ContractoServicio.ATM.SetATM.Peticion
            Dim respuesta As IAC.ContractoServicio.ATM.SetATM.Respuesta
            Dim cajero As New IAC.ContractoServicio.ATM.SetATM.Cajero
            Dim objATM As New ATM

            ' inicializa
            peticion.Procesos = New List(Of ContractoServicio.ATM.SetATM.Proceso)

            ' obtém os detalhes do ATM
            objATM.GetAtmDetail(OidATM)

            ' preenche parámetros da petição
            With peticion
                .BolBorrar = True
                .CodigoDelegacion = CodDelegacion
                .Cajeros = New List(Of ContractoServicio.ATM.SetATM.Cajero)
                .CodUsuario = CodUsuario
            End With

            ' preenche objeto cajero
            cajero.OidCajero = OidATM
            peticion.Cajeros.Add(cajero)

            ' preenche processos
            Dim peticionProceso As IAC.ContractoServicio.Proceso.SetProceso.Peticion
            Dim proceso As IAC.ContractoServicio.ATM.SetATM.Proceso

            For Each proc In objATM.Procesos

                ' instancia objetos
                proceso = New IAC.ContractoServicio.ATM.SetATM.Proceso

                peticionProceso = New IAC.ContractoServicio.Proceso.SetProceso.Peticion
                peticionProceso.Proceso = New ContractoServicio.Proceso.SetProceso.Proceso
                peticionProceso.Proceso.Cliente = New ContractoServicio.Proceso.SetProceso.Cliente
                peticionProceso.Proceso.SubCanal = New ContractoServicio.Proceso.SetProceso.SubCanalColeccion
                peticionProceso.Proceso.Cliente.SubClientes = New ContractoServicio.Proceso.SetProceso.SubClienteColeccion

                peticionProceso.Proceso.SubCanal.Add(New ContractoServicio.Proceso.SetProceso.SubCanal)
                peticionProceso.Proceso.Cliente.SubClientes.Add(New ContractoServicio.Proceso.SetProceso.SubCliente)
                peticionProceso.Proceso.Cliente.SubClientes(0).PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                peticionProceso.Proceso.Cliente.SubClientes(0).PuntosServicio.Add(New ContractoServicio.Proceso.SetProceso.PuntoServicio)

                proceso.PeticionProceso = peticionProceso
                peticionProceso.CodigoUsuario = CodUsuario

                ' preenche petição proceso
                With peticionProceso.Proceso
                    .CodigoDelegacion = CodDelegacion
                    .Vigente = False
                    .Cliente.Codigo = proc.Cliente.CodigoCliente
                    .Cliente.SubClientes(0).Codigo = proc.Cliente.Subclientes(0).CodigoSubcliente
                    .Cliente.SubClientes(0).PuntosServicio(0).Codigo = proc.Cliente.Subclientes(0).PtosServicio(0).CodigoPuntoServicio
                    .SubCanal(0).Codigo = proc.Canal.SubCanais(0).CodSubcanal
                End With

                ' adiciona petição do SetProceso a petição do SetATM
                peticion.Procesos.Add(proceso)

            Next

            ' executa SetATM
            respuesta = proxy.SetATM(peticion)

            Return respuesta

        End Function

        ''' <summary>
        ''' Insere/Atualiza/excluir um ATM
        ''' </summary>
        ''' <param name="LoginUsuario"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  14/01/2011  criado
        ''' </history>
        Public Sub Guardar(LoginUsuario As String, CodigoDelegacion As String)

            Dim objProxy As New Comunicacion.ProxyATM
            Dim objRespuesta As IAC.ContractoServicio.ATM.SetATM.Respuesta
            Dim objPeticion As IAC.ContractoServicio.ATM.SetATM.Peticion

            ' preenche petição de acordo com valores do objeto
            objPeticion = ObtenerPeticionAltaModificacion(LoginUsuario, CodigoDelegacion)

            ' executa serviço
            objRespuesta = objProxy.SetATM(objPeticion)

            ' guarda resposta do serviço
            MyBase.Respuesta = objRespuesta

        End Sub

        ''' <summary>
        ''' Operação modificar grupo
        ''' </summary>
        ''' <param name="LoginUsuario"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  16/02/2011  criado
        ''' </history>
        Public Sub GuardarGrupo(LoginUsuario As String, CodigoDelegacion As String, ATMs As List(Of ATM))

            Dim objProxy As New Comunicacion.ProxyATM
            Dim objRespuesta As IAC.ContractoServicio.ATM.SetATM.Respuesta
            Dim objPeticion As IAC.ContractoServicio.ATM.SetATM.Peticion

            objPeticion = ObtenerPeticionModificarGrupo(LoginUsuario, CodigoDelegacion, ATMs)

            ' executa serviço
            objRespuesta = objProxy.SetATM(objPeticion)

            ' guarda resposta do serviço
            MyBase.Respuesta = objRespuesta

        End Sub

        Private Function ObtenerPeticionAltaModificacion(LoginUsuario As String, CodigoDelegacion As String) As IAC.ContractoServicio.ATM.SetATM.Peticion

            Dim objPeticion As New IAC.ContractoServicio.ATM.SetATM.Peticion
            Dim objCajero As ContractoServicio.ATM.SetATM.Cajero
            Dim objProceso As ContractoServicio.ATM.SetATM.Proceso = Nothing
            Dim objSubcliente As ContractoServicio.Proceso.SetProceso.SubCliente
            Dim objPtoServicio As ContractoServicio.Proceso.SetProceso.PuntoServicio

            Dim ordem As Integer = 0

            ' inicializa
            objPeticion.Procesos = New List(Of ContractoServicio.ATM.SetATM.Proceso)
            objPeticion.Cajeros = New List(Of ContractoServicio.ATM.SetATM.Cajero)

            ' preenche petição
            With objPeticion
                .CodUsuario = LoginUsuario
                .OidGrupo = _grupo.OidGrupo
                .OidModeloCajero = _modelo.OidModeloCajero
                .OidRed = _red.OidRed
                .CodigoDelegacion = CodigoDelegacion
                .BolRegistroTira = _bolRegistroTira
            End With

            ' obtém divisas dos efetivos das morfologias
            Dim divisasEfetivos As List(Of Divisa) = ObtenerDivisasEfectivos()

            ' para cada combinação de cliente/subcliente/pto serviço cria um atm
            For Each subcli In _cliente.Subclientes

                For Each pto In subcli.PtosServicio

                    ' ATM
                    objCajero = New ContractoServicio.ATM.SetATM.Cajero

                    With objCajero

                        If Me.Acao = eAcao.Modificacion Then
                            .OidCajero = _oidCajero
                        End If

                        .CodigoCliente = _cliente.CodigoCliente
                        .CodigoSubcliente = subcli.CodigoSubcliente
                        .CodigoPuntoServicio = pto.CodigoPuntoServicio
                        .FyhActualizacion = _fyhActualizacion
                    End With

                    objPeticion.Cajeros.Add(objCajero)

                Next

            Next

            If objPeticion.Cajeros.Count = 1 Then
                objPeticion.Cajeros(0).CodigoCajero = _codCajero
            End If

            ' Procesos 
            For Each proc In _procesos

                objProceso = Nothing

                proc.CodUsuario = LoginUsuario
                proc.CodDelegacion = CodigoDelegacion

                For Each caj In objPeticion.Cajeros

                    ' cria o processo somente na primeira vez
                    If objProceso Is Nothing Then
                        Dim medPagMen = proc.MediosPago
                        proc.MediosPago = ObtenerMediosPago()
                        For Each mp In proc.MediosPago
                            mp.Terminos.Clear()
                        Next

                        For Each mpMen In medPagMen
                            Dim mpMenX = mpMen
                            Dim mp = (From MedPag In proc.MediosPago Where mpMenX.CodMedioPago IsNot Nothing AndAlso MedPag.CodMedioPago = mpMenX.CodMedioPago).FirstOrDefault
                            If mp IsNot Nothing Then
                                mp.Terminos = mpMen.Terminos
                            End If
                        Next

                        'TODO: KIRK ENTENDER E MODIFICAR ESSE PONTO 
                        'proc.MediosPago = ObtenerMediosPago()
                        objProceso = proc.ConvertToSetATM(_cliente.CodigoCliente, caj.CodigoSubcliente, caj.CodigoPuntoServicio, divisasEfetivos)

                        If proc.Acao = eAcao.Baja Then
                            objProceso.PeticionProceso.Proceso.Vigente = False
                        End If

                        objPeticion.Procesos.Add(objProceso)

                    Else

                        objSubcliente = New ContractoServicio.Proceso.SetProceso.SubCliente
                        objSubcliente.Codigo = caj.CodigoSubcliente
                        objSubcliente.PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                        objProceso.PeticionProceso.Proceso.Cliente.SubClientes.Add(objSubcliente)

                        objPtoServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicio
                        objPtoServicio.Codigo = caj.CodigoPuntoServicio
                        objSubcliente.PuntosServicio.Add(objPtoServicio)

                    End If

                Next

            Next

            ' Morfologias            
            objPeticion.Morfologias = New List(Of ContractoServicio.ATM.SetATM.Morfologia)
            Dim morf As ContractoServicio.ATM.SetATM.Morfologia

            For Each cajXMorf In _cajeroXMorfologia

                morf = New ContractoServicio.ATM.SetATM.Morfologia

                morf.OidMorfologia = cajXMorf.Morfologia.OidMorfologia
                morf.FechaInicio = cajXMorf.FecInicio

                objPeticion.Morfologias.Add(morf)

            Next

            Return objPeticion

        End Function

        Private Function ObtenerPeticionModificarGrupo(LoginUsuario As String, CodigoDelegacion As String, Optional ATMs As List(Of ATM) = Nothing) As IAC.ContractoServicio.ATM.SetATM.Peticion

            Dim objPeticion As New IAC.ContractoServicio.ATM.SetATM.Peticion
            Dim objProceso As ContractoServicio.ATM.SetATM.Proceso = Nothing
            Dim objSubcliente As ContractoServicio.Proceso.SetProceso.SubCliente
            Dim objPtoServicio As ContractoServicio.Proceso.SetProceso.PuntoServicio

            ' inicializa
            objPeticion.Cajeros = New List(Of ContractoServicio.ATM.SetATM.Cajero)
            objPeticion.Procesos = New List(Of ContractoServicio.ATM.SetATM.Proceso)

            ' preenche petição
            With objPeticion
                .CodUsuario = LoginUsuario
                .OidGrupo = _grupo.OidGrupo
                .OidModeloCajero = _modelo.OidModeloCajero
                .OidRed = _red.OidRed
                .CodigoDelegacion = CodigoDelegacion
                .BolRegistroTira = _bolRegistroTira
            End With

            ' obtém divisas dos efetivos das morfologias
            Dim divisasEfetivos As List(Of Divisa) = ObtenerDivisasEfectivos()

            If ATMs IsNot Nothing Then

                ' modificar atms do grupo
                For Each objATM In ATMs

                    ' ATM
                    objPeticion.Cajeros.Add(objATM.ConvertToSetATM())

                Next

            End If

            ' Procesos 
            For Each proc In _procesos

                objProceso = Nothing

                proc.CodUsuario = LoginUsuario
                proc.CodDelegacion = CodigoDelegacion

                For Each caj In objPeticion.Cajeros

                    ' cria o processo somente na primeira vez
                    If objProceso Is Nothing Then

                        proc.MediosPago = ObtenerMediosPago()
                        objProceso = proc.ConvertToSetATM(_cliente.CodigoCliente, caj.CodigoSubcliente, caj.CodigoPuntoServicio, divisasEfetivos)

                        If proc.Acao = eAcao.Baja Then
                            objProceso.PeticionProceso.Proceso.Vigente = False
                        End If

                        objPeticion.Procesos.Add(objProceso)

                    Else

                        objSubcliente = New ContractoServicio.Proceso.SetProceso.SubCliente
                        objSubcliente.Codigo = caj.CodigoSubcliente
                        objSubcliente.PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                        objProceso.PeticionProceso.Proceso.Cliente.SubClientes.Add(objSubcliente)

                        objPtoServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicio
                        objPtoServicio.Codigo = caj.CodigoPuntoServicio
                        objSubcliente.PuntosServicio.Add(objPtoServicio)

                    End If

                Next

            Next

            ' Morfologias            
            objPeticion.Morfologias = New List(Of ContractoServicio.ATM.SetATM.Morfologia)
            Dim morf As ContractoServicio.ATM.SetATM.Morfologia

            For Each cajXMorf In _cajeroXMorfologia

                morf = New ContractoServicio.ATM.SetATM.Morfologia

                morf.OidMorfologia = cajXMorf.Morfologia.OidMorfologia
                morf.FechaInicio = cajXMorf.FecInicio

                objPeticion.Morfologias.Add(morf)

            Next

            Return objPeticion

        End Function

        Private Function ObtenerDivisasEfectivos() As List(Of Divisa)

            Dim divisas As New List(Of Divisa)
            Dim efetivosXMofologia As List(Of Divisa)
            Dim codigo As String

            For Each cxm In _cajeroXMorfologia

                ' obtém efetivos da morfologia
                efetivosXMofologia = cxm.Morfologia.ObtenerDivisaEfetivos()

                ' adiciona somente os que ainda não foram adicionados
                For Each efe In efetivosXMofologia

                    codigo = efe.CodigoDivisa

                    If (From d In divisas Where d.CodigoDivisa = codigo).FirstOrDefault() Is Nothing Then
                        divisas.Add(efe)
                    End If

                Next

            Next

            Return divisas

        End Function

        Private Function ObtenerMediosPago() As List(Of MedioPago)

            Dim mediosPago As New List(Of MedioPago)
            Dim mediosPagoXMorfologia As List(Of MedioPago)
            Dim codigoMP As String
            Dim codTipoMP As String
            Dim codIsoDivisa As String

            For Each cxm In _cajeroXMorfologia

                ' obtém medios pago da morfologia
                mediosPagoXMorfologia = cxm.Morfologia.ObtenerMediosPago()

                ' adiciona somente os que ainda não foram adicionados
                For Each mp In mediosPagoXMorfologia

                    codigoMP = mp.CodMedioPago
                    codTipoMP = mp.TipoMedioPago.CodTipoMedioPago
                    codIsoDivisa = mp.Divisa.CodigoDivisa

                    If (From d In mediosPago Where d.CodMedioPago = codigoMP _
                        AndAlso d.Divisa.CodigoDivisa = codIsoDivisa _
                        AndAlso d.TipoMedioPago.CodTipoMedioPago = codTipoMP).FirstOrDefault() Is Nothing Then

                        mediosPago.Add(mp)

                    End If

                Next

            Next

            Return mediosPago

        End Function

        ''' <summary>
        ''' Inicializa variáveis
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub Inicializar()

            ' preenche objeto de negócio
            _oidCajero = String.Empty
            _red = New Red
            _modelo = New ModeloCajero
            _grupo = New Grupo
            _bolRegistroTira = False
            _fyhActualizacion = Nothing
            _morfologias = New List(Of Morfologia)
            _cajeroXMorfologia = New List(Of CajeroXMorfologia)
            _procesos = New List(Of Proceso)

        End Sub

        ''' <summary>
        ''' Obtener medios de pagos e términos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  09/02/2010  criado
        ''' </history>
        Public Function ObtenerTerminosMedioPago(OidProceso As String) As PantallaProceso.MedioPagoColeccion

            Dim retornoMedioPagoCol As New PantallaProceso.MedioPagoColeccion
            Dim objMedioPagoCol As New PantallaProceso.MedioPagoColeccion
            Dim item As PantallaProceso.MedioPago
            Dim terminoMP As PantallaProceso.TerminoMedioPago
            Dim CodIsoDivisa As String = String.Empty
            Dim CodTipoMedioPago As String = String.Empty
            Dim CodMedioPago As String = String.Empty
            Dim bolSelecionado As Boolean
            Dim bolEsObligatorio As Boolean

            If _cajeroXMorfologia Is Nothing OrElse _cajeroXMorfologia.Count = 0 Then
                Return objMedioPagoCol
            End If

            ' obtém processo selecionado
            Dim proceso As Proceso = (From p In _procesos Where p.OidProceso = OidProceso).FirstOrDefault()

            If proceso Is Nothing Then
                Return objMedioPagoCol
            End If

            For Each m In _cajeroXMorfologia

                If m.Morfologia Is Nothing Then
                    Continue For
                End If

                For Each c In m.Morfologia.Componentes
                    For Each obj In c.Objectos

                        For Each tipoMP In obj.TiposMedioPago

                            For Each mp In tipoMP.MediosPago

                                CodIsoDivisa = obj.CodIsoDivisa
                                CodTipoMedioPago = tipoMP.CodTipoMedioPago
                                CodMedioPago = mp.CodMedioPago

                                ' verifica se o item já foi adicionado
                                If (From i In objMedioPagoCol _
                                    Where i.CodigoDivisa = CodIsoDivisa AndAlso _
                                    i.CodigoTipoMedioPago = CodTipoMedioPago AndAlso _
                                    i.CodigoMedioPago = CodMedioPago).FirstOrDefault() IsNot Nothing Then
                                    ' se item já foi adicionado, passa pro próximo
                                    Continue For
                                End If

                                item = New PantallaProceso.MedioPago

                                With item
                                    .CodigoDivisa = obj.CodIsoDivisa
                                    .DescripcionDivisa = obj.DesDivisa
                                    .CodigoTipoMedioPago = tipoMP.CodTipoMedioPago
                                    .DescripcionTipoMedioPago = tipoMP.DesTipoMedioPago
                                    .CodigoMedioPago = mp.CodMedioPago
                                    .DescripcionMedioPago = mp.DesMedioPago
                                    .TerminosMedioPago = New PantallaProceso.TerminoMedioPagoColeccion
                                End With

                                objMedioPagoCol.Add(item)

                                ' adiciona término do medio pago
                                For Each term In mp.Terminos

                                    ' verifica se o término está selecionado
                                    Dim prc = proceso.ObtenerTermino(CodIsoDivisa, CodTipoMedioPago, CodMedioPago, term.CodigoTermino)
                                    If prc Is Nothing Then
                                        bolSelecionado = False
                                        bolEsObligatorio = False
                                    Else
                                        bolSelecionado = True
                                        bolEsObligatorio = prc.EsObligatorio
                                    End If

                                    terminoMP = New PantallaProceso.TerminoMedioPago

                                    With terminoMP
                                        .CodigoTermino = term.CodigoTermino
                                        .DescripcionTermino = term.DescripcionTermino
                                        .EsObligatorio = bolEsObligatorio
                                        .Selecionado = bolSelecionado
                                    End With

                                    item.TerminosMedioPago.Add(terminoMP)

                                Next

                            Next

                        Next

                    Next
                Next
            Next

            ' retorna somente os medios de pago que possuem términos
            retornoMedioPagoCol.AddRange((From obj In objMedioPagoCol Where obj.TerminosMedioPago IsNot Nothing AndAlso obj.TerminosMedioPago.Count > 0).ToList())

            Return retornoMedioPagoCol

        End Function

        ''' <summary>
        ''' Atualiza términos de médios pago de um proceso
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  15/02/2010  criado
        ''' </history>
        Public Sub AtualizarTerminosMedioPagoXProceso(OidProceso As String, MediosPago As PantallaProceso.MedioPagoColeccion)

            Dim mpXProceso As Negocio.MedioPago
            Dim proceso As Proceso
            Dim CodigoDivisa As String
            Dim CodigoTipoMedioPago As String
            Dim CodigoMedioPago As String
            Dim termino As TerminoMedioPago

            If MediosPago Is Nothing Then
                Exit Sub
            End If

            ' obtém proceso atualizado
            proceso = (From p In _procesos Where p.OidProceso = OidProceso).FirstOrDefault()

            If proceso Is Nothing Then
                Exit Sub
            End If

            ' inicializa lista de medios pagos
            proceso.MediosPago = New List(Of MedioPago)

            For Each mp In MediosPago

                CodigoDivisa = mp.CodigoDivisa
                CodigoMedioPago = mp.CodigoMedioPago
                CodigoTipoMedioPago = mp.CodigoTipoMedioPago

                ' percorre terminos selecionados
                For Each term In (From t In mp.TerminosMedioPago Where t.Selecionado).ToList()

                    ' obtém medio pago do processo
                    mpXProceso = (From item In proceso.MediosPago Where item.Divisa.CodigoDivisa = CodigoDivisa _
                                  AndAlso item.TipoMedioPago.CodTipoMedioPago = CodigoTipoMedioPago _
                                  AndAlso item.CodMedioPago = CodigoMedioPago).FirstOrDefault()

                    If mpXProceso Is Nothing Then

                        ' se ainda não foi adicionado a lista, cria e preenche objeto
                        mpXProceso = New MedioPago

                        With mpXProceso
                            .CodMedioPago = mp.CodigoMedioPago
                            .DesMedioPago = mp.DescripcionMedioPago
                            .Divisa = New Divisa()
                            .TipoMedioPago = New TipoMedioPago()
                        End With

                        mpXProceso.Divisa.CodigoDivisa = mp.CodigoDivisa
                        mpXProceso.Divisa.DescripcionDivisa = mp.DescripcionDivisa

                        mpXProceso.TipoMedioPago.CodTipoMedioPago = mp.CodigoTipoMedioPago
                        mpXProceso.TipoMedioPago.DesTipoMedioPago = mp.DescripcionTipoMedioPago

                        ' adiciona medio pago ao processo
                        proceso.MediosPago.Add(mpXProceso)

                    End If

                    ' adiciona término ao médio pago
                    termino = New TerminoMedioPago

                    With termino
                        .CodigoTermino = term.CodigoTermino
                        .DescripcionTermino = term.DescripcionTermino
                        .EsObligatorio = term.EsObligatorio
                    End With

                    mpXProceso.Terminos.Add(termino)

                Next

            Next

        End Sub

#End Region

    End Class

End Namespace
