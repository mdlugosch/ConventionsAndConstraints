using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Infrastructure
{
    public class UserAgentAttribute : Attribute, IActionConstraintFactory
    {
        private string substring;

        public UserAgentAttribute(string sub)
        {
            substring = sub.ToLower();
        }

        public IActionConstraint CreateInstance(IServiceProvider services)
        {
            return new UserAgentConstraint(services.GetService<UserAgentComparer>(), substring);
        }

        public bool IsReusable => false;

        private class UserAgentConstraint : IActionConstraint
        {
            private UserAgentComparer comparer;
            private string substring;

            public UserAgentConstraint(UserAgentComparer comp, string sub)
            {
                comparer = comp;
                substring = sub.ToLower();
            }

            public int Order { get; set; } = 0;

            // ActionMethode nur ausführen wenn der UserAgent-String aus der Annotation der Actionmethode entspricht oder...
            // wenn nur eine passende Actionmethode gefunden wurde.
            public bool Accept(ActionConstraintContext context)
            {
                return context.RouteContext.HttpContext
                    .Request.Headers["User-Agent"]
                    .Any(h => h.ToLower().Contains(substring)) ||
                    context.Candidates.Count() == 1;
            }
        }
    }
}
