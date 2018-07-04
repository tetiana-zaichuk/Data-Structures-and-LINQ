using System;
using System.Collections.Generic;
using System.Text;

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
            var value = GetAndValidateInputInt(0, 8);
            
            Console.Clear();
            switch (value)
            {
                case 1:
                    Console.WriteLine($"Please, enter the user id:  ");
                    var number = GetAndValidateInputInt(1, 100);
                    Queries.NumberOfCommentsUnderPosts(number);
                    break;
                case 2:
                    Console.WriteLine($"Please, enter the user id: ");
                    var number2 = GetAndValidateInputInt(1, 100);
                    Queries.CommentsListUnderPosts(number2);
                    break;
                case 3:
                    Console.WriteLine($"Total revenue: ");
                    var number3 = GetAndValidateInputInt(1, 100);
                    Queries.TodosListDone(number3);
                    break;
                case 4:
                    Queries.UsersList();
                    break;
                case 5:
                    Console.WriteLine($"Please, enter the user id: ");
                    var number5 = GetAndValidateInputInt(1, 100);
                    Queries.StructureUser(number5);
                    break;
                case 6:
                    Console.WriteLine($"Please, enter the post id: ");
                    var number6 = GetAndValidateInputInt(1, 100);
                    Queries.StructurePost(number6);
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

        private void EndOfParagraph()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("Press any key to go to menu.");
            Console.ReadKey();
        }

        public int GetAndValidateInputInt(int firstCondition, int secondCondition)
        {
            int input;
            while (!(int.TryParse(Console.ReadLine(), out input) &&
                     (input >= firstCondition && input <= secondCondition)))
            {
                Console.WriteLine($"You need to enter a number from {firstCondition} to {secondCondition}.");
            }

            return input;
        }
    }
}
