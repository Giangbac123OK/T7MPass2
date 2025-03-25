using AppData.DTO;
using AppData.IRepository;
using AppData.IService;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhgiaController : ControllerBase
    {
        private readonly IDanhGiaService _services;
        private readonly IHinhAnhService _hinhAnhService;

        public DanhgiaController(IDanhGiaService services, IHinhAnhService hinhAnhService)
        {
            _services = services;
            _hinhAnhService = hinhAnhService;
        }





        // GET: api/Danhgias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhgiaDTO>>> Getdanhgias()
        {
            try
            {
                if (await _services.GetAll() == null)
                {
                    return NotFound();
                }
                return await _services.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Danhgias/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<DanhgiaDTO>> GetDanhgia(int id)
        {
            try
            {

                if (await _services.GetAll() == null)
                {
                    return NotFound();
                }
                var danhgia = await _services.GetById(id);

                if (danhgia == null)
                {
                    return NotFound();
                }

                return danhgia;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/byIDhdct/{id}")]
        public async Task<ActionResult<DanhgiaDTO>> GetDanhgiaByidHDCT(int id)
        {
            try
            {

                if (await _services.GetAll() == null)
                {
                    return NotFound();
                }
                var danhgia = await _services.getByidHDCT(id);

                if (danhgia == null)
                {
                    return NotFound();
                }

                return danhgia;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Danhgias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/{id}")]
        public async Task<IActionResult> PutDanhgia(int id, DanhgiaDTO danhgia)
        {
            if (id != danhgia.Id)
            {
                return BadRequest("ID trong URL không khớp với ID trong dữ liệu.");
            }

            try
            {


                await _services.Update(id, danhgia);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }


        // POST: api/Danhgias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhgiaDTO>> PostDanhgia([FromForm] DanhgiaDTO danhgia, [FromForm] List<IFormFile> files)
        {
            try
            {
                // Lưu đánh giá vào cơ sở dữ liệu
                await _services.Create(danhgia);

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        // Tạo đường dẫn lưu file
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "picture");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);

                        // Lưu file vào thư mục
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Tạo đối tượng HinhanhDTO
                        var hinhanh = new HinhanhDTO
                        {
                            Idtrahang = 0,
                            Iddanhgia = danhgia.Id,
                            Urlhinhanh = fileName
                        };

                        // Lưu thông tin hình ảnh vào cơ sở dữ liệu
                        await _hinhAnhService.AddAsync(hinhanh);
                    }
                }

                // Trả về kết quả sau khi tạo mới
                return CreatedAtAction("GetDanhgia", new { id = danhgia.Id }, danhgia);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }





        // DELETE: api/Danhgias/5
        [HttpDelete("_KhachHang/{id}")]
        public async Task<IActionResult> DeleteDanhgia(int id)
        {
            if (await _services.GetAll() == null)
            {
                return NotFound();
            }

            try
            {
                await _services.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetByIdSP/{id}")]
        public async Task<IActionResult> GetByIdSP(int id)

        {
            // Kiểm tra ID
            if (id <= 0)
            {
                return BadRequest(new { message = "ID sản phẩm chi tiết không hợp lệ." });
            }

            // Gọi dịch vụ để lấy dữ liệu
            var result = await _services.GetByidSP(id);

            // Kiểm tra kết quả trả về
            if (result == null || !result.Any())
            {
                return Ok(); // Trả về null trong response
            }

            return Ok(result); // Trả về kết quả nếu tìm thấy
        }
    }
}
