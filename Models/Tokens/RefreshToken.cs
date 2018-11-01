using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EACA_API.Models.Account;

namespace EACA_API.Models.AccountEntities.Tokens
{
    [Table("AspNetRefreshTokens")]
    public class RefreshToken
    {
        [Key]
        [StringLength(450)]
        public string Id { get; set; }

        [Required]
        public DateTime IssuedUtc { get; set; }

        [Required]
        public DateTime ExpiresUtc { get; set; }

        [Required]
        [StringLength(450)]
        public string Token { get; set; }

        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApiUser User { get; set; }
    }
}
