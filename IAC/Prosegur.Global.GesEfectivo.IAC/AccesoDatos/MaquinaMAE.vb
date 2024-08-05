Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Public Class MaquinaMAE

#Region "[CONSULTAS]"

    Public Shared Function GetMaquinas(objPeticion As ContractoServicio.Maquina.GetMaquina.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As List(Of Comon.Clases.Maquina)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.BuscaMaquinasMAE
        cmd.CommandType = CommandType.Text

        If objPeticion.OidClientes IsNot Nothing AndAlso objPeticion.OidClientes.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidClientes, "OID_CLIENTE", cmd, "AND", "CLI")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidClientes, "OID_CLIENTE", cmd, "WHERE", "CLI")
            End If
        End If

        If objPeticion.OidSubClientes IsNot Nothing AndAlso objPeticion.OidSubClientes.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidSubClientes, "OID_SUBCLIENTE", cmd, "AND", "SCLI")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidSubClientes, "OID_SUBCLIENTE", cmd, "WHERE", "SCLI")
            End If
        End If

        If objPeticion.OidPuntoServicio IsNot Nothing AndAlso objPeticion.OidPuntoServicio.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidPuntoServicio, "OID_PTO_SERVICIO", cmd, "AND", "PTO")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidPuntoServicio, "OID_PTO_SERVICIO", cmd, "WHERE", "PTO")
            End If
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidDelegacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND PL.OID_DELEGACION = :OID_DELEGACION "
            Else
                cmd.CommandText &= " WHERE PL.OID_DELEGACION = :OID_DELEGACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidDelegacion))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidPlanta) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND S.OID_PLANTA = :OID_PLANTA "
            Else
                cmd.CommandText &= " WHERE S.OID_PLANTA = :OID_PLANTA "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidPlanta))
        End If

        If Not String.IsNullOrEmpty(objPeticion.Descripcion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(S.DES_SECTOR) LIKE :DES_SECTOR "
            Else
                cmd.CommandText &= " WHERE UPPER(S.DES_SECTOR) LIKE :DES_SECTOR "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Descripcion.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.DeviceID) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(M.COD_IDENTIFICACION) LIKE :COD_IDENTIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(M.COD_IDENTIFICACION) LIKE :COD_IDENTIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICACION", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DeviceID.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidFabricante) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND F.OID_FABRICANTE = :OID_FABRICANTE"
            Else
                cmd.CommandText &= " WHERE F.OID_FABRICANTE = :OID_FABRICANTE"
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_FABRICANTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidFabricante))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidModelo) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND MD.OID_MODELO = :OID_MODELO"
            Else
                cmd.CommandText &= " WHERE MD.OID_MODELO = :OID_MODELO"
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODELO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidModelo))
        End If

        If objPeticion.BolVigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND M.BOL_ACTIVO = :BOL_ACTIVO "
            Else
                cmd.CommandText &= " WHERE M.BOL_ACTIVO = :BOL_ACTIVO "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.BolVigente))
        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        'cria o objeto de Maquina
        Dim lstMaquina As New List(Of Comon.Clases.Maquina)

        Dim dtMaquina As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtMaquina IsNot Nothing _
            AndAlso dtMaquina.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtMaquina.Rows
                Dim identificador As String = String.Empty
                Dim ptoServicio As String = String.Empty

                Util.AtribuirValorObjeto(identificador, dr("OID_MAQUINA"), GetType(String))
                Dim objMaquina As Comon.Clases.Maquina

                If Not lstMaquina.Exists(Function(a) a.Identificador = identificador AndAlso a.PtoServicio.Identificador = ptoServicio) Then
                    objMaquina = PopularGetMaquina(dr)
                    lstMaquina.Add(objMaquina)
                Else
                    objMaquina = lstMaquina.Find(Function(a) a.Identificador = identificador AndAlso a.PtoServicio.Identificador = ptoServicio)
                End If

                If Not String.IsNullOrEmpty(dr("DES_PLANIFICACION").ToString()) Then
                    Dim planMaq As New Comon.Clases.PlanXmaquina
                    planMaq.Planificacion = New Comon.Clases.Planificacion
                    Util.AtribuirValorObjeto(planMaq.Planificacion.Descripcion, dr("DES_PLANIFICACION"), GetType(String))
                    Util.AtribuirValorObjeto(planMaq.FechaHoraVigenciaInicio, dr("FYH_VIGENCIA_INICIO"), GetType(DateTime))
                    Util.AtribuirValorObjeto(planMaq.FechaHoraVigenciaFin, dr("FYH_VIGENCIA_FIN"), GetType(DateTime))
                    objMaquina.PlanMaquina.Add(planMaq)
                End If
            Next
        End If

        Return lstMaquina
    End Function

    Public Shared Function GetMaquinasSinPlanificacion(objPeticion As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.BuscaMaquinas
        cmd.CommandType = CommandType.Text

        If objPeticion.OidClientes IsNot Nothing AndAlso objPeticion.OidClientes.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidClientes, "OID_CLIENTE", cmd, "AND", "CLI")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidClientes, "OID_CLIENTE", cmd, "WHERE", "CLI")
            End If
        End If

        If objPeticion.OidSubClientes IsNot Nothing AndAlso objPeticion.OidSubClientes.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidSubClientes, "OID_SUBCLIENTE", cmd, "AND", "SCLI")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidSubClientes, "OID_SUBCLIENTE", cmd, "WHERE", "SCLI")
            End If
        End If

        If objPeticion.OidPuntoServicio IsNot Nothing AndAlso objPeticion.OidPuntoServicio.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidPuntoServicio, "OID_PTO_SERVICIO", cmd, "AND", "PTO")
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidPuntoServicio, "OID_PTO_SERVICIO", cmd, "WHERE", "PTO")
            End If
        End If

        If Not String.IsNullOrEmpty(objPeticion.DeviceID) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(M.COD_IDENTIFICACION) LIKE :COD_IDENTIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(M.COD_IDENTIFICACION) LIKE :COD_IDENTIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICACION", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.DeviceID.ToUpper() & "%"))
        End If

        If objPeticion.OidMaquinasSelecionadas IsNot Nothing AndAlso objPeticion.OidMaquinasSelecionadas.Count > 0 Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidMaquinasSelecionadas, "OID_MAQUINA", cmd, "AND", "M", "", True)
            Else
                cmd.CommandText += Util.MontarClausulaIn(objPeticion.OidMaquinasSelecionadas, "OID_MAQUINA", cmd, "WHERE", "M", "", True)
            End If
        End If

        If Not String.IsNullOrEmpty(objPeticion.Descripcion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(S.DES_SECTOR) LIKE :DES_SECTOR "
            Else
                cmd.CommandText &= " WHERE UPPER(S.DES_SECTOR) LIKE :DES_SECTOR "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SECTOR", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.Descripcion.ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidFabricante) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND F.OID_FABRICANTE = :OID_FABRICANTE"
            Else
                cmd.CommandText &= " WHERE F.OID_FABRICANTE = :OID_FABRICANTE"
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_FABRICANTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidFabricante))
        End If

        If Not String.IsNullOrEmpty(objPeticion.OidModelo) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND MD.OID_MODELO = :OID_MODELO"
            Else
                cmd.CommandText &= " WHERE MD.OID_MODELO = :OID_MODELO"
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODELO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidModelo))
        End If

        If objPeticion.BolVigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND M.BOL_ACTIVO = :BOL_ACTIVO "
            Else
                cmd.CommandText &= " WHERE M.BOL_ACTIVO = :BOL_ACTIVO "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.BolVigente))
        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Dim objRespuesta As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta
        'cria o objeto de Maquina
        Dim lstGetMaquina As New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)

        Dim dtMaquina As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtMaquina IsNot Nothing _
            AndAlso dtMaquina.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtMaquina.Rows
                Dim objMaquina As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina
                objMaquina = PopularGetMaquinaSinPlanificacion(dr)
                lstGetMaquina.Add(objMaquina)
            Next
        End If
        objRespuesta.Maquinas = lstGetMaquina

        Return objRespuesta
    End Function

    Public Shared Function GetMaquina(oidMaquina As String, deviceID As String) As Comon.Clases.Maquina
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaMaquinaMAE)
        cmd.CommandType = CommandType.Text


        If Not String.IsNullOrEmpty(deviceID) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(M.COD_IDENTIFICACION) = :COD_IDENTIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(M.COD_IDENTIFICACION) = :COD_IDENTIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICACION", ProsegurDbType.Identificador_Alfanumerico, deviceID.ToUpper()))
        End If
        If Not String.IsNullOrEmpty(oidMaquina) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND M.OID_MAQUINA = :OID_MAQUINA "
            Else
                cmd.CommandText &= " WHERE M.OID_MAQUINA = :OID_MAQUINA "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Identificador_Alfanumerico, oidMaquina))
        End If

        Dim dtMaquina As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' caso encontre algum registro
        If dtMaquina IsNot Nothing _
            AndAlso dtMaquina.Rows.Count > 0 Then
            Return PopularGetMaquina(dtMaquina.Rows(0))
        End If

        Return Nothing
    End Function


    Public Shared Function GetPlanxCanal(oidPlanificacion As String) As List(Of Comon.Clases.Canal)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaPlanificacionCanales)
        cmd.CommandType = CommandType.Text


        If Not String.IsNullOrEmpty(oidPlanificacion) Then
            cmd.CommandText &= " AND pcan.oid_planificacion = :OID_PLANIFICACION "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, oidPlanificacion))
        End If


        Dim dtRespuesta As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
        Dim lstRespuesta = New List(Of Comon.Clases.Canal)
        ' caso encontre algum registro
        If dtRespuesta IsNot Nothing _
            AndAlso dtRespuesta.Rows.Count > 0 Then

            For Each dr As DataRow In dtRespuesta.Rows

                Dim objCanal As Comon.Clases.Canal

                Dim auxOid As String = String.Empty
                Util.AtribuirValorObjeto(auxOid, dr("OID_CANAL"), GetType(String))
                objCanal = lstRespuesta.FirstOrDefault(Function(x) x.Identificador = auxOid)
                If objCanal Is Nothing Then

                    objCanal = New Comon.Clases.Canal
                    Util.AtribuirValorObjeto(objCanal.Identificador, dr("OID_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(objCanal.Codigo, dr("COD_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(objCanal.Descripcion, dr("DES_CANAL"), GetType(String))

                    objCanal.SubCanales = New ObservableCollection(Of Comon.Clases.SubCanal)
                    lstRespuesta.Add(objCanal)
                End If


                Dim objSubCanal As Comon.Clases.SubCanal = New Comon.Clases.SubCanal
                Util.AtribuirValorObjeto(objSubCanal.Identificador, dr("OID_SUBCANAL"), GetType(String))
                Util.AtribuirValorObjeto(objSubCanal.Codigo, dr("COD_SUBCANAL"), GetType(String))
                Util.AtribuirValorObjeto(objSubCanal.Descripcion, dr("DES_SUBCANAL"), GetType(String))

                objCanal.SubCanales.Add(objSubCanal)


            Next
            Return lstRespuesta
        End If

        Return Nothing
    End Function




    Public Shared Function GetMaquinaDetalle(oidMaquina As String) As ContractoServicio.Maquina.GetMaquinaDetalle.Maquina
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaMaquinaDetalleMAE)
        cmd.CommandType = CommandType.Text


        If Not String.IsNullOrEmpty(oidMaquina) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND M.OID_MAQUINA = :OID_MAQUINA "
            Else
                cmd.CommandText &= " WHERE M.OID_MAQUINA = :OID_MAQUINA "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Identificador_Alfanumerico, oidMaquina))
        End If

        Dim dtMaquina As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' caso encontre algum registro
        If dtMaquina IsNot Nothing _
            AndAlso dtMaquina.Rows.Count > 0 Then
            Dim objMaquina As ContractoServicio.Maquina.GetMaquinaDetalle.Maquina = Nothing

            ' percorrer registros encontrados
            For Each dr As DataRow In dtMaquina.Rows

                If objMaquina Is Nothing Then
                    objMaquina = New ContractoServicio.Maquina.GetMaquinaDetalle.Maquina
                    objMaquina = PopularGetMaquinaDetalle(dr)
                End If

                Dim objCliente = New Comon.Clases.Cliente With {.SubClientes = New ObservableCollection(Of Comon.Clases.SubCliente)}
                Util.AtribuirValorObjeto(objCliente.Identificador, dr("OID_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.Codigo, dr("COD_CLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objCliente.Descripcion, dr("DES_CLIENTE"), GetType(String))

                If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(String))) Then
                    objCliente.PorcComisionCliente = Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(Decimal))
                End If

                Dim objSubcliente = New Comon.Clases.SubCliente With {.PuntosServicio = New ObservableCollection(Of Comon.Clases.PuntoServicio)}
                Util.AtribuirValorObjeto(objSubcliente.Identificador, dr("OID_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubcliente.Codigo, dr("COD_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objSubcliente.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))
                Dim objPuntoServicio = New Comon.Clases.PuntoServicio
                Util.AtribuirValorObjeto(objPuntoServicio.Identificador, dr("OID_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPuntoServicio.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))
                Dim objTipoPlanificacao = New Comon.Clases.TipoPlanificacion
                Util.AtribuirValorObjeto(objTipoPlanificacao.Identificador, dr("OID_TIPO_PLANIFICACION"), GetType(String))
                Util.AtribuirValorObjeto(objTipoPlanificacao.Codigo, dr("COD_TIPO_PLANIFICACION"), GetType(String))
                Util.AtribuirValorObjeto(objTipoPlanificacao.Descripcion, dr("DES_TIPO_PLANIFICACION"), GetType(String))

                objSubcliente.PuntosServicio.Add(objPuntoServicio)
                objCliente.SubClientes.Add(objSubcliente)
                objMaquina.PuntosServicio.Add(objCliente)
                objMaquina.Planificacion.TipoPlanificacion = objTipoPlanificacao

                'delegacion
                objMaquina.BancoTesoreriaPlanxMaquina = New Comon.Clases.SubCliente
                Util.AtribuirValorObjeto(objMaquina.BancoTesoreriaPlanxMaquina.Identificador, dr("OID_SUBCLIENTE_PLAMQ"), GetType(String))
                Util.AtribuirValorObjeto(objMaquina.BancoTesoreriaPlanxMaquina.Codigo, dr("COD_SUBCLIENTE_PLAMQ"), GetType(String))
                Util.AtribuirValorObjeto(objMaquina.BancoTesoreriaPlanxMaquina.Descripcion, dr("DES_SUBCLIENTE_PLAMQ"), GetType(String))

                objMaquina.CuentaTesoreriaPlanxMaquina = New Comon.Clases.PuntoServicio
                Util.AtribuirValorObjeto(objMaquina.CuentaTesoreriaPlanxMaquina.Identificador, dr("OID_PTO_SERV_PLAMQ"), GetType(String))
                Util.AtribuirValorObjeto(objMaquina.CuentaTesoreriaPlanxMaquina.Codigo, dr("COD_PTO_SERV_PLAMQ"), GetType(String))
                Util.AtribuirValorObjeto(objMaquina.CuentaTesoreriaPlanxMaquina.Descripcion, dr("DES_PTO_SERV_PLAMQ"), GetType(String))
                objMaquina.CumpleNombrePatron = TieneNombrePatron(objMaquina.DeviceID)
            Next

            Return objMaquina
        End If

        Return Nothing
    End Function

    Private Shared Function TieneNombrePatron(pDeviceID As String) As Boolean
        Dim retorno As Boolean = False
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.ObtenerMAESiCumplePatronDeviceID)
        cmd.CommandType = CommandType.Text
        Dim cantidad As Integer = 0
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
        If dt IsNot Nothing Then
            If dt.Select(String.Format("CODIGO_MAE = '{0}'", pDeviceID)).Count > 0 Then
                For Each fila In dt.Select(String.Format("CODIGO_MAE = '{0}'", pDeviceID))

                    Util.AtribuirValorObjeto(cantidad, fila("VALIDACION"), GetType(Integer))
                    If cantidad > 0 Then
                        retorno = True
                    End If
                Next
            End If
        End If

        Return retorno
    End Function

    Public Shared Function GetMaquinaPuntoServicio(oidPuntoServicio As String) As Comon.Clases.Maquina
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaMAEPuntoServicio)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, oidPuntoServicio))

        Dim dtMaquina As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        ' caso encontre algum registro
        If dtMaquina IsNot Nothing _
            AndAlso dtMaquina.Rows.Count > 0 Then
            Return PopularGetMaquina(dtMaquina.Rows(0))
        End If

        Return Nothing
    End Function

    Public Shared Sub consultaAccionesEnLote(identificadorDelegacion As String,
                                             identificadorBanco As String,
                                             identificadorCliente As String,
                                             identificadorPlanificacion As String,
                                             ByRef respuesta As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta))

        If Not String.IsNullOrEmpty(identificadorDelegacion) Then

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    ' Planta
                    Dim inner As String
                    Dim filtro As String = " AND P.OID_DELEGACION = []OID_DELEGACION "
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, identificadorDelegacion))

                    ' Cliente
                    If Not String.IsNullOrEmpty(identificadorCliente) Then
                        inner &= " INNER JOIN GEPR_TPUNTO_SERVICIO PS ON PS.OID_MAQUINA = M.OID_MAQUINA "
                        inner &= " INNER JOIN GEPR_TSUBCLIENTE SC ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE "

                        filtro &= " AND SC.OID_CLIENTE = []OID_CLIENTE "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, identificadorCliente))
                    End If

                    If Not String.IsNullOrEmpty(identificadorBanco) OrElse Not String.IsNullOrEmpty(identificadorPlanificacion) Then
                        inner &= " INNER JOIN SAPR_TPLANXMAQUINA PM ON PM.OID_MAQUINA = M.OID_MAQUINA AND PM.BOL_ACTIVO = 1 "

                        ' Planificacion
                        If Not String.IsNullOrEmpty(identificadorPlanificacion) Then
                            filtro &= " AND PM.OID_PLANIFICACION = []OID_PLANIFICACION "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, identificadorPlanificacion))
                        End If

                        ' Banco
                        If Not String.IsNullOrEmpty(identificadorBanco) Then
                            inner &= " INNER JOIN SAPR_TPLANIFICACION P ON P.OID_PLANIFICACION = PM.OID_PLANIFICACION AND P.BOL_ACTIVO = 1 "
                            filtro &= " AND P.OID_CLIENTE = []OID_BANCO "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_BANCO", ProsegurDbType.Objeto_Id, identificadorBanco))
                        End If

                    End If

                    command.CommandText = Util.PrepararQuery(String.Format(My.Resources.consultaAccionesEnLote, inner, filtro))
                    command.CommandType = CommandType.Text

                    Dim dtValores As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, command)

                    If dtValores IsNot Nothing AndAlso dtValores.Rows.Count > 0 Then

                        If respuesta Is Nothing Then respuesta = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)

                        For Each valor In dtValores.Rows

                            Dim identificador As String = String.Empty
                            Dim descripcion As String = String.Empty
                            Dim considerarecuento As Boolean
                            Dim multicliente As Boolean
                            Util.AtribuirValorObjeto(identificador, valor("OID_MAQUINA"), GetType(String))
                            Util.AtribuirValorObjeto(descripcion, valor("DESCRIPCION"), GetType(String))
                            Util.AtribuirValorObjeto(considerarecuento, valor("BOL_CONSIDERA_RECUENTOS"), GetType(Boolean))
                            Util.AtribuirValorObjeto(multicliente, valor("BOL_MULTICLIENTE"), GetType(Boolean))

                            Dim obj = New ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta
                            obj.oid_maquina = identificador
                            obj.descripcion = descripcion
                            If obj.descripcion.Length > 50 Then
                                obj.descripcion = obj.descripcion.Substring(0, 46) + " ..."
                            End If

                            obj.considerarecuento = considerarecuento
                            obj.multicliente = multicliente

                            respuesta.Add(obj)

                            'If considerarecuento Then
                            '    respuesta.Add(obj)
                            'Else
                            '    identificadoresSinRecuento.Add(obj)
                            'End If

                        Next

                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        Else
            respuesta = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        End If

    End Sub

    Public Shared Function consultaAccionesEnLoteExportar(identificadores As List(Of String)) As DataTable

        Dim dtValores As DataTable = Nothing

        If identificadores IsNot Nothing AndAlso identificadores.Count > 0 Then

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    command.CommandText = My.Resources.consultaAccionesEnLoteExportar
                    command.CommandText &= Util.MontarClausulaIn(identificadores, "OID_MAQUINA", command, "WHERE", "M")

                    command.CommandText = Util.PrepararQuery(command.CommandText)
                    command.CommandType = CommandType.Text
                    dtValores = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End If

        Return dtValores

    End Function

    Public Shared Function GetTransacaoMaquinas(oidPlanta As String) As ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.BuscaTransacaoMaquinas
        cmd.CommandType = CommandType.Text

        If oidPlanta IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND SECT.OID_PLANTA = :OID_PLANTA "
            Else
                cmd.CommandText &= " WHERE SECT.OID_PLANTA = :OID_PLANTA "
            End If

            cmd.CommandText &= "ORDER BY 1, 2"

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANTA", ProsegurDbType.Identificador_Alfanumerico, oidPlanta))

        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Dim dtMaquina As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        Dim objMaquinaColeccion As New ContractoServicio.Maquina.GetMaquinaTransacao.Respuesta

        If dtMaquina.Rows.Count > 0 AndAlso dtMaquina IsNot Nothing Then

            Dim objMaquina As ContractoServicio.Maquina.GetMaquinaTransacao.MaquinaTransacao = Nothing
            objMaquinaColeccion.Maquinas = New List(Of ContractoServicio.Maquina.GetMaquinaTransacao.MaquinaTransacao)

            For Each dr In dtMaquina.Rows
                objMaquina = New ContractoServicio.Maquina.GetMaquinaTransacao.MaquinaTransacao()
                Util.AtribuirValorObjeto(objMaquina.CodIndentificacion, dr("COD_IDENTIFICACION"), GetType(String))
                Util.AtribuirValorObjeto(objMaquina.DesSector, dr("DES_SECTOR"), GetType(String))

                objMaquinaColeccion.Maquinas.Add(objMaquina)
            Next
        End If

        Return objMaquinaColeccion
    End Function

