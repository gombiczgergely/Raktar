using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Raktar.Models
{
    public class Renter

    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RenterID { get; set; }
        [StringLength(70,MinimumLength =1, ErrorMessage = "Név mező nem lehet rövidebb 1 karakternél és hosszabb 70-nél.")]
        [Display(Name = "Név")]
        public string Name { get; set; }
        [Display(Name = "Város")]
        public string City { get; set; }
        [Display(Name = "Irányítószám")]
        public int ZipCode { get; set; }
        [Display(Name = "Utca házszám")]
        [Column("StreetAddress")]
        public string StreetAddress { get; set; }
        [Display(Name = "Telefon")]
        public long Phone { get; set; }
        [Display(Name = "Email")]
        public string Mail { get; set; }

        //public virtual Loan Loan { get; set; }



    }
}