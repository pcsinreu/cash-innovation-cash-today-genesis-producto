''' <summary>
''' Constantes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [prezende]  11/04/2012  Criado
''' </history>
Public Class Constantes

    Public Const CONST_CODIGO_SEM_ERROR_DEFAULT As Integer = 0
    Public Const CONST_CODIGO_ERROR_AMBIENTE_DEFAULT As Integer = 1
    Public Const CONST_CODIGO_ERROR_NEGOCIO_DEFAULT As Integer = 100
    Public Const CONST_CODIGO_ERROR_NEGOCIO_SESION_INVALIDA As Integer = 101  'Caducada, Error, Inexistente, Incorrecto

    'Rye
    Public Const CONST_CODIGO_ERROR_DIVISION_BULTOS_MAS_30 As Integer = 121
    Public Const CONST_CODIGO_ERROR_NO_DIVIDIDO_BULTOS As Integer = 122
    Public Const CONST_CODIGO_ERROR_ALT_DATOS_RUTA As Integer = 123

    ' Nuevo Saldos
    Public Const CONST_CODIGO_ERROR_NEGOCIO_HAYCIERRECAJA As Integer = 120

    ' exceções específicas
    Public Const CONST_CODIGO_ERROR_NEGOCIO_YES_NO As Integer = 101
    Public Const CONST_CODIGO_ERROR_NEGOCIO_ATENCION As Integer = 102
    Public Const CONST_CODIGO_ERROR_NEGOCIO_ULTIMO_BULTO_IMPRESSION As Integer = 103

    'Exceções ATM
    Public Const CONST_CODIGO_ERROR_SIN_TIRA As Integer = 109

    'Exceções Conteo
    Public Const CONST_CODIGO_ERROR_PRECINTO_EXISTENTE As Integer = 102
    Public Const CONST_CODIGO_ERROR_NEGOCIO_SALDO_PUESTO As Integer = 103
    Public Const CONST_CODIGO_ERROR_SENHA_INVALIDA As Integer = 104
    Public Const CONST_CODIGO_ERROR_PETICAO_YA_PROCESADA As Integer = 105
    Public Const CONST_CODIGO_ERROR_REMESA_CP_NO_ES_PENDIENTE As Integer = 106
    Public Const CONST_CODIGO_ERROR_FORMULA_INCORRECTA As Integer = 107
    Public Const CONST_CODIGO_ERROR_REGISTRO_MOVIMIENTO_EN_PROCESO As Integer = 108
    Public Const CONST_CODIGO_ERROR_PUESTO_MECANIZADO_NO_INFORMADO As Integer = 110
    Public Const CONST_CODIGO_ERROR_ASOCIAR_ARCHIVO_MECANIZADO As Integer = 111

    Public Const CONST_CODIGO_ERROR_BULTO_INTERNO As String = "ERV01"
    Public Const CONST_CODIGO_ERROR_PUESTO As String = "ERV02"
    Public Const CONST_CODIGO_ERROR_ESTADO_BULTO As String = "ERV03"
    Public Const CONST_CODIGO_ERROR_USUARIO As String = "ERV04"


    'Excepcion Servicio Integracion
    'Tipo Error	        Código	    Mensaje
    'Infraestructura	MSG0001	    No se pudo completar la operación, reintente más tarde.
    'Funcional     	    MSG1001	    Verifique los datos Ingresados
    'Aplicación	        MSG2002	    Aconteció un error en la aplicación. Consulte con el administrador del sistema
    Public Const CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA As String = "MSG0001"
    Public Const CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL As String = "MSG1001"
    Public Const CONST_CODIGO_ERROR_INTEGRACION_APLICACION As String = "MSG2002"
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA As String = "No se pudo completar la operación, reintente más tarde."
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL As String = "Verifique los datos Ingresados."
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_APLICACION As String = "Aconteció un error en la aplicación. Consulte con el administrador del sistema."
    Public Const CONST_CODIGO_INTEGRACION_SEM_ERRO As String = "0"
    Public Const CONST_DESCRICION_INTEGRACION_SEM_ERRO As String = "OK"

    Public Const CONST_CODIGO_INTEGRACION_SEM_ERRO_v2 As String = "0000010001"
    Public Const CONST_DESCRICION_INTEGRACION_SEM_ERRO_v2 As String = "Éxito"
    Public Const CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA_v2 As String = "2000010001"
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA_v2 As String = "No se pudo completar la operación, reintente más tarde."
    Public Const CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL_v2 As String = "2000010002"
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL_v2 As String = "Verifique los datos Ingresados."
    Public Const CONST_CODIGO_ERROR_INTEGRACION_APLICACION_v2 As String = "2000010003"
    Public Const CONST_DESCRICION_ERRO_INTEGRACION_APLICACION_v2 As String = "Aconteció un error en la aplicación. Consulte con el administrador del sistema."

    'Codigos de errores del servicio generarCertificado
    Public Const CONST_CODIGO_ERROR_CERTIFICADO_TOKEN As String = "001"
    Public Const CONST_CODIGO_ERROR_CERTIFICADO_PARAMETRO As String = "002"
    Public Const CONST_CODIGO_ERROR_CERTIFICADO_FUNCIONAL As String = "003"
    Public Const CONST_CODIGO_ERROR_CERTIFICADO_GENERICA As String = "004"


End Class