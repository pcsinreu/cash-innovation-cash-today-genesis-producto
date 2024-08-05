/* Procedure encargado de desbloquear periodos */
CREATE OR REPLACE PROCEDURE sdesbloquear_periodo (
                                                      par$oid_periodo         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                                      par$cod_pais            IN gepr_pcomon_###VERSION###.tipo$cod_,
                                                      par$cod_device_id       IN gepr_pcomon_###VERSION###.tipo$cod_, 
                                                      par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$usr_, 
                                                      par$codigo              OUT gepr_pcomon_###VERSION###.tipo$cod_, 
                                                      par$descripcion         OUT gepr_pcomon_###VERSION###.tipo$desc_)
IS
BEGIN    
    /*Version: ###VERSION_COMP###*/
    sapr_pperiodo_###VERSION###.sdesbloquear_periodo(par$oid_periodo,
        par$cod_pais,
        par$cod_device_id,
        par$cod_usuario,
        par$codigo,
        par$descripcion);
END sdesbloquear_periodo;
/