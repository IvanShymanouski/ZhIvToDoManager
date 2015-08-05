using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Infrastructure;

namespace clZhIvToDoManager
{
    [Serializable]
    class ToDoItem : IToDoItem
    {
        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public int ToDoId { get; set; }

        public int UserId { get; set; }
    }
}
