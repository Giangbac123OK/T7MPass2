using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HinhanhController : ControllerBase
    {
        private readonly IHinhAnhService _service;

        public HinhanhController(IHinhAnhService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sizes = await _service.GetAll();
            return Ok(sizes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hinhanh = await _service.GetById(id);
            if (hinhanh == null)
                return NotFound("Hình ảnh không tồn tại.");

            return Ok(hinhanh);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HinhanhDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật hình ảnh thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(HinhanhDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm hình ảnh thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHinhanh = await _service.GetById(id);
            if (existingHinhanh == null)
                return NotFound("Hình ảnh không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa hình ảnh thành công." });
        }
    }
}
