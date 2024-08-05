Public Class Constantes

    Public Const NOME_PAGINA_LOGIN As String = "~/LoginUnificado.aspx"
    Public Const NOME_PAGINA_MENU As String = "~/Default.aspx"
    'Public Const NOME_PAGINA_SELECCION_SECTOR As String = "~/Pantallas/SeleccionSector.aspx"
    Public Const NOME_PAGINA_CONSULTA_TRANSACCIONES As String = "~/Pantallas/Consultas/ConsultaTransacciones.aspx"

    Public Const CONST_REPORTES_TEMP As String = "ReportesTemp"

    Public Shared Property COLOR_CON_DIFERENCIA As Drawing.Color = Drawing.Color.LightSalmon
    Public Shared Property COLOR_SIN_DIFERENCIA As Drawing.Color = Drawing.Color.White

    Public Class Preferencias
        Public Class Abono
            Public Const FUNCIONALIDAD_ABONO As String = "ABONO"
            Public Const PROPRIEDAD_FILTRO_ELEMENTO As String = "FILTRO_ABONO_ELEMENTO"
            Public Const PROPRIEDAD_FILTRO_SALDOS As String = "FILTRO_ABONO_SALDOS"
            Public Const PROPRIEDAD_FILTRO_PEDIDO As String = "FILTRO_ABONO_PEDIDO"
        End Class
    End Class

End Class
