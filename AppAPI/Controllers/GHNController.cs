using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/ghn")]
[ApiController]
public class GHNController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public GHNController(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
    }

    // Lấy Token & ShopID từ cấu hình
    private string Token => _config["GHNConfig:Token"];
    private string ShopId => _config["GHNConfig:ShopId"];

    // API lấy danh sách tỉnh/thành phố
    [HttpGet("provinces")]
    public async Task<IActionResult> GetProvinces()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://online-gateway.ghn.vn/shiip/public-api/master-data/province");
        request.Headers.Add("Token", Token);

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        return Content(result, "application/json");
    }

    // API lấy danh sách quận/huyện theo tỉnh
    [HttpGet("districts")]
    public async Task<IActionResult> GetDistricts([FromQuery] int province_id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://online-gateway.ghn.vn/shiip/public-api/master-data/district?province_id={province_id}");
        request.Headers.Add("Token", Token);

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        return Content(result, "application/json");
    }

    // API lấy danh sách phường/xã theo quận/huyện
    [HttpGet("wards")]
    public async Task<IActionResult> GetWards([FromQuery] int district_id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://online-gateway.ghn.vn/shiip/public-api/master-data/ward?district_id={district_id}");
        request.Headers.Add("Token", Token);

        var response = await _httpClient.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();

        return Content(result, "application/json");
    }

    // API tính ngày trả hàng dự kiến
    [HttpPost("calculate-delivery")]
    public async Task<IActionResult> CalculateDelivery([FromBody] DeliveryRequest request)
    {
        var url = "https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/leadtime";

        var requestData = new
        {
            from_district_id = 1444,  // ID quận nơi gửi hàng
            from_ward_code = "20308", // Mã phường nơi gửi hàng
            to_district_id = request.ToDistrictId,
            to_ward_code = request.ToWardCode,
            service_id = 53320
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
        jsonContent.Headers.Add("Token", Token);
        jsonContent.Headers.Add("ShopId", ShopId);

        var response = await _httpClient.PostAsync(url, jsonContent);
        var result = await response.Content.ReadAsStringAsync();

        return Content(result, "application/json");
    }
}

// Model cho yêu cầu tính ngày giao hàng
public class DeliveryRequest
{
    public int ToDistrictId { get; set; }
    public string ToWardCode { get; set; }
}
