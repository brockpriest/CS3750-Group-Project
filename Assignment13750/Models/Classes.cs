using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace Assignment13750.Models
{
    public class Classes
    {
        public int Id { get; set; }
        public string CourseName { get; set; } 
        public string CourseType { get; set; }
        public int Credits { get; set; }
        public int CourseNo { get; set; }
        public string RoomNo { get; set; }

        public string? MeetingDays { get; set; } 
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

    }
}
