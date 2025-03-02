using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiamgiaController : ControllerBase
    {
        private readonly IGiamGiaService _service;

        public GiamgiaController(IGiamGiaService service)
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
            var giamgia = await _service.GetById(id);
            if (giamgia == null)
                return NotFound("giảm giá không tồn tại.");

            return Ok(giamgia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GiamgiaDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật giảm giá thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiamgiaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm giảm giá thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGiamgia = await _service.GetById(id);
            if (existingGiamgia == null)
                return NotFound("Giảm giá không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa giảm giá thành công." });
        }
    }
}
