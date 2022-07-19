using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataEntities.InterfaceModel
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Users")]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
    }
}
