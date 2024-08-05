Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class Direccion

#Region "[CONSULTAS]"

    ''' <summary>
    ''' Retorna todas as Direcciones no banco
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 - Criado
    ''' </history>
    Public Shared Function GetDirecciones(Peticion As ContractoServicio.Direccion.GetDirecciones.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Direccion.GetDirecciones.DireccionColeccion
        'Cria o comando para a busca
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        'Atribui um sql para o comando
        comando.CommandText = Util.PrepararQuery(My.Resources.SelectDireccion)
        'Defini o tipo de comando
        comando.CommandType = CommandType.Text

        'Se alguns dos parametros é vazio, insere o where na consulta
        If Not String.IsNullOrEmpty(Peticion.codTipoTablaGenesis) _
            OrElse Not String.IsNullOrEmpty(Peticion.oidTablaGenesis) Then
            comando.CommandText &= " WHERE "
        End If

        'Se o codtipoTablaGenesis possuir valor adiciona no sql o parametro enviado pela petição
        If Not String.IsNullOrEmpty(Peticion.codTipoTablaGenesis) AndAlso String.IsNullOrEmpty(Peticion.oidTablaGenesis) Then
            comando.CommandText &= "UPPER(cod_tipo_tabla_genesis) = :cod_tipo_tabla_genesis"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_tabla_genesis", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoTablaGenesis.ToUpper))
        End If
        'Se o OidTablaGenesis possuir valor adiciona no sql o parametro enviado pela petição
        If Not String.IsNullOrEmpty(Peticion.oidTablaGenesis) AndAlso String.IsNullOrEmpty(Peticion.codTipoTablaGenesis) Then
            comando.CommandText &= "oid_tabla_genesis = :oid_tabla_genesis"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_tabla_genesis", ProsegurDbType.Objeto_Id, Peticion.oidTablaGenesis))
        End If
        'Se os dois parametros possuirem valor ambos são adicionados na consulta
        If (Not String.IsNullOrEmpty(Peticion.codTipoTablaGenesis)) AndAlso (Not String.IsNullOrEmpty(Peticion.oidTablaGenesis)) Then
            comando.CommandText &= "UPPER(cod_tipo_tabla_genesis) = :cod_tipo_tabla_genesis AND oid_tabla_genesis = :oid_tabla_genesis"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_tabla_genesis", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoTablaGenesis.ToUpper))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_tabla_genesis", ProsegurDbType.Objeto_Id, Peticion.oidTablaGenesis))
        End If
        'Completa o registro adicionando o order by na consulta
        comando.CommandText &= " ORDER BY cod_tipo_tabla_genesis"

        'cria um objeto da coleção de direccion
        Dim objDireccion As New ContractoServicio.Direccion.GetDirecciones.DireccionColeccion

        'Executa o comando e pagina o resultado
        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, Peticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        'Verifica se o dataTable é vazio.
        If dt.Rows.Count > 0 _
            AndAlso dt IsNot Nothing Then
            'adiciona cada registro no registro na coleção de direccion
            For Each dr In dt.Rows
                'Adciona cada registro do resultado na coleção de Direccion
                objDireccion.Add(PopulaDireccion(dr))
            Next
        End If
        'Retorna a coleção preenchida
        Return objDireccion
    End Function

    'Recupera as direcciones da entidade
    Public Shared Function RecuperaDireccionesBase(oidTablaGenesis As String) As ContractoServicio.Direccion.DireccionColeccionBase
        Dim objDireccionColeccion As New ContractoServicio.Direccion.DireccionColeccionBase
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.SelectDireccion)
        comando.CommandType = CommandType.Text

        'Se o OidTablaGenesis possuir valor adiciona no sql o parametro enviado pela petição
        If Not String.IsNullOrEmpty(oidTablaGenesis) Then
            comando.CommandText &= " WHERE OID_TABLA_GENESIS = :OID_TABLA_GENESIS "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Objeto_Id, oidTablaGenesis))
        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr In dt.Rows
                objDireccionColeccion.Add(PopulaDireccionBase(dr))
            Next
            'Retorna coleção de DireccionesBase
            Return objDireccionColeccion
        End If

        Return Nothing

    End Function
#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' Insere uma Direccion no banco de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 - Criado
    ''' </history>
    Public Shared Sub SetDirecciones(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion, _
                                     ByRef OidDireccion As String, ByRef CodTipoTablaGenesis As String, ByRef objTransacion As Transacao)

        'Cria objeto de Respuesta do servicio
        Dim objRespuesta As New ContractoServicio.Direccion.SetDirecciones.Respuesta
        'Cria comando para execução
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        'Atribui um sql de insert para comando
        comando.CommandText = Util.PrepararQuery(My.Resources.InsertDireccion)
        'Defini o tipo de comando.
        comando.CommandType = CommandType.Text

        'Cria um Guid para o registro
        OidDireccion = Guid.NewGuid().ToString

        'Defini os parametros da instrução sql passados pela petição
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_direccion", ProsegurDbType.Objeto_Id, OidDireccion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_tabla_genesis", ProsegurDbType.Objeto_Id, Peticion.oidTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_tabla_genesis", ProsegurDbType.Descricao_Longa, Peticion.codTipoTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_pais", ProsegurDbType.Descricao_Longa, Peticion.desPais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_provincia", ProsegurDbType.Descricao_Longa, Peticion.desProvincia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_ciudad", ProsegurDbType.Descricao_Longa, Peticion.desCiudad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_numero_telefono", ProsegurDbType.Descricao_Longa, Peticion.desNumeroTelefono))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_email", ProsegurDbType.Descricao_Longa, Peticion.desEmail))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_fiscal", ProsegurDbType.Descricao_Longa, Peticion.codFiscal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_postal", ProsegurDbType.Descricao_Longa, Peticion.codPostal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_direccion_linea_1", ProsegurDbType.Descricao_Longa, Peticion.desDireccionLinea1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_direccion_linea_2", ProsegurDbType.Descricao_Longa, Peticion.desDireccionLinea2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_1", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_2", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_3", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional3))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_1", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_2", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_3", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional3))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "gmt_creacion", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_usuario_creacion", ProsegurDbType.Descricao_Longa, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "gmt_modificacion", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_usuario_modificacion", ProsegurDbType.Descricao_Longa, Peticion.desUsuarioModificacion))

        'Adiciona o comando na transação
        objTransacion.AdicionarItemTransacao(comando)

        'Com a instancia de resposta é retornado o codigo e o oid do registro 
        CodTipoTablaGenesis = Peticion.codTipoTablaGenesis

    End Sub

