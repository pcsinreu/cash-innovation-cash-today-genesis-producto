CREATE OR REPLACE PROCEDURE SP_HISTORICO_SALDOS(P_COM_DELEGACION              IN  VARCHAR2,
                              P_DETALHAR_DIVISA             IN  NUMBER DEFAULT 0,
                              P_COM_CLIENTE                 IN  VARCHAR2,
                              P_DETALHAR_SECTOR             IN  NUMBER DEFAULT 0,
                              P_COM_DIVISA                  IN  VARCHAR2,
                              P_COM_PLANTA                  IN  VARCHAR2,
                              P_COM_SECTOR                  IN  VARCHAR2,
                              P_COM_SUBCLIENTE              IN  VARCHAR2,
                              P_COM_PTO_SERVICIO            IN  VARCHAR2,
                              P_COM_FECHA                   IN OUT  VARCHAR2,
                              P_COM_CANAL                   IN  VARCHAR2,
                              P_COM_SUBCANAL                IN  VARCHAR2,
                              P_COM_DENOMINACION            IN  VARCHAR2,
                              P_COM_MEDIO_PAGO              IN  VARCHAR2,
                              P_CONSIDERAR_TOTAL_MEDIO_PAGO IN  NUMBER DEFAULT 0,
                              P_FILTRO_DENOMINACION         IN  NUMBER DEFAULT 0,
                              P_CONSIDERAR_TOTAL_EFECTIVO   IN  NUMBER DEFAULT 0,
                              P_FILTRO_MEDIO_PAGO           IN  NUMBER DEFAULT 0,
                              P_CLIENTE_SEM_SALDO           IN  NUMBER DEFAULT 0,
                              P_CONSID_EFECTIVO_MEDIO_PAGO  IN  NUMBER DEFAULT 0,
                              P_CONSIDERAR_TODOS_NIVEIS     IN  NUMBER DEFAULT 0,
                              P_RETORNO_SALDOS              OUT SYS_REFCURSOR)
IS

