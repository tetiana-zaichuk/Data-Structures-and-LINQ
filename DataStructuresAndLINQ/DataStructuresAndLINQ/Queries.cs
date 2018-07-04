using System;
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
        }

        public static IEnumerable<Tuple<int, int>> NumberOfCommentsUnderPosts(int userId)
            => comments.Where(n => n.UserId == userId).GroupBy(n => n.PostId).Select(n => Tuple.Create(n.Key, n.Count()));

        public static IEnumerable<IGrouping<int, Comment>> CommentsListUnderPosts(int userId)
            => comments.Where(n => n.UserId == userId && n.Body.Length < 50).GroupBy(n => n.PostId);

        public static IEnumerable<Todo> TodosListDone(int userId) => todos.Where(n => n.UserId == userId && n.IsComplete);

        public static List<User> UsersList()
        {
            return users.OrderBy(user =>
            {
                user.Todos = user.Todos.OrderByDescending(todo => todo.Name.Length).ToList();
                return user.Name;
            }).ToList();
        }

        public static void StructureUser(int userId)
        {
            try
            {
                var user = users.Find(n => n.Id == userId);
                var lastPost = user.Posts.OrderByDescending(n => n.CreatedAt).First();
                var numberOfComments = lastPost.Comments.Count();
                var numberOfUncompletedTasks = user.Todos.Count(t => t.IsComplete == false);
                var popularPostLongComments = user.Posts.OrderBy(p => p.Comments.Count(c => c.Body.Length > 80)).Last();
                var popularPostMaxLikes = user.Posts.Find(c => c.Likes >= user.Posts.Max(p => p.Likes));
                Console.WriteLine($"User:\n    {user.Name}. \nLast post:\n    {lastPost.CreatedAt}, title: {lastPost.Title}. " +
                                  $"\nNumber of comments under the last post:\n    {numberOfComments}." +
                                  $"\nNumber of unfulfilled tasks:\n    {numberOfUncompletedTasks}. " +
                                  $"\nThe most popular user post (a text length of more than 80 characters):\n    {popularPostLongComments.Title}. " +
                                  $"\nThe most popular user post (most of the likes):\n    {popularPostMaxLikes.Title}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There are no comments/posts.");
            }

        }

        public static void StructurePost(int postId)
        {
            var post = posts.Find(n => n.Id == postId);
            if (post?.Comments?.Any() != true)
            {
                Console.WriteLine($"There are no comments post id {postId}. ");
                return;
            }
            var longestComment = post.Comments.Find(c => c.Body.Length >= post.Comments.Max(com => com.Body.Length));
            //var numberOfLetters = post.Comments.Max(c => c.Body.Length); 
            //var numberOfMaxLikes = post.Comments.Max(p => p.Likes);
            var commentWithMaxLikes = post.Comments.Find(c => c.Likes >= post.Comments.Max(p => p.Likes));
            var numberOfBadComments = post.Comments.Count(c => c.Likes == 0 || c.Body.Length < 80);
            Console.WriteLine($"\nPost title:\n    {post.Title}. " +
                              $"\nThe longest comment of the post:\n    {longestComment.Body}. " +
                              $"\nThe most likes comment of the post:\n    {commentWithMaxLikes.Body}. " +
                              $"\nNumber of comments under the post where or 0 likes or text length <80:\n    {numberOfBadComments}.");
        }
    }
}
