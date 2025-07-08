# RentalApp

RentalApp — это современное веб-приложение для аренды недвижимости, разработанное на ASP.NET 8 с использованием принципов Clean Architecture. Проект предоставляет REST API для управления апартаментами, бронированиями и пользователями, а также покрыт модульными тестами с использованием NUnit.

===========================
📦 Функционал
===========================

- Управление апартаментами (создание, обновление, удаление, список, поиск доступных)
- Бронирование апартаментов (поиск, создание, обновление, удаление)
- Работа с пользователями (регистрация, поиск по email и id)
- Swagger-интерфейс для тестирования API
- Модульные тесты для всех слоёв архитектуры
- Чистая архитектура и разделение ответственности

===========================
🧰 Технологии
===========================

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MediatR
- NUnit
- Swagger
- GitHub Actions (CI)
- Clean Architecture

===========================
📁 Структура проекта
===========================

RentalApp/
├── Core/
│   ├── RentalApp.Application         - бизнес-логика
│   └── RentalApp.Domain              - сущности и интерфейсы
├── Infrastructure/
│   └── RentalApp.Infrastructure      - EF Core, контексты, репозитории
├── Presentation/
│   └── RentalApp.WebAPI              - ASP.NET Core контроллеры
├── NUnitTests/
│   ├── NUnitTests.Application
│   ├── NUnitTests.Domain
│   ├── NUnitTests.Infrastructure
│   └── NUnitTests.WebAPI
├── .github/workflows                 - GitHub Actions конфигурация
└── RentalApp.sln

===========================
🚀 Как запустить
===========================

1. Клонируй репозиторий:
   git clone https://github.com/Xuzker/RentalApp.git

2. Перейди в каталог:
   cd RentalApp

3. Восстанови зависимости:
   dotnet restore

4. Построй проект:
   dotnet build

5. Запусти API:
   dotnet run --project RentalApp.WebAPI

Swagger доступен по адресу:
https://localhost:7192/swagger/index.html

===========================
📘 API Контроллеры
===========================

== 🏘️ Apartments ==
GET     /api/Apartments
GET     /api/Apartments/{id}
GET     /api/Apartments/available
POST    /api/Apartments
PUT     /api/Apartments/{id}
DELETE  /api/Apartments/{id}

== 📅 Booking ==
GET     /api/Booking
GET     /api/Booking/{id}
GET     /api/Booking/users/{id}
POST    /api/Booking
PUT     /api/Booking/{id}
DELETE  /api/Booking/{id}

== 👤 User ==
GET     /api/User
GET     /api/User/{id}
GET     /api/User/by-email?email=
POST    /api/User
DELETE  /api/User/{id}

===========================
✅ Тестирование
===========================

Все слои покрыты модульными тестами на NUnit.
Для запуска всех тестов:

dotnet test

===========================
⚙️ CI/CD
===========================

В проекте используется GitHub Actions:
- Выполняется сборка
- Прогоняются тесты

===========================
🏗️ Архитектура
===========================

Проект построен по принципам Clean Architecture:

- Domain        — бизнес-сущности и интерфейсы
- Application   — use-cases, CQRS
- Infrastructure — реализация зависимостей
- WebAPI        — контроллеры и конфигурация
- NUnitTests    — модульные тесты для каждого слоя
