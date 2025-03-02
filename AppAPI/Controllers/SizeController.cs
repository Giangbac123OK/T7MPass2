using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _service;

        public SizeController(ISizeService service)
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
            var size = await _service.GetById(id);
            if (size == null)
                return NotFound("Size không tồn tại.");

            return Ok(size);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SizeDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật size thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SizeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm size thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingSizes = await _service.GetById(id);
            if (existingSizes == null)
                return NotFound("Size không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa size thành công." });
        }
    }
}
