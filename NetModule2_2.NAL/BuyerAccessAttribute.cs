using Microsoft.AspNetCore.Authorization;

namespace NetModule2_2.NAL
{
    public class BuyerAccessAttribute : AuthorizeAttribute
    {
        public BuyerAccessAttribute() : base(nameof(BuyerAccessAttribute)) { }
    }
}
