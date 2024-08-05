CREATE OR REPLACE PROCEDURE SP_SALDOS_V2(  p_com_delegacion IN varchar2,
                                                                                                     p_detalhar_divisa IN number DEFAULT 0,
                                                                                                     p_com_cliente IN varchar2,
                                                                                                     p_detalhar_sector IN number DEFAULT 0,
                                                                                                     p_com_divisa IN varchar2,
                                                                                                     p_com_planta IN varchar2,
                                                                                                     p_com_sector IN varchar2,
                                                                                                     p_com_subcliente IN varchar2,
                                                                                                     p_com_pto_servicio IN varchar2,
                                                                                                     p_com_canal IN varchar2,
                                                                                                     p_com_subcanal IN varchar2,
                                                                                                     p_com_denominacion IN varchar2,
                                                                                                     p_com_medio_pago IN varchar2,
                                                                                                     p_considerar_total_medio_pago IN number DEFAULT 0,
                                                                                                     p_filtro_denominacion IN number DEFAULT 0,
                                                                                                     p_considerar_total_efectivo IN number DEFAULT 0,
                                                                                                     p_filtro_medio_pago IN number DEFAULT 0,
                                                                                                     p_cliente_sem_saldo IN number DEFAULT 0,
                                                                                                     p_consid_efectivo_medio_pago IN number DEFAULT 0,
                                                                                                     p_considerar_todos_niveis IN number DEFAULT 0,
                                                                                                     p_retorno_saldos OUT sys_refcursor,
                                                                                                     p_cabecera IN varchar2  DEFAULT 'N'
                                                                                                                  ) IS

