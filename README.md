# BoxItUp

## Описание проекта
BoxItUp — это веб-приложение для управления коробками и их содержимым при переезде. Пользователи регистрируются в системе, получают токен доступа и могут создавать виртуальные коробки, добавлять в них вещи, а затем отправлять себе на почту список содержимого коробок. Также реализован функционал поиска вещей по названию.

Проект построен на основе микросервисной архитектуры, где каждый сервис отвечает за отдельную функциональность. Внутри каждого микросервиса используется чистая архитектура (Clean Architecture), что обеспечивает гибкость, масштабируемость и легкость поддержки.

## Основные функции

### Регистрация и авторизация пользователей
- Пользователь регистрируется в системе и получает токен доступа.
- Реализована система ролей: admin и user.
- Администратор может управлять списком пользователей через админскую панель.
- Поддержка refresh token для обновления токена доступа.

### Управление коробками
- Пользователь может создавать коробки и добавлять в них вещи.
- Коробки и их содержимое сохраняются в базе данных.
- Реализован поиск вещей по названию.

### Уведомления
- Пользователь может отправить себе на почту список содержимого коробок.
- Уведомления отправляются через микросервис NotificationService.

### Административная панель
- Администратор может просматривать и управлять пользователями.
- Администратор имеет доступ к статистике и отчетам.

## Общая архитектура MVP

### Основные компоненты
- **Frontend**: Веб-интерфейс для взаимодействия пользователя с системой. Реализованы страницы регистрации, авторизации, создания коробок, добавления вещей и отправки уведомлений.
  
- **Backend**: Состоит из трех микросервисов:
  - **AuthService**: отвечает за регистрацию, авторизацию и управление пользователями.
  - **BoxService**: отвечает за создание коробок, добавление вещей и поиск.
  - **NotificationService**: отвечает за отправку уведомлений на почту.

### Базы данных
- **PostgreSQL**: используется в AuthService для хранения данных пользователей и токенов.
- **SQL Server**: используется в BoxService для хранения данных о коробках и их содержимом.

### Брокер сообщений
- **RabbitMQ**: используется для обмена сообщениями между микросервисами (например, передача информации о коробках из BoxService в NotificationService).

### Контейнеризация
- Каждый микросервис и база данных запускаются в отдельных контейнерах Docker.

## Общая архитектура микросервисов

### 1. AuthService
- **Основная функция**: Регистрация, авторизация и управление пользователями.
- **Сущности**:
  - User: информация о пользователе (логин, пароль, роль).
  - RefreshToken: информация о токенах обновления.
- **Роли**: admin, user (реализованы как перечисления).
- **Технологии**: C#, PostgreSQL, Docker.
- **Особенности**: Поддержка refresh token (в процессе реализации).

### 2. BoxService
- **Основная функция**: Создание коробок, добавление вещей и поиск.
- **Сущности**:
  - Box: информация о коробке (название,  пользователь).
  - ItemBox: информация о вещи (название, описание, коробка).
- **Технологии**: C#, SQL Server, Docker.
- **Особенности**: Интеграция с RabbitMQ для передачи данных в NotificationService, реализация поиска вещей по названию (в процессе реализации).

### 3. NotificationService
- **Основная функция**: Отправка уведомлений на почту.
- **Технологии**: C#, Docker.
- **Особенности**: Получает данные о коробках и их содержимом через RabbitMQ, отправляет пользователю на почту список вещей в коробках.

## Текущий статус проекта

### Реализовано:
- **AuthService**:
  - Регистрация и авторизация пользователей.
  - Система ролей (admin, user).
  - Базовая работа с токенами (без обновления токена).
  - Передача токена в RabbitMQ

- **BoxService**:
  - CRUD операции над коробками и  вещей в коробке.
  - Базовая интеграция с RabbitMQ (без передачи данных в NotificationService).

### В процессе реализации:
- **AuthService**:
  - Добавление функционала refresh token.

- **BoxService**:
  - Реализация поиска вещей по названию.
  - Полная интеграция с RabbitMQ для передачи данных в NotificationService.

- **NotificationService**:
  - Реализация отправки уведомлений на почту.

## Планы по развитию
- Добавить возможность редактирования и удаления коробок и вещей.
- Реализовать статистику для администратора (количество пользователей, коробок, вещей).
- Добавить поддержку нескольких языков в интерфейсе.
- Реализовать мобильное приложение для удобства пользователей.
- Добавить интеграцию с облачными хранилищами для резервного копирования данных.