/*******************************************************************************
  Copyright 2014 PROSEGUR.
  Génesis - Gestión de Efectivo
  All rights reserved.

  Autor: Cristian Gallo                    Fecha 16/10/2014
  Archivo nombre: sp_historico_saldos.sql
  Información general:
  Este procedure contiene la lógica para crear el Informe Histórico de Saldos.

  Principales Modificaciones (iniciales, cuando, versión, descripción):
  INI  DATE        VER  DESCRIPTION
  ---  ---------- ----- --------------------------------
  CG   16/10/2014  1.0  Creación
*******************************************************************************/

  V_SQL                     VARCHAR2(30000);
  V_SQL_WHERE_BUS_CUENTA    VARCHAR2(30000);
  V_DES_CONTROL_ERROR       VARCHAR2(100);
  V_WBETWEEN_FECHA          VARCHAR2(500);
  V_WBETWEEN_FECHA_HIST     VARCHAR2(150);
  V_SSUM_SALDO              VARCHAR2(1000);
  V_SCASE_SUM_SALDO         VARCHAR2(1000);
  V_SCASE_SUM_SALDO_HIST    VARCHAR2(1000);
  V_FECHA_HISTORICA         VARCHAR2(20);
  V_OPT_FECHA               CHAR(1);
  V_NO_RESULT               EXCEPTION;
  V_SQL_WHERE_SECTOR VARCHAR2(30000);
  VAR$OID_LOG VARCHAR2(50) := SYS_GUID(); 

   /*******************************************************************************
   Autor: Cristian Gallo                    Fecha 16/10/2014
   Nombre de Función: GET_OPCION_FECHA

   Inf. general: La Función GET_OPCION_FECHA retorna un valor caracter dependiendo
                 si la fecha que llega como parámetro es mayor, menor o igual que
                 la fecha de la tabla RPGE_YSALDO_MEDIO_PAGO_HIST, igualando la
                 fecha de parámetro con la fecha de la tabla. De no encontrar
                 datos, devuelve cero '0'.

   Principales Modificaciones (iniciales, cuando, versión, descripción):
   INI  DATE        VER  DESCRIPTION
   ---  ---------- ----- --------------------------------
   CG   16/10/2014  1.0  Creación
   *******************************************************************************/
   FUNCTION GET_OPCION_FECHA (P_FECHA     IN VARCHAR2)
            RETURN CHAR IS
      l_opcion CHAR;
   BEGIN
      SELECT CASE
                WHEN to_date(to_char(MAX(hor_saldo),'dd/mm/yyyy HH24:mi:ss'),'dd/mm/yyyy HH24:mi:ss') > TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss')
                   THEN '1'
                WHEN to_date(to_char(MAX(hor_saldo),'dd/mm/yyyy HH24:mi:ss'),'dd/mm/yyyy HH24:mi:ss') < TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss')
                   THEN '2'
                ELSE -- to_date(to_char(MAX(hor_saldo),'dd/mm/yyyy HH24:mi:ss'),'dd/mm/yyyy HH24:mi:ss') = TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss')
                        '3'
             END EXISTE_REG
        INTO l_opcion
        FROM RPGE_YSALDO_MEDIO_PAGO_HIST
       WHERE 
       
       (FEC_SALDO = TRUNC(TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss')) - 1 OR FEC_SALDO = TRUNC(TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss')))
       AND SYS_EXTRACT_UTC(HOR_SALDO) < TO_DATE(P_FECHA,'dd/mm/yyyy HH24:mi:ss');
      --
      RETURN l_opcion;
      --
   EXCEPTION
      WHEN OTHERS THEN
         l_opcion := '0';
         RETURN l_opcion;
   END GET_OPCION_FECHA;

   /*******************************************************************************
   Autor: Cristian Gallo                    Fecha 16/10/2014
   Nombre de Función: GET_FECHA_HIST

   Inf. general: La Función GET_FECHA_HIST retorna un valor caracter (fecha y hora
                 histórica) dependiendo si la fecha que llega como parámetro EXISTE
                 en la tabla RPGE_YSALDO_MEDIO_PAGO_HIST, igualando la fecha de
                 parámetro con la fecha de la tabla.

   Principales Modificaciones (iniciales, cuando, versión, descripción):
   INI  DATE        VER  DESCRIPTION
   ---  ---------- ----- --------------------------------
   CG   16/10/2014  1.0  Creación
   *******************************************************************************/
   FUNCTION GET_FECHA_HIST (p_fecha     IN VARCHAR2)
            RETURN varchar2 IS
      l_fecha_hist  varchar2(20);
   BEGIN
      SELECT NVL(to_char(MAX(hor_saldo),'dd/mm/yyyy HH24:mi:ss'), 'NA')
        INTO l_fecha_hist
        FROM RPGE_YSALDO_MEDIO_PAGO_HIST
       WHERE 
       
       (FEC_SALDO = TRUNC(TO_DATE(p_fecha,'dd/mm/yyyy HH24:mi:ss')) - 1 OR FEC_SALDO = TRUNC(TO_DATE(p_fecha,'dd/mm/yyyy HH24:mi:ss')))
       AND SYS_EXTRACT_UTC(HOR_SALDO) < TO_DATE(p_fecha,'dd/mm/yyyy HH24:mi:ss');
      --
      RETURN l_fecha_hist;
      --
   EXCEPTION
      WHEN OTHERS THEN
         l_fecha_hist := 'NA';
         RETURN l_fecha_hist;
   END GET_FECHA_HIST;

   /*******************************************************************************
   Autor: Cristian Gallo                    Fecha 16/10/2014
   Nombre de Función: GET_MAX_FECHA_HIST

   Inf. general: La Función GET_MAX_FECHA_HIST retorna un valor caracter (fecha y
                 hora histórica). Devuelve la fecha MAXIMA del campo fec_saldo para
                 la tabla RPGE_YSALDO_MEDIO_PAGO_HIST.

   Principales Modificaciones (iniciales, cuando, versión, descripción):
   INI  DATE        VER  DESCRIPTION
   ---  ---------- ----- --------------------------------
   CG   16/10/2014  1.0  Creación
   *******************************************************************************/
   FUNCTION GET_MAX_FECHA_HIST
            RETURN varchar2 IS
      l_max_fecha_hist  varchar2(20);
   BEGIN
       SELECT TO_CHAR(MAX(hor_saldo), 'dd/mm/yyyy HH24:mi:ss')
        INTO l_max_fecha_hist
        FROM RPGE_YSALDO_MEDIO_PAGO_HIST;
      --
      RETURN l_max_fecha_hist;
      --
   EXCEPTION
      WHEN OTHERS THEN
         l_max_fecha_hist := 'NA';
         RETURN l_max_fecha_hist;
   END GET_MAX_FECHA_HIST;

   /*******************************************************************************
   Autor: Cristian Gallo                    Fecha 16/10/2014
   Nombre de Función: GET_SSUM_SALDO

   Inf. general: La Función GET_SSUM_SALDO retorna un valor caracter. Devuelve
   según corresponda los campos con la operación SUMA de los campos importes del
   query principal.

   Principales Modificaciones (iniciales, cuando, versión, descripción):
   INI  DATE        VER  DESCRIPTION
   ---  ---------- ----- --------------------------------
   CG   16/10/2014  1.0  Creación
   *******************************************************************************/
   FUNCTION GET_SSUM_SALDO (p_opt_fecha          IN CHAR)
            RETURN varchar2 IS
      l_ssum_fecha   varchar2(500);
   BEGIN

      IF p_opt_fecha = '1' THEN
         --
         l_ssum_fecha := ',(sum(q1.H_NUM_IMPORTE_DISPONIBLE) - sum(q1.NUM_IMPORTE_DISPONIBLE)) NUM_IMPORTE_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NUM_IMPORTE_NODISPONIBLE) - sum(q1.NUM_IMPORTE_NODISPONIBLE)) NUM_IMPORTE_NODISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_DISPONIBLE) - sum(q1.NEL_CANTIDAD_DISPONIBLE)) NEL_CANTIDAD_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_NODISPONIBLE) - sum(q1.NEL_CANTIDAD_NODISPONIBLE)) NEL_CANTIDAD_NODISPONIBLE ';
      ELSE
         --
         l_ssum_fecha := ',(sum(q1.H_NUM_IMPORTE_DISPONIBLE) + sum(q1.NUM_IMPORTE_DISPONIBLE)) NUM_IMPORTE_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NUM_IMPORTE_NODISPONIBLE) + sum(q1.NUM_IMPORTE_NODISPONIBLE)) NUM_IMPORTE_NODISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_DISPONIBLE) + sum(q1.NEL_CANTIDAD_DISPONIBLE)) NEL_CANTIDAD_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_NODISPONIBLE) + sum(q1.NEL_CANTIDAD_NODISPONIBLE)) NEL_CANTIDAD_NODISPONIBLE ';
      END IF;
      --
      RETURN l_ssum_fecha;
      --
   EXCEPTION
      WHEN OTHERS THEN
         l_ssum_fecha := ',(sum(q1.H_NUM_IMPORTE_DISPONIBLE) + sum(q1.NUM_IMPORTE_DISPONIBLE)) NUM_IMPORTE_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NUM_IMPORTE_NODISPONIBLE) + sum(q1.NUM_IMPORTE_NODISPONIBLE)) NUM_IMPORTE_NODISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_DISPONIBLE) + sum(q1.NEL_CANTIDAD_DISPONIBLE)) NEL_CANTIDAD_DISPONIBLE ' ||chr(10)||
                         ',(sum(q1.H_NEL_CANTIDAD_NODISPONIBLE) + sum(q1.NEL_CANTIDAD_NODISPONIBLE)) NEL_CANTIDAD_NODISPONIBLE ';
         RETURN l_ssum_fecha;
   END GET_SSUM_SALDO;

   /*******************************************************************************
   Autor: Cristian Gallo                    Fecha 16/10/2014
   Nombre de Función: GET_WBETWEEN_FECHA

   Inf. general: La Función GET_WBETWEEN_FECHA retorna un valor caracter. Devuelve el
   filtro de la fecha y hora DESDE y HASTA para el campo GMT_CREACION correspondiente
   a las tablas RPGE_YTRANSACCION_EFECTIVO y RPGE_YTRANSACCION_MEDIO_PAGO del query
   principal.

   Principales Modificaciones (iniciales, cuando, versión, descripción):
   INI  DATE        VER  DESCRIPTION
   ---  ---------- ----- --------------------------------
   CG   16/10/2014  1.0  Creación
   *******************************************************************************/
   FUNCTION GET_WBETWEEN_FECHA (p_opt_fecha          IN CHAR,
                                p_fecha_historica    IN VARCHAR2,
                                p_com_fecha          IN VARCHAR2)
            RETURN varchar2 IS
      l_wbetween_fecha   varchar2(400);
   BEGIN
      IF p_opt_fecha = '1' THEN
         --
         l_wbetween_fecha := ' sys_extract_utc(SE.GMT_CREACION) ' ||chr(10)||
                             ' BETWEEN to_timestamp('''||p_com_fecha||''',''dd/mm/yyyy HH24:mi:ss'') AND (to_timestamp('''||p_fecha_historica||''',''dd/mm/yyyy HH24:mi:ss'') - 1/86400)';
      ELSIF p_opt_fecha = '2' THEN
         --
         l_wbetween_fecha := ' sys_extract_utc(SE.GMT_CREACION) ' ||chr(10)||
                             ' BETWEEN (to_timestamp('''||p_fecha_historica||''',''dd/mm/yyyy HH24:mi:ss'')+ 1/86400) AND to_timestamp('''||p_com_fecha||''',''dd/mm/yyyy HH24:mi:ss'')';
      ELSE --  p_opt_fecha = '3'
         l_wbetween_fecha := ' sys_extract_utc(SE.GMT_CREACION) = to_timestamp('''||p_com_fecha||''',''dd/mm/yyyy HH24:mi:ss'')' ||chr(10)||
                             ' AND (1 = 2)'; /* FECHA DE PARAMETRO = FECHA HISTORICA, ENTONCES NO MUESTRO LAS TRANSACCIONES CARGANDO SOLO LAS históricaS*/
      END IF;
      --
      RETURN l_wbetween_fecha;
      --
   EXCEPTION
      WHEN OTHERS THEN
         l_wbetween_fecha := ' sys_extract_utc(SE.GMT_CREACION) ' ||chr(10)||
                             ' BETWEEN (to_timestamp('''||p_fecha_historica||''',''dd/mm/yyyy HH24:mi:ss'')+ 1/86400) AND to_timestamp('''||p_com_fecha||''',''dd/mm/yyyy HH24:mi:ss'')';
         RETURN l_wbetween_fecha;
   END GET_WBETWEEN_FECHA;

  
BEGIN

   P_COM_FECHA := TO_CHAR(FN_GMT_DELEGACION(P_COM_DELEGACION,P_COM_FECHA),'dd/mm/yyyy HH24:mi:ss');  
   
   /* LIMPIA LAS TABLAS TEMPORALES */
   EXECUTE IMMEDIATE 'TRUNCATE TABLE RPGE_GTT_TDATOS_CUENTA_HIST';
   EXECUTE IMMEDIATE 'TRUNCATE TABLE RPGE_GTT_TSALDOS_HISTORICO';

   V_DES_CONTROL_ERROR := 'MONTANDO CLAUSULA WHERE RECUPERAR CUENTA';

   IF P_COM_CLIENTE <> ' ' OR P_COM_SUBCLIENTE <> ' ' OR P_COM_PTO_SERVICIO <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := 'AND (';
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DEL CLIENTE */
   IF P_COM_CLIENTE <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || '(';
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' CU.OID_CLIENTE IN (' || CHR(39) || P_COM_CLIENTE || CHR(39) || ')';
      --
      IF P_CONSIDERAR_TODOS_NIVEIS = 0 THEN
         V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND CU.OID_SUBCLIENTE IS NULL AND CU.OID_PTO_SERVICIO IS NULL ';
      END IF;
      --
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA ||')';
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DEL SUBCLIENTE */
   IF P_COM_SUBCLIENTE <> ' ' THEN
      --
      IF P_COM_CLIENTE <> ' ' THEN
         V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' OR ';
      END IF;
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' (CU.OID_SUBCLIENTE IN (' || CHR(39) || P_COM_SUBCLIENTE || CHR(39) || ')';
      --
      IF P_CONSIDERAR_TODOS_NIVEIS = 0 THEN
         V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND CU.OID_PTO_SERVICIO IS NULL ';
      END IF;
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ')';
      --
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DEL PUNTO DE SERVICIO */
   IF P_COM_PTO_SERVICIO <> ' ' THEN
      --
      IF P_COM_CLIENTE <> ' ' OR P_COM_SUBCLIENTE <> ' '  THEN
         V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' OR ';
      END IF;
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' (CU.OID_PTO_SERVICIO IN (' || CHR(39) || P_COM_PTO_SERVICIO || CHR(39) || '))';
      --
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DEL CLIENTE */
   IF P_COM_CLIENTE <> ' ' OR P_COM_SUBCLIENTE <> ' ' OR P_COM_PTO_SERVICIO <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ')';
   END IF;

   IF P_COM_CANAL <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND CA.OID_CANAL IN (' || CHR(39) || P_COM_CANAL || CHR(39) || ')';
      /* CONCATENA LOS IDENTIFICADORES DEL SUBCANAL */
      IF P_COM_SUBCANAL <> ' ' THEN
        V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND CU.OID_SUBCANAL IN (' || CHR(39) || P_COM_SUBCANAL || CHR(39) || ')';
      END IF;
      --
  END IF;

   /* CONCATENA LOS IDENTIFICADORES DE SECTORES */
   IF P_COM_SECTOR <> ' ' THEN
      V_SQL_WHERE_SECTOR := V_SQL_WHERE_SECTOR || ' AND SECT.OID_SECTOR IN (' || CHR(39) || P_COM_SECTOR || CHR(39) || ')';
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DE PLANTAS */
   IF P_COM_PLANTA <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND PL.OID_PLANTA IN (' || CHR(39) || P_COM_PLANTA || CHR(39) || ')';
      V_SQL_WHERE_SECTOR := V_SQL_WHERE_SECTOR || ' AND SECT.OID_PLANTA IN (' || CHR(39) || P_COM_PLANTA || CHR(39) || ')';
   END IF;

   /* CONCATENA LOS IDENTIFICADORES DE LA DELEGACIÓN */
   IF P_COM_DELEGACION <> ' ' THEN
      V_SQL_WHERE_BUS_CUENTA := V_SQL_WHERE_BUS_CUENTA || ' AND DEL.OID_DELEGACION IN (' || CHR(39) || P_COM_DELEGACION || CHR(39) || ')';
   END IF;

  V_FECHA_HISTORICA := GET_FECHA_HIST (P_COM_FECHA);
  
  /* Si la fecha/hora del parámetro es igual a la fecha actual, ocurrio un error en proceso DUO o la fecha/hora del parámetro */
  /* es mayor que la fecha/hora actual, busco la fecha/hora MAXIMA de la tabla Historica */
  IF V_FECHA_HISTORICA = 'NA' THEN
     V_FECHA_HISTORICA :=  GET_MAX_FECHA_HIST();
    
     IF V_FECHA_HISTORICA = 'NA' THEN
        V_DES_CONTROL_ERROR := 'NO HAY DATOS GENERADOS EN LA TABLA DE HistóricoS';
        RAISE V_NO_RESULT;
     ELSE
        /* Se le asigna a la variable V_OPT_FECHA = 2, se le debe sumar al ultimo Historico generado todas las transacciones */
        /* entre la ultima fecha/hora historica cargada y la fecha/hora del parámetro */
        V_OPT_FECHA := '2';
     END IF;
  ELSE

     V_OPT_FECHA := GET_OPCION_FECHA (P_COM_FECHA);

  END IF;

  V_SCASE_SUM_SALDO := ', NVL((CASE SE.bol_disponible WHEN 1 THEN SE.num_importe END),0) AS num_importe_disponible ' ||chr(10)||
                       ', NVL((CASE SE.bol_disponible WHEN 0 THEN SE.num_importe END),0) AS num_importe_nodisponible ' ||chr(10)||
                       ', NVL((CASE SE.bol_disponible WHEN 1 THEN SE.nel_cantidad END),0) AS nel_cantidad_disponible ' ||chr(10)||
                       ', NVL((CASE SE.bol_disponible WHEN 0 THEN SE.nel_cantidad END),0) AS nel_cantidad_nodisponible ';

  V_SCASE_SUM_SALDO_HIST := ', nvl((CASE SE.bol_disponible WHEN 1 THEN (SE.num_importe) END),0) AS h_num_importe_disponible ' ||chr(10)||
                            ', nvl((CASE SE.bol_disponible WHEN 0 THEN (SE.num_importe) END),0) AS h_num_importe_nodisponible ' ||chr(10)||
                            ', nvl((CASE SE.bol_disponible WHEN 1 THEN (SE.nel_cantidad) END),0) AS h_nel_cantidad_disponible ' ||chr(10)||
                            ', nvl((CASE SE.bol_disponible WHEN 0 THEN (SE.nel_cantidad) END),0) AS h_nel_cantidad_nodisponible ';

   V_SSUM_SALDO := GET_SSUM_SALDO (V_OPT_FECHA);

   V_WBETWEEN_FECHA := GET_WBETWEEN_FECHA (V_OPT_FECHA, V_FECHA_HISTORICA, P_COM_FECHA);

   V_WBETWEEN_FECHA_HIST := ' SE.FEC_SALDO = TRUNC(TO_DATE('''||V_FECHA_HISTORICA||''',''dd/mm/yyyy HH24:mi:ss''))';

   V_DES_CONTROL_ERROR := 'ARMANDO CONSULTA - RECUPERA CUENTAS';

   /* ARMADO DE SELECT PARA RECUPERAR LA/S CUENTA/S */
   V_SQL := '(SELECT DISTINCT CU.OID_CUENTA,
                     1 BOL_TOTALIZADOR_SALDO,
                     0 BOL_SECTOR_TESORO
                FROM RPGE_YCUENTA CU
               INNER JOIN RPGE_YCLIENTE CL ON CL.OID_CLIENTE = CU.OID_CLIENTE
               INNER JOIN ( SELECT SECT.OID_SECTOR, SECT.OID_PLANTA, SECT.OID_TIPO_SECTOR
                                    FROM RPGE_YSECTOR SECT
                                    START WITH 1 = 1 ' || V_SQL_WHERE_SECTOR || '
                                    CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE
                                  ) SE ON SE.OID_SECTOR = CU.OID_SECTOR
               INNER JOIN RPGE_YPLANTA PL ON PL.OID_PLANTA = SE.OID_PLANTA
               INNER JOIN RPGE_YDELEGACION DEL ON DEL.OID_DELEGACION = PL.OID_DELEGACION
               INNER JOIN RPGE_YTIPO_SECTOR TS ON TS.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR
               INNER JOIN RPGE_YSUBCANAL SC ON SC.OID_SUBCANAL = CU.OID_SUBCANAL
               INNER JOIN RPGE_YCANAL CA ON CA.OID_CANAL = SC.OID_CANAL
                LEFT JOIN RPGE_YSUBCLIENTE SCL ON SCL.OID_SUBCLIENTE = CU.OID_SUBCLIENTE
                LEFT JOIN RPGE_YPUNTO_SERVICIO PTO ON PTO.OID_PTO_SERVICIO = CU.OID_PTO_SERVICIO
               WHERE CU.COD_TIPO_CUENTA IN (''S'',''A'') ' || V_SQL_WHERE_BUS_CUENTA || ')';

   V_DES_CONTROL_ERROR := 'INSERTANDO DATOS EN TABLA RPGE_GTT_TDATOS_CUENTA_HIST';
   
   /* INSERT EN LA TABLA TEMPORAL RPGE_GTT_TDATOS_CUENTA_HIST */
   EXECUTE IMMEDIATE ' INSERT INTO RPGE_GTT_TDATOS_CUENTA_HIST ' || V_SQL;

   V_DES_CONTROL_ERROR := 'ARMANDO CONSULTA - RECUPERA TRANSACCIONES HISTORICAS';

   V_SQL := q'[with saldo as (
   SELECT 'Titulo' as DES_TITULO
      ,q1.DES_DIVISA
      ,q1.CLIENTE AS DES_CLIENTE
      ,q1.cod_nivel_detalle
      ,(CASE
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN 'Total Efectivo'
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (trim(to_char(q1.VALOR_DENOMINACION, '999G999G990D00')) || ' (' ||
            q1.UNIDADE_MEDIDA_MEDIO_PAGO || ') x ' ||
            to_char(sum(q1.NEL_CANTIDAD_DISPONIBLE)) || ' x ' ||
            trim(to_char(q1.NUM_VALOR_UNIDAD, '999G999G990D00')))
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle IN ('T','G') THEN
            'Total Efectivo' END) DES_DISP
      ,(CASE
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN 'Total Efectivo'
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (trim(to_char(q1.VALOR_DENOMINACION, '999G999G990D00')) || ' (' ||
            q1.UNIDADE_MEDIDA_MEDIO_PAGO || ') x ' ||
            to_char(sum(q1.NEL_CANTIDAD_NODISPONIBLE)) || ' x ' ||
            trim(to_char(q1.NUM_VALOR_UNIDAD, '999G999G990D00')))
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle IN ('T','G') THEN
            'Total Efectivo' END) DES_NO_DISP
      ,q1.DES_SECTOR
      ,q1.SUB_CLIENTE AS DES_SUBCLIENTE
      ,q1.PUNTO_SERVICIO AS DES_PTO_SERVICIO
      ,q1.CANAL AS DES_CANAL
      ,q1.SUB_CANAL AS DES_SUBCANAL
      ,q1.NUM_VALOR_UNIDAD
      ,q1.VALOR_DENOMINACION AS NUM_VALOR_DENOMINACION
      ,q1.UNIDADE_MEDIDA_MEDIO_PAGO AS DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,q1.DESC_TOTAL AS DES_TOTAL
      ]' || V_SSUM_SALDO || q'[
   FROM
   ( SELECT se.cod_nivel_detalle
        ,div.oid_divisa
        ,DIV.DES_DIVISA
        ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
        ,(CASE ]' || P_DETALHAR_SECTOR || q'[
           WHEN 1 THEN (VF.DES_SECTOR)
           ELSE CHR(45) END) AS DES_SECTOR
        ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
        ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
        ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
        ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (UM.NUM_VALOR_UNIDAD)
           ELSE 0 END) AS NUM_VALOR_UNIDAD
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (DNM.NUM_VALOR)
           ELSE 0 END) AS VALOR_DENOMINACION
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (UM.DES_UNIDAD_MEDIDA)
           ELSE CHR(45) END) AS UNIDADE_MEDIDA_MEDIO_PAGO
        ,'Total Efectivo' as DESC_TOTAL
         ]' || V_SCASE_SUM_SALDO || q'[
        ,0 AS H_NUM_IMPORTE_DISPONIBLE
        ,0 AS H_NUM_IMPORTE_NODISPONIBLE
        ,0 AS H_NEL_CANTIDAD_DISPONIBLE
        ,0 AS H_NEL_CANTIDAD_NODISPONIBLE
     FROM V_FILTRO_SALDO_CLIENTE VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA_HIST DCU
       ON DCU.OID_CUENTA = VF.OID_CUENTA
    INNER JOIN RPGE_YTRANSACCION_EFECTIVO SE
       ON VF.OID_CUENTA = SE.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA DIV
       ON DIV.OID_DIVISA = SE.OID_DIVISA
     LEFT OUTER JOIN RPGE_YDENOMINACION DNM
       ON DNM.OID_DENOMINACION = SE.OID_DENOMINACION
     LEFT OUTER JOIN RPGE_YUNIDAD_MEDIDA UM
       ON UM.OID_UNIDAD_MEDIDA = SE.OID_UNIDAD_MEDIDA
    WHERE VF.OID_DELEGACION IN(']' ||P_COM_DELEGACION || q'[')
      AND (]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,2))
      AND ]' || V_WBETWEEN_FECHA || q'[
      AND DIV.OID_DIVISA IN (']' ||P_COM_DIVISA || q'[')
      AND (]' || P_FILTRO_DENOMINACION || q'[ =0 OR DNM.OID_DENOMINACION IN(']' || P_COM_DENOMINACION || q'['))
      AND (]' || P_CONSIDERAR_TOTAL_EFECTIVO || q'[ =1 OR SE.COD_NIVEL_DETALLE NOT IN('T'))
    UNION ALL   /* 1er UNION ALL */
   SELECT se.cod_nivel_detalle
        ,div.oid_divisa
        ,DIV.DES_DIVISA
        ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
        ,(CASE ]' || P_DETALHAR_SECTOR || q'[
           WHEN 1 THEN (VF.DES_SECTOR)
           ELSE CHR(45) END) AS DES_SECTOR
        ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
        ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
        ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
        ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (UM.NUM_VALOR_UNIDAD)
           ELSE 0 END) AS NUM_VALOR_UNIDAD
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (DNM.NUM_VALOR)
           ELSE 0 END) AS VALOR_DENOMINACION
        ,(CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (UM.DES_UNIDAD_MEDIDA)
           ELSE CHR(45) END) AS UNIDADE_MEDIDA_MEDIO_PAGO
        ,'Total Efectivo' as DESC_TOTAL
        ,0 AS NUM_IMPORTE_DISPONIBLE
        ,0 AS NUM_IMPORTE_NODISPONIBLE
        ,0 AS NEL_CANTIDAD_DISPONIBLE
        ,0 AS NEL_CANTIDAD_NODISPONIBLE
        ]' || V_SCASE_SUM_SALDO_HIST || q'[
     FROM V_FILTRO_SALDO_CLIENTE VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA_HIST DCU
       ON DCU.OID_CUENTA = VF.OID_CUENTA
    INNER JOIN RPGE_YSALDO_EFECTIVO_HISTORICO SE
       ON VF.OID_CUENTA = SE.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA DIV
       ON DIV.OID_DIVISA = SE.OID_DIVISA
     LEFT OUTER JOIN RPGE_YDENOMINACION DNM
       ON DNM.OID_DENOMINACION = SE.OID_DENOMINACION
     LEFT OUTER JOIN RPGE_YUNIDAD_MEDIDA UM
       ON UM.OID_UNIDAD_MEDIDA = SE.OID_UNIDAD_MEDIDA
    WHERE VF.OID_DELEGACION IN(']' ||P_COM_DELEGACION || q'[')
      AND (]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,2))
      AND ]' || V_WBETWEEN_FECHA_HIST || q'[
      AND DIV.OID_DIVISA IN (']' ||P_COM_DIVISA || q'[')
      AND (]' || P_FILTRO_DENOMINACION || q'[ =0 OR DNM.OID_DENOMINACION IN(']' || P_COM_DENOMINACION || q'['))
      AND (]' || P_CONSIDERAR_TOTAL_EFECTIVO || q'[ =1 OR SE.COD_NIVEL_DETALLE NOT IN('T'))
   )Q1
   GROUP BY DES_DIVISA
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
   --
   UNION ALL /* UNION ALL entre 1er UNION ALL Y 2do UNION ALL */
   --
   SELECT 'Titulo' as DES_TITULO
      ,q1.DES_DIVISA
      ,q1.CLIENTE AS DES_CLIENTE
      ,q1.cod_nivel_detalle
      ,(CASE
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (UNIDADE_MEDIDA_MEDIO_PAGO || ' ' || CHR(45) || ' ' || TO_CHAR(SUM(NEL_CANTIDAD_DISPONIBLE)) ||
            ' unidad(es)')
         ELSE UNIDADE_MEDIDA_MEDIO_PAGO END) DES_DISP
      ,(CASE
         WHEN ]' || P_DETALHAR_DIVISA || q'[ = 1 AND q1.cod_nivel_detalle = 'D' THEN
            (UNIDADE_MEDIDA_MEDIO_PAGO || ' ' || CHR(45) || ' ' || TO_CHAR(SUM(NEL_CANTIDAD_DISPONIBLE)) ||
            ' unidad(es)')
         ELSE UNIDADE_MEDIDA_MEDIO_PAGO END) DES_NO_DISP
      ,q1.DES_SECTOR
      ,q1.SUB_CLIENTE AS DES_SUBCLIENTE
      ,q1.PUNTO_SERVICIO AS DES_PTO_SERVICIO
      ,q1.CANAL AS DES_CANAL
      ,q1.SUB_CANAL AS DES_SUBCANAL
      ,q1.NUM_VALOR_UNIDAD
      ,q1.VALOR_DENOMINACION AS NUM_VALOR_DENOMINACION
      ,q1.UNIDADE_MEDIDA_MEDIO_PAGO AS DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,q1.DESC_TOTAL AS DES_TOTAL
      ]' || V_SSUM_SALDO || q'[
   FROM
   ( SELECT
         (CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (SE.cod_nivel_detalle)
           ELSE CHR(45) END) AS cod_nivel_detalle
        ,SE.BOL_DISPONIBLE
        ,DIV.DES_DIVISA
        ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
        ,(CASE ]' || P_DETALHAR_SECTOR || q'[
           WHEN 1 THEN (VF.DES_SECTOR)
           ELSE CHR(45) END) AS DES_SECTOR
        ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
        ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
        ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
        ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
        ,0 AS NUM_VALOR_UNIDAD
        ,0 AS VALOR_DENOMINACION
        ,(CASE
           WHEN SE.cod_nivel_detalle = 'D' AND ]' || P_DETALHAR_DIVISA || q'[ = 1 THEN
              MP.DES_MEDIO_PAGO
           WHEN SE.cod_nivel_detalle = 'D' AND  ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN
             decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
           WHEN SE.cod_nivel_detalle = 'T'  THEN
             decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
           END) AS UNIDADE_MEDIDA_MEDIO_PAGO
        ,decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores') AS DESC_TOTAL
        ]' || V_SCASE_SUM_SALDO || q'[
        ,0 AS H_NUM_IMPORTE_DISPONIBLE
        ,0 AS H_NUM_IMPORTE_NODISPONIBLE
        ,0 AS H_NEL_CANTIDAD_DISPONIBLE
        ,0 AS H_NEL_CANTIDAD_NODISPONIBLE
     FROM V_FILTRO_SALDO_CLIENTE VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA_HIST DCU
       ON DCU.OID_CUENTA = VF.OID_CUENTA
    INNER JOIN RPGE_YTRANSACCION_MEDIO_PAGO SE
       ON VF.OID_CUENTA = SE.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA DIV
       ON DIV.OID_DIVISA = SE.OID_DIVISA
     LEFT OUTER JOIN RPGE_YMEDIO_PAGO MP
       ON MP.OID_MEDIO_PAGO = SE.OID_MEDIO_PAGO
      AND MP.OID_DIVISA = SE.OID_DIVISA
    WHERE VF.OID_DELEGACION IN(']' || P_COM_DELEGACION || q'[')
      AND ( ]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,3))
      AND ]' || V_WBETWEEN_FECHA || q'[
      AND ( ]' || P_FILTRO_MEDIO_PAGO || q'[ =0 OR MP.OID_MEDIO_PAGO IN(']' || P_COM_MEDIO_PAGO || q'['))
      AND ( ]' || P_CONSIDERAR_TOTAL_MEDIO_PAGO || q'[ =1 OR SE.COD_NIVEL_DETALLE NOT IN('T'))
      AND DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
    UNION ALL  /* 2do UNION ALL */
     SELECT
         (CASE ]' || P_DETALHAR_DIVISA || q'[
           WHEN 1 THEN (SE.cod_nivel_detalle)
           ELSE CHR(45) END) AS cod_nivel_detalle
        ,SE.BOL_DISPONIBLE
        ,DIV.DES_DIVISA
        ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
        ,(CASE ]' || P_DETALHAR_SECTOR || q'[
           WHEN 1 THEN (VF.DES_SECTOR)
           ELSE CHR(45) END) AS DES_SECTOR
        ,VF.COD_SUBCLIENTE || CHR(45) || VF.DES_SUBCLIENTE AS SUB_CLIENTE
        ,VF.COD_PTO_SERVICIO || CHR(45) || VF.DES_PTO_SERVICIO AS PUNTO_SERVICIO
        ,(VF.COD_CANAL || CHR(45) || VF.DES_CANAL) AS CANAL
        ,(VF.COD_SUBCANAL || CHR(45) || VF.DES_SUBCANAL) AS SUB_CANAL
        ,0 AS NUM_VALOR_UNIDAD
        ,0 AS VALOR_DENOMINACION
        ,(CASE
           WHEN SE.cod_nivel_detalle = 'D' AND ]' || P_DETALHAR_DIVISA || q'[ = 1 THEN
              MP.DES_MEDIO_PAGO
           WHEN SE.cod_nivel_detalle = 'D' AND  ]' || P_DETALHAR_DIVISA || q'[ = 0 THEN
             decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
           WHEN SE.cod_nivel_detalle = 'T'  THEN
             decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores')
           END) AS UNIDADE_MEDIDA_MEDIO_PAGO
        ,decode(SE.COD_TIPO_MEDIO_PAGO,'codtipoa','Total Ticket','codtipob','Total Cheque','codtipoc','Total Tarjetas','codtipo','Total Otros Valores') AS DESC_TOTAL
        ,0 AS NUM_IMPORTE_DISPONIBLE
        ,0 AS NUM_IMPORTE_NODISPONIBLE
        ,0 AS NEL_CANTIDAD_DISPONIBLE
        ,0 AS NEL_CANTIDAD_NODISPONIBLE
        ]' || V_SCASE_SUM_SALDO_HIST || q'[
     FROM V_FILTRO_SALDO_CLIENTE VF
    INNER JOIN RPGE_GTT_TDATOS_CUENTA_HIST DCU
       ON DCU.OID_CUENTA = VF.OID_CUENTA
    INNER JOIN RPGE_YSALDO_MEDIO_PAGO_HIST SE
       ON VF.OID_CUENTA = SE.OID_CUENTA_SALDO
    INNER JOIN RPGE_YDIVISA DIV
       ON DIV.OID_DIVISA = SE.OID_DIVISA
     LEFT OUTER JOIN RPGE_YMEDIO_PAGO MP
       ON MP.OID_MEDIO_PAGO = SE.OID_MEDIO_PAGO
      AND MP.OID_DIVISA = SE.OID_DIVISA
    WHERE VF.OID_DELEGACION IN(']' || P_COM_DELEGACION || q'[')
      AND ( ]' || P_CONSID_EFECTIVO_MEDIO_PAGO || q'[ IN(1,3))
      AND ]' || V_WBETWEEN_FECHA_HIST || q'[
      AND ( ]' || P_FILTRO_MEDIO_PAGO || q'[ =0 OR MP.OID_MEDIO_PAGO IN(']' || P_COM_MEDIO_PAGO || q'['))
      AND ( ]' || P_CONSIDERAR_TOTAL_MEDIO_PAGO || q'[ =1 OR SE.COD_NIVEL_DETALLE NOT IN('T'))
      AND DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
   )Q1
   GROUP BY DES_DIVISA
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
   )
   SELECT DISTINCT
      'Titulo' as DES_TITULO
      ,nvl(saldo.DES_DIVISA,sem_saldo.DES_DIVISA) as DES_DIVISA
      ,nvl(saldo.DES_CLIENTE,sem_saldo.CLIENTE) as DES_CLIENTE
      ,nvl(saldo.cod_nivel_detalle,sem_saldo.cod_nivel_detalle) as cod_nivel_detalle
      ,nvl(saldo.des_disp,'-') as des_disp
      ,nvl(saldo.des_no_disp,'-') as des_no_disp
      ,nvl(saldo.DES_SECTOR,sem_saldo.DES_SECTOR) as DES_SECTOR
      ,nvl(saldo.DES_SUBCLIENTE,sem_saldo.SUB_CLIENTE) as DES_SUBCLIENTE
      ,nvl(saldo.DES_PTO_SERVICIO,sem_saldo.PUNTO_SERVICIO) as DES_PTO_SERVICIO
      ,nvl(saldo.DES_CANAL,sem_saldo.CANAL) as DES_CANAL
      ,nvl(saldo.DES_SUBCANAL,sem_saldo.SUB_CANAL) as DES_SUBCANAL
      ,nvl(saldo.NUM_VALOR_DENOMINACION,sem_saldo.VALOR_DENOMINACION) as NUM_VALOR_DENOMINACION
      ,nvl(saldo.DES_UNIDAD_MEDIDA_MEDIO_PAGO,sem_saldo.UNIDADE_MEDIDA_MEDIO_PAGO) as DES_UNIDAD_MEDIDA_MEDIO_PAGO
      ,nvl(saldo.DES_TOTAL,sem_saldo.DESC_TOTAL) as DES_TOTAL
      ,nvl(saldo.NUM_IMPORTE_DISPONIBLE,0) asNUM_IMPORTE_DISPONIBLE
      ,nvl(saldo.NUM_IMPORTE_NODISPONIBLE,0) as NUM_IMPORTE_NODISPONIBLE
      ,nvl(saldo.NEL_CANTIDAD_DISPONIBLE,0) as NEL_CANTIDAD_DISPONIBLE
      ,nvl(saldo.NEL_CANTIDAD_NODISPONIBLE,0) as NEL_CANTIDAD_NODISPONIBLE
    FROM saldo
   RIGHT OUTER JOIN (
       SELECT
          (CASE ]' || P_DETALHAR_DIVISA || q'[ /* Indica se vai detalhar por divisa*/
            WHEN 1 THEN 'D'
            ELSE 'Total Efectivo' END) AS cod_nivel_detalle
          ,1 AS BOL_DISPONIBLE
          ,DIV.DES_DIVISA
          ,(VF.COD_CLIENTE || CHR(45) || VF.DES_CLIENTE) AS CLIENTE
          ,(CASE ]' || P_DETALHAR_SECTOR || q'[ /*Indica se vai agrupar os dados para os sectores*/
             WHEN 1 THEN (VF.DES_SECTOR)
             ELSE CHR(45) END) AS DES_SECTOR
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
         FROM V_FILTRO_SALDO_CLIENTE VF
        INNER JOIN RPGE_GTT_TDATOS_CUENTA_HIST DCU
           ON DCU.OID_CUENTA = VF.OID_CUENTA
        INNER JOIN RPGE_YDIVISA DIV
           ON DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
        WHERE VF.OID_DELEGACION IN(']' || P_COM_DELEGACION  || q'[')
          AND DIV.OID_DIVISA IN (']' || P_COM_DIVISA || q'[')
                     ) SEM_SALDO
      ON sem_saldo.des_divisa = saldo.DES_DIVISA
     AND sem_saldo.DES_SECTOR = saldo.DES_SECTOR
     AND sem_saldo.cliente = saldo.DES_CLIENTE
     AND sem_saldo.canal = saldo.DES_CANAL
     AND sem_saldo.SUB_CANAL = saldo.DES_SUBCANAL
     AND sem_saldo.SUB_CLIENTE = saldo.DES_SUBCLIENTE
     AND sem_saldo.PUNTO_SERVICIO = saldo.DES_PTO_SERVICIO
   WHERE (]' || P_CLIENTE_SEM_SALDO || q'[ =1 OR NUM_IMPORTE_DISPONIBLE <> 0 OR NUM_IMPORTE_NODISPONIBLE <> 0)]';

   V_DES_CONTROL_ERROR := 'INSERTANDO DATOS EN TABLA RPGE_GTT_TSALDOS_HISTORICO';

   EXECUTE IMMEDIATE ' INSERT INTO RPGE_GTT_TSALDOS_HISTORICO ' || V_SQL;
    
   /*Esse if foi necessário para não exibir zero(0) do cliente somando o detalhado com o total*/
   IF P_CLIENTE_SEM_SALDO = 0 AND P_DETALHAR_DIVISA = 0 THEN
      OPEN P_RETORNO_SALDOS FOR
       SELECT DES_TITULO,
             DES_DIVISA,
             DES_CLIENTE,
             NULL AS COD_NIVEL_DETALLE,
             DES_DISP,
             DES_NO_DISP,
             DES_SECTOR,
             DES_SUBCLIENTE,
             DES_PTO_SERVICIO,
             DES_CANAL,
             DES_SUBCANAL,
             NUM_VALOR_DENOMINACION,
             DES_UNIDAD_MEDIDA_MEDIO_PAGO,
             DES_TOTAL,
             SUM(NUM_IMPORTE_DISPONIBLE)AS NUM_IMPORTE_DISPONIBLE,
             SUM(NUM_IMPORTE_NODISPONIBLE) AS NUM_IMPORTE_NODISPONIBLE,
             SUM(NEL_CANTIDAD_DISPONIBLE) AS NEL_CANTIDAD_DISPONIBLE,
             SUM(NEL_CANTIDAD_NODISPONIBLE) AS NEL_CANTIDAD_NODISPONIBLE
        FROM RPGE_GTT_TSALDOS_HISTORICO
      HAVING ((SUM(NUM_IMPORTE_DISPONIBLE)+ SUM(NUM_IMPORTE_NODISPONIBLE))<>0)
       GROUP BY
          DES_TITULO,
          DES_DIVISA,
          DES_CLIENTE,
          DES_DISP,
          DES_NO_DISP,
          DES_SECTOR,
          DES_SUBCLIENTE,
          DES_PTO_SERVICIO,
          DES_CANAL,
          DES_SUBCANAL,
          NUM_VALOR_DENOMINACION,
          DES_UNIDAD_MEDIDA_MEDIO_PAGO,
          DES_TOTAL
       ORDER BY
          DES_DIVISA,
          DES_SECTOR,
          DES_CLIENTE,
          DES_SUBCLIENTE,
          DES_PTO_SERVICIO,
          DES_CANAL,
          DES_SUBCANAL,
          DES_DISP,
          DES_NO_DISP;
   ELSE
      OPEN P_RETORNO_SALDOS FOR
      SELECT DES_TITULO,
             DES_DIVISA,
             DES_CLIENTE,
             COD_NIVEL_DETALLE,
             DES_DISP,
             DES_NO_DISP,
             DES_SECTOR,
             DES_SUBCLIENTE,
             DES_PTO_SERVICIO,
             DES_CANAL,
             DES_SUBCANAL,
             NUM_VALOR_DENOMINACION,
             DES_UNIDAD_MEDIDA_MEDIO_PAGO,
             DES_TOTAL,
             NUM_IMPORTE_DISPONIBLE,
             NUM_IMPORTE_NODISPONIBLE,
             NEL_CANTIDAD_DISPONIBLE,
             NEL_CANTIDAD_NODISPONIBLE
        FROM RPGE_GTT_TSALDOS_HISTORICO
       ORDER BY
          DES_DIVISA,
          DES_SECTOR,
          DES_CLIENTE,
          DES_SUBCLIENTE,
          DES_PTO_SERVICIO,
          DES_CANAL,
          DES_SUBCANAL,
          DES_DISP,
          DES_NO_DISP;
   END IF;

COMMIT;

EXCEPTION
   WHEN V_NO_RESULT THEN
         raise_application_error(-20002,
                                 sqlcode || '   ' || sqlerrm || ' ' ||
                                 V_DES_CONTROL_ERROR);
   WHEN OTHERS THEN
      BEGIN
         --si hay error se limpian las tablas temporales EXECUTE IMMEDIATE 'TRUNCATE TABLE RPGE_GTT_TDATOS_CUENTA_HIST';
         EXECUTE IMMEDIATE 'TRUNCATE TABLE RPGE_GTT_TSALDOS_HISTORICO';
         raise_application_error(-20002,
                                 sqlcode || '   ' || sqlerrm || ' ' ||
                                 V_DES_CONTROL_ERROR);
      END;
END;
/