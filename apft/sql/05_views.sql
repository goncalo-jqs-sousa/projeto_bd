use p2g3

GO
CREATE VIEW vw_ProPlayerEquipa AS
SELECT PP.nif, PP.nome AS nome_jogador, PP.prize_money, PP.games_played, E.nome AS nome_equipa
FROM FGC.ProPlayer AS PP
INNER JOIN FGC.Equipa AS E ON PP.nif = E.nif;


GO
CREATE VIEW vw_ProPlayerCharsPlayed AS
SELECT PP.nif, PP.nome AS nome_jogador, PCP.chars_played
FROM FGC.ProPlayer AS PP
INNER JOIN FGC.PPCharsPlayed AS PCP ON PP.nif = PCP.nif_pp;


GO
CREATE VIEW vw_EquipaPatrocinador AS
SELECT E.nome AS nome_equipa, P.nome AS nome_patrocinador, TP.valor_patrocinado
FROM FGC.Equipa AS E
INNER JOIN FGC.TemEP AS TP ON E.nif = TP.nif_equipa
INNER JOIN FGC.Patrocinador AS P ON TP.nif_patrocinador = P.nif;


GO
CREATE VIEW vw_TorneioJogadores AS
SELECT T.nome AS nome_torneio, PP.nome AS nome_jogador
FROM FGC.Torneio AS T
INNER JOIN FGC.JogaEm AS JE ON T.nome = JE.nome_torneio
INNER JOIN FGC.ProPlayer AS PP ON JE.nif_pp = PP.nif;


GO
CREATE VIEW vw_TorneioEquipaDesenvolvedores AS
SELECT T.nome AS nome_torneio, ED.nome AS nome_equipa_desenvolvedores
FROM FGC.Torneio AS T
INNER JOIN FGC.FightingGame AS FG ON T.nome = FG.nome
INNER JOIN FGC.EquipaDesenvolvedores AS ED ON FG.nif_ed = ED.nif;