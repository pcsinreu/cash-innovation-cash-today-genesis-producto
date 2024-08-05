''' <summary>
''' Constantes contracto servicio
''' </summary>
''' <history>
''' [magnum.oliveira] 15/07/2009 Criado
''' </history>
''' <remarks></remarks>
Public Class Constantes

    Public Const CODIGO_TIPO_MOVIMIENTO = "C"

    Public Const C_TIPO_DECLARADO_REMESA As String = "R"
    Public Const C_TIPO_DECLARADO_BULTO As String = "B"
    Public Const C_TIPO_DECLARADO_PARCIAL As String = "P"

    Public Const C_TIPO_EFECTIVO_BILLETE As String = "B"
    Public Const C_TIPO_EFECTIVO_MONEDA As String = "M"

    Public Const CONST_COD_CALIDAD_DETERIORADO As String = "D"

    '### CONSTANTES DE ERRO ###'
    Public Const CONST_ERRO_DNS As String = "System.Net.HttpWebRequest.GetRequestStream()"
    Public Const CONST_ERRO_COMUNICACION_LDAP As String = "(0x8007203A):"
    Public Const CONST_ERRO_ACCESO_LDAP As String = "(0x8007052E):"
    Public Const CONST_ERRO_BANCO As String = "ORA-12154:"
    Public Const CONST_ERRO_BANCO2 As String = "ORA-12541:"
    Public Const CONST_ERRO_BANCO3 As String = "ORA-01017:"
    Public Const CONST_ERRO_BANCO4 As String = "ORA-00942:"
    Public Const CONST_ERRO_404A As String = "404"
    Public Const CONST_ERRO_404B As String = "404:"
    Public Const CONST_ERRO_407A As String = "407"
    Public Const CONST_ERRO_407B As String = "407:"
    Public Const CONST_ERRO_URL As String = "System.Net.Sockets.Socket.DoConnect"


    '### CONSTANTE DELIMITADOR ###'
    Public Const CONST_DELIMITADOR As String = "§"

    ' Constantes para o contenedor
    Public Const CONST_TIPO_CONTENEDOR_PARCIAL As String = "P"
    Public Const CONST_TIPO_CONTENEDOR_BULTO As String = "B"
    Public Const CONST_TIPO_CONTENEDOR_REMESA As String = "R"

    '### CONSTANTES DE CARACTERISTICAS TIPO PROCESADO UTILIZADO NO CONTEO ###'
    Public Const COD_CARAC_PROCESAR_POR_PARCIALE As String = "1"
    Public Const COD_CARAC_ADMITE_IAC As String = "3"
    Public Const COD_CARAC_CONTEO_POR_REMESA As String = "4"

    '### CONSTANTES CODIGOS TIPO MEDIO DE PAGO ###'
    Public Const COD_TIPO_MEDIO_PAGO_OTROSVALORES As String = "codtipo"
    Public Const COD_TIPO_MEDIO_PAGO_TICKET As String = "codtipoa"
    Public Const COD_TIPO_MEDIO_PAGO_CHEQUE As String = "codtipob"
    Public Const COD_TIPO_MEDIO_PAGO_TARJETAS As String = "codtipoc"

End Class
