using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using taller_test;

namespace TestProject;

public class ProductApiTest: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ProductApiTest(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetProductsById_ShouldReturnProduct()
    {
        // Arrange
        var idToFind = 1;
        // Act
        var response = await _httpClient.GetAsync($"/api/products/{idToFind}");
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        var product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.Equal(idToFind, product?.Id);
    }
}