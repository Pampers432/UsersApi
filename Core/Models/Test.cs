using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Test
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid Users_id { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();

        public Test() { }

        private Test(string title, Guid users_id)
        {
            Id = Guid.NewGuid();
            Title = title;
            Users_id = users_id;
        }

        public static Test CreateTest(string title, Guid users_id)
        {
            return new Test(title, users_id);
        }
    }
}