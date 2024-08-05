Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.UtilHelper
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis
Imports System.Data
Imports Prosegur.Genesis.Excepcion
Imports Prosegur.Global.GesEfectivo
Imports System.Text
Imports System.Runtime.Caching

Namespace Genesis

    Public Class MaestroCuenta

#Region "[Obtener/Generar Cuenta]"

        Public Shared Function ObtenerCuentaPorIdentificador(IdentificadorCuenta As String,
                                                      TipoCuenta As Enumeradores.TipoCuenta,
                                                      DescripcionUsuario As String) As Clases.Cuenta

            If String.IsNullOrEmpty(IdentificadorCuenta) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCuenta"))
            End If

            'Primeiro verifica se a conta já existe no cache
            Dim objCuentaRespuesta As Clases.Cuenta = BuscarCuentaEnCache(IdentificadorCuenta, TipoCuenta)

            'se não existe no chache então fazer a busca na base de datos
            If objCuentaRespuesta Is Nothing Then
                objCuentaRespuesta = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaPorIdentificador(IdentificadorCuenta, TipoCuenta, DescripcionUsuario)
                If objCuentaRespuesta IsNot Nothing Then
                    GrabarCuentaenCache(objCuentaRespuesta)
                End If
            End If

            Return objCuentaRespuesta

        End Function

        Public Shared Sub ObtenerCuentas(ByRef cuentaMovimiento As Clases.Cuenta,
                                         ByRef cuentaSaldo As Clases.Cuenta,
                                         esDocumentoDeValor As Boolean,
                                         Optional IdentificadorAjeno As String = "",
                                         Optional ObtenerVersionSimplificada As Boolean = False,
                                         Optional ByRef Validaciones As List(Of Contractos.Integracion.Comon.ValidacionError) = Nothing,
                                         Optional ByRef CuentasPosibles As ObservableCollection(Of Clases.Cuenta) = Nothing,
                                         Optional CrearConfiguiracionNivelSaldo As Boolean? = Nothing,
                                         Optional PermitirCualquierTotalizadorSaldoServicio As Boolean? = Nothing,
                                         Optional ByRef log As StringBuilder = Nothing)

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim usuario As String = cuentaMovimiento.UsuarioModificacion

            If log Is Nothing Then log = New StringBuilder
            If Validaciones Is Nothing Then Validaciones = New List(Of Contractos.Integracion.Comon.ValidacionError)
            ' Obtener valores de Cuenta movimiento
            Try
                TiempoParcial = Now

                'Foram informados dados para conta de movimento?
                ValidarCamposObrigatoriosCuenta(cuentaMovimiento, True)
                'Se não deu erro então segue o fluxo

                If esDocumentoDeValor Then
                    Dim objCuenta As Clases.Cuenta = Nothing
                    'a conta saldo será a mesma conta de movimento
                    'recupera a conta e verifica se a conta já movimento e saldo, ou seja ambos
                    If Not String.IsNullOrEmpty(cuentaMovimiento.Identificador) Then
                        objCuenta = ObtenerCuentaPorIdentificador(cuentaMovimiento.Identificador, Enumeradores.TipoCuenta.Movimiento, usuario)
                    End If

                    'se a conta não existe então será criada
                    If objCuenta Is Nothing Then
                        cuentaMovimiento.TipoCuenta = Enumeradores.TipoCuenta.Ambos
                        ObtenerCuentaPorCodigos(cuentaMovimiento, IdentificadorAjeno, ObtenerVersionSimplificada, Validaciones)
                        objCuenta = cuentaMovimiento
                    ElseIf objCuenta.TipoCuenta <> Enumeradores.TipoCuenta.Ambos Then
                        AccesoDatos.GenesisSaldos.Cuenta.ActualizarTipoCuenta(objCuenta.Identificador, Enumeradores.TipoCuenta.Ambos, usuario)
                    End If

                    'a conta saldo será a mesma conta de movimiento
                    cuentaSaldo = objCuenta
                Else

                    If Not String.IsNullOrWhiteSpace(cuentaMovimiento.Identificador) Then
                        cuentaMovimiento = ObtenerCuentaPorIdentificador(cuentaMovimiento.Identificador, Enumeradores.TipoCuenta.Movimiento, usuario)
                    Else
                        ObtenerCuentaPorCodigos(cuentaMovimiento, IdentificadorAjeno)
                    End If

                    If cuentaMovimiento IsNot Nothing Then
                        ' a conta existia ou foi criada acima

                        'Foram informados dados para conta de saldo?
                        If cuentaSaldo Is Nothing Then

                            'Verificar si existe na tabela “SAPR_TNIVEL_SALDO” algum item onde a conta movimento seja a mesma (em memória) e o registro encontrado esteja marcado como “padrão”.
                            Dim identificadorCuentaSaldo As String = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(cuentaMovimiento.Identificador)

                            'Foi encontrado algum item marcado como padrão pra conta movimento?
                            If Not String.IsNullOrWhiteSpace(identificadorCuentaSaldo) Then
                                'Recupera os todos os dados da conta
                                cuentaSaldo = ObtenerCuentaPorIdentificador(identificadorCuentaSaldo, Enumeradores.TipoCuenta.Saldo, cuentaMovimiento.UsuarioModificacion)
                            Else
                                'verificar se existe na tabela de “SAPR_TCONFIG_NIVEL_MOVIMIENTO” algum item com os mesmos dados da conta movimento (em memória) marcado como “padrão” (considerar primeiro itens com subcanais e depois sem subcanais).
                                Dim totalizadoresSaldos = AccesoDatos.Comon.RecuperarTotalizadoresSaldos("",
                                                                                                      "",
                                                                                                      "",
                                                                                                      cuentaMovimiento.Cliente.Identificador,
                                                                                                      If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                      If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                      cuentaMovimiento.SubCanal.Identificador)

                                If totalizadoresSaldos IsNot Nothing AndAlso totalizadoresSaldos.Count > 0 AndAlso totalizadoresSaldos.Exists(Function(e) e.bolDefecto) Then
                                    'Verificar se existe na tabela de “SAPR_TCONFIG_NIVEL_MOVIMIENTO” algum item com os mesmos dados da conta movimento (em memória) 
                                    'marcado como “padrão” (considerar primeiro itens com subcanais e depois sem subcanais).

                                    'Primeiro tenta encontrar uma configuração PADRAO para o mesmo canal da conta movimento
                                    Dim codigoSubCanal = cuentaMovimiento.SubCanal.Codigo
                                    Dim configuracaoPadrao = totalizadoresSaldos.Where(Function(c) c.bolDefecto AndAlso c.SubCanales.Exists(Function(sc) sc.Codigo = codigoSubCanal)).FirstOrDefault

                                    'senão encontrou
                                    If configuracaoPadrao Is Nothing Then
                                        'Procurar por uma configuração PADRAO para qualquer canal
                                        configuracaoPadrao = totalizadoresSaldos.Where(Function(c) c.bolDefecto).FirstOrDefault
                                    End If

                                    'Foi encontrado algum item marcado como padrão pra conta movimento?
                                    If configuracaoPadrao IsNot Nothing Then
                                        cuentaSaldo = New Clases.Cuenta
                                        cuentaSaldo.Cliente = configuracaoPadrao.Cliente
                                        cuentaSaldo.SubCliente = configuracaoPadrao.SubCliente
                                        cuentaSaldo.PuntoServicio = configuracaoPadrao.PuntoServicio

                                        'Mesmos dados da conta movimento
                                        cuentaSaldo.Canal = cuentaMovimiento.Canal
                                        cuentaSaldo.SubCanal = cuentaMovimiento.SubCanal
                                        cuentaSaldo.Sector = cuentaMovimiento.Sector

                                        'Criar a conta saldo
                                        cuentaSaldo.TipoCuenta = Enumeradores.TipoCuenta.Saldo
                                        cuentaSaldo.UsuarioCreacion = cuentaMovimiento.UsuarioCreacion
                                        cuentaSaldo.UsuarioModificacion = cuentaMovimiento.UsuarioModificacion

                                        'Criar / atualizar um item na tabela “SAPR_TCUENTA” com os valores passados e tipo de conta “S”
                                        ObtenerCuentaPorCodigos(cuentaSaldo, , ObtenerVersionSimplificada, Validaciones)
                                    End If
                                Else
                                    If CrearConfiguiracionNivelSaldo Is Nothing OrElse PermitirCualquierTotalizadorSaldoServicio Is Nothing Then
                                        obtenerParametrosIAC(cuentaMovimiento.Sector.Delegacion.Codigo, CrearConfiguiracionNivelSaldo, PermitirCualquierTotalizadorSaldoServicio)
                                    End If

                                    'O parâmetro “CrearConfiguiracionNivelSaldo” está marcado?
                                    ' verificar se pode criar a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                                    ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                    If CrearConfiguiracionNivelSaldo Then
                                        'Utiliza os dados da conta movimento para gerar uma configuração de totalizador de saldos na tabela “SAPR_TCONFIG_NIVEL_MOVIMIENTO” 
                                        'apontando para ela mesma (marcando-a como padrão).

                                        Dim configuracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(cuentaMovimiento.Cliente.Identificador,
                                                                                                                                     If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                                                                                     If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, Nothing))

                                        Dim identificadorNivelSaldo As String

                                        ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
                                        If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

                                            ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
                                            ' da conta de movimento (SAPR_TCUENTA) informada no método
                                            identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(cuentaMovimiento.Cliente.Identificador,
                                                                                                     cuentaMovimiento.UsuarioModificacion,
                                                                                                     If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                                                     If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, Nothing))
                                        Else

                                            ' seleciona o identificador encontrado
                                            identificadorNivelSaldo = ObtenerValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

                                        End If

                                        ' gera uma configuração PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                        Dim identificadorConfiguracionNivelMovimiento = GenerarConfiguracionNivelMovimiento(cuentaMovimiento.Cliente.Identificador,
                                                                            identificadorNivelSaldo,
                                                                            cuentaMovimiento.UsuarioModificacion,
                                                                            If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                            If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, Nothing),
                                                                            Nothing,
                                                                            Nothing,
                                                                            True)

                                        cuentaSaldo = New Clases.Cuenta
                                        cuentaSaldo.Cliente = cuentaMovimiento.Cliente
                                        cuentaSaldo.SubCliente = cuentaMovimiento.SubCliente
                                        cuentaSaldo.PuntoServicio = cuentaMovimiento.PuntoServicio

                                        'Mesmos dados da conta movimento
                                        cuentaSaldo.Canal = cuentaMovimiento.Canal
                                        cuentaSaldo.SubCanal = cuentaMovimiento.SubCanal
                                        cuentaSaldo.Sector = cuentaMovimiento.Sector

                                        'Criar a conta saldo
                                        cuentaSaldo.TipoCuenta = Enumeradores.TipoCuenta.Saldo
                                        cuentaSaldo.UsuarioCreacion = cuentaMovimiento.UsuarioCreacion
                                        cuentaSaldo.UsuarioModificacion = cuentaMovimiento.UsuarioModificacion

                                        'Criar / atualizar um item na tabela “SAPR_TCUENTA” com os valores passados e tipo de conta “S”
                                        ObtenerCuentaPorCodigos(cuentaSaldo, , ObtenerVersionSimplificada, Validaciones)
                                        If cuentaSaldo IsNot Nothing Then
                                            Dim existeNaNivelSaldo As String = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(cuentaMovimiento.Identificador, cuentaSaldo.Identificador)

                                            If String.IsNullOrWhiteSpace(existeNaNivelSaldo) Then
                                                'Criar um item na tabela “SAPR_TNIVEL_SALDO” com os dados em memória da conta movimento e da conta saldo marcando-o como “padrão”.
                                                Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(cuentaMovimiento.Identificador, cuentaSaldo.Identificador, identificadorConfiguracionNivelMovimiento, cuentaSaldo.UsuarioModificacion)
                                            End If
                                        End If
                                    Else

                                        ' se não deve criar automaticamente a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                                        ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                        ' retorna uma conta de saldo nula (não encontrada)
                                        identificadorCuentaSaldo = String.Empty

                                        Throw New Excepcion.NegocioExcepcion("Não existe uma configuração de nível saldo válida e o parâmetro (CrearConfiguiracionNivelSaldo) que permite criá-la automaticamente não está configurado")

                                    End If
                                End If

                            End If

                        Else
                            'Foram informados dados para conta de saldo.
                            'Existe um registro na tabela “SAPR_TCUENTA” com os dados da conta saldo
                            cuentaSaldo.TipoCuenta = Enumeradores.TipoCuenta.Saldo

                            'Recupera ou cria a conta
                            'Criar um item na tabela “SAPR_TCUENTA” com os valores passados e tipo de conta “S”.
                            If Not String.IsNullOrWhiteSpace(cuentaSaldo.Identificador) Then
                                cuentaSaldo = ObtenerCuentaPorIdentificador(cuentaSaldo.Identificador, Enumeradores.TipoCuenta.Saldo, cuentaMovimiento.UsuarioModificacion)
                            Else
                                ObtenerCuentaPorCodigos(cuentaSaldo, IdentificadorAjeno)
                            End If

                            'Verificar si existe na tabela “SAPR_TNIVEL_SALDO” algum item onde a conta movimento seja a mesma (em memória) e a conta saldo seja a mesma (em memória)..
                            Dim identificadorCuentaSaldo As String = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(cuentaMovimiento.Identificador, cuentaSaldo.Identificador)

                            'Foi encontrado algum item?
                            If String.IsNullOrWhiteSpace(identificadorCuentaSaldo) Then
                                'Verificar se existe na tabela de “SAPR_TCONFIG_NIVEL_MOVIMIENTO” algum item com os mesmos dados da conta movimento (em memória) e com os dados da conta de saldo (em memória) - (considerar primeiro itens com subcanais e depois sem subcanais)
                                Dim totalizadoresSaldos = AccesoDatos.Comon.RecuperarTotalizadoresSaldos(cuentaSaldo.Cliente.Identificador,
                                                                                                      If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, ""),
                                                                                                      If(cuentaSaldo.PuntoServicio IsNot Nothing, cuentaSaldo.PuntoServicio.Identificador, ""),
                                                                                                      cuentaMovimiento.Cliente.Identificador,
                                                                                                      If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                      If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                      cuentaMovimiento.SubCanal.Identificador)

                                If totalizadoresSaldos IsNot Nothing AndAlso totalizadoresSaldos.Count > 0 Then
                                    'Verificar se existe na tabela de “SAPR_TCONFIG_NIVEL_MOVIMIENTO” algum item com os mesmos dados da conta movimento (em memória) 
                                    'marcado como “padrão” (considerar primeiro itens com subcanais e depois sem subcanais).

                                    'Primeiro tenta encontrar uma configuração PADRAO para o mesmo canal da conta movimento
                                    Dim codigoSubCanal = cuentaMovimiento.SubCanal.Codigo
                                    Dim configuracaoPadrao = totalizadoresSaldos.Where(Function(c) c.SubCanales.Exists(Function(sc) sc.Codigo = codigoSubCanal)).FirstOrDefault

                                    'senão encontrou
                                    If configuracaoPadrao Is Nothing Then
                                        'Procurar por uma configuração PADRAO para qualquer canal
                                        configuracaoPadrao = totalizadoresSaldos.FirstOrDefault
                                    End If

                                    'Foi encontrado algum item marcado como padrão pra conta movimento?
                                    If configuracaoPadrao IsNot Nothing Then
                                        cuentaSaldo.Cliente = configuracaoPadrao.Cliente
                                        cuentaSaldo.SubCliente = configuracaoPadrao.SubCliente
                                        cuentaSaldo.PuntoServicio = configuracaoPadrao.PuntoServicio

                                        'Mesmos dados da conta movimento
                                        cuentaSaldo.Canal = cuentaMovimiento.Canal
                                        cuentaSaldo.SubCanal = cuentaMovimiento.SubCanal
                                        cuentaSaldo.Sector = cuentaMovimiento.Sector

                                        'Criar a conta saldo
                                        cuentaSaldo.TipoCuenta = Enumeradores.TipoCuenta.Saldo
                                        cuentaSaldo.UsuarioCreacion = cuentaMovimiento.UsuarioCreacion
                                        cuentaSaldo.UsuarioModificacion = cuentaMovimiento.UsuarioModificacion

                                        'Criar / atualizar um item na tabela “SAPR_TCUENTA” com os valores passados e tipo de conta “S”
                                        ObtenerCuentaPorCodigos(cuentaSaldo, , ObtenerVersionSimplificada, Validaciones)

                                        If cuentaSaldo IsNot Nothing Then
                                            Dim existeNaNivelSaldo As String = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(cuentaMovimiento.Identificador, cuentaSaldo.Identificador)
                                            If String.IsNullOrWhiteSpace(existeNaNivelSaldo) Then
                                                'Criar um item na tabela “SAPR_TNIVEL_SALDO” com os dados em memória da conta movimento e da conta saldo (deve marcá-lo como padrão caso assim esteja na tabela de configuração).
                                                Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(cuentaMovimiento.Identificador, cuentaSaldo.Identificador, configuracaoPadrao.IdentificadorNivelMovimiento, cuentaSaldo.UsuarioModificacion)
                                            End If
                                        End If
                                    End If
                                Else
                                    'Não foi encontrado um Totalizador SALDO
                                    'O parâmetro “PermitirCualquierTotalizadorSaldoServicio” está marcado?
                                    'Recupera o parametro
                                    If CrearConfiguiracionNivelSaldo Is Nothing OrElse PermitirCualquierTotalizadorSaldoServicio Is Nothing Then
                                        obtenerParametrosIAC(cuentaMovimiento.Sector.Delegacion.Codigo, CrearConfiguiracionNivelSaldo, PermitirCualquierTotalizadorSaldoServicio)
                                    End If

                                    If PermitirCualquierTotalizadorSaldoServicio Then
                                        'Utiliza os dados da conta saldo para gerar uma configuração de totalizador de saldos na tabela “SAPR_TCONFIG_NIVEL_MOVIMIENTO” apontando para conta movimento. Não marcá-lo como padrão.
                                        Dim configuracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(cuentaSaldo.Cliente.Identificador,
                                                                                                                                 If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, Nothing),
                                                                                                                                 If(cuentaSaldo.PuntoServicio IsNot Nothing, cuentaSaldo.PuntoServicio.Identificador, Nothing))

                                        Dim identificadorNivelSaldo As String

                                        ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
                                        If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

                                            ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
                                            ' da conta de movimento (SAPR_TCUENTA) informada no método
                                            identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(cuentaSaldo.Cliente.Identificador,
                                                                                                     cuentaSaldo.UsuarioModificacion,
                                                                                                     If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, Nothing),
                                                                                                     If(cuentaSaldo.PuntoServicio IsNot Nothing, cuentaSaldo.PuntoServicio.Identificador, Nothing))
                                        Else

                                            ' seleciona o identificador encontrado
                                            identificadorNivelSaldo = ObtenerValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

                                        End If

                                        Dim identificadorConfiguracionNivelMovimiento = GenerarConfiguracionNivelMovimiento(cuentaMovimiento.Cliente.Identificador,
                                                                            identificadorNivelSaldo,
                                                                            cuentaMovimiento.UsuarioModificacion,
                                                                            If(cuentaMovimiento.SubCliente IsNot Nothing, cuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                            If(cuentaMovimiento.PuntoServicio IsNot Nothing, cuentaMovimiento.PuntoServicio.Identificador, Nothing),
                                                                            Nothing,
                                                                            Nothing,
                                                                            False)

                                        Dim existeNaNivelSaldo As String = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(cuentaMovimiento.Identificador, cuentaSaldo.Identificador)
                                        If String.IsNullOrWhiteSpace(existeNaNivelSaldo) Then
                                            'Criar um item na tabela “SAPR_TNIVEL_SALDO” com os dados em memória da conta movimento e da conta saldo (deve marcá-lo como padrão caso assim esteja na tabela de configuração).
                                            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(cuentaMovimiento.Identificador, cuentaSaldo.Identificador, identificadorConfiguracionNivelMovimiento, cuentaSaldo.UsuarioModificacion)
                                        End If
                                    Else
                                        'Retornar uma mensagem de erro. A conta saldo informada não está entre os totalizadores permitidos para o cliente e o parâmetro de criação automática não está configurado.
                                        Throw New Excepcion.NegocioExcepcion("ObtenerCuentas: - A conta saldo informada não está entre os totalizadores permitidos para o cliente e o parâmetro de criação automática não está configurado.")
                                    End If
                                End If
                            End If
                        End If

                        log.AppendLine("ObtenerCuentas - Obtener/Generar (Movimiento): " & Now.Subtract(TiempoParcial).ToString() & vbNewLine & "; ")

                    End If
                End If
            Catch ex As Excepcion.NegocioExcepcion
                Validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL100", .descripcion = "ObtenerCuentas - Obtener/Generar (Movimiento): " & ex.Message})
                Throw New Excepcion.NegocioExcepcion("ObtenerCuentas - Obtener/Generar (Movimiento): " & ex.Message)

            Catch ex As Exception
                Validaciones.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL100", .descripcion = "ObtenerCuentas - Obtener/Generar (Movimiento): " & ex.Message})
                Throw New Excepcion.NegocioExcepcion("ObtenerCuentas - Obtener/Generar (Movimiento): " & ex.Message)
            End Try

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("ObtenerCuentas - Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

        End Sub

        Private Shared Sub ValidarCamposObrigatoriosCuenta(cuenta As Clases.Cuenta, cuentaSaldo As Boolean)
            If cuenta Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), String.Format("Cuenta {0}", If(cuentaSaldo, "Saldo", "Movimiento"))))
            End If

            'Verifica se foi informado o identificador da conta
            If String.IsNullOrEmpty(cuenta.Identificador) Then

                'senão foi informado o identificador então verifica se os outros dados foram informados
                If cuenta.Cliente Is Nothing OrElse (String.IsNullOrWhiteSpace(cuenta.Cliente.Codigo) AndAlso String.IsNullOrWhiteSpace(cuenta.Cliente.Identificador)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Cliente"))
                End If

                If cuenta.Canal Is Nothing OrElse (String.IsNullOrWhiteSpace(cuenta.Canal.Codigo) AndAlso String.IsNullOrWhiteSpace(cuenta.Canal.Identificador)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "SubCanal"))
                End If

                If cuenta.SubCanal Is Nothing OrElse (String.IsNullOrWhiteSpace(cuenta.SubCanal.Codigo) AndAlso String.IsNullOrWhiteSpace(cuenta.SubCanal.Identificador)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "SubCanal"))
                End If

                If cuenta.Sector Is Nothing OrElse (String.IsNullOrWhiteSpace(cuenta.Sector.Codigo) AndAlso String.IsNullOrWhiteSpace(cuenta.Sector.Identificador)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Sector"))
                End If

                If cuenta.Sector.Delegacion Is Nothing OrElse (String.IsNullOrWhiteSpace(cuenta.Sector.Delegacion.Codigo) AndAlso String.IsNullOrWhiteSpace(cuenta.Sector.Delegacion.Identificador)) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Delegacion"))
                End If

            End If
        End Sub

        Private Shared Function RecuperarIdentificadores(cuenta As Clases.Cuenta) As Clases.Cuenta
            If String.IsNullOrWhiteSpace(cuenta.Cliente.Identificador) Then
                cuenta.Cliente = AccesoDatos.Genesis.Cliente.ObtenerCliente(cuenta.Cliente.Codigo)

                If cuenta.Cliente Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Cliente não existe no IAC."))
                End If
            End If

            If cuenta.SubCliente IsNot Nothing AndAlso (String.IsNullOrWhiteSpace(cuenta.SubCliente.Identificador) AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Codigo)) Then
                cuenta.SubCliente = AccesoDatos.Genesis.SubCliente.ObtenerSubCliente(cuenta.SubCliente.Codigo)

                If cuenta.SubCliente Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "SubCliente não existe no IAC."))
                End If
            End If

            If cuenta.PuntoServicio IsNot Nothing AndAlso (String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Identificador) AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Codigo)) Then
                cuenta.PuntoServicio = AccesoDatos.Genesis.PuntoServicio.ObtenerPuntoServicio(cuenta.PuntoServicio.Codigo)

                If cuenta.PuntoServicio Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "PuntoServicio não existe no IAC."))
                End If
            End If

            If String.IsNullOrWhiteSpace(cuenta.Canal.Identificador) Then

                Dim identificadorCanal = AccesoDatos.Genesis.Canal.Validar(cuenta.Canal.Codigo, String.Empty)

                If String.IsNullOrWhiteSpace(identificadorCanal) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Canal não existe no IAC."))
                End If

                cuenta.Canal.Identificador = identificadorCanal
            End If

            If String.IsNullOrWhiteSpace(cuenta.SubCanal.Identificador) Then
                cuenta.SubCanal = AccesoDatos.Genesis.SubCanal.ObtenerSubCanalPorCodigo(cuenta.SubCanal.Codigo)

                If cuenta.SubCanal Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "SubCanal não existe no IAC."))
                End If
            End If

            If String.IsNullOrWhiteSpace(cuenta.Sector.Identificador) Then
                cuenta.Sector = AccesoDatos.Genesis.Sector.ObtenerSector(cuenta.Sector.Delegacion.Codigo, String.Empty, cuenta.Sector.Codigo)

                If cuenta.Sector Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "Sector não existe no IAC."))
                End If
            End If

            Return cuenta
        End Function

        Public Shared Function ObtenerCuenta(identificadorCliente As String,
                                      identificadorSubCliente As String,
                                      identificadorPuntoServicio As String,
                                      identificadorCanal As String,
                                      identificadorSubCanal As String,
                                      identificadorDelegacion As String,
                                      identificadorPlanta As String,
                                      identificadorSector As String,
                                      tipo As Enumeradores.TipoSitio,
                                      TipoCuenta As Enumeradores.TipoCuenta,
                                      ByRef validaciones As List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError),
                                      DescripcionUsuario As String,
                                      obtenerDatosCuentas As Boolean) As Prosegur.Genesis.Comon.Clases.Cuenta

            If String.IsNullOrEmpty(identificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            If String.IsNullOrEmpty(identificadorSubCanal) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorSubCanal"))
            End If

            If String.IsNullOrEmpty(identificadorSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorSector"))
            End If

            Dim objCuentaRespuesta As Clases.Cuenta = BuscarCuentaEnCachePorIdentificadores(identificadorCliente,
                                                                                    identificadorSubCliente,
                                                                                    identificadorPuntoServicio,
                                                                                    identificadorCanal,
                                                                                    identificadorSubCanal,
                                                                                    identificadorDelegacion,
                                                                                    identificadorPlanta,
                                                                                    identificadorSector,
                                                                                    TipoCuenta)

            If objCuentaRespuesta Is Nothing Then

                objCuentaRespuesta = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuenta(identificadorCliente,
                                                                                    identificadorSubCliente,
                                                                                    identificadorPuntoServicio,
                                                                                    identificadorCanal,
                                                                                    identificadorSubCanal,
                                                                                    identificadorDelegacion,
                                                                                    identificadorPlanta,
                                                                                    identificadorSector,
                                                                                    tipo,
                                                                                    TipoCuenta,
                                                                                    validaciones,
                                                                                    DescripcionUsuario,
                                                                                    obtenerDatosCuentas)

                If objCuentaRespuesta IsNot Nothing Then
                    GrabarCuentaEnCache(objCuentaRespuesta)
                End If
            End If

            Return objCuentaRespuesta

        End Function

        Private Shared Sub ObtenerCuentaPorCodigos(ByRef cuenta As Clases.Cuenta,
                                          Optional IdentificadorAjeno As String = "",
                                          Optional ObtenerVersionSimplificada As Boolean = False,
                                          Optional ByRef Validaciones As List(Of Contractos.Integracion.Comon.ValidacionError) = Nothing)

            ' Verificar Campos obrigatorios
            If cuenta Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_codigo_cliente_vazio"), ""))
            End If

            Dim _codigoCliente As String = cuenta.Cliente.Codigo
            Dim _codigoSubCliente As String = If(Cuenta.SubCliente IsNot Nothing, Cuenta.SubCliente.Codigo, String.Empty)
            Dim _codigoPuntoServicio As String = If(Cuenta.PuntoServicio IsNot Nothing, Cuenta.PuntoServicio.Codigo, String.Empty)
            Dim _codigoCanal As String = Cuenta.Canal.Codigo
            Dim _codigoSubCanal As String = Cuenta.SubCanal.Codigo
            Dim _codigoDelegacion As String = Cuenta.Sector.Delegacion.Codigo
            Dim _codigoPlanta As String = Cuenta.Sector.Planta.Codigo
            Dim _codigoSector As String = Cuenta.Sector.Codigo

            If String.IsNullOrEmpty(_codigoCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_codigo_cliente_vazio"), _codigoCliente))
            End If

            If String.IsNullOrEmpty(_codigoSubCanal) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_codigo_subcanal_vazio"), _codigoSubCanal))
            End If

            If String.IsNullOrEmpty(_codigoSector) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_codigo_sector_vazio"), _codigoSector))
            End If

            'Verifica se a conta já existe em cache
            Dim cuentaResposta = BuscarCuentaEnCachePorCodigos(_codigoCliente,
                                        _codigoSubCliente,
                                        _codigoPuntoServicio,
                                        _codigoCanal,
                                        _codigoSubCanal,
                                        _codigoDelegacion,
                                        _codigoPlanta,
                                        _codigoSector, Nothing)

            

            If cuentaResposta Is Nothing OrElse String.IsNullOrEmpty(cuentaResposta.Identificador) Then

                cuenta = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos(cuenta,
                                                                         IdentificadorAjeno)

                GrabarCuentaEnCache(cuenta)
            Else
                cuenta = cuentaResposta
            End If
        End Sub

        Private Shared Sub ObtenerCuentaSaldo(CuentaMovimiento As Clases.Cuenta,
                                      ByRef cuentaSaldo As Clases.Cuenta,
                             Optional IdentificadorAjeno As String = "",
                             Optional ObtenerVersionSimplificada As Boolean = False,
                             Optional ByRef Validaciones As List(Of Contractos.Integracion.Comon.ValidacionError) = Nothing,
                             Optional ByRef CuentasPosibles As ObservableCollection(Of Clases.Cuenta) = Nothing,
                             Optional CrearConfiguiracionNivelSaldo As Boolean? = Nothing,
                             Optional PermitirCualquierTotalizadorSaldoServicio As Boolean? = Nothing)

            Dim recuperarCuenta As Boolean = True

            If CuentaMovimiento IsNot Nothing Then

                ' recupera a conta de saldo para conta de movimento (SAPR_TNIVEL_SALDO)
                Dim IdentificadorCuentaSaldo As String
                IdentificadorCuentaSaldo = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaSaldoPorCuentaMovimiento_v2(CuentaMovimiento.Identificador,
                                                                                                                  If(cuentaSaldo IsNot Nothing, cuentaSaldo.Identificador, Nothing))

                ' se não encontra a conta de saldo para conta de movimento (SAPR_TNIVEL_SALDO)
                If String.IsNullOrEmpty(IdentificadorCuentaSaldo) Then

                    If CrearConfiguiracionNivelSaldo Is Nothing OrElse PermitirCualquierTotalizadorSaldoServicio Is Nothing Then
                        obtenerParametrosIAC(CuentaMovimiento.Sector.Delegacion.Codigo, CrearConfiguiracionNivelSaldo, PermitirCualquierTotalizadorSaldoServicio)
                    End If

                    ' Caso a conta seja simplicificada, busco todas as informações
                    If CuentaMovimiento.Cliente Is Nothing Then
                        Dim usuario As String = CuentaMovimiento.UsuarioModificacion
                        CuentaMovimiento = AccesoDatos.GenesisSaldos.Cuenta.ObtenerCuentaPorIdentificador(CuentaMovimiento.Identificador,
                                                                                                          Enumeradores.TipoCuenta.Movimiento,
                                                                                                          CuentaMovimiento.UsuarioModificacion)
                        CuentaMovimiento.UsuarioModificacion = usuario
                    End If

                    Dim configuracionNivelMovimientoSaldo As New List(Of Clases.TotalizadorSaldo)

                    If cuentaSaldo Is Nothing Then

                        ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
                        ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                        configuracionNivelMovimientoSaldo = AccesoDatos.Comon.RecuperarTotalizadoresSaldos("",
                                                                                                  "",
                                                                                                  "",
                                                                                                  CuentaMovimiento.Cliente.Identificador,
                                                                                                  If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                  If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                  CuentaMovimiento.SubCanal.Identificador)

                        ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
                        ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)

                        If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

                            ' verificar se pode criar a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                            ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                            If CrearConfiguiracionNivelSaldo Then

                                ' a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                ' será criada utilizando os dados da própria conta de movimento passada ao método, ou seja, o apontamento de nível de saldo padrão
                                ' será para a própria combinação (cliente, subcliente e ponto de serviço) encontrada na conta de movimento

                                ' verifica se existe uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) com os 
                                ' mesmos dados da conta de movimento (SAPR_TCUENTA) informada no método
                                Dim configuracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(CuentaMovimiento.Cliente.Identificador,
                                                                                                                             If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                                                                             If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, Nothing))

                                Dim identificadorNivelSaldo As String

                                ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
                                If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

                                    ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
                                    ' da conta de movimento (SAPR_TCUENTA) informada no método
                                    identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(CuentaMovimiento.Cliente.Identificador,
                                                                                             CuentaMovimiento.UsuarioModificacion,
                                                                                             If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                                             If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, Nothing))
                                Else

                                    ' seleciona o identificador encontrado
                                    identificadorNivelSaldo = ObtenerValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

                                End If

                                ' gera uma configuração PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                GenerarConfiguracionNivelMovimiento(CuentaMovimiento.Cliente.Identificador,
                                                                    identificadorNivelSaldo,
                                                                    CuentaMovimiento.UsuarioModificacion,
                                                                    If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                    If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, Nothing),
                                                                    Nothing,
                                                                    Nothing,
                                                                    True)

                                ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                                ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                configuracionNivelMovimientoSaldo = AccesoDatos.Comon.RecuperarTotalizadoresSaldos("",
                                                                                                  "",
                                                                                                  "",
                                                                                                  CuentaMovimiento.Cliente.Identificador,
                                                                                                  If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                  If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                  CuentaMovimiento.SubCanal.Identificador)

                                IdentificadorCuentaSaldo = configuracionNivelMovimientoSaldo(0).IdentificadorNivelSaldo

                            Else

                                ' se não deve criar automaticamente a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                                ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                                ' retorna uma conta de saldo nula (não encontrada)
                                IdentificadorCuentaSaldo = String.Empty

                                Throw New Excepcion.NegocioExcepcion("Não existe uma configuração de nível saldo válida e o parâmetro (CrearConfiguiracionNivelSaldo) que permite criá-la automaticamente não está configurado")

                            End If

                        Else
                            'Verificar se existe na tabela de “SAPR_TCONFIG_NIVEL_MOVIMIENTO” algum item com os mesmos dados da conta movimento (em memória) 
                            'marcado como “padrão” (considerar primeiro itens com subcanais e depois sem subcanais).

                            'Primeiro tenta encontrar uma configuração PADRAO para o mesmo canal da conta movimento
                            Dim configuracaoPadrao = configuracionNivelMovimientoSaldo.Where(Function(c) c.bolDefecto AndAlso c.SubCanales.Exists(Function(sc) sc.Codigo = CuentaMovimiento.SubCanal.Codigo)).FirstOrDefault

                            'senão encontrou
                            If configuracaoPadrao Is Nothing Then
                                'Procurar por uma configuração PADRAO para qualquer canal
                                configuracaoPadrao = configuracionNivelMovimientoSaldo.Where(Function(c) c.bolDefecto).FirstOrDefault
                            End If

                            'Foi encontrado algum item marcado como padrão pra conta movimento?
                            If configuracaoPadrao IsNot Nothing Then
                                'Criar / atualizar um item na tabela “SAPR_TCUENTA” com os valores passados e tipo de conta “S”
                                cuentaSaldo = New Clases.Cuenta
                                cuentaSaldo.Cliente = configuracaoPadrao.Cliente
                                cuentaSaldo.SubCliente = configuracaoPadrao.SubCliente
                                cuentaSaldo.PuntoServicio = configuracaoPadrao.PuntoServicio

                                'Mesmos dados da conta movimento
                                cuentaSaldo.Canal = CuentaMovimiento.Canal
                                cuentaSaldo.SubCanal = CuentaMovimiento.SubCanal
                                cuentaSaldo.Sector = CuentaMovimiento.Sector

                                'Criar a conta saldo
                                cuentaSaldo.TipoCuenta = Enumeradores.TipoCuenta.Saldo
                                cuentaSaldo.UsuarioCreacion = CuentaMovimiento.UsuarioCreacion
                                cuentaSaldo.UsuarioModificacion = CuentaMovimiento.UsuarioModificacion
                                ObtenerCuentaPorCodigos(cuentaSaldo, , ObtenerVersionSimplificada, Validaciones)

                                If cuentaSaldo IsNot Nothing Then
                                    recuperarCuenta = False

                                    ' cria o nível de saldo para a conta de saldo / conta de movimento (SAPR_TNIVEL_SALDO)
                                    ' dessa maneira, a próxima consulta será mais rápida (uma vez que já terá um registro com a configuração)
                                    Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(CuentaMovimiento.Identificador, cuentaSaldo.Identificador, configuracaoPadrao.IdentificadorNivelMovimiento, cuentaSaldo.UsuarioModificacion)
                                End If
                            End If
                        End If

                    Else
                        ' FOI INFORMADO CUENTA SALDO

                        configuracionNivelMovimientoSaldo = AccesoDatos.Comon.RecuperarTotalizadoresSaldos(cuentaSaldo.Cliente.Identificador,
                                                                                                  If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, ""),
                                                                                                  If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, ""),
                                                                                                  CuentaMovimiento.Cliente.Identificador,
                                                                                                  If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                  If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                  CuentaMovimiento.SubCanal.Identificador)

                        ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
                        ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                        If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

                            If PermitirCualquierTotalizadorSaldoServicio Then

                                Dim configuracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(cuentaSaldo.Cliente.Identificador,
                                                                                                                             If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, Nothing),
                                                                                                                             If(cuentaSaldo.PuntoServicio IsNot Nothing, cuentaSaldo.PuntoServicio.Identificador, Nothing))

                                Dim identificadorNivelSaldo As String

                                ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
                                If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

                                    ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
                                    ' da conta de movimento (SAPR_TCUENTA) informada no método
                                    identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(cuentaSaldo.Cliente.Identificador,
                                                                                             cuentaSaldo.UsuarioModificacion,
                                                                                             If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, Nothing),
                                                                                             If(cuentaSaldo.PuntoServicio IsNot Nothing, cuentaSaldo.PuntoServicio.Identificador, Nothing))
                                Else

                                    ' seleciona o identificador encontrado
                                    identificadorNivelSaldo = ObtenerValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

                                End If

                                GenerarConfiguracionNivelMovimiento(CuentaMovimiento.Cliente.Identificador,
                                                                    identificadorNivelSaldo,
                                                                    CuentaMovimiento.UsuarioModificacion,
                                                                    If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, Nothing),
                                                                    If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, Nothing),
                                                                    Nothing,
                                                                    Nothing,
                                                                    False)

                                configuracionNivelMovimientoSaldo = AccesoDatos.Comon.RecuperarTotalizadoresSaldos(cuentaSaldo.Cliente.Identificador,
                                                                                                  If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, ""),
                                                                                                  If(cuentaSaldo.SubCliente IsNot Nothing, cuentaSaldo.SubCliente.Identificador, ""),
                                                                                                  CuentaMovimiento.Cliente.Identificador,
                                                                                                  If(CuentaMovimiento.SubCliente IsNot Nothing, CuentaMovimiento.SubCliente.Identificador, ""),
                                                                                                  If(CuentaMovimiento.PuntoServicio IsNot Nothing, CuentaMovimiento.PuntoServicio.Identificador, ""),
                                                                                                  CuentaMovimiento.SubCanal.Identificador)

                                IdentificadorCuentaSaldo = configuracionNivelMovimientoSaldo(0).IdentificadorNivelSaldo

                            Else

                                IdentificadorCuentaSaldo = String.Empty
                                Throw New Excepcion.NegocioExcepcion("A conta saldo informada não está entre os totalizadores permitidos para o cliente e o parâmetro (PermitirCualquierTotalizadorSaldoServicio) que permite criá-la automaticamente não está configurado")

                            End If
                        Else
                            IdentificadorCuentaSaldo = configuracionNivelMovimientoSaldo(0).IdentificadorNivelSaldo
                        End If

                    End If
                Else

                    If String.IsNullOrEmpty(IdentificadorCuentaSaldo) Then
                        cuentaSaldo = Nothing
                    ElseIf recuperarCuenta Then
                        'Recupera os todos os dados da conta
                        cuentaSaldo = ObtenerCuentaPorIdentificador(IdentificadorCuentaSaldo, Enumeradores.TipoCuenta.Saldo, CuentaMovimiento.UsuarioModificacion)
                    End If
                End If
            End If

        End Sub

        Private Shared Function ObtenerConfiguracaoNivelMovimiento(IdentificadorCliente As String,
                                                         Optional IdentificadorSubCliente As String = Nothing,
                                                         Optional IdentificadorPuntoServicio As String = Nothing,
                                                         Optional IdentificadorSubCanal As String = Nothing,
                                                         Optional SolamenteConfiguracionAnterior As Boolean = False) As Dictionary(Of String, String)

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelMovimientoSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, SolamenteConfiguracionAnterior)

        End Function

        Private Shared Function ObtenerConfiguracaoNivelMovimientoSaldo(IdentificadorCliente As String,
                                                         Optional IdentificadorSubCliente As String = Nothing,
                                                         Optional IdentificadorPuntoServicio As String = Nothing,
                                                         Optional IdentificadorSubCanal As String = Nothing,
                                                         Optional IdentificadorConfigNivelSaldo As String = Nothing) As List(Of String)

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelMovimiento(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, IdentificadorConfigNivelSaldo)

        End Function

        Private Shared Function ObtenerConfiguracaoNivelSaldo(IdentificadorCliente As String,
                                                    Optional IdentificadorSubCliente As String = Nothing,
                                                    Optional IdentificadorPuntoServicio As String = Nothing) As Dictionary(Of String, String)

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio)

        End Function

