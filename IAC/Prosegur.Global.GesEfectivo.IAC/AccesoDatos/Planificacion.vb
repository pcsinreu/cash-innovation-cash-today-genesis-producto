Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Planificacion

#Region "[CONSULTAS]"

    Public Shared Function GetPlanificacionProgramacion(objPetion As ContractoServicio.Planificacion.GetPlanificacionProgramacion.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As List(Of Comon.Clases.Planificacion)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaPlanificacion)
        cmd.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPetion.OidPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.OID_PLANIFICACION) = :OID_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(P.OID_PLANIFICACION) = :OID_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPetion.OidPlanificacion.ToUpper()))
        End If


        If Not String.IsNullOrEmpty(objPetion.OidBanco) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(C.OID_CLIENTE) = :OID_CLIENTE "
            Else
                cmd.CommandText &= " WHERE UPPER(C.OID_CLIENTE) = :OID_CLIENTE "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPetion.OidBanco.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(objPetion.OidTipoPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND  UPPER(P.OID_TIPO_PLANIFICACION) = :OID_TIPO_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE  UPPER(P.OID_TIPO_PLANIFICACION) = :OID_TIPO_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPetion.OidTipoPlanificacion.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(objPetion.DesPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.DES_PLANIFICACION) LIKE :DES_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(P.DES_PLANIFICACION) LIKE :DES_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PLANIFICACION", ProsegurDbType.Descricao_Longa, "%" + objPetion.DesPlanificacion.ToUpper() + "%"))
        End If

        If objPetion.Vigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND P.BOL_ACTIVO = :BOL_ACTIVO "
            Else
                cmd.CommandText &= " WHERE P.BOL_ACTIVO = :BOL_ACTIVO "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPetion.Vigente))
        End If

        'cria o objeto de Planificacion
        Dim lstPlanificacion As New List(Of Comon.Clases.Planificacion)

        Dim dtPlanificacion As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPetion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtPlanificacion IsNot Nothing _
            AndAlso dtPlanificacion.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtPlanificacion.Rows
                Dim identificador As String = String.Empty
                Util.AtribuirValorObjeto(identificador, dr("OID_PLANIFICACION"), GetType(String))
                Dim objPlanificacion As Comon.Clases.Planificacion

                If Not lstPlanificacion.Exists(Function(a) a.Identificador = identificador) Then
                    objPlanificacion = PopularGetPlanificacion(dr)
                    lstPlanificacion.Add(objPlanificacion)
                Else
                    objPlanificacion = lstPlanificacion.Find(Function(a) a.Identificador = identificador)
                End If

                Dim planProg As New Comon.Clases.PlanXProgramacion

                Util.AtribuirValorObjeto(planProg.Identificador, dr("OID_PLANXPROGRAMACION"), GetType(String))
                Util.AtribuirValorObjeto(planProg.NecDiaInicio, dr("NEC_DIA_INICIO"), GetType(Integer))
                Util.AtribuirValorObjeto(planProg.NecDiaFin, dr("NEC_DIA_FIN"), GetType(Integer))
                Util.AtribuirValorObjeto(planProg.FechaHoraInicio, dr("FYH_HORA_INICIO"), GetType(DateTime))
                Util.AtribuirValorObjeto(planProg.FechaHoraFin, dr("FYH_HORA_FIN"), GetType(DateTime))
                Util.AtribuirValorObjeto(planProg.DesUsuarioCreacion, dr("PROG_DES_USUARIO_CREACION"), GetType(String))
                Util.AtribuirValorObjeto(planProg.FechaHoraCreacion, dr("PROG_GMT_CREACION"), GetType(DateTime))
                Util.AtribuirValorObjeto(planProg.FechaHoraModificacion, dr("PROG_GMT_MODIFICACION"), GetType(DateTime))
                Util.AtribuirValorObjeto(planProg.DesUsuarioModificacion, dr("PROG_DES_USUARIO_MODIFICACION"), GetType(String))

                objPlanificacion.Programacion.Add(planProg)
            Next
        End If

        Return lstPlanificacion

    End Function

    Public Shared Function GetPlanificaciones(objPeticion As ContractoServicio.Planificacion.GetPlanificaciones.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        'cria o objeto de Planificacion
        Dim lstPlanificacion As New List(Of ContractoServicio.Planificacion.GetPlanificaciones.PlanXProgramacion)

        Dim dtPlanificacion = ObtenerPlanXProgramacion(objPeticion.OidPlanificacion, objPeticion.OidBanco, objPeticion.OidTipoPlanificacion, objPeticion.DesPlanificacion, _
                                                       objPeticion.Vigente, parametrosRespuestaPaginacion, objPeticion.ParametrosPaginacion)

        ' caso encontre algum registro
        If dtPlanificacion IsNot Nothing _
            AndAlso dtPlanificacion.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtPlanificacion.Rows

                Dim objPlanificacion As New ContractoServicio.Planificacion.GetPlanificaciones.PlanXProgramacion

                Util.AtribuirValorObjeto(objPlanificacion.OidPlanificacion, dr("OID_PLANIFICACION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.DesPlanificacion, dr("DESCRIPCION"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.BolControlaFacturacion, dr("BOL_CONTROLA_FACTURACION"), GetType(Boolean))



                Util.AtribuirValorObjeto(objPlanificacion.DesTipoPlanificacion, dr("TIPO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.BolActivo, dr("VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(objPlanificacion.DesBanco, dr("BANCO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhLunes, dr("LUNES"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhMartes, dr("MARTES"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhMiercoles, dr("MIERCOLES"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhViernes, dr("VIERNES"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhJueves, dr("JUEVES"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhSabado, dr("SABADO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FyhDomingo, dr("DOMINGO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.FechaHoraVigenciaInicio, dr("GMT_INICIO"), GetType(DateTime))
                Util.AtribuirValorObjeto(objPlanificacion.FechaHoraVigenciaFin, dr("GMT_FIN"), GetType(DateTime))
                lstPlanificacion.Add(objPlanificacion)
            Next
        End If

        objRespuesta.Planificacion = lstPlanificacion
        objRespuesta.ParametrosPaginacion.TotalRegistrosPaginados = parametrosRespuestaPaginacion.TotalRegistrosPaginados

        Return objRespuesta

    End Function

    Private Shared Function ObtenerPlanXProgramacion(OidPlanificacion As String, OidBanco As String, OidTipoPlanificacion As String, DesPlanificacion As String, Vigente As Nullable(Of Boolean), _
                        ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion, _
                        parametrosPaginacionPeticion As Genesis.Comon.Paginacion.ParametrosPeticionPaginacion) As DataTable

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query = Util.PrepararQuery(My.Resources.BuscarPlanificaciones)

        cmd.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(OidPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.OID_PLANIFICACION) = :OID_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(P.OID_PLANIFICACION) = :OID_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, OidPlanificacion.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(OidBanco) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(C.OID_CLIENTE) = :OID_CLIENTE "
            Else
                cmd.CommandText &= " WHERE UPPER(C.OID_CLIENTE) = :OID_CLIENTE "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, OidBanco.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(OidTipoPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.OID_TIPO_PLANIFICACION) = :OID_TIPO_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(P.OID_TIPO_PLANIFICACION) = :OID_TIPO_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, OidTipoPlanificacion.ToUpper()))
        End If

        If Not String.IsNullOrEmpty(DesPlanificacion) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.DES_PLANIFICACION) LIKE :DES_PLANIFICACION "
            Else
                cmd.CommandText &= " WHERE UPPER(P.DES_PLANIFICACION) LIKE :DES_PLANIFICACION "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PLANIFICACION", ProsegurDbType.Descricao_Longa, "%" + DesPlanificacion.ToUpper() + "%"))
        End If

        If Vigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(P.BOL_ACTIVO) = :BOL_ACTIVO AND (P.FYH_VIGENCIA_FIN IS NULL OR P.FYH_VIGENCIA_FIN > SYS_EXTRACT_UTC(SYSTIMESTAMP)) "
            Else
                cmd.CommandText &= " WHERE UPPER(P.BOL_ACTIVO) = :BOL_ACTIVO "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Vigente))
        End If

        cmd.CommandText = String.Format(query, cmd.CommandText)

        Dim dtPlanificacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        Return dtPlanificacion

    End Function


    Public Shared Function GetPlanificacionFacturacion(objPeticion As ContractoServicio.Planificacion.GetPlanificacionFacturacion.Peticion) As ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta

        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta

        'cria o objeto de Planificacion
        Dim lstPlanificacion As New List(Of ContractoServicio.Planificacion.GetPlanificaciones.PlanXProgramacion)


        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query = Util.PrepararQuery(My.Resources.BuscarPlanificacionFacturacion)

        cmd.CommandType = CommandType.Text


        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidDelegacion))


        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.OidPlanificacion))


        cmd.CommandText = String.Format(query, cmd.CommandText)

        Dim dtPlanificacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)



        ' caso encontre algum registro
        If dtPlanificacion IsNot Nothing _
            AndAlso dtPlanificacion.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtPlanificacion.Rows

                Dim objPlanificacion As New ContractoServicio.Planificacion.GetPlanificacionFacturacion.Planificacion

                Util.AtribuirValorObjeto(objPlanificacion.OidPlanificacion, dr("OID_PLANIFICACION"), GetType(String))

                If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("COMISION_PLANIFICACION"), GetType(String))) Then
                    objPlanificacion.PorcComisionPlanificacion = Util.AtribuirValorObj(dr("COMISION_PLANIFICACION"), GetType(Decimal))
                End If

                objPlanificacion.BancoTesoreriaDelegacion = New Comon.Clases.SubCliente
                Util.AtribuirValorObjeto(objPlanificacion.BancoTesoreriaDelegacion.Identificador, dr("OID_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.BancoTesoreriaDelegacion.Codigo, dr("COD_SUBCLIENTE"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.BancoTesoreriaDelegacion.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))


                objPlanificacion.CuentaTesoreriaDelegacion = New Comon.Clases.PuntoServicio
                Util.AtribuirValorObjeto(objPlanificacion.CuentaTesoreriaDelegacion.Identificador, dr("OID_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.CuentaTesoreriaDelegacion.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
                Util.AtribuirValorObjeto(objPlanificacion.CuentaTesoreriaDelegacion.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))

                Util.AtribuirValorObjeto(objPlanificacion.BolControlaFacturacion, dr("BOL_CONTROLA_FACTURACION"), GetType(Boolean))

                objRespuesta.Planificacion = objPlanificacion
            Next
        End If



        Return objRespuesta

    End Function


#End Region

#Region "[INSERIR]"

#End Region

#Region "[UPDATE]"

    Public Shared Function VincularMaquina(oidMaquina As String, oidPlanificaicon As String, oidSector As String, fechaValorInicio As DateTime, fechaValorFin As DateTime?, codUsuario As String,
                                           numPorcentComision As Nullable(Of Decimal), oidPtoServicioTesoreria As String, oidSubclienteTesoreria As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VincularMaquina)
        comando.CommandType = CommandType.Text

        Dim oidPlanxMaquina As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANXMAQUINA", ProsegurDbType.Objeto_Id, oidPlanxMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificaicon))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, oidSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE_TESORERIA", ProsegurDbType.Objeto_Id, oidSubclienteTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO_TESORERIA", ProsegurDbType.Objeto_Id, oidPtoServicioTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PORCENT_COMISION", ProsegurDbType.Numero_Decimal, numPorcentComision))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VIGENCIA_INICIO", ProsegurDbType.Data_Hora, fechaValorInicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VIGENCIA_FIN", ProsegurDbType.Data_Hora, If(fechaValorFin Is Nothing OrElse fechaValorFin = DateTime.MinValue, Nothing, fechaValorFin)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO", ProsegurDbType.Descricao_Longa, codUsuario))

        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidPlanxMaquina

    End Function

    Public Shared Sub DesvincularMaquina(oidMaquina As String, oidPlanificaicon As String, codUsuario As String, objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.DesvincularMaquina)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificaicon))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codUsuario))

        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

    Shared Sub ActualizarVinculoMaquina(oidMaquina As String, oidPlanificaicon As String, oidSector As String, fechaValorInicio As DateTime, fechaValorFin As DateTime?, codUsuario As String,
                                        numPorcentComision As Nullable(Of Decimal), oidPtoServicioTesoreria As String, oidSubclienteTesoreria As String, ByVal objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarVinculoMaquina)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificaicon))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SECTOR", ProsegurDbType.Objeto_Id, oidSector))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VIGENCIA_INICIO", ProsegurDbType.Data_Hora, fechaValorInicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_VIGENCIA_FIN", ProsegurDbType.Data_Hora, If(fechaValorFin Is Nothing OrElse fechaValorFin = DateTime.MinValue, Nothing, fechaValorFin)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, codUsuario))


        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE_TESORERIA", ProsegurDbType.Objeto_Id, oidSubclienteTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO_TESORERIA", ProsegurDbType.Objeto_Id, oidPtoServicioTesoreria))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PORCENT_COMISION", ProsegurDbType.Numero_Decimal, numPorcentComision))





        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

    End Sub

