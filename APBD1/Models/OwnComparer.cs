using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD2.Models
{
    public class OwnComparer : IEqualityComparer<Student>
    {

        public bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{x.fname} {x.lname} {x.indexNumber}",
                $"{y.fname} {y.lname} {y.indexNumber}");
        }

        public int GetHashCode([DisallowNull] Student obj)
        {
            return StringComparer
                .CurrentCultureIgnoreCase
                .GetHashCode($"{obj.fname} {obj.lname} {obj.indexNumber}");
        }
    }
}
