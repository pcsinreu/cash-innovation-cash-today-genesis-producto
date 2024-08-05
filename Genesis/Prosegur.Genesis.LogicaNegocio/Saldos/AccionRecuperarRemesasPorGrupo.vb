Imports System.Data
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Global.Saldos

Public Class AccionRecuperarRemesasPorGrupo

    Public Function Ejecutar(peticion As RecuperarRemesasPorGrupo.Peticion) As RecuperarRemesasPorGrupo.Respuesta

        Dim respuesta As New RecuperarRemesasPorGrupo.Respuesta

        Try

            If ValidarPeticion(peticion, respuesta) Then

                Dim documentos As New Negocio.Documentos()

                Dim resultado As DataTable = documentos.RecuperaRemesasPorGrupo(peticion.Filtro.CodigosGrupo,
                                                                                peticion.Filtro.FechaHoraSaldoDesde,
                                                                                peticion.Filtro.FechaHoraSaldoHasta,
                                                                                peticion.Filtro.CodigoPlanta,
                                                                                peticion.Filtro.CodigoSector,
                                                                                peticion.Filtro.CodidoCliente,
                                                                                peticion.Filtro.CodigoCanal,
                                                                                peticion.Filtro.CodigoMoneda,
                                                                                peticion.Filtro.SoloSaldoDisponible)

                PopularRespuesta(peticion, respuesta, resultado)

                'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                respuesta.MensajeError = String.Empty

            End If

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()

        End Try

        Return respuesta

    End Function

    Private Function ValidarPeticion(peticion As RecuperarRemesasPorGrupo.Peticion, ByRef Respuesta As RecuperarRemesasPorGrupo.Respuesta) As Boolean

        ' objeto que recebe as mensagens
        Dim Mensagens As New System.Text.StringBuilder

        ' verifica se o usuário foi informado
        If peticion.Usuario Is Nothing OrElse (peticion.Usuario IsNot Nothing AndAlso (String.IsNullOrEmpty(peticion.Usuario.Login) OrElse String.IsNullOrEmpty(peticion.Usuario.Clave))) Then
            ' adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("12_msg_dados_usuario_naoinformado"))
        End If

        ' verifica se os dados do usuário são válidos
        If peticion.Usuario IsNot Nothing AndAlso (peticion.Usuario IsNot Nothing AndAlso (Not String.IsNullOrEmpty(peticion.Usuario.Login) AndAlso Not String.IsNullOrEmpty(peticion.Usuario.Clave))) Then

            Dim objUsuario As New Negocio.Usuario
            objUsuario.Nombre = peticion.Usuario.Login
            objUsuario.Clave = peticion.Usuario.Clave
            objUsuario.Ingresar()

            If objUsuario.Id <> -1 Then
                If objUsuario.Bloqueado Then
                    ' adiciona texto de mensagem
                    Mensagens.AppendLine(Traduzir("12_msg_login_bloqueado"))
                End If
            Else
                ' adiciona texto de mensagem
                Mensagens.AppendLine(Traduzir("12_msg_login_invalido"))
            End If

        End If

        ' verifica se o filtro foi informado
        If peticion.Filtro Is Nothing Then
            ' adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("12_msg_dados_filtro_naoinformado"))
        End If

        ' verifica se o codigos grupo foi informado
        If peticion.Filtro IsNot Nothing AndAlso peticion.Filtro.CodigosGrupo IsNot Nothing AndAlso peticion.Filtro.CodigosGrupo.Count = 0 Then
            ' adiciona texto de mensagem
            Mensagens.AppendLine(Traduzir("12_msg_dados_filtro_codigosgrupo_naoinformado"))
        End If

        ' verifica se o codigos grupo são válidos
        If peticion.Filtro IsNot Nothing AndAlso peticion.Filtro.CodigosGrupo IsNot Nothing AndAlso peticion.Filtro.CodigosGrupo.Count > 0 Then
            For Each codigoGrupo In peticion.Filtro.CodigosGrupo

                Dim grupo As New Negocio.Grupo
                grupo.Codigo = codigoGrupo
                grupo.Realizar()

                ' valida se o grupo existe no banco
                If String.IsNullOrEmpty(grupo.Id) OrElse grupo.Id <= 0 Then
                    ' adiciona texto de mensagem
                    Mensagens.AppendLine(String.Format(Traduzir("12_msg_dados_filtro_codigosgrupo_novalido"), codigoGrupo))
                Else
                    ' se o grupo existe no banco, valida os formulários associados
                    For Each formulario As KeyValuePair(Of String, Boolean) In grupo.Formularios
                        ' verifica se o formulário não é válido
                        If Not formulario.Value Then
                            ' adiciona texto de mensagem
                            Mensagens.AppendLine(String.Format(Traduzir("12_msg_codigo_formulario_novalido"), formulario.Key, codigoGrupo))
                        End If
                    Next
                End If

            Next
        End If

        ' verifica se há mensagem de informação inválida
        If (Mensagens.Length > 0) Then
            ' preenche o objeto de resposta
            Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            Respuesta.MensajeError = Mensagens.ToString
            ' retorna falso
            Return False
        Else
            ' retorna verdadeiro
            Return True
        End If

    End Function

    Public Sub PopularRespuesta(peticion As RecuperarRemesasPorGrupo.Peticion, ByRef respuesta As RecuperarRemesasPorGrupo.Respuesta, dados As DataTable)

        If dados IsNot Nothing AndAlso dados.Rows IsNot Nothing AndAlso dados.Rows.Count > 0 Then

            For Each item As DataRow In dados.Rows

                If respuesta.Grupos Is Nothing Then
                    respuesta.Grupos = New RecuperarRemesasPorGrupo.Grupos()
                End If

                Dim codigoGrupo As String = Util.AtribuirValorObj(item("GRUPO_CODIGO"), GetType(String))
                Dim grupo As RecuperarRemesasPorGrupo.Grupo

                If respuesta.Grupos.Exists(Function(g) g.Codigo = codigoGrupo) Then
                    grupo = respuesta.Grupos.Find(Function(g) g.Codigo = codigoGrupo)
                Else
                    grupo = New RecuperarRemesasPorGrupo.Grupo()
                    grupo.Codigo = Util.AtribuirValorObj(item("GRUPO_CODIGO"), GetType(String))
                    grupo.Descripcion = Util.AtribuirValorObj(item("GRUPO_DESCRIPCION"), GetType(String))
                    grupo.Vigente = Util.AtribuirValorObj(item("GRUPO_VIGENTE"), GetType(Boolean))
                    respuesta.Grupos.Add(grupo)
                End If

                If grupo.Transacciones Is Nothing Then
                    grupo.Transacciones = New RecuperarRemesasPorGrupo.Transacciones()
                End If

                Dim oidTransaccion As String = Util.AtribuirValorObj(item("TRANSACION_OIDTRANSACION"), GetType(String))
                Dim transaccion As RecuperarRemesasPorGrupo.Transaccion

                If grupo.Transacciones.Exists(Function(t) t.OidTransaccion = oidTransaccion) Then
                    transaccion = grupo.Transacciones.Find(Function(t) t.OidTransaccion = oidTransaccion)
                Else
                    transaccion = New RecuperarRemesasPorGrupo.Transaccion()
                    transaccion.OidTransaccion = Util.AtribuirValorObj(item("TRANSACION_OIDTRANSACION"), GetType(String))
                    transaccion.Fecha = Util.AtribuirValorObj(item("TRANSACION_FECHA"), GetType(DateTime))
                    transaccion.NumExterno = Util.AtribuirValorObj(item("TRANSACION_NUMEXTERNO"), GetType(String))
                    transaccion.Planta = New RecuperarRemesasPorGrupo.Planta With {.Codigo = Util.AtribuirValorObj(item("PLANTA_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("PLANTA_DESCRIPCION"), GetType(String))}
                    transaccion.SectorOrigen = New RecuperarRemesasPorGrupo.SectorOrigen With {.Codigo = Util.AtribuirValorObj(item("SECTORORIGEN_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("SECTORORIGEN_DESCRIPCION"), GetType(String))}
                    transaccion.SectorDestino = New RecuperarRemesasPorGrupo.SectorDestino With {.Codigo = Util.AtribuirValorObj(item("SECTORDESTINO_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("SECTORDESTINO_DESCRIPCION"), GetType(String))}
                    transaccion.Cliente = New RecuperarRemesasPorGrupo.Cliente With {.Codigo = Util.AtribuirValorObj(item("CLIENTE_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("CLIENTE_DESCRIPCION"), GetType(String))}
                    transaccion.CanalOrigen = New RecuperarRemesasPorGrupo.CanalOrigen With {.Codigo = Util.AtribuirValorObj(item("CANALORIGEN_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("CANALORIGEN_DESCRIPCION"), GetType(String))}
                    transaccion.CanalDestino = New RecuperarRemesasPorGrupo.CanalDestino With {.Codigo = Util.AtribuirValorObj(item("CANALDESTINO_CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(item("CANALDESTINO_DESCRIPCION"), GetType(String))}
                    transaccion.Documento = New RecuperarRemesasPorGrupo.Documento With {.NombreDocumento = Util.AtribuirValorObj(item("DOCUMENTO_NOMBREDOCUMENTO"), GetType(String))}
                    If Not Util.AtribuirValorObj(item("SALDO_DISPONIBLE"), GetType(Int16)) = -1 Then
                        transaccion.Disponible = Convert.ToBoolean(Util.AtribuirValorObj(item("SALDO_DISPONIBLE"), GetType(Int16)))
                    End If
                    grupo.Transacciones.Add(transaccion)
                End If

                If transaccion.Monedas Is Nothing Then
                    transaccion.Monedas = New RecuperarRemesasPorGrupo.Monedas()
                End If

                Dim codigoMoneda As String = Util.AtribuirValorObj(item("MONEDA_CODIGO"), GetType(String))
                Dim moneda As RecuperarRemesasPorGrupo.Moneda

                If transaccion.Monedas.Exists(Function(m) m.Codigo = codigoMoneda) Then
                    moneda = transaccion.Monedas.Find(Function(m) m.Codigo = codigoMoneda)
                Else
                    moneda = New RecuperarRemesasPorGrupo.Moneda()
                    moneda.Codigo = Util.AtribuirValorObj(item("MONEDA_CODIGO"), GetType(String))
                    moneda.Descripcion = Util.AtribuirValorObj(item("MONEDA_DESCRIPCION"), GetType(String))
                    transaccion.Monedas.Add(moneda)
                End If

                If moneda.Especies Is Nothing Then
                    moneda.Especies = New RecuperarRemesasPorGrupo.Especies()
                End If

                Dim especie As RecuperarRemesasPorGrupo.Especie
                especie = New RecuperarRemesasPorGrupo.Especie()
                especie.Codigo = Util.AtribuirValorObj(item("ESPECIE_CODIGO"), GetType(String))
                especie.Descripcion = Util.AtribuirValorObj(item("ESPECIE_DESCRIPCION"), GetType(String))
                especie.Cantidad = Util.AtribuirValorObj(item("ESPECIE_CANTIDAD"), GetType(Int32))
                especie.Importe = Util.AtribuirValorObj(item("ESPECIE_IMPORTE"), GetType(Decimal))
                moneda.Especies.Add(especie)

            Next

            TotalizaValores(peticion, respuesta)

        End If

    End Sub

    Private Sub TotalizaValores(peticion As RecuperarRemesasPorGrupo.Peticion, ByRef respuesta As RecuperarRemesasPorGrupo.Respuesta)

        If respuesta IsNot Nothing AndAlso respuesta.Grupos IsNot Nothing AndAlso respuesta.Grupos.Count > 0 Then

            For Each grupo As RecuperarRemesasPorGrupo.Grupo In respuesta.Grupos
                If grupo.Transacciones IsNot Nothing AndAlso grupo.Transacciones.Count > 0 Then
                    For Each transaccion As RecuperarRemesasPorGrupo.Transaccion In grupo.Transacciones
                        If transaccion.Monedas IsNot Nothing AndAlso transaccion.Monedas.Count > 0 Then
                            For Each moneda As RecuperarRemesasPorGrupo.Moneda In transaccion.Monedas
                                If moneda.Especies IsNot Nothing AndAlso moneda.Especies.Count > 0 Then
                                    For Each especie As RecuperarRemesasPorGrupo.Especie In moneda.Especies
                                        moneda.Importe += especie.Importe
                                    Next
                                End If
                                If Not peticion.Filtro.SaldoDesglosado Then
                                    moneda.Especies = Nothing
                                End If
                            Next
                        End If
                    Next
                End If
            Next

        End If

    End Sub

End Class
