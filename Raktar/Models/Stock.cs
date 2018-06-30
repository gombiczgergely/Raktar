using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Raktar.Models
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StockID { get; set; }
        //[Key]
        //[ForeignKey("Equipment")]
        public string EquipmentID { get; set; }
        [Display(Name = "Van e szabad:")]
        public bool InStock { get; set; }
        [Display(Name = "Szabad mennyiség")]
        public int SQuantity { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}