v_sql varchar2(30000);
v_sql_where_bus_cuenta varchar2(30000);
v_des_control_error varchar2(100);
v_sql_where_sector varchar2(30000);
vv_todos varchar2(30000) := '-1';
--Recupero de la Descripcion de los parametros
l_sql varchar2(2000);
l_list_sector varchar2(32000);
l_list_delegacion varchar2(32000);
l_list_planta varchar2(32000);
l_list_divisa varchar2(32000);
l_list_canal varchar2(32000);
l_list_subcanal varchar2(32000);
BEGIN



  -- LIMPANDO TABELAS TEMPORARIAS
  execute immediate 'TRUNCATE TABLE RPGE_GTT_TDATOS_CUENTA';
  execute immediate 'TRUNCATE TABLE RPGE_GTT_TSALDOS_V2';

 v_des_control_error := 'MONTANDO CLAUSULA WHERE RECUPERAR CUENTA';


  IF p_com_cliente <> ' ' OR p_com_subcliente <> ' ' OR P_COM_PTO_SERVICIO <> ' ' THEN
    v_sql_where_bus_cuenta := 'AND (';
  END IF;

  IF p_com_cliente <> ' ' THEN
    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || '(';
    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' CU.OID_CLIENTE IN (' || chr(39) || P_COM_CLIENTE || chr(39) || ')';
    IF p_considerar_todos_niveis = 0 THEN
      v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND CU.OID_SUBCLIENTE IS NULL AND CU.OID_PTO_SERVICIO IS NULL ';
    END IF;
    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta ||')';
  END IF;
  IF p_com_subcliente <> ' ' THEN

    IF p_com_cliente <> ' ' THEN
       v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' OR ';
    END IF;

    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' (CU.OID_SUBCLIENTE IN (' || chr(39) || P_COM_SUBCLIENTE || chr(39) || ')';

    IF p_considerar_todos_niveis = 0 THEN
      v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND CU.OID_PTO_SERVICIO IS NULL ';
    END IF;
    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ')';
  END IF;

  IF p_com_pto_servicio <> ' ' THEN
   IF p_com_cliente <> ' ' OR p_com_subcliente <> ' '  THEN
       v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' OR ';
    END IF;
    v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' (CU.OID_PTO_SERVICIO IN (' || chr(39) || P_COM_PTO_SERVICIO || chr(39) || '))';
  END IF;

  IF p_com_cliente <> ' ' OR p_com_subcliente <> ' ' OR p_com_ptO_servicio <> ' ' THEN
   v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ')';
  END IF;


  IF p_com_canal <> ' ' THEN
      v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND CA.OID_CANAL IN (' || chr(39) || P_COM_CANAL || chr(39) || ')';
      -- CONCATENA OS IDENTIFICADORES DOS SUBCANAIS
      IF p_com_subcanal <> ' ' THEN
        v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND CU.OID_SUBCANAL IN (' || chr(39) || P_COM_SUBCANAL || chr(39) || ')';
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(SC.DES_SUBCANAL,'' - '') WITHIN GROUP(ORDER BY SC.DES_SUBCANAL )    LIST_SUBCANAL
                 FROM  RPGE_YSUBCANAL SC
                 WHERE SC.OID_SUBCANAL IN ('||  chr(39) || P_COM_SUBCANAL || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_subcanal   ;
      END IF;
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(CA.DES_CANAL,'' - '') WITHIN GROUP(ORDER BY CA.DES_CANAL )    LIST_CANAL
                 FROM  RPGE_YCANAL CA
                 WHERE CA.OID_CANAL IN ('||  chr(39) || P_COM_CANAL || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_canal   ;



  END IF;
  -- CONCATENA OS IDENTIFICADORES DOS SETORES
  IF p_com_sector <> ' ' THEN
     v_sql_where_sector := v_sql_where_sector || ' AND SECT.OID_SECTOR IN (' || chr(39) || P_COM_SECTOR || chr(39) || ')';
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(SECT.DES_SECTOR,'' - '') WITHIN GROUP(ORDER BY SECT.DES_SECTOR )    LIST_SECTOR
                 FROM  RPGE_YSECTOR SECT
                 WHERE SECT.OID_SECTOR IN ('||  chr(39) || P_COM_SECTOR || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_sector   ;
  END IF;

  -- CONCATENA OS IDENTIFICADORES DOS DIVISAS
  IF p_com_divisa <> ' ' THEN
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(DIV.DES_DIVISA,'' - '') WITHIN GROUP(ORDER BY DIV.DES_DIVISA )    LIST_DIVISA
                 FROM  RPGE_YDIVISA DIV
                 WHERE DIV.OID_DIVISA IN ('||  chr(39) || P_COM_DIVISA || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_divisa ;
  END IF;


  -- CONCATENA OS IDENTIFICADORES DAS PLANTAS
  IF p_com_planta <> ' ' THEN
     v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND PL.OID_PLANTA IN (' || chr(39) || P_COM_PLANTA || chr(39) || ')';
     v_sql_where_sector := v_sql_where_sector || ' AND SECT.OID_PLANTA IN (' || chr(39) || P_COM_PLANTA || chr(39) || ')';
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(PL.DES_PLANTA,'' - '') WITHIN GROUP(ORDER BY PL.DES_PLANTA )    LIST_PLANTA
                 FROM   RPGE_YPLANTA PL
                 WHERE PL.OID_PLANTA IN ('||  chr(39) || P_COM_PLANTA || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_planta   ;
  END IF;

  -- CONCATENA OS IDENTIFICADORES DAS DELEGA¿OES
  IF p_com_delegacion <> ' ' THEN
     v_sql_where_bus_cuenta := v_sql_where_bus_cuenta || ' AND DEL.OID_DELEGACION IN (' || chr(39) || P_COM_DELEGACION || chr(39) || ')';
     --Para visualizacion de los parametros en la salida del reporte
    l_sql:= 'SELECT LISTAGG(DEL.DES_DELEGACION,'' - '') WITHIN GROUP(ORDER BY DEL.DES_DELEGACION )    LIST_DELEGACION
                 FROM  RPGE_YDELEGACION DEL
                 WHERE DEL.OID_DELEGACION IN ('||  chr(39) || P_COM_DELEGACION || chr(39) ||')
                 GROUP BY 1' ;
              execute immediate l_sql
              INTO l_list_delegacion   ;
  END IF;

  v_des_control_error := 'MONTANDO CONSULTA RECUPERAR CUENTA';

  -- MONTA O SELECT PARA RECUPERAR A CONTA
  v_sql := q'[SELECT DISTINCT CU.OID_CUENTA,
               1 BOL_TOTALIZADOR_SALDO,
               0 BOL_SECTOR_TESORO
               FROM RPGE_YCUENTA CU
               INNER JOIN RPGE_YCLIENTE CL ON CL.OID_CLIENTE = CU.OID_CLIENTE
               INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR
                              FROM RPGE_YSECTOR SECT
                              START WITH 1 = 1 ]' || V_SQL_WHERE_SECTOR || q'[
                              CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE
                            ) SE ON SE.OID_SECTOR = CU.OID_SECTOR
               INNER JOIN RPGE_YPLANTA PL ON PL.OID_PLANTA = SE.OID_PLANTA
               INNER JOIN RPGE_YDELEGACION DEL ON DEL.OID_DELEGACION = PL.OID_DELEGACION
               INNER JOIN RPGE_YTIPO_SECTOR TS ON TS.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR
               INNER JOIN RPGE_YSUBCANAL SC ON SC.OID_SUBCANAL = CU.OID_SUBCANAL
               INNER JOIN RPGE_YCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
               LEFT JOIN RPGE_YSUBCLIENTE SCL ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
               LEFT JOIN RPGE_YPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
               WHERE CU.COD_TIPO_CUENTA IN ('S','A') ]' || V_SQL_WHERE_BUS_CUENTA || q'[]';


            v_des_control_error := 'INSERINDO DADOS TABELA RPGE_GTT_TDATOS_CUENTA';
            -- INSERINDO NA TABELA TEMPORARIA
            execute immediate ' INSERT INTO RPGE_GTT_TDATOS_CUENTA ' || V_SQL;

VV_TODOS:='    P_COM_DELEGACION      := '||    NVL(    P_COM_DELEGACION       ,'NULL')       ||';'||chr(13)||
'    P_DETALHAR_DIVISA       := '||    NVL(    P_DETALHAR_DIVISA        ,0)       ||';'||chr(13)||
'    P_COM_CLIENTE      := '||    NVL(    P_COM_CLIENTE       ,'NULL')       ||';'||chr(13)||
'    P_DETALHAR_SECTOR      := '||    NVL(    P_DETALHAR_SECTOR       ,0)       ||';'||chr(13)||
'    P_COM_DIVISA      := '||    NVL(    P_COM_DIVISA       ,'NULL')       ||';'||chr(13)||
'    P_COM_PLANTA      := '||    NVL(    P_COM_PLANTA       ,'NULL')       ||';'||chr(13)||
'    P_COM_SECTOR       := '||    NVL(    P_COM_SECTOR        ,'NULL')       ||';'||chr(13)||
'    P_COM_SUBCLIENTE      := '||    NVL(    P_COM_SUBCLIENTE       ,'NULL')       ||';'||chr(13)||
'    P_COM_PTO_SERVICIO       := '||    NVL(    P_COM_PTO_SERVICIO        ,'NULL')       ||';'||chr(13)||
'    P_COM_CANAL      := '||    NVL(    P_COM_CANAL       ,'NULL')       ||';'||chr(13)||
'    P_COM_SUBCANAL      := '||    NVL(    P_COM_SUBCANAL       ,'NULL')       ||';'||chr(13)||
'    P_COM_DENOMINACION      := '||    NVL(    P_COM_DENOMINACION       ,'NULL')       ||';'||chr(13)||
'    P_COM_MEDIO_PAGO       := '||    NVL(    P_COM_MEDIO_PAGO        ,'NULL')       ||';'||chr(13)||
'    P_CONSIDERAR_TOTAL_MEDIO_PAGO       := '||    NVL(    P_CONSIDERAR_TOTAL_MEDIO_PAGO        ,0)       ||';'||chr(13)||
'    P_FILTRO_DENOMINACION      := '||    NVL(    P_FILTRO_DENOMINACION       ,0)       ||';'||chr(13)||
'    P_CONSIDERAR_TOTAL_EFECTIVO      := '||    NVL(    P_CONSIDERAR_TOTAL_EFECTIVO       ,0)       ||';'||chr(13)||
'    P_FILTRO_MEDIO_PAGO      := '||    NVL(    P_FILTRO_MEDIO_PAGO       ,0)       ||';'||chr(13)||
'    P_CLIENTE_SEM_SALDO      := '||    NVL(    P_CLIENTE_SEM_SALDO       ,0)       ||';'||chr(13)||
'    P_CONSID_EFECTIVO_MEDIO_PAGO       := '||    NVL(    P_CONSID_EFECTIVO_MEDIO_PAGO        ,0)       ||';'||chr(13)||
'    P_CONSIDERAR_TODOS_NIVEIS     := '||    NVL(    P_CONSIDERAR_TODOS_NIVEIS      ,0)       ||';'||chr(13)||
'    P_CABECERA    := '||    NVL(    P_CABECERA     ,'N')       ;

v_sql := q'[WITH saldo AS (
        SELECT 'Titulo' AS DES_TITULO
        ,q1.DES_DIVISA
        ,q1.CLIENTE AS DES_CLIENTE
        ,q1.cod_nivel_detalle
        ,(CASE  /*P_DETALHAR_DIVISA*/
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN 'Total Efectivo'
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
          (trim(q1.DES_DENOMINACION)||' '||DECODE (Q1.BOL_BILLETE, 1, 'B', 'M')  ||' (' || q1.UNIDADE_MEDIDA_MEDIO_PAGO || ') x ' ||  to_char(sum(q1.NEL_CANTIDAD_DISPONIBLE)) || ' x ' ||  trim(to_char(q1.NUM_VALOR_UNIDAD, '999G999G990D00')))
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle IN ('T','G') THEN
          'Total Efectivo'
          END) DES_DISP
      ,(CASE
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN 'Total Efectivo'
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
          (trim(q1.DES_DENOMINACION)||' '||DECODE (Q1.BOL_BILLETE, 1, 'B', 'M') ||' (' || q1.UNIDADE_MEDIDA_MEDIO_PAGO || ') x ' || to_char(sum(q1.NEL_CANTIDAD_NODISPONIBLE)) || ' x ' ||  trim(to_char(q1.NUM_VALOR_UNIDAD, '999G999G990D00')))
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle IN ('T','G') THEN
          'Total Efectivo'
           END) DES_NO_DISP
      ,q1.DES_SECTOR
      ,q1.SUB_CLIENTE AS DES_SUBCLIENTE
      ,q1.PUNTO_SERVICIO AS DES_PTO_SERVICIO
      ,q1.CANAL AS DES_CANAL
      ,q1.SUB_CANAL AS DES_SUBCANAL
      ,q1.NUM_VALOR_UNIDAD
      ,q1.VALOR_DENOMINACION AS NUM_VALOR_DENOMINACION
      ,q1.UNIDADE_MEDIDA_MEDIO_PAGO AS DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,q1.DESC_TOTAL AS DES_TOTAL
      ,sum(q1.NUM_IMPORTE_DISPONIBLE) NUM_IMPORTE_DISPONIBLE
      ,sum(q1.NUM_IMPORTE_NODISPONIBLE) NUM_IMPORTE_NODISPONIBLE
      ,sum(q1.NEL_CANTIDAD_DISPONIBLE) NEL_CANTIDAD_DISPONIBLE
      ,sum(q1.NEL_CANTIDAD_NODISPONIBLE) NEL_CANTIDAD_NODISPONIBLE
      ,q1.DELEGACION AS DELEGACION
      ,q1.PLANTA
      --,q1.CALIDAD
      ,q1.CALIDAD_DISPONIBLE
      ,q1.CALIDAD_NODISPONIBLE
      ,Q1.OID_DIVISA
      ,Q1.BOL_BILLETE
      ,Q1.DES_DENOMINACION
FROM
 (SELECT SE.COD_NIVEL_DETALLE
        ,DIV.DES_DIVISA
        ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
        ,(CASE ]' || P_DETALHAR_SECTOR || q'[
           WHEN 1 THEN
            (VF.DES_SECTOR)
           ELSE
            CHR(45)
         END) AS DES_SECTOR
        ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
        ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
        ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
        ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN
            (UM.NUM_VALOR_UNIDAD)
           ELSE
            0
         END) AS NUM_VALOR_UNIDAD
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN
            (DNM.NUM_VALOR)
           ELSE
            0
         END) AS VALOR_DENOMINACION
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN
            (UM.DES_UNIDAD_MEDIDA)
           ELSE
            CHR(45)
         END) AS UNIDADE_MEDIDA_MEDIO_PAGO
     ,'Total Efectivo' AS DESC_TOTAL
        ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 1 THEN
                SE.num_importe
             END),
             0) AS NUM_IMPORTE_DISPONIBLE
        ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 0 THEN
                SE.num_importe
             END),
             0) AS NUM_IMPORTE_NODISPONIBLE
        ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 1 THEN
                SE.NEL_CANTIDAD
             END),
             0) AS NEL_CANTIDAD_DISPONIBLE
        ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 0 THEN
                SE.NEL_CANTIDAD
             END),
             0) AS NEL_CANTIDAD_NODISPONIBLE
        ,VF.DES_DELEGACION AS DELEGACION
        ,VF.COD_PLANTA AS PLANTA
        --,CAL.DES_CALIDAD AS CALIDAD
         ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 1 THEN
                CAL.DES_CALIDAD
             END),
             NULL) AS CALIDAD_DISPONIBLE
        ,nvl((CASE SE.BOL_DISPONIBLE
               WHEN 0 THEN
                CAL.DES_CALIDAD
             END),
             NULL) AS CALIDAD_NODISPONIBLE
        ,DIV.OID_DIVISA
        ,DNM.BOL_BILLETE AS BOL_BILLETE
       , DNM.DES_DENOMINACION AS DES_DENOMINACION
       --, trim(to_char(DNM.NUM_VALOR, '999G999G990D00'))  AS DES_DENOMINACION
    FROM V_FILTRO_SALDO_CLIENTE         VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA   DCU   ON DCU.OID_CUENTA       = VF.OID_CUENTA
    INNER JOIN RPGE_YSALDO_EFECTIVO     SE    ON VF.OID_CUENTA        = SE.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA             DIV   ON DIV.OID_DIVISA       = SE.OID_DIVISA
    LEFT OUTER JOIN RPGE_YDENOMINACION  DNM   ON DNM.OID_DENOMINACION = SE.OID_DENOMINACION
    LEFT OUTER JOIN RPGE_YUNIDAD_MEDIDA UM    ON UM.OID_UNIDAD_MEDIDA = SE.OID_UNIDAD_MEDIDA
    LEFT OUTER JOIN RPGE_YCALIDAD CAL  ON SE.OID_CALIDAD = CAL.OID_CALIDAD
   WHERE VF.OID_DELEGACION IN(']' ||P_COM_DELEGACION || q'[')
    AND (]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,2))
    AND DIV.OID_DIVISA IN (']' ||P_COM_DIVISA || q'[')
    AND (]' || P_FILTRO_DENOMINACION || q'[ =0 OR DNM.OID_DENOMINACION IN(']' || p_com_denominacion || q'['))
    AND (]' || P_CONSIDERAR_TOTAL_EFECTIVO || q'[ =1 OR SE.COD_NIVEL_DETALLE NOT IN('T'))
    )Q1 GROUP BY DES_DIVISA
         ,q1.CLIENTE
         ,q1.COD_NIVEL_DETALLE
         ,q1.DES_SECTOR
         ,q1.SUB_CLIENTE
         ,q1.PUNTO_SERVICIO
         ,q1.CANAL
         ,q1.SUB_CANAL
         ,q1.NUM_VALOR_UNIDAD
         ,q1.VALOR_DENOMINACION
         ,q1.UNIDADE_MEDIDA_MEDIO_PAGO
        ,q1.DESC_TOTAL
        ,q1.DELEGACION
        ,q1.PLANTA
        --,q1.CALIDAD
        ,q1.CALIDAD_DISPONIBLE
        ,q1.CALIDAD_NODISPONIBLE
        ,Q1.OID_DIVISA
        ,Q1.BOL_BILLETE
       , Q1.DES_DENOMINACION
