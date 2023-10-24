using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceTemplate.DataAccess.Entities
{
    public class SC_AMA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AMACode { get; set; }
        public string Description { get; set; }
        public decimal AMAFee { get; set; }
        public int? MedicareItem { get; set; }
        public decimal? ScheduleFee { get; set; }

        public SC_MBS MedibankSchedule { get; set; }
    }
}
