using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HinhanhController : ControllerBase
    {
        private readonly IHinhAnhService _Service;

        public HinhanhController(IHinhAnhService service)
        {
            _Service = service;
        }

        // API để lấy tất cả Hình ảnh trả hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hoadonList = await _Service.GetAllAsync();
            return Ok(hoadonList);
        }

        // API để lấy Hình ảnh trả hàng theo Id
        [HttpGet("TraHang/{id}")]
        public async Task<IActionResult> GetByIdTraHang(int id)
        {
            var hoadon = await _Service.GetByIdTraHangAsync(id);
            if (hoadon == null) return NotFound(new { message = "Hình ảnh trả hàng không tìm thấy" });
            return Ok(hoadon);
        }

        [HttpGet("DanhGia/{id}")]
        public async Task<IActionResult> GetByIdDanhGia(int id)
        {
            var hoadon = await _Service.GetByIdTraHangAsync(id);
            if (hoadon == null) return NotFound(new { message = "Hình ảnh đánh giá không tìm thấy" });
            return Ok(hoadon);
        }

        [HttpGet("DanhGia/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadon = await _Service.GetByIdAsync(id);
            if (hoadon == null) return NotFound(new { message = "Hình ảnh tìm theo mã không tìm thấy" });
            return Ok(hoadon);
        }
        [HttpPost]
        public async Task<IActionResult> Add(HinhanhDTO dto)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }
            try
            {
                // Thêm hình ảnh (hoặc Hình ảnh trả hàng tùy theo context)
                await _Service.AddAsync(dto);

                // Trả về ID của hình ảnh vừa được thêm
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi thêm hình ảnh
                return StatusCode(500, new { message = "Lỗi khi thêm hình ảnh", error = ex.Message });
            }
        }


        // API để cập nhật Hình ảnh trả hàng
        [HttpPut("/{id}")]
        public async Task<IActionResult> Update(int id, HinhanhDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hình ảnh trả hàng không tìm thấy" });
            }

            try
            {
                await _Service.UpdateAsync(dto, id);
                return NoContent(); // Trả về status code 204 nếu cập nhật thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật Hình ảnh trả hàng
                return StatusCode(500, new { message = "Lỗi khi cập nhật Hình ảnh trả hàng", error = ex.Message });
            }
        }

        // API để xóa Hình ảnh trả hàng
        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hình ảnh trả hàng không tìm thấy" });
            }

            try
            {
                await _Service.DeleteAsync(id);
                return NoContent(); // Trả về status code 204 nếu xóa thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi xóa Hình ảnh trả hàng
                return StatusCode(500, new { message = "Lỗi khi xóa Hình ảnh trả hàng", error = ex.Message });
            }
        }
    }
}
