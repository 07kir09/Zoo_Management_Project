```markdown
# Zoo Management Project

Автоматизированная система управления зоопарком, построенная по принципам Clean Architecture и Domain‑Driven Design.

---

## Описание

Приложение покрывает основные бизнес‑процессы:

- Управление **животными** (CRUD)
- Управление **вольерами** (CRUD)
- Перемещение животных между вольерами
- Организация расписания **кормлений** (CRUD, пометка выполненного кормления)
- Получение **статистики** (количество животных, общее число вольеров, свободные места)

Все данные хранятся в In‑Memory репозиториях, что упрощает запуск и разработку.

---

## Архитектура

Проект разбит на четыре независимых слоя:

```
src/
├── Zoo.Domain           — ядрo, содержит сущности, Value Objects, enum’ы, доменные события
├── Zoo.Application      — приложение, содержит интерфейсы репозиториев и сервисы бизнес‑логики
├── Zoo.Infrastructure   — инфраструктура, In‑Memory реализации репозиториев и диспетчер доменных событий
└── Zoo.WebApi           — presentation, ASP.NET Core Web API с контроллерами и DTO
```

- **Domain**: не зависит ни от кого, содержит только чистую бизнес‑модель.  
- **Application**: зависит только от Domain, инкапсулирует сценарии бизнес‑логики.  
- **Infrastructure**: реализует интерфейсы из Application, не содержит бизнес‑правил.  
- **WebApi**: зависит только от Application.Interfaces и Domain‑DTO, отвечает за HTTP‑уровень.

---

## Принципы

- **Clean Architecture**: все зависимости идут внутрь (в Domain ни на кого нет ссылок).  
- **DDD**:  
  - Использование `enum` и хозяева сущностей (`Animal`, `Enclosure`, `FeedingSchedule`)  
  - Бизнес‑правила (`HasSpace()`, `Feed()`, `MarkDone()` и т. д.) инкапсулированы внутри домена.  
- **Domain Events**: при перемещении животного (`AnimalMovedEvent`) и наступлении времени кормления (`FeedingTimeEvent`) через `IDomainEventDispatcher`.

---

## Быстрый старт

### Запуск из терминала

```bash
cd src/Zoo.WebApi
dotnet restore
dotnet build
dotnet run
```

По умолчанию приложение слушает:

- **HTTP**  → `http://localhost:5000`  
- **HTTPS** → `https://localhost:5001`

## Swagger UI

После старта откройте в браузере:

```
https://localhost:5001/

Там вы увидите все контроллеры и сможете тестировать API.

---

## Основные эндпоинты

| Метод      | URL                                 | Описание                                  |
|------------|-------------------------------------|-------------------------------------------|
| GET        | `/api/animals`                      | Список всех животных                      |
| POST       | `/api/animals`                      | Создать новое животное                    |
| DELETE     | `/api/animals/{id}`                 | Удалить животное                          |
| POST       | `/api/animals/{id}/move/{encId}`    | Переместить животное в другой вольер      |
| GET        | `/api/enclosures`                   | Список всех вольеров                      |
| POST       | `/api/enclosures`                   | Создать новый вольер                      |
| DELETE     | `/api/enclosures/{id}`              | Удалить вольер                            |
| GET        | `/api/feedingschedules`             | Список расписаний кормлений               |
| POST       | `/api/feedingschedules`             | Добавить кормление                        |
| POST       | `/api/feedingschedules/{id}/done`   | Отметить кормление выполненным           |
| DELETE     | `/api/feedingschedules/{id}`        | Удалить запись о кормлении                |
| GET        | `/api/statistics`                   | Текущая статистика зоопарка (ZooStats)    |

---

## Структура кода

```text
src/
├─ Zoo.Domain/
│   ├─ Entities/
│   │   ├ Animal.cs
│   │   ├ Enclosure.cs
│   │   └ FeedingSchedule.cs
│   ├─ Enums/
│   │   ├ Gender.cs
│   │   ├ FoodType.cs
│   │   ├ EnclosureType.cs
│   │   ├ HealthStatus.cs
│   │   └ FeedingStatus.cs
│   └─ Events/
│       ├ AnimalMovedEvent.cs
│       └ FeedingTimeEvent.cs
│
├─ Zoo.Application/
│   ├─ Interfaces/
│   │   ├ IAnimalRepository.cs
│   │   ├ IEnclosureRepository.cs
│   │   ├ IFeedingScheduleRepository.cs
│   │   └ IDomainEventDispatcher.cs
│   ├─ Services/
│   │   ├ AnimalTransferService.cs
│   │   ├ FeedingOrganizationService.cs
│   │   └ ZooStatisticsService.cs
│   └─ Models/
│       └ ZooStats.cs
│
├─ Zoo.Infrastructure/
│   ├─ Repositories/
│   │   ├ InMemoryAnimalRepository.cs
│   │   ├ InMemoryEnclosureRepository.cs
│   │   └ InMemoryFeedingScheduleRepository.cs
│   └─ Events/
│       └ DomainEventDispatcher.cs
│
└─ Zoo.WebApi/
    ├─ Controllers/
    │   ├ AnimalsController.cs
    │   ├ EnclosuresController.cs
    │   ├ FeedingSchedulesController.cs
    │   └ StatisticsController.cs
    ├─ Dtos/
    │   ├ AnimalDto.cs
    │   ├ EnclosureDto.cs
    │   ├ FeedingScheduleDto.cs
    │   ├ CreateAnimalRequest.cs
    │   ├ CreateEnclosureRequest.cs
    │   └ CreateFeedingRequest.cs
    └ Program.cs
```

---

## Внесение изменений и тестирование

- **Добавление Value Objects**: можно выделить отдельные struct/record для `AnimalName`, `BirthDate`, `EnclosureCapacity` и т. д.  
- **Переключение хранилища**: достаточно заменить регистрацию In‑Memory репозиториев на EF Core/другую реализацию.  
- **Интеграционные тесты**: рекомендуем подключить `Microsoft.AspNetCore.Mvc.Testing` и писать тесты через `WebApplicationFactory<Program>`.
