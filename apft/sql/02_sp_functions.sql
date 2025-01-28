use p2g3

-- SPs
GO
CREATE PROCEDURE InserirEquipa
    @nif nif,
    @nome str150,
    @num_jogadores INT,
    @num_torneios_ganhos INT
AS
BEGIN
    INSERT INTO FGC.Equipa (nif, nome, num_jogadores, num_torneios_ganhos)
    VALUES (@nif, @nome, @num_jogadores, @num_torneios_ganhos);
END;



GO
CREATE PROCEDURE GetGameDevelopersByPlayer
    @playerNIF nif
AS
BEGIN
    SELECT DISTINCT ed.nome AS NomeEquipaDev
    FROM FGC.EquipaDesenvolvedores ed
    INNER JOIN FGC.EDJogosDesenvolvidos edj ON edj.nif_ed = ed.nif
    INNER JOIN FGC.Joga j ON j.nome_fg = edj.jogos_desenvolvidos
    WHERE j.nif_pp = @playerNIF
END


GO
CREATE PROCEDURE GetSponsors
    @entityType VARCHAR(20), -- 'tournament', 'team', or 'player'
    @entityID nif -- NIF do torneio, equipe ou jogador profissional
AS
BEGIN
    IF @entityType = 'tournament'
    BEGIN
        SELECT p.nome AS NomePatrocinador
        FROM FGC.Patrocinador p
        INNER JOIN FGC.TemTP tp ON tp.nif_patrocinador = p.nif
        WHERE tp.nome_torneio = @entityID
    END
    ELSE IF @entityType = 'team'
    BEGIN
        SELECT p.nome AS NomePatrocinador
        FROM FGC.Patrocinador p
        INNER JOIN FGC.TemEP ep ON ep.nif_patrocinador = p.nif
        WHERE ep.nif_equipa = @entityID
    END
    ELSE IF @entityType = 'player'
    BEGIN
        SELECT p.nome AS NomePatrocinador
        FROM FGC.Patrocinador p
        INNER JOIN FGC.TemPP pp ON pp.nif_patrocinador = p.nif
        WHERE pp.nif_pp = @entityID
    END
END


-- UDFs

GO
CREATE FUNCTION CalculateAveragePrizeMoney() RETURNS DECIMAL(10, 2) AS
BEGIN
    DECLARE @averagePrizeMoney DECIMAL(10, 2)

    SELECT @averagePrizeMoney = AVG(prize_money)
    FROM FGC.ProPlayer

    RETURN @averagePrizeMoney
END