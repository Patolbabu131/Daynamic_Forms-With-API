using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Daynamic_forms_mvc.Model
{
    public class Forms
    {
        [Key]
        public int FID { get; set; }
        public string? subject { get; set; }
        public string? description { get; set; }

        public List<Question> Question { get; set; }


    }
}