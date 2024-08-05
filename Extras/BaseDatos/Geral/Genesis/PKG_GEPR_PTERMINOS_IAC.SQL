CREATE OR REPLACE PACKAGE GEPR_PTERMINOS_IAC_###VERSION### AS
  /*Version: ###VERSION_COMP###*/

  /* sins_iac_remesa */
  PROCEDURE sins_iac_remesa(par$oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_iac_bulto */
  PROCEDURE sins_iac_bulto(par$oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                           par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_iac_parcial */
  PROCEDURE sins_iac_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                             par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sins_iac_medio_pago */
  PROCEDURE sins_iac_medio_pago(par$oid_contado_mp   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_declarado_mp IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_termino      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$des_valor        IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$nec_ind_grupo    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                par$usuario          IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_iac_remesa */
  PROCEDURE sdel_tvalor_iac_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$deletes   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_iac_bulto */
  PROCEDURE sdel_tvalor_iac_bulto(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$deletes  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_iac_parcial */
  PROCEDURE sdel_tvalor_iac_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_term_mp_remesa */
  PROCEDURE sdel_tvalor_term_mp_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$deletes   OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_term_mp_bulto */
  PROCEDURE sdel_tvalor_term_mp_bulto(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes  OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_term_mp_parcial */
  PROCEDURE sdel_tvalor_term_mp_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_iac */
  PROCEDURE sdel_tvalor_iac(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$eliminar_hijos IN BOOLEAN,
                            par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

  /* sdel_tvalor_term_mp */
  PROCEDURE sdel_tvalor_term_mp(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$eliminar_hijos IN BOOLEAN,
                                par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_);

 /* Insert/update de un termino */
 PROCEDURE supd_termino (par$oid_termino        IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_termino        IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$des_termino        IN gepr_pcomon_###VERSION###.tipo$desc_,
                         par$obs_termino        IN gepr_pcomon_###VERSION###.tipo$obs_,
                         par$oid_formato        IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$nec_longitud       IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$oid_a_val          IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_mascara        IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$bol_mostrar_cod    IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_val_pos        IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_vigente        IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$bol_ac_digit       IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_esp_saldos     IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_migracion      IN gepr_pcomon_###VERSION###.tipo$cod_);



  PROCEDURE srecupera_por_codigo(par$cod_iac IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$rcIAC   OUT SYS_REFCURSOR);

 PROCEDURE srecupera_iac_terminos(par$rcIAC   OUT SYS_REFCURSOR);
                                 
END GEPR_PTERMINOS_IAC_###VERSION###;
/
CREATE OR REPLACE PACKAGE BODY GEPR_PTERMINOS_IAC_###VERSION### AS

  /* sins_iac_remesa */
  PROCEDURE sins_iac_remesa(par$oid_remesa  IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                            par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                            par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TVALOR_IACXREMESA
      (OID_VALOR_IACXREMESA,
       OID_REMESA,
       OID_TERMINO,
       DES_VALOR_IAC,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (SYS_GUID(),
       par$oid_remesa,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_iac_remesa;

  /* sins_iac_bulto */
  PROCEDURE sins_iac_bulto(par$oid_bulto   IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                           par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                           par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                           par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TVALOR_IACXBULTO
      (OID_VALOR_IACXBULTO,
       OID_BULTO,
       OID_TERMINO,
       DES_VALOR_IAC,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (SYS_GUID(),
       par$oid_bulto,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_iac_bulto;

  /* sins_iac_parcial */
  PROCEDURE sins_iac_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$oid_termino IN gepr_pcomon_###VERSION###.tipo$oid_,
                             par$des_valor   IN gepr_pcomon_###VERSION###.tipo$desc_,
                             par$usuario     IN gepr_pcomon_###VERSION###.tipo$usr_,
                             par$inserts    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TVALOR_IACXPARCIAL
      (OID_VALOR_IACXPARCIAL,
       OID_PARCIAL,
       OID_TERMINO,
       DES_VALOR_IAC,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION)
    VALUES
      (SYS_GUID(),
       par$oid_parcial,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario);
    par$inserts := sql%rowcount;
  END sins_iac_parcial;

  /* sins_iac_medio_pago */
  PROCEDURE sins_iac_medio_pago(par$oid_contado_mp   IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_declarado_mp IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_termino      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$des_valor        IN gepr_pcomon_###VERSION###.tipo$desc_,
                                par$nec_ind_grupo    IN gepr_pcomon_###VERSION###.tipo$nel_,
                                par$usuario          IN gepr_pcomon_###VERSION###.tipo$usr_,
                                par$inserts         OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
    var$gmt_zero VARCHAR2(50) := gepr_putilidades_###VERSION###.fgmt_zero;
  BEGIN
    INSERT INTO SAPR_TVALOR_TERM_MP_ELEMENTO
      (OID_VALOR_TERM_MP_ELEMENTO,
       OID_CONTADO_MEDIO_PAGO,
       OID_DECLARADO_MEDIO_PAGO,
       OID_TERMINO_MEDIO_PAGO,
       DES_VALOR,
       GMT_CREACION,
       DES_USUARIO_CREACION,
       GMT_MODIFICACION,
       DES_USUARIO_MODIFICACION,
       NEC_INDICE_GRUPO)
    VALUES
      (SYS_GUID(),
       par$oid_contado_mp,
       par$oid_declarado_mp,
       par$oid_termino,
       par$des_valor,
       var$gmt_zero,
       par$usuario,
       var$gmt_zero,
       par$usuario,
       par$nec_ind_grupo);
    par$inserts := sql%rowcount;
  END sins_iac_medio_pago;

  /* sdel_tvalor_iac_remesa */
  PROCEDURE sdel_tvalor_iac_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                   par$deletes   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_IACXREMESA WHERE OID_REMESA = par$oid_remesa;
    par$deletes := sql%rowcount;
  END sdel_tvalor_iac_remesa;

  /* sdel_tvalor_iac_bulto */
  PROCEDURE sdel_tvalor_iac_bulto(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                  par$deletes  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_IACXBULTO WHERE OID_BULTO = par$oid_bulto;
    par$deletes := sql%rowcount;
  END sdel_tvalor_iac_bulto;

  /* sdel_tvalor_iac_parcial */
  PROCEDURE sdel_tvalor_iac_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                    par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_IACXPARCIAL WHERE OID_PARCIAL = par$oid_parcial;
    par$deletes := sql%rowcount;
  END sdel_tvalor_iac_parcial;

  /* sdel_tvalor_term_mp_remesa */
  PROCEDURE sdel_tvalor_term_mp_remesa(par$oid_remesa IN gepr_pcomon_###VERSION###.tipo$oid_,
                                       par$deletes   OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
     WHERE OID_CONTADO_MEDIO_PAGO IN
           (SELECT OID_CONTADO_MEDIO_PAGO
              FROM SAPR_TCONTADO_MEDIO_PAGO
             WHERE OID_REMESA = par$oid_remesa
               AND OID_BULTO IS NULL
               AND OID_PARCIAL IS NULL)
        OR OID_DECLARADO_MEDIO_PAGO IN
           (SELECT OID_DECLARADO_MEDIO_PAGO
              FROM SAPR_TDECLARADO_MEDIO_PAGO
             WHERE OID_REMESA = par$oid_remesa
               AND OID_BULTO IS NULL
               AND OID_PARCIAL IS NULL);
     par$deletes := sql%rowcount;
  END sdel_tvalor_term_mp_remesa;

  /* sdel_tvalor_term_mp_bulto */
  PROCEDURE sdel_tvalor_term_mp_bulto(par$oid_bulto IN gepr_pcomon_###VERSION###.tipo$oid_,
                                      par$deletes  OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
     WHERE OID_CONTADO_MEDIO_PAGO IN
           (SELECT OID_CONTADO_MEDIO_PAGO
              FROM SAPR_TCONTADO_MEDIO_PAGO
             WHERE OID_BULTO = par$oid_bulto
               AND OID_PARCIAL IS NULL)
        OR OID_DECLARADO_MEDIO_PAGO IN
           (SELECT OID_DECLARADO_MEDIO_PAGO
              FROM SAPR_TDECLARADO_MEDIO_PAGO
             WHERE OID_BULTO = par$oid_bulto
               AND OID_PARCIAL IS NULL);
    par$deletes := sql%rowcount;
  END sdel_tvalor_term_mp_bulto;

  /* sdel_tvalor_term_mp_parcial */
  PROCEDURE sdel_tvalor_term_mp_parcial(par$oid_parcial IN gepr_pcomon_###VERSION###.tipo$oid_,
                                        par$deletes    OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
    DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
     WHERE OID_CONTADO_MEDIO_PAGO IN
           (SELECT OID_CONTADO_MEDIO_PAGO
              FROM SAPR_TCONTADO_MEDIO_PAGO
             WHERE OID_PARCIAL = par$oid_parcial)
        OR OID_DECLARADO_MEDIO_PAGO IN
           (SELECT OID_DECLARADO_MEDIO_PAGO
              FROM SAPR_TDECLARADO_MEDIO_PAGO
             WHERE OID_PARCIAL = par$oid_parcial);
     par$deletes := sql%rowcount;
  END sdel_tvalor_term_mp_parcial;

  /* sdel_tvalor_iac */
  PROCEDURE sdel_tvalor_iac(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                            par$eliminar_hijos IN BOOLEAN,
                            par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    IF par$eliminar_hijos THEN
    
      IF par$oid_remesa IS NOT NULL THEN
      
        IF par$oid_parcial IS NOT NULL THEN
          /* Eliminar Parcial - No hay hijos */
          DELETE SAPR_TVALOR_IACXPARCIAL
           WHERE OID_PARCIAL = par$oid_parcial;
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          /* Eliminar todas parciales hijas */
          DELETE SAPR_TVALOR_IACXPARCIAL
           WHERE OID_PARCIAL IN
                 (SELECT P.OID_PARCIAL
                    FROM SAPR_TPARCIAL P
                   WHERE P.OID_BULTO = par$oid_bulto);
          par$deletes := sql%rowcount;
          /* Eliminar bulto */
          DELETE SAPR_TVALOR_IACXBULTO
           WHERE OID_BULTO IN
                 (SELECT B.OID_BULTO
                    FROM SAPR_TBULTO B
                   WHERE B.OID_REMESA = par$oid_remesa);
          par$deletes := sql%rowcount;
        ELSE
          /* Eliminar todas parciales hijas */
          DELETE SAPR_TVALOR_IACXPARCIAL
           WHERE OID_PARCIAL IN
                 (SELECT P.OID_PARCIAL
                    FROM SAPR_TPARCIAL P
                   INNER JOIN SAPR_TBULTO B
                      ON B.OID_BULTO = P.OID_BULTO
                   WHERE B.OID_REMESA = par$oid_remesa);
          par$deletes := sql%rowcount;
          /* Eliminar bulto */
          DELETE SAPR_TVALOR_IACXBULTO
           WHERE OID_BULTO IN
                 (SELECT B.OID_BULTO
                    FROM SAPR_TBULTO B
                   WHERE B.OID_REMESA = par$oid_remesa);
          par$deletes := sql%rowcount;
          /* Eliminar remesa */
          DELETE SAPR_TVALOR_IACXREMESA WHERE OID_REMESA = par$oid_remesa;
          par$deletes := sql%rowcount;
        END IF;
      
      END IF;
    
    ELSE
      IF par$oid_parcial IS NOT NULL THEN
        DELETE SAPR_TVALOR_IACXPARCIAL WHERE OID_PARCIAL = par$oid_parcial;
        par$deletes := sql%rowcount;
      ELSIF par$oid_bulto IS NOT NULL THEN
        DELETE SAPR_TVALOR_IACXBULTO WHERE OID_BULTO = par$oid_bulto;
        par$deletes := sql%rowcount;
      ELSE
        DELETE SAPR_TVALOR_IACXREMESA WHERE OID_REMESA = par$oid_remesa;
        par$deletes := sql%rowcount;
      END IF;
    END IF;
  END sdel_tvalor_iac;

  /* sdel_tvalor_term_mp */
  PROCEDURE sdel_tvalor_term_mp(par$oid_remesa     IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_bulto      IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$oid_parcial    IN gepr_pcomon_###VERSION###.tipo$oid_,
                                par$eliminar_hijos IN BOOLEAN,
                                par$deletes        OUT gepr_pcomon_###VERSION###.tipo$nel_) IS
  BEGIN
  
    IF par$eliminar_hijos THEN
    
      IF par$oid_remesa IS NOT NULL THEN
      
        IF par$oid_parcial IS NOT NULL THEN
          DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
           WHERE OID_CONTADO_MEDIO_PAGO IN
                 (SELECT OID_CONTADO_MEDIO_PAGO
                    FROM SAPR_TCONTADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa
                     AND OID_BULTO = par$oid_bulto
                     AND OID_PARCIAL = par$oid_parcial)
              OR OID_DECLARADO_MEDIO_PAGO IN
                 (SELECT OID_DECLARADO_MEDIO_PAGO
                    FROM SAPR_TDECLARADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa
                     AND OID_BULTO = par$oid_bulto
                     AND OID_PARCIAL = par$oid_parcial);
          par$deletes := sql%rowcount;
        ELSIF par$oid_bulto IS NOT NULL THEN
          DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
           WHERE OID_CONTADO_MEDIO_PAGO IN
                 (SELECT OID_CONTADO_MEDIO_PAGO
                    FROM SAPR_TCONTADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa
                     AND OID_BULTO = par$oid_bulto)
              OR OID_DECLARADO_MEDIO_PAGO IN
                 (SELECT OID_DECLARADO_MEDIO_PAGO
                    FROM SAPR_TDECLARADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa
                     AND OID_BULTO = par$oid_bulto);
          par$deletes := sql%rowcount;
        ELSE
          DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
           WHERE OID_CONTADO_MEDIO_PAGO IN
                 (SELECT OID_CONTADO_MEDIO_PAGO
                    FROM SAPR_TCONTADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa)
              OR OID_DECLARADO_MEDIO_PAGO IN
                 (SELECT OID_DECLARADO_MEDIO_PAGO
                    FROM SAPR_TDECLARADO_MEDIO_PAGO
                   WHERE OID_REMESA = par$oid_remesa);
          par$deletes := sql%rowcount;
        END IF;
      
      END IF;
    
    ELSE
    
      IF par$oid_parcial IS NOT NULL THEN
        DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
         WHERE OID_CONTADO_MEDIO_PAGO IN
               (SELECT OID_CONTADO_MEDIO_PAGO
                  FROM SAPR_TCONTADO_MEDIO_PAGO
                 WHERE OID_PARCIAL = par$oid_parcial)
            OR OID_DECLARADO_MEDIO_PAGO IN
               (SELECT OID_DECLARADO_MEDIO_PAGO
                  FROM SAPR_TDECLARADO_MEDIO_PAGO
                 WHERE OID_PARCIAL = par$oid_parcial);
        par$deletes := sql%rowcount;
      ELSIF par$oid_bulto IS NOT NULL THEN
        DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
         WHERE OID_CONTADO_MEDIO_PAGO IN
               (SELECT OID_CONTADO_MEDIO_PAGO
                  FROM SAPR_TCONTADO_MEDIO_PAGO
                 WHERE OID_BULTO = par$oid_bulto
                   AND OID_PARCIAL IS NULL)
            OR OID_DECLARADO_MEDIO_PAGO IN
               (SELECT OID_DECLARADO_MEDIO_PAGO
                  FROM SAPR_TDECLARADO_MEDIO_PAGO
                 WHERE OID_BULTO = par$oid_bulto
                   AND OID_PARCIAL IS NULL);
        par$deletes := sql%rowcount;
      ELSE
        DELETE SAPR_TVALOR_TERM_MP_ELEMENTO
         WHERE OID_CONTADO_MEDIO_PAGO IN
               (SELECT OID_CONTADO_MEDIO_PAGO
                  FROM SAPR_TCONTADO_MEDIO_PAGO
                 WHERE OID_REMESA = par$oid_remesa
                   AND OID_BULTO IS NULL
                   AND OID_PARCIAL IS NULL)
            OR OID_DECLARADO_MEDIO_PAGO IN
               (SELECT OID_DECLARADO_MEDIO_PAGO
                  FROM SAPR_TDECLARADO_MEDIO_PAGO
                 WHERE OID_REMESA = par$oid_remesa
                   AND OID_BULTO IS NULL
                   AND OID_PARCIAL IS NULL);
        par$deletes := sql%rowcount;
      END IF;
    
    END IF;
  
  END sdel_tvalor_term_mp;

  PROCEDURE srecupera_por_codigo(par$cod_iac IN gepr_pcomon_###VERSION###.tipo$oid_,
                                 par$rcIAC   OUT SYS_REFCURSOR) IS
  BEGIN
    OPEN par$rcIAC FOR
      SELECT IAC.COD_IAC, T.OID_TERMINO, T.COD_TERMINO
        FROM GEPR_TINFORM_ADICIONAL_CLIENTE IAC
       INNER JOIN GEPR_TTERMINO_POR_IAC TPIAC
          ON TPIAC.OID_IAC = IAC.OID_IAC
       INNER JOIN GEPR_TTERMINO T
          ON T.OID_TERMINO = TPIAC.OID_TERMINO
       WHERE IAC.COD_IAC = par$cod_iac;
  END srecupera_por_codigo;

 PROCEDURE srecupera_iac_terminos(
    par$rcIAC   OUT SYS_REFCURSOR
   ) IS
   BEGIN
    OPEN par$rcIAC FOR
         SELECT 
             IAC.OID_IAC,
             IAC.COD_IAC,
             IAC.DES_IAC,
             TERMINO.OID_TERMINO,
             TERMINO.COD_TERMINO,
             TERMINO.DES_TERMINO
        FROM
            GEPR_TINFORM_ADICIONAL_CLIENTE IAC 
            INNER JOIN GEPR_TTERMINO_POR_IAC CRUCE ON CRUCE.OID_IAC = IAC.OID_IAC
            INNER JOIN GEPR_TTERMINO TERMINO ON TERMINO.OID_TERMINO = CRUCE.OID_TERMINO
        order by IAC.COD_IAC;
  END srecupera_iac_terminos; 

  /* Insert/update de un termino */
  PROCEDURE supd_termino (par$oid_termino        IN OUT gepr_pcomon_###VERSION###.tipo$oid_,
                         par$cod_termino        IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$des_termino        IN gepr_pcomon_###VERSION###.tipo$desc_,
                         par$obs_termino        IN gepr_pcomon_###VERSION###.tipo$obs_,
                         par$oid_formato        IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$nec_longitud       IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$oid_a_val          IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$oid_mascara        IN gepr_pcomon_###VERSION###.tipo$oid_,
                         par$bol_mostrar_cod    IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_val_pos        IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_vigente        IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_usuario        IN gepr_pcomon_###VERSION###.tipo$cod_,
                         par$bol_ac_digit       IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$bol_esp_saldos     IN gepr_pcomon_###VERSION###.tipo$nel_,
                         par$cod_migracion      IN gepr_pcomon_###VERSION###.tipo$cod_) IS
var$qry_update    gepr_pcomon_###VERSION###.tipo$obs_ ;

BEGIN

    BEGIN
      IF par$oid_termino IS NULL THEN

        par$oid_termino  := sys_guid();
        INSERT INTO GEPR_TTERMINO (
                    OID_TERMINO,
                    COD_TERMINO,
                    DES_TERMINO,
                    OBS_TERMINO,
                    OID_FORMATO,
                    NEC_LONGITUD,
                    OID_ALGORITMO_VALIDACION,
                    OID_MASCARA,
                    BOL_MOSTRAR_CODIGO,
                    BOL_VALORES_POSIBLES,
                    BOL_VIGENTE,
                    COD_USUARIO,
                    FYH_ACTUALIZACION,
                    BOL_ACEPTAR_DIGITACION,
                    BOL_ESPECIFICO_DE_SALDOS,
                    COD_MIGRACION
                  )
                  VALUES
                  ( par$oid_termino,
                    par$cod_termino,
                    par$des_termino,
                    par$obs_termino,
                    par$oid_formato,
                    par$nec_longitud,
                    par$oid_a_val,
                    par$oid_mascara,
                    par$bol_mostrar_cod,
                    par$bol_val_pos,
                    par$bol_vigente,
                    par$cod_usuario,
                    CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE),
                    par$bol_ac_digit,
                    par$bol_esp_saldos,
                    par$cod_migracion);
      ELSE
           IF  par$cod_termino IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ COD_TERMINO = ']' || par$cod_termino ||q'[', ]';
           END IF;

           IF  par$des_termino IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ DES_TERMINO = ']' || par$des_termino ||q'[', ]';
           END IF;

           IF  par$obs_termino IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OBS_TERMINO = ']' || par$obs_termino ||q'[', ]';
           END IF;

           IF  par$nec_longitud IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ NEC_LONGITUD = ']' || par$nec_longitud ||q'[', ]';
           END IF;

           IF  par$oid_a_val IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ OID_ALGORITMO_VALIDACION = ']' || par$oid_a_val ||q'[', ]';
           END IF;

           IF  par$oid_mascara IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ OID_MASCARA = ']' || par$oid_mascara ||q'[', ]';
           END IF;

           IF  par$bol_mostrar_cod IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_MOSTRAR_CODIGO = ']' || par$bol_mostrar_cod ||q'[', ]';
           END IF;

           IF  par$bol_val_pos IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_VALORES_POSIBLES = ']' || par$bol_val_pos ||q'[', ]';
           END IF;

           IF  par$bol_vigente IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_VIGENTE = ']' || par$bol_vigente ||q'[', ]';
           END IF;

           IF  par$bol_ac_digit IS NOT NULL THEN
               var$qry_update := var$qry_update || q'[ BOL_ACEPTAR_DIGITACION = ']' || par$bol_ac_digit ||q'[', ]';
           END IF;

           IF  par$bol_esp_saldos IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ BOL_ESPECIFICO_DE_SALDOS = ']' || par$bol_esp_saldos ||q'[', ]';
           END IF;

           IF  par$cod_migracion IS NOT NULL  THEN
               var$qry_update := var$qry_update || q'[ COD_MIGRACION = ']' || par$cod_migracion ||q'[', ]';
           END IF;


           EXECUTE IMMEDIATE q'[ UPDATE GEPR_TTERMINO SET ]' || var$qry_update ||q'[
             FYH_ACTUALIZACION = :1, COD_USUARIO = :2
             WHERE OID_TERMINO = :3]'
              USING CAST(sys_extract_utc(SYSTIMESTAMP) AS DATE), par$cod_usuario, par$oid_termino;
           
    END IF;
    END;
  
END supd_termino;
  
END GEPR_PTERMINOS_IAC_###VERSION###;
/