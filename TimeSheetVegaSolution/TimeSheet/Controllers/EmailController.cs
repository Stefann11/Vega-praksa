using Contracts.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Email;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SendMailAsync23()
        {
            IFormFileCollection files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
            Message message = new Message(new string[] { "codemazetest@mailinator.com" }, 
                "Test mail with Attachments", "This is the content from our mail with attachments.", 
                files);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }
    }
}
