﻿namespace IdentityIssuer.Application.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsAdminTenant { get; set; }
    }
}
