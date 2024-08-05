CREATE OR REPLACE PACKAGE GEPR_PPAIS_###VERSION### IS

  /*Version: ###VERSION_COMP###*/
  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'GEPR_PPAIS_###VERSION###';
  const$version  CONSTANT gepr_pcomon_###VERSION###.tipo$desc_  := '###VERSION_COMP###';

  PROCEDURE srecuperar_datos(par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                             par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_paises                IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_paises                IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_delegaciones          IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_delegaciones          IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_plantas               IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_plantas               IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$rec_codigos_ajenos        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rec_historico_cambios     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rec_datos_facturacion     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rc_paises                 OUT sys_refcursor,
                             par$rc_delegaciones           OUT sys_refcursor,
                             par$rc_delegaciones_historico OUT sys_refcursor,
                             par$rc_plantas                OUT sys_refcursor,
                             par$rc_codigos_ajenos         OUT sys_refcursor,
                             par$rc_cuentas_facturacion    OUT sys_refcursor,
                             par$rc_datos_bancarios        OUT sys_refcursor,
                             par$validaciones              OUT sys_refcursor,
                             par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_);
                             
 PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_paises               IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_paises               IN gepr_pcomon_###VERSION###.tipo$descs_,                             
                             par$cod_delegaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_delegaciones         IN gepr_pcomon_###VERSION###.tipo$descs_,                             
                             par$cod_plantas              IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_plantas              IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_);   
                             
  /* Procedimiento que se encarga de recuperar el OID_PAIS según los parametros enviados
  En caso de no indicar el código de pais y sólo se encuentre un registro en la tabla
  devuelve el OID_PAIS y COD_PAIS de ese pais, caso contrario genera registros en la tabla auxiliar*/
  PROCEDURE srecuperar_pais(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                            par$oid_pais                OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_);   

  FUNCTION fhay_error RETURN BOOLEAN;                          

