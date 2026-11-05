# Лабораторная работа №2 — вариант 12 (AirLogger, MS SQL Server)

Вариант 12:
1. Поиск по всем полям — `Professor` → `sp_SearchProfessor`
2. Сортировка по всем полям — `UserException` → `sp_SortUserException`
3. Фильтрация по всем полям — `ReportLabWork` → `sp_FilterReportLabWork`

## Порядок запуска (SSMS)
1. `01_create_db.sql` — создаёт БД и таблицы с PK/FK/CHECK/UNIQUE (ссылочная целостность).
2. `02_seed_data.sql` — тестовые данные.
3. `03_variant12_queries.sql` — процедуры и примеры вызовов (скриншоты результатов).
4. `04_referential_integrity_tests.sql` — тесты ссылочной целостности (скриншоты ошибок).

## Допущение
ER-диаграмма отсутствует, поэтому поля сущностей выбраны по смыслу. Если у преподавателя есть ER_AirLogger.pdf, сверьте названия полей.

## Откат
Работа лежит в ветке `lab2-airlogger-v12`; ветки `test5lab` и `master` не изменены.
