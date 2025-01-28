use p2g3

go
--schema
if not exists (select * from sys.schemas where name='FGC') -- Fighting Game Community
begin
  exec('create schema FGC;')
end

go
--tipos
if not exists( select * from sys.types where name = 'str150')
begin
	create type str150 from varchar(150)
end

if not exists( select * from sys.types where name = 'nif')
begin
	create type nif from varchar(9)
end

if not exists( select * from sys.types where name = 'telefone')
begin
	create type telefone from VARCHAR(12)
end

if not exists( select * from sys.types where name = 'email')
begin
	create type email from VARCHAR(100)
end

if not exists( select * from sys.types where name = 'str50')
begin
	create type str50 from VARCHAR(50)
end


go
-- tabelas
CREATE TABLE FGC.Equipa (
    nif nif NOT NULL CHECK (LEN(nif) = 9),
    nome str150 NOT NULL,
    num_jogadores INT CHECK (num_jogadores >= 1),
    num_torneios_ganhos INT,
    PRIMARY KEY(nif),
);

CREATE TABLE FGC.ProPlayer (
    nif nif NOT NULL CHECK (LEN(nif) = 9),
    nome str150 NOT NULL,
    prize_money INT,
    games_played INT NOT NULL CHECK (games_played > 0),
    nif_equipa nif NOT NULL CHECK (LEN(nif_equipa) = 9),
    PRIMARY KEY(nif),
    FOREIGN KEY(nif_equipa) REFERENCES FGC.Equipa(nif),
);

CREATE TABLE FGC.PPCharsPlayed (
    nif_pp nif NOT NULL CHECK (LEN(nif_pp) = 9),
    chars_played str50,
    PRIMARY KEY(nif_pp, chars_played),
    FOREIGN KEY(nif_pp) REFERENCES FGC.ProPlayer(nif),
);

CREATE TABLE FGC.PPUsername (
    nif_pp nif NOT NULL CHECK (LEN(nif_pp) = 9),
    username str50 NOT NULL,
    PRIMARY KEY(nif_pp, username),
    FOREIGN KEY(nif_pp) REFERENCES FGC.ProPlayer(nif),
);

CREATE TABLE FGC.Patrocinador (
    nif nif NOT NULL CHECK (LEN(nif) = 9),
    telefone telefone NOT NULL,
    nome str150 NOT NULL,
    email email NOT NULL,
    PRIMARY KEY(nif),
);

CREATE TABLE FGC.Staff (
    nif nif NOT NULL CHECK (LEN(nif) = 9),
    nome str150 NOT NULL,
    num_staff INT UNIQUE NOT NULL,
    salario INT NOT NULL CHECK (salario >= 1),
    PRIMARY KEY(nif),
);

CREATE TABLE FGC.Limpeza (
    nif_staff nif NOT NULL CHECK (LEN(nif_staff) = 9),
    PRIMARY KEY(nif_staff),
    FOREIGN KEY(nif_staff) REFERENCES FGC.Staff(nif),
);

CREATE TABLE FGC.Comentador (
    nif_staff nif NOT NULL CHECK (LEN(nif_staff) = 9),
    PRIMARY KEY(nif_staff),
    FOREIGN KEY(nif_staff) REFERENCES FGC.Staff(nif),
);

CREATE TABLE FGC.Seguranca (
    nif_staff nif NOT NULL CHECK (LEN(nif_staff) = 9),
    PRIMARY KEY(nif_staff),
    FOREIGN KEY(nif_staff) REFERENCES FGC.Staff(nif),
);

CREATE TABLE FGC.Organizador (
    nif_staff nif NOT NULL CHECK (LEN(nif_staff) = 9),
    username str50 NOT NULL,
    PRIMARY KEY(nif_staff),
    FOREIGN KEY(nif_staff) REFERENCES FGC.Staff(nif),
);

CREATE TABLE FGC.EquipaDesenvolvedores (
    nif nif NOT NULL CHECK (LEN(nif) = 9),
    nome str150 NOT NULL,
    num_desenvolvedores INT NOT NULL CHECK (num_desenvolvedores >= 1),
    PRIMARY KEY(nif),
);

CREATE TABLE FGC.EDJogosDesenvolvidos (
    nif_ed nif NOT NULL CHECK (LEN(nif_ed) = 9),
    jogos_desenvolvidos str50,
    PRIMARY KEY(nif_ed, jogos_desenvolvidos),
    FOREIGN KEY(nif_ed) REFERENCES FGC.EquipaDesenvolvedores(nif),
);

