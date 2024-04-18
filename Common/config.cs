using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class config
    {
        public IConfiguration con { get; }
        public config(IConfiguration configuration)
        {
            con = configuration;
        }
    }
}
