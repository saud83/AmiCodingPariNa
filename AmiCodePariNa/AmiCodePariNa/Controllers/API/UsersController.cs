using AmiCodePariNa.Data;
using AmiCodePariNa.Models.Users;
using AmiCodePariNa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmiCodePariNa.Controllers.API
{
    public class UsersController : ApiController
    {
        private AmiCodePariNaContext db;
        public UsersController()
        {
            db = new AmiCodePariNaContext();
        }

        
        [HttpGet]
        public IHttpActionResult GetInputValues(DateTime start_datetime, DateTime end_datetime, int user_id)
        {
            // search for the input values of a user between start_datetime and end_datetime
            var numbersForUser = db.khoj
            .Where(k => k.UserId == user_id && k.date >= start_datetime && k.date <= end_datetime)
            .Select(k => new
            {
                timestamp = k.date.ToString("yyyy-MM-dd HH:mm:ss"),
                input_values = k.listOfNumbers
            }).ToList();

            // Create the response format
            var response = new
            {
                status = "success",
                user_id = user_id,
                payload = numbersForUser.Select(iv => new InputValue
                {
                    Timestamp = iv.timestamp,
                    InputValues = string.Join(", ", iv.input_values)
                }).ToList()
            };

            return Ok(response);
        }
    }
}
