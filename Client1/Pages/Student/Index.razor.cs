using AntDesign;
using AntDesign.TableModels;
using BlazorDownloadFile;
using Common;
using Microsoft.AspNetCore.Components;
using OfficeOpenXml;
using SeverGrpc_NHibernate.Utilities;
using Shared;
using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;

namespace Client1.Pages.Student
{
    public partial class Index : ComponentBase
    {
        [Inject]
        private IStudentService _studentService { get; set; } = null!;
        [Inject] private IBlazorDownloadFileService DownloadFileService { get; set; } = null!;

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

        private async Task ExportExcel()
        {
            StudentPaginationRequest.BasePaginationRequest.PageSize = 500;
            var data =await _studentService.GetPaginationAsync(StudentPaginationRequest);
            string sheetName = "Student List";
            List<string> headers = new() { "ID", "Name", "Date of Birth", "Address", "Class", "Teacher" };

            // Dữ liệu sinh viên (BodyExcel)
            List<List<string>> body = data.Data.Select(s => new List<string>
            {
                s.Id.ToString(),
                s.Name,
                s.DateOfBirth.ToString("dd-MM-yyyy"), 
                s.Address,
                s.Class?.Name ?? "N/A", 
                s.TeacherName
            }).ToList();

            StudentPaginationRequest.BasePaginationRequest.PageSize = 10;

            ExcelPackage excelFile = Utilities.ExportExcel(sheetName, headers, body, "A1", 2, 3);

            byte[] fileBytes = excelFile.GetAsByteArray();
            await DownloadFileService.DownloadFile("Students.xlsx", fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

      
    }
}
