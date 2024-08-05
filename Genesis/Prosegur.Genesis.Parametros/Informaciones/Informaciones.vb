Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro
Imports Prosegur.Genesis.ContractoServicio.Login
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.ImportarParametros

Public Module Informaciones

    Private _Usuario As Parametros_v2.Usuario
    Private _Aplicaciones As List(Of Parametros_v2.Aplicacion)
    Private _identificadorDelegacion As String
    Private _identificadorPlanta As String
    Private _identificadorSector As String
    Private _delegacion As Comon.Clases.Delegacion

    Public Property Delegacion As Comon.Clases.Delegacion
        Get
            Return _delegacion
        End Get
        Set(value As Comon.Clases.Delegacion)
            _delegacion = value
        End Set
    End Property

    Public Property IdentificadorDelegacion As String
        Get
            Return _identificadorDelegacion
        End Get
        Set(value As String)
            _identificadorDelegacion = value
        End Set
    End Property

    Public Property IdentificadorPlanta As String
        Get
            Return _identificadorPlanta
        End Get
        Set(value As String)
            _identificadorPlanta = value
        End Set
    End Property

    Public Property IdentificadorSector As String
        Get
            Return _identificadorSector
        End Get
        Set(value As String)
            _identificadorSector = value
        End Set
    End Property


    Public ReadOnly Property usuario As Parametros_v2.Usuario
        Get
            Return _Usuario
        End Get
    End Property

    Public ReadOnly Property codigoDelegacion() As String
        Get
            Return _Aplicaciones(0).delegacion.codigo
        End Get
    End Property

    Public ReadOnly Property descripcionDelegacion() As String
        Get
            Return _Aplicaciones(0).delegacion.descripcion
        End Get
    End Property

    Public ReadOnly Property codigoPais() As String
        Get
            Return _Aplicaciones(0).pais.codigo
        End Get
    End Property

    Public ReadOnly Property codigoPuesto() As String
        Get
            Return _Aplicaciones(0).puesto.codigo
        End Get
    End Property

    Public ReadOnly Property codigoPlanta() As String
        Get
            Return _Aplicaciones(0).planta.codigo
        End Get
    End Property

    Public Sub cargarInformaciones(aplicaciones As EjecutarLogin.AplicacionVersionColeccion,
                                   usuario As EjecutarLogin.Usuario,
                                   codigoAplicacion As String)


        Informaciones.cargarParametros(usuario.CodigoDelegacion,
                                       usuario.CodigoPais,
                                       usuario.CodigoPlanta,
                                       usuario.CodigoPuesto,
                                       codigoAplicacion,
                                       aplicaciones.FirstOrDefault(Function(x) x.CodigoAplicacion = codigoAplicacion).DesURLServicio)

#If DEBUG Then

        If Not String.IsNullOrEmpty(usuario.CodigoPuesto) Then

            Dim Codigos As String() = usuario.CodigoPuesto.Split("@")

            If Codigos.Count > 1 Then
                usuario.CodigoPuesto = Codigos(0)
            End If

        End If
#End If

        Informaciones.cargarAplicaciones(aplicaciones, codigoAplicacion)

        Informaciones.cargarUsuario(usuario)

    End Sub

    Private Sub cargarParametros(codigoDelegacion As String,
                                codigoPais As String,
                                codigoPlanta As String,
                                codigoPuesto As String,
                                codigoAplicacion As String,
                                URLServicioDeLaVersao As String,
                       Optional obtenerParametrosGenesis As Boolean = False,
                       Optional obtenerParametrosReportes As Boolean = False)

        Dim peticion As New obtenerParametros.Peticion
        peticion.codigoAplicacion = String.Empty
        peticion.codigoDelegacion = codigoDelegacion
        peticion.codigoPais = codigoPais
        peticion.obtenerParametrosGenesis = obtenerParametrosGenesis
        peticion.obtenerParametrosReportes = obtenerParametrosReportes
        peticion.codigoPuesto = codigoPuesto
        peticion.codigoDelegacion = codigoDelegacion
        peticion.codigoHostPuesto = My.Computer.Name

#If DEBUG Then

        If Not String.IsNullOrEmpty(codigoPuesto) Then

            Dim Codigos As String() = codigoPuesto.Split("@")

            If Codigos.Count > 1 Then
                peticion.codigoHostPuesto = Codigos(1)
                peticion.codigoPuesto = Codigos(0)
            End If

        End If
