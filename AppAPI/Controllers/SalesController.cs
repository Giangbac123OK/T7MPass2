using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Models;
using AppData.DTO;
using AppData.IService;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _KhachHang_service;

        public SalesController(ISaleService service)
        {
            _KhachHang_service = service;
        }
        /*	[HttpGet]
			public async Task<IActionResult> GetAll()
			{
				var sales = await _KhachHang_service.GetAllAsync();
				return Ok(sales.Select(s => new
				{

					s.Ten,
					s.Mota,
					Trangthai = s.Trangthai switch
					{
						0 => "Đang diễn ra",
						1 => "Chuẩn bị diễn ra",
						2 => "Đã diễn ra",
						3 => "Dừng phát hành",
						_ => "Không xác định"
					},
					s.Ngaybatdau,
					s.Ngayketthuc
				}));
			}*/
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _KhachHang_service.GetAllWithIdAsync();
            return Ok(sales.Select(s => new
            {
                s.Id, // Thêm Id vào kết quả trả về
                s.Ten,
                s.Mota,
                Trangthai = s.Trangthai switch
                {
                    0 => "Đang diễn ra",
                    1 => "Chuẩn bị diễn ra",
                    2 => "Đã diễn ra",
                    3 => "Dừng phát hành",
                    _ => "Không xác định"
                },
                s.Ngaybatdau,
                s.Ngayketthuc
            }));
        }


        [HttpGet("_KhachHang/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _KhachHang_service.GetByIdAsync(id);
            if (sale == null) return NotFound();

            return Ok(new
            {
                sale.Ten,
                sale.Mota,
                Trangthai = sale.Trangthai switch
                {
                    0 => "Đang diễn ra",
                    1 => "Chuẩn bị diễn ra",
                    2 => "Đã diễn ra",
                    3 => "Dừng phát hành",
                    _ => "Không xác định"
                },
                sale.Ngaybatdau,
                sale.Ngayketthuc
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SaleDTO saleDto)
        {
            /*await _KhachHang_service.AddAsync(saleDto);
			//var x = a _KhachHang_service.AddAsync(saleDto);
			return CreatedAtAction(nameof(GetById), new { id = saleDto.Ten }, saleDto);*/
            try
            {


                await _KhachHang_service.AddAsync(saleDto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                // Chỉ trả ra thông báo lỗi đơn giản
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung (nếu cần), có thể ghi log hoặc xử lý khác
                return StatusCode(500, "Đã xảy ra lỗi, vui lòng thử lại sau.");
            }
        }

        [HttpPut("_KhachHang/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleDTO saleDto)
        {
            await _KhachHang_service.UpdateAsync(id, saleDto);
            return NoContent();
        }

        [HttpPut("_KhachHang/{id}/cancel")]
        public async Task<IActionResult> UpdateStatusToCancelled(int id)
        {
            try
            {
                await _KhachHang_service.UpdateStatusToCancelled(id);
                return NoContent(); // Thành công mà không cần trả về nội dung
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("_KhachHang/{id}/update-status")]
        public async Task<IActionResult> UpdateStatusBasedOnDates(int id)
        {
            try
            {
                await _KhachHang_service.UpdateStatusBasedOnDates(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("_KhachHang/{id}/update-status-load")]
        public async Task<IActionResult> UpdateStatusload(int id)
        {
            try
            {
                await _KhachHang_service.UpdateStatusLoad(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("_KhachHang/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _KhachHang_service.DeleteAsync(id);
            return NoContent();
        }
    }
}
