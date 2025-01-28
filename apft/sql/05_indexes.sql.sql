use p2g3

go

CREATE INDEX idx_Equipa_nif ON FGC.Equipa (nif);
CREATE INDEX idx_ProPlayer_nif ON FGC.ProPlayer (nif);
CREATE INDEX idx_Patrocinador_nif ON FGC.Patrocinador (nif);
CREATE INDEX idx_Staff_nif ON FGC.Staff (nif);
CREATE INDEX idx_EquipaDesenvolvedores_nif ON FGC.EquipaDesenvolvedores (nif);
CREATE INDEX idx_FightingGame_nome ON FGC.FightingGame (nome);
CREATE INDEX idx_Torneio_nome ON FGC.Torneio (nome);