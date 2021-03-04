using System;
namespace PocketCloset.Models
{
    public class SecQuestion
    {
        public int userId { get; set; }
        public int queId { get; set; }
        public string question { get; set; } //security question
        public string answer { get; set; }


        public SecQuestion() { }

        public SecQuestion(int userId, string question, string answer)
        {
            this.userId = userId;
            this.question = question;
            this.answer = answer;
        }

        public SecQuestion(int userId, int queId, string question, string answer)
        {
            this.userId = userId;
            this.queId = queId;
            this.question = question;
            this.answer = answer;
        }

    }
}
