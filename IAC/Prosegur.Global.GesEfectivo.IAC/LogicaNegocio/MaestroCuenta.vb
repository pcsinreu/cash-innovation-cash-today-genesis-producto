'Imports Prosegur.Framework.Dicionario.Tradutor
'Imports System.Data

'Public Class MaestroCuenta

'    Private Shared Function RetornaValorLista(Chave As String, Lista As Dictionary(Of String, String)) As String

'        If Lista.ContainsKey(Chave) Then
'            Return Lista(Chave)
'        Else
'            Return Nothing
'        End If

'    End Function

'    Private Shared Function RecuperarCuenta(IdentificadorCliente As String, IdentificadorSubCanal As String, IdentificadorSector As String,
'                                    Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        If String.IsNullOrEmpty(IdentificadorSubCanal) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorSubCanal"))
'        End If

'        If String.IsNullOrEmpty(IdentificadorSector) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorSector"))
'        End If

'        Return Nothing ' Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarCuenta(IdentificadorCliente, IdentificadorSubCanal, IdentificadorSector, IdentificadorSubCliente, IdentificadorPuntoServicio)

'    End Function

'    Public Shared Function GenerarCuenta(IdentificadorCliente As String, IdentificadorSubCanal As String, IdentificadorSector As String, DescripcionUsuario As String,
'                                    Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing, Optional TipoCuenta As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Movimiento) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        If String.IsNullOrEmpty(IdentificadorSubCanal) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorSubCanal"))
'        End If

'        If String.IsNullOrEmpty(IdentificadorSector) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorSector"))
'        End If

'        Return Nothing ' Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarCuenta(IdentificadorCliente, IdentificadorSubCanal, IdentificadorSector, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, TipoCuenta)

'    End Function

'    Public Shared Sub ActualizarTipoCuenta(IdentificadorCuenta As String, TipoCuenta As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta, DescripcionUsuario As String)

'        If String.IsNullOrEmpty(IdentificadorCuenta) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCuenta"))
'        End If

'        If String.IsNullOrEmpty(DescripcionUsuario) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "DescripcionUsuario"))
'        End If

'        Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarTipoCuenta(IdentificadorCuenta, TipoCuenta, DescripcionUsuario)

'    End Sub

'    Public Shared Sub DesactivarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento As String, DescripcionUsuario As String)

'        If String.IsNullOrEmpty(IdentificadorConfiguracionNivelMovimiento) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorConfiguracionNivelMovimiento"))
'        End If

'        If String.IsNullOrEmpty(DescripcionUsuario) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "DescripcionUsuario"))
'        End If

'        Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.DesactivarConfiguracionNivelMovimiento(IdentificadorConfiguracionNivelMovimiento, DescripcionUsuario)

'    End Sub

'    Public Shared Function RecuperarCuentaMovimiento(IdentificadorCliente As String, IdentificadorSubCanal As String, IdentificadorSector As String,
'                                    Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        Return RecuperarCuenta(IdentificadorCliente, IdentificadorSubCanal, IdentificadorSector, IdentificadorSubCliente, IdentificadorPuntoServicio)

'    End Function

'    Public Shared Function RecuperarCuentaMovimiento(IdentificadorCuentaMovimiento As String) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        If String.IsNullOrEmpty(IdentificadorCuentaMovimiento) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCuentaMovimiento"))
'        End If

'        Return Nothing 'Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarCuenta(IdentificadorCuentaMovimiento)

'    End Function

'    Public Shared Function RecuperarCuentaSaldo(CuentaMovimiento As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta, CrearConfiguiracionAutomaticamenteSiNoEncuentra As Boolean, Optional DescripcionUsuario As String = Nothing) As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        If CrearConfiguiracionAutomaticamenteSiNoEncuentra AndAlso String.IsNullOrEmpty(DescripcionUsuario) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "DescripcionUsuario"))
'        End If

'        Dim cuenta As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta

'        ' recupera a conta de saldo para conta de movimento (SAPR_TNIVEL_SALDO)
'        cuenta = Nothing 'Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarCuentaSaldoPorCuentaMovimiento(CuentaMovimiento.Identificador)

'        ' se não encontra a conta de saldo para conta de movimento (SAPR_TNIVEL_SALDO)
'        If cuenta Is Nothing Then

