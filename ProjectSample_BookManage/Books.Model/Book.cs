//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Books.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public string dateTime;

        public int BookID { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public Nullable<System.DateTime> PublisherDate { get; set; }
        public Nullable<int> PageNumber { get; set; }
        public Nullable<System.DateTime> AddOn { get; set; }
    }
}
