using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDevQuiz.Commands
{
    public class CommandUser
    {
        public string Name { get; set; }
        public int? Score { get; set; }
        public string ImgUrl { get; set; }
    }
}
