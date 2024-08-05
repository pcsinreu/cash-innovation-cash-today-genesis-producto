CREATE OR REPLACE PACKAGE gepr_pmod_modelo_DD_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  PROCEDURE sobtener_accion(par$cod_accion    IN varchar2,
                            par$rc_accion     OUT sys_refcursor,
                            par$rc_scripts    OUT sys_refcursor,
                            par$rc_xsls       OUT sys_refcursor,
                            par$rc_parametros OUT sys_refcursor,
                            par$rc_htmljs     OUT sys_refcursor,
                            par$rc_filtro     OUT sys_refcursor,
                            par$rc_resultados    OUT sys_refcursor);

END gepr_pmod_modelo_DD_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY gepr_pmod_modelo_DD_###VERSION### AS

  PROCEDURE sobtener_accion(par$cod_accion    IN varchar2,
                            par$rc_accion     OUT sys_refcursor,
                            par$rc_scripts    OUT sys_refcursor,
                            par$rc_xsls       OUT sys_refcursor,
                            par$rc_parametros OUT sys_refcursor,
                            par$rc_htmljs     OUT sys_refcursor,
                            par$rc_filtro     OUT sys_refcursor,
                            par$rc_resultados    OUT sys_refcursor) IS
  
  BEGIN
  
    open par$rc_accion for
      select NEC_ACCION, COD_ACCION, DES_ACCION, MODO
        from gepr_tmod_acciones
       where cod_accion = par$cod_accion;
  
    open par$rc_scripts for
      SELECT s.NEC_ACCION,
             s.NEC_SCRIPT,
             S.COD_CONEXION,
             s.OBS_SCRIPT,
             s.DES_DESCRIPCION,
             s.NEC_ORDEN
        FROM GEPR_TMOD_SCRIPTS s
       inner join gepr_tmod_acciones a
          on s.NEC_ACCION = a.NEC_ACCION
       where a.cod_accion = par$cod_accion
       order by s.NEC_ORDEN;
  
    open par$rc_xsls for
      SELECT x.NEC_ACCION,
             x.NEC_XSL,
             x.DES_NOMBRE,
             x.DES_PATH_RELATIVO,
             x.DES_DESCRIPCION,
             x.NEC_ORDEN            
        FROM GEPR_TMOD_XSLS x
       inner join gepr_tmod_acciones a
          on x.NEC_ACCION = a.NEC_ACCION
       where a.cod_accion = par$cod_accion
       order by x.NEC_ORDEN;
  
    open par$rc_parametros for
      SELECT p.NEC_ACCION,
             p.NEC_SCRIPT,
             p.NEC_PARAMETRO,
             P.COD_TIPO_DATO,
             p.COD_ELEMENTO_REQUEST,
             p.COD_METODO_REQUEST,
             p.COD_PARAMETRO_SCRIPT
        FROM GEPR_TMOD_PARAMETROS p
       inner join gepr_tmod_acciones a
          on p.NEC_ACCION = a.NEC_ACCION
       where a.cod_accion = par$cod_accion;
  
    open par$rc_htmljs for
      SELECT A.NEC_ACCION     ID_ACCION,
             A.COD_ACCION     ACCION,
             HJ.DES_PATH_HTML HTML,
             HJ.DES_PATH_JS   JS
        FROM GEPR_TMOD_HTMLJS HJ, GEPR_TMOD_ACCIONES A
       WHERE HJ.NEC_ACCION = A.NEC_ACCION
         AND A.COD_ACCION = PAR$COD_ACCION;
         
     open par$rc_filtro for
     SELECT FILTROS.PATH
     FROM GEPR_TMOD_FILTROS FILTROS,
          GEPR_TMOD_ACCIONES ACCIONES
     WHERE FILTROS.NEC_ACCION = ACCIONES.NEC_ACCION
       AND ACCIONES.COD_ACCION = PAR$COD_ACCION;
  
     open par$rc_resultados for
     SELECT RESULTADOS.*
     FROM GEPR_TMOD_RESULTADOS RESULTADOS,
          GEPR_TMOD_ACCIONES ACCIONES
     WHERE 
     RESULTADOS.NEC_ACCION = ACCIONES.NEC_ACCION
     AND ACCIONES.COD_ACCION = PAR$COD_ACCION;
     
  END sobtener_accion;

END gepr_pmod_modelo_DD_###VERSION###;
/
