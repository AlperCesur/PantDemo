//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoginRegistrationInMVCWithDatabase.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Buildings
    {
        public int ID { get; set; }
        public string BuildingType { get; set; }
        public Nullable<decimal> BuildingCost { get; set; }
        public Nullable<decimal> ConstructionTime { get; set; }
    }
}
