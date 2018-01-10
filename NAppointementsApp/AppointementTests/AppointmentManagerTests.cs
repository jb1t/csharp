using Microsoft.VisualStudio.TestTools.UnitTesting;
using NAppointments;
using System;
using System.Collections.Generic;

namespace NAppointementTests
{
    [TestClass]
    public class AppointmentManagerTests
    {
        [TestMethod]
        public void FindConflicts_WhenNoAppointmentsExist_ThrowException()
        {
            //setup
            var appt = new AppointmentManager(null);

            //act & Assert
            Assert.ThrowsException<Exception>(() => appt.FindConflicts());
        }

        [TestMethod]
        public void FindConflicts_WhenThereAreNoConflicts_NothingReturned()
        {
            //setup
            var appointments = new List<Appointment>() { new Appointment(1, 3), new Appointment(4, 5) };
            var apptMgr = new AppointmentManager(appointments);

            //act
            var result = apptMgr.FindConflicts();

            //assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void FindConflicts_WhenThereIsOneConflict_ReturnOneConflictTuple()
        {
            //setup
            var appointments = new List<Appointment>() { new Appointment(1, 3), new Appointment(2, 5) };
            var apptMgr = new AppointmentManager(appointments);

            //act
            var result = apptMgr.FindConflicts();

            //assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void FindConflicts_WhenThereIsMultipleConflicts_ReturnTwoConflictTuple()
        {
            //setup
            var appointments = new List<Appointment>() {
                new Appointment(1, 3),
                new Appointment(2, 5),
                new Appointment(7, 9),
                new Appointment(5, 6),
                new Appointment(8, 10),
            };
            var apptMgr = new AppointmentManager(appointments);

            //act
            var result = apptMgr.FindConflicts();

            //assert
            Assert.AreEqual(2, result.Count);
        }


        [TestMethod]
        public void FindConflicts_ForExample_Return10Conflicts()
        {
            //setup - example inputs from https://www.geeksforgeeks.org/given-n-appointments-find-conflicting-appointments/
            var appointments = new List<Appointment>() {
                new Appointment(1, 5),
                new Appointment(3, 7),
                new Appointment(2, 6),
                new Appointment(10, 15),
                new Appointment(5, 6),
                new Appointment(4, 100),
            };
            var apptMgr = new AppointmentManager(appointments);

            //act
            var result = apptMgr.FindConflicts();

            //assert
            Assert.AreEqual(10, result.Count);
        }



    }
}
