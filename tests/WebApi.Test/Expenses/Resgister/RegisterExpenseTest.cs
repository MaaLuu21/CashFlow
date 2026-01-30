using CommonTestUtilities.requests;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Resgister;
public class RegisterExpenseTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "api/Expenses";

    private readonly HttpClient _httpClient;
    private readonly string _token;

    public RegisterExpenseTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();

        _token = webApplicationFactory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestResgisterExpenseJsonBuilder.Build();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string cultureInfo)
    {
        var request = RequestResgisterExpenseJsonBuilder.Build();
        request.Title = string.Empty; 

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(cultureInfo));

        var result = await _httpClient.PostAsJsonAsync(METHOD, request);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

    }
}
