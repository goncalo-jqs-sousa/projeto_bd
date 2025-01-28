use p2g3

GO
CREATE TRIGGER tr_insere_username ON FGC.ProPlayer AFTER INSERT AS
BEGIN
    INSERT INTO FGC.PPUsername (nif_pp, username)
    SELECT nif, 'NovoUsername'
    FROM inserted;
END;

-- Excluir Entidades Relacionadas = EER
GO
CREATE TRIGGER eer_equipa_desenvolvedores ON FGC.EquipaDesenvolvedores AFTER DELETE AS
BEGIN
    DELETE FROM FGC.FightingGame
    WHERE nif_ed IN (SELECT nif FROM deleted);
END;

GO
CREATE TRIGGER eer_equipa ON FGC.Equipa AFTER DELETE AS
BEGIN
    DELETE FROM FGC.ProPlayer
    WHERE nif_equipa IN (SELECT nif FROM deleted);
END;

GO
CREATE TRIGGER eer_staff ON FGC.Staff AFTER DELETE AS
BEGIN
    DELETE FROM FGC.Torneio
    WHERE nif_staff IN (SELECT nif FROM deleted);
END;
