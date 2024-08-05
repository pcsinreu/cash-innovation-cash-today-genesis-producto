Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

Public Class AccionRecuperarSaldosPorSector

    ''' <summary>
    ''' Método principal responsável por obter dados de saldos 
    ''' e chamar os metodos que converte para o objeto respuesta
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 16/07/2009 Criado
    ''' </history>
    Public Function Ejecutar(Peticion As RecuperarSaldosPorSector.Peticion) As RecuperarSaldosPorSector.Respuesta

        Dim objRespuesta As New RecuperarSaldosPorSector.Respuesta
        Dim objSaldos As Negocio.Saldos
        Dim objCentrosProceso As Negocio.CentrosProceso
        Dim objBancos As Negocio.Bancos
        Dim objClientes As Negocio.Clientes
        Dim i As Integer = 0

        Try

            If Peticion.Sector <> String.Empty Then

                'Busca os Ids Centro proceso do IdPS sector informardo
                objCentrosProceso = New Negocio.CentrosProceso

                objCentrosProceso.IdPS = Peticion.Sector
                objCentrosProceso.Realizar()


                objSaldos = New Negocio.Saldos

                objSaldos.DiscriminarEspecies = Peticion.DiscriminarEspecies
                objSaldos.IntegrarCentrosProceso = Peticion.IntegrarCentrosProceso
                objSaldos.SoloSaldoDisponible = Peticion.SoloSaldoDisponible

                If Peticion.FechaSaldo = DateTime.MinValue Then
                    objSaldos.Actual = True
                    objSaldos.Fecha = Util.GetDateTime(objCentrosProceso(0).Planta.CodDelegacionGenesis)
                Else
                    objSaldos.Actual = False
                    objSaldos.Fecha = Peticion.FechaSaldo
                End If


                'Preenche a lista de Idps separados por |
                i = 0
                For Each CentroProceso In objCentrosProceso

                    i += 1
                    objSaldos.ListaIdCentroProceso &= CentroProceso.Id
                    objSaldos.ListaIdCentroProceso &= "|"
                    If i = objCentrosProceso.Count Then
                        objSaldos.ListaIdCentroProceso = "|" & objSaldos.ListaIdCentroProceso
                    End If

                Next

                'Preenche a lista de id monedas separados por |
                i = 0
                For Each IdMoneda As String In Peticion.Monedas

                    i += 1
                    objSaldos.ListaIdMoneda &= IdMoneda
                    objSaldos.ListaIdMoneda &= "|"
                    If i = Peticion.Monedas.Count Then
                        objSaldos.ListaIdMoneda = "|" & objSaldos.ListaIdMoneda
                    End If

                Next

                'Preenche a lista de Id Banco
                If Peticion.Canales.Count > 0 Then

                    'Para cada IdPs Banco no objeto Peticion deve-se bucar os id's correspondentes
                    i = 0
                    For Each IdPSCanal In Peticion.Canales

                        i += 1

                        objBancos = New Negocio.Bancos

                        objBancos.IdPS = IdPSCanal
                        objBancos.Realizar()

                        For Each Banco In objBancos
                            'Preenche a lista com | no final
                            objSaldos.ListaIdBanco &= Banco.Id
                            objSaldos.ListaIdBanco &= "|"

                        Next

                        'Se for o ultimo elemento adiciona o | no inicio da lista
                        If i = Peticion.Canales.Count AndAlso objSaldos.ListaIdBanco.Length > 0 Then
                            objSaldos.ListaIdBanco = "|" & objSaldos.ListaIdBanco
                        End If

                    Next

                Else 'Se nao for enviado a lista de bancos a consulta considera todos bancos

                    objSaldos.TodosBancos = True
                    objSaldos.ListaIdBanco = Nothing

                End If

                If Peticion.Clientes.Count > 0 Then

                    'Para cada IdPs Cliente no objeto Peticion deve-se bucar os id's correspondentes
                    i = 0
                    For Each IdPSCliente In Peticion.Clientes

                        i += 1

                        objClientes = New Negocio.Clientes

                        objClientes.IdPS = IdPSCliente
                        objClientes.Realizar()

                        For Each Cliente In objClientes
                            'Preenche a lista com | no final
                            objSaldos.ListaIdCliente &= Cliente.Id
                            objSaldos.ListaIdCliente &= "|"

                        Next

                        'Se for o ultimo elemento adiciona o | no inicio da lista
                        If i = Peticion.Clientes.Count AndAlso objSaldos.ListaIdCliente.Length > 0 Then
                            objSaldos.ListaIdCliente = "|" & objSaldos.ListaIdCliente
                        End If

                    Next

                Else
                    objSaldos.TodosClientes = True
                    objSaldos.ListaIdCliente = Nothing
                End If

                objSaldos.Realizar()

                For Each Saldo As Negocio.Saldo In objSaldos

                    PreencherRespuesta(objRespuesta, Saldo)

                Next

            End If

            'Caso não ocorra exceção, retorna o objrespuesta com codigo 0 e mensagem erro vazio
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Preenche o objeto respuesta com os dados do objeto saldo
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 16/07/2009 Criado
    ''' </history>
    Private Sub PreencherRespuesta(ByRef objRespuesta As RecuperarSaldosPorSector.Respuesta, _
                                   objSaldo As Negocio.Saldo)

        Dim objSaldoRespuesta As New RecuperarSaldosPorSector.Saldo

        With objSaldoRespuesta

            .Disponible = objSaldo.Disponible

            Dim objCanal As New Negocio.Banco
            objCanal.Id = objSaldo.IdBanco
            objCanal.Realizar()
            .Canal.IdPS = objCanal.IdPS
            .Canal.Descripcion = objCanal.Descripcion

            Dim objCliente As New Negocio.Cliente
            objCliente.Id = objSaldo.IdCliente
            objCliente.Realizar()
            .Cliente.IdPS = objCliente.IdPS
            .Cliente.Descripcion = objCliente.Descripcion

            Dim objMoneda As New Negocio.Moneda
            objMoneda.Id = objSaldo.IdMoneda
            objMoneda.Realizar()
            .Moneda.Id = objMoneda.Id
            .Moneda.Descripcion = objMoneda.Descripcion
            .Moneda.Simbolo = objMoneda.Simbolo

            .Importe = objSaldo.Importe
            .Fajos = ConverterFajos(objSaldo.Fajos)

        End With

        ' Verifica se o objeto de resposta não está vazio
        If objRespuesta IsNot Nothing Then

            ' Verifica se a coleção de saldos não está vazia
            If objRespuesta.Saldos Is Nothing Then

                ' Cria uma nova lista
                objRespuesta.Saldos = New RecuperarSaldosPorSector.Saldos

            End If

            objRespuesta.Saldos.Add(objSaldoRespuesta)

        End If

    End Sub

    ''' <summary>
    ''' Converte a lista de fajos dos saldos para o objeto fajos do respuesta
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 16/07/2009 Criado
    ''' </history>
    Private Function ConverterFajos(objFajos As Negocio.Fajos) As RecuperarSaldosPorSector.Fajos

        Dim objFajosRespuesta As New RecuperarSaldosPorSector.Fajos

        If objFajos IsNot Nothing AndAlso objFajos.Count > 0 Then

            Dim objFajoRespuesta As RecuperarSaldosPorSector.Fajo = Nothing
            Dim objEspecie As Negocio.Especie

            For Each objFajo As Negocio.Fajo In objFajos

                ' criar novo campo respuesta
                objFajoRespuesta = New RecuperarSaldosPorSector.Fajo

                objEspecie = New Negocio.Especie
                objEspecie.Id = objFajo.IdEspecie
                objEspecie.Realizar()
                objFajoRespuesta.Especie = objEspecie.Id
                objFajoRespuesta.Descripcion = objEspecie.Descripcion

                objFajoRespuesta.Cantidad = objFajo.Cantidad
                objFajoRespuesta.Importe = objFajo.Importe

                ' adicionar para coleção
                objFajosRespuesta.Add(objFajoRespuesta)

            Next

        End If

        Return objFajosRespuesta

    End Function

End Class
