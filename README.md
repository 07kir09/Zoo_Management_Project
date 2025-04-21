```markdown
# 🦁 Zoo Management API

Автоматизированная система управления зоопарком на базе .NET 8 и ASP.NET Core, построенная по принципам **Clean Architecture** и **Domain‑Driven Design**.

---

## 📋 Оглавление

- [🎯 Цель проекта](#-цель-проекта)  
- [⚙️ Функционал](#️-функционал)  
- [🗂 Архитектура](#-архитектура)  
- [🚀 Быстрый старт](#-быстрый-старт)  
- [🛠 Технические детали](#-технические-детали)  
- [📖 API](#-api)  
- [🔄 Изменяемое хранилище](#-изменяемое-хранилище)  
- [✅ Лицензия](#-лицензия)  

---

## 🎯 Цель проекта

Создать лёгкий и расширяемый Web API для управления зоопарком:

- CRUD для **животных**, **вольеров** и **расписаний кормлений**  
- Перемещение животных между вольерами  
- Пометка выполнения кормлений  
- Получение ключевой статистики (количество животных, свободные места в вольерах)

---

## ⚙️ Функционал

1. **Животные**  
   - Добавить / удалить  
   - Просмотр списка и деталей  
   - Перемещение между вольерами  

2. **Вольеры**  
   - Добавить / удалить  
   - Просмотр списка и деталей  

3. **Кормления**  
   - Запланировать / удалить  
   - Просмотр расписания  
   - Отметить как выполненное  

4. **Статистика**  
   - Общее число животных  
   - Общее число вольеров  
   - Свободные места в вольерах  

---

## 🗂 Архитектура

```
src/
├── Zoo.Domain           — Доменная модель, энумы и события
├── Zoo.Application      — Интерфейсы репозиториев и сервисы бизнес‑логики
├── Zoo.Infrastructure   — Реализации репозиториев (In‑Memory), диспетчер событий
└── Zoo.WebApi           — ASP.NET Core Web API: контроллеры, DTO, Program.cs
```

- **Domain**: чистая бизнес‑логика  
- **Application**: сценарии приложения  
- **Infrastructure**: детали хранения и доставки событий  
- **WebApi**: точки входа HTTP

---

## 🚀 Быстрый старт

1. **Установите .NET 8 SDK**  
   https://dotnet.microsoft.com/download/dotnet/8.0

2. **Клонируйте репозиторий**  
   ```bash
   git clone https://github.com/your-org/zoo-management.git
   cd zoo-management/src/Zoo.WebApi
   ```

3. **Запустите приложение**  
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

4. **Откройте в браузере**  
   - Swagger UI:  
     ```
     https://localhost:5001/
     ```  
   - API JSON Spec:  
     ```
     https://localhost:5001/swagger/v1/swagger.json
     ```

---

## 🛠 Технические детали

- **Язык & Платформа:** C#, .NET 8, ASP.NET Core  
- **Архитектурный паттерн:** Clean Architecture  
- **Методология:** Domain‑Driven Design  
- **Хранилище:** In‑Memory (конкурентные словари)  
- **Документация API:** Swashbuckle / Swagger  
- **Тестирование:** можно добавить xUnit + `WebApplicationFactory<Program>`

---

## 📖 API Endpoints

<table>
  <thead>
    <tr>
      <th>Метод</th><th>URL</th><th>Описание</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>GET</td><td>/api/animals</td><td>Список всех животных</td>
    </tr>
    <tr>
      <td>POST</td><td>/api/animals</td><td>Создать животное</td>
    </tr>
    <tr>
      <td>DELETE</td><td>/api/animals/{id}</td><td>Удалить животное</td>
    </tr>
    <tr>
      <td>POST</td><td>/api/animals/{id}/move/{encId}</td><td>Переместить животное</td>
    </tr>
    <tr>
      <td>GET</td><td>/api/enclosures</td><td>Список всех вольеров</td>
    </tr>
    <tr>
      <td>POST</td><td>/api/enclosures</td><td>Создать вольер</td>
    </tr>
    <tr>
      <td>DELETE</td><td>/api/enclosures/{id}</td><td>Удалить вольер</td>
    </tr>
    <tr>
      <td>GET</td><td>/api/feedingschedules</td><td>Список всех кормлений</td>
    </tr>
    <tr>
      <td>POST</td><td>/api/feedingschedules</td><td>Запланировать кормление</td>
    </tr>
    <tr>
      <td>POST</td><td>/api/feedingschedules/{id}/done</td><td>Отметить кормление</td>
    </tr>
    <tr>
      <td>DELETE</td><td>/api/feedingschedules/{id}</td><td>Удалить кормление</td>
    </tr>
    <tr>
      <td>GET</td><td>/api/statistics</td><td>Текущая статистика (ZooStats)</td>
    </tr>
  </tbody>
</table>

---

## 🔄 Изменяемое хранилище

Чтобы заменить In‑Memory на EF Core или другое:

1. Создать новый проект `Zoo.Infrastructure.EFCore`  
2. Реализовать интерфейсы репозиториев через `DbContext`  
3. В `Program.cs` заменить:
   ```csharp
   builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
   ```
   на
   ```csharp
   builder.Services.AddScoped<IAnimalRepository, EfCoreAnimalRepository>();
   ```
