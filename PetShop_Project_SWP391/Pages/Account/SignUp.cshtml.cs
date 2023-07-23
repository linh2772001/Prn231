﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
/*using PetShop_Project_SWP391.Models;*/
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BusinessObject.Models;

namespace PetShop_Project_SWP391.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private readonly ProjectContext projectContext;

        public SignUpModel(ProjectContext projectContext)
        {
            this.projectContext = projectContext;
        }

        [BindProperty]
        public BusinessObject.Models.Customer Customer { get; set; }
        [BindProperty]
        public BusinessObject.Models.Account Account { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(Account.Email) ||
                string.IsNullOrEmpty(Customer.Name) ||
                string.IsNullOrEmpty(Customer.Address) ||
                string.IsNullOrEmpty(Customer.District) ||
                string.IsNullOrEmpty(Customer.Province) ||
                string.IsNullOrEmpty(Customer.Wards) ||
                string.IsNullOrEmpty(Customer.Phone))
            {
                ViewData["msgEmpty"] = "Vui lòng nhập cả tất cả thông tin bên trên";
                return Page();
            }

            if (!Regex.IsMatch(Customer.Phone, @"^0\d{9}$"))
            {
                ViewData["msgPhone"] = "Số điện thoại không hợp lệ. Vui lòng nhập số bắt đầu từ 0 và có 10 chữ số!";
                return Page();
            }

            bool checkResetPass = false;
            string newPassword = GeneratePassword(10);
                var acc = await projectContext.Accounts.FirstOrDefaultAsync(a => a.Email == Account.Email);
                if (acc == null)
                {
                    try
                    {
                        // Gửi email chứa mật khẩu mới
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("petshopswp391@gmail.com");
                            mail.To.Add(Account.Email);
                            mail.Subject = "Password";
                            mail.Body = $"Hi {Account.Email},\n\nYour password is: {newPassword} " +
                                $"\n\nThank you.";

                            using (SmtpClient smtpClient = new SmtpClient())
                            {
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.Credentials = new NetworkCredential("petshopswp391@gmail.com", "nvgezfftwyhtnrcc");
                                smtpClient.EnableSsl = true;
                                smtpClient.Port = 587;
                                smtpClient.Host = "smtp.gmail.com";
                                smtpClient.Send(mail);
                            }
                        }
                        checkResetPass = true;
                    }
                    catch
                    {
                        ViewData["error"] = "Gửi mail lỗi! Vui lòng kiểm tra kết nối hoặc địa chỉ email";
                        checkResetPass = false;
                    }

                    if (checkResetPass)
                    {
                        var cus = new Customer()
                        {
                            CustomerId = GenerateCusID(10),
                            Name = Customer.Name,
                            Address = Customer.Address,
                            District = Customer.District,
                            Province = Customer.Province,
                            Wards = Customer.Wards,
                            Phone = Customer.Phone,
                            CreateDate = DateTime.Now,
                        };

                        var newAcc = new BusinessObject.Models.Account()
                        {
                            Email = Account.Email,
                            Password = newPassword,
                            CustomerId = cus.CustomerId,
                            Role = 2,
                            IsActive = true,
                        };

                        await projectContext.Customers.AddAsync(cus);
                        await projectContext.Accounts.AddAsync(newAcc);
                        await projectContext.SaveChangesAsync();
                        ViewData["msgSuccess"] = "Đăng ký tài khoản thành công.";
                        ViewData["message"] = "Vui lòng vào email để nhận mật khẩu. Xác nhận mật khẩu trên Email trong Hộp thư đến (Inbox) hoặc Thư rác (Spam)";
                        return Page();
                    }
                }
                else
                {
                    ViewData["msgEmailSame"] = "Tài khoản đã tồn tại!";
                    return Page();
                }

            return Page();
        }

        public string GenerateCusID(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder sb = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public string GeneratePassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            StringBuilder sb = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
