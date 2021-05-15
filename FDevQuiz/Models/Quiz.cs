using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int DifficultyId { get; set; }
        public string Title { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
