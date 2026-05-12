# Kanban Board Backend API

RESTful API для управления проектами Kanban с поддержкой команд, проектов, задач и совместной работы.

## Содержание

- [Технологии](#технологии)
- [Архитектура](#архитектура)
- [Структура проекта](#структура-проекта)
- [Установка и запуск](#установка-и-запуск)
- [Настройка](#настройка)
- [API Endpoints](#api-endpoints)
  - [Аутентификация](#аутентификация-apiatauth)
  - [Пользователи](#пользователи-apiuser)
  - [Команды](#команды-apiteams)
  - [Проекты](#проекты-apiprojects)
  - [Колонки](#колонки-apicolumns)
  - [Задачи](#задачи-apiissues)
  - [Комментарии](#комментарии-apicommentaries)
  - [Вложения](#вложения-apiattachments)
  - [Приглашения](#приглашения-apiinvites)
  - [Роли](#роли-apiroles)
- [Модели данных](#модели-данных)
- [Исключения](#исключения)
- [Аутентификация](#аутентификация-1)
- [Миграции БД](#миграции-бд)
- [Docker](#docker)
- [Разработка](#разработка)

---

## Технологии

| Компонент | Технология | Версия |
|-----------|------------|--------|
| Framework | ASP.NET Core | 10.0 |
| ORM | Entity Framework Core | 10.0.3 |
| Database | PostgreSQL (Npgsql) | 10.0.0 |
| CQRS/Mediator | MediatR | 14.1.0 |
| Authentication | JWT Bearer | 10.0.5 |
| API Documentation | OpenAPI/Swagger | Встроен |

---

## Архитектура

Проект построен на принципах **Clean Architecture** с разделением на 4 слоя:

```
┌─────────────────────────────────────────────────────────────┐
│                     Backend.Server                           │
│              (Controllers, Middleware, DI)                   │
├─────────────────────────────────────────────────────────────┤
│                   Backend.Application                        │
│        (Commands, Queries, Handlers, Mappers)                │
├─────────────────────────────────────────────────────────────┤
│                     Backend.Domain                            │
│         (Entities, Enums, Interfaces, Exceptions)             │
├─────────────────────────────────────────────────────────────┤
│                 Backend.Infrastructure                       │
│              (JwtService, FileStorageService)                 │
├─────────────────────────────────────────────────────────────┤
│                   Backend.Database                           │
│            (EF Core, Repositories, Migrations)               │
└─────────────────────────────────────────────────────────────┘
```

### Паттерны

- **CQRS (Command Query Responsibility Segregation)** — разделение команд (изменение данных) и запросов (чтение данных) через MediatR
- **Repository Pattern** — абстракция доступа к данным
- **Fluent API** — конфигурация Entity Framework через Fluent API

---

## Структура проекта

```
Backend/
├── Backend.Application/           # CQRS слой
│   ├── Abstractions/             # Базовые интерфейсы
│   │   ├── ICommand.cs
│   │   └── IQuery.cs
│   ├── Commands/                # Команды (запись)
│   │   ├── Command/             # Определения команд
│   │   ├── CommandHandler/      # Обработчики команд
│   │   └── Outcome/             # Результаты выполнения
│   ├── Queries/                 # Запросы (чтение)
│   │   ├── Query/               # Определения запросов
│   │   ├── QueryHandler/        # Обработчики запросов
│   │   └── Response/            # Ответы запросов
│   └── Map/                      # Мапперы DTO
│
├── Backend.Domain/              # Доменный слой
│   ├── Entity/                  # Сущности
│   │   ├── User.cs
│   │   ├── UserProfile.cs
│   │   ├── Team.cs
│   │   ├── Role.cs
│   │   ├── TeamUserProfile.cs
│   │   ├── Project.cs
│   │   ├── Column.cs
│   │   ├── ColumnRelation.cs
│   │   ├── Issue.cs
│   │   ├── Commentary.cs
│   │   ├── Attachment.cs
│   │   └── Invite.cs
│   ├── Enum/                    # Перечисления
│   │   ├── IssuePriority.cs
│   │   └── IssueType.cs
│   ├── Exceptions/              # Кастомные исключения
│   ├── Repository/               # Интерфейсы репозиториев
│   ├── Service/                  # Интерфейсы сервисов
│   └── Settings/                 # Настройки (JwtSettings)
│
├── Backend.Infrastructure/       # Инфраструктурный слой
│   └── Service/
│       ├── JwtService.cs
│       ├── PasswordHasher.cs
│       ├── FileStorageService.cs
│       └── TokenService.cs
│
├── Backend.Database/             # Слой доступа к данным
│   ├── Configuration/            # Fluent API конфигурации
│   ├── Migrations/               # EF Core миграции
│   ├── Repository/               # Реализации репозиториев
│   └── ApplicationDbContext.cs   # DbContext
│
├── Backend.Server/              # Точка входа
│   ├── Controller/               # API контроллеры
│   ├── Extension/               # Методы расширения для DI
│   ├── Middleware/              # Промежуточное ПО
│   └── Program.cs               # Точка входа
│
├── Storage/                      # Файловое хранилище
│   ├── attachments/
│   └── avatars/
│
└── .github/                      # GitHub Actions CI/CD
```

---

## Установка и запуск

### Предварительные требования

- .NET SDK 10.0+
- PostgreSQL 12+
- Docker (опционально)

### Шаги запуска

1. **Клонирование и установка зависимостей**

```bash
dotnet restore
```

2. **Настройка переменных окружения**

```powershell
$env:CONNECTION_STRING = "Host=localhost;Port=5432;Database=KanbanDb;Username=postgres;Password=your_password"
```

3. **Запуск миграций**

```bash
dotnet ef database update --project Backend.Database --startup-project Backend.Server
```

4. **Запуск сервера**

```bash
dotnet run --project Backend.Server/Backend.Server.csproj
```

Сервер запустится по адресу: `http://localhost:5062`

### Swagger UI

Документация API доступна по адресу: `http://localhost:5062/swagger`

---

## Настройка

### Конфигурация приложения (`appsettings.json`)

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "your-super-secret-key-with-at-least-32-characters-long",
    "Issuer": "http://localhost:5062",
    "Audience": "http://localhost:5173",
    "ExpiryDays": 1
  }
}
```

### Переменные окружения

| Переменная | Описание | По умолчанию |
|------------|----------|--------------|
| `CONNECTION_STRING` | Строка подключения к PostgreSQL | `Host=localhost;Port=5432;Database=BackendDevDb;Username=postgres;Password=ba11inin` |
| `ASPNETCORE_ENVIRONMENT` | Режим окружения | `Development` |

### CORS

API настроен для работы с React-фронтендом на:
- `http://localhost:5173` (dev)
- `http://localhost:4173` (preview)

---

## API Endpoints

### Аутентификация (`/api/auth`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| POST | `/api/auth/register` | Регистрация нового пользователя |
| POST | `/api/auth/login` | Вход пользователя |
| POST | `/api/auth/verifyToken` | Проверка JWT токена |

### Пользователи (`/api/user`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/user/{userId}` | Получение профиля пользователя |
| PUT | `/api/user/avatar` | Обновление аватара |

### Команды (`/api/teams`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/teams` | Получение всех команд пользователя |
| GET | `/api/teams/{teamId}` | Получение деталей команды |
| POST | `/api/teams` | Создание новой команды |
| PUT | `/api/teams` | Обновление названия команды |
| DELETE | `/api/teams/{teamId}/{userProfileId}` | Удаление пользователя из команды |

### Проекты (`/api/projects`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/projects/{projectId}` | Получение деталей проекта |
| POST | `/api/projects` | Создание нового проекта |
| PUT | `/api/projects` | Обновление проекта |

### Колонки (`/api/columns`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| POST | `/api/columns` | Создание колонки |
| PUT | `/api/columns` | Обновление названия колонки |
| DELETE | `/api/columns/{id}` | Удаление колонки |
| PUT | `/api/columns/updatePosition` | Обновление позиции колонки |
| PUT | `/api/columns/updateRelation` | Обновление связей между колонками |

### Задачи (`/api/issues`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/issues/{issuePublicId}` | Получение деталей задачи |
| POST | `/api/issues` | Создание задачи |
| PUT | `/api/issues` | Обновление задачи |
| POST | `/api/issues/moveIssue` | Перемещение задачи в колонку |
| POST | `/api/issues/commentary` | Добавление комментария с вложениями |

### Комментарии (`/api/commentaries`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| PUT | `/api/commentaries/updateContent` | Обновление содержимого комментария |
| DELETE | `/api/commentaries/{id}` | Удаление комментария |

### Вложения (`/api/attachments`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| GET | `/api/attachments/{attachmentId}` | Получение содержимого вложения |

### Приглашения (`/api/invites`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| POST | `/api/invites` | Создание/получение приглашения |
| POST | `/api/invites/inviteUser` | Добавление пользователя в команду по приглашению |

### Роли (`/api/roles`)

| Метод | Endpoint | Описание |
|-------|----------|----------|
| POST | `/api/roles` | Создание роли |
| PUT | `/api/roles` | Обновление роли |
| PUT | `/api/roles/updateUserRole` | Обновление роли пользователя |
| DELETE | `/api/roles/{id}` | Удаление роли |

---

## Модели данных

### User
```csharp
- Id: Guid (PK)
- Email: string (уникальный)
- PasswordHash: string
- UserProfileId: Guid (FK, 1:1)
- UserProfile: UserProfile
```

### UserProfile
```csharp
- Id: Guid (PK)
- FirstName: string
- SecondName: string
- Avatar: string (путь к файлу)
- CreatedAt: DateTime
```

### Team
```csharp
- Id: Guid (PK)
- Name: string
- CreatedAt: DateTime
- TeamUserProfiles: ICollection<TeamUserProfile>
- Projects: ICollection<Project>
- Roles: ICollection<Role>
```

### Role
```csharp
- Id: Guid (PK)
- Name: string
- TeamId: Guid (FK)
- Team: Team
- TeamUserProfiles: ICollection<TeamUserProfile>
```

### TeamUserProfile (Join Table)
```csharp
- Id: Guid (PK)
- UserProfileId: Guid (FK)
- TeamId: Guid (FK)
- RoleId: Guid (FK)
- UserProfile: UserProfile
- Team: Team
- Role: Role
```

### Project
```csharp
- Id: Guid (PK)
- PublicId: int (автоинкремент)
- Name: string
- Description: string
- CreatedAt: DateTime
- CreatedByUserProfileId: Guid (FK)
- TeamId: Guid (FK)
- Columns: ICollection<Column>
```

### Column
```csharp
- Id: Guid (PK)
- Name: string
- Position: int
- ProjectId: Guid (FK)
- Issues: ICollection<Issue>
- ColumnRelations: ICollection<ColumnRelation>
```

### ColumnRelation
```csharp
- Id: Guid (PK)
- ColumnId: Guid (FK)
- PrevColumnId: Guid? (FK, nullable)
- NextColumnId: Guid? (FK, nullable)
- Column: Column
```

### Issue
```csharp
- Id: Guid (PK)
- PublicId: int (уникальный в пределах проекта)
- Name: string
- Description: string
- Priority: IssuePriority (enum)
- Type: IssueType (enum)
- Position: int
- CreatedAt: DateTime
- UpdatedAt: DateTime
- DueDate: DateTime?
- ColumnId: Guid (FK)
- ProjectId: Guid (FK)
- Commentaries: ICollection<Commentary>
- Attachments: ICollection<Attachment>
```

### IssuePriority (Enum)
| Значение | Описание |
|----------|---------|
| 0 | Minimal |
| 1 | Low |
| 2 | Medium |
| 3 | High |
| 4 | Critical |

### IssueType (Enum)
| Значение | Описание |
|----------|---------|
| 0 | Bug |
| 1 | Story |
| 2 | Task |
| 3 | Investigation |

### Commentary
```csharp
- Id: Guid (PK)
- Content: string
- CreatedAt: DateTime
- UpdatedAt: DateTime
- IssueId: Guid (FK)
- CreatedByUserProfileId: Guid (FK)
- Attachments: ICollection<Attachment>
```

### Attachment
```csharp
- Id: Guid (PK)
- FileName: string
- FilePath: string
- FileSize: long
- ContentType: string
- CreatedAt: DateTime
- IssueId: Guid? (FK, nullable)
- CommentaryId: Guid? (FK, nullable)
```

### Invite
```csharp
- Id: Guid (PK)
- Token: string (уникальный, 32 символа)
- TeamId: Guid (FK)
- ExpiresAt: DateTime
- CreatedAt: DateTime
```

---

## Исключения

### UserInputException (HTTP 400)
Для некорректных данных от пользователя.

### ForbiddenException (HTTP 403)
Когда пользователь не имеет доступа к ресурсу.

### ConflictException (HTTP 409)
При конфликте ресурсов (например, дублирование).

### NotFoundException (HTTP 404)
Когда запрашиваемый ресурс не найден.

---

## Аутентификация

### JWT Token

Токен содержит:
- `UserId`: ID пользователя
- `Email`: Email пользователя
- `FirstName`: Имя
- `SecondName`: Фамилия

### Использование токена

Все запросы (кроме `/api/auth/*`) требуют заголовки:

```
Authorization: Bearer <jwt_token>
UserProfileId: <current_user_profile_id>
ProjectId: <current_project_context_id> (опционально)
```

### Пароли

Пароли хешируются с помощью SHA256.

---

## Миграции БД

### Создание миграции

```bash
dotnet ef migrations add <MigrationName> --project Backend.Database --startup-project Backend.Server
```

### Применение миграций

```bash
dotnet ef database update --project Backend.Database --startup-project Backend.Server
```

### Удаление последней миграции

```bash
dotnet ef migrations remove --project Backend.Database --startup-project Backend.Server
```

### Автоматические миграции

При запуске приложения в режиме `Development` миграции применяются автоматически.

---

## Docker

### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY out/Backend.Server/ /app/
EXPOSE 8081
EXPOSE 8080
ENTRYPOINT ["dotnet", "Backend.Server.dll"]
```

### Сборка и запуск

```bash
docker build -t kanban-backend .
docker run -p 8081:8081 kanban-backend
```

---

## Разработка

### Порты

| Сервис | URL |
|--------|-----|
| Backend API | http://localhost:5062 |
| Frontend (React) | http://localhost:5173 |
| Swagger UI | http://localhost:5062/swagger |

### Сборка

```bash
# Сборка всего проекта
dotnet build

# Сборка конкретного проекта
dotnet build Backend.Server/Backend.Server.csproj
```

### Запуск тестов

```bash
dotnet test
```

---

## TODO

- [ ] Unit тесты
- [ ] Integration тесты
- [ ] WebSocket для real-time обновлений
- [ ] Email уведомления
- [ ] Документация на английском языке
