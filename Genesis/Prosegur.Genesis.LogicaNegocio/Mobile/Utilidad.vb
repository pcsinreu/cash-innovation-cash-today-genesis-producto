Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisMovil
Imports Prosegur.Framework.Dicionario.Tradutor
Namespace Mobile
    Public Class Utilidad
        ''' <summary>
        ''' Recupera a descrição do cliente, canal, subcanal, divisa, denominação e tipo contenedor através dos respectivos códigos
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerDescripcionesFiltroExtracion(Peticion As ObtenerDescripcionesFiltroExtracion.Peticion) As ObtenerDescripcionesFiltroExtracion.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
                End If

                Return AccesoDatos.Genesis.Utilidad.ObtenerDescripcionesFiltroExtracion(Peticion)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Shared Function ConsultarContenedoresPorFIFO(Peticion As ContractoServicio.Contractos.GenesisMovil.ConsultarContenedorFIFO.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
                End If

                Dim peticionContenedor As New ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Peticion
                peticionContenedor.Contenedor = New ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor

                peticionContenedor.CodigoUsuario = Peticion.CodigoUsuario

                peticionContenedor.Contenedor.CodTipoContenedor = Peticion.Contenedor.CodTipoContenedor
                peticionContenedor.Contenedor.CuentaContenedor = New Comon.Clases.CuentasContenedor

                peticionContenedor.Contenedor.CuentaContenedor.Cuenta = New Comon.Clases.Cuenta
                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector = New Comon.Clases.Sector

                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion = New Comon.Clases.Delegacion
                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector.Delegacion.Identificador = Peticion.Contenedor.IdentificadorDelegacion

                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector.Planta = New Comon.Clases.Planta
                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector.Planta.Identificador = Peticion.Contenedor.IdentificadorPlanta

                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Sector.Identificador = Peticion.Contenedor.IdentificadorSector

                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Cliente = New Comon.Clases.Cliente
                peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Cliente.Identificador = Peticion.Contenedor.IdentificadorCliente

                If Peticion.Contenedor.Canal IsNot Nothing Then
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Canal = New Comon.Clases.Canal
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Canal.Identificador = Peticion.Contenedor.Canal.Identificador
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.Canal.Codigo = Peticion.Contenedor.Canal.Codigo
                End If

                If Peticion.Contenedor.SubCanal IsNot Nothing Then
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.SubCanal = New Comon.Clases.SubCanal
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.SubCanal.Identificador = Peticion.Contenedor.SubCanal.Identificador
                    peticionContenedor.Contenedor.CuentaContenedor.Cuenta.SubCanal.Codigo = Peticion.Contenedor.SubCanal.Codigo
                End If

                peticionContenedor.Contenedor.ValoresContenedor = New List(Of ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor.ImporteContenedor)
                Dim importe As New ContractoServicio.GenesisSaldos.Contenedores.Comon.Contenedor.ImporteContenedor
                importe.IdentificadorDivisa = Peticion.Contenedor.IdentificadorDivisa
                importe.IdentificadorDenominacion = Peticion.Contenedor.IdentificadorDenominacion
                importe.Importe = Peticion.Contenedor.Importe
                peticionContenedor.Contenedor.ValoresContenedor.Add(importe)

                Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ConsultarContenedoresPorFIFO(peticionContenedor)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Shared Function ReenvioEntreSectores(Peticion As ContractoServicio.Contractos.GenesisMovil.ReenvioEntreSectores.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
                End If

                If Peticion.Documento Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Documento")))
                End If

                If Peticion.Documento.SectorOrigen Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("SectorOrigen")))
                End If

                If Peticion.Documento.SectorDestino Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("SectorDestino")))
                End If

                If Peticion.Documento.PrecintosContenedores Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("PrecintosContenedores")))
                End If

                Dim peticionReenvio As New ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Peticion

                peticionReenvio.CodigoUsuario = Peticion.CodigoUsuario
                peticionReenvio.Documento = New ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Documento
                peticionReenvio.Documento.CodigoUsuario = Peticion.Documento.CodigoUsuario
                peticionReenvio.Documento.CodigoFormulario = Nothing

                peticionReenvio.Documento.FechaHoraGestion = Date.ParseExact(Peticion.Documento.FechaHoraGestion, "dd/MM/yyyy HH:mm:ss",
                           System.Globalization.DateTimeFormatInfo.InvariantInfo)

                peticionReenvio.Documento.SectorOrigen = New ContractoServicio.GenesisSaldos.Contenedores.Comon.Sector
                peticionReenvio.Documento.SectorOrigen.IdentificadorDelegacion = Peticion.Documento.SectorOrigen.IdentificadorDelegacion
                peticionReenvio.Documento.SectorOrigen.IdentificadorPlanta = Peticion.Documento.SectorOrigen.IdentificadorPlanta
                peticionReenvio.Documento.SectorOrigen.IdentificadorSector = Peticion.Documento.SectorOrigen.IdentificadorSector

                peticionReenvio.Documento.SectorDestino = New ContractoServicio.GenesisSaldos.Contenedores.Comon.Sector
                peticionReenvio.Documento.SectorDestino.IdentificadorDelegacion = Peticion.Documento.SectorDestino.IdentificadorDelegacion
                peticionReenvio.Documento.SectorDestino.IdentificadorPlanta = Peticion.Documento.SectorDestino.IdentificadorPlanta
                peticionReenvio.Documento.SectorDestino.IdentificadorSector = Peticion.Documento.SectorDestino.IdentificadorSector

                peticionReenvio.Documento.Contenedores = New List(Of ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Contenedor)
                For Each precinto In Peticion.Documento.PrecintosContenedores
                    Dim contenedor As New ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Contenedor
                    contenedor.CodigoPrecinto = precinto
                    peticionReenvio.Documento.Contenedores.Add(contenedor)
                Next

                Return Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.ReenvioEntreSectores(peticionReenvio)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Shared Function GrabarInventarioContenedor(Peticion As ContractoServicio.Contractos.GenesisMovil.GrabarInventarioContenedor.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
                End If

                If Peticion.Inventarios Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Inventarios")))
                End If

                Dim respuesta As New ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta

                For Each inventario In Peticion.Inventarios
                    Dim peticionInventario As New ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Peticion
                    peticionInventario.Inventario = New ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Inventario

                    peticionInventario.Inventario.Sector = New ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Sector
                    peticionInventario.Inventario.Sector.codDelegacion = inventario.CodigoDelegacion
                    peticionInventario.Inventario.Sector.codPlanta = inventario.CodigoPlanta
                    peticionInventario.Inventario.Sector.codSector = inventario.CodigoSector

                    If Not String.IsNullOrEmpty(inventario.CodigoCliente) Then
                        peticionInventario.Inventario.Cliente = New ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Cliente
                        peticionInventario.Inventario.Cliente.codCliente = inventario.CodigoCliente
                    End If

                    peticionInventario.Inventario.regresarDetalles = False
                    peticionInventario.Inventario.UsuarioCreacion = inventario.UsuarioCreacion

                    If inventario.CodigosPrecintos IsNot Nothing AndAlso inventario.CodigosPrecintos.Count > 0 Then
                        peticionInventario.Inventario.codigosPrecintos = inventario.CodigosPrecintos
                    End If

                    Dim respuestaInventario As ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta
                    respuestaInventario = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Contenedor.GrabarInventarioContenedor(peticionInventario)

                    If (respuestaInventario IsNot Nothing AndAlso (respuestaInventario.CodigoError <> 0 OrElse
                                                                  Not String.IsNullOrEmpty(respuestaInventario.MensajeError) OrElse
                                                                  Not String.IsNullOrEmpty(respuestaInventario.MensajeErrorDescriptiva))) Then
                        respuesta = respuestaInventario
                    End If

                Next

                Return respuesta

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Shared Function ObtenerPosicionesSector(Peticion As ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Peticion) As ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta

            Dim objRespuesta As New ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta

            Try

                If Peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
                End If

                If String.IsNullOrEmpty(Peticion.IdentificadorSector) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("IdentificadorSector")))
                End If

                objRespuesta = Prosegur.Genesis.AccesoDatos.GenesisSaldos.Contenedor.ObtenerPosicionesSector(Peticion.IdentificadorSector)

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.Mensajes.Add(ex.Message)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.Excepciones.Add(ex.Message)
            End Try

            Return objRespuesta
        End Function

    End Class
End Namespace
