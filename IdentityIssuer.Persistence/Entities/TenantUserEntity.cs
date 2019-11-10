﻿using Microsoft.AspNetCore.Identity;

namespace IdentityIssuer.Persistence.Entities
{
    public class TenantUserEntity : IdentityUser<int>
    {
        public string DisplayName { get; set; }
        public string UserGuid { get; set; }
        public int TenantId { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string ImageUrl { get; set; }

        public virtual TenantEntity Tenant { get; set; }
    }
}