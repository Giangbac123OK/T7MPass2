using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiohangController : ControllerBase
    {
        private readonly IGioHangService _service;

        public GiohangController(IGioHangService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var giohangs = await _service.GetAll();
            return Ok(giohangs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var giohang = await _service.GetById(id);
            if (giohang == null)
                return NotFound("Giỏ hàng không tồn tại.");

            return Ok(giohang);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GiohangDTO dto)
        {
            dto.id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật giỏ hàng thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiohangDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm giỏ hàng thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGiohang = await _service.GetById(id);
            if (existingGiohang == null)
                return NotFound("Giỏ hàng không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa giỏ hàng thành công." });
        }
    }
}
