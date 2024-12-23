namespace PizzaMeow.MiddleWare
{
    public class RequestLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpContext _context;
        public RequestLimitMiddleware(RequestDelegate next, HttpContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task ExecuteAsync()
        {
            //TODO
        }
    }
}
