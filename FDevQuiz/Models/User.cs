using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string ImgUrl { get; set; }
    }
}