UNION
select 'Titulo' as DES_TITULO
        ,q1.DES_DIVISA
        ,q1.CLIENTE AS DES_CLIENTE
      ,q1.cod_nivel_detalle
      ,(CASE /*P_DETALHAR_DIVISA*/
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (UNIDADE_MEDIDA_MEDIO_PAGO || ' ' || CHR(45) || ' ' || TO_CHAR(SUM(NEL_CANTIDAD_DISPONIBLE)) ||
              ' unidad(es)')
         ELSE UNIDADE_MEDIDA_MEDIO_PAGO
       END) DES_DISP
       ,(CASE /*P_DETALHAR_DIVISA*/
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (UNIDADE_MEDIDA_MEDIO_PAGO || ' ' || CHR(45) || ' ' || TO_CHAR(SUM(NEL_CANTIDAD_DISPONIBLE)) ||
              ' unidad(es)')
         ELSE UNIDADE_MEDIDA_MEDIO_PAGO
       END) DES_NO_DISP
      ,q1.DES_SECTOR
      ,q1.SUB_CLIENTE AS DES_SUBCLIENTE
      ,q1.PUNTO_SERVICIO AS DES_PTO_SERVICIO
      ,q1.CANAL AS DES_CANAL
      ,q1.SUB_CANAL AS DES_SUBCANAL
      ,q1.NUM_VALOR_UNIDAD
      ,q1.VALOR_DENOMINACION AS NUM_VALOR_DENOMINACION
      ,q1.UNIDADE_MEDIDA_MEDIO_PAGO AS DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,q1.DESC_TOTAL AS DES_TOTAL
      ,sum(q1.NUM_IMPORTE_DISPONIBLE) NUM_IMPORTE_DISPONIBLE
      ,sum(q1.NUM_IMPORTE_NODISPONIBLE) NUM_IMPORTE_NODISPONIBLE
      ,sum(q1.NEL_CANTIDAD_DISPONIBLE) NEL_CANTIDAD_DISPONIBLE
      ,sum(q1.NEL_CANTIDAD_NODISPONIBLE) NEL_CANTIDAD_NODISPONIBLE
      ,q1.DELEGACION AS DELEGACION
      ,q1.PLANTA AS PLANTA
      --,'' as CALIDAD
      ,'' AS CALIDAD_DISPONIBLE
      ,'' AS CALIDAD_NODISPONIBLE
      ,Q1.OID_DIVISA
       ,0  AS BOL_BILLETE
      ,'--'  AS DES_DENOMINACION
