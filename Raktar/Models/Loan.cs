using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Raktar.Models
{
    public class Loan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string LoanID { get; set; }
        //[Key]
        //[ForeignKey("Renter")]
        public string RenterID { get; set; }
        //[Key]
        //[ForeignKey("Equipment")]
        [Display(Name = "Felszerelés")]
        public string EquipmentID { get; set; }
        [Display(Name = "Mennyiség")]
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Kölcsönzés ideje:")]
        public DateTime Date { get; set; }

        public virtual Renter Renter { get; set; }
        public virtual Equipment Equipment { get; set; }

    }
}