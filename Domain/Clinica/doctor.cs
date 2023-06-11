using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinica
{
    public class Doctor : Clinic
    {
        public List<string> specialty;
        public List<DateTime> EnterWorkShift;
        public List<DateTime> ExitWorkShift;
        public List<Pacient> pacient;

    }
}