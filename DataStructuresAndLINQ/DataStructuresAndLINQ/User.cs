﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndLINQ
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public List<Post> Posts;
        public List<Todo> Todos;

        public void Show()
        {
            Console.WriteLine($"User id: {Id}, name: {Name}, email: {Email}, ");
            foreach (var post in Posts)
            {
                Console.WriteLine($"Post title: {post.Title}");
                foreach (var comment in post.Comments)
                {
                    Console.WriteLine($"Comment: {comment.Body}");
                }
            }
            if(Todos?.Any()==true)
            foreach (var todo in Todos)
            {
                Console.WriteLine($"Todo: {todo.Name}");
            }
        }
    }
}
