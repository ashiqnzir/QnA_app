using System;
using System.Collections.Generic;

namespace QnA_app.Models
{
    public partial class AnswersTbl
    {
        public int? Qid { get; set; }
        public int Ansid { get; set; }
        public string Answer { get; set; } = null!;
        public int? Likes { get; set; }
        public string? AnsweredBy { get; set; }

        public virtual QuestionsTbl? QidNavigation { get; set; }
    }
}
