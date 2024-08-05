Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Excepcion

Public MustInherit Class BaseIntegracion

    Protected ReadOnly Property Configuracion As Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion

    Protected Sub New(pConfiguracion As Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion)
        Configuracion = pConfiguracion
        Configuracion.Link = GetParametro(Configuracion.CodigoAplicacion, Configuracion.NombreParametroUrl)
    End Sub

    Protected Property IntegracionesPendientes As IntegracionGenerica.Respuesta

    Public Function Ejecutar() As List(Of Integracion)

        Dim dicIntegracionEstado As New Dictionary(Of String, Enumeradores.EstadoIntegracion)

        'Validar si existe el parámetro de URL de envío
        If String.IsNullOrWhiteSpace(Configuracion.Link) Then
            Throw New NegocioExcepcion("No existe un valor para el codigo de parametro " + Configuracion.NombreParametroUrl)
        End If

        If Configuracion.IdentificadoresIntegracion Is Nothing OrElse Configuracion.IdentificadoresIntegracion.Count = 0 Then
            IntegracionesPendientes = RecuperarIntegracionesPendientes()
        Else
            IntegracionesPendientes = Prosegur.Genesis.AccesoDatos.Genesis.Integracion.RecuperarIntegracionPorIdentificadorTabla(Configuracion.IdentificadorLlamada, Configuracion.IdentificadoresIntegracion, Configuracion.CodigoProceso, Configuracion.SistemaOrigem, Configuracion.SistemaDestino)
        End If

        For Each identificadorIntegracion In IntegracionesPendientes.ListaIntegracion
            If Not dicIntegracionEstado.ContainsKey(identificadorIntegracion.IdentificadorTablaIntegracion) Then
                dicIntegracionEstado.Add(identificadorIntegracion.IdentificadorTablaIntegracion, identificadorIntegracion.CodigoEstado)
            End If
        Next

        'Ver de cambiar
        Dim Respuesta = New List(Of Integracion)
        Dim identificador As String

        For Each identificadorIntegracion In IntegracionesPendientes.ListaIntegracion
            Dim objRespuesta = New Integracion
            identificador = identificadorIntegracion.IdentificadorTablaIntegracion

            If (Configuracion.CodigoEstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.ReenvioManual) OrElse
                Not (dicIntegracionEstado.ContainsKey(identificador) AndAlso
                ((dicIntegracionEstado(identificador) = Comon.Enumeradores.EstadoIntegracion.Pendiente OrElse
                dicIntegracionEstado(identificador) = Comon.Enumeradores.EstadoIntegracion.Cerrado))) Then

                Dim log = New Text.StringBuilder

                'Se agrega el CodigoTablaIntegracion del objeto Clases.Integracion porque podemos tener para el caso de un mismo código de proceso que realice varias notificiaciones/envios
                Dim objPeticionIntegracion = New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Integracion.Peticion With {
                    .CodigoTablaIntegracion = identificadorIntegracion.CodigoTablaIntegracion,
                    .IdentificadorTablaIntegracion = identificador,
                    .ModuloOrigem = Configuracion.SistemaOrigem,
                    .ModuloDestino = Configuracion.SistemaDestino,
                    .CodigoProceso = Configuracion.CodigoProceso,
                    .IncrementoIntento = 0,
                    .ReiniciarIntento = Configuracion.ReiniciarIntento,
                    .Usuario = Configuracion.Usuario,
                    .Detalle = New Contractos.Integracion.Comon.Entidad,
                    .EstadoDetalle = Configuracion.CodigoEstadoDetalle
                }
                objPeticionIntegracion.Detalle.Descripcion = Configuracion.Mensaje

                If Configuracion.Detener Then
                    objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Cerrado
                Else
                    objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Pendiente
                End If

                Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ConfigurarIntegracion(Configuracion.IdentificadorLlamada, objPeticionIntegracion, log)

                If Not Configuracion.Detener Then
                    Try
                      'Se obtiene la información para enviar al sistema destino
                        Dim objPeticion = GenerarPeticion(Configuracion.IdentificadorLlamada, identificador, Configuracion.CodigoPais)

                        Try
                            If objPeticion IsNot Nothing Then
                                Dim respuestaSistemaDestino = EjecutarSistemaDestino(Configuracion.IdentificadorLlamada, Configuracion.Link, objPeticion)

                                If ValidarExito(respuestaSistemaDestino) Then
                                    objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Cerrado
                                Else

                                    If dicIntegracionEstado.ContainsKey(identificador) Then
                                        'En caso de que no sea exito volvemos a poner el estado anterior
                                        objPeticionIntegracion.Estado = dicIntegracionEstado(identificador)
                                    Else
                                        objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Abierto
                                    End If

                                End If
                                objPeticionIntegracion.Detalle = New Contractos.Integracion.Comon.Entidad()


                                objRespuesta = BuscarRespuesta(respuestaSistemaDestino)
                                objPeticionIntegracion.Detalle.Codigo = objRespuesta.TipoError
                                objPeticionIntegracion.Detalle.Descripcion = objRespuesta.Detalle

                                If Not String.IsNullOrWhiteSpace(objRespuesta.TipoError) Then
                                    objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioFallo
                                Else
                                    objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioExito
                                End If
                            Else
                                objRespuesta.TipoResultado = "0"
                                objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioFallo
                                If dicIntegracionEstado.ContainsKey(identificador) Then
                                    'En caso de que no sea exito volvemos a poner el estado anterior
                                    objPeticionIntegracion.Estado = dicIntegracionEstado(identificador)
                                Else
                                    objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Abierto
                                End If
                            End If

                        Catch ex As Exception

                            If dicIntegracionEstado.ContainsKey(identificador) Then
                                'En caso de que no sea exito volvemos a poner el estado anterior
                                objPeticionIntegracion.Estado = dicIntegracionEstado(identificador)
                            Else
                                objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Abierto
                            End If


                            objPeticionIntegracion.Detalle = New Contractos.Integracion.Comon.Entidad

                            objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioFallo

                            objRespuesta.TipoResultado = "3"
                            objRespuesta.TipoError = "Infraestructura"
                            objRespuesta.Detalle = "Message: " & ex.Message


                            objPeticionIntegracion.Detalle.Codigo = objRespuesta.TipoError
                            objPeticionIntegracion.Detalle.Descripcion = objRespuesta.Detalle
                            objPeticionIntegracion.Log = Util.RecuperarMensagemTratada(ex)
                        End Try

                    Catch ex As NegocioExcepcion
                        'Respuesta
                        objRespuesta.TipoResultado = "2"
                        objRespuesta.TipoError = ex.Codigo
                        objRespuesta.Detalle = ex.Descricao

                        'Integracion
                        If dicIntegracionEstado.ContainsKey(identificador) Then
                            'En caso de que no sea exito volvemos a poner el estado anterior
                            objPeticionIntegracion.Estado = dicIntegracionEstado(identificador)
                        Else
                            objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Abierto
                        End If
                        objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioFallo

                        objPeticionIntegracion.Detalle = New Contractos.Integracion.Comon.Entidad With {
                            .Codigo = "Negocio",
                            .Descripcion = objRespuesta.Detalle
                        }

                    Catch ex As Exception
                        'Respuesta
                        objRespuesta.TipoResultado = "3"
                        objRespuesta.TipoError = "Infraestructura"
                        objRespuesta.Detalle = "Message: " & ex.Message

                        'Integracion
                        If dicIntegracionEstado.ContainsKey(identificador) Then
                            'En caso de que no sea exito volvemos a poner el estado anterior
                            objPeticionIntegracion.Estado = dicIntegracionEstado(identificador)
                        Else
                            objPeticionIntegracion.Estado = Comon.Enumeradores.EstadoIntegracion.Abierto
                        End If
                        objPeticionIntegracion.Detalle = New Contractos.Integracion.Comon.Entidad With {
                            .Codigo = objRespuesta.TipoError,
                            .Descripcion = objRespuesta.Detalle
                        }
                        objPeticionIntegracion.EstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.EnvioFallo
                        objPeticionIntegracion.Log = Util.RecuperarMensagemTratada(ex)
                    End Try

                    objRespuesta.Identificador = identificador
                    objPeticionIntegracion.ReiniciarIntento = False
                    objPeticionIntegracion.IncrementoIntento = 1

                    Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ConfigurarIntegracion(Configuracion.IdentificadorLlamada, objPeticionIntegracion, log)
                End If
            Else

                objRespuesta.TipoError = ""
                objRespuesta.Detalle = "Documento 'PENDIENTE' o 'CERRADO'."
            End If

            Respuesta.Add(objRespuesta)

        Next

        Return Respuesta

    End Function

    'Carregar Petição para envio sistema destino
    Protected MustOverride Function GenerarPeticion(identificadorLlamada As String, identificador As String, codigoPais As String) As Object

    'executar Petição e retornar resposta do serviço destino
    Protected MustOverride Function EjecutarSistemaDestino(identificadorLlamada As String, link As String, peticion As Object) As Object

    'verificar exito em sistema destino
    Protected MustOverride Function ValidarExito(respuesta As Object) As Boolean

    'Buscar Lista de Mensagens de de resposta
    Protected MustOverride Function BuscarRespuesta(respuesta As Object) As Integracion

    Public Sub DefinirIdentificadores(identificadores As List(Of String))
        Configuracion.IdentificadoresIntegracion = identificadores
    End Sub

    Protected Function RecuperarIntegracionesPendientes() As IntegracionGenerica.Respuesta
        Dim peticionIntegracion = New IntegracionGenerica.Peticion With
                         {
                         .CodigoProceso = Configuracion.CodigoProceso,
                         .CodigoOrigen = Configuracion.SistemaOrigem,
                         .CodigoDestino = Configuracion.SistemaDestino,
                         .NombreParametroReintentoMaximo = Configuracion.NombreParametroReintentoMaximo,
                         .CodigoPais = Configuracion.CodigoPais,
                         .IdentificadorLlamada = Configuracion.IdentificadorLlamada
                     }
        peticionIntegracion.ListaCodigosEstado = New List(Of Comon.Enumeradores.EstadoIntegracion)
        peticionIntegracion.ListaCodigosEstado.AddRange(Configuracion.EstadosBusquedaIntegracion)


        Return AccesoDatos.Genesis.Integracion.RecuperarIntegracionPendientes(peticionIntegracion) '.ListaIntegracion.Select(Function(x) x.IdentificadorTablaIntegracion).Distinct().ToList()
    End Function

    Protected Function GetParametro(codigoAplicacion As String, nombreParametro As String) As String
        Dim url As String = String.Empty
        Dim lista = Util.GetParametros(codigoAplicacion, nombreParametro)
        If lista IsNot Nothing AndAlso lista.Count > 0 Then
            If Not lista.ElementAt(0).MultiValue AndAlso lista.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                url = lista.ElementAt(0).Valores.ElementAt(0)
            Else
                If lista.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    url = lista.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If
        Return url
    End Function

    Public Class Integracion

        Public Property Identificador As String
        Public Property TipoResultado As String
        Public Property TipoError As String
        Public Property Detalle As String

    End Class


End Class
