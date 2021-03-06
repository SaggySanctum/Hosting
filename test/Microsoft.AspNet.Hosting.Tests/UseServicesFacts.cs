﻿using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.OptionsModel;
using Xunit;

namespace Microsoft.AspNet.Hosting.Tests
{
    public class UseServicesFacts
    {
        [Fact]
        public void OptionsAccessorCanBeResolvedAfterCallingUseServicesWithAction()
        {
            var baseServiceProvider = new ServiceCollection().BuildServiceProvider();
            var builder = new Microsoft.AspNet.Builder.Builder(baseServiceProvider);

            builder.UseServices(serviceCollection => { });

            var optionsAccessor = builder.ApplicationServices.GetService<IOptionsAccessor<object>>();
            Assert.NotNull(optionsAccessor);
        }


        [Fact]
        public void OptionsAccessorCanBeResolvedAfterCallingUseServicesWithFunc()
        {
            var baseServiceProvider = new ServiceCollection().BuildServiceProvider();
            var builder = new Microsoft.AspNet.Builder.Builder(baseServiceProvider);
            IServiceProvider serviceProvider = null;

            builder.UseServices(serviceCollection =>
            {
                serviceProvider = serviceCollection.BuildServiceProvider(builder.ApplicationServices);
                return serviceProvider;
            });

            Assert.Same(serviceProvider, builder.ApplicationServices);
            var optionsAccessor = builder.ApplicationServices.GetService<IOptionsAccessor<object>>();
            Assert.NotNull(optionsAccessor);
        }
    }
}