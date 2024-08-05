''' <summary>
''' Constantes contracto servicio
''' </summary>
''' <remarks></remarks>
Public Class Constantes

    ' Constantes para o Event Viewer
    Public Const NOME_LOG_EVENTOS As String = "IAC"

    ' Constantes do site WEB
    Public Const NOME_PAGINA_LOGIN As String = "LoginUnificado.aspx"
    Public Const NOME_PAGINA_MENU As String = "Default.aspx"

    '### CONSTANTES DE CARACTERISTICAS TIPO PROCESADO UTILIZADO NO CONTEO ###'
    Public Const COD_CARAC_PROCESAR_POR_PARCIALE As String = "1"
    Public Const COD_CARAC_CONTEO_EN_CIEGO As String = "2"
    Public Const COD_CARAC_ADMITE_IAC As String = "3"

    '### CONSTANTES DE TIPO DO FORMATO
    Public Const COD_FORMATO_TEXTO As String = "1"
    Public Const COD_FORMATO_ENTERO As String = "2"
    Public Const COD_FORMATO_DECIMAL As String = "3"
    Public Const COD_FORMATO_FECHA As String = "4"
    Public Const COD_FORMATO_FECHA_HORA As String = "5"
    Public Const COD_FORMATO_BOLEANO As String = "6"

    Public Const MAX_LONGITUDE As Int32 = 255

    Public Const CONST_NOME_ARQ_LOG As String = "Log"

    ' constantes - tipo de meios de pagamento
    Public Const COD_TIPO_MEDIO_PG_OTROSVALORES As String = "codtipo"
    Public Const COD_TIPO_MEDIO_PG_TICKET As String = "codtipoa"
    Public Const COD_TIPO_MEDIO_PG_CHEQUE As String = "codtipob"
    Public Const COD_TIPO_MEDIO_PG_TARJETA As String = "codtipoc"

    ' constantes  - Tipos de Algoritmos da fórmula términos
    Public Const COD_ALGORITMO_VALIDACION_RIB As String = "Clave_RIB"
    Public Const COD_ALGORITMO_VALIDACION_CARREFOUR As String = "Cajs_Carrefour"
    ' constante - Valor Longitude para algoritmo Clave_RIB
    Public Const CONST_VALOR_LONGITUDE_CLAVE_RIB As String = "23"

    '### CONSTANTES DE ERRO ###'
    Public Const CONST_ERRO_DNS As String = "System.Net.HttpWebRequest.GetRequestStream()"
    Public Const CONST_ERRO_COMUNICACION_LDAP As String = "(0x8007203A):"
    Public Const CONST_ERRO_ACCESO_LDAP As String = "(0x8007052E):"
    Public Const CONST_ERRO_BANCO As String = "ORA-12154:"
    Public Const CONST_ERRO_BANCO2 As String = "ORA-12541:"
    Public Const CONST_ERRO_BANCO_AK_VIOLATION As String = "ORA-00001:"
    Public Const CONST_ERRO_404A As String = "404"
    Public Const CONST_ERRO_404B As String = "404:"
    Public Const CONST_ERRO_407A As String = "407"
    Public Const CONST_ERRO_407B As String = "407:"
    Public Const CONST_ERRO_URL As String = "System.Net.Sockets.Socket.DoConnect"

    ' Función dispensador
    Public Const C_COD_FUNCION_CONTENEDOR_DISPENSADOR As Integer = 1
    Public Const C_COD_FUNCION_CONTENEDOR_INGRESADOR As Integer = 2
    Public Const C_COD_FUNCION_CONTENEDOR_DEPOSITO As Integer = 3
    Public Const C_COD_FUNCION_CONTENEDOR_RECHAZO As Integer = 4
    Public Const C_COD_FUNCION_CONTENEDOR_TARJETA As Integer = 5

    ' modalidad recogida
    Public Const C_COD_MODALIDAD_EN_BASE As Integer = 1
    Public Const C_COD_MODALIDAD_A_PIE As Integer = 2
    Public Const C_COD_MODALIDAD_ADICION_CON_DOS_TIRAS As Integer = 3

    ' Tipo de componente (morfologia)
    Public Const C_COD_TIPO_BOLSA As String = "Bolsa"

    ' Constantes para o contenedor
    Public Const CONST_TIPO_CONTENEDOR_PARCIAL As String = "P"
    Public Const CONST_TIPO_CONTENEDOR_BULTO As String = "B"
    Public Const CONST_TIPO_CONTENEDOR_REMESA As String = "R"

    ' código utilizado no campo Orden do componente
    Public Const C_ORDEN_DISPENSADOR As String = "DI"
    Public Const C_ORDEN_DEPOSITO As String = "DE"
    Public Const C_ORDEN_INGRESADOR As String = "IN"
    Public Const C_ORDEN_RECHAZO As String = "RE"
    Public Const C_ORDEN_TARJETA As String = "TA"

    'Codigo nivel dos parametros
    Public Const C_NIVEL_PARAMETRO_PUESTO As String = "3"
    Public Const C_NIVEL_PARAMETRO_DELEGACION As String = "2"
    Public Const C_NIVEL_PARAMETRO_PAIS As String = "1"

    ' constantes para nomes das entidades codigo ajeno
    Public Const COD_CLIENTE As String = "CLIENTE"
    Public Const COD_SUBCLIENTE As String = "SUBCLIENTE"
    Public Const COD_PUNTOSERVICIO As String = "PUNTO_SERVICIO"
    Public Const COD_CANAL As String = "CANAL"
    Public Const COD_SUBCANAL As String = "SUBCANAL"
    Public Const COD_PLANTA As String = "PLANTA"
    Public Const COD_TIPO_SECTOR As String = "TIPO_SECTOR"
    Public Const COD_SECTOR As String = "SECTOR"
    Public Const COD_GRUPO_CLIENTE As String = "GRUPO_CLIENTE"
    Public Const COD_DELEGACION As String = "DELEGACION"
    Public Const COD_DIVISA As String = "DIVISA"
    Public Const COD_DENOMINACION As String = "DENOMINACION"
    Public Const COD_MAQUINA As String = "MAQUINA"

    'Constantes Codigos Tipo
    Public Const CONST_CODIGO_TIPO_FORMATO As String = "05"

   
End Class
