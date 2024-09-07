using System.ComponentModel.DataAnnotations;

namespace Ultimate_POS_Api.DTOS
{
    public class Register
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string OperatorID { get; set; } = string.Empty;

        public string ReEnterPassword { get; set; } = string.Empty;
    }
}
