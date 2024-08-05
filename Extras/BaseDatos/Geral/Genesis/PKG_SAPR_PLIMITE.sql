CREATE OR REPLACE PACKAGE SAPR_PLIMITE_###VERSION### IS

  /*Version: ###VERSION_COMP###*/

  const$codFuncionalidad CONSTANT gepr_pcomon_###VERSION###.tipo$desc_ := 'SAPR_PLIMITE_###VERSION###';
  const$new_line  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(13);
  const$comilla_simple  CONSTANT  gepr_pcomon_###VERSION###.tipo$cod_ := CHR(39);

 PROCEDURE supd_limite(
                            par$oid_limite          IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_pto_serv        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$num_limite          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_);                          

PROCEDURE svalidar_limite( par$cod_ajeno           IN gepr_pcomon_###VERSION###.tipo$cod_, 
                           par$cod_divisa          IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_pto_serv        IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$num_limite          IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_);
/* Devuelve la informaci�n almacenada en SAPR_TLIMITE */
PROCEDURE srecuperar_datos(
                           par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_pto_servicio    IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$rc_datos            OUT sys_refcursor);
/*Recuperar periodos acreditados*/
PROCEDURE srecuperar_periodos_acred(
                        /*Filtros*/
                        par$aoid_estado_per     IN gepr_pcomon_###VERSION###.tipo$oids_, 
                        par$aoid_device_id      IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_planificacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_banco          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$fyh_per_ini         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$fyh_per_fin         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        /*Patron*/
                        par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        /*OUTs*/
                        par$rc_periodos         OUT SYS_REFCURSOR,
                        par$rc_validaciones     OUT SYS_REFCURSOR
                        );

END SAPR_PLIMITE_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY SAPR_PLIMITE_###VERSION### IS

PROCEDURE supd_limite(
                            par$oid_limite          IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_planificacion   IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_maquina         IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_pto_serv        IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_divisa          IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$num_limite          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$bol_activo          IN gepr_pcomon_###VERSION###.tipo$nel_,
                            par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_) IS
 var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
 var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_;
BEGIN
  
  
     IF par$oid_limite IS NULL THEN
          /*Si no existe el limite hago insert*/
          par$oid_limite := SYS_GUID();

          INSERT INTO SAPR_TLIMITE (OID_LIMITE, OID_PLANIFICACION,  OID_MAQUINA, OID_PTO_SERVICIO, OID_DIVISA, 
          NUM_LIMITE, BOL_ACTIVO, GMT_CREACION, DES_USUARIO_CREACION, GMT_MODIFICACION,
          DES_USUARIO_MODIFICACION) VALUES
          (par$oid_limite, par$oid_planificacion, par$oid_maquina, par$oid_pto_serv, par$oid_divisa, par$num_limite, par$bol_activo, 
          var$gmt_zero, par$cod_usuario, CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario);
          
     ELSE
            /*Si existe el limite hago update*/
            IF  par$oid_planificacion IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_PLANIFICACION = ']' || par$oid_planificacion ||q'[', ]';
            END IF;
            IF  par$oid_maquina IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_MAQUINA = ']' || par$oid_maquina ||q'[', ]';
            END IF;
            IF  par$oid_pto_serv IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_PTO_SERVICIO = ']' || par$oid_pto_serv ||q'[', ]';
            END IF;
            IF  par$oid_divisa IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_DIVISA = ']' || par$oid_divisa ||q'[', ]';
            END IF;
            IF  par$num_limite IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ NUM_LIMITE = ']' || par$num_limite ||q'[', ]';
            END IF;
            IF  par$bol_activo IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_ACTIVO = ']' || par$bol_activo ||q'[', ]';
            END IF;
           IF  var$qry_update IS NOT NULL  THEN
           
         
             EXECUTE IMMEDIATE q'[ UPDATE SAPR_TLIMITE SET ]' || var$qry_update ||q'[
             GMT_MODIFICACION = :1, DES_USUARIO_MODIFICACION = :2
             WHERE OID_LIMITE = :3]'
              USING CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario, par$oid_limite;
           END IF;
        END IF;

