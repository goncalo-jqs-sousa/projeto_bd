-- 1º -> Quantos jogadores de 1 equipa específica há num torneio
-- Substituir 'nome_da_equipa' pelo nome da equipa desejada e 'nome_do_torneio' pelo nome do torneio desejado.
SELECT COUNT(*) AS total_jogadores
FROM FGC.JogaEm je
JOIN FGC.ProPlayer pp ON je.nif_pp = pp.nif
JOIN FGC.Equipa e ON pp.nif_equipa = e.nif
JOIN FGC.Torneio t ON je.nome_torneio = t.nome
WHERE e.nome = 'nome_da_equipa'
  AND t.nome = 'nome_do_torneio';


-- 2º -> Lucro de um Torneio (juntar anuncios e patrocinios e remover salario e prize_pool)
-- Substituir 'nome_do_torneio' pelo nome do torneio desejado.
SELECT
  (t.valor_anuncios + COALESCE(SUM(tp.valor_patrocinado), 0)) AS lucro_torneio
FROM FGC.Torneio t
LEFT JOIN FGC.TemTP tp ON t.nome = tp.nome_torneio
WHERE t.nome = 'nome_do_torneio'
GROUP BY t.nome, t.valor_anuncios;

-- 3º -> Ver Desenvolvedores de jogos jogados em torneios por 1 jogador especifico
-- Substitua 'nome_do_jogador' pelo nome do jogador específico.
SELECT DISTINCT ed.nome
FROM FGC.Joga j
JOIN FGC.ProPlayer pp ON j.nif_pp = pp.nif
JOIN FGC.EquipaDesenvolvedores ed ON pp.nif_equipa = ed.nif
WHERE pp.nome = 'nome_do_jogador';


-- 4º -> Lista de todos os patrocinadores de um torneio/equipa/pro_player
-- Substitua 'nome_do_torneio', 'nif_da_equipa' e 'nif_do_pro_player' pelo respetivo valor.
SELECT p.nome
FROM FGC.TemTP tp
JOIN FGC.Patrocinador p ON tp.nif_patrocinador = p.nif
WHERE tp.nome_torneio = 'nome_do_torneio';


SELECT p.nome
FROM FGC.TemEP te
JOIN FGC.Patrocinador p ON te.nif_patrocinador = p.nif
WHERE te.nif_equipa = 'nif_da_equipa';

SELECT p.nome
FROM FGC.TemPP tp
JOIN FGC.Patrocinador p ON tp.nif_patrocinador = p.nif
WHERE tp.nif_pp = 'nif_do_pro_player';


-- 5º -> Quantidade de pro_players a participar num torneio
--Substitua 'nome_do_torneio' pelo nome do torneio desejado.
SELECT COUNT(*) AS total_pro_players
FROM FGC.JogaEm je
WHERE je.nome_torneio = 'nome_do_torneio';