FROM
 (SELECT  /*P_DETALHAR_DIVISA*/
      (CASE ]' || P_DETALHAR_DIVISA || q'[ /* Indica se vai detalhar por divisa*/
         WHEN 1 THEN
          (SMP.COD_NIVEL_DETALLE)
         ELSE
          CHR(45)
       END) AS cod_nivel_detalle
      ,SMP.BOL_DISPONIBLE
      ,DIV.DES_DIVISA
      ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
      ,(CASE ]' || P_DETALHAR_SECTOR || q'[ /*Indica se vai agrupar os dados para os sectores*/
         WHEN 1 THEN
          (VF.DES_SECTOR)
         ELSE
          CHR(45)
       END) AS DES_SECTOR
      ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
      ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
      ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
      ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
      ,0 AS NUM_VALOR_UNIDAD
      ,0 AS VALOR_DENOMINACION
      ,(CASE  /*P_DETALHAR_DIVISA*/
         WHEN SMP.cod_nivel_detalle = 'D' AND ]' || P_DETALHAR_DIVISA || q'[ = 1 THEN
          MP.DES_MEDIO_PAGO
     WHEN SMP.cod_nivel_detalle = 'D' AND  ]' || P_DETALHAR_DIVISA || q'[ = 0  /*P_DETALHAR_DIVISA*/  THEN
      decode(SMP.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
         WHEN SMP.cod_nivel_detalle = 'T'  THEN
          decode(SMP.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
       END) AS UNIDADE_MEDIDA_MEDIO_PAGO
     ,decode(SMP.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores') AS DESC_TOTAL
       ,nvl((CASE SMP.BOL_DISPONIBLE
               WHEN 1 THEN
                SMP.num_importe
             end),
             0) AS num_importe_disponible
      ,nvl((CASE SMP.BOL_DISPONIBLE
               WHEN 0 THEN
                SMP.num_importe
             end),
             0) AS num_importe_nodisponible
        ,nvl((CASE SMP.BOL_DISPONIBLE
               WHEN 1 THEN
                SMP.NEL_CANTIDAD
             end),
             0) AS NEL_CANTIDAD_disponible
        ,nvl((CASE SMP.BOL_DISPONIBLE
               WHEN 0 THEN
                SMP.NEL_CANTIDAD
             end),
             0) AS NEL_CANTIDAD_nodisponible
        ,VF.DES_DELEGACION AS DELEGACION
        ,VF.COD_PLANTA AS PLANTA
       ,DIV.OID_DIVISA
  FROM V_FILTRO_SALDO_CLIENTE VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA DCU   ON DCU.OID_CUENTA = VF.OID_CUENTA
    INNER JOIN RPGE_YSALDO_MEDIO_PAGO SMP   ON VF.OID_CUENTA = SMP.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA DIV             ON DIV.OID_DIVISA = SMP.OID_DIVISA
  LEFT OUTER JOIN RPGE_YMEDIO_PAGO MP       ON MP.OID_MEDIO_PAGO = SMP.OID_MEDIO_PAGO AND MP.OID_DIVISA = SMP.OID_DIVISA
 WHERE VF.OID_DELEGACION IN(']' || p_com_delegacion || q'[')
  AND ( ]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,3))
  AND ( ]' || P_FILTRO_MEDIO_PAGO || q'[ =0 OR MP.OID_MEDIO_PAGO IN(']' || p_com_medio_pago || q'['))
  AND ( ]' || P_CONSIDERAR_TOTAL_MEDIO_PAGO || q'[ =1 OR SMP.COD_NIVEL_DETALLE NOT IN('T'))
  AND DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'['))Q1
 group by DES_DIVISA
         ,q1.CLIENTE
         ,q1.cod_nivel_detalle
         ,q1.DES_SECTOR
         ,q1.SUB_CLIENTE
         ,q1.PUNTO_SERVICIO
         ,q1.CANAL
         ,q1.SUB_CANAL
         ,q1.NUM_VALOR_UNIDAD
         ,q1.VALOR_DENOMINACION
         ,q1.UNIDADE_MEDIDA_MEDIO_PAGO
         ,q1.DESC_TOTAL
         ,q1.DELEGACION
         ,q1.PLANTA
         ,Q1.OID_DIVISA
        ,TO_CHAR(NULL)
     )
