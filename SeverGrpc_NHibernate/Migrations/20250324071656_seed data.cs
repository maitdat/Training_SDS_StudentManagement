using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeverGrpc_NHibernate.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teacher",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" },
                    { 2, new DateTime(1985, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith" },
                    { 3, new DateTime(1975, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Class",
                columns: new[] { "Id", "Name", "Subject", "TeacherId" },
                values: new object[,]
                {
                    { 1, "Math 101", "Mathematics", 1 },
                    { 2, "Math 102", "Mathematics", 1 },
                    { 3, "Math 103", "Mathematics", 1 },
                    { 4, "Physics 201", "Physics", 2 },
                    { 5, "Physics 202", "Physics", 2 },
                    { 6, "Physics 203", "Physics", 2 },
                    { 7, "Chemistry 301", "Chemistry", 3 },
                    { 8, "Chemistry 302", "Chemistry", 3 },
                    { 9, "Chemistry 303", "Chemistry", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Class",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teacher",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
