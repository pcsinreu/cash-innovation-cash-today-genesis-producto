Imports Prosegur.DbHelper
Imports System.Text

Public Class InformeResultadoContaje

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Selecionar
    ''' </summary>
    ''' <param name="IdBulto"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 20/05/2009 Criado    
    ''' </history>
    Private Shared Function RetornarCaracteristicas(IdBulto As String) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion

        ' inicializar variáveis
        Dim Caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion = Nothing
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim dt As DataTable = Nothing

        ' obter comando
        comando.CommandText = Util.PrepararQuery(My.Resources.CaracteristicaSelecionar.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_BULTO", ProsegurDbType.Objeto_Id, IdBulto))

        ' executar comando
        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se exitir registros
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Caracteristicas = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion

            For Each dr As DataRow In dt.Rows

                ' adicionar para coleção
                Caracteristicas.Add(PopularCaracteristica(dr))

            Next

        End If

        Return Caracteristicas

    End Function

    ''' <summary>
    ''' Recupera as informações de contagem da remesa
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/12/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarInformacionesResultadoContaje(peticion As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa

        Dim objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa = Nothing

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        ' criar comando
        Dim comando As IDbCommand = conexao.CreateCommand()

        ' executar consulta
        Dim drResutaldoContaje As IDataReader = Nothing

        Try

            ' Limpa o objeto da memória quando termina de usá-lo
            Using comando

                comando.CommandText = Constantes.PKG_INFORME_RESULTADO_CONTAJE
                comando.CommandType = CommandType.StoredProcedure

                comando.Parameters.Add(Util.CriarParametroOracle("cv_InfParcial", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_InfAdCliente", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Declarados", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Efectivos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_MediosPago", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Intervenciones", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Motivos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_Falsos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(Util.CriarParametroOracle("cv_PrecintosBultos", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_REMESA", ProsegurDbType.Objeto_Id, peticion.OidRemesa))

                ' executar consulta
                drResutaldoContaje = comando.ExecuteReader()

                objRemesa = PopularInformeContaje(drResutaldoContaje)

                ' verifica se pelo menos uma das coleções foi preenchida
                ' caso não exista nenhuma, deverá retorna nothing no objeto
                If objRemesa.Bultos Is Nothing AndAlso objRemesa.Declarados Is Nothing AndAlso objRemesa.Diferencias Is Nothing AndAlso objRemesa.IACs Is Nothing AndAlso objRemesa.InfoCliente Is Nothing AndAlso objRemesa.Intervenciones Is Nothing Then
                    objRemesa = Nothing
                Else
                    objRemesa.CodIsoDivisaLocal = Bulto.RetornarDivisaLocal(objRemesa.Bultos(0).IdentificadorBulto)
                End If

            End Using

        Catch ex As Exception

            Throw

        Finally

            ' Limpa os parâmetros utilizados anteriormente
            comando.Parameters.Clear()

            ' Fecha a conexão do Data Reader
            If drResutaldoContaje IsNot Nothing Then
                drResutaldoContaje.Close()
                drResutaldoContaje.Dispose()
            End If

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return objRemesa

    End Function

    ''' <summary>
    ''' Busca as informações de contagem
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function BuscarInformacionesResultadoContaje(Peticion As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion) As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.InformacionesContejeSelecionar.ToString
        cmd.CommandType = CommandType.Text

        Dim query As New StringBuilder

        If Not String.IsNullOrEmpty(Peticion.CodDelegacion) Then

            query.Append(" AND R.COD_DELEGACION = []COD_DELEGACION ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodDelegacion))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodCliente) Then

            query.Append(" AND B.COD_CLIENTE = []COD_CLIENTE ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodCliente))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodPrecintoBulto) Then

            query.Append(" AND B.COD_PRECINTO = []PRECINTO_BULTO ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "PRECINTO_BULTO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodPrecintoBulto))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodPrecintoRemesa) Then

            query.Append(" AND R.COD_PRECINTO = []PRECINTO_REMESA ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "PRECINTO_REMESA", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodPrecintoRemesa))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodPuntoServicio) Then

            query.Append(" AND B.COD_PUNTO_SERVICIO = []COD_PUNTO_SERVICIO ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodPuntoServicio))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodSubCliente) Then

            query.Append(" AND  B.COD_SUBCLIENTE = []COD_SUBCLIENTE ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodSubCliente))

        End If

        If Not String.IsNullOrEmpty(Peticion.CodTransporte) Then

            query.Append(" AND B.COD_TRANSPORTE = []COD_TRANSPORTE ")

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TRANSPORTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.CodTransporte))

        End If

        If Not Peticion.FechaInicio.Equals(Date.MinValue) AndAlso Not Peticion.FechaFin.Equals(Date.MinValue) Then

            If Peticion.EsFechaTransporte Then

                query.Append(" AND (TRUNC(B.FEC_TRANSPORTE) >= TRUNC(TO_DATE([]FEC_INICIO)) AND TRUNC(B.FEC_TRANSPORTE) <= TRUNC(TO_DATE([]FEC_FIN)))  ")

            Else

                query.Append(" AND (TRUNC(R.FYH_INICIO_CONTEO) >= TRUNC(TO_DATE([]FEC_INICIO)) AND TRUNC(R.FYH_FIN_CONTEO) <= TRUNC(TO_DATE([]FEC_FIN)))  ")

            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_INICIO", ProsegurDbType.Data_Hora, Peticion.FechaInicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FEC_FIN", ProsegurDbType.Data_Hora, Peticion.FechaFin))

        End If

        cmd.CommandText &= query.ToString
        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return RetornarBuscaInformacionesContaje(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd))
    End Function

#End Region

#Region "[POPULAR]"

    ''' <summary>
    ''' Popula a busca das informações de contagem
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RetornarBuscaInformacionesContaje(dt As DataTable) As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion

        Dim objRemesas As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objRemesas = New ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion

            For Each dr In dt.Rows
                Dim drLocal = dr

                If (From remesa In objRemesas Where remesa.OidRemsa = drLocal("OID_REMESA")).Count > 0 Then
                    Continue For
                End If

                objRemesas.Add(New ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Remesa With { _
                                                .Cliente = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String)),
                                                .CodTransporte = Util.AtribuirValorObj(dr("COD_TRANSPORTE"), GetType(String)), _
                                                .FechaConteo = Util.AtribuirValorObj(dr("FYH_FIN_CONTEO"), GetType(DateTime)), _
                                                .OidRemsa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String)), _
                                                .PrecintoRemesa = Util.AtribuirValorObj(dr("PRECINTO_REMESA"), GetType(String)), _
                                                .PuntoServicio = Util.AtribuirValorObj(dr("DES_PUNTO_SERVICIO"), GetType(String)), _
                                                .SubCliente = Util.AtribuirValorObj(dr("DES_SUBCLIENTE"), GetType(String)),
                                                .FechaTransporte = Util.AtribuirValorObj(dr("FEC_TRANSPORTE"), GetType(String))})
            Next

        End If

        Return objRemesas
    End Function

    ''' <summary>
    ''' PopularCaracteristica
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 20/05/2009 Criado    
    ''' </history>
    Private Shared Function PopularCaracteristica(dr As DataRow) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Caracteristica

        ' criar objeto
        Dim objCaracteristica As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Caracteristica

        If dr("COD_CARACTERISTICA") IsNot DBNull.Value Then
            objCaracteristica.Codigo = dr("COD_CARACTERISTICA")
        End If

        If dr("DES_CARACTERISTICA") IsNot DBNull.Value Then
            objCaracteristica.Descripcion = dr("DES_CARACTERISTICA")
        End If

        If dr("COD_CARACTERISTICA_CONTEO") IsNot DBNull.Value Then
            objCaracteristica.CodigoCaracteristicaConteo = dr("COD_CARACTERISTICA_CONTEO")
        End If

        ' retornar objeto
        Return objCaracteristica

    End Function

    ''' <summary>
    ''' Popular objeto Parte Diferencias
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularInformeContaje(dr As IDataReader) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa

        Dim objRemesa As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa

        ' CURSOR INFORMACIONES PARCIALES
        While (dr.Read)
            PopularInfContaje_InfoCliente(dr, objRemesa)
        End While

        ' CURSOR INFORMACIONES ADICIONALES
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_IAC(dr, objRemesa)
        End While

        ' CURSOR DECLARADOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_Declarados(dr, objRemesa)
        End While

        ' CURSOR EFECTIVOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_Efectivos(dr, objRemesa)
        End While

        ' CURSOR MEDIO PAGOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_MediosPago(dr, objRemesa)
        End While

        ' CURSOR OBSERVACIONES
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_Intervenciones(dr, objRemesa)
        End While

        ' CURSOR MOTIVOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_Motivos(dr, objRemesa)
        End While

        ' CURSOR FALSOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_Falsos(dr, objRemesa)
        End While

        ' CURSOR PRECINTOSBULTOS
        dr.NextResult()
        While (dr.Read)
            PopularInfContaje_PrecintosBultos(dr, objRemesa)
        End While

        ' só adiciona o número do documento se realmente existirem no objeto
        If objRemesa IsNot Nothing Then
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                Dim caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion
                caracteristicas = RetornarCaracteristicas(objRemesa.Bultos.First().IdentificadorBulto)
                For Each b In objRemesa.Bultos
                    b.Caracteristicas = caracteristicas
                Next
            End If
        End If

        Return objRemesa
    End Function

    ''' <summary>
    ''' Cria uma nova Parcial no Objeto Remesa
    ''' </summary>
    ''' <param name="oidParcial"></param>
    ''' <param name="objBulto"></param>
    ''' <remarks></remarks>
    Private Shared Sub CriaParcial(oidParcial As String, ByRef objBulto As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Bulto)

        If objBulto.Parciales Is Nothing Then
            objBulto.Parciales = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.ParcialColeccion
        End If

        Dim oParcial As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Parcial
        oParcial.IdentificadorParcial = oidParcial
        oParcial.IACs = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IACColeccion
        oParcial.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion
        oParcial.Diferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion
        oParcial.Intervenciones = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion
        oParcial.Efectivos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion
        oParcial.MediosPagos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.MedioPagoColeccion

        objBulto.Parciales.Add(oParcial)

    End Sub

    ''' <summary>
    ''' Cria um novo Malote no Objeto Remesa
    ''' </summary>
    ''' <param name="oidBulto"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub CriaBulto(oidBulto As String, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        If objRemesa.Bultos Is Nothing Then
            objRemesa.Bultos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.BultoColeccion
        End If

        Dim oBulto As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Bulto
        oBulto.IdentificadorBulto = oidBulto
        oBulto.IACs = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IACColeccion
        oBulto.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion
        oBulto.Diferencias = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DiferenciaColeccion
        oBulto.Intervenciones = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion
        oBulto.Parciales = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.ParcialColeccion

        objRemesa.Bultos.Add(oBulto)

    End Sub

    ''' <summary>
    ''' Insere objeto Falso nas Intervenções
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objIntervenciones"></param>
    ''' <remarks></remarks>
    Private Shared Sub InserirFalsosEmIntervenciones(dr As IDataReader, ByRef objIntervenciones As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion)

        For Each objIntervencion In objIntervenciones
            If objIntervencion.IdentificadorIntervencion = Util.VerificarDBNull(dr("OID_INTERVENCION")) Then

                If objIntervencion.Falsos Is Nothing Then
                    ' Caso o objeto seja Nothing, Cria
                    objIntervencion.Falsos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.FalsoColeccion
                    objIntervencion.Falsos.Add(CriaFalso(dr))
                Else

                    ' Verifica se existe o Falso
                    Dim existe_Falso As Boolean = (From b In objIntervencion.Falsos Where b.CodDenominacion = dr("COD_DENOMINACION")).Count > 0
                    If existe_Falso Then
                        'Percorer os falsos e verifica se já foi adicioando, senão adciona
                        For Each objFalso In objIntervencion.Falsos
                            If objFalso.CodDenominacion = dr("COD_DENOMINACION") Then
                                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("DES_NUMERO_PLANCHA"), GetType(String))) OrElse _
                                    Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("DES_NUMERO_SERIE"), GetType(String))) Then

                                    objFalso.InformacionesBilletes.Add(CriaInformacionBillete(dr))
                                    objFalso.Unidades += 1

                                End If
                            End If
                        Next
                    Else
                        ' Senão existe o Falso, cria
                        objIntervencion.Falsos.Add(CriaFalso(dr))
                    End If

                End If

            End If
        Next
    End Sub

    ''' <summary>
    ''' Insere objeto Motivos nas Intervenções
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objIntervenciones"></param>
    ''' <remarks></remarks>
    Private Shared Sub InserirMotivosEmIntervenciones(dr As IDataReader, ByRef objIntervenciones As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion)

        For Each objIntervencion In objIntervenciones.Where(Function(par) par.IdentificadorIntervencion = Util.VerificarDBNull(dr("OID_INTERVENCION")))

            If objIntervencion.Motivos Is Nothing Then
                ' Caso o objeto seja Nothing, Cria
                objIntervencion.Motivos = New List(Of String)
            End If

            objIntervencion.Motivos.Add(dr("COD_MOTIVO"))

        Next
    End Sub

    ''' <summary>
    ''' Cria objeto Falso
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CriaFalso(dr As IDataReader) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Falso

        ' Preenche objeto de Falso
        Dim objFalso As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Falso
        With objFalso
            .CodDenominacion = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
            .CodIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
            .InformacionesBilletes = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionBilleteColeccion
            .NombreDivisa = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))
            .Unidades = Util.AtribuirValorObj(dr("NEL_UNIDADES"), GetType(Integer))
            .DesDenominacion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
            .InformacionesBilletes = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionBilleteColeccion
            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("DES_NUMERO_PLANCHA"), GetType(String))) OrElse _
                Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("DES_NUMERO_SERIE"), GetType(String))) Then

                .InformacionesBilletes.Add(CriaInformacionBillete(dr))

            End If

        End With

        Return objFalso

    End Function

    ''' <summary>
    ''' Cria objeto Informacion Billete
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CriaInformacionBillete(dr As IDataReader) As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionBillete

        ' Preenche objeto de Informacion Billete
        Dim objColInformacionBillete As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionBillete
        With objColInformacionBillete
            .NumPlancha = Util.AtribuirValorObj(dr("DES_NUMERO_PLANCHA"), GetType(String))
            .NumSerie = Util.AtribuirValorObj(dr("DES_NUMERO_SERIE"), GetType(String))
        End With

        Return objColInformacionBillete
    End Function

    ''' <summary>
    ''' Popula as informações do cliente
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_InfoCliente(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim oidParcial As String = Util.AtribuirValorObj(dr("OID_PARCIAL"), GetType(String))
        Dim oidBulto As String = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

        If objRemesa.InfoCliente Is Nothing Then
            objRemesa.InfoCliente = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.InformacionCliente
        End If

        objRemesa.IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))
        objRemesa.InfoCliente.CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
        objRemesa.InfoCliente.CodSubCliente = Util.AtribuirValorObj(dr("COD_SUBCLIENTE"), GetType(String))
        objRemesa.InfoCliente.CodTransporte = Util.AtribuirValorObj(dr("COD_TRANSPORTE"), GetType(String))
        objRemesa.InfoCliente.DescCliente = Util.AtribuirValorObj(dr("NOMBRE_CLIENTE"), GetType(String))
        objRemesa.InfoCliente.DescSubCliente = Util.AtribuirValorObj(dr("NOMBRE_SUBCLIENTE"), GetType(String))
        objRemesa.InfoCliente.FechaProceso = Util.AtribuirValorObj(dr("FECHA_PROCESO"), GetType(DateTime))
        objRemesa.InfoCliente.FechaTransporte = Util.AtribuirValorObj(dr("FECHA_TRANSPORTE"), GetType(DateTime))
        objRemesa.CodPrecinto = Util.AtribuirValorObj(dr("NUM_REMESA"), GetType(String))
        objRemesa.FechaFinConteo = Util.AtribuirValorObj(dr("FECHA_CONTEO_REMESA"), GetType(String))
        objRemesa.CodUsuario = Util.AtribuirValorObj(dr("COD_USUARIO_REMESA"), GetType(String))
        objRemesa.InfoCliente.CodDelegacion = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String))
        objRemesa.InfoCliente.CodPuntoServicio = Util.AtribuirValorObj(dr("COD_PUNTO_SERVICIO"), GetType(String))
        objRemesa.InfoCliente.DescPuntoServicio = Util.AtribuirValorObj(dr("DES_PUNTO_SERVICIO"), GetType(String))

        If Not String.IsNullOrEmpty(oidBulto) Then

            Dim objBulto As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Bulto

            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = oidBulto).Count = 0 Then
                CriaBulto(oidBulto, objRemesa)
            End If

            objBulto = (From bul In objRemesa.Bultos Where bul.IdentificadorBulto = oidBulto).FirstOrDefault
            With objBulto
                .NumPrecinto = Util.AtribuirValorObj(dr("NUM_PRECINTO"), GetType(String))
                .FechaFinConteo = Util.AtribuirValorObj(dr("FECHA_CONTEO_BULTO"), GetType(String))
                .CodUsuario = Util.AtribuirValorObj(dr("COD_USUARIO_BULTO"), GetType(String))
            End With

            If objBulto IsNot Nothing Then

                If objBulto.Parciales Is Nothing OrElse (From par In objBulto.Parciales Where par.IdentificadorParcial = oidParcial).Count = 0 Then
                    CriaParcial(oidParcial, objBulto)
                End If

                Dim objParcial = (From par In objBulto.Parciales Where par.IdentificadorParcial = oidParcial).FirstOrDefault

                With objParcial
                    .FechaFinConteo = Util.AtribuirValorObj(dr("FECHA_CONTEO_PARC"), GetType(String))
                    .CodUsuario = Util.AtribuirValorObj(dr("COD_USUARIO_PARC"), GetType(String))
                    .NumParcial = Util.AtribuirValorObj(dr("NUM_PARCIAL"), GetType(Integer))
                End With
            End If
        End If

    End Sub

    ''' <summary>
    ''' Popula as informações de IAC para Remesa, Bulto e Parcial
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    Private Shared Sub PopularInfContaje_IAC(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        ' Preenche o objeto de IAC
        Dim objIac As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IAC
        With objIac
            .CodTermino = Util.AtribuirValorObj(dr("COD_TERMINO"), GetType(String))
            .DesTermino = Util.AtribuirValorObj(dr("DES_TERMINO"), GetType(String))
            .ValorTermino = Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String))
        End With

        Dim OidParcial As String = Util.VerificarDBNull(dr("OID_PARCIAL"))
        Dim OidBulto As String = Util.VerificarDBNull(dr("OID_BULTO"))

        If Not String.IsNullOrEmpty(OidParcial) Then

            ' IAC a nível de Parcial
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then

                    If b.Parciales Is Nothing OrElse (From par In b.Parciales Where par.IdentificadorParcial = OidParcial).Count = 0 Then
                        CriaParcial(OidParcial, b)
                    End If

                    For Each p In b.Parciales.Where(Function(par) par.IdentificadorParcial = OidParcial)
                        If p.IACs Is Nothing Then
                            p.IACs = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IACColeccion
                        End If
                        p.IACs.Add(objIac)
                    Next
                End If
            Next

        ElseIf Not String.IsNullOrEmpty(OidBulto) Then

            ' IAC a nível de Bulto
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then
                    If b.IACs Is Nothing Then
                        b.IACs = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IACColeccion
                    End If
                    b.IACs.Add(objIac)
                End If
            Next

        Else

            ' IAC a nível de Remesa
            If objRemesa.IACs Is Nothing Then
                objRemesa.IACs = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IACColeccion
            End If

            objRemesa.IACs.Add(objIac)

        End If


    End Sub

    ''' <summary>
    ''' Popula as informações de declarado para parcial, bulto e remesa
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_Declarados(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim OidParcial As String = Util.AtribuirValorObj(dr("OID_PARCIAL"), GetType(String))
        Dim OidBulto As String = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))
        Dim OTipoContenedor As String = Util.AtribuirValorObj(dr("TIPO_DECLARADO"), GetType(String))

        ' Preenche o objeto Declarado
        Dim objDeclarado As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Declarado
        With objDeclarado
            .CodIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
            .DescripcionDivisa = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))
            .ImporteCheque = Util.AtribuirValorObj(dr("NUM_IMPORTE_CHEQUE"), GetType(Decimal))
            .ImporteEfectivo = Util.AtribuirValorObj(dr("NUM_IMPORTE_EFECTIVO"), GetType(Decimal))
            .ImporteOtroValor = Util.AtribuirValorObj(dr("NUM_IMPORTE_OTRO_VALOR"), GetType(Decimal))
            .ImporteTicket = Util.AtribuirValorObj(dr("NUM_IMPORTE_TICKET"), GetType(Decimal))
            .ImporteTotal = Util.AtribuirValorObj(dr("NUM_IMPORTE_TOTAL"), GetType(Decimal))
        End With

        If OTipoContenedor = ContractoServ.Constantes.CONST_TIPO_CONTENEDOR_PARCIAL Then

            ' Nível de Parcial
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then

                    If b.Parciales Is Nothing OrElse (From par In b.Parciales Where par.IdentificadorParcial = OidParcial).Count = 0 Then
                        CriaParcial(OidParcial, b)
                    End If

                    For Each p In b.Parciales.Where(Function(par) par.IdentificadorParcial = OidParcial)
                        If p.Declarados Is Nothing Then
                            p.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion
                        End If
                        p.Declarados.Add(objDeclarado)
                    Next
                End If
            Next

        ElseIf OTipoContenedor = ContractoServ.Constantes.CONST_TIPO_CONTENEDOR_BULTO Then

            ' Nível de Bulto
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then
                    If b.Declarados Is Nothing Then
                        b.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion
                    End If
                    b.Declarados.Add(objDeclarado)
                End If
            Next

        Else

            If objRemesa.Declarados Is Nothing Then
                objRemesa.Declarados = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.DeclaradoColeccion
            End If

            ' Nível de Remesa
            objRemesa.Declarados.Add(objDeclarado)

        End If

    End Sub

    ''' <summary>
    ''' Popula os Efectivos Contados da remesa
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_Efectivos(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim OidParcial As String = Util.AtribuirValorObj(dr("OID_PARCIAL"), GetType(String))
        Dim OidBulto As String = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

        ' Preenche objeto de Efectivos
        Dim objEfectivo As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Efectivo
        With objEfectivo
            .CodIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
            .Denominacion = Util.AtribuirValorObj(dr("DENOMINACION"), GetType(Decimal))
            .desDenominacion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
            .Deteriorado = Util.AtribuirValorObj(dr("DETERIORADO"), GetType(Integer))
            .TipoEfectivo = Util.AtribuirValorObj(dr("TIPO_EFECTIVO"), GetType(String))
            .Falso = Util.AtribuirValorObj(dr("FALSO"), GetType(Integer))
            .NombreDivisa = Util.AtribuirValorObj(dr("DIVISA"), GetType(String))
            .Unidades = Util.AtribuirValorObj(dr("UNIDADES"), GetType(Integer))
        End With

        If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
            CriaBulto(OidBulto, objRemesa)
        End If

        For Each b In objRemesa.Bultos
            If b.IdentificadorBulto = OidBulto Then

                If b.Parciales Is Nothing OrElse (From par In b.Parciales Where par.IdentificadorParcial = OidParcial).Count = 0 Then
                    CriaParcial(OidParcial, b)
                End If

                For Each p In b.Parciales.Where(Function(par) par.IdentificadorParcial = OidParcial)
                    If p.Efectivos Is Nothing Then
                        p.Efectivos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.EfectivoColeccion
                    End If
                    p.Efectivos.Add(objEfectivo)
                Next
            End If
        Next

    End Sub

    ''' <summary>
    ''' Retorna os meios de pagamento contados da remesa pesquisada
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_MediosPago(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim OidParcial As String = Util.AtribuirValorObj(dr("OID_PARCIAL"), GetType(String))
        Dim OidBulto As String = Util.AtribuirValorObj(dr("OID_BULTO"), GetType(String))

        ' Preenche objeto de Medio Pago
        Dim ObjMedioPago As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.MedioPago
        With ObjMedioPago
            .CodIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
            .CodTipoMedioPago = Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
            .Importe = Util.AtribuirValorObj(dr("VALOR"), GetType(Decimal))
            .NombreMedioPago = Util.AtribuirValorObj(dr("TIPO_MEDIO_PAGO"), GetType(String))
            .DescripcionDivisa = Util.AtribuirValorObj(dr("DIVISA"), GetType(String))
        End With

        If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
            CriaBulto(OidBulto, objRemesa)
        End If

        For Each b In objRemesa.Bultos
            If b.IdentificadorBulto = OidBulto Then

                If b.Parciales Is Nothing OrElse (From par In b.Parciales Where par.IdentificadorParcial = OidParcial).Count = 0 Then
                    CriaParcial(OidParcial, b)
                End If

                For Each p In b.Parciales.Where(Function(par) par.IdentificadorParcial = OidParcial)
                    If p.MediosPagos Is Nothing Then
                        p.MediosPagos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.MedioPagoColeccion
                    End If
                    p.MediosPagos.Add(ObjMedioPago)
                Next
            End If
        Next


    End Sub

    ''' <summary>
    ''' Recupera as intervencions da remesa, bulto e parcial
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_Intervenciones(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        ' Preenche objeto de Intervencion
        Dim objIntervencion As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Intervencion
        With objIntervencion
            .CodContador = Util.AtribuirValorObj(dr("COD_CONTADOR"), GetType(String))
            .IdentificadorIntervencion = Util.AtribuirValorObj(dr("OID_INTERVENCION"), GetType(String))
            .CodSupervisor = Util.AtribuirValorObj(dr("COD_SUPERVISOR"), GetType(String))
            .Comentario = Util.AtribuirValorObj(dr("DES_COMENTARIO"), GetType(String))
            .FechaFinIntervencion = Util.AtribuirValorObj(dr("FYH_FIN_INTERV"), GetType(DateTime))
            .Falsos = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.FalsoColeccion
        End With

        Dim OidParcial As String = Util.VerificarDBNull(dr("OID_PARCIAL"))
        Dim OidBulto As String = Util.VerificarDBNull(dr("OID_BULTO"))

        If Not String.IsNullOrEmpty(OidParcial) AndAlso Not String.IsNullOrEmpty(OidBulto) Then

            ' IAC a nível de Parcial
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then

                    If b.Parciales Is Nothing OrElse (From par In b.Parciales Where par.IdentificadorParcial = OidParcial).Count = 0 Then
                        CriaParcial(OidParcial, b)
                    End If

                    For Each p In b.Parciales.Where(Function(par) par.IdentificadorParcial = OidParcial)
                        If p.Intervenciones Is Nothing Then
                            p.Intervenciones = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion
                        End If
                        p.Intervenciones.Add(objIntervencion)
                    Next
                End If
            Next

        ElseIf Not String.IsNullOrEmpty(OidBulto) Then

            ' IAC a nível de Bulto
            If objRemesa.Bultos Is Nothing OrElse (From b In objRemesa.Bultos Where b.IdentificadorBulto = OidBulto).Count = 0 Then
                CriaBulto(OidBulto, objRemesa)
            End If

            For Each b In objRemesa.Bultos
                If b.IdentificadorBulto = OidBulto Then
                    If b.Intervenciones Is Nothing Then
                        b.Intervenciones = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion
                    End If
                    b.Intervenciones.Add(objIntervencion)
                End If
            Next

        Else

            ' IAC a nível de Remesa
            If objRemesa.Intervenciones Is Nothing Then
                objRemesa.Intervenciones = New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.IntervencionColeccion
            End If

            objRemesa.Intervenciones.Add(objIntervencion)

        End If

    End Sub

    ''' <summary>
    ''' Recupera os Billetes e Monedas Falsas
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_Falsos(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim OidParcial As String = Util.VerificarDBNull(dr("OID_PARCIAL"))
        Dim OidBulto As String = Util.VerificarDBNull(dr("OID_BULTO"))
        Dim OidIntervencion As String = Util.VerificarDBNull(dr("OID_INTERVENCION"))

        If Not String.IsNullOrEmpty(OidIntervencion) Then

            If Not String.IsNullOrEmpty(OidParcial) Then

                ' Nível Parcial
                For Each objBulto In objRemesa.Bultos
                    If objBulto.IdentificadorBulto = OidBulto Then
                        For Each objParcial In objBulto.Parciales
                            If objParcial.IdentificadorParcial = OidParcial AndAlso objParcial.Intervenciones IsNot Nothing Then
                                InserirFalsosEmIntervenciones(dr, objParcial.Intervenciones)
                            End If
                        Next
                    End If
                Next

            ElseIf Not String.IsNullOrEmpty(OidBulto) Then

                ' Nível Bulto
                For Each objBulto In objRemesa.Bultos
                    If objBulto.IdentificadorBulto = OidBulto AndAlso objBulto.Intervenciones IsNot Nothing Then
                        InserirFalsosEmIntervenciones(dr, objBulto.Intervenciones)
                    End If
                Next

            ElseIf objRemesa.Intervenciones IsNot Nothing Then
                ' Nível Remesa
                InserirFalsosEmIntervenciones(dr, objRemesa.Intervenciones)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Recupera os Precintos do Bulto
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_PrecintosBultos(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim codigoPrecintoBulto As String = Util.VerificarDBNull(dr("COD_PRECINTO"))

        objRemesa.CodPrecintosBultos &= codigoPrecintoBulto & ContractoServ.Constantes.CONST_DELIMITADOR

        If objRemesa IsNot Nothing Then
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each b In objRemesa.Bultos.Where(Function(bulto) bulto.NumPrecinto = codigoPrecintoBulto)
                    b.NumeroParcialesDeclarados = If(Util.VerificarDBNull(dr("NEC_PARCIALES")) Is Nothing, 0, Util.VerificarDBNull(dr("NEC_PARCIALES")))
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' Recupera os Motivos
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="objRemesa"></param>
    ''' <remarks></remarks>
    Private Shared Sub PopularInfContaje_Motivos(dr As IDataReader, ByRef objRemesa As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)

        Dim OidParcial As String = Util.VerificarDBNull(dr("OID_PARCIAL"))
        Dim OidBulto As String = Util.VerificarDBNull(dr("OID_BULTO"))
        Dim OidIntervencion As String = Util.VerificarDBNull(dr("OID_INTERVENCION"))

        If Not String.IsNullOrEmpty(OidIntervencion) Then

            If Not String.IsNullOrEmpty(OidParcial) Then

                ' Nível Parcial
                For Each objBulto In objRemesa.Bultos
                    If objBulto.IdentificadorBulto = OidBulto Then
                        For Each objParcial In objBulto.Parciales
                            If objParcial.IdentificadorParcial = OidParcial AndAlso objParcial.Intervenciones IsNot Nothing Then
                                InserirMotivosEmIntervenciones(dr, objParcial.Intervenciones)
                            End If
                        Next
                    End If
                Next

            ElseIf Not String.IsNullOrEmpty(OidBulto) Then

                ' Nível Bulto
                For Each objBulto In objRemesa.Bultos
                    If objBulto.IdentificadorBulto = OidBulto AndAlso objBulto.Intervenciones IsNot Nothing Then
                        InserirMotivosEmIntervenciones(dr, objBulto.Intervenciones)
                    End If
                Next

            ElseIf objRemesa.Intervenciones IsNot Nothing Then
                ' Nível Remesa
                InserirMotivosEmIntervenciones(dr, objRemesa.Intervenciones)
            End If

        End If

    End Sub

#End Region

End Class
