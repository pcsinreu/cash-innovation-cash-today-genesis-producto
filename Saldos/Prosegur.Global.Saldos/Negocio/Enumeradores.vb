Public Class Enumeradores

    ' Nome das entidades usadas no automata
    Public Enum Automata_Entidade
        Banco
        Cliente
        CentroProcesso
    End Enum

    ' Filtros usados no relatório de Seguimiento Bultos
    Public Enum Relatorio_SeguimientoBulto
        NumeroExterno
        CodigoBolsa
        NumeroPrecinto
    End Enum

    ' Tipo do arquivo exportado do relatório 
    Public Enum Relatorio_Exportado_Tipo
        Excel
        PDF
    End Enum

    ' Tipo do arquivo exportado do relatório 
    Public Enum ReporteCondicion
        RelatorioBultosPorPlanta = 1
        RelatorioBultosPorRecorrido = 2
        RelatorioInventarioDeBultos = 3
        UtilizadoProcedureSeleccionarIDGrupo = 4
        RelatorioTransacoesV5 = 5
    End Enum

End Class