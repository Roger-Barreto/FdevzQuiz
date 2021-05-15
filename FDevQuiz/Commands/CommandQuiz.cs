using FDevQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Commands
{
    public class CommandQuiz
    {
        public int DifficultyId { get; set; }
        public string Title { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
