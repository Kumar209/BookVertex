using BookVertex.Business.Services.IServices;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookVertex.Business.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["Mailjet:ApiKey"];
            _secretKey = _configuration["Mailjet:SecretKey"];
            _senderEmail = _configuration["Mailjet:SenderEmail"];
            _senderName = _configuration["Mailjet:SenderName"];
        }


        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                MailjetClient client = new MailjetClient(_apiKey, _secretKey);

                var email = new TransactionalEmailBuilder().WithFrom(new SendContact(_senderEmail, _senderName))
                    .WithTo(new SendContact(toEmail)).WithSubject(subject).WithHtmlPart(htmlContent).Build();


                var response = await client.SendTransactionalEmailAsync(email);

                //if (response.Messages != null && response.Messages.Length > 0)
                //{
                //    var message = response.Messages[0];
                //    if (message.Status == "success")
                //    {
                //        return true;
                //    }
                //    else
                //    {
                //        return false;
                //    }

                //}

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        /*        public async Task<bool> SendOrderConfirmationEmailAsync(string toEmail, int orderId, decimal orderTotal)
                {
                    var subject = $"Order Confirmation #{orderId} - BulkyBook";

                    // Simple HTML email to demonstrate email functionality
                    var htmlContent = $@"
                        <h1>Thank you for your order!</h1>
                        <p>Your order has been placed successfully.</p>
                        <hr />
                        <p><strong>Order Number:</strong> {orderId}</p>
                        <p><strong>Order Date:</strong> {DateTime.Now:MMMM dd, yyyy}</p>
                        <p><strong>Total Amount:</strong> {orderTotal:C}</p>
                        <hr />
                        <p>Thank you for shopping with BookVertex!</p>
                        <p>- The BookVertex Team</p>";

                    return await SendEmailAsync(toEmail, subject, htmlContent);
                }*/


        public async Task<bool> SendOrderConfirmationEmailAsync( string toEmail, int orderId, decimal orderTotal)
        {
            var subject = $"Order Confirmation #{orderId} - BookVertex";

            var htmlContent = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Order Confirmation</title>
            </head>

            <body style='margin:0; padding:0; background-color:#f4f6f8; font-family:Arial, Helvetica, sans-serif; color:#1f2937;'>

                <table width='100%' cellpadding='0' cellspacing='0' border='0'
                       style='background-color:#f4f6f8; padding:40px 15px;'>
                    <tr>
                        <td align='center'>

                            <table width='600' cellpadding='0' cellspacing='0' border='0'
                                   style='max-width:600px; width:100%; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 4px 18px rgba(0,0,0,0.08);'>

                                <!-- Header -->
                                <tr>
                                    <td style='background:linear-gradient(135deg,#0086d1,#0066a8); padding:28px 30px; text-align:center;'>

                                        <div style='font-size:28px; font-weight:700; color:#ffffff;'>
                                            📚 Book<span style='color:#d9ff00;'>Vertex</span>
                                        </div>

                                        <div style='margin-top:6px; font-size:13px; color:#dbeafe;'>
                                            Your Gateway to Endless Stories
                                        </div>

                                    </td>
                                </tr>

                                <!-- Success -->
                                <tr>
                                    <td style='padding:35px 35px 20px; text-align:center;'>

                                        <div style='width:60px; height:60px; margin:0 auto 18px;
                                                    border-radius:50%; background:#e8f7ee;
                                                    line-height:60px; font-size:28px;'>
                                            ✓
                                        </div>

                                        <h1 style='margin:0; font-size:25px; color:#111827;'>
                                            Thank You for Your Order!
                                        </h1>

                                        <p style='margin:12px 0 0; font-size:15px; line-height:1.6; color:#6b7280;'>
                                            Your order has been placed successfully.
                                            We'll keep you updated as it makes its way to you.
                                        </p>

                                    </td>
                                </tr>

                                <!-- Order Information -->
                                <tr>
                                    <td style='padding:10px 35px 30px;'>

                                        <table width='100%' cellpadding='0' cellspacing='0' border='0'
                                               style='border:1px solid #e5e7eb; border-radius:10px; overflow:hidden;'>

                                            <tr>
                                                <td colspan='2'
                                                    style='background:#f8fafc; padding:15px 18px;
                                                           font-size:13px; font-weight:700;
                                                           color:#374151; text-transform:uppercase;
                                                           letter-spacing:0.5px;'>
                                                    Order Details
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style='padding:15px 18px; color:#6b7280; font-size:14px;
                                                           border-top:1px solid #e5e7eb;'>
                                                    Order Number
                                                </td>

                                                <td align='right'
                                                    style='padding:15px 18px; font-size:14px;
                                                           font-weight:700; border-top:1px solid #e5e7eb;'>
                                                    #${orderId}
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style='padding:15px 18px; color:#6b7280; font-size:14px;
                                                           border-top:1px solid #e5e7eb;'>
                                                    Order Date
                                                </td>

                                                <td align='right'
                                                    style='padding:15px 18px; font-size:14px;
                                                           border-top:1px solid #e5e7eb;'>
                                                    {DateTime.Now:MMMM dd, yyyy}
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style='padding:15px 18px; color:#6b7280; font-size:14px;
                                                           border-top:1px solid #e5e7eb;'>
                                                    Total Amount
                                                </td>

                                                <td align='right'
                                                    style='padding:15px 18px; font-size:18px;
                                                           font-weight:700; color:#0086d1;
                                                           border-top:1px solid #e5e7eb;'>
                                                    {orderTotal:C}
                                                </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>

                                <!-- Message -->
                                <tr>
                                    <td style='padding:0 35px 30px;'>

                                        <div style='background:#f0f9ff; border-radius:10px;
                                                    padding:18px 20px; border-left:4px solid #0086d1;'>

                                            <p style='margin:0; font-size:14px; line-height:1.6; color:#374151;'>
                                                Your order is now being processed. You can view
                                                your order status anytime from your BookVertex account.
                                            </p>

                                        </div>

                                    </td>
                                </tr>

                                <!-- Footer -->
                                <tr>
                                    <td style='background:#111827; padding:25px 30px; text-align:center;'>

                                        <div style='font-size:16px; font-weight:700; color:#ffffff;'>
                                            Book<span style='color:#d9ff00;'>Vertex</span>
                                        </div>

                                        <p style='margin:8px 0 0; font-size:12px; color:#9ca3af;'>
                                            Your Gateway to Endless Stories
                                        </p>

                                        <p style='margin:15px 0 0; font-size:11px; color:#6b7280;'>
                                            Thank you for choosing BookVertex.
                                        </p>

                                        <p style='margin:8px 0 0; font-size:11px; color:#6b7280;'>
                                            © {DateTime.Now.Year} BookVertex. All rights reserved.
                                        </p>

                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>
                </table>

            </body>
            </html>";

            return await SendEmailAsync(toEmail, subject, htmlContent);
        }
    }
}
