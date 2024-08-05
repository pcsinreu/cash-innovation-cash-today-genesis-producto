Imports System.Configuration

Public Class Constantes

    ' configura o nome da conexão
    Public Const CONEXAO_SALDOS As String = "SALDOS"

    ' nome do event viewer
    Public Const NOME_LOG_EVENTOS As String = "SALDOS"

    ' codigo dos estados comprobantes
    Public Const COD_ESTADO_COMPROBANTE_RECHAZADO As String = "R"
    Public Const COD_ESTADO_COMPROBANTE_IMPRESO As String = "I"
    Public Const COD_ESTADO_ENPROCESO As String = "P"
    Public Const COD_ESTADO_ACEITO As String = "A"

    Public Const ESTADO_RECHAZADO As String = "Rechazar"

    ' Constantes usadas nos arquivos criados no processo de importação e exportação do automata
    Public Const AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL As String = "000000"
    Public Const AUTOMATA_ARQUIVO_SEPARADOR As String = " "
    Public Const AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO As String = "0"
    Public Const AUTOMATA_EXTENSAO_ARQUIVO_LOG As String = ".log"
    Public Const AUTOMATA_ARQUIVO_VERSAO As String = "V5.0.04.76"


    ' Chaves identificadores dos registros gravados nas linhas do arquivo do automata
    Public Const AUTOMATA_ARQUIVO_CHAVE_BACLIDE = "BACLIDE"
    Public Const AUTOMATA_ARQUIVO_CHAVE_BACLIVA = "BACLIVA"
    Public Const AUTOMATA_ARQUIVO_CHAVE_BOLPREC = "BOLPREC"
    Public Const AUTOMATA_ARQUIVO_CHAVE_CNTDEST = "CNTDEST"
    Public Const AUTOMATA_ARQUIVO_CHAVE_CNTRPRC = "CNTRPRC"
    Public Const AUTOMATA_ARQUIVO_CHAVE_DESTINO = "DESTINO"
    Public Const AUTOMATA_ARQUIVO_CHAVE_DETALLE = "DETALLE"
    Public Const AUTOMATA_ARQUIVO_CHAVE_HEADERX = "HEADERX"
    Public Const AUTOMATA_ARQUIVO_CHAVE_IDDOCET = "IDDOCET"
    Public Const AUTOMATA_ARQUIVO_CHAVE_MONTOXX = "MONTOXX"
    Public Const AUTOMATA_ARQUIVO_CHAVE_PLANSUC = "PLANSUC"
    Public Const AUTOMATA_ARQUIVO_CHAVE_RECIBOV = "RECIBOV"
    Public Const AUTOMATA_ARQUIVO_CHAVE_VERSION = "VERSION"

    ' Campos do formulário do automata
    Public Const AUTOMATA_FORMULARIO_CAMPO_BANCO = "Banco"
    Public Const AUTOMATA_FORMULARIO_CAMPO_BANCODEPOSITO = "BancoDeposito"
    Public Const AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN = "CentroProcesoOrigen"
    Public Const AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO = "CentroProcesoDestino"
    Public Const AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN = "ClienteOrigen"
    Public Const AUTOMATA_FORMULARIO_CAMPO_CLIENTEDESTINO = "ClienteDestino"
    Public Const AUTOMATA_FORMULARIO_CAMPO_NUMEXTERNO = "NumExterno"

    ' Estados do Automata
    Public Const AUTOMATA_ESTADO_AGREGADO = "Agregado"
    Public Const AUTOMATA_ESTADO_NOAGREGADO = "NoAgregado"

    ' Sub Estados do Automada
    Public Const AUTOMATA_SUBESTADO_ENPROCESSO = "EnProcesso"
    Public Const AUTOMATA_SUBESTADO_IMPRESO = "Impreso"

    '' Versões do arquivo do automata
    Public Const AUTOMATA_ARQUIVO_VERSAO_SIGII = "SIGII"
    Public Const AUTOMATA_ARQUIVO_VERSAO_RBO = "RBO"

    ' Documento: código de fechamento de tesouro
    Public Const COD_FECHAMENTO_TESOURO As String = "0000#"

    ' Nome dos campos
    Public Const NOMBRE_CENTRO_PROCESO_ORIGEM As String = "CentroProcesoOrigen"
    Public Const NOMBRE_CENTRO_PROCESO_DESTINO As String = "CentroProcesoDestino"

    ' Filtros para buscar as transacciones de una planta
    Public Const CONST_OID_TRANSACCION As String = "OID_TRANSACCION"
    Public Const CONST_FECHA_HORA_DESDE As String = "FECHA_HORA_DESDE"
    Public Const CONST_FECHA_HORA_HASTA As String = "FECHA_HORA_HASTA"
    Public Const CONST_COD_CLIENTE As String = "COD_CLIENTE"
    Public Const CONST_COD_PLANTA As String = "COD_PLANTA"
    Public Const CONST_COD_SECTOR As String = "COD_SECTOR"
    Public Const CONST_COD_CANAL As String = "COD_CANAL"
    Public Const CONST_COD_MONEDA As String = "COD_MONEDA"
    Public Const CONST_SOLO_SALDO_DIPONIBLE As String = "SOLO_SALDO_DIPONIBLE"

End Class