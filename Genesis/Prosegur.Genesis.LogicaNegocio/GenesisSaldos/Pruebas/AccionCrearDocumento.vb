Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions
Imports System.Threading.Tasks
Imports System.Text
Imports System.Net

Namespace Pruebas

    Public Class AccionCrearDocumento

        Public Shared Function crearDocumentoFondos(peticion As Contractos.Pruebas.crearDocumentoFondos.Peticion) As Contractos.Pruebas.crearDocumentoFondos.Respuesta

            Dim respuesta As New Contractos.Pruebas.crearDocumentoFondos.Respuesta
            Dim peticiones As New List(Of Contractos.Integracion.crearDocumentoFondos.Peticion)
            Dim respuestas As New List(Of Contractos.Integracion.crearDocumentoFondos.Respuesta)

            ' === Validar peticion ===
            Try
                validar(peticion)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.descripcionResultado = ex.Message.ToString
            End Try

            ' === Crear escenarios ===
            Try

                ' Valores Patron
                Dim peticionPatron As New Contractos.Integracion.crearDocumentoFondos.Peticion
                Dim peticionPatronAjeno As New Contractos.Integracion.crearDocumentoFondos.Peticion
                crearPeticionesPatron(peticion, peticiones, peticionPatron, peticionPatronAjeno)

                ' Valores Nothing
                crearPeticionesNothing(peticiones, peticionPatron, peticionPatronAjeno)

                ' Fechas
                crearPeticionesFechas(peticiones, peticionPatron.Clonar, peticionPatronAjeno.Clonar, peticion.fechaHoraGestionFondosInicio, peticion.fechaHoraGestionFondosFin)

                ' Formularios
                crearPeticionesFormularios(peticiones, peticionPatron.Clonar, peticionPatronAjeno.Clonar, peticion.codigoFormulario)

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.descripcionResultado = ex.ToString
            End Try

            ' === Llamar servicio ===
            Try
                If peticiones IsNot Nothing AndAlso peticiones.Count > 0 Then
                    Dim i As Integer = 0
                    For Each p In peticiones
                        If p IsNot Nothing AndAlso p.movimiento IsNot Nothing AndAlso i > 3 Then
                            p.movimiento.codigoExterno = "PR_" & Now.ToString("yyyyMMdd_HHmmss") & "_" & CInt(Int((1000 * Rnd()) + 1))
                        End If
                        respuestas.Add(LogicaNegocio.Integracion.AccionCrearDocumento.crearDocumentoFondos(p))
                        i = i + 1
                    Next
                End If
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.descripcionResultado = ex.ToString
            End Try

            respuesta.peticiones = peticiones
            respuesta.respuestas = respuestas

            Return respuesta
        End Function

        Private Shared Sub validar(peticion As Contractos.Pruebas.crearDocumentoFondos.Peticion)

        End Sub

        Private Shared Sub crearPeticionesPatron(peticion As Contractos.Pruebas.crearDocumentoFondos.Peticion,
                                                 ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                                 ByRef peticionPatron As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                 ByRef peticionPatronAjeno As Contractos.Integracion.crearDocumentoFondos.Peticion)

            With peticionPatron

                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    .tokenAcceso = AppSettings("Token")
                End If

                .IdentificadorAjeno = String.Empty
                .validarPostError = peticion.validarPostError
                .movimiento = New Contractos.Integracion.Comon.Movimiento
                .movimiento.codigoExterno = "PR_" & Now.ToString("yyyyMMdd_HHmmss") & "_" & CInt(Int((1000 * Rnd()) + 1))
                .movimiento.accion = Contractos.Integracion.Comon.Enumeradores.Accion.Aceptado
                .movimiento.codigoFormulario = peticion.codigoFormulario(0)
                .movimiento.fechaHoraGestionFondos = peticion.fechaHoraGestionFondosInicio
                .movimiento.usuario = peticion.usuario
                .movimiento.origen = peticion.movimiento.origen
                .movimiento.destino = peticion.movimiento.destino
                .movimiento.valores = peticion.movimiento.valores
                .movimiento.camposAdicionales = peticion.movimiento.camposAdicionales

            End With
            peticiones.Add(peticionPatron)

            ' Probar grabar los mismo datos
            peticiones.Add(peticionPatron)

            With peticionPatronAjeno

                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    .tokenAcceso = AppSettings("Token")
                End If

                .IdentificadorAjeno = peticion.IdentificadorAjeno
                .validarPostError = peticion.validarPostError
                .movimiento = New Contractos.Integracion.Comon.Movimiento
                .movimiento.codigoExterno = "PR_" & Now.ToString("yyyyMMdd_HHmmss") & "_" & CInt(Int((1000 * Rnd()) + 1))
                .movimiento.accion = Contractos.Integracion.Comon.Enumeradores.Accion.Aceptado
                .movimiento.codigoFormulario = peticion.codigoFormulario(0)
                .movimiento.fechaHoraGestionFondos = peticion.fechaHoraGestionFondosInicio
                .movimiento.usuario = peticion.usuario
                .movimiento.origen = peticion.movimientoAjeno.origen
                .movimiento.destino = peticion.movimientoAjeno.destino
                .movimiento.valores = peticion.movimientoAjeno.valores
                .movimiento.camposAdicionales = peticion.movimientoAjeno.camposAdicionales

            End With
            peticiones.Add(peticionPatronAjeno)

            ' Probar grabar los mismo datos
            peticiones.Add(peticionPatronAjeno)

        End Sub

        Private Shared Sub crearPeticionesFechas(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                                 peticionPatron As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                 peticionPatronAjeno As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                 fechaHoraGestionFondosInicio As DateTime,
                                                 fechaHoraGestionFondosFin As DateTime)

            peticionPatron.movimiento.fechaHoraGestionFondos = fechaHoraGestionFondosFin
            peticiones.Add(peticionPatron)

            peticionPatronAjeno.movimiento.fechaHoraGestionFondos = fechaHoraGestionFondosFin
            peticiones.Add(peticionPatronAjeno)

        End Sub

        Private Shared Sub crearPeticionesFormularios(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                                      peticionPatron As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                      peticionPatronAjeno As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                                      codigoFormulario As List(Of String))

            For Each formulario In codigoFormulario

                peticionPatron.movimiento.codigoFormulario = formulario
                peticiones.Add(peticionPatron)

                peticionPatronAjeno.movimiento.codigoFormulario = formulario
                peticiones.Add(peticionPatronAjeno)

            Next

        End Sub

