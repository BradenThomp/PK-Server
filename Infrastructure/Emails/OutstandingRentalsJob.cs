using Application.Common.Repository;
using FluentScheduler;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Emails
{
    public class OutstandingRentalsJob : IJob
    {
        private readonly IRentalRepository _repo;
        protected readonly IConfiguration _configuration;

        public OutstandingRentalsJob(IRentalRepository repo)
        {
            _repo = repo;
        }

        public void SendMail(string address, string subject, string body)
        {


            var fromAddress = new MailAddress("capstone765@gmail.com", "From Name");
            var toAddress = new MailAddress(address, "To Name");
            const string fromPassword = "Calgary123@";



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        public void Execute()
        {
            var rentals = _repo.GetAllAsync().GetAwaiter().GetResult();
            var today = DateTime.Now;
            
            foreach (Domain.Models.Rental r in rentals)
            {
                
                Console.WriteLine("Rental ID:" + r.Id);
                Console.WriteLine("Date Returned:" + r.DateReturned);

                var connection = new MySqlConnection(_configuration.GetConnectionString("ApplicationMySQLDataBase"));
                connection.Open();
                using var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT Name,Email From Customer WHERE Id=" + r.Id;
                var d = cmd.ExecuteNonQuery().ToString();

                Console.WriteLine(d);
                var customer_email = "rashikhassan@gmail.com";
                var subject = "Rental# " + r.Id + " is Due";
                var content = "Dear Valued Customer," + "\n\nYour rental with Pk Sound is overdue. From our records we can see that you have rented speakers" +
                    " from us on " + r.RentalDate + " and was suppose to return to our office on " + r.ExpectedReturnDate + 
                    "\n\nPlease return the speaker as soon as possible to our pickup location to avoid late fees.";

                if ( DateTime.Compare(today, r.ExpectedReturnDate) > 0 && r.DateReturned == null) {
                    SendMail(customer_email, subject, content);
                }

                /*
                foreach (var id in r.RentedSpeakers)
                {
                    Console.WriteLine("Device ID:" + id);
                }
                */

            }
           
        }
    }
}
