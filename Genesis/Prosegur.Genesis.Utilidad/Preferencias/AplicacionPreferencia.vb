Imports System.Runtime.Caching
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports System.Configuration

Namespace Preferencias

    ''' <summary>
    ''' Representa la aplicación en el agrupamiento de las preferencias.
    ''' </summary>
    Public Class AplicacionPreferencia

        ''' <summary>
        ''' Código de la aplicación.
        ''' </summary>
        Private ReadOnly _codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion

        ''' <summary>
        ''' Código del usuário.
        ''' </summary>
        Private ReadOnly _codigoUsuario As String

        ''' <summary>
        ''' Código del Pais.
        ''' </summary>
        Private ReadOnly _codigoPais As String

        ''' <summary>
        ''' Url do servicio Genesis.
        ''' </summary>
        Private ReadOnly _urlServicioGenesis As String

        Public ReadOnly Property CodigoAplicacion() As String
            Get
                Return _codigoAplicacion
            End Get
        End Property

        Public ReadOnly Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
        End Property

        Public ReadOnly Property CodigoPais() As String
            Get
                Return _codigoPais
            End Get
        End Property

        Public ReadOnly Property URLServicioGenesis() As String
            Get
                Return _urlServicioGenesis
            End Get
        End Property

        ''' <summary>
        ''' Almacena la lista de todas las características de esta aplicación con las preferencias ya grabadas.
        ''' </summary>
        Private _funcionalidades As New List(Of FuncionalidadAplicacion)

        ''' <summary>
        ''' Tempo em minutos para expirar o cache - Default 1 dia (1440 minutos)
        ''' </summary>
        Private ReadOnly Property ExpiraCachePreferencias() As Integer
            Get
                Dim _expCache As Integer = 1440
                Dim _value = ConfigurationManager.AppSettings("ExpiraCachePreferencias")
                If (_value IsNot Nothing) Then
                    Integer.TryParse(_value.ToString(), _expCache)
                End If
                Return _expCache
            End Get
        End Property


        ''' <summary>
        ''' Constructor de la clase <see cref="AplicacionPreferencia"/>.
        ''' </summary>
        ''' <param name="codigoAplicacion">Código de la aplicación</param>
        ''' <param name="codigoUsuario">Código del usuário</param>
        ''' <remarks>Accesible sólo dentro del mismo assembly.</remarks>
        Public Sub New(codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion, codigoUsuario As String)
            _codigoAplicacion = codigoAplicacion
            _codigoUsuario = codigoUsuario
        End Sub

        Public Sub New(codigoPais As String, codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion, codigoUsuario As String, urlServicioGenesis As String)
            _codigoAplicacion = codigoAplicacion
            _codigoUsuario = codigoUsuario
            _urlServicioGenesis = urlServicioGenesis
            _codigoPais = codigoPais
        End Sub

        ''' <summary>
        ''' Realiza una búsqueda de una funcionalidad determinada.
        ''' </summary>
        ''' <param name="codigo">Código de la funcionalidad</param>
        ''' <returns>Objecto de la funcionalidad</returns>
        Public Function Funcionalidad(codigo As String) As FuncionalidadAplicacion

            Dim objectoFuncionalidad As FuncionalidadAplicacion

            ' La primera opción es hacer una búsqueda en la variable que contiene la lista de funcionalidades ya cargada
            objectoFuncionalidad = _funcionalidades.SingleOrDefault(Function(x) x.Codigo = codigo)

            If objectoFuncionalidad Is Nothing Then
                ' Si la función no está en la lista de funcionalidades ya cargadas, busco en el cache o base de datos
                objectoFuncionalidad = BuscarFuncionalidad(codigo)
                _funcionalidades.Add(objectoFuncionalidad)

            End If

            Return objectoFuncionalidad
        End Function

        ''' <summary>
        ''' Centraliza la búsqueda de la funcionalidad.
        ''' </summary>
        ''' <param name="codigo">Código de la funcionalidad</param>
        ''' <returns>Objecto de la funcionalidad</returns>
        Private Function BuscarFuncionalidad(codigo As String) As FuncionalidadAplicacion
            Dim objectoFuncionalidad As FuncionalidadAplicacion

            ' Primero busco en cache
            objectoFuncionalidad = BuscarFuncionalidadEnCache(codigo)

            If objectoFuncionalidad Is Nothing Then

                ' No ha encontrado en cache, por lo tanto busco en la base de datos
                objectoFuncionalidad = BuscarFuncionalidadEnLaBaseDeDatos(codigo)

                If objectoFuncionalidad Is Nothing Then
                    ' Si no hay datos en cache y en la base de datos, creamos un objeto vacío
                    objectoFuncionalidad = New FuncionalidadAplicacion(_codigoPais, _codigoAplicacion, _codigoUsuario, codigo)
                End If

                ' Añande el valor al cache
                GravarFuncionalidadEnCache(objectoFuncionalidad)
                objectoFuncionalidad = BuscarFuncionalidadEnCache(codigo)
            End If

            Return objectoFuncionalidad
        End Function

        ''' <summary>
        ''' Realiza una búsqueda de una funcionalidad en cache.
        ''' </summary>
        ''' <param name="codigo">Código de la funcionalidad</param>
        ''' <returns>Objecto de la funcionalidad</returns>
        Private Function BuscarFuncionalidadEnCache(codigo As String) As FuncionalidadAplicacion
            Dim cache As ObjectCache = MemoryCache.Default
            Dim objectoFuncionalidad As FuncionalidadAplicacion = DirectCast(cache.Get(
                    String.Format(Preferencias.PREFERENCIAS_CACHE_KEY, _codigoPais, _codigoUsuario, _codigoAplicacion, codigo)), FuncionalidadAplicacion)

            Return objectoFuncionalidad
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="funcionalidad"></param>
        ''' <remarks></remarks>
        Private Sub GravarFuncionalidadEnCache(funcionalidad As FuncionalidadAplicacion)

            Dim cache As ObjectCache = MemoryCache.Default
            Dim cacheKey As String = String.Format(Preferencias.PREFERENCIAS_CACHE_KEY, _codigoPais, _codigoUsuario, _codigoAplicacion, funcionalidad.Codigo)

            If (cache.Contains(cacheKey)) Then
                cache.Remove(cacheKey)
            End If

            'Quando o cache expirar será gravado na base de dados
            Dim policy = New CacheItemPolicy() With { _
                .RemovedCallback = New CacheEntryRemovedCallback(AddressOf CachedItemRemovedCallback), _
                .AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Me.ExpiraCachePreferencias())
            }

            cache.Add(cacheKey, funcionalidad, policy)
        End Sub

        Public Sub LimparPreferencias()
            Dim cache As ObjectCache = MemoryCache.Default
            Dim listaCacheLimpar = cache.Where(Function(c) c.Key.StartsWith(String.Format(Preferencias.PREFERENCIAS_CACHE_KEY, _codigoPais, _codigoUsuario, _codigoAplicacion, String.Empty))).ToList()

            If (listaCacheLimpar IsNot Nothing) Then
                For Each cacheLimpar In listaCacheLimpar
                    Dim objectoFuncionalidad As FuncionalidadAplicacion = DirectCast(cache.Get(cacheLimpar.Key), FuncionalidadAplicacion)
                    objectoFuncionalidad.Propriedad().Clear()
                    objectoFuncionalidad.Componente().Clear()
                Next
            End If

            Dim urlServicioGenesis As String = cache.Get(_codigoPais)
            Dim proxy As New ProxyPreferencias(urlServicioGenesis)
            Dim peticion As New BorrarPreferenciasAplicacionPeticion()
            peticion.CodigoAplicacion = _codigoAplicacion
            peticion.CodigoUsuario = _codigoUsuario
            proxy.BorrarPreferenciasAplicacion(peticion)
        End Sub


        Private Shared Sub CachedItemRemovedCallback(arguments As CacheEntryRemovedArguments)
            'se o cache expirou
            If arguments.RemovedReason = CacheEntryRemovedReason.Expired Then
                Dim funcionalidade = DirectCast(arguments.CacheItem.Value, FuncionalidadAplicacion)

                If funcionalidade IsNot Nothing Then
                    Dim cache As ObjectCache = MemoryCache.Default
                    Dim urlServicioGenesis As String = cache.Get(funcionalidade.CodigoPais)
                    'Verifica se existe uma url do genesis no cache para o pais

                    Dim proxy As New ProxyPreferencias(urlServicioGenesis)
                    Dim peticion As New GuardarPreferenciasPeticion()
                    peticion.Preferencias = New ObjectModel.ObservableCollection(Of PreferenciaUsuario)

                    For Each codigoComponente As String In funcionalidade.Componente().Keys
                        Dim comp = funcionalidade.Componente(codigoComponente)

                        For Each codigoPropriedad As String In comp.Propriedad().Keys
                            Dim prop = comp.Propriedad(codigoPropriedad)

                            Dim preferencia As New PreferenciaUsuario() With { _
                                .CodigoAplicacion = funcionalidade.CodigoAplicacion,
                                .CodigoUsuario = funcionalidade.CodigoUsuario,
                                .CodigoFuncionalidad = funcionalidade.Codigo,
                                .CodigoComponente = codigoComponente,
                                .CodigoPropriedad = codigoPropriedad,
                                .Valor = prop.Valor,
                                .TipoValorBinario = prop.Binario.Tipo,
                                .ValorBinario = prop.Binario.Valor
                            }

                            peticion.Preferencias.Add(preferencia)
                        Next
                    Next

                    For Each codigoPropriedad As String In funcionalidade.Propriedad().Keys
                        Dim prop = funcionalidade.Propriedad(codigoPropriedad)

                        Dim preferencia As New PreferenciaUsuario() With { _
                            .CodigoAplicacion = funcionalidade.CodigoAplicacion,
                            .CodigoUsuario = funcionalidade.CodigoUsuario,
                            .CodigoFuncionalidad = funcionalidade.Codigo,
                            .CodigoPropriedad = codigoPropriedad,
                            .Valor = prop.Valor,
                            .TipoValorBinario = prop.Binario.Tipo,
                            .ValorBinario = prop.Binario.Valor
                        }

                        peticion.Preferencias.Add(preferencia)
                    Next

                    If peticion.Preferencias.Count > 0 Then
                        proxy.GuardarPreferencias(peticion)
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Realiza una búsqueda de una funcionalidad en la base de datos.
        ''' </summary>
        ''' <param name="codigo">Código de la funcionalidad</param>
        ''' <returns>Objecto de la funcionalidad</returns>
        Private Function BuscarFuncionalidadEnLaBaseDeDatos(codigo As String) As FuncionalidadAplicacion
            Dim proxy As New ProxyPreferencias(Me.URLServicioGenesis)
            Dim respuesta = proxy.ObtenerPreferencias(New ObtenerPreferenciasPeticion() _
                                                      With {
                                                          .CodigoFuncionalidad = codigo,
                                                          .CodigoUsuario = _codigoUsuario,
                                                          .codigoAplicacion = _codigoAplicacion})

            Dim funcionalidad As New FuncionalidadAplicacion(CodigoPais, _codigoAplicacion, _codigoUsuario, codigo)

            For Each pref As PreferenciaUsuario In respuesta.Preferencias

                If Not String.IsNullOrEmpty(pref.CodigoComponente) Then
                    Dim comp As ComponenteFuncionalidad = funcionalidad.Componente(pref.CodigoComponente)
                    Dim prop As PropriedadFuncionalidad = comp.Propriedad(pref.CodigoPropriedad)
                    prop.Valor = pref.Valor
                    prop.Binario.Tipo = pref.TipoValorBinario
                    prop.Binario.Valor = pref.ValorBinario

                ElseIf Not String.IsNullOrEmpty(pref.CodigoPropriedad) Then
                    Dim prop As PropriedadFuncionalidad = funcionalidad.Propriedad(pref.CodigoPropriedad)
                    prop.Valor = pref.Valor
                    prop.Binario.Tipo = pref.TipoValorBinario
                    prop.Binario.Valor = pref.ValorBinario
                End If
            Next

            Return funcionalidad
        End Function

        ''' <summary>
        ''' Devuelve todas las funcionalidades.
        ''' </summary>
        ''' <returns>Colección de funcionalidades</returns>
        Protected Friend Function Funcionalidad() As List(Of FuncionalidadAplicacion)
            Return _funcionalidades
        End Function
    End Class
End Namespace