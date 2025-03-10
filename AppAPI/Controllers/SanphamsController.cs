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
    public class SanphamsController : ControllerBase
    {
        private readonly ISanPhamService _services;

        public SanphamsController(ISanPhamService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sanphams = await _services.GetAll();
            return Ok(sanphams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sanpham = await _services.GetById(id);
            if (sanpham == null)
                return NotFound("Sản phẩm không tồn tại.");

            return Ok(sanpham);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SanphamDTO dto)
        {
            dto.Id = id;

            try
            {
                await _services.Update(dto);
                return Ok(new { message = "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("sp-noi-bat")]
        public async Task<IActionResult> SpNoiBat()
        {
            try
            {
                return Ok(await _services.SpNoiBat());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("sp-moi-nhat")]
        public async Task<IActionResult> SpMoiNhat()
        {
            try
            {
                return Ok(await _services.SpMoiNhat());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(SanphamDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _services.Create(dto);
            return Ok(new { message = "Thêm thành công.", data = dto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sanpham = await _services.GetById(id);
            if (sanpham == null)
                return NotFound(" không tồn tại.");

            await _services.Delete(id);
            return Ok(new { message = "Xóa  thành công." });
        }
    }
}