END supd_limite;

PROCEDURE svalidar_limite( par$cod_ajeno           IN gepr_pcomon_###VERSION###.tipo$cod_, 
                           par$cod_divisa          IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$cod_pto_serv        IN gepr_pcomon_###VERSION###.tipo$cod_,
                           par$num_limite          IN gepr_pcomon_###VERSION###.tipo$nel_,
                           par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_
                            ) IS
    var$entidad                     gepr_pcomon_###VERSION###.tipo$desc_:= 'LIMITE';
    var$mensaje                     gepr_pcomon_###VERSION###.tipo$desc_;
    var$mensajeEntidadValidada      gepr_pcomon_###VERSION###.tipo$desc_;
    var$oidDivisaExistente          gepr_pcomon_###VERSION###.tipo$oid_;
    var$oidPunto                    gepr_pcomon_###VERSION###.tipo$oid_;
BEGIN
  
    IF par$cod_divisa IS NOT NULL THEN
                /*Si existe el codigo de divisa, guardamos el OID_DIVISA*/
                if par$cod_ajeno IS NULL THEN

                    BEGIN
                        SELECT OID_DIVISA
                        INTO var$oidDivisaExistente
                        FROM GEPR_TDIVISA
                        WHERE COD_ISO_DIVISA = par$cod_divisa AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidDivisaExistente := NULL;
                    END;
                    
                     BEGIN
                        SELECT OID_PTO_SERVICIO
                        INTO var$oidPunto
                        FROM GEPR_TPUNTO_SERVICIO
                        WHERE COD_PTO_SERVICIO = par$cod_pto_serv AND ROWNUM = 1;
                    EXCEPTION WHEN no_data_found THEN
                        var$oidPunto := NULL;
                    END;
                ELSE
                    BEGIN
                      SELECT OID_TABLA_GENESIS 
                      INTO var$oidDivisaExistente FROM GEPR_TCODIGO_AJENO 
                      WHERE COD_TIPO_TABLA_GENESIS = 'GEPR_TDIVISA' 
                      AND upper(trim(COD_IDENTIFICADOR)) = upper(trim(par$cod_ajeno))  AND upper(trim(COD_AJENO)) = upper(trim(par$cod_divisa));
                    EXCEPTION WHEN no_data_found THEN
                        var$oidDivisaExistente := NULL;
                    END;
                    
                     BEGIN
                      SELECT OID_TABLA_GENESIS 
                      INTO var$oidPunto FROM GEPR_TCODIGO_AJENO 
                      WHERE COD_TIPO_TABLA_GENESIS = 'GEPR_TPUNTO_SERVICIO' 
                      AND upper(trim(COD_IDENTIFICADOR)) = upper(trim(par$cod_ajeno))  AND upper(trim(COD_AJENO)) = upper(trim(par$cod_pto_serv));
                    EXCEPTION WHEN no_data_found THEN
                        var$oidPunto := NULL;
                    END;
                END IF;
                /*PTO_SERV*/
                 IF var$oidPunto IS NOT NULL THEN
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                    VALUES (
                        var$oidPunto,
                        par$cod_pto_serv,
                        'OID_PTO_SERVICIO');
                 END IF;
                /*OID_DIVISA*/
                IF var$oidDivisaExistente IS NOT NULL THEN
                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CALIFICADOR)
                    VALUES (
                        var$oidDivisaExistente,
                        par$cod_divisa,
                        'OID_DIVISA');
                ELSE
                     /*Traducir palabra DIVISA*/
                    var$mensajeEntidadValidada := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'DIVISA',
                               gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                               gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                               NULL,
                               0);

                    /*Buscamos el mensaje traducido*/
                    /*2040080005 - El {0} de codigo "{1}" no existe.*/
                    var$mensaje := gepr_putilidades_###VERSION###.ftraduzir(par$cod_cultura,'2040080005',
                    gepr_pcomon_###VERSION###.const$CodFunConfigurarCliente,
                    gepr_pcomon_###VERSION###.const$codAplicacionGenesis,
                    var$mensajeEntidadValidada || '|' || par$cod_divisa,
                    0);

                    INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2, COD_CAMPO3, COD_CAMPO4,COD_CALIFICADOR)
                    VALUES (
                        '2040080005',
                        var$mensaje,
                        var$entidad,
                        par$cod_divisa,
                        'VALIDACIONES');
                END IF;
    END IF;
  
    IF par$num_limite IS NULL OR par$num_limite = 0 then
        /*No se paso ningun limite*/
        INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, COD_CAMPO2,COD_CAMPO3, COD_CAMPO4, COD_CALIFICADOR)
                       VALUES (
                        '2040080005',
                        'Limite vacio',
                        var$entidad,
                        par$num_limite,
                        'VALIDACIONES');
    END IF;

