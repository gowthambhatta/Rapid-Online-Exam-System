using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace onllineexam.Models
{
    public enum useType
    {
        Student, Teacher
    }
    public class lOGIN1
    {
        [Key]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string email { get; set; }
        [Required]
        [Display(Name = "User Type")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string pass { get; set; }
     [NotMapped]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("pass", ErrorMessage = "Password is invalid")]
        public string confirmpass { get; set; }
    }
}