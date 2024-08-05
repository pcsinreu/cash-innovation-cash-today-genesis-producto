Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration

Namespace Integracion

    Public Class AccionIngresoRemesas

        ''' <summary>
        ''' Realiza o Ingresso de Remessas.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [maoliveira]  05/02/2014  criado
        ''' </history>
        Public Shared Function Ejecutar(Peticion As IngresoRemesas.Peticion) As IngresoRemesas.Respuesta

            Dim respuesta As New IngresoRemesas.Respuesta

            Try

                ' Realiza o Ingresso de Remessa
                respuesta = ConverterIngresoRemesaNuevoRespuesta(AccionIngresoRemesasNuevo.Ejecutar(ConverterIngresoRemesaNuevoPeticion(Peticion)))

                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                respuesta.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                respuesta.MensajeError = ex.Descricao

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()

            End Try

            Return respuesta

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoRespuesta(respuesta As IngresoRemesasNuevo.Respuesta) As IngresoRemesas.Respuesta

            ' Variável que receberá a resposta
            Dim objRespuesta As New IngresoRemesas.Respuesta

            ' Se existe resposta
            If respuesta IsNot Nothing Then

                ' Recebe os dados da resposta
                objRespuesta.CodigoError = respuesta.CodigoError
                objRespuesta.MensajeError = respuesta.MensajeError
                objRespuesta.MensajeErrorDescriptiva = respuesta.MensajeErrorDescriptiva

                ' Verifica se existem remessas com erros
                If respuesta.RemesasError IsNot Nothing AndAlso respuesta.RemesasError.Count > 0 Then

                    ' Cria a lista de remessas erros
                    objRespuesta.RemesasError = New IngresoRemesas.RemesasError

                    ' Para cada remessa com erro existente
                    For Each remesaError As IngresoRemesasNuevo.RemesaError In respuesta.RemesasError

                        ' Adiciona a remessa com erro
                        objRespuesta.RemesasError.Add(New IngresoRemesas.RemesaError With
                                                      {
                                                        .IdRemesaError = remesaError.IdRemesaError,
                                                        .DescRemesaError = remesaError.DescRemesaError
                                                      })
                    Next

                End If

                ' Verifica se existem remessas ingressadas
                If respuesta.RemesasOK IsNot Nothing AndAlso respuesta.RemesasOK.Count > 0 Then

                    ' Cria a lista de remessas erros
                    objRespuesta.RemesasOK = New IngresoRemesas.RemesasOk

                    ' Para cada remessa com erro existente
                    For Each RemesaOk As IngresoRemesasNuevo.RemesaOk In respuesta.RemesasOK

                        ' Adiciona a remessa com erro
                        objRespuesta.RemesasOK.Add(New IngresoRemesas.RemesaOk With
                                                      {
                                                        .IdentificadorRemesaOriginal = RemesaOk.IdentificadorRemesaOriginal,
                                                        .IdentificadorRemesaGenerada = RemesaOk.IdentificadorDocumentoGenerado,
                                                        .CodigoDelegacion = RemesaOk.CodigoDelegacion,
                                                        .Bultos = New IngresoRemesas.BultosOk(),
                                                        .Observaciones = RemesaOk.Observaciones
                                                      })
                        ' Adiciona os malotes
                        objRespuesta.RemesasOK.Last.Bultos.AddRange(RemesaOk.Bultos.Select(Function(b) New IngresoRemesas.BultoOk With {.IdentificadorBultoOriginal = b.IdentificadorBultoOriginal}).ToList())
                    Next

                End If

            End If

            ' Retorna a resposta
            Return objRespuesta

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoPeticion(Peticion As IngresoRemesas.Peticion) As IngresoRemesasNuevo.Peticion

            ' Define a petição de retorno
            Dim objPeticion As IngresoRemesasNuevo.Peticion = Nothing

            ' Se existe petição
            If Peticion IsNot Nothing Then

                ' Cria uma nova instancia da petição
                objPeticion = New IngresoRemesasNuevo.Peticion With {
                                                                        .CodigoUsuario = Comon.Constantes.CODIGO_APLICACION_LEGADO
                                                                    }

                ' Converte as remessas
                objPeticion.Remesas = ConverterIngresoRemesaNuevoRemesas(Peticion.Remesas)

            End If

            ' Retorna a petição
            Return objPeticion

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoRemesas(Remesas As IngresoRemesas.Remesas) As IngresoRemesasNuevo.Remesas

            ' Define a variável que recebe as remessas
            Dim objRemesas As IngresoRemesasNuevo.Remesas = Nothing

            ' Se existem remessas
            If Remesas IsNot Nothing AndAlso Remesas.Count > 0 Then

                ' Cria a lista de remessas
                objRemesas = New IngresoRemesasNuevo.Remesas

                ' Para cada remessa existente
                For Each remesa As IngresoRemesas.Remesa In Remesas

                    ' Adiciona o ojeto na lista de remessas
                    objRemesas.Add(New IngresoRemesasNuevo.Remesa)

                    ' Com a última remessa adicionada
                    With objRemesas.Last

                        Dim SectorOrigen As Comon.Clases.Sector = LogicaNegocio.Genesis.Sector.ObtenerSectorPorIDPS(remesa.CodigoSector)
                        If SectorOrigen Is Nothing Then
                            Throw New Exception(String.Format(Traduzir("02_msg_sectororigen_vazio"), remesa.CodigoSector))
                        Else
                            remesa.CodigoSector = SectorOrigen.Codigo
                            .CodigoPlanta = If(SectorOrigen.Planta IsNot Nothing, SectorOrigen.Planta.Codigo, String.Empty)
                        End If

                        If Not String.IsNullOrEmpty(remesa.CodigoSectorDestino) Then

                            Dim SectorDestino As Comon.Clases.Sector = LogicaNegocio.Genesis.Sector.ObtenerSectorPorIDPS(remesa.CodigoSectorDestino)
                            If SectorDestino Is Nothing Then
                                Throw New Exception(String.Format(Traduzir("02_msg_sectordestino_vazio"), remesa.CodigoSectorDestino))
                            Else
                                remesa.CodigoSector = SectorDestino.Codigo
                            End If

                        End If

                        ' Preenche os dados da remessa
                        .CodigoDelegacion = remesa.CodigoDelegacion
                        .CodigoEstado = remesa.CodigoEstado
                        .CodigoPrecinto = remesa.CodigoPrecinto
                        .CodigoSector = remesa.CodigoSector
                        .CodigoSectorDestino = remesa.CodigoSectorDestino
                        .EsInterno = remesa.EsInterno
                        .IdRemesaOrigen = remesa.IdRemesaOrigen
                        .NumBultos = remesa.NumBultos
                        .NumeroExterno = remesa.NumeroExterno
                        .UtilizarReglaAutomata = remesa.UtilizarReglaAutomata
                        .Bultos = ConverterIngresoRemesaNuevoBultos(remesa.Bultos)
                        .CamposExtra = ConverterIngresoRemesaNuevoCamposExtra(remesa.CamposExtra)
                        .DeclaradosAgrupacionRemesa = ConverterIngresoRemesaNuevoDeclaradosAgrupacionRemesa(remesa.DeclaradosAgrupacionRemesa)
                        .DeclaradosDetalleRemesa = ConverterIngresoRemesaNuevoDeclaradosDetalleRemesa(remesa.DeclaradosDetalleRemesa)
                        .DeclaradosTotalRemesa = ConverterIngresoRemesaNuevoDeclaradosTotalRemesa(remesa.DeclaradosTotalRemesa)
                        .ValoresRemesa = ConverterIngresoRemesaNuevoValoresRemesa(remesa.ValoresRemesa)
                        .CodigoCajero = remesa.CodigoCajero
                    End With

                Next

            End If

            ' Retorna as remessas
            Return objRemesas

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoCamposExtra(camposExtras As IngresoRemesas.CamposExtras) As IngresoRemesasNuevo.CamposExtras

            ' Define a variável que receberá a lista de campos extras
            Dim objCamposExtras As IngresoRemesasNuevo.CamposExtras = Nothing

            ' Verifica se existe campos extras
            If camposExtras IsNot Nothing Then

                ' Cria a lista de campos extras
                objCamposExtras = New IngresoRemesasNuevo.CamposExtras

                ' Para cada campo extra existente 
                For Each campoExtra As IngresoRemesas.CampoExtra In camposExtras

                    ' Adiciona o campo extra na lista
                    objCamposExtras.Add(New IngresoRemesasNuevo.CampoExtra)

                    ' Com o último campo extra adicionado
                    With objCamposExtras.Last

                        ' Preenche os dados do campos extras
                        .Nombre = campoExtra.Nombre
                        .Valor = campoExtra.Valor

                    End With

                Next

            End If

            ' Retona a lista de campos extras
            Return objCamposExtras

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosAgrupacionRemesa(declaradosAgrupacionRemesa As IngresoRemesas.DeclaradosAgrupacionRemesa) As IngresoRemesasNuevo.DeclaradosAgrupacionRemesa

            ' Define a variável que receberá a lista de declarados agrupações 
            Dim objDeclaradosAgrupacion As IngresoRemesasNuevo.DeclaradosAgrupacionRemesa = Nothing

            ' Verifica se existe declarados agrupações 
            If declaradosAgrupacionRemesa IsNot Nothing Then

                ' Cria a lista de declarados agrupações 
                objDeclaradosAgrupacion = New IngresoRemesasNuevo.DeclaradosAgrupacionRemesa

                ' Para cada declarado agrupação existente 
                For Each declaradoAgrupacion As IngresoRemesas.DeclaradoAgrupacionRemesa In declaradosAgrupacionRemesa

                    ' Adiciona o declarado agrupação na lista
                    objDeclaradosAgrupacion.Add(New IngresoRemesasNuevo.DeclaradoAgrupacionRemesa)

                    ' Com o último declarado agrupação adicionado
                    With objDeclaradosAgrupacion.Last

                        ' Preenche os dados do declarados agrupações 
                        .CodigoAgrupacion = declaradoAgrupacion.CodigoAgrupacion
                        .NumImporte = declaradoAgrupacion.NumImporte

                    End With

                Next

            End If

            ' Retona a lista de declarados agrupações 
            Return objDeclaradosAgrupacion

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosDetalleRemesa(declaradosDetalleRemesa As IngresoRemesas.DeclaradosDetalleRemesa) As IngresoRemesasNuevo.DeclaradosDetalleRemesa

            ' Define a variável que receberá a lista de declarados detalhes 
            Dim objDeclaradosDetalle As IngresoRemesasNuevo.DeclaradosDetalleRemesa = Nothing

            ' Verifica se existe declarados detalhes 
            If declaradosDetalleRemesa IsNot Nothing Then

                ' Cria a lista de declarados detalhes 
                objDeclaradosDetalle = New IngresoRemesasNuevo.DeclaradosDetalleRemesa

                ' Para cada declarado detalhe existente 
                For Each declaradoDetalle As IngresoRemesas.DeclaradoDetalleRemesa In declaradosDetalleRemesa

                    ' Adiciona o declarado detalhe na lista
                    objDeclaradosDetalle.Add(New IngresoRemesasNuevo.DeclaradoDetalleRemesa)

                    ' Com o último declarado detalhe adicionado
                    With objDeclaradosDetalle.Last

                        ' Preenche os dados do declarados detalhes 
                        .CodigoDenominacion = declaradoDetalle.CodigoDenominacion
                        .Unidades = declaradoDetalle.Unidades

                    End With

                Next

            End If

            ' Retona a lista de declarados detalhes 
            Return objDeclaradosDetalle

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosTotalRemesa(declaradosTotalRemesa As IngresoRemesas.DeclaradosTotalRemesa) As IngresoRemesasNuevo.DeclaradosTotalRemesa

            ' Define a variável que receberá a lista de declarados Totais 
            Dim objDeclaradosTotal As IngresoRemesasNuevo.DeclaradosTotalRemesa = Nothing

            ' Verifica se existe declarados Totais 
            If declaradosTotalRemesa IsNot Nothing Then

                ' Cria a lista de declarados Totais 
                objDeclaradosTotal = New IngresoRemesasNuevo.DeclaradosTotalRemesa

                ' Para cada declarado Total existente 
                For Each declaradoTotal As IngresoRemesas.DeclaradoTotalRemesa In declaradosTotalRemesa

                    ' Adiciona o declarado Total na lista
                    objDeclaradosTotal.Add(New IngresoRemesasNuevo.DeclaradoTotalRemesa)

                    ' Com o último declarado Total adicionado
                    With objDeclaradosTotal.Last

                        ' Preenche os dados do declarados Totais 
                        .CodigoIsoDivisa = declaradoTotal.CodigoIsoDivisa
                        .ImporteCheque = declaradoTotal.ImporteCheque
                        .ImporteEfectivo = declaradoTotal.ImporteEfectivo
                        .ImporteOtroValor = declaradoTotal.ImporteOtroValor
                        .ImporteTicket = declaradoTotal.ImporteTicket
                        .ImporteTotal = declaradoTotal.ImporteTotal

                    End With

                Next

            End If

            ' Retona a lista de declarados Totais 
            Return objDeclaradosTotal

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoValoresRemesa(valoresRemesa As IngresoRemesas.ValoresRemesa) As IngresoRemesasNuevo.ValoresRemesa

            ' Define a variável que receberá a lista de valores
            Dim objValores As IngresoRemesasNuevo.ValoresRemesa = Nothing

            ' Verifica se existe valores
            If valoresRemesa IsNot Nothing Then

                ' Cria a lista de valores
                objValores = New IngresoRemesasNuevo.ValoresRemesa

                ' Para cada valor existente 
                For Each valor As IngresoRemesas.ValorRemesa In valoresRemesa

                    ' Adiciona o valor na lista
                    objValores.Add(New IngresoRemesasNuevo.ValorRemesa)

                    ' Com o último valor adicionado
                    With objValores.Last

                        ' Preenche os dados do valores
                        .CodTerminoIac = valor.CodTerminoIac
                        .DesValorTerminoIac = valor.DesValorTerminoIac

                    End With

                Next

            End If

            ' Retona a lista de valores
            Return objValores

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoBultos(bultos As IngresoRemesas.Bultos) As IngresoRemesasNuevo.Bultos

            ' Define a variável que receberá a lista de malotes
            Dim objBultos As IngresoRemesasNuevo.Bultos = Nothing

            ' Verifica se existe malotes
            If bultos IsNot Nothing Then

                ' Cria a lista de malotes
                objBultos = New IngresoRemesasNuevo.Bultos

                ' Para cada malote existente
                For Each bulto As IngresoRemesas.Bulto In bultos

                    ' Adiciona o malote na lista
                    objBultos.Add(New IngresoRemesasNuevo.Bulto)

                    ' Com o último malote adicionado
                    With objBultos.Last

                        ' Preenche os dados do malote
                        .BolBancoMadre = bulto.BolBancoMadre
                        .CodigoBancoIngreso = bulto.CodigoBancoIngreso
                        .CodigoBolsa = bulto.IdBultoOrigen
                        .CodigoCanal = bulto.CodigoCanal
                        .CodigoCliente = bulto.CodigoCliente
                        .CodigoEstado = bulto.CodigoEstado
                        .CodigoFormato = bulto.CodigoFormato
                        .CodigoPrecinto = bulto.CodigoPrecinto
                        .CodigoPuntoServicio = bulto.CodigoPuntoServicio
                        .CodigoRuta = bulto.CodigoRuta
                        .CodigoSubCanal = bulto.CodigoSubCanal
                        .CodigoSubCliente = bulto.CodigoSubCliente
                        .CodigoTipoTrabajo = bulto.CodigoTipoTrabajo
                        .CodigoTransporte = bulto.CodigoTransporte
                        .CodigoUbicacion = bulto.CodigoUbicacion
                        .DescripcionCanal = bulto.DescripcionCanal
                        .DescripcionCliente = bulto.DescripcionCliente
                        .DescripcionPuntoServicio = bulto.DescripcionPuntoServicio
                        .DescripcionSubCanal = bulto.DescripcionSubCanal
                        .DescripcionSubCliente = bulto.DescripcionSubCliente
                        .FechaConfeccion = bulto.FechaConfeccion
                        .FechaProceso = bulto.FechaProceso
                        .FechaTransporte = bulto.FechaTransporte
                        .IdBultoOrigen = bulto.IdBultoOrigen
                        .IdentificadorSOL = bulto.IdentificadorSOL
                        .NumeroParciales = bulto.NumeroParciales
                        .DeclaradosAgrupacionBulto = ConverterIngresoRemesaNuevoDeclaradosAgrupacionBulto(bulto.DeclaradosAgrupacionBulto)
                        .DeclaradosDetBulto = ConverterIngresoRemesaNuevoDeclaradosDetBulto(bulto.DeclaradosDetBulto)
                        .DeclaradosTotalBulto = ConverterIngresoRemesaNuevoDeclaradosTotalBulto(bulto.DeclaradosTotalBulto)
                        .Parciales = ConverterIngresoRemesaNuevoParciales(bulto.Parciales)
                        .ValoresBulto = ConverterIngresoRemesaNuevoValoresBulto(bulto.ValoresBulto)
                        .CodigoClienteSaldo = bulto.CodigoClienteSaldo
                        .DescripcionClienteSaldo = bulto.DescripcionClienteSaldo
                        .CodigoSubClienteSaldo = bulto.CodigoSubClienteSaldo
                        .DescripcionSubClienteSaldo = bulto.DescripcionSubClienteSaldo
                        .CodigoPuntoServicioSaldo = bulto.CodigoPuntoServicioSaldo
                        .DescripcionPuntoServicioSaldo = bulto.DescripcionPuntoServicioSaldo

                    End With

                Next

            End If

            ' Retona a lista de malotes
            Return objBultos

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosAgrupacionBulto(declaradosAgrupacionBulto As IngresoRemesas.DeclaradosAgrupacionBulto) As IngresoRemesasNuevo.DeclaradosAgrupacionBulto

            ' Define a variável que receberá a lista de declarados agrupações 
            Dim objDeclaradosAgrupacion As IngresoRemesasNuevo.DeclaradosAgrupacionBulto = Nothing

            ' Verifica se existe declarados agrupações 
            If declaradosAgrupacionBulto IsNot Nothing Then

                ' Cria a lista de declarados agrupações 
                objDeclaradosAgrupacion = New IngresoRemesasNuevo.DeclaradosAgrupacionBulto

                ' Para cada declarado agrupação existente 
                For Each declaradoAgrupacion As IngresoRemesas.DeclaradoAgrupacionBulto In declaradosAgrupacionBulto

                    ' Adiciona o declarado agrupação na lista
                    objDeclaradosAgrupacion.Add(New IngresoRemesasNuevo.DeclaradoAgrupacionBulto)

                    ' Com o último declarado agrupação adicionado
                    With objDeclaradosAgrupacion.Last

                        ' Preenche os dados do declarados agrupações 
                        .CodigoAgrupacion = declaradoAgrupacion.CodigoAgrupacion
                        .NumImporte = declaradoAgrupacion.NumImporte
                        .CodCaracteristicaConteo = declaradoAgrupacion.CodCaracteristicaConteo

                    End With

                Next

            End If

            ' Retona a lista de declarados agrupações 
            Return objDeclaradosAgrupacion

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosDetBulto(declaradosDetalleBulto As IngresoRemesas.DeclaradosDetalleBulto) As IngresoRemesasNuevo.DeclaradosDetalleBulto

            ' Define a variável que receberá a lista de declarados detalhes 
            Dim objDeclaradosDetalle As IngresoRemesasNuevo.DeclaradosDetalleBulto = Nothing

            ' Verifica se existe declarados detalhes 
            If declaradosDetalleBulto IsNot Nothing Then

                ' Cria a lista de declarados detalhes 
                objDeclaradosDetalle = New IngresoRemesasNuevo.DeclaradosDetalleBulto

                ' Para cada declarado detalhe existente 
                For Each declaradoDetalle As IngresoRemesas.DeclaradoDetalleBulto In declaradosDetalleBulto

                    ' Adiciona o declarado detalhe na lista
                    objDeclaradosDetalle.Add(New IngresoRemesasNuevo.DeclaradoDetalleBulto)

                    ' Com o último declarado detalhe adicionado
                    With objDeclaradosDetalle.Last

                        ' Preenche os dados do declarados detalhes 
                        .CodigoDenominacion = declaradoDetalle.CodigoDenominacion
                        .Unidades = declaradoDetalle.Unidades

                    End With

                Next

            End If

            ' Retona a lista de declarados detalhes 
            Return objDeclaradosDetalle

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosTotalBulto(declaradosTotalBulto As IngresoRemesas.DeclaradosTotalBulto) As IngresoRemesasNuevo.DeclaradosTotalBulto

            ' Define a variável que receberá a lista de declarados Totais 
            Dim objDeclaradosTotal As IngresoRemesasNuevo.DeclaradosTotalBulto = Nothing

            ' Verifica se existe declarados Totais 
            If declaradosTotalBulto IsNot Nothing Then

                ' Cria a lista de declarados Totais 
                objDeclaradosTotal = New IngresoRemesasNuevo.DeclaradosTotalBulto

                ' Para cada declarado Total existente 
                For Each declaradoTotal As IngresoRemesas.DeclaradoTotalBulto In declaradosTotalBulto

                    ' Adiciona o declarado Total na lista
                    objDeclaradosTotal.Add(New IngresoRemesasNuevo.DeclaradoTotalBulto)

                    ' Com o último declarado Total adicionado
                    With objDeclaradosTotal.Last

                        ' Preenche os dados do declarados Totais 
                        .CodigoIsoDivisa = declaradoTotal.CodigoIsoDivisa
                        .ImporteCheque = declaradoTotal.ImporteCheque
                        .ImporteEfectivo = declaradoTotal.ImporteEfectivo
                        .ImporteOtroValor = declaradoTotal.ImporteOtroValor
                        .ImporteTicket = declaradoTotal.ImporteTicket
                        .ImporteTotal = declaradoTotal.ImporteTotal

                    End With

                Next

            End If

            ' Retona a lista de declarados Totais 
            Return objDeclaradosTotal

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoValoresBulto(valoresBulto As IngresoRemesas.ValoresBulto) As IngresoRemesasNuevo.ValoresBulto

            ' Define a variável que receberá a lista de valores
            Dim objValores As IngresoRemesasNuevo.ValoresBulto = Nothing

            ' Verifica se existe valores
            If valoresBulto IsNot Nothing Then

                ' Cria a lista de valores
                objValores = New IngresoRemesasNuevo.ValoresBulto

                ' Para cada valor existente 
                For Each valor As IngresoRemesas.ValorBulto In valoresBulto

                    ' Adiciona o valor na lista
                    objValores.Add(New IngresoRemesasNuevo.ValorBulto)

                    ' Com o último valor adicionado
                    With objValores.Last

                        ' Preenche os dados do valores
                        .CodTerminoIac = valor.CodTerminoIac
                        .DesValorTerminoIac = valor.DesValorTerminoIac

                    End With

                Next

            End If

            ' Retona a lista de valores
            Return objValores

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoParciales(parciales As IngresoRemesas.Parciales) As IngresoRemesasNuevo.Parciales

            ' Define a variável que receberá a lista de parciais
            Dim objParciales As IngresoRemesasNuevo.Parciales = Nothing

            ' Verifica se existe parciais
            If parciales IsNot Nothing Then

                ' Cria a lista de parciais
                objParciales = New IngresoRemesasNuevo.Parciales

                ' Para cada parcial existente
                For Each parcial As IngresoRemesas.Parcial In parciales

                    ' Adiciona o parcial na lista
                    objParciales.Add(New IngresoRemesasNuevo.Parcial)

                    ' Com o último parcial adicionado
                    With objParciales.Last

                        ' Preenche os dados do parcial
                        .CodigoEstado = parcial.CodigoEstado
                        .CodigoFormato = parcial.CodigoFormato
                        .CodigoPrecinto = parcial.CodigoPrecinto
                        .IdParcialOrigen = parcial.IdParcialOrigen
                        .Secuencia = parcial.Secuencia
                        .DeclaradosAgrupacionParcial = ConverterIngresoRemesaNuevoDeclaradosAgrupacionParcial(parcial.DeclaradosAgrupacionParcial)
                        .DeclaradosDetalleParcial = ConverterIngresoRemesaNuevoDeclaradosDetParcial(parcial.DeclaradosDetalleParcial)
                        .DeclaradoTotaisParcial = ConverterIngresoRemesaNuevoDeclaradosTotalParcial(parcial.DeclaradoTotaisParcial)
                        .ValoresParcial = ConverterIngresoRemesaNuevoValoresParcial(parcial.ValoresParcial)

                    End With

                Next

            End If

            ' Retona a lista de parciais
            Return objParciales

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosAgrupacionParcial(declaradosAgrupacionParcial As IngresoRemesas.DeclaradosAgrupacionParcial) As IngresoRemesasNuevo.DeclaradosAgrupacionParcial

            ' Define a variável que receberá a lista de declarados agrupações 
            Dim objDeclaradosAgrupacion As IngresoRemesasNuevo.DeclaradosAgrupacionParcial = Nothing

            ' Verifica se existe declarados agrupações 
            If declaradosAgrupacionParcial IsNot Nothing Then

                ' Cria a lista de declarados agrupações 
                objDeclaradosAgrupacion = New IngresoRemesasNuevo.DeclaradosAgrupacionParcial

                ' Para cada declarado agrupação existente 
                For Each declaradoAgrupacion As IngresoRemesas.DeclaradoAgrupacionParcial In declaradosAgrupacionParcial

                    ' Adiciona o declarado agrupação na lista
                    objDeclaradosAgrupacion.Add(New IngresoRemesasNuevo.DeclaradoAgrupacionParcial)

                    ' Com o último declarado agrupação adicionado
                    With objDeclaradosAgrupacion.Last

                        ' Preenche os dados do declarados agrupações 
                        .CodigoAgrupacion = declaradoAgrupacion.CodigoAgrupacion
                        .NumImporte = declaradoAgrupacion.NumImporte

                    End With

                Next

            End If

            ' Retona a lista de declarados agrupações 
            Return objDeclaradosAgrupacion

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosDetParcial(declaradosDetalleParcial As IngresoRemesas.DeclaradosDetalleParcial) As IngresoRemesasNuevo.DeclaradosDetalleParcial

            ' Define a variável que receberá a lista de declarados detalhes 
            Dim objDeclaradosDetalle As IngresoRemesasNuevo.DeclaradosDetalleParcial = Nothing

            ' Verifica se existe declarados detalhes 
            If declaradosDetalleParcial IsNot Nothing Then

                ' Cria a lista de declarados detalhes 
                objDeclaradosDetalle = New IngresoRemesasNuevo.DeclaradosDetalleParcial

                ' Para cada declarado detalhe existente 
                For Each declaradoDetalle As IngresoRemesas.DeclaradoDetalleParcial In declaradosDetalleParcial

                    ' Adiciona o declarado detalhe na lista
                    objDeclaradosDetalle.Add(New IngresoRemesasNuevo.DeclaradoDetalleParcial)

                    ' Com o último declarado detalhe adicionado
                    With objDeclaradosDetalle.Last

                        ' Preenche os dados do declarados detalhes 
                        .CodigoDenominacion = declaradoDetalle.CodigoDenominacion
                        .Unidades = declaradoDetalle.Unidades

                    End With

                Next

            End If

            ' Retona a lista de declarados detalhes 
            Return objDeclaradosDetalle

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoDeclaradosTotalParcial(declaradoTotaisParcial As IngresoRemesas.DeclaradoTotaisParcial) As IngresoRemesasNuevo.DeclaradoTotaisParcial

            ' Define a variável que receberá a lista de declarados Totais 
            Dim objDeclaradosTotal As IngresoRemesasNuevo.DeclaradoTotaisParcial = Nothing

            ' Verifica se existe declarados Totais 
            If declaradoTotaisParcial IsNot Nothing Then

                ' Cria a lista de declarados Totais 
                objDeclaradosTotal = New IngresoRemesasNuevo.DeclaradoTotaisParcial

                ' Para cada declarado Total existente 
                For Each declaradoTotal As IngresoRemesas.DeclaradoTotalParcial In declaradoTotaisParcial

                    ' Adiciona o declarado Total na lista
                    objDeclaradosTotal.Add(New IngresoRemesasNuevo.DeclaradoTotalParcial)

                    ' Com o último declarado Total adicionado
                    With objDeclaradosTotal.Last

                        ' Preenche os dados do declarados Totais 
                        .CodigoIsoDivisa = declaradoTotal.CodigoIsoDivisa
                        .ImporteCheque = declaradoTotal.ImporteCheque
                        .ImporteEfectivo = declaradoTotal.ImporteEfectivo
                        .ImporteOtroValor = declaradoTotal.ImporteOtroValor
                        .ImporteTicket = declaradoTotal.ImporteTicket
                        .ImporteTotal = declaradoTotal.ImporteTotal

                    End With

                Next

            End If

            ' Retona a lista de declarados Totais 
            Return objDeclaradosTotal

        End Function

        Private Shared Function ConverterIngresoRemesaNuevoValoresParcial(valoresParcial As IngresoRemesas.ValoresParcial) As IngresoRemesasNuevo.ValoresParcial

            ' Define a variável que receberá a lista de valores
            Dim objValores As IngresoRemesasNuevo.ValoresParcial = Nothing

            ' Verifica se existe valores
            If valoresParcial IsNot Nothing Then

                ' Cria a lista de valores
                objValores = New IngresoRemesasNuevo.ValoresParcial

                ' Para cada valor existente 
                For Each valor As IngresoRemesas.ValorParcial In valoresParcial

                    ' Adiciona o valor na lista
                    objValores.Add(New IngresoRemesasNuevo.ValorParcial)

                    ' Com o último valor adicionado
                    With objValores.Last

                        ' Preenche os dados do valores
                        .CodTerminoIac = valor.CodTerminoIac
                        .DesValorTerminoIac = valor.DesValorTerminoIac

                    End With

                Next

            End If

            ' Retona a lista de valores
            Return objValores

        End Function

    End Class

End Namespace