using AntDesign;
using AntDesign.TableModels;
using Common;
using Microsoft.AspNetCore.Components;
using Shared;
using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;

namespace Client1.Pages.Student
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IStudentService _studentService { get; set; } = null!;

        private StudentPaginationRequest StudentPaginationRequest = new StudentPaginationRequest();
        BasePaginationResponse<StudentResponse> _response = new BasePaginationResponse<StudentResponse>();
        private CreateOrUpdateDialog? _createOrUpdateModal;

        private async Task LoadData()
        {
            try
            {
                
                _response = await _studentService.GetPaginationAsync(StudentPaginationRequest);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
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
        private async Task HandleTableChange(QueryModel<StudentResponse> query)
        {
            StudentPaginationRequest.BasePaginationRequest.PageNo = query.PageIndex;
            StudentPaginationRequest.BasePaginationRequest.PageSize = query.PageSize; 
            var sortColumn = query.SortModel?.FirstOrDefault(); 

            if (sortColumn != null && sortColumn.SortDirection == SortDirection.Ascending)
            {
                StudentPaginationRequest.SortByName = Sort.Asc;
            } 
            else if (sortColumn != null && sortColumn.SortDirection == SortDirection.Descending)
            {
                StudentPaginationRequest.SortByName = Sort.Desc;
            }
            else
            {
                StudentPaginationRequest.SortByName = Sort.None;
            }

                await LoadData();
        }

        private async Task ApplyFilters()
        {
            StudentPaginationRequest.BasePaginationRequest.PageNo = 1;
            await LoadData();
        }

    }
}
