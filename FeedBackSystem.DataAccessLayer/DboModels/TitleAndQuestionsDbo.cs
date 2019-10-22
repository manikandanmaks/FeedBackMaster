using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedBackSystem.DataAccessLayer.DboModels

{
    public class TitleAndQuestionsDbo
    {
        public int TitleId { get; set; }

        [Required(ErrorMessage = "Please Enter  Title ", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        public string Title { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public decimal AvgRating { get; set; }
        

    }
}