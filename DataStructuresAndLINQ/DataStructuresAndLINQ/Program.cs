using System;

namespace DataStructuresAndLINQ
{
    class Program
    {
        static void Main()
        {
            Queries.BindEntities();
            var menu = new Menu();
            var flag = true;
            while (flag)
            {
                Console.Clear();
                menu.ShowMenu();
                flag = menu.Action();
            }
        }
    }
}
