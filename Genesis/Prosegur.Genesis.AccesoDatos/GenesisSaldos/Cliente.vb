
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarClientes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarClientes.Salida
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace GenesisSaldos
    Public Class Cliente

        Public Shared Sub RecuperarClientes(peticion As RecuperarClientes.Peticion,
                                              ByRef respuesta As RecuperarClientes.Respuesta,
                                              ByRef log As StringBuilder)

            Dim TiempoParcial As DateTime
            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ArmarWrapper(peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                respuesta.Paginacion = New ContractoServicio.Contractos.Integracion.Comon.Paginacion

                If peticion.Paginacion IsNot Nothing Then
                    respuesta.Paginacion = peticion.Paginacion
                    If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) Then
                        respuesta.Paginacion.Indice = 0
                    End If
                Else
                    respuesta.Paginacion.Indice = 0
                End If


                PoblarRespuesta(ds, respuesta, peticion)

                spw = Nothing
                ds.Dispose()


                log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")



            Catch ex As Exception

            End Try

        End Sub

        Private Shared Sub PoblarRespuesta(ByRef pDS As DataSet, ByRef pRespuesta As RecuperarClientes.Respuesta, ByRef pPeticion As RecuperarClientes.Peticion)

            If pDS IsNot Nothing AndAlso pDS.Tables.Contains("validaciones") AndAlso pDS.Tables("validaciones").Rows.Count > 0 Then
                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of Salida.Detalle)
                Dim unDetalle As Salida.Detalle
                For Each row As DataRow In pDS.Tables("validaciones").Rows
                    unDetalle = New Salida.Detalle

                    unDetalle.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    unDetalle.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))

                    pRespuesta.Resultado.Detalles.Add(unDetalle)
                Next
            End If

            If pDS IsNot Nothing AndAlso pDS.Tables.Contains("clientes") AndAlso pDS.Tables("clientes").Rows.Count > 0 Then
                If pRespuesta.Clientes Is Nothing Then pRespuesta.Clientes = New List(Of Salida.Cliente)

                Dim unCliente As Salida.Cliente
                Dim datosBancarios As List(Of Salida.DatoBancario)
                Dim codigosAjenos As List(Of CodigoAjeno)
                Dim subclientes As List(Of Salida.SubCliente)

                For Each row As DataRow In pDS.Tables("clientes").Rows


                    unCliente = New Salida.Cliente

                    unCliente.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    unCliente.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                    unCliente.Identificador = Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                    unCliente.Tipo = Util.AtribuirValorObj(row("TIPO"), GetType(String))
                    unCliente.Vigente = Util.AtribuirValorObj(row("VIGENTE"), GetType(Boolean))
                    unCliente.CodigoBancario = Util.AtribuirValorObj(row("CODIGOBANCARIO"), GetType(String))

                    'Si el nivel es CLIENTE, no hay que mostrar sus subclientes asociados.
                    If pPeticion.Nivel.ToString.ToUpper <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNivel.Cliente.ToString.ToUpper Then
                        subclientes = DevolverSubClientes(pDS, unCliente.Identificador, pPeticion)
                        If subclientes.Count > 0 Then
                            unCliente.SubClientes = subclientes
                        End If
                    End If
                    If pPeticion.RecuperarDatosBancarios Then
                        datosBancarios = DevolverDatosBancarios(pDS, unCliente.Identificador, "OID_CLIENTE")
                        If datosBancarios.Count > 0 Then
                            unCliente.DatosBancarios = datosBancarios
                        End If
                    End If

                    If pPeticion.RecuperarCodigosAjenos Then
                        codigosAjenos = DevolverCodigosAjenos(pDS, unCliente.Identificador, "GEPR_TCLIENTE")

                        If codigosAjenos.Count > 0 Then
                            unCliente.CodigosAjenos = codigosAjenos
                        End If
                    End If

                    If Not pRespuesta.Clientes.Contains(unCliente) Then
                        pRespuesta.Clientes.Add(unCliente)
                    End If
                Next
            End If
        End Sub

        Private Shared Function DevolverDatosBancarios(pDS As DataSet, pOid As String, pNombreCampo As String) As List(Of Salida.DatoBancario)
            Dim listaRetorno As New List(Of Salida.DatoBancario)
            Dim strNombreTablaDeDatos As String = "datos_bancarios"
            If pDS IsNot Nothing AndAlso pDS.Tables.Contains(strNombreTablaDeDatos) AndAlso pDS.Tables(strNombreTablaDeDatos).Rows.Count > 0 Then
                Dim unDatoBancario As Salida.DatoBancario
                For Each row As DataRow In pDS.Tables(strNombreTablaDeDatos).Select(pNombreCampo + " = '" + pOid + "'")
                    unDatoBancario = New Salida.DatoBancario With {
                        .Identificador = Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String)),
                        .CodigoBanco = Util.AtribuirValorObj(row("CODIGOBANCO"), GetType(String)),
                        .CodigoAgencia = Util.AtribuirValorObj(row("CODIGOAGENCIA"), GetType(String)),
                        .CodigoDivisa = Util.AtribuirValorObj(row("CODIGODIVISA"), GetType(String)),
                        .NumeroDocumento = Util.AtribuirValorObj(row("NUMERODOCUMENTO"), GetType(String)),
                        .Vigente = Util.AtribuirValorObj(row("VIGENTE"), GetType(Boolean)),
                        .Tipo = Util.AtribuirValorObj(row("TIPO"), GetType(String)),
                        .Patron = Util.AtribuirValorObj(row("PATRON"), GetType(Boolean)),
                        .Observaciones = Util.AtribuirValorObj(row("OBSERVACIONES"), GetType(String)),
                        .Titularidad = Util.AtribuirValorObj(row("TITULARIDAD"), GetType(String)),
                        .NumeroCuenta = Util.AtribuirValorObj(row("NUMEROCUENTA"), GetType(String)),
                        .CampoAdicional1 = Util.AtribuirValorObj(row("CAMPOADICIONAL1"), GetType(String)),
                        .CampoAdicional2 = Util.AtribuirValorObj(row("CAMPOADICIONAL2"), GetType(String)),
                        .CampoAdicional3 = Util.AtribuirValorObj(row("CAMPOADICIONAL3"), GetType(String)),
                        .CampoAdicional4 = Util.AtribuirValorObj(row("CAMPOADICIONAL4"), GetType(String)),
                        .CampoAdicional5 = Util.AtribuirValorObj(row("CAMPOADICIONAL5"), GetType(String)),
                        .CampoAdicional6 = Util.AtribuirValorObj(row("CAMPOADICIONAL6"), GetType(String)),
                        .CampoAdicional7 = Util.AtribuirValorObj(row("CAMPOADICIONAL7"), GetType(String)),
                        .CampoAdicional8 = Util.AtribuirValorObj(row("CAMPOADICIONAL8"), GetType(String))
                        }
                    If Not listaRetorno.Contains(unDatoBancario) Then
                        listaRetorno.Add(unDatoBancario)
                    End If

                Next
            End If

            Return listaRetorno
        End Function

        Private Shared Function DevolverCodigosAjenos(pDS As DataSet, pOid As String, pNombreTabla As String) As List(Of CodigoAjeno)
            Dim listaRetorno As New List(Of CodigoAjeno)
            If pDS IsNot Nothing AndAlso pDS.Tables.Contains("codigos_ajenos") AndAlso pDS.Tables("codigos_ajenos").Rows.Count > 0 Then
                Dim unCodigoAjeno As CodigoAjeno

                For Each row As DataRow In pDS.Tables("codigos_ajenos").Select("OID_GENESIS = '" + pOid + "' AND NOMBRETABLA='" + pNombreTabla + "'")
                    unCodigoAjeno = New CodigoAjeno With {
                        .Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String)),
                        .Identificador = Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String)),
                        .Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String)),
                        .CodigoIdentificador = Util.AtribuirValorObj(row("CODIGOIDENTIFICADOR"), GetType(String)),
                        .Patron = Util.AtribuirValorObj(row("PATRON"), GetType(Boolean)),
                        .Vigente = Util.AtribuirValorObj(row("VIGENTE"), GetType(Boolean))
                        }
                    If Not listaRetorno.Contains(unCodigoAjeno) Then
                        listaRetorno.Add(unCodigoAjeno)
                    End If
                Next
            End If

            Return listaRetorno
        End Function

        Private Shared Function ArmarWrapper(peticion As Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PCLIENTE_{0}.srecuperar_datosentidades", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            If peticion.Paginacion IsNot Nothing Then

                If Not String.IsNullOrEmpty(peticion.Paginacion.Indice) OrElse IsNumeric(peticion.Paginacion.Indice) Then
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, peticion.Paginacion.Indice, , False)
                ElseIf Not String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) AndAlso IsNumeric(peticion.Paginacion.RegistroPorPagina) Then
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, 0, , False)
                Else
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                End If
                If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) OrElse Not IsNumeric(peticion.Paginacion.RegistroPorPagina) Then
                    spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                Else
                    spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, peticion.Paginacion.RegistroPorPagina, , False)
                End If

            Else
                spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, Nothing, , False)
            End If


            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            If peticion.Configuracion IsNot Nothing Then
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            Else
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, Nothing,  , False)
            End If

            spw.AgregarParam("par$cod_clientes", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_clientes", ProsegurDbType.Descricao_Longa, Nothing, , True)

            spw.AgregarParam("par$cod_subclientes", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_subclientes", ProsegurDbType.Descricao_Longa, Nothing, , True)

            spw.AgregarParam("par$cod_ptoservs", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_ptoservs", ProsegurDbType.Descricao_Longa, Nothing, , True)



            spw.AgregarParam("par$nivel", ProsegurDbType.Descricao_Curta, peticion.Nivel.ToString.ToUpper, ParameterDirection.Input, False)

            If peticion.RecuperarDatosBancarios Is Nothing Then
                spw.AgregarParam("par$bol_recdatbancs", ProsegurDbType.Logico, 0, , False)
            Else
                spw.AgregarParam("par$bol_recdatbancs", ProsegurDbType.Logico, If(peticion.RecuperarDatosBancarios, 1, 0), , False)
            End If

            If peticion.RecuperarCodigosAjenos Is Nothing Then
                spw.AgregarParam("par$bol_rec_codaje", ProsegurDbType.Logico, 0, , False)
            Else
                spw.AgregarParam("par$bol_rec_codaje", ProsegurDbType.Logico, If(peticion.RecuperarCodigosAjenos, 1, 0), , False)
            End If


            spw.AgregarParam("par$rc_clientes", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "clientes")
            spw.AgregarParam("par$rc_subclientes", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "subclientes")
            spw.AgregarParam("par$rc_ptos_serv", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "puntos")
            spw.AgregarParam("par$rc_dat_banc", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos_bancarios")
            spw.AgregarParam("par$rc_cod_ajenos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "codigos_ajenos")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")


            ' ----> Poblar arrays asociativos  ---> INICIO
            If peticion.Clientes IsNot Nothing AndAlso peticion.Clientes.Count > 0 Then
                spw.Param("par$cod_clientes").AgregarValorArray("")
                spw.Param("par$des_clientes").AgregarValorArray("")
                For Each unCliente As ContractoServicio.Contractos.Integracion.Comon.Entidad In peticion.Clientes
                    spw.Param("par$cod_clientes").AgregarValorArray(unCliente.Codigo)
                    spw.Param("par$des_clientes").AgregarValorArray(unCliente.Descripcion)
                Next
            End If

            If peticion.SubClientes IsNot Nothing AndAlso peticion.SubClientes.Count > 0 Then
                spw.Param("par$cod_subclientes").AgregarValorArray("")
                spw.Param("par$des_subclientes").AgregarValorArray("")
                For Each unSubCliente As ContractoServicio.Contractos.Integracion.Comon.Entidad In peticion.SubClientes
                    spw.Param("par$cod_subclientes").AgregarValorArray(unSubCliente.Codigo)
                    spw.Param("par$des_subclientes").AgregarValorArray(unSubCliente.Descripcion)
                Next
            End If

            If peticion.PuntosServicio IsNot Nothing AndAlso peticion.PuntosServicio.Count > 0 Then
                spw.Param("par$cod_ptoservs").AgregarValorArray("")
                spw.Param("par$des_ptoservs").AgregarValorArray("")
                For Each unPtoServ As ContractoServicio.Contractos.Integracion.Comon.Entidad In peticion.PuntosServicio
                    spw.Param("par$cod_ptoservs").AgregarValorArray(unPtoServ.Codigo)
                    spw.Param("par$des_ptoservs").AgregarValorArray(unPtoServ.Descripcion)
                Next
            End If
            ' ----> Poblar arrays asociativos  ---> FIN

            Return spw
        End Function
        Private Shared Function DevolverSubClientes(pDS As DataSet, pOidCliente As String, ByRef pPeticion As RecuperarClientes.Peticion) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarClientes.Salida.SubCliente)
            Dim listaRetorno As New List(Of Salida.SubCliente)

            If pDS.Tables.Contains("subclientes") AndAlso pDS.Tables("subclientes").Rows.Count > 0 Then
                Dim unSubCliente As Salida.SubCliente
                Dim datosBancarios As List(Of Salida.DatoBancario)
                Dim codigosAjenos As List(Of Salida.CodigoAjeno)
                Dim puntosDeServicios As List(Of Salida.PuntoServicio)

                For Each row As DataRow In pDS.Tables("subclientes").Select("OID_CLIENTE = '" + pOidCliente + "'")
                    unSubCliente = New Salida.SubCliente

                    unSubCliente.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    unSubCliente.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                    unSubCliente.Identificador = Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                    unSubCliente.Vigente = Util.AtribuirValorObj(row("VIGENTE"), GetType(Boolean))

                    'Si el nivel es SUBCLIENTE, no hay que mostrar sus subclientes asociados.
                    If pPeticion.Nivel.ToString.ToUpper <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNivel.SubCliente.ToString.ToUpper Then
                        puntosDeServicios = DevolverPuntosDeServicios(pDS, unSubCliente.Identificador, pPeticion)
                        If puntosDeServicios.Count > 0 Then
                            unSubCliente.PuntosServicio = puntosDeServicios
                        End If
                    End If



                    If pPeticion.RecuperarCodigosAjenos Then
                        codigosAjenos = DevolverCodigosAjenos(pDS, unSubCliente.Identificador, "GEPR_TSUBCLIENTE")
                        If codigosAjenos.Count > 0 Then
                            unSubCliente.CodigosAjenos = codigosAjenos
                        End If
                    End If

                    If pPeticion.RecuperarDatosBancarios Then
                        datosBancarios = DevolverDatosBancarios(pDS, unSubCliente.Identificador, "OID_SUBCLIENTE")
                        If datosBancarios.Count > 0 Then
                            unSubCliente.DatosBancarios = datosBancarios
                        End If

                    End If


                    If Not listaRetorno.Contains(unSubCliente) Then
                        listaRetorno.Add(unSubCliente)
                    End If
                Next
            End If

            Return listaRetorno
        End Function

        Private Shared Function DevolverPuntosDeServicios(ByRef pDS As DataSet, ByRef pOidSubcliente As String, ByRef pPeticion As RecuperarClientes.Peticion) As List(Of ContractoServicio.Contractos.Integracion.RecuperarClientes.Salida.PuntoServicio)


            Dim listaRetorno As New List(Of ContractoServicio.Contractos.Integracion.RecuperarClientes.Salida.PuntoServicio)


            If pDS.Tables.Contains("puntos") AndAlso pDS.Tables("puntos").Rows.Count > 0 Then
                Dim unPuntoDeServicio As Salida.PuntoServicio
                Dim datosBancarios As List(Of Salida.DatoBancario)
                Dim codigosAjenos As List(Of Salida.CodigoAjeno)


                For Each row As DataRow In pDS.Tables("puntos").Select("OID_SUBCLIENTE = '" + pOidSubcliente + "'")
                    unPuntoDeServicio = New Salida.PuntoServicio

                    unPuntoDeServicio.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                    unPuntoDeServicio.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                    unPuntoDeServicio.Identificador = Util.AtribuirValorObj(row("IDENTIFICADOR"), GetType(String))
                    unPuntoDeServicio.Vigente = Util.AtribuirValorObj(row("VIGENTE"), GetType(Boolean))

                    If pPeticion.RecuperarCodigosAjenos Then
                        codigosAjenos = DevolverCodigosAjenos(pDS, unPuntoDeServicio.Identificador, "GEPR_TPUNTO_SERVICIO")
                        If codigosAjenos.Count > 0 Then
                            unPuntoDeServicio.CodigosAjenos = codigosAjenos
                        End If
                    End If

                    If pPeticion.RecuperarDatosBancarios Then
                        datosBancarios = DevolverDatosBancarios(pDS, unPuntoDeServicio.Identificador, "OID_PTO_SERVICIO")
                        If datosBancarios.Count > 0 Then
                            unPuntoDeServicio.DatosBancarios = datosBancarios
                        End If
                    End If

                    If Not listaRetorno.Contains(unPuntoDeServicio) Then
                        listaRetorno.Add(unPuntoDeServicio)
                    End If
                Next
            End If

            Return listaRetorno

        End Function

    End Class
End Namespace