#End Region

#Region "[Inserir]"

        Private Shared Function GenerarCuentaSaldos(identificadorCuentaMovimiento As String,
                                            codigoAjeno As String,
                                            codigoCliente As String,
                                            codigoSubCliente As String,
                                            codigoPuntoServicio As String,
                                            codigoCanal As String,
                                            codigoSubCanal As String,
                                            codigoDelegacion As String,
                                            codigoPlanta As String,
                                            codigoSector As String,
                                            DescripcionUsuario As String) As String

            Dim validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Dim _identificadorCliente As String = ""
            Dim _identificadorSubCliente As String = ""
            Dim _identificadorPuntoServicio As String = ""
            Dim _identificadorCanal As String = ""
            Dim _identificadorSubCanal As String = ""
            Dim _identificadorDelegacion As String = ""
            Dim _identificadorPlanta As String = ""
            Dim _identificadorSector As String = ""
            Dim _codigoDelegacion As String = codigoDelegacion

            If Not String.IsNullOrEmpty(codigoAjeno) Then

                Dim identificadores As DataTable = AccesoDatos.GenesisSaldos.Cuenta.ObtenerIdentificadorPorCodigosAjeno(codigoAjeno, codigoCliente, codigoSubCliente, codigoPuntoServicio, codigoCanal, codigoSubCanal, codigoDelegacion, codigoPlanta, codigoSector)

                If identificadores IsNot Nothing AndAlso identificadores.Rows.Count > 0 Then

                    If Not String.IsNullOrEmpty(codigoCliente) Then
                        Dim cliente = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TCLIENTE' AND COD_AJENO = '" & codigoCliente & "'")
                        If cliente IsNot Nothing AndAlso cliente.Count = 1 Then
                            _identificadorCliente = Util.AtribuirValorObj(cliente(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorCliente) Then
                            Throw New Excepcion.NegocioExcepcion("codigoCliente")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoSubCliente) Then
                        Dim SubCliente = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCLIENTE' AND COD_AJENO = '" & codigoSubCliente & "'")
                        If SubCliente IsNot Nothing AndAlso SubCliente.Count = 1 Then
                            _identificadorSubCliente = Util.AtribuirValorObj(SubCliente(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorSubCliente) Then
                            Throw New Excepcion.NegocioExcepcion("codigoSubCliente")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                        Dim puntoServicio = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' AND COD_AJENO = '" & codigoPuntoServicio & "'")
                        If puntoServicio IsNot Nothing AndAlso puntoServicio.Count = 1 Then
                            _identificadorPuntoServicio = Util.AtribuirValorObj(puntoServicio(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorPuntoServicio) Then
                            Throw New Excepcion.NegocioExcepcion("codigoPuntoServicio")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoCanal) Then
                        Dim Canal = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TCANAL' AND COD_AJENO = '" & codigoCanal & "'")
                        If Canal IsNot Nothing AndAlso Canal.Count = 1 Then
                            _identificadorCanal = Util.AtribuirValorObj(Canal(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorCanal) Then
                            Throw New Excepcion.NegocioExcepcion("codigoCanal")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoSubCanal) Then
                        Dim subCanal = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSUBCANAL' AND COD_AJENO = '" & codigoSubCanal & "'")
                        If subCanal IsNot Nothing AndAlso subCanal.Count = 1 Then
                            _identificadorSubCanal = Util.AtribuirValorObj(subCanal(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorSubCanal) Then
                            Throw New Excepcion.NegocioExcepcion("codigoSubCanal")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoDelegacion) Then
                        Dim delegacion = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND COD_AJENO = '" & codigoDelegacion & "'")
                        If delegacion IsNot Nothing AndAlso delegacion.Count = 1 Then
                            _identificadorDelegacion = Util.AtribuirValorObj(delegacion(0)("OID_TABLA_GENESIS"), GetType(String))
                            codigoDelegacion = AccesoDatos.Genesis.Delegacion.ObtenerCodigoPorIdentificador(_identificadorDelegacion)
                        End If
                        If String.IsNullOrEmpty(_identificadorDelegacion) Then
                            Throw New Excepcion.NegocioExcepcion("codigoDelegacion")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoPlanta) Then
                        Dim planta = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' AND COD_AJENO = '" & codigoPlanta & "'")
                        If planta IsNot Nothing AndAlso planta.Count = 1 Then
                            _identificadorPlanta = Util.AtribuirValorObj(planta(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorPlanta) Then
                            Throw New Excepcion.NegocioExcepcion("codigoPlanta")
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigoSector) Then
                        Dim sector = identificadores.Select(" COD_TIPO_TABLA_GENESIS = 'GEPR_TSECTOR' AND COD_AJENO = '" & codigoSector & "'")
                        If sector IsNot Nothing AndAlso sector.Count = 1 Then
                            _identificadorSector = Util.AtribuirValorObj(sector(0)("OID_TABLA_GENESIS"), GetType(String))
                        End If
                        If String.IsNullOrEmpty(_identificadorSector) Then
                            Throw New Excepcion.NegocioExcepcion("codigoSector")
                        End If
                    End If

                End If

            Else

                If Not String.IsNullOrEmpty(codigoCliente) Then
                    _identificadorCliente = AccesoDatos.Genesis.Cliente.ObtenerIdentificadorCliente(codigoCliente)
                    If String.IsNullOrEmpty(_identificadorCliente) Then
                        Throw New Excepcion.NegocioExcepcion("codigoCliente")
                    End If
                End If

                If Not String.IsNullOrEmpty(codigoSubCliente) Then
                    _identificadorSubCliente = AccesoDatos.Genesis.SubCliente.ObtenerIdentificadorSubCliente(_identificadorCliente, codigoSubCliente)
                    If String.IsNullOrEmpty(_identificadorSubCliente) Then
                        Throw New Excepcion.NegocioExcepcion("codigoSubCliente")
                    End If
                End If

                If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
                    _identificadorPuntoServicio = AccesoDatos.Genesis.PuntoServicio.ObtenerIdentificadorPuntoServicio(_identificadorCliente, _identificadorSubCliente, codigoPuntoServicio)
                    If String.IsNullOrEmpty(_identificadorPuntoServicio) Then
                        Throw New Excepcion.NegocioExcepcion("codigoPuntoServicio")
                    End If
                End If

                If Not String.IsNullOrEmpty(codigoSubCanal) Then
                    _identificadorSubCanal = AccesoDatos.Genesis.SubCanal.ObtenerIdentificadorSubCanal(codigoSubCanal)
                    If String.IsNullOrEmpty(_identificadorSubCanal) Then
                        Throw New Excepcion.NegocioExcepcion("codigoSubCanal")
                    End If
                End If

                If Not String.IsNullOrEmpty(codigoSector) Then
                    _identificadorSector = AccesoDatos.Genesis.Sector.ObtenerIdentificadorSector(codigoSector)
                    If String.IsNullOrEmpty(_identificadorSector) Then
                        Throw New Excepcion.NegocioExcepcion("codigoSector")
                    End If
                End If

            End If


            If String.IsNullOrEmpty(_identificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion("_identificadorCliente")
            End If

            If String.IsNullOrEmpty(_identificadorSubCanal) Then
                Throw New Excepcion.NegocioExcepcion("_identificadorSubCanal")
            End If

            If String.IsNullOrEmpty(_identificadorSector) Then
                Throw New Excepcion.NegocioExcepcion("_identificadorSector")
            End If



            Dim CrearConfiguiracionNivelSaldo As Boolean
            Dim PermitirCualquierTotalizadorSaldoServicio As Boolean
            obtenerParametrosIAC(codigoDelegacion, CrearConfiguiracionNivelSaldo, PermitirCualquierTotalizadorSaldoServicio)

            ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
            ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
            Dim configuracionNivelMovimientoSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelMovimiento(_identificadorCliente,
                                                                                                                        _identificadorSubCliente,
                                                                                                                        _identificadorPuntoServicio,
                                                                                                                        _identificadorSubCanal)

            ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
            ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
            If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

                ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
                ' conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                configuracionNivelMovimientoSaldo = ObtenerConfiguracaoNivelMovimiento(_identificadorCliente,
                                                                                       _identificadorSubCliente,
                                                                                       _identificadorPuntoServicio)

                ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

                    ' verificar se pode criar a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                    ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                    If CrearConfiguiracionNivelSaldo Then

                        ' a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                        ' será criada utilizando os dados da própria conta de movimento passada ao método, ou seja, o apontamento de nível de saldo padrão
                        ' será para a própria combinação (cliente, subcliente e ponto de serviço) encontrada na conta de movimento

                        ' verifica se existe uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) com os 
                        ' mesmos dados da conta de movimento (SAPR_TCUENTA) informada no método
                        Dim configuracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(_identificadorCliente,
                                                                                                                     _identificadorSubCliente,
                                                                                                                     _identificadorPuntoServicio)

                        Dim identificadorNivelSaldo As String

                        ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
                        If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

                            ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
                            ' da conta de movimento (SAPR_TCUENTA) informada no método
                            identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(_identificadorCliente,
                                                                                     DescripcionUsuario,
                                                                                     _identificadorSubCliente,
                                                                                     _identificadorPuntoServicio)
                        Else

                            ' seleciona o identificador encontrado
                            identificadorNivelSaldo = ObtenerValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

                        End If

                        ' gera uma configuração PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                        GenerarConfiguracionNivelMovimiento(_identificadorCliente,
                                                            identificadorNivelSaldo,
                                                            DescripcionUsuario,
                                                            _identificadorSubCliente,
                                                            _identificadorPuntoServicio)

                        ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
                        ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
                        configuracionNivelMovimientoSaldo = ObtenerConfiguracaoNivelMovimiento(_identificadorCliente,
                                                                                               _identificadorSubCliente,
                                                                                               _identificadorPuntoServicio)

                    End If

                End If

            End If
            ' utiliza a "configuracionNivelMovimientoSaldo" para localizar ou criar a conta de saldos

            Dim cuenta As Clases.Cuenta

            ' tenta localizar uma conta com os dados da configuração encontrada incluindo o subcanal e o setor
            cuenta = ObtenerCuenta(ObtenerValorLista("OID_CLIENTE", configuracionNivelMovimientoSaldo),
                                   ObtenerValorLista("OID_SUBCLIENTE", configuracionNivelMovimientoSaldo),
                                   ObtenerValorLista("OID_PTO_SERVICIO", configuracionNivelMovimientoSaldo),
                                   _identificadorCanal,
                                   _identificadorSubCanal,
                                   _identificadorDelegacion,
                                   _identificadorPlanta,
                                   _identificadorSector,
                                   Enumeradores.TipoSitio.NoDefinido,
                                   Enumeradores.TipoCuenta.Saldo,
                                   validaciones,
                                   DescripcionUsuario,
                                   False)

            ' cria o nível de saldo para a conta de saldo / conta de movimento (SAPR_TNIVEL_SALDO)
            ' dessa maneira, a próxima consulta será mais rápida (uma vez que já terá um registro com a configuração)
            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(identificadorCuentaMovimiento, cuenta.Identificador, ObtenerValorLista("OID_CONFIG_NIVEL_MOVIMIENTO", configuracionNivelMovimientoSaldo), DescripcionUsuario)

            If cuenta IsNot Nothing Then
                Return cuenta.Identificador
            End If

            Return Nothing

        End Function

        Private Shared Function GenerarConfiguracionNivelSaldo(IdentificadorCliente As String, DescripcionUsuario As String,
                                                              Optional IdentificadorSubCliente As String = Nothing,
                                                              Optional IdentificadorPuntoServicio As String = Nothing,
                                                              Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            If String.IsNullOrEmpty(DescripcionUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "DescripcionUsuario"))
            End If

            Dim DatosConfiguracionNivelSaldo As Dictionary(Of String, String) = ObtenerConfiguracaoNivelSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio)

            If DatosConfiguracionNivelSaldo Is Nothing OrElse DatosConfiguracionNivelSaldo.Count = 0 Then
                Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarConfiguracionNivelSaldo(IdentificadorCliente, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, Transacion)
            Else
                Return DatosConfiguracionNivelSaldo("OID_CONFIG_NIVEL_SALDO")
            End If

        End Function

        Private Shared Function GenerarConfiguracionNivelMovimiento(IdentificadorCliente As String, IdentificadorConfiguracionNivelSaldo As String,
                                                                   DescripcionUsuario As String,
                                                                   Optional IdentificadorSubCliente As String = Nothing,
                                                                   Optional IdentificadorPuntoServicio As String = Nothing,
                                                                   Optional IdentificadorSubCanal As String = Nothing,
                                                                   Optional ByRef Transacion As DbHelper.Transacao = Nothing,
                                                          Optional bolDefecto As Boolean = False) As String

            Dim validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            If String.IsNullOrEmpty(IdentificadorConfiguracionNivelSaldo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorConfiguracionNivelSaldo"))
            End If

            If String.IsNullOrEmpty(DescripcionUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "DescripcionUsuario"))
            End If

            ' gera a nova configuração de nível de movimento para as informações informadas
            Dim identificadorConfiguracionNivelMovimiento As String = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarConfiguracionNivelMovimiento(IdentificadorCliente, IdentificadorConfiguracionNivelSaldo, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, Transacion, bolDefecto)

            If bolDefecto Then
                ' verifica se existe uma configuração de nível de movimento anterior para os mesmos dados
                Dim configuracionNivelMovimientoSaldoAnterior As Dictionary(Of String, String) = ObtenerConfiguracaoNivelMovimiento(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, True)

                ' verifica se foi retornada alguma configuração anterior a criada
                If configuracionNivelMovimientoSaldoAnterior IsNot Nothing AndAlso configuracionNivelMovimientoSaldoAnterior.Count > 0 Then

                    ' recupera os dados da configuração de saldo atual
                    Dim configuracionNivelSaldo As Dictionary(Of String, String) = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelSaldoPorIdentificador(IdentificadorConfiguracionNivelSaldo)

                    Dim identificadorConfiguracionNivelMovimientoSaldoAnterior As String = configuracionNivelMovimientoSaldoAnterior("OID_CONFIG_NIVEL_MOVIMIENTO")

                    ' seleciona todas as combinações de conta de movimento e de saldo
                    ' que utilizam como base a configuração anterior
                    Dim dtNivelSaldo As DataTable = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarNivelSaldoPorConfiguracionMovimiento(identificadorConfiguracionNivelMovimientoSaldoAnterior)

                    If dtNivelSaldo IsNot Nothing AndAlso dtNivelSaldo.Rows.Count > 0 Then

                        For Each drNivelSaldo As DataRow In dtNivelSaldo.Rows

                            ' para cada nível de saldo encontrado, é necessário recuperar a conta de movimento
                            Dim cuentaMovimientoAtual As Prosegur.Genesis.Comon.Clases.Cuenta = ObtenerCuentaPorIdentificador(drNivelSaldo("OID_CUENTA_MOVIMIENTO"), Enumeradores.TipoCuenta.Movimiento, DescripcionUsuario)

                            ' se não encontra a conta de movimento, deve retornar um erro pela inconsistência no banco
                            If cuentaMovimientoAtual Is Nothing Then
                                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_cuentanoencontra"), drNivelSaldo("OID_CUENTA_MOVIMIENTO")))
                            End If

                            ' verifica se existe uma conta de saldo para a combinação informada
                            Dim cuentaSaldoNova As Prosegur.Genesis.Comon.Clases.Cuenta = ObtenerCuenta(ObtenerValorLista("OID_CLIENTE", configuracionNivelSaldo),
                                                                                                        ObtenerValorLista("OID_SUBCLIENTE", configuracionNivelSaldo),
                                                                                                        ObtenerValorLista("OID_PTO_SERVICIO", configuracionNivelSaldo),
                                                                                                        cuentaMovimientoAtual.Canal.Identificador,
                                                                                                        cuentaMovimientoAtual.SubCanal.Identificador,
                                                                                                        cuentaMovimientoAtual.Sector.Delegacion.Identificador,
                                                                                                        cuentaMovimientoAtual.Sector.Planta.Identificador,
                                                                                                        cuentaMovimientoAtual.Sector.Identificador,
                                                                                                        Enumeradores.TipoSitio.NoDefinido,
                                                                                                        Enumeradores.TipoCuenta.Saldo,
                                                                                                        validaciones,
                                                                                                        DescripcionUsuario,
                                                                                                        True)

                            ' actualiza a configuração de nível de saldo
                            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarNivelSaldo(drNivelSaldo("OID_NIVEL_SALDO"), cuentaMovimientoAtual.Identificador, cuentaSaldoNova.Identificador, DescripcionUsuario, Transacion)

                        Next

                    End If

                    ' desabilita o registro anterior
                    Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarConfiguracionNivelMovimiento(identificadorConfiguracionNivelMovimientoSaldoAnterior, False, DescripcionUsuario, Transacion)

                End If
            End If

            Return identificadorConfiguracionNivelMovimiento

        End Function

        Private Shared Sub ActualizarConfiguracionNivelMovimientoDefecto(IdentificadorConfiguracionNivelMovimiento As String,
                                                                 bolDefecto As Boolean,
                                                                 DescripcionUsuario As String,
                                                        Optional ByRef Transacion As DbHelper.Transacao = Nothing)

            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarConfiguracionNivelMovimientoDefecto(IdentificadorConfiguracionNivelMovimiento, bolDefecto, DescripcionUsuario, Transacion)

        End Sub

        Private Shared Function GenerarConfiguracionNivelMovimiento(IdentificadorCliente As String, IdentificadorConfiguracionNivelSaldo As String,
                                                                   DescripcionUsuario As String,
                                                                   bolDefecto As Boolean,
                                                                   Optional IdentificadorSubCliente As String = Nothing,
                                                                   Optional IdentificadorPuntoServicio As String = Nothing,
                                                                   Optional IdentificadorSubCanal As String = Nothing,
                                                                   Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

            Dim validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            If String.IsNullOrEmpty(IdentificadorCliente) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorCliente"))
            End If

            If String.IsNullOrEmpty(IdentificadorConfiguracionNivelSaldo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorConfiguracionNivelSaldo"))
            End If

            If String.IsNullOrEmpty(DescripcionUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "DescripcionUsuario"))
            End If

            ' gera a nova configuração de nível de movimento para as informações informadas
            Dim identificadorConfiguracionNivelMovimiento As String = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarConfiguracionNivelMovimiento(IdentificadorCliente, IdentificadorConfiguracionNivelSaldo, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, Transacion, bolDefecto)

            Return identificadorConfiguracionNivelMovimiento

        End Function

        Public Shared Sub GrabarConfiguracionesNivelMovimiento(ConfigNivelSaldo As List(Of IAC.ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov),
                                                               CodigoUsuario As String,
                                                               IdentificadorCliente As String,
                                                               Optional IdentificadorSubCliente As String = Nothing,
                                                               Optional IdentificadorPuntoServicio As String = Nothing)
            'Recupera totalizadores de saldo atualmente cadastrados
            Dim peticion As New ContractoServicio.Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos.Peticion
            With peticion
                .IdentificadorClienteMovimiento = IdentificadorCliente
                .IdentificadorSubClienteMovimiento = IdentificadorSubCliente
                .IdentificadorPuntoServicioMovimiento = IdentificadorPuntoServicio
            End With
            Dim respTotSaldoAnt = TotalizadorSaldo.RecuperarTotalizadoresSaldos(peticion)

            ' cria cada um dos niveis de saldo e movimiento
            For Each mov In ConfigNivelSaldo

                Dim oidNivelSaldo As String = Nothing
                Dim oidNivelMovimiento As String = Nothing

                ' verifica se já existe uma configuração nivel saldo (SAPR_TCONF_NIVEL_SADO)
                Dim temp As Dictionary(Of String, String) = MaestroCuenta.ObtenerConfiguracaoNivelSaldo(mov.configNivelSaldo.oidCliente, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)

                If temp.ContainsKey("OID_CONFIG_NIVEL_SALDO") AndAlso Not String.IsNullOrEmpty(temp("OID_CONFIG_NIVEL_SALDO")) Then
                    oidNivelSaldo = temp("OID_CONFIG_NIVEL_SALDO")
                Else
                    oidNivelSaldo = MaestroCuenta.GenerarConfiguracionNivelSaldo(mov.configNivelSaldo.oidCliente, CodigoUsuario, mov.configNivelSaldo.oidSubcliente, mov.configNivelSaldo.oidPtoServicio)
                End If

                ' verifica se é novo, caso seja novo pegar o oid da variavel oid
                If (mov.oidPtoServicio Is Nothing) Then
                    mov.oidPtoServicio = IdentificadorPuntoServicio
                End If
                If (mov.oidSubCliente Is Nothing) Then
                    mov.oidSubCliente = IdentificadorSubCliente
                End If
                If (mov.oidCliente Is Nothing) Then
                    mov.oidCliente = IdentificadorCliente
                End If

                ' verifica se existe o nivel movimento
                Dim oids = MaestroCuenta.ObtenerConfiguracaoNivelMovimientoSaldo(mov.oidCliente, mov.oidSubCliente, mov.oidPtoServicio, mov.oidSubCanal, oidNivelSaldo)

                Dim existe As Boolean = oids.Count > 0
                If existe AndAlso mov.bolActivo Then
                    MaestroCuenta.ActualizarConfiguracionNivelMovimientoDefecto(mov.oidConfigNivelMovimiento, mov.bolDefecto, CodigoUsuario)
                ElseIf Not existe AndAlso mov.bolActivo Then
                    oidNivelMovimiento = MaestroCuenta.GenerarConfiguracionNivelMovimiento(mov.oidCliente, oidNivelSaldo, CodigoUsuario, mov.bolDefecto, mov.oidSubCliente, mov.oidPtoServicio, mov.oidSubCanal)
                ElseIf (Not mov.bolActivo AndAlso Not String.IsNullOrEmpty(mov.oidConfigNivelMovimiento)) Then
                    MaestroCuenta.DesactivarConfiguracionNivelMovimiento(mov.oidConfigNivelMovimiento, CodigoUsuario)
                End If

                If Not String.IsNullOrEmpty(oidNivelMovimiento) Then
                    mov.oidConfigNivelMovimiento = oidNivelMovimiento
                End If

            Next

            'Desativa registro apagados
            If respTotSaldoAnt.TotalizadoresSaldos IsNot Nothing AndAlso respTotSaldoAnt.TotalizadoresSaldos.Count > 0 Then
                For Each totSaldo In respTotSaldoAnt.TotalizadoresSaldos
                    If Not ConfigNivelSaldo.Exists(Function(a) a.oidConfigNivelMovimiento = totSaldo.IdentificadorNivelMovimiento) Then
                        MaestroCuenta.DesactivarConfiguracionNivelMovimiento(totSaldo.IdentificadorNivelMovimiento, CodigoUsuario)
                    End If
                Next
            End If

        End Sub

#End Region

#Region "[Actualizaciones]"

        Public Shared Sub DesactivarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento As String, DescripcionUsuario As String)

            If String.IsNullOrEmpty(IdentificadorConfiguracionNivelMovimiento) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "IdentificadorConfiguracionNivelMovimiento"))
            End If

            If String.IsNullOrEmpty(DescripcionUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("msg_campo_obrigatorio"), "DescripcionUsuario"))
            End If

            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.DesactivarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento, DescripcionUsuario)

            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.DeletarNivelSaldo(IdentificadorConfiguracionNivelMovimiento)

        End Sub