#Region "[John Snow]"

        Private Shared Sub crearPeticionesNothing(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                          peticionPatron As Contractos.Integracion.crearDocumentoFondos.Peticion,
                                          peticionPatronAjeno As Contractos.Integracion.crearDocumentoFondos.Peticion)

            peticiones.Add(Nothing)
            peticiones.Add(New Contractos.Integracion.crearDocumentoFondos.Peticion)

            peticiones.Add(tokenNothing(peticionPatron.Clonar))
            peticiones.Add(tokenNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(validarPostErrorNothing(peticionPatron.Clonar))
            peticiones.Add(validarPostErrorNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(movimientoNothing(peticionPatron.Clonar))
            peticiones.Add(movimientoNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(accionNothing(peticionPatron.Clonar))
            peticiones.Add(accionNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(codigoFormularioNothing(peticionPatron.Clonar))
            peticiones.Add(codigoFormularioNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(codigoExternoNothing(peticionPatron.Clonar))
            peticiones.Add(codigoExternoNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(fechaHoraGestionFondosNothing(peticionPatron.Clonar))
            peticiones.Add(fechaHoraGestionFondosNothing(peticionPatronAjeno.Clonar))

            peticiones.Add(usuarioNothing(peticionPatron.Clonar))
            peticiones.Add(usuarioNothing(peticionPatronAjeno.Clonar))

            cuentaNothing(peticiones, peticionPatron.Clonar)
            cuentaNothing(peticiones, peticionPatronAjeno.Clonar)

            valoresNothing(peticiones, peticionPatron.Clonar)
            valoresNothing(peticiones, peticionPatronAjeno.Clonar)

            camposAdicionalesNothing(peticiones, peticionPatron.Clonar)
            camposAdicionalesNothing(peticiones, peticionPatronAjeno.Clonar)

        End Sub

        Private Shared Function tokenNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.tokenAcceso = Nothing
            Return peticion
        End Function

        Private Shared Function validarPostErrorNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.validarPostError = Nothing
            Return peticion
        End Function

        Private Shared Function movimientoNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento = Nothing
            Return peticion
        End Function

        Private Shared Function accionNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento.accion = Nothing
            Return peticion
        End Function

        Private Shared Function codigoFormularioNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento.codigoFormulario = Nothing
            Return peticion
        End Function

        Private Shared Function codigoExternoNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento.codigoExterno = Nothing
            Return peticion
        End Function

        Private Shared Function fechaHoraGestionFondosNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento.fechaHoraGestionFondos = Nothing
            Return peticion
        End Function

        Private Shared Function usuarioNothing(peticion As Contractos.Integracion.crearDocumentoFondos.Peticion) As Contractos.Integracion.crearDocumentoFondos.Peticion
            peticion.movimiento.usuario = Nothing
            Return peticion
        End Function

        Private Shared Sub cuentaNothing(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                         peticion As Contractos.Integracion.crearDocumentoFondos.Peticion)

            Dim clonePeticion As Contractos.Integracion.crearDocumentoFondos.Peticion = peticion.Clonar
            clonePeticion.movimiento.origen = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.destino = Nothing
            peticiones.Add(clonePeticion)

        End Sub

        Private Shared Sub valoresNothing(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                          peticion As Contractos.Integracion.crearDocumentoFondos.Peticion)

            Dim clonePeticion As Contractos.Integracion.crearDocumentoFondos.Peticion = peticion.Clonar
            clonePeticion.movimiento.valores = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.divisas = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.divisas(0).codigoDivisa = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.divisas(0).denominaciones = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.divisas(0).importe = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.mediosDePago = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.mediosDePago(0).cantidad = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.mediosDePago(0).codigoDivisa = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.mediosDePago(0).codigoMedioDePago = Nothing
            peticiones.Add(clonePeticion)

            clonePeticion = peticion.Clonar
            clonePeticion.movimiento.valores.mediosDePago(0).importe = Nothing
            peticiones.Add(clonePeticion)

        End Sub

        Private Shared Sub camposAdicionalesNothing(ByRef peticiones As List(Of Contractos.Integracion.crearDocumentoFondos.Peticion),
                                                    peticion As Contractos.Integracion.crearDocumentoFondos.Peticion)
            peticion.movimiento.camposAdicionales = Nothing
            peticiones.Add(peticion)
        End Sub

#End Region

    End Class

End Namespace

