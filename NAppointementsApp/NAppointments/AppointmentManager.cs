using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAppointments
{
    public class AppointmentManager
    {
        private IList<Appointment> _Appointments { get; set; }

        public AppointmentManager(IList<Appointment> appointments)
        {
            this._Appointments = appointments;
        }

        public List<Tuple<Appointment,Appointment>> FindConflicts()
        {
            var conflicts = new List<Tuple<Appointment, Appointment>>();

            if (this._Appointments == null)
                throw new Exception("No appointments defined.");

            var previousAppointments = new List<Appointment>();

            foreach (var currentAppointment in this._Appointments)
            {
                var currentConflicts = previousAppointments.Where(x => x.HasConflict(currentAppointment)).Select(x => new Tuple<Appointment,Appointment>(currentAppointment, x));
                if(currentConflicts.Count() > 0)
                    conflicts.AddRange(currentConflicts);

                previousAppointments.Add(currentAppointment);
            }

            return conflicts;
        }
    }
}