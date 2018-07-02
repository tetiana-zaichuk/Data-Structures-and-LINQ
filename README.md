# DataStructures-and-LINQ
Homework (bsa 18). .NET Data Structures and LINQ.

Academy 2018: .NET Data Structures and LINQ. Maxim Goncharuk
Для успешного выполнения домашнего задания вам понадобится:
создать консольное приложение
используя HttpClient (или WebClient) получить набор открытых данных с помощью API запросов к https://5b128555d50a5c0014ef1204.mockapi.io/:endpoint

Где endpoint может иметь следующие значения:
users
posts
comments
todos
address
представить полученные данные в виде набора сущностей (вложенных объектов). Для десериализации используйте Newtonsoft.
-Users
---Posts
-------Comments
---Todos
Сущности должны быть связаны. Для того чтобы определить связи необходимо использовать id. Для создания иерархии выше использовать Join() из Linq. - реализовать набор методов для выборки данных из полученной коллекции (или нескольких коллекций в зависимости от запроса)

Список запросов:
Получить количество комментов под постами конкретного пользователя (по айди) (список из пост-количество)

Получить список комментов под постами конкретного пользователя (по айди), где body коммента < 50 символов (список из комментов)

Получить список (id, name) из списка todos которые выполнены для конкретного пользователя (по айди)

Получить список пользователей по алфавиту (по возрастанию) с отсортированными todo items по длине name (по убыванию)

Получить следующую структуру (передать Id пользователя в параметры)

User

Последний пост пользователя (по дате)

Количество комментов под последним постом

Количество невыполненных тасков для пользователя

Самый популярный пост пользователя (там где больше всего комментов с длиной текста больше 80 символов)

Самый популярный пост пользователя (там где больше всего лайков)

Получить следующую структуру (передать Id поста в параметры)

Пост

Самый длинный коммент поста

Самый залайканный коммент поста

Количество комментов под постом где или 0 лайков или длина текста < 80

Каждая выборка должна выполняться одним методом.
