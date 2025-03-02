using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DanhgiaController : ControllerBase
    {
        private readonly IDanhGiaService _service;

        public DanhgiaController(IDanhGiaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var danhgias = await _service.GetAll();
            return Ok(danhgias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var danhgias = await _service.GetById(id);
            if (danhgias == null)
                return NotFound("đánh giá không tồn tại.");

            return Ok(danhgias);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DanhgiaDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật đánh giá thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(DanhgiaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm đánh giá thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingDanhgia = await _service.GetById(id);
            if (existingDanhgia == null)
                return NotFound("Đánh giá không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa đánh giá thành công." });
        }
    }
}
