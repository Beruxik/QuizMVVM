using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMVVM.Model
{
    public class Questions
    {
        [System.Runtime.Serialization.DataMember(Name = "content")]
        public string? Content { get; set; }
        [System.Runtime.Serialization.DataMember(Name = "answers")]
        public List<Answer>? Answers { get; set; }
    }
}
