using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingServer.Models
{
    public class TrainingModel
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}