END GEPR_PPAIS_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY GEPR_PPAIS_###VERSION### IS

  PROCEDURE srecuperar_datos(par$info_ejecucion            IN gepr_pcomon_###VERSION###.tipo$desc_,
                             par$cod_cultura               IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_usuario               IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$cod_identificador_ajeno   IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_paises                IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_paises                IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_delegaciones          IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_delegaciones          IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_plantas               IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_plantas               IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$rec_codigos_ajenos        IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rec_historico_cambios     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rec_datos_facturacion     IN gepr_pcomon_###VERSION###.tipo$nbol_,
                             par$rc_paises                 OUT sys_refcursor,
                             par$rc_delegaciones           OUT sys_refcursor,
                             par$rc_delegaciones_historico OUT sys_refcursor,
                             par$rc_plantas                OUT sys_refcursor,
                             par$rc_codigos_ajenos         OUT sys_refcursor,
                             par$rc_cuentas_facturacion    OUT sys_refcursor,
                             par$rc_datos_bancarios        OUT sys_refcursor,
                             par$validaciones              OUT sys_refcursor,
                             par$cod_ejecucion             OUT gepr_pcomon_###VERSION###.tipo$nel_) IS

    const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_datos';

    /* LOG */
    var$inicio    TIMESTAMP := SYSTIMESTAMP;
    var$registros gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$inserts   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$updates   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$deletes   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$merges    gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$selects   gepr_pcomon_###VERSION###.tipo$nel_ := 0;
    var$err       varchar2(4000);
    var$dic_todos gepr_pcomon_###VERSION###.tipo$desc_;
    
    var$consulta_paises            clob := '';
    var$codigo_ajeno_paises        clob := '';    
    var$filtro_paises              clob := '';    
    var$consulta_delegaciones      clob := '';
    var$codigo_ajeno_delegaciones  clob := '';            
    var$filtro_delegaciones        clob := '';        
    var$consulta_plantas           clob := '';    
    var$codigo_ajeno_plantas       clob := '';    
    var$filtro_plantas             clob := '';    
    var$consulta_delegaciones_hist clob := '';
    var$consulta_codigos_ajenos    clob := '';
    var$consulta_cuentas_facturaci clob := '';
    var$consulta_datos_bancarios   clob := '';
    
    
      /* MAQUINAS */
      CURSOR cur$maquinas IS

         SELECT T.OID_CAMPO1 COD_MAQUINA
              , S.OID_SECTOR
           FROM SAPR_GTT_TAUXILIAR T
      LEFT JOIN SAPR_TMAQUINA M ON M.COD_IDENTIFICACION = T.OID_CAMPO1
      LEFT JOIN GEPR_TSECTOR S ON S.OID_SECTOR = M.OID_SECTOR
          WHERE T.COD_CALIFICADOR = 'COD_MAQUINA';                

  BEGIN

      /* ##### LIMPIAR TABLAS TEMPORARIAS ##### */
      DELETE SAPR_GTT_TAUXILIAR;

      /* #### Inicializar los cursores #### */
      OPEN par$rc_paises FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_delegaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_delegaciones_historico FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_plantas FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_codigos_ajenos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_cuentas_facturacion FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$rc_datos_bancarios FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      OPEN par$validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
      
      
      
      svalidar_codigos(par$cod_identificador_ajeno => par$cod_identificador_ajeno,
                             par$cod_paises        => par$cod_paises,
                             par$des_paises        => par$des_paises,                             
                             par$cod_delegaciones  => par$cod_delegaciones,
                             par$des_delegaciones  => par$des_delegaciones,                             
                             par$cod_plantas       => par$cod_plantas,
                             par$des_plantas       => par$des_plantas,
                             par$cod_cultura       => par$cod_cultura);
      
      
      dbms_output.put_line('1');
      IF NOT fhay_error() THEN
                    dbms_output.put_line('2');
          
              IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                var$codigo_ajeno_paises := ' LEFT JOIN GEPR_TCODIGO_AJENO COAJ_PAIS ON PAIS.OID_PAIS = COAJ_PAIS.OID_TABLA_GENESIS AND
                                             COAJ_PAIS.COD_IDENTIFICADOR = ''' || TRIM(par$cod_identificador_ajeno) || ''' AND
                                             COAJ_PAIS.COD_TIPO_TABLA_GENESIS = ''GEPR_TPAIS'' ';
                var$codigo_ajeno_delegaciones := ' LEFT JOIN GEPR_TCODIGO_AJENO COAJ_DELE ON DELE.OID_DELEGACION = COAJ_DELE.OID_TABLA_GENESIS AND
                                                   COAJ_DELE.COD_IDENTIFICADOR = ''' || TRIM(par$cod_identificador_ajeno) || ''' AND
                                                   COAJ_DELE.COD_TIPO_TABLA_GENESIS = ''GEPR_TDELEGACION'' ';
                var$codigo_ajeno_plantas := ' LEFT JOIN GEPR_TCODIGO_AJENO COAJ_PLAN ON PLAN.OID_PLANTA = COAJ_PLAN.OID_TABLA_GENESIS AND
                                              COAJ_PLAN.COD_IDENTIFICADOR = ''' || TRIM(par$cod_identificador_ajeno) || ''' AND
                                              COAJ_PLAN.COD_TIPO_TABLA_GENESIS = ''GEPR_TPLANTA'' ';
              END IF;

              /* par$cod_paises*/
              IF par$cod_paises IS NOT NULL AND par$cod_paises.COUNT > 1  THEN
                IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                  var$filtro_paises := var$filtro_paises || ' AND COAJ_PAIS.COD_AJENO IN (';
                ELSE
                  var$filtro_paises := var$filtro_paises || ' AND PAIS.COD_PAIS IN (';
                END IF;
                FOR idx IN par$cod_paises.first .. par$cod_paises.last LOOP
                  IF par$cod_paises(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_paises(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_paises := var$filtro_paises || ', ';
                    END IF;
                    var$filtro_paises := var$filtro_paises || '''' || par$cod_paises(idx) || '''';
                  END IF;
                END LOOP;
                var$filtro_paises := var$filtro_paises || ') ';
              END IF;
              
              /* par$des_paises*/
              IF par$des_paises IS NOT NULL AND par$des_paises.COUNT > 1  THEN
                var$filtro_paises := var$filtro_paises || ' AND (';
                FOR idx IN par$des_paises.first .. par$des_paises.last LOOP
                  IF par$des_paises(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$des_paises(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_paises := var$filtro_paises || ' OR ';
                    END IF;
                    IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                      var$filtro_paises := var$filtro_paises || 'UPPER(COAJ_PAIS.DES_AJENO) LIKE UPPER(''' || par$des_paises(idx) || '%'')';
                    ELSE
                      var$filtro_paises := var$filtro_paises || 'UPPER(PAIS.DES_PAIS) LIKE UPPER(''' || par$des_paises(idx) || '%'')';
                    END IF;
                  END IF;
                END LOOP;
                var$filtro_paises := var$filtro_paises || ') ';
              END IF;

              /* par$cod_delegaciones*/
              IF par$cod_delegaciones IS NOT NULL AND par$cod_delegaciones.COUNT > 1  THEN
                IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                  var$filtro_delegaciones := var$filtro_delegaciones || ' AND COAJ_DELE.COD_AJENO IN (';
                ELSE
                  var$filtro_delegaciones := var$filtro_delegaciones || ' AND DELE.COD_DELEGACION IN (';
                END IF;
                FOR idx IN par$cod_delegaciones.first .. par$cod_delegaciones.last LOOP
                  IF par$cod_delegaciones(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_delegaciones(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_delegaciones := var$filtro_delegaciones || ', ';
                    END IF;
                    var$filtro_delegaciones := var$filtro_delegaciones || '''' || par$cod_delegaciones(idx) || '''';
                  END IF;
                END LOOP;
                var$filtro_delegaciones := var$filtro_delegaciones || ') ';
              END IF;
              
              /* par$des_delegaciones*/
              IF par$des_delegaciones IS NOT NULL AND par$des_delegaciones.COUNT > 1  THEN
                var$filtro_delegaciones := var$filtro_delegaciones || ' AND (';
                FOR idx IN par$des_delegaciones.first .. par$des_delegaciones.last LOOP
                  IF par$des_delegaciones(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$des_delegaciones(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_delegaciones := var$filtro_delegaciones || ' OR ';
                    END IF;
                    IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                      var$filtro_delegaciones := var$filtro_delegaciones || 'UPPER(COAJ_DELE.DES_AJENO) LIKE UPPER(''' || par$des_delegaciones(idx) || '%'')';
                    ELSE
                      var$filtro_delegaciones := var$filtro_delegaciones || 'UPPER(DELE.DES_DELEGACION) LIKE UPPER(''' || par$des_delegaciones(idx) || '%'')';
                    END IF;
                  END IF;
                END LOOP;
                var$filtro_delegaciones := var$filtro_delegaciones || ') ';
              END IF;
              
              /* par$cod_plantas*/
              IF par$cod_plantas IS NOT NULL AND par$cod_plantas.COUNT > 1  THEN
                IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                  var$filtro_plantas := var$filtro_plantas || ' AND COAJ_PLAN.COD_AJENO IN (';
                ELSE
                  var$filtro_plantas := var$filtro_plantas || ' AND PLAN.COD_PLANTA IN (';
                END IF;        
                FOR idx IN par$cod_plantas.first .. par$cod_plantas.last LOOP
                  IF par$cod_plantas(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_plantas(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_plantas := var$filtro_plantas || ', ';
                    END IF;
                    var$filtro_plantas := var$filtro_plantas || '''' || par$cod_plantas(idx) || '''';
                  END IF;
                END LOOP;
                var$filtro_plantas := var$filtro_plantas || ') ';
              END IF;
              
              /* par$des_plantas*/
              IF par$des_plantas IS NOT NULL AND par$des_plantas.COUNT > 1  THEN
                var$filtro_plantas := var$filtro_plantas || ' AND (';
                FOR idx IN par$des_plantas.first .. par$des_plantas.last LOOP
                  IF par$des_plantas(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$des_plantas(idx))), 0) > 0 THEN
                    IF idx > 2 THEN
                      var$filtro_plantas := var$filtro_plantas || ' OR ';
                    END IF;
                    IF par$cod_identificador_ajeno IS NOT NULL AND NVL(LENGTH(TRIM(par$cod_identificador_ajeno)), 0) > 0 THEN
                      var$filtro_plantas := var$filtro_plantas || 'UPPER(COAJ_PLAN.DES_AJENO) LIKE UPPER(''' || par$des_plantas(idx) || '%'')';
                    ELSE
                      var$filtro_plantas := var$filtro_plantas || 'UPPER(PLAN.DES_PLANTA) LIKE UPPER(''' || par$des_plantas(idx) || '%'')';
                    END IF;            
                  END IF;
                END LOOP;
                var$filtro_plantas := var$filtro_plantas || ') ';
              END IF;
              
              var$consulta_paises := '
                    SELECT PAIS.OID_PAIS,
                           PAIS.COD_PAIS,
                           PAIS.DES_PAIS,
                           PAIS.BOL_ACTIVO
                      FROM GEPR_TPAIS PAIS
                           ' || var$codigo_ajeno_paises || '
                     WHERE 1=1 ' || var$filtro_paises;

              OPEN par$rc_paises FOR
                var$consulta_paises;
                
              var$consulta_delegaciones := '
                    SELECT PAIS.OID_PAIS,
                           DELE.OID_DELEGACION,
                           DELE.COD_DELEGACION,
                           DELE.DES_DELEGACION,
                           DELE.BOL_VIGENTE,
                           DELE.NEC_GMT_MINUTOS,
                           DELE.FYH_VERANO_INICIO,
                           DELE.FYH_VERANO_FIN,
                           DELE.NEC_VERANO_AJUSTE,
                           DELE.DES_ZONA
                      FROM GEPR_TPAIS PAIS
                INNER JOIN GEPR_TDELEGACION DELE ON PAIS.OID_PAIS = DELE.OID_PAIS 
                           ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || '
                     WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones;

                IF var$filtro_paises is not null THEN
                    var$consulta_delegaciones:=  var$consulta_delegaciones ||   ' AND DELE.OID_PAIS IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR =   ''' || 'OID_PAISES' || ''')';
                END IF;
                    
                IF var$filtro_plantas is not null THEN
                   var$consulta_delegaciones:=  var$consulta_delegaciones ||   ' AND DELE.OID_DELEGACION IN (SELECT DISTINCT OID_DELEGACION FROM GEPR_TPLANTA WHERE OID_PLANTA IN (SELECT OID_CAMPO1 FROM SAPR_GTT_TAUXILIAR WHERE COD_CALIFICADOR =   ''' || 'OID_PLANTAS' || '''))'; -- '') '';  
                END IF;

              OPEN par$rc_delegaciones FOR
                var$consulta_delegaciones;
                
              var$consulta_plantas := '
                    SELECT DELE.OID_DELEGACION,
                           PLAN.OID_PLANTA,
                           PLAN.COD_PLANTA,
                           PLAN.DES_PLANTA,
                           PLAN.BOL_ACTIVO
                      FROM GEPR_TPAIS PAIS
                INNER JOIN GEPR_TDELEGACION DELE ON PAIS.OID_PAIS = DELE.OID_PAIS
                INNER JOIN GEPR_TPLANTA PLAN ON DELE.OID_DELEGACION = PLAN.OID_DELEGACION
                           ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || var$codigo_ajeno_plantas || '
                     WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones || var$filtro_plantas;
               
              OPEN par$rc_plantas FOR
                var$consulta_plantas;
                        
              IF par$rec_codigos_ajenos = '1' THEN
                
                var$consulta_codigos_ajenos := '
                      SELECT COAJ.OID_CODIGO_AJENO,
                             COAJ.COD_AJENO,
                             COAJ.DES_AJENO,
                             COAJ.BOL_ACTIVO,
                             COAJ.COD_IDENTIFICADOR,
                             COAJ.BOL_DEFECTO,
                             COAJ.COD_TIPO_TABLA_GENESIS,
                             COAJ.OID_TABLA_GENESIS
                        FROM GEPR_TCODIGO_AJENO COAJ
                  INNER JOIN GEPR_TPAIS PAIS ON COAJ.COD_TIPO_TABLA_GENESIS = ''GEPR_TPAIS'' AND
                                                COAJ.OID_TABLA_GENESIS = PAIS.OID_PAIS
                             ' || var$codigo_ajeno_paises || '
                       WHERE 1=1 ' || var$filtro_paises || '
                       UNION
                      SELECT COAJ.OID_CODIGO_AJENO,
                             COAJ.COD_AJENO,
                             COAJ.DES_AJENO,
                             COAJ.BOL_ACTIVO,
                             COAJ.COD_IDENTIFICADOR,
                             COAJ.BOL_DEFECTO,
                             COAJ.COD_TIPO_TABLA_GENESIS,
                             COAJ.OID_TABLA_GENESIS
                        FROM GEPR_TCODIGO_AJENO COAJ
                  INNER JOIN GEPR_TDELEGACION DELE ON COAJ.COD_TIPO_TABLA_GENESIS = ''GEPR_TDELEGACION'' AND
                                                      COAJ.OID_TABLA_GENESIS = DELE.OID_DELEGACION
                  INNER JOIN GEPR_TPAIS PAIS ON DELE.OID_PAIS = PAIS.OID_PAIS
                             ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || '
                       WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones || '
                       UNION
                      SELECT COAJ.OID_CODIGO_AJENO,
                             COAJ.COD_AJENO,
                             COAJ.DES_AJENO,
                             COAJ.BOL_ACTIVO,
                             COAJ.COD_IDENTIFICADOR,
                             COAJ.BOL_DEFECTO,
                             COAJ.COD_TIPO_TABLA_GENESIS,
                             COAJ.OID_TABLA_GENESIS
                        FROM GEPR_TCODIGO_AJENO COAJ
                  INNER JOIN GEPR_TPLANTA PLAN ON COAJ.COD_TIPO_TABLA_GENESIS = ''GEPR_TPLANTA'' AND
                                                  COAJ.OID_TABLA_GENESIS = PLAN.OID_PLANTA
                  INNER JOIN GEPR_TDELEGACION DELE ON PLAN.OID_DELEGACION = DELE.OID_DELEGACION
                  INNER JOIN GEPR_TPAIS PAIS ON DELE.OID_PAIS = PAIS.OID_PAIS
                             ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || var$codigo_ajeno_plantas || '
                       WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones || var$filtro_plantas;
                   
                OPEN par$rc_codigos_ajenos FOR
                  var$consulta_codigos_ajenos;
              
              END IF;
              
              IF par$rec_historico_cambios = '1' THEN
                
                var$consulta_delegaciones_hist := '
                      SELECT DELE.OID_DELEGACION,
                             HIST.OID_HIST_DELEGACION,
                             HIST.COD_DELEGACION,
                             HIST.DES_DELEGACION,
                             HIST.BOL_VIGENTE,
                             HIST.NEC_GMT_MINUTOS,
                             HIST.FYH_VERANO_INICIO,
                             HIST.FYH_VERANO_FIN,
                             HIST.NEC_VERANO_AJUSTE,
                             HIST.DES_ZONA,
                             HIST.FYH_ACCION
                        FROM GEPR_TPAIS PAIS
                  INNER JOIN GEPR_TDELEGACION DELE ON PAIS.OID_PAIS = DELE.OID_PAIS 
                  INNER JOIN GEPR_THIST_DELEGACION HIST ON DELE.OID_DELEGACION = HIST.OID_DELEGACION
                             ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || '
                       WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones;
                     
                OPEN par$rc_delegaciones_historico FOR
                  var$consulta_delegaciones_hist;
                  
              END IF;
              
              IF par$rec_datos_facturacion = '1' THEN

                var$consulta_cuentas_facturaci := '
                      SELECT DELE.OID_DELEGACION,
                             FACT.OID_DELEGACIONXCONFIG_FACTUR,
                             CLIE.COD_CLIENTE COD_CLIENTE_CAPITAL,
                             CLIE.DES_CLIENTE DES_CLIENTE_CAPITAL,                     
                             SUCL.COD_SUBCLIENTE COD_SUBCLIENTE_TESORERIA,
                             SUCL.DES_SUBCLIENTE DES_SUBCLIENTE_TESORERIA,                     
                             PUSE.COD_PTO_SERVICIO COD_PTO_SERVICIO_TESORERIA,
                             PUSE.DES_PTO_SERVICIO DES_PTO_SERVICIO_TESORERIA
                        FROM GEPR_TPAIS PAIS
                  INNER JOIN GEPR_TDELEGACION DELE ON PAIS.OID_PAIS = DELE.OID_PAIS 
                  INNER JOIN GEPR_TDELEGACIONXCONFIG_FACTUR FACT ON DELE.OID_DELEGACION = FACT.OID_DELEGACION
                  INNER JOIN GEPR_TCLIENTE CLIE ON FACT.OID_CLIENTE_CAPITAL = CLIE.OID_CLIENTE
                  INNER JOIN GEPR_TSUBCLIENTE SUCL ON FACT.OID_SUBCLIENTE_TESORERIA = SUCL.OID_SUBCLIENTE
                  INNER JOIN GEPR_TPUNTO_SERVICIO PUSE ON FACT.OID_PTO_SERVICIO_TESORERIA = PUSE.OID_PTO_SERVICIO
                             ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || '
                       WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones;        
                
                OPEN par$rc_cuentas_facturacion FOR
                  var$consulta_cuentas_facturaci;
                  
                var$consulta_datos_bancarios := '
                      SELECT CLIE.COD_CLIENTE || ''#'' || SUCL.COD_SUBCLIENTE || ''#'' || PUSE.COD_PTO_SERVICIO OID_COMBINATORIA,
                             DELE.OID_DELEGACION,
                             DABA.OID_DATO_BANCARIO,
                             BANC.COD_CLIENTE COD_BANCO,
                             NULL COD_AGENCIA, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             DABA.COD_CUENTA_BANCARIA,
                             DABA.COD_TIPO_CUENTA_BANCARIA,
                             DABA.COD_DOCUMENTO,
                             DABA.DES_TITULARIDAD,
                             DIVI.COD_ISO_DIVISA,
                             DABA.DES_OBSERVACIONES,
                             DABA.BOL_DEFECTO,
                             DABA.BOL_ACTIVO,
                             NULL DES_CAMPO_ADICIONAL_1, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_2, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_3, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_4, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_5, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_6, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216
                             NULL DES_CAMPO_ADICIONAL_7, -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216                                                                                                         
                             NULL DES_CAMPO_ADICIONAL_8 -- CAMBIAR AL EJECUTAR LA HISTORIA PGP-216                     
                        FROM GEPR_TPAIS PAIS
                  INNER JOIN GEPR_TDELEGACION DELE ON PAIS.OID_PAIS = DELE.OID_PAIS 
                  INNER JOIN GEPR_TDELEGACIONXCONFIG_FACTUR FACT ON DELE.OID_DELEGACION = FACT.OID_DELEGACION
                  INNER JOIN GEPR_TCLIENTE CLIE ON FACT.OID_CLIENTE_CAPITAL = CLIE.OID_CLIENTE
                  INNER JOIN GEPR_TSUBCLIENTE SUCL ON FACT.OID_SUBCLIENTE_TESORERIA = SUCL.OID_SUBCLIENTE
                  INNER JOIN GEPR_TPUNTO_SERVICIO PUSE ON FACT.OID_PTO_SERVICIO_TESORERIA = PUSE.OID_PTO_SERVICIO
                  INNER JOIN SAPR_TDATO_BANCARIO DABA ON PUSE.OID_PTO_SERVICIO = DABA.OID_PTO_SERVICIO
                  INNER JOIN GEPR_TCLIENTE BANC ON DABA.OID_BANCO = BANC.OID_CLIENTE
                  INNER JOIN GEPR_TDIVISA DIVI ON DABA.OID_DIVISA = DIVI.OID_DIVISA
                             ' || var$codigo_ajeno_paises || var$codigo_ajeno_delegaciones || '
                       WHERE 1=1 ' || var$filtro_paises || var$filtro_delegaciones;  
                
                OPEN par$rc_datos_bancarios FOR
                  var$consulta_datos_bancarios;
          
         END IF;
    END IF;
    
    
      /* Validaciones */
      OPEN par$validaciones FOR
           SELECT OID_CAMPO1 AS CODIGO, COD_CAMPO2 AS DESCRIPCION
             FROM SAPR_GTT_TAUXILIAR
            WHERE COD_CALIFICADOR = 'VALIDACIONES';



    var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
    gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                     const$nombre_func,
                                                     var$inserts,
                                                     var$updates,
                                                     var$deletes,
                                                     var$merges,
                                                     var$selects,
                                                     var$registros,
                                                     var$inicio,
                                                     null,
                                                     'C',
                                                     par$info_ejecucion,
                                                     var$err,
                                                     par$cod_usuario,
                                                     par$cod_ejecucion);
                                                     
    COMMIT;

  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
      
      var$err := gepr_putilidades_###VERSION###.fformat_error;
      raise_application_error(gepr_pcomon_###VERSION###.const$codCustomError, var$err, true);
      var$registros := var$inserts + var$updates + var$deletes + var$merges + var$selects;
      gepr_putilidades_###VERSION###.sins_tlog_ejecucion_trn_in(const$codFuncionalidad || '.' ||
                                                       const$nombre_func,
                                                       var$inserts,
                                                       var$updates,
                                                       var$deletes,
                                                       var$merges,
                                                       var$selects,
                                                       var$registros,
                                                       var$inicio,
                                                       null,
                                                       'R',
                                                       par$info_ejecucion,
                                                       var$err,
                                                       par$cod_usuario,
                                                       par$cod_ejecucion);

  END srecuperar_datos;
  
  
  
  
  
   PROCEDURE svalidar_codigos(par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                             par$cod_paises               IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_paises               IN gepr_pcomon_###VERSION###.tipo$descs_,                             
                             par$cod_delegaciones         IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_delegaciones         IN gepr_pcomon_###VERSION###.tipo$descs_,                             
                             par$cod_plantas              IN gepr_pcomon_###VERSION###.tipo$cods_,
                             par$des_plantas              IN gepr_pcomon_###VERSION###.tipo$descs_,
                             par$cod_cultura              IN gepr_pcomon_###VERSION###.tipo$cod_) IS
      var$oid_auxiliar     gepr_pcomon_###VERSION###.tipo$oid_;

    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN     
     IF par$cod_identificador_ajeno IS NULL THEN
        /* PAISES */
        IF par$cod_paises IS NOT NULL AND par$cod_paises.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS PAISES */
           FOR idx IN par$cod_paises.first .. par$cod_paises.last LOOP
             IF par$cod_paises(idx) IS NOT NULL THEN            
                
             
                BEGIN
                    SELECT PA.OID_PAIS
                      INTO var$oid_auxiliar
                      FROM GEPR_TPAIS PA
                    WHERE PA.COD_PAIS = par$cod_paises(idx);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_PAISES');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                    dbms_output.put_line('No fue posible encontrar un pais con el codigo Genesis: '|| par$cod_paises(idx) );
                    /*No fue posible encontrar un pais con el codigo Genesis: {0}*/
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040160003', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160003',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                     par$cod_paises(idx),
                                                     0), 'VALIDACIONES');

                END;
                  
             ELSIF par$des_paises(idx) IS NOT NULL THEN  
                           
                          
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
               SELECT PA.OID_PAIS, 'OID_PAISES'
                      FROM GEPR_TPAIS PA
                    WHERE PA.DES_PAIS LIKE par$des_paises(idx) || '%' ;
              
             END IF;
           END LOOP;
        END IF;     
         
        
        /* DELEGACIONES */
        IF par$cod_delegaciones IS NOT NULL AND par$cod_delegaciones.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS DELEGACIONES */
           FOR idx IN par$cod_delegaciones.first .. par$cod_delegaciones.last LOOP
             IF par$cod_delegaciones(idx) IS NOT NULL THEN         
               
               BEGIN
                    SELECT DE.OID_DELEGACION
                      INTO var$oid_auxiliar
                      FROM GEPR_TDELEGACION DE
                    WHERE DE.COD_DELEGACION = par$cod_delegaciones(idx);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_DELEGACIONES');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                   
                     dbms_output.put_line('No fue posible encontrar una delegación con el codigo Genesis: '|| par$cod_delegaciones(idx) );
                     
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040120005', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160004',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                     par$cod_delegaciones(idx),
                                                     0), 'VALIDACIONES');

                END;
                
             ELSIF par$des_delegaciones(idx) IS NOT NULL THEN  
                            
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
               SELECT DE.OID_DELEGACION, 'OID_DELEGACIONES'
                      FROM GEPR_TDELEGACION DE
                    WHERE DE.DES_DELEGACION LIKE par$des_delegaciones(idx) || '%' ;
               END IF;
           END LOOP;
        END IF;     
        
        /* PLANTAS */
        IF par$cod_plantas IS NOT NULL AND par$cod_plantas.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS PLANTAS */
           FOR idx IN par$cod_plantas.first .. par$cod_plantas.last LOOP
             IF par$cod_plantas(idx) IS NOT NULL THEN 
             
                  BEGIN
                    SELECT PL.OID_PLANTA
                      INTO var$oid_auxiliar
                      FROM GEPR_TPLANTA PL
                    WHERE PL.COD_PLANTA = par$cod_plantas(idx);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_PLANTAS');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                    
                     dbms_output.put_line('No fue posible encontrar una planta con el codigo Genesis: '|| par$cod_plantas(idx) );
                         INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040120006', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160005',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                     par$cod_plantas(idx),
                                                     0), 'VALIDACIONES');

                END;
                  
             ELSIF par$des_plantas(idx) IS NOT NULL THEN  
                                             
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
               SELECT PL.OID_PLANTA, 'OID_PLANTAS'
                      FROM GEPR_TPLANTA PL
                    WHERE PL.DES_PLANTA LIKE par$des_plantas(idx) || '%' ;
               
             END IF;
           END LOOP;
        END IF;      
      
      ELSE      
      
        /* PAISES */
        IF par$cod_paises IS NOT NULL AND par$cod_paises.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LOS PAISES */
           FOR idx IN par$cod_paises.first .. par$cod_paises.last LOOP
             IF par$cod_paises(idx) IS NOT NULL THEN            
                
             
                BEGIN
                  
                    SELECT CA.OID_TABLA_GENESIS
                    INTO var$oid_auxiliar
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_paises(idx)
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS';
                     
                     

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_PAISES');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                    /*NÃO FOI POSSIVEL ENCONTRAR DELEGAÇÃO*/
                      INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040160006', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160006',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                      par$cod_paises(idx) || '|' || par$cod_identificador_ajeno || '|' || 'GEPR_TPAIS',
                                                     0), 'VALIDACIONES');

                END;
                  
             ELSIF par$des_paises(idx) IS NOT NULL THEN  
                           
                          
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
               
                  SELECT CA.OID_TABLA_GENESIS, 'OID_PAISES'
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.DES_AJENO =  par$des_paises(idx) || '%'
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS';
                     
              
             END IF;
           END LOOP;
        END IF;     
         
        
        /* DELEGACIONES */
        IF par$cod_delegaciones IS NOT NULL AND par$cod_delegaciones.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS DELEGACIONES */
           FOR idx IN par$cod_delegaciones.first .. par$cod_delegaciones.last LOOP
             IF par$cod_delegaciones(idx) IS NOT NULL THEN         
               
               BEGIN
                                     
                    SELECT CA.OID_TABLA_GENESIS
                    INTO var$oid_auxiliar
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_delegaciones(idx)
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION';
                     

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_DELEGACIONES');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                    /*NÃO FOI POSSIVEL ENCONTRAR DELEGAÇÃO*/
                     INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040160006', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160006',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                      par$cod_delegaciones(idx) || '|' || par$cod_identificador_ajeno || '|' || 'GEPR_TDELEGACION',
                                                     0), 'VALIDACIONES');

                END;                
                
             ELSIF par$des_delegaciones(idx) IS NOT NULL THEN  
                            
               INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
               
               
                  SELECT CA.OID_TABLA_GENESIS, 'OID_DELEGACIONES'
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.DES_AJENO =  par$des_delegaciones(idx) || '%'
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION';
                     
                     
               END IF;
           END LOOP;
        END IF;     
        
        /* PLANTAS */
        IF par$cod_plantas IS NOT NULL AND par$cod_plantas.COUNT > 1  THEN
           /* INSERT EN LA TABLA TEMPORARIA LOS CODIGOS DE LAS PLANTAS */
           FOR idx IN par$cod_plantas.first .. par$cod_plantas.last LOOP
             IF par$cod_plantas(idx) IS NOT NULL THEN 
             
                  BEGIN


                    SELECT CA.OID_TABLA_GENESIS
                    INTO var$oid_auxiliar
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.COD_AJENO = par$cod_plantas(idx)
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA';
                     
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
                    VALUES (var$oid_auxiliar, 'OID_PLANTAS');
                    
                EXCEPTION
                  WHEN no_data_found THEN
                    var$oid_auxiliar := NULL;
                    /*NÃO FOI POSSIVEL ENCONTRAR DELEGAÇÃO*/
                   
                     INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040160006', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160006',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                      par$cod_plantas(idx) || '|' || par$cod_identificador_ajeno || '|' || 'GEPR_TPLANTA',
                                                     0), 'VALIDACIONES');

                END;
                  
             ELSIF par$des_plantas(idx) IS NOT NULL THEN  
                                             
              INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CALIFICADOR)
              
                  SELECT CA.OID_TABLA_GENESIS, 'OID_PLANTAS'
                    FROM GEPR_TCODIGO_AJENO CA
                   WHERE CA.DES_AJENO =  par$des_plantas(idx) || '%'
                     AND CA.COD_IDENTIFICADOR = par$cod_identificador_ajeno
                     AND CA.COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA';
                     
               
             END IF;
           END LOOP;
        END IF;
             
      END IF;
      
        
      --VALIDAR SE TEM ALGUM INSERIDO
    var$existe := 0;
    BEGIN
      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR
       WHERE COD_CALIFICADOR IN ('OID_PAISES', 'OID_DELEGACIONES','OID_PLANTAS');

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    IF var$existe = 0 THEN
       INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                      VALUES ('2040160007', gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,
                                                     '2040160007',
                                                     'GENERICO',
                                                     gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                                                      null,
                                                     0), 'VALIDACIONES');
      END IF;



       
  END svalidar_codigos;
  
  
  PROCEDURE srecuperar_pais(par$oid_llamada             IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_identificador_ajeno IN gepr_pcomon_###VERSION###.tipo$cod_,
                            par$cod_pais                IN OUT gepr_pcomon_###VERSION###.tipo$cod_,
                            par$oid_pais                OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$cod_cultura             IN gepr_pcomon_###VERSION###.tipo$cod_) IS 

  const$nombre_func CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'srecuperar_pais';
  var$mensaje   gepr_pcomon_###VERSION###.tipo$desc_;
  BEGIN
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                                  par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                                  par$des_version     => const$version,
                                  par$des_detalle     => 'Se informa el código de país: ' || par$cod_pais,
                                  par$cod_identificador   => ''); 
    SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                          par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                          par$des_version     => const$version,
                          par$des_detalle     => 'Se informa el código ajeno: ' || par$cod_identificador_ajeno,
                          par$cod_identificador   => ''); 

    IF par$cod_pais IS NOT NULL THEN
      BEGIN
        IF par$cod_identificador_ajeno IS NOT NULL THEN
          SELECT PAIS.OID_PAIS, PAIS.COD_PAIS INTO par$oid_pais, par$cod_pais FROM GEPR_TCODIGO_AJENO AJEN
          INNER JOIN GEPR_TPAIS PAIS ON PAIS.OID_PAIS = AJEN.OID_TABLA_GENESIS 
          AND AJEN.COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS' 
          AND AJEN.COD_IDENTIFICADOR = par$cod_identificador_ajeno
          WHERE AJEN.COD_AJENO = par$cod_pais AND PAIS.BOL_ACTIVO = 1 AND AJEN.BOL_ACTIVO = 1;
        ELSE
          SELECT OID_PAIS INTO par$oid_pais FROM GEPR_TPAIS
          WHERE COD_PAIS = par$cod_pais AND BOL_ACTIVO = 1;
        END IF;
      EXCEPTION
        WHEN no_data_found THEN
          par$oid_pais := null;
          par$cod_pais := null;
          /*Informar que no existe el codigo de pais*/
          /*Buscamos el mensaje traducido*/
          /*2040010027 - El código de país informado no existe. */
          var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040010027',
          gepr_pcomon_###VERSION###.const$CodFunGenerico,
          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
          '',
          0);

          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES ('2040010027', var$mensaje, 'VALIDACIONES');

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     => 'Validación 2040010027: ' || var$mensaje,
                      par$cod_identificador   => ''); 
      END;
    ELSE
      BEGIN
          SELECT OID_PAIS, COD_PAIS INTO par$oid_pais, par$cod_pais FROM GEPR_TPAIS WHERE BOL_ACTIVO = 1;

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
                      par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
                      par$des_version     => const$version,
                      par$des_detalle     => 'Se regresa el oid_pais: ' || par$oid_pais || ' y el cod_pais: ' || par$cod_pais,
                      par$cod_identificador   => ''); 

      EXCEPTION
        WHEN too_many_rows THEN
          par$oid_pais := null;
          par$cod_pais := null;
          
          /* Si hay mas de un pais configurado en la base debe enviar el codigo*/
          /*Buscamos el mensaje traducido*/
          /*2040010026 - Debe indicar el código de país. */
          var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040010026',
          gepr_pcomon_###VERSION###.const$CodFunGenerico,
          gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
          '',
          0);
               
          INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
          VALUES ('2040010026', var$mensaje,'VALIDACIONES');

          SAPR_PLOG_API.SAGREGA_DETALLE(par$oid_llamada  => par$oid_llamada,
            par$des_origen      => const$codFuncionalidad || '.' || const$nombre_func,
            par$des_version     => const$version,
            par$des_detalle     => 'Validación 2040010026: ' || var$mensaje,
            par$cod_identificador   => ''); 
      END;
    END IF;
  END srecuperar_pais;

   FUNCTION fhay_error RETURN BOOLEAN IS
    var$existe gepr_pcomon_###VERSION###.tipo$nel_ := 0;
  BEGIN

    BEGIN

      SELECT COUNT(1)
        INTO var$existe
        FROM SAPR_GTT_TAUXILIAR A
       WHERE A.COD_CALIFICADOR = 'VALIDACIONES' and A.OID_CAMPO1 = '2040160007';

    EXCEPTION WHEN no_data_found THEN
      var$existe := 0;
    END;

    RETURN  gepr_putilidades_###VERSION###.fmayor_que_cero(var$existe);

  END fhay_error;


END GEPR_PPAIS_###VERSION###;
/