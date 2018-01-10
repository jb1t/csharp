using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAppointments
{
    public class Appointment
    {
        private int _StartTime { get; set; }
        private int _EndTime { get; set; }

        public int StartTime { get { return this._StartTime; } }
        public int EndTime { get { return this._EndTime; } }

        public Appointment(int startTime, int endTime)
        {
            if (endTime <= startTime)
            {
                throw new ArgumentException("StartTime must be less than EndTime");
            }

            this._StartTime = startTime;
            this._EndTime = endTime;
        }

        public bool HasConflict(Appointment appointment)
        {
            var returnVal = (appointment.StartTime >= this.StartTime && appointment.StartTime < this.EndTime) ||
                            (appointment.EndTime > this.StartTime && appointment.EndTime <= this.EndTime) ||
                            (this.StartTime >= appointment.StartTime && this.StartTime < appointment.EndTime) ||
                            (this.EndTime > appointment.StartTime && this.EndTime <= appointment.EndTime);
            return returnVal;
        }

        public override string ToString()
        {
            return $"[{this.StartTime},{this.EndTime}]";
        }
    }
}
