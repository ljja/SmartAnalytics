using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.Models
{
   public  class UserLocalMessage
    {
       public int sum { get; set; }
       public List<UserLocalPageItem> data { get; set; }
    }
}