#End Region

#Region "[INSERIR]"

    Public Shared Function SetMaquina(objMaquina As Comon.Clases.Maquina, objTransacao As Transacao) As String

        Dim oidMaquina As String = Guid.NewGuid().ToString

        If String.IsNullOrEmpty(objMaquina.Identificador) Then
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaMaquinaMAE)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, objMaquina.Sector.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODELO", ProsegurDbType.Objeto_Id, objMaquina.Modelo.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_MAQUINA", ProsegurDbType.Objeto_Id, objMaquina.TipoMaquina.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICACION", ProsegurDbType.Identificador_Alfanumerico, objMaquina.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objMaquina.BolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONSIDERA_RECUENTOS", ProsegurDbType.Logico, objMaquina.ConsideraRecuentos))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objMaquina.FechaHoraCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, objMaquina.DesUsuarioCreacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objMaquina.FechaHoraModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objMaquina.DesUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MULTICLIENTE", ProsegurDbType.Logico, objMaquina.MultiClientes))

            If objTransacao IsNot Nothing Then
                objTransacao.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            End If
        Else
            oidMaquina = objMaquina.Identificador

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.ModificarMaquinaMAE)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, objMaquina.Sector.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODELO", ProsegurDbType.Objeto_Id, objMaquina.Modelo.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_MAQUINA", ProsegurDbType.Objeto_Id, objMaquina.TipoMaquina.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_IDENTIFICACION", ProsegurDbType.Identificador_Alfanumerico, objMaquina.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objMaquina.BolActivo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONSIDERA_RECUENTOS", ProsegurDbType.Logico, objMaquina.ConsideraRecuentos))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objMaquina.FechaHoraModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, objMaquina.DesUsuarioModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MULTICLIENTE", ProsegurDbType.Logico, objMaquina.MultiClientes))

            If objTransacao IsNot Nothing Then
                objTransacao.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
            End If
        End If

        Return oidMaquina

    End Function

