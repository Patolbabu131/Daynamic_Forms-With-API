using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json.Serialization;

namespace Daynamic_forms.Model
{
    //public class Question
    //{
    //    [Key]
    //    public int QID { get; set; }
    //    public string? questions { get; set; }
    //    public Answer? anstype { get; set; }
    //    public string? answer { get; set; } 
    //}

    public class Question
    {
        [Key]
        public int QID { get; set; }
        public string? questions { get; set; }
        public Answer? anstype { get; set; }
        public string? answer { get; set; }

        public int FormsId { get; set; }
    }
}
public enum Answer
{
    SinglelineQ ,
    TrueOrFalseQ
}
