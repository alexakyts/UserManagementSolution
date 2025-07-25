﻿# Проект: User Management API та Тести

## Частина 1: User Management API (.NET)

### Ендпоінти
* **POST /users/createuser**
    * Створює нового користувача.
    * Вимоги до тіла запиту (JSON):
        ```json
        {
          "fullName": "string",
          "email": "string (valid email format)",
          "dateOfBirth": "YYYY-MM-DDTHH:MM:SS (cannot be future date)"
        }
        ```
    * Повертає `201 Created` при успіху або `400 Bad Request` при помилках валідації.
* **GET /users/allusers**
    * Повертає список усіх користувачів.
    * Повертає `200 OK` зі списком користувачів.
* **DELETE /users/deleteuser/{id}**
    * Видаляє користувача за ID.
    * Повертає `204 No Content` при успішному видаленні або `404 Not Found`, якщо користувача не знайдено.
* **GET /users/{id}**
    * Повертає користувача за ID.
    * Повертає `200 OK` з даними користувача або `404 Not Found`, якщо користувача не знайдено. (Цей ендпоінт використовується для `CreatedAtAction` і прихований зі Swagger за допомогою `[ApiExplorerSettings(IgnoreApi = true)]`)

### Як запустити API
#### 1. Запуск через командний рядок:
1.  Перейдіть до кореневої папки проекту .NET (там, де знаходиться файл `.csproj`).
2.  Відкрийте командний рядок (термінал) або PowerShell.
3.  Виконайте команду:
    ```bash
    dotnet run
    ```
4.  API буде запущено за адресами, `https://localhost:7162` та `http://localhost:5134`. Перевірте вивід у консолі, щоб дізнатися точні порти.
5.  Документація Swagger буде доступна за адресою, в моєму випадку: `https://localhost:7162/swagger`.

#### 2. Запуск через VS:
1.  **Відкрийте проект у VS:** Запустіть VS, виберіть `File` > `Open` > `Project/Solution...`, і відкрийте ваш .NET API проект (файл `.sln` або `.csproj`).
2.  **Запустіть проект:** Натисніть на зелену кнопку "Run" на панелі інструментів VS, або виберіть `Debug` > `Start Debugging` (чи `Ctrl+F5`).
4.  **Перевірте запущені адреси:** VS автоматично запустить API, і зазвичай відкриє браузер зі Swagger. Точні URL-адреси також будуть вказані у вікні виводу VS.

## Частина 2: Автоматизовані тести (Java + Rest Assured)
### Опис
Цей Java-проект має автоматизовані тести для перевірки функціоналу User Management API, написані за допомогою Rest Assured та JUnit 5.

### Тестові сценарії:
* Створення дійсного користувача (позитивний сценарій).
* Створення користувача з недійсною електронною поштою (негативний сценарій).
* Створення користувача з датою народження у майбутньому (негативний сценарій).

### Вимоги до системи
* Java Development Kit (JDK) 11.
* Apache Maven.
* IntelliJ IDEA.

### Як запустити тести
1.  **Переконайтесь, що ваш .NET API запущено** Тести потребують працюючого API.
2.  **Відкрийте Java-проект** (папку `UserApiTests`) в IntelliJ IDEA або іншій IDE.
3.  **Переконайтесь, що `RestAssured.baseURI` та `RestAssured.port` у файлі `src/test/java/com/usermanagement.test/UserApiTests.java` відповідають адресі запущеного .NET API.**
    * Зараз встановлено:
        ```java
        RestAssured.baseURI = "https://localhost";
        RestAssured.port = 7162; // Або ваш HTTPS порт
        RestAssured.useRelaxedHTTPSValidation(); // Залишити для самопідписаних сертифікатів
        ```
4.  **Виконайте тести:**
    * **Через IDE:** Відкрийте файл `UserApiTests.java`, клацніть правою кнопкою миші на класі `UserApiTests` і виберіть `Run 'UserApiTests'`.
    * **Через Maven (з командного рядка):** Перейдіть до кореневої папки Java-проекту (там, де знаходиться `pom.xml`) і виконайте команду:
        ```bash
        mvn test
        ```

---
