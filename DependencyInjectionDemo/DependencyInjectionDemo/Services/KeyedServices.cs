namespace DependencyInjectionDemo.Services
{
    public interface IKeyedMessageService
    {
        string GetMessage();
    }

    public class KeyedServiceA : IKeyedMessageService
    {
        public string GetMessage() => "Keyed Service A";
    }

    public class KeyedServiceB : IKeyedMessageService
    {
        public string GetMessage() => "Keyed Service B";
    }
}
