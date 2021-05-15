using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Models
{
    public class Question
    {
        public string Title { get; set; }
        public ICollection<Alternative> Alternatives { get; set; }
    }
}
