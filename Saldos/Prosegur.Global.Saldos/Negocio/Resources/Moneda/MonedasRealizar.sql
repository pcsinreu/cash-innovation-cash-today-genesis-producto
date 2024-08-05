SELECT IdMoneda AS Id, Simbolo, Descripcion, Visible, IsoGenesis, IdGenesis, IsoSaldos
FROM PD_Moneda
WHERE (Visible = :Visible OR :Visible = 0)
ORDER BY IdMoneda