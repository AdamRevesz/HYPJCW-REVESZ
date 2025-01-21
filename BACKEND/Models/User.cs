using Microsoft.AspNetCore.Identity;
using Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : IdentityUser,IIdEntity
    {
        [EmailAddress]
        public string EmailAddress { get; set; } = "";
        public string Password { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsProfessional { get; set; }
        public bool IsAdult => (DateTime.Now - BirthDate.ToDateTime(TimeOnly.MinValue)).TotalDays / 365.25 >= 18;
        public DateOnly AccountCreated { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public virtual List<Content> Contents { get; set; } = new List<Content>();
        public virtual List<Comments> Comments { get; set; } = new List<Comments>();

        public User(string username, string emailAddress, string password, string firstName, string lastName) : base(username)
        {
            EmailAddress = emailAddress;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public User() {}
        public User(string userName) : base(userName)
        {
        }
    }
}