select distinct
         'Titulo' as DES_TITULO
       ,nvl(saldo.DES_DIVISA,sem_saldo.DES_DIVISA)as DES_DIVISA
       ,nvl(saldo.DES_CLIENTE,sem_saldo.CLIENTE)as DES_CLIENTE
       ,nvl(saldo.cod_nivel_detalle,sem_saldo.cod_nivel_detalle)as cod_nivel_detalle
       ,nvl(saldo.des_disp,'-')as des_disp
       ,nvl(saldo.des_no_disp,'-')as des_no_disp
       ,nvl(saldo.DES_SECTOR,sem_saldo.DES_SECTOR)as DES_SECTOR
       ,nvl(saldo.DES_SUBCLIENTE,sem_saldo.SUB_CLIENTE)as DES_SUBCLIENTE
       ,nvl(saldo.DES_PTO_SERVICIO,sem_saldo.PUNTO_SERVICIO)as DES_PTO_SERVICIO
       ,nvl(saldo.DES_CANAL,sem_saldo.CANAL)as DES_CANAL
       ,nvl(saldo.DES_SUBCANAL,sem_saldo.SUB_CANAL)as DES_SUBCANAL
       ,nvl(saldo.NUM_VALOR_DENOMINACION,sem_saldo.VALOR_DENOMINACION)as NUM_VALOR_DENOMINACION
       ,nvl(saldo.DES_UNIDAD_MEDIDA_MEDIO_PAGO,sem_saldo.UNIDADE_MEDIDA_MEDIO_PAGO)as DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,nvl(saldo.DES_TOTAL,sem_saldo.DESC_TOTAL)as DES_TOTAL
      ,nvl(saldo.NUM_IMPORTE_DISPONIBLE,0) NUM_IMPORTE_DISPONIBLE
      ,nvl(saldo.NUM_IMPORTE_NODISPONIBLE,0)NUM_IMPORTE_NODISPONIBLE
      ,nvl(saldo.NEL_CANTIDAD_DISPONIBLE,0) NEL_CANTIDAD_DISPONIBLE
      ,nvl(saldo.NEL_CANTIDAD_NODISPONIBLE,0) NEL_CANTIDAD_NODISPONIBLE
      ,NVL(SALDO.DELEGACION,SEM_SALDO.DELEGACION) AS DELEGACION
      ,NVL(SALDO.PLANTA,SEM_SALDO.PLANTA) AS PLANTA
      --,SALDO.CALIDAD AS CALIDAD
      ,SALDO.CALIDAD_DISPONIBLE
      ,SALDO.CALIDAD_NODISPONIBLE
      ,NVL(SALDO.OID_DIVISA,SEM_SALDO.OID_DIVISA) AS OID_DIVISA
       ,NVL(SALDO.BOL_BILLETE,SEM_SALDO.BOL_BILLETE) AS BOL_BILLETE
       ,NVL(SALDO.DES_DENOMINACION ,SEM_SALDO.DES_DENOMINACION) AS DES_DENOMINACION