END svalidar_limite;

/* Devuelve la informaci�n almacenada en SAPR_TLIMITE */
PROCEDURE srecuperar_datos(
                           par$oid_planificacion      IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_maquina            IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_pto_servicio       IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$rc_datos               OUT sys_refcursor) IS
BEGIN     
    OPEN par$rc_datos FOR
        SELECT L.OID_LIMITE ,
        L.OID_PLANIFICACION ,
        L.OID_MAQUINA ,
        L.OID_PTO_SERVICIO ,
        L.OID_DIVISA ,
        L.NUM_LIMITE ,
        L.BOL_ACTIVO ,
        L.GMT_CREACION ,
        L.DES_USUARIO_CREACION ,
        L.GMT_MODIFICACION ,
        L.DES_USUARIO_MODIFICACION  ,
        D.COD_ISO_DIVISA ,
        D.DES_DIVISA ,
        D.COD_SIMBOLO
        FROM SAPR_TLIMITE L
        INNER JOIN GEPR_TDIVISA D ON D.OID_DIVISA = L.OID_DIVISA
        WHERE (OID_PLANIFICACION = par$oid_planificacion OR par$oid_planificacion IS NULL)
        AND (OID_MAQUINA = par$oid_maquina OR par$oid_maquina IS NULL)
        AND (OID_PTO_SERVICIO = par$oid_pto_servicio OR par$oid_pto_servicio IS NULL) 
        AND BOL_ACTIVO = 1;      
END srecuperar_datos;

/*Recuperar periodos acreditados*/
PROCEDURE srecuperar_periodos_acred(
                        /*Filtros*/
                        par$aoid_estado_per     IN gepr_pcomon_###VERSION###.tipo$oids_, 
                        par$aoid_device_id      IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_planificacion  IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$aoid_banco          IN gepr_pcomon_###VERSION###.tipo$oids_,
                        par$fyh_per_ini         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$fyh_per_fin         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        /*Patron*/
                        par$cod_usuario         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        par$cod_cultura         IN gepr_pcomon_###VERSION###.tipo$cod_,
                        /*OUTs*/
                        par$rc_periodos         OUT SYS_REFCURSOR,
                        par$rc_validaciones     OUT SYS_REFCURSOR
                        )
IS
var$query             gepr_pcomon_###VERSION###.tipo$obs_;
var$filtro            gepr_pcomon_###VERSION###.tipo$obs_;
var$query_desde       gepr_pcomon_###VERSION###.tipo$obs_;
var$query_hasta       gepr_pcomon_###VERSION###.tipo$obs_;
BEGIN

delete sapr_gtt_tauxiliar;
/* #### Inicializar los cursores #### */
OPEN par$rc_periodos FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;
OPEN par$rc_validaciones FOR SELECT NULL CODIGO FROM DUAL WHERE 1 <> 1;

