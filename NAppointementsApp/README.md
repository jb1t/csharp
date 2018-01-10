## [NAppointments App](https://github.com/jb1t/csharp/tree/master/NAppointementsApp)

I preparing for some interviews and I was told by a HR Recruiter that in the next phase of my interviewing would be a phone/computer session where they'd ask me a question similar to something like this: [https://www.geeksforgeeks.org/given-n-appointments-find-conflicting-appointments/](https://www.geeksforgeeks.org/given-n-appointments-find-conflicting-appointments/).

So I thought today to practice I would take that example and implement it. 

Based on my understanding of the requirements here is my solution. If you look at the comments below this question/solution. You'll see a lot of people are confused by the example output.

My understanding you give me a list/array of appointments. I think of these appointments as real life meetings you'd have with peers or a manager, however, at this time only on the hour and have to last at minimum an hour increments. So you provide the list and the logic states:[](http://)

> "An appointment is conflicting, if it conflicts with any of the previous appointments in array."

With that in mind... I created two main classes. Appointment.cs and AppointmentManager.cs.

- **[Appointment](https://github.com/jb1t/csharp/blob/master/NAppointementsApp/NAppointments/Appointment.cs)** has a start and end time. An Appointment can determine if it has a conflict with another appointment.

- **[AppointmentManager](https://github.com/jb1t/csharp/blob/master/NAppointementsApp/NAppointments/AppointmentManager.cs)** takes in a list of Appointments. It has a method to get all the conflicts.

I created a UnitTest Project **[AppointmentTests.csproj](https://github.com/jb1t/csharp/tree/master/NAppointementsApp/AppointementTests)**.

Check out the tests you'll my thought process as how I determine if there are conflicts. 
