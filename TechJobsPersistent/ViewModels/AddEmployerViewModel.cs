using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobsPersistent.ViewModels
{
    public class AddEmployerViewModel
    {
        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Please enter a name between 3 and 40 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a location.")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Please enter a location name between 3 and 40 characters.")]
        public string Location { get; set; }
    }
}
