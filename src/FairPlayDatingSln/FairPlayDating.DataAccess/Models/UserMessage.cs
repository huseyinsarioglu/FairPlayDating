﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace FairPlayDating.DataAccess.Models
{
    public partial class UserMessage
    {
        [Key]
        public long UserMessageId { get; set; }
        public long FromApplicationUserId { get; set; }
        public long ToApplicationUserId { get; set; }
        [Required]
        public string Message { get; set; }

        [ForeignKey(nameof(FromApplicationUserId))]
        [InverseProperty(nameof(ApplicationUser.UserMessageFromApplicationUser))]
        public virtual ApplicationUser FromApplicationUser { get; set; }
        [ForeignKey(nameof(ToApplicationUserId))]
        [InverseProperty(nameof(ApplicationUser.UserMessageToApplicationUser))]
        public virtual ApplicationUser ToApplicationUser { get; set; }
    }
}