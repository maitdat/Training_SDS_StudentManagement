using Microsoft.AspNetCore.Components;
using Shared;
using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;

namespace Client1.Pages.Student
{
    public partial class ListStudent : ComponentBase
    {
        [Inject]
        private IStudentService _studentService { get;set; } = null!;

        List<StudentResponse> _response = new List<StudentResponse>();
        private CreateOrUpdateDialog? _createOrUpdateModal;

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
            _response = await _studentService.GetStudentsAsync();
        }

        private async Task Delete(int id)
        {
            var newIdRequest = new IdRequest { Id = id };
            await _studentService.DeleteStudentAsync(newIdRequest);

            await LoadData();
        }
        private void OpenModal()
        {
            _createOrUpdateModal!.Title = "Create";
            _createOrUpdateModal?.ShowModal();
        }

        private void OpenUpdateModal(StudentResponse response)
        {
            _createOrUpdateModal!.Title = "Update";

            _createOrUpdateModal!._student = new StudentRequest
            {
                Id = response.Id,
                Name = response.Name,
                DateOfBirth = response.DateOfBirth,
                ClassId = response.Class.Id,
                Address = response.Address
            };
            _createOrUpdateModal?.ShowModal();
        }
    }
}
