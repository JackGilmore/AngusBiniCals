using System;
using System.Collections.Generic;
using System.Linq;

namespace AngusBiniCals.Models
{
    public class AddressComparer : IComparer<AddressEntry>
    {
        public int Compare(AddressEntry x, AddressEntry y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;

            var xSplit = x.Address.Split();
            var ySplit = y.Address.Split();

            if (!xSplit.Any()) return 1;
            if (!ySplit.Any()) return -1;

            var xNumResult = int.TryParse(xSplit.First(), out var xNum);
            var yNumResult = int.TryParse(ySplit.First(), out var yNum);

            if (!xNumResult || !yNumResult)
            {
                return string.Compare(x.Address, y.Address, StringComparison.Ordinal);
            }

            if (xNum == yNum)
            {
                return 0;
            }
            if (xNum > yNum)
            {
                return 1;
            }
            if (xNum < yNum)
            {
                return -1;
            }

            return string.Compare(x.Address, y.Address, StringComparison.Ordinal);
        }
    }
}