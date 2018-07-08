using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresAndLINQ
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public List<Post> Posts=new List<Post>();
        public List<Todo> Todos=new List<Todo>();

        public void Show()
        {
            Console.WriteLine($"User id: {Id}, name: {Name}, email: {Email}, ");
            foreach (var post in Posts)
            {
                Console.WriteLine($"Post title: {post.Title}");
                if (post.Comments?.Any() == true)
                    foreach (var comment in post.Comments)
                    {
                        Console.WriteLine($"Comment: {comment.Body}");
                    }
            }
            if (Todos?.Any() == true)
                foreach (var todo in Todos)
                {
                    Console.WriteLine($"Todo: {todo.Name}");
                }
        }
    }
}