from saldo
right outer join(SELECT
(CASE ]' || P_DETALHAR_DIVISA || q'[ /* Indica se vai detalhar por divisa*/
         WHEN 1 THEN
          'D'
         ELSE
          'Total Efectivo'
       END) AS cod_nivel_detalle
      ,1 AS BOL_DISPONIBLE
      ,DIV.DES_DIVISA
      ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
      ,(CASE ]' || P_DETALHAR_SECTOR || q'[ /*Indica se vai agrupar os dados para os sectores*/
         WHEN 1 THEN
          (VF.DES_SECTOR)
         ELSE
          CHR(45)
       END) AS DES_SECTOR
      ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
      ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
      ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
      ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
      ,0 AS NUM_VALOR_UNIDAD
      ,0 AS VALOR_DENOMINACION
      ,CHR(45) AS UNIDADE_MEDIDA_MEDIO_PAGO
      ,'Total Efectivo' as DESC_TOTAL
      ,0 as NUM_IMPORTE
      ,0 AS NEL_CANTIDAD
      ,VF.des_DELEGACION AS DELEGACION
      ,VF.COD_PLANTA AS PLANTA
      ,DIV.OID_DIVISA
      ,'' AS  BOL_BILLETE
      ,'Total Efectivo' AS DES_DENOMINACION
  FROM V_FILTRO_SALDO_CLIENTE VF
     INNER JOIN RPGE_YSALDO_EFECTIVO     SE    ON VF.OID_CUENTA        = SE.OID_CUENTA_SALDO
     LEFT OUTER JOIN RPGE_YDENOMINACION  DNM   ON DNM.OID_DENOMINACION = SE.OID_DENOMINACION
     INNER JOIN RPGE_GTT_TDATOS_CUENTA DCU ON DCU.OID_CUENTA = VF.OID_CUENTA
     INNER JOIN RPGE_YDIVISA DIV           ON DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
  WHERE VF.OID_DELEGACION IN(']' || P_COM_DELEGACION  || q'[')
     AND DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
  )SEM_SALDO
    ON    SEM_SALDO.des_divisa = saldo.DES_DIVISA
      and sem_saldo.DES_SECTOR = saldo.DES_SECTOR
      and sem_saldo.cliente = saldo.DES_CLIENTE
      and sem_saldo.canal = saldo.DES_CANAL
      and sem_saldo.SUB_CANAL = saldo.DES_SUBCANAL
      and sem_saldo.SUB_CLIENTE = saldo.DES_SUBCLIENTE
      and sem_saldo.PUNTO_SERVICIO = saldo.DES_PTO_SERVICIO
