# GarageTagManagement API

## Objetivo

O objetivo do sistema **GarageTagManagement** é facilitar o controle e a distribuição de tags de garagem para moradores e visitantes de um condomínio, permitindo um gerenciamento mais eficiente do uso das vagas de estacionamento.

## Regras de Uso das Tags de Garagem

1. **Direitos de Uso:**

   - Cada apartamento possui o direito a duas tags de garagem.
   - O uso de duas tags simultâneas é permitido apenas para **moradores**.

2. **Reserva para Visitantes:**
   - Se apenas uma tag estiver ativa, ou se nenhuma das tags estiver em uso, a segunda tag (reserva) pode ser concedida a um visitante.
   - A validade da tag de visitante é limitada a um período de **48 horas**.

## Endpoints da API

### TagsController

O controlador `TagsController` oferece diversas operações para o gerenciamento de tags de garagem, incluindo operações CRUD e funcionalidades adicionais.

### 1. **Listar todas as tags**

- **Método:** `GET`
- **Rota:** `/api/tags`
- **Descrição:** Retorna todas as tags cadastradas. Retorna `NoContent` se não houver tags.

### 2. **Obter tag por ID**

- **Método:** `GET`
- **Rota:** `/api/tags/{id}`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Retorna uma tag específica pelo seu ID. Retorna `NotFound` se não encontrada.

### 3. **Obter tag por ID do apartamento**

- **Método:** `GET`
- **Rota:** `/api/tags/apto/{idApartamento}`
- **Parâmetro:** `idApartamento` (string) - ID do apartamento
- **Descrição:** Retorna uma tag específica associada a um apartamento. Retorna `NotFound` se a tag não for encontrada.

### 4. **Adicionar nova tag**

- **Método:** `POST`
- **Rota:** `/api/tags`
- **Descrição:** Adiciona uma nova tag ao sistema. Retorna `BadRequest` se a tag for inválida (nula) e `CreatedAtAction` com o novo ID após a criação.

### 5. **Atualizar tag por ID**

- **Método:** `PUT`
- **Rota:** `/api/tags/{id}`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Atualiza os dados de uma tag existente. Retorna `BadRequest` se a tag for nula e `NotFound` se a tag não existir.

### 6. **Excluir tag por ID**

- **Método:** `DELETE`
- **Rota:** `/api/tags/{id}`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Remove uma tag específica pelo seu ID. Retorna `NotFound` se a tag não for encontrada.

### 7. **Alternar status ativo da tag**

- **Método:** `PUT`
- **Rota:** `/api/tags/{id}/toggle`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Alterna o status ativo (`IsActive`) de uma tag. Retorna `NotFound` se a tag não for encontrada.

### 8. **Verificar validade da tag por ID**

- **Método:** `GET`
- **Rota:** `/api/tags/{id}/isValid`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Verifica se uma tag é válida. Retorna `NotFound` se a tag não for encontrada.

### 9. **Verificar validade da tag por ID do apartamento**

- **Método:** `GET`
- **Rota:** `/api/tags/apto/{idApartamento}/isValid`
- **Parâmetro:** `idApartamento` (string) - ID do apartamento
- **Descrição:** Verifica se a tag de um apartamento é válida. Retorna `NotFound` se a tag não for encontrada.

### 10. **Estender validade da tag**

- **Método:** `PUT`
- **Rota:** `/api/tags/{id}/toggleValid`
- **Parâmetro:** `id` (int) - ID da tag
- **Descrição:** Estende a validade (`ValidadeTag`) de uma tag em um ano, se ela tiver uma validade definida. Retorna `NotFound` se a tag não for encontrada.

## Estrutura do Projeto

- **Namespace:** `GarageTagManagement.Controllers`
- **Modelo de Dados:** `Tag`
- **Serviço:** `TagService`

## Observações

- Esta API utiliza o `Swagger` para documentação. Após iniciar o projeto, acesse `/swagger` para explorar os endpoints e testar as funcionalidades.
- A implementação de regras de negócio, como a validação e alternância de status, é gerenciada pelo `TagService`.

## Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** ASP.NET Core
- **Documentação:** Swagger
- - **Mensageria:** RabbitMQ

## Eventos

- **Evento:** `NewApartmentRegistered`
- **Descrição:** Disparado quando um novo apartamento é registrado no sistema.

- **Evento:** `NewTagCreated`
- **Descrição:** Disparado quando uma tag é ativada. Mensagens diferentes para síndicos e moradores.

- **Evento:** `NewTagVisitorCreated`
- **Descrição:** Disparado quando uma tag de visitante é ativada. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagDeactivated`
- **Descrição:** Disparado quando uma tag é desativada. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagVisitorDeactivated`
- **Descrição:** Disparado quando uma tag de visitante é desativada. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagValidityExtended`
- **Descrição:** Disparado quando a validade de uma tag é estendida. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagValidityExtendedVisitor`
- **Descrição:** Disparado quando a validade de uma tag de visitante é estendida. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagValidityExpired`
- **Descrição:** Disparado quando a validade de uma tag expira. Mensagens diferentes para síndicos e moradores.

- **Evento:** `TagValidityExpiredVisitor`
- **Descrição:** Disparado quando a validade de uma tag de visitante expira. Mensagens diferentes para síndicos e moradores.
