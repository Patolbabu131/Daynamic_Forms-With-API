using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Daynamic_forms.Model
{
    //public class Forms
    //{
    //    [Key]
    //    public int FID { get; set; }
    //    public string? subject { get; set; }
    //    public string? description { get; set; }

    //    public List<Question>? Question { get; set; } 

    //}

    public class Forms
    {
        [Key]
        public int FID { get; set; }
        public string? subject { get; set; }
        public string? description { get; set; }

        public List<Question> Question { get; set; }
    

    }
}
