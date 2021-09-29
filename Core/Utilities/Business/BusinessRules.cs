using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] loigcs)
        {
            foreach(var logic in loigcs)
            {
                if (!logic.Success)
                    return logic;
            }
            return null;
        }
    }
}
