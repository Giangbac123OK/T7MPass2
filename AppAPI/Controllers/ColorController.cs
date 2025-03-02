using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorController(IColorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var colors = await _service.GetAll();
            return Ok(colors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var color = await _service.GetById(id);
            if (color == null)
                return NotFound("Màu không tồn tại.");

            return Ok(color);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ColorDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật màu thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm màu thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingColor = await _service.GetById(id);
            if (existingColor == null)
                return NotFound("Màu không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa màu thành công." });
        }
    }
}
