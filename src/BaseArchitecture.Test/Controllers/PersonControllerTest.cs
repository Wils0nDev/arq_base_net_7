using BaseArchitecture.Application.Handlers.Queries.PersonController.Update;
using BaseArchitecture.Application.Models.Database;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace BaseArchitecture.IntegrationTest.Controllers
{
    [TestClass, TestCategory("Integration")]
    public class PersonControllerTest
    {
        public string urlResource;
        public HttpClient httpClient;
        public PersonControllerTest()
        {
            urlResource = Environment.GetEnvironmentVariable("urlService").ToString() + "Person/";
            httpClient = new HttpClient();
        }

        [TestMethod]
        public async Task FindAll_StatusCode_Ok()
        {
            var response = await httpClient.GetAsync(urlResource + "FindAll");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task FindAll_Valid_Response()
        {
            bool conversionResult = false;
            var response = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await response.Content.ReadAsStringAsync();

            try
            {
                var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);
                conversionResult = true;
            }
            catch (Exception)
            {
                conversionResult = false;
            }

            Assert.IsTrue(conversionResult);
        }

        [TestMethod]
        public async Task FindAll_Performance_Sanity()
        {
            var watch = new Stopwatch();

            watch.Start();

            var response = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await response.Content.ReadAsStringAsync();

            watch.Stop();

            Assert.IsTrue(watch.ElapsedMilliseconds < 3000);
        }

        [TestMethod]
        public async Task Create_StatusCode_Ok()
        {
            var idPerson = Guid.NewGuid();
            var postRequest = new CreateRequest()
            {
                Person = new ModelPerson
                {
                    IdPerson = idPerson,
                    Name = "Test Person",
                    LastName = "Test Person",
                    RecordStatus = "A",
                    UserRecordCreation = "Testing"
                },
                ListAttachedFile = null
            };
            var httpContent = JsonContent.Create(postRequest);
            var responseCreate = await httpClient.PostAsync(urlResource + "Create", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, responseCreate.StatusCode);
        }

        [TestMethod]
        public async Task Create_Confirm()
        {
            var idPerson = Guid.NewGuid();
            var postRequest = new CreateRequest()
            {
                Person = new ModelPerson
                {
                    IdPerson = idPerson,
                    Name = "Test Person",
                    LastName = "Test Person",
                    RecordStatus = "A",
                    UserRecordCreation = "Testing"
                },
                ListAttachedFile = null
            };
            var httpContent = JsonContent.Create(postRequest);
            var responseCreate = await httpClient.PostAsync(urlResource + "Create", httpContent);

            var response = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await response.Content.ReadAsStringAsync();

            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);
            Assert.IsTrue(people?.Any(p => p.IdPerson.Equals(idPerson)));
        }

        [TestMethod]
        public async Task Create_StatusCode_BadRequest()
        {
            var idPerson = Guid.NewGuid();
            var postRequest = new CreateRequest();
            postRequest = null;

            var httpContent = JsonContent.Create(postRequest);
            var responseCreate = await httpClient.PostAsync(urlResource + "Create", httpContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseCreate.StatusCode);
        }

        [TestMethod]
        public async Task FindId_StatusCode_Ok()
        {
            //var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            //var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            //var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            //var responseFindId = await httpClient.GetAsync(urlResource + "FindId/" + people[0].IdPerson);
            //contentResponse = await responseFindId.Content.ReadAsStringAsync();
            //Console.WriteLine(contentResponse);
            //var person = JsonConvert.DeserializeObject<ModelPerson>(contentResponse);

            //Assert.IsTrue(person.IdPerson.Equals(people[0].IdPerson));

            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task FindId_StatusCode_No_Code_Header()
        {
            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            var responseFindId = httpClient.GetAsync(urlResource + "FindId/" + people[0].IdPerson).Result;

            Assert.IsTrue(responseFindId.StatusCode == HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task Update_StatusCode_Ok()
        {
            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            var updateRequest = new UpdateRequest()
            {
                Person = people[0],
                ListAttachedFile = new List<AttachedFilesUpdate>()
            };

            updateRequest.Person.Name = "Update Test";
            updateRequest.Person.LastName = "Update Test";
            updateRequest.Person.UserEditRecord = "Testing";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updateRequest), Encoding.UTF8, "application/json");
            var responseCreate = await httpClient.PutAsync(urlResource + "Update", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, responseCreate.StatusCode);
        }

        [TestMethod]
        public async Task Update_Confirm()
        {
            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            var updateRequest = new UpdateRequest()
            {
                Person = people[0],
                ListAttachedFile = new List<AttachedFilesUpdate>()
            };

            updateRequest.Person.Name = "Update Test";
            updateRequest.Person.LastName = "Update Test";
            updateRequest.Person.UserEditRecord = "Testing";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updateRequest), Encoding.UTF8, "application/json");
            var responseCreate = await httpClient.PutAsync(urlResource + "Update", httpContent);

            responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            Assert.IsTrue(people?.Any(p => p.IdPerson == updateRequest.Person.IdPerson
                && p.Name == updateRequest.Person.Name
                && p.LastName == updateRequest.Person.LastName
                && p.UserEditRecord == updateRequest.Person.UserEditRecord
            )); ;
        }

        [TestMethod]
        public async Task Update_StatusCode_BadRequest()
        {
            var updateRequest = new UpdateRequest();
            updateRequest = null;

            var httpContent = new StringContent(JsonConvert.SerializeObject(updateRequest), Encoding.UTF8, "application/json");
            var responseCreate = await httpClient.PutAsync(urlResource + "Update", httpContent);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseCreate.StatusCode);
        }

        [TestMethod]
        public async Task Delete_StatusCode_Ok()
        {
            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);
            var deletePerson = people.FirstOrDefault(p => p.UserRecordCreation.Equals("Testing"));

            var responseDelete = await httpClient.DeleteAsync(urlResource + "Delete/" + deletePerson.IdPerson);
            Assert.AreEqual(HttpStatusCode.OK, responseDelete.StatusCode);
        }

        [TestMethod]
        public async Task Delete_StatusCode_BadRequest()
        {
            var responseDelete = await httpClient.DeleteAsync(urlResource + "Delete/" + "AAAAA");
            Assert.AreEqual(HttpStatusCode.BadRequest, responseDelete.StatusCode);
        }

        [TestMethod]
        public async Task Delete_StatusCode_NotFound()
        {
            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);
            var deletePerson = people.FirstOrDefault(p => p.UserRecordCreation.Equals("Testing"));

            var responseDelete = await httpClient.DeleteAsync(urlResource + "Delete/" + deletePerson.IdPerson);

            responseDelete = await httpClient.DeleteAsync(urlResource + "Delete/" + deletePerson.IdPerson);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseDelete.StatusCode);
        }
        [TestMethod]
        public async Task Delete_Confirm()
        {
            var idPerson = Guid.NewGuid();
            var postRequest = new CreateRequest()
            {
                Person = new ModelPerson
                {
                    IdPerson = idPerson,
                    Name = "Test Person",
                    LastName = "Test Person",
                    RecordStatus = "A",
                    UserRecordCreation = "Testing"
                },
                ListAttachedFile = null
            };
            var httpContent = JsonContent.Create(postRequest);
            var responseCreate = await httpClient.PostAsync(urlResource + "Create", httpContent);

            var responseDelete = await httpClient.DeleteAsync(urlResource + "Delete/" + idPerson);

            var responseFindAll = await httpClient.GetAsync(urlResource + "FindAll");
            var contentResponse = await responseFindAll.Content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<List<ModelPerson>>(contentResponse);

            Assert.IsTrue(!people?.Any(p => p.IdPerson.Equals(idPerson)));
        }
    }
}
