using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(string i_ErrorMessage, float i_MinValue, float i_MaxValue) : base(i_ErrorMessage)
        {
            r_MinValue = i_MinValue;
            r_MaxValue = i_MaxValue;
        }

        public float MinValue
        {
            get { return r_MinValue; }
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }
    }
}
