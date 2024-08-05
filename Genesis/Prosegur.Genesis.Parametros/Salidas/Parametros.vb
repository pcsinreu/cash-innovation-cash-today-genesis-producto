Imports Prosegur.AgenteDispositivos.ContractoServicio
Imports Prosegur.AgenteDispositivos.ContractoServicio.DatosMensajeVideoSalidas
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.ValorPosible
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Reflection
Imports System.Threading.Tasks
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comunicacion.ProxyWS.Integracion


Namespace Salidas

    Public Class Parametros

        Private Shared _InformacionUsuario As ContractoServicio.Login.EjecutarLogin.Usuario
        Private Shared _ClientesSaldo As Dictionary(Of String, ObservableCollection(Of Comon.Clases.Cliente))

        Public Shared Property ClientesSaldo As Dictionary(Of String, ObservableCollection(Of Comon.Clases.Cliente))
            Get
                Return _ClientesSaldo
            End Get
            Set(value As Dictionary(Of String, ObservableCollection(Of Comon.Clases.Cliente)))
                _ClientesSaldo = value
            End Set
        End Property

        Public Shared ReadOnly Property InformacionUsuario As ContractoServicio.Login.EjecutarLogin.Usuario
            Get
                Return _InformacionUsuario
            End Get
        End Property

        Private Shared _ParametrosIAC As New ParametrosIAC()
        Public Shared ReadOnly Property ParametrosIAC As ParametrosIAC
            Get
                Return _ParametrosIAC
            End Get
        End Property

        Private Shared _ParametrosDVR As New DatosMensajeVideoSalidas()
        Public Shared ReadOnly Property ParametrosDVR As DatosMensajeVideoSalidas
            Get
                Return _ParametrosDVR
            End Get
        End Property

        Private Shared _Divisas As New ObservableCollection(Of Comon.Clases.Divisa)
        Public Shared ReadOnly Property Divisas As ObservableCollection(Of Comon.Clases.Divisa)
            Get
                If _Divisas Is Nothing OrElse _Divisas.Count = 0 Then

                    SyncLock _Divisas

                        If _Divisas Is Nothing Then
                            RecuperarDivisas()
                        End If

                    End SyncLock

                End If

                Return _Divisas

            End Get
        End Property

        Private Shared _Puestos As New ObservableCollection(Of Comon.Clases.Puesto)
        Public Shared ReadOnly Property Puestos As ObservableCollection(Of Comon.Clases.Puesto)
            Get
                If _Puestos Is Nothing OrElse _Puestos.Count = 0 Then

                    SyncLock _Puestos

                        If _Puestos Is Nothing Then
                            RecuperarPuestos()
                        End If

                    End SyncLock

                End If

                Return _Puestos

            End Get
        End Property

        Private Shared _Calidades As New ObservableCollection(Of Comon.Clases.Calidad)
        Public Shared ReadOnly Property Calidades As ObservableCollection(Of Comon.Clases.Calidad)
            Get
                If _Calidades Is Nothing OrElse _Calidades.Count = 0 Then

                    SyncLock _Calidades

                        If _Calidades Is Nothing Then
                            RecuperarCalidades()
                        End If

                    End SyncLock

                End If

                Return _Calidades

            End Get
        End Property

        Private Shared _UnidadesMedida As New ObservableCollection(Of Comon.Clases.UnidadMedida)
        Public Shared ReadOnly Property UnidadesMedida As ObservableCollection(Of Comon.Clases.UnidadMedida)
            Get
                If _UnidadesMedida Is Nothing OrElse _UnidadesMedida.Count = 0 Then

                    SyncLock _UnidadesMedida

                        If _UnidadesMedida Is Nothing Then
                            RecuperarUnidadesMedida()
                        End If

                    End SyncLock

                End If

                Return _UnidadesMedida

            End Get
        End Property

        Private Shared _TiposBulto As New ObservableCollection(Of Comon.Clases.TipoBulto)
        Public Shared ReadOnly Property TiposBulto As ObservableCollection(Of Comon.Clases.TipoBulto)
            Get
                If _TiposBulto Is Nothing OrElse _TiposBulto.Count = 0 Then

                    SyncLock _TiposBulto

                        If _TiposBulto Is Nothing Then
                            RecuperarTiposBulto()
                        End If

                    End SyncLock

                End If

                Return _TiposBulto

            End Get
        End Property

        Private Shared _Emisores As New ObservableCollection(Of Comon.Clases.EmisorDocumento)
        Public Shared ReadOnly Property Emisores As ObservableCollection(Of Comon.Clases.EmisorDocumento)
            Get
                If _Emisores Is Nothing OrElse _Emisores.Count = 0 Then

                    SyncLock _Emisores

                        If _Emisores Is Nothing Then
                            RecuperarEmisores()
                        End If

                    End SyncLock

                End If

                Return _Emisores

            End Get
        End Property

        Private Shared _TiposMercancia As New ObservableCollection(Of Comon.Clases.TipoMercancia)
        Public Shared ReadOnly Property TiposMercancia As ObservableCollection(Of Comon.Clases.TipoMercancia)
            Get
                If _TiposMercancia Is Nothing OrElse _TiposMercancia.Count = 0 Then

                    SyncLock _TiposMercancia

                        If _TiposMercancia Is Nothing Then
                            RecuperarTiposMercancia()
                        End If

                    End SyncLock

                End If

                Return _TiposMercancia

            End Get
        End Property

        Private Shared _Sectores As New ObservableCollection(Of Comon.Clases.Sector)
        Public Shared ReadOnly Property Sectores As ObservableCollection(Of Comon.Clases.Sector)
            Get

                If _Sectores Is Nothing OrElse _Sectores.Count = 0 Then

                    SyncLock _Sectores

                        If _Sectores Is Nothing Then
                            RecuperarSectores()
                        End If

                    End SyncLock

                End If

                Return _Sectores

            End Get
        End Property

        Private Shared _ProxyAgente As New AgenteDispositivos.ProxyAgente()
        Public Shared ReadOnly Property ProxyAgente As AgenteDispositivos.ProxyAgente
            Get
                Return _ProxyAgente
            End Get
        End Property

        Public Shared Sub Inicializar(informacionUsuario As ContractoServicio.Login.EjecutarLogin.Usuario)

            Try

                _InformacionUsuario = informacionUsuario

            Dim tfRecuperarParametrosPosto As Task = Nothing
            Dim tfRecuperarParametrosGenesis As Task = Nothing
            Dim tfRecuperarParametrosSaldos As Task = Nothing

            tfRecuperarParametrosPosto = New Task(Sub()
                                                      MapearParametros(RecuperaDatosPuesto(informacionUsuario))
                                                      ParametrosDVR.CodigoUsuario = informacionUsuario.Login
                                                      ParametrosDVR.ApellidoUsuario = informacionUsuario.Apellido
                                                      ParametrosDVR.CodigoPuesto = informacionUsuario.CodigoPuesto
                                                      ParametrosDVR.DescripcionDelegacion = informacionUsuario.DesDelegacion
                                                      ParametrosDVR.NombreUsuario = informacionUsuario.Nombre
                                                  End Sub)

            tfRecuperarParametrosGenesis = New Task(Sub()
                                                        RecuperarParametrosGenesis()
                                                    End Sub)

            tfRecuperarParametrosSaldos = New Task(Sub()
                                                       RecuperarParametrosSaldos()
                                                   End Sub)

            tfRecuperarParametrosGenesis.Start()
            tfRecuperarParametrosPosto.Start()
            tfRecuperarParametrosSaldos.Start()

            Task.WaitAll(New Task() {tfRecuperarParametrosGenesis, tfRecuperarParametrosPosto, tfRecuperarParametrosSaldos})

            Catch ex As Exception

                If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) AndAlso TypeOf ex.InnerException Is Excepcion.NegocioExcepcion Then
                    Throw New Excepcion.NegocioExcepcion(ex.InnerException.Message)
                Else
                    Throw New Exception(ex.ToString())
                End If

            End Try


        End Sub

        Private Shared Sub MapearParametros(datosPuesto As RecuperarParametros.DatosPuesto)

            If datosPuesto IsNot Nothing Then
                SyncLock ParametrosIAC

                    Dim propriedades As PropertyInfo() = ParametrosIAC.GetType().GetProperties()
                    For Each Valor As RecuperarParametros.Parametro In datosPuesto.Parametros

                        Dim propriedadeCodigo As String = Valor.CodigoParametro
                        Dim propriedadeValor As Object
                        Dim propriedade As PropertyInfo = propriedades.FirstOrDefault(Function(p) p.Name.ToUpper().Equals(propriedadeCodigo.ToUpper()))

                        If propriedade IsNot Nothing Then

                            If Valor.ValoresPosibles IsNot Nothing AndAlso Valor.ValoresPosibles.Count > 0 Then
                                Dim colecaoValores As New List(Of ValorPosible)
                                For Each vp In Valor.ValoresPosibles
                                    Dim valorPosivel As New ValorPosible
                                    valorPosivel.Codigo = If(vp.CodigoValor.Trim().Length = 0, Nothing, vp.CodigoValor.Trim())
                                    valorPosivel.Descripcion = vp.Valor
                                    If Valor.ValorParametro IsNot Nothing Then
                                        valorPosivel.esValorDefecto = (vp.CodigoValor.Trim().ToUpper().Equals(Valor.ValorParametro.Trim().ToUpper()))
                                    End If
                                    colecaoValores.Add(valorPosivel)
                                Next
                                propriedadeValor = colecaoValores
                            Else
                                propriedadeValor = If(Valor.ValorParametro IsNot Nothing, Convert.ChangeType(If(IsNumeric(Valor.ValorParametro.Trim()) AndAlso (TypeOf propriedade.GetValue(ParametrosIAC, Nothing) Is Boolean OrElse TypeOf propriedade.GetValue(ParametrosIAC, Nothing) Is Integer), Convert.ToInt32(Valor.ValorParametro.Trim()), Valor.ValorParametro.Trim()), propriedade.PropertyType), Nothing)
                            End If

                            If propriedadeValor IsNot Nothing Then
                                propriedade.SetValue(ParametrosIAC, propriedadeValor, Nothing)
                            End If
                        End If
                    Next

                End SyncLock
            End If

        End Sub

        Private Shared Function RecuperaDatosPuesto(informaciones As ContractoServicio.Login.EjecutarLogin.Usuario) As RecuperarParametros.DatosPuesto

            Dim Peticion As New RecuperarParametros.Peticion
            Dim Respuesta As New RecuperarParametros.Respuesta
            Dim ProxyLogin = Prosegur.Genesis.Comunicacion.ClienteServicio.IacIntegracion

            Peticion.CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALIDAS
            Peticion.CodigoHostPuesto = My.Computer.Name
            Peticion.CodigoPuesto = informaciones.CodigoPuesto
            Peticion.CodigoDelegacion = informaciones.CodigoDelegacion

