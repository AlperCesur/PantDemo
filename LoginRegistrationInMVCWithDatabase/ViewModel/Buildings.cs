using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginRegistrationInMVCWithDatabase.ViewModel
{
    public class Buildings
    {
        [Key]
        public int ID { get; set; }
        public string BuildingType { get; set; }
        public decimal BuildingCost{ get; set; }
        public decimal ConstructionTime { get; set; }
        public SelectList SelectList { get; set; }
    }
}