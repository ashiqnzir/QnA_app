using Microsoft.AspNetCore.Mvc;
using QnA_app.Models;
using System.Diagnostics;

namespace QnA_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static int iidd { get; set; }
        public QnA_DBContext Context { get; }

        public HomeController(ILogger<HomeController> logger, QnA_DBContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(QuestionsTbl Q)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Signup_Login", "auth");
            }
            if (ModelState.IsValid)
            {
                var username=HttpContext.Session.GetString("username");
                QuestionsTbl Q1 = new QuestionsTbl()
                {
                    Question = Q.Question,
                    Askedby=username
                 };
                Context.QuestionsTbls.Add(Q1);
                Context.SaveChanges();
            }
            return View();
        }

        public IActionResult Questions()
        {
            List<QuestionsTbl> Qs = new List<QuestionsTbl>();
            foreach (var item in Context.QuestionsTbls)
            {
                Qs.Add(item);
            }
            return View(Qs);
        }


        //when a user will click on anser this question , this action will get called
       
        public IActionResult AnswerThisQuestion(int id)
        {
            iidd = id;
            
            var question = Context.QuestionsTbls.FirstOrDefault(item => item.Qid == id);
            QuestionsTbl obj = new QuestionsTbl
            {
                Qid = question.Qid,
                Question=question.Question,
                Askedby=question.Askedby
            };
            ViewBag.Question = obj;
            return View();

        }
        [HttpPost]
        public IActionResult AnswerThisQuestion(AnswersTbl data)
        {
            
            if (ModelState.IsValid)
            {
                var getuser = HttpContext.Session.GetString("username");
                AnswersTbl obj = new AnswersTbl
                {
                    Qid = iidd,
                    Answer = data.Answer,
                    AnsweredBy = getuser


                };

                Context.AnswersTbls.Add(obj);
                Context.SaveChanges();
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "unknown error");
            }
            return View();
        }
        public IActionResult SolvedQuestions()
        {
           
            var joinedData = from t1 in Context.AnswersTbls
                             join t2 in Context.QuestionsTbls on t1.Qid equals t2.Qid
                             select new
                             {
                                 Answer = t1.Answer,
                                 Answerby=t1.AnsweredBy,
                                 Question = t2.Question,
                                 Questionby=t2.Askedby
                             };

            return View(joinedData.ToList());

        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}