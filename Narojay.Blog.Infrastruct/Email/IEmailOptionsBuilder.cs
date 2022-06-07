using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Narojay.Blog.Infrastruct.Email.Core;
using Nest;

namespace Narojay.Blog.Infrastruct.Email
{
    public interface IEmailOptionsBuilder
    {
        IServiceCollection ServiceCollection { get; }


        IEmailOptionsBuilder UseMail(EmailOption options, ServiceLifetime lifetime = ServiceLifetime.Scoped);
    }

    class EmailOptionsBuilder : IEmailOptionsBuilder
    {
        public IServiceCollection ServiceCollection { get; }


        public EmailOptionsBuilder(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }


        public IEmailOptionsBuilder UseMail(EmailOption options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            AddProviderService(options);

            ServiceCollection.TryAdd(new ServiceDescriptor(typeof(IEmailService),typeof(EmailService), lifetime));

            return this;
        }


        private void AddProviderService(EmailOption emailOption)
        {
           var provider =    new MailProvider(emailOption);
           ServiceCollection.TryAddSingleton<IMailProvider>(provider);
        }
    }
}
