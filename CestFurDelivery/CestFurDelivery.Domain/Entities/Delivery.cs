using CestFurDelivery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Domain.Entities
{
    public class Delivery : IDelivery
    {
        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        public Guid? Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public TimeSpan TimeStart { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End time")]
        public TimeSpan TimeEnd { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Client name")]
        public string? ClientName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Client surname")]
        public string? ClientSurname { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Furniture")]
        public string? Furniture { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 0)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Note")]
        public string? Note { get; set; }

        [Required]
        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        [Display(Name = "Delivery state")]
        public Guid IdDeliveryState { get; set; }

        [Required]
        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        [Display(Name = "Vehicle1")]
        public Guid Vehicle1 { get; set; }

        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        [Display(Name = "Vehicle2")]
        public Guid? Vehicle2 { get; set; }

        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        [Display(Name = "Vehicle3")]
        public Guid? Vehicle3 { get; set; }

        [Required]
        //[RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "Cannot use empty Guid")]
        [Display(Name = "Team")]
        public Guid Team { get; set; }
    }
}
