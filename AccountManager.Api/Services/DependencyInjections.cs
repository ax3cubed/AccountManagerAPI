using AccountManager.Business;
using AccountManager.Data;
using AccountManager.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManager.Api
{
    public static class DependencyInjections
    {
        public static void Register(IServiceCollection services)
        {
            services

                .AddTransient<EncryptionService>()
                .AddTransient<JWTService>()


                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient(typeof(IGeneric<>), typeof(GenericRepository<>))

                .AddTransient<IUser, UserRepository>().AddTransient<UserBusiness>()
                .AddTransient<IAccount, AccountRepository>().AddTransient<AccountBusiness>();
        }
    }
}
