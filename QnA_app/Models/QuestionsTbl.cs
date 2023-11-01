using System;
using System.Collections.Generic;

namespace QnA_app.Models
{
    public partial class QuestionsTbl
    {
        public QuestionsTbl()
        {
            AnswersTbls = new HashSet<AnswersTbl>();
        }

        public int Qid { get; set; }
        public string Question { get; set; } = null!;
        public string? Askedby { get; set; }

        public virtual ICollection<AnswersTbl> AnswersTbls { get; set; }
    }
}
