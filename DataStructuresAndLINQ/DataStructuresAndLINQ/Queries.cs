﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndLINQ
{
    public static class Queries
    {
        private static List<User> users = HttpRequest<User>.GetInfo(Endpoint.users);
        private static List<Post> posts = HttpRequest<Post>.GetInfo(Endpoint.posts);
        private static List<Comment> comments = HttpRequest<Comment>.GetInfo(Endpoint.comments);
        private static List<Todo> todos = HttpRequest<Todo>.GetInfo(Endpoint.todos);

        public static void BindEntities()
        {
            //var sdf = users.Select(u => u.Todos = todos.Where(t => t.UserId == u.Id).ToList());
            /* var users1 = users.GroupJoin(todos, user => user.Id, todo => todo.UserId,
                (user, ts) => new 
                {
                   /*if (user.Todos != null)
                        user.Todos.Add((Todo) todo);
                    return user;
                }).ToList();
            */

            foreach (var user in users)
            {
                user.Posts = posts.Where(n => n.UserId == user.Id).ToList();

                //var v= user.Posts.Join(comments, n => n.Id, c => c.PostId, k); 
                foreach (var post in user.Posts)
                {
                    post.Comments = comments.Where(n => n.PostId == post.Id).ToList();
                }

               user.Todos = todos.Where(n => n.UserId == user.Id).ToList();
            }
            /*
            users[7].Show();
            users[45].Show();*/
        }

        public static void NumberOfCommentsUnderPosts(int userId)
        {
            var countComments = comments.Where(n => n.UserId == userId).GroupBy(n => n.PostId).Select(n => Tuple.Create(n.Key, n.Count()));
            foreach (var comment in countComments)
            {
                Console.WriteLine($"Post id: {comment.Item1}, number of comments: {comment.Item2}");
            }
        }

        public static void CommentsListUnderPosts(int userId)
        {
            var commentsList = comments.Where(n => n.UserId == userId && n.Body.Length < 50).GroupBy(n => n.PostId);
            foreach (var comment in commentsList)
            {
                Console.WriteLine($"Post id: {comment.Key}, comments list:");
                foreach (var c in comment)
                {
                    Console.WriteLine(c.Body);
                }

            }
        }

        public static void TodosListDone(int userId)
        {
            var todosList = todos.Where(n => n.UserId == userId && n.IsComplete);
            foreach (var todo in todosList)
            {
                Console.WriteLine($"Todos: \n {todo.Name}");
            }
        }

        /*public static void StructureUser(int userId)
        {
            var user = users.Where(n => n.Id == userId);
            posts.OrderBy(n => n.CreatedAt);
        }

        public static void StructurePost(int userId)
        {
            var todosList = todos.Where(n => n.UserId == userId && n.IsComplete);
            foreach (var todo in todosList)
            {
                Console.WriteLine($"Todos: \n {todo.Name}");
            }
        }*/

        public static void UsersList()
        {
            var usersList = users.OrderBy(user =>
            {
                user.Todos = user.Todos.OrderByDescending(todo => todo.Name.Length).ToList();
                return user.Name;
            });
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
        }
    }
}
