namespace BuildBackEnd.Core.Models
{
    public class CourseCategoryBridge
    {

        public int CourseId { get; set; }
        public Courses Course { get; set; }


        public int CategoryId { get; set; }
        public Categories Category { get; set; }

    }
}
