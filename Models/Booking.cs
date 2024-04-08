using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AppointmentForm.Models
{
    public class Booking
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only characters")]
        [MaxLength(20,ErrorMessage ="Max lenght must be 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please enter only characters")]
        [MaxLength(20, ErrorMessage = "Max lenght must be 20 characters")]
        public string LastName { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+",ErrorMessage ="Please enter valid emailid")]
        [MaxLength(50, ErrorMessage = "Max lenght must be 50 characters")]
        public string EmailId { get; set; }

        [Required]
        //[Range(1000000000, 9999999999, ErrorMessage = "Please Enter valid Mobile no")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter 10 digit Mobile no")]
        public string MobileNo { get; set; }


        [Required]
        public string DoctorName { get; set; }

        [Required]
        [Range(1, 105,ErrorMessage ="Please enter Age between 1 to 105")]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string AppointmentSlot { get; set; }

    }
    public class DoctorList
    {
        public string Name { get; set; }
        public string Id { get; set; }
    }
    public class AppointMentSlot
    {
        public string slot { get; set; }
        public string slotid { get; set; }
    }
}