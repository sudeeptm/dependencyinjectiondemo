using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Services
{
    public interface IService
    {
        string GetMessage();
    }

    public class ServiceA : IService
    {
        public string GetMessage() => "Service A";
    }

    public class ServiceB : IService
    {
        public string GetMessage() => "Service B";
    }

}
