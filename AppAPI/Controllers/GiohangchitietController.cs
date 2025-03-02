using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiohangchitietController : ControllerBase
    {
        private readonly IGioHangChiTetService _service;

        public GiohangchitietController(IGioHangChiTetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var giohangchitiets = await _service.GetAll();
            return Ok(giohangchitiets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var giohangchitiet = await _service.GetById(id);
            if (giohangchitiet == null)
                return NotFound("Giỏ hàng chi tiết không tồn tại.");

            return Ok(giohangchitiet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GiohangchitietDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật giỏ hàng chi tiết thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiohangchitietDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm giỏ hàng chi tiết thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGiohangchitiet = await _service.GetById(id);
            if (existingGiohangchitiet == null)
                return NotFound("Giỏ hàng chi tiết không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa giỏ hàng chi tiết thành công." });
        }
    }
}
