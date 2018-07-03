using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresAndLINQ
{
    public class Todo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
    }
}
