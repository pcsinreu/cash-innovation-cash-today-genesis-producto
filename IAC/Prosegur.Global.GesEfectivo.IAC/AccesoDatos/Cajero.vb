Imports Prosegur.DBHelper
Imports System.Text
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

''' <summary>
''' Classe ATM
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  07/01/2011  criado
''' </history>
Public Class Cajero

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Obtém ATMs através dos filtros informados
    ''' </summary>
    ''' <param name="CodCajero"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="CodGrupo"></param>
    ''' <param name="CodModeloCajero"></param>
    ''' <param name="CodRed"></param>
    ''' <param name="Vigente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011  criado
    ''' </history>
    Public Shared Function GetATMs(CodDelegacion As String, CodCajero As String, CodRed As String, _
                                   CodModeloCajero As String, CodGrupo As String, Vigente As Boolean, _
                                   CodCliente As String, CodsSubcliente As List(Of String), CodsPtoServicio As List(Of String)) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()
        Dim where As New StringBuilder()
        query.Append(My.Resources.GetATMs.ToString)

        ' adiciona filtros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))
        Util.AdicionarFiltroOpcionalLike("COD_CAJERO", ProsegurDbType.Identificador_Alfanumerico, CodCajero.ToUpper(), comando, where)
        If (Not String.IsNullOrEmpty(CodRed)) Then
            Util.AdicionarFiltroOpcionalLike("COD_RED", ProsegurDbType.Identificador_Alfanumerico, CodRed.ToUpper(), comando, where)
        End If
        If (Not String.IsNullOrEmpty(CodModeloCajero)) Then
            Util.AdicionarFiltroOpcionalLike("COD_MODELO_CAJERO", ProsegurDbType.Identificador_Alfanumerico, CodModeloCajero.ToUpper(), comando, where)
        End If
        If (Not String.IsNullOrEmpty(CodGrupo)) Then
            Util.AdicionarFiltroOpcionalLike("COD_GRUPO", ProsegurDbType.Identificador_Alfanumerico, CodGrupo.ToUpper(), comando, where)
        End If

        If CodCliente IsNot Nothing AndAlso CodCliente.Count > 0 Then

            where.Append("AND COD_CLIENTE ='" & CodCliente.ToUpper() & "'")

        End If

        If CodsSubcliente IsNot Nothing AndAlso CodsSubcliente.Count > 0 Then
            where.Append(Util.MontarClausulaIn(CodsSubcliente, "COD_SUBCLIENTE", comando, "AND"))
        End If

        If CodsPtoServicio IsNot Nothing AndAlso CodsPtoServicio.Count > 0 Then
            where.Append(Util.MontarClausulaIn(CodsPtoServicio, "COD_PTO_SERVICIO", comando, "AND"))
        End If

        comando.CommandText = Util.PrepararQuery(String.Format(query.ToString(), where.ToString()))
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Obtiene los datos principales del ATM de acuerdo a su filiación (Cliente/SubCliente/PuntoServicio).
    ''' </summary>
    ''' <param name="CodCliente">Código del Cliente</param>
    ''' <param name="CodSubCliente">Código del SubCliente</param>
    ''' <param name="CodPuntoServicio">Código del Punto de Servicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetATMByFiliacion(CodCliente As String, CodSubCliente As String, CodPuntoServicio As String) As Integracion.ContractoServicio.GetATM.ATM

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        With comando

            .CommandText = Util.PrepararQuery(My.Resources.GetATMByFiliacion)
            .CommandType = CommandType.Text

            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))
            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodSubCliente))
            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodPuntoServicio))

        End With

        Return PreencherObjetoATM(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando))

    End Function

    Private Shared Function PreencherObjetoATM(Table As DataTable) As Integracion.ContractoServicio.GetATM.ATM

        If Table IsNot Nothing AndAlso Table.Rows.Count = 1 Then

            Dim objAtm As New Integracion.ContractoServicio.GetATM.ATM

            Util.AtribuirValorObjeto(objAtm.CodigoCajero, Table.Rows(0)("COD_CAJERO"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.CodigoCliente, Table.Rows(0)("COD_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.CodigoPuntoServicio, Table.Rows(0)("COD_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.CodigoSubcliente, Table.Rows(0)("COD_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.DescripcionCliente, Table.Rows(0)("DES_CLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.DescripcionModeloCajero, Table.Rows(0)("DES_MODELO_CAJERO"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.DescripcionPuntoServicio, Table.Rows(0)("DES_PTO_SERVICIO"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.DescripcionRed, Table.Rows(0)("DES_RED"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.DescripcionSubcliente, Table.Rows(0)("DES_SUBCLIENTE"), GetType(String))
            Util.AtribuirValorObjeto(objAtm.IdCajero, Table.Rows(0)("OID_CAJERO"), GetType(String))

            Return objAtm

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Obtém ATMs através dos filtros informados
    ''' </summary>
    ''' <param name="codigoDelegacion"></param>
    ''' <param name="codigoCajero"></param>
    ''' <param name="codCliente"></param>
    ''' <param name="codigoSubcliente"></param>
    ''' <param name="codigoPuntoServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos]  17/03/2011  criado
    ''' </history>
    Public Shared Function GetATMsByRegistrarTira(codigoDelegacion As String,
                                                  codigoCajero As String,
                                                  codCliente As String,
                                                  codigoSubcliente As String,
                                                  codigoPuntoServicio As String) As List(Of Integracion.ContractoServicio.GetATMByRegistrarTira.ATM)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()
        Dim where As New StringBuilder()
        query.Append(My.Resources.GetATMsByRegistrarTira.ToString)

        ' adiciona filtros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

        If Not String.IsNullOrEmpty(codigoCajero) Then
            where.Append("AND UPPER(GEPR_TCAJERO.COD_CAJERO) = UPPER([]COD_CAJERO) ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CAJERO", ProsegurDbType.Identificador_Alfanumerico, codigoCajero))
        End If
        If Not String.IsNullOrEmpty(codCliente) Then
            where.Append("AND UPPER(GEPR_TCLIENTE.COD_CLIENTE) = UPPER([]COD_CLIENTE) ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, codCliente))
        End If
        If Not String.IsNullOrEmpty(codigoSubcliente) Then
            where.Append("AND UPPER(GEPR_TSUBCLIENTE.COD_SUBCLIENTE) = UPPER([]COD_SUBCLIENTE) ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, codigoSubcliente))
        End If
        If Not String.IsNullOrEmpty(codigoPuntoServicio) Then
            where.Append("AND UPPER(GEPR_TPUNTO_SERVICIO.COD_PTO_SERVICIO) = UPPER([]COD_PTO_SERVICIO) ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, codigoPuntoServicio))
        End If

        comando.CommandText = Util.PrepararQuery(String.Format(query.ToString(), where.ToString()))
        comando.CommandType = CommandType.Text
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Return DataTableToATMsByRegistrarTira(dt)

    End Function

 

    Private Shared Function DataTableToATMsByRegistrarTira(dt As DataTable) As List(Of Integracion.ContractoServicio.GetATMByRegistrarTira.ATM)

        Dim listaATMs As New List(Of Integracion.ContractoServicio.GetATMByRegistrarTira.ATM)
        Dim atm As New Integracion.ContractoServicio.GetATMByRegistrarTira.ATM


        ' verifica se os Cajeiros foram encontrados
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return Nothing
        End If

        'Dim listaCajeroXMorfologia As New List(Of Integracion.ContractoServicio.GetATMByRegistrarTira.CajeroXMorfologia)
        Dim listaCarXmorfAux As New List(Of CajeroXMorfologiaAux)

        For Each row In dt.Rows

            Dim id As String = row("OID_CAJERO")
            ' Impede que seja adcionado ATM em duplicidade.
            If listaATMs.Where(Function(F) F.OidCajero = id).ToList.Count <= 0 Then
                ' cria ATM
                atm = New Integracion.ContractoServicio.GetATMByRegistrarTira.ATM

                With atm

                    Util.AtribuirValorObjeto(.OidCajero, row("OID_CAJERO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoCajero, row("COD_CAJERO"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoRed, row("COD_RED"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoModeloCajero, row("COD_MODELO_CAJERO"), GetType(String))
                    Util.AtribuirValorObjeto(.OidGrupo, row("OID_GRUPO"), GetType(String))
                    Util.AtribuirValorObjeto(.BolRegistroTira, row("BOL_REGISTRO_TIRA"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.CodigoCliente, row("COD_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionCliente, row("DES_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoSubcliente, row("COD_SUBCLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionSubcliente, row("DES_SUBCLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(.CodigoPuntoServicio, row("COD_PTO_SERVICIO"), GetType(String))
                    Util.AtribuirValorObjeto(.DescripcionPuntoServicio, row("DES_PTO_SERVICIO"), GetType(String))
                    Util.AtribuirValorObjeto(.BolVigente, row("BOL_VIGENTE"), GetType(Boolean))
                    Util.AtribuirValorObjeto(.BolVigente, row("BOL_VIGENTE"), GetType(Boolean))
                    .FyhActualizacion = row("FYH_ACTUALIZACION")

                End With
                listaATMs.Add(atm)

            End If
            Dim carXmorfAux As New CajeroXMorfologiaAux
            With carXmorfAux
                Util.AtribuirValorObjeto(.OidCajero, row("OID_CAJERO"), GetType(String))
                Util.AtribuirValorObjeto(.OidMorfologia, row("OID_MORFOLOGIA"), GetType(String))
                .FecInicio = row("FEC_INICIO")
                Util.AtribuirValorObjeto(.BolVigente, row("BOL_VIGENTE_MORFOLOGIA"), GetType(Boolean))
                Util.AtribuirValorObjeto(.NecModalidadeRecogida, row("NEC_MODALIDAD_RECOGIDA"), GetType(Integer))
            End With
            listaCarXmorfAux.Add(carXmorfAux)
        Next

        For Each atmx In listaATMs
            Dim idCar = atmx.OidCajero
            Dim listaCarXmorfAuxSec As List(Of CajeroXMorfologiaAux) = listaCarXmorfAux.Where(Function(f) f.OidCajero = idCar).ToList
            atmx.CajeroXMorfologias = New List(Of Integracion.ContractoServicio.GetATMByRegistrarTira.CajeroXMorfologia)
            For Each CarXmorfAuxSec In listaCarXmorfAuxSec
                Dim CarXmorfSec As New Integracion.ContractoServicio.GetATMByRegistrarTira.CajeroXMorfologia
                CarXmorfSec.OidMorfologia = CarXmorfAuxSec.OidMorfologia
                CarXmorfSec.FecInicio = CarXmorfAuxSec.FecInicio
                CarXmorfSec.BolVigente = CarXmorfAuxSec.BolVigente
                CarXmorfSec.NecModalidadeRecogida = CarXmorfAuxSec.NecModalidadeRecogida
                atmx.CajeroXMorfologias.Add(CarXmorfSec)
            Next
        Next


        Return listaATMs
    End Function

    ''' <summary>
    ''' Obtém um ATM 
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Public Shared Function GetATMDetail(OidCajero As String, OidGrupo As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()
        Dim strClausulaWhere As String = String.Empty

        query.Append(My.Resources.GetATMDetail.ToString)

        'Adiciona Where caso algum parâmetro seja passado
        Dim clausulaWhere As New CriterioColecion

        ' adiciona filtros
        If Not String.IsNullOrEmpty(OidCajero) Then
            clausulaWhere.addCriterio("AND", " GEPR_TCAJERO.OID_CAJERO = []OID_CAJERO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CAJERO", ProsegurDbType.Objeto_Id, OidCajero))
        End If

        If Not String.IsNullOrEmpty(OidGrupo) Then
            clausulaWhere.addCriterio("AND", " GEPR_TGRUPO.OID_GRUPO = []OID_GRUPO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO", ProsegurDbType.Objeto_Id, OidGrupo))
        End If

        'Adiciona a clausula Where
        If clausulaWhere.Count > 0 Then
            strClausulaWhere = Util.MontarClausulaWhere(clausulaWhere)
        End If

        comando.CommandText = query.ToString() & " " & Util.PrepararQuery(strClausulaWhere)
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Obtém data de ultima atulização de um ATM 
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/01/2011  criado
    ''' </history>
    Public Shared Function GetFyhActulizacion(OidCajero As String) As DateTime

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetATMFuhActualizacion.ToString)
        comando.CommandType = CommandType.Text

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CAJERO", ProsegurDbType.Objeto_Id, OidCajero))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)("FYH_ACTUALIZACION")) Then
            Return Convert.ToDateTime(dt.Rows(0)("FYH_ACTUALIZACION"))
        Else
            Return Nothing
        End If

    End Function

    Public Shared Function RetornaCajero(IdPuntoServicio As String) As Integracion.ContractoServicio.GetProceso.Cajero

        Dim objCajero As New Integracion.ContractoServicio.GetProceso.Cajero

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        With comando

            .CommandText = Util.PrepararQuery(My.Resources.RecuperarATM)
            .CommandType = CommandType.Text

            .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IdPuntoServicio))

        End With

        Dim dtDados As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtDados IsNot Nothing AndAlso dtDados.Rows.Count = 1 Then

            Util.AtribuirValorObjeto(objCajero.IdCajero, dtDados.Rows(0)("OID_CAJERO"), GetType(String))
            Util.AtribuirValorObjeto(objCajero.CodigoCajero, dtDados.Rows(0)("COD_CAJERO"), GetType(String))
            Util.AtribuirValorObjeto(objCajero.RegistraTira, dtDados.Rows(0)("BOL_REGISTRO_TIRA"), GetType(Boolean))

        End If

        Return objCajero

    End Function

    Shared Function GetATMsSimplificado(Peticion As GetATMsSimplificado.Peticion) As GetATMsSimplificado.ATMColeccion
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = My.Resources.GetATMsSimplificado.ToString
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim respuesta As New GetATMsSimplificado.ATMColeccion

            For Each row In dt.Rows

                respuesta.Add(New GetATMsSimplificado.ATM With { _
                              .CodigoCajero = Util.AtribuirValorObj(row("COD_CAJERO"), GetType(String)),
                              .BolVigente = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean)),
                              .CodigoCliente = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String)),
                              .CodigoPuntoServicio = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String)),
                              .CodigoSubcliente = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String)),
                              .DescripcionCliente = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String)),
                              .DescripcionPuntoServicio = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String)),
                              .DescripcionSubcliente = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String)),
                              .OidCajero = Util.AtribuirValorObj(row("OID_CAJERO"), GetType(String))
                          })

            Next
            Return respuesta
        Else
            Return Nothing
        End If

    End Function

    Shared Function GetATMsSimplificadoV2(Peticion As GetATMsSimplificadoV2.Peticion) As GetATMsSimplificadoV2.ATMColeccion
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetATMsSimplificadoV2.ToString)
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodigoDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Peticion.EsVigente))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim respuesta As New GetATMsSimplificadoV2.ATMColeccion

            For Each row In dt.Rows

                respuesta.Add(New GetATMsSimplificadoV2.ATM With { _
                              .CodigoCajero = Util.AtribuirValorObj(row("COD_CAJERO"), GetType(String)),
                              .BolVigente = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean)),
                              .CodigoCliente = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String)),
                              .CodigoPuntoServicio = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String)),
                              .CodigoSubcliente = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String)),
                              .DescripcionCliente = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String)),
                              .DescripcionPuntoServicio = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String)),
                              .DescripcionSubcliente = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String)),
                              .OidCajero = Util.AtribuirValorObj(row("OID_CAJERO"), GetType(String))
                          })

            Next
            Return respuesta
        Else
            Return Nothing
        End If

    End Function

#End Region

#Region "[DELETES]"

    ''' <summary>
    ''' Obtém um ATM 
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Public Shared Sub EliminarATM(OidCajero As String, CodUsuario As String, FyhActualizacion As DateTime, _
                                  ByRef transacion As Prosegur.DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()

        query.Append(My.Resources.EliminarATM.ToString)

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajero", ProsegurDbType.Objeto_Id, OidCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_vigente", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        comando.CommandText = Util.PrepararQuery(query.ToString())
        comando.CommandType = CommandType.Text

        If transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[INSERTS]"

    ''' <summary>
    ''' Insere um ATM
    ''' </summary>
    ''' <param name="OidCajero"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  11/01/2011  criado
    ''' </history>
    Public Shared Sub InsertarATM(OidCajero As String, OidModeloCajero As String, OidRed As String, OidPtoServicio As String, _
                                  OidGrupo As String, CodCajero As String, CodDelegacion As String, BolRegistroTira As Boolean, _
                                  BolVigente As Boolean, CodUsuario As String, FyhActualizacion As DateTime, _
                                  ByRef Transacion As Prosegur.DbHelper.Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder()

        query.Append(My.Resources.InsertarATM.ToString)

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajero", ProsegurDbType.Objeto_Id, OidCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_modelo_cajero", ProsegurDbType.Objeto_Id, OidModeloCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_pto_servicio", ProsegurDbType.Objeto_Id, OidPtoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_cajero", ProsegurDbType.Identificador_Alfanumerico, CodCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_registro_tira", ProsegurDbType.Logico, BolRegistroTira))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_vigente", ProsegurDbType.Logico, BolVigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        If String.IsNullOrEmpty(OidRed) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_red", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_red", ProsegurDbType.Objeto_Id, OidRed))
        End If

        If String.IsNullOrEmpty(OidGrupo) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_grupo", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_grupo", ProsegurDbType.Objeto_Id, OidGrupo))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString())
        comando.CommandType = CommandType.Text

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region

#Region "[UPDATES]"

    ''' <summary>
    ''' Atualiza dados de um ATM
    ''' </summary>
    ''' <param name="BolRegistroTira"></param>
    ''' <param name="CodCajero"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="OidCajero"></param>
    ''' <param name="OidGrupo"></param>
    ''' <param name="OidModeloCajero"></param>
    ''' <param name="OidPtoServicio"></param>
    ''' <param name="OidRed"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="FyhActualizacion"></param>
    ''' <param name="Transacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/10/2011  criado
    ''' </history>
    Public Shared Sub ActualizarATM(OidCajero As String, OidModeloCajero As String, OidRed As String, OidPtoServicio As String, _
                                  OidGrupo As String, CodCajero As String, CodDelegacion As String, BolRegistroTira As Boolean, _
                                  CodUsuario As String, FyhActualizacion As DateTime, _
                                  ByRef Transacion As Prosegur.DbHelper.Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim query As New StringBuilder
        query.Append(My.Resources.ActualizarATM.ToString)

        ' configura parâmetros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cajero", ProsegurDbType.Objeto_Id, OidCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_modelo_cajero", ProsegurDbType.Objeto_Id, OidModeloCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_pto_servicio", ProsegurDbType.Objeto_Id, OidPtoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_cajero", ProsegurDbType.Identificador_Alfanumerico, CodCajero))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, CodDelegacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "bol_registro_tira", ProsegurDbType.Logico, BolRegistroTira))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data_Hora, FyhActualizacion))

        If String.IsNullOrEmpty(OidRed) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_red", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_red", ProsegurDbType.Objeto_Id, OidRed))
        End If

        If String.IsNullOrEmpty(OidGrupo) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_grupo", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_grupo", ProsegurDbType.Objeto_Id, OidGrupo))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        If Transacion Is Nothing Then
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        Else
            Transacion.AdicionarItemTransacao(comando)
        End If

    End Sub

#End Region



End Class

Friend Structure CajeroXMorfologiaAux
    Property OidCajero As String
    Property OidMorfologia As String
    Property FecInicio As Date
    Property BolVigente As Boolean
    Property NecModalidadeRecogida As Integer
End Structure
