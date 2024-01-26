namespace PruebaTecnica.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PruebaTecnica.Entities;

    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Usuario Usuario => (Usuario)HttpContext.Items["Usuario"];
    }
}
