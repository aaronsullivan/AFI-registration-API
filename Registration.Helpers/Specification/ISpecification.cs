using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Helpers.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T x);
    }
}
