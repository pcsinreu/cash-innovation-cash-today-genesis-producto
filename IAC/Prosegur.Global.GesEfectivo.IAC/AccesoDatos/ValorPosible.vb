Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis

Public Class ValorPosible

#Region "[ATRIBUTOS]"

    Private Const CONST_NIVEL_PUNTO_SERVICIO As String = "3"
    Private Const CONST_NIVEL_SUB_CLIENTE As String = "2"
    Private Const CONST_NIVEL_CLIENTE As String = "1"
    Private Const CONST_NIVEL_NENHUM As String = "0"

#End Region


#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém o término e os valores posibles por nivel
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 05/12/2011 Criado
    ''' </history>
    Public Shared Function RecuperaValoresPosiblesPorNivel(ObjPeticion As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.Peticion) As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.TerminoRespostaColeccion

        ' criar objeto respuesta
        Dim objTerminos As New RecuperaValoresPosiblesPorNivel.TerminoRespostaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder

        ' obter query
        query.Append(My.Resources.RecuperaValoresPosiblesPorNivel.ToString)


        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(ObjPeticion.Cliente), Nothing, ObjPeticion.Cliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(ObjPeticion.Subcliente), Nothing, ObjPeticion.Subcliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(ObjPeticion.PuntoServicio), Nothing, ObjPeticion.PuntoServicio)))

        If ObjPeticion.Terminos IsNot Nothing AndAlso ObjPeticion.Terminos.Count > 0 Then
            query.Append(Util.MontarClausulaIn(ObjPeticion.Terminos.Select(Function(f) f.CodigoTermino).ToList, "COD_TERMINO", comando, "AND"))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dt.Rows

                ' obter código termino
                Dim CodTermino As String = dr("COD_TERMINO")
                Dim CodValorPosible As String = dr("COD_VALOR")

                ' verificar se termino existe na coleção
                Dim PesTermino = From Terminos In objTerminos _
                                 Where Terminos.CodigoTermino = CodTermino

                ' se não existe termino
                If PesTermino.Count = 0 Then

                    ' criar termino
                    Dim objTermino As New Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.TerminoResposta
                    objTermino.CodigoTermino = CodTermino
                    objTermino.ValoresPosibles = New Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.ValorPosibleColeccion
                    objTerminos.Add(objTermino)

                End If

                ' verificar se valor posible existe na coleção
                Dim PesValorPosible As Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.TerminoResposta = _
                                    objTerminos.Find(New Predicate(Of Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.TerminoResposta)(Function(s) s.CodigoTermino = CodTermino))

                ' caso exista o termino
                If PesValorPosible IsNot Nothing Then

                    ' verificar se valor posible existe
                    Dim PesValorPosibleLQ = From ValoresPosibles In PesValorPosible.ValoresPosibles _
                                            Where ValoresPosibles.CodigoValorPosible = CodValorPosible

                    ' se não existe valor posible
                    If PesValorPosibleLQ.Count = 0 AndAlso Convert.ToBoolean(dr("BOL_VIGENTE")) Then

                        Dim objValorPosible As New Integracion.ContractoServicio.RecuperaValoresPosiblesPorNivel.ValorPossible
                        objValorPosible.CodigoValorPosible = CodValorPosible

                        If dr("DES_VALOR") IsNot DBNull.Value _
                            AndAlso Not String.IsNullOrEmpty(dr("DES_VALOR")) Then
                            objValorPosible.DescripcionValorPosible = dr("DES_VALOR")
                        End If

                        If dr("BOL_VALOR_DEFECTO") IsNot DBNull.Value _
                            AndAlso Not String.IsNullOrEmpty(dr("BOL_VALOR_DEFECTO")) Then
                            objValorPosible.EsValorDefecto = Convert.ToBoolean(dr("BOL_VALOR_DEFECTO"))
                        End If

                        PesValorPosible.ValoresPosibles.Add(objValorPosible)

                    End If

                End If

            Next

        End If

        ' retornar terminos
        Return objTerminos

    End Function


    ''' <summary>
    ''' Obtém o término e os valores posibles
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Shared Function GetValoresPosibles(objPeticion As ContractoServicio.ValorPosible.GetValoresPosibles.Peticion) As ContractoServicio.ValorPosible.TerminoColeccion

        ' criar objeto respuesta
        Dim objTerminos As New ContractoServicio.ValorPosible.TerminoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder

        ' obter query
        query.Append(My.Resources.GetValoresPosibles.ToString)

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(objPeticion.CodigoCliente), Nothing, objPeticion.CodigoCliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(objPeticion.CodigoSubCliente), Nothing, objPeticion.CodigoSubCliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(objPeticion.CodigoPuntoServicio), Nothing, objPeticion.CodigoPuntoServicio)))

        If Not String.IsNullOrEmpty(objPeticion.CodigoTermino) Then
            query.Append(" AND T.COD_TERMINO = []COD_TERMINO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TERMINO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTermino))
        End If

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dt.Rows

                ' obter código termino
                Dim CodTermino As String = dr("COD_TERMINO")
                Dim CodValorPosible As String = dr("COD_VALOR")

                ' verificar se termino existe na coleção
                Dim PesTermino = From Terminos In objTerminos _
                                 Where Terminos.Codigo = CodTermino

                ' se não existe termino
                If PesTermino.Count = 0 Then

                    ' criar termino
                    Dim objTermino As New ContractoServicio.ValorPosible.Termino
                    objTermino.Codigo = CodTermino
                    objTermino.ValoresPosibles = New ContractoServicio.ValorPosible.ValorPosibleColeccion
                    objTerminos.Add(objTermino)

                End If

                ' verificar se valor posible existe na coleção
                Dim PesValorPosible As ContractoServicio.ValorPosible.Termino = _
                                    objTerminos.Find(New Predicate(Of ContractoServicio.ValorPosible.Termino)(Function(s) s.Codigo = CodTermino))

                ' caso exista o termino
                If PesValorPosible IsNot Nothing Then

                    ' verificar se valor posible existe
                    Dim PesValorPosibleLQ = From ValoresPosibles In PesValorPosible.ValoresPosibles _
                                            Where ValoresPosibles.Codigo = CodValorPosible

                    ' se não existe valor posible
                    If PesValorPosibleLQ.Count = 0 Then

                        Dim objValorPosible As New ContractoServicio.ValorPosible.ValorPosible
                        objValorPosible.Codigo = CodValorPosible

                        If dr("DES_VALOR") IsNot DBNull.Value _
                            AndAlso Not String.IsNullOrEmpty(dr("DES_VALOR")) Then
                            objValorPosible.Descripcion = dr("DES_VALOR")
                        End If

                        If dr("BOL_VIGENTE") IsNot DBNull.Value _
                            AndAlso Not String.IsNullOrEmpty(dr("BOL_VIGENTE")) Then
                            objValorPosible.Vigente = Convert.ToBoolean(dr("BOL_VIGENTE"))
                        End If

                        If dr("BOL_VALOR_DEFECTO") IsNot DBNull.Value _
                            AndAlso Not String.IsNullOrEmpty(dr("BOL_VALOR_DEFECTO")) Then
                            objValorPosible.esValorDefecto = Convert.ToBoolean(dr("BOL_VALOR_DEFECTO"))
                        End If

                        PesValorPosible.ValoresPosibles.Add(objValorPosible)

                    End If

                End If

            Next

        End If

        ' retornar terminos
        Return objTerminos

    End Function

    ''' <summary>
    ''' Busca Todos os terminos iac
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 09/02/2009 Created
    ''' </history>
    Public Shared Function RetornaValoresTerminoIac(codCliente As String, _
                                                    codSubCliente As String, _
                                                    codPuntoServicio As String, _
                                                    oid_Termino As String) As Integracion.ContractoServicio.GetProceso.ValorPosibleColeccion
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'RETORNA VALOR TERMINO.
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaValorTermino)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Objeto_Id, codCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Objeto_Id, codSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Objeto_Id, codPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oid_Termino))

        Dim dtValorTermino As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim dvValorTerminoView As DataView = dtValorTermino.DefaultView

        'Primeramente se buscará para un término de IAC valores  posibles asociados al punto servicio
        dvValorTerminoView.RowFilter = " NIVEL = '" & CONST_NIVEL_PUNTO_SERVICIO & "'"

        If dvValorTerminoView.ToTable().Rows.Count = 0 Then

            'caso no exista valores asociados a este nível, se buscará a nivel de Subcliente 
            dvValorTerminoView = dtValorTermino.DefaultView
            dvValorTerminoView.RowFilter = " NIVEL = '" & CONST_NIVEL_SUB_CLIENTE & "'"

            If dvValorTerminoView.ToTable().Rows.Count = 0 Then

                'posteriormente a nivel de Cliente
                dvValorTerminoView = dtValorTermino.DefaultView
                dvValorTerminoView.RowFilter = " NIVEL = '" & CONST_NIVEL_CLIENTE & "'"

                If dvValorTerminoView.ToTable().Rows.Count = 0 Then

                    'Caso no exista valores asociados a ningún de estos niveles deberá ser verificado se para el termino
                    ' de IAC existen valores posibles pero no asociados a ningún nivel
                    dvValorTerminoView = dtValorTermino.DefaultView
                    dvValorTerminoView.RowFilter = " NIVEL = '" & CONST_NIVEL_NENHUM & "'"

                End If

            End If

        End If

        dtValorTermino = dvValorTerminoView.ToTable()

        If dtValorTermino IsNot Nothing AndAlso dtValorTermino.Rows.Count > 0 Then

            Return RetornaColeccionValoresPosiblesTerminoIAC(dtValorTermino)

        Else

            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' Obtém o oid valor termino
    ''' </summary>
    ''' <param name="OidCliente"></param>
    ''' <param name="OidTermino"></param>
    ''' <param name="CodigoValorPosible"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Public Shared Function ObterOidValorTermino(OidCliente As String, _
                                                OidSubCliente As String, _
                                                OidPuntoServicio As String, _
                                                OidTermino As String, _
                                                CodigoValorPosible As String)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidValorTermino.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_cliente", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(OidCliente), Nothing, OidCliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_subcliente", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(OidSubCliente), Nothing, OidSubCliente)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, IIf(String.IsNullOrEmpty(OidPuntoServicio), Nothing, OidPuntoServicio)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_valor", ProsegurDbType.Identificador_Alfanumerico, CodigoValorPosible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_termino", ProsegurDbType.Identificador_Alfanumerico, OidTermino))

        ' executar query e retornar resultado
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("oid_valor")
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' Preenche Valor Posiveis - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Termino"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 02/04/2009 Criado
    ''' </history>
    Public Shared Function PopularValorPosible(Oid_Termino As String) As GetProcesos.ValorPosibleColeccion

        'Cria objetos 
        Dim objValPos As GetProcesos.ValorPosible = Nothing
        Dim objValPosColeccion As GetProcesos.ValorPosibleColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterValorTerminoPorOidTermino.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, Oid_Termino))

        'Preenche DataTable com o resultado da consulta
        Dim ValPos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If ValPos IsNot Nothing AndAlso ValPos.Rows.Count > 0 Then

            'Instancia coleção de ValorPosibleColeccion
            objValPosColeccion = New GetProcesos.ValorPosibleColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In ValPos.Rows

                'Instancia objeto ValorPosible
                objValPos = New GetProcesos.ValorPosible

                'Preenche propriedades ValorPosible
                With objValPos

                    Util.AtribuirValorObjeto(.Codigo, row("COD_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Descripcion, row("DES_VALOR"), GetType(String))
                    Util.AtribuirValorObjeto(.Vigente, row("BOL_VIGENTE"), GetType(Boolean))

                End With

                'Adciona objeto ValorPosible à coleção de ValorPosible
                objValPosColeccion.Add(objValPos)

            Next

        End If

        Return objValPosColeccion

    End Function

#Region "[GETPROCESO]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="oidTermino"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornarValoresPosiblesTerminoMedioPago(oidTermino As String) As GetProceso.ValorPosibleColeccion
        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'BUSCA VALOR TERMINO
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaValorTerminoMedioPago.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oidTermino))

        Dim dtValorTerminoMedioPago As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtValorTerminoMedioPago IsNot Nothing AndAlso dtValorTerminoMedioPago.Rows.Count > 0 Then

            Return PopularValoresPosiblesTerminosMedioPago(dtValorTerminoMedioPago)

        Else

            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularValoresPosiblesTerminosMedioPago(dt As DataTable) As GetProceso.ValorPosibleColeccion

        Dim objValoresPosibles As New GetProceso.ValorPosibleColeccion

        For Each dr As DataRow In dt.Rows

            objValoresPosibles.Add(PopularValorPosible(dr))

        Next

        Return objValoresPosibles

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dtTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function RetornaColeccionValoresPosiblesTerminoIAC(dtTerminos As DataTable) As Integracion.ContractoServicio.GetProceso.ValorPosibleColeccion
        Dim objTerminosIac As New Integracion.ContractoServicio.GetProceso.ValorPosibleColeccion

        For Each dr As DataRow In dtTerminos.Rows

            ' adicionar para objeto
            objTerminosIac.Add(PopularValorPosible(dr))

        Next

        Return objTerminosIac

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularValorPosible(dr As DataRow) As Integracion.ContractoServicio.GetProceso.ValorPosible

        Dim objValorPosible As New Integracion.ContractoServicio.GetProceso.ValorPosible

        If dr("COD_VALOR") IsNot DBNull.Value Then
            objValorPosible.Codigo = dr("COD_VALOR")
        End If

        If dr("DES_VALOR") IsNot DBNull.Value Then
            objValorPosible.Descripcion = dr("DES_VALOR")
        End If

        If DirectCast(dr.Table, System.Data.DataTable).Columns("BOL_VALOR_DEFECTO") IsNot Nothing AndAlso dr("BOL_VALOR_DEFECTO") IsNot DBNull.Value Then
            objValorPosible.EsValorDefecto = dr("BOL_VALOR_DEFECTO")
        End If

        Return objValorPosible

    End Function

#End Region

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere valor posible
    ''' </summary>
    ''' <param name="objValorPosible"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="OidCliente"></param>
    ''' <param name="OidTermino"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    Public Shared Sub AltaValorPosible(objValorPosible As ContractoServicio.ValorPosible.ValorPosible, _
                                       CodigoUsuario As String, _
                                       OidCliente As String, _
                                       OidSubCliente As String, _
                                       OidPuntoServicio As String, _
                                       OidTermino As String)

        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter query
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaValoresPosibles.ToString)
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_VALOR", ProsegurDbType.Objeto_Id, Guid.NewGuid().ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CLIENTE", ProsegurDbType.Objeto_Id, IIf(String.IsNullOrEmpty(OidCliente), Nothing, OidCliente)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCLIENTE", ProsegurDbType.Objeto_Id, IIf(String.IsNullOrEmpty(OidSubCliente), Nothing, OidSubCliente)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PTO_SERVICIO", ProsegurDbType.Objeto_Id, IIf(String.IsNullOrEmpty(OidPuntoServicio), Nothing, OidPuntoServicio)))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, OidTermino))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, objValorPosible.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR", ProsegurDbType.Descricao_Longa, objValorPosible.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objValorPosible.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VALOR_DEFECTO", ProsegurDbType.Logico, objValorPosible.esValorDefecto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            ' executar comando
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("009_msg_Erro_UKValorPosible"))
        End Try

    End Sub

