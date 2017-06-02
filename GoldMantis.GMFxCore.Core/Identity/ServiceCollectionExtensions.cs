using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GoldMantis.GMFxCore.Core.Identity
{
    public static class ServiceCollectionExtensions
    {
        //public static IdentityBuilder AddAbpIdentity<TTenant, TUser, TRole, TSecurityStampValidator>
        //    (this IServiceCollection services, Action<IdentityOptions> setupAction) 
        //    where TTenant : AbpTenant<TUser> 
        //    where TUser : AbpUser<TUser> 
        //    where TRole : AbpRole<TUser>, new() 
        //    where TSecurityStampValidator :
        //    AbpSecurityStampValidator<TTenant, TRole, TUser>
        //{
        //    services.TryAddScoped<ISecurityStampValidator, TSecurityStampValidator>();
        //    return services.AddIdentity<TUser, TRole>(setupAction);
        //}
    }
}
