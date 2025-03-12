using AppData.DTO;
using AppData.IService;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiamgiaController : ControllerBase
    {
        private readonly IGiamGiaService _Service;
        public GiamgiaController(IGiamGiaService service)
        {
            _Service = service;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Service.GetAllAsync();
            return Ok(result.Select(gg => new
            {
                gg.Id,
                gg.Mota,
                Donvi = gg.Donvi == 0 ? "VND" : "%",
                gg.Giatri,
                gg.Ngaybatdau,
                gg.Ngayketthuc,
                gg.Soluong,
                Trangthai = gg.Trangthai switch
                {
                    0 => "Đang phát hành",
                    1 => "Chuẩn bị phát hành",
                    2 => "Dừng phát hành",
                    _ => "Không xác định"
                }
            }));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var giamgia = await _Service.GetByIdAsync(id);
                return Ok(new
                {
                    giamgia.Id,
                    giamgia.Mota,
                    Donvi = giamgia.Donvi == 0 ? "VND" : "%",
                    giamgia.Giatri,
                    giamgia.Ngaybatdau,
                    giamgia.Ngayketthuc,
                    giamgia.Soluong,
                    Trangthai = giamgia.Trangthai switch
                    {
                        0 => "Đang phát hành",
                        1 => "Chuẩn bị phát hành",
                        2 => "Dừng phát hành",
                        _ => "Không xác định"
                    }
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Không tìm thấy mã giảm giá.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiamgiaDTO dto)
        {
            await _Service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Mota }, dto);
        }
        [HttpPost("_KhachHang/AddRankToGiamgia")]
        public async Task<IActionResult> AddRankToGiamgia([FromBody] GiamgiaDTO dto)
        {
            try
            {
                await _Service.AddRankToGiamgia(dto);
                return Ok("Rank added to Giảm Giá thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GiamgiaDTO dto)
        {
            await _Service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Service.DeleteAsync(id);
            return NoContent();
        }
    }
}
