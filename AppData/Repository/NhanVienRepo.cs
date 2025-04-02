using AppData.IRepository;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace AppData.Repository
{
    public class NhanVienRepo : INhanVienRepo
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NhanVienRepo(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IEnumerable<Nhanvien>> GetAllAsync()
        {
            return await _context.Set<Nhanvien>().ToListAsync();
        }

        public async Task<Nhanvien> GetByIdAsync(int id)
        {
            return await _context.Set<Nhanvien>().FindAsync(id);
        }

        public async Task AddAsync(Nhanvien kh)
        {
            await _context.nhanviens.AddAsync(kh);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Nhanvien nhanvien)
        {
            _context.Set<Nhanvien>().Update(nhanvien);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var nhanvien = await GetByIdAsync(id);
            if (nhanvien != null)
            {
                _context.Set<Nhanvien>().Remove(nhanvien);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Không tìm thấy nhân viên");
            }
        }

        public async Task<IEnumerable<Nhanvien>> TimKiemNhanvienAsync(string search)
        {
            if (search == null)
            {
                return await _context.nhanviens.ToListAsync();
            }
            else
            {
                search = search.ToLower();
                return await _context.nhanviens.Where(x => x.Hovaten.StartsWith(search) || x.Sdt.StartsWith(search) || x.Diachi.StartsWith(search)).ToListAsync();
            }
        }
        public async Task SendAccountCreationEmail(string toEmail, string hoten, string password, int role)
        {
            string SenderEmail = "gianghtph44220@fpt.edu.vn";
            string SenderPassword = "zuvk nurf kcka ufgc"; // Nên sử dụng biến môi trường hoặc cách bảo mật khác cho mật khẩu
            string SmtpServer = "smtp.gmail.com";
            int SmtpPort = 587;

            try
            {
                // Kiểm tra định dạng email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                var checkRegex = Regex.IsMatch(toEmail, pattern);
                if (!checkRegex)
                {
                    throw new ArgumentException("Email không hợp lệ");
                }
                string chucvu;
                if (role == 0)
                {
                    chucvu = "Admin";
                }
                else if (role == 1)
                {
                    chucvu = "quản lý";
                }
                else
                {
                    chucvu = "nhân viên";
                }
                // Tạo email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("T7M Sneaker", SenderEmail));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = "Tài khoản của bạn đã được tạo";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                <h2>Xin chào {hoten},</h2>
                <p>Admin đã tạo tài khoản cho bạn.</p>
                <p><strong>Tên đăng nhập:</strong> {toEmail}</p>
                <p><strong>Mật khẩu:</strong> {password}</p>
                <p>Với chức vụ là :</strong> {chucvu}</p></p>
                <p>Vui lòng đăng nhập và đổi mật khẩu sớm nhất có thể.</p>
                <br/>
                <p>Trân trọng,</p>
                <p>Đội ngũ quản trị</p>
                "
                };

                // Gửi email
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(SmtpServer, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(SenderEmail, SenderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                Console.WriteLine("Email đã được gửi thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
