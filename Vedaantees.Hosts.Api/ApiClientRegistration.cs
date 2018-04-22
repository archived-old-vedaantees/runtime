using System;
using System.Collections.Generic;

namespace Vedaantees.Hosts.Api
{
    public class ApiClientRegistration
    {
        public string Route { get; set; }
        public Type Type { get; set; }
    }

    public class ApiClientRegistrations : List<ApiClientRegistration>
    {
        
    }
}