#End If



        Dim proxyIacIntegracion As New Prosegur.Genesis.Comunicacion.ProxyIacIntegracion(URLServicioDeLaVersao)
        Dim respuesta As obtenerParametros.Respuesta = proxyIacIntegracion.obtenerParametros(peticion)


        If respuesta IsNot Nothing AndAlso respuesta.codigoResultado <> Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO Then
            Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, respuesta.descripcionResultado)
        ElseIf respuesta.parametros Is Nothing OrElse respuesta.parametros.Count = 0 OrElse _
            respuesta.parametros.FirstOrDefault(Function(x) x.codigoAplicacion = codigoAplicacion) Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, String.Format(Traduzir("NoFuePosibleCargarParametrosAplicacion"), codigoAplicacion))
        End If

        For Each _parametro As Comon.Clases.Parametro In respuesta.parametros

            If _Aplicaciones Is Nothing Then _Aplicaciones = New List(Of Parametros_v2.Aplicacion)

            Dim aplicacion As Parametros_v2.Aplicacion = Nothing
            aplicacion = _Aplicaciones.FirstOrDefault(Function(p) p.codigo = _parametro.codigoAplicacion)

            If aplicacion Is Nothing Then
                aplicacion = New Parametros_v2.Aplicacion With {
                    .codigo = _parametro.codigoAplicacion,
                    .delegacion = New Parametros_v2.Delegacion With {
                            .codigo = _parametro.codigoDelegacion
                        },
                    .pais = New Parametros_v2.Pais With {
                            .codigo = _parametro.codigoPais
                        }
                    }
                If Not String.IsNullOrEmpty(_parametro.codigoPuesto) Then
                    aplicacion.puesto = New Parametros_v2.Puesto With {
                            .codigo = _parametro.codigoPuesto,
                            .host = _parametro.codigoHostPuesto
                        }
                End If
                If Not String.IsNullOrEmpty(codigoPlanta) Then
                    aplicacion.planta = New Parametros_v2.Planta With {
                            .codigo = codigoPlanta
                        }
                End If
                _Aplicaciones.Add(aplicacion)
            End If

            If aplicacion IsNot Nothing Then

                Dim valor As New Object
                Select Case _parametro.tipoParametro

                    Case 3
                        '3 - CHECKBOX
                        Dim valorBoolean As Boolean = False

                        If Not String.IsNullOrEmpty(_parametro.valorParametro) AndAlso _parametro.valorParametro = "1" Then
                            valorBoolean = True
                        End If

                        valor = valorBoolean
                    Case Else
                        Dim valorString As String = _parametro.valorParametro
                        valor = valorString
                End Select

                Select Case _parametro.nivelParametro
                    Case 1
                        '1	Pais
                        If aplicacion.pais.parametros Is Nothing Then aplicacion.pais.parametros = New Dictionary(Of String, Object)
                        aplicacion.pais.parametros.Add(_parametro.codigoParametro.ToUpper, valor)

                    Case 2
                        '2	Delegacion
                        If aplicacion.delegacion.parametros Is Nothing Then aplicacion.delegacion.parametros = New Dictionary(Of String, Object)
                        aplicacion.delegacion.parametros.Add(_parametro.codigoParametro.ToUpper, valor)

                    Case 3
                        '3	Puesto

                        If aplicacion.puesto Is Nothing Then
                            aplicacion.puesto = New Parametros_v2.Puesto With {
                                .codigo = _parametro.codigoPuesto,
                                .host = _parametro.codigoHostPuesto
                            }
                        End If

                        If aplicacion.puesto.parametros Is Nothing Then aplicacion.puesto.parametros = New Dictionary(Of String, Object)
                        aplicacion.puesto.parametros.Add(_parametro.codigoParametro.ToUpper, valor)

                End Select
            End If
        Next

        If _Aplicaciones IsNot Nothing AndAlso _Aplicaciones.Count > 0 Then

            For Each _Aplicacion In _Aplicaciones

                If _Aplicacion.puesto Is Nothing Then
                    _Aplicacion.puesto = New Parametros_v2.Puesto With {
                            .codigo = codigoPuesto,
                            .host = My.Computer.Name
                        }
                End If

            Next

        End If

    End Sub

    Private Sub cargarAplicaciones(aplicaciones As Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion, codigoAplicacionLogada As String)

        If aplicaciones IsNot Nothing AndAlso aplicaciones.Count > 0 Then

            For Each _aplicacion In aplicaciones

                Dim aplicacion As Parametros_v2.Aplicacion = Nothing
                aplicacion = _Aplicaciones.FirstOrDefault(Function(p) p.codigo = _aplicacion.CodigoAplicacion)

                If aplicacion IsNot Nothing Then
                    With aplicacion
                        .codigo = _aplicacion.CodigoAplicacion
                        .codigoBuild = _aplicacion.CodigoBuild
                        .codigoVersion = _aplicacion.CodigoVersion
                        .descricionURLServicio = _aplicacion.DesURLServicio
                        .descricionURLSitio = _aplicacion.DesURLSitio
                        .descripcion = _aplicacion.DescripcionAplicacion
                        .identificador = _aplicacion.OidAplicacion
                    End With
                Else
                    aplicacion = New Parametros_v2.Aplicacion With {
                                        .codigo = _aplicacion.CodigoAplicacion,
                                        .codigoBuild = _aplicacion.CodigoBuild,
                                        .codigoVersion = _aplicacion.CodigoVersion,
                                        .descricionURLServicio = _aplicacion.DesURLServicio,
                                        .descricionURLSitio = _aplicacion.DesURLSitio,
                                        .descripcion = _aplicacion.DescripcionAplicacion,
                                        .identificador = _aplicacion.OidAplicacion
                                    }
                    _Aplicaciones.Add(aplicacion)
                End If

            Next

            'Coloca na primeira posição da lista a aplicação que o usuário estiver logado.
            'Para que a busca pelos parametros primeiro passe por essa aplicação.
            Dim appLogada = _Aplicaciones.Where(Function(ap) ap.codigo = codigoAplicacionLogada).FirstOrDefault
            If appLogada IsNot Nothing Then
                'Remove
                _Aplicaciones.Remove(appLogada)

                'Adiciona na primeira posição
                _Aplicaciones.Insert(0, appLogada)
            End If
        End If

    End Sub

    Private Sub cargarUsuario(usuario As EjecutarLogin.Usuario)

        _Usuario = New Parametros_v2.Usuario With {
                        .chave = usuario.Password,
                        .login = usuario.Login,
                        .nombre = usuario.Nombre,
                        .apellido = usuario.Apellido
                    }
        If (usuario.SectorLogado IsNot Nothing) Then
            _Usuario.Sector = New Parametros_v2.Sector
            _Usuario.Sector.Identificador = usuario.SectorLogado.Identificador
            _Usuario.Sector.Codigo = usuario.SectorLogado.Codigo
            _Usuario.Sector.Descripcion = usuario.SectorLogado.Descripcion
            IdentificadorDelegacion = usuario.SectorLogado.Delegacion.Identificador
            IdentificadorPlanta = usuario.SectorLogado.Planta.Identificador
            Delegacion = usuario.SectorLogado.Delegacion
        End If

        If usuario.Continentes IsNot Nothing AndAlso usuario.Continentes.Count > 0 Then
            For Each continente As EjecutarLogin.Continente In usuario.Continentes
                For Each pais As EjecutarLogin.Pais In continente.Paises
                    For Each delegacion As EjecutarLogin.DelegacionPlanta In pais.Delegaciones

                        For Each planta As EjecutarLogin.Planta In delegacion.Plantas

                            For Each sector As EjecutarLogin.TipoSector In planta.TiposSectores
                                For Each permiso As EjecutarLogin.Permiso In sector.Permisos
                                    Dim aplicacion As Parametros_v2.Aplicacion = Nothing
                                    aplicacion = _Aplicaciones.FirstOrDefault(Function(p) p.codigo = permiso.CodigoAplicacion)

                                    If aplicacion.delegacion IsNot Nothing AndAlso aplicacion.delegacion.codigo = delegacion.Codigo Then
                                        aplicacion.delegacion.descripcion = delegacion.Nombre

                                        If aplicacion.permisos Is Nothing Then aplicacion.permisos = New List(Of String)
                                        aplicacion.permisos.Add(permiso.Nombre)

                                        If aplicacion.planta IsNot Nothing Then
                                            aplicacion.planta.descripcion = planta.DesPlanta
                                        End If
                                    End If

                                Next
                            Next
                        Next
                    Next
                Next
            Next
        End If
    End Sub

    Public Function obtenerAplicacion(codigoAplicacion As String) As Parametros_v2.Aplicacion
        Dim _aplicacion As Parametros_v2.Aplicacion = Nothing

        If _Aplicaciones IsNot Nothing AndAlso _Aplicaciones.Count > 0 Then
            _aplicacion = _Aplicaciones.FirstOrDefault(Function(x) x.codigo = codigoAplicacion)
        End If

        Return _aplicacion
    End Function

    Public Function obtenerURLServicioDeLaVersao(codigoAplicacion As String) As String
        Dim url As String = String.Empty
        Dim aplicacion = Informaciones.obtenerAplicacion(codigoAplicacion)
        If aplicacion IsNot Nothing Then
            url = aplicacion.descricionURLServicio
        End If
        Return url
    End Function

    Public Function obtenerParametros(_codigoParametro As String,
                             Optional codigoAplicacion As String = "",
                             Optional codigoDelegacion As String = "",
                             Optional codigoPais As String = "",
                             Optional codigoPlanta As String = "",
                             Optional codigoPuesto As String = "") As Object

        Dim valor As Object = Nothing

        If String.IsNullOrEmpty(codigoAplicacion) Then
            For Each aplicacion In _Aplicaciones
                If aplicacion.puesto IsNot Nothing AndAlso aplicacion.puesto.parametros IsNot Nothing AndAlso aplicacion.puesto.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = aplicacion.puesto.parametros.Item(_codigoParametro.ToUpper)
                    'If valor IsNot Nothing Then 
                    Exit For
                End If
                If aplicacion.delegacion IsNot Nothing AndAlso aplicacion.delegacion.parametros IsNot Nothing AndAlso aplicacion.delegacion.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = aplicacion.delegacion.parametros.Item(_codigoParametro.ToUpper)
                    'If valor IsNot Nothing Then
                    Exit For
                End If
                If aplicacion.pais IsNot Nothing AndAlso aplicacion.pais.parametros IsNot Nothing AndAlso aplicacion.pais.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = aplicacion.pais.parametros.Item(_codigoParametro.ToUpper)
                    'If valor IsNot Nothing Then 
                    Exit For
                End If
            Next
        Else
            Dim _aplicacion As Parametros_v2.Aplicacion = _Aplicaciones.FirstOrDefault(Function(x) x.codigo = codigoAplicacion)
            If _aplicacion IsNot Nothing Then
                If _aplicacion.puesto IsNot Nothing AndAlso _aplicacion.puesto.parametros IsNot Nothing AndAlso _aplicacion.puesto.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = _aplicacion.puesto.parametros.Item(_codigoParametro.ToUpper)
                End If
                If _aplicacion.delegacion IsNot Nothing AndAlso _aplicacion.delegacion.parametros IsNot Nothing AndAlso _aplicacion.delegacion.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = _aplicacion.delegacion.parametros.Item(_codigoParametro.ToUpper)
                End If
                If _aplicacion.pais IsNot Nothing AndAlso _aplicacion.pais.parametros IsNot Nothing AndAlso _aplicacion.pais.parametros.ContainsKey(_codigoParametro.ToUpper) Then
                    valor = _aplicacion.pais.parametros.Item(_codigoParametro.ToUpper)
                End If
            End If
        End If

        Return valor
    End Function

    Public Function validarPermiso(codigoPermiso As String,
                          Optional codigoAplicacion As String = "") As Boolean

        If String.IsNullOrEmpty(codigoAplicacion) Then
            For Each _aplicacion In _Aplicaciones
                If _aplicacion.permisos IsNot Nothing AndAlso _aplicacion.permisos.Count > 0 AndAlso _aplicacion.permisos.Contains(codigoPermiso) Then
                    Return True
                End If
            Next
        Else
            Dim _aplicacion As Parametros_v2.Aplicacion = _Aplicaciones.FirstOrDefault(Function(x) x.codigo = codigoAplicacion)
            If _aplicacion IsNot Nothing AndAlso _aplicacion.permisos IsNot Nothing AndAlso _aplicacion.permisos.Count > 0 AndAlso _aplicacion.permisos.Contains(codigoPermiso) Then
                Return True
            End If
        End If

        Return False
    End Function

End Module
