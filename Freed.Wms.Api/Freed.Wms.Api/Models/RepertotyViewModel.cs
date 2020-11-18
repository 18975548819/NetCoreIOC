using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models
{
    public class RepertotyViewModel
    {
        [Required]
        public string GroupType { get; set; }
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
