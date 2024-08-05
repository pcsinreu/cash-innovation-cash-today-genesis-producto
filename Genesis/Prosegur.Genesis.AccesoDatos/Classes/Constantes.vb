Public Class Constantes

    Private Shared _Versao As String

    Public Shared ReadOnly SP_SALDOS_GENERAR_CERTIFICADO_EJECUTAR As String = "SAPR_PCERTIFICACION_" & Prosegur.Genesis.Comon.Util.Version & ".SEJECUTAR"
    Public Shared ReadOnly SP_SALDOS_GENERAR_CERTIFICADO_CONVERTIR As String = "SAPR_PCERTIFICACION_" & Prosegur.Genesis.Comon.Util.Version & ".SCONVERTIR"
    Public Shared ReadOnly SP_SALDOS_RECUPERAR_FILTROS_CERTIFICADO As String = "SAPR_PCERTIFICACION_" & Prosegur.Genesis.Comon.Util.Version & ".SRECUPERAR_FILTROS_COD_CERT"
    Public Shared ReadOnly SP_SALDOS_BORRAR_CERTIFICADO As String = "SP_BORRAR_CERTIFICADO_" & Prosegur.Genesis.Comon.Util.Version
    Public Shared ReadOnly SP_GEPR_YDUPR_SCREAR_ITEM_2 As String = "GEPR_YDUPR_SCREAR_ITEM_2"
    Public Shared ReadOnly SP_SALIDAS_VALIDAR_RECIBO As String = "UTIL_" & Prosegur.Genesis.Comon.Util.Version & ".SP_VALIDAR_RECIBO"
    Public Shared ReadOnly SP_SALIDAS_GRABAR_RECIBO As String = "UTIL_" & Prosegur.Genesis.Comon.Util.Version & ".SP_GRABAR_RECIBO"
    Public Shared ReadOnly SP_GUARDAR_DOCUMENTO_CONTENEDOR_ALTA As String = "sapr_pdocumento_" & Prosegur.Genesis.Comon.Util.Version & ".sguardar_doc_contenedor_alta"
    Public Shared ReadOnly SP_GUARDAR_GRUPO_DOCUMENTO_CONTENEDOR_ALTA As String = "sapr_pdocumentos_grp_" & Prosegur.Genesis.Comon.Util.Version & ".sguardar_grp_docs_cont_alta"

    ' Identificadores das conexões
    Public Const CONEXAO_ATM As String = "ATM"
    Public Const CONEXAO_CONTEO As String = "CONTEO"
    Public Const CONEXAO_DUO As String = "DUO"
    Public Const CONEXAO_GENESIS As String = "GENESIS"
    Public Const CONEXAO_REPORTES As String = "REPORTE"
    Public Const CONEXAO_SALDOS As String = "SALDOS"
    Public Const CONEXAO_SALIDAS As String = "SALIDAS"
    Public Const CONEXAO_DASHBOARD As String = "DASHBOARD"

    ' Constantes que armazenam os valores padrões para Tipos de Mercancia
    Public Const CONST_TIPO_MERCANCIA_BILLETE As String = "BILLETE"
    Public Const CONST_TIPO_MERCANCIA_MONEDA As String = "MONEDA"
    Public Const CONST_TIPO_MERCANCIA_EXTRANJERO As String = "EXTRANJERO"
    Public Const CONST_TIPO_MERCANCIA_MODULO As String = "MODULO"
    Public Const CONST_TIPO_MERCANCIA_ATM As String = "ATM"
    Public Const CONST_TIPO_MERCANCIA_MIXTO As String = "MIXTO"

    'Codigo nivel dos parametros
    Public Const C_NIVEL_PARAMETRO_PUESTO As String = "3"
    Public Const C_NIVEL_PARAMETRO_DELEGACION As String = "2"
    Public Const C_NIVEL_PARAMETRO_PAIS As String = "1"

    Public Shared ReadOnly SP_RECIBIR_MENSAJE_EXTERNO As String = "sapr_pnotificacion_" & Prosegur.Genesis.Comon.Util.Version & ".srecibir_mensaje_externo"
    Public Shared ReadOnly SP_GRABAR_NOTIFICACION_LEIDO As String = "sapr_pnotificacion_" & Prosegur.Genesis.Comon.Util.Version & ".sgrabar_notificacion_leido"
    Public Shared ReadOnly SP_ANADIR_NOTIFICACION As String = "sapr_pnotificacion_" & Prosegur.Genesis.Comon.Util.Version & ".sins_notificacion"

End Class