WHERE (]' || P_CLIENTE_SEM_SALDO || q'[ =1 OR NUM_IMPORTE_DISPONIBLE<>0 OR NUM_IMPORTE_NODISPONIBLE<>0)]';

  v_des_control_error := 'INSERINDO DADOS TABELA RPGE_GTT_TSALDOS_V2';

 EXECUTE IMMEDIATE ' INSERT INTO RPGE_GTT_TSALDOS_V2 ' || V_SQL;


/*Esse if foi necessario para na¿ exibir zero(0) do cliente somando o detalhado com o total*/
IF p_cabecera = 'N' THEN
    IF p_cliente_sem_saldo = 0 AND p_detalhar_divisa = 0 THEN
       OPEN p_retorno_saldos FOR
       SELECT  des_titulo,
            des_divisa,
            des_cliente,
            NULL AS cod_nivel_detalle,
            des_disp,
            des_no_disp,
            des_sector,
            des_subcliente,
            des_pto_servicio,
            des_canal,
            des_subcanal,
            num_valor_denominacion,
            des_unidad_medida_medio_pago medio_pago,
            des_total,
            sum(num_importe_disponible)AS num_importe_disponible,
            sum(num_importe_nodisponible) AS num_importe_nodisponible,
            sum(nel_cantidad_disponible) AS nel_cantidad_disponible,
            sum(nel_cantidad_nodisponible) AS nel_cantidad_nodisponible
            ,des_delegacion
            ,cod_planta
            --,des_calidad
            ,DES_CALIDAD_DISPONIBLE
            ,DES_CALIDAD_NODISPONIBLE
            ,oid_divisa
             ,bol_billete
            , des_denominacion
        FROM rpge_gtt_tsaldos_v2
        HAVING((sum(num_importe_disponible)<>0 OR sum(num_importe_nodisponible)<>0 ))
        GROUP BY
          des_titulo,
          des_divisa,
          des_cliente,
          des_disp,
          des_no_disp,
          des_sector,
          des_subcliente,
          des_pto_servicio,
          des_canal,
          des_subcanal,
          num_valor_denominacion,
          des_unidad_medida_medio_pago,
          des_total
          ,des_delegacion
          ,cod_planta
          --,des_calidad
          ,DES_CALIDAD_DISPONIBLE
          ,DES_CALIDAD_NODISPONIBLE
          ,oid_divisa
           ,bol_billete
          ,des_denominacion
          ;
    ELSE
          OPEN p_retorno_saldos FOR
          SELECT des_titulo,
            des_divisa,
            des_cliente,
            cod_nivel_detalle,
            des_disp,
            des_no_disp,
            des_sector,
            des_subcliente,
            des_pto_servicio,
            des_canal,
            des_subcanal,
            num_valor_denominacion,
            des_unidad_medida_medio_pago,
            des_total,
            num_importe_disponible,
            num_importe_nodisponible,
            nel_cantidad_disponible,
            nel_cantidad_nodisponible
            ,des_delegacion
            ,cod_planta
            --,des_calidad
            ,DES_CALIDAD_DISPONIBLE
            ,DES_CALIDAD_NODISPONIBLE
            ,oid_divisa
            ,bol_billete
            ,des_denominacion
           FROM rpge_gtt_tsaldos_v2;
    END IF;