#End Region

#Region "[DELETE]"

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Responsável por popular a classe de Planificacion 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Shared Function PopularGetPlanificacion(dr As DataRow) As Comon.Clases.Planificacion

        ' criar objeto Planificacion
        Dim objPlanificacion As New Comon.Clases.Planificacion

        Util.AtribuirValorObjeto(objPlanificacion.Identificador, dr("OID_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.Codigo, dr("COD_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.Descripcion, dr("DES_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.FechaHoraVigenciaInicio, dr("FYH_VIGENCIA_INICIO"), GetType(DateTime))
        Util.AtribuirValorObjeto(objPlanificacion.FechaHoraVigenciaFin, dr("FYH_VIGENCIA_FIN"), GetType(DateTime))
        Util.AtribuirValorObjeto(objPlanificacion.BolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objPlanificacion.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objPlanificacion.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objPlanificacion.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))

        objPlanificacion.Cliente = New Comon.Clases.Cliente
        Util.AtribuirValorObjeto(objPlanificacion.Cliente.Identificador, dr("OID_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.Cliente.Codigo, dr("COD_CLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.Cliente.Descripcion, dr("DES_CLIENTE"), GetType(String))

        objPlanificacion.TipoPlanificacion = New Comon.Clases.TipoPlanificacion
        Util.AtribuirValorObjeto(objPlanificacion.TipoPlanificacion.Identificador, dr("OID_TIPO_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPlanificacion.TipoPlanificacion.Descripcion, dr("DES_TIPO_PLANIFICACION"), GetType(String))

        objPlanificacion.Programacion = New List(Of Comon.Clases.PlanXProgramacion)

        ' retornar objeto divisa preenchido
        Return objPlanificacion

    End Function

#End Region



End Class
