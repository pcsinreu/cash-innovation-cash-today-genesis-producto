
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class Login
#Region "ObtenerInformacionLogin"
    Public Shared Function ObtenerInformacionLogin(peticion As ContractoServicio.Login.ObtenerInformacionLogin.Peticion) As ContractoServicio.Login.EjecutarLogin.Respuesta

        Dim respuesta As ContractoServicio.Login.EjecutarLogin.Respuesta

        Try
            Dim spw As SPWrapper = armarWrapperObtenerInformacionLogin(peticion)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaObtenerInformacionLogin(ds)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta
    End Function
    Private Shared Function armarWrapperObtenerInformacionLogin(unaPeticion As ContractoServicio.Login.ObtenerInformacionLogin.Peticion) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.srecuperar_usuario_login", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$desLogin", ProsegurDbType.Descricao_Curta, unaPeticion.DesLogin, , False)
        spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, unaPeticion.CodigoPais, , False)
        spw.AgregarParam("par$rc_datos_login", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")

        Return spw

    End Function
    Private Shared Function PoblarRespuestaObtenerInformacionLogin(ds As DataSet) As ContractoServicio.Login.EjecutarLogin.Respuesta
        'Inicializo el objeto de respuesta
        Dim objRespuesta As New ContractoServicio.Login.EjecutarLogin.Respuesta With {
            .Usuario = New ContractoServicio.Login.EjecutarLogin.Usuario,
            .Aplicaciones = New ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion,
            .ResultadoOperacion = ContractoServicio.Login.EjecutarLogin.ResultadoOperacionLoginLocal.Autenticado
        }

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("datos") AndAlso ds.Tables("datos").Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables("datos")

                'Almaceno los valores del usuario que deberían ser los mismos en todas las filas
                objRespuesta.Usuario.Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_USUARIO"), GetType(String))
                objRespuesta.Usuario.OidUsuario = Util.AtribuirValorObj(dt.Rows(0)("OID_USUARIO"), GetType(String))
                objRespuesta.Usuario.Apellido = Util.AtribuirValorObj(dt.Rows(0)("DES_APELLIDO"), GetType(String))
                objRespuesta.Usuario.Nombre = Util.AtribuirValorObj(dt.Rows(0)("DES_NOMBRE"), GetType(String))
                objRespuesta.Usuario.Login = Util.AtribuirValorObj(dt.Rows(0)("DES_LOGIN"), GetType(String))
                objRespuesta.Usuario.Idioma = Util.AtribuirValorObj(dt.Rows(0)("DES_IDIOMA_PREFERIDO"), GetType(String))

                'Creo un continente generico para almacenar el Pais que debería ser el mismo en todas las filas
                'Al crear el pais se almacenan los datos de la delegación seleccionada como default
                'Al crear la delegación se agrega un Sector generico para almacenar los roles y permisos
                objRespuesta.Usuario.Continentes = New List(Of ContractoServicio.Login.EjecutarLogin.Continente) From {
                    New ContractoServicio.Login.EjecutarLogin.Continente With {
                        .Nombre = "GLOBAL",
                        .Paises = New List(Of ContractoServicio.Login.EjecutarLogin.Pais) From {
                            New ContractoServicio.Login.EjecutarLogin.Pais With {
                                .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String)),
                                .Nombre = Util.AtribuirValorObj(dt.Rows(0)("DES_PAIS"), GetType(String)),
                                .Delegaciones = New List(Of ContractoServicio.Login.EjecutarLogin.Delegacion) From {
                                    New ContractoServicio.Login.EjecutarLogin.DelegacionPlanta With {
                                        .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_DELEGACION"), GetType(String)),
                                        .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_DELEGACION"), GetType(String)),
                                        .Nombre = Util.AtribuirValorObj(dt.Rows(0)("DES_DELEGACION"), GetType(String)),
                                        .GMT = Util.AtribuirValorObj(dt.Rows(0)("NEC_GMT_MINUTOS"), GetType(Short)),
                                        .VeranoFechaHoraIni = Util.AtribuirValorObj(dt.Rows(0)("FYH_VERANO_INICIO"), GetType(Date)),
                                        .VeranoFechaHoraFin = Util.AtribuirValorObj(dt.Rows(0)("FYH_VERANO_FIN"), GetType(Date)),
                                        .VeranoAjuste = Util.AtribuirValorObj(dt.Rows(0)("NEC_VERANO_AJUSTE"), GetType(Short)),
                                        .Zona = Util.AtribuirValorObj(dt.Rows(0)("DES_ZONA"), GetType(String)),
                                        .Sectores = New List(Of ContractoServicio.Login.EjecutarLogin.Sector) From {
                                            New ContractoServicio.Login.EjecutarLogin.Sector With {
                                                .Codigo = "GLOBAL"
                                            }
                                        },
                                        .Plantas = New List(Of ContractoServicio.Login.EjecutarLogin.Planta) From {
                                            New ContractoServicio.Login.EjecutarLogin.Planta With {
                                                .oidPlanta = Util.AtribuirValorObj(dt.Rows(0)("OID_PLANTA"), GetType(String)),
                                                .CodigoPlanta = Util.AtribuirValorObj(dt.Rows(0)("COD_PLANTA"), GetType(String)),
                                                .DesPlanta = Util.AtribuirValorObj(dt.Rows(0)("DES_PLANTA"), GetType(String))
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Dim sector = objRespuesta.Usuario.Continentes.First().Paises.First().Delegaciones.First().Sectores().First()

                For Each fila As DataRow In dt.Rows

                    Dim rolActual = Util.AtribuirValorObj(fila("COD_ROLE"), GetType(String))
                    Dim permisoActual = Util.AtribuirValorObj(fila("COD_PERMISO"), GetType(String))
                    Dim aplicacionActual = Util.AtribuirValorObj(fila("COD_APLICACION"), GetType(String))

                    'Busco si el rol ya se encuentra en la lista
                    Dim rolEncontrado = sector.Roles.FirstOrDefault(Function(x) x.Nombre = rolActual)


                    'En caso de no encontrarse lo creo y lo almaceno en la lista de roles del sector
                    If rolEncontrado Is Nothing Then
                        sector.Roles.Add(
                            New ContractoServicio.Login.EjecutarLogin.Role With {
                                .Nombre = rolActual,
                                .Timeout = "10"
                            })
                    End If

                    'Busco si el permiso para la aplicación ya se encuentra en la lista
                    Dim permisoEncontrado = sector.Permisos.FirstOrDefault(
                            Function(x) x.CodigoAplicacion = aplicacionActual AndAlso x.Nombre = permisoActual)

                    'En caso de no encontrarse lo creo y lo almaceno en la lista de permisos del sector
                    If permisoEncontrado Is Nothing Then
                        sector.Permisos.Add(
                            New ContractoServicio.Login.EjecutarLogin.Permiso With {
                                .Nombre = permisoActual,
                                .CodigoAplicacion = aplicacionActual
                            })
                    End If


                    'Busco si la aplicación ya se encuentra en la lista
                    Dim aplicacionEncontrada = objRespuesta.Aplicaciones.FirstOrDefault(
                            Function(x) x.CodigoAplicacion = aplicacionActual)

                    'En caso de no encontrarse la creo y lo almaceno en la lista de aplicaciones
                    'Se obtiene la versión del GlobalAssembly de dos maneras distintas a modo de ejemplo
                    If aplicacionEncontrada Is Nothing Then
                        Dim aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion = New ContractoServicio.Login.EjecutarLogin.AplicacionVersion With {
                                .OidAplicacion = Util.AtribuirValorObj(fila("OID_APLICACION"), GetType(String)),
                                .CodigoAplicacion = aplicacionActual,
                                .DescripcionAplicacion = Util.AtribuirValorObj(fila("DES_APLICACION"), GetType(String)),
                                .CodigoVersion = $"{Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major}.{Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor}",
                                .CodigoBuild = Prosegur.Genesis.Comon.Util.VersionPunto.ToString()
                            }
                        aplicacionVersion.DesURLSitio = $"{Util.AtribuirValorObj(fila("URL_APLICACION"), GetType(String))}.{ aplicacionVersion.CodigoVersion}/"
                        aplicacionVersion.DesURLServicio = $"{Util.AtribuirValorObj(fila("URL_SERVICIO"), GetType(String))}.{ aplicacionVersion.CodigoVersion}/"
                        objRespuesta.Aplicaciones.Add(
                            aplicacionVersion)
                    End If
                Next
            End If
        End If

        Return objRespuesta
    End Function
#End Region

#Region "CrearTokenAcceso"
    Public Shared Function CrearTokenAcceso(OidAplicacion As String, OidUsuario As String, PermisosSerializados As String, ConfiguracionesSerializados As String) As ContractoServicio.Login.CrearTokenAcceso.Respuesta
        Dim respuesta As New ContractoServicio.Login.CrearTokenAcceso.Respuesta

        Try
            'Generamos un nuevo Id para el Token
            Dim oidTokenAcceso As String = Guid.NewGuid.ToString()

            Dim spw As SPWrapper = armarWrapperCrearTokenAcceso(oidTokenAcceso, OidAplicacion, OidUsuario, PermisosSerializados, ConfiguracionesSerializados)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            ' e retornamos o oid do token
            respuesta.TokenAcceso = New ContractoServicio.Login.CrearTokenAcceso.TokenAcceso() With {.OidTokenAcceso = oidTokenAcceso}
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT

        Catch ex As Exception
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.Message
        End Try

        Return respuesta
    End Function
    Private Shared Function armarWrapperCrearTokenAcceso(oidTokenAcceso As String, oidAplicacion As String, oidUsuario As String, permisosSerializados As String, configuracionesSerializados As String) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.sgrabar_token_acceso", Prosegur.Genesis.Comon.Util.Version)
        'Indicamos True como segundo parametro para utilizar el ExecuteNonQuery
        Dim spw As New SPWrapper(SP, True)
        spw.AgregarParam("par$oid_token_acceso", ProsegurDbType.Identificador_Alfanumerico, oidTokenAcceso, , False)
        spw.AgregarParam("par$oid_usuario", ProsegurDbType.Identificador_Alfanumerico, oidUsuario)
        spw.AgregarParam("par$oid_aplicacion", ProsegurDbType.Identificador_Alfanumerico, oidAplicacion)
        spw.AgregarParam("par$fyh_token_acceso", ProsegurDbType.Data_Hora, DateTime.Now)
        spw.AgregarParam("par$des_permisos_acceso", ProsegurDbType.Binario, Text.Encoding.UTF8.GetBytes(permisosSerializados))
        spw.AgregarParam("par$des_configuraciones", ProsegurDbType.Binario, Text.Encoding.UTF8.GetBytes(configuracionesSerializados))

        Return spw
    End Function
#End Region
#Region "ObtenerToken"
    Public Shared Function ObtenerTokenAcceso(peticion As ContractoServicio.Login.ObtenerTokenAcceso.Peticion) As ContractoServicio.Login.ObtenerTokenAcceso.Respuesta

        Dim respuesta As ContractoServicio.Login.ObtenerTokenAcceso.Respuesta

        Try
            Dim spw As SPWrapper = armarWrapperObtenerTokenAcceso(peticion)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta = PoblarRespuestaObtenerTokenAcceso(ds)

        Catch ex As Exception
            Throw ex
        End Try
        Return respuesta
    End Function

    Private Shared Function armarWrapperObtenerTokenAcceso(unaPeticion As ContractoServicio.Login.ObtenerTokenAcceso.Peticion) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.sobtener_token_acceso", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_token_acceso", ProsegurDbType.Descricao_Curta, unaPeticion.OidTokenAcceso)
        spw.AgregarParam("par$rc_datos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")

        Return spw

    End Function
    Private Shared Function PoblarRespuestaObtenerTokenAcceso(ds As DataSet) As ContractoServicio.Login.ObtenerTokenAcceso.Respuesta
        'Inicializo el objeto de respuesta
        Dim objRespuesta = New ContractoServicio.Login.ObtenerTokenAcceso.Respuesta With {
            .Token = New ContractoServicio.Login.ObtenerTokenAcceso.TokenAcceso
        }

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
            If ds.Tables.Contains("datos") AndAlso ds.Tables("datos").Rows.Count > 0 Then
                Dim dt As DataTable = ds.Tables("datos")
                objRespuesta.Token.OidTokenAcceso = Util.AtribuirValorObj(dt.Rows(0)("OID_TOKEN_ACCESO"), GetType(String))
                objRespuesta.Token.OidUsuario = Util.AtribuirValorObj(dt.Rows(0)("OID_USUARIO"), GetType(String))
                objRespuesta.Token.OidAplicacion = Util.AtribuirValorObj(dt.Rows(0)("OID_APLICACION"), GetType(String))
                objRespuesta.Token.Fecha = Util.AtribuirValorObj(dt.Rows(0)("FYH_TOKEN_ACCESO"), GetType(String))
                objRespuesta.Token.PermisosSerializado = Util.AtribuirValorObj(Text.Encoding.UTF8.GetString(dt.Rows(0)("DES_PERMISOS_ACCESO")), GetType(String))
                objRespuesta.Token.ConfiguracionesSerializado = Util.AtribuirValorObj(Text.Encoding.UTF8.GetString(dt.Rows(0)("DES_CONFIGURACIONES")), GetType(String))
            End If
        End If

        Return objRespuesta
    End Function
#End Region

#Region "BorrarTokenAcceso"
    Public Shared Function BorrarTokenAcceso(unaPeticion As ContractoServicio.Login.BorrarTokenAcceso.Peticion) As ContractoServicio.RespuestaGenerico
        Dim respuesta As New ContractoServicio.RespuestaGenerico

        Try
            'Generamos un nuevo Id para el Token
            Dim oidTokenAcceso As String = Guid.NewGuid.ToString()

            Dim spw As SPWrapper = armarWrapperBorrarTokenAcceso(unaPeticion)
            Dim ds As DataSet = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT

        Catch ex As Exception
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.Message
        End Try

        Return respuesta
    End Function

    Private Shared Function armarWrapperBorrarTokenAcceso(unaPeticion As ContractoServicio.Login.BorrarTokenAcceso.Peticion) As SPWrapper
        Dim SP As String = String.Format("SAPR_PUSUARIO_{0}.sborrar_token_acceso", Prosegur.Genesis.Comon.Util.Version)
        'Indicamos True como segundo parametro para utilizar el ExecuteNonQuery
        Dim spw As New SPWrapper(SP, True)

        spw.AgregarParam("par$oid_token_acceso", ProsegurDbType.Identificador_Alfanumerico, unaPeticion.OidTokenAcceso)

        Return spw
    End Function
#End Region
End Class
