﻿using Azure.Core;
using SeverGrpc_NHibernate.Model;
using SeverGrpc_NHibernate.RepositoryNHibernate;
using Shared;
using Shared.DTOs.RequestModel;
using Shared.DTOs.ResponseModel;

namespace SeverGrpc_NHibernate.Service
{
    public class StudentService : IStudentService
    {
        private readonly INHibernateRepository<Student> _studentRepository;
        private readonly INHibernateRepository<Class> _classRepository;
        public StudentService(INHibernateRepository<Student> studentRepository, INHibernateRepository<Class> classRepository)
        {
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }
        public async Task AddStudentAsync(StudentRequest request)
        {
            var cls = _classRepository.FindBy(x => x.Id == request.ClassId);
            if (cls == null)
            {
                throw new Exception("Class not found");
            }

            var student = new Student
            {
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Class = cls
            };

            _studentRepository.Add(student);
        }

        public async Task UpdateStudentAsync(StudentRequest request)
        {
            var student = _studentRepository.FindBy(x => x.Id == request.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var cls = _classRepository.FindBy(x => x.Id == request.ClassId);
            if (cls == null)
            {
                throw new Exception("Class not found");
            }

            student.Name = request.Name;
            student.DateOfBirth = request.DateOfBirth;
            student.Address = request.Address;
            student.Class = cls;

            _studentRepository.Update(student);
        }

        public async Task DeleteStudentAsync(IdRequest request)
        {
            var student = _studentRepository.FindBy(x => x.Id == request.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            _studentRepository.Delete(student);
        }

        public StudentResponse GetStudent(IdRequest request)
        {
            var student = _studentRepository.FindBy(x => x.Id == request.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            return new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                Class = new ClassResponse
                {
                    Id = student.Class.Id,
                    Name = student.Class.Name,
                    Subject = student.Class.Subject
                },
                TeacherName = student.Class.Teacher.Name
            };
        }

        public async Task<List<StudentResponse>> GetStudentsAsync()
        {
            var students = _studentRepository.All().ToList();
            var studentResponses = students.Select(student => new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                Class = new ClassResponse
                {
                    Id = student.Class.Id,
                    Name = student.Class.Name,
                    Subject = student.Class.Subject
                },
                TeacherName = student.Class.Teacher.Name
            }).ToList();

            return await Task.FromResult( studentResponses); 
        }
    }
}
