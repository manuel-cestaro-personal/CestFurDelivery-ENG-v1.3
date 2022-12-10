using CestFurDelivery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CestFurDelivery.Domain.Entities
{
    public class Team : ITeam
    {
        public Guid? Id { get; set; }

		[Required]
		[StringLength(50, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 1)]
		[DataType(DataType.Text)]
		[Display(Name = "Name")]
		public string? Name { get; set; }

		[StringLength(500, ErrorMessage = "The {0} must be at max {1} characters long.", MinimumLength = 0)]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Description")]
		public string? Description { get; set; }
		public bool? IsActive { get; set; }
	}
}
