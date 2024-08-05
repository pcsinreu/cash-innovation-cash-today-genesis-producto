Public Class Enumeradores

#Region "Enumerador ação"

    ''' <summary>
    ''' Enumerador com as ações mais comuns utilizada na aplicação
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Enum eAcao As Integer
        Inicial = 0
        NoAction = 1
        Alta = 2
        Baja = 3
        Modificacion = 4
        Consulta = 5
        Busca = 6
        Erro = 7
        Duplicar = 8
    End Enum

    ''' <summary>
    ''' Telas do sistema
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' [magnum.oliveira] 16/12/2009 Alterado
    ''' [blcosta] 12/08/2010 Alterado
    ''' </history>
    Public Enum eTelas As Integer
        EMBRANCO
        ERRO
        LOGIN
        MENU
        CLIENTE
        BILLETAJE_SUCURSAL
        CORTE_PARCIAL
        RESPALDO_COMPLETO
        DETALLE_PARCIALES
        CONTADO_PUESTO
        SUBCLIENTE
        PROCESO
        PUNTO_SERVICIO
        PRINCIPAL
        RECIBO_F22_RESPALDO
        RELACION_HABILITACION_TIRA_CONTEO
        PARTE_DIFERENCIAS
        REPORTAR_PEDIDO_BCP
        INFORME_RESULTADO_CONTAJE
        REPORTES
        REPORTES_CONFIGURAR
        ACREDITACIONES
    End Enum

    ''' <summary>
    ''' Ações disponíveis por telas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Enum eAcoesTela As Integer
        _CONSULTAR
        _MODIFICAR
    End Enum

    ''' <summary>
    ''' Enumerador que define os tipos de mensagens do sistema
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 21/07/2009 Criado
    ''' </history>
    Public Enum eMensagem As Integer
        Atencao
        Erro
        Informacao
        Exito
        Download
    End Enum

#End Region

#Region "Enumerações relatórios"

    ''' <summary>
    ''' Enumeração dos tipos de data utilizadas pelo relatório "Contados por Posto"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Public Enum eTipoFecha
        Processo = 0
        Transporte = 1
    End Enum

    ''' <summary>
    ''' Enumeração dos CodTipoConteo utilizadas pelo relatório "Relación Habilitación X Tira X Conteo"
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 13/07/2011 Criado
    ''' </history>
    Public Enum eCodTipoConteo
        Dispensadores = 1
        Ingresadores = 2
        Depositos = 3
    End Enum

    ''' <summary>
    ''' Enumeração dos tipos de operação da tela de configuração de relatórios (ConfiguracionReport.aspx)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eTipoOperacion
        Grabar = 0
        Generar = 1
        PostBack = 2
    End Enum

#End Region

#Region "[Tipo Medio Pago]"

    Public Enum TipoMedioPago
        Efectivo = 1
        Cheque = 2
        Ticket = 3
        OtroValor = 4
        Total = 5
    End Enum

#End Region

#Region "[Formato Arquivo]"

    Public Enum eFormatoArchivo
        [CSV] = 1
        [PDF] = 2
        [EXCEL] = 3
        [EXCEL2010] = 4
        [MHTML] = 5
    End Enum

#End Region
#Region "Enumeradores Helpers"
    Public Enum Modo
        Alta
        AltaImpresion 'Integracion Salidas -> NuevoSaldos
        Modificacion
        Baja
        Consulta
    End Enum

    Public Enum DiscriminarPor
        <Prosegur.Genesis.Comon.Atributos.ValorEnum("Sector")>
        Sector

        <Prosegur.Genesis.Comon.Atributos.ValorEnum("ClienteyCanal")>
        ClienteyCanal

        <Prosegur.Genesis.Comon.Atributos.ValorEnum("Cuenta")>
        Cuenta
        <Prosegur.Genesis.Comon.Atributos.ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum
#End Region
End Class
