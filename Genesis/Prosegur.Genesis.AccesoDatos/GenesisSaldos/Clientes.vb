Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfigurarClientes
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Imports Prosegur.Genesis.Comon.Extenciones
Namespace GenesisSaldos
    Public Class Clientes
        Public Shared Function ConfigurarClientes(identificadorLlamada As String, peticion As Peticion, log As StringBuilder, ByRef respuesta As Respuesta) As List(Of Salida.Cliente)
            Dim TiempoParcial As DateTime

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                If peticion.Clientes IsNot Nothing AndAlso peticion.Clientes.Count > 0 Then

                    log.AppendLine("AccesoDatos - cantidad de clientes: " & peticion.Clientes.Count.ToString() & ";")

                    TiempoParcial = Now
                    spw = sconfigurar_clientes(identificadorLlamada, peticion)
                    log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                    log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    PoblarClientes(ds, respuesta)
                    log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "3") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorAplicacion
                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "2") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorNegocio

                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "0") Then
                        respuesta.Resultado.Codigo = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.Exito
                    End If
                End If


            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return respuesta.Clientes
        End Function

        Private Shared Sub PoblarClientes(ds As DataSet, ByRef respuesta As Respuesta)
            Dim clientes = New List(Of Salida.Cliente)


            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    'Clientes
                    For Each rowCliente As DataRow In ds.Tables("validaciones").Select("ENTIDAD = 'CLIENTE'", "IDX")
                        Dim indiceCliente = Util.AtribuirValorObj(rowCliente("IDX"), GetType(Integer))

                        Dim cliente = clientes.FirstOrDefault(Function(x) x.Indice = indiceCliente)
                        If cliente Is Nothing Then
                            cliente = New Salida.Cliente With {
                               .Detalles = New List(Of Salida.Detalle),
                               .SubClientes = New List(Of Salida.SubCliente)
                           }
                            cliente.Codigo = Util.AtribuirValorObj(rowCliente("CODIGO_ENTIDAD"), GetType(String))
                            cliente.Indice = indiceCliente
                            clientes.Add(cliente)
                        End If

                        If Not cliente.Detalles.Any(Function(x) x.Codigo = Util.AtribuirValorObj(rowCliente("CODIGO"), GetType(String)) AndAlso
                                                        x.Descripcion = Util.AtribuirValorObj(rowCliente("DESCRIPCION"), GetType(String))) Then
                            Dim detalleCliente As New Salida.Detalle
                            detalleCliente.Codigo = Util.AtribuirValorObj(rowCliente("CODIGO"), GetType(String))
                            detalleCliente.Descripcion = Util.AtribuirValorObj(rowCliente("DESCRIPCION"), GetType(String))
                            cliente.Detalles.Add(detalleCliente)
                        End If


                        'Busco en los detalles si hay errores para agregar el tipo de resultado al cliente
                        If cliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "3") Then
                            cliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorAplicacion
                            respuesta.Resultado.Codigo = cliente.TipoResultado
                        ElseIf cliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "2") Then
                            cliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorNegocio
                            respuesta.Resultado.Codigo = cliente.TipoResultado
                        ElseIf cliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "0") Then
                            cliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.Exito
                            respuesta.Resultado.Codigo = cliente.TipoResultado
                        End If

                        'Subclientes
                        'Filtramos los subclientes por el indice del cliente ordenados por IDX
                        For Each rowSubcliente As DataRow In ds.Tables("validaciones").Select("ENTIDAD = 'SUBCLIENTE' AND IDX_PADRE = '" & indiceCliente.ToString & "'", "IDX")

                            Dim indiceSubcliente = Util.AtribuirValorObj(rowSubcliente("IDX"), GetType(Integer))

                            Dim subcliente = cliente.SubClientes.FirstOrDefault(Function(x) x.Indice = indiceSubcliente)
                            If subcliente Is Nothing Then
                                subcliente = New Salida.SubCliente With {
                                    .Detalles = New List(Of Salida.Detalle),
                                    .PuntosServicio = New List(Of Salida.PuntoServicio)
                                }
                                subcliente.Codigo = Util.AtribuirValorObj(rowSubcliente("CODIGO_ENTIDAD"), GetType(String))
                                subcliente.Indice = indiceSubcliente
                                cliente.SubClientes.Add(subcliente)
                            End If


                            If Not subcliente.Detalles.Any(Function(x) x.Codigo = Util.AtribuirValorObj(rowSubcliente("CODIGO"), GetType(String)) AndAlso
                                                        x.Descripcion = Util.AtribuirValorObj(rowSubcliente("DESCRIPCION"), GetType(String))) Then
                                Dim detalleSubcliente As New Salida.Detalle
                                detalleSubcliente.Codigo = Util.AtribuirValorObj(rowSubcliente("CODIGO"), GetType(String))
                                detalleSubcliente.Descripcion = Util.AtribuirValorObj(rowSubcliente("DESCRIPCION"), GetType(String))
                                subcliente.Detalles.Add(detalleSubcliente)
                            End If

                            'Busco en los detalles si hay errores para agregar el tipo de resultado al cliente
                            If subcliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "3") Then
                                subcliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorAplicacion
                                respuesta.Resultado.Codigo = subcliente.TipoResultado
                            ElseIf subcliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "2") Then
                                subcliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorNegocio
                                respuesta.Resultado.Codigo = subcliente.TipoResultado
                            ElseIf subcliente.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "0") Then
                                subcliente.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.Exito
                                respuesta.Resultado.Codigo = subcliente.TipoResultado
                            End If


                            'PuntoServicio
                            For Each rowPuntoServicio As DataRow In ds.Tables("validaciones").Select("ENTIDAD = 'PUNTOSERVICIO' AND IDX_PADRE = '" & indiceSubcliente.ToString & "'", "IDX")
                                Dim indicePuntoServicio = Util.AtribuirValorObj(rowPuntoServicio("IDX"), GetType(Integer))

                                Dim puntoServicio = subcliente.PuntosServicio.FirstOrDefault(Function(x) x.Indice = indicePuntoServicio)
                                If puntoServicio Is Nothing Then
                                    puntoServicio = New Salida.PuntoServicio With {.Detalles = New List(Of Salida.Detalle)}
                                    puntoServicio.Codigo = Util.AtribuirValorObj(rowPuntoServicio("CODIGO_ENTIDAD"), GetType(String))
                                    puntoServicio.Indice = indicePuntoServicio
                                    subcliente.PuntosServicio.Add(puntoServicio)
                                End If

                                If Not puntoServicio.Detalles.Any(Function(x) x.Codigo = Util.AtribuirValorObj(rowPuntoServicio("CODIGO"), GetType(String)) AndAlso
                                                        x.Descripcion = Util.AtribuirValorObj(rowPuntoServicio("DESCRIPCION"), GetType(String))) Then
                                    Dim detallePuntoServicio As New Salida.Detalle
                                    detallePuntoServicio.Codigo = Util.AtribuirValorObj(rowPuntoServicio("CODIGO"), GetType(String))
                                    detallePuntoServicio.Descripcion = Util.AtribuirValorObj(rowPuntoServicio("DESCRIPCION"), GetType(String))
                                    puntoServicio.Detalles.Add(detallePuntoServicio)
                                End If

                                'Busco en los detalles si hay errores para agregar el tipo de resultado al cliente
                                If puntoServicio.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "3") Then
                                    puntoServicio.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorAplicacion
                                    respuesta.Resultado.Codigo = puntoServicio.TipoResultado
                                ElseIf puntoServicio.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "2") Then
                                    puntoServicio.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.ErrorNegocio
                                    respuesta.Resultado.Codigo = puntoServicio.TipoResultado
                                ElseIf puntoServicio.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = "0") Then
                                    puntoServicio.TipoResultado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoResultado.Exito
                                    respuesta.Resultado.Codigo = puntoServicio.TipoResultado
                                End If
                            Next
                        Next
                    Next
                    If respuesta.Resultado Is Nothing Then
                        respuesta.Resultado = New Salida.Resultado()
                    End If

                    If respuesta.Resultado.Detalles Is Nothing Then
                        respuesta.Resultado.Detalles = New List(Of Salida.Detalle)()
                    End If

                    For Each rowDetalle As DataRow In ds.Tables("validaciones").Select("ENTIDAD IS NULL")
                        Dim objDetalle As New Salida.Detalle With {
                            .Codigo = Util.AtribuirValorObj(rowDetalle("CODIGO"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(rowDetalle("DESCRIPCION"), GetType(String))
                            }
                        respuesta.Resultado.Detalles.Add(objDetalle)
                    Next
                End If
            End If
            respuesta.Clientes = clientes
        End Sub
        Private Shared Function sconfigurar_clientes(identifiacdorLlamada As String, peticion As Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PCLIENTE_{0}.sconfigurar_cliente", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identifiacdorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)

            'Cliente
            spw.AgregarParam("par$acod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$anel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$acod_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ades_descripcion", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$acod_tipo_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_codigo_bancario", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abol_banco_capital", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abol_banco_comision", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$anel_porc_comision", ProsegurDbType.Numero_Decimal, Nothing, , True)

            'Subcliente        
            spw.AgregarParam("par$ascli_cod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ascli_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$ascli_nel_index_scli", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$ascli_cod_subcliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ascli_des_descripcion", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'Punto Servicio     
            spw.AgregarParam("par$apser_cod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apser_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$apser_nel_index_scli", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$apser_nel_index_pto", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$apser_cod_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$apser_des_pto_servicio", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'Datos Bancarios
            spw.AgregarParam("par$aban_cod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aban_cod_entidad", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aban_nel_index_entidad", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aban_oid_identificador", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aban_cod_codigobanco", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aban_cod_codigoagencia", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aban_cod_numerocuenta", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aban_cod_tipo_cuenta", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aban_cod_documento", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aban_des_titularidad", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_cod_divisa", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aban_des_observaciones", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_bol_defecto", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional1", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional2", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional3", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional4", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional5", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional6", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional7", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aban_des_campoadicional8", ProsegurDbType.Descricao_Longa, Nothing, , True)

            'Codigo Ajeno
            spw.AgregarParam("par$aajen_cod_accion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aajen_cod_entidad", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aajen_nel_index_entidad", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aajen_cod_identificador", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aajen_cod_codigo_ajeno", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$aajen_cod_desc_ajeno", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aajen_bol_defecto", ProsegurDbType.Logico, Nothing, , True)

            'Direcciones
            spw.AgregarParam("par$adir_cod_entidad", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$adir_nel_index_entidad", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$adir_des_pais", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_prov", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_ciudad", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_num_tel", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_email", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_cod_fiscal", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$adir_cod_postal", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$adir_des_dic_1", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_dic_2", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cam_adicional_1", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cam_adicional_2", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cam_adicional_3", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cat_adicional_1", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cat_adicional_2", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$adir_des_cat_adicional_3", ProsegurDbType.Descricao_Longa, Nothing, , True)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser())
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            Dim indexCli As Integer = 0
            Dim indexSubCli As Integer = 0
            Dim indexPtoServicio As Integer = 0
            If peticion.Clientes IsNot Nothing AndAlso peticion.Clientes.Count > 0 Then
                spw.Param("par$acod_accion").AgregarValorArray(DBNull.Value)
                spw.Param("par$anel_index").AgregarValorArray(DBNull.Value)
                spw.Param("par$acod_cliente").AgregarValorArray(DBNull.Value)
                spw.Param("par$ades_descripcion").AgregarValorArray(DBNull.Value)
                spw.Param("par$acod_tipo_cliente").AgregarValorArray(DBNull.Value)
                spw.Param("par$acod_codigo_bancario").AgregarValorArray(DBNull.Value)
                spw.Param("par$abol_banco_capital").AgregarValorArray(DBNull.Value)
                spw.Param("par$abol_banco_comision").AgregarValorArray(DBNull.Value)
                spw.Param("par$anel_porc_comision").AgregarValorArray(DBNull.Value)

                For Each cli In peticion.Clientes
                    spw.Param("par$acod_accion").AgregarValorArray(cli.Accion.ToString().ToUpper())
                    spw.Param("par$anel_index").AgregarValorArray(indexCli)
                    spw.Param("par$acod_cliente").AgregarValorArray(cli.Codigo)
                    spw.Param("par$ades_descripcion").AgregarValorArray(cli.Descripcion)
                    spw.Param("par$acod_tipo_cliente").AgregarValorArray(cli.Tipo.RecuperarValor())
                    spw.Param("par$acod_codigo_bancario").AgregarValorArray(cli.CodigoBancario)

                    If cli.BancoCapital Is Nothing Then
                        If cli.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Alta Then
                            spw.Param("par$abol_banco_capital").AgregarValorArray(0)
                        Else
                            spw.Param("par$abol_banco_capital").AgregarValorArray(DBNull.Value)
                        End If
                    ElseIf cli.BancoCapital = "1" OrElse cli.BancoCapital.ToUpper = "TRUE" Then
                        spw.Param("par$abol_banco_capital").AgregarValorArray(1)
                    Else
                        spw.Param("par$abol_banco_capital").AgregarValorArray(DBNull.Value)

                    End If

                    If cli.BancoComision Is Nothing Then
                        If cli.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Alta Then
                            spw.Param("par$abol_banco_comision").AgregarValorArray(0)
                        Else
                            spw.Param("par$abol_banco_comision").AgregarValorArray(DBNull.Value)
                        End If
                    ElseIf cli.BancoComision = "1" OrElse cli.BancoComision.ToUpper = "TRUE" Then
                        spw.Param("par$abol_banco_comision").AgregarValorArray(1)
                    Else
                        spw.Param("par$abol_banco_comision").AgregarValorArray(DBNull.Value)

                    End If



                    If cli.PorcComisionCliente Is Nothing Then
                        spw.Param("par$anel_porc_comision").AgregarValorArray(DBNull.Value)
                    Else
                        spw.Param("par$anel_porc_comision").AgregarValorArray(cli.PorcComisionCliente)
                    End If

                    If cli.DatosBancarios IsNot Nothing AndAlso cli.DatosBancarios.Count > 0 Then
                        spw.Param("par$aban_cod_accion").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_entidad").AgregarValorArray("CLIENTE")
                        spw.Param("par$aban_nel_index_entidad").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_oid_identificador").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_codigobanco").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_codigoagencia").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_numerocuenta").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_tipo_cuenta").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_documento").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_titularidad").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_cod_divisa").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_observaciones").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional1").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional2").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional3").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional4").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional5").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional6").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional7").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_des_campoadicional8").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)

                        For Each cliDatoBancario In cli.DatosBancarios
                            spw.Param("par$aban_cod_accion").AgregarValorArray(cliDatoBancario.Accion.ToString().ToUpper())
                            spw.Param("par$aban_cod_entidad").AgregarValorArray("CLIENTE")
                            spw.Param("par$aban_nel_index_entidad").AgregarValorArray(indexCli)
                            spw.Param("par$aban_oid_identificador").AgregarValorArray(cliDatoBancario.Identificador)

                            AgregarParametroAlWrapper(spw, "par$aban_cod_codigobanco", cliDatoBancario.CodigoBanco)
                            AgregarParametroAlWrapper(spw, "par$aban_cod_codigoagencia", cliDatoBancario.CodigoAgencia)
                            AgregarParametroAlWrapper(spw, "par$aban_cod_numerocuenta", cliDatoBancario.NumeroCuenta)
                            AgregarParametroAlWrapper(spw, "par$aban_cod_tipo_cuenta", cliDatoBancario.Tipo)
                            AgregarParametroAlWrapper(spw, "par$aban_cod_documento", cliDatoBancario.NumeroDocumento)
                            AgregarParametroAlWrapper(spw, "par$aban_des_titularidad", cliDatoBancario.Titularidad)
                            AgregarParametroAlWrapper(spw, "par$aban_cod_divisa", cliDatoBancario.CodigoDivisa)
                            AgregarParametroAlWrapper(spw, "par$aban_des_observaciones", cliDatoBancario.Observaciones)

                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional1", cliDatoBancario.CampoAdicional1)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional2", cliDatoBancario.CampoAdicional2)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional3", cliDatoBancario.CampoAdicional3)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional4", cliDatoBancario.CampoAdicional4)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional5", cliDatoBancario.CampoAdicional5)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional6", cliDatoBancario.CampoAdicional6)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional7", cliDatoBancario.CampoAdicional7)
                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional8", cliDatoBancario.CampoAdicional8)

                            If cliDatoBancario.Patron Is Nothing Then
                                If cliDatoBancario.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                    spw.Param("par$aban_bol_defecto").AgregarValorArray(0)
                                Else
                                    spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)
                                End If
                            ElseIf cliDatoBancario.Patron = "1" OrElse cliDatoBancario.Patron.ToUpper = "TRUE" Then
                                spw.Param("par$aban_bol_defecto").AgregarValorArray(1)
                            Else
                                spw.Param("par$aban_bol_defecto").AgregarValorArray(0)

                            End If
                        Next

                    End If

                    If cli.CodigosAjenos IsNot Nothing AndAlso cli.CodigosAjenos.Count > 0 Then
                        spw.Param("par$aajen_cod_accion").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aajen_cod_entidad").AgregarValorArray("CLIENTE")
                        spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aajen_cod_identificador").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(DBNull.Value)
                        spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)

                        For Each cliAj In cli.CodigosAjenos
                            spw.Param("par$aajen_cod_accion").AgregarValorArray(cliAj.Accion.ToString().ToUpper())
                            spw.Param("par$aajen_cod_entidad").AgregarValorArray("CLIENTE")
                            spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(indexCli)
                            spw.Param("par$aajen_cod_identificador").AgregarValorArray(cliAj.CodigoIdentificador)
                            spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(cliAj.Codigo)
                            spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(cliAj.Descripcion)
                            If cliAj.Patron Is Nothing Then
                                If cliAj.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                    spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)
                                Else
                                    spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)
                                End If
                            ElseIf cliAj.Patron = "1" OrElse cliAj.Patron.ToUpper = "TRUE" Then
                                spw.Param("par$aajen_bol_defecto").AgregarValorArray(1)
                            Else
                                spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)
                            End If
                        Next
                    End If


                    'Direcciones
                    If cli.Direccion IsNot Nothing Then

                        spw.Param("par$adir_cod_entidad").AgregarValorArray("CLIENTE")
                        spw.Param("par$adir_nel_index_entidad").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_pais").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_prov").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_ciudad").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_num_tel").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_email").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_cod_fiscal").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_cod_postal").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_dic_1").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_dic_2").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cam_adicional_1").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cam_adicional_2").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cam_adicional_3").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cat_adicional_1").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cat_adicional_2").AgregarValorArray(DBNull.Value)
                        spw.Param("par$adir_des_cat_adicional_3").AgregarValorArray(DBNull.Value)


                        AgregarParametroAlWrapper(spw, "par$adir_cod_entidad", "CLIENTE")
                        AgregarParametroAlWrapper(spw, "par$adir_nel_index_entidad", indexCli)
                        AgregarParametroAlWrapper(spw, "par$adir_des_pais", cli.Direccion.Pais)
                        AgregarParametroAlWrapper(spw, "par$adir_des_prov", cli.Direccion.Provincia)
                        AgregarParametroAlWrapper(spw, "par$adir_des_ciudad", cli.Direccion.Ciudad)
                        AgregarParametroAlWrapper(spw, "par$adir_des_num_tel", cli.Direccion.Telefono)
                        AgregarParametroAlWrapper(spw, "par$adir_cod_fiscal", cli.Direccion.CodigoFiscal)
                        AgregarParametroAlWrapper(spw, "par$adir_des_email", cli.Direccion.Email)
                        AgregarParametroAlWrapper(spw, "par$adir_cod_postal", cli.Direccion.CodigoPostal)
                        AgregarParametroAlWrapper(spw, "par$adir_des_dic_1", cli.Direccion.DireccionLinea1)
                        AgregarParametroAlWrapper(spw, "par$adir_des_dic_2", cli.Direccion.DireccionLinea2)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_1", cli.Direccion.CampoAdicional1)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_2", cli.Direccion.CampoAdicional2)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_3", cli.Direccion.CampoAdicional3)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_1", cli.Direccion.CategoriaAdicional1)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_2", cli.Direccion.CategoriaAdicional2)
                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_3", cli.Direccion.CategoriaAdicional3)
                    End If

                    If cli.SubClientes IsNot Nothing AndAlso cli.SubClientes.Count > 0 Then

                        spw.Param("par$ascli_cod_accion").AgregarValorArray(DBNull.Value)
                        spw.Param("par$ascli_nel_index").AgregarValorArray(DBNull.Value)
                        spw.Param("par$ascli_nel_index_scli").AgregarValorArray(DBNull.Value)
                        spw.Param("par$ascli_cod_subcliente").AgregarValorArray(DBNull.Value)
                        spw.Param("par$ascli_des_descripcion").AgregarValorArray(DBNull.Value)
                        For Each subCli In cli.SubClientes

                            spw.Param("par$ascli_cod_accion").AgregarValorArray(subCli.Accion.ToString().ToUpper())
                            spw.Param("par$ascli_nel_index").AgregarValorArray(indexCli)
                            spw.Param("par$ascli_nel_index_scli").AgregarValorArray(indexSubCli)
                            spw.Param("par$ascli_cod_subcliente").AgregarValorArray(subCli.Codigo)
                            spw.Param("par$ascli_des_descripcion").AgregarValorArray(subCli.Descripcion)

                            If subCli.DatosBancarios IsNot Nothing AndAlso subCli.DatosBancarios.Count > 0 Then

                                spw.Param("par$aban_cod_accion").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_entidad").AgregarValorArray("SUBCLIENTE")
                                spw.Param("par$aban_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_oid_identificador").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_codigobanco").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_codigoagencia").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_numerocuenta").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_tipo_cuenta").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_documento").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_titularidad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_cod_divisa").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_observaciones").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional1").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional2").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional3").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional4").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional5").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional6").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional7").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_des_campoadicional8").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)

                                For Each subCliDatoBancario In subCli.DatosBancarios
                                    spw.Param("par$aban_cod_accion").AgregarValorArray(subCliDatoBancario.Accion.ToString().ToUpper())
                                    spw.Param("par$aban_cod_entidad").AgregarValorArray("SUBCLIENTE")
                                    spw.Param("par$aban_nel_index_entidad").AgregarValorArray(indexSubCli)
                                    spw.Param("par$aban_oid_identificador").AgregarValorArray(subCliDatoBancario.Identificador)

                                    AgregarParametroAlWrapper(spw, "par$aban_cod_codigobanco", subCliDatoBancario.CodigoBanco)
                                    AgregarParametroAlWrapper(spw, "par$aban_cod_codigoagencia", subCliDatoBancario.CodigoAgencia)
                                    AgregarParametroAlWrapper(spw, "par$aban_cod_numerocuenta", subCliDatoBancario.NumeroCuenta)
                                    AgregarParametroAlWrapper(spw, "par$aban_cod_tipo_cuenta", subCliDatoBancario.Tipo)
                                    AgregarParametroAlWrapper(spw, "par$aban_cod_documento", subCliDatoBancario.NumeroDocumento)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_titularidad", subCliDatoBancario.Titularidad)
                                    AgregarParametroAlWrapper(spw, "par$aban_cod_divisa", subCliDatoBancario.CodigoDivisa)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_observaciones", subCliDatoBancario.Observaciones)

                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional1", subCliDatoBancario.CampoAdicional1)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional2", subCliDatoBancario.CampoAdicional2)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional3", subCliDatoBancario.CampoAdicional3)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional4", subCliDatoBancario.CampoAdicional4)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional5", subCliDatoBancario.CampoAdicional5)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional6", subCliDatoBancario.CampoAdicional6)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional7", subCliDatoBancario.CampoAdicional7)
                                    AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional8", subCliDatoBancario.CampoAdicional8)

                                    If subCliDatoBancario.Patron Is Nothing Then
                                        If subCliDatoBancario.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                            spw.Param("par$aban_bol_defecto").AgregarValorArray(0)
                                        Else
                                            spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)
                                        End If
                                    ElseIf subCliDatoBancario.Patron = "1" OrElse subCliDatoBancario.Patron.ToUpper = "TRUE" Then
                                        spw.Param("par$aban_bol_defecto").AgregarValorArray(1)
                                    Else
                                        spw.Param("par$aban_bol_defecto").AgregarValorArray(0)

                                    End If
                                Next

                            End If

                            If subCli.CodigosAjenos IsNot Nothing AndAlso subCli.CodigosAjenos.Count > 0 Then

                                spw.Param("par$aajen_cod_accion").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aajen_cod_entidad").AgregarValorArray("SUBCLIENTE")
                                spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aajen_cod_identificador").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(DBNull.Value)
                                spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)

                                For Each subCliAj In subCli.CodigosAjenos
                                    spw.Param("par$aajen_cod_accion").AgregarValorArray(subCliAj.Accion.ToString().ToUpper())
                                    spw.Param("par$aajen_cod_entidad").AgregarValorArray("SUBCLIENTE")
                                    spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(indexSubCli)
                                    spw.Param("par$aajen_cod_identificador").AgregarValorArray(subCliAj.CodigoIdentificador)
                                    spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(subCliAj.Codigo)
                                    spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(subCliAj.Descripcion)
                                    If subCliAj.Patron Is Nothing Then
                                        If subCliAj.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                            spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)
                                        Else
                                            spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)
                                        End If
                                    ElseIf subCliAj.Patron = "1" OrElse subCliAj.Patron.ToUpper = "TRUE" Then
                                        spw.Param("par$aajen_bol_defecto").AgregarValorArray(1)
                                    Else
                                        spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)

                                    End If
                                Next
                            End If


                            'Direcciones
                            If subCli.Direccion IsNot Nothing Then

                                spw.Param("par$adir_cod_entidad").AgregarValorArray("SUBCLIENTE")
                                spw.Param("par$adir_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_pais").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_prov").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_ciudad").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_num_tel").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_email").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_cod_fiscal").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_cod_postal").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_dic_1").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_dic_2").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cam_adicional_1").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cam_adicional_2").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cam_adicional_3").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cat_adicional_1").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cat_adicional_2").AgregarValorArray(DBNull.Value)
                                spw.Param("par$adir_des_cat_adicional_3").AgregarValorArray(DBNull.Value)

                                AgregarParametroAlWrapper(spw, "par$adir_cod_entidad", "SUBCLIENTE")
                                AgregarParametroAlWrapper(spw, "par$adir_nel_index_entidad", indexSubCli)
                                AgregarParametroAlWrapper(spw, "par$adir_des_pais", subCli.Direccion.Pais)
                                AgregarParametroAlWrapper(spw, "par$adir_des_prov", subCli.Direccion.Provincia)
                                AgregarParametroAlWrapper(spw, "par$adir_des_ciudad", subCli.Direccion.Ciudad)
                                AgregarParametroAlWrapper(spw, "par$adir_des_num_tel", subCli.Direccion.Telefono)
                                AgregarParametroAlWrapper(spw, "par$adir_des_email", subCli.Direccion.Email)
                                AgregarParametroAlWrapper(spw, "par$adir_cod_fiscal", subCli.Direccion.CodigoFiscal)
                                AgregarParametroAlWrapper(spw, "par$adir_cod_postal", subCli.Direccion.CodigoPostal)
                                AgregarParametroAlWrapper(spw, "par$adir_des_dic_1", subCli.Direccion.DireccionLinea1)
                                AgregarParametroAlWrapper(spw, "par$adir_des_dic_2", subCli.Direccion.DireccionLinea2)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_1", subCli.Direccion.CampoAdicional1)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_2", subCli.Direccion.CampoAdicional2)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_3", subCli.Direccion.CampoAdicional3)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_1", subCli.Direccion.CategoriaAdicional1)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_2", subCli.Direccion.CategoriaAdicional2)
                                AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_3", subCli.Direccion.CategoriaAdicional3)

                            End If

                            If subCli.PuntosServicio IsNot Nothing AndAlso subCli.PuntosServicio.Count > 0 Then
                                spw.Param("par$apser_cod_accion").AgregarValorArray(DBNull.Value)
                                spw.Param("par$apser_nel_index").AgregarValorArray(DBNull.Value)
                                spw.Param("par$apser_nel_index_scli").AgregarValorArray(DBNull.Value)
                                spw.Param("par$apser_nel_index_pto").AgregarValorArray(DBNull.Value)
                                spw.Param("par$apser_cod_pto_servicio").AgregarValorArray(DBNull.Value)
                                spw.Param("par$apser_des_pto_servicio").AgregarValorArray(DBNull.Value)
                                For Each pto In subCli.PuntosServicio

                                    spw.Param("par$apser_cod_accion").AgregarValorArray(pto.Accion.ToString().ToUpper())
                                    spw.Param("par$apser_nel_index").AgregarValorArray(indexCli)
                                    spw.Param("par$apser_nel_index_scli").AgregarValorArray(indexSubCli)
                                    spw.Param("par$apser_nel_index_pto").AgregarValorArray(indexPtoServicio)
                                    spw.Param("par$apser_cod_pto_servicio").AgregarValorArray(pto.Codigo)
                                    spw.Param("par$apser_des_pto_servicio").AgregarValorArray(pto.Descripcion)

                                    If pto.DatosBancarios IsNot Nothing AndAlso pto.DatosBancarios.Count > 0 Then

                                        spw.Param("par$aban_cod_accion").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_entidad").AgregarValorArray("PUNTOSERVICIO")
                                        spw.Param("par$aban_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_oid_identificador").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_codigobanco").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_codigoagencia").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_numerocuenta").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_tipo_cuenta").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_documento").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_titularidad").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_cod_divisa").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_observaciones").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional1").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional2").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional3").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional4").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional5").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional6").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional7").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_des_campoadicional8").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)

                                        For Each ptoDatoBancario In pto.DatosBancarios
                                            spw.Param("par$aban_cod_accion").AgregarValorArray(ptoDatoBancario.Accion.ToString().ToUpper())
                                            spw.Param("par$aban_cod_entidad").AgregarValorArray("PUNTOSERVICIO")
                                            spw.Param("par$aban_nel_index_entidad").AgregarValorArray(indexPtoServicio)
                                            spw.Param("par$aban_oid_identificador").AgregarValorArray(ptoDatoBancario.Identificador)

                                            AgregarParametroAlWrapper(spw, "par$aban_cod_codigobanco", ptoDatoBancario.CodigoBanco)
                                            AgregarParametroAlWrapper(spw, "par$aban_cod_codigoagencia", ptoDatoBancario.CodigoAgencia)
                                            AgregarParametroAlWrapper(spw, "par$aban_cod_numerocuenta", ptoDatoBancario.NumeroCuenta)
                                            AgregarParametroAlWrapper(spw, "par$aban_cod_tipo_cuenta", ptoDatoBancario.Tipo)
                                            AgregarParametroAlWrapper(spw, "par$aban_cod_documento", ptoDatoBancario.NumeroDocumento)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_titularidad", ptoDatoBancario.Titularidad)
                                            AgregarParametroAlWrapper(spw, "par$aban_cod_divisa", ptoDatoBancario.CodigoDivisa)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_observaciones", ptoDatoBancario.Observaciones)

                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional1", ptoDatoBancario.CampoAdicional1)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional2", ptoDatoBancario.CampoAdicional2)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional3", ptoDatoBancario.CampoAdicional3)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional4", ptoDatoBancario.CampoAdicional4)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional5", ptoDatoBancario.CampoAdicional5)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional6", ptoDatoBancario.CampoAdicional6)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional7", ptoDatoBancario.CampoAdicional7)
                                            AgregarParametroAlWrapper(spw, "par$aban_des_campoadicional8", ptoDatoBancario.CampoAdicional8)

                                            If ptoDatoBancario.Patron Is Nothing Then
                                                If ptoDatoBancario.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                                    spw.Param("par$aban_bol_defecto").AgregarValorArray(0)
                                                Else
                                                    spw.Param("par$aban_bol_defecto").AgregarValorArray(DBNull.Value)
                                                End If
                                            ElseIf ptoDatoBancario.Patron = "1" OrElse ptoDatoBancario.Patron.ToUpper = "TRUE" Then
                                                spw.Param("par$aban_bol_defecto").AgregarValorArray(1)
                                            Else
                                                spw.Param("par$aban_bol_defecto").AgregarValorArray(0)

                                            End If
                                        Next

                                    End If


                                    If pto.CodigosAjenos IsNot Nothing AndAlso pto.CodigosAjenos.Count > 0 Then

                                        spw.Param("par$aajen_cod_accion").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aajen_cod_entidad").AgregarValorArray("PUNTOSERVICIO")
                                        spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aajen_cod_identificador").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)

                                        For Each ptoAj In pto.CodigosAjenos
                                            spw.Param("par$aajen_cod_accion").AgregarValorArray(ptoAj.Accion.ToString().ToUpper())
                                            spw.Param("par$aajen_cod_entidad").AgregarValorArray("PUNTOSERVICIO")
                                            spw.Param("par$aajen_nel_index_entidad").AgregarValorArray(indexPtoServicio)
                                            spw.Param("par$aajen_cod_identificador").AgregarValorArray(ptoAj.CodigoIdentificador)
                                            spw.Param("par$aajen_cod_codigo_ajeno").AgregarValorArray(ptoAj.Codigo)
                                            spw.Param("par$aajen_cod_desc_ajeno").AgregarValorArray(ptoAj.Descripcion)
                                            If ptoAj.Patron Is Nothing Then
                                                If ptoAj.Accion = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Modificar Then
                                                    spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)
                                                Else
                                                    spw.Param("par$aajen_bol_defecto").AgregarValorArray(DBNull.Value)
                                                End If
                                            ElseIf ptoAj.Patron = "1" OrElse ptoAj.Patron.ToUpper = "TRUE" Then
                                                spw.Param("par$aajen_bol_defecto").AgregarValorArray(1)
                                            Else
                                                spw.Param("par$aajen_bol_defecto").AgregarValorArray(0)

                                            End If
                                        Next

                                    End If


                                    'Direcciones
                                    If pto.Direccion IsNot Nothing Then

                                        spw.Param("par$adir_cod_entidad").AgregarValorArray("PUNTOSERVICIO")
                                        spw.Param("par$adir_nel_index_entidad").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_pais").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_prov").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_ciudad").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_num_tel").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_email").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_cod_fiscal").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_cod_postal").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_dic_1").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_dic_2").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cam_adicional_1").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cam_adicional_2").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cam_adicional_3").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cat_adicional_1").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cat_adicional_2").AgregarValorArray(DBNull.Value)
                                        spw.Param("par$adir_des_cat_adicional_3").AgregarValorArray(DBNull.Value)

                                        AgregarParametroAlWrapper(spw, "par$adir_cod_entidad", "PUNTOSERVICIO")
                                        AgregarParametroAlWrapper(spw, "par$adir_nel_index_entidad", indexPtoServicio)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_pais", pto.Direccion.Pais)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_prov", pto.Direccion.Provincia)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_ciudad", pto.Direccion.Ciudad)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_num_tel", pto.Direccion.Telefono)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_email", pto.Direccion.Email)
                                        AgregarParametroAlWrapper(spw, "par$adir_cod_fiscal", pto.Direccion.CodigoFiscal)
                                        AgregarParametroAlWrapper(spw, "par$adir_cod_postal", pto.Direccion.CodigoPostal)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_dic_1", pto.Direccion.DireccionLinea1)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_dic_2", pto.Direccion.DireccionLinea2)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_1", pto.Direccion.CampoAdicional1)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_2", pto.Direccion.CampoAdicional2)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cam_adicional_3", pto.Direccion.CampoAdicional3)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_1", pto.Direccion.CategoriaAdicional1)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_2", pto.Direccion.CategoriaAdicional2)
                                        AgregarParametroAlWrapper(spw, "par$adir_des_cat_adicional_3", pto.Direccion.CategoriaAdicional3)
                                    End If

                                    indexPtoServicio += 1
                                Next

                            End If

                            indexSubCli += 1
                        Next


                    End If

                    indexCli += 1
                Next
            End If
            Return spw
        End Function
        ''' <summary>
        ''' Se encarga de agregar parametros en el wrapper considerando los valores vacios
        ''' </summary>
        ''' <param name="spw"> Objeto SPWrapper </param>
        ''' <param name="nombreParametro"> Nombre del parametro para el SP</param>
        ''' <param name="valorParametro"> Valor del parametro para el SP </param>
        Private Shared Sub AgregarParametroAlWrapper(ByRef spw As SPWrapper, ByRef nombreParametro As String, ByRef valorParametro As String)
            If valorParametro Is Nothing Then
                spw.Param(nombreParametro).AgregarValorArray(DBNull.Value)
            ElseIf valorParametro.Equals(String.Empty) Then
                spw.Param(nombreParametro).AgregarValorArray(Prosegur.Genesis.Comon.Constantes.CONST_VACIO_PARA_BBDD)
            Else
                spw.Param(nombreParametro).AgregarValorArray(valorParametro)
            End If
        End Sub
    End Class
End Namespace