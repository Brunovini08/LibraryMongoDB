# LibraryMongoDB

## Descrição do Projeto
O **LibraryMongoDB** é uma aplicação de console em C# que demonstra operações básicas de CRUD (Create, Read, Update, Delete) utilizando o banco de dados **MongoDB**. O projeto manipula informações sobre **autores** e **livros**, exercitando consultas simples entre collections relacionadas.

A aplicação permite cadastrar autores e livros, listar todos os registros exibindo o autor de cada livro, atualizar informações de autores e remover livros ou autores da base de dados.

---

## Tecnologias Utilizadas
- **C#** (.NET 8 ou superior)
- **MongoDB** (local ou Atlas)
- **MongoDB.Driver** (driver oficial para C#)
- Aplicativo de Console (Console Application)

---

## Estrutura do Banco de Dados

### Collection `Authors`
Cada documento contém:
- `Id` (ObjectId) – identificador único gerado automaticamente pelo MongoDB
- `Name` (string) – nome do autor
- `Country` (string) – país de origem do autor
- `Index` (int) - index de busca do autor

### Collection `Books`
Cada documento contém:
- `Id` (ObjectId) – identificador único
- `Title` (string) – título do livro
- `AuthorId` (int) – referência ao autor na collection `Authors`
- `Year` (int) – ano de publicação
- `Index` (int) - index de busca do livro

---

## Funcionalidades do Sistema

### Create
- Inserir novos autores na collection `Authors`.
- Inserir novos livros na collection `Books`, vinculando-os a um autor existente.

### Read
- Listar todos os autores cadastrados.
- Listar todos os livros cadastrados, exibindo o **nome do autor** correspondente (simulando um join entre collections).

### Update
- Atualizar informações de um autor (como nome ou país de origem).
- Atualizar informações de um livro (como título, ano ou a referência do autor).

### Delete
- Remover um livro específico da base de dados.
- Remover um autor, junto com seus livros (implementando remoção em cascata).

---

## Execução do Projeto
1. Certifique-se de ter o **MongoDB** instalado e rodando localmente, ou configure a string de conexão para uma instância MongoDB Atlas.
2. Clone ou baixe o projeto `LibraryMongoDB`.
3. Abra o projeto em um IDE compatível com C# (Visual Studio, Visual Studio Code).
4. Execute o projeto como **Console Application**.
5. Siga as instruções exibidas no console para realizar operações CRUD.

---

## Observações
- Todas as operações com o MongoDB são realizadas de forma **assíncrona**, garantindo melhor desempenho.
- A aplicação inclui tratamento básico de erros e mensagens claras para o usuário.
- A remoção de autores também remove seus livros associados para manter a integridade dos dados.

---

## Autor
- Desenvolvido como exercício de aprendizado em C# e MongoDB.
