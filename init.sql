-- Criação da tabela de apartamentos
CREATE TABLE apartamentos (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    tags_ativas INTEGER[] -- Array de IDs de tags ativas
);

-- Criação da tabela de tags
CREATE TABLE tags (
    id SERIAL PRIMARY KEY,
    apartamento_id INTEGER, -- Chave estrangeira para a tabela apartamentos
    is_active BOOLEAN DEFAULT TRUE,
    valid_to DATE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (apartamento_id) REFERENCES apartamentos(id) ON DELETE CASCADE
);

-- Criação de uma tabela para os usuários
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    matricula INTEGER UNIQUE NOT NULL,
    nome VARCHAR(100) NOT NULL,
    senha VARCHAR(255) NOT NULL,
    cargo VARCHAR(10) NOT NULL CHECK (cargo IN ('admin', 'sindico', 'morador'))
);

-- Função para gerar matrícula automaticamente com base no prefixo de cargo
CREATE OR REPLACE FUNCTION gerar_matricula()
RETURNS TRIGGER AS $$
DECLARE
    prefixo INTEGER;
BEGIN
    IF NEW.cargo = 'admin' THEN
        prefixo := 1;
    ELSIF NEW.cargo = 'sindico' THEN
        prefixo := 2;
    ELSIF NEW.cargo = 'morador' THEN
        prefixo := 3;
    ELSE
        RAISE EXCEPTION 'Cargo inválido';
    END IF;

    -- Define a matrícula com o prefixo correspondente (e.g., 1001, 2001, etc.)
    NEW.matricula := prefixo * 1000 + (SELECT COALESCE(MAX(matricula % 1000), 0) + 1 FROM usuarios WHERE matricula / 1000 = prefixo);

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger para aplicar a função de geração de matrícula antes da inserção
CREATE TRIGGER trigger_gerar_matricula
BEFORE INSERT ON usuarios
FOR EACH ROW
WHEN (NEW.matricula IS NULL)
EXECUTE FUNCTION gerar_matricula();

-- Criando uma tabela de relacionamento entre apartamentos e tags
CREATE TABLE apartamento_tags (
    apartamento_id INTEGER NOT NULL,
    tag_id INTEGER NOT NULL,
    PRIMARY KEY (apartamento_id, tag_id),
    FOREIGN KEY (apartamento_id) REFERENCES apartamentos(id) ON DELETE CASCADE,
    FOREIGN KEY (tag_id) REFERENCES tags(id) ON DELETE CASCADE
);

INSERT INTO apartamentos (nome, tags_ativas) 
VALUES
('Apartamento 101', ARRAY[1, 2]),
('Apartamento 102', ARRAY[2, 3]),
('Apartamento 103', ARRAY[1, 3]),
('Apartamento 104', ARRAY[1]),
('Apartamento 105', ARRAY[2]);

INSERT INTO tags (apartamento_id, is_active, valid_to)
VALUES
(1, TRUE, '2024-12-31'),
(2, TRUE, '2024-12-31'),
(3, FALSE, '2024-12-31'),
(4, TRUE, '2025-01-31'),
(5, TRUE, '2024-11-30');

-- Inserção de usuários
INSERT INTO usuarios (cargo, nome, senha)
VALUES
('admin', 'Carlos Silva', 'senha123'),
('sindico', 'Maria Oliveira', 'senha123'),
('morador', 'João Souza', 'senha123'),
('morador', 'Ana Costa', 'senha123'),
('sindico', 'Paulo Mendes', 'senha123');

INSERT INTO apartamento_tags (apartamento_id, tag_id)
VALUES
(1, 1),
(1, 2),
(2, 2),
(2, 3),
(3, 1),
(3, 3),
(4, 1),
(5, 2);



