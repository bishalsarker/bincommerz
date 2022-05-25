using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Auth
{
    [Table("users", Schema = "bcomm_user")]
    public class User : WithHashId
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }
    }

    public enum SubscriptionPlans
    {
        Free,
        Basic,
        Enterprise
    }
}
