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
using AppData.IRepository;
using AppData.ViewModel;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrahangsController : ControllerBase
    {
        private readonly ITraHangService _ser;
        private readonly ITraHangChiTietService _chiTietService;
        private readonly IHoaDonChiTietService _hdctSer;
        private readonly ISanPhamChiTietService _spctSer;
        private readonly ISanPhamService _spSer;
        private readonly IKhachHangService _khSer;
        public TrahangsController(ITraHangService ser, ITraHangChiTietService chiTietService, IHoaDonChiTietService hdctSer, ISanPhamChiTietService spctSer, ISanPhamService spSer, IKhachHangService khSer)
        {
            _ser = ser;
            _chiTietService = chiTietService;
            _hdctSer = hdctSer;
            _spctSer = spctSer;
            _spSer = spSer;
            _khSer = khSer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _ser.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var a = await _ser.GetById(id);
                if (a == null) return BadRequest("Không tồn tại");
                return Ok(a);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(TrahangDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    await _ser.Add(dto);
                    return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TrahangDTO dto)
        {
            try
            {
                await _ser.Update(id, dto);
                return Ok("Sửa thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ser.DeleteById(id);
                return Ok("Xóa thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("HuyDon")]
        public async Task<IActionResult> Huydon(int id, int idnv, string? chuthich)
        {
            try
            {
                await _ser.Huydon(id,idnv,chuthich);
                return Ok("Sửa thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("Doidiem")]
        public async Task<IActionResult> DoiDiem(int id, int idnv, string? chuthich)
        {
            try
            {
                var th = await _ser.GetById(id);
                if(th == null)
                {
                    return BadRequest("Không tồn tại đơn hàng");
                }
                if (th.Trangthai != 0)
                {
                    return BadRequest("Đơn hàng hàng đã bị hủy hoặc đã xác nhận thành công!");
                }
                var kh = await _khSer.GetKhachhangByIdAsync(th.Idkh);
                if(kh == null)
                {
                    return BadRequest("Không tồn tại khách hàng");
                }
                kh.Diemsudung += Convert.ToInt32(Math.Round(th.Sotienhoan));
                await _khSer.UpdateKhachhangAsync(th.Idkh, kh);
                th.Idnv = idnv;
                await _ser.Update(id, th);
                return Ok("Đổi điểm thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateTrangThaiHd/{id}")]
        public async Task<IActionResult> UpdateTrangThaiHd(int id)
        {
            try
            {
                await _ser.UpdateTrangThaiHd(id);
                return Ok("Sửa thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
