Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Public Class Modulo

    ''' <summary>
    ''' Permite realizar el alta, modificación y baja lógica  
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 31/03/2015 Criado
    ''' </history>
    Public Shared Function SetModulo(peticion As Integracion.ContractoServicio.Modulo.SetModulo.Peticion) As String

        Dim oidCliente As String = String.Empty
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        comando.CommandType = CommandType.Text

        'obtém o oidListaValor
        Dim listaValor = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.ListaTipoValores.GetValores(Prosegur.Genesis.Comon.Constantes.CODIGO_LISTA_TIPO_FORMATO, peticion.Modulo.CodEmbalaje, Nothing, Nothing)
        Dim oidListaValor As String = String.Empty
        If listaValor IsNot Nothing AndAlso listaValor.Count > 0 Then
            oidListaValor = listaValor.First.OidValor
        End If

        Dim oidModulo = GetOidModulo(peticion.Modulo.CodModulo)
        If peticion.Modulo.CodCliente IsNot Nothing AndAlso peticion.Modulo.CodCliente <> String.Empty Then
            oidCliente = AccesoDatos.Cliente.BuscarOidCliente(peticion.Modulo.CodCliente)
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_LISTA_VALOR", ProsegurDbType.Identificador_Alfanumerico, oidListaValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, oidCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MODULO", ProsegurDbType.Descricao_Longa, peticion.Modulo.DesModulo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, peticion.Modulo.BolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, peticion.Modulo.GmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, peticion.Modulo.DesUsuarioModificacion))

        'Alta
        If String.IsNullOrEmpty(oidModulo) Then

            oidModulo = Guid.NewGuid.ToString
            comando.CommandText = Util.PrepararQuery(My.Resources.InserirTipoModulo.ToString)
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, oidModulo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.Modulo.CodModulo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, peticion.Modulo.GmtModificacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, peticion.Modulo.DesUsuarioModificacion))

            objtransacion.AdicionarItemTransacao(comando)
            objtransacion.RealizarTransacao()

        Else
            'Modificacion
            comando.CommandText = Util.PrepararQuery(My.Resources.AtualizarTipoModulo.ToString)

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, oidModulo))

            objtransacion.AdicionarItemTransacao(comando)
            objtransacion.RealizarTransacao()

        End If

        If peticion.Modulo.ModulosDesglose IsNot Nothing AndAlso peticion.Modulo.ModulosDesglose.Count > 0 Then

            Dim codDivisa As String = String.Empty
            Dim codDenominacion As String = String.Empty
            Dim oidDenominacion As String = String.Empty

            For Each tmd In peticion.Modulo.ModulosDesglose

                objtransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
                comando.Parameters.Clear()

                If codDivisa <> tmd.CodDivisa OrElse codDenominacion <> tmd.CodDenominacion Then

                    Dim oidDivisa = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Divisa.ObterOidDivisa(tmd.CodDivisa)
                    Dim denominacion = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Denominacion.ObterOidDenominacion(oidDivisa, tmd.CodDenominacion)

                    oidDenominacion = denominacion
                    codDivisa = tmd.CodDivisa
                    codDenominacion = tmd.CodDenominacion

                End If

                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEL_UNIDADES", ProsegurDbType.Inteiro_Longo, tmd.NelUnidades))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, tmd.GmtModificacion))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, tmd.DesUsuarioModificacion))

                Dim oidModuloDesglose = GetOidModuloDesglose(oidModulo, oidDenominacion)

                'Alta
                If String.IsNullOrEmpty(oidModuloDesglose) Then

                    comando.CommandText = Util.PrepararQuery(My.Resources.InserirTipoModuloDesglose.ToString)

                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, oidModulo))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO_DESGLOSE", ProsegurDbType.Identificador_Alfanumerico, Guid.NewGuid.ToString()))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, oidDenominacion))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, tmd.GmtModificacion))
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, tmd.DesUsuarioModificacion))

                Else
                    'Modificacion
                    comando.CommandText = Util.PrepararQuery(My.Resources.AtualizarTipoModuloDesglose.ToString)
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO_DESGLOSE", ProsegurDbType.Identificador_Alfanumerico, oidModuloDesglose))

                End If

                objtransacion.AdicionarItemTransacao(comando)
                objtransacion.RealizarTransacao()
            Next

        End If

        Return peticion.Modulo.CodModulo

    End Function

    ''' <summary>
    ''' Permite realizar la busca de tipoModulo  
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 01/04/2015 Criado
    ''' </history>
    Public Shared Function GetModulo(peticion As Integracion.ContractoServicio.Modulo.GetModulo.Peticion) As Integracion.ContractoServicio.Modulo.GetModulo.Respuesta

        Dim respuesta As New Integracion.ContractoServicio.Modulo.GetModulo.Respuesta
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetTiposModulosDesglose.ToString)
        comando.CommandType = CommandType.Text

        If peticion IsNot Nothing Then

            If Not String.IsNullOrEmpty(peticion.OidModulo) Then

                comando.CommandText += " AND  TM.OID_MODULO = []OID_MODULO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.OidModulo))

            End If

            If Not String.IsNullOrEmpty(peticion.DesModulo) Then

                comando.CommandText += " AND UPPER(TM.DES_MODULO) LIKE UPPER('%" & peticion.DesModulo & "%')"

            End If

            If Not String.IsNullOrEmpty(peticion.CodCliente) Then

                comando.CommandText += " AND CLI.COD_CLIENTE = []COD_CLIENTE "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodCliente))

            End If

            If peticion.BolActivo IsNot Nothing Then

                comando.CommandText += " AND  TM.BOL_ACTIVO = []BOL_ACTIVO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, peticion.BolActivo))

            End If

            If Not String.IsNullOrEmpty(peticion.CodModulo) Then

                comando.CommandText += " AND  TM.COD_MODULO = []COD_MODULO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.CodModulo))

            ElseIf peticion.CodModulos IsNot Nothing AndAlso peticion.CodModulos.Count > 0 Then
                comando.CommandText &= Util.MontarClausulaIn(peticion.CodModulos, "COD_MODULO", comando, "AND", "TM")
            End If

        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            respuesta.Modulos = PreencheTipoModulo(dt)
        End If

        Return respuesta

    End Function

    Public Shared Function GetModuloCliente(peticion As Integracion.ContractoServicio.Modulo.GetModuloCliente.Peticion) As Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta

        Dim filtros As New System.Text.StringBuilder
        Dim filtrosSinCliente As New System.Text.StringBuilder

        Dim respuesta As New Integracion.ContractoServicio.Modulo.GetModuloCliente.Respuesta
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = My.Resources.GetTiposModulosAssociadosOuNaoClientes.ToString
        comando.CommandType = CommandType.Text

        If peticion IsNot Nothing Then

            If Not String.IsNullOrEmpty(peticion.OidModulo) Then
                filtros.Append(" AND  TM.OID_MODULO = []OID_MODULO")
                filtrosSinCliente.Append(" AND  TM.OID_MODULO = []OID_MODULO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.OidModulo))
            End If

            If Not String.IsNullOrEmpty(peticion.DesModulo) Then

                filtros.Append(" AND UPPER(TM.DES_MODULO) LIKE UPPER([]DES_MODULO)")
                filtrosSinCliente.Append(" AND UPPER(TM.DES_MODULO) LIKE UPPER([]DES_MODULO)")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MODULO", ProsegurDbType.Descricao_Longa, "%" & peticion.DesModulo & "%"))


            End If

            If Not String.IsNullOrEmpty(peticion.CodCliente) Then

                filtros.Append(" AND CLI.COD_CLIENTE = []COD_CLIENTE")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodCliente))

            End If

            If peticion.BolActivo IsNot Nothing Then

                filtros.Append("  AND  TM.BOL_ACTIVO = []BOL_ACTIVO")
                filtrosSinCliente.Append("  AND  TM.BOL_ACTIVO = []BOL_ACTIVO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, peticion.BolActivo))

            End If

            If Not String.IsNullOrEmpty(peticion.CodModulo) Then

                filtros.Append("  AND  TM.COD_MODULO = []COD_MODULO")
                filtrosSinCliente.Append("  AND  TM.COD_MODULO = []COD_MODULO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.CodModulo))

            ElseIf peticion.CodModulos IsNot Nothing AndAlso peticion.CodModulos.Count > 0 Then
                Dim clausulaIn As String = Util.MontarClausulaIn(peticion.CodModulos, "COD_MODULO", comando, "AND", "TM")
                filtros.Append(clausulaIn)
                filtrosSinCliente.Append(clausulaIn)
            End If

        End If

        If filtros.Length > 0 AndAlso filtrosSinCliente.Length > 0 Then
            comando.CommandText = Util.PrepararQuery(String.Format(comando.CommandText, filtros.ToString, filtrosSinCliente.ToString))

        ElseIf filtros.Length > 0 AndAlso filtrosSinCliente.Length = 0 Then
            comando.CommandText = Util.PrepararQuery(String.Format(comando.CommandText, filtros.ToString, String.Empty))

        ElseIf filtros.Length = 0 AndAlso filtrosSinCliente.Length = 0 Then
            comando.CommandText = Util.PrepararQuery(String.Format(comando.CommandText, String.Empty, String.Empty))
        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            respuesta.Modulos = PreencheTipoModulo(dt)
        End If

        Return respuesta

    End Function


    Public Shared Function RecuperarModulos(peticion As ContractoServicio.Modulo.RecuperarModulo.Peticion) As ContractoServicio.Modulo.RecuperarModulo.Respuesta

        Dim respuesta As New ContractoServicio.Modulo.RecuperarModulo.Respuesta
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.RecuperarModulos.ToString)
        comando.CommandType = CommandType.Text

        If peticion IsNot Nothing Then

            If Not String.IsNullOrEmpty(peticion.OidModulo) Then

                comando.CommandText += " AND  TM.OID_MODULO = []OID_MODULO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.OidModulo))

            End If

            If Not String.IsNullOrEmpty(peticion.DesModulo) Then

                comando.CommandText += " AND UPPER(TM.DES_MODULO) LIKE UPPER('%" & peticion.DesModulo & "%')"

            End If

            If Not String.IsNullOrEmpty(peticion.CodCliente) Then

                comando.CommandText += " AND CLI.COD_CLIENTE = []COD_CLIENTE "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, peticion.CodCliente))

            End If

            If peticion.BolActivo IsNot Nothing Then

                comando.CommandText += " AND  TM.BOL_ACTIVO = []BOL_ACTIVO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, peticion.BolActivo))

            End If

            If Not String.IsNullOrEmpty(peticion.CodModulo) Then

                comando.CommandText += " AND  TM.COD_MODULO = []COD_MODULO "
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODULO", ProsegurDbType.Identificador_Alfanumerico, peticion.CodModulo))

            ElseIf peticion.CodigosModulos IsNot Nothing AndAlso peticion.CodigosModulos.Count > 0 Then
                comando.CommandText &= Util.MontarClausulaIn(peticion.CodigosModulos, "COD_MODULO", comando, "AND", "TM")
            End If

        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            respuesta.Modulos = PreencheModulo(dt)
        End If

        Return respuesta

    End Function

    Public Shared Function PreencheModulo(dt As DataTable) As List(Of ContractoServicio.Modulo.Modulo)

        Dim modulos As New List(Of ContractoServicio.Modulo.Modulo)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr In dt.Rows

                Dim modulo = modulos.Where(Function(t) t.OidModulo = Util.AtribuirValorObj(dr("OID_MODULO"), GetType(String))).FirstOrDefault

                If modulo Is Nothing Then

                    modulo = New ContractoServicio.Modulo.Modulo

                    modulo.OidModulo = Util.AtribuirValorObj(dr("OID_MODULO"), GetType(String))
                    modulo.CodModulo = Util.AtribuirValorObj(dr("COD_MODULO"), GetType(String))
                    modulo.DesModulo = Util.AtribuirValorObj(dr("DES_MODULO"), GetType(String))
                    modulo.CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                    modulo.BolActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean))
                    modulos.Add(modulo)

                End If

            Next

        End If

        Return modulos

    End Function


    Public Shared Function PreencheTipoModulo(dt As DataTable) As List(Of Integracion.ContractoServicio.Modulo.Modulo)

        Dim modulos As New List(Of Integracion.ContractoServicio.Modulo.Modulo)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr In dt.Rows

                Dim modulo = modulos.Where(Function(t) t.OidModulo = Util.AtribuirValorObj(dr("OID_MODULO"), GetType(String))).FirstOrDefault

                If modulo Is Nothing Then

                    modulo = New Integracion.ContractoServicio.Modulo.Modulo

                    modulo.OidModulo = Util.AtribuirValorObj(dr("OID_MODULO"), GetType(String))
                    modulo.CodEmbalaje = Util.AtribuirValorObj(dr("COD_EMBALAJE"), GetType(String))
                    modulo.CodModulo = Util.AtribuirValorObj(dr("COD_MODULO"), GetType(String))
                    modulo.DesModulo = Util.AtribuirValorObj(dr("DES_MODULO"), GetType(String))
                    modulo.CodCliente = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String))
                    modulo.BolActivo = Util.AtribuirValorObj(dr("BOL_ACTIVO"), GetType(Boolean))
                    modulo.GmtCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                    modulo.DesUsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                    modulo.GmtModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                    modulo.DesUsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
                    modulo.ModulosDesglose = New List(Of Integracion.ContractoServicio.Modulo.ModuloDesglose)

                    modulos.Add(modulo)

                End If

                Dim moduloDesglose = modulo.ModulosDesglose.Where(Function(d) d.OidModuloDesglose = Util.AtribuirValorObj(dr("OID_MODULO_DESGLOSE"), GetType(String))).FirstOrDefault

                If moduloDesglose Is Nothing Then

                    moduloDesglose = New Integracion.ContractoServicio.Modulo.ModuloDesglose

                    moduloDesglose.OidModuloDesglose = Util.AtribuirValorObj(dr("OID_MODULO_DESGLOSE"), GetType(String))
                    moduloDesglose.CodDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                    moduloDesglose.CodDenominacion = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                    moduloDesglose.DesValor = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                    moduloDesglose.BolBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean))
                    moduloDesglose.NumValor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Decimal))
                    moduloDesglose.NelUnidades = Util.AtribuirValorObj(dr("NEL_UNIDADES"), GetType(Integer))
                    moduloDesglose.GmtCreacion = Util.AtribuirValorObj(dr("GMT_CREACION_D"), GetType(DateTime))
                    moduloDesglose.DesUsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION_D"), GetType(String))
                    moduloDesglose.GmtModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION_D"), GetType(DateTime))
                    moduloDesglose.DesUsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION_D"), GetType(String))

                    modulo.ModulosDesglose.Add(moduloDesglose)

                End If

            Next

        End If

        Return modulos

    End Function

    Public Shared Function GetOidModulo(codModulo As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetOidTipoModulo.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODULO", ProsegurDbType.Identificador_Alfanumerico, codModulo))

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return Util.AtribuirValorObj(dt.Rows(0)("OID_MODULO"), GetType(String))
        End If

        Return String.Empty

    End Function

    Public Shared Function GetOidModuloDesglose(oidModulo As String, oidDenominacion As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetOidTipoModuloDesglose.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MODULO", ProsegurDbType.Identificador_Alfanumerico, oidModulo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, oidDenominacion))

        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return Util.AtribuirValorObj(dt.Rows(0)("OID_MODULO_DESGLOSE"), GetType(String))
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' RecuperarModulos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RecuperarModulos() As List(Of String)

        Dim Modulos As List(Of String) = Nothing
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ModulosRecuperarModulos)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO", ProsegurDbType.Identificador_Alfanumerico, ContractoServicio.Constantes.CONST_CODIGO_TIPO_FORMATO))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Modulos = New List(Of String)

            For Each dr In dt.Rows
                Modulos.Add(Util.AtribuirValorObj(dr("COD_VALOR"), GetType(String)))
            Next

        End If

        Return Modulos
    End Function

End Class
