Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos

Public Class AccionGuardarCliente

    ''' <summary>
    ''' Método principal responsável por obter dados do formulario 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 18/09/2009 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As GuardarCliente.Peticion) As GuardarCliente.Respuesta

        Dim objRespuesta As New GuardarCliente.Respuesta

        ' setar codigo 0 e mensagem erro vazio
        objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        objRespuesta.MensajeError = String.Empty

        ' Inicializar os objetos dentro da respuesta
        objRespuesta.ClientesOk = New GuardarCliente.ClienteOkColeccion
        objRespuesta.ClientesError = New GuardarCliente.ClienteErrorColeccion

        Try

            If Peticion IsNot Nothing Then

                If Peticion.UtilizarReglaAutomatas Then

                    ' Chama o metodo que cria o cliente
                    CriarClienteAutomatas(Peticion, objRespuesta)

                Else

                    ' Chama o metodo que cria o cliente
                    CriarCliente(Peticion, objRespuesta)

                End If

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Atualiza os clientes encontrados
    ''' </summary>
    ''' <param name="IdPsClienteMatrizCalculado"></param>
    ''' <param name="objClientes"></param>
    ''' <param name="objClientesMatrizes"></param>
    ''' <param name="Cliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/01/2013 - Criado
    ''' </history>
    Private Sub ActualizarClientes(IdPsClienteMatrizCalculado As String, objClientes As Negocio.Clientes, _
                                   ByRef objClientesMatrizes As Negocio.Clientes, Cliente As Prosegur.Global.Saldos.ContractoServicio.GuardarCliente.Cliente)

        'Declaração de variaveis
        Dim IdCliente As Integer = 0
        Dim IdPsCliente As String = String.Empty
        Dim InserirActualizarMatriz As Boolean = True
        Dim objClienteMatriz As Negocio.Cliente = Nothing
        Dim TodosClientesSonSaldoSucursal As Boolean = False

        For Each objClienteCorrente In objClientes

            'Verifica se todos os clientes encontrados não tem cliente matriz associado.
            If objClientes.Count = 1 Then

                If IdPsClienteMatrizCalculado = objClienteCorrente.IdPS AndAlso objClienteCorrente.SaldosPorSucursal Then
                    InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, objClienteCorrente, Nothing, Cliente, False, False)
                    Continue For
                ElseIf (objClienteCorrente.Matriz Is Nothing OrElse objClienteCorrente.Matriz.Id = 0) Then
                    InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, objClienteMatriz, objClienteCorrente, Cliente, False, True)
                    Continue For
                End If

            End If


            If objClientes.Count > 1 AndAlso ((From cm In objClientes Where cm.SaldosPorSucursal).Count = objClientes.Count OrElse TodosClientesSonSaldoSucursal) Then

                TodosClientesSonSaldoSucursal = True

                'Valida se o cliente enviado pelo IAC é um cliente matriz.
                If IdPsClienteMatrizCalculado = Cliente.CodCliente Then
                    InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, objClienteCorrente, Nothing, Cliente, False, False)
                    Exit For
                Else
                    InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, objClienteMatriz, objClienteCorrente, Cliente, True, False)
                End If

                'Verifica se é um cliente matriz.
            ElseIf Not objClienteCorrente.SaldosPorSucursal Then
                InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, objClienteMatriz, objClienteCorrente, Cliente, True, False)
            End If

        Next

    End Sub

    ''' <summary>
    ''' Insere um novo cliente
    ''' </summary>
    ''' <param name="IdPsClienteMatrizCalculado"></param>
    ''' <param name="objClientesMatrizes"></param>
    ''' <param name="objClienteMatriz"></param>
    ''' <param name="objCliente"></param>
    ''' <param name="Cliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/01/2013 - Criado
    ''' </history>
    Private Sub InserirActualizarClientearCliente(IdPsClienteMatrizCalculado As String, ByRef objClientesMatrizes As Negocio.Clientes, ByRef objClienteMatriz As Negocio.Cliente, _
                                                  ByRef objCliente As Negocio.Cliente, Cliente As Prosegur.Global.Saldos.ContractoServicio.GuardarCliente.Cliente, _
                                                  PesquisarMatrizConId As Boolean, EjecutarBusquedaMatriz As Boolean,
                                                  Optional FueEncontradoCliente As Boolean = True)

        Dim IdCliente As Integer = 0
        Dim IdPsCliente As String = String.Empty

        'Se o cliente matriz é vazio
        'Executa a pesquisa pelo cliente matriz.
        If Not PesquisarMatrizConId Then

            'Se o cliente matriz não é vazio somente atualiza o cliente matriz.
            If EjecutarBusquedaMatriz Then

                objClientesMatrizes = New Negocio.Clientes
                objClientesMatrizes.IdPS = IdPsClienteMatrizCalculado

                objClientesMatrizes.RealizarIdPs()

                'Se o cliente matriz foi encontrado, atualiza o cliente matriz.
                If objClientesMatrizes IsNot Nothing AndAlso objClientesMatrizes.Count > 0 Then

                    If objCliente IsNot Nothing Then IdCliente = objCliente.Id
                    Dim MontarClausulaConIdCliente As Boolean = (objCliente IsNot Nothing)

                    If FueEncontradoCliente Then
                        objClienteMatriz = (From cm In objClientesMatrizes _
                                            Where cm.SaldosPorSucursal AndAlso If(MontarClausulaConIdCliente, cm.SaldosPorSucursal AndAlso cm.Id <> IdCliente, cm.SaldosPorSucursal)).FirstOrDefault
                    Else
                        objClienteMatriz = objClientesMatrizes(0)
                    End If

                    If objClienteMatriz IsNot Nothing Then

                        objClienteMatriz.Descripcion = Cliente.DescripcionCliente
                        objClienteMatriz.SaldosPorSucursal = True

                        objClienteMatriz.Registrar()

                    Else
                        'Se não encontrou cliente matriz, cria um novo cliente matriz.
                        objClienteMatriz = InserirCliente(IdPsClienteMatrizCalculado, Cliente.DescripcionCliente, Nothing)
                    End If

                Else
                    'Se não encontrou cliente matriz, cria um novo cliente matriz.
                    objClienteMatriz = InserirCliente(IdPsClienteMatrizCalculado, Cliente.DescripcionCliente, Nothing)
                End If

            Else

                objClienteMatriz.Descripcion = Cliente.DescripcionCliente
                objClienteMatriz.SaldosPorSucursal = True

                objClienteMatriz.Registrar()

            End If

        Else

            objClienteMatriz = New Negocio.Cliente
            objClienteMatriz.Id = objCliente.Matriz.Id

            'Executa a busca do cliente matriz
            objClienteMatriz.Realizar()

            'Verifica se o IdPs do cliente matriz é igual ao IdPs calculado.
            If objClienteMatriz.IdPS = IdPsClienteMatrizCalculado Then

                'Atualiza o cliente matriz.
                objClienteMatriz.Descripcion = Cliente.DescripcionCliente
                objClienteMatriz.SaldosPorSucursal = True

                objClienteMatriz.Registrar()

            Else

                'Executa a pesquisa para verificar sem existe um cliente matriz
                'com o IDPS calculado
                objClientesMatrizes = New Negocio.Clientes
                objClientesMatrizes.IdPS = IdPsClienteMatrizCalculado

                objClientesMatrizes.RealizarIdPs()

                'Verifica se foi encontrado algum cliente matriz
                If objClientesMatrizes IsNot Nothing AndAlso objClientesMatrizes.Count > 0 Then

                    If objCliente IsNot Nothing Then
                        IdPsCliente = objCliente.IdPS
                        IdCliente = objCliente.Id
                    End If

                    'Recupera o cliente matriz
                    objClienteMatriz = (From cliMat In objClientesMatrizes
                                        Where (cliMat.Id <> IdCliente AndAlso _
                                              (cliMat.IdPS <> IdPsCliente OrElse
                                              (cliMat.IdPS = IdPsCliente AndAlso cliMat.SaldosPorSucursal = True)))).FirstOrDefault

                    If objClienteMatriz IsNot Nothing Then

                        'Atualiza o cliente matriz.
                        objClienteMatriz.Descripcion = Cliente.DescripcionCliente
                        objClienteMatriz.SaldosPorSucursal = True

                        objClienteMatriz.Registrar()

                    Else
                        'Se não encontrou cliente matriz, cria um novo cliente matriz.
                        objClienteMatriz = InserirCliente(IdPsClienteMatrizCalculado, Cliente.DescripcionCliente, Nothing)
                    End If

                Else
                    'Se não encontrou cliente matriz, cria um novo cliente matriz.
                    objClienteMatriz = InserirCliente(IdPsClienteMatrizCalculado, Cliente.DescripcionCliente, Nothing)
                End If

            End If

        End If

        If objCliente Is Nothing Then
            'Faz a inserção do cliente.
            objCliente = InserirCliente(Cliente.CodCliente, Cliente.DescripcionCliente, If(objClienteMatriz IsNot Nothing, objClienteMatriz, objClientesMatrizes(0)))
        Else
            'Atualiza o cliente
            ActualizarCliente(objCliente, objClienteMatriz.Id, Cliente.CodCliente, Cliente.DescripcionCliente)
        End If

    End Sub
    ''' <summary>
    ''' Cria o cliente utilizando a regra automatas.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <param name="Respuesta"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/01/2013 - Criado
    ''' </history>
    Private Sub CriarClienteAutomatas(Peticion As GuardarCliente.Peticion, _
                                      ByRef Respuesta As GuardarCliente.Respuesta)

        If Peticion.Clientes IsNot Nothing Then

            'Declara as variáveis.
            Dim CodigosClienteMatriz() As String = Nothing
            Dim IdPsClienteMatrizCalculado As String = String.Empty
            Dim objClientes As Negocio.Clientes = Nothing
            Dim objClientesMatrizes As Negocio.Clientes = Nothing
            'Dim objClienteMatriz As Negocio.Cliente = Nothing
            'Dim objCliente As Negocio.Cliente = Nothing

            For Each Cliente In Peticion.Clientes

                ' Valida se os dados do cliente foram todos informados
                If Not String.IsNullOrEmpty(Cliente.CodCliente) AndAlso Not String.IsNullOrEmpty(Cliente.DescripcionCliente) Then

                    Try

                        CodigosClienteMatriz = Cliente.CodCliente.Split("-")

                        'Recuepra o IDPS do cliente matriz
                        If CodigosClienteMatriz IsNot Nothing AndAlso CodigosClienteMatriz.Count > 0 Then
                            IdPsClienteMatrizCalculado = CodigosClienteMatriz(0)
                        End If

                        objClientes = New Negocio.Clientes

                        objClientes.IdPS = Cliente.CodCliente

                        objClientes.RealizarIdPs()

                        'Verifica se foi recuperado algum cliente
                        If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then

                            'percorre os clientes recuperados
                            ActualizarClientes(IdPsClienteMatrizCalculado, objClientes, objClientesMatrizes, Cliente)

                        Else

                            InserirActualizarClientearCliente(IdPsClienteMatrizCalculado, objClientesMatrizes, Nothing, Nothing, Cliente, False, True, False)

                        End If

                    Catch ex As Exception

                        Util.TratarErroBugsnag(ex)
                        AtualizarRespuestaClienteError(Respuesta, Cliente, ex.Message)
                        Continue For
                    End Try

                    ' Chamo o metodo que atualiza a colecao de clientes inseridos com sucesso (clientesOk)
                    AtualizarRespuestaClienteOk(Respuesta, Cliente)

                Else
                    ' Se os dados nao foram informados corretamente, adiciona
                    ' o objeto na colecao de clientes com erro (clientesError)
                    AtualizarRespuestaClienteError(Respuesta, Cliente, Traduzir("03_msg_erro_cliente"))
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Insere o cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' [anselmo.gois] 02/01/2013 - Criado
    Private Function InserirCliente(IdPsCliente As String, Descripcion As String, ClienteMatriz As Negocio.Cliente) As Negocio.Cliente

        Dim Cliente As New Negocio.Cliente

        'Preenche os campos do objeto cliente matriz
        Cliente.IdPS = IdPsCliente
        Cliente.Descripcion = Descripcion
        Cliente.SaldosPorSucursal = If(ClienteMatriz Is Nothing, True, False)
        Cliente.Logo = "logo/generico.gif"
        Cliente.ConImagen = False

        Cliente.Bancos = Nothing
        Cliente.Matriz = ClienteMatriz

        'Se o cliente matriz não for nulo, quer dizer que estamos fazendo a inserção de um cliente.
        'Sendo assim preenche os campos do genesis para fazer o mapeio.
        If ClienteMatriz IsNot Nothing Then

            Cliente.CodClienteGenesis = IdPsCliente
            Cliente.CodSubClienteGenesis = IdPsCliente
            Cliente.CodPuntoServicioGenesis = IdPsCliente

        End If

        'Insere um novo cliente matriz.
        Cliente.Registrar()

        Return Cliente
    End Function

    ''' <summary>
    ''' Atualiza o cliente 
    ''' </summary>
    ''' <param name="Cliente"></param>
    ''' <param name="IdMatriz"></param>
    ''' <param name="CodCliente"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/01/2013 - Criado
    ''' </history>
    Private Sub ActualizarCliente(ByRef Cliente As Negocio.Cliente, IdMatriz As Integer, CodCliente As String, DescripcionCliente As String)

        Cliente.Descripcion = DescripcionCliente
        Cliente.CodClienteGenesis = CodCliente
        Cliente.CodSubClienteGenesis = CodCliente
        Cliente.CodPuntoServicioGenesis = CodCliente
        Cliente.SaldosPorSucursal = False

        If IdMatriz <> 0 Then
            If Cliente.Matriz Is Nothing Then Cliente.Matriz = New Negocio.Cliente
            Cliente.Matriz.Id = IdMatriz
        End If

        Cliente.Registrar()

    End Sub

    ''' <summary>
    ''' Método que cria os clientes presentes na colecao de clientes da Peticion
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub CriarCliente(Peticion As GuardarCliente.Peticion, _
                             ByRef Respuesta As GuardarCliente.Respuesta)

        Dim IdentificadorCliente As Integer = 0

        If Peticion.Clientes IsNot Nothing Then

            For Each Cliente In Peticion.Clientes

                IdentificadorCliente = 0

                ' Valida se os dados do cliente foram todos informados
                If Cliente.CodCliente <> String.Empty AndAlso Cliente.DescripcionCliente <> String.Empty Then

                    Try

                        Dim IdPSCliente As String = Cliente.CodCliente

                        ' Chama o metodo responsavel por inserir o cliente no sistema Saldos
                        IdentificadorCliente = RegistrarCliente(0,
                                                                IdPSCliente,
                                                                Cliente.DescripcionCliente,
                                                                Cliente.CodCliente,
                                                                String.Empty,
                                                                String.Empty)

                    Catch ex As Exception
                        Util.TratarErroBugsnag(ex)

                        AtualizarRespuestaClienteError(Respuesta, Cliente, ex.Message)
                        Continue For

                    End Try

                    ' Se foi inserido com sucesso, adiciono o objeto na colecao de clientes inseridos com sucesso(clientesOk)
                    If IdentificadorCliente <> 0 Then

                        ' Chamo o metodo que atualiza a colecao de clientes inseridos com sucesso (clientesOk)
                        AtualizarRespuestaClienteOk(Respuesta, Cliente)

                        ' Chamo o metodo que cria os subclientes da colecao de subclientes do cliente do laço
                        CriarSubCliente(Cliente, Respuesta, IdentificadorCliente)

                    Else

                        ' Se nao foi inserido com sucesso, adiciona o objeto na colecao de clientes com erro (clientesError)
                        AtualizarRespuestaClienteError(Respuesta, Cliente, Traduzir("03_msg_erro_cliente"))

                    End If

                Else

                    ' Se os dados nao foram informados corretamente, adiciona
                    ' o objeto na colecao de clientes com erro (clientesError)
                    AtualizarRespuestaClienteError(Respuesta, Cliente, Traduzir("03_msg_erro_cliente"))

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Método que cria os sub-clientes presentes no cliente
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub CriarSubCliente(Cliente As GuardarCliente.Cliente, _
                                ByRef Respuesta As GuardarCliente.Respuesta, _
                                IdentificadorCliente As Integer)

        Dim IdentificadorSubCliente As Integer = 0

        If Cliente.SubClientes IsNot Nothing Then

            For Each SubCliente In Cliente.SubClientes

                ' Valida se os dados do SubCliente foram todos informados
                If SubCliente.CodSubCliente <> String.Empty AndAlso SubCliente.DescripcionSubCliente <> String.Empty Then

                    IdentificadorSubCliente = 0

                    Try

                        Dim IdPSSubCliente As String = Cliente.CodCliente & "-" & SubCliente.CodSubCliente

                        ' Chama o metodo responsavel por inserir o SubCliente no sistema Saldos
                        IdentificadorSubCliente = RegistrarCliente(IdentificadorCliente,
                                                                   IdPSSubCliente,
                                                                   SubCliente.DescripcionSubCliente,
                                                                   Cliente.CodCliente,
                                                                   SubCliente.CodSubCliente,
                                                                   String.Empty)

                    Catch ex As Exception
                        Util.TratarErroBugsnag(ex)

                        AtualizarRespuestaSubClienteError(Respuesta, Cliente.CodCliente, SubCliente, ex.Message)
                        Continue For

                    End Try

                    ' Se foi inserido com sucesso, adiciono o objeto na colecao de sub-clientes inseridos com sucesso(subclientesOk)
                    If IdentificadorSubCliente <> 0 Then

                        ' Chamo o metodo que atualiza a colecao de subclientes inseridos com sucesso (subclientesOk)
                        AtualizarRespuestaSubClienteOk(Respuesta, Cliente.CodCliente, SubCliente)

                        ' Chamo o metodo que cria os puntos servicios da colecao de puntos servicios do subcliente do laço
                        CriarPuntoServicio(Cliente, SubCliente, Respuesta, IdentificadorSubCliente)

                    Else

                        ' Se nao foi inserido com sucesso, adiciona o objeto na colecao de subclientes com erro (subclientesError)
                        AtualizarRespuestaSubClienteError(Respuesta, Cliente.CodCliente, SubCliente, Traduzir("03_msg_erro_cliente"))

                    End If

                Else

                    ' Se os dados nao foram informados corretamente, adiciona
                    ' o objeto na colecao de subclientes com erro (subclientesError)
                    AtualizarRespuestaSubClienteError(Respuesta, Cliente.CodCliente, SubCliente, Traduzir("03_msg_erro_cliente"))

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Método que cria os puntos servicios presentes no subcliente
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub CriarPuntoServicio(Cliente As GuardarCliente.Cliente, _
                                   SubCliente As GuardarCliente.SubCliente, _
                                   ByRef Respuesta As GuardarCliente.Respuesta, _
                                   IdentificadorSubCliente As Integer)

        Dim IdentificadorPuntoServicio As Integer = 0

        If SubCliente.PuntoServicios IsNot Nothing Then

            For Each PuntoServicio In SubCliente.PuntoServicios

                ' Valida se os dados do PuntoServicio foram todos informados
                If PuntoServicio.CodPuntoServicio <> String.Empty AndAlso PuntoServicio.DescripcionPuntoServicio <> String.Empty Then

                    IdentificadorPuntoServicio = 0

                    Try

                        Dim IdPSPuntoServicio As String = Cliente.CodCliente & "-" & SubCliente.CodSubCliente & "-" & PuntoServicio.CodPuntoServicio

                        ' Chama o metodo responsavel por inserir o PuntoServicio no sistema Saldos
                        IdentificadorPuntoServicio = RegistrarCliente(IdentificadorSubCliente,
                                                                      IdPSPuntoServicio,
                                                                      PuntoServicio.DescripcionPuntoServicio,
                                                                      Cliente.CodCliente,
                                                                      SubCliente.CodSubCliente,
                                                                      PuntoServicio.CodPuntoServicio)

                    Catch ex As Exception
                        Util.TratarErroBugsnag(ex)

                        AtualizarRespuestaPuntoServicioError(Respuesta, Cliente.CodCliente, SubCliente.CodSubCliente, PuntoServicio, ex.Message)
                        Continue For

                    End Try

                    ' Se foi inserido com sucesso, adiciono o objeto na colecao de PuntoServicio inseridos com sucesso(PuntoServiciosOk)
                    If IdentificadorPuntoServicio <> 0 Then

                        ' Chamo o metodo que atualiza a colecao de PuntoServicios inseridos com sucesso (PuntoServiciosOk)
                        AtualizarRespuestaPuntoServicioOk(Respuesta, Cliente.CodCliente, SubCliente.CodSubCliente, PuntoServicio)

                    Else

                        ' Se nao foi inserido com sucesso, adiciona o objeto na colecao de PuntoServicios com erro (PuntoServiciosError)
                        AtualizarRespuestaPuntoServicioError(Respuesta, Cliente.CodCliente, SubCliente.CodSubCliente, PuntoServicio, Traduzir("03_msg_erro_cliente"))

                    End If

                Else

                    ' Se os dados nao foram informados corretamente, adiciona
                    ' o objeto na colecao de PuntoServicios com erro (PuntoServiciosError)
                    AtualizarRespuestaPuntoServicioError(Respuesta, Cliente.CodCliente, SubCliente.CodSubCliente, PuntoServicio, Traduzir("03_msg_erro_cliente"))

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Metodo responsavel por inserir o cliente no Sistema de Saldos
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Function RegistrarCliente(IdMatriz As Integer, IdPS As String, _
                                      Descripcion As String, CodClienteGenesis As String, _
                                      CodSubClienteGenesis As String, _
                                      CodPuntoServicioGenesis As String) As Integer

        Dim objCliente As New Negocio.Cliente

        objCliente.CodClienteGenesis = CodClienteGenesis
        objCliente.CodSubClienteGenesis = CodSubClienteGenesis
        objCliente.CodPuntoServicioGenesis = CodPuntoServicioGenesis

        ' Chama o metodo que procura o cliente pelos campos CodClienteGenesis, CodSubClienteGenesis, CodPuntoServicioGenesis
        objCliente.RealizarPorClienteGenesis()

        ' Atualiza os campos idps e descripcion
        objCliente.IdPS = IdPS
        objCliente.Descripcion = Descripcion

        ' Se for sub-cliente ou punto servicio, atualiza o campo matriz
        If IdMatriz <> 0 Then
            objCliente.Matriz.Id = IdMatriz
        End If

        ' Se o cliente for encontrado ira atualizar, se nao existir irá inserir
        objCliente.Registrar()

        Return objCliente.Id

    End Function

    ''' <summary>
    ''' Atualiza o objeto ClienteOk da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaClienteOk(ByRef Respuesta As GuardarCliente.Respuesta, _
                                            Cliente As GuardarCliente.Cliente)
        ' Adiciona o cliente na colecao de clientes ok
        Respuesta.ClientesOk.Add(New GuardarCliente.ClienteOk With { _
                                     .CodCliente = Cliente.CodCliente, _
                                     .SubClientes = New GuardarCliente.SubClienteOkColeccion})

    End Sub

    ''' <summary>
    ''' Atualiza o objeto ClienteError da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaClienteError(ByRef Respuesta As GuardarCliente.Respuesta, _
                                               Cliente As GuardarCliente.Cliente, _
                                               DescripcionError As String)
        ' Adiciona o cliente na colecao de clientes que ocorreu erro
        Respuesta.ClientesError.Add(New GuardarCliente.ClienteError With { _
                                    .CodCliente = Cliente.CodCliente, _
                                    .DescripcionError = DescripcionError, _
                                    .SubClientes = New GuardarCliente.SubClienteErrorColeccion})


    End Sub

    ''' <summary>
    ''' Atualiza o objeto ClienteOk da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaSubClienteOk(ByRef Respuesta As GuardarCliente.Respuesta, _
                                               CodCliente As String, _
                                               SubCliente As GuardarCliente.SubCliente)

        ' Cria o subcliente ok para adicionar na colecao
        Dim SubClienteOk As New GuardarCliente.SubClienteOk With _
                                    {.CodSubCliente = SubCliente.CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioOkColeccion}

        ' verifica se o cliente do subcliente ja existe na colecao
        Dim ClienteExistente = Respuesta.ClientesOk.FirstOrDefault(Function(cli) cli.CodCliente = CodCliente)

        ' se existe, adiciona o subcliente
        If ClienteExistente IsNot Nothing Then
            ClienteExistente.SubClientes.Add(SubClienteOk)
        Else
            ' se nao existe cria o cliente antes de adicionar o subcliente
            Dim NovoClienteOk As New GuardarCliente.ClienteOk With _
                                    {.CodCliente = CodCliente, _
                                     .SubClientes = New GuardarCliente.SubClienteOkColeccion _
                                    }

            NovoClienteOk.SubClientes.Add(SubClienteOk)

            Respuesta.ClientesOk.Add(NovoClienteOk)

        End If

    End Sub

    ''' <summary>
    ''' Atualiza o objeto SubClienteError da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaSubClienteError(ByRef Respuesta As GuardarCliente.Respuesta, _
                                                  CodCliente As String, _
                                                  SubCliente As GuardarCliente.SubCliente, _
                                                  DescripcionError As String)

        ' Cria o subcliente erro para adicionar na colecao
        Dim SubClienteError As New GuardarCliente.SubClienteError With _
                                    {.CodSubCliente = SubCliente.CodSubCliente, _
                                     .DescripcionError = DescripcionError, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioErrorColeccion _
                                    }

        ' verifica se o cliente do subcliente ja existe na colecao de erro
        Dim ClienteExistente = Respuesta.ClientesError.FirstOrDefault(Function(cli) cli.CodCliente = CodCliente)

        ' se existe, adiciona o subcliente
        If ClienteExistente IsNot Nothing Then
            ClienteExistente.SubClientes.Add(SubClienteError)
        Else
            ' se nao existe cria o cliente antes de adicionar o subcliente
            Dim NovoClienteErro As New GuardarCliente.ClienteError With _
                                    {.CodCliente = CodCliente, _
                                     .DescripcionError = String.Empty, _
                                     .SubClientes = New GuardarCliente.SubClienteErrorColeccion _
                                    }

            NovoClienteErro.SubClientes.Add(SubClienteError)

            Respuesta.ClientesError.Add(NovoClienteErro)

        End If

    End Sub

    ''' <summary>
    ''' Atualiza o objeto PuntoServicioOk da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaPuntoServicioOk(ByRef Respuesta As GuardarCliente.Respuesta, _
                                                  CodCliente As String, _
                                                  CodSubCliente As String, _
                                                  PuntoServicio As GuardarCliente.PuntoServicio)

        ' Cria o pontoservicio ok que sera inserido na colecao
        Dim NovoPuntoServicioOk As New GuardarCliente.PuntoServicioOk With _
                                       {.CodPuntoServicio = PuntoServicio.CodPuntoServicio}

        ' verifica se o cliente do pontoservicio ja existe na colecao de clientes ok
        Dim ClienteExistente = Respuesta.ClientesOk.FirstOrDefault(Function(cli) cli.CodCliente = CodCliente)

        If ClienteExistente IsNot Nothing Then

            Dim SubClienteOk As GuardarCliente.SubClienteOk
            If ClienteExistente.SubClientes IsNot Nothing Then

                ' se existe, busco o subcliente e adiciono o pontoservicio, caso contrario crio o subcliente antes
                SubClienteOk = ClienteExistente.SubClientes.FirstOrDefault(Function(subcli) subcli.CodSubCliente = CodSubCliente)

                If SubClienteOk Is Nothing Then
                    SubClienteOk = New GuardarCliente.SubClienteOk With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioOkColeccion _
                                    }
                    SubClienteOk.PuntosServicio.Add(NovoPuntoServicioOk)
                End If

                SubClienteOk.PuntosServicio.Add(NovoPuntoServicioOk)

            Else ' se nao existe nenhum subcliente crio o subcliente correspondente e adiciono na colecao juntamente com o seu pnto de servicio
                Dim NovoSubClienteOk As New GuardarCliente.SubClienteOk With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioOkColeccion _
                                    }
                NovoSubClienteOk.PuntosServicio.Add(NovoPuntoServicioOk)

                ClienteExistente.SubClientes.Add(NovoSubClienteOk)

            End If

        Else ' se nao existe clientes, crio os tres, cliente, subcliente e ponto servicio e adiciono na colecao

            Dim NovoClienteOk As New GuardarCliente.ClienteOk With _
                                    {.CodCliente = CodCliente, _
                                     .SubClientes = New GuardarCliente.SubClienteOkColeccion _
                                    }

            Dim NovoSubClienteOk As New GuardarCliente.SubClienteOk With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioOkColeccion _
                                    }

            NovoSubClienteOk.PuntosServicio.Add(NovoPuntoServicioOk)

            NovoClienteOk.SubClientes.Add(NovoSubClienteOk)

            Respuesta.ClientesOk.Add(NovoClienteOk)

        End If

    End Sub

    ''' <summary>
    ''' Atualiza o objeto PuntoServicioError da Respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Criado
    ''' </history>
    Private Sub AtualizarRespuestaPuntoServicioError(ByRef Respuesta As GuardarCliente.Respuesta, _
                                                     CodCliente As String, _
                                                     CodSubCliente As String, _
                                                     PuntoServicio As GuardarCliente.PuntoServicio, _
                                                     DescripcionError As String)

        ' cria o puntoservicio para adicionar na colecao de erro
        Dim NovoPuntoServicioError As New GuardarCliente.PuntoServicioError With _
                                       {.CodPuntoServicio = PuntoServicio.CodPuntoServicio, _
                                        .DescripcionError = DescripcionError}

        ' verifica se o cliente ja existe na colecao de erro
        Dim ClienteExistente = Respuesta.ClientesError.FirstOrDefault(Function(cli) cli.CodCliente = CodCliente)

        If ClienteExistente IsNot Nothing Then
            ' se o cliente ja existe na colecao de erro e há subclientes, adiciona o punto de servico no sub cliente correspondente
            Dim SubClienteComErro As GuardarCliente.SubClienteError = Nothing
            If ClienteExistente.SubClientes IsNot Nothing Then

                ' Se o subcliente do ponto de servico ja existe na colecao de erros, adiciono o ponto de servico, caso contrario
                ' crio o subcliente e o adiciono antes
                SubClienteComErro = ClienteExistente.SubClientes.FirstOrDefault(Function(subcli) subcli.CodSubCliente = CodSubCliente)

                If SubClienteComErro Is Nothing Then
                    SubClienteComErro = New GuardarCliente.SubClienteError With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioErrorColeccion _
                                    }

                    ClienteExistente.SubClientes.Add(SubClienteComErro)
                End If

                SubClienteComErro.PuntosServicio.Add(NovoPuntoServicioError)

            Else ' Se nao existe nenhum subcliente na colecao, crio o subcliente do pontoservicio e adiciono os dois na colecao de erro
                SubClienteComErro = New GuardarCliente.SubClienteError With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioErrorColeccion _
                                    }
                SubClienteComErro.PuntosServicio.Add(NovoPuntoServicioError)

                ClienteExistente.SubClientes.Add(SubClienteComErro)

            End If

        Else ' Se nao existe o cliente, crio todos os tres objetos e os adiciono na colecao de erro

            Dim NovoClienteError As New GuardarCliente.ClienteError With _
                                    {.CodCliente = CodCliente, _
                                     .SubClientes = New GuardarCliente.SubClienteErrorColeccion _
                                    }

            Dim NovoSubClienteError As New GuardarCliente.SubClienteError With _
                                    {.CodSubCliente = CodSubCliente, _
                                     .PuntosServicio = New GuardarCliente.PuntoServicioErrorColeccion _
                                    }

            NovoSubClienteError.PuntosServicio.Add(NovoPuntoServicioError)

            NovoClienteError.SubClientes.Add(NovoSubClienteError)

            Respuesta.ClientesError.Add(NovoClienteError)

        End If

    End Sub

End Class