CREATE TABLE FGC.FightingGame (
    nome str150 NOT NULL,
    num_vendas INT NOT NULL CHECK (num_vendas >= 1),
    active_players INT NOT NULL CHECK (active_players >= 1),
    player_peak INT NOT NULL CHECK (player_peak >= 1),
    nif_ed nif NOT NULL CHECK (LEN(nif_ed) = 9),
    PRIMARY KEY(nome),
    FOREIGN KEY(nif_ed) REFERENCES FGC.EquipaDesenvolvedores(nif),
);

CREATE TABLE FGC.Torneio (
    nome str150 NOT NULL,
    bilhetes_vendidos INT NOT NULL CHECK (bilhetes_vendidos >= 0),
    prize_pool INT NOT NULL CHECK (prize_pool >= 1),
    valor_anuncios INT NOT NULL CHECK (valor_anuncios >= 1),
    inscricoes INT NOT NULL CHECK (inscricoes >= 0),
    nif_staff nif NOT NULL CHECK (LEN(nif_staff) = 9),
    PRIMARY KEY(nome),
    FOREIGN KEY(nif_staff) REFERENCES FGC.Staff(nif),
);

CREATE TABLE FGC.TemEP (
    nif_equipa nif NOT NULL CHECK (LEN(nif_equipa) = 9),
    nif_patrocinador nif NOT NULL CHECK (LEN(nif_patrocinador) = 9),
    valor_patrocinado INT NOT NULL CHECK (valor_patrocinado >= 1),
    PRIMARY KEY(nif_equipa, nif_patrocinador),
    FOREIGN KEY(nif_equipa) REFERENCES FGC.Equipa(nif),
    FOREIGN KEY(nif_patrocinador) REFERENCES FGC.Patrocinador(nif),
);

CREATE TABLE FGC.TemPP (
    nif_pp nif NOT NULL CHECK (LEN(nif_pp) = 9),
    nif_patrocinador nif NOT NULL CHECK (LEN(nif_patrocinador) = 9),
    valor_patrocinado INT NOT NULL CHECK (valor_patrocinado >= 1),
    PRIMARY KEY(nif_pp, nif_patrocinador),
    FOREIGN KEY(nif_pp) REFERENCES FGC.ProPlayer(nif),
    FOREIGN KEY(nif_patrocinador) REFERENCES FGC.Patrocinador(nif),
);

CREATE TABLE FGC.TemTP (
    nome_torneio str150 NOT NULL,
    nif_patrocinador nif NOT NULL CHECK (LEN(nif_patrocinador) = 9),
    valor_patrocinado INT NOT NULL CHECK (valor_patrocinado >= 1),
    PRIMARY KEY(nome_torneio, nif_patrocinador),
    FOREIGN KEY(nome_torneio) REFERENCES FGC.Torneio(nome),
    FOREIGN KEY(nif_patrocinador) REFERENCES FGC.Patrocinador(nif),
);

CREATE TABLE FGC.JogaEm (
    nif_pp nif NOT NULL CHECK (LEN(nif_pp) = 9),
    nome_torneio str150 NOT NULL,
    PRIMARY KEY(nif_pp, nome_torneio),
    FOREIGN KEY(nif_pp) REFERENCES FGC.ProPlayer(nif),
    FOREIGN KEY(nome_torneio) REFERENCES FGC.Torneio(nome),
);

CREATE TABLE FGC.Em (
    nome_torneio str150 NOT NULL,
    nome_fg str150 NOT NULL,
    PRIMARY KEY(nome_torneio, nome_fg),
    FOREIGN KEY(nome_torneio) REFERENCES FGC.Torneio(nome),
    FOREIGN KEY(nome_fg) REFERENCES FGC.FightingGame(nome),
);

CREATE TABLE FGC.Joga (
    nif_pp nif NOT NULL CHECK (LEN(nif_pp) = 9),
    nome_fg str150 NOT NULL,
    PRIMARY KEY(nif_pp, nome_fg),
    FOREIGN KEY(nif_pp) REFERENCES FGC.ProPlayer(nif),
    FOREIGN KEY(nome_fg) REFERENCES FGC.FightingGame(nome),
);