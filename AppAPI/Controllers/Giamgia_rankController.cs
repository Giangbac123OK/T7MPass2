using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Giamgia_rankController : ControllerBase
    {
        private readonly IGiamGia_RankService _service;

        public Giamgia_rankController(IGiamGia_RankService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var giamgia_Ranks = await _service.GetAll();
            return Ok(giamgia_Ranks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var giamgia_Rank = await _service.GetById(id);
            if (giamgia_Rank == null)
                return NotFound("Giảm giá_Rank  không tồn tại.");

            return Ok(giamgia_Rank);
        }

        [HttpPost]
        public async Task<IActionResult> Create(giamgia_rankDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm giảm giá_rank thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existinggiamgia_rank = await _service.GetById(id);
            if (existinggiamgia_rank == null)
                return NotFound("Giảm giá_Rank không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa giảm giá_rank thành công." });
        }
    }
}
