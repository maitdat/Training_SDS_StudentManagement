using System.ComponentModel.Design;
using System.Xml.Linq;
using Training_SDS_StudentManagement.Model;

namespace Training_SDS_StudentManagement.Service
{
    public class ConsoleService
    {
        private List<Student> students = new List<Student>();
        private List<Classes> classes = new List<Classes>()
            {
                new Classes { Id = 1, Name = "C2008L", Subject = "C#", Teacher = new Teacher { Id = 1, Name = "John Doe" } },
                new Classes { Id = 2, Name = "C2009L", Subject = "Java", Teacher = new Teacher { Id = 2, Name = "Jane Doe" } },
                new Classes { Id = 3, Name = "C2010L", Subject = "Python", Teacher = new Teacher { Id = 3, Name = "Jack Doe" } }
            };
        private int nextId = 1;
        private int currentMenu = 0;
        private enum Menu { AddStudent = 1, ViewStudents = 2, EditStudent = 3, DeleteStudent = 4, SortStudentsByName = 5, SearchStudentById = 6, Exit = 7 };

        public void Run()
        {
            while (currentMenu != (int)Menu.Exit)
            {
                Console.WriteLine("\nHệ thống quản lý sinh viên");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xem danh sách sinh viên");
                Console.WriteLine("3. Sửa thông tin sinh viên");
                Console.WriteLine("4. Xóa sinh viên");
                Console.WriteLine("5. Sắp xếp sinh viên theo tên");
                Console.WriteLine("6. Tìm kiếm sinh viên theo mã");
                Console.WriteLine("7. Thoát");
                Console.Write("Nhập lựa chọn của bạn: ");
                currentMenu = int.Parse(Console.ReadLine() ?? "0");
                switch (currentMenu)
                {
                    case (int)Menu.AddStudent:
                        AddStudent();
                        break;
                    case (int)Menu.ViewStudents:
                        ViewStudents();
                        break;
                    case (int)Menu.EditStudent:
                        EditStudent();
                        break;
                    case (int)Menu.DeleteStudent:
                        DeleteStudent();
                        break;
                    case (int)Menu.SortStudentsByName:
                        SortStudentsByName();
                        break;
                    case (int)Menu.SearchStudentById:
                        SearchStudentById();
                        break;
                    case (int)Menu.Exit:
                        Console.WriteLine("Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
        }

        public void AddStudent()
        {
            Student student = new Student();
            student.Id = nextId++;

            Console.Write("Nhập tên: ");
            student.Name = Console.ReadLine() ?? "";

            DateTime dateOfBirth;
            while (true)
            {
                Console.Write("Ngày sinh (yyyy-mm-dd): ");
                string dobInput = Console.ReadLine();
                if (DateTime.TryParse(dobInput, out dateOfBirth))
                {
                    student.DateOfBirth = dateOfBirth;
                    break;
                }
                else
                {
                    Console.WriteLine("Ngày sinh không hợp lệ. Vui lòng thử lại.");
                }
            }

            Console.Write("Địa chỉ: ");
            student.Address = Console.ReadLine() ?? "";

            Console.WriteLine("Danh sách lớp:");
            for (int i = 0; i < classes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {classes[i].Name} - {classes[i].Subject} - {classes[i].Teacher.Name}");
            }

            Console.Write("Nhập số thứ tự lớp: ");
            int classIndex = int.Parse(Console.ReadLine() ?? "0") - 1;
            if (classIndex >= 0 && classIndex < classes.Count)
            {
                student.Classes = classes[classIndex];
            }
            else
            {
                Console.WriteLine("Lớp không hợp lệ.");
                return;
            }

            students.Add(student);
            Console.WriteLine("Thêm thành công");
        }

        public void ViewStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Không có sinh viên nào.");
                return;
            }

            Console.WriteLine("\nDanh sách sinh viên:");
            foreach (var s in students)
            {
                Console.WriteLine($"Mã sinh viên: {s.Id}, Tên: {s.Name}, Ngày sinh: {s.DateOfBirth:yyyy-MM-dd}, Địa chỉ: {s.Address}, Lớp: {s.Classes.Name}");
            }
        }

        public void EditStudent()
        {
            Console.Write("Nhập mã sinh viên để sửa: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Student? student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            Console.Write("Nhập tên mới (để trống để giữ nguyên): ");
            string name = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(name)) student.Name = name;

            Console.Write("Nhập ngày sinh mới (yyyy-mm-dd, để trống để giữ nguyên): ");
            string dobInput = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(dobInput)) student.DateOfBirth = DateTime.Parse(dobInput);

            Console.Write("Nhập địa chỉ mới (để trống để giữ nguyên): ");
            string address = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(address)) student.Address = address;

            Console.WriteLine("Danh sách lớp:");
            for (int i = 0; i < classes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {classes[i].Name} - {classes[i].Subject} - {classes[i].Teacher.Name}");
            }

            Console.Write("Nhập số thứ tự lớp: ");
            int classIndex = int.Parse(Console.ReadLine() ?? "0") - 1;
            if (classIndex >= 0 && classIndex < classes.Count)
            {
                student.Classes = classes[classIndex];
            }
            else
            {
                Console.WriteLine("Lớp không hợp lệ.");
                return;
            }

            Console.WriteLine("Cập nhật sinh viên thành công!");
        }

        public void DeleteStudent()
        {
            Console.Write("Nhập mã sinh viên để xóa: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Student? student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            students.Remove(student);
            Console.WriteLine("Xóa sinh viên thành công!");
        }

        public void SortStudentsByName()
        {
            students = students.OrderBy(s => s.Name).ToList();
            Console.WriteLine("Sắp xếp sinh viên theo tên thành công.");

        }

        public void SearchStudentById()
        {
            Console.Write("Nhập mã sinh viên để tìm kiếm: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Student? student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine($"Mã sinh viên: {student.Id}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth:yyyy-MM-dd}, Địa chỉ: {student.Address}, Lớp: {student.Classes.Name}");
        }
    }
}
