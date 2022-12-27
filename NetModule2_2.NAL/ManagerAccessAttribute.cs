using Microsoft.AspNetCore.Authorization;

namespace NetModule2_2.NAL
{
    public class ManagerAccessAttribute : AuthorizeAttribute
    {
        public ManagerAccessAttribute() : base(nameof(ManagerAccessAttribute)) { }
    }
}