var$query := 'INSERT INTO SAPR_GTT_TAUXILIAR (OID_CAMPO1, OID_CAMPO7, COD_CAMPO2, COD_CALIFICADOR )' || const$new_line;
var$query := var$query || 'SELECT' || const$new_line;
var$query := var$query || 'DISTINCT PER.OID_PERIODO, PER.OID_MAQUINA, PER.OID_PLANIFICACION, ' || const$comilla_simple || 'FILTRO_PERIODOS' || const$comilla_simple || const$new_line;
var$query := var$query || 'FROM SAPR_TPERIODO PER' || const$new_line;
var$query := var$query || 'INNER JOIN SAPR_TESTADO_PERIODO ESP ON ESP.OID_ESTADO_PERIODO = PER.OID_ESTADO_PERIODO' || const$new_line;
var$query := var$query || q'[INNER JOIN SAPR_TTIPO_PERIODO TIPE ON TIPE.OID_TIPO_PERIODO = PER.OID_TIPO_PERIODO AND TIPE.COD_TIPO_PERIODO = 'AC' ]' || const$new_line;
/*No realizo INNER JOIN si no filtramos por banco*/
IF par$aoid_banco IS NOT NULL OR par$aoid_banco.COUNT>0 THEN
    var$query := var$query || 'INNER JOIN SAPR_TPLANIFICACION PLANIF ON PLANIF.OID_PLANIFICACION = PER.OID_PLANIFICACION' || const$new_line;
END IF;
var$query := var$query ||  q'[ WHERE ESP.COD_ESTADO_PERIODO IN ('DB', 'BL' )]' || const$new_line;

IF par$fyh_per_ini IS NOT NULL OR par$fyh_per_fin IS NOT NULL THEN
 

   IF par$fyh_per_ini IS NOT NULL THEN
        var$query_desde := q'[ PER.FYH_FIN >= TO_DATE(']' || par$fyh_per_ini || q'[', 'yyyy-mm-dd hh24:mi:ss')   ]' || const$new_line;
    END IF;
    
    IF par$fyh_per_fin IS NOT NULL THEN    
        var$query_hasta := q'[ PER.FYH_INICIO <= TO_DATE(']' || par$fyh_per_fin || q'[', 'yyyy-mm-dd hh24:mi:ss') ]' || const$new_line;
    END IF;
    
    
    
    var$query := var$query || q'[ AND ( ]';
        
    IF par$fyh_per_ini IS NOT NULL THEN
        var$query := var$query || var$query_desde ;  
    END IF;
    
    IF par$fyh_per_ini IS NOT NULL AND par$fyh_per_fin IS NOT NULL THEN      
       var$query := var$query || q'[ AND ]';
    END IF;
    
    IF par$fyh_per_fin IS NOT NULL THEN
        var$query := var$query || var$query_hasta ;  
    END IF;
  
  
    var$query := var$query || q'[ ) ]';
  
END IF;

/* par$aoid_planificacion*/
IF par$aoid_planificacion IS NOT NULL AND par$aoid_planificacion.COUNT > 1  THEN                
  var$filtro := var$filtro || ' AND PER.OID_PERIODO IN (';
  
  FOR idx IN par$aoid_planificacion.first .. par$aoid_planificacion.last LOOP
    IF par$aoid_planificacion(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$aoid_planificacion(idx))), 0) > 0 THEN
      IF idx > 2 THEN
        var$filtro := var$filtro || ', ';
      END IF;
      var$filtro := var$filtro || '''' || par$aoid_planificacion(idx) || '''';
    END IF;
  END LOOP;
  var$filtro := var$filtro || ') ';
END IF;




/* par$aoid_estado_per*/
IF par$aoid_estado_per IS NOT NULL AND par$aoid_estado_per.COUNT > 1  THEN                
  var$filtro := var$filtro || ' AND PER.OID_ESTADO_PERIODO IN (';
  
  FOR idx IN par$aoid_estado_per.first .. par$aoid_estado_per.last LOOP
    IF par$aoid_estado_per(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$aoid_estado_per(idx))), 0) > 0 THEN
      IF idx > 2 THEN
        var$filtro := var$filtro || ', ';
      END IF;
      var$filtro := var$filtro || '''' || par$aoid_estado_per(idx) || '''';
    END IF;
  END LOOP;
  var$filtro := var$filtro || ') ';
END IF;


