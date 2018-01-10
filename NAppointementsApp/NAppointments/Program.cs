using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAppointments
{
    class Program
    {
        static void Main(string[] args)
        {
            var appointments = new List<Appointment>() {
                new Appointment(1, 5),
                new Appointment(3, 7),
                new Appointment(2, 6),
                new Appointment(10, 15),
                new Appointment(5, 6),
                new Appointment(4, 100),
            };

            Console.WriteLine("Following are conflicting intervals");

            var appointmentManager = new AppointmentManager(appointments);

            var conflicts = appointmentManager.FindConflicts();

            foreach (var conflict in conflicts)
            {
                Console.WriteLine($"{conflict.Item1} conflicts with {conflict.Item2}");
            }
            Console.ReadKey();
        }
    }
}
