﻿using System;
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
using AppData.IRepository;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoadonsController : ControllerBase
    {
        private readonly IHoaDonService _Service;
        private readonly IHoaDonChiTietService _HDCTservice;
        private readonly AppDbContext _context;

        public HoadonsController(IHoaDonService service, AppDbContext context, IHoaDonChiTietService HDCTservice)
        {
            _Service = service;
            _context = context;
            _HDCTservice = HDCTservice;
        }
        [HttpPut("da-nhan-don-hang-{id}")]
        public async Task<IActionResult> Danhandonhang(int id)
        {
            try
            {
                await _Service.Danhandonhang(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // API để lấy tất cả hoá đơn
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hoadonList = await _Service.GetAllAsync();
            return Ok(hoadonList);
        }

        // API để lấy hoá đơn theo Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hoadon = await _Service.GetByIdAsync(id);
            if (hoadon == null) return NotFound(new { message = "Hoá đơn không tìm thấy" });
            return Ok(hoadon);
        }

        // API để thêm hoá đơn
        [HttpPost]
        public async Task<IActionResult> Add(HoadonDTO dto)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            try
            {
                if (dto.Idgg > 0)
                {
                    var giamgia = await _context.giamgias.FirstOrDefaultAsync(id => (int?)id.Id == dto.Idgg);
                    if (giamgia == null)
                    {
                        return NotFound(new { message = "Id giảm giá không tìm thấy" });
                    }
                    // Cập nhật số lượng
                    giamgia.Soluong -= 1;
                    _context.giamgias.Update(giamgia);

                    // Lưu thay đổi vào DB
                    await _context.SaveChangesAsync();
                }

                // Thêm hóa đơn
                await _Service.AddAsync(dto);

                // Trả về ID của hóa đơn mới được tạo
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi thêm hoá đơn
                return StatusCode(500, new { message = "Lỗi khi thêm hoá đơn", error = ex.Message });
            }
        }

        [HttpGet("voucher/{id}")]
        public async Task<IActionResult> Checkvoucher(int id)
        {
            // Lấy dữ liệu hóa đơn từ service
            var hoadon = await _Service.Checkvoucher(id);

            // Nếu không tìm thấy hóa đơn, trả về null
            if (hoadon == null)
                return Ok(null); // Trả về null nếu không có dữ liệu

            // Nếu có dữ liệu, trả về hóa đơn
            return Ok(hoadon);
        }


        // API để cập nhật hoá đơn
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HoadonDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                await _Service.UpdateAsync(dto, id);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật hoá đơn
                return StatusCode(500, new { message = "Lỗi khi cập nhật hoá đơn", error = ex.Message });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("trangthai/{id}")]
        public async Task<IActionResult> Updatetrangthai(int id, int trangthai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _context.hoadons.FirstOrDefaultAsync(kh => kh.Id == id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            if (existingHoadon.Idgg != null)
            {
                var voucher = await _context.giamgias.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idgg);
                if (voucher == null)
                {
                    return NotFound(new { message = "Voucher không tìm thấy" });
                }
                // Giảm số lượng mã giảm giá
                voucher.Soluong += 1;
                _context.giamgias.Update(voucher);
                await _context.SaveChangesAsync();
            }

            if (existingHoadon.Trangthai == 1 || existingHoadon.Trangthai == 0)
            {
                await _HDCTservice.ReturnProductAsync(id);
            }

            // Cập nhật điểm khách hàng nếu trạng thái là 3 và có sử dụng điểm
            if (trangthai == 3)
            {
                var khachhang = await _context.khachhangs.FirstOrDefaultAsync(kh => kh.Id == existingHoadon.Idkh);
                if (khachhang == null)
                {
                    return NotFound(new { message = "Khách hàng không tìm thấy" });
                }

                // Tính điểm từ hoá đơn và cập nhật điểm khách hàng
                int diemhoadon = (int)existingHoadon.Tongtiencantra / 100; // Quy đổi 100 VND = 1 điểm
                khachhang.Diemsudung += diemhoadon;
                khachhang.Tichdiem += diemhoadon;

                // Kiểm tra và cập nhật rank khách hàng
                var currentRank = await _context.ranks.FirstOrDefaultAsync(r => r.Id == khachhang.Idrank);
                if (currentRank != null && khachhang.Tichdiem > currentRank.MaxMoney)
                {
                    // Tìm rank tiếp theo phù hợp với điểm hiện tại
                    var nextRank = await _context.ranks
                        .Where(r => r.MinMoney <= khachhang.Tichdiem)
                        .OrderByDescending(r => r.MinMoney)
                        .FirstOrDefaultAsync();

                    if (nextRank != null && nextRank.Id != currentRank.Id)
                    {
                        khachhang.Idrank = nextRank.Id; // Cập nhật rank mới
                    }
                }

                _context.khachhangs.Update(khachhang);
                await _context.SaveChangesAsync();
            }

            try
            {
                existingHoadon.Trangthai = trangthai;

                _context.hoadons.Update(existingHoadon);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = existingHoadon.Id }, existingHoadon);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật hoá đơn
                return StatusCode(500, new { message = "Lỗi khi cập nhật hoá đơn", error = ex.Message });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("trangthaitrahang/{id}")]
        public async Task<IActionResult> Updatetrangthaitrahang(int id, int trangthai)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            var existingHoadon = await _context.hoadons.FirstOrDefaultAsync(kh => kh.Id == id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                existingHoadon.Trangthai = trangthai;

                _context.hoadons.Update(existingHoadon);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = existingHoadon.Id }, existingHoadon);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi cập nhật hoá đơn
                return StatusCode(500, new { message = "Lỗi khi cập nhật hoá đơn", error = ex.Message });
            }
        }

        // API để cập nhật hoá đơn
        [HttpPut("CheckTraHang/{id}")]
        public async Task<IActionResult> CheckTraHang(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về lỗi nếu DTO không hợp lệ
            }

            // B1: Lấy danh sách hóa đơn chi tiết theo `idhd`
            var hoadonchitietList = await _context.hoadonchitiets
                .Where(hdct => hdct.Idhd == id)
                .ToListAsync();

            if (hoadonchitietList == null || !hoadonchitietList.Any())
            {
                return NotFound(new { message = "Không tìm thấy danh sách hóa đơn chi tiết cho hóa đơn này" });
            }

            // B2: Kiểm tra từng hóa đơn chi tiết có tồn tại trong bảng trả hàng chi tiết
            var idsChuaTonTai = new List<int>();
            foreach (var hdct in hoadonchitietList)
            {
                var existsInTraHangChiTiet = await _context.trahangchitiets
                    .AnyAsync(thct => thct.Idhdct == hdct.Id);

                if (!existsInTraHangChiTiet)
                {
                    idsChuaTonTai.Add(hdct.Id); // Thêm idhdct chưa tồn tại vào danh sách
                }
            }

            // B3.1: Nếu tất cả hóa đơn chi tiết đều tồn tại trong bảng trả hàng chi tiết
            if (!idsChuaTonTai.Any())
            {
                return Ok(new { result = true, message = "Tất cả hóa đơn chi tiết đã tồn tại trong bảng trả hàng chi tiết" });
            }

            // B3.2: Nếu vẫn còn một số hóa đơn chi tiết chưa tồn tại
            return Ok(new
            {
                result = false,
                message = "Một số hóa đơn chi tiết chưa tồn tại trong bảng trả hàng chi tiết",
                missingIds = idsChuaTonTai
            });
        }


        // API để xóa hoá đơn
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingHoadon = await _Service.GetByIdAsync(id);
            if (existingHoadon == null)
            {
                return NotFound(new { message = "Hoá đơn không tìm thấy" });
            }

            try
            {
                await _Service.DeleteAsync(id);
                return NoContent(); // Trả về status code 204 nếu xóa thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi xóa hoá đơn
                return StatusCode(500, new { message = "Lỗi khi xóa hoá đơn", error = ex.Message });
            }
        }
        [HttpGet("hoa-don-theo-ma-kh-{id}")]
        public async Task<IActionResult> Hoadontheomakh(int id, [FromQuery] string? search)
        {
            try
            {
                return Ok(await _Service.TimhoadontheoIdKH(id, search));
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có khi xóa hoá đơn
                return StatusCode(500, new { message = "Lỗi khi xóa hoá đơn", error = ex.Message });
            }
        }
    }
}
