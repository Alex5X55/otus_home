using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public enum AnswerStatus
        {
        OK,
        Error,
        NotFound
            }
    public class Answer
    {
       public AnswerStatus answerStatus { get; set; }
       public string text { get; set; }
    }





}
