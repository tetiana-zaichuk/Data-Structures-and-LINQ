using System;

namespace DataStructuresAndLINQ
{
    class Program
    {
        static void Main()
        {
            /*var users = HttpRequest<User>.GetInfo(Endpoint.users);
            foreach (var user in users)
            {
                Console.WriteLine($"UserId: {user.Id }, userName: {user.Name}");
            }*/
            Queries.BindEntities();
            /*Queries.NumberOfCommentsUnderPosts(45);
            Queries.CommentsListUnderPosts(45);
            Queries.TodosListDone(45);
            Queries.UsersList();*/
            Console.ReadKey();
        }
        
    }
}
