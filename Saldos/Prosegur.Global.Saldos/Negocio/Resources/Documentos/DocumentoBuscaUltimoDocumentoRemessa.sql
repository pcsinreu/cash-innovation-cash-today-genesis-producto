select d.iddocumento from pd_documentocabecera d
inner join pd_centroproceso cd on cd.idcentroproceso = d.idcentroprocesodestino
inner join pd_planta pd on pd.idplanta = cd.idplanta
where d.numexterno = :NUM_EXTERNO
 and pd.coddelegaciongenesis = :COD_DELEGACION
 {0}
order by d.iddocumento desc, d.fecha desc