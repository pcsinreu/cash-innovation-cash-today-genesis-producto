Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Maquina
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel
Imports System.Text

Public Class AccionMAE

    Public Function SetMAE(objPeticion As ContractoServicio.MAE.SetMAE.Peticion) As ContractoServicio.MAE.SetMAE.Respuesta

        Dim objTransaccionOracle As Genesis.DataBaseHelper.Transaccion = New DataBaseHelper.Transaccion()

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MAE.SetMAE.Respuesta

        Try
            Dim objMaquina As ContractoServicio.Maquina.GetMaquinaDetalle.Maquina = Nothing

            If objPeticion.dicionario Is Nothing OrElse objPeticion.dicionario.Count = 0 Then
                objPeticion.dicionario = CarregaDicionario()
            End If

            If Not String.IsNullOrEmpty(objPeticion.OidMaquina) Then
                objMaquina = AccesoDatos.MaquinaMAE.GetMaquinaDetalle(objPeticion.OidMaquina)
            End If

            Dim objColaTransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)


            'Add campos extra
            If objPeticion.PeticionDatosBancarios IsNot Nothing AndAlso objPeticion.PeticionDatosBancarios.DatosBancarios IsNot Nothing AndAlso objPeticion.PeticionDatosBancarios.DatosBancarios.Count > 0 Then
                Dim objAccionDatoBancario As New AccionDatoBancario

                Dim objRespuestaDatoBancario = objAccionDatoBancario.SetDatosBancarios(objPeticion.PeticionDatosBancarios)
                If objRespuestaDatoBancario.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Dim exception As Exception = New Exception("Datos Bancarios: " & objRespuestaDatoBancario.MensajeError)
                    Throw exception
                End If
            End If

            'validar si ya existe un sector con el código igual al valor del “Device ID”
            Dim oidSector As String = String.Empty
            'Dim desSector As String = String.Empty
            Dim objAccionSetor As New AccionSetor
            Dim objPeticionSetor As New ContractoServicio.Setor.GetSectores.Peticion
            'Verifica se é edição da máquina, se for deve atualizar os dados do setor

            If String.IsNullOrEmpty(objPeticion.OidMaquina) Then 'Es alta
                objPeticionSetor.codSector = objPeticion.DeviceID
            ElseIf objPeticion.DeviceIDAnterior <> objPeticion.DeviceID Then 'Cambio la delegacion
                objPeticionSetor.codSector = objPeticion.DeviceID
            Else
                objPeticionSetor.codSector = objPeticion.DeviceIDAnterior
            End If

            ' objPeticionSetor.codSector = IIf(String.IsNullOrEmpty(objPeticion.OidMaquina), objPeticion.DeviceIDAnterior, objPeticion.DeviceID)
            objPeticionSetor.oidPlanta = IIf(objPeticion.OidPlanta = objPeticion.OidPlantaAnterior, objPeticion.OidPlantaAnterior, objPeticion.OidPlanta)
            objPeticionSetor.ParametrosPaginacion.RealizarPaginacion = False
            Dim objRespuestaSetor = objAccionSetor.getSectores(objPeticionSetor)
            If objRespuestaSetor.Setor Is Nothing OrElse objRespuestaSetor.Setor.Count = 0 Then

                'Validar si existe el tipo de sector “MAETC”
                Dim oidTipoSectorMAE As String = ValidarTipoSectorMAE()
                If String.IsNullOrEmpty(oidTipoSectorMAE) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(RecuperarValorDic(objPeticion.dicionario, "MSG_INFO_TIPO_SECTOR_NO_EXISTENTE"), "MAETC"))
                End If

                oidSector = AltaSector(objPeticion.DeviceID, objPeticion.Descripcion, objPeticion.OidPlanta, oidTipoSectorMAE, Nothing, False, False, False, False, objPeticion.desUsuarioModificacion, objColaTransacion)

            Else
                If Not objRespuestaSetor.Setor.First.bolActivo Then
                    oidSector = AltaSector(objRespuestaSetor.Setor.First.codSector,
                               objRespuestaSetor.Setor.First.desSector,
                               objRespuestaSetor.Setor.First.oidPlanta,
                               objRespuestaSetor.Setor.First.oidTipoSector,
                               objRespuestaSetor.Setor.First.oidSectorPadre,
                               objRespuestaSetor.Setor.First.bolCentroProceso,
                               objRespuestaSetor.Setor.First.bolPermiteDisponerValor,
                               objRespuestaSetor.Setor.First.bolTesoro,
                               objRespuestaSetor.Setor.First.bolConteo,
                               objPeticion.desUsuarioModificacion,
                               objColaTransacion,
                               objRespuestaSetor.Setor.First.oidSector)
                ElseIf Not String.IsNullOrEmpty(objPeticion.OidMaquina) Then
                    oidSector = AltaSector(objPeticion.DeviceID,
                               objPeticion.Descripcion,
                                IIf(objRespuestaSetor.Setor.First.oidPlanta = objPeticion.OidPlantaAnterior, objRespuestaSetor.Setor.First.oidPlanta, objPeticion.OidPlanta),
                                objRespuestaSetor.Setor.First.oidTipoSector,
                               objRespuestaSetor.Setor.First.oidSectorPadre,
                               objRespuestaSetor.Setor.First.bolCentroProceso,
                               objRespuestaSetor.Setor.First.bolPermiteDisponerValor,
                               objRespuestaSetor.Setor.First.bolTesoro,
                               objRespuestaSetor.Setor.First.bolConteo,
                               objPeticion.desUsuarioModificacion,
                               objColaTransacion,
                               objRespuestaSetor.Setor.First.oidSector)
                Else
                    oidSector = objRespuestaSetor.Setor.First.oidSector
                End If

            End If

            'Adicionar codigo ajeno do sector que foi criado ou atualizar existente
            Dim oidAjeno As String = Nothing
            Dim ajenoExistente = GetCodigosAjenos(oidSector, "GEPR_TSECTOR")

            If ajenoExistente IsNot Nothing Then

                Dim codigoAjeno = ajenoExistente.EntidadCodigosAjenos.Select(Function(f) f.CodigosAjenos).FirstOrDefault()

                If codigoAjeno IsNot Nothing AndAlso codigoAjeno.Count > 0 Then
                    oidAjeno = codigoAjeno(0).OidCodigoAjeno
                End If

            End If
            objPeticion.CodigosAjeno.Add(New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno With {
                                                    .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                    .CodTipoTablaGenesis = ContractoServicio.Constantes.COD_SECTOR,
                                                    .OidTablaGenesis = oidSector,
                                                    .CodAjeno = objPeticion.DeviceID,
                                                    .DesAjeno = objPeticion.Descripcion,
                                                    .BolActivo = True,
                                                    .DesUsuarioCreacion = objPeticion.desUsuarioCreacion,
                                                    .GmtCreacion = objPeticion.gmtCreacion,
                                                    .DesUsuarioModificacion = objPeticion.desUsuarioModificacion,
                                                    .OidCodigoAjeno = oidAjeno,
                                                    .GmtModificacion = objPeticion.gmtModificacion})


            'Add codigos ajeno
            If objPeticion.CodigosAjeno IsNot Nothing AndAlso objPeticion.CodigosAjeno.Count > 0 Then
                For Each codAjeno In objPeticion.CodigosAjeno
                    codAjeno.CodTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                    Where item.CodTipoTablaGenesis = codAjeno.CodTipoTablaGenesis
                                                    Select item.Entidade).FirstOrDefault()
                Next

                'Filtramos para omitir los codigos ajenos de la entidad SAPR_TMAQUINA para tratarlos más adelante
                Dim filtrado = objPeticion.CodigosAjeno.Where(Function(x) x.CodTipoTablaGenesis <> Constantes.ConstanteMaquina)

                Dim objPeticionCodigosAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
                objPeticionCodigosAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion

                For Each codAjeno In filtrado
                    If String.IsNullOrWhiteSpace(codAjeno.OidCodigoAjeno) Then
                        Dim ajenoExistente2 = GetCodigosAjenos(codAjeno.OidTablaGenesis, codAjeno.CodTipoTablaGenesis)
                        If ajenoExistente2 IsNot Nothing AndAlso ajenoExistente2.EntidadCodigosAjenos IsNot Nothing AndAlso ajenoExistente2.EntidadCodigosAjenos.Count > 0 Then
                            Throw New Excepcion.NegocioExcepcion(String.Format(RecuperarValorDic(objPeticion.dicionario, "MSG_ERROR_AJENO"), codAjeno.CodAjeno, codAjeno.CodTipoTablaGenesis))
                        End If
                    End If
                    objPeticionCodigosAjeno.CodigosAjenos.Add(codAjeno)
                Next

                Dim objRespuestaCodAjeno = New AccionCodigoAjeno().SetCodigosAjenos(objPeticionCodigosAjeno, objColaTransacion)
                If objRespuestaCodAjeno.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Dim exception1 As Exception = New Exception("CodAjeno: " & objRespuestaCodAjeno.MensajeError)
                    Throw exception1
                End If
            End If

            'Deberá validar si ya existe una MAE con el mismo “Device ID”
            Dim objMaquinaDevID = AccesoDatos.MaquinaMAE.GetMaquina(Nothing, objPeticion.DeviceID)
            If objMaquinaDevID IsNot Nothing AndAlso objMaquinaDevID.Identificador <> objPeticion.OidMaquina Then
                Throw New Excepcion.NegocioExcepcion(String.Format(RecuperarValorDic(objPeticion.dicionario, "MSG_INFO_MAE_EXISTENTE"), objPeticion.DeviceID))
            End If

            Dim objMaquinaGrabar As New Comon.Clases.Maquina

            objMaquinaGrabar.Identificador = objPeticion.OidMaquina
            objMaquinaGrabar.Codigo = objPeticion.DeviceID
            objMaquinaGrabar.Modelo = New Comon.Clases.Modelo
            objMaquinaGrabar.Modelo.Identificador = objPeticion.OidModelo
            objMaquinaGrabar.TipoMaquina = New Comon.Clases.TipoMaquina
            Dim lstTiposMaq = AccesoDatos.TipoMaquina.GetTipos("MAE", True)
            objMaquinaGrabar.TipoMaquina.Identificador = lstTiposMaq.First.Identificador
            objMaquinaGrabar.Sector = New Comon.Clases.Sector
            objMaquinaGrabar.Sector.Identificador = oidSector
            objMaquinaGrabar.DesUsuarioCreacion = objPeticion.desUsuarioCreacion
            objMaquinaGrabar.DesUsuarioModificacion = objPeticion.desUsuarioModificacion
            objMaquinaGrabar.FechaHoraCreacion = objPeticion.gmtCreacion
            objMaquinaGrabar.FechaHoraModificacion = objPeticion.gmtModificacion
            objMaquinaGrabar.ConsideraRecuentos = objPeticion.ConsideraRecuentos
            objMaquinaGrabar.MultiClientes = objPeticion.MultiClientes
            objMaquinaGrabar.PorcComisionMaquina = objPeticion.PorcComisionMaquina

            objMaquinaGrabar.BolActivo = True

            objMaquinaGrabar.Identificador = AccesoDatos.MaquinaMAE.SetMaquina(objMaquinaGrabar, objColaTransacion)


            'INICIO Agregar Código ajeno MAE
            If objPeticion.CodigosAjeno IsNot Nothing AndAlso objPeticion.CodigosAjeno.Count > 0 Then
                Dim objPeticionCodigosAjenoMAE As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
                'Filtramos para agregar los codigos ajenos de la entidad SAPR_TMAQUINA
                Dim filtradoMAE = objPeticion.CodigosAjeno.Where(Function(x) x.CodTipoTablaGenesis = Constantes.ConstanteMaquina).ToList
                objPeticionCodigosAjenoMAE.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion
                For Each codAjeno In filtradoMAE
                    'AGREGAR Oid_Maquina a CodigoAjeno
                    codAjeno.OidTablaGenesis = objMaquinaGrabar.Identificador
                    objPeticionCodigosAjenoMAE.CodigosAjenos.Add(codAjeno)
                Next

                Dim accionCodigoAjeno = New AccionCodigoAjeno()

                'Buscamos todos los códigos ajenos de la máquina
                Dim peticionCodigoAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion
                peticionCodigoAjeno.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
                peticionCodigoAjeno.ParametrosPaginacion = New ParametrosPeticionPaginacion
                peticionCodigoAjeno.ParametrosPaginacion.RealizarPaginacion = False
                peticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = Constantes.ConstanteMaquina
                peticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objMaquinaGrabar.Identificador
                Dim codigosMAE = accionCodigoAjeno.GetCodigosAjenos(peticionCodigoAjeno)


                If codigosMAE.CodigoError = 0 AndAlso codigosMAE.EntidadCodigosAjenos IsNot Nothing AndAlso codigosMAE.EntidadCodigosAjenos.Count > 0 Then
                    'Agregamos para borrar los códigos que no se encuentran en la petición y que son distintos de Identificador MAE
                    For Each entidadCodigoAjeno In codigosMAE.EntidadCodigosAjenos
                        'Creamos el objeto para almacenar los códigos a borrar
                        Dim codigosParaBorrar = entidadCodigoAjeno.CodigosAjenos.Where(Function(x) x.CodIdentificador <> "MAE" AndAlso
                            Not filtradoMAE.Exists(Function(f) f.OidCodigoAjeno = x.OidCodigoAjeno))

                        For Each borrar In codigosParaBorrar
                            accionCodigoAjeno.BorrarCodigoAjeno(borrar.OidCodigoAjeno, objColaTransacion)
                        Next
                    Next
                End If

                If (objPeticionCodigosAjenoMAE.CodigosAjenos.Count > 0) Then
                    Dim objRespuestaCodAjenoMAE = accionCodigoAjeno.SetCodigosAjenos(objPeticionCodigosAjenoMAE, objColaTransacion)
                    If objRespuestaCodAjenoMAE.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        Dim exception2 As Exception = New Exception("CodAjeno: " & objRespuestaCodAjenoMAE.MensajeError)
                        Throw exception2
                    End If
                End If
            End If
            'FIN Agregar Código ajeno MAES

            'Atualiza oid_maquina punto servicio
            If objMaquina IsNot Nothing Then
                AccesoDatos.PuntoServicio.DesvincularMaquina(objMaquina.OidMaquina, objPeticion.desUsuarioModificacion, objColaTransacion)
            End If
            For Each cliente In objPeticion.PuntosServicio
                AccesoDatos.PuntoServicio.ActualizarMaquina(cliente.SubClientes.First.PuntosServicio.First.Identificador, objMaquinaGrabar.Identificador, objPeticion.desUsuarioModificacion, objColaTransacion)
            Next

            Dim oidPtoServicioTesoreria As String = String.Empty
            Dim oidSubclienteTesoreria As String = String.Empty

            If objPeticion.BancoTesoreria IsNot Nothing Then
                oidSubclienteTesoreria = objPeticion.BancoTesoreria.Identificador

                If objPeticion.BancoTesoreria.PuntosServicio IsNot Nothing AndAlso objPeticion.BancoTesoreria.PuntosServicio.Count > 0 Then
                    oidPtoServicioTesoreria = objPeticion.BancoTesoreria.PuntosServicio.FirstOrDefault().Identificador
                End If
            End If


            Dim isUpdate As Boolean = False
            Dim mustAssociatePlan As Boolean = False
            Dim isHasTheSectorChangedOfDelegation As Boolean = False

            If objMaquina IsNot Nothing Then
                isUpdate = True
                If objMaquina.Planificacion IsNot Nothing _
                    AndAlso Not String.IsNullOrEmpty(objMaquina.Planificacion.Identificador) _
                    AndAlso objMaquina.Planificacion.Identificador <> objPeticion.OidPlanificacion Then
                    mustAssociatePlan = True
                ElseIf Not (objMaquina.BancoTesoreriaPlanxMaquina Is Nothing AndAlso String.IsNullOrWhiteSpace(oidSubclienteTesoreria) OrElse
                    objMaquina.BancoTesoreriaPlanxMaquina.Identificador = oidSubclienteTesoreria) Then
                    mustAssociatePlan = True
                ElseIf Not (objMaquina.CuentaTesoreriaPlanxMaquina Is Nothing AndAlso String.IsNullOrWhiteSpace(oidPtoServicioTesoreria) OrElse
                    objMaquina.CuentaTesoreriaPlanxMaquina.Identificador = oidPtoServicioTesoreria) Then
                    mustAssociatePlan = True
                ElseIf Not objPeticion.PorcComisionMaquina.Equals(objMaquina.PorcComisionMaquina) Then
                    mustAssociatePlan = True

                ElseIf Not objPeticion.FechaValorInicio.Equals(objMaquina.Planificacion.FechaHoraVigenciaInicio) AndAlso objPeticion.FechaValorInicio <> objMaquina.FechaValorInicio Then
                    mustAssociatePlan = True

                ElseIf Not objPeticion.FechaValorFin.Equals(objMaquina.Planificacion.FechaHoraVigenciaFin) AndAlso objPeticion.FechaValorFin <> objMaquina.FechaValorFin Then
                    mustAssociatePlan = True
                End If

                If objPeticion.OidPlanta <> objPeticion.OidPlantaAnterior Then
                    isHasTheSectorChangedOfDelegation = True
                End If

            Else
                ' es un alta
                mustAssociatePlan = True


            End If



            'Consideramos los estados bloqueado, desbloqueado como abiertos
            Dim listaEstadosPeriodosAbiertos As New List(Of String)

            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Abierto.RecuperarValor())
            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Bloqueado.RecuperarValor())
            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Desbloqueado.RecuperarValor())

            If mustAssociatePlan Then
                If isUpdate Then
                    If objMaquina.Planificacion IsNot Nothing AndAlso objMaquina.Planificacion.TipoPlanificacion.Codigo = "FECHA_VALOR_CONFIR" Then
                        AccesoDatos.Periodo.BorrarPeriodosPorDocumentoAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)
                        AccesoDatos.Periodo.BorrarSaldosAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)
                        AccesoDatos.Periodo.BorrarCalculoMedioPagoAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)
                        AccesoDatos.Periodo.BorrarPeriodoRelacionAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)
                        AccesoDatos.Periodo.BorrarPeriodoRelacionadoAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)
                        AccesoDatos.Periodo.BorrarPeriodosAFuturo(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)

                        AccesoDatos.Periodo.ActualizarPeriodosFV_Confirmacion(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objColaTransacion)

                    Else
                        ' Debo borrar todos los períodos abiertos
                        Dim deboBorrarVigente As Boolean = True

                        AccesoDatos.PeriodoPorDocumento.Borrar(objMaquina.OidMaquina, listaEstadosPeriodosAbiertos, objColaTransacion, deboBorrarVigente)
                        AccesoDatos.CalculoEfectivo.Borrar(objMaquina.OidMaquina, listaEstadosPeriodosAbiertos, objColaTransacion, deboBorrarVigente)
                        AccesoDatos.CalculoMedioPago.Borrar(objMaquina.OidMaquina, listaEstadosPeriodosAbiertos, objColaTransacion, deboBorrarVigente)
                        AccesoDatos.Periodo.BorrarPeriodos(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, listaEstadosPeriodosAbiertos, objColaTransacion, deboBorrarVigente)

                    End If
                    AccesoDatos.Planificacion.DesvincularMaquina(objMaquina.OidMaquina, objMaquina.Planificacion.Identificador, objPeticion.desUsuarioModificacion, objColaTransacion)
                    If Not String.IsNullOrEmpty(objPeticion.OidPlanificacion) Then
                        AccesoDatos.Planificacion.VincularMaquina(objMaquinaGrabar.Identificador, objPeticion.OidPlanificacion, oidSector, objPeticion.FechaValorInicio, objPeticion.FechaValorFin, objPeticion.desUsuarioModificacion,
                                                          objPeticion.PorcComisionMaquina, oidPtoServicioTesoreria, oidSubclienteTesoreria, objColaTransacion)

                    End If


                    If isHasTheSectorChangedOfDelegation Then
                        UpdateDelegationInSector(objMaquinaGrabar.Sector.Identificador, objPeticion.OidPlanta, objColaTransacion)
                    End If
                Else
                    'insert
                    If Not String.IsNullOrEmpty(objPeticion.OidPlanificacion) Then
                        AccesoDatos.Planificacion.VincularMaquina(objMaquinaGrabar.Identificador, objPeticion.OidPlanificacion, oidSector, objPeticion.FechaValorInicio, objPeticion.FechaValorFin, objPeticion.desUsuarioModificacion,
                                                              objPeticion.PorcComisionMaquina, oidPtoServicioTesoreria, oidSubclienteTesoreria, objColaTransacion)

                    End If
                End If
            End If

            objTransaccionOracle.IniciarTransaccion = True

            objColaTransacion.RealizarTransacao()

            If mustAssociatePlan Then
                If Not String.IsNullOrEmpty(objPeticion.OidPlanificacion) Then
                    objRespuesta.AsociaPlan = mustAssociatePlan
                End If

            End If

            'INICIO configuración de limites de la MAE
            Dim peticionMae As New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Peticion
            peticionMae.Configuracion = New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Entrada.Configuracion With {
                .Usuario = objPeticion.desUsuarioModificacion
            }

            Dim log As New StringBuilder

            Dim Mae As New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Entrada.Maquina
            Mae.Accion = "MODIFICAR"
            Mae.DeviceID = objPeticion.DeviceID
            Mae.Limites = New List(Of Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Entrada.Limite)
            For Each limite In objPeticion.Limites
                Mae.Limites.Add(New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Entrada.Limite With {
                                .Accion = limite.Accion,
                                .CodigoDivisa = limite.Divisa.CodigoISO,
                                .NumLimite = limite.NumLimite})
            Next
            peticionMae.Maquinas = New List(Of Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs.Entrada.Maquina)
            peticionMae.Maquinas.Add(Mae)
            Genesis.AccesoDatos.GenesisSaldos.Maquina.ConfigurarMaquinas(String.Empty, peticionMae, log)

            'FIN configuración de limites de la MAE


            GrabarPlanificacionPorMAEPorCanalAndSubCanal(objPeticion, objMaquinaGrabar, objTransaccionOracle)

            DataBaseHelper.AccesoDB.TransactionCommit(objTransaccionOracle)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
            objRespuesta.OidMaquina = objMaquinaGrabar.Identificador

        Catch ex As Excepcion.NegocioExcepcion

            DataBaseHelper.AccesoDB.TransactionRollback(objTransaccionOracle)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception

            DataBaseHelper.AccesoDB.TransactionRollback(objTransaccionOracle)
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Private Sub UpdateDelegationInSector(pOidSector As String, pOidPlanta As String, pObjTransacao As Transacao)
        Prosegur.Genesis.AccesoDatos.Genesis.Sector.ActualizarDelegacionPlanta(pOidSector, pOidPlanta, pObjTransacao)
    End Sub

    Private Sub GrabarPlanificacionPorMAEPorCanalAndSubCanal(pPeticion As ContractoServicio.MAE.SetMAE.Peticion,
                                                      pMaquinaGrabar As Comon.Clases.Maquina, objTransaccion As Genesis.DataBaseHelper.Transaccion)
        Dim objPlanificacion As Comon.Clases.Planificacion = New Comon.Clases.Planificacion
        'Dim lstCanales As ObservableCollection(Of Comon.Clases.Canal) = New ObservableCollection(Of Comon.Clases.Canal)
        'Dim lstSubCanles As ObservableCollection(Of Comon.Clases.SubCanal) = New ObservableCollection(Of Comon.Clases.SubCanal)
        Dim lstPlanes As ObservableCollection(Of Comon.Clases.PlanMaqPorCanalSubCanalPunto)
        lstPlanes = pPeticion.PlanesCanalesSubcanalesPtos
        Dim strCodUsuario As String = pPeticion.desUsuarioModificacion
        Dim objMaquina As Comon.Clases.Maquina = New Comon.Clases.Maquina

        Dim lstPunto As ObservableCollection(Of Comon.Clases.PuntoServicio) = New ObservableCollection(Of Comon.Clases.PuntoServicio)

        objMaquina.Identificador = pMaquinaGrabar.Identificador
        objPlanificacion.Identificador = pPeticion.OidPlanificacion

        'deletar por retirar/modificar planificação da maquina 
        Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.DelPlanX_CanalMAE(objPlanificacion, pMaquinaGrabar, strCodUsuario, objTransaccion)


        'deletar por retirar/modificar punto servicio da maquina 
        For Each cliente In pPeticion.PuntosServicio
            lstPunto.AddRange(cliente.SubClientes.First.PuntosServicio)
        Next

        Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.DelPlanX_CanalPuntos(lstPunto, objMaquina, strCodUsuario, objTransaccion)



        If lstPlanes IsNot Nothing Then
            strCodUsuario = pPeticion.desUsuarioModificacion
            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.GrabarPlanPorCanal(objPlanificacion, lstPlanes, objMaquina, strCodUsuario, objTransaccion)
        End If



    End Sub

    Private Function ValidarTipoSectorMAE() As String
        Dim objPeticionAccionTipoSetor = New AccionTipoSetor
        Dim objPeticionTipoSetor As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        objPeticionTipoSetor.codTipoSector = "MAETC"
        objPeticionTipoSetor.bolActivo = True
        objPeticionTipoSetor.ParametrosPaginacion.RealizarPaginacion = False
        Dim objRespTipoSetor = objPeticionAccionTipoSetor.GetTiposSectores(objPeticionTipoSetor)
        If objRespTipoSetor.TipoSetor IsNot Nothing AndAlso objRespTipoSetor.TipoSetor.Count > 0 Then
            Return objRespTipoSetor.TipoSetor.First.oidTipoSector
        Else
            Return Nothing
        End If
    End Function

    Private Function ValidarSector(codSector As String, oidPlanta As String) As String
        Dim objAccionSetor As New AccionSetor
        Dim objPeticionSetor As New ContractoServicio.Setor.GetSectores.Peticion
        objPeticionSetor.codSector = codSector
        objPeticionSetor.oidPlanta = oidPlanta
        objPeticionSetor.bolActivo = True
        objPeticionSetor.ParametrosPaginacion.RealizarPaginacion = False
        Dim objRespuestaSetor = objAccionSetor.getSectores(objPeticionSetor)
        If objRespuestaSetor.Setor IsNot Nothing AndAlso objRespuestaSetor.Setor.Count > 0 Then
            Return objRespuestaSetor.Setor.First.oidSector
        Else
            Return Nothing
        End If
    End Function

    Private Function AltaSector(codSector As String, desSector As String, oidPlanta As String, oidTipoSector As String, oidSectorPadre As Object, bolCentroProceso As Boolean, bolPermiteDisponerValor As Boolean, bolTesoro As Boolean, bolConteo As Boolean, usuario As String, objTransacao As Transacao, Optional oidSector As String = Nothing) As String
        Dim objPeticionSetSetor As New ContractoServicio.Setor.SetSectores.Peticion
        objPeticionSetSetor.oidSector = IIf(String.IsNullOrEmpty(oidSector), New Guid().ToString(), oidSector)
        objPeticionSetSetor.codSector = codSector
        objPeticionSetSetor.desSector = desSector
        objPeticionSetSetor.oidPlanta = oidPlanta
        objPeticionSetSetor.oidTipoSector = oidTipoSector
        objPeticionSetSetor.oidSectorPadre = oidSectorPadre
        objPeticionSetSetor.bolCentroProceso = bolCentroProceso
        objPeticionSetSetor.bolPermiteDisponerValor = bolPermiteDisponerValor
        objPeticionSetSetor.bolTesoro = bolTesoro
        objPeticionSetSetor.bolConteo = bolConteo
        objPeticionSetSetor.bolActivo = True
        objPeticionSetSetor.gmtCreacion = DateTime.Now
        objPeticionSetSetor.desUsuarioCreacion = usuario
        objPeticionSetSetor.gmtModificacion = DateTime.Now
        objPeticionSetSetor.desUsuarioModificacion = usuario
        Dim oidSectorCreado As String = String.Empty

        If String.IsNullOrEmpty(oidSector) Then
            oidSectorCreado = AccesoDatos.Sector.setSectores(objPeticionSetSetor, objTransacao)
        Else
            AccesoDatos.Sector.AtualizarSector(objPeticionSetSetor, objTransacao)
            Return oidSector
        End If

        If Not IsNullOrEmpty(oidSectorCreado) Then
            Return oidSectorCreado
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtém os dados do codigo ajeno por oidTablaGenesis
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Function GetCodigosAjenos(idTablaGenesis As String, codTipoTablaGenesis As String) As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = codTipoTablaGenesis
        objPeticion.CodigosAjeno.OidTablaGenesis = idTablaGenesis
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno

        ' chamar servicio
        Return objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

    End Function

    Public Function BajaMAE(oidMaquina As String, oidPlanificacion As String, usuario As String) As ContractoServicio.MAE.BajaMAE.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.MAE.BajaMAE.Respuesta

        Try
            Dim objTransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            AccesoDatos.MaquinaMAE.BajaMaquina(oidMaquina, usuario, objTransacion)
            Dim listaEstadosPeriodosAbiertos As New List(Of String)

            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Abierto.RecuperarValor())
            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Bloqueado.RecuperarValor())
            listaEstadosPeriodosAbiertos.Add(Prosegur.Genesis.Comon.Enumeradores.EstadoPeriodo.Desbloqueado.RecuperarValor())


            'Borrar Periodos
            If Not String.IsNullOrEmpty(oidPlanificacion) Then

                'Desvincular maquina
                AccesoDatos.Planificacion.DesvincularMaquina(oidMaquina, oidPlanificacion, usuario, objTransacion)

                AccesoDatos.PeriodoPorDocumento.Borrar(oidMaquina, listaEstadosPeriodosAbiertos, objTransacion)

                AccesoDatos.CalculoEfectivo.Borrar(oidMaquina, listaEstadosPeriodosAbiertos, objTransacion)

                AccesoDatos.CalculoMedioPago.Borrar(oidMaquina, listaEstadosPeriodosAbiertos, objTransacion)

                AccesoDatos.Periodo.BorrarPeriodos(oidMaquina, oidPlanificacion, listaEstadosPeriodosAbiertos, objTransacion)

            End If

            Dim objMaquina = New Comon.Clases.Maquina
            objMaquina.Identificador = oidMaquina
            'deletar por retirar/modificar planificação da maquina 
            Prosegur.Genesis.AccesoDatos.Genesis.Planificacion.DelPlanX_CanalMAE(Nothing, objMaquina, usuario)


            objTransacion.RealizarTransacao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
            objRespuesta.MensajeError = ex.Descricao
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        End Try

        Return objRespuesta
    End Function

    Public Shared Sub consultaAccionesEnLote(identificadorDelegacion As String,
                                             identificadorBanco As String,
                                             identificadorCliente As String,
                                             identificadorPlanificacion As String,
                                             ByRef respuesta As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta))

        AccesoDatos.MaquinaMAE.consultaAccionesEnLote(identificadorDelegacion, identificadorBanco, identificadorCliente, identificadorPlanificacion, respuesta)
    End Sub

    Public Shared Sub AccionesEnLote(identificadores As List(Of String),
                                     nuevoValue As Boolean,
                                     accion As Integer)

        AccesoDatos.MaquinaMAE.AccionesEnLote(identificadores, nuevoValue, accion)
    End Sub

    Public Shared Function consultaAccionesEnLoteExportar(identificadores As List(Of String)) As String

        Dim archivo As String = String.Empty

        Try

            Dim dtValores As DataTable = AccesoDatos.MaquinaMAE.consultaAccionesEnLoteExportar(identificadores)
            archivo = Util.GenerarCSV(dtValores)

        Catch ex As Exception
            Throw ex
        Finally
            GC.Collect()
        End Try

        Return archivo

    End Function

#Region "[DICIONARIO]"

    Private Function CarregaDicionario() As Comon.SerializableDictionary(Of String, String)
        Dim dtDicionario As DataTable = Prosegur.Genesis.AccesoDatos.Genesis.Diccionario.ObtenerValorDicionario(Globalization.CultureInfo.CurrentCulture.ToString(), "ABM_MAE")
        Dim lstDicionario As New Comon.SerializableDictionary(Of String, String)
        If dtDicionario IsNot Nothing AndAlso dtDicionario.Rows.Count > 0 Then
            For Each dr As DataRow In dtDicionario.Rows
                lstDicionario.Add(dr("COD_EXPRESION"), dr("VALOR"))
            Next
        End If
        Return lstDicionario
    End Function

    Private Function RecuperarValorDic(dicionario As Comon.SerializableDictionary(Of String, String), chave As String) As String
        If dicionario IsNot Nothing AndAlso dicionario.Count > 0 Then
            If Not dicionario.ContainsKey(chave) Then
                Return chave
            Else
                Return dicionario(chave)
            End If
        End If
        Return chave
    End Function

#End Region



End Class