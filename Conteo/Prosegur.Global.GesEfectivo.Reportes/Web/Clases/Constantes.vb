Public Class Constantes

#Region "Constantes"

    Public Const COD_APLICACION As String = "Reportes"

    'Mensagens 
    Public Const InfoMsgBaja As String = "info_msg_baja"
    Public Const InfoMsgSeleccionarRegistro As String = "info_msg_seleccionar_registro"
    Public Const InfoMsgBajaRegistro As String = "info_msg_baja_registro"
    Public Const InfoMsgSinRegistro As String = "info_msg_sin_registro"
    Public Const InfoMsgMaxRegistro As String = "info_msg_max_registro"
    Public Const InfoMsgSairPagina As String = "info_msg_sair_pagina"
    Public Const InfoMsgSeleccionarRegistroUnico As String = "info_msg_seleccionar_registro_unico"

    'Número máximos de caracteres a serem exibidos por coluna no GridView
    Public Const MaximoCaracteresLinha As Integer = 20
    'Número máximos de registros a serem exibidos no GridView
    Public Const MaximoRegistrosGrid As Integer = 100
    'Quebra de linha
    Public Const LineBreak As String = "<BR/>"

    ' Constantes de culturas 
    Public Class Cultura
        Public Const EN_US As String = "en-US"
    End Class

    ' Constantes para o Event Viewer
    Public Const NOME_LOG_EVENTOS As String = "REPORTES"

    'Constante nome aplicação
    Public Const CONST_NOME_APLICACION As String = "ReporteProsegur"

    'Constantes BCP
    Public Const CONST_BCP_CODIGO_PROCESO_DUO As String = "ReportarPedidoBCP"

    '
    Public Const CONST_NOME_ARQUIVO_LOG_ITEM_PROCESO As String = "FECHA_REPORTE_PEDIDO.TXT"

    Public Const CONST_REPORTES_NOMBRE_ZIP As String = "Reportes"
    Public Const CONST_REPORTES_TEMP As String = "ReportesTemp"

    ' Constantes do site WEB
    Public Const NOME_PAGINA_LOGIN As String = "LoginUnificado.aspx"
    Public Const NOME_PAGINA_MENU As String = "Default.aspx"
    Public Const NOME_PAGINA_PRINCIPAL As String = "Principal.aspx"

    ' Constantes do tipo de informação do relatório
    Public Class TipoInformacaoRelatorio
        Public Const BILLETAJE_POR_SUCURSAL As String = "B"
        Public Const CORTE_PARCIAL As String = "C"
        Public Const TOTAL_RECONTADO As String = "T"
        Public Const RESPALDO_COMPLETO As String = "R"
        Public Const DETALLE_PARCIALES As String = "D"
        Public Const CONTADO_PUESTO As String = "P"
    End Class

    Public Const CODIGO_PARAMETRO_MAES_URL = "UrlAcreditacionesMae"

    Public Class RespaldoCompleto
        Public Const SEPARADOR_INFORMACOES_IAC As String = ";"
        Public Const SEPARADOR_INFORMACAO_IAC As String = "="
    End Class

    Public Class TipoFecha
        Public Const TRANSPORTE As Integer = 0
        Public Const PROCESO As Integer = 1
        Public Const CONTEO As Integer = 2
    End Class

    Public Const DESCRICAO_EFETIVO As String = "efectivo"

    Public Const LBL_TITULO_RELATORIO As String = "lbl_titulo_relatorio"

    '### CONSTANTES DE ERRO ###'
    Public Const CONST_ERRO_DNS As String = "System.Net.HttpWebRequest.GetRequestStream()"
    Public Const CONST_ERRO_COMUNICACION_LDAP As String = "(0x8007203A):"
    Public Const CONST_ERRO_ACCESO_LDAP As String = "(0x8007052E):"
    Public Const CONST_ERRO_BANCO As String = "ORA-12154:"
    Public Const CONST_ERRO_BANCO2 As String = "ORA-12541:"
    Public Const CONST_ERRO_BANCO3 As String = "ORA-01017:"
    Public Const CONST_ERRO_404A As String = "404"
    Public Const CONST_ERRO_404B As String = "404:"
    Public Const CONST_ERRO_407A As String = "407"
    Public Const CONST_ERRO_407B As String = "407:"
    Public Const CONST_ERRO_URL As String = "System.Net.Sockets.Socket.DoConnect"

    '### CONSTANTES ESTADOS REMESSA ###'
    Public Const CONST_ESTADO_REMESSA_EN_CURSO As String = "EC"

    '### REPORTING SERVICE ###'
    Public Const wsdl2010 As String = "/ReportService2010.asmx"
    Public Const wsdl2005 As String = "/ReportService2005.asmx"
    Public Const wsdlreportExecution As String = "/ReportExecution2005.asmx"

#End Region

End Class
