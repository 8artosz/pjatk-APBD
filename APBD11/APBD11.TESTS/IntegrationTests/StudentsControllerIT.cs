using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Apbd14.DTO.Response;
using Apbd14.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Apbd14.Tests.IntegrationTests
{
    [Collection("Integration tests")]
    public class StudentsControllerIT
    {
        private readonly HttpClient _httpClient;
        private readonly TestApbdWebApplicationFactory<TestStartup> _factory;

        public StudentsControllerIT(TestApbdWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task GetStudentsList_200Ok()
        {
            HttpResponseMessage result = await _httpClient.GetAsync("api/students");

            Assert.NotNull(result);

            var studentList = JsonConvert.DeserializeObject<List<StudentResponse>>(await result.Content.ReadAsStringAsync());

            Assert.NotEmpty(studentList);
            //Assert.True(studentList.Count > 0);
           // Assert.False(studentList.Count < 1);

        }
        public async Task GetStudentsById_404NotFound()
        {
            HttpResponseMessage result = await _httpClient.GetAsync("api/students/1000");
            Assert.NotNull(result);
            Assert.True(result.StatusCode == HttpStatusCode.NotFound);
        }
        [Fact]
        public async Task GetStudentsById_200Ok()
        {
            HttpResponseMessage result = await _httpClient.GetAsync("api/students/1");
            Assert.NotNull(result);
            var student = JsonConvert.DeserializeObject<Student>(await result.Content.ReadAsStringAsync());
            Assert.NotNull(student);
            Assert.True(student.IdStudent == 1);
            Assert.True(student.IdGroup == 1);
            Assert.True(student.FirstName == "Jan");
            Assert.True(student.LastName == "Janowski");

        }
        [Fact]
        public async Task DeleteStudentById_204NoContent()
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync("api/students/1");
            Assert.NotNull(result);

            Assert.True(result.StatusCode == HttpStatusCode.NoContent);
        }
        [Fact]
        public async Task DeleteStudentById_404NotFound()
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync("api/students/1000");
            Assert.NotNull(result);

            Assert.True(result.StatusCode == HttpStatusCode.NotFound);


        }

    }
}
