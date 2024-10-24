using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SoftPlex.Task.Core.Domain.Dto;

namespace SoftPlex.Api.Tests;

public class ProductsControllerTests : IntegrationTests
{
    [Test]
    public async System.Threading.Tasks.Task Products_PostAction_ShouldReturnOk()
    {
        //Arrange
        var request = new ProductPostDto("testName", "testDescription");
        
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        //Act
        var responseContent = await TestHttpClient.PostAsync($"/api/products", jsonContent);
        var response = await responseContent.Content.ReadFromJsonAsync<ProductDto>();

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.IsNotNull(response);
        Assert.That(request.Name, Is.EqualTo(response.Name));
        Assert.That(request.Description, Is.EqualTo(response.Description));
    }
    
    [Test]
    public async System.Threading.Tasks.Task Products_PutAction_ShouldReturnOk()
    {
        //Arrange
        var requestPost = new ProductPostDto("testName", "testDescription");
        using StringContent jsonContentPost = new(JsonSerializer.Serialize(requestPost), Encoding.UTF8, "application/json");
        var responseContentPost = await TestHttpClient.PostAsync($"/api/products", jsonContentPost);
        var responsePost = await responseContentPost.Content.ReadFromJsonAsync<ProductDto>();
        
        var request = new ProductDto(responsePost.Id, "testNewName", "testNewDescription");
        
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
       
        DataContext.ChangeTracker.Clear(); // так как MemoryDatabase Singleton чистим 
        
        //Act
        var responseContent = await TestHttpClient.PutAsync($"/api/products", jsonContent);
        var response = await responseContent.Content.ReadFromJsonAsync<ProductDto>();

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(responsePost.Id, Is.EqualTo(response.Id));
        Assert.IsNotNull(response);
        Assert.That(request.Name, Is.EqualTo(response.Name));
        Assert.That(request.Description, Is.EqualTo(response.Description));
    }
    
    [Test]
    public async System.Threading.Tasks.Task Products_PutAction_ShouldReturnNotFound()
    {
        //Arrange
        var request = new ProductDto(Guid.NewGuid(), "testNewName", "testNewDescription");
        using StringContent jsonContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        //Act
        var responseContent = await TestHttpClient.PutAsync($"/api/products", jsonContent);

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [Test]
    public async System.Threading.Tasks.Task Products_DeleteAction_ShouldReturnOk()
    {
        //Arrange
        var responseContentGet = await TestHttpClient.GetAsync($"/api/products");
        var responseGet = await responseContentGet.Content.ReadFromJsonAsync<ProductDto[]>();

        var old = responseGet.First();
        
        var request = new ProductDto(old.Id, "testNewName", "testNewDescription");

        //Act
        var responseContent = await TestHttpClient.DeleteAsync($"/api/products/{old.Id}");
        
        //Arrange
        responseContentGet = await TestHttpClient.GetAsync($"/api/products");
        responseGet = await responseContentGet.Content.ReadFromJsonAsync<ProductDto[]>();

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsFalse(responseGet.Any(x => x.Id == old.Id));
    }
    
    [Test]
    public async System.Threading.Tasks.Task Products_DeleteAction_ShouldReturnNotFound()
    {
        //Act
        var responseContent = await TestHttpClient.DeleteAsync($"/api/products/{Guid.NewGuid()}");
        
        //Arrange
        var responseContentGet = await TestHttpClient.GetAsync($"/api/products");

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
    
    [TestCase("a")]
    [TestCase("Sofa")]
    [TestCase("")]
    [TestCase(null)]
    public async System.Threading.Tasks.Task Products_SearchByNameAction_ShouldReturnAny(string search)
    {
        //Act
        var responseContent = await TestHttpClient.GetAsync($"/api/products?searchByName={search}");
        var response = await responseContent.Content.ReadFromJsonAsync<ProductDto[]>();

        //Assert
        Assert.That(responseContent.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.IsNotNull(response);
        Assert.IsTrue(response.Any());
    }
}