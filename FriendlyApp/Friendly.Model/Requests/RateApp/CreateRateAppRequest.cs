using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friendly.Model.Requests.RateApp
{
    public class CreateRateAppRequest
    {
        public int UserId { get; set; }
        public double Rating { get; set; }
    }
}
