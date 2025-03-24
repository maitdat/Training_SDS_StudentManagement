using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OneOf.Types;
using Shared;
using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;
using System.Text.Json;

namespace Client1.Pages.Student
{
    public partial class CreateOrUpdateDialog : ComponentBase
    {
        [Inject]
        private IStudentService _studentService { get; set; } = null!;
        [Inject]
        private IClassService _classService { get; set; } = null!;

        [Parameter] public EventCallback OnSuccess { get; set; }

        public StudentRequest _student = new StudentRequest();
        private List<ClassResponse> _classes = new List<ClassResponse>();
        private bool _visible;
        public string Title { get; set; } = null!;

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

        public void ShowModal()
        {
            _visible = true;
            StateHasChanged();
        }

        private async Task LoadData()
        {
            _classes = await _classService.GetClassesAsync();
        }

        private async Task OnFinish(EditContext editContext)
        {
            if(_student.Id >0)
            {
                await _studentService.UpdateStudentAsync(_student);
            }
            else
            {
                await _studentService.AddStudentAsync(_student);
            }
            _visible = false;
            _student = new StudentRequest();

            await OnSuccess.InvokeAsync();
            StateHasChanged();
        }

        private void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(_student)}");
        }

        
    }
}
