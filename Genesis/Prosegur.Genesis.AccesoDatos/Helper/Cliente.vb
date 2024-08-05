Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

''' <summary>
''' Classe de Acesso a dados Cliente.
''' </summary>
''' <history>
''' [Thiago Dias] 22/08/2013 - Criado.
''' [Thiago Dias] 27/09/2013 - Modificado.
'''</history>
Public Class Cliente

    ''' <summary>
    ''' Pesquisa por informações de Clientes.
    ''' </summary>
    Public Shared Function BuscarClientes(peticion As PeticionHelper,
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaClientes.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_CLIENTE", "DES_CLIENTE", "COD_CLIENTE")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    ''' <summary>
    '''Busqueda de Clientes por Tipo Planificacion.
    ''' </summary>
    Public Shared Function BuscarClientesPorTipoPlanificacion(peticion As PeticionHelper,
                                             ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

        Dim query As New StringBuilder
        Dim dtResultado As DataTable

        ' Obtem Query.
        query.Append(My.Resources.BuscaClientesPorTipoPlanificacion.ToString())

        ' Realiza Pesquisa.
        dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_CLIENTE", "DES_CLIENTE", "COD_CLIENTE")

        ' Retorna lista contendo dados de Respuesta Cliente.
        Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

    End Function

    ''' <summary>
    ''' Pesquisa por informações de Clientes.
    ''' </summary>
    Public Shared Function BuscarCodigoMigracion(codigoCliente As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.RecuperarCodigoMigracion.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Curta, codigoCliente))

        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

        Dim Result As Object = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, comando)

        Return If(IsDBNull(Result), String.Empty, Result)

    End Function

    Public Shared Function RecuperarValorCodigoAjeno(identificadorCliente As String, codigoIdentificadorAjeno As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.RecuperarValorCodigoAjeno.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_TABLA_GENESIS", ProsegurDbType.Descricao_Curta, "GEPR_TCLIENTE"))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TABLA_GENESIS", ProsegurDbType.Descricao_Curta, identificadorCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, codigoIdentificadorAjeno))

        comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

        Dim Result As Object = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, comando)

        Return If(IsDBNull(Result), String.Empty, Result)

    End Function

End Class
