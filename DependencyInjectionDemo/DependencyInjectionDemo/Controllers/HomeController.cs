using DependencyInjectionDemo.Models;
using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DependencyInjectionDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransientService transientService1;
        private readonly ITransientService transientService2;
        private readonly IScopedService scopedService1;
        private readonly IScopedService scopedService2;
        private readonly ISingletonService singletonService1;
        private readonly ISingletonService singletonService2;

        private readonly Func<string, IService> serviceAccessor;

        private readonly IKeyedMessageService keyedMessageServiceA;
        private readonly IKeyedMessageService keyedMessageServiceB;

        public HomeController(
            ILogger<HomeController> logger,
            ITransientService transientService1,
            ITransientService transientService2,
            IScopedService scopedService1,
            IScopedService scopedService2,
            ISingletonService singletonService1,
            ISingletonService singletonService2,
            Func<string, IService> serviceAccessor,
            [FromKeyedServices(Constants.KeyedServiceA)] IKeyedMessageService keyedMessageServiceA,
            [FromKeyedServices(Constants.KeyedServiceB)] IKeyedMessageService keyedMessageServiceB)
        {
            _logger = logger;
            this.transientService1 = transientService1;
            this.transientService2 = transientService2;
            this.scopedService1 = scopedService1;
            this.scopedService2 = scopedService2;
            this.singletonService1 = singletonService1;
            this.singletonService2 = singletonService2;

            this.serviceAccessor = serviceAccessor;
            
            this.keyedMessageServiceA = keyedMessageServiceA;
            this.keyedMessageServiceB = keyedMessageServiceB;
        }

        public IActionResult Index()
        {
            // Transient: Different GUIDs every time because a new instance is created each time
            ViewBag.Transient1 = transientService1.GetOperationId();
            ViewBag.Transient2 = transientService2.GetOperationId();

            // Scoped: Same GUID within a request but different across requests
            ViewBag.Scoped1 = scopedService1.GetOperationId();
            ViewBag.Scoped2 = scopedService2.GetOperationId();

            // Singleton: Same GUID throughout the application's lifetime
            ViewBag.Singleton1 = singletonService1.GetOperationId();
            ViewBag.Singleton2 = singletonService2.GetOperationId();
            return View();
        }

        public IActionResult Privacy()
        {
            var serviceA = serviceAccessor(Constants.ServiceA);
            ViewBag.ServiceAMessage = serviceA.GetMessage();
            var serviceB = serviceAccessor(Constants.ServiceB);
            ViewBag.ServiceBMessage = serviceB.GetMessage();

            return View();
        }

        public IActionResult Keyed()
        {
            ViewBag.KeydServiceAMessage = keyedMessageServiceA.GetMessage();
            ViewBag.KeyedServiceBMessage = keyedMessageServiceB.GetMessage();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
