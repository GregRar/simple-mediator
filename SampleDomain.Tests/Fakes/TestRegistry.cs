using SampleDomain.Core;
using SampleDomain.Mediator;
using StructureMap;
using StructureMap.Graph;

namespace SampleDomain.Tests.Fakes
{
    public class TestRegistry : Registry
    {
        public TestRegistry()
        {
            this.Scan(scan =>
            {
                var assemblies = AssemblyFinder.FindAssemblies(assembly => assembly.FullName.Contains("SampleDomain"), includeExeFiles: false);
                foreach (var assembly in assemblies)
                {
                    scan.Assembly(assembly);
                }
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.RegisterConcreteTypesAgainstTheFirstInterface();
                scan.AddAllTypesOf<IHanderType>();
                scan.AddAllTypesOf<ICommandHandler>();
            });

            this.For<EmailSender>().Use(new EmailSender()).Singleton();
            this.For<InMemoryDataStore>().Use(new InMemoryDataStore()).Singleton();
            this.For(typeof(IRepository<>)).Use(typeof(FakeRepository<>));
        }
    }
}