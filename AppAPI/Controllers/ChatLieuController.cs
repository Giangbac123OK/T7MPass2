using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatLieuController : ControllerBase
    {
        private readonly IChatLieuService _service;

        public ChatLieuController(IChatLieuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var chatLieus = await _service.GetAll();
            return Ok(chatLieus);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chatLieu = await _service.GetById(id);
            if (chatLieu == null)
                return NotFound("Chất liệu không tồn tại.");

            return Ok(chatLieu);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ChatLieuDTO dto)
        {
            dto.Id = id;

            try
            {
                await _service.Update(dto);
                return Ok(new { message = "Cập nhật chất liệu thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatLieuDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.Create(dto);
            return Ok(new { message = "Thêm chất liệu thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingChatlieu = await _service.GetById(id);
            if (existingChatlieu == null)
                return NotFound("Chất liệu không tồn tại.");

            await _service.Delete(id);
            return Ok(new { message = "Xóa chất liệu thành công." });
        }
    }
}