#End Region

#Region "[Metodos]"

        Private Shared Function ObtenerValorLista(Chave As String, Lista As Dictionary(Of String, String)) As String
            If Lista.ContainsKey(Chave) Then
                Return Lista(Chave)
            Else
                Return Nothing
            End If
        End Function

        Private Shared Sub obtenerParametrosIAC(codigoDelegacion As String,
                                                ByRef CrearConfiguiracionNivelSaldo As Boolean?,
                                                ByRef PermitirCualquierTotalizadorSaldoServicio As Boolean?)

            Dim listParametros As New List(Of String)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_PERMITIR_CUALQUIER_TOT_SALDOS)

            Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(codigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)

            If parametros IsNot Nothing Then

                For Each parametro In parametros
                    If parametro.codigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO Then
                        CrearConfiguiracionNivelSaldo = (parametro.valorParametro <> "0")
                    ElseIf parametro.codigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_PERMITIR_CUALQUIER_TOT_SALDOS Then
                        PermitirCualquierTotalizadorSaldoServicio = (parametro.valorParametro <> "0")
                    End If
                Next
            End If

        End Sub

#End Region

#Region "[Servicio - Accion]"

        Public Shared Function AccionObtenerCuentaPorCodigos(Peticion As ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Peticion) As ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta

            Dim Respuesta As New ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentaPorCodigos.Respuesta()

            Try
                ' valida o token passado na petição
                Util.VerificaInformacionesToken(Peticion)

                If Peticion Is Nothing Then
                    Throw New NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_RECUPERARCUENTA_PETICION_OBLIGATORIA"))
                End If

                If Peticion.Cuenta Is Nothing Then
                    Throw New NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_RECUPERARCUENTA_PARAMETROS_OBLIGATORIOS"))
                Else

                    Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)


                    Respuesta.CuentaMovimiento = New Clases.Cuenta With {
                        .Cliente = New Clases.Cliente With {.Codigo = Peticion.Cuenta.codigoCliente},
                        .SubCliente = New Clases.SubCliente With {.Codigo = Peticion.Cuenta.codigoSubCliente},
                        .PuntoServicio = New Clases.PuntoServicio With {.Codigo = Peticion.Cuenta.codigoPuntoServicio},
                        .Canal = New Clases.Canal With {.Codigo = Peticion.Cuenta.codigoCanal},
                        .SubCanal = New Clases.SubCanal With {.Codigo = Peticion.Cuenta.codigoSubCanal},
                        .Sector = New Clases.Sector With {.Codigo = Peticion.Cuenta.codigoSector,
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = Peticion.Cuenta.codigoDelegacion},
                                                          .Planta = New Clases.Planta With {.Codigo = Peticion.Cuenta.codigoPlanta}},
                        .UsuarioModificacion = "SERVICIO_RECUPERAR_CUENTA",
                        .TipoCuenta = Enumeradores.TipoCuenta.Movimiento}
                    Respuesta.CuentaSaldo = Nothing

                    ObtenerCuentas(Respuesta.CuentaMovimiento, Respuesta.CuentaSaldo, Peticion.EsDocumentoDeValor)

                    If Respuesta.CuentaMovimiento Is Nothing Then
                        Throw New NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_RECUPERARCUENTA_CUENTA_NO_ENCONTRADA"))
                    End If

                End If

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

        Public Shared Function AccionObtenerCuentas(Peticion As ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Peticion) As ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim Respuesta As New ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Respuesta()
            Respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO
            Respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_INTEGRACION_SEM_ERRO
            Respuesta.ValidacionesError = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Try
                ' valida o token passado na petição
                Util.VerificaInformacionesToken(Peticion)

                If Peticion Is Nothing Then
                    Respuesta.ValidacionesError.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL100", .descripcion = Traduzir("NUEVO_SALDOS_SERVICIO_RECUPERARCUENTA_PETICION_OBLIGATORIA")})
                End If

                If Peticion.CuentaMovimiento Is Nothing Then
                    Respuesta.ValidacionesError.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL100", .descripcion = Traduzir("NUEVO_SALDOS_SERVICIO_RECUPERARCUENTA_PARAMETROS_OBLIGATORIOS")})
                Else

                    ' === Cuenta Movimiento
                    With Peticion.CuentaMovimiento
                        If .Cliente Is Nothing Then .Cliente = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .SubCliente Is Nothing Then .SubCliente = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .PuntoServicio Is Nothing Then .PuntoServicio = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .Canal Is Nothing Then .Canal = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .SubCanal Is Nothing Then .SubCanal = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .Delegacion Is Nothing Then .Delegacion = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .Planta Is Nothing Then .Planta = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        If .Sector Is Nothing Then .Sector = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                    End With

                    Respuesta.CuentaMovimiento = New Clases.Cuenta With {
                        .Cliente = New Clases.Cliente With {.Codigo = Peticion.CuentaMovimiento.Cliente.Codigo, .Identificador = Peticion.CuentaMovimiento.Cliente.Identificador},
                        .SubCliente = New Clases.SubCliente With {.Codigo = Peticion.CuentaMovimiento.SubCliente.Codigo, .Identificador = Peticion.CuentaMovimiento.SubCliente.Identificador},
                        .PuntoServicio = New Clases.PuntoServicio With {.Codigo = Peticion.CuentaMovimiento.PuntoServicio.Codigo, .Identificador = Peticion.CuentaMovimiento.PuntoServicio.Identificador},
                        .Canal = New Clases.Canal With {.Codigo = Peticion.CuentaMovimiento.Canal.Codigo, .Identificador = Peticion.CuentaMovimiento.Canal.Identificador},
                        .SubCanal = New Clases.SubCanal With {.Codigo = Peticion.CuentaMovimiento.SubCanal.Codigo, .Identificador = Peticion.CuentaMovimiento.SubCanal.Identificador},
                        .Sector = New Clases.Sector With {.Codigo = Peticion.CuentaMovimiento.Sector.Codigo, .Identificador = Peticion.CuentaMovimiento.Sector.Identificador,
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = Peticion.CuentaMovimiento.Delegacion.Codigo, .Identificador = Peticion.CuentaMovimiento.Delegacion.Identificador},
                                                          .Planta = New Clases.Planta With {.Codigo = Peticion.CuentaMovimiento.Planta.Codigo, .Identificador = Peticion.CuentaMovimiento.Planta.Identificador}},
                        .UsuarioModificacion = "SERVICIO_OBTENER_CUENTA_MOV",
                        .TipoCuenta = Enumeradores.TipoCuenta.Movimiento}


                    ' === Cuenta Saldo
                    If Peticion.CuentaSaldo IsNot Nothing Then
                        With Peticion.CuentaSaldo
                            If .Cliente Is Nothing Then .Cliente = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .SubCliente Is Nothing Then .SubCliente = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .PuntoServicio Is Nothing Then .PuntoServicio = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .Canal Is Nothing Then .Canal = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .SubCanal Is Nothing Then .SubCanal = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .Delegacion Is Nothing Then .Delegacion = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .Planta Is Nothing Then .Planta = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                            If .Sector Is Nothing Then .Sector = New Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor
                        End With

                        Respuesta.CuentaSaldo = New Clases.Cuenta With {
                        .Cliente = New Clases.Cliente With {.Codigo = Peticion.CuentaSaldo.Cliente.Codigo, .Identificador = Peticion.CuentaSaldo.Cliente.Identificador},
                        .SubCliente = New Clases.SubCliente With {.Codigo = Peticion.CuentaSaldo.SubCliente.Codigo, .Identificador = Peticion.CuentaSaldo.SubCliente.Identificador},
                        .PuntoServicio = New Clases.PuntoServicio With {.Codigo = Peticion.CuentaSaldo.PuntoServicio.Codigo, .Identificador = Peticion.CuentaSaldo.PuntoServicio.Identificador},
                        .Canal = New Clases.Canal With {.Codigo = Peticion.CuentaSaldo.Canal.Codigo, .Identificador = Peticion.CuentaSaldo.Canal.Identificador},
                        .SubCanal = New Clases.SubCanal With {.Codigo = Peticion.CuentaSaldo.SubCanal.Codigo, .Identificador = Peticion.CuentaSaldo.SubCanal.Identificador},
                        .Sector = New Clases.Sector With {.Codigo = Peticion.CuentaSaldo.Sector.Codigo, .Identificador = Peticion.CuentaSaldo.Sector.Identificador,
                                                          .Delegacion = New Clases.Delegacion With {.Codigo = Peticion.CuentaSaldo.Delegacion.Codigo, .Identificador = Peticion.CuentaSaldo.Delegacion.Identificador},
                                                          .Planta = New Clases.Planta With {.Codigo = Peticion.CuentaSaldo.Planta.Codigo, .Identificador = Peticion.CuentaSaldo.Planta.Identificador}},
                        .UsuarioModificacion = "SERVICIO_OBTENER_CUENTA_SAL",
                        .TipoCuenta = Enumeradores.TipoCuenta.Saldo}

                    End If

                    TiempoParcial = Now
                    ObtenerCuentas(Respuesta.CuentaMovimiento,
                                   Respuesta.CuentaSaldo,
                                   Peticion.EsDocumentoDeValor,
                                   Peticion.IdentificadorAjeno,
                                   Peticion.ObtenerVersionSimplificada,
                                   Respuesta.ValidacionesError,
                                   Nothing,
                                   Peticion.CrearConfiguiracionNivelSaldo,
                                   Peticion.PermitirCualquierTotalizadorSaldoServicio,
                                   log)
                    log.AppendLine("ObtenerCuentas - Tiempo total: " & Now.Subtract(TiempoParcial).ToString() & vbNewLine & "; ")

                End If

            Catch ex As Excepcion.NegocioExcepcion

                ' Respuesta defecto para Excepciones de Negocio
                Respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                Respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                ' Validar si es un error de Infraestructura o de Aplicacion
                If ex.Message.ToUpper() = "TIMEOUT" Then

                    ' Respuesta defecto para Excepciones de Infraestructura
                    Respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                    Respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                Else

                    ' Respuesta defecto para Excepciones de Aplicaciones
                    Respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                End If

                Respuesta.ValidacionesError.Add(New Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL100", .descripcion = ex.Message})

            End Try

            If Respuesta.CuentaMovimiento IsNot Nothing AndAlso String.IsNullOrEmpty(Respuesta.CuentaMovimiento.Identificador) Then
                Respuesta.CuentaMovimiento = Nothing
            End If
            If Respuesta.CuentaSaldo IsNot Nothing AndAlso String.IsNullOrEmpty(Respuesta.CuentaSaldo.Identificador) Then
                Respuesta.CuentaSaldo = Nothing
            End If

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Añadir el log en la respuesta del servicio
            Respuesta.TiempoDeEjecucion = log.ToString()

            Return Respuesta

        End Function

#End Region

#Region "[Cache]"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="cuenta"></param>
        ''' <remarks></remarks>
        Private Shared Sub GrabarCuentaEnCache(cuenta As Clases.Cuenta)

            If cuenta IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Identificador) Then
                Dim cacheKey As String = cuenta.Identificador
                Dim cache As ObjectCache = MemoryCache.Default
                If (cache.Contains(cacheKey)) Then
                    cache.Remove(cacheKey)
                End If

                'O cache irá expirar em 3 minutos
                Dim policy = New CacheItemPolicy() With {.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3)}
                cache.Add(cacheKey, cuenta, policy)

                Dim identificadorCliente As String = String.Empty
                Dim identificadorSubCliente As String = String.Empty
                Dim identificadorPuntoServicio As String = String.Empty
                Dim identificadorCanal As String = String.Empty
                Dim identificadorSubCanal As String = String.Empty
                Dim identificadorDelegacion As String = String.Empty
                Dim identificadorPlanta As String = String.Empty
                Dim identificadorSector As String = String.Empty

                If cuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Cliente.Identificador) Then
                    identificadorCliente = cuenta.Cliente.Identificador
                End If

                If cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Identificador) Then
                    identificadorSubCliente = cuenta.SubCliente.Identificador
                End If

                If cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Identificador) Then
                    identificadorPuntoServicio = cuenta.PuntoServicio.Identificador
                End If

                If cuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Canal.Identificador) Then
                    identificadorCanal = cuenta.Canal.Identificador
                End If

                If cuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCanal.Identificador) Then
                    identificadorSubCanal = cuenta.SubCanal.Identificador
                End If

                If cuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Identificador) Then
                    identificadorSector = cuenta.Sector.Identificador

                    If cuenta.Sector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Delegacion.Identificador) Then
                        identificadorDelegacion = cuenta.Sector.Delegacion.Identificador
                    End If

                    If cuenta.Sector.Planta IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Planta.Identificador) Then
                        identificadorPlanta = cuenta.Sector.Planta.Identificador
                    End If
                End If

                cacheKey = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", identificadorCliente,
                                                                                        identificadorSubCliente,
                                                                                        identificadorPuntoServicio,
                                                                                        identificadorCanal,
                                                                                        identificadorSubCanal,
                                                                                        identificadorDelegacion,
                                                                                        identificadorPlanta,
                                                                                        identificadorSector)
                
                If Not String.IsNullOrWhiteSpace(cacheKey) Then
                    cache = MemoryCache.Default

                    If (cache.Contains(cacheKey)) Then
                        cache.Remove(cacheKey)
                    End If

                    'O cache irá expirar em 3 minutos
                    policy = New CacheItemPolicy() With {.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3)}
                    cache.Add(cacheKey, cuenta, policy)
                End If

                Dim codigoCliente As String = String.Empty
                Dim codigoSubCliente As String = String.Empty
                Dim codigoPuntoServicio As String = String.Empty
                Dim codigoCanal As String = String.Empty
                Dim codigoSubCanal As String = String.Empty
                Dim codigoDelegacion As String = String.Empty
                Dim codigoPlanta As String = String.Empty
                Dim codigoSector As String = String.Empty

                If cuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Cliente.Codigo) Then
                    codigoCliente = cuenta.Cliente.Codigo
                End If

                If cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Codigo) Then
                    codigoSubCliente = cuenta.SubCliente.Codigo
                End If

                If cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Codigo) Then
                    codigoPuntoServicio = cuenta.PuntoServicio.Codigo
                End If

                If cuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Canal.Codigo) Then
                    codigoCanal = cuenta.Canal.Codigo
                End If

                If cuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCanal.Codigo) Then
                    codigoSubCanal = cuenta.SubCanal.Codigo
                End If

                If cuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Codigo) Then
                    codigoSector = cuenta.Sector.Codigo

                    If cuenta.Sector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Delegacion.Codigo) Then
                        codigoDelegacion = cuenta.Sector.Delegacion.Codigo
                    End If

                    If cuenta.Sector.Planta IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Planta.Codigo) Then
                        codigoPlanta = cuenta.Sector.Planta.Codigo
                    End If
                End If

                cacheKey = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", codigoCliente,
                                                                    codigoSubCliente,
                                                                    codigoPuntoServicio,
                                                                    codigoCanal,
                                                                    codigoSubCanal,
                                                                    codigoDelegacion,
                                                                    codigoPlanta,
                                                                    codigoSector)

                If Not String.IsNullOrWhiteSpace(cacheKey) Then
                    cache = MemoryCache.Default

                    If (cache.Contains(cacheKey)) Then
                        cache.Remove(cacheKey)
                    End If

                    'O cache irá expirar em 3 minutos
                    policy = New CacheItemPolicy() With {.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(3)}
                    cache.Add(cacheKey, cuenta, policy)
                End If

            End If

        End Sub

        ''' <summary>
        ''' Realiza una búsqueda de una identificadorCuenta en cache.
        ''' </summary>
        ''' <param name="identificadorCuenta">identificadorCuenta</param>
        ''' <returns>Objecto de la funcionalidad</returns>
        Private Shared Function BuscarCuentaEnCache(identificadorCuenta As String, Optional tipoCuenta As Enumeradores.TipoCuenta? = Nothing) As Clases.Cuenta
            Dim cache As ObjectCache = MemoryCache.Default
            Dim objCuenta As Clases.Cuenta = DirectCast(cache.Get(identificadorCuenta), Clases.Cuenta)
            If objCuenta IsNot Nothing AndAlso tipoCuenta IsNot Nothing Then
                If objCuenta.TipoCuenta = tipoCuenta Then
                    Return objCuenta

                ElseIf objCuenta.TipoCuenta = Enumeradores.TipoCuenta.Ambos Then
                    Return objCuenta
                Else
                    Return Nothing
                End If
            End If

            Return objCuenta
        End Function

        Private Shared Function BuscarCuentaEnCache(cuenta As Clases.Cuenta, Optional tipoCuenta As Enumeradores.TipoCuenta? = Nothing) As Clases.Cuenta
            Dim objCuenta As Clases.Cuenta = Nothing

            If cuenta IsNot Nothing Then
                Dim identificadorCliente As String = String.Empty
                Dim identificadorSubCliente As String = String.Empty
                Dim identificadorPuntoServicio As String = String.Empty
                Dim identificadorCanal As String = String.Empty
                Dim identificadorSubCanal As String = String.Empty
                Dim identificadorDelegacion As String = String.Empty
                Dim identificadorPlanta As String = String.Empty
                Dim identificadorSector As String = String.Empty

                If cuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Cliente.Identificador) Then
                    identificadorCliente = cuenta.Cliente.Identificador
                End If

                If cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Identificador) Then
                    identificadorSubCliente = cuenta.SubCliente.Identificador
                End If

                If cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Identificador) Then
                    identificadorPuntoServicio = cuenta.PuntoServicio.Identificador
                End If

                If cuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Canal.Identificador) Then
                    identificadorCanal = cuenta.Canal.Identificador
                End If

                If cuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCanal.Identificador) Then
                    identificadorSubCanal = cuenta.SubCanal.Identificador
                End If

                If cuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Identificador) Then
                    identificadorSector = cuenta.Sector.Identificador

                    If cuenta.Sector.Delegacion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Delegacion.Identificador) Then
                        identificadorDelegacion = cuenta.Sector.Delegacion.Identificador
                    End If

                    If cuenta.Sector.Planta IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.Sector.Planta.Identificador) Then
                        identificadorPlanta = cuenta.Sector.Planta.Identificador
                    End If
                End If

                Dim cacheKey As String = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", identificadorCliente,
                                                                                        identificadorSubCliente,
                                                                                        identificadorPuntoServicio,
                                                                                        identificadorCanal,
                                                                                        identificadorSubCanal,
                                                                                        identificadorDelegacion,
                                                                                        identificadorPlanta,
                                                                                        identificadorSector)

                If String.IsNullOrWhiteSpace(cacheKey) Then
                    cacheKey = cuenta.Identificador
                End If

                If Not String.IsNullOrWhiteSpace(cacheKey) Then
                    Dim cache As ObjectCache = MemoryCache.Default
                    objCuenta = DirectCast(cache.Get(cacheKey), Clases.Cuenta)
                    If objCuenta IsNot Nothing AndAlso tipoCuenta IsNot Nothing Then
                        If objCuenta.TipoCuenta = tipoCuenta Then
                            Return objCuenta

                        ElseIf objCuenta.TipoCuenta = Enumeradores.TipoCuenta.Ambos Then
                            Return objCuenta
                        Else
                            Return Nothing
                        End If
                    End If
                End If
            End If

            Return objCuenta
        End Function

        Private Shared Function BuscarCuentaEnCachePorIdentificadores(identificadorCliente As String,
                                      identificadorSubCliente As String,
                                      identificadorPuntoServicio As String,
                                      identificadorCanal As String,
                                      identificadorSubCanal As String,
                                      identificadorDelegacion As String,
                                      identificadorPlanta As String,
                                      identificadorSector As String,
                                      Optional tipoCuenta As Enumeradores.TipoCuenta? = Nothing) As Clases.Cuenta

            Dim objCuenta As Clases.Cuenta = Nothing
            Dim cacheKey As String = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", identificadorCliente,
                                                                                    identificadorSubCliente,
                                                                                    identificadorPuntoServicio,
                                                                                    identificadorCanal,
                                                                                    identificadorSubCanal,
                                                                                    identificadorDelegacion,
                                                                                    identificadorPlanta,
                                                                                    identificadorSector)

            If Not String.IsNullOrWhiteSpace(cacheKey) Then
                Dim cache As ObjectCache = MemoryCache.Default
                objCuenta = DirectCast(cache.Get(cacheKey), Clases.Cuenta)
                If objCuenta IsNot Nothing AndAlso tipoCuenta IsNot Nothing Then
                    If objCuenta.TipoCuenta = tipoCuenta Then
                        Return objCuenta

                    ElseIf objCuenta.TipoCuenta = Enumeradores.TipoCuenta.Ambos Then
                        Return objCuenta
                    Else
                        Return Nothing
                    End If
                End If
            End If

            Return objCuenta
        End Function

        Private Shared Function BuscarCuentaEnCachePorCodigos(codigoCliente As String,
                                      codigoSubCliente As String,
                                      codigoPuntoServicio As String,
                                      codigoCanal As String,
                                      codigoSubCanal As String,
                                      codigoDelegacion As String,
                                      codigoPlanta As String,
                                      codigoSector As String,
                                      tipoCuenta As Enumeradores.TipoCuenta?) As Clases.Cuenta

            Dim objCuenta As Clases.Cuenta = Nothing
            Dim cacheKey As String = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", codigoCliente,
                                                                                    codigoSubCliente,
                                                                                    codigoPuntoServicio,
                                                                                    codigoCanal,
                                                                                    codigoSubCanal,
                                                                                    codigoDelegacion,
                                                                                    codigoPlanta,
                                                                                    codigoSector)

            If Not String.IsNullOrWhiteSpace(cacheKey) Then
                Dim cache As ObjectCache = MemoryCache.Default
                objCuenta = DirectCast(cache.Get(cacheKey), Clases.Cuenta)
                If objCuenta IsNot Nothing AndAlso tipoCuenta IsNot Nothing Then
                    If objCuenta.TipoCuenta = tipoCuenta Then
                        Return objCuenta

                    ElseIf objCuenta.TipoCuenta = Enumeradores.TipoCuenta.Ambos Then
                        Return objCuenta
                    Else
                        Return Nothing
                    End If
                End If
            End If

            Return objCuenta
        End Function
#End Region
    End Class

End Namespace