'            ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
'            ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'            Dim configuracionNivelMovimientoSaldo As Dictionary(Of String, String) = RecuperarConfiguracaoNivelMovimientoSaldo(CuentaMovimiento.IdentificadorCliente,
'                                                                                                                               CuentaMovimiento.IdentificadorSubCliente,
'                                                                                                                               CuentaMovimiento.IdentificadorPuntoServicio,
'                                                                                                                               CuentaMovimiento.IdentificadorSubCanal)

'            ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
'            ' conta de movimento INCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'            If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

'                ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a 
'                ' conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                configuracionNivelMovimientoSaldo = RecuperarConfiguracaoNivelMovimientoSaldo(CuentaMovimiento.IdentificadorCliente,
'                                                                                              CuentaMovimiento.IdentificadorSubCliente,
'                                                                                              CuentaMovimiento.IdentificadorPuntoServicio)

'                ' se não encontrou a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
'                ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                If configuracionNivelMovimientoSaldo Is Nothing OrElse configuracionNivelMovimientoSaldo.Count = 0 Then

'                    ' verificar se pode criar a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
'                    ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                    If CrearConfiguiracionAutomaticamenteSiNoEncuentra Then

'                        ' a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                        ' será criada utilizando os dados da própria conta de movimento passada ao método, ou seja, o apontamento de nível de saldo padrão
'                        ' será para a própria combinação (cliente, subcliente e ponto de serviço) encontrada na conta de movimento

'                        ' verifica se existe uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) com os 
'                        ' mesmos dados da conta de movimento (SAPR_TCUENTA) informada no método
'                        Dim configuracionNivelSaldo As Dictionary(Of String, String) = RecuperarConfiguracaoNivelSaldo(CuentaMovimiento.IdentificadorCliente,
'                                                                                                                       CuentaMovimiento.IdentificadorSubCliente,
'                                                                                                                       CuentaMovimiento.IdentificadorPuntoServicio)

'                        Dim identificadorNivelSaldo As String

'                        ' se não encontrou a configuração de nível saldo (SAPR_TCONFIG_NIVEL_SALDO)
'                        If (configuracionNivelSaldo Is Nothing OrElse configuracionNivelSaldo.Count = 0) Then

'                            ' criar uma configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) utilizando os mesmos dados 
'                            ' da conta de movimento (SAPR_TCUENTA) informada no método
'                            identificadorNivelSaldo = GenerarConfiguracionNivelSaldo(CuentaMovimiento.IdentificadorCliente,
'                                                                                     DescripcionUsuario,
'                                                                                     CuentaMovimiento.IdentificadorSubCliente,
'                                                                                     CuentaMovimiento.IdentificadorPuntoServicio)

'                        Else

'                            ' seleciona o identificador encontrado
'                            identificadorNivelSaldo = RetornaValorLista("OID_CONFIG_NIVEL_SALDO", configuracionNivelSaldo)

'                        End If

