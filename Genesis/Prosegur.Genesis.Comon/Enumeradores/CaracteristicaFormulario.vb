﻿Imports Prosegur.Genesis.Comon.Atributos

Namespace Enumeradores
    ''' <summary>
    ''' Enumerador CaracteristicaFormulario.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Enum CaracteristicaFormulario
        <ValorEnum("CARACTERISTICA_PRINCIPAL_GESTION_REMESAS")>
        GestiondeRemesas
        <ValorEnum("CARACTERISTICA_PRINCIPAL_GESTION_BULTOS")>
        GestiondeBultos
        <ValorEnum("CARACTERISTICA_PRINCIPAL_GESTION_CONTENEDORES")>
        GestiondeContenedores
        <ValorEnum("CARACTERISTICA_PRINCIPAL_GESTION_PARCIALES")>
        GestiondeParciales
        <ValorEnum("CARACTERISTICA_PRINCIPAL_GESTION_FONDOS")>
        GestiondeFondos
        <ValorEnum("CARACTERISTICA_PRINCIPAL_CIERRES")>
        Cierres
        <ValorEnum("CARACTERISTICA_PRINCIPAL_OTROS_MOVIMIENTOS")>
        OtrosMovimientos
        <ValorEnum("ACCION_ALTAS")>
        Altas
        <ValorEnum("ACCION_BAJAS")>
        Bajas
        <ValorEnum("ACCION_REENVIOS")>
        Reenvios
        <ValorEnum("ACCION_CLASIFICACION")>
        Classificacion
        <ValorEnum("ACCION_ACTAS")>
        Actas
        <ValorEnum("ACCION_AJUSTES")>
        Ajustes
        <ValorEnum("ACCION_SUSTITUCION")>
        Sustitucion
        <ValorEnum("ACCION_MOVIMIENTO_FONDOS")>
        MovimientodeFondos
        <ValorEnum("ACCION_SOLICITACION_FONDOS")>
        SolicitaciondeFondos
        <ValorEnum("PERMITE_LLEGAR_SALDO_NEGATIVO")>
        PermiteLlegarASaldoNegativo
        <ValorEnum("INCLUIR_SECTORES_HIJOS")>
        IncluirSectoresHijos
        <ValorEnum("NO_PERMITE_LLEGAR_SALDO_NEGATIVO")>
        NoPermiteLlegarASaldoNegativo
        <ValorEnum("CIERRE_TESORO")>
        CierreDeTesoro
        <ValorEnum("CIERRE_CAJA")>
        CierreDeCaja
        <ValorEnum("CUADRE_FISICO")>
        CuadreFisico
        <ValorEnum("UTILIZA_LECTOR_CODIGO_BARRA")>
        UtilizaLectorCodigoBarra
        <ValorEnum("ENTRE_SECTORES_MISMA_PLANTA")>
        EntreSectoresDeLaMismaPlanta
        <ValorEnum("ENTRE_SECTORES_PLANTAS_DIFERENTES")>
        EntreSectoresdePlantasDelegacionesDiferentes
        <ValorEnum("ACTA_RECUENTO")>
        ActaDeRecuento
        <ValorEnum("ACTA_CLASIFICACION")>
        ActaDeClasificado
        <ValorEnum("ACTA_EMBOLSADO")>
        ActaDeEmbolsado
        <ValorEnum("ACTA_DESEMBOLSADO")>
        ActaDeDesembolsado
        <ValorEnum("ENTRE_CANALES")>
        EntreCanales
        <ValorEnum("ENTRE_CLIENTES")>
        EntreClientes
        <ValorEnum("MOVIMIENTO_ACEPTACION_AUTOMATICA")>
        MovimientodeAceptacionAutomatica
        <ValorEnum("CONFIRMAR_CON_IMPRESION")>
        AlConfirmarElDocumentoSeImprime
        <ValorEnum("DOCUMENTO_INDIVIDUAL")>
        DocumentoEsDelTipoIndividual
        <ValorEnum("DOCUMENTO_AGRUPADOR")>
        DocumentoEsDelTipoGrupo
        <ValorEnum("MOVIMIENTO_CON_IINFORMACIONES_ROTA")>
        MovimientoConInformacionesDeRota
        <ValorEnum("GESTIONAR_BULTOS_POR_REMESA")>
        PermiteGestionarBultosPorRemesa
        <ValorEnum("GENERA_CONTRAMOVIMIENTOS_DOC_ORIGINAL")>
        GeneraContraMovimientosDocOriginal
        <ValorEnum("BAJA_ELEMENTOS")>
        BajaElementos
        <ValorEnum("SALIDAS_RECORRIDO")>
        SalidasRecorrido
        <ValorEnum("REENVIO_AUTOMATICO")>
        ReenvioAutomatico
        <ValorEnum("INTEGRACION_SALIDAS")>
        IntegracionSalidas
        <ValorEnum("CODIGO_EXTERNO_OBLIGATORIO")>
        CodigoExternoObligatorio
        <ValorEnum("INTEGRACION_RECEPCIONYENVIO")>
        IntegracionRecepcionEnvio
        <ValorEnum("INTEGRACION_LEGADO")>
        IntegracionLegado
        <ValorEnum("INTEGRACION_CONTEO")>
        IntegracionConteo
        <ValorEnum("ACCION_CONTESTAR_SOLICITACION_FONDOS")>
        ContestarSolicitacionDeFondos
        <ValorEnum("SOLO_PERMITIR_AJUSTES_POSITIVOS")>
        SoloPermitirAjustesPositivos
        <ValorEnum("SOLO_PERMITIR_AJUSTES_NEGATIVOS")>
        SoloPermitirAjustesNegativos
        <ValorEnum("EXCLUIR_SECTORES_HIJOS")>
        ExcluirSectoresHijos
        <ValorEnum("PACK_MODULAR")>
        PackModular
        <ValorEnum("DESARMAR_CONTENEDORES")>
        DesarmarContenedor
        <ValorEnum("MODIFICAR_TERMINOS")>
        ModificarTerminos
        <ValorEnum("NO-DEFINIDO")>
        NoDefinido
    End Enum
End Namespace