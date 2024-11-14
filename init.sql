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

-- Inserção de uma chave estrangeira para tags ativas em apartamentos
ALTER TABLE apartamentos
ADD CONSTRAINT fk_tags_ativas
FOREIGN KEY (tags_ativas)
REFERENCES tags(id) ON DELETE SET NULL;
