CREATE OR REPLACE FUNCTION FN_GMT_DELEGACION_###VERSION###(par$oid_delegacion IN gepr_pcomon_###VERSION###.tipo$oid_,
                                             par$fyh_gestion    IN gepr_pcomon_###VERSION###.tipo$fyh_) RETURN gepr_pcomon_###VERSION###.tipo$fyh_ IS

    /*Version: ###VERSION_COMP###*/

    var$HusoHorarioEnMinutos  GEPR_TDELEGACION.NEC_GMT_MINUTOS % TYPE := 0;
    var$FechaHoraVeranoInicio GEPR_TDELEGACION.FYH_VERANO_INICIO % TYPE := NULL;
    var$FechaHoraVeranoFin    GEPR_TDELEGACION.FYH_VERANO_FIN % TYPE := NULL;
    var$AjusteHorarioVerano   GEPR_TDELEGACION.NEC_VERANO_AJUSTE % TYPE := 0;
    var$nel_gmt_minuto        gepr_pcomon_###VERSION###.tipo$nel_;
    var$fyh_fecha             gepr_pcomon_###VERSION###.tipo$fyh_ := NULL;

BEGIN

      IF par$fyh_gestion IS NOT NULL THEN

        /* Verifica si la delegacion existe y recupera las informaciones de GMT */
        BEGIN
          SELECT NEC_GMT_MINUTOS,
                 FYH_VERANO_INICIO,
                 FYH_VERANO_FIN,
                 NEC_VERANO_AJUSTE
            INTO var$HusoHorarioEnMinutos,
                 var$FechaHoraVeranoInicio,
                 var$FechaHoraVeranoFin,
                 var$AjusteHorarioVerano
            FROM GEPR_TDELEGACION
           WHERE OID_DELEGACION = par$oid_delegacion;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
              var$HusoHorarioEnMinutos := 0;
              var$FechaHoraVeranoInicio := NULL;
              var$FechaHoraVeranoFin := NULL;
              var$AjusteHorarioVerano := NULL;
        END;

        IF var$HusoHorarioEnMinutos <> 0 THEN
          var$nel_gmt_minuto := var$HusoHorarioEnMinutos;
        END IF;

        IF ((nvl(var$AjusteHorarioVerano, 0) > 0) AND
           (var$FechaHoraVeranoInicio IS NOT NULL) AND
           (var$FechaHoraVeranoFin IS NOT NULL)) THEN

          /* si necesario a�adir los minutos de horario verano */
          IF (par$fyh_gestion >= var$FechaHoraVeranoInicio) AND
             (par$fyh_gestion < (var$FechaHoraVeranoFin + (var$HusoHorarioEnMinutos / 1440))) THEN

             IF var$nel_gmt_minuto IS NULL THEN
               var$nel_gmt_minuto := 0;
             END IF;
            var$nel_gmt_minuto := var$nel_gmt_minuto + var$AjusteHorarioVerano;
          END IF;
        END IF;
        
        IF var$nel_gmt_minuto IS NOT NULL THEN
           var$fyh_fecha := par$fyh_gestion + var$nel_gmt_minuto / 1440;
        ELSE
           var$fyh_fecha := par$fyh_gestion;
        END IF;  

     END IF;

     RETURN var$fyh_fecha;

END FN_GMT_DELEGACION_###VERSION###;
/
-- Se crea momentaneamente para evitar problemas con otros pacakges
CREATE OR REPLACE FUNCTION FN_GMT_DELEGACION(par$oid_delegacion IN VARCHAR2, 
                                             par$fyh_gestion    IN DATE) RETURN DATE IS

	/*Version: ###VERSION_COMP###*/

    var$HusoHorarioEnMinutos  GEPR_TDELEGACION.NEC_GMT_MINUTOS % TYPE := 0;
    var$FechaHoraVeranoInicio GEPR_TDELEGACION.FYH_VERANO_INICIO % TYPE := NULL;
    var$FechaHoraVeranoFin    GEPR_TDELEGACION.FYH_VERANO_FIN % TYPE := NULL;
    var$AjusteHorarioVerano   GEPR_TDELEGACION.NEC_VERANO_AJUSTE % TYPE := 0;
    var$nel_gmt_minuto        NUMBER(38);
    var$fyh_fecha             DATE := NULL;
    
BEGIN

      IF par$fyh_gestion IS NOT NULL THEN
          
        /* Verifica si la delegacion existe y recupera las informaciones de GMT */
        BEGIN
          SELECT NEC_GMT_MINUTOS,
                 FYH_VERANO_INICIO,
                 FYH_VERANO_FIN,
                 NEC_VERANO_AJUSTE
            INTO var$HusoHorarioEnMinutos,
                 var$FechaHoraVeranoInicio,
                 var$FechaHoraVeranoFin,
                 var$AjusteHorarioVerano
            FROM GEPR_TDELEGACION
           WHERE OID_DELEGACION = par$oid_delegacion;
        EXCEPTION
          WHEN NO_DATA_FOUND THEN
              var$HusoHorarioEnMinutos := 0;
              var$FechaHoraVeranoInicio := NULL;
              var$FechaHoraVeranoFin := NULL;
              var$AjusteHorarioVerano := NULL;
        END;

        IF var$HusoHorarioEnMinutos <> 0 THEN
          var$nel_gmt_minuto := var$HusoHorarioEnMinutos;
        END IF;

        IF ((NVL(var$AjusteHorarioVerano, 0) <> 0) AND
           (var$FechaHoraVeranoInicio IS NOT NULL) AND
           (var$FechaHoraVeranoFin IS NOT NULL)) THEN

          /* si necesario añadir los minutos de horario verano */
          IF (par$fyh_gestion >= var$FechaHoraVeranoInicio) AND
             (par$fyh_gestion < (var$FechaHoraVeranoFin + (var$HusoHorarioEnMinutos / 1440))) THEN

             IF var$nel_gmt_minuto IS NULL THEN
               var$nel_gmt_minuto := 0;
             END IF;
            var$nel_gmt_minuto := var$nel_gmt_minuto + var$AjusteHorarioVerano;
          END IF;
        END IF;
        
        IF var$nel_gmt_minuto IS NOT NULL THEN
           var$fyh_fecha := par$fyh_gestion + var$nel_gmt_minuto / 1440;
        ELSE
           var$fyh_fecha := par$fyh_gestion;
        END IF;  

     END IF;
      
     RETURN var$fyh_fecha;

END FN_GMT_DELEGACION;
/