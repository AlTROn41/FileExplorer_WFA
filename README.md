# File Explorer

Этот проект представляет собой простое графическое приложение для управления файловой системой на платформе Windows, разработанное с использованием языка программирования C#. Программа предоставляет пользователям возможность просматривать и навигировать по папкам и файлам на их компьютере с помощью удобного интерфейса.

## Функциональность
- **Просмотр дисков:** Приложение загружает и отображает доступные диски на компьютере.
- **Навигация по папкам:** Пользователи могут легко переходить между папками, используя дерево папок в интерфейсе.
- **Отображение содержимого:** При выборе папки отображаются ее содержимое, включая подпапки и файлы.
- **Поддержка навигации назад:** Пользователи могут перемещаться в предыдущие папки с помощью кнопки "Назад".
- **Обработка ошибок:** Приложение обрабатывает ошибки доступа, выводя соответствующие уведомления пользователю.
- **Открытие папок:** Папки можно открывать и просматривать их содержимое, дважды щелкнув по элементу в списке.

## Структура проекта
Проект организован в виде Windows Forms приложения и включает в себя следующие компоненты:
- **Форма (Form1):** Главный интерфейс приложения, содержащий элементы управления для отображения дерева папок и содержимого папок.
- **Обработчики событий:** Методы для обработки событий, таких как выбор узлов в дереве и двойной клик по элементам списка.
- **Библиотека:** Все методы и вспомогательные классы находятся в отдельной библиотеке **FileExplorerLibrary** для лучшей организации кода и повторного использования функциональности.

## Установка и запуск
1. Склонируйте репозиторий или загрузите проект на свой компьютер с помощью команды
```bash
git clone https://github.com/AlTROn41/FileExplorer_WFA.git
```
2. Откройте проект в Visual Studio.
3. Постройте решение и запустите приложение.

## Использование
- При запуске приложения пользователи увидят деревообразное представление дисков.
- Разворачивайте узлы, чтобы просматривать подпапки.
- Выберите папку, чтобы увидеть её содержимое в нижней части окна.
- Используйте кнопки "Назад" и "Вперёд" для навигации по истории папок.

## Вклад
Если вы хотите внести свой вклад в проект, не стесняйтесь открывать новые задачи или создавать запросы на изменение!

---
