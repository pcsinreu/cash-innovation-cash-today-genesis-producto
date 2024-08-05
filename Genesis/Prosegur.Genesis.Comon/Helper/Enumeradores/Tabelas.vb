Imports Prosegur.Genesis.Comon.Atributos

Namespace Helper.Enumeradores

    ''' <summary>
    ''' Classe de Enumeradores das Tabelas Genesis implementadas pelo Controle Helper de Busca.
    ''' </summary>
    ''' <history>
    ''' [Thiago Dias] 22/11/2013
    '''</history>
    <Serializable()>
    Public Class Tabelas

        Public Enum TabelaHelper

            <ValorEnum("GEPR_TCALIDAD")>
            Calidad
            <ValorEnum("GEPR_TCANAL")>
            Canal
            <ValorEnum("GEPR_TCLIENTE")>
            Cliente
            <ValorEnum("GEPR_TCLIENTE")>
            ClientePorTipoPlanificacion
            <ValorEnum("GEPR_TDELEGACION")>
            Delegacion
            <ValorEnum("GEPR_TDELEGACION")>
            DelegacionCuadre
            <ValorEnum("GEPR_TDENOMINACION")>
            Denominacion
            <ValorEnum("GEPR_TDIVISA")>
            Divisa
            <ValorEnum("GEPR_TFORMATO")>
            Formato
            <ValorEnum("SAPR_TFORMULARIO")>
            Formulario
            <ValorEnum("GEPR_TMASCARA")>
            Mascara
            <ValorEnum("GEPR_TMEDIO_PAGO")>
            MedioPago
            <ValorEnum("GEPR_TPAIS")>
            Pais
            <ValorEnum("GEPR_TPLANTA")>
            Planta
            <ValorEnum("GEPR_TPUNTO_SERVICIO")>
            PuntoServicio
            <ValorEnum("GEPR_TPUNTO_SERVICIO")>
            PuntoServicioPorCodigo
            <ValorEnum("GEPR_TPUNTO_SERVICIO")>
            PuntoServicioMaquina
            <ValorEnum("GEPR_TSECTOR")>
            Sector
            <ValorEnum("GEPR_TSUBCANAL")>
            SubCanal
            <ValorEnum("GEPR_TSUBCLIENTE")>
            SubCliente
            <ValorEnum("GEPR_TTERMINO")>
            Termino
            <ValorEnum("SAPR_TTIPO_BULTO")>
            TipoBulto
            <ValorEnum("GEPR_TTIPO_SECTOR")>
            TipoSector
            <ValorEnum("SAPR_TTIPO_CONTENEDOR")>
            TipoContenedor
            <ValorEnum("GEPR_TUNIDAD_MEDIDA")>
            UnidadMedida
            <ValorEnum("GEPR_TTIPO_SECTORXFORMULARIO")>
            SectorXFormulario
            <ValorEnum("GEPR_TPUESTO")>
           Puesto
            <ValorEnum("GEPR_TAPLICACION")>
           Aplicacion
            <ValorEnum("GEPR_TPARAMETRO")>
           Parametro
            <ValorEnum("GEPR_TPARAMETRO_VALOR")>
           ParametroValor
            <ValorEnum("GEPR_TCARACTTIPOSECTORXTIPOSEC")>
            CaracteristicaTipoSectorTipoSector
            <ValorEnum("GEPR_TCARACT_TIPOSECTOR")>
            CaracteristicaTipoSector
            <ValorEnum("GEPR_TTIPO_CLIENTE")>
            TipoCliente
            <ValorEnum("ADPR_TUSUARIO")>
            Usuario
            <ValorEnum("SAPR_TPLANIFICACION")>
            Planificacion
            <ValorEnum("SAPR_TTIPO_PLANIFICACION")>
            TipoPlanificacion
            <ValorEnum("GEPR_TSECTOR")>
            MaquinaSector
            <ValorEnum("SAPR_TMAQUINA")>
            MaquinaPunto
            <ValorEnum("GEPR_TCANAL")>
            CanalPorCodigo
            <ValorEnum("GEPR_TSUBCANAL")>
            SubCanalPorCodigo
            <ValorEnum("SAPR_TDATO_BANCARIO_CAMBIO")>
            DatoBancarioCambio
            <ValorEnum("SAPR_TDATO_BANCARIO_APROBACION")>
            DatoBancarioAprobacion
            <ValorEnum("SAPR_TDATO_BANCARIO_COMENTARIO")>
            DatoBancarioComentario
        End Enum

    End Class

End Namespace