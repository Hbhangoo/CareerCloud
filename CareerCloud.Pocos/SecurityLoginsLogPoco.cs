﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Threading.Tasks;

namespace CareerCloud.Pocos
{[Table("Security_Logins_Log")]
    public class SecurityLoginsLogPoco:IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        [Column("Source_IP")]
        public String SourceIP { get; set; }
         [Column("Is_Succesful")]
        public bool IsSuccesful { get; set; }
        [Column("Logon_Date")]
        public DateTime LogonDate { get; set; }

        public virtual SecurityLoginPoco SecurityLogins { get; set; }
    }
}
