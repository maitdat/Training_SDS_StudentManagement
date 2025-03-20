using Training_SDS_StudentManagement.Data;
using Training_SDS_StudentManagement.DesignPattern.Repository;
using Training_SDS_StudentManagement.Model;
using Training_SDS_StudentManagement.Service.StudentService;

namespace Training_SDS_StudentManagement.Service.ConsoleAppWithDB
{
    public class ConsoleAppWithDBService : IConsoleAppWithDBService
    {
        private readonly IStudentService _studentService;
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Classes> _classesRepository;
        private readonly AppDbContext _appDbContext;

        private int currentMenu = 0;
        private enum Menu { AddStudent = 1, ViewStudents = 2, EditStudent = 3, DeleteStudent = 4, SortStudentsByName = 5, SearchStudentById = 6, Exit = 7 };

        public ConsoleAppWithDBService(IStudentService studentService, IGenericRepository<Student> studentRepository,IGenericRepository<Classes> classesRepository, 
            AppDbContext appDbContext)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
            _classesRepository = classesRepository;
            _appDbContext = appDbContext;
        }

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

        private void AddStudent()
        {
            Console.Write("Danh sách lớp:");
            var listClasses = _classesRepository.GetAll();
            listClasses.ToList().ForEach(x => Console.WriteLine($"ID: {x.Id}, Tên : {x.Name}, Môn học: {x.Subject}"));
            var id = long.Parse(Console.ReadLine());
            var item = _appDbContext.Classes.FirstOrDefault(x=>x.Id == id);

            var student = new Student
            {
                Name = Prompt("Nhập tên sinh viên: "),
                DateOfBirth = DateTime.Parse(Prompt("Nhập ngày sinh (yyyy-mm-dd): ")),
                Address = Prompt("Nhập địa chỉ: "),
                Classes = item
            };
            _studentRepository.Add(student);
            _studentRepository.SaveChanges();
            Console.WriteLine("Thêm sinh viên thành công.");
        }

        private void ViewStudents()
        {
            var students = _studentRepository.GetAll().ToList();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address}, Lớp: {student.Classes?.Name}");
            }
        }

        private void EditStudent()
        {
            var id = int.Parse(Prompt("Nhập ID sinh viên để chỉnh sửa: "));
            var student = _studentRepository.GetByIdAsync(id).Result;
            if (student != null)
            {
                student.Name = Prompt("Nhập tên mới: ");
                student.DateOfBirth = DateTime.Parse(Prompt("Nhập ngày sinh mới (yyyy-mm-dd): "));
                student.Address = Prompt("Nhập địa chỉ mới: ");
                student.Classes = PromptGetClass();
                _studentRepository.Update(student);
                _studentRepository.SaveChanges();
                Console.WriteLine("Cập nhật sinh viên thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
            }
        }

        private void DeleteStudent()
        {
            var id = int.Parse(Prompt("Nhập ID sinh viên để xóa: "));
            var student = _studentRepository.GetByIdAsync(id).Result;
            if (student != null)
            {
                _studentRepository.Remove(student);
                _studentRepository.SaveChanges();
                Console.WriteLine("Xóa sinh viên thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
            }
        }

        private void SortStudentsByName()
        {
            var students = _studentService.SortStudentsByName();
            foreach (var student in students)
            {
                Console.WriteLine(student.Name);
            }
        }

        private void SearchStudentById()
        {
            var id = int.Parse(Prompt("Nhập ID sinh viên để tìm kiếm: "));
            var student = _studentRepository.GetByIdAsync(id).Result;
            if (student != null)
            {
                Console.WriteLine($"ID: {student.Id}, Tên: {student.Name}, Ngày sinh: {student.DateOfBirth}, Địa chỉ: {student.Address}, Lớp: {student.Classes?.Name}");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên.");
            }
        }

        private string Prompt(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private Classes? PromptGetClass()
        {
            Console.Write("Danh sách lớp:");
            var listClasses = _classesRepository.GetAll();
            listClasses.ToList().ForEach(x=> Console.WriteLine($"ID: {x.Id}, Tên : {x.Name}, Môn học: {x.Subject}"));
            var id = long.Parse(Console.ReadLine());
            return listClasses.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
