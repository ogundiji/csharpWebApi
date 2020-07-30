using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiFundamental.Core.Data.Entities
{
    public class ErrorLogger
    {
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Error_message { get; set; }

        [Column(TypeName = "text")]
        public string Error_message_detail { get; set; }

        [StringLength(50)]
        public string Controller_Name { get; set; }
        public DateTime Error_Logged_date { get; set; }
    }
}