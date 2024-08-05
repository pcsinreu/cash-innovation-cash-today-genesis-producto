SELECT distinct total.iddocumento,
                total.idprimordial,
                total.fecha,
                total.numcomprobante,
                total.idcentroprocesoorigen,
                total.centroprocesoorigendesc,
                total.idcentroprocesodestino,
                total.centroprocesodestinodesc,
                total.idclienteorigen,
                total.clienteorigendesc,
                total.idclientedestino,
                total.clientedestinodesc,
                total.idbanco,
                total.bancodesc,
                total.idbancodeposito,
                total.bancodepositodesc,
                total.idestadocomprobante,
                total.idusuario,
                total.idformulario,
                total.idbancodeposito,
                total.idusuarioresuelve,
                total.fecharesuelve,
                total.fechagestion,
                total.numexterno,
                total.agrupado,
                total.esgrupo,
                total.idgrupo,
                total.idorigen,
                total.reenviado,
                total.disponible,
                total.idusuariodispone,
                total.fechadispone,
                total.sustituido,
                total.essustituto,
                total.iddocdetalles,
                total.descripcion,
                total.conbultos,
                total.convalores
  FROM (SELECT DISTINCT dc.iddocumento,
                        dc.idprimordial,
                        dc.fecha,
                        dc.numcomprobante,
                        dc.idcentroprocesoorigen,
                        cpo.descripcion AS centroprocesoorigendesc,
                        dc.idcentroprocesodestino,
                        cpd.descripcion AS centroprocesodestinodesc,
                        dc.idclienteorigen,
                        co.idps || '-' || co.descripcion AS clienteorigendesc,
                        dc.idclientedestino,
                        cd.idps || '-' || cd.descripcion AS clientedestinodesc,
                        dc.idbanco,
                        bc.idps || '-' || bc.descripcion AS bancodesc,
                        dc.idbancodeposito,
                        bdc.idps || '-' || bdc.descripcion AS bancodepositodesc,
                        dc.idestadocomprobante,
                        dc.idusuario,
                        dc.idformulario,
                        dc.idusuarioresuelve,
                        dc.fecharesuelve,
                        dc.fechagestion,
                        dc.numexterno,
                        NVL(dc.agrupado, 0) AS agrupado,
                        NVL(dc.esgrupo, 0) AS esgrupo,
                        NVL(dc.idgrupo, 0) AS idgrupo,
                        NVL(dc.idorigen, 0) AS idorigen,
                        NVL(dc.reenviado, 0) AS reenviado,
                        NVL(dc.disponible, 1) AS disponible,
                        dc.idusuariodispone,
                        dc.fechadispone,
                        NVL(dc.sustituido, 0) AS sustituido,
                        NVL(dc.essustituto, 0) AS essustituto,
                        NVL(dc.iddocdetalles, 0) AS iddocdetalles,
                        bu.numprecinto,
                        bu.codbolsa,
                        f.conbultos,
                        f.convalores,
                        f.descripcion
          FROM pd_documentocabecera dc
          LEFT JOIN pd_bulto bu ON dc.iddocumento = bu.iddocumento
          LEFT JOIN pd_banco bd ON dc.idbancodeposito = bd.idbanco
          LEFT JOIN pd_cliente bdc ON bd.idbanco = bdc.idcliente
          LEFT JOIN pd_banco b ON dc.idbanco = b.idbanco
          LEFT JOIN pd_cliente bc ON b.idbanco = bc.idcliente
          LEFT JOIN pd_cliente cd ON dc.idclientedestino = cd.idcliente
          LEFT JOIN pd_cliente co ON dc.idclienteorigen = co.idcliente
          LEFT JOIN pd_centroproceso cpd ON dc.idcentroprocesodestino =
                                            cpd.idcentroproceso
          LEFT JOIN pd_centroproceso cpo ON dc.idcentroprocesoorigen =
                                            cpo.idcentroproceso
         INNER JOIN pd_formulario f ON dc.idformulario = f.idformulario
          LEFT JOIN pd_formulariousuario fu ON f.idformulario =
                                               fu.idformulario
                                           AND (fu.idusuario IS NULL OR
                                               fu.idusuario = :idusuario OR
                                               :idusuario = 0)) total
 WHERE ((:distinguirporvistadestinatario = 0 AND
       ((total.idcentroprocesoorigen = :idcentroproceso AND
       INSTR(:idsestadoscomprobanteemitido,
                 '|' || total.idestadocomprobante || '|') > 0) OR
       (NVL(total.idcentroprocesodestino, total.idcentroprocesoorigen) =
       :idcentroproceso AND
       INSTR(:idsestadoscomprobanterecibido,
                 '|' || total.idestadocomprobante || '|') > 0))) OR
       (:distinguirporvistadestinatario = 1 AND
       ((:vistadestinatario = 0 AND
       total.idcentroprocesoorigen = :idcentroproceso AND
       INSTR(:idsestadoscomprobanteemitido,
                 '|' || total.idestadocomprobante || '|') > 0) OR
       (:vistadestinatario = 1 AND
       NVL(total.idcentroprocesodestino, total.idcentroprocesoorigen) =
       :idcentroproceso AND
       INSTR(:idsestadoscomprobanterecibido,
                 '|' || total.idestadocomprobante || '|') > 0))))
   AND esgrupo = 0
   AND (INSTR(:idsformulariosrestriccion, '|' || total.idformulario || '|') > 0 OR
       LENGTH(:idsformulariosrestriccion) IS NULL)
   AND (:distinguirporreenvio = 0 OR
       (:distinguirporreenvio = 1 AND total.reenviado = :reenviado))
   AND (:distinguirpordisponibilidad = 0 OR (:distinguirpordisponibilidad = 1 AND
       total.disponible = :disponible))
   AND (:distinguirporbultos = 0 OR
       (:distinguirporbultos = 1 AND total.conbultos = :conbultos))
   AND (:distinguirporvalores = 0 OR
       (:distinguirporvalores = 1 AND total.convalores = :convalores))
   AND ((:contomados = 0 AND
       (NOT (SELECT COUNT(1)
                 FROM pd_documentocabecera idc
                WHERE idc.idorigen = total.iddocumento) > 0)) OR
       (:contomados = 1))
   AND (:distinguirporsustitucion = 0 OR
       (:distinguirporsustitucion = 1 AND total.sustituido = :sustituido))
   AND ((instr(:ListaCodigos, '|' || rtrim(total.NumPrecinto) || '|') > 0) OR
       (total.idprimordial in
       (select c.iddocumento
            From -- Recupera a quantidade de Precintos encontrados no documento
                 (select d.iddocumento,
                         count(b.numprecinto) QtdPrecintoEcontrados
                    from pd_bulto b
                   inner join pd_documentocabecera d on b.iddocumento =
                                                        d.iddocumento
                   where instr(:ListaCodigos,
                               '|' || rtrim(b.NumPrecinto) || '|') > 0
                   group by d.iddocumento) c)))
 ORDER BY total.numexterno, total.numcomprobante
 