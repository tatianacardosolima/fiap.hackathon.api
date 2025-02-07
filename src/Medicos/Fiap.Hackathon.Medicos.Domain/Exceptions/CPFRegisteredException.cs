using Fiap.Hackathon.Common.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Hackathon.Medicos.Domain.Exceptions
{
    public class CPFRegisteredException : Exception
    {
        public CPFRegisteredException() : base() { }
        public CPFRegisteredException(string message) : base(message) { }


        public static void ThrowWhenCPF(bool result, string errorMessage)
        {
            if (result) 
                throw new CPFRegisteredException(errorMessage);
        }

        
    }
}