#If DEBUG Then

            If Not String.IsNullOrEmpty(informaciones.CodigoPuesto) Then

                Dim Codigos As String() = informaciones.CodigoPuesto.Split("@")

                If Codigos.Count > 1 Then
                    Peticion.CodigoHostPuesto = Codigos(1)
                    Peticion.CodigoPuesto = Codigos(0)
                    informaciones.CodigoPuesto = Codigos(0)
                End If

            End If
#End If
            Respuesta = ProxyLogin.RecuperarParametros(Peticion)

            If Respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                If Respuesta.CodigoError >= Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Throw New Excepcion.NegocioExcepcion(Respuesta.CodigoError, Respuesta.MensajeError)
                Else
                    Throw New Exception(Respuesta.MensajeError)
                End If
            End If

            Respuesta.DatosPuesto.CodigoDelegacion = informaciones.CodigoDelegacion

            Return Respuesta.DatosPuesto

        End Function

        Private Shared Sub RecuperarParametrosGenesis()

            Dim Peticion As New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion With
                {
                    .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS,
                    .CodigoDelegacion = InformacionUsuario.CodigoDelegacion,
                    .Parametros = New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() From
                                                {
                                                    New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_URL_SERVICIO_INTEGRACION_SOL},
                                                    New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_DELEGACION_INTEGRACION_SALDOS_URL}
                                                }
                }

            Dim objProxy As New Prosegur.Genesis.Comunicacion.ProxyIacIntegracion
            Dim Respuesta As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = objProxy.GetParametrosDelegacionPais(Peticion)
            If Respuesta.CodigoError > Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Excepcion.NegocioExcepcion(Respuesta.CodigoError, Respuesta.MensajeError)
            End If

            If Respuesta.Parametros IsNot Nothing AndAlso Respuesta.Parametros.Count > 0 Then

                ParametrosIAC.UrlServicioIntegracionSol = (From vp In Respuesta.Parametros
                                                           Where vp.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_URL_SERVICIO_INTEGRACION_SOL
                                                           Select vp.ValorParametro).FirstOrDefault

                Prosegur.Genesis.Parametros.Parametros.IntegracionNuevoSaldosUrlWeb = (From vp In Respuesta.Parametros
                                                                                       Where vp.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_URL_SERVICIO_INTEGRACION_SOL
                                                                                       Select vp.ValorParametro).FirstOrDefault
            End If

        End Sub

        Private Shared Sub RecuperarParametrosSaldos()

            Dim Peticion As New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion With
                {
                    .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS,
                    .CodigoDelegacion = InformacionUsuario.CodigoDelegacion,
                    .Parametros = New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() From
                                                {
                                                    New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO}
                                                }
                }
            Dim objProxy As New ProxyIacIntegracion
            Dim Respuesta As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = objProxy.GetParametrosDelegacionPais(Peticion)
            If Respuesta.CodigoError > Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Excepcion.NegocioExcepcion(Respuesta.CodigoError, Respuesta.MensajeError)
            End If

            If Respuesta.Parametros IsNot Nothing AndAlso Respuesta.Parametros.Count > 0 Then
                ParametrosIAC.CrearConfiguiracionNivelSaldo = Respuesta.Parametros.FirstOrDefault.ValorParametro
            End If

        End Sub

        Public Shared Sub RecuperarDivisas()

            SyncLock _Divisas

                Dim Respuesta As ContractoServicio.Contractos.Comon.Divisa.ObtenerDivisas.ObtenerDivisasRespuesta = Comunicacion.ClienteServicio.Comon.ObtenerDivisas()
                _Divisas = Respuesta.ListaDivisas

            End SyncLock
        End Sub

        Public Shared Sub RecuperarPuestos()
            SyncLock _Puestos

                Dim Peticion As New ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Peticion

                Peticion.BolVigente = True
                Peticion.CodigoDelegacion = _InformacionUsuario.CodigoDelegacion
                Peticion.Aplicacion = Comon.Enumeradores.Aplicacion.GenesisSalidas

                Dim Respuesta As ContractoServicio.Contractos.Genesis.Puesto.ObtenerPuestos.Respuesta = Comunicacion.ClienteServicio.Comon.ObtenerPuestos(Peticion)

                _Puestos = Respuesta.Puestos

            End SyncLock
        End Sub

        Public Shared Sub RecuperarCalidades()
            SyncLock _Calidades

                Dim Respuesta As ContractoServicio.Contractos.Comon.Calidad.ObtenerCalidades.ObtenerCalidadesRespuesta = Comunicacion.ClienteServicio.Comon.ObtenerCalidades
                _Calidades = Respuesta.ListaCalidades

            End SyncLock
        End Sub

        Public Shared Sub RecuperarUnidadesMedida()
            SyncLock _UnidadesMedida

                Dim Respuesta As ContractoServicio.Contractos.Comon.UnidadMedida.ObtenerUnidadesMedidaRespuesta = Comunicacion.ClienteServicio.Comon.ObtenerUnidadesMedida
                _UnidadesMedida = Respuesta.UnidadesMedida

            End SyncLock
        End Sub

        Public Shared Sub RecuperarTiposBulto()

            SyncLock _TiposBulto

                Dim peticion As New ContractoServicio.NuevoSalidas.TipoBulto.ObtenerTiposBulto.Peticion With
                    {
                        .CodigoDelegacion = _InformacionUsuario.CodigoDelegacion
                    }

                Dim Respuesta As ContractoServicio.NuevoSalidas.TipoBulto.ObtenerTiposBulto.Respuesta = Comunicacion.ClienteServicio.NuevoSalidas.ObtenerTiposBulto(peticion)

                _TiposBulto = Respuesta.TiposBulto

            End SyncLock

        End Sub

        Public Shared Sub RecuperarEmisores()
            SyncLock _Emisores

                Dim peticion As New ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoPeticion _
                            With
                                {
                                    .EmisorDocumento = New Comon.Clases.EmisorDocumento
                                }

                Dim respuesta As ContractoServicio.EmisorDocumento.ObtenerEmisoresDocumento.ObtenerEmisoresDocumentoRespuesta = Comunicacion.ClienteServicio.Comon.ObtenerEmisoresDocumento(peticion)
                _Emisores = respuesta.ListaEmisores

            End SyncLock
        End Sub

        Public Shared Sub RecuperarTiposMercancia()

            SyncLock _TiposMercancia

                Dim respuesta As ContractoServicio.Contractos.NuevoSalidas.TiposMercancia.ObtenerTiposMercancia.Respuesta =
                Prosegur.Genesis.Comunicacion.ClienteServicio.NuevoSalidas.ObtenerTiposMercancia()

                _TiposMercancia = respuesta.TiposMercancia

            End SyncLock

        End Sub

        Public Shared Sub RecuperarSectores()

            SyncLock _Sectores

                Dim Peticion As New ContractoServicio.Contractos.Comon.Sector.RecuperarSectoresSalidasPeticion With
                    {
                       .CodigoDelegacion = _InformacionUsuario.CodigoDelegacion,
                       .CodigoPlanta = _InformacionUsuario.CodigoPlanta
                    }

                Dim Respuesta As ContractoServicio.Contractos.Comon.Sector.RecuperarSectoresSalidasRespuesta = Comunicacion.ClienteServicio.Comon.RecuperarSectoresSalidas(Peticion)
                _Sectores = Respuesta.Sectores

            End SyncLock

        End Sub

        ' utilizado para garantir o acesso a objetos entre threads diferentes sem causar um erro de acesso a threads cruzadas
        Private Shared ReadOnly LockObject As Object = New Object()

        Private Shared Sub GravarArquivo(str As String)

            ' verifica se o parâmetro GENERAR_ARCHIVO_LOG foi informado corretamente
            'If Configuration.ConfigurationManager.AppSettings("GENERAR_ARCHIVO_LOG") IsNot Nothing AndAlso Configuration.ConfigurationManager.AppSettings("GENERAR_ARCHIVO_LOG") = "1" Then

            ' garante o acesso em threads paralelas
            SyncLock LockObject
                ' grava o log em disco
                Dim fil As New StreamWriter("Log_Salidas_CargarParametros.txt", True)
                fil.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & "-" & str)
                fil.Close()
            End SyncLock

            'End If

        End Sub

    End Class

End Namespace