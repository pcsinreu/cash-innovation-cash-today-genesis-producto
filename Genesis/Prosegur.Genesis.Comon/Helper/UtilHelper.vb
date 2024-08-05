Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper

''' <summary>
''' Classe que mantém as funcionalidades auxiliares utilizadas pelo Helper.
''' </summary>
''' <history>
''' [Thiago Dias] 10/09/2013 - Criado.
''' [Thiago Dias] 22/11/2013 - Modificado.
'''</history>
<Serializable()>
Public Class UtilHelper

    ''' <summary>
    ''' Estrutura para definição da query básica para as consultas do Controle Helper.
    ''' </summary>
    <Serializable()>
    Structure QueryHelperControl

        ''' <summary>
        ''' Define tipo de parâmetro da Estrutura.
        ''' </summary>
        ReadOnly Property ParametroHelper As TipoParametro
            Get
                Return TipoParametro.Query
            End Get
        End Property

        ''' <summary>
        ''' Nome da Coluna na tabela que contém os dados referente ao Código.        
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' O nome da coluna não deve conter Alias.
        ''' <code>ColunaCodigo = <c>COD_CLIENTE</c></code>
        ''' </example>
        '''</remarks>
        Property ColunaCodigo As String

        ''' <summary>
        ''' Nome da Coluna na tabela que contém os dados referente à Descrição.        
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' O nome da coluna não deve conter Alias.
        ''' <code>ColunaDescricao = <c>DES_CLIENTE</c></code>
        ''' </example>
        ''' </remarks>
        Property ColunaDescricao As String

        ''' <summary>
        ''' Tabela a ser referenciada na consulta.
        ''' <example>
        ''' <code>
        ''' Tabela = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}
        ''' </code>        
        ''' </example>
        ''' </summary>
        Property Tabela As Tabela

    End Structure

    ''' <summary>
    ''' Estrutura para definição da Tabela a ser referenciada na consulta.
    ''' </summary>
    <Serializable()>
    Structure Tabela

        Private _Tabela As TabelaHelper

        Public Property Tabela As TabelaHelper
            Get
                Return _Tabela
            End Get
            Set(value As TabelaHelper)
                _Tabela = value
            End Set
        End Property

    End Structure

    ''' <summary>
    ''' Estrutura para definição das restrições a serem aplicadas na query.
    ''' </summary>
    <Serializable()>
    Structure ArgumentosFiltro

        Public NomeColuna As String
        Public ValorFiltro As String
        Public TipoCondicaoFiltro As TipoCondicion

        ''' <summary>
        ''' Construtor Padrao da Estrutura de Argumentos Filtro.
        ''' </summary>
        ''' <param name="Coluna">Nome da Coluna a ser utilizada no Filtro.</param>
        ''' <param name="Filtro">Valor do Filtro a ser aplicado na consulta.</param>
        ''' <param name="Condicao">Recebe qual condição será executada.</param>
        Sub New(Coluna As String, Filtro As String, Optional Condicao As TipoCondicion = TipoCondicion.Igual)
            NomeColuna = Coluna
            ValorFiltro = Filtro
            TipoCondicaoFiltro = Condicao
        End Sub

        ''' <summary>
        ''' Nome da Coluna a ser utilizada no Filtro.
        ''' </summary>
        ReadOnly Property ColunaFiltro As String
            Get
                Return NomeColuna
            End Get
        End Property

        ''' <summary>
        ''' Valor do Filtro a ser incluído na consulta.
        ''' </summary>
        ReadOnly Property ValFiltro As String
            Get
                Return ValorFiltro
            End Get
        End Property

        ''' <summary>
        ''' Condição do Filtro a ser incluído na consulta.
        ''' </summary>
        ReadOnly Property CondicaoFiltro As TipoCondicion
            Get
                Return TipoCondicaoFiltro
            End Get
        End Property

    End Structure

    ''' <summary>
    ''' Estrutura para definição do tipo de ordenação a ser incluída na consulta.
    ''' </summary>
    <Serializable()>
    Structure OrderSQL

        Private nomeColuna As String

        ''' <summary>
        ''' Construtor de OrdenaçãoSQL.
        ''' </summary>
        ''' <param name="objNomeColuna">
        ''' Nome da coluna a ser utilizada na ordenação da consulta.
        ''' Obs: O nome da coluna não deve conter Alias.
        ''' </param>
        Sub New(objNomeColuna As String)
            nomeColuna = objNomeColuna
        End Sub

        ''' <summary>
        ''' Define tipo de parâmetro da Estrutura.
        ''' </summary>
        ReadOnly Property ParametroHelper As TipoParametro
            Get
                Return TipoParametro.Orden
            End Get
        End Property

        ''' <summary>
        ''' Nome da coluna a ser utilizada na ordenação da consulta.
        ''' </summary>
        ReadOnly Property ColunaOrdenacao As String
            Get
                Return nomeColuna
            End Get
        End Property

    End Structure

    ''' <summary>
    ''' Estrura para definição da junção nas consultas.
    ''' </summary>
    <Serializable()>
    Structure JoinSQL

        ''' <summary>
        ''' Define tipo de parâmetro da Estrutura.
        ''' </summary>
        ReadOnly Property ParametroHelper As TipoParametro
            Get
                Return TipoParametro.Juncao
            End Get
        End Property

        ''' <summary>
        ''' Tabela a ser utilizada para montagem da consulta Join, posicionada à esquerda do operador.
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' <code>
        ''' TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}
        ''' </code>
        ''' </example>
        ''' </remarks>
        Property TabelaEsquerda As Tabela

        ''' <summary>
        ''' Nome da coluna da tabela à esquerda a ser comparada na junção.
        ''' </summary>
        ''' <remarks>
        ''' Obs: O nome da coluna não deve conter Alias.
        ''' <example>
        ''' <code>CampoComumTabEsq = <c>OID_CLIENTE</c></code>        
        ''' </example>
        ''' </remarks>
        Property CampoComumTabEsq As String

        ''' <summary>
        ''' Tabela a ser utilizada para montagem da consulta Join, posicionada à direita do operador.        
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' <code>
        ''' TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.SubCliente}
        ''' </code>        
        ''' </example>
        '''</remarks>
        Property TabelaDireita As Tabela

        ''' <summary>
        ''' Nome da coluna da tabela à direita a ser comparada na junção.        
        ''' </summary>        
        ''' <remarks>
        ''' <example>
        ''' Obs: O nome da coluna não deve conter Alias.
        ''' <code>CampoComumTabDireita = <c>OID_CLIENTE</c></code>
        ''' </example>
        '''</remarks>
        Property CampoComumTabDireita As String

        ''' <summary>
        ''' Nome da Coluna a ser utilizada no condição <c>WHERE</c> da consulta.
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' <code>NomeCampoChave = <c>COD_CLIENTE</c></code>
        ''' </example>
        '''</remarks>
        Property NomeCampoChave As String

        ''' <summary>
        ''' Valor do filtro a ser incluído na junção.        
        ''' </summary>
        ''' <remarks>
        ''' <example>
        ''' <code>BH</code>
        ''' </example>
        '''</remarks>
        Property ValorCampoChave As String

        ''' <summary>
        ''' Join personalizado
        ''' </summary>
        ''' <value>
        ''' Join personalizado.
        ''' </value>
        Property JoinPersonalizado As String

    End Structure

End Class

