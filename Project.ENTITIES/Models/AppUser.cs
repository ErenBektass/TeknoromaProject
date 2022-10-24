using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
     public class AppUser:BaseEntity
     {
        public AppUser()
        {
            
            ActivationCode = Guid.NewGuid();
        }

        public string Email { get; set; }
        public UserRole MyProperty { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }

        //Relational Properties
        public virtual UserProfile Profile { get; set; }
        public virtual Order Orders { get; set; }
    } 
}
