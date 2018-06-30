using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Raktar.Models
{
    public class Equipment
    {
        [Display(Name = "Felszerelés leirása")]
        public string Describe { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EquipmentID { get; set; }
        [Display(Name = "Márka")]
        public string Brand { get; set; }
        [Display(Name = "Tipus")]
        public string Type { get; set; }
        [Display(Name = "Kiegészítő márkája")]
        public string AdditionalBrand  { get; set; }
        [Display(Name = "Kiegészítő Tipusa")]
        public string AdditionalType { get; set; }
        [Display(Name = "Kiegészítő megnevezése")]
        public string AdditionalDescribe { get; set; }
        [Display(Name = "Netto Át")]
        [Column(TypeName = "money")]
        public int NettoPrice { get; set; }
        [Column(TypeName = "money")]
        [Display(Name = "Bruttó Ár")]
        public int BruttoPrice { get; set; }

        [Display(Name = "Felszerelés Neve")]
        public string EqiupmentName
        {
            get
            {
                return Brand + ", " + Type;
            }
        }

        //static readonly double AFA = 1.25;

        //public virtual Loan Loan { get; set; }
        //public virtual Stock Stock { get; set; }


    }
}