ELSE
    IF p_cliente_sem_saldo = 0 AND p_detalhar_divisa = 0 THEN
       OPEN p_retorno_saldos FOR
                  SELECT l_list_divisa list_divisa,
                         des_divisa,
                         l_list_sector list_sector,
                         l_list_canal list_canal,
                         num_valor_denominacion,
                         des_unidad_medida_medio_pago medio_pago,
                         des_total,
                         SUM (NVL (num_importe_disponible, 0)) AS num_importe,
                         'Usuario de Conexion' p_usuario,
                         l_list_delegacion list_delegacion,
                         l_list_planta list_planta,
                         oid_divisa,
                         'Total efectivo' AS des_denominacion
                    FROM RPGE_GTT_TSALDOS_V2
                GROUP BY des_divisa,
                         num_valor_denominacion,
                         des_unidad_medida_medio_pago,
                         des_total,
                         oid_divisa
                ORDER BY TO_NUMBER (  NVL (REGEXP_REPLACE (des_denominacion, '[^[:digit:]]', ''), 0)) DESC;
    ELSE
          OPEN p_retorno_saldos FOR
             SELECT l_list_divisa list_divisa,
                         des_divisa,
                         l_list_sector list_sector,
                         l_list_canal list_canal,
                         num_valor_denominacion,
                         des_unidad_medida_medio_pago medio_pago,
                         des_total,
                         SUM (NVL (num_importe_disponible, 0)) AS num_importe,
                         SUM (NVL (nel_cantidad_disponible, TO_NUMBER (NULL))) AS nel_cantidad,
                         l_list_delegacion list_delegacion,
                         l_list_planta list_planta,
                         DES_CALIDAD_DISPONIBLE,
                         DES_CALIDAD_NODISPONIBLE,
                         oid_divisa,
                         CASE
                            WHEN NVL (des_denominacion, 'T') = 'T' THEN ''
                            ELSE DECODE (bol_billete, 1, 'B', 'M')
                         END
                            bol_billete,
                         NVL (des_denominacion, 'Total Efectivo') des_denominacion
                    FROM RPGE_GTT_TSALDOS_V2
                GROUP BY des_divisa,
                         num_valor_denominacion,
                         des_unidad_medida_medio_pago,
                         des_total,
                         des_calidad_disponible,
                         des_calidad_nodisponible,
                         oid_divisa,
                         CASE
                            WHEN NVL (des_denominacion, 'T') = 'T' THEN ''
                            ELSE DECODE (bol_billete, 1, 'B', 'M')
                         END,
                         NVL (des_denominacion, 'Total Efectivo')
                ORDER BY TO_NUMBER (  NVL (REGEXP_REPLACE (des_denominacion, '[^[:digit:]]', ''), 0)) DESC, BOL_BILLETE ASC ;
    END IF;
END IF;
COMMIT;

EXCEPTION
    WHEN others THEN
      BEGIN
        --si hay error se limpian las tablas temporales
        execute immediate 'TRUNCATE TABLE RPGE_GTT_TDATOS_CUENTA';
        execute immediate 'TRUNCATE TABLE RPGE_GTT_TSALDOS_V2';
        raise_application_error(-20002, SQLCODE || '   ' || SQLERRM || ' ' ||  v_des_control_error);
      END;

END;
/
