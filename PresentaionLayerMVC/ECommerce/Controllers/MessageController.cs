using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using DomainLayerCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ModelClasses;
using System.Security.Claims;

namespace PresentaionLayer.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly string Admin = "b696b434-b763-431f-82d2-3c5b97450dba";
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IHubContext<ChatHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _hubContext = hubContext;
        }

        public IActionResult AllMsg()
        {
            return PartialView("Message");
        }
        
        public IActionResult Index(string? id)
        {
            var headers = _unitOfWork.MessageHeaders.GetAll();
            ViewBag.headers=headers;
            var header = headers.FirstOrDefault(m => m.User1_Id == id);
            if (header == null)
            {
                return View(new List<Message>());
            }
            var msgs = _unitOfWork.Messages.GetAll().Where(u => u.HeaderId == header.Id ).ToList();
            return View(msgs);
        }
        public IActionResult LoadMessages(string? id)
        {
            var headers = _unitOfWork.MessageHeaders.GetAll();
            var header = headers.FirstOrDefault(m => m.User1_Id == id);
            if (header == null)
            {
                return PartialView("MessagesPartial",new List<Message>());
            }
            var msgs = _unitOfWork.Messages.GetAll().Where(u => u.HeaderId == header.Id).ToList();
            return PartialView("MessagesPartial", msgs);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var userId = messageDto.FromUserId == Admin ? messageDto.ToUserId : messageDto.FromUserId;
            var isFirst = _unitOfWork.MessageHeaders.GetAll().Any(m => (m.User1_Id == userId && m.User2_Id == Admin));

            if (!isFirst)
            {
                var msgheader = new MessageHeader
                {
                    User1_Id = userId,
                    User2_Id = Admin,
                    CustomerName = User.FindFirstValue(ClaimTypes.Name)
                };
                _unitOfWork.MessageHeaders.Add(msgheader);
                _unitOfWork.ApplyChanges();
            }
            
            var messageHeader = _unitOfWork.MessageHeaders.GetAll().FirstOrDefault(m=>m.User1_Id==userId);
            var message = new Message
            {
                HeaderId = messageHeader.Id,
                Msg = messageDto.Msg,
                CreatedAt = DateTime.Now,
                IsRead = false
                
            };
            message.AdminMsg=messageDto.FromUserId==Admin;
            _unitOfWork.Messages.Add(message);
            _unitOfWork.ApplyChanges();

            // Use SignalR to send the message in real-time
            if (messageDto.ToUserId == Admin)
            {
                await _hubContext.Clients.User(Admin).SendAsync("sendAdminMsg", message.Msg , messageHeader.User1_Id , messageHeader.CustomerName);
            }
            else
            {
                await _hubContext.Clients.User(messageDto.ToUserId).SendAsync("sendUserMsg", message.Msg);
            }

            return Ok();
        }
    }
}
