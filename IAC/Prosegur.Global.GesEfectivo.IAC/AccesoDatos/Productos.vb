Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DBHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class Producto

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Função GetProductos, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/01/2008 Created
    ''' </history>
    Public Shared Function GetProductos(peticion As ContractoServicio.Producto.GetProductos.Peticion) As ContractoServicio.Producto.GetProductos.ProductoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetProductos.ToString()

        Dim filtros As New System.Text.StringBuilder

        'Monta a query de acordo com os parametros passados.
        filtros.Append(MontaClausulaProductos(peticion, comando))

        If (filtros.Length > 0) Then
            comando.CommandText &= " WHERE " & filtros.ToString
        End If

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaProductos As New ContractoServicio.Producto.GetProductos.ProductoColeccion

        'Percorre o dt e retorna uma coleção productos.
        objRetornaProductos = RetornaColecaoProductos(dt)

        Return objRetornaProductos
    End Function

    ''' <summary>
    ''' Popula o datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/01/2008 Created
    ''' </history>
    Private Shared Function PopularProductos(dr As DataRow) As ContractoServicio.Producto.GetProductos.Producto

        Dim objproducto As New ContractoServicio.Producto.GetProductos.Producto

        Util.AtribuirValorObjeto(objproducto.CodigoProducto, dr("COD_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objproducto.DescripcionProducto, dr("DES_PRODUCTO"), GetType(String))
        Util.AtribuirValorObjeto(objproducto.ClaseBillete, dr("DES_CLASE_BILLETE"), GetType(String))
        Util.AtribuirValorObjeto(objproducto.FactorCorreccion, dr("NUM_FACTOR_CORRECCION"), GetType(Double))
        Util.AtribuirValorObjeto(objproducto.EsManual, dr("BOL_MANUAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproducto.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objproducto.DescripcionMaquinas, dr("DES_MAQUINA"), GetType(String))

        Return objproducto

    End Function

    ''' <summary>
    ''' Retorna todos os canais vigentes 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboProductos() As ContractoServicio.Utilidad.GetComboProductos.ProductoColeccion

        ' criar objeto cliente
        Dim objColProductos As New ContractoServicio.Utilidad.GetComboProductos.ProductoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboProductos.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Retorna coleção de canais
        objColProductos = RetornaColProductos(dtCliente)

        ' retornar coleção de termino
        Return objColProductos
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna todos os productos vigentes
    ''' </summary>
    ''' <param name="dtProductos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </sumary>
    Private Shared Function RetornaColProductos(dtProductos As DataTable) As ContractoServicio.Utilidad.GetComboProductos.ProductoColeccion

        Dim objColProductos As New ContractoServicio.Utilidad.GetComboProductos.ProductoColeccion

        If dtProductos IsNot Nothing _
            AndAlso dtProductos.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtProductos.Rows
                ' adicionar para coleção
                objColProductos.Add(PopulaCanalGetComboProductos(dr))
            Next

        End If

        Return objColProductos
    End Function

    ''' <summary>
    ''' Popula Canal GetComboProductos
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaCanalGetComboProductos(dr As DataRow) As ContractoServicio.Utilidad.GetComboProductos.Producto

        Dim objProducto As New ContractoServicio.Utilidad.GetComboProductos.Producto

        Util.AtribuirValorObjeto(objProducto.Codigo, dr("COD_PRODUCTO"), GetType(String))

        Util.AtribuirValorObjeto(objProducto.Descripcion, dr("DES_PRODUCTO"), GetType(String))

        Util.AtribuirValorObjeto(objProducto.DescripcionClaseBillete, dr("DES_CLASE_BILLETE"), GetType(String))

        'Atributo utilizado para ordenar pelo códgio do Produto se todos os produtos posssuem código numérico
        Dim intResult As Integer
        If Integer.TryParse(objProducto.Codigo, intResult) Then
            objProducto.CodigoInt = intResult
        End If

        Return objProducto

    End Function

    ''' <summary>
    ''' Monta Query Canal
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaProductos(objpeticion As ContractoServicio.Producto.GetProductos.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        'Monta a clausula e adiciona os parametros.
        filtros.Append("PRO.BOL_MANUAL = []BOL_MANUAL AND PRO.BOL_VIGENTE = []BOL_VIGENTE ")

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MANUAL", ProsegurDbType.Logico, objpeticion.EsManual))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objpeticion.Vigente))

        filtros.Append(Util.MontarClausulaLikeUpper(objpeticion.CodigoProducto, "COD_PRODUCTO", comando, "AND", "PRO"))

        filtros.Append(Util.MontarClausulaLikeUpper(objpeticion.DescripcionProducto, "DES_PRODUCTO", comando, "AND", "PRO"))

        filtros.Append(Util.MontarClausulaIn(objpeticion.ClaseBillete, "DES_CLASE_BILLETE", comando, "AND", "PRO"))

        filtros.Append(Util.MontarClausulaIn(objpeticion.FactorCorreccion, "NUM_FACTOR_CORRECCION", comando, "AND", "PRO"))


        filtros.Append(Util.MontarClausulaIn(objpeticion.DescripcionMaquinas, "DES_MAQUINA", comando, "AND", "MAQ"))

        Return filtros
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de productos
    ''' </summary>
    ''' <param name="dtProductos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoProductos(dtProductos As DataTable) As ContractoServicio.Producto.GetProductos.ProductoColeccion

        Dim objRetornaProducto As New ContractoServicio.Producto.GetProductos.ProductoColeccion

        If dtProductos IsNot Nothing AndAlso dtProductos.Rows.Count > 0 Then
            For Each dr As DataRow In dtProductos.Rows
                ' adicionar para objeto
                objRetornaProducto.Add(PopularProductos(dr))
            Next
        End If

        Return objRetornaProducto
    End Function

    ''' <summary>
    ''' obtem o oid do producto.
    ''' </summary>
    ''' <param name="CodProducto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 Criado
    ''' </history>
    Public Shared Function BuscarOidProducto(CodProducto As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidProducto.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PRODUCTO", ProsegurDbType.Identificador_Alfanumerico, CodProducto))

        Dim oidProducto As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            oidProducto = dtQuery.Rows(0)("OID_PRODUCTO")
        End If

        Return oidProducto

    End Function

    ''' <summary>
    ''' Popular Produtos e Maquinas GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularProductoProcesos(Oid_Proceso As String) As GetProcesos.Producto

        'Cria objetos Producto e Maquina
        Dim objProducto As GetProcesos.Producto = Nothing
        Dim objMaquina As GetProcesos.Maquina = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProductoPorProceso.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, Oid_Proceso))

        'Preenche DataTable com o resultado da consulta
        Dim Productos As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Productos IsNot Nothing AndAlso Productos.Rows.Count > 0 Then

            'Instancia objeto Producto
            objProducto = New GetProcesos.Producto

            'Variável para comparação
            Dim Maquina As String = Nothing

            'Preenche Producto
            With objProducto

                Util.AtribuirValorObjeto(.Codigo, Productos.Rows(0)("COD_PRODUCTO"), GetType(String))
                Util.AtribuirValorObjeto(.Descripcion, Productos.Rows(0)("DES_PRODUCTO"), GetType(String))
                Util.AtribuirValorObjeto(.ClaseBillete, Productos.Rows(0)("DES_CLASE_BILLETE"), GetType(String))
                Util.AtribuirValorObjeto(.FactorCorreccion, Productos.Rows(0)("NUM_FACTOR_CORRECCION"), GetType(Decimal))
                Util.AtribuirValorObjeto(.ProcesadoManual, Productos.Rows(0)("BOL_MANUAL"), GetType(Boolean))

                'Cria nova coleção de máquinas
                .Maquinas = New GetProcesos.MaquinaColeccion

            End With

            'Preenche Maquinas se houver
            For Each row As DataRow In Productos.Rows

                Util.AtribuirValorObjeto(Maquina, row("DES_MAQUINA"), GetType(String))

                If Maquina IsNot Nothing Then

                    'Instancia objeto Maquina
                    objMaquina = New GetProcesos.Maquina

                    'Preenche Maquina
                    objMaquina.Descripcion = Maquina

                    'Adiciona Maquina à coleção
                    objProducto.Maquinas.Add(objMaquina)

                End If

            Next

        End If

        'Retorna producto e máquinas
        Return objProducto

    End Function


#End Region

End Class