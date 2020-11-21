﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rhapsodyLatest.Auth
{
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
            UserValidator = new UserValidator<AppUser>(this)
            {
               AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<AppUser>(context.Get<EFCodeFirstModel>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}