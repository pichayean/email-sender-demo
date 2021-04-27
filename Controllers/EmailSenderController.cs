using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace email_sender_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailSenderController : ControllerBase
    {
        private readonly ILogger<EmailSenderController> _logger;
        private readonly IEmailSenderService _emailSenderService;

        public EmailSenderController(ILogger<EmailSenderController> logger, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _emailSenderService = emailSenderService;
        }

        [HttpGet]
        [Route("sendsampleemail")]
        public IActionResult Get(string fromEmail, string toEmail)
        {
            string titlewords = LoremNET.Lorem.Words(5, 10, false, true);
            string sentence = LoremNET.Lorem.Sentence(30, 100);

            _emailSenderService.Send(fromEmail, toEmail, titlewords, $@"<p> {sentence} </p>");

            return Ok(new
            {
                Status = "sended"
            });
        }
    }
}