#End Region

#Region "[UPDATE]"


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="identificadores"></param>
    ''' <param name="nuevoValue"></param>
    ''' <param name="Accion">0 - Considerar Recuentos    1 - MultiClientes</param>
    Public Shared Sub AccionesEnLote(identificadores As List(Of String),
                                     nuevoValue As Boolean, accion As Integer)

        If identificadores IsNot Nothing AndAlso identificadores.Count > 0 Then

            Try
                If accion = 0 OrElse (accion = 1 AndAlso nuevoValue) Then


                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                        command.CommandText = My.Resources.updateAccionesEnLote

                        If accion = 0 Then

                            command.CommandText += "  SET M.BOL_CONSIDERA_RECUENTOS = []BOL_CONSIDERA_RECUENTOS "

                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_CONSIDERA_RECUENTOS", ProsegurDbType.Logico, If(nuevoValue, 1, 0)))
                        ElseIf accion = 1 Then

                            command.CommandText += "  SET M.BOL_MULTICLIENTE = []BOL_MULTICLIENTE "
                            command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MULTICLIENTE", ProsegurDbType.Logico, If(nuevoValue, 1, 0)))

                        End If

                        command.CommandText &= Util.MontarClausulaIn(identificadores, "OID_MAQUINA", command, "WHERE", "M")

                        command.CommandText = Util.PrepararQuery(command.CommandText)
                        command.CommandType = CommandType.Text
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, command)

                    End Using
                ElseIf accion = 1 AndAlso Not nuevoValue Then



                    Dim objTransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

                    Dim command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    command.CommandText = My.Resources.updateAccionesEnLoteMulticliente



                    command.CommandText = String.Format(command.CommandText, Util.MontarClausulaIn(identificadores, "OID_MAQUINA", command))

                    command.CommandText = Util.PrepararQuery(command.CommandText)
                    command.CommandType = CommandType.Text

                    objTransacion.AdicionarItemTransacao(command)

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, command)


                    Dim command2 As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

                    command2.CommandText = My.Resources.updateAccionesEnLote

                    command2.CommandText += "  SET M.BOL_MULTICLIENTE = []BOL_MULTICLIENTE "
                    command2.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MULTICLIENTE", ProsegurDbType.Logico, If(nuevoValue, 1, 0)))

                    command2.CommandText &= Util.MontarClausulaIn(identificadores, "OID_MAQUINA", command2, "WHERE", "M")

                    command2.CommandText = Util.PrepararQuery(command2.CommandText)
                    command2.CommandType = CommandType.Text

                    objTransacion.AdicionarItemTransacao(command2)

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, command2)


                    objTransacion.RealizarTransacao()

                End If

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End If

    End Sub

