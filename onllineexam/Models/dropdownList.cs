using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace onllineexam.Models
{
    
        public class DrpList
        {
            public string exam_title { get; set; }
            public int examid { get; set; }

            public List<TestGenerator> Examlist { get; set; }
            public int QuestionNo { get; set; }
        }
        public class CustomModel
        {
            public List<DrpList> Examlist { get; set; }
            public int ExamId { get; set; }
        }

        
    }

