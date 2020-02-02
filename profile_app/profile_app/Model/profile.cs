using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace profile_app.Model
{
    public class profile
    {

        //Data Validations using Data annotation
        //[Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
        //[DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:d}")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [StringLength(20)]      
        public string FullName { get; set; }
        public int Age { get; set; }

        public string Dob { get; set; }

        public string City { get; set; }
    }
}
