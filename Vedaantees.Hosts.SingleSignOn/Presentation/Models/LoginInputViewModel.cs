using System.ComponentModel.DataAnnotations;

namespace Vedaantees.Hosts.SingleSignOn.Presentation.Models
{
    public class LoginInputViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool   RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}