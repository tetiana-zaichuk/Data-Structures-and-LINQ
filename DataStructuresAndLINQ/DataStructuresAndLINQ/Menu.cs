﻿using System;
using System.Linq;

namespace DataStructuresAndLINQ
{
    public class Menu
    {
        private readonly string[] _menu =
        {
            "Please, enter the number of an action.\n",
            "1 - Get the number of comments under the posts of a user",
            "2 - Get the list of comments under the posts of a user, where the body comment <50 characters",
            "3 - Get the list (id, name) from the list of todos that are executed for a user",
            "4 - Get the list of users in alphabetical order (ascending) with sorted todo items by length name (descending)",
            "5 - Get the structure about the user",
            "6 - Get the structure about the post",
            "0 - Exit"
        };

        public void ShowMenu()
        {
            foreach (var el in _menu)
            {
                Console.WriteLine(el);
            }
        }

        public bool Action()
        {
            var flag = true;
            var value = GetAndValidateInputInt(0, 6);
            Console.Clear();
            switch (value)
            {
                case 1:
                    Console.WriteLine($"Please, enter the user id:  ");
                    var number = GetAndValidateInputInt(1, 100);
                    var countComments = Queries.NumberOfCommentsUnderPosts(number);
                    if (countComments?.Any() != true) Console.WriteLine("There are no comments/posts.");
                    foreach (var comment in countComments)
                    {
                        Console.WriteLine($"Post id: {comment.Item1}, number of comments: {comment.Item2}");
                    }
                    break;
                case 2:
                    Console.WriteLine($"Please, enter the user id: ");
                    var number2 = GetAndValidateInputInt(1, 100);
                    var commentsList = Queries.CommentsListUnderPosts(number2);
                    if (commentsList?.Any() != true) Console.WriteLine("There are no comments/posts.");
                    foreach (var comment in commentsList)
                    {
                        Console.WriteLine($"Post id: {comment.Key}, comments list:");
                        foreach (var c in comment)
                        {
                            Console.WriteLine(c.Body);
                        }
                    }
                    break;
                case 3:
                    Console.WriteLine($"Please, enter the user id: ");
                    var number3 = GetAndValidateInputInt(1, 100);
                    var todosList = Queries.TodosListDone(number3);
                    if (todosList?.Any() != true) Console.WriteLine("There are no TODOs.");
                    foreach (var todo in todosList)
                    {
                        Console.WriteLine($"Todos: \n {todo.Name}");
                    }
                    break;
                case 4:
                    var usersList = Queries.UsersList();
                    foreach (var el in usersList)
                    {
                        Console.WriteLine($"User: {el.Name}");
                        /*if (user.todo.?.Any() != true) continue;*/
                        Console.WriteLine("TODOs:");
                        foreach (var l in el.Todos)
                        {
                            Console.WriteLine($"    {l.Name}");
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine($"Please, enter the user id: ");
                    var number5 = GetAndValidateInputInt(1, 100);
                    Queries.StructureUser(number5);
                    break;
                case 6:
                    Console.WriteLine($"Please, enter the post id: ");
                    var number6 = GetAndValidateInputInt(1, 100);
                    var tuple = Queries.StructurePost(number6);
                    //if (tuple.post?.Comments?.Any() != true)
                    try
                    {
                        Console.WriteLine($"\nPost title:\n    {tuple.post.Title}. " +
                        $"\nThe longest comment of the post:\n    {tuple.longestComment.Body}. " +
                        $"\nThe most likes comment of the post:\n    {tuple.commentWithMaxLikes.Body}. " +
                        $"\nNumber of comments under the post where or 0 likes or text length <80:\n    {tuple.numberOfBadComments}.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"There are no comments/posts. ");
                    }

                    break;
                default:
                    flag = false;
                    break;
            }
            if (flag)
            {
                EndOfParagraph();
            }
            return flag;
        }

        private static void EndOfParagraph()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to go to menu.");
            Console.ReadKey();
        }

        public int GetAndValidateInputInt(int firstCondition, int secondCondition)
        {
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input) && (input >= firstCondition && input <= secondCondition)))
            {
                Console.WriteLine($"You need to enter a number from {firstCondition} to {secondCondition}.");
            }
            return input;
        }
    }
}
