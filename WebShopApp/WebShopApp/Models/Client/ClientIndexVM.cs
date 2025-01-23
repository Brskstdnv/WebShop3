﻿using System.ComponentModel.DataAnnotations;

namespace WebShopApp.Models.Client
{
    public class ClientIndexVM
    {
        public string Id { get; set; } = null!;

        [Display(Name = "UserName")]
        public string UserName { get; set; } = null!;

        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        public string Adress { get; set; } = null!;

        public string Email {  get; set; } = null!; 

        public bool IsAdmin { get; set; }


    }
}
