// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Restaurante.UI2.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public class InputModel
        {

            [Required(ErrorMessage = "El nombre es requerido")]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "La contraseña es requerida")]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }


            [Required(ErrorMessage = "El correo electronico es requerido")]
            [EmailAddress(ErrorMessage = "Ingrese un correo valido")]
            [Display(Name = "Correo electronico")]
            public string Email { get; set; }

        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email };


                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {


                    string elCorreoElectronicoDelUsuario = user.Email;

                    string elMensajeDeCambioDeContrasena = "Cuenta de usuario creada satisfactoriamente para el usuario " + Input.UserName + ".";

                    string elAsuntoDelCorreo = "Solicitud de creación de usuario";

                    string elCuerpoDelCorreo = "<body><text>" + elMensajeDeCambioDeContrasena + "</text></body>";

                    EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);

                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation(error.Description.ToString() + " " + error.Code.ToString());
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("proyectolenguajes18@gmail.com", "qhqtwqpcfgdfflla");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("proyectolenguajes18@gmail.com", "proyectolenguaje");
            correo.To.Add(new MailAddress(destinatario));
            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = cuerpo;

            smtp.Send(correo);

        }

    }
}
