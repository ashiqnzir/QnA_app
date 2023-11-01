using System;
using System.Collections.Generic;

namespace QnA_app.Models
{
    public partial class CommentsTbl
    {
        public int? Ansid { get; set; }
        public string Comment { get; set; } = null!;

        public virtual AnswersTbl? Ans { get; set; }
    }
}
