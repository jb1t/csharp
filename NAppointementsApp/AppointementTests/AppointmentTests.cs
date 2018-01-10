using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAppointments;
using System;

namespace NAppointementTests
{
    [TestClass]
    public class AppointmentTests
    {
        [TestMethod]
        public void InValidAppointment_ThrowException()
        {
            //setup
            Assert.ThrowsException<ArgumentException>(() => new Appointment(3, 1));
        }
        [TestMethod]
        public void IsConflicted_WhenAppointmentOutOfRange_NoConflict()
        {
            //set
            var app1 = new Appointment(1, 3);
            var app2 = new Appointment(4, 6);

            //act
            var hasConflict = app1.HasConflict(app2);

            //Assert
            Assert.IsFalse(hasConflict);
        }

        [TestMethod]
        public void IsConflicted_WhenAppointmentOverlaps_Conflict()
        {
            //set
            var app1 = new Appointment(1, 3);
            var app2 = new Appointment(2, 5);

            //act
            var hasConflict = app1.HasConflict(app2);

            //Assert
            Assert.IsTrue(hasConflict);
        }

        [TestMethod]
        public void IsConflicted_WhenAppointmentHasSameUpperBound_NoConflict()
        {
            //set
            var app1 = new Appointment(1, 3);
            var app2 = new Appointment(3, 5);

            //act
            var hasConflict = app1.HasConflict(app2);

            //Assert
            Assert.IsFalse(hasConflict);
        }

        [TestMethod]
        public void IsConflicted_WhenAppointmentHasSameLowerBound_NoConflict()
        {
            //set
            var app1 = new Appointment(3, 5);
            var app2 = new Appointment(1, 3);

            //act
            var hasConflict = app1.HasConflict(app2);

            //Assert
            Assert.IsFalse(hasConflict);
        }
    }
}