/* par$aoid_estado_per*/
IF par$aoid_device_id IS NOT NULL AND par$aoid_device_id.COUNT > 1  THEN                
  var$filtro := var$filtro || ' AND PER.OID_MAQUINA IN (';
  
  FOR idx IN par$aoid_device_id.first .. par$aoid_device_id.last LOOP
    IF par$aoid_device_id(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$aoid_device_id(idx))), 0) > 0 THEN
      IF idx > 2 THEN
        var$filtro := var$filtro || ', ';
      END IF;
      var$filtro := var$filtro || '''' || par$aoid_device_id(idx) || '''';
    END IF;
  END LOOP;
  var$filtro := var$filtro || ') ';
END IF;



/* par$aoid_estado_per*/
IF par$aoid_banco IS NOT NULL AND par$aoid_banco.COUNT > 1  THEN                
  var$filtro := var$filtro || ' AND PLANIF.OID_CLIENTE IN (';
  
  FOR idx IN par$aoid_banco.first .. par$aoid_banco.last LOOP
    IF par$aoid_banco(idx) IS NOT NULL AND NVL(LENGTH(TRIM(par$aoid_banco(idx))), 0) > 0 THEN
      IF idx > 2 THEN
        var$filtro := var$filtro || ', ';
      END IF;
      var$filtro := var$filtro || '''' || par$aoid_banco(idx) || '''';
    END IF;
  END LOOP;
  var$filtro := var$filtro || ') ';
END IF;

var$query := var$query || var$filtro;  
dbms_output.put_line('srecuperar_periodos_acred - Imprimo var$query:');
dbms_output.put_line(var$query);
EXECUTE IMMEDIATE var$query;

