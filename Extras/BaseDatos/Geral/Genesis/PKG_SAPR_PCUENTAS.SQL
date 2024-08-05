CREATE OR REPLACE PACKAGE sapr_pcuentas_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'sapr_pcuentas_###VERSION###';

  /* Recuperar cuenta - pruebas */
  PROCEDURE sprobar_cuenta(par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$oid_formulario   IN gepr_pcomon_###VERSION###.tipo$oid_,

                           /* cuenta movimento */
                           par$mov_oid_cuenta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_oid_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_canal      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_canal      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_planta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_planta     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_sector     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_sector     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_cod_pais       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_cod_tipo       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,

                           /* cuenta saldos */
                           par$sal_oid_cuenta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_oid_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_canal      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_canal      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_planta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_planta     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_sector     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_sector     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_cod_pais       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_cod_tipo       IN OUT gepr_pcomon_###VERSION###.tipo$cod_);

  /* Recuperar cuenta */
  PROCEDURE srecuperar_cuenta(par$cuenta_mov        IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$cuenta_sal        IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$oid_formulario  IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects          OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar cuenta Saldos */
  /* Esta procedure es para organizar o codigo, favor utilizar a procedure srecuperar_cuenta */
  PROCEDURE srecuperar_cuenta_saldos(par$cuenta_mov        IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cuenta_sal        IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cod_usuario       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_cultura       IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$inserts          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$updates          OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$selects          OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* svalidar_nivel_saldo */
  PROCEDURE svalidar_nivel_saldo(par$cuenta_mov IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                 par$cuenta_sal IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                 par$selects    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validaciones - patron */
  PROCEDURE svalidar_cuenta(par$cuenta_mov   IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                            par$cuenta_sal   IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                            par$cod_usuario  IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura  IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validaciones - integraciones */
  PROCEDURE svalidar_integracion(par$oid_cliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_subcliente   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_puntoservico IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_canal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_subcanal     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_delegacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_planta       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_sector       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$selects         OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Validaciones - codigos Cliente/SubCliente/PtoServicio/Canal/SubCanal/Delegacion/Planta/Sector */
  PROCEDURE svalidar_codigos(par$cuenta      IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                             par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar detalle cuenta */
  PROCEDURE srecuperar_detalle_cuenta(par$cuenta  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                      par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recuperar identificador cuenta */
  PROCEDURE srecuperar_cuenta_por_oids(par$cuenta  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                       par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Recupera os totalizadores de saldo */
  PROCEDURE srecuperar_totalizadores_saldo(par$cuenta_mov     IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$cuenta_sal     IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$totalizadores OUT gepr_pcomon_###VERSION###.tipo$totalizadores,
                                           par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                           par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                           par$cod_usuario   IN gepr_pcomon_###VERSION###.tipo$cod_);

  PROCEDURE srecuperar_tot_otros_niveis(par$cuenta_mov    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                        par$cuenta_sal    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                        par$cod_usuario   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$totalizadores OUT gepr_pcomon_###VERSION###.tipo$totalizadores,
                                        par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_);
  /* supd_tipo_cuenta */
  PROCEDURE supd_tipo_cuenta(par$oid_cuenta       IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_tipo_cuenta  IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario      IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$updates         OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_tcuenta */
  PROCEDURE sins_tcuenta(par$cuenta      IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                         par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
						 par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Crea la configuracion nivel saldo */
  PROCEDURE sins_tconfig_nivel_saldo(par$cuenta                IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cod_usuario           IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_config_nivel_sal OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Cria a configuracao nivel movimento */
  PROCEDURE screar_config_nivel_movimiento(par$cuenta               IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                           par$oid_config_nivel_sal IN gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$oid_subcanal         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$oid_config_nivel_mov OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* Cria o nivel saldo */
  PROCEDURE sins_tnivel_saldo(par$cuenta_mov           IN gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$cuenta_sal           IN gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$oid_config_nivel_Mov IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* fcargar_rc_cuentas */
  FUNCTION fcargar_rc_cuentas(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) RETURN SYS_REFCURSOR;

  /* scargar_tt_oids_cuentas_gpr */
  PROCEDURE scargar_tt_oids_cuentas_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_cuentas_docs */
  PROCEDURE scargar_tt_oids_cuentas_docs(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* scargar_tt_oids_cuentas_elem */
  PROCEDURE scargar_tt_oids_cuentas_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_);


END sapr_pcuentas_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY sapr_pcuentas_###VERSION### AS

  /* Recuperar cuenta - pruebas */
  PROCEDURE sprobar_cuenta(par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                           
                           /* cuenta movimento */
                           par$mov_oid_cuenta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_oid_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_canal      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_canal      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_planta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_planta     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_oid_sector     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$mov_cod_sector     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_cod_pais       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$mov_cod_tipo       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           
                           /* cuenta saldos */
                           par$sal_oid_cuenta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_oid_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_cliente    IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_subcliente IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_ptservicio IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_canal      IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_canal      IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_subcanal   IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_delegacion IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_planta     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_planta     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_oid_sector     IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                           par$sal_cod_sector     IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_cod_pais       IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                           par$sal_cod_tipo       IN OUT gepr_pcomon_###VERSION###.tipo$cod_) AS
  
    par$cuenta_mov gepr_pcomon_###VERSION###.tipo$cuenta;
    par$cuenta_sal gepr_pcomon_###VERSION###.tipo$cuenta;
  
    /* LOG */
    var$inserts gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  
  BEGIN
  
    par$cuenta_mov.oid_cuenta       := par$mov_oid_cuenta;
    par$cuenta_mov.oid_cliente      := par$mov_oid_cliente;
    par$cuenta_mov.cod_cliente      := par$mov_cod_cliente;
    par$cuenta_mov.oid_subcliente   := par$mov_oid_subcliente;
    par$cuenta_mov.cod_subcliente   := par$mov_cod_subcliente;
    par$cuenta_mov.oid_pto_servicio := par$mov_oid_ptservicio;
    par$cuenta_mov.cod_pto_servicio := par$mov_cod_ptservicio;
    par$cuenta_mov.oid_canal        := par$mov_oid_canal;
    par$cuenta_mov.cod_canal        := par$mov_cod_canal;
    par$cuenta_mov.oid_subcanal     := par$mov_oid_subcanal;
    par$cuenta_mov.cod_subcanal     := par$mov_cod_subcanal;
    par$cuenta_mov.oid_delegacion   := par$mov_oid_delegacion;
    par$cuenta_mov.cod_delegacion   := par$mov_cod_delegacion;
    par$cuenta_mov.oid_planta       := par$mov_oid_planta;
    par$cuenta_mov.cod_planta       := par$mov_cod_planta;
    par$cuenta_mov.oid_sector       := par$mov_oid_sector;
    par$cuenta_mov.cod_sector       := par$mov_cod_sector;
    par$cuenta_mov.cod_pais         := par$mov_cod_pais;
    par$cuenta_mov.cod_tipo_cuenta  := par$mov_cod_tipo;
  
    par$cuenta_sal.oid_cuenta       := par$sal_oid_cuenta;
    par$cuenta_sal.oid_cliente      := par$sal_oid_cliente;
    par$cuenta_sal.cod_cliente      := par$sal_cod_cliente;
    par$cuenta_sal.oid_subcliente   := par$sal_oid_subcliente;
    par$cuenta_sal.cod_subcliente   := par$sal_cod_subcliente;
    par$cuenta_sal.oid_pto_servicio := par$sal_oid_ptservicio;
    par$cuenta_sal.cod_pto_servicio := par$sal_cod_ptservicio;
    par$cuenta_sal.oid_canal        := par$sal_oid_canal;
    par$cuenta_sal.cod_canal        := par$sal_cod_canal;
    par$cuenta_sal.oid_subcanal     := par$sal_oid_subcanal;
    par$cuenta_sal.cod_subcanal     := par$sal_cod_subcanal;
    par$cuenta_sal.oid_delegacion   := par$sal_oid_delegacion;
    par$cuenta_sal.cod_delegacion   := par$sal_cod_delegacion;
    par$cuenta_sal.oid_planta       := par$sal_oid_planta;
    par$cuenta_sal.cod_planta       := par$sal_cod_planta;
    par$cuenta_sal.oid_sector       := par$sal_oid_sector;
    par$cuenta_sal.cod_sector       := par$sal_cod_sector;
    par$cuenta_sal.cod_pais         := par$sal_cod_pais;
    par$cuenta_sal.cod_tipo_cuenta  := par$sal_cod_tipo;
  
    srecuperar_cuenta(par$cuenta_mov     => par$cuenta_mov,
                      par$cuenta_sal     => par$cuenta_sal,
                      par$oid_formulario => par$oid_formulario,
                      par$cod_usuario    => par$cod_usuario,
                      par$cod_cultura    => par$cod_cultura,
                      par$inserts        => var$inserts,
                      par$updates        => var$updates,
                      par$selects        => var$selects);
  
    par$mov_oid_cuenta     := par$cuenta_mov.oid_cuenta;
    par$mov_oid_cliente    := par$cuenta_mov.oid_cliente;
    par$mov_cod_cliente    := par$cuenta_mov.cod_cliente;
    par$mov_oid_subcliente := par$cuenta_mov.oid_subcliente;
    par$mov_cod_subcliente := par$cuenta_mov.cod_subcliente;
    par$mov_oid_ptservicio := par$cuenta_mov.oid_pto_servicio;
    par$mov_cod_ptservicio := par$cuenta_mov.cod_pto_servicio;
    par$mov_oid_canal      := par$cuenta_mov.oid_canal;
    par$mov_cod_canal      := par$cuenta_mov.cod_canal;
    par$mov_oid_subcanal   := par$cuenta_mov.oid_subcanal;
    par$mov_cod_subcanal   := par$cuenta_mov.cod_subcanal;
    par$mov_oid_delegacion := par$cuenta_mov.oid_delegacion;
    par$mov_cod_delegacion := par$cuenta_mov.cod_delegacion;
    par$mov_oid_planta     := par$cuenta_mov.oid_planta;
    par$mov_cod_planta     := par$cuenta_mov.cod_planta;
    par$mov_oid_sector     := par$cuenta_mov.oid_sector;
    par$mov_cod_sector     := par$cuenta_mov.cod_sector;
    par$mov_cod_pais       := par$cuenta_mov.cod_pais;
    par$mov_cod_tipo       := par$cuenta_mov.cod_tipo_cuenta;
    par$sal_oid_cuenta     := par$cuenta_sal.oid_cuenta;
    par$sal_oid_cliente    := par$cuenta_sal.oid_cliente;
    par$sal_cod_cliente    := par$cuenta_sal.cod_cliente;
    par$sal_oid_subcliente := par$cuenta_sal.oid_subcliente;
    par$sal_cod_subcliente := par$cuenta_sal.cod_subcliente;
    par$sal_oid_ptservicio := par$cuenta_sal.oid_pto_servicio;
    par$sal_cod_ptservicio := par$cuenta_sal.cod_pto_servicio;
    par$sal_oid_canal      := par$cuenta_sal.oid_canal;
    par$sal_cod_canal      := par$cuenta_sal.cod_canal;
    par$sal_oid_subcanal   := par$cuenta_sal.oid_subcanal;
    par$sal_cod_subcanal   := par$cuenta_sal.cod_subcanal;
    par$sal_oid_delegacion := par$cuenta_sal.oid_delegacion;
    par$sal_cod_delegacion := par$cuenta_sal.cod_delegacion;
    par$sal_oid_planta     := par$cuenta_sal.oid_planta;
    par$sal_cod_planta     := par$cuenta_sal.cod_planta;
    par$sal_oid_sector     := par$cuenta_sal.oid_sector;
    par$sal_cod_sector     := par$cuenta_sal.cod_sector;
    par$sal_cod_pais       := par$cuenta_sal.cod_pais;
    par$sal_cod_tipo       := par$cuenta_sal.cod_tipo_cuenta;
  
  END sprobar_cuenta;

  /* Recuperar cuenta */
  PROCEDURE srecuperar_cuenta(par$cuenta_mov     IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$cuenta_sal     IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$oid_formulario IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_usuario    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$cod_cultura    IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates        OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects        OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_cuenta';
  
    /* LOG */
    var$inicio         TIMESTAMP;
    var$registros      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$info_ejecucion gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_ejecucion  gepr_pcomon_###VERSION###.tipo$nel_;
    var$err            VARCHAR2(4000);
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$selects := 0;
  
    /* Validar cuenta saldo - Delegacion y Planta */
    IF par$cuenta_sal.oid_cuenta IS NULL AND
       par$cuenta_sal.oid_delegacion IS NULL AND
       par$cuenta_sal.cod_delegacion IS NULL AND
       par$cuenta_sal.oid_planta IS NULL AND
       par$cuenta_sal.cod_planta IS NULL AND
       par$cuenta_sal.oid_canal IS NULL AND
       par$cuenta_sal.cod_canal IS NULL AND
       par$cuenta_sal.oid_subcanal IS NULL AND
       par$cuenta_sal.cod_subcanal IS NULL AND
       (par$cuenta_sal.oid_sector IS NOT NULL OR
       par$cuenta_sal.cod_sector IS NOT NULL) THEN
    
      IF par$cuenta_mov.oid_delegacion IS NOT NULL OR
         par$cuenta_mov.cod_delegacion IS NOT NULL OR
         par$cuenta_mov.oid_planta IS NOT NULL OR
         par$cuenta_mov.cod_planta IS NOT NULL OR
         par$cuenta_mov.oid_canal IS NOT NULL OR
         par$cuenta_mov.cod_canal IS NOT NULL OR
         par$cuenta_mov.oid_subcanal IS NOT NULL OR
         par$cuenta_mov.cod_subcanal IS NOT NULL THEN
      
        par$cuenta_sal.oid_delegacion := par$cuenta_mov.oid_delegacion;
        par$cuenta_sal.cod_delegacion := par$cuenta_mov.cod_delegacion;
        par$cuenta_sal.oid_planta     := par$cuenta_mov.oid_planta;
        par$cuenta_sal.cod_planta     := par$cuenta_mov.cod_planta;
        par$cuenta_sal.oid_canal      := par$cuenta_mov.oid_canal;
        par$cuenta_sal.cod_canal      := par$cuenta_mov.cod_canal;
        par$cuenta_sal.oid_subcanal   := par$cuenta_mov.oid_subcanal;
        par$cuenta_sal.cod_subcanal   := par$cuenta_mov.cod_subcanal;
      
      ELSIF par$cuenta_mov.oid_cuenta IS NOT NULL THEN
      
        BEGIN
          SELECT P.OID_DELEGACION,
                 P.OID_PLANTA,
                 SC.OID_CANAL,
                 SC.OID_SUBCANAL
            INTO par$cuenta_sal.oid_delegacion,
                 par$cuenta_sal.oid_planta,
                 par$cuenta_sal.oid_canal,
                 par$cuenta_sal.oid_subcanal
            FROM SAPR_TCUENTA C
           INNER JOIN GEPR_TSECTOR S
              ON S.OID_SECTOR = C.OID_SECTOR
           INNER JOIN GEPR_TPLANTA P
              ON P.OID_PLANTA = S.OID_PLANTA
           INNER JOIN GEPR_TSUBCANAL SC
              ON SC.OID_SUBCANAL = C.OID_SUBCANAL
           WHERE C.OID_CUENTA = par$cuenta_mov.oid_cuenta;
        EXCEPTION
          WHEN no_data_found THEN
            raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'msg_delegacion_invalida',
                                                                        gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        '',
                                                                        1));
        END;
      END IF;
    
    END IF;
  
    /* Validaciones patron */
    svalidar_cuenta(par$cuenta_mov  => par$cuenta_mov,
                    par$cuenta_sal  => par$cuenta_sal,
                    par$cod_usuario => par$cod_usuario,
                    par$cod_cultura => par$cod_cultura,
                    par$selects     => var$selects_int);
    par$selects   := par$selects + var$selects_int;
    var$registros := var$registros + var$selects_int;
  
    /* Recuperar informaciones de la cuenta movimento */
    IF par$cuenta_mov.oid_cuenta IS NOT NULL AND
       par$cuenta_mov.oid_cliente IS NULL THEN
    
      /* Si tengo el identificador de la cuenta movimento */
      srecuperar_detalle_cuenta(par$cuenta  => par$cuenta_mov,
                                par$selects => var$selects_int);
    
      par$selects := par$selects + var$selects_int;
    
    ELSIF par$cuenta_mov.oid_cliente IS NOT NULL THEN
    
      /* Si NO tengo el identificador de la cuenta, pero tengo las informaciones necesarias para obtener la cuenta o criar */
      srecuperar_cuenta_por_oids(par$cuenta  => par$cuenta_mov,
                                 par$selects => var$selects_int);
    
      par$selects := par$selects + var$selects_int;
    
      IF par$cuenta_mov.oid_cuenta IS NULL THEN
      
        par$cuenta_mov.cod_tipo_cuenta := gepr_pcomon_###VERSION###.const$codTipoCuentaMov;
      
        /* Crear cuenta */
        sins_tcuenta(par$cuenta      => par$cuenta_mov,
                     par$cod_usuario => par$cod_usuario,
					 par$cod_cultura => par$cod_cultura,
                     par$inserts     => var$inserts_int);
        par$inserts := par$inserts + var$inserts_int;
      
        SELECT D.Cod_Pais
          INTO par$cuenta_mov.cod_pais
          FROM GEPR_TDELEGACION D
         WHERE D.OID_DELEGACION = par$cuenta_mov.oid_delegacion;
        par$selects := par$selects + sql%rowcount;
      
      END IF;
    END IF;
  
    /* Si es gestion de elemento, es posible recibir la cuenta saldos */
    --IF TO DO: validar caracterisitcas do par$oid_formulario par$bol_gestion_elem > 0 THEN
  
    /* Recuperar informaciones de la cuenta saldos */
    IF par$cuenta_sal.oid_cuenta IS NOT NULL AND
       par$cuenta_sal.oid_cliente IS NULL THEN
    
      srecuperar_detalle_cuenta(par$cuenta  => par$cuenta_sal,
                                par$selects => var$selects_int);
    
      par$selects := par$selects + var$selects_int;
    
    ELSIF par$cuenta_sal.oid_cliente IS NOT NULL THEN
    
      srecuperar_cuenta_por_oids(par$cuenta  => par$cuenta_sal,
                                 par$selects => var$selects_int);
    
      par$selects := par$selects + var$selects_int;
    
    END IF;
  
    IF par$cuenta_sal.oid_cuenta IS NULL THEN
      srecuperar_cuenta_saldos(par$cuenta_mov  => par$cuenta_mov,
                               par$cuenta_sal  => par$cuenta_sal,
                               par$cod_usuario => par$cod_usuario,
                               par$cod_cultura => par$cod_cultura,
                               par$inserts     => var$inserts_int,
                               par$updates     => var$updates_int,
                               par$selects     => var$selects_int);
    END IF;
  /*
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           var$deletes_int,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           var$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           var$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true); */
    
  END srecuperar_cuenta;

  /* Recuperar cuenta Saldos */
  /* Esta procedure es para organizar o codigo, favor utilizar a procedure srecuperar_cuenta */
  PROCEDURE srecuperar_cuenta_saldos(par$cuenta_mov  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cuenta_sal  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$updates     OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                     par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_cuenta';
  
    var$totalizadores           gepr_pcomon_###VERSION###.tipo$totalizadores;
    var$bolPermitirCualquierTot gepr_pcomon_###VERSION###.tipo$nbol_;
    var$existeTotalizador       gepr_pcomon_###VERSION###.tipo$nbol_ := 0;
    var$oid_config_nivel_sal    gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_config_nivel_mov    gepr_pcomon_###VERSION###.tipo$oid_;
  
    /* LOG */
    var$inicio         TIMESTAMP;
    var$registros      gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges         gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects_int    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$info_ejecucion gepr_pcomon_###VERSION###.tipo$nel_;
    var$cod_ejecucion  gepr_pcomon_###VERSION###.tipo$nel_;
    var$err            VARCHAR2(4000);
  
  BEGIN
  
    var$inicio  := SYSTIMESTAMP;
    par$inserts := 0;
    par$updates := 0;
    par$selects := 0;
  
    /* 'Verificar si existe na tabela SAPR_TNIVEL_SALDO algum item onde a conta movimento
    seja a mesma (em mem ia) e o registro encontrado esteja marcado como  padrao  */
    svalidar_nivel_saldo(par$cuenta_mov => par$cuenta_mov,
                         par$cuenta_sal => par$cuenta_sal,
                         par$selects    => var$selects_int);
    par$selects := par$selects + var$selects_int;
  
    IF par$cuenta_sal.oid_cuenta IS NULL THEN
      /* No existe la cuenta saldos o no esta configurada */
    
      /* Recupera os totalizadores de saldo */
      /* Verificar se existe na tabela de  PR_TCONFIG_NIVEL_MOVIMIE lgum item com os
      mesmos dados da conta movimento (em mem ria) e com os dados da conta de saldo (em me ia)
      (considerar primeiro itens com subcanais e depois sem subcanais) */
      srecuperar_totalizadores_saldo(par$cuenta_mov    => par$cuenta_mov,
                                     par$cuenta_sal    => par$cuenta_sal,
                                     par$totalizadores => var$totalizadores,
                                     par$selects       => var$selects_int,
                                     par$inserts       => var$inserts_int,
                                     par$cod_usuario   => par$cod_usuario);
    
      par$selects := par$selects + var$selects_int;
      par$inserts := par$inserts + var$inserts_int;
    
      IF var$totalizadores IS NOT NULL THEN
        IF var$totalizadores.COUNT > 0 THEN
          var$existeTotalizador := 1;
        END IF;
      END IF;
    
      /* Recupera a conta saldo do totalizador defecto */
      IF var$existeTotalizador > 0 THEN
      
        par$cuenta_sal.oid_cuenta       := NULL;
        par$cuenta_sal.oid_cliente      := var$totalizadores(0).oid_cliente;
        par$cuenta_sal.cod_cliente      := NULL;
        par$cuenta_sal.oid_subcliente   := var$totalizadores(0)
                                           .oid_subcliente;
        par$cuenta_sal.cod_subcliente   := NULL;
        par$cuenta_sal.oid_pto_servicio := var$totalizadores(0)
                                           .oid_pto_servicio;
        par$cuenta_sal.cod_pto_servicio := NULL;
        par$cuenta_sal.oid_canal        := par$cuenta_mov.oid_canal;
        par$cuenta_sal.cod_canal        := NULL;
        par$cuenta_sal.oid_subcanal     := par$cuenta_mov.oid_subcanal;
        par$cuenta_sal.cod_subcanal     := NULL;
        par$cuenta_sal.oid_delegacion   := par$cuenta_mov.oid_delegacion;
        par$cuenta_sal.cod_delegacion   := NULL;
        par$cuenta_sal.oid_planta       := par$cuenta_mov.oid_planta;
        par$cuenta_sal.cod_planta       := NULL;
        par$cuenta_sal.oid_sector       := par$cuenta_mov.oid_sector;
        par$cuenta_sal.cod_sector       := NULL;
        par$cuenta_sal.cod_pais         := NULL;
        par$cuenta_sal.cod_tipo_cuenta  := gepr_pcomon_###VERSION###.const$codTipoCuentaSaldo;
      
        srecuperar_cuenta_por_oids(par$cuenta  => par$cuenta_sal,
                                   par$selects => var$selects_int);
        par$selects := par$selects + var$selects_int;
      
        IF par$cuenta_sal.oid_cuenta IS NULL THEN
        
          par$cuenta_sal.oid_cuenta       := NULL;
          par$cuenta_sal.oid_cliente      := var$totalizadores(0)
                                             .oid_cliente;
          par$cuenta_sal.cod_cliente      := NULL;
          par$cuenta_sal.oid_subcliente   := var$totalizadores(0)
                                             .oid_subcliente;
          par$cuenta_sal.cod_subcliente   := NULL;
          par$cuenta_sal.oid_pto_servicio := var$totalizadores(0)
                                             .oid_pto_servicio;
          par$cuenta_sal.cod_pto_servicio := NULL;
          par$cuenta_sal.oid_canal        := par$cuenta_mov.oid_canal;
          par$cuenta_sal.cod_canal        := NULL;
          par$cuenta_sal.oid_subcanal     := par$cuenta_mov.oid_subcanal;
          par$cuenta_sal.cod_subcanal     := NULL;
          par$cuenta_sal.oid_delegacion   := par$cuenta_mov.oid_delegacion;
          par$cuenta_sal.cod_delegacion   := NULL;
          par$cuenta_sal.oid_planta       := par$cuenta_mov.oid_planta;
          par$cuenta_sal.cod_planta       := NULL;
          par$cuenta_sal.oid_sector       := par$cuenta_mov.oid_sector;
          par$cuenta_sal.cod_sector       := NULL;
          par$cuenta_sal.cod_pais         := NULL;
          par$cuenta_sal.cod_tipo_cuenta  := gepr_pcomon_###VERSION###.const$codTipoCuentaSaldo;
        
          /* crear a SAPR_TCUENTA com os valores da minha cuenta saldo (S) */
          sins_tcuenta(par$cuenta      => par$cuenta_sal,
                       par$cod_usuario => par$cod_usuario,
					   par$cod_cultura => par$cod_cultura,
                       par$inserts     => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
        END IF;
      
        var$oid_config_nivel_mov := var$totalizadores(0)
                                    .OID_CONFIG_NIVEL_MOVIMIENTO;
      
      ELSE
        /* Si NO fue encontrado un totalizador debe verificar el parametro PermitirCualquierTotalizadorSaldoServicio  */
        /* Recupera o parametro PermitirCualquierTotalizadorSaldoServicio */
        var$bolPermitirCualquierTot := gepr_putilidades_###VERSION###.frecuperar_parametro(par$cod_delegacion => par$cuenta_mov.cod_delegacion,
                                                                                      par$cod_pais       => par$cuenta_mov.cod_pais,
                                                                                      par$cod_parametro  => gepr_pcomon_###VERSION###.const$CodParamIACCualquierTot,
                                                                                      par$cod_aplicacion => gepr_pcomon_###VERSION###.const$codAplicacionSaldos);
      
        IF par$cuenta_sal.oid_cuenta IS NULL THEN
          IF (par$cuenta_sal.oid_cliente IS NOT NULL OR par$cuenta_sal.cod_cliente IS NOT NULL) THEN
            srecuperar_cuenta_por_oids(par$cuenta  => par$cuenta_sal,
                                       par$selects => var$selects_int);
            par$selects := par$selects + var$selects_int;
            
            IF par$cuenta_sal.oid_cuenta IS NULL THEN
              
                /* crear a SAPR_TCUENTA com os valores da minha cuenta saldo (S) */
                par$cuenta_sal.cod_tipo_cuenta  := gepr_pcomon_###VERSION###.const$codTipoCuentaSaldo;
                sins_tcuenta(par$cuenta      => par$cuenta_sal,
                             par$cod_usuario => par$cod_usuario,
							 par$cod_cultura => par$cod_cultura,
                             par$inserts     => var$inserts_int);
                par$inserts := par$inserts + var$inserts_int;
                            
            END IF;
          ELSE
             par$cuenta_sal := par$cuenta_mov;
          END IF;
        END IF;
      
        IF var$bolPermitirCualquierTot > 0 THEN
        
          /* Cria a configuracao nivel saldo */
          sins_tconfig_nivel_saldo(par$cuenta               => par$cuenta_sal,
                                   par$cod_usuario          => par$cod_usuario,
                                   par$oid_config_nivel_sal => var$oid_config_nivel_sal,
                                   par$inserts              => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
          par$updates := var$updates_int + var$updates_int;
        
          /* Cria a configuracao nivel movimento */
          screar_config_nivel_movimiento(par$cuenta               => par$cuenta_sal,
                                         par$cod_usuario          => par$cod_usuario,
                                         par$oid_config_nivel_sal => var$oid_config_nivel_sal,
                                         par$oid_subcanal         => null,
                                         par$oid_config_nivel_mov => var$oid_config_nivel_mov,
                                         par$inserts              => var$inserts_int);
          par$inserts := par$inserts + var$inserts_int;
        
        ELSE
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'MsgPermitirCualquierTot',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      NULL,
                                                                      1));
        END IF;
      END IF;
    
      sins_tnivel_saldo(par$cuenta_mov           => par$cuenta_mov,
                        par$cuenta_sal           => par$cuenta_sal,
                        par$oid_config_nivel_Mov => var$oid_config_nivel_mov,
                        par$cod_usuario          => par$cod_usuario,
                        par$inserts              => var$inserts_int,
                        par$selects              => var$selects_int,
                        par$updates              => var$updates_int);
      par$inserts := par$inserts + var$inserts_int;
      par$selects := par$selects + var$selects_int;
      par$updates := par$updates + var$updates_int;
    
    END IF;
  
    IF par$cuenta_sal.oid_cuenta IS NOT NULL THEN
      /* recuperar informaciones de la cuenta saldos */
      srecuperar_detalle_cuenta(par$cuenta  => par$cuenta_sal,
                                par$selects => var$selects_int);
      par$selects := par$selects + var$selects_int;
    END IF;
 /* 
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                           const$nombre_func,
                                                           par$inserts,
                                                           par$updates,
                                                           var$deletes_int,
                                                           var$merges,
                                                           par$selects,
                                                           var$registros,
                                                           var$inicio,
                                                           null,
                                                           'R',
                                                           var$info_ejecucion,
                                                           var$err,
                                                           par$cod_usuario,
                                                           var$cod_ejecucion);
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              'Error: ' || const$codFuncionalidad || '.' ||
                              const$nombre_func,
                              true);
    */
  END srecuperar_cuenta_saldos;

  /* svalidar_nivel_saldo */
  PROCEDURE svalidar_nivel_saldo(par$cuenta_mov IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                 par$cuenta_sal IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                 par$selects    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$count gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN
  
    /* Si la procedure recibir el identificador de la cuenta saldos, debe ser utilizada */
  
    par$selects := 0;
  
    /* INICIO: XGH PARA CORRIGIR ERROS ANTIGOS */
    /*         NAO POSSIVEL TER DOIS ITENS IGUAIS NA TABELA SAPR_TNIVEL_SALDO */
    BEGIN
      IF par$cuenta_sal.oid_cuenta IS NULL THEN
        SELECT COUNT(1) AS CUANT
          INTO var$count
          FROM SAPR_TNIVEL_SALDO NS
         INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
            ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
               NS.OID_CONFIG_NIVEL_MOVIMIENTO
         WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta
         GROUP BY NS.OID_CUENTA_MOVIMIENTO;
        par$selects := par$selects + sql%rowcount;
      ELSE
        SELECT COUNT(1) AS CUANT
          INTO var$count
          FROM SAPR_TNIVEL_SALDO NS
         INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
            ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
               NS.OID_CONFIG_NIVEL_MOVIMIENTO
         WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta
           AND NS.OID_CUENTA_SALDO = par$cuenta_sal.oid_cuenta
         GROUP BY NS.OID_CUENTA_MOVIMIENTO;
        par$selects := par$selects + sql%rowcount;
      END IF;
    
    EXCEPTION
      WHEN no_data_found THEN
        var$count := 0;
    END;
  
    IF var$count > 1 THEN
      IF par$cuenta_sal.oid_cuenta IS NULL THEN
        DELETE SAPR_TNIVEL_SALDO NS1
         WHERE NS1.OID_NIVEL_SALDO =
               (SELECT NS.OID_NIVEL_SALDO
                  FROM SAPR_TNIVEL_SALDO NS
                 INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                    ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
                       NS.OID_CONFIG_NIVEL_MOVIMIENTO
                 WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta
                   AND rownum = 1);
      ELSE
        DELETE SAPR_TNIVEL_SALDO NS1
         WHERE NS1.OID_NIVEL_SALDO =
               (SELECT NS.OID_NIVEL_SALDO
                  FROM SAPR_TNIVEL_SALDO NS
                 INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                    ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
                       NS.OID_CONFIG_NIVEL_MOVIMIENTO
                 WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta
                   AND NS.OID_CUENTA_SALDO = par$cuenta_sal.oid_cuenta
                   AND rownum = 1);
      END IF;
    
    END IF;
  
    /* FIM: XGH */
    BEGIN
    
      IF par$cuenta_sal.oid_cuenta IS NULL THEN
        SELECT NS.OID_CUENTA_SALDO
          INTO par$cuenta_sal.oid_cuenta
          FROM SAPR_TNIVEL_SALDO NS
         INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
            ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
               NS.OID_CONFIG_NIVEL_MOVIMIENTO			   
         INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
            ON CNS.OID_CONFIG_NIVEL_SALDO = CNM.OID_CONFIG_NIVEL_SALDO
         WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta 
			AND CNM.BOL_DEFECTO = 1
         -- VALIDA SE POSSUI par$cuenta_sal CLIENTE/SUBCLIENTE/PTO_SERVICIO PREENCHIDO 
         -- E SE A CONTA SALDO RECUPERADA É A QUE FOI PEDIDA
         AND (
               (par$cuenta_sal.oid_cliente IS NOT NULL AND CNS.OID_CLIENTE = par$cuenta_sal.oid_cliente
               AND NVL(CNS.oid_subcliente, '###') =
                   NVL(par$cuenta_sal.oid_subcliente, '###')
               AND NVL(CNS.oid_pto_servicio, '###') =
                   NVL(par$cuenta_sal.oid_pto_servicio, '###'))
                   
               OR par$cuenta_sal.oid_cliente IS NULL              
              )
           AND rownum = 1;
        par$selects := par$selects + sql%rowcount;
      ELSE
        SELECT NS.OID_CUENTA_SALDO
          INTO par$cuenta_sal.oid_cuenta
          FROM SAPR_TNIVEL_SALDO NS
         INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
            ON CNM.OID_CONFIG_NIVEL_MOVIMIENTO =
               NS.OID_CONFIG_NIVEL_MOVIMIENTO
         WHERE NS.OID_CUENTA_MOVIMIENTO = par$cuenta_mov.oid_cuenta
           AND NS.OID_CUENTA_SALDO = par$cuenta_sal.oid_cuenta
           AND rownum = 1;
        par$selects := par$selects + sql%rowcount;
      END IF;
    EXCEPTION
      WHEN no_data_found THEN
        par$cuenta_sal.oid_cuenta := NULL;
    END;
  
  END svalidar_nivel_saldo;

  /* Validaciones - patron */
  PROCEDURE svalidar_cuenta(par$cuenta_mov  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                            par$cuenta_sal  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                            par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  
    /*    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidacion_patron';*/
  
    var$selects_int gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  
  BEGIN
  
    par$selects := 0;
  
    /* Usuario */
    IF par$cod_usuario IS NULL THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$usuario',
                                                                  1));
      RETURN;
    END IF;
  
    /* Cuenta movimento */
    svalidar_codigos(par$cuenta      => par$cuenta_mov,
                     par$cod_cultura => par$cod_cultura,
                     par$selects     => var$selects_int);
    par$selects := par$selects + var$selects_int;
  
    IF par$cuenta_mov.oid_cuenta IS NULL AND
       par$cuenta_mov.oid_cliente IS NULL AND
       par$cuenta_mov.oid_subcanal IS NULL AND
       par$cuenta_mov.oid_sector IS NULL THEN
    
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'MsgAtributoObrigatorio',
                                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  'par$oid_cuenta_mov',
                                                                  1));
    
    ELSIF par$cuenta_mov.oid_cuenta IS NULL THEN
    
      svalidar_integracion(par$oid_cliente      => par$cuenta_mov.oid_cliente,
                           par$oid_subcliente   => par$cuenta_mov.oid_subcliente,
                           par$oid_puntoservico => par$cuenta_mov.oid_pto_servicio,
                           par$oid_canal        => par$cuenta_mov.oid_canal,
                           par$oid_subcanal     => par$cuenta_mov.oid_subcanal,
                           par$oid_delegacion   => par$cuenta_mov.oid_delegacion,
                           par$oid_planta       => par$cuenta_mov.oid_planta,
                           par$oid_sector       => par$cuenta_mov.oid_sector,
                           par$cod_cultura      => par$cod_cultura,
                           par$selects          => var$selects_int);
      par$selects := par$selects + var$selects_int;
    
    END IF;
  
    /* Cuenta saldos */
    svalidar_codigos(par$cuenta      => par$cuenta_sal,
                     par$cod_cultura => par$cod_cultura,
                     par$selects     => var$selects_int);
    par$selects := par$selects + var$selects_int;
  
    IF par$cuenta_sal.oid_cliente IS NOT NULL THEN
    
      svalidar_integracion(par$oid_cliente      => par$cuenta_sal.oid_cliente,
                           par$oid_subcliente   => par$cuenta_sal.oid_subcliente,
                           par$oid_puntoservico => par$cuenta_sal.oid_pto_servicio,
                           par$oid_canal        => par$cuenta_sal.oid_canal,
                           par$oid_subcanal     => par$cuenta_sal.oid_subcanal,
                           par$oid_delegacion   => par$cuenta_sal.oid_delegacion,
                           par$oid_planta       => par$cuenta_sal.oid_planta,
                           par$oid_sector       => par$cuenta_sal.oid_sector,
                           par$cod_cultura      => par$cod_cultura,
                           par$selects          => var$selects_int);
      par$selects := par$selects + var$selects_int;
    
    END IF;
  
  END svalidar_cuenta;

  /* Validaciones - integraciones */
  PROCEDURE svalidar_integracion(par$oid_cliente      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_subcliente   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_puntoservico IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_canal        IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_subcanal     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_delegacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_planta       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$oid_sector       IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$cod_cultura      IN gepr_pcomon_###VERSION###.tipo$cod_,
                                 par$selects          OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  
    /*    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_integracion';*/
  
    var$cant gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  
  BEGIN
  
    par$selects := 0;
  
    /* Integridad de cliente/subcliente/puntoservicio */
    var$cant := 0;
    IF par$oid_subcliente IS NOT NULL AND par$oid_puntoservico IS NOT NULL THEN
    
      SELECT COUNT(1)
        INTO var$cant
        FROM gepr_tcliente clie
        LEFT OUTER JOIN gepr_tsubcliente sucl
          ON clie.oid_cliente = sucl.oid_cliente
        LEFT OUTER JOIN gepr_tpunto_servicio puse
          ON sucl.oid_subcliente = puse.oid_subcliente
       WHERE clie.oid_cliente = par$oid_cliente
         AND NVL(sucl.oid_subcliente, '###') =
             NVL(par$oid_subcliente, '###')
         AND NVL(puse.oid_pto_servicio, '###') =
             NVL(par$oid_puntoservico, '###')
         AND clie.bol_vigente = 1;
      par$selects := par$selects + sql%rowcount;
    
    ELSIF par$oid_subcliente IS NOT NULL THEN
    
      SELECT COUNT(1)
        INTO var$cant
        FROM gepr_tcliente clie
        LEFT OUTER JOIN gepr_tsubcliente sucl
          ON clie.oid_cliente = sucl.oid_cliente
       WHERE clie.oid_cliente = par$oid_cliente
         AND sucl.oid_subcliente = par$oid_subcliente
         AND clie.bol_vigente = 1;
      par$selects := par$selects + sql%rowcount;
    
    ELSE
    
      SELECT COUNT(1)
        INTO var$cant
        FROM gepr_tcliente clie
       WHERE clie.oid_cliente = par$oid_cliente
         AND clie.bol_vigente = 1;
    END IF;
    par$selects := par$selects + sql%rowcount;
  
    IF NOT gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant) THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_relacion_cliente_subcliente_puntoservicio_invalida',
                                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  '',
                                                                  1));
      RETURN;
    END IF;
  
    /* Integridad de delegacion/planta/sector */
    var$cant := 0;
    SELECT COUNT(1)
      INTO var$cant
      FROM gepr_tdelegacion d
      LEFT OUTER JOIN gepr_tplanta p
        ON d.oid_delegacion = p.oid_delegacion
      LEFT OUTER JOIN gepr_tsector s
        ON s.oid_planta = p.oid_planta
     WHERE d.oid_delegacion = par$oid_delegacion
       AND p.oid_planta = par$oid_planta
       AND s.oid_sector = par$oid_sector
       AND s.bol_activo = 1;
    par$selects := par$selects + sql%rowcount;
  
    IF NOT gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant) THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_relacion_delegacion_planta_sector_invalida',
                                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  '',
                                                                  1));
      RETURN;
    END IF;
  
    /* Integridad de canal/subcanal */
    var$cant := 0;
    SELECT COUNT(1)
      INTO var$cant
      FROM gepr_tcanal c
      LEFT OUTER JOIN gepr_tsubcanal sc
        ON sc.oid_canal = c.oid_canal
     WHERE c.oid_canal = par$oid_canal
       AND sc.oid_subcanal = par$oid_subcanal
       AND sc.bol_vigente = 1;
    par$selects := par$selects + sql%rowcount;
  
    IF NOT gepr_putilidades_###VERSION###.fmayor_que_cero(var$cant) THEN
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                              gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                  'msg_relacion_canal_subcanal_invalida',
                                                                  gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                  gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                  '',
                                                                  1));
      RETURN;
    END IF;
  
  END svalidar_integracion;

  /* Validaciones - codigos Cliente/SubCliente/PtoServicio/Canal/SubCanal/Delegacion/Planta/Sector */
  PROCEDURE svalidar_codigos(par$cuenta      IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                             par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$selects     OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  
    /*    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'svalidar_codigos';*/
  
  BEGIN
  
    par$selects := 0;
  
    IF par$cuenta.oid_cliente IS NULL AND par$cuenta.cod_cliente IS NULL AND
       par$cuenta.oid_cuenta IS NOT NULL THEN
    
      BEGIN
        SELECT C.OID_CLIENTE,
               C.OID_SUBCLIENTE,
               C.OID_PTO_SERVICIO,
               SC.OID_CANAL,
               C.OID_SUBCANAL,
               P.OID_DELEGACION,
               P.OID_PLANTA,
               C.OID_SECTOR
          INTO par$cuenta.oid_cliente,
               par$cuenta.oid_subcliente,
               par$cuenta.oid_pto_servicio,
               par$cuenta.oid_canal,
               par$cuenta.oid_subcanal,
               par$cuenta.oid_delegacion,
               par$cuenta.oid_planta,
               par$cuenta.oid_sector
          FROM SAPR_TCUENTA C
         INNER JOIN GEPR_TSUBCANAL SC
            ON SC.OID_SUBCANAL = C.OID_SUBCANAL
         INNER JOIN GEPR_TSECTOR S
            ON S.OID_SECTOR = C.OID_SECTOR
         INNER JOIN GEPR_TPLANTA P
            ON P.OID_PLANTA = S.OID_PLANTA
         WHERE C.OID_CUENTA = par$cuenta.oid_cuenta
           AND rownum = 1;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_cliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    
    END IF;
  
    /* Identificador cliente */
    IF par$cuenta.oid_cliente IS NULL AND
       par$cuenta.cod_cliente IS NOT NULL THEN
      BEGIN
        SELECT C.OID_CLIENTE
          INTO par$cuenta.oid_cliente
          FROM GEPR_TCLIENTE C
         WHERE C.COD_CLIENTE = par$cuenta.cod_cliente;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_cliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_cliente IS NOT NULL AND
          par$cuenta.cod_cliente IS NULL THEN
      BEGIN
        SELECT C.COD_CLIENTE
          INTO par$cuenta.cod_cliente
          FROM GEPR_TCLIENTE C
         WHERE C.OID_CLIENTE = par$cuenta.oid_cliente;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_cliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador subcliente */
    IF par$cuenta.oid_subcliente IS NULL AND
       par$cuenta.cod_subcliente IS NOT NULL THEN
      BEGIN
        SELECT SC.OID_SUBCLIENTE
          INTO par$cuenta.oid_subcliente
          FROM GEPR_TSUBCLIENTE SC
         INNER JOIN GEPR_TCLIENTE C
            ON C.OID_CLIENTE = SC.OID_CLIENTE
         WHERE C.COD_CLIENTE = par$cuenta.cod_cliente
           AND SC.COD_SUBCLIENTE = par$cuenta.cod_subcliente;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_subcliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_subcliente IS NOT NULL AND
          par$cuenta.cod_subcliente IS NULL THEN
      BEGIN
        SELECT SC.COD_SUBCLIENTE
          INTO par$cuenta.cod_subcliente
          FROM GEPR_TSUBCLIENTE SC
         INNER JOIN GEPR_TCLIENTE C
            ON C.OID_CLIENTE = SC.OID_CLIENTE
         WHERE C.OID_CLIENTE = par$cuenta.oid_cliente
           AND SC.OID_SUBCLIENTE = par$cuenta.oid_subcliente;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_subcliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador punto servicio */
    IF par$cuenta.oid_pto_servicio IS NULL AND
       par$cuenta.cod_pto_servicio IS NOT NULL THEN
      BEGIN
        SELECT PS.OID_PTO_SERVICIO
          INTO par$cuenta.oid_pto_servicio
          FROM GEPR_TPUNTO_SERVICIO PS
         INNER JOIN GEPR_TSUBCLIENTE SC
            ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
         INNER JOIN GEPR_TCLIENTE C
            ON C.OID_CLIENTE = SC.OID_CLIENTE
         WHERE C.COD_CLIENTE = par$cuenta.cod_cliente
           AND SC.COD_SUBCLIENTE = par$cuenta.cod_subcliente
           AND PS.COD_PTO_SERVICIO = par$cuenta.cod_pto_servicio;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_punto_servicio_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_pto_servicio IS NOT NULL AND
          par$cuenta.cod_pto_servicio IS NULL THEN
      BEGIN
        SELECT PS.COD_PTO_SERVICIO
          INTO par$cuenta.cod_pto_servicio
          FROM GEPR_TPUNTO_SERVICIO PS
         INNER JOIN GEPR_TSUBCLIENTE SC
            ON SC.OID_SUBCLIENTE = PS.OID_SUBCLIENTE
         INNER JOIN GEPR_TCLIENTE C
            ON C.OID_CLIENTE = SC.OID_CLIENTE
         WHERE C.OID_CLIENTE = par$cuenta.oid_cliente
           AND SC.OID_SUBCLIENTE = par$cuenta.oid_subcliente
           AND PS.OID_PTO_SERVICIO = par$cuenta.oid_pto_servicio;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_punto_servicio_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador canal */
    IF par$cuenta.oid_canal IS NULL AND par$cuenta.cod_canal IS NOT NULL THEN
      BEGIN
        SELECT C.OID_CANAL
          INTO par$cuenta.oid_canal
          FROM GEPR_TCANAL C
         WHERE C.COD_CANAL = par$cuenta.cod_canal;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_canal_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_canal IS NOT NULL AND par$cuenta.cod_canal IS NULL THEN
      BEGIN
        SELECT C.COD_CANAL
          INTO par$cuenta.cod_canal
          FROM GEPR_TCANAL C
         WHERE C.OID_CANAL = par$cuenta.oid_canal;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_canal_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador subcanal */
    IF par$cuenta.oid_subcanal IS NULL AND
       par$cuenta.cod_subcanal IS NOT NULL THEN
      BEGIN
        SELECT SC.OID_SUBCANAL
          INTO par$cuenta.oid_subcanal
          FROM GEPR_TSUBCANAL SC
         INNER JOIN GEPR_TCANAL C
            ON C.OID_CANAL = SC.OID_CANAL
         WHERE C.COD_CANAL = par$cuenta.cod_canal
           AND SC.COD_SUBCANAL = par$cuenta.cod_subcanal;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_subcliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_subcanal IS NOT NULL AND
          par$cuenta.cod_subcanal IS NULL THEN
      BEGIN
        SELECT SC.COD_SUBCANAL
          INTO par$cuenta.cod_subcanal
          FROM GEPR_TSUBCANAL SC
         INNER JOIN GEPR_TCANAL C
            ON C.OID_CANAL = SC.OID_CANAL
         WHERE C.OID_CANAL = par$cuenta.oid_canal
           AND SC.OID_SUBCANAL = par$cuenta.oid_subcanal;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_subcliente_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador delegacion */
    IF par$cuenta.oid_delegacion IS NULL AND
       par$cuenta.cod_delegacion IS NOT NULL THEN
      BEGIN
        SELECT D.OID_DELEGACION
          INTO par$cuenta.oid_delegacion
          FROM GEPR_TDELEGACION D
         WHERE D.COD_DELEGACION = par$cuenta.cod_delegacion;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_delegacion_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_delegacion IS NOT NULL AND
          par$cuenta.cod_delegacion IS NULL THEN
      BEGIN
        SELECT D.COD_DELEGACION
          INTO par$cuenta.cod_delegacion
          FROM GEPR_TDELEGACION D
         WHERE D.OID_DELEGACION = par$cuenta.oid_delegacion;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_delegacion_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador planta */
    IF par$cuenta.oid_planta IS NULL AND par$cuenta.cod_planta IS NOT NULL THEN
      BEGIN
        SELECT P.OID_PLANTA
          INTO par$cuenta.oid_planta
          FROM GEPR_TPLANTA P
         INNER JOIN GEPR_TDELEGACION D
            ON D.OID_DELEGACION = P.OID_DELEGACION
         WHERE D.COD_DELEGACION = par$cuenta.cod_delegacion
           AND P.COD_PLANTA = par$cuenta.cod_planta;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_planta_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_planta IS NOT NULL AND
          par$cuenta.cod_planta IS NULL THEN
      BEGIN
        SELECT P.COD_PLANTA
          INTO par$cuenta.cod_planta
          FROM GEPR_TPLANTA P
         INNER JOIN GEPR_TDELEGACION D
            ON D.OID_DELEGACION = P.OID_DELEGACION
         WHERE D.OID_DELEGACION = par$cuenta.oid_delegacion
           AND P.OID_PLANTA = par$cuenta.oid_planta;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_planta_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
    /* Identificador sector */
    IF par$cuenta.oid_sector IS NULL AND par$cuenta.cod_sector IS NOT NULL THEN
      BEGIN
        SELECT S.OID_SECTOR
          INTO par$cuenta.oid_sector
          FROM GEPR_TSECTOR S
         INNER JOIN GEPR_TPLANTA P
            ON P.OID_PLANTA = S.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION D
            ON D.OID_DELEGACION = P.OID_DELEGACION
         WHERE D.COD_DELEGACION = par$cuenta.cod_delegacion
           AND P.COD_PLANTA = par$cuenta.cod_planta
           AND S.COD_SECTOR = par$cuenta.cod_sector;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_sector_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    ELSIF par$cuenta.oid_sector IS NOT NULL AND
          par$cuenta.cod_sector IS NULL THEN
      BEGIN
        SELECT S.COD_SECTOR
          INTO par$cuenta.cod_sector
          FROM GEPR_TSECTOR S
         INNER JOIN GEPR_TPLANTA P
            ON P.OID_PLANTA = S.OID_PLANTA
         INNER JOIN GEPR_TDELEGACION D
            ON D.OID_DELEGACION = P.OID_DELEGACION
         WHERE D.OID_DELEGACION = par$cuenta.oid_delegacion
           AND P.OID_PLANTA = par$cuenta.oid_planta
           AND S.OID_SECTOR = par$cuenta.oid_sector;
        par$selects := par$selects + sql%rowcount;
      EXCEPTION
        WHEN no_data_found THEN
          raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                  gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                      'msg_sector_invalida',
                                                                      gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                      gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                      '',
                                                                      1));
      END;
    END IF;
  
  END svalidar_codigos;

  /* Recuperar detalle cuenta */
  PROCEDURE srecuperar_detalle_cuenta(par$cuenta  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                      par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  BEGIN
  
    BEGIN
      SELECT cu.oid_cliente,
             cl.cod_cliente,
             cl.des_cliente,
             cu.oid_subcliente,
             scl.cod_subcliente,
             scl.des_subcliente,
             cu.oid_pto_servicio,
             pts.cod_pto_servicio,
             pts.des_pto_servicio,
             ca.oid_canal,
             ca.cod_canal,
             sc.oid_subcanal,
             sc.cod_subcanal,
             se.oid_sector,
             se.cod_sector,
             pl.oid_planta,
             pl.cod_planta,
             dl.oid_delegacion,
             dl.cod_delegacion,
             dl.cod_pais,
             cu.cod_tipo_cuenta
        INTO par$cuenta.oid_cliente,
             par$cuenta.cod_cliente,
             par$cuenta.des_cliente,
             par$cuenta.oid_subcliente,
             par$cuenta.cod_subcliente,
             par$cuenta.des_subcliente,
             par$cuenta.oid_pto_servicio,
             par$cuenta.cod_pto_servicio,
             par$cuenta.des_pto_servicio,
             par$cuenta.oid_canal,
             par$cuenta.cod_canal,
             par$cuenta.oid_subcanal,
             par$cuenta.cod_subcanal,
             par$cuenta.oid_sector,
             par$cuenta.cod_sector,
             par$cuenta.oid_planta,
             par$cuenta.cod_planta,
             par$cuenta.oid_delegacion,
             par$cuenta.cod_delegacion,
             par$cuenta.cod_pais,
             par$cuenta.cod_tipo_cuenta
        FROM sapr_tcuenta cu
       INNER JOIN gepr_tsector se
          ON se.oid_sector = cu.oid_sector
       INNER JOIN gepr_tplanta pl
          ON pl.oid_planta = se.oid_planta
       INNER JOIN gepr_tdelegacion dl
          ON dl.oid_delegacion = pl.oid_delegacion
       INNER JOIN gepr_tsubcanal sc
          ON sc.oid_subcanal = cu.oid_subcanal
       INNER JOIN gepr_tcanal ca
          ON ca.oid_canal = sc.oid_canal
       INNER JOIN gepr_tcliente cl
          ON cl.oid_cliente = cu.oid_cliente
        LEFT JOIN gepr_tsubcliente scl
          ON scl.oid_subcliente = cu.oid_subcliente
        LEFT JOIN gepr_tpunto_servicio pts
          ON pts.oid_pto_servicio = cu.oid_pto_servicio
       WHERE cu.oid_cuenta = par$cuenta.oid_cuenta;
    EXCEPTION
      WHEN no_data_found THEN
        par$cuenta.oid_cliente := NULL;
    END;
  
    par$selects := sql%rowcount;
  
  END srecuperar_detalle_cuenta;

  /* Recuperar identificador cuenta */
  PROCEDURE srecuperar_cuenta_por_oids(par$cuenta  IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                                       par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  BEGIN
  
    BEGIN
      SELECT cu.oid_cuenta, dl.cod_pais, cu.cod_tipo_cuenta
        INTO par$cuenta.oid_cuenta,
             par$cuenta.cod_pais,
             par$cuenta.cod_tipo_cuenta
        FROM sapr_tcuenta cu
       INNER JOIN gepr_tsector se
          ON se.oid_sector = cu.oid_sector
       INNER JOIN gepr_tplanta pl
          ON pl.oid_planta = se.oid_planta
       INNER JOIN gepr_tdelegacion dl
          ON dl.oid_delegacion = pl.oid_delegacion
       INNER JOIN gepr_tsubcanal sc
          ON sc.oid_subcanal = cu.oid_subcanal
       INNER JOIN gepr_tcliente cl
          ON cl.oid_cliente = cu.oid_cliente
        LEFT JOIN gepr_tsubcliente scl
          ON scl.oid_subcliente = cu.oid_subcliente
        LEFT JOIN gepr_tpunto_servicio pts
          ON pts.oid_pto_servicio = cu.oid_pto_servicio
       WHERE cu.oid_cliente = par$cuenta.oid_cliente
         AND cu.oid_sector = par$cuenta.oid_sector
         AND cu.oid_subcanal = par$cuenta.oid_subcanal
         AND NVL(cu.oid_subcliente, '###') =
             NVL(par$cuenta.oid_subcliente, '###')
         AND NVL(cu.oid_pto_servicio, '###') =
             NVL(par$cuenta.oid_pto_servicio, '###');
    EXCEPTION
      WHEN no_data_found THEN
        par$cuenta.oid_cuenta := NULL;
    END;
  
    par$selects := sql%rowcount;
  
  END srecuperar_cuenta_por_oids;

  /* Recupera os totalizadores de saldo */
  PROCEDURE srecuperar_totalizadores_saldo(par$cuenta_mov    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$cuenta_sal    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$totalizadores OUT gepr_pcomon_###VERSION###.tipo$totalizadores,
                                           par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                           par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                           par$cod_usuario   IN gepr_pcomon_###VERSION###.tipo$cod_) AS
    par$totalizador gepr_pcomon_###VERSION###.tipo$totalizador;
  
    /*CURSOR DE CON SUBCANAL y SALDOS*/
    CURSOR cur$totalizadorSC_SAL(par$cuenta_mov IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                 par$cuenta_sal IN gepr_pcomon_###VERSION###.tipo$cuenta) IS
      SELECT DISTINCT NIVEL_MOV.OID_CONFIG_NIVEL_MOVIMIENTO,
                      NIVEL_MOV.BOL_DEFECTO,
                      CLI_SALDO.OID_CLIENTE,
                      CLI_SALDO.COD_CLIENTE,
                      CLI_SALDO.DES_CLIENTE,
                      SBCLI_SALDO.OID_SUBCLIENTE,
                      SBCLI_SALDO.COD_SUBCLIENTE,
                      SBCLI_SALDO.DES_SUBcLIENTE,
                      PTS_SALDO.OID_PTO_SERVICIO,
                      PTS_SALDO.COD_PTO_SERVICIO,
                      PTS_SALDO.DES_PTO_SERVICIO,
                      NIVEL_MOV.OID_SUBCANAL
        FROM SAPR_TCONFIG_NIVEL_SALDO NIVEL_SALDO
       INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO NIVEL_MOV
          ON NIVEL_MOV.OID_CONFIG_NIVEL_SALDO =
             NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLI_SALDO
          ON CLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
          ON SBCLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
         AND SBCLI_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
          ON PTS_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
         AND PTS_SALDO.OID_PTO_SERVICIO = NIVEL_SALDO.OID_PTO_SERVICIO
       INNER JOIN GEPR_TCLIENTE CLI_MOV
          ON CLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_MOV
          ON SBCLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
         AND SBCLI_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_MOV
          ON PTS_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
         AND PTS_MOV.OID_PTO_SERVICIO = NIVEL_MOV.OID_PTO_SERVICIO
       WHERE NIVEL_MOV.BOL_ACTIVO = 1
         AND NIVEL_MOV.OID_CLIENTE = par$cuenta_mov.OID_CLIENTE
         AND NIVEL_MOV.OID_SUBCANAL = par$cuenta_mov.OID_SUBCANAL
         AND NVL(NIVEL_MOV.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_mov.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_MOV.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_mov.OID_PTO_SERVICIO, '###')
         AND NIVEL_SALDO.OID_CLIENTE = par$cuenta_sal.OID_CLIENTE
         AND NVL(NIVEL_SALDO.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_sal.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_SALDO.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_sal.oid_pto_servicio, '###')
         AND rownum = 1;
  
    /*CURSOR DE CON SUBCANAL*/
    CURSOR cur$totalizadorSC(par$cuenta_mov IN gepr_pcomon_###VERSION###.tipo$cuenta) IS
      SELECT DISTINCT NIVEL_MOV.OID_CONFIG_NIVEL_MOVIMIENTO,
                      NIVEL_MOV.BOL_DEFECTO,
                      CLI_SALDO.OID_CLIENTE,
                      CLI_SALDO.COD_CLIENTE,
                      CLI_SALDO.DES_CLIENTE,
                      SBCLI_SALDO.OID_SUBCLIENTE,
                      SBCLI_SALDO.COD_SUBCLIENTE,
                      SBCLI_SALDO.DES_SUBcLIENTE,
                      PTS_SALDO.OID_PTO_SERVICIO,
                      PTS_SALDO.COD_PTO_SERVICIO,
                      PTS_SALDO.DES_PTO_SERVICIO,
                      NIVEL_MOV.OID_SUBCANAL
        FROM SAPR_TCONFIG_NIVEL_SALDO NIVEL_SALDO
       INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO NIVEL_MOV
          ON NIVEL_MOV.OID_CONFIG_NIVEL_SALDO =
             NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLI_SALDO
          ON CLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
          ON SBCLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
         AND SBCLI_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
          ON PTS_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
         AND PTS_SALDO.OID_PTO_SERVICIO = NIVEL_SALDO.OID_PTO_SERVICIO
       INNER JOIN GEPR_TCLIENTE CLI_MOV
          ON CLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_MOV
          ON SBCLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
         AND SBCLI_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_MOV
          ON PTS_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
         AND PTS_MOV.OID_PTO_SERVICIO = NIVEL_MOV.OID_PTO_SERVICIO
       WHERE NIVEL_MOV.BOL_ACTIVO = 1
         AND NIVEL_MOV.BOL_DEFECTO = 1
         AND NIVEL_MOV.OID_CLIENTE = par$cuenta_mov.OID_CLIENTE
         AND NIVEL_MOV.OID_SUBCANAL = par$cuenta_mov.OID_SUBCANAL
         AND NVL(NIVEL_MOV.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_mov.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_MOV.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_mov.OID_PTO_SERVICIO, '###')
         AND rownum = 1;
  
    /*CURSOR DE SIN SUBCANAL y SALDOS*/
    CURSOR cur$totalizadorSinSC_SAL(par$cuenta_mov IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                    par$cuenta_sal IN gepr_pcomon_###VERSION###.tipo$cuenta) IS
      SELECT DISTINCT NIVEL_MOV.OID_CONFIG_NIVEL_MOVIMIENTO,
                      NIVEL_MOV.BOL_DEFECTO,
                      CLI_SALDO.OID_CLIENTE,
                      CLI_SALDO.COD_CLIENTE,
                      CLI_SALDO.DES_CLIENTE,
                      SBCLI_SALDO.OID_SUBCLIENTE,
                      SBCLI_SALDO.COD_SUBCLIENTE,
                      SBCLI_SALDO.DES_SUBcLIENTE,
                      PTS_SALDO.OID_PTO_SERVICIO,
                      PTS_SALDO.COD_PTO_SERVICIO,
                      PTS_SALDO.DES_PTO_SERVICIO,
                      NIVEL_MOV.OID_SUBCANAL
        FROM SAPR_TCONFIG_NIVEL_SALDO NIVEL_SALDO
       INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO NIVEL_MOV
          ON NIVEL_MOV.OID_CONFIG_NIVEL_SALDO =
             NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLI_SALDO
          ON CLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
          ON SBCLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
         AND SBCLI_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
          ON PTS_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
         AND PTS_SALDO.OID_PTO_SERVICIO = NIVEL_SALDO.OID_PTO_SERVICIO
       INNER JOIN GEPR_TCLIENTE CLI_MOV
          ON CLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_MOV
          ON SBCLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
         AND SBCLI_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_MOV
          ON PTS_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
         AND PTS_MOV.OID_PTO_SERVICIO = NIVEL_MOV.OID_PTO_SERVICIO
       WHERE NIVEL_MOV.BOL_ACTIVO = 1
         AND NIVEL_MOV.OID_SUBCANAL IS NULL
         AND NIVEL_MOV.OID_CLIENTE = par$cuenta_mov.OID_CLIENTE
         AND NVL(NIVEL_MOV.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_mov.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_MOV.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_mov.OID_PTO_SERVICIO, '###')
         AND NIVEL_SALDO.OID_CLIENTE = par$cuenta_sal.OID_CLIENTE
         AND NVL(NIVEL_SALDO.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_sal.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_SALDO.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_sal.oid_pto_servicio, '###')
         AND rownum = 1;
  
    /*CURSOR DE SIN SUBCANAL */
    CURSOR cur$totalizadorSinSC(par$cuenta_mov IN gepr_pcomon_###VERSION###.tipo$cuenta) IS
      SELECT DISTINCT NIVEL_MOV.OID_CONFIG_NIVEL_MOVIMIENTO,
                      NIVEL_MOV.BOL_DEFECTO,
                      CLI_SALDO.OID_CLIENTE,
                      CLI_SALDO.COD_CLIENTE,
                      CLI_SALDO.DES_CLIENTE,
                      SBCLI_SALDO.OID_SUBCLIENTE,
                      SBCLI_SALDO.COD_SUBCLIENTE,
                      SBCLI_SALDO.DES_SUBCLIENTE,
                      PTS_SALDO.OID_PTO_SERVICIO,
                      PTS_SALDO.COD_PTO_SERVICIO,
                      PTS_SALDO.DES_PTO_SERVICIO,
                      NIVEL_MOV.OID_SUBCANAL
        FROM SAPR_TCONFIG_NIVEL_SALDO NIVEL_SALDO
       INNER JOIN SAPR_TCONFIG_NIVEL_MOVIMIENTO NIVEL_MOV
          ON NIVEL_MOV.OID_CONFIG_NIVEL_SALDO =
             NIVEL_SALDO.OID_CONFIG_NIVEL_SALDO
       INNER JOIN GEPR_TCLIENTE CLI_SALDO
          ON CLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
          ON SBCLI_SALDO.OID_CLIENTE = NIVEL_SALDO.OID_CLIENTE
         AND SBCLI_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
          ON PTS_SALDO.OID_SUBCLIENTE = NIVEL_SALDO.OID_SUBCLIENTE
         AND PTS_SALDO.OID_PTO_SERVICIO = NIVEL_SALDO.OID_PTO_SERVICIO
       INNER JOIN GEPR_TCLIENTE CLI_MOV
          ON CLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
        LEFT JOIN GEPR_TSUBCLIENTE SBCLI_MOV
          ON SBCLI_MOV.OID_CLIENTE = NIVEL_MOV.OID_CLIENTE
         AND SBCLI_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_MOV
          ON PTS_MOV.OID_SUBCLIENTE = NIVEL_MOV.OID_SUBCLIENTE
         AND PTS_MOV.OID_PTO_SERVICIO = NIVEL_MOV.OID_PTO_SERVICIO
       WHERE NIVEL_MOV.BOL_ACTIVO = 1
         AND NIVEL_MOV.BOL_DEFECTO = 1
         AND NIVEL_MOV.OID_SUBCANAL IS NULL
         AND NIVEL_MOV.OID_CLIENTE = par$cuenta_mov.OID_CLIENTE
         AND NVL(NIVEL_MOV.OID_SUBCLIENTE, '###') =
             NVL(par$cuenta_mov.OID_SUBCLIENTE, '###')
         AND NVL(NIVEL_MOV.OID_PTO_SERVICIO, '###') =
             NVL(par$cuenta_mov.OID_PTO_SERVICIO, '###');
  
  BEGIN
    par$selects := 0;
  
    IF par$cuenta_sal.oid_cliente IS NOT NULL THEN
      /*CURSOR DE CON SUBCANAL y SALDOS*/
      FOR rec$totalizador IN cur$totalizadorSC_SAL(par$cuenta_mov,
                                                   par$cuenta_sal) LOOP
        par$totalizador.OID_CONFIG_NIVEL_MOVIMIENTO := rec$totalizador.oid_config_nivel_movimiento;
        par$totalizador.BOL_DEFECTO := rec$totalizador.BOL_DEFECTO;
        par$totalizador.OID_CLIENTE := rec$totalizador.OID_CLIENTE;
        par$totalizador.COD_CLIENTE := rec$totalizador.COD_CLIENTE;
        par$totalizador.DES_CLIENTE := rec$totalizador.DES_CLIENTE;
        par$totalizador.OID_SUBCLIENTE := rec$totalizador.OID_SUBCLIENTE;
        par$totalizador.COD_SUBCLIENTE := rec$totalizador.COD_SUBCLIENTE;
        par$totalizador.DES_SUBCLIENTE := rec$totalizador.DES_SUBCLIENTE;
        par$totalizador.OID_PTO_SERVICIO := rec$totalizador.OID_PTO_SERVICIO;
        par$totalizador.COD_PTO_SERVICIO := rec$totalizador.COD_PTO_SERVICIO;
        par$totalizador.DES_PTO_SERVICIO := rec$totalizador.DES_PTO_SERVICIO;
        par$totalizador.OID_SUBCANAL := rec$totalizador.OID_SUBCANAL;
        par$totalizadores(0) := par$totalizador;
        EXIT;
      END LOOP rec$totalizador;
    ELSE
      /*CURSOR DE CON SUBCANAL*/
      FOR rec$totalizador IN cur$totalizadorSC(par$cuenta_mov) LOOP
        par$totalizador.OID_CONFIG_NIVEL_MOVIMIENTO := rec$totalizador.oid_config_nivel_movimiento;
        par$totalizador.BOL_DEFECTO := rec$totalizador.BOL_DEFECTO;
        par$totalizador.OID_CLIENTE := rec$totalizador.OID_CLIENTE;
        par$totalizador.COD_CLIENTE := rec$totalizador.COD_CLIENTE;
        par$totalizador.DES_CLIENTE := rec$totalizador.DES_CLIENTE;
        par$totalizador.OID_SUBCLIENTE := rec$totalizador.OID_SUBCLIENTE;
        par$totalizador.COD_SUBCLIENTE := rec$totalizador.COD_SUBCLIENTE;
        par$totalizador.DES_SUBCLIENTE := rec$totalizador.DES_SUBCLIENTE;
        par$totalizador.OID_PTO_SERVICIO := rec$totalizador.OID_PTO_SERVICIO;
        par$totalizador.COD_PTO_SERVICIO := rec$totalizador.COD_PTO_SERVICIO;
        par$totalizador.DES_PTO_SERVICIO := rec$totalizador.DES_PTO_SERVICIO;
        par$totalizador.OID_SUBCANAL := rec$totalizador.OID_SUBCANAL;
        par$totalizadores(0) := par$totalizador;
        EXIT;
      END LOOP rec$totalizador;
    END IF;
  
    IF par$totalizadores.COUNT = 0 THEN
    
      IF par$cuenta_sal.oid_cliente IS NOT NULL THEN
        /*CURSOR DE SIN SUBCANAL y SALDOS*/
        FOR rec$totalizador IN cur$totalizadorSINSC_SAL(par$cuenta_mov,
                                                        par$cuenta_sal) LOOP
          par$totalizador.OID_CONFIG_NIVEL_MOVIMIENTO := rec$totalizador.oid_config_nivel_movimiento;
          par$totalizador.BOL_DEFECTO := rec$totalizador.BOL_DEFECTO;
          par$totalizador.OID_CLIENTE := rec$totalizador.OID_CLIENTE;
          par$totalizador.COD_CLIENTE := rec$totalizador.COD_CLIENTE;
          par$totalizador.DES_CLIENTE := rec$totalizador.DES_CLIENTE;
          par$totalizador.OID_SUBCLIENTE := rec$totalizador.OID_SUBCLIENTE;
          par$totalizador.COD_SUBCLIENTE := rec$totalizador.COD_SUBCLIENTE;
          par$totalizador.DES_SUBCLIENTE := rec$totalizador.DES_SUBCLIENTE;
          par$totalizador.OID_PTO_SERVICIO := rec$totalizador.OID_PTO_SERVICIO;
          par$totalizador.COD_PTO_SERVICIO := rec$totalizador.COD_PTO_SERVICIO;
          par$totalizador.DES_PTO_SERVICIO := rec$totalizador.DES_PTO_SERVICIO;
          par$totalizador.OID_SUBCANAL := rec$totalizador.OID_SUBCANAL;
          par$totalizadores(0) := par$totalizador;
          EXIT;
        END LOOP rec$totalizador;
      ELSE
      
        /*CURSOR DE SIN SUBCANAL */
        FOR rec$totalizador IN cur$totalizadorSINSC(par$cuenta_mov) LOOP
          par$totalizador.OID_CONFIG_NIVEL_MOVIMIENTO := rec$totalizador.oid_config_nivel_movimiento;
          par$totalizador.BOL_DEFECTO := rec$totalizador.BOL_DEFECTO;
          par$totalizador.OID_CLIENTE := rec$totalizador.OID_CLIENTE;
          par$totalizador.COD_CLIENTE := rec$totalizador.COD_CLIENTE;
          par$totalizador.DES_CLIENTE := rec$totalizador.DES_CLIENTE;
          par$totalizador.OID_SUBCLIENTE := rec$totalizador.OID_SUBCLIENTE;
          par$totalizador.COD_SUBCLIENTE := rec$totalizador.COD_SUBCLIENTE;
          par$totalizador.DES_SUBCLIENTE := rec$totalizador.DES_SUBCLIENTE;
          par$totalizador.OID_PTO_SERVICIO := rec$totalizador.OID_PTO_SERVICIO;
          par$totalizador.COD_PTO_SERVICIO := rec$totalizador.COD_PTO_SERVICIO;
          par$totalizador.DES_PTO_SERVICIO := rec$totalizador.DES_PTO_SERVICIO;
          par$totalizador.OID_SUBCANAL := rec$totalizador.OID_SUBCANAL;
          par$totalizadores(0) := par$totalizador;
          EXIT;
        END LOOP rec$totalizador;
      END IF;
    END IF;
  
    IF par$totalizadores.COUNT = 0 THEN
    
      srecuperar_tot_otros_niveis(par$cuenta_mov    => par$cuenta_mov,
                                  par$cuenta_sal    => par$cuenta_sal,
                                  par$cod_usuario   => par$cod_usuario,
                                  par$totalizadores => par$totalizadores,
                                  par$selects       => par$selects,
                                  par$inserts       => par$inserts);
    end if;
  
  END srecuperar_totalizadores_saldo;

  PROCEDURE srecuperar_tot_otros_niveis(par$cuenta_mov    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                        par$cuenta_sal    IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                        par$cod_usuario   IN gepr_pcomon_###VERSION###.tipo$cod_,
                                        par$totalizadores OUT gepr_pcomon_###VERSION###.tipo$totalizadores,
                                        par$selects       OUT gepr_pcomon_###VERSION###.tipo$nel_,
                                        par$inserts       OUT gepr_pcomon_###VERSION###.tipo$nel_) as
    var$oid_cliente          gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_cliente          gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_subcliente       gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_subcliente       gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_pto_servicio     gepr_pcomon_###VERSION###.tipo$oid_;
    var$cod_pto_servicio     gepr_pcomon_###VERSION###.tipo$cod_;
    var$oid_subcanal         gepr_pcomon_###VERSION###.tipo$oid_;
    var$des_cliente          gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_subcliente       gepr_pcomon_###VERSION###.tipo$desc_;
    var$des_pto_servicio     gepr_pcomon_###VERSION###.tipo$desc_;
    var$oid_config_nivel_sal gepr_pcomon_###VERSION###.tipo$oid_;
    var$oid_config_nivel_mov gepr_pcomon_###VERSION###.tipo$oid_;
    var$inserts              gepr_pcomon_###VERSION###.tipo$oid_;
    par$totalizador          gepr_pcomon_###VERSION###.tipo$totalizador;
  begin
  
    if par$cuenta_sal.cod_cliente is not null or
       par$cuenta_sal.oid_cliente is not null THEN
    
      if (par$cuenta_sal.cod_cliente is not null and
         par$cuenta_sal.cod_canal is not null and
         par$cuenta_sal.cod_subcanal is not null and
         par$cuenta_sal.cod_delegacion is not null and
         par$cuenta_sal.cod_planta is not null and
         par$cuenta_sal.cod_sector is not null) then
      
        begin
          SELECT OID_CLIENTE,
                 COD_CLIENTE,
                 DES_CLIENTE,
                 OID_SUBCLIENTE,
                 COD_SUBCLIENTE,
                 DES_SUBCLIENTE,
                 OID_PTO_SERVICIO,
                 COD_PTO_SERVICIO,
                 DES_PTO_SERVICIO,
                 OID_SUBCANAL,
                 OID_CONFIG_NIVEL_SALDO
            INTO var$oid_cliente,
                 var$cod_cliente,
                 var$des_cliente,
                 var$oid_subcliente,
                 var$cod_subcliente,
                 var$des_subcliente,
                 var$oid_pto_servicio,
                 var$cod_pto_servicio,
                 var$des_pto_servicio,
                 var$oid_subcanal,
                 var$oid_config_nivel_sal
            FROM (SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND CNM.OID_SUBCLIENTE IS NULL
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.cod_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.cod_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.cod_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.cod_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.cod_pto_servicio, '###')
                  UNION
                  SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SCM
                      ON SCM.OID_SUBCLIENTE = CNM.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND SCM.COD_SUBCLIENTE = par$cuenta_mov.cod_subcliente
                     AND CNM.OID_PTO_SERVICIO IS NULL
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.cod_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.cod_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.cod_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.cod_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.cod_pto_servicio, '###')
                  UNION
                  SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SCM
                      ON SCM.OID_SUBCLIENTE = CNM.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTOM
                      ON PTOM.OID_PTO_SERVICIO = CNM.OID_PTO_SERVICIO
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND SCM.COD_SUBCLIENTE = par$cuenta_mov.cod_subcliente
                     AND PTOM.COD_PTO_SERVICIO =
                         par$cuenta_mov.cod_pto_servicio
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.cod_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.cod_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.cod_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.cod_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.cod_pto_servicio, '###'))
           WHERE ROWNUM = 1;
        
        exception
          when no_data_found then
            var$oid_config_nivel_sal := null;
        end;
      
      elsif (par$cuenta_sal.oid_cliente is not null and
            par$cuenta_sal.oid_canal is not null and
            par$cuenta_sal.oid_subcanal is not null and
            par$cuenta_sal.oid_delegacion is not null and
            par$cuenta_sal.oid_planta is not null and
            par$cuenta_sal.oid_sector is not null) then
      
        begin
        
          SELECT OID_CLIENTE,
                 COD_CLIENTE,
                 DES_CLIENTE,
                 OID_SUBCLIENTE,
                 COD_SUBCLIENTE,
                 DES_SUBcLIENTE,
                 OID_PTO_SERVICIO,
                 COD_PTO_SERVICIO,
                 DES_PTO_SERVICIO,
                 OID_SUBCANAL,
                 OID_CONFIG_NIVEL_SALDO
            INTO var$oid_cliente,
                 var$cod_cliente,
                 var$des_cliente,
                 var$oid_subcliente,
                 var$cod_subcliente,
                 var$des_subcliente,
                 var$oid_pto_servicio,
                 var$cod_pto_servicio,
                 var$des_pto_servicio,
                 var$oid_subcanal,
                 var$oid_config_nivel_sal
            FROM (SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND CNM.OID_SUBCLIENTE IS NULL
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.oid_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.oid_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.oid_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.oid_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.oid_pto_servicio, '###')
                  UNION
                  SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SCM
                      ON SCM.OID_SUBCLIENTE = CNM.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND SCM.COD_SUBCLIENTE = par$cuenta_mov.cod_subcliente
                     AND CNM.OID_PTO_SERVICIO IS NULL
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.oid_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.oid_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.oid_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.oid_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.oid_pto_servicio, '###')
                  UNION
                  SELECT CLI_SALDO.OID_CLIENTE,
                         CLI_SALDO.COD_CLIENTE,
                         CLI_SALDO.DES_CLIENTE,
                         SBCLI_SALDO.OID_SUBCLIENTE,
                         SBCLI_SALDO.COD_SUBCLIENTE,
                         SBCLI_SALDO.DES_SUBcLIENTE,
                         PTS_SALDO.OID_PTO_SERVICIO,
                         PTS_SALDO.COD_PTO_SERVICIO,
                         PTS_SALDO.DES_PTO_SERVICIO,
                         CNM.OID_SUBCANAL,
                         CNS.OID_CONFIG_NIVEL_SALDO
                    FROM SAPR_TCONFIG_NIVEL_MOVIMIENTO CNM
                   INNER JOIN SAPR_TCONFIG_NIVEL_SALDO CNS
                      ON CNS.OID_CONFIG_NIVEL_SALDO =
                         CNM.OID_CONFIG_NIVEL_SALDO
                   INNER JOIN GEPR_TCLIENTE CM
                      ON CM.OID_CLIENTE = CNM.OID_CLIENTE
                   INNER JOIN GEPR_TCLIENTE CLI_SALDO
                      ON CLI_SALDO.OID_CLIENTE = CNS.OID_CLIENTE
                   INNER JOIN GEPR_TSUBCANAL SCA
                      ON SCA.OID_SUBCANAL = CNM.OID_SUBCANAL
                   INNER JOIN GEPR_TCANAL CA
                      ON CA.OID_CANAL = SCA.OID_CANAL
                    LEFT JOIN GEPR_TSUBCLIENTE SCM
                      ON SCM.OID_SUBCLIENTE = CNM.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTOM
                      ON PTOM.OID_PTO_SERVICIO = CNM.OID_PTO_SERVICIO
                    LEFT JOIN GEPR_TSUBCLIENTE SBCLI_SALDO
                      ON SBCLI_SALDO.OID_SUBCLIENTE = CNS.OID_SUBCLIENTE
                    LEFT JOIN GEPR_TPUNTO_SERVICIO PTS_SALDO
                      ON PTS_SALDO.OID_PTO_SERVICIO = CNS.OID_PTO_SERVICIO
                   WHERE CM.COD_CLIENTE = par$cuenta_mov.cod_cliente
                     AND SCM.COD_SUBCLIENTE = par$cuenta_mov.cod_subcliente
                     AND PTOM.COD_PTO_SERVICIO =
                         par$cuenta_mov.cod_pto_servicio
                     AND CLI_SALDO.COD_CLIENTE = par$cuenta_sal.oid_cliente
                     AND CNM.BOL_ACTIVO = 1
                     AND NVL(SCA.COD_SUBCANAL, '###') =
                         NVL(par$cuenta_sal.oid_subcanal, '###')
                     AND NVL(CA.COD_CANAL, '###') =
                         NVL(par$cuenta_sal.oid_canal, '###')
                     AND NVL(SBCLI_SALDO.COD_SUBCLIENTE, '###') =
                         NVL(par$cuenta_sal.oid_subcliente, '###')
                     AND NVL(PTS_SALDO.COD_PTO_SERVICIO, '###') =
                         NVL(par$cuenta_sal.oid_pto_servicio, '###'))
           WHERE ROWNUM = 1;
        exception
          when no_data_found then
            var$oid_config_nivel_sal := null;
        end;
      end if;
    
      par$selects := sql%rowcount;
    
      if var$oid_config_nivel_sal is not null then
      
        screar_config_nivel_movimiento(par$cuenta               => par$cuenta_mov,
                                       par$cod_usuario          => par$cod_usuario,
                                       par$oid_config_nivel_sal => var$oid_config_nivel_sal,
                                       par$oid_subcanal         => var$oid_subcanal,
                                       par$oid_config_nivel_mov => var$oid_config_nivel_mov,
                                       par$inserts              => var$inserts);
      
        par$inserts := par$inserts + var$inserts;
      
        par$totalizador.OID_CONFIG_NIVEL_MOVIMIENTO := var$oid_config_nivel_mov;
        par$totalizador.BOL_DEFECTO := 1;
        par$totalizador.OID_CLIENTE := var$oid_cliente;
        par$totalizador.COD_CLIENTE := var$cod_cliente;
        par$totalizador.DES_CLIENTE := var$des_cliente;
        par$totalizador.OID_SUBCLIENTE := var$oid_subcliente;
        par$totalizador.COD_SUBCLIENTE := var$cod_subcliente;
        par$totalizador.DES_SUBCLIENTE := var$des_subcliente;
        par$totalizador.OID_PTO_SERVICIO := var$oid_pto_servicio;
        par$totalizador.COD_PTO_SERVICIO := var$cod_pto_servicio;
        par$totalizador.DES_PTO_SERVICIO := var$des_pto_servicio;
        par$totalizador.OID_SUBCANAL := var$oid_subcanal;
        par$totalizadores(0) := par$totalizador;
      
      end if;
    
    end if;
  
  end srecuperar_tot_otros_niveis;

  /* supd_tipo_cuenta */
  PROCEDURE supd_tipo_cuenta(par$oid_cuenta      IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$cod_tipo_cuenta IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario     IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$updates         OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
  BEGIN
  
    UPDATE sapr_tcuenta
       SET cod_tipo_cuenta          = par$cod_tipo_cuenta,
           gmt_modificacion         = gepr_putilidades_###VERSION###.fgmt_zero,
           des_usuario_modificacion = par$cod_usuario
     WHERE oid_cuenta = par$oid_cuenta;
  
    par$updates := sql%rowcount;
  
  END supd_tipo_cuenta;
  /* sins_tcuenta */
  PROCEDURE sins_tcuenta(par$cuenta      IN OUT gepr_pcomon_###VERSION###.tipo$cuenta,
                         par$cod_usuario IN gepr_pcomon_###VERSION###.tipo$cod_,
						 par$cod_cultura IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$inserts     OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    par$cuenta.oid_cuenta := SYS_GUID();
  
    INSERT INTO sapr_tcuenta
      (oid_cuenta,
       oid_cliente,
       oid_sector,
       oid_subcanal,
       oid_subcliente,
       oid_pto_servicio,
       cod_tipo_cuenta,
       gmt_creacion,
       des_usuario_creacion,
       gmt_modificacion,
       des_usuario_modificacion)
    VALUES
      (par$cuenta.oid_cuenta,
       par$cuenta.oid_cliente,
       par$cuenta.oid_sector,
       par$cuenta.oid_subcanal,
       par$cuenta.oid_subcliente,
       par$cuenta.oid_pto_servicio,
       par$cuenta.cod_tipo_cuenta,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario);
  
    par$inserts := sql%rowcount;

   exception
    when DUP_VAL_ON_INDEX then
      /* Se deu erro de AK o errro sera abafado. Pois se trata de registro em concorrência.
      E o fluxo pode continuar normalmente. */
      begin
       
        select c.oid_cuenta
          into par$cuenta.oid_cuenta
          from sapr_tcuenta c
          where c.oid_cliente = par$cuenta.oid_cliente and
               c.oid_sector = par$cuenta.oid_sector and
               c.oid_subcanal = par$cuenta.oid_subcanal and
               nvl(c.oid_subcliente,'###') =  nvl(par$cuenta.oid_subcliente,'###') and
               nvl(c.oid_pto_servicio,'###') = nvl(par$cuenta.oid_pto_servicio,'###');
      
        exception when no_data_found then 
        raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError,
                                    gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                                        'msgCuentaNoEncontrada',
                                                                        gepr_pcomon_###VERSION###.const$CodFunDicObtCuentaSaldo,
                                                                        gepr_pcomon_###VERSION###.const$codAplicacionSaldos,
                                                                        '',
                                                                        1));
      end;
  END sins_tcuenta;

  /* Crea la configuracion nivel saldo */
  PROCEDURE sins_tconfig_nivel_saldo(par$cuenta               IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                     par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                     par$oid_config_nivel_sal OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                     par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    BEGIN
      SELECT OID_CONFIG_NIVEL_SALDO
        INTO par$oid_config_nivel_sal
        FROM SAPR_TCONFIG_NIVEL_SALDO
       WHERE OID_CLIENTE = par$cuenta.oid_cliente
         AND NVL(OID_SUBCLIENTE, '#') = NVL(par$cuenta.oid_subcliente, '#')
         AND NVL(OID_PTO_SERVICIO, '#') =
             NVL(par$cuenta.oid_pto_servicio, '#');
    EXCEPTION
      WHEN NO_DATA_FOUND THEN
        par$oid_config_nivel_sal := SYS_GUID();
      
        INSERT INTO SAPR_TCONFIG_NIVEL_SALDO
          (OID_CONFIG_NIVEL_SALDO,
           OID_CLIENTE,
           OID_SUBCLIENTE,
           OID_PTO_SERVICIO,
           GMT_CREACION,
           DES_USUARIO_CREACION,
           GMT_MODIFICACION,
           DES_USUARIO_MODIFICACION)
        VALUES
          (par$oid_config_nivel_sal,
           par$cuenta.oid_cliente,
           par$cuenta.oid_subcliente,
           par$cuenta.oid_pto_servicio,
           var$gmt_zero,
           par$cod_usuario,
           var$gmt_zero,
           par$cod_usuario);
      
        par$inserts := sql%rowcount;
    END;
  
    /*VERIFICA QUAL TABELA DE SER ATUALIZA- CLIENTE, SUBCIENTE OU PUNTO_SERVICIO*/
    IF par$cuenta.oid_subcliente IS NULL AND
       par$cuenta.oid_pto_servicio IS NULL THEN
      UPDATE GEPR_TCLIENTE
         SET BOL_TOTALIZADOR_SALDO = 1
       WHERE OID_CLIENTE = par$cuenta.oid_cliente;
    
    ELSIF par$cuenta.oid_pto_servicio IS NULL THEN
      UPDATE GEPR_TSUBCLIENTE
         SET BOL_TOTALIZADOR_SALDO = 1
       WHERE OID_SUBCLIENTE = par$cuenta.oid_subcliente;
    ELSE
      UPDATE GEPR_TPUNTO_SERVICIO
         SET BOL_TOTALIZADOR_SALDO = 1
       WHERE OID_PTO_SERVICIO = par$cuenta.oid_pto_servicio;
    END IF;
  
  END sins_tconfig_nivel_saldo;

  /* Cria a configuracao nivel movimento */
  PROCEDURE screar_config_nivel_movimiento(par$cuenta               IN gepr_pcomon_###VERSION###.tipo$cuenta,
                                           par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                                           par$oid_config_nivel_sal IN gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$oid_subcanal         IN gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$oid_config_nivel_mov OUT gepr_pcomon_###VERSION###.tipo$oid_,
                                           par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
  
    par$oid_config_nivel_mov := SYS_GUID();
  
    /* Insere a configuracao nivel movimento */
    INSERT INTO SAPR_TCONFIG_NIVEL_MOVIMIENTO
      (OID_CONFIG_NIVEL_MOVIMIENTO,
       OID_CONFIG_NIVEL_SALDO,
       OID_CLIENTE,
       OID_SUBCLIENTE,
       OID_PTO_SERVICIO,
       OID_SUBCANAL,
       FYH_VIGENCIA,
       BOL_ACTIVO,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION,
       BOL_DEFECTO)
    VALUES
      (par$oid_config_nivel_mov,
       par$oid_config_nivel_sal,
       par$cuenta.oid_cliente,
       par$cuenta.oid_subcliente,
       par$cuenta.oid_pto_servicio,
       par$oid_subcanal,
       sysdate,
       1,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario,
       1);
  
    par$inserts := par$inserts + sql%rowcount;
  
  END screar_config_nivel_movimiento;

  /* Cria o nivel saldo */
  PROCEDURE sins_tnivel_saldo(par$cuenta_mov           IN gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$cuenta_sal           IN gepr_pcomon_###VERSION###.tipo$cuenta,
                              par$oid_config_nivel_Mov IN gepr_pcomon_###VERSION###.tipo$oid_,
                              par$cod_usuario          IN gepr_pcomon_###VERSION###.tipo$cod_,
                              par$inserts              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$selects              OUT gepr_pcomon_###VERSION###.tipo$nel_,
                              par$updates              OUT gepr_pcomon_###VERSION###.tipo$nel_) AS
    var$gmt_zero    VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
    var$tipo_cuenta gepr_pcomon_###VERSION###.tipo$cod_;
  BEGIN
  
    INSERT INTO SAPR_TNIVEL_SALDO
      (OID_NIVEL_SALDO,
       OID_CUENTA_MOVIMIENTO,
       OID_CUENTA_SALDO,
       OID_CONFIG_NIVEL_MOVIMIENTO,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (SYS_GUID(),
       par$cuenta_mov.oid_cuenta,
       par$cuenta_sal.oid_cuenta,
       par$oid_config_nivel_Mov,
       var$gmt_zero,
       par$cod_usuario,
       var$gmt_zero,
       par$cod_usuario);
  
    par$inserts := sql%rowcount;
  
    SELECT C.COD_TIPO_CUENTA
      INTO var$tipo_cuenta
      FROM SAPR_TCUENTA C
     WHERE C.OID_CUENTA = par$cuenta_sal.oid_cuenta;
    par$selects := sql%rowcount;
  
    IF var$tipo_cuenta = gepr_pcomon_###VERSION###.const$codTipoCuentaMov THEN
      UPDATE SAPR_TCUENTA
         SET COD_TIPO_CUENTA = gepr_pcomon_###VERSION###.const$codTipoCuentaAmbos
       WHERE OID_CUENTA = par$cuenta_sal.oid_cuenta;
      par$updates := sql%rowcount;
    END IF;
  exception
    when DUP_VAL_ON_INDEX then
      /* Se deu erro de AK o errro sera abafado. Pois se trata de registro em concorrência.
      E o fluxo pode continuar normalmente. */
      par$inserts := 0;
      par$updates := 0;
      par$selects := 0;
  END sins_tnivel_saldo;

  /* fcargar_rc_cuentas */
  FUNCTION fcargar_rc_cuentas(par$selects OUT gepr_pcomon_###VERSION###.tipo$nel_)
    RETURN SYS_REFCURSOR IS
    var$rcCuentas SYS_REFCURSOR;
  BEGIN
  
    OPEN var$rcCuentas FOR
      SELECT CU.OID_CUENTA,
             CU.COD_TIPO_CUENTA,
             CL.OID_CLIENTE,
             CL.COD_CLIENTE,
             CL.DES_CLIENTE,
             CL.BOL_ENVIADO_SALDOS         AS CL_BOL_ENVIADO_SALDOS,
             CL.BOL_TOTALIZADOR_SALDO      AS CL_BOL_TOTALIZADOR_SALDO,
             CL.BOL_VIGENTE                AS CL_BOL_VIGENTE,
             SCL.OID_SUBCLIENTE,
             SCL.COD_SUBCLIENTE,
             SCL.DES_SUBCLIENTE,
             SCL.BOL_ENVIADO_SALDOS        AS SCL_BOL_ENVIADO_SALDOS,
             SCL.BOL_TOTALIZADOR_SALDO     AS SCL_BOL_TOTALIZADOR_SALDO,
             SCL.BOL_VIGENTE               AS SCL_BOL_VIGENTE,
             PTO.OID_PTO_SERVICIO,
             PTO.COD_PTO_SERVICIO,
             PTO.DES_PTO_SERVICIO,
             PTO.BOL_ENVIADO_SALDOS        AS PTO_BOL_ENVIADO_SALDOS,
             PTO.BOL_TOTALIZADOR_SALDO     AS PTO_BOL_TOTALIZADOR_SALDO,
             PTO.BOL_VIGENTE               AS PTO_BOL_VIGENTE,
             CAN.OID_CANAL,
             CAN.COD_CANAL,
             CAN.DES_CANAL,
             CAN.BOL_VIGENTE               AS CAN_BOL_VIGENTE,
             SBC.OID_SUBCANAL,
             SBC.COD_SUBCANAL,
             SBC.DES_SUBCANAL,
             SBC.BOL_VIGENTE               AS SBC_BOL_VIGENTE,
             SE.OID_SECTOR,
             SE.COD_SECTOR,
             SE.DES_SECTOR,
             SE.BOL_ACTIVO                 AS SE_BOL_ACTIVO,
             SE.BOL_CENTRO_PROCESO,
             SE.BOL_CONTEO,
             SE.BOL_TESORO,
             SE.BOL_PERMITE_DISPONER_VALOR,
             D.OID_DELEGACION,
             D.COD_PAIS,
             D.COD_DELEGACION,
             D.DES_DELEGACION,
             D.BOL_VIGENTE                 AS D_BOL_VIGENTE,
             D.OID_PAIS,
             D.NEC_GMT_MINUTOS,
             D.FYH_VERANO_INICIO,
             D.FYH_VERANO_FIN,
             D.NEC_VERANO_AJUSTE,
             D.DES_ZONA,
             P.OID_PLANTA,
             P.COD_PLANTA,
             P.DES_PLANTA,
             P.BOL_ACTIVO                  AS P_BOL_ACTIVO,
             TSE.BOL_ACTIVO                AS TSE_BOL_ACTIVO,
             TSE.COD_TIPO_SECTOR,
             TSE.DES_TIPO_SECTOR,
             TSE.OID_TIPO_SECTOR
        FROM SAPR_TCUENTA CU
       INNER JOIN GEPR_TCLIENTE CL
          ON CL.OID_CLIENTE = CU.OID_CLIENTE
       INNER JOIN GEPR_TSUBCANAL SBC
          ON SBC.OID_SUBCANAL = CU.OID_SUBCANAL
       INNER JOIN GEPR_TCANAL CAN
          ON CAN.OID_CANAL = SBC.OID_CANAL
       INNER JOIN GEPR_TSECTOR SE
          ON SE.OID_SECTOR = CU.OID_SECTOR
       INNER JOIN GEPR_TTIPO_SECTOR TSE
          ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR
       INNER JOIN GEPR_TPLANTA P
          ON P.OID_PLANTA = SE.OID_PLANTA
       INNER JOIN GEPR_TDELEGACION D
          ON D.OID_DELEGACION = P.OID_DELEGACION
        LEFT JOIN GEPR_TSUBCLIENTE SCL
          ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
        LEFT JOIN GEPR_TPUNTO_SERVICIO PTO
          ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
       WHERE CU.OID_CUENTA IN
             (SELECT OID OID_CUENTA
                FROM SAPR_GTT_TOIDS
               WHERE COD_CALIFICADOR = 'CUENTA');
    par$selects := sql%ROWCOUNT;
  
    RETURN var$rcCuentas;
  
  END fcargar_rc_cuentas;

  /* scargar_tt_oids_cuentas_gpr */
  PROCEDURE scargar_tt_oids_cuentas_gpr(par$oid_grupo_documento IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$inserts             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
      /* cuentas del grupo de documentos */
       (SELECT OID_CUENTA_ORIGEN,
               OID_CUENTA_DESTINO,
               OID_CUENTA_SALDO_ORIGEN,
               OID_CUENTA_SALDO_DESTINO
          FROM SAPR_TDOCUMENTO
         WHERE OID_GRUPO_DOCUMENTO = par$oid_grupo_documento
        UNION
        /* cuentas de los documentos */
        SELECT OID_CUENTA_ORIGEN,
               OID_CUENTA_DESTINO,
               OID_CUENTA_SALDO_ORIGEN,
               OID_CUENTA_SALDO_DESTINO
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO IN
               (SELECT DISTINCT OID OID_DOCUMENTO
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'DOCUMENTO'
                    OR COD_CALIFICADOR = 'DOC_PADRE'))
      SELECT DISTINCT OID_CUENTA, 'CUENTA'
        FROM (SELECT OID_CUENTA_ORIGEN OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_DESTINO OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO_ORIGEN OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO_DESTINO OID_CUENTA FROM q)
       WHERE OID_CUENTA IS NOT NULL;
  
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_cuentas_gpr;

  /* scargar_tt_oids_cuentas_docs */
  PROCEDURE scargar_tt_oids_cuentas_docs(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
      /* cuentas de los documentos */
       (SELECT OID_CUENTA_ORIGEN,
               OID_CUENTA_DESTINO,
               OID_CUENTA_SALDO_ORIGEN,
               OID_CUENTA_SALDO_DESTINO
          FROM SAPR_TDOCUMENTO
         WHERE OID_DOCUMENTO IN
               (SELECT DISTINCT OID OID_DOCUMENTO
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'DOCUMENTO'
                    OR COD_CALIFICADOR = 'DOC_PADRE'))
      SELECT DISTINCT OID_CUENTA, 'CUENTA'
        FROM (SELECT OID_CUENTA_ORIGEN OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_DESTINO OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO_ORIGEN OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO_DESTINO OID_CUENTA FROM q)
       WHERE OID_CUENTA IS NOT NULL;
  
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_cuentas_docs;

  /* scargar_tt_oids_cuentas_elem */
  PROCEDURE scargar_tt_oids_cuentas_elem(par$inserts OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    INSERT INTO SAPR_GTT_TOIDS
      (OID, COD_CALIFICADOR)
      WITH q AS
      /* cuentas de las remesas */
       (SELECT OID_CUENTA, OID_CUENTA_SALDO
          FROM SAPR_TREMESA
         WHERE OID_REMESA IN
               (SELECT DISTINCT OID OID_REMESA
                  FROM SAPR_GTT_TOIDS
                 WHERE COD_CALIFICADOR = 'REMESA')
        UNION
        /* cuentas de los bultos */
        SELECT OID_CUENTA, OID_CUENTA_SALDO
          FROM SAPR_TBULTO
         WHERE OID_BULTO IN (SELECT DISTINCT OID OID_BULTO
                               FROM SAPR_GTT_TOIDS
                              WHERE COD_CALIFICADOR = 'BULTO'))
      SELECT DISTINCT OID_CUENTA, 'CUENTA'
        FROM (SELECT OID_CUENTA OID_CUENTA
                FROM q
              UNION ALL
              SELECT OID_CUENTA_SALDO OID_CUENTA FROM q)
       WHERE OID_CUENTA IS NOT NULL;
    par$inserts := 0 + sql%ROWCOUNT;
  
  END scargar_tt_oids_cuentas_elem;

END sapr_pcuentas_###VERSION###;
/
