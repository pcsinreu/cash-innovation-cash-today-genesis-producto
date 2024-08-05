select doc.iddocumento
       ,doc.numcomprobante
       ,case when est.codigo = 'A' then 'AC' else case when est.codigo = 'R' then 'RC' else 'RE' end end codigo 
from pd_documentocabecera doc
inner join pd_estadocomprobante est on doc.idestadocomprobante = est.idestadocomprobante
inner join pd_centroproceso cpo on doc.idcentroprocesoorigen = cpo.idcentroproceso
inner join pd_centroproceso cpd on doc.idcentroprocesodestino = cpd.idcentroproceso
     where est.codigo in ('A', 'R','I')
       and doc.BOL_EXPORTADO_SALIDAS = 0 
       and doc.idformulario = :ID_FORMULARIO
       and ( cpo.idps = :COD_SECTOR  
          or cpd.idps = :COD_SECTOR )
       and (doc.nel_intentos is null             
       or doc.nel_intentos <= :NEL_INTENTOS)
