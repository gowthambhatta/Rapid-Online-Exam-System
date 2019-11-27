//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace onllineexam.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TestGenerator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestGenerator()
        {
            this.QuestionDatas = new HashSet<QuestionData>();
            this.Results = new HashSet<Result>();
            this.TestAccessors = new HashSet<TestAccessor>();
        }
    
        public int Test_id { get; set; }
        [Display(Name ="Test Name")]
        [Required]
        public string Test_name { get; set; }
        [Display(Name = "Teacher Name")]
        [Required]
        public Nullable<int> Teach_id { get; set; }
        [Display(Name = "Test date")]
        [Required]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Test_date { get; set; }
        [Display(Name = "Test Time in Minutes")]
        [Required]
        [MaxLength(3)]
        public string Test_time { get; set; }
        [Display(Name = "Test Subject Name")]
        [Required]
        public string sub_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionData> QuestionDatas { get; set; }
        public virtual QuestionFile QuestionFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestAccessor> TestAccessors { get; set; }
    }
}
