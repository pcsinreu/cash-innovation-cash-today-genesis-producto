SELECT M.IdMotivo as Id, M.Descripcion, M.Accion
  FROM PD_Motivo M
 INNER JOIN PD_MotivoCentroProceso MCP ON M.IdMotivo = MCP.IdMotivo
                                      and MCP.IdCentroProceso =
                                          :IdCentroProceso