using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.Controllers.Api
{
    public class AppointmentController : ApiController
    {
        private VidlyContext _context;

        public AppointmentController()
        {
            _context = new VidlyContext();
        }

        [HttpGet]
        public IHttpActionResult GetAppointments()
        {
            var appoinQuery = _context.Appointments.ToList();


            var appointDto = appoinQuery.ToList().Select(Mapper.Map<Appointment, AppointmentDto>);
            return Ok(appointDto);
            // return _context.customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }


        [HttpPost]
        public IHttpActionResult CreateAppointment(AppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
              return BadRequest();



            var usr = _context.usersList.Where(x => x.Id == appointmentDto.UserId).ToList().Count();

            if (usr == 0)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User Does not exists", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

            //check user status
            var status = (from x in _context.usersList
                         where x.Id == appointmentDto.UserId
                         && x.status == false
                         select x).Count();
                
            
            if (status > 0)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("InActive Users can not book appointments", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
              
            //check selected date and time

            //check cannot book more then 1 hour


            double i = getMinutes(appointmentDto.BookingStart, appointmentDto.BookingEnd);
            if (i<= 60)
            {
                //valid date
                int cmp = DateTime.Compare(appointmentDto.BookingStart, DateTime.Now);

                if (cmp >= 1)
                {
                    //date is grater

                    if(checkConflict(appointmentDto.BookingStart, appointmentDto.BookingEnd))
                    {
                        var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Docter has an appointment on same time kindly select another time", System.Text.Encoding.UTF8, "text/plain"),
                            StatusCode = HttpStatusCode.NotFound
                        };
                        throw new HttpResponseException(response);
                     
                    }else
                    {
                        //check user book only one appnmnt per day
                        if(checkSameDateAppointment(appointmentDto.BookingStart, appointmentDto.BookingEnd, appointmentDto.UserId))
                        {
                            //has booked appnmnt
                            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                            {
                                Content = new StringContent("You have already booked an appointment for the same date", System.Text.Encoding.UTF8, "text/plain"),
                                StatusCode = HttpStatusCode.NotFound
                            };
                            throw new HttpResponseException(response);
                            throw new Exception("Already booked appointment for the same date");
                        }else
                        {
                            //allow to booked 
                            var appoint = Mapper.Map<AppointmentDto, Appointment>(appointmentDto);
                            _context.Appointments.Add(appoint);
                            _context.SaveChanges();

                            appointmentDto.Id = appoint.Id;

                            //use created to redirect created value.
                            return Created(new Uri(Request.RequestUri + "/" + appoint.Id), appoint);
                        }


                    }

                }
                else if (cmp < 1)
                {
                    //date is less
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Can not booking past date time booking", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                   
                    
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Can not booking past date time booking", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);

                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Invalid Booking start and End Date", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
                
            }

           
        }

        [NonAction]
        public double getMinutes (DateTime strtTime, DateTime EndTime)
        {
            TimeSpan ts = EndTime - strtTime;


            return ts.TotalMinutes;
        }

     
        [NonAction]
        public bool checkConflict (DateTime strtdateTime, DateTime endDateTime)
        {
            var appointment = (from x in _context.Appointments
                                    join u in _context.usersList on x.UserId equals u.Id
                                    where
                                    ((x.BookingStart < strtdateTime && x.BookingEnd > strtdateTime)
                                    ||(x.BookingStart < endDateTime && x.BookingEnd > endDateTime))
                                    && u.status == true
                                    select x).Count();
                                   //&& (x.BookingEnd <= strtdateTime && x.BookingEnd )

            
            return appointment > 0 ? true : false;
        }

        [NonAction]
        public bool checkSameDateAppointment(DateTime strtdateTime, DateTime endDateTime, int userID)
        {
            

            var appointment = (from x in _context.Appointments
                               where
                             (DbFunctions.TruncateTime(x.BookingStart) >= DbFunctions.TruncateTime(strtdateTime))
                               //(x.BookingStart.ToShortDateString().Equals(strtdateTime.ToShortDateString()))
                               && x.UserId == userID
                               select x).Count();
            //&& (x.BookingEnd <= strtdateTime && x.BookingEnd )


            return appointment > 0 ? true : false;
        }

    }
}