Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class OrdenServicio
#Region "[CONSULTAR]"
    Public Shared Function RecuperarOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarOrdenesServicio.Peticion) As List(Of Genesis.Comon.Clases.OrdenServicio)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarOrdenesServicio(pPeticion)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarOrdenesServicio(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Shared Function armarWrapperRecuperarOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarOrdenesServicio.Peticion) As SPWrapper

        Dim SP As String = String.Format("SAPR_PORDENES_SERVICIO_{0}.SRECUPERAR_ORDENES", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, pPeticion.CodigoUsuario, , False)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                Tradutor.CulturaSistema.Name,
                                                                If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

        spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Longa, pPeticion.CodigoPais, , False)

        spw.AgregarParam("par$aoid_cliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_subcliente", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
        spw.AgregarParam("par$aoid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

        spw.AgregarParam("par$contrato", ProsegurDbType.Descricao_Longa, pPeticion.Contrato, , False)
        spw.AgregarParam("par$orden_servicio", ProsegurDbType.Descricao_Longa, pPeticion.OrdenServicio, , False)
        spw.AgregarParam("par$acod_producto", ProsegurDbType.Descricao_Longa, Nothing, , True)

        spw.AgregarParam("par$rc_ordenes", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ordenes")

        spw.AgregarParam("par$fecha_inicio", ProsegurDbType.Data_Hora, pPeticion.FechaInicio, , False)
        spw.AgregarParam("par$fecha_fin", ProsegurDbType.Data_Hora, pPeticion.FechaFin, , False)
        spw.AgregarParam("par$estado", ProsegurDbType.Logico, pPeticion.Estado, , False)

        For Each oid_cliente As String In pPeticion.OidClientes
            spw.Param("par$aoid_cliente").AgregarValorArray(oid_cliente)
        Next

        For Each oid_subcliente As String In pPeticion.OidSubClientes
            spw.Param("par$aoid_subcliente").AgregarValorArray(oid_subcliente)
        Next

        For Each oid_pto_servicio As String In pPeticion.OidPuntosServicios
            spw.Param("par$aoid_pto_servicio").AgregarValorArray(oid_pto_servicio)
        Next

        For Each producto As String In pPeticion.CodigosProductos
            spw.Param("par$acod_producto").AgregarValorArray(producto)
        Next

        Return spw
    End Function

    Private Shared Function PoblarRespuestaRecuperarOrdenesServicio(ds As DataSet) As List(Of Genesis.Comon.Clases.OrdenServicio)
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicio)

        If ds.Tables("ordenes") IsNot Nothing Then
            For Each row In ds.Tables("ordenes").Rows
                Dim ordenServicioG = New Genesis.Comon.Clases.OrdenServicio
                Util.AtribuirValorObjeto(ordenServicioG.OidAcuerdoServicio, row("OID_ACUERDO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.Cliente, row("CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.SubCliente, row("SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.PuntoServicio, row("PUNTO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.Contrato, row("CONTRATO"), GetType(String))

                Util.AtribuirValorObjeto(ordenServicioG.OrdenServicio, row("ORDENSERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.CodigoProducto, row("DES_PRODUCT_CODE"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.Producto, row("PRODUCTO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.FechaReferencia, row("FECHAREFERENCIA"), GetType(DateTime))
                Util.AtribuirValorObjeto(ordenServicioG.FechaCalculo, row("FECHACALCULO"), GetType(DateTime))
                Util.AtribuirValorObjeto(ordenServicioG.Estado, row("ESTADO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioG.OidSaldoAcuerdoRef, row("OID_SALDO_ACUERDO_REF"), GetType(String))

                'If (Not Resultado.Any(Function(a) a.OidAcuerdoServicio = ordenServicioG.OidAcuerdoServicio)) Then
                Resultado.Add(ordenServicioG)
                'End If
            Next


        End If

        Return Resultado
    End Function
    Public Shared Function RecuperarDetallesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarDetallesOrdenesServicio.Peticion) As List(Of Genesis.Comon.Clases.OrdenServicioDetalle)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarDetallesOrdenesServicio(pPeticion)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarDetallesOrdenesServicio(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Shared Function armarWrapperRecuperarDetallesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarDetallesOrdenesServicio.Peticion) As SPWrapper

        Dim SP As String = String.Format("SAPR_PORDENES_SERVICIO_{0}.SRECUPERAR_DETALLES", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_acuerdo_servicio", ProsegurDbType.Identificador_Alfanumerico, pPeticion.Oid_acuerdo_servicio, , False)
        spw.AgregarParam("par$oid_saldo_acuerdo_ref", ProsegurDbType.Identificador_Alfanumerico, pPeticion.Oid_saldo_acuerdo_ref, , False)
        spw.AgregarParam("par$product_code", ProsegurDbType.Descricao_Longa, pPeticion.ProductCode, , False)

        spw.AgregarParam("par$rc_detalles", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "detalles")

        Return spw
    End Function
    Private Shared Function PoblarRespuestaRecuperarDetallesOrdenesServicio(ds As DataSet) As List(Of Genesis.Comon.Clases.OrdenServicioDetalle)
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicioDetalle)

        If ds.Tables("detalles") IsNot Nothing Then
            For Each row In ds.Tables("detalles").Rows
                Dim ordenServicioGD = New Genesis.Comon.Clases.OrdenServicioDetalle
                'Util.AtribuirValorObjeto(ordenServicioGD.OidSaldoAcuerdo, row("OID_SALDO_ACUERDO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioGD.Tipo, row("TIPO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioGD.Cantidad, row("CANTIDAD"), GetType(Integer))
                Util.AtribuirValorObjeto(ordenServicioGD.Divisa, row("DIVISA"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioGD.TipoMercancia, row("TIPOMERCANCIA"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioGD.Total, row("TOTAL"), GetType(Integer))

                Resultado.Add(ordenServicioGD)
            Next
        End If

        Return Resultado
    End Function

    Public Shared Function RecuperarNotificacionesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarNotificacionesOrdenesServicio.Peticion) As List(Of Genesis.Comon.Clases.OrdenServicioNotificacion)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarNotificacionesOrdenesServicio(pPeticion)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarNotificacionesOrdenesServicio(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Shared Function armarWrapperRecuperarNotificacionesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarNotificacionesOrdenesServicio.Peticion) As SPWrapper

        Dim SP As String = String.Format("SAPR_PORDENES_SERVICIO_{0}.SRECUPERAR_NOTIFICACIONES", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_saldo_acuerdo_ref", ProsegurDbType.Identificador_Alfanumerico, pPeticion.Oid_saldo_acuerdo_ref, , False)

        spw.AgregarParam("par$rc_notificaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "notificaciones")

        Return spw
    End Function
    Private Shared Function PoblarRespuestaRecuperarNotificacionesOrdenesServicio(ds As DataSet) As List(Of Genesis.Comon.Clases.OrdenServicioNotificacion)
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicioNotificacion)

        If ds.Tables("notificaciones") IsNot Nothing Then
            For Each row In ds.Tables("notificaciones").Rows
                Dim ordenServicioN = New Genesis.Comon.Clases.OrdenServicioNotificacion

                Util.AtribuirValorObjeto(ordenServicioN.OidIntegracion, row("OIDINTEGRACION"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.Fecha, row("FECHA"), GetType(DateTime))
                Util.AtribuirValorObjeto(ordenServicioN.Estado, row("ESTADO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.Intentos, row("INTENTOS"), GetType(Integer))
                Util.AtribuirValorObjeto(ordenServicioN.UltimoError, row("ULTIMOERROR"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.OidSaldoAcuerdoRef, row("OIDSALDOACUERDOREF"), GetType(String))

                Resultado.Add(ordenServicioN)
            Next
        End If

        Return Resultado
    End Function

    Public Shared Function RecuperarDetNotificacionesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarDetNotificacionesOrdenesServicio.Peticion) As List(Of Genesis.Comon.Clases.OrdenServicioDetNotificacion)
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecuperarDetNotificacionesOrdenesServicio(pPeticion)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecuperarDetNotificacionesOrdenesServicio(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Shared Function armarWrapperRecuperarDetNotificacionesOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarDetNotificacionesOrdenesServicio.Peticion) As SPWrapper

        Dim SP As String = String.Format("SAPR_PORDENES_SERVICIO_{0}.SRECUPERAR_NOTIFICACIONES_DET", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_integracion", ProsegurDbType.Identificador_Alfanumerico, pPeticion.Oid_integracion, , False)

        spw.AgregarParam("par$rc_notificaciones_det", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "notificaciones_det")

        Return spw
    End Function
    Private Shared Function PoblarRespuestaRecuperarDetNotificacionesOrdenesServicio(ds As DataSet) As List(Of Genesis.Comon.Clases.OrdenServicioDetNotificacion)
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicioDetNotificacion)

        If ds.Tables("notificaciones_det") IsNot Nothing Then
            For Each row In ds.Tables("notificaciones_det").Rows
                Dim ordenServicioN = New Genesis.Comon.Clases.OrdenServicioDetNotificacion

                Util.AtribuirValorObjeto(ordenServicioN.OidIntegracion, row("OIDINTEGRACION"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.NumeroDeIntento, row("NUMERODEINTENTO"), GetType(Integer))
                Util.AtribuirValorObjeto(ordenServicioN.Fecha, row("FECHA"), GetType(DateTime))
                Util.AtribuirValorObjeto(ordenServicioN.Estado, row("ESTADO"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.Observaciones, row("OBSERVACIONES"), GetType(String))
                Util.AtribuirValorObjeto(ordenServicioN.BError, row("ERROR"), GetType(Integer))

                Resultado.Add(ordenServicioN)
            Next
        End If

        Return Resultado
    End Function


    Public Shared Function RecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As Boolean
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperRecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref, cod_usuario)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaRecalcularSaldoAcuerdo(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Shared Function armarWrapperRecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As SPWrapper

        Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecalcular_saldo_acuerdo", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_saldo_acuerdo_ref", ProsegurDbType.Identificador_Alfanumerico, oid_saldo_acuerdo_ref, , False)
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, cod_usuario, , False)
        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

        Return spw
    End Function
    Private Shared Function PoblarRespuestaRecalcularSaldoAcuerdo(ds As DataSet) As Boolean
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicioDetNotificacion)
        If ds.Tables("par$rc_validaciones") IsNot Nothing AndAlso ds.Tables("par$rc_validaciones").Rows.Count >= 1 Then
            Return False
        End If
        Return True
    End Function


    Public Shared Function NotificarSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As Boolean
        Dim ds As DataSet = Nothing
        Dim spw As SPWrapper = Nothing
        Try
            spw = armarWrapperNotificarSaldoAcuerdo(oid_saldo_acuerdo_ref, cod_usuario)
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
            Return PoblarRespuestaNotificarSaldoAcuerdo(ds)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Shared Function armarWrapperNotificarSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As SPWrapper

        Dim SP As String = String.Format("SAPR_PSALDOS_{0}.snotificar_saldo_acuerdo", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_saldo_acuerdo_ref", ProsegurDbType.Identificador_Alfanumerico, oid_saldo_acuerdo_ref, , False)
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, cod_usuario, , False)
        spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

        Return spw
    End Function
    Private Shared Function PoblarRespuestaNotificarSaldoAcuerdo(ds As DataSet) As Boolean
        Dim Resultado = New List(Of Genesis.Comon.Clases.OrdenServicioDetNotificacion)
        If ds.Tables("par$rc_validaciones") IsNot Nothing AndAlso ds.Tables("par$rc_validaciones").Rows.Count >= 1 Then
            Return False
        End If
        Return True
    End Function
#End Region
End Class
