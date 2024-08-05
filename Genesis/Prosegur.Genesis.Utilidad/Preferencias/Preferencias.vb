Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.Comunicacion
Imports System.Runtime.Caching

Namespace Preferencias
    ''' <summary>
    ''' Punto de entrada para la lectura y grabación de las preferencias del usuario.
    ''' </summary>
    Public Class Preferencias

        ''' <summary>
        ''' Definición del nombre de la llave en cache.
        ''' </summary>
        ''' <remarks>{0} Pais {1} = Usuário | {2} = Aplicación | {3} = Funcionalidad</remarks>
        Friend Const PREFERENCIAS_CACHE_KEY As String = "PREF_{0}_{1}_{2}_{3}"

        ''' <summary>
        ''' Código del país para la lectura y grabácion de las preferencias.
        ''' </summary>
        Private ReadOnly _codigoPais As String

        ''' <summary>
        ''' Código del usuário para la lectura y grabácion de las preferencias.
        ''' </summary>
        Private ReadOnly _codigoUsuario As String

        ''' <summary>
        ''' Url do servicio Genesis.
        ''' </summary>
        Private ReadOnly _urlServicioGenesis As String

        Private _aplicaciones As New List(Of AplicacionPreferencia)

        ''' <summary>
        ''' Constructor de la clase <see cref="Preferencias"/>.
        ''' </summary>
        ''' <param name="codigoUsuario">Código del usuário para la lectura y grabácion de las preferencias</param>
        Public Sub New(codigoPais As String, codigoUsuario As String)
            _codigoPais = codigoPais
            _codigoUsuario = codigoUsuario
        End Sub

        Public Sub New(codigoPais As String, codigoUsuario As String, urlServicioGenesis As String)
            _codigoPais = codigoPais
            _codigoUsuario = codigoUsuario
            _urlServicioGenesis = urlServicioGenesis

            GravarEmCache(codigoPais, urlServicioGenesis)
        End Sub

        Private Shared Sub CachedItemRemovedCallback(arguments As CacheEntryRemovedArguments)
            If arguments.RemovedReason = CacheEntryRemovedReason.Expired Then
                'Adiciona o pais e url novamente no cache
                GravarEmCache(arguments.CacheItem.Key, arguments.CacheItem.Value)
            End If
        End Sub

        Private Shared Sub GravarEmCache(codigoPais As String, urlServicioGenesis As String)
            If Not String.IsNullOrEmpty(codigoPais) AndAlso Not String.IsNullOrEmpty(urlServicioGenesis) Then
                Dim cache As ObjectCache = MemoryCache.Default
                Dim cacheKey As String = codigoPais

                If (cache.Contains(cacheKey)) Then
                    cache.Remove(cacheKey)
                End If

                'Quando o cache expirar será renovado
                Dim policy = New CacheItemPolicy() With { _
                    .RemovedCallback = New CacheEntryRemovedCallback(AddressOf CachedItemRemovedCallback), _
                    .AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                }

                cache.Add(cacheKey, urlServicioGenesis, policy)
            End If
        End Sub

        ''' <summary>
        ''' Realiza una búsqueda para una determinada aplicación.
        ''' </summary>
        ''' <param name="codigoAplicacion">Código de la aplicación</param>
        ''' <returns>Aplicación y sus funcionalidades ya registran</returns>
        Public Function Aplicacion(codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion) As AplicacionPreferencia

            Dim retornoAplicacion = _aplicaciones.SingleOrDefault(Function(aplic) aplic.CodigoPais = _codigoPais AndAlso aplic.CodigoUsuario = _codigoUsuario AndAlso aplic.CodigoAplicacion = codigoAplicacion)

            If retornoAplicacion Is Nothing Then
                retornoAplicacion = New AplicacionPreferencia(_codigoPais, codigoAplicacion, _codigoUsuario, Me._urlServicioGenesis)
                _aplicaciones.Add(retornoAplicacion)
            End If

            Return retornoAplicacion

        End Function

        ''' <summary>
        ''' Almacena todos los cambios.
        ''' </summary>
        Public Sub GuardarTodos()

            Dim proxy As New ProxyPreferencias(Me._urlServicioGenesis)

            Dim peticion As New GuardarPreferenciasPeticion()

            peticion.Preferencias = New ObjectModel.ObservableCollection(Of PreferenciaUsuario)

            For Each aplic As AplicacionPreferencia In _aplicaciones

                For Each func As FuncionalidadAplicacion In aplic.Funcionalidad()

                    For Each codigoComponente As String In func.Componente().Keys
                        Dim comp = func.Componente(codigoComponente)

                        For Each codigoPropriedad As String In comp.Propriedad().Keys
                            Dim prop = comp.Propriedad(codigoPropriedad)

                            Dim preferencia As New PreferenciaUsuario() With {
                                .CodigoAplicacion = aplic.CodigoAplicacion,
                                .CodigoUsuario = aplic.CodigoUsuario,
                                .CodigoFuncionalidad = func.Codigo,
                                .CodigoComponente = codigoComponente,
                                .CodigoPropriedad = codigoPropriedad,
                                .Valor = prop.Valor,
                                .TipoValorBinario = prop.Binario.Tipo,
                                .ValorBinario = prop.Binario.Valor
                            }

                            peticion.Preferencias.Add(preferencia)
                        Next
                    Next

                    For Each codigoPropriedad As String In func.Propriedad().Keys
                        Dim prop = func.Propriedad(codigoPropriedad)

                        Dim preferencia As New PreferenciaUsuario() With { _
                            .CodigoAplicacion = aplic.CodigoAplicacion,
                            .CodigoUsuario = aplic.CodigoUsuario,
                            .CodigoFuncionalidad = func.Codigo,
                            .CodigoPropriedad = codigoPropriedad,
                            .Valor = prop.Valor,
                            .TipoValorBinario = prop.Binario.Tipo,
                            .ValorBinario = prop.Binario.Valor
                        }

                        peticion.Preferencias.Add(preferencia)
                    Next

                Next

            Next

            If peticion.Preferencias.Count > 0 Then
                proxy.GuardarPreferencias(peticion)
            End If

        End Sub
    End Class
End Namespace