''' <summary>
''' Constantes acceso datos
''' </summary>
''' <remarks></remarks>
''' <history>
''' [magnum.oliveira] 16/07/2008 Criado
''' </history>
Public Class Constantes

    ' conexão gestão de efetivo
    Public Const CONEXAO_GE As String = "GENESIS"

    ' conexão Reportes
    Public Const CONEXAO_REPORTE As String = "REPORTE"

    ' Procedure que retorna os dados do relatório de conteo parcial para gerar o arquivo CSV
    Public Shared ReadOnly SP_CONTEO_PARCIAL_CSV As String = "SP_CONTEO_PARCIAL_CSV_" & Prosegur.Genesis.Comon.Util.Version & ".GET_ROWS"
    ' Procedure que retorna os dados do relatório de conteo parcial para gerar o arquivo PDF
    Public Shared ReadOnly SP_CONTEO_PARCIAL_PDF As String = "SP_CONTEO_PARCIAL_PDF_" & Prosegur.Genesis.Comon.Util.Version & ".GET_ROWS"

    ' Procedure que retorna os dados do relatório de respaldo completo para gerar o arquivo CSV
    Public Shared ReadOnly SP_RESPALDO_COMPLETO_CSV As String = "SP_RESPALDO_COMP_CSV_" & Prosegur.Genesis.Comon.Util.Version & ".GET_ROWS"
    ' Procedure que retorna os dados do relatório de respaldo completo para gerar o arquivo PDF
    Public Shared ReadOnly SP_RESPALDO_COMPLETO_PDF As String = "SP_RESPALDO_COMP_PDF_" & Prosegur.Genesis.Comon.Util.Version & ".GET_ROWS"

    ' Procedure que apaga a tabela temporária de IAC criada pela procedure SP_RESPALDO_COMPLETO_CSV
    Public Const SP_DROP_IAC_TABLE As String = "SP_DROP_IAC_TABLE"

    ' Procedure que retorna os dados do relatório de detalle parciales
    Public Shared ReadOnly PKG_DETALLE_PARCIALES_CSV As String = "PKG_DETALLEPARCIALES_" & Prosegur.Genesis.Comon.Util.Version & ".CSV"
    ' Procedure que retorna os dados do relatório de detalle parciales
    Public Shared ReadOnly PKG_DETALLE_PARCIALES_PDF As String = "PKG_DETALLEPARCIALES_" & Prosegur.Genesis.Comon.Util.Version & ".PDF"

    ' Procedure que retorna os dados do relatório de contados por puesto para gerar o arquivo CSV
    Public Shared ReadOnly SP_CONTADO_PUESTO_CSV As String = "PKG_CONTADOPORPUESTO_" & Prosegur.Genesis.Comon.Util.Version & ".CSV"
    ' Procedure que retorna os dados do relatório de contados por puesto para gerar o arquivo PDF
    Public Shared ReadOnly SP_CONTADO_PUESTO_PDF As String = "PKG_CONTADOPORPUESTO_" & Prosegur.Genesis.Comon.Util.Version & ".PDF"

    ' Procedure que retorna os dados do relatório Recibo F22 e Respaldo para gerar um arquivo TXT
    Public Shared ReadOnly PKG_RECIBO_F22_RESPALDO_TXT As String = "PKG_RECIBOF22RESPALDO_" & Prosegur.Genesis.Comon.Util.Version & ".GET_ROWS"

    'Procedure que retorna os dados de contagem da remesa
    Public Shared ReadOnly PKG_INFORME_RESULTADO_CONTAJE As String = "PKG_RESULTADOCONTAJE_" & Prosegur.Genesis.Comon.Util.Version & ".PDF"
End Class