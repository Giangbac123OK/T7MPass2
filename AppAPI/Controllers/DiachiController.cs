using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiachiController : ControllerBase
    {
        private readonly IDiaChiService _service;

        public DiachiController(IDiaChiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diachis = await _service.GetAll();
            return Ok(diachis);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var diachi = await _service.GetById(id);
            if (diachi == null)
                return NotFound("Địa chỉ không tồn tại.");

            return Ok(diachi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DiachiDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật địa chỉ thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiachiDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm địa chỉ thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDiachi = await _service.GetById(id);
            if (existingDiachi == null)
                return NotFound("Đại chỉ không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa địa chỉ thành công." });
        }
    }
}
