//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BayWatchCSVExtractor
{
    using System;
    using System.Collections.Generic;
    
    public partial class BEACHED
    {
        public BEACHED()
        {
            this.BEACHED_ADDITIONAL_TECHNOLOGY = new HashSet<BEACHED_ADDITIONAL_TECHNOLOGY>();
            this.BEACHED_IN_HOUSE_TECHNOLOGY = new HashSet<BEACHED_IN_HOUSE_TECHNOLOGY>();
            this.BEACHED_LANGUAGE = new HashSet<BEACHED_LANGUAGE>();
        }
    
        public long BEACHED_ID { get; set; }
        public string FULL_NAME { get; set; }
        public string STREAM { get; set; }
        public string ACADEMY { get; set; }
        public string PREV_PLACEMENT { get; set; }
        public string PREV_JOB_TITLE { get; set; }
        public Nullable<byte> AVAILABLE { get; set; }
    
        public virtual ICollection<BEACHED_ADDITIONAL_TECHNOLOGY> BEACHED_ADDITIONAL_TECHNOLOGY { get; set; }
        public virtual ICollection<BEACHED_IN_HOUSE_TECHNOLOGY> BEACHED_IN_HOUSE_TECHNOLOGY { get; set; }
        public virtual ICollection<BEACHED_LANGUAGE> BEACHED_LANGUAGE { get; set; }
    }
}