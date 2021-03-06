//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SwinnyVetAPI.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class Treatment
    {
        public string PetName { get; set; }
        public string OwnerID { get; set; }
        public string ProcedureID { get; set; }
        public System.DateTime TreatmentDate { get; set; }
        public string TreatmentNotes { get; set; }
        public Nullable<decimal> TreatmentPrice { get; set; }

        [JsonIgnore]
        public virtual Pet Pet { get; set; }
        [JsonIgnore]
        public virtual Procedure Procedure { get; set; }
    }
}