OPEN par$rc_periodos FOR
WITH periodos AS 
(
    SELECT
    DISTINCT
    AUX.OID_CAMPO1 AS OID_PERIODO,
    AUX.OID_CAMPO7 AS OID_MAQUINA,
    TRIM(AUX.COD_CAMPO2) AS OID_PLANIFICACION,
    MAQ.COD_IDENTIFICACION AS DEVICE_ID,
    PAI.COD_PAIS,
    PLANIF.COD_PLANIFICACION || ' - ' || PLANIF.DES_PLANIFICACION AS PLANIFICACION,
    CLI.COD_CLIENTE || ' - ' || CLI.DES_CLIENTE AS BANCO,   
    case when PER.OID_ACREDITACION IS NULL then 
        '0'
        else 
        '1'
    end AS BOL_ACREDITADO,
    ESTPER.COD_ESTADO_PERIODO AS ESTADO_PERIODO,
    PER.FYH_INICIO AS FYH_INICIO,
    PER.FYH_FIN AS FYH_FIN
    FROM SAPR_GTT_TAUXILIAR AUX
    INNER JOIN SAPR_TMAQUINA MAQ ON AUX.OID_CAMPO7 = MAQ.OID_MAQUINA
   
    INNER JOIN SAPR_TPLANIFICACION PLANIF ON PLANIF.OID_PLANIFICACION = TRIM(AUX.COD_CAMPO2)
    INNER JOIN SAPR_TPERIODO PER ON PER.OID_PERIODO = AUX.OID_CAMPO1
    INNER JOIN SAPR_TESTADO_PERIODO ESTPER ON PER.OID_ESTADO_PERIODO = ESTPER.OID_ESTADO_PERIODO
    INNER JOIN GEPR_TCLIENTE CLI ON PLANIF.OID_CLIENTE = CLI.OID_CLIENTE
     LEFT JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = MAQ.OID_SECTOR
    LEFT JOIN GEPR_TPLANTA PLA ON PLA.OID_PLANTA = SEC.OID_PLANTA
    LEFT JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PLA.OID_DELEGACION
    LEFT JOIN GEPR_TPAIS PAI ON PAI.OID_PAIS = DEL.OID_PAIS
        WHERE
    AUX.COD_CALIFICADOR = 'FILTRO_PERIODOS'
),
saldos AS 
(
    SELECT
    periodos.oid_planificacion,
            periodos.oid_maquina,
    saldo.oid_periodo,
    saldo.oid_divisa,
    SUM (saldo.num_importe) AS IMPORTE_ACTUAL
    FROM sapr_tcalculo_efectivo saldo
    INNER JOIN periodos ON saldo.oid_periodo = periodos.oid_periodo
    GROUP BY
    periodos.oid_planificacion,
    periodos.oid_maquina,
            saldo.oid_periodo,
    saldo.oid_divisa
),
limites AS 
(
    SELECT
    saldos.OID_PERIODO,
    limite.OID_LIMITE,
    limite.oid_divisa,
    limite.num_limite,
    'Planificacion' AS Tipo,
    1 AS ORDEN
    FROM sapr_tlimite limite INNER JOIN saldos ON saldos.oid_planificacion = limite.oid_planificacion AND saldos.oid_divisa = limite.oid_divisa
        WHERE
    limite.bol_activo = 1 AND saldos.IMPORTE_ACTUAL > limite.num_limite
        UNION ALL
        SELECT saldos.OID_PERIODO,
            limite.OID_LIMITE,
    limite.oid_divisa,
    limite.num_limite,
    'Maquina' AS Tipo,
    2 AS ORDEN
    FROM     sapr_tlimite limite
        INNER JOIN saldos ON saldos.oid_maquina = limite.oid_maquina AND saldos.oid_divisa = limite.oid_divisa AND limite.oid_pto_servicio IS NULL
    WHERE
    limite.bol_activo = 1 AND saldos.IMPORTE_ACTUAL > limite.num_limite
    UNION
    SELECT
    saldos.OID_PERIODO,
    limite.OID_LIMITE,
    limite.oid_divisa,
    limite.num_limite,
    'PtoServicio' AS Tipo,
    3 AS ORDEN
    FROM sapr_tlimite limite
    INNER JOIN saldos ON  saldos.oid_divisa = limite.oid_divisa
    inner join gepr_tpunto_servicio ptoser on ptoser.oid_maquina = saldos.oid_maquina
    WHERE
    limite.bol_activo = 1 AND saldos.IMPORTE_ACTUAL > limite.num_limite
    and ptoser.oid_pto_servicio = limite.oid_pto_servicio
),
prioridad AS 
(
    SELECT
    oid_periodo,
    oid_divisa,
    MAX (orden) AS orden
    FROM limites
        GROUP BY oid_periodo, oid_divisa
)
SELECT
    period.OID_PERIODO AS OID_PERIODO,
    period.OID_MAQUINA AS OID_MAQUINA, 
    period.OID_PLANIFICACION,
    PERIOD.BANCO,
    PERIOD.ESTADO_PERIODO,
    PERIOD.COD_PAIS,
    sald.oid_divisa,
    divisa.COD_ISO_DIVISA || ' - ' || divisa.des_divisa AS DIVISA,
    sald.IMPORTE_ACTUAL AS VALOR_ACTUAL,
    limits.num_limite AS LIMITE_CONFIGURADO,
    limits.Tipo AS TIPO_LIMITE,
    PERIOD.DEVICE_ID AS DEVICEID,
    PERIOD.PLANIFICACION as PLANIFICACION,
    PERIOD.BOL_ACREDITADO as BOL_ACREDITADO,
    PERIOD.FYH_INICIO AS FYH_INICIO,
    PERIOD.FYH_FIN AS FYH_FIN
FROM 
    periodos period
LEFT JOIN saldos sald ON period.oid_periodo = sald.oid_periodo AND sald.oid_maquina =  period.oid_maquina
LEFT JOIN GEPR_TDIVISA DIVISA ON DIVISA.OID_DIVISA = sald.oid_divisa
LEFT JOIN prioridad priori ON priori.oid_periodo = sald.oid_periodo AND sald.oid_divisa = priori.oid_divisa
LEFT JOIN limites limits ON limits.oid_periodo = priori.oid_periodo AND limits.oid_divisa = priori.oid_divisa AND priori.orden = limits.orden
;

OPEN par$rc_validaciones FOR SELECT 'Sucesso',var$query CODIGO FROM DUAL;
commit;

EXCEPTION
WHEN others then
    OPEN par$rc_validaciones FOR SELECT 'Error' CODIGO FROM DUAL;

END srecuperar_periodos_acred;


END SAPR_PLIMITE_###VERSION###;
/