'                        ' gera uma configuração PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                        GenerarConfiguracionNivelMovimiento(CuentaMovimiento.IdentificadorCliente,
'                                                            identificadorNivelSaldo,
'                                                            DescripcionUsuario,
'                                                            CuentaMovimiento.IdentificadorSubCliente,
'                                                            CuentaMovimiento.IdentificadorPuntoServicio)

'                        ' recupera a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
'                        ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                        configuracionNivelMovimientoSaldo = RecuperarConfiguracaoNivelMovimientoSaldo(CuentaMovimiento.IdentificadorCliente,
'                                                                                                      CuentaMovimiento.IdentificadorSubCliente,
'                                                                                                      CuentaMovimiento.IdentificadorPuntoServicio)

'                    Else

'                        ' se não deve criar automaticamente a configuração de nível de saldo (SAPR_TCONFIG_NIVEL_SALDO) para 
'                        ' a conta de movimento PADRÃO - EXCLUINDO subcanal (SAPR_TCONFIG_NIVEL_MOVIMIENTO)
'                        ' retorna uma conta de saldo nula (não encontrada)
'                        Return cuenta

'                    End If

'                End If

'            End If

'            ' utiliza a "configuracionNivelMovimientoSaldo" para localizar ou criar a conta de saldos

'            ' tenta localizar uma conta com os dados da configuração encontrada incluindo o subcanal e o setor
'            cuenta = Nothing
'            ' Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarCuenta(RetornaValorLista("OID_CLIENTE", configuracionNivelMovimientoSaldo),
'            'CuentaMovimiento.IdentificadorSubCanal,
'            'CuentaMovimiento.IdentificadorSector,
'            'RetornaValorLista("OID_SUBCLIENTE", configuracionNivelMovimientoSaldo),
'            'RetornaValorLista("OID_PTO_SERVICIO", configuracionNivelMovimientoSaldo))

'            ' se não encontrou
'            If cuenta Is Nothing Then

'                ' cria conta de saldo
'                cuenta = Nothing
'                'Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarCuenta(RetornaValorLista("OID_CLIENTE", configuracionNivelMovimientoSaldo),
'                '                                      CuentaMovimiento.IdentificadorSubCanal,
'                '                                      CuentaMovimiento.IdentificadorSector,
'                '                                      DescripcionUsuario,
'                '                                      RetornaValorLista("OID_SUBCLIENTE", configuracionNivelMovimientoSaldo),
'                '                                      RetornaValorLista("OID_PTO_SERVICIO", configuracionNivelMovimientoSaldo),
'                '                                      Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Saldo)
'            Else

'                ' se encontrou, valida se a conta é do tipo "Movimento", se for, atualiza para
'                ' "Ambos" - significando que faz movimento e leva saldo
'                Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarTipoCuenta(cuenta.Identificador, Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Ambos, DescripcionUsuario)

'                cuenta.TipoCuenta = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Ambos

'            End If

'            ' cria o nível de saldo para a conta de saldo / conta de movimento (SAPR_TNIVEL_SALDO)
'            ' dessa maneira, a próxima consulta será mais rápida (uma vez que já terá um registro com a configuração)
'            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarNivelSaldo(CuentaMovimiento.Identificador, cuenta.Identificador, RetornaValorLista("OID_CONFIG_NIVEL_MOVIMIENTO", configuracionNivelMovimientoSaldo), DescripcionUsuario)

'        End If

'        Return cuenta

'    End Function

'    Public Shared Function GenerarConfiguracionNivelSaldo(IdentificadorCliente As String, DescripcionUsuario As String,
'                                                          Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing, Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        If String.IsNullOrEmpty(DescripcionUsuario) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "DescripcionUsuario"))
'        End If

'        Dim DatosConfiguracionNivelSaldo As Dictionary(Of String, String) = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio)

'        If DatosConfiguracionNivelSaldo Is Nothing OrElse DatosConfiguracionNivelSaldo.Count = 0 Then
'            Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarConfiguracionNivelSaldo(IdentificadorCliente, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, Transacion)
'        Else
'            Return DatosConfiguracionNivelSaldo("OID_CONFIG_NIVEL_SALDO")
'        End If

'    End Function

'    Public Shared Function GenerarConfiguracionNivelMovimiento(IdentificadorCliente As String, IdentificadorConfiguracionNivelSaldo As String, DescripcionUsuario As String,
'                                                               Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing, Optional IdentificadorSubCanal As String = Nothing, Optional ByRef Transacion As DbHelper.Transacao = Nothing) As String

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        If String.IsNullOrEmpty(IdentificadorConfiguracionNivelSaldo) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorConfiguracionNivelSaldo"))
'        End If

'        If String.IsNullOrEmpty(DescripcionUsuario) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "DescripcionUsuario"))
'        End If

'        ' gera a nova configuração de nível de movimento para as informações informadas
'        Dim identificadorConfiguracionNivelMovimiento As String = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.GenerarConfiguracionNivelMovimiento(IdentificadorCliente, IdentificadorConfiguracionNivelSaldo, DescripcionUsuario, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, Transacion)

'        ' verifica se existe uma configuração de nível de movimento anterior para os mesmos dados
'        Dim configuracionNivelMovimientoSaldoAnterior As Dictionary(Of String, String) = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelMovimientoSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal, True)

'        ' verifica se foi retornada alguma configuração anterior a criada
'        If configuracionNivelMovimientoSaldoAnterior IsNot Nothing AndAlso configuracionNivelMovimientoSaldoAnterior.Count > 0 Then

'            ' recupera os dados da configuração de saldo atual
'            Dim configuracionNivelSaldo As Dictionary(Of String, String) = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelSaldoPorIdentificador(IdentificadorConfiguracionNivelSaldo)

'            Dim identificadorConfiguracionNivelMovimientoSaldoAnterior As String = configuracionNivelMovimientoSaldoAnterior("OID_CONFIG_NIVEL_MOVIMIENTO")

'            ' seleciona todas as combinações de conta de movimento e de saldo
'            ' que utilizam como base a configuração anterior
'            Dim dtNivelSaldo As DataTable = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarNivelSaldoPorConfiguracionMovimiento(identificadorConfiguracionNivelMovimientoSaldoAnterior)

'            If dtNivelSaldo IsNot Nothing AndAlso dtNivelSaldo.Rows.Count > 0 Then

'                For Each drNivelSaldo As DataRow In dtNivelSaldo.Rows

'                    ' para cada nível de saldo encontrado, é necessário recuperar a conta de movimento
'                    Dim cuentaMovimientoAtual As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta = Nothing 'Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarCuenta(drNivelSaldo("OID_CUENTA_MOVIMIENTO"))

'                    ' se não encontra a conta de movimento, deve retornar um erro pela inconsistência no banco
'                    If cuentaMovimientoAtual Is Nothing Then
'                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_cuentanoencontra"), drNivelSaldo("OID_CUENTA_MOVIMIENTO")))
'                    End If

'                    ' verifica se existe uma conta de saldo para a combinação informada
'                    Dim cuentaSaldoNova As Prosegur.Genesis.ContractoServicio.GenesisSaldos.Cuenta = RecuperarCuenta(RetornaValorLista("OID_CLIENTE", configuracionNivelSaldo), cuentaMovimientoAtual.IdentificadorSubCanal, cuentaMovimientoAtual.IdentificadorSector, RetornaValorLista("OID_SUBCLIENTE", configuracionNivelSaldo), RetornaValorLista("OID_PTO_SERVICIO", configuracionNivelSaldo))

'                    ' caso não exista
'                    If cuentaSaldoNova Is Nothing Then

'                        ' cria-se uma nova conta de saldo para os dados
'                        cuentaSaldoNova = GenerarCuenta(RetornaValorLista("OID_CLIENTE", configuracionNivelSaldo),
'                                                        cuentaMovimientoAtual.IdentificadorSubCanal,
'                                                        cuentaMovimientoAtual.IdentificadorSector,
'                                                        DescripcionUsuario,
'                                                        RetornaValorLista("OID_SUBCLIENTE", configuracionNivelSaldo),
'                                                        RetornaValorLista("OID_PTO_SERVICIO", configuracionNivelSaldo),
'                                                        Prosegur.Genesis.ContractoServicio.GenesisSaldos.Enumeraciones.TipoCuenta.Saldo)

'                    End If

'                    ' actualiza a configuração de nível de saldo
'                    Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarNivelSaldo(drNivelSaldo("OID_NIVEL_SALDO"), cuentaMovimientoAtual.Identificador, cuentaSaldoNova.Identificador, DescripcionUsuario, Transacion)

'                Next

'            End If

'            ' desabilita o registro anterior
'            Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.ActualizarConfiguracionNivelMovimiento(identificadorConfiguracionNivelMovimientoSaldoAnterior, False, DescripcionUsuario, Transacion)

'        End If

'        Return identificadorConfiguracionNivelMovimiento

'    End Function

'    Public Shared Function RecuperarConfiguracaoNivelMovimientoSaldo(IdentificadorCliente As String,
'                                                                     Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing, Optional IdentificadorSubCanal As String = Nothing) As Dictionary(Of String, String)

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelMovimientoSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio, IdentificadorSubCanal)

'    End Function

'    Public Shared Function RecuperarConfiguracaoNivelSaldo(IdentificadorCliente As String, Optional IdentificadorSubCliente As String = Nothing, Optional IdentificadorPuntoServicio As String = Nothing) As Dictionary(Of String, String)

'        If String.IsNullOrEmpty(IdentificadorCliente) Then
'            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("000_msg_campoobrigatorio"), "IdentificadorCliente"))
'        End If

'        Return Prosegur.Genesis.AccesoDatos.GenesisSaldos.Cuenta.RecuperarConfiguracaoNivelSaldo(IdentificadorCliente, IdentificadorSubCliente, IdentificadorPuntoServicio)

'    End Function

'End Class
