using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructuresAndLINQ
{
    public static class Queries
    {
        private static List<User> users = HttpRequest<User>.GetInfo(Endpoint.users);
        private static List<Post> posts = HttpRequest<Post>.GetInfo(Endpoint.posts);
        private static List<Comment> comments = HttpRequest<Comment>.GetInfo(Endpoint.comments);
        private static List<Todo> todos = HttpRequest<Todo>.GetInfo(Endpoint.todos);

        static Queries()
        {
            users=BindEntities();
        }

        public static List<User> BindEntities()
        {
            var userWithTodo = users.GroupJoin(todos, user => user.Id, todo => todo.UserId, (u, t) => new { u.Name, u.Avatar, u.Id, u.CreatedAt, u.Email, todo = t.Select(n => n) });
            var postWithComment = posts.GroupJoin(comments, post => post.Id, com => com.PostId, (p, c) => new { p.Id, p.CreatedAt, p.Likes, p.Title, p.UserId, p.Body, Comments = c.Select(n => n) });
            var usersFinal = userWithTodo.GroupJoin(postWithComment, user => user.Id, post => post.UserId, (u, p) => Tuple.Create(u.Name, u.Avatar, u.Id, u.CreatedAt, u.Email, u.todo, p.Select(n => n)));
            foreach (var user in users)
            {
                user.Posts = posts.Where(n => n.UserId == user.Id).ToList();
                foreach (var post in user.Posts)
                {
                    post.Comments = comments.Where(n => n.PostId == post.Id).ToList();
                }
                user.Todos = todos.Where(n => n.UserId == user.Id).ToList();
            }
            return users;
        }

        public static List<Tuple<int, int>> NumberOfCommentsUnderPosts(int userId)
            => comments.Where(n => n.UserId == userId).GroupBy(n => n.PostId).Select(n => Tuple.Create(n.Key, n.Count())).ToList();

        public static List<IGrouping<int, Comment>> CommentsListUnderPosts(int userId)
            => comments.Where(n => n.UserId == userId && n.Body.Length < 50).GroupBy(n => n.PostId).ToList();
         
        public static List<Todo> TodosListDone(int userId) => todos.Where(n => n.UserId == userId && n.IsComplete).ToList();

        public static List<User> UsersList()
        {
            return users.OrderBy(user =>
            {
                user.Todos = user.Todos.OrderByDescending(todo => todo.Name.Length).ToList();
                return user.Name;
            }).ToList();
        }

        public static (User, Post, int?, int?, Post, Post) StructureUser(int userId)
            => users.Where(u => u.Id == userId).Select(u => (u,
                u.Posts.OrderByDescending(n => n.CreatedAt).FirstOrDefault(),
                u.Posts.OrderByDescending(n => n.CreatedAt).FirstOrDefault()?.Comments.Count(),
                u.Todos?.Count(t => t.IsComplete == false),
                u.Posts?.OrderBy(p => p.Comments.Count(c => c.Body.Length > 80)).LastOrDefault(),
                u.Posts?.OrderBy(p => p.Likes).LastOrDefault())).ToList().FirstOrDefault();

        public static (Post, Comment, Comment, int) StructurePost(int postId)
            => users.SelectMany(user => user.Posts, (user, post) => new { user, post })
                .Where(@t => @t.post.Id == postId)
                .Select(@t => (@t.post, @t.post.Comments.OrderBy(com => com.Body.Length).LastOrDefault(),
                    @t.post.Comments.OrderBy(p => p.Likes).LastOrDefault(),
                    @t.post.Comments.Count(c => c.Likes == 0 || c.Body.Length < 80))).ToList().FirstOrDefault();
    }
}
