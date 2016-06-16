using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymReservation.Models
{
    public class GymClass
    {
        public int ID { get; set; }
              
        public string Name { get; set; }
         
         DateTime StartTime { get; set; }
         
        public TimeSpan Duration { get; set; }
         
        public DateTime EndTime => StartTime + Duration;

        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> AttendingMembers { get; set; }
    }
}