#End Region

#Region "[ALTERAR]"

    ''' <summary>
    ''' Modifica valores posibles.
    ''' </summary>
    ''' <param name="objValorPosible"></param>
    ''' <param name="OidValorPosible"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="OidCliente"></param>
    ''' <param name="OidTermino"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Public Shared Sub ModificarValorPosible(objValorPosible As ContractoServicio.ValorPosible.ValorPosible, _
                                            OidValorPosible As String, _
                                            CodigoUsuario As String, _
                                            OidCliente As String, _
                                            OidSubCliente As String, _
                                            OidPuntoServicio As String, _
                                            OidTermino As String)

        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter a query
            Dim query As New StringBuilder
            query.Append("UPDATE gepr_tvalor_termino_iac SET ")

            query.Append(Util.AdicionarCampoQuery("oid_cliente = []oid_cliente,", "oid_cliente", comando, IIf(String.IsNullOrEmpty(OidCliente), Nothing, OidCliente), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("oid_subcliente = []oid_subcliente,", "oid_subcliente", comando, IIf(String.IsNullOrEmpty(OidSubCliente), Nothing, OidSubCliente), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("oid_pto_servicio = []oid_pto_servicio,", "oid_pto_servicio", comando, IIf(String.IsNullOrEmpty(OidPuntoServicio), Nothing, OidPuntoServicio), ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("oid_termino = []oid_termino,", "oid_termino", comando, OidTermino, ProsegurDbType.Objeto_Id))
            query.Append(Util.AdicionarCampoQuery("cod_valor = []cod_valor,", "cod_valor", comando, objValorPosible.Codigo, ProsegurDbType.Identificador_Alfanumerico))
            query.Append(Util.AdicionarCampoQuery("des_valor = []des_valor,", "des_valor", comando, objValorPosible.Descripcion, ProsegurDbType.Descricao_Longa))
            query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objValorPosible.Vigente, ProsegurDbType.Logico))
            query.Append(Util.AdicionarCampoQuery("BOL_VALOR_DEFECTO = []BOL_VALOR_DEFECTO,", "BOL_VALOR_DEFECTO", comando, objValorPosible.esValorDefecto, ProsegurDbType.Logico))

            query.Append("cod_usuario = []cod_usuario,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

            query.Append("fyh_actualizacion = []fyh_actualizacion ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

            ' adicionar clausula where
            query.Append("WHERE oid_valor = []oid_valor ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_valor", ProsegurDbType.Objeto_Id, OidValorPosible))

            ' obter query
            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            ' executar comando
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("009_msg_Erro_UKValorPosible"))
        End Try

    End Sub

#End Region

End Class