#End Region

#Region "[DELETE]"

    Public Shared Sub BajaMaquina(oidMaquina As String, usuario As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaMaquinaMAE)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, DateTime.UtcNow))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))

        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Responsável por popular a classe de Maquina 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Shared Function PopularGetMaquina(dr As DataRow) As Comon.Clases.Maquina

        ' criar objeto Maquina
        Dim objMaquina As New Comon.Clases.Maquina

        Util.AtribuirValorObjeto(objMaquina.Identificador, dr("OID_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Codigo, dr("COD_IDENTIFICACION"), GetType(String))
        'Util.AtribuirValorObjeto(objMaquina.Descripcion, dr("DES_SECTOR"), GetType(String))


        objMaquina.Sector = New Comon.Clases.Sector
        Util.AtribuirValorObjeto(objMaquina.Sector.Identificador, dr("OID_SECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Sector.Descripcion, dr("DES_SECTOR"), GetType(String))

        objMaquina.Delegacion = New Comon.Clases.Delegacion With {.Plantas = New ObservableCollection(Of Comon.Clases.Planta)}
        Util.AtribuirValorObjeto(objMaquina.Delegacion.Identificador, dr("OID_DELEGACION"), GetType(String))
        objMaquina.Delegacion.Plantas.Add(New Comon.Clases.Planta)
        Util.AtribuirValorObjeto(objMaquina.Delegacion.Plantas.First.Identificador, dr("OID_PLANTA"), GetType(String))

        objMaquina.Modelo = New Comon.Clases.Modelo
        Util.AtribuirValorObjeto(objMaquina.Modelo.Identificador, dr("OID_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Modelo.Codigo, dr("COD_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Modelo.Descripcion, dr("DES_MODELO"), GetType(String))

        objMaquina.Modelo.Fabricante = New Comon.Clases.Fabricante
        Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Identificador, dr("OID_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Codigo, dr("COD_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Descripcion, dr("DES_FABRICANTE"), GetType(String))

        objMaquina.PlanMaquina = New List(Of Comon.Clases.PlanXmaquina)

        Util.AtribuirValorObjeto(objMaquina.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.ConsideraRecuentos, dr("BOL_CONSIDERA_RECUENTOS"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

        objMaquina.Cliente = New Comon.Clases.Cliente
        If dr.Table.Columns.Contains("OID_CLIENTE") Then
            Util.AtribuirValorObjeto(objMaquina.Cliente.Identificador, dr("OID_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.Cliente.Codigo, dr("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.Cliente.Descripcion, dr("DES_CLIENTE"), GetType(String))
        End If

        objMaquina.SubCliente = New Comon.Clases.SubCliente
        If dr.Table.Columns.Contains("OID_SUBCLIENTE") Then
            Util.AtribuirValorObjeto(objMaquina.SubCliente.Identificador, dr("OID_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.SubCliente.Codigo, dr("COD_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.SubCliente.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))
        End If

        If dr.Table.Columns.Contains("OID_PTO_SERVICIO") Then
            objMaquina.PtoServicio = New Comon.Clases.PuntoServicio
            Util.AtribuirValorObjeto(objMaquina.PtoServicio.Identificador, dr("OID_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.PtoServicio.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(objMaquina.PtoServicio.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))
        End If
        ' retornar objeto divisa preenchido

        Return objMaquina

    End Function

    Private Shared Function PopularGetMaquinaDetalle(dr As DataRow) As ContractoServicio.Maquina.GetMaquinaDetalle.Maquina

        ' criar objeto Maquina
        Dim objMaquina As New ContractoServicio.Maquina.GetMaquinaDetalle.Maquina

        Util.AtribuirValorObjeto(objMaquina.MultiClientes, dr("BOL_MULTICLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidMaquina, dr("OID_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidDelegacion, dr("OID_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidPlanta, dr("OID_PLANTA"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.DeviceID, dr("COD_IDENTIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Descripcion, dr("DES_SECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidSector, dr("OID_SECTOR"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidFabricante, dr("OID_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.OidModelo, dr("OID_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.FechaValorInicio, dr("FYH_VIGENCIA_INICIO"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.FechaValorFin, dr("FYH_VIGENCIA_FIN"), GetType(DateTime))

        If objMaquina.FechaValorInicio <> DateTime.MinValue Then
            objMaquina.FechaValor = True
        End If

        objMaquina.Planificacion = New Comon.Clases.Planificacion
        Util.AtribuirValorObjeto(objMaquina.Planificacion.Identificador, dr("OID_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.Descripcion, dr("DES_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.FechaHoraVigenciaInicio, dr("INICIO_PLAN"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.FechaHoraVigenciaFin, dr("FIN_PLAN"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.BolActivo, dr("PLAN_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.BolControlaFacturacion, dr("BOL_CONTROLA_FACTURACION"), GetType(Boolean))

        objMaquina.Planificacion.Cliente = New Comon.Clases.Cliente
        Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Identificador, dr("OID_BANCO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Codigo, dr("COD_BANCO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Descripcion, dr("DES_BANCO"), GetType(String))

        If Not String.IsNullOrWhiteSpace(objMaquina.Planificacion.Identificador) Then

            Dim peticionDelegacion = New ContractoServicio.Planificacion.GetPlanificaciones.Peticion()


            objMaquina.Planificacion.Canales = GetPlanxCanal(objMaquina.Planificacion.Identificador)

        End If

        'objMaquina.Planificacion.BancoTesoreria = New Comon.Clases.SubCliente
        'Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Identificador, dr("OID_SUBCLIENTE_PLAMQ"), GetType(String))
        'Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Codigo, dr("COD_SUBCLIENTE_PLAMQ"), GetType(String))
        'Util.AtribuirValorObjeto(objMaquina.Planificacion.Cliente.Descripcion, dr("DES_SUBCLIENTE_PLAMQ"), GetType(String))


        objMaquina.PuntosServicio = New List(Of Comon.Clases.Cliente)

        Util.AtribuirValorObjeto(objMaquina.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.ConsideraRecuentos, dr("BOL_CONSIDERA_RECUENTOS"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objMaquina.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

        objMaquina.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_MAQUINA").ToString())

        If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("NUM_PORCENT_COM_MAQ"), GetType(String))) Then
            objMaquina.PorcComisionMaquina = Util.AtribuirValorObj(dr("NUM_PORCENT_COM_MAQ"), GetType(Decimal))
        End If

        objMaquina.Planes = GetPlanesPorMAE(objMaquina)

        ' retornar objeto divisa preenchido
        Return objMaquina

    End Function

    Private Shared Function GetPlanesPorMAE(objMaquina As ContractoServicio.Maquina.GetMaquinaDetalle.Maquina) As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
        Dim planes As New ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)

        Dim objDS As DataSet = Nothing
        Dim SP As String = String.Format("SAPR_PMAQUINA_{0}.srecuperar_plancanal_maquina", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, objMaquina.OidMaquina, , False)
        spw.AgregarParam("par$rc_canales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "canales")
        spw.AgregarParam("par$rc_subcanales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "subcanales")


        objDS = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GE, False, Nothing)
        Dim indiceAgrupador As Integer = -1
        Dim codigoAgrupadorFila As Integer = 0
        Dim unPlan As PlanMaqPorCanalSubCanalPunto
        Dim unCanal As Comon.Clases.Canal
        Dim unPunto As Comon.Clases.PuntoServicio
        Dim unSubCanal As Comon.Clases.SubCanal

        Dim oidPunto As String = String.Empty
        Dim oidCanal As String = String.Empty
        Dim oidSubCanal As String = String.Empty

        If objDS IsNot Nothing AndAlso objDS.Tables.Count > 0 Then
            If objDS.Tables.Contains("canales") AndAlso objDS.Tables("canales").Rows.Count > 0 Then

                For Each dr As DataRow In objDS.Tables("canales").Rows
                    codigoAgrupadorFila = Util.AtribuirValorObj(dr("COD_AGRUPADOR"), GetType(Int16))

                    If indiceAgrupador <> codigoAgrupadorFila Then
                        unPlan = New PlanMaqPorCanalSubCanalPunto
                        unPlan.Canales = New ObservableCollection(Of Comon.Clases.Canal)
                        unPlan.PuntosServicios = New ObservableCollection(Of Comon.Clases.PuntoServicio)
                        unPlan.CodigoAgrupador = codigoAgrupadorFila
                    End If

                    oidPunto = Util.AtribuirValorObj(dr("OID_PTO_SERVICIO"), GetType(String))

                    If Not String.IsNullOrEmpty(oidPunto) Then
                        unPunto = New Comon.Clases.PuntoServicio
                        unPunto.Identificador = oidPunto
                        unPunto.Codigo = Util.AtribuirValorObj(dr("COD_PTO_SERVICIO"), GetType(String))
                        unPunto.Descripcion = Util.AtribuirValorObj(dr("DES_PTO_SERVICIO"), GetType(String))

                        If unPlan IsNot Nothing AndAlso Not unPlan.PuntosServicios.Contains(unPunto) Then
                            unPlan.PuntosServicios.Add(unPunto)
                        End If
                    End If

                    oidCanal = Util.AtribuirValorObj(dr("OID_CANAL"), GetType(String))
                    If Not String.IsNullOrEmpty(oidCanal) Then
                        unCanal = New Comon.Clases.Canal
                        unCanal.Identificador = oidCanal
                        unCanal.Codigo = Util.AtribuirValorObj(dr("COD_CANAL"), GetType(String))
                        unCanal.Descripcion = Util.AtribuirValorObj(dr("DES_CANAL"), GetType(String))
                        unCanal.SubCanales = New ObservableCollection(Of Comon.Clases.SubCanal)
                        If Not unPlan.Canales.Contains(unCanal) Then
                            unPlan.Canales.Add(unCanal)
                        End If
                    End If


                    If indiceAgrupador <> codigoAgrupadorFila Then
                        planes.Add(unPlan)
                    End If

                    indiceAgrupador = codigoAgrupadorFila
                Next
            End If

            If objDS.Tables.Contains("subcanales") AndAlso objDS.Tables("subcanales").Rows.Count > 0 Then

                For Each dr As DataRow In objDS.Tables("subcanales").Rows
                    codigoAgrupadorFila = Util.AtribuirValorObj(dr("COD_AGRUPADOR"), GetType(Int16))
                    For Each elementoPlan In planes
                        If elementoPlan.CodigoAgrupador = codigoAgrupadorFila Then
                            oidCanal = Util.AtribuirValorObj(dr("OID_CANAL"), GetType(String))

                            unSubCanal = New Comon.Clases.SubCanal
                            unSubCanal.Identificador = Util.AtribuirValorObj(dr("OID_SUBCANAL"), GetType(String))
                            unSubCanal.Codigo = Util.AtribuirValorObj(dr("COD_SUBCANAL"), GetType(String))
                            unSubCanal.Descripcion = Util.AtribuirValorObj(dr("DES_SUBCANAL"), GetType(String))

                            For Each canal As Comon.Clases.Canal In elementoPlan.Canales
                                If canal.Identificador = oidCanal Then
                                    If Not canal.SubCanales.Contains(unSubCanal) Then
                                        canal.SubCanales.Add(unSubCanal)
                                    End If
                                End If
                            Next
                        End If
                    Next
                Next
            End If

        End If



        Return planes
    End Function

    Private Shared Function PopularGetMaquinaSinPlanificacion(dr As DataRow) As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina

        ' criar objeto Maquina
        Dim objMaquina As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina

        Util.AtribuirValorObjeto(objMaquina.OidMaquina, dr("OID_MAQUINA"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objMaquina.ConsideraRecuentos, dr("BOL_CONSIDERA_RECUENTOS"), GetType(Boolean))

        Util.AtribuirValorObjeto(objMaquina.DeviceID, dr("COD_IDENTIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Descripcion, dr("DES_SECTOR"), GetType(String))

        Util.AtribuirValorObjeto(objMaquina.DesFabricante, dr("DES_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.DesModelo, dr("DES_MODELO"), GetType(String))

        Util.AtribuirValorObjeto(objMaquina.CodigoCliente, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.Cliente, dr("DES_CLIENTE"), GetType(String))


        Util.AtribuirValorObjeto(objMaquina.CodigoSubCliente, dr("COD_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.SubCliente, dr("DES_SUBCLIENTE"), GetType(String))

        Util.AtribuirValorObjeto(objMaquina.OidPtoServicio, dr("OID_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.CodigoPtoServicio, dr("COD_PTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objMaquina.PtoServicio, dr("DES_PTO_SERVICIO"), GetType(String))

        objMaquina.Cliente = objMaquina.CodigoCliente + " - " + objMaquina.Cliente
        objMaquina.SubCliente = objMaquina.CodigoSubCliente + " - " + objMaquina.SubCliente
        objMaquina.PtoServicio = objMaquina.CodigoPtoServicio + " - " + objMaquina.PtoServicio

        ' retornar objeto divisa preenchido
        Return objMaquina

    End Function
#End Region

End Class