#End Region

#Region "[ATUALIZA]"

    ''' <summary>
    ''' Atualiza uma Direccion no banco de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 - Criado
    ''' </history>
    Public Shared Sub AtualizaDireccion(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion, _
                                        ByRef OidDireccion As String, ByRef CodTipoTablaGenesis As String, ByRef objTransacion As Transacao)

        'Cria o comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        'Atribui um sql ao comando
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaDireccion)
        'Defini o tipo de comando
        comando.CommandType = CommandType.Text
        'Adiciona na consulta os parametros enviados pela petição
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_direccion", ProsegurDbType.Objeto_Id, Peticion.oidDireccion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_tabla_genesis", ProsegurDbType.Objeto_Id, Peticion.oidTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_tipo_tabla_genesis", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoTablaGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_pais", ProsegurDbType.Descricao_Longa, Peticion.desPais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_provincia", ProsegurDbType.Descricao_Longa, Peticion.desProvincia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_ciudad", ProsegurDbType.Descricao_Longa, Peticion.desCiudad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_numero_telefono", ProsegurDbType.Descricao_Longa, Peticion.desNumeroTelefono))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_email", ProsegurDbType.Descricao_Longa, Peticion.desEmail))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_fiscal", ProsegurDbType.Identificador_Alfanumerico, Peticion.codFiscal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_postal", ProsegurDbType.Identificador_Alfanumerico, Peticion.codPostal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_direccion_linea_1", ProsegurDbType.Descricao_Longa, Peticion.desDireccionLinea1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_direccion_linea_2", ProsegurDbType.Descricao_Longa, Peticion.desDireccionLinea2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_1", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_2", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_campo_adicional_3", ProsegurDbType.Descricao_Longa, Peticion.desCampoAdicional3))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_1", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional1))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_2", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional2))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_categoria_adicional_3", ProsegurDbType.Descricao_Longa, Peticion.desCategoriaAdicional3))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "gmt_creacion", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_usuario_creacion", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "gmt_modificacion", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "des_usuario_modificacion", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioModificacion))

        'Adiciona o comando a transação
        objTransacion.AdicionarItemTransacao(comando)
        
        'Retorna o codigo e Oid do registro atualizado
        OidDireccion = Peticion.oidDireccion
        CodTipoTablaGenesis = Peticion.codTipoTablaGenesis

    End Sub

#End Region

#Region "[BAJA]"

    ''' <summary>
    ''' Realiza a baixa fisica no banco de dados do endereço
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 02/05/2013 - Criado
    ''' </history>
    Public Shared Sub BajaDireccion(Peticion As ContractoServicio.Direccion.SetDirecciones.Peticion, _
                                         ByRef bolBaja As Boolean, ByRef CodTipoTablaGenesis As String, ByRef objTransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery("DELETE FROM GEPR_TDIRECCION WHERE oid_tabla_genesis = []oid_tabla_genesis")
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_tabla_genesis", ProsegurDbType.Objeto_Id, Peticion.oidTablaGenesis))

        objTransacion.AdicionarItemTransacao(comando)

        bolBaja = Peticion.bolBaja
        CodTipoTablaGenesis = Peticion.codTipoTablaGenesis

    End Sub

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Popula o classe de Direccion recuperadas pelo metodo de GetDirecciones
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 25/04/2013 - Criado
    ''' </history>
    Private Shared Function PopulaDireccion(dr As DataRow) As ContractoServicio.Direccion.Direccion
        'Cria um objeto da classe de Direccion
        Dim objDireccion As New ContractoServicio.Direccion.Direccion
        'Atribui o resultado na linha do dataRown ao campo da classe
        Util.AtribuirValorObjeto(objDireccion.codFiscal, dr("COD_FISCAL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.codPostal, dr("COD_POSTAL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.codTipoTablaGenesis, dr("COD_TIPO_TABLA_GENESIS"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional1, dr("DES_CAMPO_ADICIONAL_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional2, dr("DES_CAMPO_ADICIONAL_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional3, dr("DES_CAMPO_ADICIONAL_3"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional1, dr("DES_CATEGORIA_ADICIONAL_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional2, dr("DES_CATEGORIA_ADICIONAL_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional3, dr("DES_CATEGORIA_ADICIONAL_3"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCiudad, dr("DES_CIUDAD"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desDireccionLinea1, dr("DES_DIRECCION_LINEA_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desDireccionLinea2, dr("DES_DIRECCION_LINEA_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desEmail, dr("DES_EMAIL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desNumeroTelefono, dr("DES_NUMERO_TELEFONO"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desPais, dr("DES_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desProvincia, dr("DES_PROVINCIA"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.gmtCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDireccion.gmtModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDireccion.oidDireccion, dr("OID_DIRECCION"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.oidTablaGenesis, dr("OID_TABLA_GENESIS"), GetType(String))

        'Retorna a descrição e o codigo da tabela.
        Dim codigoTipoTabla As String = dr("COD_TIPO_TABLA_GENESIS").ToString()
        Dim OidTablaGenesis As String = dr("OID_TABLA_GENESIS").ToString()
        'Se os parametros não foram enviados não retorna os dados da tabela.
        If Not String.IsNullOrEmpty(codigoTipoTabla) AndAlso _
            Not String.IsNullOrEmpty(OidTablaGenesis) Then
            'O Retorno do metodo de busca dados tabela, preenche a tabla genesis para atribuir o retorno de objDireccion
            Dim objTablaGenesis As ContractoServicio.Direccion.GetDirecciones.TablaGenesis = RetornaDadosTabela(codigoTipoTabla, OidTablaGenesis)

            Util.AtribuirValorObjeto(objDireccion.codTablaGenesis, objTablaGenesis.Codigo, GetType(String))
            Util.AtribuirValorObjeto(objDireccion.desTablaGenesis, objTablaGenesis.Descricao, GetType(String))
        End If

        'Retorna a classe preenchida
        Return objDireccion
    End Function


    ''' <summary>
    ''' Popula o classe de DireccionBase recuperadas pelo metodo de GetDirecciones
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 12/06/2013 - Criado
    ''' </history>
    Private Shared Function PopulaDireccionBase(dr As DataRow) As ContractoServicio.Direccion.DireccionBase
        'Cria um objeto da classe de Direccion
        Dim objDireccion As New ContractoServicio.Direccion.DireccionBase
        'Atribui o resultado na linha do dataRown ao campo da classe
        Util.AtribuirValorObjeto(objDireccion.codFiscal, dr("COD_FISCAL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.codPostal, dr("COD_POSTAL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional1, dr("DES_CAMPO_ADICIONAL_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional2, dr("DES_CAMPO_ADICIONAL_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCampoAdicional3, dr("DES_CAMPO_ADICIONAL_3"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional1, dr("DES_CATEGORIA_ADICIONAL_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional2, dr("DES_CATEGORIA_ADICIONAL_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCategoriaAdicional3, dr("DES_CATEGORIA_ADICIONAL_3"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desCiudad, dr("DES_CIUDAD"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desDireccionLinea1, dr("DES_DIRECCION_LINEA_1"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desDireccionLinea2, dr("DES_DIRECCION_LINEA_2"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desEmail, dr("DES_EMAIL"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desNumeroTelefono, dr("DES_NUMERO_TELEFONO"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desPais, dr("DES_PAIS"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.desProvincia, dr("DES_PROVINCIA"), GetType(String))
        Util.AtribuirValorObjeto(objDireccion.oidDireccion, dr("OID_DIRECCION"), GetType(String))

        'Retorna a classe preenchida
        Return objDireccion
    End Function
    ''' <summary>
    ''' Popula o classe de tabla genesis
    ''' </summary>
    ''' <param name="codigoTipoTabla"></param>
    ''' <param name="OidtablaGenesis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 02/05/2013 - Criado
    ''' </history>
    Private Shared Function RetornaDadosTabela(codigoTipoTabla As String, OidtablaGenesis As String)
        'Cria o objeto de tabla genesis
        Dim objTabaGenesis As New ContractoServicio.Direccion.GetDirecciones.TablaGenesis
        'Cria um comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        'Cria um string para concateneção de valores
        Dim query As New StringBuilder
        'Retorna via linq o tipo da entidade passada por parametro
        Dim tabela = (From p In Constantes.MapeoEntidadesCodigoAjeno _
               Where p.Entidade.Equals(codigoTipoTabla)
               Select p).FirstOrDefault
        'Cria o comando SQL
        query.AppendLine("SELECT * FROM")
        query.AppendLine(tabela.Entidade)
        query.AppendLine("WHERE ")
        query.AppendLine(tabela.OidTablaGenesis & " = []OID_TABLA_GENESIS")
        'Atribui o SQL no comando
        comando.CommandText = Util.PrepararQuery(query.ToString())
        comando.CommandType = CommandType.Text
        'Adiciona o parâmetro na query
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TABLA_GENESIS", ProsegurDbType.Objeto_Id, OidtablaGenesis))
        'Executa o comando e retorna o um datatable
        Dim data As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If data.Rows.Count > 0 AndAlso _
            data IsNot Nothing Then
            'Realiza o for each no dados retornado para preencher o objeto de Tabla Genesis
            For Each dr In data.Rows
                Util.AtribuirValorObjeto(objTabaGenesis.Codigo, dr("" & tabela.CodTablaGenesis & ""), GetType(String))
                Util.AtribuirValorObjeto(objTabaGenesis.Descricao, dr("" & tabela.DesTablaGenesis & ""), GetType(String))
            Next

        End If
        'Retorna o objeto de tabla de genesis
        Return objTabaGenesis
    End Function
#End Region

End Class
