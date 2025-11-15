using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Room
    {
        public Guid Id { get; set; }

        public string ExitKeyWord { get; set; }

        public Guid Test_id { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public Room() { }

        private Room(string exitKeyWord, Guid test_id)
        {
            Id = Guid.NewGuid();
            ExitKeyWord = exitKeyWord;
            Test_id = test_id;
        }

        public static Room CreateRoom(string exitKeyWord, Guid test_id)
        {
            return new Room(exitKeyWord, test_id);
        }
    }
}