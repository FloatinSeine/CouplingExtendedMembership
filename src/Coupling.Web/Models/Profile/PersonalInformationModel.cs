using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Coupling.Web.Models.Profile
{
    public class PersonalInformationModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        
        //[Required]
        [Display(Name = "BirthDay")]
        public int BirthDay { get; set; }

        //[Required]
        [Display(Name = "BirthMonth")]
        public string BirthMonth { get; set; }

        //[Required]
        [Display(Name = "BirthYear")]
        public int BirthYear { get; set; }
        
        //[Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        
        //[Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 9)]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

    }
}