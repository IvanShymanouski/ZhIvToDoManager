using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Infrastructure;
using System.Runtime.Serialization.Json;
using System.IO;

namespace clZhIvToDoManager
{
    public class ToDoManager : IToDoManager
    {

        string bindName = "BasicHttpBinding_IToDoManager";
        string uri = "http://epbyminw0341/ToDoManagerService/TodoManager.svc";
        static bool runApp = true;

        public void CreateToDoItem(IToDoItem todo)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);

            List<IToDoItem> exList = new List<IToDoItem>(0);
            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {                
            }
        }

        public int CreateUser(string name)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);

            List<IToDoItem> exList = new List<IToDoItem>(0);
            int Id;
            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {
                Id = client.CreateUser(name);
            }
            return Id;
        }

        public void DeleteToDoItem(int todoItemId)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);

            List<IToDoItem> exList = new List<IToDoItem>(0);
            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {
                
            }
        }

        public List<IToDoItem> GetTodoList(int userId)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);
            List<IToDoItem> exList = new List<IToDoItem>(0);

            if (runApp)
            {
                runApp = false;
                using (var client = new Remote.ToDoManagerClient(binding, endP))
                {
                    var list = client.GetTodoList(userId);
                    foreach (var l in list)
                    {
                        exList.Add(new ToDoItem() { Name = l.Name, IsCompleted = l.IsCompleted, ToDoId = l.ToDoId, UserId = l.UserId });
                    }
                }
                WriteItemsToFile(exList);
            }
            else
            {
                //exList = ReadItemsFromFile();
                asyncReadToDoItem(userId);
            }

            return exList;
        }

        public void UpdateToDoItem(IToDoItem todo)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);

            List<IToDoItem> exList = new List<IToDoItem>(0);
            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {

            }
        }

        private void WriteItemsToFile(List<IToDoItem> todo)
        {
            const string filename = "userItems.json";
            var s = new DataContractJsonSerializer(typeof(List<ToDoItem>));
            using (var fs = File.OpenWrite(filename))
            {
                foreach (var p in todo)
                {
                    s.WriteObject(fs, p);
                }
            }
        }

        private List<IToDoItem> ReadItemsFromFile()
        {
            const string filename = "userItems.json";
            var s = new DataContractJsonSerializer(typeof(List<ToDoItem>));
            var dat = new List<IToDoItem>();
            if (!File.Exists("userItems.json")) return dat;
            using (var fs = File.OpenRead(filename))
            {
                dat = s.ReadObject(fs) as List<IToDoItem>;
            }
            return dat;
        }

        private async void asyncReadToDoItem(int userId)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);
            List<IToDoItem> exList = new List<IToDoItem>(0);

            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {
                var list = await client.GetTodoListAsync(userId);
                foreach (var l in list)
                {
                    exList.Add(new ToDoItem() { Name = l.Name, IsCompleted = l.IsCompleted, ToDoId = l.ToDoId, UserId = l.UserId });
                }
            }
            WriteItemsToFile(exList);
        }

        private async void asyncWriteToDoItem(Remote.ToDoItem item)
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None);
            binding.Name = bindName;
            binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            var endP = new EndpointAddress(uri);
            List<IToDoItem> exList = new List<IToDoItem>(0);

            using (var client = new Remote.ToDoManagerClient(binding, endP))
            {
                await client.CreateToDoItemAsync(item);
            }
        }
    }
}
