# Sistema de gerenciamento de tags de garagem.

Objetivo: Facilitar o controle e distribuição de tag de garagens para moradores e visitantes do condomínio.

Regra: Cada apartamento tem direito à duas tags de garagem. Duas somente em caso de moradores; Caso tenha só uma ativa ou nenhuma, a "reserva" pode ser concedida a um visitante por um período de 48 horas;

Ideia de banco de dados

id | id_apartamento | tags_ativas | tipo_tag (morador ou visita) | validade_tag (caso visitante deve contar 48 horas) | is_active

pode armazenar o banco em memória.

rotas:

get all tags
get tag by id
get tag by apto
update tag is_active (verdadeiro ou falso)
delete tag
create tag
update tag tipo_tag

duas torres tendo 10 apartamentos

apto 1-A até 10-B e o mesmo para o B

## Estrutura de Pastas e Arquivos

```plaintext
GarageTagManagement
├── Controllers
│   └── TagsController.cs           # Controller para definir os endpoints
├── Models
│   └── Tag.cs                      # Modelo de dados para a tag
├── Repositories
│   └── TagRepository.cs            # Repositório em memória para armazenar as tags
├── Services
│   └── TagService.cs               # Serviço para lógica de negócios das tags
├── Program.cs                      # Configuração inicial do projeto
├── Startup.cs                      # Configurações de middleware e serviços
└── GarageTagManagement.csproj      # Arquivo do projeto
```

## Descrição dos Arquivos

1. **`Models/Tag.cs`**: Modelo de dados para uma tag, com propriedades como `Id`, `IdApartamento`, `TagsAtivas`, `TipoTag`, `ValidadeTag`, `IsActive`.

2. **`Repositories/TagRepository.cs`**: Classe para armazenar e manipular os dados em memória. Implementa operações CRUD para as tags.

3. **`Services/TagService.cs`**: Contém a lógica de negócios, como as regras de distribuição para moradores e visitantes, e verificação da validade da tag.

4. **`Controllers/TagsController.cs`**: Define as rotas e endpoints para gerenciar as tags:

   - **`GET /api/tags`**: Obter todas as tags.
   - **`GET /api/tags/{id}`**: Obter uma tag específica pelo ID.
   - **`GET /api/tags/apto/{id_apartamento}`**: Obter as tags de um apartamento específico.
   - **`POST /api/tags`**: Criar uma nova tag.
   - **`PUT /api/tags/{id}`**: Atualizar o status `is_active` ou `tipo_tag` de uma tag.
   - **`DELETE /api/tags/{id}`**: Deletar uma tag.

Com essa estrutura inicial, você pode focar na implementação da lógica e das rotas no `TagsController` e no `TagService`, garantindo que as regras sejam aplicadas ao atualizar ou criar tags.
