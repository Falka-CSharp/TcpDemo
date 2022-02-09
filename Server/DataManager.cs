using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Models;
using System.IO;
namespace Server
{
    public class DataManager
    {
        private string filePath;
        public MyTaskRepos Repos { get; set; }
        public DataManager()
        {
            filePath = @"..\..\Data\mytasks.json";
            Repos = new MyTaskRepos();
        }
        public async void LoadData()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
                Repos = await JsonSerializer.DeserializeAsync<MyTaskRepos>(fs);
        }
        public async void SaveData()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
                await JsonSerializer.SerializeAsync(fs, Repos, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("SAved");
        }
        public void InitData()
        {
            Repos.MyTasks.Add(new MyTask()
            {
                Title = "Title-1",
                About = "About-1",
                Start = new DateTime(2020, 2, 7),
                Finish = new DateTime(2020, 2, 7),
                Status = "In Progress",
                User = "teacher"
            });
            Repos.MyTasks.Add(new MyTask()
            {
                Title = "Title-2",
                About = "About-2",
                Start = new DateTime(2020, 2, 7),
                Finish = new DateTime(2020, 2, 7),
                Status = "In Progress",
                User = "student"
            });
            Repos.MyTasks.Add(new MyTask()
            {
                Title = "Title-3",
                About = "About-3",
                Start = new DateTime(2020, 2, 7),
                Finish = new DateTime(2020, 2, 7),
                Status = "Planned",
                User = "teacher"
            });
            Repos.MyTasks.Add(new MyTask()
            {
                Title = "Title-4",
                About = "About-4",
                Start = new DateTime(2020, 3, 7),
                Finish = new DateTime(2020, 3, 7),
                Status = "In Progress",
                User = "student"
            });
            SaveData();
        }
    }
}
