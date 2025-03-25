using AntDesign.Charts;
using Microsoft.AspNetCore.Components;
using OfficeOpenXml.DataValidation;
using Shared;
using Shared.DTOs.ResponseModel;

namespace Client1.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        private IClassService _classService{ get; set; } = null!;
        [Inject]
        private IStudentService _studentService{ get; set; } = null!;

        private List<TeacherChart> data = new List<TeacherChart>();
        private List<ClassChart> data2 = new List<ClassChart>();


        readonly BarConfig config1 = new BarConfig
        {
            Title = new Title
            {
                Visible = true,
                Text = "Tổng các lớp"
            },
            XField = "values",
            YField = "type"
        };

        readonly BarConfig config2 = new BarConfig
        {
            Title = new Title
            {
                Visible = true,
                Text = "Tổng học sinh"
            },
            XField = "values",
            YField = "type"
        };


        protected override async Task OnInitializedAsync()
        {
            try
            {

                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching students: {ex.Message}");
            }
        }

        private async Task LoadData()
        {
            var classes = await _classService.GetClassesAsync();

            data = classes.GroupBy(x => x.TeacherName).Select(x => new TeacherChart
            {
                type = x.Key,
                values = x.Count()
            }).ToList();

            var students = await _studentService.GetStudentsAsync();
            data2 = students.GroupBy(x => x.Class.Name).Select(x => new ClassChart
            {
                type = x.Key,
                values = x.Count()
            }).ToList();

            StateHasChanged();
        }
    }
    public class TeacherChart
    {
        public string type { get; set; }

        public int values { get; set; }
    }
    public class ClassChart
    {
        public string type { get; set; }
        public int values { get; set; }

    }
}
