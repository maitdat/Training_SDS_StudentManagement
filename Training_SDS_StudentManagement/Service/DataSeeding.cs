using Training_SDS_StudentManagement.Model;

namespace Training_SDS_StudentManagement.Service
{
    public class DataSeeding
    {
        public static List<Teacher> SeedTeachers()
        {
            return new List<Teacher>
                {
                    new Teacher { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(2000, 10,10) },
                    new Teacher { Id = 2, Name = "Jane Smith", DateOfBirth = new DateTime(1992, 10,2) },
                    new Teacher { Id = 3, Name = "Emily Johnson", DateOfBirth =  new DateTime(1985, 2,13) }
                };
        }

        public static List<Classes> SeedClasses()
        {
            return new List<Classes>
                {
                    new Classes { Id = 1, Name = "Math 101",Subject = "Math", TeacherId = 1 },
                    new Classes { Id = 2, Name = "Science 101",Subject = "Science", TeacherId = 2 },
                    new Classes { Id = 3, Name = "History 101",Subject = "History", TeacherId = 3 }
                };
        }
    }
}
