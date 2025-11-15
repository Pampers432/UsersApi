using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public char ExitKeyLetter { get; set; }

        public Guid Room_id { get; set; }

        public Room Room { get; set; }

        public Question() { }

        private Question(string text, char exitKeyLetter, Guid room_id)
        {
            Id = Guid.NewGuid();
            Text = text;
            ExitKeyLetter = exitKeyLetter;
            Room_id = room_id;
        }

        public static Question CreateQuestion(string text, char exitKeyLetter, Guid room_id)
        {
            return new Question(text, exitKeyLetter, room_id);
        }
    }
}